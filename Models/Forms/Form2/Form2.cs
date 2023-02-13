﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Models.Attributes;
using Models.Forms.DataAccess;
using OfficeOpenXml;

namespace Models.Forms.Form2;

public abstract class Form2 : Form
{
    [FormProperty(true, "Форма")]
    public Form2()
    {
    }

    protected void InPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        OnPropertyChanged(args.PropertyName);
    }

    #region CorrectionNumber

    public byte CorrectionNumber_DB { get; set; }

    [NotMapped]
    [FormProperty(true, "Номер корректировки")]
    public RamAccess<byte> CorrectionNumber
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(CorrectionNumber)))
            {
                ((RamAccess<byte>)Dictionary[nameof(CorrectionNumber)]).Value = CorrectionNumber_DB;
                return (RamAccess<byte>)Dictionary[nameof(CorrectionNumber)];
            }

            var rm = new RamAccess<byte>(CorrectionNumber_Validation, CorrectionNumber_DB);
            rm.PropertyChanged += CorrectionNumberValueChanged;
            Dictionary.Add(nameof(CorrectionNumber), rm);
            return (RamAccess<byte>)Dictionary[nameof(CorrectionNumber)];
        }
        set
        {
            CorrectionNumber_DB = value.Value;
            OnPropertyChanged();
        }
    }

    private void CorrectionNumberValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            CorrectionNumber_DB = ((RamAccess<byte>)Value).Value;
        }
    }

    private bool CorrectionNumber_Validation(RamAccess<byte> value)
    {
        value.ClearErrors();
        return true;
    }

    #endregion

    #region IExcel

    public override void ExcelGetRow(ExcelWorksheet worksheet, int row)
    {
        NumberInOrder_DB = int.TryParse(Convert.ToString(worksheet.Cells[row, 1].Value), out var intValue)
            ? intValue
            : 0;
    }

    public override int ExcelRow(ExcelWorksheet worksheet, int row, int column, bool transpose = true,
        string sumNumber = "")
    {
        worksheet.Cells[row, column].Value = NumberInOrder_DB == 0
            ? sumNumber
            : NumberInOrder_DB;

        return 1;
    }

    protected static int ExcelHeader(ExcelWorksheet worksheet, int row, int column, bool transpose = true)
    {
        worksheet.Cells[row, column].Value = ((FormPropertyAttribute)typeof(Form).GetProperty(nameof(NumberInOrder))
            ?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[2];

        return 1;
    }

    #endregion
}