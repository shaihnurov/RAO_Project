﻿using Models.DataAccess;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.1: Сортировка, переработка и кондиционирование РАО на установках")]
    public class Form21 : Abstracts.Form2
    {
        public Form21() : base()
        {
            //FormNum.Value = "21";
            //NumberOfFields.Value = 24;
            Init();
            Validate_all();
        }

        private void Init()
        {
            DataAccess.Init<string>(nameof(MachinePower), MachinePower_Validation, null);
            MachinePower.PropertyChanged += InPropertyChanged;
            DataAccess.Init<byte?>(nameof(MachineCode), MachineCode_Validation, null);
            MachineCode.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(RefineMachineName), RefineMachineName_Validation, null);
            RefineMachineName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(NumberOfHoursPerYear), NumberOfHoursPerYear_Validation, null);
            NumberOfHoursPerYear.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(CodeRAOIn), CodeRAOIn_Validation, null);
            CodeRAOIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(StatusRAOIn), StatusRAOIn_Validation, null);
            StatusRAOIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(VolumeIn), VolumeIn_Validation, null);
            VolumeIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(MassIn), MassIn_Validation, null);
            MassIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(QuantityIn), QuantityIn_Validation, null);
            QuantityIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(TritiumActivityIn), TritiumActivityIn_Validation, null);
            TritiumActivityIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(TritiumActivityOut), TritiumActivityOut_Validation, null);
            TritiumActivityOut.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(BetaGammaActivityIn), BetaGammaActivityIn_Validation, null);
            BetaGammaActivityIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(BetaGammaActivityOut), BetaGammaActivityOut_Validation, null);
            BetaGammaActivityOut.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(TransuraniumActivityIn), TransuraniumActivityIn_Validation, null);
            TransuraniumActivityIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(TransuraniumActivityOut), TransuraniumActivityOut_Validation, null);
            TransuraniumActivityOut.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(AlphaActivityIn), AlphaActivityIn_Validation, null);
            AlphaActivityIn.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(AlphaActivityOut), AlphaActivityOut_Validation, null);
            AlphaActivityOut.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(VolumeOut), VolumeOut_Validation, null);
            VolumeOut.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(MassOut), MassOut_Validation, null);
            MassOut.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(QuantityOZIIIout), QuantityOZIIIout_Validation, null);
            QuantityOZIIIout.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(CodeRAOout), CodeRAOout_Validation, null);
            CodeRAOout.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(StatusRAOout), StatusRAOout_Validation, null);
            StatusRAOout.PropertyChanged += InPropertyChanged;
        }

        private void Validate_all()
        {
            MachinePower_Validation(MachinePower);
            MachineCode_Validation(MachineCode);
            RefineMachineName_Validation(RefineMachineName);
            NumberOfHoursPerYear_Validation(NumberOfHoursPerYear);
            CodeRAOIn_Validation(CodeRAOIn);
            StatusRAOIn_Validation(StatusRAOIn);
            VolumeIn_Validation(VolumeIn);
            MassIn_Validation(MassIn);
            QuantityIn_Validation(QuantityIn);
            TritiumActivityIn_Validation(TritiumActivityIn);
            TritiumActivityOut_Validation(TritiumActivityOut);
            BetaGammaActivityIn_Validation(BetaGammaActivityIn);
            BetaGammaActivityOut_Validation(BetaGammaActivityOut);
            TransuraniumActivityIn_Validation(TransuraniumActivityIn);
            TransuraniumActivityOut_Validation(TransuraniumActivityOut);
            AlphaActivityIn_Validation(AlphaActivityIn);
            AlphaActivityOut_Validation(AlphaActivityOut);
            VolumeOut_Validation(VolumeOut);
            MassOut_Validation(MassOut);
            QuantityOZIIIout_Validation(QuantityOZIIIout);
            CodeRAOout_Validation(CodeRAOout);
            StatusRAOout_Validation(StatusRAOout);
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //RefineMachineName property
        [Attributes.Form_Property("Наименование установки переработки")]public int? RefineMachineNameId { get; set; }
        public virtual RamAccess<string> RefineMachineName
        {
            get => DataAccess.Get<string>(nameof(RefineMachineName));
            set
            {
                DataAccess.Set(nameof(RefineMachineName), value);
                OnPropertyChanged(nameof(RefineMachineName));
            }
        }

        private bool RefineMachineName_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //RefineMachineName property

        //MachineCode property
        [Attributes.Form_Property("Код установки переработки")]public int? MachineCodeId { get; set; }
        public virtual RamAccess<byte?> MachineCode
        {
            get => DataAccess.Get<byte?>(nameof(MachineCode));
            set
            {
                DataAccess.Set(nameof(MachineCode), value);
                OnPropertyChanged(nameof(MachineCode));
            }
        }

        private bool MachineCode_Validation(RamAccess<byte?> value)//TODO
        {
            value.ClearErrors();
            bool a = (value.Value >= 11) && (value.Value <= 17);
            bool b = (value.Value >= 21) && (value.Value <= 24);
            bool c = (value.Value >= 31) && (value.Value <= 32);
            bool d = (value.Value >= 41) && (value.Value <= 43);
            bool e = (value.Value >= 51) && (value.Value <= 56);
            bool f = (value.Value >= 61) && (value.Value <= 63);
            bool g = (value.Value >= 71) && (value.Value <= 73);
            bool h = (value.Value == 19) || (value.Value == 29) || (value.Value == 39) || (value.Value == 49) || (value.Value == 99) || (value.Value == 79);
            if (!(a || b || c || d || e || f || g || h))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //MachineCode property

        //MachinePower property
        [Attributes.Form_Property("Мощность, куб. м/год")]public int? MachinePowerId { get; set; }
        public virtual RamAccess<string> MachinePower
        {
            get => DataAccess.Get<string>(nameof(MachinePower));
            set
            {
                DataAccess.Set(nameof(MachinePower), value);
                OnPropertyChanged(nameof(MachinePower));
            }
        }

        private bool MachinePower_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                return true;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //MachinePower property

        //NumberOfHoursPerYear property
        [Attributes.Form_Property("Количество часов работы за год")]public int? NumberOfHoursPerYearId { get; set; }
        public virtual RamAccess<string> NumberOfHoursPerYear
        {
            get => DataAccess.Get<string>(nameof(NumberOfHoursPerYear));
            set
            {
                DataAccess.Set(nameof(NumberOfHoursPerYear), value);
                OnPropertyChanged(nameof(NumberOfHoursPerYear));
            }
        }

        private bool NumberOfHoursPerYear_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return false;
            }
            if (value.Value.Equals("прим.") || value.Value.Equals("0"))
            {

            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //NumberOfHoursPerYear property

        //CodeRAOIn property
        [Attributes.Form_Property("Код РАО")]public int? CodeRAOInId { get; set; }
        public virtual RamAccess<string> CodeRAOIn
        {
            get => DataAccess.Get<string>(nameof(CodeRAOIn));
            set
            {
                DataAccess.Set(nameof(CodeRAOIn), value);
                OnPropertyChanged(nameof(CodeRAOIn));
            }
        }

        private bool CodeRAOIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return false;
            }
            Regex a = new Regex("^[0-9]{11}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //CodeRAOIn property

        //StatusRAOIn property
        [Attributes.Form_Property("Статус РАО")]public int? StatusRAOInId { get; set; }
        public virtual RamAccess<string> StatusRAOIn  //1 cyfer or OKPO.
        {
            get => DataAccess.Get<string>(nameof(StatusRAOIn));
            set
            {
                DataAccess.Set(nameof(StatusRAOIn), value);
                OnPropertyChanged(nameof(StatusRAOIn));
            }
        }

        private bool StatusRAOIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (value.Value.Length == 1)
            {
                int tmp;
                try
                {
                    tmp = int.Parse(value.Value);
                    if ((tmp < 1) || ((tmp > 4) && (tmp != 6) && (tmp != 9)))
                    {
                        value.AddError("Недопустимое значение");
                        return false;
                    }
                }
                catch (Exception)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
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
        //StatusRAOIn property

        //VolumeIn property
        [Attributes.Form_Property("Объем, куб. м")]public int? VolumeInId { get; set; }
        public virtual RamAccess<string> VolumeIn//SUMMARIZABLE
        {
            get => DataAccess.Get<string>(nameof(VolumeIn));
            set
            {
                DataAccess.Set(nameof(VolumeIn), value);
                OnPropertyChanged(nameof(VolumeIn));
            }
        }

        private bool VolumeIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Value.Equals(""))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //VolumeIn property

        //MassIn Property
        [Attributes.Form_Property("Масса, т")]public int? MassInId { get; set; }
        public virtual RamAccess<string> MassIn//SUMMARIZABLE
        {
            get => DataAccess.Get<string>(nameof(MassIn));
            set
            {
                DataAccess.Set(nameof(MassIn), value);
                OnPropertyChanged(nameof(MassIn));
            }
        }

        private bool MassIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Value.Equals(""))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //MassIn Property

        //QuantityIn property
        [Attributes.Form_Property("Количество ОЗИИИ, шт.")]public int? QuantityInId { get; set; }
        public virtual RamAccess<string> QuantityIn//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(QuantityIn));//OK
                }

                {

                }
            }
            set
            {



                {
                    DataAccess.Set(nameof(QuantityIn), value);
                }
                OnPropertyChanged(nameof(QuantityIn));
            }
        }
        // positive int.
        private bool QuantityIn_Validation(RamAccess<string> value1)//Ready
        {
            value1.ClearErrors();
            if (value1.Equals("прим."))
            {
                return true;
            }
            string tmp = value1.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            int value;
            try
            {
                value = int.Parse(tmp);
                if (value <= 0)
                {
                    value1.AddError("Число должно быть больше нуля"); return false;
                }
            }
            catch
            {
                value1.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //QuantityIn property

        //TritiumActivityIn property
        [Attributes.Form_Property("Активность трития, Бк")]public int? TritiumActivityInId { get; set; }
        public virtual RamAccess<string> TritiumActivityIn//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(TritiumActivityIn));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(TritiumActivityIn), value);
                }
                OnPropertyChanged(nameof(TritiumActivityIn));
            }
        }

        private bool TritiumActivityIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //TritiumActivityIn property

        //BetaGammaActivityIn property
        [Attributes.Form_Property("Активность бета-, гамма-излучающих, кроме трития, Бк")]public int? BetaGammaActivityInId { get; set; }
        public virtual RamAccess<string> BetaGammaActivityIn//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(BetaGammaActivityIn));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(BetaGammaActivityIn), value);
                }
                OnPropertyChanged(nameof(BetaGammaActivityIn));
            }
        }

        private bool BetaGammaActivityIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //BetaGammaActivity property

        //AlphaActivityIn property
        [Attributes.Form_Property("Активность альфа-излучающих, кроме трансурановых, Бк")]public int? AlphaActivityInId { get; set; }
        public virtual RamAccess<string> AlphaActivityIn//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(AlphaActivityIn));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(AlphaActivityIn), value);
                }
                OnPropertyChanged(nameof(AlphaActivityIn));
            }
        }

        private bool AlphaActivityIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //AlphaActivityIn property

        //TransuraniumActivityIn property
        [Attributes.Form_Property("Активность трансурановых, Бк")]public int? TransuraniumActivityInId { get; set; }
        public virtual RamAccess<string> TransuraniumActivityIn//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(TransuraniumActivityIn));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(TransuraniumActivityIn), value);
                }
                OnPropertyChanged(nameof(TransuraniumActivityIn));
            }
        }

        private bool TransuraniumActivityIn_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //TransuraniumActivityIn property

        //CodeRAOout property
        [Attributes.Form_Property("Код РАО")]public int? CodeRAOoutId { get; set; }
        public virtual RamAccess<string> CodeRAOout
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(CodeRAOout));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(CodeRAOout), value);
                }
                OnPropertyChanged(nameof(CodeRAOout));
            }
        }

        private bool CodeRAOout_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return false;
            }
            Regex a = new Regex("^[0-9]{11}$");
            if (!a.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //CodeRAOout property

        //StatusRAOout property
        [Attributes.Form_Property("Статус РАО")]public int? StatusRAOoutId { get; set; }
        public virtual RamAccess<string> StatusRAOout  //1 cyfer or OKPO.
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(StatusRAOout));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(StatusRAOout), value);
                }
                OnPropertyChanged(nameof(StatusRAOout));
            }
        }

        private bool StatusRAOout_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (value.Value.Length == 1)
            {
                int tmp;
                try
                {
                    tmp = int.Parse(value.Value);
                    if ((tmp < 1) || ((tmp > 4) && (tmp != 6) && (tmp != 9)))
                    {
                        value.AddError("Недопустимое значение");
                        return false;
                    }
                }
                catch (Exception)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
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
        //StatusRAOout property

        //VolumeOut property
        [Attributes.Form_Property("Объем, куб. м")]public int? VolumeOutId { get; set; }
        public virtual RamAccess<string> VolumeOut//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(VolumeOut));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(VolumeOut), value);
                }
                OnPropertyChanged(nameof(VolumeOut));
            }
        }

        private bool VolumeOut_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Value.Equals(""))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //VolumeOut property

        //MassOut Property
        [Attributes.Form_Property("Масса, т")]public int? MassOutId { get; set; }
        public virtual RamAccess<string> MassOut//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(MassOut));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(MassOut), value);
                }
                OnPropertyChanged(nameof(MassOut));
            }
        }

        private bool MassOut_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Value.Equals(""))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
        //MassOut Property

        //QuantityOZIIIout property
        [Attributes.Form_Property("Количество ОЗИИИ, шт.")]public int? QuantityOZIIIoutId { get; set; }
        public virtual RamAccess<string> QuantityOZIIIout//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(QuantityOZIIIout));//OK

                }

                {

                }
            }
            set
            {



                {
                    DataAccess.Set(nameof(QuantityOZIIIout), value);
                }
                OnPropertyChanged(nameof(QuantityOZIIIout));
            }
        }
        // positive int.
        private bool QuantityOZIIIout_Validation(RamAccess<string> value1)//Ready
        {
            value1.ClearErrors();
            if (value1.Equals("прим.") || string.IsNullOrEmpty(value1.Value))
            {
                return true;
            }
            string tmp = value1.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            int value;
            try
            {
                value = int.Parse(tmp);
                if (value <= 0)
                {
                    value1.AddError("Число должно быть больше нуля"); return false;
                }
            }
            catch
            {
                value1.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //QuantityOZIIIout property

        //TritiumActivityOut property
        [Attributes.Form_Property("Активность трития, Бк")]public int? TritiumActivityOutId { get; set; }
        public virtual RamAccess<string> TritiumActivityOut//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(TritiumActivityOut));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(TritiumActivityOut), value);
                }
                OnPropertyChanged(nameof(TritiumActivityOut));
            }
        }

        private bool TritiumActivityOut_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //TritiumActivityOut property

        //BetaGammaActivityOut property
        [Attributes.Form_Property("Активность бета-, гамма-излучающих, кроме трития, Бк")]public int? BetaGammaActivityOutId { get; set; }
        public virtual RamAccess<string> BetaGammaActivityOut//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(BetaGammaActivityOut));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(BetaGammaActivityOut), value);
                }
                OnPropertyChanged(nameof(BetaGammaActivityOut));
            }
        }

        private bool BetaGammaActivityOut_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //BetaGammaActivityOut property

        //AlphaActivityOut property
        [Attributes.Form_Property("Активность альфа-излучающих, кроме трансурановых, Бк")]public int? AlphaActivityOutId { get; set; }
        public virtual RamAccess<string> AlphaActivityOut//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(AlphaActivityOut));
                }

                {

                }
            }
            set
            {
                {
                    DataAccess.Set(nameof(AlphaActivityOut), value);
                }
                OnPropertyChanged(nameof(AlphaActivityOut));
            }
        }

        private bool AlphaActivityOut_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //AlphaActivityOut property

        //TransuraniumActivityOut property
        [Attributes.Form_Property("Активность трансурановых, Бк")]public int? TransuraniumActivityOutId { get; set; }
        public virtual RamAccess<string> TransuraniumActivityOut//SUMMARIZABLE
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(TransuraniumActivityOut));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(TransuraniumActivityOut), value);
                }
                OnPropertyChanged(nameof(TransuraniumActivityOut));
            }
        }

        private bool TransuraniumActivityOut_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e') || value.Value.Contains('E')))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            string tmp = value.Value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
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
        //TransuraniumActivityOut property
    }
}
