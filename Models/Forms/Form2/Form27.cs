﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using Models.Attributes;
using Models.Collections;
using Models.Forms.DataAccess;
using OfficeOpenXml;
using Spravochniki;

namespace Models.Forms.Form2;

[Serializable]
[Form_Class("Форма 2.7: Поступление радионуклидов в атмосферный воздух")]
public class Form27 : Form2
{
    public Form27() : base()
    {
        FormNum.Value = "2.7";
        //NumberOfFields.Value = 13;
        Validate_all();
    }
    private void Validate_all()
    {
        ObservedSourceNumber_Validation(ObservedSourceNumber);
        RadionuclidName_Validation(RadionuclidName);
        AllowedWasteValue_Validation(AllowedWasteValue);
        FactedWasteValue_Validation(FactedWasteValue);
        WasteOutbreakPreviousYear_Validation(WasteOutbreakPreviousYear);
    }

    [FormProperty(true,"Форма")]
    public override bool Object_Validation()
    {
        return !(ObservedSourceNumber.HasErrors||
                 RadionuclidName.HasErrors||
                 AllowedWasteValue.HasErrors||
                 FactedWasteValue.HasErrors||
                 WasteOutbreakPreviousYear.HasErrors);
    }

    //ObservedSourceNumber property
    #region  ObservedSourceNumber
    public string ObservedSourceNumber_DB { get; set; } = "";
    [NotMapped]
    [FormProperty(true,"null-2","Наименование, номер источника выбросов","2")]
    public RamAccess<string> ObservedSourceNumber
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(ObservedSourceNumber)))
            {
                ((RamAccess<string>)Dictionary[nameof(ObservedSourceNumber)]).Value = ObservedSourceNumber_DB;
                return (RamAccess<string>)Dictionary[nameof(ObservedSourceNumber)];
            }
            else
            {
                var rm = new RamAccess<string>(ObservedSourceNumber_Validation, ObservedSourceNumber_DB);
                rm.PropertyChanged += ObservedSourceNumberValueChanged;
                Dictionary.Add(nameof(ObservedSourceNumber), rm);
                return (RamAccess<string>)Dictionary[nameof(ObservedSourceNumber)];
            }
        }
        set
        {
            ObservedSourceNumber_DB = value.Value;
            OnPropertyChanged(nameof(ObservedSourceNumber));
        }
    }
    //If change this change validation
    private void ObservedSourceNumberValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            ObservedSourceNumber_DB = ((RamAccess<string>)Value).Value;
        }
    }
    private bool ObservedSourceNumber_Validation(RamAccess<string> value)//Ready
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        return true;
    }
    //ObservedSourceNumber property
    #endregion

    //RadionuclidName property
    #region  RadionuclidName
    public string RadionuclidName_DB { get; set; } = "";
    [NotMapped]
    [FormProperty(true,"null-3","Наименование радионуклида","3")]
    public RamAccess<string> RadionuclidName
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(RadionuclidName)))
            {
                ((RamAccess<string>)Dictionary[nameof(RadionuclidName)]).Value = RadionuclidName_DB;
                return (RamAccess<string>)Dictionary[nameof(RadionuclidName)];
            }
            else
            {
                var rm = new RamAccess<string>(RadionuclidName_Validation, RadionuclidName_DB);
                rm.PropertyChanged += RadionuclidNameValueChanged;
                Dictionary.Add(nameof(RadionuclidName), rm);
                return (RamAccess<string>)Dictionary[nameof(RadionuclidName)];
            }
        }
        set
        {
            RadionuclidName_DB = value.Value;
            OnPropertyChanged(nameof(RadionuclidName));
        }
    }


    private void RadionuclidNameValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            RadionuclidName_DB = ((RamAccess<string>)Value).Value;
        }
    }
    private bool RadionuclidName_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        var tmpstr = value.Value.ToLower().Replace(" ", "");
        var query = from item in Spravochniks.SprRadionuclids where item.Item1 == tmpstr select item.Item1;
        if (!query.Any())
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }
    //RadionuclidName property
    #endregion

    //AllowedWasteValue property
    #region  AllowedWasteValue
    public string AllowedWasteValue_DB { get; set; } = "";
    [NotMapped]
    [FormProperty(true,"Выброс радионуклида в атмосферу за отчетный год, Бк", "разрешенный","4")]
    public RamAccess<string> AllowedWasteValue
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(AllowedWasteValue)))
            {
                ((RamAccess<string>)Dictionary[nameof(AllowedWasteValue)]).Value = AllowedWasteValue_DB;
                return (RamAccess<string>)Dictionary[nameof(AllowedWasteValue)];
            }
            else
            {
                var rm = new RamAccess<string>(AllowedWasteValue_Validation, AllowedWasteValue_DB);
                rm.PropertyChanged += AllowedWasteValueValueChanged;
                Dictionary.Add(nameof(AllowedWasteValue), rm);
                return (RamAccess<string>)Dictionary[nameof(AllowedWasteValue)];
            }
        }
        set
        {
            AllowedWasteValue_DB = value.Value;
            OnPropertyChanged(nameof(AllowedWasteValue));
        }
    }


    private void AllowedWasteValueValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var value1 = ((RamAccess<string>)Value).Value;
            if (value1 != null)
            {
                value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                if (value1.Equals("-"))
                {
                    AllowedWasteValue_DB = value1;
                    return;
                }
                if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
                {
                    value1 = value1.Replace("+", "e+").Replace("-", "e-");
                }
                try
                {
                    var value2 = Convert.ToDouble(value1);
                    value1 = $"{value2:0.######################################################e+00}";
                }
                catch (Exception ex)
                { }
            }
            AllowedWasteValue_DB = value1;
        }
    }
    private bool AllowedWasteValue_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        if (value.Value.Equals("прим."))
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
                     NumberStyles.AllowExponent;
        try
        {
            if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
        }
        catch
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }
    //AllowedWasteValue property
    #endregion

    //FactedWasteValue property
    #region  FactedWasteValue
    public string FactedWasteValue_DB { get; set; } = "";
    [NotMapped]
    [FormProperty(true,"Выброс радионуклида в атмосферу за отчетный год, Бк", "фактический","5")]
    public RamAccess<string> FactedWasteValue
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(FactedWasteValue)))
            {
                ((RamAccess<string>)Dictionary[nameof(FactedWasteValue)]).Value = FactedWasteValue_DB;
                return (RamAccess<string>)Dictionary[nameof(FactedWasteValue)];
            }
            else
            {
                var rm = new RamAccess<string>(FactedWasteValue_Validation, FactedWasteValue_DB);
                rm.PropertyChanged += FactedWasteValueValueChanged;
                Dictionary.Add(nameof(FactedWasteValue), rm);
                return (RamAccess<string>)Dictionary[nameof(FactedWasteValue)];
            }
        }
        set
        {
            FactedWasteValue_DB = value.Value;
            OnPropertyChanged(nameof(FactedWasteValue));
        }
    }


    private void FactedWasteValueValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var value1 = ((RamAccess<string>)Value).Value;
            if (value1 != null)
            {
                value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                if (value1.Equals("-"))
                {
                    FactedWasteValue_DB = value1;
                    return;
                }
                if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
                {
                    value1 = value1.Replace("+", "e+").Replace("-", "e-");
                }
                try
                {
                    var value2 = Convert.ToDouble(value1);
                    value1 = $"{value2:0.######################################################e+00}";
                }
                catch (Exception ex)
                { }
            }
            FactedWasteValue_DB = value1;
        }
    }
    private bool FactedWasteValue_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        if (value.Value.Equals("-"))
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        var tmp = value1;
        var len = tmp.Length;
        if (tmp[0] == '(' && tmp[len - 1] == ')')
        {
            tmp = tmp.Remove(len - 1, 1);
            tmp = tmp.Remove(0, 1);
        }
        var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
                     NumberStyles.AllowExponent;
        try
        {
            if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
        }
        catch
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }
    //FactedWasteValue property
    #endregion

    //WasteOutbreakPreviousYear property
    #region  WasteOutbreakPreviousYear
    public string WasteOutbreakPreviousYear_DB { get; set; } = "";
    [NotMapped]
    [FormProperty(true,"Выброс радионуклида в атмосферу за предыдущий год, Бк", "фактический","6")]
    public RamAccess<string> WasteOutbreakPreviousYear
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(WasteOutbreakPreviousYear)))
            {
                ((RamAccess<string>)Dictionary[nameof(WasteOutbreakPreviousYear)]).Value = WasteOutbreakPreviousYear_DB;
                return (RamAccess<string>)Dictionary[nameof(WasteOutbreakPreviousYear)];
            }
            else
            {
                var rm = new RamAccess<string>(WasteOutbreakPreviousYear_Validation, WasteOutbreakPreviousYear_DB);
                rm.PropertyChanged += WasteOutbreakPreviousYearValueChanged;
                Dictionary.Add(nameof(WasteOutbreakPreviousYear), rm);
                return (RamAccess<string>)Dictionary[nameof(WasteOutbreakPreviousYear)];
            }
        }
        set
        {
            WasteOutbreakPreviousYear_DB = value.Value;
            OnPropertyChanged(nameof(WasteOutbreakPreviousYear));
        }
    }


    private void WasteOutbreakPreviousYearValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var value1 = ((RamAccess<string>)Value).Value;
            if (value1 != null)
            {
                value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                if (value1.Equals("-"))
                {
                    WasteOutbreakPreviousYear_DB = value1;
                    return;
                }
                if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
                {
                    value1 = value1.Replace("+", "e+").Replace("-", "e-");
                }
                try
                {
                    var value2 = Convert.ToDouble(value1);
                    value1 = $"{value2:0.######################################################e+00}";
                }
                catch (Exception ex)
                { }
            }
            WasteOutbreakPreviousYear_DB = value1;
        }
    }
    private bool WasteOutbreakPreviousYear_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        if (value.Value.Equals("-"))
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        var tmp = value1;
        var len = tmp.Length;
        if (tmp[0] == '(' && tmp[len - 1] == ')')
        {
            tmp = tmp.Remove(len - 1, 1);
            tmp = tmp.Remove(0, 1);
        }
        var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
                     NumberStyles.AllowExponent;
        try
        {
            if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
        }
        catch
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }
    //WasteOutbreakPreviousYear property
    #endregion


    #region IExcel
    public void ExcelGetRow(ExcelWorksheet worksheet, int Row)
    {
        double val;
        base.ExcelGetRow(worksheet, Row);
        ObservedSourceNumber_DB = Convert.ToString(worksheet.Cells[Row, 1].Value);
        RadionuclidName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
        AllowedWasteValue_DB = Convert.ToString(worksheet.Cells[Row, 3].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 3].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 3].Value);
        FactedWasteValue_DB = Convert.ToString(worksheet.Cells[Row, 4].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 4].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 4].Value);
        WasteOutbreakPreviousYear_DB = Convert.ToString(worksheet.Cells[Row, 5].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 5].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 5].Value);
    }

    public int ExcelRow(ExcelWorksheet worksheet, int Row, int Column, bool Transpon = true)
    {
        var cnt = base.ExcelRow(worksheet, Row, Column, Transpon);
        Column += Transpon ? cnt : 0;
        Row += !Transpon ? cnt : 0;
        double val;

        worksheet.Cells[Row + (!Transpon ? 0 : 0), Column + (Transpon ? 0 : 0)].Value = ObservedSourceNumber_DB;
        worksheet.Cells[Row + (!Transpon ? 1 : 0), Column + (Transpon ? 1 : 0)].Value = RadionuclidName_DB;
        worksheet.Cells[Row + (!Transpon ? 2 : 0), Column + (Transpon ? 2 : 0)].Value = string.IsNullOrEmpty(AllowedWasteValue_DB) || AllowedWasteValue_DB == null ? 0 : double.TryParse(AllowedWasteValue_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : AllowedWasteValue_DB;
        worksheet.Cells[Row + (!Transpon ? 3 : 0), Column + (Transpon ? 3 : 0)].Value = string.IsNullOrEmpty(FactedWasteValue_DB) || FactedWasteValue_DB == null ? 0 : double.TryParse(FactedWasteValue_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : FactedWasteValue_DB;
        worksheet.Cells[Row + (!Transpon ? 4 : 0), Column + (Transpon ? 4 : 0)].Value = string.IsNullOrEmpty(WasteOutbreakPreviousYear_DB) || WasteOutbreakPreviousYear_DB == null ? 0 : double.TryParse(WasteOutbreakPreviousYear_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : WasteOutbreakPreviousYear_DB;
        return 5;
    }

    public static int ExcelHeader(ExcelWorksheet worksheet, int Row, int Column, bool Transpon = true)
    {
        var cnt = Form2.ExcelHeader(worksheet, Row, Column, Transpon);
        Column += Transpon ? cnt : 0;
        Row += !Transpon ? cnt : 0;

        worksheet.Cells[Row + (!Transpon ? 0 : 0), Column + (Transpon ? 0 : 0)].Value = ((FormPropertyAttribute) Type.GetType("Models.Forms.Form2.Form27,Models").GetProperty(nameof(ObservedSourceNumber)).GetCustomAttributes(typeof(FormPropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 1 : 0), Column + (Transpon ? 1 : 0)].Value = ((FormPropertyAttribute) Type.GetType("Models.Forms.Form2.Form27,Models").GetProperty(nameof(RadionuclidName)).GetCustomAttributes(typeof(FormPropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 2 : 0), Column + (Transpon ? 2 : 0)].Value = ((FormPropertyAttribute) Type.GetType("Models.Forms.Form2.Form27,Models").GetProperty(nameof(AllowedWasteValue)).GetCustomAttributes(typeof(FormPropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 3 : 0), Column + (Transpon ? 3 : 0)].Value = ((FormPropertyAttribute) Type.GetType("Models.Forms.Form2.Form27,Models").GetProperty(nameof(FactedWasteValue)).GetCustomAttributes(typeof(FormPropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 4 : 0), Column + (Transpon ? 4 : 0)].Value = ((FormPropertyAttribute) Type.GetType("Models.Forms.Form2.Form27,Models").GetProperty(nameof(WasteOutbreakPreviousYear)).GetCustomAttributes(typeof(FormPropertyAttribute), false).First()).Names[1];
        return 5;
    }
    #endregion

    #region IDataGridColumn
    private static DataGridColumns _DataGridColumns { get; set; }
    public override DataGridColumns GetColumnStructure(string param = "")
    {
        if (_DataGridColumns == null)
        {
            #region NumberInOrder (1)
            var NumberInOrderR = ((FormPropertyAttribute)typeof(Form).GetProperty(nameof(NumberInOrder)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
            NumberInOrderR.SetSizeColToAllLevels(50);
            NumberInOrderR.Binding = nameof(NumberInOrder);
            NumberInOrderR.Blocked = true;
            NumberInOrderR.ChooseLine = true;
            #endregion
            #region ObservedSourceNumber (2)
            var ObservedSourceNumberR = ((FormPropertyAttribute)typeof(Form27).GetProperty(nameof(ObservedSourceNumber)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            ObservedSourceNumberR.SetSizeColToAllLevels(228);
            ObservedSourceNumberR.Binding = nameof(ObservedSourceNumber);
            NumberInOrderR += ObservedSourceNumberR;
            #endregion
            #region RadionuclidName (3)
            var RadionuclidNameR = ((FormPropertyAttribute)typeof(Form27).GetProperty(nameof(RadionuclidName)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            RadionuclidNameR.SetSizeColToAllLevels(183);
            RadionuclidNameR.Binding = nameof(RadionuclidName);
            NumberInOrderR += RadionuclidNameR;
            #endregion
            #region AllowedWasteValue (4)
            var AllowedWasteValueR = ((FormPropertyAttribute)typeof(Form27).GetProperty(nameof(AllowedWasteValue)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            AllowedWasteValueR.SetSizeColToAllLevels(170);
            AllowedWasteValueR.Binding = nameof(AllowedWasteValue);
            NumberInOrderR += AllowedWasteValueR;
            #endregion
            #region FactedWasteValue (5)
            var FactedWasteValueR = ((FormPropertyAttribute)typeof(Form27).GetProperty(nameof(FactedWasteValue)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            FactedWasteValueR.SetSizeColToAllLevels(170);
            FactedWasteValueR.Binding = nameof(FactedWasteValue);
            NumberInOrderR += FactedWasteValueR;
            #endregion
            #region WasteOutbreakPreviousYear (6)
            var WasteOutbreakPreviousYearR = ((FormPropertyAttribute)typeof(Form27).GetProperty(nameof(WasteOutbreakPreviousYear)).GetCustomAttributes(typeof(FormPropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            WasteOutbreakPreviousYearR.SetSizeColToAllLevels(363);
            WasteOutbreakPreviousYearR.Binding = nameof(WasteOutbreakPreviousYear);
            NumberInOrderR += WasteOutbreakPreviousYearR;
            #endregion
            _DataGridColumns = NumberInOrderR;
        }
        return _DataGridColumns;
    }
    #endregion
}