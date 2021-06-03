﻿using Models.DataAccess;
using System;
using System.Globalization;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.1: Сортировка, переработка и кондиционирование РАО на установках")]
    public class Form21 : Abstracts.Form2
    {
        public Form21() : base()
        {
            FormNum = "21";
            NumberOfFields = 24;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //RefineMachineName property
        [Attributes.Form_Property("Наименование установки переработки")]
        public string RefineMachineName
        {
            get
            {
                if (GetErrors(nameof(RefineMachineName)) == null)
                {
                    return (string)_dataAccess.Get(nameof(RefineMachineName));
                }
                else
                {
                    return _RefineMachineName_Not_Valid;
                }
            }
            set
            {
                _RefineMachineName_Not_Valid = value;
                if (GetErrors(nameof(RefineMachineName)) == null)
                {
                    _dataAccess.Set(nameof(RefineMachineName), value);
                }
                OnPropertyChanged(nameof(RefineMachineName));
            }
        }

        private string _RefineMachineName_Not_Valid = "";
        private void RefineMachineName_Validation()
        {
            ClearErrors(nameof(RefineMachineName));
        }
        //RefineMachineName property

        //MachineCode property
        [Attributes.Form_Property("Код установки переработки")]
        public short MachineCode
        {
            get
            {
                if (GetErrors(nameof(MachineCode)) == null)
                {
                    return (short)_dataAccess.Get(nameof(MachineCode));
                }
                else
                {
                    return _MachineCode_Not_Valid;
                }
            }
            set
            {
                _MachineCode_Not_Valid = value;
                if (GetErrors(nameof(MachineCode)) == null)
                {
                    _dataAccess.Set(nameof(MachineCode), value);
                }
                OnPropertyChanged(nameof(MachineCode));
            }
        }

        private short _MachineCode_Not_Valid = 0;
        private void MachineCode_Validation(short value)//TODO
        {
            ClearErrors(nameof(MachineCode));
            bool a = (value >= 11) && (value <= 17);
            bool b = (value >= 21) && (value <= 24);
            bool c = (value >= 31) && (value <= 32);
            bool d = (value >= 41) && (value <= 43);
            bool e = (value >= 51) && (value <= 56);
            bool f = (value >= 61) && (value <= 63);
            bool g = (value >= 71) && (value <= 73);
            bool h = (value == 19) || (value == 29) || (value == 39) || (value == 49) || (value == 99) || (value == 79);
            if (!(a || b || c || d || e || f || g || h))
                AddError(nameof(MachineCode), "Недопустимое значение");
        }
        //MachineCode property

        //MachinePower property
        [Attributes.Form_Property("Мощность, куб. м/год")]
        public string MachinePower
        {
            get
            {
                if (GetErrors(nameof(MachinePower)) == null)
                {
                    return (string)_dataAccess.Get(nameof(MachinePower));
                }
                else
                {
                    return _MachinePower_Not_Valid;
                }
            }
            set
            {
                _MachinePower_Not_Valid = value;
                if (GetErrors(nameof(MachinePower)) == null)
                {
                    _dataAccess.Set(nameof(MachinePower), value);
                }
                OnPropertyChanged(nameof(MachinePower));
            }
        }

        private string _MachinePower_Not_Valid = "";
        private void MachinePower_Validation(string value)//TODO
        {
            ClearErrors(nameof(MachinePower));
        }
        //MachinePower property

        //NumberOfHoursPerYear property
        [Attributes.Form_Property("Количество часов работы за год")]
        public int NumberOfHoursPerYear
        {
            get
            {
                if (GetErrors(nameof(NumberOfHoursPerYear)) == null)
                {
                    return (int)_dataAccess.Get(nameof(NumberOfHoursPerYear));
                }
                else
                {
                    return _NumberOfHoursPerYear_Not_Valid;
                }
            }
            set
            {
                _NumberOfHoursPerYear_Not_Valid = value;
                if (GetErrors(nameof(NumberOfHoursPerYear)) == null)
                {
                    _dataAccess.Set(nameof(NumberOfHoursPerYear), value);
                }
                OnPropertyChanged(nameof(NumberOfHoursPerYear));
            }
        }

        private int _NumberOfHoursPerYear_Not_Valid = -1;
        private void NumberOfHoursPerYear_Validation(int value)//TODO
        {
            ClearErrors(nameof(NumberOfHoursPerYear));
        }
        //NumberOfHoursPerYear property

        //CodeRAOIn property
        [Attributes.Form_Property("Код РАО")]
        public string CodeRAOIn
        {
            get
            {
                if (GetErrors(nameof(CodeRAOIn)) == null)
                {
                    return (string)_dataAccess.Get(nameof(CodeRAOIn));
                }
                else
                {
                    return _CodeRAOIn_Not_Valid;
                }
            }
            set
            {
                _CodeRAOIn_Not_Valid = value;
                if (GetErrors(nameof(CodeRAOIn)) == null)
                {
                    _dataAccess.Set(nameof(CodeRAOIn), value);
                }
                OnPropertyChanged(nameof(CodeRAOIn));
            }
        }

        private string _CodeRAOIn_Not_Valid = "";
        private void CodeRAOIn_Validation(string value)//TODO
        {
            ClearErrors(nameof(CodeRAOIn));
        }
        //CodeRAOIn property

        //StatusRAOIn property
        [Attributes.Form_Property("Статус РАО")]
        public string StatusRAOIn  //1 cyfer or OKPO.
        {
            get
            {
                if (GetErrors(nameof(StatusRAOIn)) == null)
                {
                    return (string)_dataAccess.Get(nameof(StatusRAOIn));
                }
                else
                {
                    return _StatusRAOIn_Not_Valid;
                }
            }
            set
            {
                _StatusRAOIn_Not_Valid = value;
                if (GetErrors(nameof(StatusRAOIn)) == null)
                {
                    _dataAccess.Set(nameof(StatusRAOIn), value);
                }
                OnPropertyChanged(nameof(StatusRAOIn));
            }
        }

        private string _StatusRAOIn_Not_Valid = "";
        private void StatusRAO_Validation(string value)//TODO
        {
            ClearErrors(nameof(StatusRAOIn));
        }
        //StatusRAOIn property

        //VolumeIn property
        [Attributes.Form_Property("Объем, куб. м")]
        public double VolumeIn
        {
            get
            {
                if (GetErrors(nameof(VolumeIn)) == null)
                {
                    return (double)_dataAccess.Get(nameof(VolumeIn));
                }
                else
                {
                    return _VolumeIn_Not_Valid;
                }
            }
            set
            {
                _VolumeIn_Not_Valid = value;
                if (GetErrors(nameof(VolumeIn)) == null)
                {
                    _dataAccess.Set(nameof(VolumeIn), value);
                }
                OnPropertyChanged(nameof(VolumeIn));
            }
        }

        private double _VolumeIn_Not_Valid = -1;
        private void VolumeIn_Validation(double value)//TODO
        {
            ClearErrors(nameof(VolumeIn));
        }
        //VolumeIn property

        //MassIn Property
        [Attributes.Form_Property("Масса, т")]
        public double MassIn
        {
            get
            {
                if (GetErrors(nameof(MassIn)) == null)
                {
                    return (double)_dataAccess.Get(nameof(MassIn));
                }
                else
                {
                    return _MassIn_Not_Valid;
                }
            }
            set
            {
                _MassIn_Not_Valid = value;
                if (GetErrors(nameof(MassIn)) == null)
                {
                    _dataAccess.Set(nameof(MassIn), value);
                }
                OnPropertyChanged(nameof(MassIn));
            }
        }

        private double _MassIn_Not_Valid = -1;
        private void MassIn_Validation()//TODO
        {
            ClearErrors(nameof(MassIn));
        }
        //MassIn Property

        //QuantityIn property
        [Attributes.Form_Property("Количество ОЗИИИ, шт.")]
        public int QuantityIn
        {
            get
            {
                if (GetErrors(nameof(QuantityIn)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(QuantityIn));//OK
                    return tmp != null ? (int)tmp : -1;
                }
                else
                {
                    return _QuantityIn_Not_Valid;
                }
            }
            set
            {
                QuantityIn_Validation(value);

                if (GetErrors(nameof(QuantityIn)) == null)
                {
                    _dataAccess.Set(nameof(QuantityIn), value);
                }
                OnPropertyChanged(nameof(QuantityIn));
            }
        }
        // positive int.
        private int _QuantityIn_Not_Valid = -1;
        private void QuantityIn_Validation(int value)//Ready
        {
            ClearErrors(nameof(QuantityIn));
            if (value <= 0)
                AddError(nameof(QuantityIn), "Недопустимое значение");
        }
        //QuantityIn property

        //TritiumActivityIn property
        [Attributes.Form_Property("Активность трития, Бк")]
        public string TritiumActivityIn
        {
            get
            {
                if (GetErrors(nameof(TritiumActivityIn)) == null)
                {
                    return (string)_dataAccess.Get(nameof(TritiumActivityIn));
                }
                else
                {
                    return _TritiumActivityIn_Not_Valid;
                }
            }
            set
            {
                _TritiumActivityIn_Not_Valid = value;
                if (GetErrors(nameof(TritiumActivityIn)) == null)
                {
                    _dataAccess.Set(nameof(TritiumActivityIn), value);
                }
                OnPropertyChanged(nameof(TritiumActivityIn));
            }
        }

        private string _TritiumActivityIn_Not_Valid = "";
        private void TritiumActivityIn_Validation(string value)//TODO
        {
            ClearErrors(nameof(TritiumActivityIn));
            string tmp = value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(TritiumActivityIn), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(TritiumActivityIn), "Недопустимое значение");
            }
        }
        //TritiumActivityIn property

        //BetaGammaActivityIn property
        [Attributes.Form_Property("Активность бета-, гамма-излучающих, кроме трития, Бк")]
        public string BetaGammaActivityIn
        {
            get
            {
                if (GetErrors(nameof(BetaGammaActivityIn)) == null)
                {
                    return (string)_dataAccess.Get(nameof(BetaGammaActivityIn));
                }
                else
                {
                    return _BetaGammaActivityIn_Not_Valid;
                }
            }
            set
            {
                _BetaGammaActivityIn_Not_Valid = value;
                if (GetErrors(nameof(BetaGammaActivityIn)) == null)
                {
                    _dataAccess.Set(nameof(BetaGammaActivityIn), value);
                }
                OnPropertyChanged(nameof(BetaGammaActivityIn));
            }
        }

        private string _BetaGammaActivityIn_Not_Valid = "";
        private void BetaGammaActivity_Validation(string value)//TODO
        {
            ClearErrors(nameof(BetaGammaActivityIn));
            if ((value==null)||(value.Equals("")))
            {
                AddError(nameof(BetaGammaActivityIn), "Поле не заполнено");
                return;
            }
            if (!(value.Contains('e')))
            {
                AddError(nameof(BetaGammaActivityIn), "Недопустимое значение");
                return;
            }
            string tmp = value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(BetaGammaActivityIn), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(BetaGammaActivityIn), "Недопустимое значение");
            }
        }
        //BetaGammaActivity property

        //AlphaActivityIn property
        [Attributes.Form_Property("Активность альфа-излучающих, кроме трансурановых, Бк")]
        public string AlphaActivityIn
        {
            get
            {
                if (GetErrors(nameof(AlphaActivityIn)) == null)
                {
                    return (string)_dataAccess.Get(nameof(AlphaActivityIn));
                }
                else
                {
                    return _AlphaActivityIn_Not_Valid;
                }
            }
            set
            {
                _AlphaActivityIn_Not_Valid = value;
                if (GetErrors(nameof(AlphaActivityIn)) == null)
                {
                    _dataAccess.Set(nameof(AlphaActivityIn), value);
                }
                OnPropertyChanged(nameof(AlphaActivityIn));
            }
        }

        private string _AlphaActivityIn_Not_Valid = "";
        private void AlphaActivityIn_Validation(string value)//TODO
        {
            ClearErrors(nameof(AlphaActivityIn));
            string tmp = value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(AlphaActivityIn), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(AlphaActivityIn), "Недопустимое значение");
            }
        }
        //AlphaActivityIn property

        //TransuraniumActivityIn property
        [Attributes.Form_Property("Активность трансурановых, Бк")]
        public string TransuraniumActivityIn
        {
            get
            {
                if (GetErrors(nameof(TransuraniumActivityIn)) == null)
                {
                    return (string)_dataAccess.Get(nameof(TransuraniumActivityIn));
                }
                else
                {
                    return _TransuraniumActivityIn_Not_Valid;
                }
            }
            set
            {
                _TransuraniumActivityIn_Not_Valid = value;
                if (GetErrors(nameof(TransuraniumActivityIn)) == null)
                {
                    _dataAccess.Set(nameof(TransuraniumActivityIn), value);
                }
                OnPropertyChanged(nameof(TransuraniumActivityIn));
            }
        }

        private string _TransuraniumActivityIn_Not_Valid = "";
        private void TransuraniumActivityIn_Validation(string value)//TODO
        {
            ClearErrors(nameof(TransuraniumActivityIn));
            string tmp = value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(TransuraniumActivityIn), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(TransuraniumActivityIn), "Недопустимое значение");
            }
        }
        //TransuraniumActivityIn property

        //CodeRAOout property
        [Attributes.Form_Property("Код РАО")]
        public string CodeRAOout
        {
            get
            {
                if (GetErrors(nameof(CodeRAOout)) == null)
                {
                    return (string)_dataAccess.Get(nameof(CodeRAOout));
                }
                else
                {
                    return _CodeRAOout_Not_Valid;
                }
            }
            set
            {
                _CodeRAOout_Not_Valid = value;
                if (GetErrors(nameof(CodeRAOout)) == null)
                {
                    _dataAccess.Set(nameof(CodeRAOout), value);
                }
                OnPropertyChanged(nameof(CodeRAOout));
            }
        }

        private string _CodeRAOout_Not_Valid = "";
        private void CodeRAOout_Validation(string value)//TODO
        {
            ClearErrors(nameof(CodeRAOout));
        }
        //CodeRAOout property

        //StatusRAOout property
        [Attributes.Form_Property("Статус РАО")]
        public string StatusRAOout  //1 cyfer or OKPO.
        {
            get
            {
                if (GetErrors(nameof(StatusRAOout)) == null)
                {
                    return (string)_dataAccess.Get(nameof(StatusRAOout));
                }
                else
                {
                    return _StatusRAOout_Not_Valid;
                }
            }
            set
            {
                _StatusRAOout_Not_Valid = value;
                if (GetErrors(nameof(StatusRAOout)) == null)
                {
                    _dataAccess.Set(nameof(StatusRAOout), value);
                }
                OnPropertyChanged(nameof(StatusRAOout));
            }
        }

        private string _StatusRAOout_Not_Valid = "";
        private void StatusRAOout_Validation(string value)//TODO
        {
            ClearErrors(nameof(StatusRAOout));
        }
        //StatusRAOout property

        //VolumeOut property
        [Attributes.Form_Property("Объем, куб. м")]
        public double VolumeOut
        {
            get
            {
                if (GetErrors(nameof(VolumeOut)) == null)
                {
                    return (double)_dataAccess.Get(nameof(VolumeOut));
                }
                else
                {
                    return _VolumeOut_Not_Valid;
                }
            }
            set
            {
                _VolumeOut_Not_Valid = value;
                if (GetErrors(nameof(VolumeOut)) == null)
                {
                    _dataAccess.Set(nameof(VolumeOut), value);
                }
                OnPropertyChanged(nameof(VolumeOut));
            }
        }

        private double _VolumeOut_Not_Valid = -1;
        private void VolumeOut_Validation(double value)//TODO
        {
            ClearErrors(nameof(VolumeOut));
        }
        //VolumeOut property

        //MassOut Property
        [Attributes.Form_Property("Масса, т")]
        public double MassOut
        {
            get
            {
                if (GetErrors(nameof(MassOut)) == null)
                {
                    return (double)_dataAccess.Get(nameof(MassOut));
                }
                else
                {
                    return _MassOut_Not_Valid;
                }
            }
            set
            {
                _MassOut_Not_Valid = value;
                if (GetErrors(nameof(MassOut)) == null)
                {
                    _dataAccess.Set(nameof(MassOut), value);
                }
                OnPropertyChanged(nameof(MassOut));
            }
        }

        private double _MassOut_Not_Valid = -1;
        private void MassOut_Validation()//TODO
        {
            ClearErrors(nameof(MassOut));
        }
        //MassOut Property

        //QuantityOZIIIout property
        [Attributes.Form_Property("Количество ОЗИИИ, шт.")]
        public int QuantityOZIIIout
        {
            get
            {
                if (GetErrors(nameof(QuantityOZIIIout)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(QuantityOZIIIout));//OK
                    return tmp != null ? (int)tmp : -1;
                }
                else
                {
                    return _QuantityOZIIIout_Not_Valid;
                }
            }
            set
            {
                QuantityOZIIIout_Validation(value);

                if (GetErrors(nameof(QuantityOZIIIout)) == null)
                {
                    _dataAccess.Set(nameof(QuantityOZIIIout), value);
                }
                OnPropertyChanged(nameof(QuantityOZIIIout));
            }
        }
        // positive int.
        private int _QuantityOZIIIout_Not_Valid = -1;
        private void QuantityOZIIIout_Validation(int value)//Ready
        {
            ClearErrors(nameof(QuantityOZIIIout));
            if (value <= 0)
                AddError(nameof(QuantityOZIIIout), "Недопустимое значение");
        }
        //QuantityOZIIIout property

        //TritiumActivityOut property
        [Attributes.Form_Property("Активность трития, Бк")]
        public string TritiumActivityOut
        {
            get
            {
                if (GetErrors(nameof(TritiumActivityOut)) == null)
                {
                    return (string)_dataAccess.Get(nameof(TritiumActivityOut));
                }
                else
                {
                    return _TritiumActivityOut_Not_Valid;
                }
            }
            set
            {
                _TritiumActivityOut_Not_Valid = value;
                if (GetErrors(nameof(TritiumActivityOut)) == null)
                {
                    _dataAccess.Set(nameof(TritiumActivityOut), value);
                }
                OnPropertyChanged(nameof(TritiumActivityOut));
            }
        }

        private string _TritiumActivityOut_Not_Valid = "";
        private void TritiumActivityOut_Validation(string value)//TODO
        {
            ClearErrors(nameof(TritiumActivityOut));
            string tmp = value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(TritiumActivityOut), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(TritiumActivityOut), "Недопустимое значение");
            }
        }
        //TritiumActivityOut property

        //BetaGammaActivityOut property
        [Attributes.Form_Property("Активность бета-, гамма-излучающих, кроме трития, Бк")]
        public string BetaGammaActivityOut
        {
            get
            {
                if (GetErrors(nameof(BetaGammaActivityOut)) == null)
                {
                    return (string)_dataAccess.Get(nameof(BetaGammaActivityOut));
                }
                else
                {
                    return _BetaGammaActivityOut_Not_Valid;
                }
            }
            set
            {
                _BetaGammaActivityOut_Not_Valid = value;
                if (GetErrors(nameof(BetaGammaActivityOut)) == null)
                {
                    _dataAccess.Set(nameof(BetaGammaActivityOut), value);
                }
                OnPropertyChanged(nameof(BetaGammaActivityOut));
            }
        }

        private string _BetaGammaActivityOut_Not_Valid = "";
        private void BetaGammaActivityOut_Validation(string value)//TODO
        {
            ClearErrors(nameof(BetaGammaActivityOut));
            string tmp = value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(BetaGammaActivityOut), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(BetaGammaActivityOut), "Недопустимое значение");
            }
        }
        //BetaGammaActivityOut property

        //AlphaActivityOut property
        [Attributes.Form_Property("Активность альфа-излучающих, кроме трансурановых, Бк")]
        public string AlphaActivityOut
        {
            get
            {
                if (GetErrors(nameof(AlphaActivityOut)) == null)
                {
                    return (string)_dataAccess.Get(nameof(AlphaActivityOut));
                }
                else
                {
                    return _AlphaActivityOut_Not_Valid;
                }
            }
            set
            {
                _AlphaActivityOut_Not_Valid = value;
                if (GetErrors(nameof(AlphaActivityOut)) == null)
                {
                    _dataAccess.Set(nameof(AlphaActivityOut), value);
                }
                OnPropertyChanged(nameof(AlphaActivityOut));
            }
        }

        private string _AlphaActivityOut_Not_Valid = "";
        private void AlphaActivityOut_Validation(string value)//TODO
        {
            ClearErrors(nameof(AlphaActivityOut));
            string tmp = value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(AlphaActivityOut), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(AlphaActivityOut), "Недопустимое значение");
            }
        }
        //AlphaActivityOut property

        //TransuraniumActivityOut property
        [Attributes.Form_Property("Активность трансурановых, Бк")]
        public string TransuraniumActivityOut
        {
            get
            {
                if (GetErrors(nameof(TransuraniumActivityOut)) == null)
                {
                    return (string)_dataAccess.Get(nameof(TransuraniumActivityOut));
                }
                else
                {
                    return _TransuraniumActivityOut_Not_Valid;
                }
            }
            set
            {
                _TransuraniumActivityOut_Not_Valid = value;
                if (GetErrors(nameof(TransuraniumActivityOut)) == null)
                {
                    _dataAccess.Set(nameof(TransuraniumActivityOut), value);
                }
                OnPropertyChanged(nameof(TransuraniumActivityOut));
            }
        }

        private string _TransuraniumActivityOut_Not_Valid = "";
        private void TransuraniumActivityOut_Validation(string value)//TODO
        {
            ClearErrors(nameof(TransuraniumActivityOut));
            string tmp = value;
            int len = tmp.Length;
            if ((tmp[0] == '(') && (tmp[len - 1] == ')'))
            {
                tmp = tmp.Remove(len - 1, 1);
                tmp = tmp.Remove(0, 1);
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(tmp, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(TransuraniumActivityOut), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(TransuraniumActivityOut), "Недопустимое значение");
            }
        }
        //TransuraniumActivityOut property
    }
}