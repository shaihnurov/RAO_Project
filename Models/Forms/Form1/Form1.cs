﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Models.Attributes;
using Models.Collections;
using Models.Forms.DataAccess;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Spravochniki;

namespace Models.Forms.Form1;

public abstract class Form1 : Form
{
    [FormProperty(true,"Форма")]

    [NotMapped]
    public bool flag = false;

    protected void InPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        OnPropertyChanged(args.PropertyName);
    }

    public DataGridColumns GetDataGridColumn() 
    {
        return null;
    } 

    #region OperationCode
    public string OperationCode_DB { get; set; } = "";
    public bool OperationCode_Hidden_Priv { get; set; }
    [NotMapped]
    public bool OperationCode_Hidden
    {
        get => OperationCode_Hidden_Priv;
        set
        {
            OperationCode_Hidden_Priv = value;
        }
    }

    [NotMapped]
    [FormProperty(true, "Сведения об операции","код","2")]
    public RamAccess<string> OperationCode
    {
        get
        {
            if (!OperationCode_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(OperationCode)))
                {
                    ((RamAccess<string>)Dictionary[nameof(OperationCode)]).Value = OperationCode_DB;
                    return (RamAccess<string>)Dictionary[nameof(OperationCode)];
                }
                else
                {
                    var rm = new RamAccess<string>(OperationCode_Validation, OperationCode_DB);
                    rm.PropertyChanged += OperationCodeValueChanged;
                    Dictionary.Add(nameof(OperationCode), rm);
                    return (RamAccess<string>)Dictionary[nameof(OperationCode)];
                }
            }
            else
            {
                var tmp = new RamAccess<string>(null, null);
                return tmp;
            }
        }
        set
        {
            if (!OperationCode_Hidden_Priv)
            {
                OperationCode_DB = value.Value;
                OnPropertyChanged();
            }
        }
    }
    private void OperationCodeValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            OperationCode_DB = ((RamAccess<string>)value).Value;
        }
    }
    protected virtual bool OperationCode_Validation(RamAccess<string> value)//Ready
    {
        value.ClearErrors();
        return true;
    }
    #endregion

    #region OperationDate
    public string OperationDate_DB { get; set; } = "";
    public bool OperationDate_Hidden_Priv { get; set; }
    [NotMapped]
    public bool OperationDate_Hidden
    {
        get => OperationDate_Hidden_Priv;
        set
        {
            OperationDate_Hidden_Priv = value;
        }
    }

    [NotMapped]
    [FormProperty(true, "Сведения об операции", "дата", "3")]
    public RamAccess<string> OperationDate
    {
        get
        {
            if (!OperationDate_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(OperationDate)))
                {
                    ((RamAccess<string>)Dictionary[nameof(OperationDate)]).Value = OperationDate_DB;
                    return (RamAccess<string>)Dictionary[nameof(OperationDate)];
                }
                else
                {
                    var rm = new RamAccess<string>(OperationDate_Validation, OperationDate_DB);
                    rm.PropertyChanged += OperationDateValueChanged;
                    Dictionary.Add(nameof(OperationDate), rm);
                    return (RamAccess<string>)Dictionary[nameof(OperationDate)];
                }
            }
            else
            {
                var tmp = new RamAccess<string>(null, null);
                return tmp;
            }
        }
        set
        {
            if (!OperationDate_Hidden_Priv)
            {
                OperationDate_DB = value.Value;
                OnPropertyChanged();
            }
        }
    }
    private void OperationDateValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var tmp = ((RamAccess<string>)value).Value;
            if (new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$").IsMatch(tmp))
            {
                tmp = tmp.Insert(6, "20");
            }
            OperationDate_DB = tmp;
        }
    }

    protected virtual bool OperationDate_Validation(RamAccess<string> value)//Ready
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        var tmp = value.Value;
        
        if (!new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$").IsMatch(tmp) || !DateTimeOffset.TryParse(tmp, out _))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }
    #endregion

    #region DocumentVid
        
    public byte? DocumentVid_DB { get; set; }
    public bool DocumentVid_Hidden_Priv { get; set; }
    [NotMapped]
    public bool DocumentVid_Hidden
    {
        get => DocumentVid_Hidden_Priv;
        set
        {
            DocumentVid_Hidden_Priv = value;
        }
    }

    [NotMapped]
    [FormProperty(true,"Документ","вид", "16")]
    public RamAccess<byte?> DocumentVid
    {
        get
        {
            if (!DocumentVid_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(DocumentVid)))
                {
                    ((RamAccess<byte?>)Dictionary[nameof(DocumentVid)]).Value = DocumentVid_DB;
                    return (RamAccess<byte?>)Dictionary[nameof(DocumentVid)];
                }
                else
                {
                    var rm = new RamAccess<byte?>(DocumentVid_Validation, DocumentVid_DB);
                    rm.PropertyChanged += DocumentVidValueChanged;
                    Dictionary.Add(nameof(DocumentVid), rm);
                    return (RamAccess<byte?>)Dictionary[nameof(DocumentVid)];
                }
            }
            else
            {
                var tmp = new RamAccess<byte?>(null, null);
                return tmp;
            }
        }
        set
        {
            DocumentVid_DB = value.Value;
            OnPropertyChanged();
        }
    }

    private void DocumentVidValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            DocumentVid_DB = ((RamAccess<byte?>)Value).Value;
        }
    }

    protected virtual bool DocumentVid_Validation(RamAccess<byte?> value)//Ready
    {
        value.ClearErrors();
        if (value.Value == null)
        {
            value.AddError("Поле не заполнено");
            return false;
        }

        if (Spravochniks.SprDocumentVidName.Any(item => value.Value == item.Item1))
        {
            return true;
        }
        value.AddError("Недопустимое значение");
        return false;
    }
    #endregion

    #region DocumentNumber
    public string DocumentNumber_DB { get; set; } = "";
    public bool DocumentNumber_Hidden_Priv { get; set; }
    [NotMapped]
    public bool DocumentNumber_Hidden
    {
        get => DocumentNumber_Hidden_Priv;
        set
        {
            DocumentNumber_Hidden_Priv = value;
        }
    }

    [NotMapped]
    [FormProperty(true,"Документ", "номер", "17")]
    public RamAccess<string> DocumentNumber
    {
        get
        {
            if (!DocumentNumber_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(DocumentNumber)))
                {
                    ((RamAccess<string>)Dictionary[nameof(DocumentNumber)]).Value = DocumentNumber_DB;
                    return (RamAccess<string>)Dictionary[nameof(DocumentNumber)];
                }
                else
                {
                    var rm = new RamAccess<string>(DocumentNumber_Validation, DocumentNumber_DB);
                    rm.PropertyChanged += DocumentNumberValueChanged;
                    Dictionary.Add(nameof(DocumentNumber), rm);
                    return (RamAccess<string>)Dictionary[nameof(DocumentNumber)];
                }
            }
            else
            {
                var tmp = new RamAccess<string>(null, null);
                return tmp;
            }
        }
        set
        {
            if (!DocumentNumber_Hidden_Priv)
            {
                DocumentNumber_DB = value.Value;
                OnPropertyChanged();
            }
        }
    }
    private void DocumentNumberValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            DocumentNumber_DB = ((RamAccess<string>)value).Value;
        }
    }
    protected virtual bool DocumentNumber_Validation(RamAccess<string> value)//Ready
    { return true; }
    #endregion

    #region DocumentDate
    public string DocumentDate_DB { get; set; } = "";
    public bool DocumentDate_Hidden_Priv { get; set; }
    [NotMapped]
    public bool DocumentDate_Hidden
    {
        get => DocumentDate_Hidden_Priv;
        set
        {
            DocumentDate_Hidden_Priv = value;
        }
    }

    [NotMapped]
    [FormProperty(true,"Документ", "дата", "18")]
    public RamAccess<string> DocumentDate
    {
        get
        {
            if (!DocumentDate_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(DocumentDate)))
                {
                    ((RamAccess<string>)Dictionary[nameof(DocumentDate)]).Value = DocumentDate_DB;
                    return (RamAccess<string>)Dictionary[nameof(DocumentDate)];
                }
                else
                {
                    var rm = new RamAccess<string>(DocumentDate_Validation, DocumentDate_DB);
                    rm.PropertyChanged += DocumentDateValueChanged;
                    Dictionary.Add(nameof(DocumentDate), rm);
                    return (RamAccess<string>)Dictionary[nameof(DocumentDate)];
                }
            }
            else
            {
                var tmp = new RamAccess<string>(null, null);
                return tmp;
            }
        }
        set
        {
            if (!DocumentDate_Hidden_Priv)
            {
                DocumentDate_DB = value.Value;
                OnPropertyChanged();
            }
        }
    }
    private void DocumentDateValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var tmp = ((RamAccess<string>)value).Value;
            if (new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$").IsMatch(tmp))
            {
                tmp = tmp.Insert(6, "20");
            }
            DocumentDate_DB = tmp;
        }
    }

    protected virtual bool DocumentDate_Validation(RamAccess<string> value)//Ready
    {
        value.ClearErrors();
        if(string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        var tmp = value.Value;
        
        if (!new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$").IsMatch(tmp) || !DateTimeOffset.TryParse(tmp, out _))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        
        return true;
    }
    #endregion

    #region IExcel

    public override void ExcelGetRow(ExcelWorksheet worksheet, int row)
    {
        NumberInOrder_DB = int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out var intValue)
            ? intValue
            : 0;
        OperationCode_DB = Convert.ToString(worksheet.Cells[row, 2].Value);
        OperationDate_DB = ConvertFromExcelDate(worksheet.Cells[row, 3].Text);
    }

    public override int ExcelRow(ExcelWorksheet worksheet, int row, int column, bool transpose = true, string sumNumber = "")
    {
        worksheet.Cells[row, column].Value = NumberInOrder_DB;
        worksheet.Cells[row + (!transpose ? 1 : 0), column + (transpose ? 1 : 0)].Value = OperationCode_DB;
        worksheet.Cells[row + (!transpose ? 2 : 0), column + (transpose ? 2 : 0)].Value = ConvertToExcelDate(OperationDate_DB);
        return 3;
    }

    protected static int ExcelHeader(ExcelWorksheet worksheet, int row, int column, bool transpose = true)
    {
        worksheet.Cells[row, column].Value =
            ((FormPropertyAttribute)typeof(Form).GetProperty(nameof(NumberInOrder))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[2];     
        worksheet.Cells[row + (!transpose ? 1 : 0), column + (transpose ? 1 : 0)].Value =
            ((FormPropertyAttribute)typeof(Form1).GetProperty(nameof(OperationCode))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];     
        worksheet.Cells[row + (!transpose ? 2 : 0), column + (transpose ? 2 : 0)].Value =
            ((FormPropertyAttribute)typeof(Form1).GetProperty(nameof(OperationDate))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        return 3;
    }

    #endregion
}