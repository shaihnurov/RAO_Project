﻿using Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 5.4: Сведения о наличии в подведомственных организациях ОРИ")]
    public class Form54 : Abstracts.Form5
    {
        public Form54() : base()
        {
            FormNum = "54";
            NumberOfFields = 10;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //TypeOfAccountedParts property
        [Attributes.Form_Property("Тип учетных единиц")]
        public int TypeOfAccountedParts
        {
            get
            {
                if (GetErrors(nameof(TypeOfAccountedParts)) == null)
                {
                    return (int)_dataAccess.Get(nameof(TypeOfAccountedParts));
                }
                else
                {
                    return _TypeOfAccountedParts_Not_Valid;
                }
            }
            set
            {
                _TypeOfAccountedParts_Not_Valid = value;
                if (GetErrors(nameof(TypeOfAccountedParts)) == null)
                {
                    _dataAccess.Set(nameof(TypeOfAccountedParts), _TypeOfAccountedParts_Not_Valid);
                }
                OnPropertyChanged(nameof(TypeOfAccountedParts));
            }
        }
        //1 or 2
        private int _TypeOfAccountedParts_Not_Valid = -1; //1 or 2
        private void TypeOfAccountedParts_Validation(int value)//Ready
        {
            ClearErrors(nameof(TypeOfAccountedParts));
            if ((value != 1) && (value != 2))
                AddError(nameof(TypeOfAccountedParts), "Недопустимое значение");
        }
        //TypeOfAccountedParts property

        //KindOri property
        [Attributes.Form_Property("Вид ОРИ")]
        public int KindOri
        {
            get
            {
                if (GetErrors(nameof(KindOri)) == null)
                {
                    return (int)_dataAccess.Get(nameof(KindOri));
                }
                else
                {
                    return _KindOri_Not_Valid;
                }
            }
            set
            {
                _KindOri_Not_Valid = value;
                if (GetErrors(nameof(KindOri)) == null)
                {
                    _dataAccess.Set(nameof(KindOri), _KindOri_Not_Valid);
                }
                OnPropertyChanged(nameof(KindOri));
            }
        }

        private int _KindOri_Not_Valid = -1;
        private void KindOri_Validation(int value)//TODO
        {
        }
        //KindOri property

        //AggregateState property
        [Attributes.Form_Property("Агрегатное состояние")]
        public byte AggregateState//1 2 3
        {
            get
            {
                if (GetErrors(nameof(AggregateState)) == null)
                {
                    return (byte)_dataAccess.Get(nameof(AggregateState));
                }
                else
                {
                    return _AggregateState_Not_Valid;
                }
            }
            set
            {
                _AggregateState_Not_Valid = value;
                if (GetErrors(nameof(AggregateState)) == null)
                {
                    _dataAccess.Set(nameof(AggregateState), _AggregateState_Not_Valid);
                }
                OnPropertyChanged(nameof(AggregateState));
            }
        }

        private byte _AggregateState_Not_Valid = 0;
        private void AggregateState_Validation(byte value)//Ready
        {
            ClearErrors(nameof(AggregateState));
            if ((value != 1) && (value != 2) && (value != 3))
                AddError(nameof(AggregateState), "Недопустимое значение");
        }
        //AggregateState property

        //Radionuclids property
        [Attributes.Form_Property("Радионуклиды")]
        public string Radionuclids
        {
            get
            {
                if (GetErrors(nameof(Radionuclids)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(Radionuclids));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    return _Radionuclids_Not_Valid;
                }
            }
            set
            {
                Radionuclids_Validation(value);

                if (GetErrors(nameof(Radionuclids)) == null)
                {
                    _dataAccess.Set(nameof(Radionuclids), _Radionuclids_Not_Valid);
                }
                OnPropertyChanged(nameof(Radionuclids));
            }
        }
        //If change this change validation
        private string _Radionuclids_Not_Valid = "";
        private void Radionuclids_Validation(string value)//TODO
        {
            ClearErrors(nameof(Radionuclids));
            if ((value == null) || value.Equals(""))
            {
                AddError(nameof(Radionuclids), "Поле не заполнено");
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

        //Activity property
        [Attributes.Form_Property("Активность, Бк")]
        public string Activity
        {
            get
            {
                if (GetErrors(nameof(Activity)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(Activity));//OK
                    return tmp != null ? (string)tmp : null;
                }
                else
                {
                    return _Activity_Not_Valid;
                }
            }
            set
            {
                _Activity_Not_Valid = value;
                if (GetErrors(nameof(Activity)) == null)
                {
                    _dataAccess.Set(nameof(Activity), _Activity_Not_Valid);
                }
                OnPropertyChanged(nameof(Activity));
            }
        }

        private string _Activity_Not_Valid = "";
        private void Activity_Validation(string value)//Ready
        {
            ClearErrors(nameof(Activity));
            if ((value == null) || value.Equals(""))
            {
                AddError(nameof(Activity), "Поле не заполнено");
                return;
            }
            if (!(value.Contains('e')))
            {
                AddError(nameof(Activity), "Недопустимое значение");
                return;
            }
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(Activity), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(Activity), "Недопустимое значение");
            }
        }
        //Activity property

        //Quantity property
        [Attributes.Form_Property("Количество, шт.")]
        public int Quantity
        {
            get
            {
                if (GetErrors(nameof(Quantity)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(Quantity));//OK
                    return tmp != null ? (int)tmp : -1;
                }
                else
                {
                    return _Quantity_Not_Valid;
                }
            }
            set
            {
                Quantity_Validation(value);
                //_Quantity_Not_Valid = value;

                if (GetErrors(nameof(Quantity)) == null)
                {
                    _dataAccess.Set(nameof(Quantity), _Quantity_Not_Valid);
                }
                OnPropertyChanged(nameof(Quantity));
            }
        }
        // positive int.
        private int _Quantity_Not_Valid = -1;
        private void Quantity_Validation(int value)//Ready
        {
            ClearErrors(nameof(Quantity));
            if (value <= 0)
                AddError(nameof(Quantity), "Недопустимое значение");
        }
        //Quantity property

        //Volume property
        [Attributes.Form_Property("Объем, куб. м")]
        public double Volume
        {
            get
            {
                if (GetErrors(nameof(Volume)) == null)
                {
                    return (double)_dataAccess.Get(nameof(Volume));
                }
                else
                {
                    return _Volume_Not_Valid;
                }
            }
            set
            {
                _Volume_Not_Valid = value;
                if (GetErrors(nameof(Volume)) == null)
                {
                    _dataAccess.Set(nameof(Volume), _Volume_Not_Valid);
                }
                OnPropertyChanged(nameof(Volume));
            }
        }

        private double _Volume_Not_Valid = -1;
        private void Volume_Validation(double value)//TODO
        {
            ClearErrors(nameof(Volume));
        }
        //Volume property

        //Mass Property
        [Attributes.Form_Property("Масса, кг")]
        public double Mass
        {
            get
            {
                if (GetErrors(nameof(Mass)) == null)
                {
                    return (double)_dataAccess.Get(nameof(Mass));
                }
                else
                {
                    return _Mass_Not_Valid;
                }
            }
            set
            {
                _Mass_Not_Valid = value;
                if (GetErrors(nameof(Mass)) == null)
                {
                    _dataAccess.Set(nameof(Mass), _Mass_Not_Valid);
                }
                OnPropertyChanged(nameof(Mass));
            }
        }

        private double _Mass_Not_Valid = -1;
        private void Mass_Validation()//TODO
        {
            ClearErrors(nameof(Mass));
        }
        //Mass Property
    }
}
