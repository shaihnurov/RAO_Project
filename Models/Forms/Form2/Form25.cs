﻿using Models.DataAccess; 
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Linq;
using Models.Abstracts;
using Models.Attributes;
using OfficeOpenXml;
using Models.Collections;

namespace Models;

[Serializable]
[Form_Class("Форма 2.5: Наличие РВ, содержащихся в отработавшем ядерном топливе, в пунктах хранения")]
public class Form25 : Form2
{
    public Form25() : base()
    {
        FormNum.Value = "2.5";
        //NumberOfFields.Value = 12;
        Validate_all();
    }
    private void Validate_all()
    {
        CodeOYAT_Validation(CodeOYAT);
        FcpNumber_Validation(FcpNumber);
        StoragePlaceCode_Validation(StoragePlaceCode);
        StoragePlaceName_Validation(StoragePlaceName);
        FuelMass_Validation(FuelMass);
        CellMass_Validation(CellMass);
        Quantity_Validation(Quantity);
        BetaGammaActivity_Validation(BetaGammaActivity);
        AlphaActivity_Validation(AlphaActivity);
    }

    [Form_Property(true,"Форма")]
    public override bool Object_Validation()
    {
        return !(CodeOYAT.HasErrors||
                 FcpNumber.HasErrors||
                 StoragePlaceCode.HasErrors||
                 StoragePlaceName.HasErrors||
                 FuelMass.HasErrors||
                 CellMass.HasErrors||
                 Quantity.HasErrors||
                 BetaGammaActivity.HasErrors||
                 AlphaActivity.HasErrors);
    }

