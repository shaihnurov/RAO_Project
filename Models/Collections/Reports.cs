﻿using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using Models.DBRealization;
using Models;
using Models.DataAccess;
using OfficeOpenXml;
using System;
using System.Threading.Tasks;

namespace Models.Collections
{

    public class Reports : IKey,INumberInOrder,IDataGridColumn
    {
        [NotMapped]
        public long Order
        {
            get
            {
                try
                {
                    var num_str = Master_DB.RegNoRep.Value.Substring(0, 5);
                    var num_int = Convert.ToInt64(num_str);
                    return num_int;
                }
                catch 
                {
                    return 0;
                }
                //throw new NotImplementedException();
            }
        }
        public Reports()
        {
            Init();
        }
        private void Init()
        {

            Report_Collection = new ObservableCollectionWithItemPropertyChanged<Report>();
            Report_Collection.CollectionChanged += CollectionChanged;
        }

        public Report Master_DB { get; set; }

        [NotMapped]
        public Report Master
        {
            get
            {
                return Master_DB;
            }
            set
            {
                Master_DB = value;
                OnPropertyChanged(nameof(Master));
            }
        }

        ObservableCollectionWithItemPropertyChanged<Report> Report_Collection_DB;

        public ObservableCollectionWithItemPropertyChanged<Report> Report_Collection
        {
            get
            {
                return Report_Collection_DB;
            }
            set
            {
                Report_Collection_DB = value;
                OnPropertyChanged(nameof(Report_Collection));
            }
        }

        public void Sort()
        {
            Report_Collection.QuickSort();
        }
        public async Task SortAsync()
        {
            await Report_Collection.QuickSortAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }

        public void SetOrder(long index) { }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            OnPropertyChanged(nameof(Report_Collection));
        }

        public void CleanIds()
        {
            Id = 0;
            Master.CleanIds();
            foreach (Report item in Report_Collection)
            {
                item.CleanIds();
            }
        }

        private bool Master_Validation(RamAccess<Report> value)
        {
            return true;
        }

        private bool Report_Collection_Validation(RamAccess<ObservableCollectionWithItemPropertyChanged<Report>> value)
        {
            return true;
        }

        //Property Changed
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        //Property Changed

        public int ExcelRow(ExcelWorksheet worksheet,int Row, int Column, bool Tanspon = true)
        {
            throw new System.NotImplementedException();
        }

        public int ExcelHeader(ExcelWorksheet worksheet, int Row,int Column,bool Transpon=true)
        {
            throw new System.NotImplementedException();
        }

        #region IDataGridColumn
        public DataGridColumns GetColumnStructure(string param = "")
        {
            DataGridColumns regNoR = ((Attributes.Form_PropertyAttribute)typeof(Form10).GetProperty(nameof(Form10.RegNo)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
            regNoR.SizeCol = 80;
            regNoR.Binding = nameof(Reports.Master)+"."+nameof(Report.RegNoRep);

            DataGridColumns ShortJurLicoR = ((Attributes.Form_PropertyAttribute)typeof(Form10).GetProperty(nameof(Form10.ShortJurLico)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
            ShortJurLicoR.SizeCol = 305;
            ShortJurLicoR.Binding = nameof(Reports.Master) + "." + nameof(Report.ShortJurLicoRep);
            regNoR += ShortJurLicoR;

            DataGridColumns okpoR = ((Attributes.Form_PropertyAttribute)typeof(Form10).GetProperty(nameof(Form10.Okpo)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
            okpoR.SizeCol = 62;
            okpoR.Binding = nameof(Reports.Master) + "." + nameof(Report.OkpoRep);
            regNoR += okpoR;

            return regNoR;
        }
        #endregion
    }
}