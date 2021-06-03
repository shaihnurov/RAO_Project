﻿using Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 1.8: Сведения о жидких кондиционированных РАО")]
    public class Form18 : Abstracts.Form1
    {
        public Form18() : base()
        {
            FormNum = "18";
            NumberOfFields = 37;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //IndividualNumberZHRO property
        [Attributes.Form_Property("Индивидуальный номер ЖРО")]
        public IDataAccess<string> IndividualNumberZHRO
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(IndividualNumberZHRO));
                }
                else
                {
                    
                }
            }
            set
            {
                IndividualNumberZHRO_Validation(value);
                
                {
                    _dataAccess.Set(nameof(IndividualNumberZHRO), value);
                }
                OnPropertyChanged(nameof(IndividualNumberZHRO));
            }
        }


        private void IndividualNumberZHRO_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
        }
        //IndividualNumberZHRO property

        //IndividualNumberZHROrecoded property
        public IDataAccess<string> IndividualNumberZHROrecoded
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(IndividualNumberZHROrecoded));
                }
                else
                {
                    
                }
            }
            set
            {
                IndividualNumberZHROrecoded_Validation(value);
                
                {
                    _dataAccess.Set(nameof(IndividualNumberZHROrecoded), value);
                }
                OnPropertyChanged(nameof(IndividualNumberZHROrecoded));
            }
        }


        private void IndividualNumberZHROrecoded_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
        }
        //IndividualNumberZHROrecoded property

        //PassportNumber property
        [Attributes.Form_Property("Номер паспорта")]
        public IDataAccess<string> PassportNumber
        {
            get
            {
                
                {
                    var tmp = _dataAccess.Get<string>(nameof(PassportNumber));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    
                }
            }
            set
            {
                PassportNumber_Validation(value);

                
                {
                    _dataAccess.Set(nameof(PassportNumber), value);
                }
                OnPropertyChanged(nameof(PassportNumber));
            }
        }


        private void PassportNumber_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                value.AddError( "Поле не заполнено");
                return;
            }
            if (value.Equals("прим."))
            {
                if ((PassportNumberNote == null) || (PassportNumberNote == ""))
                    value.AddError( "Поле не может быть пустым");
            }
        }
        //PassportNumber property

        //PassportNumberNote property
        public IDataAccess<string> PassportNumberNote
        {
            get
            {
                
                {
                    var tmp = _dataAccess.Get<string>(nameof(PassportNumberNote));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    
                }
            }
            set
            {
                PassportNumberNote_Validation(value);
                
                {
                    _dataAccess.Set(nameof(PassportNumberNote), value);
                }
                OnPropertyChanged(nameof(PassportNumberNote));
            }
        }


        private void PassportNumberNote_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
        }
        //PassportNumberNote property

        //PassportNumberRecoded property
        [Attributes.Form_Property("Номер упаковки")]
        public IDataAccess<string> PassportNumberRecoded
        {
            get
            {
                
                {
                    var tmp = _dataAccess.Get<string>(nameof(PassportNumberRecoded));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    
                }
            }
            set
            {
                PassportNumberRecoded_Validation(value);
                
                {
                    _dataAccess.Set(nameof(PassportNumberRecoded), value);
                }
                OnPropertyChanged(nameof(PassportNumberRecoded));
            }
        }
        //If change this change validation

        private void PassportNumberRecoded_Validation(IDataAccess<string> value)//Ready
        {
            value.ClearErrors();
        }
        //PassportNumberRecoded property

        //Volume6 property
        [Attributes.Form_Property("Объем, куб. м")]
        public IDataAccess<string> Volume6
        {
            get
            {
                if (GetErrors(nameof(Volume6)) == null)
                {
                    return _dataAccess.Get<string>(nameof(Volume6));
                }
                else
                {
                    
                }
            }
            set
            {
                Volume6_Validation(value);
                if (GetErrors(nameof(Volume6)) == null)
                {
                    _dataAccess.Set(nameof(Volume6), value);
                }
                OnPropertyChanged(nameof(Volume6));
            }
        }


        private void Volume6_Validation(IDataAccess<string> value)//TODO
        {
            ClearErrors(nameof(Volume6));
            if (string.IsNullOrEmpty(value)) return;
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                AddError(nameof(Volume6), "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(Volume6), "Число должно быть больше нуля");
            }
            catch (Exception)
            {
                AddError(nameof(Volume6), "Недопустимое значение");
                return;
            }
        }
        //Volume6 property

        //Mass7 Property
        [Attributes.Form_Property("Масса, т")]
        public IDataAccess<string> Mass7
        {
            get
            {
                if (GetErrors(nameof(Mass7)) == null)
                {
                    return _dataAccess.Get<string>(nameof(Mass7));
                }
                else
                {
                    
                }
            }
            set
            {
                Mass7_Validation(value);
                if (GetErrors(nameof(Mass7)) == null)
                {
                    _dataAccess.Set(nameof(Mass7), value);
                }
                OnPropertyChanged(nameof(Mass7));
            }
        }


        private void Mass7_Validation(IDataAccess<string> value)//TODO
        {
            ClearErrors(nameof(Mass7));
            if (string.IsNullOrEmpty(value)) return;
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                AddError(nameof(Mass7), "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(Mass7), "Число должно быть больше нуля");
            }
            catch (Exception)
            {
                AddError(nameof(Mass7), "Недопустимое значение");
                return;
            }
        }
        //Mass7 Property

        //SaltConcentration property
        [Attributes.Form_Property("Солесодержание, г/л")]
        public double? SaltConcentration
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(SaltConcentration));
                }
                else
                {
                    
                }
            }
            set
            {
                SaltConcentration_Validation(value);
                
                {
                    _dataAccess.Set(nameof(SaltConcentration), value);
                }
                OnPropertyChanged(nameof(SaltConcentration));
            }
        }


        private void SaltConcentration_Validation(double? value)
        {
            value.ClearErrors();
            if (value==null) return;
            if (value <= 0)
            {
                value.AddError( "Недопустимое значение");
                return;
            }
        }
        //SaltConcentration property

        //Radionuclids property
        [Attributes.Form_Property("Наименования радионуклидов")]
        public IDataAccess<string> Radionuclids
        {
            get
            {
                
                {
                    var tmp = _dataAccess.Get<string>(nameof(Radionuclids));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    
                }
            }
            set
            {
                Radionuclids_Validation(value);

                
                {
                    _dataAccess.Set(nameof(Radionuclids), value);
                }
                OnPropertyChanged(nameof(Radionuclids));
            }
        }
        //If change this change validation

        private void Radionuclids_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                value.AddError( "Поле не заполнено");
                return;
            }
            List<Tuple<string, string>> spr = new List<Tuple<string, string>>();//Here binds spravochnik
            foreach (var item in spr)
            {
                if (item.Item2.Equals(value))
                {
                    Radionuclids = item.Item2;
                    return;
                }
            }
        }
        //Radionuclids property

        //SpecificActivity property
        [Attributes.Form_Property("Удельная активность, Бк/г")]
        public IDataAccess<string> SpecificActivity
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(SpecificActivity));
                }
                else
                {
                    
                }
            }
            set
            {
                SpecificActivity_Validation(value);
                
                {
                    _dataAccess.Set(nameof(SpecificActivity), value);
                }
                OnPropertyChanged(nameof(SpecificActivity));
            }
        }


        private void SpecificActivity_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                value.AddError( "Поле не заполнено");
                return;
            }
            if (!(value.Value.Contains('e')||value.Value.Contains('E')))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    value.AddError( "Число должно быть больше нуля");
            }
            catch
            {
                value.AddError( "Недопустимое значение");
            }
        }
        //SpecificActivity property

        //ProviderOrRecieverOKPO property
        [Attributes.Form_Property("ОКПО поставщика/получателя")]
        public IDataAccess<string> ProviderOrRecieverOKPO
        {
            get
            {
                
                {
                    var tmp = _dataAccess.Get<string>(nameof(ProviderOrRecieverOKPO));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    
                }
            }
            set
            {
                ProviderOrRecieverOKPO_Validation(value);

                
                {
                    _dataAccess.Set(nameof(ProviderOrRecieverOKPO), value);
                }
                OnPropertyChanged(nameof(ProviderOrRecieverOKPO));
            }
        }


        private void ProviderOrRecieverOKPO_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null))
            {
                return;
            }
            if (value.Equals("Минобороны") || value.Equals("прим.")) return;
            if ((value.Length != 8) && (value.Length != 14))
                value.AddError( "Недопустимое значение");
            else
            {
                var mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
                if (!mask.IsMatch(value.Value))
                    value.AddError( "Недопустимое значение");
            }
        }
        //ProviderOrRecieverOKPO property

        //ProviderOrRecieverOKPONote property
        public IDataAccess<string> ProviderOrRecieverOKPONote
        {
            get
            {
                
                {
                    var tmp = _dataAccess.Get<string>(nameof(ProviderOrRecieverOKPONote));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    
                }
            }
            set
            {
                ProviderOrRecieverOKPONote_Validation(value);
                
                {
                    _dataAccess.Set(nameof(ProviderOrRecieverOKPONote), value);
                }
                OnPropertyChanged(nameof(ProviderOrRecieverOKPONote));
            }
        }


        private void ProviderOrRecieverOKPONote_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
        }
        //ProviderOrRecieverOKPONote property

        //TransporterOKPO property
        [Attributes.Form_Property("ОКПО перевозчика")]
        public IDataAccess<string> TransporterOKPO
        {
            get
            {
                
                {
                    var tmp = _dataAccess.Get<string>(nameof(TransporterOKPO));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    
                }
            }
            set
            {
                TransporterOKPO_Validation(value);

                
                {
                    _dataAccess.Set(nameof(TransporterOKPO), value);
                }
                OnPropertyChanged(nameof(TransporterOKPO));
            }
        }


        private void TransporterOKPO_Validation(IDataAccess<string> value)//Done
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            if (value.Equals("-")) return;
            if (value.Equals("Минобороны")) return;
            if (value.Equals("прим."))
            {
                if ((TransporterOKPONote == null) || TransporterOKPONote.Equals(""))
                    value.AddError( "Заполните примечание");
                return;
            }
            if ((value.Length != 8) && (value.Length != 14))
                value.AddError( "Недопустимое значение");
            else
            {
                var mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
                if (!mask.IsMatch(value.Value))
                    value.AddError( "Недопустимое значение");
            }
        }
        //TransporterOKPO property

        //TransporterOKPONote property
        public IDataAccess<string> TransporterOKPONote
        {
            get
            {
                
                {
                    var tmp = _dataAccess.Get<string>(nameof(TransporterOKPONote));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    
                }
            }
            set
            {
                TransporterOKPONote_Validation(value);
                
                {
                    _dataAccess.Set(nameof(TransporterOKPONote), value);
                }
                OnPropertyChanged(nameof(TransporterOKPONote));
            }
        }


        private void TransporterOKPONote_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
        }
        //TransporterOKPONote property

        //StoragePlaceName property
        [Attributes.Form_Property("Наименование ПХ")]
        public IDataAccess<string> StoragePlaceName
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(StoragePlaceName));
                }
                else
                {
                    
                }
            }
            set
            {
                StoragePlaceName_Validation(value);
                
                {
                    _dataAccess.Set(nameof(StoragePlaceName), value);
                }
                OnPropertyChanged(nameof(StoragePlaceName));
            }
        }
        //If change this change validation

        private void StoragePlaceName_Validation(IDataAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value)) return;
            var spr = new List<string>();
            if (!spr.Contains(value))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
        }
        //StoragePlaceName property

        //StoragePlaceNameNote property
        public IDataAccess<string> StoragePlaceNameNote
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(StoragePlaceNameNote));
                }
                else
                {
                    
                }
            }
            set
            {
                StoragePlaceNameNote_Validation(value);
                
                {
                    _dataAccess.Set(nameof(StoragePlaceNameNote), value);
                }
                OnPropertyChanged(nameof(StoragePlaceNameNote));
            }
        }
        //If change this change validation

        private void StoragePlaceNameNote_Validation(IDataAccess<string> value)//Ready
        {
            value.ClearErrors();
        }
        //StoragePlaceNameNote property

        //StoragePlaceCode property
        [Attributes.Form_Property("Код ПХ")]
        public IDataAccess<string> StoragePlaceCode //8 cyfer code or - .
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(StoragePlaceCode));
                }
                else
                {
                    
                }
            }
            set
            {
                StoragePlaceCode_Validation(value);
                
                {
                    _dataAccess.Set(nameof(StoragePlaceCode), value);
                }
                OnPropertyChanged(nameof(StoragePlaceCode));
            }
        }
        //if change this change validation

        private void StoragePlaceCode_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value)|| value.Equals("-")) return;
            var lst = new List<string>();//HERE binds spr
            if(!lst.Contains(value))
            value.AddError( "Недопустимое значение");
        }
        //StoragePlaceCode property

        //CodeRAO property
        [Attributes.Form_Property("Код РАО")]
        public IDataAccess<string> CodeRAO
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(CodeRAO));
                }
                else
                {
                    
                }
            }
            set
            {
                CodeRAO_Validation(value);
                
                {
                    _dataAccess.Set(nameof(CodeRAO), value);
                }
                OnPropertyChanged(nameof(CodeRAO));
            }
        }


        private void CodeRAO_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                value.AddError( "Поле не заполнено");
                return;
            }
            var a = new Regex("^[0-9]{11}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
        }
        //CodeRAO property

        //StatusRAO property
        [Attributes.Form_Property("Статус РАО")]
        public IDataAccess<string> StatusRAO  //1 cyfer or OKPO.
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(StatusRAO));
                }
                else
                {
                    
                }
            }
            set
            {
                StatusRAO_Validation(value);
                
                {
                    _dataAccess.Set(nameof(StatusRAO), value);
                }
                OnPropertyChanged(nameof(StatusRAO));
            }
        }


        private void StatusRAO_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (value.Length == 1)
            {
                int tmp;
                try
                {
                    tmp = int.Parse(value.Value);
                    if ((tmp < 1) || ((tmp > 4) && (tmp != 6) && (tmp != 9)))
                    {
                        value.AddError( "Недопустимое значение");
                    }
                }
                catch (Exception)
                {
                    value.AddError( "Недопустимое значение");
                }
                return;
            }
            if ((value.Length != 8) && (value.Length != 14))
                value.AddError( "Недопустимое значение");
            else
            {
                var mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
                if (!mask.IsMatch(value.Value))
                    value.AddError( "Недопустимое значение");
            }
        }
        //StatusRAO property

        //Volume20 property
        [Attributes.Form_Property("Объем, куб. м")]
        public IDataAccess<string> Volume20
        {
            get
            {
                if (GetErrors(nameof(Volume20)) == null)
                {
                    return _dataAccess.Get<string>(nameof(Volume20));
                }
                else
                {
                    
                }
            }
            set
            {
                Volume20_Validation(value);
                if (GetErrors(nameof(Volume20)) == null)
                {
                    _dataAccess.Set(nameof(Volume20), value);
                }
                OnPropertyChanged(nameof(Volume20));
            }
        }


        private void Volume20_Validation(IDataAccess<string> value)//TODO
        {
            ClearErrors(nameof(Volume20));
            if ((value.Value == null) || value.Equals(""))
            {
                AddError(nameof(Volume20), "Поле не заполнено");
                return;
            }
            if (!((value.Value.Contains('e') || value.Value.Contains('E'))))
            {
                AddError(nameof(Volume20), "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(Volume20), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(Volume20), "Недопустимое значение");
            }
        }
        //Volume20 property

        //Mass21 Property
        [Attributes.Form_Property("Масса, т")]
        public IDataAccess<string> Mass21
        {
            get
            {
                if (GetErrors(nameof(Mass21)) == null)
                {
                    return _dataAccess.Get<string>(nameof(Mass21));
                }
                else
                {
                    
                }
            }
            set
            {
                Mass21_Validation(value);
                if (GetErrors(nameof(Mass21)) == null)
                {
                    _dataAccess.Set(nameof(Mass21), value);
                }
                OnPropertyChanged(nameof(Mass21));
            }
        }


        private void Mass21_Validation(IDataAccess<string> value)//TODO
        {
            ClearErrors(nameof(Mass21));
            if ((value.Value == null) || value.Equals(""))
            {
                AddError(nameof(Mass21), "Поле не заполнено");
                return;
            }
            if (!((value.Value.Contains('e') || value.Value.Contains('E'))))
            {
                AddError(nameof(Mass21), "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(Mass21), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(Mass21), "Недопустимое значение");
            }
        }
        //Mass21 Property

        //TritiumActivity property
        [Attributes.Form_Property("Активность трития, Бк")]
        public IDataAccess<string> TritiumActivity
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(TritiumActivity));
                }
                else
                {
                    
                }
            }
            set
            {
                TritiumActivity_Validation(value);
                
                {
                    _dataAccess.Set(nameof(TritiumActivity), value);
                }
                OnPropertyChanged(nameof(TritiumActivity));
            }
        }


        private void TritiumActivity_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                value.AddError( "Поле не заполнено");
                return;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    value.AddError( "Число должно быть больше нуля");
            }
            catch
            {
                value.AddError( "Недопустимое значение");
            }
        }
        //TritiumActivity property

        //BetaGammaActivity property
        [Attributes.Form_Property("Активность бета-, гамма-излучающих, кроме трития, Бк")]
        public IDataAccess<string> BetaGammaActivity
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(BetaGammaActivity));
                }
                else
                {
                    
                }
            }
            set
            {
                BetaGammaActivity_Validation(value);
                
                {
                    _dataAccess.Set(nameof(BetaGammaActivity), value);
                }
                OnPropertyChanged(nameof(BetaGammaActivity));
            }
        }


        private void BetaGammaActivity_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                value.AddError( "Поле не заполнено");
                return;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    value.AddError( "Число должно быть больше нуля");
            }
            catch
            {
                value.AddError( "Недопустимое значение");
            }
        }
        //BetaGammaActivity property

        //AlphaActivity property
        [Attributes.Form_Property("Активность альфа-излучающих, кроме трансурановых, Бк")]
        public IDataAccess<string> AlphaActivity
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(AlphaActivity));
                }
                else
                {
                    
                }
            }
            set
            {
                AlphaActivity_Validation(value);
                
                {
                    _dataAccess.Set(nameof(AlphaActivity), value);
                }
                OnPropertyChanged(nameof(AlphaActivity));
            }
        }


        private void AlphaActivity_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                value.AddError( "Поле не заполнено");
                return;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    value.AddError( "Число должно быть больше нуля");
            }
            catch
            {
                value.AddError( "Недопустимое значение");
            }
        }
        //AlphaActivity property

        //TransuraniumActivity property
        [Attributes.Form_Property("Активность трансурановых, Бк")]
        public IDataAccess<string> TransuraniumActivity
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(TransuraniumActivity));
                }
                else
                {
                    
                }
            }
            set
            {
                TransuraniumActivity_Validation(value);
                
                {
                    _dataAccess.Set(nameof(TransuraniumActivity), value);
                }
                OnPropertyChanged(nameof(TransuraniumActivity));
            }
        }


        private void TransuraniumActivity_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                value.AddError( "Поле не заполнено");
                return;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    value.AddError( "Число должно быть больше нуля");
            }
            catch
            {
                value.AddError( "Недопустимое значение");
            }
        }
        //TransuraniumActivity property

        //RefineOrSortRAOCode property
        [Attributes.Form_Property("Код переработки/сортировки РАО")]
        public IDataAccess<string> RefineOrSortRAOCode //2 cyfer code or empty.
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(RefineOrSortRAOCode));
                }
                else
                {
                    
                }
            }
            set
            {
                RefineOrSortRAOCode_Validation(value);
                
                {
                    _dataAccess.Set(nameof(RefineOrSortRAOCode), value);
                }
                OnPropertyChanged(nameof(RefineOrSortRAOCode));
            }
        }
        //If change this change validation

        private void RefineOrSortRAOCode_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (OperationCode == 55)
            {
                if (string.IsNullOrEmpty(value))
                {
                    value.AddError( "Поле не заполнено");
                    return;
                }
                var a = new Regex("^[0-9][0-9]$");
                if (!a.IsMatch(value.Value))
                {
                    value.AddError( "Недопустимое значение");
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value.AddError( "Недопустимое значение");
                    return;
                }
            }
        }
        //RefineOrSortRAOCode property

        //Subsidy property
        [Attributes.Form_Property("Субсидия, %")]
        public IDataAccess<string> Subsidy // 0<number<=100 or empty.
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Subsidy));
                }
                else
                {
                    
                }
            }
            set
            {
                Subsidy_Validation(value);
                
                {
                    _dataAccess.Set(nameof(Subsidy), value);
                }
                OnPropertyChanged(nameof(Subsidy));
            }
        }


        private void Subsidy_Validation(IDataAccess<string> value)//Ready
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals("")) return;
            try
            {
                int tmp = Int32.Parse(value.Value);
                if (!((tmp > 0) && (tmp <= 100)))
                    value.AddError( "Недопустимое значение");
            }
            catch
            {
                value.AddError( "Недопустимое значение");
            }
        }
        //Subsidy property

        //FcpNumber property
        [Attributes.Form_Property("Номер мероприятия ФЦП")]
        public IDataAccess<string> FcpNumber
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(FcpNumber));
                }
                else
                {
                    
                }
            }
            set
            {
                FcpNumber_Validation(value);
                
                {
                    _dataAccess.Set(nameof(FcpNumber), value);
                }
                OnPropertyChanged(nameof(FcpNumber));
            }
        }


        private void FcpNumber_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
        }
        //FcpNumber property

        protected override void OperationCode_Validation(IDataAccess<short?> value)//OK
        {
            value.ClearErrors();
            if (value.Value == null) return;
            List<short> spr = new List<short>();    //HERE BINDS SPRAVOCHNIK
            if (!spr.Contains((short)value))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            bool a0 = value.Value == 1;
            bool a2 = value.Value == 18;
            bool a3 = value.Value == 55;
            bool a4 = value.Value == 63;
            bool a5 = value.Value == 64;
            bool a6 = value.Value == 68;
            bool a7 = value.Value == 97;
            bool a8 = value.Value == 98;
            bool a9 = value.Value == 99;
            bool a10 = (value >= 21) && (value <= 29);
            bool a11 = (value >= 31) && (value <= 39);
            if (!(a0 || a2 || a3 || a4 || a5 || a6 || a7 || a8 || a9 || a10 || a11))
                value.AddError( "Код операции не может быть использован в форме 1.7");
            return;
        }

        protected override void OperationDate_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var a = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            try { DateTimeOffset.Parse(value.Value); }
            catch (Exception)
            {
                value.AddError( "Недопустимое значение");
                return;
            }
        }

        protected override void DocumentNumber_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
        }

        protected override void DocumentVid_Validation(byte? value)
        {
            value.ClearErrors();
            List<Tuple<byte?, string>> spr = new List<Tuple<byte?, string>>
            {
                new Tuple<byte?, string>(0,""),
                new Tuple<byte?, string>(1,""),
                new Tuple<byte?, string>(2,""),
                new Tuple<byte?, string>(3,""),
                new Tuple<byte?, string>(4,""),
                new Tuple<byte?, string>(5,""),
                new Tuple<byte?, string>(6,""),
                new Tuple<byte?, string>(7,""),
                new Tuple<byte?, string>(8,""),
                new Tuple<byte?, string>(9,""),
                new Tuple<byte?, string>(10,""),
                new Tuple<byte?, string>(11,""),
                new Tuple<byte?, string>(12,""),
                new Tuple<byte?, string>(13,""),
                new Tuple<byte?, string>(14,""),
                new Tuple<byte?, string>(15,""),
                new Tuple<byte?, string>(19,""),
                new Tuple<byte?, string>(null,"")
            };   //HERE BINDS SPRAVOCHNICK
            foreach (var item in spr)
            {
                if (item.Item1 == value) return;
            }
            value.AddError( "Недопустимое значение");
        }

        protected override void DocumentDate_Validation(IDataAccess<string> value)
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Equals(""))
            {
                return;
            }
            var a = new Regex("^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            try { DateTimeOffset.Parse(value.Value); }
            catch (Exception)
            {
                value.AddError( "Недопустимое значение");
                return;
            }
            bool b = (OperationCode == 68);
            bool c = (OperationCode == 52) || (OperationCode == 55);
            bool d = (OperationCode == 18) || (OperationCode == 51);
            if (b || c || d)
                if (!value.Equals(OperationDate))
                    value.AddError( "Заполните примечание");
        }
    }
}
