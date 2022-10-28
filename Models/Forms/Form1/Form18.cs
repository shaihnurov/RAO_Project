﻿using Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Spravochniki;
using System.Linq;
using System.ComponentModel;
using Models.Abstracts;
using Models.Attributes;
using OfficeOpenXml;
using Models.Collections;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Models
{
    [Attributes.Form_Class("Форма 1.8: Сведения о жидких кондиционированных РАО")]
    public class Form18 : Abstracts.Form1
    {
        public Form18() : base()
        {
            FormNum.Value = "1.8";
            Validate_all();
        }
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
            return !(CodeRAO.HasErrors||
            IndividualNumberZHRO.HasErrors||
            SpecificActivity.HasErrors||
            SaltConcentration.HasErrors||
            Radionuclids.HasErrors||
            ProviderOrRecieverOKPO.HasErrors||
            TransporterOKPO.HasErrors||
            TritiumActivity.HasErrors||
            BetaGammaActivity.HasErrors||
            AlphaActivity.HasErrors||
            TransuraniumActivity.HasErrors||
            PassportNumber.HasErrors||
            RefineOrSortRAOCode.HasErrors||
            Subsidy.HasErrors||
            FcpNumber.HasErrors||
            StatusRAO.HasErrors||
            Volume6.HasErrors||
            Mass7.HasErrors||
            Volume20.HasErrors||
            Mass21.HasErrors||
            StoragePlaceName.HasErrors||
            StoragePlaceCode.HasErrors);
        }

        #region  Sum
        public bool Sum_DB { get; set; } = false;

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
                else
                {
                    var rm = new RamAccess<bool>(Sum_Validation, Sum_DB);
                    rm.PropertyChanged += SumValueChanged;
                    Dictionary.Add(nameof(Sum), rm);
                    return (RamAccess<bool>)Dictionary[nameof(Sum)];
                }
            }
            set
            {
                Sum_DB = value.Value;
                OnPropertyChanged(nameof(Sum));
            }
        }

        private void SumValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Sum_DB = ((RamAccess<bool>)Value).Value;
            }
        }

        private bool Sum_Validation(RamAccess<bool> value)
        {
            value.ClearErrors();
            return true;
        }
        #endregion

        #region IndividualNumberZHRO
        public string IndividualNumberZHRO_DB { get; set; } = "";
        public bool IndividualNumberZHRO_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool IndividualNumberZHRO_Hidden
        {
            get => IndividualNumberZHRO_Hidden_Priv;
            set
            {
                IndividualNumberZHRO_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"Сведения о партии ЖРО","индивидуальный номер (идентификационный код) партии ЖРО","4")]
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
                    else
                    {
                        var rm = new RamAccess<string>(IndividualNumberZHRO_Validation, IndividualNumberZHRO_DB);
                        rm.PropertyChanged += IndividualNumberZHROValueChanged;
                        Dictionary.Add(nameof(IndividualNumberZHRO), rm);
                        return (RamAccess<string>)Dictionary[nameof(IndividualNumberZHRO)];
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
                if (!IndividualNumberZHRO_Hidden_Priv)
                {
                    IndividualNumberZHRO_DB = value.Value;
                    OnPropertyChanged(nameof(IndividualNumberZHRO));
                }
            }
        }
        private void IndividualNumberZHROValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                IndividualNumberZHRO_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool IndividualNumberZHRO_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        #endregion

        #region PassportNumber
        public string PassportNumber_DB { get; set; } = "";
        public bool PassportNumber_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool PassportNumber_Hidden
        {
            get => PassportNumber_Hidden_Priv;
            set
            {
                PassportNumber_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"Сведения о партии ЖРО","номер паспорта","5")]
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
                    else
                    {
                        var rm = new RamAccess<string>(PassportNumber_Validation, PassportNumber_DB);
                        rm.PropertyChanged += PassportNumberValueChanged;
                        Dictionary.Add(nameof(PassportNumber), rm);
                        return (RamAccess<string>)Dictionary[nameof(PassportNumber)];
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
                if (!PassportNumber_Hidden_Priv)
                {
                    PassportNumber_DB = value.Value;
                    OnPropertyChanged(nameof(PassportNumber));
                }
            }
        }
        private void PassportNumberValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                PassportNumber_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool PassportNumber_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if(string.IsNullOrEmpty(value.Value))
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

        #region Volume6
        public string Volume6_DB { get; set; } = null;
        public bool Volume6_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool Volume6_Hidden
        {
            get => Volume6_Hidden_Priv;
            set
            {
                Volume6_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"Сведения о партии ЖРО","объем, куб. м","6")]
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
                    else
                    {
                        var rm = new RamAccess<string>(Volume6_Validation, Volume6_DB);
                        rm.PropertyChanged += Volume6ValueChanged;
                        Dictionary.Add(nameof(Volume6), rm);
                        return (RamAccess<string>)Dictionary[nameof(Volume6)];
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
                if (!Volume6_Hidden_Priv)
                {
                    Volume6_DB = value.Value;
                    OnPropertyChanged(nameof(Volume6));
                }
            }
        }
        private void Volume6ValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Volume6_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                Volume6_DB = value1;
            }
        }
        private bool Volume6_Validation(RamAccess<string> value)//TODO
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
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch (Exception)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region Mass7
        public string Mass7_DB { get; set; } = null;
        public bool Mass7_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool Mass7_Hidden
        {
            get => Mass7_Hidden_Priv;
            set
            {
                Mass7_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"Сведения о партии ЖРО","масса, т","7")]
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
                    else
                    {
                        var rm = new RamAccess<string>(Mass7_Validation, Mass7_DB);
                        rm.PropertyChanged += Mass7ValueChanged;
                        Dictionary.Add(nameof(Mass7), rm);
                        return (RamAccess<string>)Dictionary[nameof(Mass7)];
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
                if (!Mass7_Hidden_Priv)
                {
                    Mass7_DB = value.Value;
                    OnPropertyChanged(nameof(Mass7));
                }
            }
        }
        private void Mass7ValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Mass7_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                Mass7_DB = value1;
            }
        }
        private bool Mass7_Validation(RamAccess<string> value)//TODO
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
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch (Exception)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region SaltConcentration
        public string SaltConcentration_DB { get; set; } = null;
        public bool SaltConcentration_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool SaltConcentration_Hidden
        {
            get => SaltConcentration_Hidden_Priv;
            set
            {
                SaltConcentration_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"Сведения о партии ЖРО","солесодержание, г/л","8")]
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
                    else
                    {
                        var rm = new RamAccess<string>(SaltConcentration_Validation, SaltConcentration_DB);
                        rm.PropertyChanged += SaltConcentrationValueChanged;
                        Dictionary.Add(nameof(SaltConcentration), rm);
                        return (RamAccess<string>)Dictionary[nameof(SaltConcentration)];
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
                if (!SaltConcentration_Hidden_Priv)
                {
                    SaltConcentration_DB = value.Value;
                    OnPropertyChanged(nameof(SaltConcentration));
                }
            }
        }
        private void SaltConcentrationValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        SaltConcentration_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                SaltConcentration_DB = value1;
            }
        }
        private bool SaltConcentration_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
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
        #endregion

        #region Radionuclids
        public string Radionuclids_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true,"Сведения о партии ЖРО","наименование радионуклида","9")]
        public RamAccess<string> Radionuclids
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Radionuclids)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Radionuclids)]).Value = Radionuclids_DB;
                    return (RamAccess<string>)Dictionary[nameof(Radionuclids)];
                }
                else
                {
                    var rm = new RamAccess<string>(Radionuclids_Validation, Radionuclids_DB);
                    rm.PropertyChanged += RadionuclidsValueChanged;
                    Dictionary.Add(nameof(Radionuclids), rm);
                    return (RamAccess<string>)Dictionary[nameof(Radionuclids)];
                }
            }//OK
            set
            {
                Radionuclids_DB = value.Value;
                OnPropertyChanged(nameof(Radionuclids));
            }
        }//If change this change validation

        private void RadionuclidsValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Radionuclids_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool Radionuclids_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            string[] nuclids = value.Value.Split(";");
            for (int k = 0; k < nuclids.Length; k++)
            {
                nuclids[k] = nuclids[k].ToLower().Replace(" ", "");
            }
            bool flag = true;
            foreach (var nucl in nuclids)
            {
                var tmp = from item in Spravochniks.SprRadionuclids where nucl == item.Item1 select item.Item1;
                if (tmp.Count() == 0)
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

        #region SpecificActivity
        public string SpecificActivity_DB { get; set; } = null;
        [NotMapped]
        [Attributes.Form_Property(true,"Сведения о партии ЖРО","удельная активность, Бк/г","10")]
        public RamAccess<string> SpecificActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(SpecificActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(SpecificActivity)]).Value = SpecificActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(SpecificActivity_Validation, SpecificActivity_DB);
                    rm.PropertyChanged += SpecificActivityValueChanged;
                    Dictionary.Add(nameof(SpecificActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(SpecificActivity)];
                }
            }
            set
            {
                SpecificActivity_DB = value.Value;
                OnPropertyChanged(nameof(SpecificActivity));
            }
        }
        private void SpecificActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        SpecificActivity_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                SpecificActivity_DB = value1;
            }
        }
        private bool SpecificActivity_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        #endregion

        #region ProviderOrRecieverOKPO
        public string ProviderOrRecieverOKPO_DB { get; set; } = "";
        public bool ProviderOrRecieverOKPO_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool ProviderOrRecieverOKPO_Hidden
        {
            get => ProviderOrRecieverOKPO_Hidden_Priv;
            set
            {
                ProviderOrRecieverOKPO_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"ОКПО","поставщика или получателя","14")]
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
                    else
                    {
                        var rm = new RamAccess<string>(ProviderOrRecieverOKPO_Validation, ProviderOrRecieverOKPO_DB);
                        rm.PropertyChanged += ProviderOrRecieverOKPOValueChanged;
                        Dictionary.Add(nameof(ProviderOrRecieverOKPO), rm);
                        return (RamAccess<string>)Dictionary[nameof(ProviderOrRecieverOKPO)];
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
                if (!ProviderOrRecieverOKPO_Hidden_Priv)
                {
                    ProviderOrRecieverOKPO_DB = value.Value;
                    OnPropertyChanged(nameof(ProviderOrRecieverOKPO));
                }
            }
        }
        private void ProviderOrRecieverOKPOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                string value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                    if (Spravochniks.OKSM.Contains(value1.ToUpper()))
                    {
                        value1 = value1.ToUpper();
                    }
                ProviderOrRecieverOKPO_DB = value1;
            }
        }
        private bool ProviderOrRecieverOKPO_Validation(RamAccess<string> value)//TODO
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
            if ((value.Value.Length != 8) && (value.Value.Length != 14))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            Regex mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
            if (!mask.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region TransporterOKPO
        public string TransporterOKPO_DB { get; set; } = "";
        public bool TransporterOKPO_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool TransporterOKPO_Hidden
        {
            get => TransporterOKPO_Hidden_Priv;
            set
            {
                TransporterOKPO_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"ОКПО","перевозчика","15")]
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
                    else
                    {
                        var rm = new RamAccess<string>(TransporterOKPO_Validation, TransporterOKPO_DB);
                        rm.PropertyChanged += TransporterOKPOValueChanged;
                        Dictionary.Add(nameof(TransporterOKPO), rm);
                        return (RamAccess<string>)Dictionary[nameof(TransporterOKPO)];
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
                if (!TransporterOKPO_Hidden_Priv)
                {
                    TransporterOKPO_DB = value.Value;
                    OnPropertyChanged(nameof(TransporterOKPO));
                }
            }
        }
        private void TransporterOKPOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                TransporterOKPO_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool TransporterOKPO_Validation(RamAccess<string> value)//Done
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Equals("-")||value.Value.Equals("Минобороны"))
            {
                return true;
            }
            if (value.Value.Equals("прим."))
            {
                //if ((TransporterOKPONote == null) || TransporterOKPONote.Equals(""))
                //    value.AddError( "Заполните примечание");
                return true;
            }
            if ((value.Value.Length != 8) && (value.Value.Length != 14))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            Regex mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
            if (!mask.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region StoragePlaceName
        public string StoragePlaceName_DB { get; set; } = "";
        public bool StoragePlaceName_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool StoragePlaceName_Hidden
        {
            get => StoragePlaceName_Hidden_Priv;
            set
            {
                StoragePlaceName_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"Пункт хранения","наименование","16")]
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
                    else
                    {
                        var rm = new RamAccess<string>(StoragePlaceName_Validation, StoragePlaceName_DB);
                        rm.PropertyChanged += StoragePlaceNameValueChanged;
                        Dictionary.Add(nameof(StoragePlaceName), rm);
                        return (RamAccess<string>)Dictionary[nameof(StoragePlaceName)];
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
                if (!StoragePlaceName_Hidden_Priv)
                {
                    StoragePlaceName_DB = value.Value;
                    OnPropertyChanged(nameof(StoragePlaceName));
                }
            }
        }

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

        #region StoragePlaceCode
        public string StoragePlaceCode_DB { get; set; } = "";
        public bool StoragePlaceCode_Hidden_Priv { get; set; } = false;
        [NotMapped]
        public bool StoragePlaceCode_Hidden
        {
            get => StoragePlaceCode_Hidden_Priv;
            set
            {
                StoragePlaceCode_Hidden_Priv = value;
            }
        }
        [NotMapped]
        [Attributes.Form_Property(true,"Пункт хранения","код","17")]
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
                    else
                    {
                        var rm = new RamAccess<string>(StoragePlaceCode_Validation, StoragePlaceCode_DB);
                        rm.PropertyChanged += StoragePlaceCodeValueChanged;
                        Dictionary.Add(nameof(StoragePlaceCode), rm);
                        return (RamAccess<string>)Dictionary[nameof(StoragePlaceCode)];
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
                if (!StoragePlaceCode_Hidden_Priv)
                {
                    StoragePlaceCode_DB = value.Value;
                    OnPropertyChanged(nameof(StoragePlaceCode));
                }
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
            //List<string> lst = new List<string>();//HERE binds spr
            //if (!lst.Contains(value.Value))
            //{
            //    value.AddError("Недопустимое значение"); return false;
            //}
            //return true;
            if (value.Value == "-") return true;
            Regex a = new Regex("^[0-9]{8}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            var tmp = value.Value;
            if (tmp.Length == 8)
            {
                Regex a0 = new Regex("^[1-9]");
                if (!a0.IsMatch(tmp.Substring(0, 1)))
                {
                    value.AddError("Недопустимый вид пункта - " + tmp.Substring(0, 1));
                }
                Regex a1 = new Regex("^[1-3]");
                if (!a1.IsMatch(tmp.Substring(1, 1)))
                {
                    value.AddError("Недопустимое состояние пункта - " + tmp.Substring(1, 1));
                }
                Regex a2 = new Regex("^[1-2]");
                if (!a2.IsMatch(tmp.Substring(2, 1)))
                {
                    value.AddError("Недопустимая изоляция от окружающей среды - " + tmp.Substring(2, 1));
                }
                Regex a3 = new Regex("^[1-59]");
                if (!a3.IsMatch(tmp.Substring(3, 1)))
                {
                    value.AddError("Недопустимая зона нахождения пунтка - " + tmp.Substring(3, 1));
                }
                Regex a4 = new Regex("^[0-4]");
                if (!a4.IsMatch(tmp.Substring(4, 1)))
                {
                    value.AddError("Недопустимое значение пункта - " + tmp.Substring(4, 1));
                }
                Regex a5 = new Regex("^[1-49]");
                if (!a5.IsMatch(tmp.Substring(5, 1)))
                {
                    value.AddError("Недопустимое размещение пункта хранения относительно поверхности земли - " + tmp.Substring(5, 1));
                }
                Regex a67 = new Regex("^[1]{1}[1-9]{1}|^[2]{1}[1-69]{1}|^[3]{1}[1]{1}|^[4]{1}[1-49]{1}|^[5]{1}[1-69]{1}|^[6]{1}[1]{1}|^[7]{1}[1349]{1}|^[8]{1}[1-69]{1}|^[9]{1}[9]{1}");
                if (!a67.IsMatch(tmp.Substring(6, 2)))
                {
                    value.AddError("Недопустимоый код типа РАО - " + tmp.Substring(6, 2));
                }
                if (value.HasErrors)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region CodeRAO
        public string CodeRAO_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО","код","18")]
        public RamAccess<string> CodeRAO
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(CodeRAO)))
                {
                    ((RamAccess<string>)Dictionary[nameof(CodeRAO)]).Value = CodeRAO_DB;
                    return (RamAccess<string>)Dictionary[nameof(CodeRAO)];
                }
                else
                {
                    var rm = new RamAccess<string>(CodeRAO_Validation, CodeRAO_DB);
                    rm.PropertyChanged += CodeRAOValueChanged;
                    Dictionary.Add(nameof(CodeRAO), rm);
                    return (RamAccess<string>)Dictionary[nameof(CodeRAO)];
                }
            }
            set
            {
                CodeRAO_DB = value.Value;
                OnPropertyChanged(nameof(CodeRAO));
            }
        }
        private void CodeRAOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var tmp = ((RamAccess<string>)Value).Value.ToLower();
                tmp = tmp.Replace("х", "x");
                CodeRAO_DB = tmp;
            }
        }
        private bool CodeRAO_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if(string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            var tmp = value.Value.ToLower();
            tmp = tmp.Replace("х", "x");
            Regex a = new Regex("^[0-9x+]{11}$");
            if (!a.IsMatch(tmp))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        #endregion

        #region StatusRAO
        public string StatusRAO_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО","статус","19")]
        public RamAccess<string> StatusRAO  //1 cyfer or OKPO.
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(StatusRAO)))
                {
                    ((RamAccess<string>)Dictionary[nameof(StatusRAO)]).Value = StatusRAO_DB;
                    return (RamAccess<string>)Dictionary[nameof(StatusRAO)];
                }
                else
                {
                    var rm = new RamAccess<string>(StatusRAO_Validation, StatusRAO_DB);
                    rm.PropertyChanged += StatusRAOValueChanged;
                    Dictionary.Add(nameof(StatusRAO), rm);
                    return (RamAccess<string>)Dictionary[nameof(StatusRAO)];
                }
            }
            set
            {
                StatusRAO_DB = value.Value;
                OnPropertyChanged(nameof(StatusRAO));
            }
        }
        private void StatusRAOValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                StatusRAO_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool StatusRAO_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Length == 1)
            {
                int tmp;
                try
                {
                    tmp = int.Parse(value.Value);
                    if ((tmp < 1) || ((tmp > 4) && (tmp != 6) && (tmp != 9)))
                    {
                        value.AddError("Недопустимое значение"); return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    value.AddError("Недопустимое значение"); return false;
                }
            }
            if ((value.Value.Length != 8) && (value.Value.Length != 14))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            Regex mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
            if (!mask.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region Volume20
        public string Volume20_DB { get; set; } = null;
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО","объем, куб. м","20")]
        public RamAccess<string> Volume20
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Volume20)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Volume20)]).Value = Volume20_DB;
                    return (RamAccess<string>)Dictionary[nameof(Volume20)];
                }
                else
                {
                    var rm = new RamAccess<string>(Volume20_Validation, Volume20_DB);
                    rm.PropertyChanged += Volume20ValueChanged;
                    Dictionary.Add(nameof(Volume20), rm);
                    return (RamAccess<string>)Dictionary[nameof(Volume20)];
                }
            }
            set
            {
                Volume20_DB = value.Value;
                OnPropertyChanged(nameof(Volume20));
            }
        }
        private void Volume20ValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Volume20_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                Volume20_DB = value1;
            }
        }
        private bool Volume20_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region Mass21
        public string Mass21_DB { get; set; } = null;
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО","масса, т","21")]
        public RamAccess<string> Mass21
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Mass21)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Mass21)]).Value = Mass21_DB;
                    return (RamAccess<string>)Dictionary[nameof(Mass21)];
                }
                else
                {
                    var rm = new RamAccess<string>(Mass21_Validation, Mass21_DB);
                    rm.PropertyChanged += Mass21ValueChanged;
                    Dictionary.Add(nameof(Mass21), rm);
                    return (RamAccess<string>)Dictionary[nameof(Mass21)];
                }
            }
            set
            {
                Mass21_DB = value.Value;
                OnPropertyChanged(nameof(Mass21));
            }
        }
        private void Mass21ValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        Mass21_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                Mass21_DB = value1;
            }
        }
        private bool Mass21_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
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
        #endregion

        #region TritiumActivity
        public string TritiumActivity_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО","тритий","22")]
        public RamAccess<string> TritiumActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TritiumActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TritiumActivity)]).Value = TritiumActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(TritiumActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(TritiumActivity_Validation, TritiumActivity_DB);
                    rm.PropertyChanged += TritiumActivityValueChanged;
                    Dictionary.Add(nameof(TritiumActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(TritiumActivity)];
                }
            }
            set
            {
                TritiumActivity_DB = value.Value;
                OnPropertyChanged(nameof(TritiumActivity));
            }
        }
        private void TritiumActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        TritiumActivity_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                TritiumActivity_DB = value1;
            }
        }
        private bool TritiumActivity_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if(string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value == "-")
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region BetaGammaActivity
        public string BetaGammaActivity_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО","бета-, гамма-излучающие радионуклиды (исключая тритий)","23")]
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
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
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
                return true;
            }
            if (value.Value == "-")
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region AlphaActivity
        public string AlphaActivity_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО", "альфа-излучающие радионуклиды (исключая трансурановые)","24")]
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
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
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
            if(string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value == "-")
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region TransuraniumActivity
        public string TransuraniumActivity_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО", "трансурановые радионуклиды","25")]
        public RamAccess<string> TransuraniumActivity
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(TransuraniumActivity)))
                {
                    ((RamAccess<string>)Dictionary[nameof(TransuraniumActivity)]).Value = TransuraniumActivity_DB;
                    return (RamAccess<string>)Dictionary[nameof(TransuraniumActivity)];
                }
                else
                {
                    var rm = new RamAccess<string>(TransuraniumActivity_Validation, TransuraniumActivity_DB);
                    rm.PropertyChanged += TransuraniumActivityValueChanged;
                    Dictionary.Add(nameof(TransuraniumActivity), rm);
                    return (RamAccess<string>)Dictionary[nameof(TransuraniumActivity)];
                }
            }
            set
            {
                TransuraniumActivity_DB = value.Value;
                OnPropertyChanged(nameof(TransuraniumActivity));
            }
        }
        private void TransuraniumActivityValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                var value1 = ((RamAccess<string>)Value).Value;
                if (value1 != null)
                {
                    value1 = value1.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
                    if (value1.Equals("-"))
                    {
                        TransuraniumActivity_DB = value1;
                        return;
                    }
                    if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
                    {
                        value1 = value1.Replace("+", "e+").Replace("-", "e-");
                    }
                    try
                    {
                        var value2 = Convert.ToDouble(value1);
                        value1 = String.Format("{0:0.######################################################e+00}", value2);
                    }
                    catch (Exception ex)
                    { }
                }
                TransuraniumActivity_DB = value1;
            }
        }
        private bool TransuraniumActivity_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if(string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value == "-")
            {
                return true;
            }
            var value1 = value.Value.Replace('е', 'e').Replace('Е', 'e').Replace('E', 'e');
            if ((!value1.Contains('e')) && (value1.Contains('+') ^ value1.Contains('-')))
            {
                value1 = value1.Replace("+", "e+").Replace("-", "e-");
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value1, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region RefineOrSortRAOCode
        public string RefineOrSortRAOCode_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true,"Характеристика ЖРО", "Код переработки / сортировки РАО","26")]
        public RamAccess<string> RefineOrSortRAOCode //2 cyfer code or empty.
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(RefineOrSortRAOCode)))
                {
                    ((RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)]).Value = RefineOrSortRAOCode_DB;
                    return (RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)];
                }
                else
                {
                    var rm = new RamAccess<string>(RefineOrSortRAOCode_Validation, RefineOrSortRAOCode_DB);
                    rm.PropertyChanged += RefineOrSortRAOCodeValueChanged;
                    Dictionary.Add(nameof(RefineOrSortRAOCode), rm);
                    return (RamAccess<string>)Dictionary[nameof(RefineOrSortRAOCode)];
                }
            }
            set
            {
                RefineOrSortRAOCode_DB = value.Value;
                OnPropertyChanged(nameof(RefineOrSortRAOCode));
            }
        }//If change this change validation

        private void RefineOrSortRAOCodeValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                RefineOrSortRAOCode_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool RefineOrSortRAOCode_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if(value.Value == "-")
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
            else if(string.IsNullOrEmpty(OperationCode.Value))
            {
                value.AddError("Не указан код операции");
                return false;
            }
            return true;
        }
        #endregion

        #region Subsidy
        public string Subsidy_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true, "null-27","Субсидия, %","27")]
        public RamAccess<string> Subsidy // 0<number<=100 or empty.
        {
            get
            {
                if (Dictionary.ContainsKey(nameof(Subsidy)))
                {
                    ((RamAccess<string>)Dictionary[nameof(Subsidy)]).Value = Subsidy_DB;
                    return (RamAccess<string>)Dictionary[nameof(Subsidy)];
                }
                else
                {
                    var rm = new RamAccess<string>(Subsidy_Validation, Subsidy_DB);
                    rm.PropertyChanged += SubsidyValueChanged;
                    Dictionary.Add(nameof(Subsidy), rm);
                    return (RamAccess<string>)Dictionary[nameof(Subsidy)];
                }
            }
            set
            {
                Subsidy_DB = value.Value;
                OnPropertyChanged(nameof(Subsidy));
            }
        }
        private void SubsidyValueChanged(object Value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Value")
            {
                Subsidy_DB = ((RamAccess<string>)Value).Value;
            }
        }
        private bool Subsidy_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Equals("-"))
            {
                return true;
            }
            try
            {
                int tmp = int.Parse(value.Value);
                if (!((tmp > 0) && (tmp <= 100)))
                {
                    value.AddError("Недопустимое значение"); return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        #endregion

        #region FcpNumber
        public string FcpNumber_DB { get; set; } = "";
        [NotMapped]
        [Attributes.Form_Property(true, "null-28","Номер мероприятия ФЦП","28")]
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
        #endregion

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
            bool a0 = value.Value == "01";
            bool a2 = value.Value == "18";
            bool a3 = value.Value == "55";
            bool a4 = value.Value == "63";
            bool a5 = value.Value == "64";
            bool a6 = value.Value == "68";
            bool a7 = value.Value == "97";
            bool a8 = value.Value == "98";
            bool a9 = value.Value == "99";
            bool a12 = value.Value == "51";
            bool a13 = value.Value == "52";
            bool a14 = value.Value == "10";
            bool a10 = (int.Parse(value.Value) >= 21) && (int.Parse(value.Value) <= 29);
            bool a11 = (int.Parse(value.Value) >= 31) && (int.Parse(value.Value) <= 39);
            if (!(a0 || a2 || a3 || a4 || a5 || a6 || a7 || a8 || a9 || a10 || a11 || a12 || a13 || a14))
            {
                value.AddError("Код операции не может быть использован в форме 1.7");
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
            foreach (Tuple<byte?, string> item in Spravochniks.SprDocumentVidName)
            {
                if (value.Value == item.Item1)
                {
                    return true;
                }
            }
            value.AddError("Недопустимое значение");
            return false;
        }

        protected override bool DocumentDate_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if(string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value == "прим.")
            {
                return true;
            }
            var tmp = value.Value;
            Regex b1 = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{2}$");
            if (b1.IsMatch(tmp))
            {
                tmp = tmp.Insert(6, "20");
            }
            Regex a = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$");
            if (!a.IsMatch(tmp))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            try { DateTimeOffset.Parse(tmp); }
            catch (Exception)
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            bool b = (OperationCode.Value == "68");
            bool c = (OperationCode.Value == "52") || (OperationCode.Value == "55");
            bool d = (OperationCode.Value == "18") || (OperationCode.Value == "51");
            if (b || c || d)
            {
                if (!tmp.Equals(OperationDate))
                {
                    //value.AddError("Заполните примечание");//to do note handling
                    return true;
                }
            }
            return true;
        }

        #region IExcel
        public void ExcelGetRow(ExcelWorksheet worksheet, int Row)
        {
            double val;
            base.ExcelGetRow(worksheet, Row);
            IndividualNumberZHRO_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            PassportNumber_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            Volume6_DB = Convert.ToString(worksheet.Cells[Row, 6].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 6].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 6].Value);
            Mass7_DB = Convert.ToString(worksheet.Cells[Row, 7].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 7].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 7].Value);
            SaltConcentration_DB = Convert.ToString(worksheet.Cells[Row, 8].Value).Equals("0") ? "-" : Convert.ToString(worksheet.Cells[Row, 8].Value);
            Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            SpecificActivity_DB = Convert.ToString(worksheet.Cells[Row, 10].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 10].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 10].Value);
            DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 11].Value);
            DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            StoragePlaceName_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            CodeRAO_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            StatusRAO_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            Volume20_DB = Convert.ToString(worksheet.Cells[Row, 20].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 20].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 20].Value);
            Mass21_DB = Convert.ToString(worksheet.Cells[Row, 21].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 21].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 21].Value);
            TritiumActivity_DB = Convert.ToString(worksheet.Cells[Row, 22].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 22].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 22].Value);
            BetaGammaActivity_DB = Convert.ToString(worksheet.Cells[Row, 23].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 23].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 23].Value);
            AlphaActivity_DB = Convert.ToString(worksheet.Cells[Row, 24].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 24].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 24].Value);
            TransuraniumActivity_DB = Convert.ToString(worksheet.Cells[Row, 25].Value).Equals("0") ? "-" : double.TryParse(Convert.ToString(worksheet.Cells[Row, 25].Value), out val) ? val.ToString("0.00######################################################e+00", CultureInfo.InvariantCulture) : Convert.ToString(worksheet.Cells[Row, 25].Value);
            RefineOrSortRAOCode_DB = Convert.ToString(worksheet.Cells[Row, 26].Value);
            Subsidy_DB = Convert.ToString(worksheet.Cells[Row, 27].Value).Equals("0") ? "-" : Convert.ToString(worksheet.Cells[Row, 27].Value);
            FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 28].Value);
        }
        public int ExcelRow(ExcelWorksheet worksheet, int Row, int Column, bool Transpon = true, string SumNumber = "")
        {
            var cnt = base.ExcelRow(worksheet, Row, Column, Transpon);
            Column += (Transpon == true ? cnt : 0);
            Row += (Transpon == false ? cnt : 0);
            double val;

            worksheet.Cells[Row + (!Transpon ? 0 : 0), Column + (Transpon ? 0 : 0)].Value = IndividualNumberZHRO_DB;
            worksheet.Cells[Row + (!Transpon ? 1 : 0), Column + (Transpon ? 1 : 0)].Value = PassportNumber_DB;
            worksheet.Cells[Row + (!Transpon ? 2 : 0), Column + (Transpon ? 2 : 0)].Value = Volume6_DB== "" || Volume6_DB == "-" || Volume6_DB == null ? 0  : double.TryParse(Volume6_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : Volume6_DB;
            worksheet.Cells[Row + (!Transpon ? 3 : 0), Column + (Transpon ? 3 : 0)].Value = Mass7_DB== "" || Mass7_DB == "-" || Mass7_DB == null ? 0  : double.TryParse(Mass7_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : Mass7_DB;
            worksheet.Cells[Row + (!Transpon ? 4 : 0), Column + (Transpon ? 4 : 0)].Value = SaltConcentration_DB== "" || SaltConcentration_DB == "-" || SaltConcentration_DB == null ? 0  : int.TryParse(SaltConcentration_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out int valInt) ? valInt : SaltConcentration_DB;
            worksheet.Cells[Row + (!Transpon ? 5 : 0), Column + (Transpon ? 5 : 0)].Value = Radionuclids_DB;
            worksheet.Cells[Row + (!Transpon ? 6 : 0), Column + (Transpon ? 6 : 0)].Value = SpecificActivity_DB== "" || SpecificActivity_DB == "-" || SpecificActivity_DB == null ? 0  : double.TryParse(SpecificActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : SpecificActivity_DB;
            worksheet.Cells[Row + (!Transpon ? 7 : 0), Column + (Transpon ? 7 : 0)].Value = DocumentVid_DB;
            worksheet.Cells[Row + (!Transpon ? 8 : 0), Column + (Transpon ? 8 : 0)].Value = DocumentNumber_DB;
            worksheet.Cells[Row + (!Transpon ? 9 : 0), Column + (Transpon ? 9 : 0)].Value = DocumentDate_DB;
            worksheet.Cells[Row + (!Transpon ? 10 : 0), Column + (Transpon ? 10 : 0)].Value = ProviderOrRecieverOKPO_DB;
            worksheet.Cells[Row + (!Transpon ? 11 : 0), Column + (Transpon ? 11 : 0)].Value = TransporterOKPO_DB;
            worksheet.Cells[Row + (!Transpon ? 12 : 0), Column + (Transpon ? 12 : 0)].Value = StoragePlaceName_DB;
            worksheet.Cells[Row + (!Transpon ? 13 : 0), Column + (Transpon ? 13 : 0)].Value = StoragePlaceCode_DB;
            worksheet.Cells[Row + (!Transpon ? 14 : 0), Column + (Transpon ? 14 : 0)].Value = CodeRAO_DB;
            worksheet.Cells[Row + (!Transpon ? 15 : 0), Column + (Transpon ? 15 : 0)].Value = StatusRAO_DB;
            worksheet.Cells[Row + (!Transpon ? 16 : 0), Column + (Transpon ? 16 : 0)].Value = Volume20_DB== "" || Volume20_DB == "-" || Volume20_DB == null ? 0  : double.TryParse(Volume20_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : Volume20_DB;
            worksheet.Cells[Row + (!Transpon ? 17 : 0), Column + (Transpon ? 17 : 0)].Value = Mass21_DB== "" || Mass21_DB == "-" || Mass21_DB == null ? 0  : double.TryParse(Mass21_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : Mass21_DB;
            worksheet.Cells[Row + (!Transpon ? 18 : 0), Column + (Transpon ? 18 : 0)].Value = TritiumActivity_DB== "" || TritiumActivity_DB == "-" || TritiumActivity_DB == null ? 0  : double.TryParse(TritiumActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : TritiumActivity_DB;
            worksheet.Cells[Row + (!Transpon ? 19 : 0), Column + (Transpon ? 19 : 0)].Value = BetaGammaActivity_DB== "" || BetaGammaActivity_DB == "-" || BetaGammaActivity_DB == null ? 0  : double.TryParse(BetaGammaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : BetaGammaActivity_DB;
            worksheet.Cells[Row + (!Transpon ? 20 : 0), Column + (Transpon ? 20 : 0)].Value = AlphaActivity_DB== "" || AlphaActivity_DB == "-" || AlphaActivity_DB == null ? 0  : double.TryParse(AlphaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : AlphaActivity_DB;
            worksheet.Cells[Row + (!Transpon ? 21 : 0), Column + (Transpon ? 21 : 0)].Value = TransuraniumActivity_DB== "" || TransuraniumActivity_DB == "-" || TransuraniumActivity_DB == null ? 0  : double.TryParse(TransuraniumActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : TransuraniumActivity_DB;
            worksheet.Cells[Row + (!Transpon ? 22 : 0), Column + (Transpon ? 22 : 0)].Value = RefineOrSortRAOCode_DB;
            worksheet.Cells[Row + (!Transpon ? 23 : 0), Column + (Transpon ? 23 : 0)].Value = Subsidy_DB== "" || Subsidy_DB == "-" || Subsidy_DB == null ? 0  : int.TryParse(Subsidy_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : Subsidy_DB;
            worksheet.Cells[Row + (!Transpon ? 24 : 0), Column + (Transpon ? 24 : 0)].Value = FcpNumber_DB;

            return 25;
        }

        public static int ExcelHeader(ExcelWorksheet worksheet, int Row,int Column,bool Transpon=true)
        {
            var cnt = Form1.ExcelHeader(worksheet, Row, Column, Transpon);
            Column = Column + +(Transpon == true ? cnt : 0);
            Row = Row + (Transpon == false ? cnt : 0);

            worksheet.Cells[Row + (!Transpon ? 0 : 0), Column + (Transpon ? 0 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(IndividualNumberZHRO)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 1 : 0), Column + (Transpon ? 1 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(PassportNumber)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 2 : 0), Column + (Transpon ? 2 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(Volume6)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 3 : 0), Column + (Transpon ? 3 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(Mass7)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 4 : 0), Column + (Transpon ? 4 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(SaltConcentration)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 5 : 0), Column + (Transpon ? 5 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(Radionuclids)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 6 : 0), Column + (Transpon ? 6 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(SpecificActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 7 : 0), Column + (Transpon ? 7 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(DocumentVid)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 8 : 0), Column + (Transpon ? 8 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(DocumentNumber)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 9 : 0), Column + (Transpon ? 9 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(DocumentDate)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 10 : 0), Column + (Transpon ? 10 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(ProviderOrRecieverOKPO)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 11 : 0), Column + (Transpon ? 11 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(TransporterOKPO)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 12 : 0), Column + (Transpon ? 12 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(StoragePlaceName)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 13 : 0), Column + (Transpon ? 13 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(StoragePlaceCode)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 14 : 0), Column + (Transpon ? 14 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(CodeRAO)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 15 : 0), Column + (Transpon ? 15 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(StatusRAO)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 16 : 0), Column + (Transpon ? 16 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(Volume20)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 17 : 0), Column + (Transpon ? 17 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(Mass21)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 18 : 0), Column + (Transpon ? 18 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(TritiumActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 19 : 0), Column + (Transpon ? 19 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(BetaGammaActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 20 : 0), Column + (Transpon ? 20 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(AlphaActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 21 : 0), Column + (Transpon ? 21 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(TransuraniumActivity)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 22 : 0), Column + (Transpon ? 22 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(RefineOrSortRAOCode)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 23 : 0), Column + (Transpon ? 23 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(Subsidy)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];
            worksheet.Cells[Row + (!Transpon ? 24 : 0), Column + (Transpon ? 24 : 0)].Value = ((Form_PropertyAttribute)System.Type.GetType("Models.Form18,Models").GetProperty(nameof(FcpNumber)).GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Names[1];

            return 25;
        }
        #endregion

        #region IDataGridColumn
        private static DataGridColumns _DataGridColumns { get; set; } = null;
        public override DataGridColumns GetColumnStructure(string param = "")
        {
            if (_DataGridColumns == null)
            {
                #region NumberInOrder (1)
                DataGridColumns NumberInOrderR = ((Attributes.Form_PropertyAttribute)typeof(Form).GetProperty(nameof(Form.NumberInOrder)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD();
                NumberInOrderR.SetSizeColToAllLevels(50);
                NumberInOrderR.Binding = nameof(Form.NumberInOrder);
                NumberInOrderR.Blocked = true;
                NumberInOrderR.ChooseLine = true;
                #endregion
                #region OperationCode (2)
                DataGridColumns OperationCodeR = ((Attributes.Form_PropertyAttribute)typeof(Form1).GetProperty(nameof(Form1.OperationCode)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                OperationCodeR.SetSizeColToAllLevels(88);
                OperationCodeR.Binding = nameof(Form1.OperationCode);
                NumberInOrderR += OperationCodeR;
                #endregion
                #region OperationDate (3)
                DataGridColumns OperationDateR = ((Attributes.Form_PropertyAttribute)typeof(Form1).GetProperty(nameof(Form1.OperationDate)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                OperationDateR.SetSizeColToAllLevels(88);
                OperationDateR.Binding = nameof(Form1.OperationDate);
                NumberInOrderR += OperationDateR;
                #endregion
                #region IndividualNumberZHRO (4)
                DataGridColumns IndividualNumberZHROR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.IndividualNumberZHRO)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                IndividualNumberZHROR.SetSizeColToAllLevels(320);
                IndividualNumberZHROR.Binding = nameof(Form18.IndividualNumberZHRO);
                NumberInOrderR += IndividualNumberZHROR;
                #endregion
                #region PassportNumber (5)
                DataGridColumns PassportNumberR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.PassportNumber)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                PassportNumberR.SetSizeColToAllLevels(100);
                PassportNumberR.Binding = nameof(Form18.PassportNumber);
                NumberInOrderR += PassportNumberR;
                #endregion
                #region Volume6 (6)
                DataGridColumns Volume6R = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.Volume6)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                Volume6R.SetSizeColToAllLevels(88);
                Volume6R.Binding = nameof(Form18.Volume6);
                NumberInOrderR += Volume6R;
                #endregion
                #region Mass7 (7)
                DataGridColumns Mass7R = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.Mass7)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                Mass7R.SetSizeColToAllLevels(88);
                Mass7R.Binding = nameof(Form18.Mass7);
                NumberInOrderR += Mass7R;
                #endregion
                #region SaltConcentration (8)
                DataGridColumns SaltConcentrationR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.SaltConcentration)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                SaltConcentrationR.SetSizeColToAllLevels(125);
                SaltConcentrationR.Binding = nameof(Form18.SaltConcentration);
                NumberInOrderR += SaltConcentrationR;
                #endregion
                #region Radionuclids (9)
                DataGridColumns RadionuclidsR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.Radionuclids)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                RadionuclidsR.SetSizeColToAllLevels(170);
                RadionuclidsR.Binding = nameof(Form18.Radionuclids);
                NumberInOrderR += RadionuclidsR;
                #endregion
                #region SpecificActivity (10)
                DataGridColumns SpecificActivityR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.SpecificActivity)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                SpecificActivityR.SetSizeColToAllLevels(163);
                SpecificActivityR.Binding = nameof(Form18.SpecificActivity);
                NumberInOrderR += SpecificActivityR;
                #endregion
                #region DocumentVid (11)
                DataGridColumns DocumentVidR = ((Attributes.Form_PropertyAttribute)typeof(Form1).GetProperty(nameof(Form1.DocumentVid)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                DocumentVidR.SetSizeColToAllLevels(88);
                DocumentVidR.Binding = nameof(Form1.DocumentVid);
                NumberInOrderR += DocumentVidR;
                #endregion
                #region DocumentNumber (12)
                DataGridColumns DocumentNumberR = ((Attributes.Form_PropertyAttribute)typeof(Form1).GetProperty(nameof(Form1.DocumentNumber)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                DocumentNumberR.SetSizeColToAllLevels(103);
                DocumentNumberR.Binding = nameof(Form1.DocumentNumber);
                NumberInOrderR += DocumentNumberR;
                #endregion
                #region DocumentDate (13)
                DataGridColumns DocumentDateR = ((Attributes.Form_PropertyAttribute)typeof(Form1).GetProperty(nameof(Form1.DocumentDate)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                DocumentDateR.SetSizeColToAllLevels(88);
                DocumentDateR.Binding = nameof(Form1.DocumentDate);
                NumberInOrderR += DocumentDateR;
                #endregion
                #region ProviderOrRecieverOKPO (14)
                DataGridColumns ProviderOrRecieverOKPOR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.ProviderOrRecieverOKPO)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                ProviderOrRecieverOKPOR.SetSizeColToAllLevels(100);
                ProviderOrRecieverOKPOR.Binding = nameof(Form18.ProviderOrRecieverOKPO);
                NumberInOrderR += ProviderOrRecieverOKPOR;
                #endregion
                #region TransporterOKPO (15)
                DataGridColumns TransporterOKPOR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.TransporterOKPO)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                TransporterOKPOR.SetSizeColToAllLevels(163);
                TransporterOKPOR.Binding = nameof(Form18.TransporterOKPO);
                NumberInOrderR += TransporterOKPOR;
                #endregion
                #region StoragePlaceName (16)
                DataGridColumns StoragePlaceNameR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.StoragePlaceName)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                StoragePlaceNameR.SetSizeColToAllLevels(103);
                StoragePlaceNameR.Binding = nameof(Form18.StoragePlaceName);
                NumberInOrderR += StoragePlaceNameR;
                #endregion
                #region StoragePlaceCode (17)
                DataGridColumns StoragePlaceCodeR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.StoragePlaceCode)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                StoragePlaceCodeR.SetSizeColToAllLevels(88);
                StoragePlaceCodeR.Binding = nameof(Form18.StoragePlaceCode);
                NumberInOrderR += StoragePlaceCodeR;
                #endregion
                #region CodeRAO (18)
                DataGridColumns CodeRAOR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.CodeRAO)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                CodeRAOR.SetSizeColToAllLevels(88);
                CodeRAOR.Binding = nameof(Form18.CodeRAO);
                NumberInOrderR += CodeRAOR;
                #endregion
                #region StatusRAO (19)
                DataGridColumns StatusRAOR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.StatusRAO)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                StatusRAOR.SetSizeColToAllLevels(88);
                StatusRAOR.Binding = nameof(Form18.StatusRAO);
                NumberInOrderR += StatusRAOR;
                #endregion
                #region Volume20 (20)
                DataGridColumns Volume20R = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.Volume20)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                Volume20R.SetSizeColToAllLevels(88);
                Volume20R.Binding = nameof(Form18.Volume20);
                NumberInOrderR += Volume20R;
                #endregion
                #region Mass21 (21)
                DataGridColumns Mass21R = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.Mass21)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                Mass21R.SetSizeColToAllLevels(88);
                Mass21R.Binding = nameof(Form18.Mass21);
                NumberInOrderR += Mass21R;
                #endregion
                #region TritiumActivity (22)
                DataGridColumns TritiumActivityR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.TritiumActivity)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                TritiumActivityR.SetSizeColToAllLevels(163);
                TritiumActivityR.Binding = nameof(Form18.TritiumActivity);
                NumberInOrderR += TritiumActivityR;
                #endregion
                #region BetaGammaActivity (23)
                DataGridColumns BetaGammaActivityR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.BetaGammaActivity)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                BetaGammaActivityR.SetSizeColToAllLevels(160);
                BetaGammaActivityR.Binding = nameof(Form18.BetaGammaActivity);
                NumberInOrderR += BetaGammaActivityR;
                #endregion
                #region AlphaActivity (24)
                DataGridColumns AlphaActivityR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.AlphaActivity)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                AlphaActivityR.SetSizeColToAllLevels(163);
                AlphaActivityR.Binding = nameof(Form18.AlphaActivity);
                NumberInOrderR += AlphaActivityR;
                #endregion
                #region TransuraniumActivity (25)
                DataGridColumns TransuraniumActivityR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.TransuraniumActivity)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                TransuraniumActivityR.SetSizeColToAllLevels(200);
                TransuraniumActivityR.Binding = nameof(Form18.TransuraniumActivity);
                NumberInOrderR += TransuraniumActivityR;
                #endregion
                #region RefineOrSortRAOCode (26)
                DataGridColumns RefineOrSortRAOCodeR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.RefineOrSortRAOCode)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                RefineOrSortRAOCodeR.SetSizeColToAllLevels(120);
                RefineOrSortRAOCodeR.Binding = nameof(Form18.RefineOrSortRAOCode);
                NumberInOrderR += RefineOrSortRAOCodeR;
                #endregion
                #region Subsidy (27)
                DataGridColumns SubsidyR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.Subsidy)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                SubsidyR.SetSizeColToAllLevels(88);
                SubsidyR.Binding = nameof(Form18.Subsidy);
                NumberInOrderR += SubsidyR;
                #endregion
                #region FcpNumber (28)
                DataGridColumns FcpNumberR = ((Attributes.Form_PropertyAttribute)typeof(Form18).GetProperty(nameof(Form18.FcpNumber)).GetCustomAttributes(typeof(Attributes.Form_PropertyAttribute), true).FirstOrDefault()).GetDataColumnStructureD(NumberInOrderR);
                FcpNumberR.SetSizeColToAllLevels(163);
                FcpNumberR.Binding = nameof(Form18.FcpNumber);
                NumberInOrderR += FcpNumberR;
                #endregion
                _DataGridColumns = NumberInOrderR;
            }
            return _DataGridColumns;
        }
        #endregion
    }
}
