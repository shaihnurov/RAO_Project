﻿using Collections;
using Models.DataAccess;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using Collections;
using Models.Attributes;
using OfficeOpenXml;

namespace Models
{
    public class Note : IKey
    {
        public Note()
        {
            Init();
        }
        public Note(int rowNumber, int graphNumber, string comment)
        {
            RowNumber.Value = rowNumber;
            GraphNumber.Value = graphNumber;
            Comment.Value = comment;
            Init();
        }
        protected void InPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged(args.PropertyName);
        }
        public void Init()
        {
            RowNumber.PropertyChanged += InPropertyChanged;
            GraphNumber.PropertyChanged += InPropertyChanged;
            Comment.PropertyChanged += InPropertyChanged;
            RowNumber_Validation(RowNumber);
            GraphNumber_Validation(GraphNumber);
            Comment_Validation(Comment);
        }

        public int Id { get; set; }

        #region RowNUmber
        public int? RowNumber_DB { get; set; } = null;
        [NotMapped]
        [Attributes.Form_Property("Номер строки")]
        public RamAccess<int?> RowNumber
        {
            get
            {
                var tmp = new RamAccess<int?>(RowNumber_Validation, RowNumber_DB);
                tmp.PropertyChanged += RowNumberValueChanged;
                return tmp;
            }
            set
            {
                RowNumber_DB = value.Value;
                OnPropertyChanged(nameof(RowNumber));
            }
        }
        private void RowNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                RowNumber_DB = ((RamAccess<int?>)Value).Value;
            }
        }
        private bool RowNumber_Validation(RamAccess<int?> value)
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        #region GraphNumber
        public int? GraphNumber_DB { get; set; } = null;
        [NotMapped]
        [Attributes.Form_Property("Номер графы")]
        public RamAccess<int?> GraphNumber
        {
            get
            {
                var tmp = new RamAccess<int?>(GraphNumber_Validation, GraphNumber_DB);
                tmp.PropertyChanged += GraphNumberValueChanged;
                return tmp;
            }
            set
            {
                GraphNumber_DB = value.Value;
                OnPropertyChanged(nameof(GraphNumber));
            }
        }
        private void GraphNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                GraphNumber_DB = ((RamAccess<int?>)Value).Value;
            }
        }
        private bool GraphNumber_Validation(RamAccess<int?> value)
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        #region Comment
        public string Comment_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property("Примечание")]
        public RamAccess<string> Comment
        {
            get
            {
                var tmp = new RamAccess<string>(Comment_Validation, Comment_DB);
                tmp.PropertyChanged += CommentValueChanged;
                return tmp;
            }
            set
            {
                Comment_DB = value.Value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        private void CommentValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Comment_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool Comment_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        //Для валидации
        public bool Object_Validation()
        {
            return true;
        }
        //Для валидации

        //Property Changed
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        //Property Changed
        #region IExcel
        public void ExcelRow(ExcelWorksheet worksheet, int Row)
        {
            worksheet.Cells[Row, 1].Value = RowNumber_DB;
            worksheet.Cells[Row, 2].Value = GraphNumber_DB;
            worksheet.Cells[Row, 3].Value = Comment_DB;
        }

        public static void ExcelHeader(ExcelWorksheet worksheet)
        {
            worksheet.Cells[1, 2].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Note,Models").GetProperty(nameof(RowNumber)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Name;
            worksheet.Cells[1, 3].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Note,Models").GetProperty(nameof(GraphNumber)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Name;
            worksheet.Cells[1, 4].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Note,Models").GetProperty(nameof(Comment)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Name;
        }
        #endregion
    }
}
