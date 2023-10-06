﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Models.Attributes;
using Models.Collections;
using Models.Forms.DataAccess;
using OfficeOpenXml;
using Spravochniki;

namespace Models.Forms.Form1;

[Form_Class("Форма 1.8: Сведения о жидких кондиционированных РАО")]
public class Form18 : Form1
{
    #region Constructor
    
    public Form18()
    {
        FormNum.Value = "1.8";
        Validate_all();
    }

    #endregion

    #region Validation
    
    private void Validate_all()
    {
        CodeRAO_Validation(CodeRAO);
        IndividualNumberZHRO_Validation(IndividualNumberZHRO);
        SpecificActivity_Validation(SpecificActivity);
        SaltConcentration_Validation(SaltConcentration);
        Radionuclids_Validation(Radionuclids);
        ProviderOrRecieverOKPO_Validation(ProviderOrRecieverOKPO);
        TransporterOKPO_Validation(TransporterOKPO);
        TritiumActivity_Validation(TritiumActivity);
        BetaGammaActivity_Validation(BetaGammaActivity);
        AlphaActivity_Validation(AlphaActivity);
        TransuraniumActivity_Validation(TransuraniumActivity);
        PassportNumber_Validation(PassportNumber);
        RefineOrSortRAOCode_Validation(RefineOrSortRAOCode);
        Subsidy_Validation(Subsidy);
        FcpNumber_Validation(FcpNumber);
        StatusRAO_Validation(StatusRAO);
        Volume6_Validation(Volume6);
        Mass7_Validation(Mass7);
        Volume20_Validation(Volume20);
        Mass21_Validation(Mass21);
        StoragePlaceName_Validation(StoragePlaceName);
        StoragePlaceCode_Validation(StoragePlaceCode);
    }

    public override bool Object_Validation()
    {
        return !(CodeRAO.HasErrors ||
                 IndividualNumberZHRO.HasErrors ||
                 SpecificActivity.HasErrors ||
                 SaltConcentration.HasErrors ||
                 Radionuclids.HasErrors ||
                 ProviderOrRecieverOKPO.HasErrors ||
                 TransporterOKPO.HasErrors ||
                 TritiumActivity.HasErrors ||
                 BetaGammaActivity.HasErrors ||
                 AlphaActivity.HasErrors ||
                 TransuraniumActivity.HasErrors ||
                 PassportNumber.HasErrors ||
                 RefineOrSortRAOCode.HasErrors ||
                 Subsidy.HasErrors ||
                 FcpNumber.HasErrors ||
                 StatusRAO.HasErrors ||
                 Volume6.HasErrors ||
                 Mass7.HasErrors ||
                 Volume20.HasErrors ||
                 Mass21.HasErrors ||
                 StoragePlaceName.HasErrors ||
                 StoragePlaceCode.HasErrors);
    }

    protected override bool OperationCode_Validation(RamAccess<string> value)//OK
    {
        value.ClearErrors();
        if (value.Value == null)
        {
            return true;
        }
        if (!Spravochniks.SprOpCodes.Contains(value.Value))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (!new Regex(@"^\d{2}$").IsMatch(value.Value)
            || !byte.TryParse(value.Value, out var byteValue)
            || byteValue is not (1 or 10 or 18 or >= 21 and <= 29 or >= 31 and <= 39 or 51 or 52 or 55 or 63 or 64 or 68 or 97 or 98 or 99))
        {
            value.AddError("Код операции не может быть использован в форме 1.8");
            return false;
        }

        return true;
    }

