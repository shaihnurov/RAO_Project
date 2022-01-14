﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Collections;
using OfficeOpenXml;
using Models.Abstracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Models.Collections
{
    public class ObservableCollectionWithItemPropertyChanged<T> : ObservableCollection<T>, IKey
        where T : IKey
    {
        /// <summary>
        /// Occurs when a property is changed within an item.
        /// </summary>
        public event EventHandler<ItemPropertyChangedEventArgs> ItemPropertyChanged;

        [NotMapped]
        public int Order
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Id { get; set; }

        public ObservableCollectionWithItemPropertyChanged() : base()
        {
        }

        public void CleanIds()
        {
            foreach (var item in Items)
            {
                item.Id = 0;
            }
        }

        public ObservableCollectionWithItemPropertyChanged(List<T> list) : base(list)
        {
            ObserveAll();
        }

        public ObservableCollectionWithItemPropertyChanged(IEnumerable<T> enumerable) : base(enumerable)
        {
            ObserveAll();
        }

        public bool Sorted { get; set; } = false;
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (T item in e.OldItems)
                {
                    item.PropertyChanged -= ChildPropertyChanged;
                    Sorted = false;
                }

            }

            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (T item in e.NewItems)
                {
                    if (item != null)
                    {
                        item.PropertyChanged += ChildPropertyChanged;
                        Sorted = false;
                    }
                }
            }

            base.OnCollectionChanged(e);
        }

        //метод для обмена элементов массива
        void Swap(int index1,int index2)
        {
            var t = Items[index1];
            Items[index1] = Items[index2];
            Items[index2] = t;
        }
        int Partition(int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {

                if (Items[i].Order < Items[maxIndex].Order)
                {
                    pivot++;
                    Swap(pivot, i);
                }
            }

            pivot++;
            Swap(pivot, maxIndex);
            return pivot;
        }
        void QuickSort(int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return;
            }

            var pivotIndex = Partition( minIndex, maxIndex);
            QuickSort(minIndex, pivotIndex - 1);
            QuickSort(pivotIndex + 1, maxIndex);
        }

        public void QuickSort()
        {
            if (!Sorted)
            {
                try
                {
                    if (!CheckForSort())
                    {
                        QuickSort(0, Items.Count - 1);
                        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        Sorted = true;
                    }
                }
                catch
                {

                }
            }
        }

        public bool CheckForSort()
        {
            int count = 1;
            bool flag = true;
            foreach(var item in Items)
            {
                if(item.Order != count)
                {
                    flag = false;
                    break;
                }
                count++;
            }

            return flag;
        }

        protected void OnItemPropertyChanged(ItemPropertyChangedEventArgs e)
        {
            ItemPropertyChanged?.Invoke(this, e);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected void OnItemPropertyChanged(int index, PropertyChangedEventArgs e)
        {
            OnItemPropertyChanged(new ItemPropertyChangedEventArgs(index, e));
        }

        protected override void ClearItems()
        {
            foreach (T item in Items)
                item.PropertyChanged -= ChildPropertyChanged;

            base.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            this[index].PropertyChanged -= ChildPropertyChanged;
            base.RemoveItem(index);
        }

        protected void ObserveAll()
        {
            foreach (T item in Items)
                item.PropertyChanged += ChildPropertyChanged;

            QuickSort();

        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged += ChildPropertyChanged;
                Items.Add(item);
            }
            Sorted = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public void AddRangeNoChange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged += ChildPropertyChanged;
                Items.Add(item);
            }
        }

        protected void ChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            T typedSender = (T) sender;
            int i = Items.IndexOf(typedSender);

            if (i < 0)
                throw new ArgumentException("Received property notification from item not in collection");

            OnItemPropertyChanged(i, e);
        }

        #region IExcel
        public int ExcelRow(ExcelWorksheet worksheet, int Row,int Column,bool Tanspon=true)
        {
            throw new System.NotImplementedException();
        }

        public int ExcelHeader(ExcelWorksheet worksheet, int Row,int Column,bool Transpon=true)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}