﻿using Models.Collections;
using Models.DataAccess;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using OfficeOpenXml;

namespace Models.Abstracts
{
    public abstract class Form : INotifyPropertyChanged, IKey
    {
        public int Id { get; set; }

        #region FormNum
        public string FormNum_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property("Форма")]
        public RamAccess<string> FormNum
        {
            get
            {
                var tmp = new RamAccess<string>(FormNum_Validation, FormNum_DB);
                tmp.PropertyChanged += FormNumValueChanged;
                return tmp;
            }
            set
            {
                FormNum_DB = value.Value;
                OnPropertyChanged(nameof(FormNum));
            }
        }
        private void FormNumValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                FormNum_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool FormNum_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        #region NumberInOrder
        public int NumberInOrder_DB { get; set; } = 0;

        [NotMapped]
        [Attributes.Form_Property("№ п/п")]
        public RamAccess<int> NumberInOrder
        {
            get
            {
                var tmp = new RamAccess<int>(NumberInOrder_Validation, NumberInOrder_DB);
                tmp.PropertyChanged += NumberInOrderValueChanged;
                return tmp;
            }
            set
            {
                NumberInOrder_DB = value.Value;
                OnPropertyChanged(nameof(NumberInOrder));
            }
        }
        private void NumberInOrderValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                NumberInOrder_DB = ((RamAccess<int>)Value).Value;
            }
        }
        private bool NumberInOrder_Validation(RamAccess<int> value)//Ready
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        #region NumberOfFields
        public int NumberOfFields_DB { get; set; } = 0;
        [NotMapped]
        [Attributes.Form_Property("Число полей")]
        public RamAccess<int> NumberOfFields
        {
            get
            {
                var tmp = new RamAccess<int>(NumberOfFields_Validation, NumberOfFields_DB);
                tmp.PropertyChanged += NumberOfFieldsValueChanged;
                return tmp;
            }
            set
            {
                NumberOfFields_DB = value.Value;
                OnPropertyChanged(nameof(NumberOfFields));
            }
        }
        private void NumberOfFieldsValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                NumberOfFields_DB = ((RamAccess<int>)Value).Value;
            }
        }
        private bool NumberOfFields_Validation(RamAccess<int> value)
        {
            value.ClearErrors();
            return true;

        }
        #endregion

        #region For_Validation
        public abstract bool Object_Validation();
        #endregion

        #region INotifyPropertyChanged
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region IExcel
        public int ExcelRow(ExcelWorksheet worksheet, int Row, int Column, bool Tanspon = true)
        {
            return 0;
        }

        public static int ExcelHeader(ExcelWorksheet worksheet, int Row,int Column,bool Transpon=true)
        {
            return 0;
        }
        #endregion
    }
}