    protected override bool DocumentNumber_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        return true;
    }

    protected override bool DocumentVid_Validation(RamAccess<byte?> value)
    {
        value.ClearErrors();
        if (Spravochniks.SprDocumentVidName.Any(item => value.Value == item.Item1))
        {
            return true;
        }
        value.AddError("Недопустимое значение");
        return false;
    }

    protected override bool DocumentDate_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "прим.")
        {
            return true;
        }
        var tmp = value.Value;
        if (new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$").IsMatch(tmp))
        {
            tmp = tmp.Insert(6, "20");
        }
        if (!new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$").IsMatch(tmp) || !DateTimeOffset.TryParse(tmp, out _))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        var b = OperationCode.Value == "68";
        var c = OperationCode.Value is "52" or "55";
        var d = OperationCode.Value is "18" or "51";
        if (b || c || d)
        {
            if (!tmp.Equals(OperationDate.Value))
            {
                //value.AddError("Заполните примечание");//to do note handling
                return true;
            }
        }
        return true;
    }

    #endregion

    #region Properties

    #region  Sum

    // ReSharper disable once MemberCanBePrivate.Global - не делай private!!!
    public bool Sum_DB { get; set; }

    [NotMapped]
    public RamAccess<bool> Sum
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(Sum)))
            {
                ((RamAccess<bool>)Dictionary[nameof(Sum)]).Value = Sum_DB;
                return (RamAccess<bool>)Dictionary[nameof(Sum)];
            }
            var rm = new RamAccess<bool>(Sum_Validation, Sum_DB);
            rm.PropertyChanged += SumValueChanged;
            Dictionary.Add(nameof(Sum), rm);
            return (RamAccess<bool>)Dictionary[nameof(Sum)];
        }
        set
        {
            Sum_DB = value.Value;
            OnPropertyChanged(nameof(Sum));
        }
    }

    private void SumValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            Sum_DB = ((RamAccess<bool>)value).Value;
        }
    }

    private static bool Sum_Validation(RamAccess<bool> value)
    {
        value.ClearErrors();
        return true;
    }

    #endregion

    #region IndividualNumberZHRO (4)

    public string IndividualNumberZHRO_DB { get; set; } = "";

    private bool IndividualNumberZHRO_Hidden_Priv { get; set; }

    [NotMapped]
    public bool IndividualNumberZHRO_Hidden
    {
        get => IndividualNumberZHRO_Hidden_Priv;
        set => IndividualNumberZHRO_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "Сведения о партии ЖРО", "индивидуальный номер (идентификационный код) партии ЖРО", "4")]
    public RamAccess<string> IndividualNumberZHRO
    {
        get
        {
            if (!IndividualNumberZHRO_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(IndividualNumberZHRO)))
                {
                    ((RamAccess<string>)Dictionary[nameof(IndividualNumberZHRO)]).Value = IndividualNumberZHRO_DB;
                    return (RamAccess<string>)Dictionary[nameof(IndividualNumberZHRO)];
                }
                var rm = new RamAccess<string>(IndividualNumberZHRO_Validation, IndividualNumberZHRO_DB);
                rm.PropertyChanged += IndividualNumberZHROValueChanged;
                Dictionary.Add(nameof(IndividualNumberZHRO), rm);
                return (RamAccess<string>)Dictionary[nameof(IndividualNumberZHRO)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!IndividualNumberZHRO_Hidden_Priv)
            {
                IndividualNumberZHRO_DB = value.Value;
                OnPropertyChanged(nameof(IndividualNumberZHRO));
            }
        }
    }

    private void IndividualNumberZHROValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            IndividualNumberZHRO_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool IndividualNumberZHRO_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        return true;
    }

    #endregion

    #region PassportNumber (5)

    public string PassportNumber_DB { get; set; } = "";

    private bool PassportNumber_Hidden_Priv { get; set; }

    [NotMapped]
    public bool PassportNumber_Hidden
    {
        get => PassportNumber_Hidden_Priv;
        set => PassportNumber_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "Сведения о партии ЖРО", "номер паспорта", "5")]
    public RamAccess<string> PassportNumber
    {
        get
        {
            if (!PassportNumber_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(PassportNumber)))
                {
                    ((RamAccess<string>)Dictionary[nameof(PassportNumber)]).Value = PassportNumber_DB;
                    return (RamAccess<string>)Dictionary[nameof(PassportNumber)];
                }
                var rm = new RamAccess<string>(PassportNumber_Validation, PassportNumber_DB);
                rm.PropertyChanged += PassportNumberValueChanged;
                Dictionary.Add(nameof(PassportNumber), rm);
                return (RamAccess<string>)Dictionary[nameof(PassportNumber)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!PassportNumber_Hidden_Priv)
            {
                PassportNumber_DB = value.Value;
                OnPropertyChanged(nameof(PassportNumber));
            }
        }
    }

    private void PassportNumberValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            PassportNumber_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool PassportNumber_Validation(RamAccess<string> value)
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "-")
        {
            return true;
        }
        if (value.Value.Equals("прим."))
        {
            //if ((PassportNumberNote.Value == null) || (PassportNumberNote.Value == ""))
            //{
            //    value.AddError("Поле не может быть пустым");//to do note handling
            //}
            return true;
        }
        return true;
    }

    #endregion

    #region Volume6 (6)

    public string Volume6_DB { get; set; }

    private bool Volume6_Hidden_Priv { get; set; }

    [NotMapped]
    public bool Volume6_Hidden
    {
        get => Volume6_Hidden_Priv;
        set => Volume6_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "Сведения о партии ЖРО", "объем, куб. м", "6")]
    public RamAccess<string> Volume6
    {
        get
        {
            if (!Volume6_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(Volume6)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Volume6)]).Value = Volume6_DB;
                    return (RamAccess<string>)Dictionary[nameof(Volume6)];
                }
                var rm = new RamAccess<string>(Volume6_Validation, Volume6_DB);
                rm.PropertyChanged += Volume6ValueChanged;
                Dictionary.Add(nameof(Volume6), rm);
                return (RamAccess<string>)Dictionary[nameof(Volume6)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!Volume6_Hidden_Priv)
            {
                Volume6_DB = value.Value;
                OnPropertyChanged(nameof(Volume6));
            }
        }
    }

    private void Volume6ValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
        {
            value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                Volume6_DB = value1;
                return;
            }
            if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        Volume6_DB = value1;
    }

    private static bool Volume6_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "-")
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region Mass7 (7)

    public string Mass7_DB { get; set; }

    private bool Mass7_Hidden_Priv { get; set; }

    [NotMapped]
    public bool Mass7_Hidden
    {
        get => Mass7_Hidden_Priv;
        set => Mass7_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "Сведения о партии ЖРО", "масса, т", "7")]
    public RamAccess<string> Mass7
    {
        get
        {
            if (!Mass7_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(Mass7)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Mass7)]).Value = Mass7_DB;
                    return (RamAccess<string>)Dictionary[nameof(Mass7)];
                }
                var rm = new RamAccess<string>(Mass7_Validation, Mass7_DB);
                rm.PropertyChanged += Mass7ValueChanged;
                Dictionary.Add(nameof(Mass7), rm);
                return (RamAccess<string>)Dictionary[nameof(Mass7)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!Mass7_Hidden_Priv)
            {
                Mass7_DB = value.Value;
                OnPropertyChanged(nameof(Mass7));
            }
        }
    }

    private void Mass7ValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
        {
            value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                Mass7_DB = value1;
                return;
            }
            if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        Mass7_DB = value1;
    }

    private static bool Mass7_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "-")
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region SaltConcentration (8)

    public string SaltConcentration_DB { get; set; }

    private bool SaltConcentration_Hidden_Priv { get; set; }

    [NotMapped]
    public bool SaltConcentration_Hidden
    {
        get => SaltConcentration_Hidden_Priv;
        set => SaltConcentration_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "Сведения о партии ЖРО", "солесодержание, г/л", "8")]
    public RamAccess<string> SaltConcentration
    {
        get
        {
            if (!SaltConcentration_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(SaltConcentration)))
                {
                    ((RamAccess<string>)Dictionary[nameof(SaltConcentration)]).Value = SaltConcentration_DB;
                    return (RamAccess<string>)Dictionary[nameof(SaltConcentration)];
                }
                var rm = new RamAccess<string>(SaltConcentration_Validation, SaltConcentration_DB);
                rm.PropertyChanged += SaltConcentrationValueChanged;
                Dictionary.Add(nameof(SaltConcentration), rm);
                return (RamAccess<string>)Dictionary[nameof(SaltConcentration)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!SaltConcentration_Hidden_Priv)
            {
                SaltConcentration_DB = value.Value;
                OnPropertyChanged(nameof(SaltConcentration));
            }
        }
    }

    private void SaltConcentrationValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
        {
            value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                SaltConcentration_DB = value1;
                return;
            }
            if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        SaltConcentration_DB = value1;
    }

    private static bool SaltConcentration_Validation(RamAccess<string> value)
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
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region Radionuclids (9)

    public string Radionuclids_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "Сведения о партии ЖРО", "наименование радионуклида", "9")]
    public RamAccess<string> Radionuclids
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(Radionuclids)))
            {
                ((RamAccess<string>)Dictionary[nameof(Radionuclids)]).Value = Radionuclids_DB;
                return (RamAccess<string>)Dictionary[nameof(Radionuclids)];
            }
            var rm = new RamAccess<string>(Radionuclids_Validation, Radionuclids_DB);
            rm.PropertyChanged += RadionuclidsValueChanged;
            Dictionary.Add(nameof(Radionuclids), rm);
            return (RamAccess<string>)Dictionary[nameof(Radionuclids)];
        }//OK
        set
        {
            Radionuclids_DB = value.Value;
            OnPropertyChanged(nameof(Radionuclids));
        }
    }//If change this change validation

    private void RadionuclidsValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            Radionuclids_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool Radionuclids_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        var nuclids = value.Value.Split(";");
        for (var k = 0; k < nuclids.Length; k++)
        {
            nuclids[k] = nuclids[k].ToLower().Replace(" ", "");
        }
        var flag = true;
        foreach (var nucl in nuclids)
        {
            var tmp = from item in Spravochniks.SprRadionuclids where nucl == item.Item1 select item.Item1;
            if (!tmp.Any())
                flag = false;
        }
        if (!flag)
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }

    #endregion

    #region SpecificActivity (10)

    public string SpecificActivity_DB { get; set; }

    [NotMapped]
    [FormProperty(true, "Сведения о партии ЖРО", "удельная активность, Бк/г", "10")]
    public RamAccess<string> SpecificActivity
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(SpecificActivity)))
            {
                ((RamAccess<string>)Dictionary[nameof(SpecificActivity)]).Value = SpecificActivity_DB;
                return (RamAccess<string>)Dictionary[nameof(SpecificActivity)];
            }
            var rm = new RamAccess<string>(SpecificActivity_Validation, SpecificActivity_DB);
            rm.PropertyChanged += SpecificActivityValueChanged;
            Dictionary.Add(nameof(SpecificActivity), rm);
            return (RamAccess<string>)Dictionary[nameof(SpecificActivity)];
        }
        set
        {
            SpecificActivity_DB = value.Value;
            OnPropertyChanged(nameof(SpecificActivity));
        }
    }

    private void SpecificActivityValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
        {
            value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                SpecificActivity_DB = value1;
                return;
            }
            if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        SpecificActivity_DB = value1;
    }

    private static bool SpecificActivity_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Поле не заполнено");
            return false;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region ProviderOrRecieverOKPO (14)

    public string ProviderOrRecieverOKPO_DB { get; set; } = "";

    private bool ProviderOrRecieverOKPO_Hidden_Priv { get; set; }

    [NotMapped]
    public bool ProviderOrRecieverOKPO_Hidden
    {
        get => ProviderOrRecieverOKPO_Hidden_Priv;
        set => ProviderOrRecieverOKPO_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "ОКПО", "поставщика или получателя", "14")]
    public RamAccess<string> ProviderOrRecieverOKPO
    {
        get
        {
            if (!ProviderOrRecieverOKPO_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(ProviderOrRecieverOKPO)))
                {
                    ((RamAccess<string>)Dictionary[nameof(ProviderOrRecieverOKPO)]).Value = ProviderOrRecieverOKPO_DB;
                    return (RamAccess<string>)Dictionary[nameof(ProviderOrRecieverOKPO)];
                }
                var rm = new RamAccess<string>(ProviderOrRecieverOKPO_Validation, ProviderOrRecieverOKPO_DB);
                rm.PropertyChanged += ProviderOrRecieverOKPOValueChanged;
                Dictionary.Add(nameof(ProviderOrRecieverOKPO), rm);
                return (RamAccess<string>)Dictionary[nameof(ProviderOrRecieverOKPO)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!ProviderOrRecieverOKPO_Hidden_Priv)
            {
                ProviderOrRecieverOKPO_DB = value.Value;
                OnPropertyChanged(nameof(ProviderOrRecieverOKPO));
            }
        }
    }

    private void ProviderOrRecieverOKPOValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
            if (Spravochniks.OKSM.Contains(value1.ToUpper()))
            {
                value1 = value1.ToUpper();
            }
        ProviderOrRecieverOKPO_DB = value1;
    }

    private static bool ProviderOrRecieverOKPO_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (Spravochniks.OKSM.Contains(value.Value.ToUpper()))
        {
            return true;
        }
        if (value.Value.Equals("Минобороны"))
        {
            return true;
        }
        if (value.Value.Length != 8 && value.Value.Length != 14)
        {
            value.AddError("Недопустимое значение"); return false;
        }
        if (!new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$").IsMatch(value.Value))
        {
            value.AddError("Недопустимое значение"); return false;
        }
        return true;
    }

    #endregion

    #region TransporterOKPO (15)

    public string TransporterOKPO_DB { get; set; } = "";

    private bool TransporterOKPO_Hidden_Priv { get; set; }

    [NotMapped]
    public bool TransporterOKPO_Hidden
    {
        get => TransporterOKPO_Hidden_Priv;
        set => TransporterOKPO_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "ОКПО", "перевозчика", "15")]
    public RamAccess<string> TransporterOKPO
    {
        get
        {
            if (!TransporterOKPO_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(TransporterOKPO)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TransporterOKPO)]).Value = TransporterOKPO_DB;
                    return (RamAccess<string>)Dictionary[nameof(TransporterOKPO)];
                }
                var rm = new RamAccess<string>(TransporterOKPO_Validation, TransporterOKPO_DB);
                rm.PropertyChanged += TransporterOKPOValueChanged;
                Dictionary.Add(nameof(TransporterOKPO), rm);
                return (RamAccess<string>)Dictionary[nameof(TransporterOKPO)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!TransporterOKPO_Hidden_Priv)
            {
                TransporterOKPO_DB = value.Value;
                OnPropertyChanged(nameof(TransporterOKPO));
            }
        }
    }

    private void TransporterOKPOValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            TransporterOKPO_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool TransporterOKPO_Validation(RamAccess<string> value)//Done
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value.Equals("-") || value.Value.Equals("Минобороны"))
        {
            return true;
        }
        if (value.Value.Equals("прим."))
        {
            //if ((TransporterOKPONote == null) || TransporterOKPONote.Equals(""))
            //    value.AddError( "Заполните примечание");
            return true;
        }
        if (value.Value.Length != 8 && value.Value.Length != 14)
        {
            value.AddError("Недопустимое значение"); return false;
        }
        if (!new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$").IsMatch(value.Value))
        {
            value.AddError("Недопустимое значение"); return false;
        }
        return true;
    }

    #endregion

    #region StoragePlaceName (16)

    public string StoragePlaceName_DB { get; set; } = "";

    private bool StoragePlaceName_Hidden_Priv { get; set; }

    [NotMapped]
    public bool StoragePlaceName_Hidden
    {
        get => StoragePlaceName_Hidden_Priv;
        set => StoragePlaceName_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "Пункт хранения", "наименование", "16")]
    public RamAccess<string> StoragePlaceName
    {
        get
        {
            if (!StoragePlaceName_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(StoragePlaceName)))
                {
                    ((RamAccess<string>)Dictionary[nameof(StoragePlaceName)]).Value = StoragePlaceName_DB;
                    return (RamAccess<string>)Dictionary[nameof(StoragePlaceName)];
                }
                var rm = new RamAccess<string>(StoragePlaceName_Validation, StoragePlaceName_DB);
                rm.PropertyChanged += StoragePlaceNameValueChanged;
                Dictionary.Add(nameof(StoragePlaceName), rm);
                return (RamAccess<string>)Dictionary[nameof(StoragePlaceName)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!StoragePlaceName_Hidden_Priv)
            {
                StoragePlaceName_DB = value.Value;
                OnPropertyChanged(nameof(StoragePlaceName));
            }
        }
    }

    private void StoragePlaceNameValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            StoragePlaceName_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool StoragePlaceName_Validation(RamAccess<string> value)//Ready
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        //List<string> spr = new List<string>();
        //if (!spr.Contains(value.Value))
        //{
        //    value.AddError("Недопустимое значение");
        //    return false;
        //}
        return true;
    }

    #endregion

    #region StoragePlaceCode (17)

    public string StoragePlaceCode_DB { get; set; } = "";

    private bool StoragePlaceCode_Hidden_Priv { get; set; }

    [NotMapped]
    public bool StoragePlaceCode_Hidden
    {
        get => StoragePlaceCode_Hidden_Priv;
        set => StoragePlaceCode_Hidden_Priv = value;
    }

    [NotMapped]
    [FormProperty(true, "Пункт хранения", "код", "17")]
    public RamAccess<string> StoragePlaceCode //8 cyfer code or - .
    {
        get
        {
            if (!StoragePlaceCode_Hidden_Priv)
            {
                if (Dictionary.ContainsKey(nameof(StoragePlaceCode)))
                {
                    ((RamAccess<string>)Dictionary[nameof(StoragePlaceCode)]).Value = StoragePlaceCode_DB;
                    return (RamAccess<string>)Dictionary[nameof(StoragePlaceCode)];
                }
                var rm = new RamAccess<string>(StoragePlaceCode_Validation, StoragePlaceCode_DB);
                rm.PropertyChanged += StoragePlaceCodeValueChanged;
                Dictionary.Add(nameof(StoragePlaceCode), rm);
                return (RamAccess<string>)Dictionary[nameof(StoragePlaceCode)];
            }
            var tmp = new RamAccess<string>(null, null);
            return tmp;
        }
        set
        {
            if (!StoragePlaceCode_Hidden_Priv)
            {
                StoragePlaceCode_DB = value.Value;
                OnPropertyChanged(nameof(StoragePlaceCode));
            }
        }
    }

    private void StoragePlaceCodeValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            StoragePlaceCode_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool StoragePlaceCode_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        //List<string> lst = new List<string>();//HERE binds spr
        //if (!lst.Contains(value.Value))
        //{
        //    value.AddError("Недопустимое значение"); return false;
        //}
        //return true;
        if (value.Value == "-") return true;
        if (!new Regex("^[0-9]{8}$").IsMatch(value.Value))
        {
            value.AddError("Недопустимое значение"); return false;
        }
        var tmp = value.Value;
        if (tmp.Length != 8) return true;
        if (!new Regex("^[1-9]").IsMatch(tmp[..1]))
        {
            value.AddError($"Недопустимый вид пункта - {tmp[..1]}");
        }
        if (!new Regex("^[1-3]").IsMatch(tmp.Substring(1, 1)))
        {
            value.AddError($"Недопустимое состояние пункта - {tmp.Substring(1, 1)}");
        }
        if (!new Regex("^[1-2]").IsMatch(tmp.Substring(2, 1)))
        {
            value.AddError($"Недопустимая изоляция от окружающей среды - {tmp.Substring(2, 1)}");
        }
        if (!new Regex("^[1-59]").IsMatch(tmp.Substring(3, 1)))
        {
            value.AddError($"Недопустимая зона нахождения пункта - {tmp.Substring(3, 1)}");
        }
        if (!new Regex("^[0-4]").IsMatch(tmp.Substring(4, 1)))
        {
            value.AddError($"Недопустимое значение пункта - {tmp.Substring(4, 1)}");
        }
        if (!new Regex("^[1-49]").IsMatch(tmp.Substring(5, 1)))
        {
            value.AddError($"Недопустимое размещение пункта хранения относительно поверхности земли - {tmp.Substring(5, 1)}");
        }
        if (!new Regex("^[1]{1}[1-9]{1}|^[2]{1}[1-69]{1}|^[3]{1}[1]{1}|^[4]{1}[1-49]{1}|^[5]{1}[1-69]{1}|^[6]{1}[1]{1}|^[7]{1}[1349]{1}|^[8]{1}[1-69]{1}|^[9]{1}[9]{1}")
                .IsMatch(tmp.Substring(6, 2)))
        {
            value.AddError($"Недопустимый код типа РАО - {tmp.Substring(6, 2)}");
        }
        return !value.HasErrors;
    }

    #endregion

    #region CodeRAO (18)

    public string CodeRAO_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "код", "18")]
    public RamAccess<string> CodeRAO
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(CodeRAO)))
            {
                ((RamAccess<string>)Dictionary[nameof(CodeRAO)]).Value = CodeRAO_DB;
                return (RamAccess<string>)Dictionary[nameof(CodeRAO)];
            }
            var rm = new RamAccess<string>(CodeRAO_Validation, CodeRAO_DB);
            rm.PropertyChanged += CodeRAOValueChanged;
            Dictionary.Add(nameof(CodeRAO), rm);
            return (RamAccess<string>)Dictionary[nameof(CodeRAO)];
        }
        set
        {
            CodeRAO_DB = value.Value;
            OnPropertyChanged(nameof(CodeRAO));
        }
    }

    private void CodeRAOValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var tmp = ((RamAccess<string>)value).Value.ToLower();
        tmp = tmp.Replace("х", "x");
        CodeRAO_DB = tmp;
    }

    private static bool CodeRAO_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        var tmp = value.Value.ToLower();
        tmp = tmp.Replace("х", "x");
        if (!new Regex("^[0-9x+]{11}$").IsMatch(tmp))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }

    #endregion

    #region StatusRAO (19)

    public string StatusRAO_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "статус", "19")]
    public RamAccess<string> StatusRAO  //1 cyfer or OKPO.
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(StatusRAO)))
            {
                ((RamAccess<string>)Dictionary[nameof(StatusRAO)]).Value = StatusRAO_DB;
                return (RamAccess<string>)Dictionary[nameof(StatusRAO)];
            }
            var rm = new RamAccess<string>(StatusRAO_Validation, StatusRAO_DB);
            rm.PropertyChanged += StatusRAOValueChanged;
            Dictionary.Add(nameof(StatusRAO), rm);
            return (RamAccess<string>)Dictionary[nameof(StatusRAO)];
        }
        set
        {
            StatusRAO_DB = value.Value;
            OnPropertyChanged(nameof(StatusRAO));
        }
    }

    private void StatusRAOValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            StatusRAO_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool StatusRAO_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value.Length == 1)
        {
            if (!int.TryParse(value.Value, out var intValue) || intValue < 1 || (intValue > 4 && intValue != 6 && intValue != 9))
            {
                value.AddError("Недопустимое значение"); 
                return false;
            }
        }
        if (value.Value.Length != 8 && value.Value.Length != 14)
        {
            value.AddError("Недопустимое значение"); 
            return false;
        }
        if (!new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$").IsMatch(value.Value))
        {
            value.AddError("Недопустимое значение"); 
            return false;
        }
        return true;
    }

    #endregion

    #region Volume20 (20)

    public string Volume20_DB { get; set; }

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "объем, куб. м", "20")]
    public RamAccess<string> Volume20
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(Volume20)))
            {
                ((RamAccess<string>)Dictionary[nameof(Volume20)]).Value = Volume20_DB;
                return (RamAccess<string>)Dictionary[nameof(Volume20)];
            }
            var rm = new RamAccess<string>(Volume20_Validation, Volume20_DB);
            rm.PropertyChanged += Volume20ValueChanged;
            Dictionary.Add(nameof(Volume20), rm);
            return (RamAccess<string>)Dictionary[nameof(Volume20)];
        }
        set
        {
            Volume20_DB = value.Value;
            OnPropertyChanged(nameof(Volume20));
        }
    }

    private void Volume20ValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
        {
            value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                Volume20_DB = value1;
                return;
            }
            if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        Volume20_DB = value1;
    }

    private static bool Volume20_Validation(RamAccess<string> value)
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
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region Mass21 (21)

    public string Mass21_DB { get; set; }

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "масса, т", "21")]
    public RamAccess<string> Mass21
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(Mass21)))
            {
                ((RamAccess<string>)Dictionary[nameof(Mass21)]).Value = Mass21_DB;
                return (RamAccess<string>)Dictionary[nameof(Mass21)];
            }
            var rm = new RamAccess<string>(Mass21_Validation, Mass21_DB);
            rm.PropertyChanged += Mass21ValueChanged;
            Dictionary.Add(nameof(Mass21), rm);
            return (RamAccess<string>)Dictionary[nameof(Mass21)];
        }
        set
        {
            Mass21_DB = value.Value;
            OnPropertyChanged(nameof(Mass21));
        }
    }

    private void Mass21ValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
        {
            value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                Mass21_DB = value1;
                return;
            }
            if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        Mass21_DB = value1;
    }

    private static bool Mass21_Validation(RamAccess<string> value)//TODO
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
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region TritiumActivity (22)

    public string TritiumActivity_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "тритий", "22")]
    public RamAccess<string> TritiumActivity
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(TritiumActivity)))
            {
                ((RamAccess<string>)Dictionary[nameof(TritiumActivity)]).Value = TritiumActivity_DB;
                return (RamAccess<string>)Dictionary[nameof(TritiumActivity)];
            }
            var rm = new RamAccess<string>(TritiumActivity_Validation, TritiumActivity_DB);
            rm.PropertyChanged += TritiumActivityValueChanged;
            Dictionary.Add(nameof(TritiumActivity), rm);
            return (RamAccess<string>)Dictionary[nameof(TritiumActivity)];
        }
        set
        {
            TritiumActivity_DB = value.Value;
            OnPropertyChanged(nameof(TritiumActivity));
        }
    }

    private void TritiumActivityValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
        {
            value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                TritiumActivity_DB = value1;
                return;
            }
            if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        TritiumActivity_DB = value1;
    }

    private static bool TritiumActivity_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "-")
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region BetaGammaActivity (23)

    public string BetaGammaActivity_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "бета-, гамма-излучающие радионуклиды (исключая тритий)", "23")]
    public RamAccess<string> BetaGammaActivity
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(BetaGammaActivity)))
            {
                ((RamAccess<string>)Dictionary[nameof(BetaGammaActivity)]).Value = BetaGammaActivity_DB;
                return (RamAccess<string>)Dictionary[nameof(BetaGammaActivity)];
            }
            var rm = new RamAccess<string>(BetaGammaActivity_Validation, BetaGammaActivity_DB);
            rm.PropertyChanged += BetaGammaActivityValueChanged;
            Dictionary.Add(nameof(BetaGammaActivity), rm);
            return (RamAccess<string>)Dictionary[nameof(BetaGammaActivity)];
        }
        set
        {
            BetaGammaActivity_DB = value.Value;
            OnPropertyChanged(nameof(BetaGammaActivity));
        }
    }

    private void BetaGammaActivityValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
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
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        BetaGammaActivity_DB = value1;
    }

    private static bool BetaGammaActivity_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "-")
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region AlphaActivity (24)

    public string AlphaActivity_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "альфа-излучающие радионуклиды (исключая трансурановые)", "24")]
    public RamAccess<string> AlphaActivity
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(AlphaActivity)))
            {
                ((RamAccess<string>)Dictionary[nameof(AlphaActivity)]).Value = AlphaActivity_DB;
                return (RamAccess<string>)Dictionary[nameof(AlphaActivity)];
            }
            var rm = new RamAccess<string>(AlphaActivity_Validation, AlphaActivity_DB);
            rm.PropertyChanged += AlphaActivityValueChanged;
            Dictionary.Add(nameof(AlphaActivity), rm);
            return (RamAccess<string>)Dictionary[nameof(AlphaActivity)];
        }
        set
        {
            AlphaActivity_DB = value.Value;
            OnPropertyChanged(nameof(AlphaActivity));
        }
    }

    private void AlphaActivityValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
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
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        AlphaActivity_DB = value1;
    }

    private static bool AlphaActivity_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "-")
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region TransuraniumActivity (25)

    public string TransuraniumActivity_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "трансурановые радионуклиды", "25")]
    public RamAccess<string> TransuraniumActivity
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(TransuraniumActivity)))
            {
                ((RamAccess<string>)Dictionary[nameof(TransuraniumActivity)]).Value = TransuraniumActivity_DB;
                return (RamAccess<string>)Dictionary[nameof(TransuraniumActivity)];
            }
            var rm = new RamAccess<string>(TransuraniumActivity_Validation, TransuraniumActivity_DB);
            rm.PropertyChanged += TransuraniumActivityValueChanged;
            Dictionary.Add(nameof(TransuraniumActivity), rm);
            return (RamAccess<string>)Dictionary[nameof(TransuraniumActivity)];
        }
        set
        {
            TransuraniumActivity_DB = value.Value;
            OnPropertyChanged(nameof(TransuraniumActivity));
        }
    }

    private void TransuraniumActivityValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        var value1 = ((RamAccess<string>)value).Value;
        if (value1 != null)
        {
            value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if (value1.Equals("-"))
            {
                TransuraniumActivity_DB = value1;
                return;
            }
            if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            if (double.TryParse(value1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var doubleValue))
            {
                value1 = $"{doubleValue:0.######################################################e+00}";
            }
        }
        TransuraniumActivity_DB = value1;
    }

    private static bool TransuraniumActivity_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "-")
        {
            return true;
        }
        var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
        if (!value1.Contains('e') && value1.Contains('+') ^ value1.Contains('-'))
        {
            value1 = value1.Replace("+", "e+").Replace("-", "e-");
        }
        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
        if (!double.TryParse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB"), out var doubleValue))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        if (doubleValue <= 0)
        {
            value.AddError("Число должно быть больше нуля"); 
            return false;
        }
        return true;
    }

    #endregion

    #region RefineOrSortRAOCode (26)

    public string RefineOrSortRAOCode_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "Характеристика ЖРО", "Код переработки / сортировки РАО", "26")]
    public RamAccess<string> RefineOrSortRAOCode //2 cyfer code or empty.
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(RefineOrSortRAOCode)))
            {
                ((RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)]).Value = RefineOrSortRAOCode_DB;
                return (RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)];
            }
            var rm = new RamAccess<string>(RefineOrSortRAOCode_Validation, RefineOrSortRAOCode_DB);
            rm.PropertyChanged += RefineOrSortRAOCodeValueChanged;
            Dictionary.Add(nameof(RefineOrSortRAOCode), rm);
            return (RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)];
        }
        set
        {
            RefineOrSortRAOCode_DB = value.Value;
            OnPropertyChanged(nameof(RefineOrSortRAOCode));
        }
    }//If change this change validation

    private void RefineOrSortRAOCodeValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            RefineOrSortRAOCode_DB = ((RamAccess<string>)value).Value;
        }
    }

    private bool RefineOrSortRAOCode_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value))
        {
            return true;
        }
        if (value.Value == "-")
        {
            return true;
        }
        if (OperationCode.Value == "55")
        {
            if (!Spravochniks.SprRifineOrSortCodes.Contains(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
        }
        else if (string.IsNullOrEmpty(OperationCode.Value))
        {
            value.AddError("Не указан код операции");
            return false;
        }
        return true;
    }

    #endregion

    #region Subsidy (27)

    public string Subsidy_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "null-27", "Субсидия, %", "27")]
    public RamAccess<string> Subsidy // 0<number<=100 or empty.
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(Subsidy)))
            {
                ((RamAccess<string>)Dictionary[nameof(Subsidy)]).Value = Subsidy_DB;
                return (RamAccess<string>)Dictionary[nameof(Subsidy)];
            }
            var rm = new RamAccess<string>(Subsidy_Validation, Subsidy_DB);
            rm.PropertyChanged += SubsidyValueChanged;
            Dictionary.Add(nameof(Subsidy), rm);
            return (RamAccess<string>)Dictionary[nameof(Subsidy)];
        }
        set
        {
            Subsidy_DB = value.Value;
            OnPropertyChanged(nameof(Subsidy));
        }
    }

    private void SubsidyValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            Subsidy_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool Subsidy_Validation(RamAccess<string> value)//Ready
    {
        value.ClearErrors();
        if (string.IsNullOrEmpty(value.Value) || value.Value.Equals("-"))
        {
            return true;
        }
        if (!int.TryParse(value.Value, out var intValue) || intValue is not (>= 0 and <= 100))
        {
            value.AddError("Недопустимое значение");
            return false;
        }
        return true;
    }

    #endregion

    #region FcpNumber (28)

    public string FcpNumber_DB { get; set; } = "";

    [NotMapped]
    [FormProperty(true, "null-28", "Номер мероприятия ФЦП", "28")]
    public RamAccess<string> FcpNumber
    {
        get
        {
            if (Dictionary.ContainsKey(nameof(FcpNumber)))
            {
                ((RamAccess<string>)Dictionary[nameof(FcpNumber)]).Value = FcpNumber_DB;
                return (RamAccess<string>)Dictionary[nameof(FcpNumber)];
            }
            var rm = new RamAccess<string>(FcpNumber_Validation, FcpNumber_DB);
            rm.PropertyChanged += FcpNumberValueChanged;
            Dictionary.Add(nameof(FcpNumber), rm);
            return (RamAccess<string>)Dictionary[nameof(FcpNumber)];
        }
        set
        {
            FcpNumber_DB = value.Value;
            OnPropertyChanged(nameof(FcpNumber));
        }
    }

    private void FcpNumberValueChanged(object value, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value")
        {
            FcpNumber_DB = ((RamAccess<string>)value).Value;
        }
    }

    private static bool FcpNumber_Validation(RamAccess<string> value)//TODO
    {
        value.ClearErrors();
        return true;
    }

    #endregion 

    #endregion

    #region IExcel

    public void ExcelGetRow(ExcelWorksheet worksheet, int row)
    {
        base.ExcelGetRow(worksheet, row);
        IndividualNumberZHRO_DB = Convert.ToString(worksheet.Cells[row, 4].Value);
        PassportNumber_DB = Convert.ToString(worksheet.Cells[row, 5].Value);
        Volume6_DB = ConvertFromExcelDouble(worksheet.Cells[row, 6].Value);
        Mass7_DB = ConvertFromExcelDouble(worksheet.Cells[row, 7].Value);
        SaltConcentration_DB = ConvertFromExcelDouble(worksheet.Cells[row, 8].Value);
        Radionuclids_DB = Convert.ToString(worksheet.Cells[row, 9].Value);
        SpecificActivity_DB = ConvertFromExcelDouble(worksheet.Cells[row, 10].Value);
        DocumentVid_DB = byte.TryParse(Convert.ToString(worksheet.Cells[row, 11].Value), out var byteValue) ? byteValue : null;
        DocumentNumber_DB = Convert.ToString(worksheet.Cells[row, 12].Value);
        DocumentDate_DB = ConvertFromExcelDate(worksheet.Cells[row, 13].Text);
        ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[row, 14].Value);
        TransporterOKPO_DB = Convert.ToString(worksheet.Cells[row, 15].Value);
        StoragePlaceName_DB = Convert.ToString(worksheet.Cells[row, 16].Value);
        StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[row, 17].Value);
        CodeRAO_DB = Convert.ToString(worksheet.Cells[row, 18].Value);
        StatusRAO_DB = Convert.ToString(worksheet.Cells[row, 19].Value);
        Volume20_DB = ConvertFromExcelDouble(worksheet.Cells[row, 20].Value);
        Mass21_DB = ConvertFromExcelDouble(worksheet.Cells[row, 21].Value);
        TritiumActivity_DB = ConvertFromExcelDouble(worksheet.Cells[row, 22].Value);
        BetaGammaActivity_DB = ConvertFromExcelDouble(worksheet.Cells[row, 23].Value);
        AlphaActivity_DB = ConvertFromExcelDouble(worksheet.Cells[row, 24].Value);
        TransuraniumActivity_DB = ConvertFromExcelDouble(worksheet.Cells[row, 25].Value);
        RefineOrSortRAOCode_DB = Convert.ToString(worksheet.Cells[row, 26].Value);
        Subsidy_DB = Convert.ToString(worksheet.Cells[row, 27].Value);
        FcpNumber_DB = Convert.ToString(worksheet.Cells[row, 28].Value);
    }

    public int ExcelRow(ExcelWorksheet worksheet, int row, int column, bool transpose = true, string sumNumber = "")
    {
        var cnt = base.ExcelRow(worksheet, row, column, transpose);
        column += transpose ? cnt : 0;
        row += !transpose ? cnt : 0;

        worksheet.Cells[row, column].Value = ConvertToExcelString(IndividualNumberZHRO_DB);
        worksheet.Cells[row + (!transpose ? 1 : 0), column + (transpose ? 1 : 0)].Value = ConvertToExcelString(PassportNumber_DB);
        worksheet.Cells[row + (!transpose ? 2 : 0), column + (transpose ? 2 : 0)].Value = ConvertToExcelDouble(Volume6_DB);
        worksheet.Cells[row + (!transpose ? 3 : 0), column + (transpose ? 3 : 0)].Value = ConvertToExcelDouble(Mass7_DB);
        worksheet.Cells[row + (!transpose ? 4 : 0), column + (transpose ? 4 : 0)].Value = ConvertToExcelDouble(SaltConcentration_DB);
        worksheet.Cells[row + (!transpose ? 5 : 0), column + (transpose ? 5 : 0)].Value = ConvertToExcelString(Radionuclids_DB);
        worksheet.Cells[row + (!transpose ? 6 : 0), column + (transpose ? 6 : 0)].Value = ConvertToExcelDouble(SpecificActivity_DB);
        worksheet.Cells[row + (!transpose ? 7 : 0), column + (transpose ? 7 : 0)].Value = DocumentVid_DB is null ? "-" : DocumentVid_DB;
        worksheet.Cells[row + (!transpose ? 8 : 0), column + (transpose ? 8 : 0)].Value = ConvertToExcelString(DocumentNumber_DB);
        worksheet.Cells[row + (!transpose ? 9 : 0), column + (transpose ? 9 : 0)].Value = ConvertToExcelDate(DocumentDate_DB);
        worksheet.Cells[row + (!transpose ? 10 : 0), column + (transpose ? 10 : 0)].Value = ConvertToExcelString(ProviderOrRecieverOKPO_DB);
        worksheet.Cells[row + (!transpose ? 11 : 0), column + (transpose ? 11 : 0)].Value = ConvertToExcelString(TransporterOKPO_DB);
        worksheet.Cells[row + (!transpose ? 12 : 0), column + (transpose ? 12 : 0)].Value = ConvertToExcelString(StoragePlaceName_DB);
        worksheet.Cells[row + (!transpose ? 13 : 0), column + (transpose ? 13 : 0)].Value = ConvertToExcelString(StoragePlaceCode_DB);
        worksheet.Cells[row + (!transpose ? 14 : 0), column + (transpose ? 14 : 0)].Value = ConvertToExcelString(CodeRAO_DB);
        worksheet.Cells[row + (!transpose ? 15 : 0), column + (transpose ? 15 : 0)].Value = ConvertToExcelString(StatusRAO_DB);
        worksheet.Cells[row + (!transpose ? 16 : 0), column + (transpose ? 16 : 0)].Value = ConvertToExcelDouble(Volume20_DB);
        worksheet.Cells[row + (!transpose ? 17 : 0), column + (transpose ? 17 : 0)].Value = ConvertToExcelDouble(Mass21_DB);
        worksheet.Cells[row + (!transpose ? 18 : 0), column + (transpose ? 18 : 0)].Value = ConvertToExcelDouble(TritiumActivity_DB);
        worksheet.Cells[row + (!transpose ? 19 : 0), column + (transpose ? 19 : 0)].Value = ConvertToExcelDouble(BetaGammaActivity_DB);
        worksheet.Cells[row + (!transpose ? 20 : 0), column + (transpose ? 20 : 0)].Value = ConvertToExcelDouble(AlphaActivity_DB);
        worksheet.Cells[row + (!transpose ? 21 : 0), column + (transpose ? 21 : 0)].Value = ConvertToExcelDouble(TransuraniumActivity_DB);
        worksheet.Cells[row + (!transpose ? 22 : 0), column + (transpose ? 22 : 0)].Value = ConvertToExcelString(RefineOrSortRAOCode_DB);
        worksheet.Cells[row + (!transpose ? 23 : 0), column + (transpose ? 23 : 0)].Value = ConvertToExcelString(Subsidy_DB);
        worksheet.Cells[row + (!transpose ? 24 : 0), column + (transpose ? 24 : 0)].Value = ConvertToExcelString(FcpNumber_DB);

        return 25;
    }

    public static int ExcelHeader(ExcelWorksheet worksheet, int row, int column, bool transpose = true)
    {
        var cnt = Form1.ExcelHeader(worksheet, row, column, transpose);
        column += +(transpose ? cnt : 0);
        row += !transpose ? cnt : 0;

        worksheet.Cells[row, column].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(IndividualNumberZHRO))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 1 : 0), column + (transpose ? 1 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(PassportNumber))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 2 : 0), column + (transpose ? 2 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(Volume6))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 3 : 0), column + (transpose ? 3 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(Mass7))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 4 : 0), column + (transpose ? 4 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(SaltConcentration))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 5 : 0), column + (transpose ? 5 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(Radionuclids))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 6 : 0), column + (transpose ? 6 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(SpecificActivity))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 7 : 0), column + (transpose ? 7 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(DocumentVid))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 8 : 0), column + (transpose ? 8 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(DocumentNumber))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 9 : 0), column + (transpose ? 9 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(DocumentDate))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 10 : 0), column + (transpose ? 10 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(ProviderOrRecieverOKPO))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 11 : 0), column + (transpose ? 11 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(TransporterOKPO))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 12 : 0), column + (transpose ? 12 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(StoragePlaceName))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 13 : 0), column + (transpose ? 13 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(StoragePlaceCode))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 14 : 0), column + (transpose ? 14 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(CodeRAO))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 15 : 0), column + (transpose ? 15 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(StatusRAO))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 16 : 0), column + (transpose ? 16 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(Volume20))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 17 : 0), column + (transpose ? 17 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(Mass21))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 18 : 0), column + (transpose ? 18 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(TritiumActivity))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 19 : 0), column + (transpose ? 19 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(BetaGammaActivity))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 20 : 0), column + (transpose ? 20 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(AlphaActivity))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 21 : 0), column + (transpose ? 21 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(TransuraniumActivity))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 22 : 0), column + (transpose ? 22 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(RefineOrSortRAOCode))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 23 : 0), column + (transpose ? 23 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(Subsidy))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];
        worksheet.Cells[row + (!transpose ? 24 : 0), column + (transpose ? 24 : 0)].Value = ((FormPropertyAttribute)Type.GetType("Models.Forms.Form1.Form18,Models")?.GetProperty(nameof(FcpNumber))?.GetCustomAttributes(typeof(FormPropertyAttribute), false).First())?.Names[1];

        return 25;
    }

    #endregion

    #region IDataGridColumn

    private static DataGridColumns _DataGridColumns { get; set; }

    public override DataGridColumns GetColumnStructure(string param = "")
    {
        if (_DataGridColumns != null) return _DataGridColumns;

        #region NumberInOrder (1)

        var numberInOrderR = ((FormPropertyAttribute)typeof(Form)
                .GetProperty(nameof(NumberInOrder))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            ?.GetDataColumnStructureD();
        if (numberInOrderR != null)
        {
            numberInOrderR.SetSizeColToAllLevels(50);
            numberInOrderR.Binding = nameof(NumberInOrder);
            numberInOrderR.Blocked = true;
            numberInOrderR.ChooseLine = true;
        }

        #endregion

        #region OperationCode (2)

        var operationCodeR = ((FormPropertyAttribute)typeof(Form1)
                .GetProperty(nameof(OperationCode))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            ?.GetDataColumnStructureD(numberInOrderR);
        if (operationCodeR != null)
        {
            operationCodeR.SetSizeColToAllLevels(88);
            operationCodeR.Binding = nameof(OperationCode);
            numberInOrderR += operationCodeR;
        }

        #endregion

        #region OperationDate (3)

        var operationDateR = ((FormPropertyAttribute)typeof(Form1)
                .GetProperty(nameof(OperationDate))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            ?.GetDataColumnStructureD(numberInOrderR);
        if (operationDateR != null)
        {
            operationDateR.SetSizeColToAllLevels(88);
            operationDateR.Binding = nameof(OperationDate);
            numberInOrderR += operationDateR;
        }

        #endregion

        #region IndividualNumberZHRO (4)

        var individualNumberZhroR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(IndividualNumberZHRO))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            ?.GetDataColumnStructureD(numberInOrderR);
        if (individualNumberZhroR != null)
        {
            individualNumberZhroR.SetSizeColToAllLevels(320);
            individualNumberZhroR.Binding = nameof(IndividualNumberZHRO);
            numberInOrderR += individualNumberZhroR;
        }

        #endregion

        #region PassportNumber (5)

        var passportNumberR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(PassportNumber))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            ?.GetDataColumnStructureD(numberInOrderR);
        if (passportNumberR != null)
        {
            passportNumberR.SetSizeColToAllLevels(100);
            passportNumberR.Binding = nameof(PassportNumber);
            numberInOrderR += passportNumberR;
        }

        #endregion

        #region Volume6 (6)

        var volume6R = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(Volume6))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            ?.GetDataColumnStructureD(numberInOrderR);
        if (volume6R != null)
        {
            volume6R.SetSizeColToAllLevels(88);
            volume6R.Binding = nameof(Volume6);
            numberInOrderR += volume6R;
        }

        #endregion

        #region Mass7 (7)

        var mass7R = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(Mass7))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            ?.GetDataColumnStructureD(numberInOrderR);
        if (mass7R != null)
        {
            mass7R.SetSizeColToAllLevels(88);
            mass7R.Binding = nameof(Mass7);
            numberInOrderR += mass7R;
        }

        #endregion

        #region SaltConcentration (8)

        var saltConcentrationR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(SaltConcentration))
                ?.GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            ?.GetDataColumnStructureD(numberInOrderR);
        saltConcentrationR.SetSizeColToAllLevels(125);
        saltConcentrationR.Binding = nameof(SaltConcentration);
        numberInOrderR += saltConcentrationR;
        
        #endregion

        #region Radionuclids (9)

        var radionuclidsR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(Radionuclids))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        radionuclidsR.SetSizeColToAllLevels(170);
        radionuclidsR.Binding = nameof(Radionuclids);
        numberInOrderR += radionuclidsR;

        #endregion

        #region SpecificActivity (10)

        var specificActivityR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(SpecificActivity))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        specificActivityR.SetSizeColToAllLevels(163);
        specificActivityR.Binding = nameof(SpecificActivity);
        numberInOrderR += specificActivityR;
        
        #endregion

        #region DocumentVid (11)

        var documentVidR = ((FormPropertyAttribute)typeof(Form1)
                .GetProperty(nameof(DocumentVid))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        documentVidR.SetSizeColToAllLevels(88);
        documentVidR.Binding = nameof(DocumentVid);
        numberInOrderR += documentVidR;
        
        #endregion

        #region DocumentNumber (12)

        var documentNumberR = ((FormPropertyAttribute)typeof(Form1)
                .GetProperty(nameof(DocumentNumber))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        documentNumberR.SetSizeColToAllLevels(103);
        documentNumberR.Binding = nameof(DocumentNumber);
        numberInOrderR += documentNumberR;
        
        #endregion

        #region DocumentDate (13)

        var documentDateR = ((FormPropertyAttribute)typeof(Form1)
                .GetProperty(nameof(DocumentDate))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        documentDateR.SetSizeColToAllLevels(88);
        documentDateR.Binding = nameof(DocumentDate);
        numberInOrderR += documentDateR;
        
        #endregion

        #region ProviderOrRecieverOKPO (14)

        var providerOrRecieverOkpoR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(ProviderOrRecieverOKPO))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        providerOrRecieverOkpoR.SetSizeColToAllLevels(100);
        providerOrRecieverOkpoR.Binding = nameof(ProviderOrRecieverOKPO);
        numberInOrderR += providerOrRecieverOkpoR;
        
        #endregion

        #region TransporterOKPO (15)

        var transporterOkpoR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(TransporterOKPO))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        transporterOkpoR.SetSizeColToAllLevels(163);
        transporterOkpoR.Binding = nameof(TransporterOKPO);
        numberInOrderR += transporterOkpoR;
        
        #endregion

        #region StoragePlaceName (16)

        var storagePlaceNameR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(StoragePlaceName))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        storagePlaceNameR.SetSizeColToAllLevels(103);
        storagePlaceNameR.Binding = nameof(StoragePlaceName);
        numberInOrderR += storagePlaceNameR;
        
        #endregion

        #region StoragePlaceCode (17)

        var storagePlaceCodeR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(StoragePlaceCode))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        storagePlaceCodeR.SetSizeColToAllLevels(88);
        storagePlaceCodeR.Binding = nameof(StoragePlaceCode);
        numberInOrderR += storagePlaceCodeR;
        
        #endregion

        #region CodeRAO (18)

        var codeRaoR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(CodeRAO))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        codeRaoR.SetSizeColToAllLevels(88);
        codeRaoR.Binding = nameof(CodeRAO);
        numberInOrderR += codeRaoR;
        
        #endregion

        #region StatusRAO (19)

        var statusRaoR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(StatusRAO))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        statusRaoR.SetSizeColToAllLevels(88);
        statusRaoR.Binding = nameof(StatusRAO);
        numberInOrderR += statusRaoR;
        
        #endregion

        #region Volume20 (20)

        var volume20R = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(Volume20))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        volume20R.SetSizeColToAllLevels(88);
        volume20R.Binding = nameof(Volume20);
        numberInOrderR += volume20R;
        
        #endregion

        #region Mass21 (21)

        var mass21R = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(Mass21))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        mass21R.SetSizeColToAllLevels(88);
        mass21R.Binding = nameof(Mass21);
        numberInOrderR += mass21R;

        #endregion

        #region TritiumActivity (22)

        var tritiumActivityR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(TritiumActivity))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        tritiumActivityR.SetSizeColToAllLevels(163);
        tritiumActivityR.Binding = nameof(TritiumActivity);
        numberInOrderR += tritiumActivityR;
        
        #endregion

        #region BetaGammaActivity (23)

        var betaGammaActivityR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(BetaGammaActivity))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        betaGammaActivityR.SetSizeColToAllLevels(160);
        betaGammaActivityR.Binding = nameof(BetaGammaActivity);
        numberInOrderR += betaGammaActivityR;
        
        #endregion

        #region AlphaActivity (24)

        var alphaActivityR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(AlphaActivity))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        alphaActivityR.SetSizeColToAllLevels(163);
        alphaActivityR.Binding = nameof(AlphaActivity);
        numberInOrderR += alphaActivityR;
        
        #endregion

        #region TransuraniumActivity (25)

        var transuraniumActivityR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(TransuraniumActivity))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        transuraniumActivityR.SetSizeColToAllLevels(200);
        transuraniumActivityR.Binding = nameof(TransuraniumActivity);
        numberInOrderR += transuraniumActivityR;
        
        #endregion

        #region RefineOrSortRAOCode (26)

        var refineOrSortRAOCodeR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(RefineOrSortRAOCode))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        refineOrSortRAOCodeR.SetSizeColToAllLevels(120);
        refineOrSortRAOCodeR.Binding = nameof(RefineOrSortRAOCode);
        numberInOrderR += refineOrSortRAOCodeR;
        
        #endregion

        #region Subsidy (27)

        var subsidyR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(Subsidy))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        subsidyR.SetSizeColToAllLevels(88);
        subsidyR.Binding = nameof(Subsidy);
        numberInOrderR += subsidyR;

        #endregion

        #region FcpNumber (28)

        var fcpNumberR = ((FormPropertyAttribute)typeof(Form18)
                .GetProperty(nameof(FcpNumber))
                .GetCustomAttributes(typeof(FormPropertyAttribute), true)
                .FirstOrDefault())
            .GetDataColumnStructureD(numberInOrderR);
        fcpNumberR.SetSizeColToAllLevels(163);
        fcpNumberR.Binding = nameof(FcpNumber);
        numberInOrderR += fcpNumberR;

        #endregion

        _DataGridColumns = numberInOrderR;
        return _DataGridColumns;
    }

    #endregion
}