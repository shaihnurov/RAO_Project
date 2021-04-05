﻿using System;
using System.Globalization;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Client_Model
{
    [Serializable]
    [Attributes.FormVisual_Class("Форма 2.9: Активность радионуклидов, отведенных со сточными водами")]
    public class Form29 : Form2
    {
        public Form29() : base()
        {
        }

        [Attributes.FormVisual("Форма")]
        public override string FormNum { get { return "29"; } }
        public override int NumberOfFields { get; } = 8;
        public override void Object_Validation()
        {

        }

        //WasteSourceName property
        [Attributes.FormVisual("Наименование, номер выпуска сточных вод")]
        public string WasteSourceName
        {
            get
            {
                if (GetErrors(nameof(WasteSourceName)) != null)
                {
                    return (string)_WasteSourceName.Get();
                }
                else
                {
                    return _WasteSourceName_Not_Valid;
                }
            }
            set
            {
                _WasteSourceName_Not_Valid = value;
                if (GetErrors(nameof(WasteSourceName)) != null)
                {
                    _WasteSourceName.Set(_WasteSourceName_Not_Valid);
                }
                OnPropertyChanged(nameof(WasteSourceName));
            }
        }
        private IDataLoadEngine _WasteSourceName;
        private string _WasteSourceName_Not_Valid = "";
        private void WasteSourceName_Validation()
        {
            ClearErrors(nameof(WasteSourceName));
        }
        //WasteSourceName property

        //RadionuclidName property
        [Attributes.FormVisual("Радионуклид")]
        public string RadionuclidName
        {
            get
            {
                if (GetErrors(nameof(RadionuclidName)) != null)
                {
                    return (string)_RadionuclidName.Get();
                }
                else
                {
                    return _RadionuclidName_Not_Valid;
                }
            }
            set
            {
                _RadionuclidName_Not_Valid = value;
                if (GetErrors(nameof(RadionuclidName)) != null)
                {
                    _RadionuclidName.Set(_RadionuclidName_Not_Valid);
                }
                OnPropertyChanged(nameof(RadionuclidName));
            }
        }
        private IDataLoadEngine _RadionuclidName;//If change this change validation
        private string _RadionuclidName_Not_Valid = "";
        private void RadionuclidName_Validation()//TODO
        {
            ClearErrors(nameof(RadionuclidName));
        }
        //RadionuclidName property

        //AllowedActivity property
        [Attributes.FormVisual("Допустимая активность радионуклида, Бк")]
        public string AllowedActivity
        {
            get
            {
                if (GetErrors(nameof(AllowedActivity)) != null)
                {
                    return (string)_AllowedActivity.Get();
                }
                else
                {
                    return _AllowedActivity_Not_Valid;
                }
            }
            set
            {
                _AllowedActivity_Not_Valid = value;
                if (GetErrors(nameof(AllowedActivity)) != null)
                {
                    _AllowedActivity.Set(_AllowedActivity_Not_Valid);
                }
                OnPropertyChanged(nameof(AllowedActivity));
            }
        }
        private IDataLoadEngine _AllowedActivity;
        private string _AllowedActivity_Not_Valid = "";
        private void AllowedActivity_Validation(string value)//Ready
        {
            ClearErrors(nameof(AllowedActivity));
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(AllowedActivity), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(AllowedActivity), "Недопустимое значение");
            }
        }
        //AllowedActivity property

        private string _allowedActivityNote = "";
        public string AllowedActivityNote
        {
            get { return _allowedActivityNote; }
            set
            {
                _allowedActivityNote = value;
                OnPropertyChanged("AllowedActivityNote");
            }
        }

        //FactedActivity property
        [Attributes.FormVisual("Фактическая активность радионуклида, Бк")]
        public string FactedActivity
        {
            get
            {
                if (GetErrors(nameof(FactedActivity)) != null)
                {
                    return (string)_FactedActivity.Get();
                }
                else
                {
                    return _FactedActivity_Not_Valid;
                }
            }
            set
            {
                _FactedActivity_Not_Valid = value;
                if (GetErrors(nameof(FactedActivity)) != null)
                {
                    _FactedActivity.Set(_FactedActivity_Not_Valid);
                }
                OnPropertyChanged(nameof(FactedActivity));
            }
        }
        private IDataLoadEngine _FactedActivity;
        private string _FactedActivity_Not_Valid = "";
        private void FactedActivity_Validation(string value)//Ready
        {
            ClearErrors(nameof(FactedActivity));
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(FactedActivity), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(FactedActivity), "Недопустимое значение");
            }
        }
        //FactedActivity property

        private string _factedActivityNote = "";
        public string FactedActivityNote
        {
            get { return _factedActivityNote; }
            set
            {
                _factedActivityNote = value;
                OnPropertyChanged("FactedActivityNote");
            }
        }
    }
}
