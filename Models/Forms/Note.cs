﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using Models.Attributes;
using Models.Collections;
using Models.Forms.DataAccess;
using Models.Interfaces;
using OfficeOpenXml;

namespace Models.Forms;

public class Note : IKey, INumberInOrder, IDataGridColumn
{
    public Note()
    {
        Init();
    }
    public Note(string rowNumber, string graphNumber, string comment)
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
    [NotMapped]
    Dictionary<string, RamAccess> Dictionary { get; set; } = new();
    public void Init()
    {
        RowNumber_Validation(RowNumber);
        GraphNumber_Validation(GraphNumber);
        Comment_Validation(Comment);
    }

    public int Id { get; set; }

    public void SetOrder(long index) { }
    public long Order { get;set; }

    #region RowNUmber
    public string? RowNumber_DB { get; set; }
    [NotMapped]
    [FormProperty(false, "№ строки", "1")]
#nullable enable
    public RamAccess<string?> RowNumber
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(RowNumber)))
            {
                ((RamAccess<string?>)Dictionary[nameof(RowNumber)]).Value = RowNumber_DB;
                return (RamAccess<string?>)Dictionary[nameof(RowNumber)];
            }
            else
            {
                var rm = new RamAccess<string?>(RowNumber_Validation, RowNumber_DB);
                rm.PropertyChanged += RowNumberValueChanged;
                Dictionary.Add(nameof(RowNumber), rm);
                return (RamAccess<string?>)Dictionary[nameof(RowNumber)];
            }
        }
        set
        {
            RowNumber_DB = value.Value;
            OnPropertyChanged(nameof(RowNumber));
        }
    }
    private void RowNumberValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            RowNumber_DB = ((RamAccess<string?>)value).Value;
        }
    }
    private bool RowNumber_Validation(RamAccess<string?> value)
    {
        value.ClearErrors();
        return true;
    }
    #endregion

    #region GraphNumber
    public string? GraphNumber_DB { get; set; }
    [NotMapped]
    [FormProperty(false, "№ графы", "2")]
#nullable enable
    public RamAccess<string?> GraphNumber
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(GraphNumber)))
            {
                ((RamAccess<string?>)Dictionary[nameof(GraphNumber)]).Value = GraphNumber_DB;
                return (RamAccess<string?>)Dictionary[nameof(GraphNumber)];
            }
            else
            {
                var rm = new RamAccess<string?>(GraphNumber_Validation, GraphNumber_DB);
                rm.PropertyChanged += GraphNumberValueChanged;
                Dictionary.Add(nameof(GraphNumber), rm);
                return (RamAccess<string?>)Dictionary[nameof(GraphNumber)];
            }
        }
        set
        {
            GraphNumber_DB = value.Value;
            OnPropertyChanged(nameof(GraphNumber));
        }
    }
    private void GraphNumberValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            GraphNumber_DB = ((RamAccess<string?>)value).Value;
        }
    }
    private bool GraphNumber_Validation(RamAccess<string?> value)
    {
        value.ClearErrors();
        return true;
    }
    #endregion

    #region Comment
    public string? Comment_DB { get; set; } = "";
    [NotMapped]
    [FormProperty(false, "Пояснение", "3")]
    public RamAccess<string> Comment
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(Comment)))
            {
                ((RamAccess<string>)Dictionary[nameof(Comment)]).Value = Comment_DB;
                return (RamAccess<string>)Dictionary[nameof(Comment)];
            }
            else
            {
                var rm = new RamAccess<string>(Comment_Validation, Comment_DB);
                rm.PropertyChanged += CommentValueChanged;
                Dictionary.Add(nameof(Comment), rm);
                return (RamAccess<string>)Dictionary[nameof(Comment)];
            }
        }
        set
        {
            Comment_DB = value.Value;
            OnPropertyChanged(nameof(Comment));
        }
    }
    private void CommentValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            Comment_DB = ((RamAccess<string>)value).Value;
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
    public void ExcelGetRow(ExcelWorksheet worksheet, int row)
    {
        RowNumber_DB = Convert.ToString(worksheet.Cells[row, 1].Value);
        GraphNumber_DB = Convert.ToString(worksheet.Cells[row, 2].Value);
        Comment_DB = Convert.ToString(worksheet.Cells[row, 3].Value);
    }
    public int ExcelRow(ExcelWorksheet worksheet, int row, int column, bool transpon = true, string sumNumber = "")
    {
        worksheet.Cells[row + 0, column + 0].Value = RowNumber_DB;
        worksheet.Cells[row + (!transpon ? 1 : 0), column + (transpon ? 1 : 0)].Value = GraphNumber_DB;
        worksheet.Cells[row + (!transpon ? 2 : 0), column + (transpon ? 2 : 0)].Value = Comment_DB;
        return 3;
    }

    public static int ExcelHeader(ExcelWorksheet worksheet, int row, int column, bool transpon = true)
    {
        worksheet.Cells[row + 0, column + 0].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Note,Models").GetProperty(nameof(RowNumber)).GetCustomAttributes(typeof(FormPropertyAttribute), false).First()).Names[0];
        worksheet.Cells[row + (!transpon ? 1 : 0), column + (transpon ? 1 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Note,Models").GetProperty(nameof(GraphNumber)).GetCustomAttributes(typeof(FormPropertyAttribute), false).First()).Names[0];
        worksheet.Cells[row + (!transpon ? 2 : 0), column + (transpon ? 2 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Note,Models").GetProperty(nameof(Comment)).GetCustomAttributes(typeof(FormPropertyAttribute), false).First()).Names[0];
        return 3;
    }
    #endregion

    #region IDataGridColumn
    public DataGridColumns GetColumnStructure(string param = "")
    {

        var RowNumberN = ((FormPropertyAttribute)typeof(Note).GetProperty(nameof(RowNumber)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
        RowNumberN.SizeCol = 100;
        RowNumberN.Binding = nameof(RowNumber);
            
        var GraphNumberN = ((FormPropertyAttribute)typeof(Note).GetProperty(nameof(GraphNumber)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
        GraphNumberN.SizeCol = 100;
        GraphNumberN.Binding = nameof(GraphNumber);
        RowNumberN += GraphNumberN;

        var CommentN = ((FormPropertyAttribute)typeof(Note).GetProperty(nameof(Comment)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
        CommentN.SizeCol = 660;
        CommentN.Binding = nameof(Comment);
        CommentN.IsTextWrapping = true;
        RowNumberN += CommentN;

        return RowNumberN;
    }
    #endregion
}