    //StoragePlaceName property
    #region  StoragePlaceName
    public string StoragePlaceName_DB { get; set; } = "";
    [NotMapped]
    [Form_Property(true,"Пункт хранения ОЯТ", "наименование, номер","2")]
    public RamAccess<string> StoragePlaceName
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(StoragePlaceName)))
            {
                ((RamAccess<string>)Dictionary[nameof(StoragePlaceName)]).Value = StoragePlaceName_DB;
                return (RamAccess<string>)Dictionary[nameof(StoragePlaceName)];
            }
            else
            {
                var rm = new RamAccess<string>(StoragePlaceName_Validation, StoragePlaceName_DB);
                rm.PropertyChanged += StoragePlaceNameValueChanged;
                Dictionary.Add(nameof(StoragePlaceName), rm);
                return (RamAccess<string>)Dictionary[nameof(StoragePlaceName)];
            }
        }
        set
        {
            StoragePlaceName_DB = value.Value;
            OnPropertyChanged(nameof(StoragePlaceName));
        }
    }
    //If change this change validation
    private void StoragePlaceNameValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            StoragePlaceName_DB = ((RamAccess<string>)Value).Value;
        }
    }
    private bool StoragePlaceName_Validation(RamAccess<string> value)//Ready
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено"); return false;
        }
        return true;
    }
    //StoragePlaceName property
    #endregion

    //CodeOYAT property
    #region  CodeOYAT
    public string CodeOYAT_DB { get; set; } = ""; [NotMapped]
    [Form_Property(true,"Наличие на конец отчетного года", "код ОЯТ","4")]
    public RamAccess<string> CodeOYAT
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(CodeOYAT)))
            {
                ((RamAccess<string>)Dictionary[nameof(CodeOYAT)]).Value = CodeOYAT_DB;
                return (RamAccess<string>)Dictionary[nameof(CodeOYAT)];
            }
            else
            {
                var rm = new RamAccess<string>(CodeOYAT_Validation, CodeOYAT_DB);
                rm.PropertyChanged += CodeOYATValueChanged;
                Dictionary.Add(nameof(CodeOYAT), rm);
                return (RamAccess<string>)Dictionary[nameof(CodeOYAT)];
            }
        }
        set
        {
            CodeOYAT_DB = value.Value;
            OnPropertyChanged(nameof(CodeOYAT));
        }
    }

    private void CodeOYATValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            CodeOYAT_DB = ((RamAccess<string>)Value).Value;
        }
    }
    private bool CodeOYAT_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено"); return false;
        }
        Regex a = new("^[0-9]{5}$");
        if (!a.IsMatch(value.Value))
        {
            value.AddError("Недопустимое значение"); return false;
        }
        return true;
    }
    //CodeOYAT property
    #endregion

    //StoragePlaceCode property
    #region  StoragePlaceCode
    public string StoragePlaceCode_DB { get; set; } = ""; [NotMapped]
    [Form_Property(true,"Пункт хранения ОЯТ", "код","3")]
    public RamAccess<string> StoragePlaceCode //8 cyfer code or - .
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(StoragePlaceCode)))
            {
                ((RamAccess<string>)Dictionary[nameof(StoragePlaceCode)]).Value = StoragePlaceCode_DB;
                return (RamAccess<string>)Dictionary[nameof(StoragePlaceCode)];
            }
            else
            {
                var rm = new RamAccess<string>(StoragePlaceCode_Validation, StoragePlaceCode_DB);
                rm.PropertyChanged += StoragePlaceCodeValueChanged;
                Dictionary.Add(nameof(StoragePlaceCode), rm);
                return (RamAccess<string>)Dictionary[nameof(StoragePlaceCode)];
            }
        }
        set
        {
            StoragePlaceCode_DB = value.Value;
            OnPropertyChanged(nameof(StoragePlaceCode));
        }
    }
    private void StoragePlaceCodeValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            StoragePlaceCode_DB = ((RamAccess<string>)Value).Value;
        }
    }
    private bool StoragePlaceCode_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено"); return false;
        }
        if (value.Value == "-")
        {
            return true;
        }
        Regex a = new("^[0-9]{8}$");
        if (!a.IsMatch(value.Value))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }
    //StoragePlaceCode property
    #endregion

    //FcpNumber property
    #region  FcpNumber
    public string FcpNumber_DB { get; set; } = ""; [NotMapped]
    [Form_Property(true,"Наличие на конец отчетного года", "номер мероприятия ФЦП","5")]
    public RamAccess<string> FcpNumber
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(FcpNumber)))
            {
                ((RamAccess<string>)Dictionary[nameof(FcpNumber)]).Value = FcpNumber_DB;
                return (RamAccess<string>)Dictionary[nameof(FcpNumber)];
            }
            else
            {
                var rm = new RamAccess<string>(FcpNumber_Validation, FcpNumber_DB);
                rm.PropertyChanged += FcpNumberValueChanged;
                Dictionary.Add(nameof(FcpNumber), rm);
                return (RamAccess<string>)Dictionary[nameof(FcpNumber)];
            }
        }
        set
        {
            FcpNumber_DB = value.Value;
            OnPropertyChanged(nameof(FcpNumber));
        }
    }

    private void FcpNumberValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            FcpNumber_DB = ((RamAccess<string>)Value).Value;
        }
    }
    private bool FcpNumber_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        return true;
    }
    //FcpNumber property
    #endregion

    //FuelMass property
    #region  FuelMass
    public string FuelMass_DB { get; set; } = "";
    [NotMapped]
    [Form_Property(true,"Наличие на конец отчетного года", "топлива (нетто)","6")]
    public RamAccess<string> FuelMass
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(FuelMass)))
            {
                ((RamAccess<string>)Dictionary[nameof(FuelMass)]).Value = FuelMass_DB;
                return (RamAccess<string>)Dictionary[nameof(FuelMass)];
            }
            else
            {
                var rm = new RamAccess<string>(FuelMass_Validation, FuelMass_DB);
                rm.PropertyChanged += FuelMassValueChanged;
                Dictionary.Add(nameof(FuelMass), rm);
                return (RamAccess<string>)Dictionary[nameof(FuelMass)];
            }
        }
        set
        {
            FuelMass_DB = value.Value;
            OnPropertyChanged(nameof(FuelMass));
        }
    }

    private void FuelMassValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var value1 = ((RamAccess<string>)Value).Value;
            if (value1 != null)
            {
                value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                if (value1.Equals("-"))
                {
                    FuelMass_DB = value1;
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
            FuelMass_DB = value1;
        }
    }
    private bool FuelMass_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        string tmp = value1;
        int len = tmp.Length;
        if (tmp[0] == '(' && tmp[len - 1] == ')')
        {
            tmp = tmp.Remove(len - 1, 1);
            tmp = tmp.Remove(0, 1);
        }
        NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
    //FuelMass property
    #endregion

    //CellMass property
    #region  CellMass
    public string CellMass_DB { get; set; } = ""; [NotMapped]
    [Form_Property(true,"Наличие на конец отчетного года", "ОТВС(ТВЭЛ, выемной части реактора) брутто","7")]
    public RamAccess<string> CellMass
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(CellMass)))
            {
                ((RamAccess<string>)Dictionary[nameof(CellMass)]).Value = CellMass_DB;
                return (RamAccess<string>)Dictionary[nameof(CellMass)];
            }
            else
            {
                var rm = new RamAccess<string>(CellMass_Validation, CellMass_DB);
                rm.PropertyChanged += CellMassValueChanged;
                Dictionary.Add(nameof(CellMass), rm);
                return (RamAccess<string>)Dictionary[nameof(CellMass)];
            }
        }
        set
        {
            CellMass_DB = value.Value;
            OnPropertyChanged(nameof(CellMass));
        }
    }

    private void CellMassValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var value1 = ((RamAccess<string>)Value).Value;
            if (value1 != null)
            {
                value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                if (value1.Equals("-"))
                {
                    CellMass_DB = value1;
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
            CellMass_DB = value1;
        }
    }
    private bool CellMass_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        string tmp = value1;
        int len = tmp.Length;
        if (tmp[0] == '(' && tmp[len - 1] == ')')
        {
            tmp = tmp.Remove(len - 1, 1);
            tmp = tmp.Remove(0, 1);
        }
        NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
                              NumberStyles.AllowExponent;
        try
        {
            if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
            {
                value.AddError("Число должно быть больше нуля"); return false;
            }
        }
        catch
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }
    //CellMass property
    #endregion

    //Quantity property
    #region  Quantity
    public int? Quantity_DB { get; set; } [NotMapped]
    [Form_Property(true,"Наличие на конец отчетного года", "количество, шт","8")]
    public RamAccess<int?> Quantity
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(Quantity)))
            {
                ((RamAccess<int?>)Dictionary[nameof(Quantity)]).Value = Quantity_DB;
                return (RamAccess<int?>)Dictionary[nameof(Quantity)];
            }
            else
            {
                var rm = new RamAccess<int?>(Quantity_Validation, Quantity_DB);
                rm.PropertyChanged += QuantityValueChanged;
                Dictionary.Add(nameof(Quantity), rm);
                return (RamAccess<int?>)Dictionary[nameof(Quantity)];
            }
        }
        set
        {
            Quantity_DB = value.Value;
            OnPropertyChanged(nameof(Quantity));
        }
    }
    // positive int.
    private void QuantityValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            Quantity_DB = ((RamAccess<int?>)Value).Value;
        }
    }
    private bool Quantity_Validation(RamAccess<int?> value)//Ready
    {
        value.ClearErrors();
        if (value.Value == null)
        {
            return true;
        }
        if (value.Value <= 0)
        {
            value.AddError("Недопустимое значение"); return false;
        }
        return true;
    }
    //Quantity property
    #endregion

    //BetaGammaActivity property
    #region  BetaGammaActivity
    public string BetaGammaActivity_DB { get; set; } = "";
    [NotMapped]
    [Form_Property(true,"Наличие на конец отчетного года", "бета-, гамма-излучающих нуклидов","10")]
    public RamAccess<string> BetaGammaActivity
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(BetaGammaActivity)))
            {
                ((RamAccess<string>)Dictionary[nameof(BetaGammaActivity)]).Value = BetaGammaActivity_DB;
                return (RamAccess<string>)Dictionary[nameof(BetaGammaActivity)];
            }
            else
            {
                var rm = new RamAccess<string>(BetaGammaActivity_Validation, BetaGammaActivity_DB);
                rm.PropertyChanged += BetaGammaActivityValueChanged;
                Dictionary.Add(nameof(BetaGammaActivity), rm);
                return (RamAccess<string>)Dictionary[nameof(BetaGammaActivity)];
            }
        }
        set
        {
            BetaGammaActivity_DB = value.Value;
            OnPropertyChanged(nameof(BetaGammaActivity));
        }
    }

    private void BetaGammaActivityValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var value1 = ((RamAccess<string>)Value).Value;
            if (value1 != null)
            {
                value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                if (value1.Equals("-"))
                {
                    BetaGammaActivity_DB = value1;
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
            BetaGammaActivity_DB = value1;
        }
    }
    private bool BetaGammaActivity_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if(string.IsNullOrEmpty(value.Value))
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
        string tmp = value1;
        int len = tmp.Length;
        if (tmp[0] == '(' && tmp[len - 1] == ')')
        {
            tmp = tmp.Remove(len - 1, 1);
            tmp = tmp.Remove(0, 1);
        }
        NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
                              NumberStyles.AllowExponent;
        try
        {
            if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
            {
                value.AddError("Число должно быть больше нуля"); return false;
            }
        }
        catch
        {
            value.AddError("Недопустимое значение"); return false;
        }
        return true;
    }
    //BetaGammaActivity property
    #endregion

    //AlphaActivity property
    #region  AlphaActivity
    public string AlphaActivity_DB { get; set; } = ""; [NotMapped]
    [Form_Property(true,"Наличие на конец отчетного года", "альфа-излучающих нуклидов","9")]
    public RamAccess<string> AlphaActivity
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(AlphaActivity)))
            {
                ((RamAccess<string>)Dictionary[nameof(AlphaActivity)]).Value = AlphaActivity_DB;
                return (RamAccess<string>)Dictionary[nameof(AlphaActivity)];
            }
            else
            {
                var rm = new RamAccess<string>(AlphaActivity_Validation, AlphaActivity_DB);
                rm.PropertyChanged += AlphaActivityValueChanged;
                Dictionary.Add(nameof(AlphaActivity), rm);
                return (RamAccess<string>)Dictionary[nameof(AlphaActivity)];
            }
        }
        set
        {
            AlphaActivity_DB = value.Value;
            OnPropertyChanged(nameof(AlphaActivity));
        }
    }

    private void AlphaActivityValueChanged(object Value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            var value1 = ((RamAccess<string>)Value).Value;
            if (value1 != null)
            {
                value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                if (value1.Equals("-"))
                {
                    AlphaActivity_DB = value1;
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
            AlphaActivity_DB = value1;
        }
    }
    private bool AlphaActivity_Validation(RamAccess<string> value)//TODO
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
        string tmp = value1;
        int len = tmp.Length;
        if (tmp[0] == '(' && tmp[len - 1] == ')')
        {
            tmp = tmp.Remove(len - 1, 1);
            tmp = tmp.Remove(0, 1);
        }
        NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
                              NumberStyles.AllowExponent;
        try
        {
            if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
            {
                value.AddError("Число должно быть больше нуля"); return false;
            }
        }
        catch
        {
            value.AddError("Недопустимое значение"); return false;
        }
        return true;
    }
    //AlphaActivity property
    #endregion

    #region IExcel
    public void ExcelGetRow(ExcelWorksheet worksheet, int Row)
    {
        double val;
        base.ExcelGetRow(worksheet, Row);
        StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[Row, 1].Value);
        StoragePlaceName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
        CodeOYAT_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
        FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
        FuelMass_DB = Convert.ToString(worksheet.Cells[Row, 5].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 5].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 5].Value);
        CellMass_DB = Convert.ToString(worksheet.Cells[Row, 6].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 6].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 6].Value);
        Quantity_DB = Convert.ToInt32(worksheet.Cells[Row, 7].Value);
        AlphaActivity_DB = Convert.ToString(worksheet.Cells[Row, 8].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 8].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 8].Value);
        BetaGammaActivity_DB = Convert.ToString(worksheet.Cells[Row, 9].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 9].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 9].Value);
    }
    public int ExcelRow(ExcelWorksheet worksheet, int Row, int Column, bool Transpon = true)
    {
        var cnt = base.ExcelRow(worksheet, Row, Column, Transpon);
        Column += Transpon ? cnt : 0;
        Row += !Transpon ? cnt : 0;
        double val;

        worksheet.Cells[Row + (!Transpon ? 0 : 0), Column + (Transpon ? 0 : 0)].Value = StoragePlaceCode_DB;
        worksheet.Cells[Row + (!Transpon ? 1 : 0), Column + (Transpon ? 1 : 0)].Value = StoragePlaceName_DB;
        worksheet.Cells[Row + (!Transpon ? 2 : 0), Column + (Transpon ? 2 : 0)].Value = CodeOYAT_DB;
        worksheet.Cells[Row + (!Transpon ? 3 : 0), Column + (Transpon ? 3 : 0)].Value = FcpNumber_DB;
        worksheet.Cells[Row + (!Transpon ? 4 : 0), Column + (Transpon ? 4 : 0)].Value = string.IsNullOrEmpty(FuelMass_DB) || FuelMass_DB == null ? 0 : double.TryParse(FuelMass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : FuelMass_DB;
        worksheet.Cells[Row + (!Transpon ? 5 : 0), Column + (Transpon ? 5 : 0)].Value = string.IsNullOrEmpty(CellMass_DB) || CellMass_DB == null ? 0 : double.TryParse(CellMass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : CellMass_DB;
        worksheet.Cells[Row + (!Transpon ? 6 : 0), Column + (Transpon ? 6 : 0)].Value = Quantity_DB;
        worksheet.Cells[Row + (!Transpon ? 7 : 0), Column + (Transpon ? 7 : 0)].Value = string.IsNullOrEmpty(AlphaActivity_DB) || AlphaActivity_DB == null ? 0 : double.TryParse(AlphaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : AlphaActivity_DB;
        worksheet.Cells[Row + (!Transpon ? 8 : 0), Column + (Transpon ? 8 : 0)].Value = string.IsNullOrEmpty(BetaGammaActivity_DB) || BetaGammaActivity_DB == null ? 0 : double.TryParse(BetaGammaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : BetaGammaActivity_DB;
        return 9;
    }

    public static int ExcelHeader(ExcelWorksheet worksheet, int Row, int Column, bool Transpon = true)
    {
        var cnt = Form2.ExcelHeader(worksheet, Row, Column, Transpon);
        Column += Transpon ? cnt : 0;
        Row += !Transpon ? cnt : 0;

        worksheet.Cells[Row + (!Transpon ? 0 : 0), Column + (Transpon ? 0 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(StoragePlaceName)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 1 : 0), Column + (Transpon ? 1 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(StoragePlaceCode)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 2 : 0), Column + (Transpon ? 2 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(CodeOYAT)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 3 : 0), Column + (Transpon ? 3 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(FcpNumber)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 4 : 0), Column + (Transpon ? 4 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(FuelMass)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 5 : 0), Column + (Transpon ? 5 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(CellMass)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 6 : 0), Column + (Transpon ? 6 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(Quantity)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 7 : 0), Column + (Transpon ? 7 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(AlphaActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        worksheet.Cells[Row + (!Transpon ? 8 : 0), Column + (Transpon ? 8 : 0)].Value = ((Form_PropertyAttribute) Type.GetType("Models.Form25,Models").GetProperty(nameof(BetaGammaActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
        return 9;
    }
    #endregion
    #region IDataGridColumn
    private static DataGridColumns _DataGridColumns { get; set; }
    public override DataGridColumns GetColumnStructure(string param = "")
    {
        if (_DataGridColumns == null)
        {
            #region NumberInOrder (1)
            DataGridColumns NumberInOrderR = ((Form_PropertyAttribute)typeof(Form).GetProperty(nameof(NumberInOrder)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
            NumberInOrderR.SetSizeColToAllLevels(50);
            NumberInOrderR.Binding = nameof(NumberInOrder);
            NumberInOrderR.Blocked = true;
            NumberInOrderR.ChooseLine = true;
            #endregion
            #region StoragePlaceName (2)
            DataGridColumns StoragePlaceNameR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(StoragePlaceName)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            StoragePlaceNameR.SetSizeColToAllLevels(163);
            StoragePlaceNameR.Binding = nameof(StoragePlaceName);
            NumberInOrderR += StoragePlaceNameR;
            #endregion
            #region StoragePlaceCode (3)
            DataGridColumns StoragePlaceCodeR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(StoragePlaceCode)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            StoragePlaceCodeR.SetSizeColToAllLevels(88);
            StoragePlaceCodeR.Binding = nameof(StoragePlaceCode);
            NumberInOrderR += StoragePlaceCodeR;
            #endregion
            #region CodeOYAT (4)
            DataGridColumns CodeOYATR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(CodeOYAT)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            CodeOYATR.SetSizeColToAllLevels(88);
            CodeOYATR.Binding = nameof(CodeOYAT);
            NumberInOrderR += CodeOYATR;
            #endregion
            #region FcpNumber (5)
            DataGridColumns FcpNumberR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(FcpNumber)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            FcpNumberR.SetSizeColToAllLevels(163);
            FcpNumberR.Binding = nameof(FcpNumber);
            NumberInOrderR += FcpNumberR;
            #endregion
            #region FuelMass (6)
            DataGridColumns FuelMassR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(FuelMass)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            FuelMassR.SetSizeColToAllLevels(103);
            FuelMassR.Binding = nameof(FuelMass);
            NumberInOrderR += FuelMassR;
            #endregion
            #region CellMass (7)
            DataGridColumns CellMassR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(CellMass)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            CellMassR.SetSizeColToAllLevels(288);
            CellMassR.Binding = nameof(CellMass);
            NumberInOrderR += CellMassR;
            #endregion
            #region Quantity (8)
            DataGridColumns QuantityR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(Quantity)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            QuantityR.SetSizeColToAllLevels(100);
            QuantityR.Binding = nameof(Quantity);
            NumberInOrderR += QuantityR;
            #endregion
            #region AlphaActivity (9)
            DataGridColumns AlphaActivityR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(AlphaActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            AlphaActivityR.SetSizeColToAllLevels(185);
            AlphaActivityR.Binding = nameof(AlphaActivity);
            NumberInOrderR += AlphaActivityR;
            #endregion
            #region BetaGammaActivity (10)
            DataGridColumns BetaGammaActivityR = ((Form_PropertyAttribute)typeof(Form25).GetProperty(nameof(BetaGammaActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
            BetaGammaActivityR.SetSizeColToAllLevels(185);
            BetaGammaActivityR.Binding = nameof(BetaGammaActivity);
            NumberInOrderR += BetaGammaActivityR;
            #endregion
            _DataGridColumns = NumberInOrderR;
        }
        return _DataGridColumns;
    }
    #endregion
}