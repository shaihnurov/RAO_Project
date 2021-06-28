﻿using Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.6: Контроль загрязнения подземных вод РВ")]
    public class Form26 : Abstracts.Form2
    {
        public Form26() : base()
        {
            FormNum.Value = "26";
            NumberOfFields.Value = 11;
            Init();
            Validate_all();
        }

        private void Init()
        {
            DataAccess.Init<string>(nameof(ObservedSourceNumber), ObservedSourceNumber_Validation, null);
            ObservedSourceNumber.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(ControlledAreaName), ControlledAreaName_Validation, null);
            ControlledAreaName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(SupposedWasteSource), SupposedWasteSource_Validation, null);
            SupposedWasteSource.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(DistanceToWasteSource), DistanceToWasteSource_Validation, null);
            DistanceToWasteSource.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(TestDepth), TestDepth_Validation, null);
            TestDepth.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(RadionuclidName), RadionuclidName_Validation, null);
            RadionuclidName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(AverageYearConcentration), AverageYearConcentration_Validation, null);
            AverageYearConcentration.PropertyChanged += InPropertyChanged;
        }

        private void Validate_all()
        {
            ObservedSourceNumber_Validation(ObservedSourceNumber);
            ControlledAreaName_Validation(ControlledAreaName);
            SupposedWasteSource_Validation(SupposedWasteSource);
            DistanceToWasteSource_Validation(DistanceToWasteSource);
            TestDepth_Validation(TestDepth);
            RadionuclidName_Validation(RadionuclidName);
            AverageYearConcentration_Validation(AverageYearConcentration);
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //SourcesQuantity property
        [Attributes.Form_Property("Количество источников, шт.")]
        public RamAccess<int> SourcesQuantity
        {
            get
            {

                {
                    return DataAccess.Get<int>(nameof(SourcesQuantity));
                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(SourcesQuantity), value);
                }
                OnPropertyChanged(nameof(SourcesQuantity));
            }
        }
        // positive int.
        private bool SourcesQuantity_Validation(RamAccess<int?> value)//Ready
        {
            value.ClearErrors();
            if (value.Value <= 0)
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //SourcesQuantity property

        //ObservedSourceNumber property
        [Attributes.Form_Property("Номер наблюдательной скважины")]
        public RamAccess<string> ObservedSourceNumber
        {
            get => DataAccess.Get<string>(nameof(ObservedSourceNumber));
            set
            {
                DataAccess.Set(nameof(ObservedSourceNumber), value);
                OnPropertyChanged(nameof(ObservedSourceNumber));
            }
        }
        //If change this change validation
        private bool ObservedSourceNumber_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors(); return true;
        }
        //ObservedSourceNumber property

        //ControlledAreaName property
        [Attributes.Form_Property("Наименование зоны контроля")]
        public RamAccess<string> ControlledAreaName
        {
            get => DataAccess.Get<string>(nameof(ControlledAreaName));
            set
            {
                DataAccess.Set(nameof(ControlledAreaName), value);
                OnPropertyChanged(nameof(ControlledAreaName));
            }
        }
        //If change this change validation
        private bool ControlledAreaName_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            List<string> spr = new List<string>()
            {
                "пп",
                "сзз",
                "зн",
                "прим."
            };
            if (!spr.Contains(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //ControlledAreaName property

        //SupposedWasteSource property
        [Attributes.Form_Property("Предполагаемый источник поступления радиоактивных веществ")]
        public RamAccess<string> SupposedWasteSource
        {
            get => DataAccess.Get<string>(nameof(SupposedWasteSource));
            set
            {
                DataAccess.Set(nameof(SupposedWasteSource), value);
                OnPropertyChanged(nameof(SupposedWasteSource));
            }
        }

        private bool SupposedWasteSource_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();//done
            return true;
        }
        //SupposedWasteSource property

        //DistanceToWasteSource property
        [Attributes.Form_Property("Расстояние от источника поступления радиоактивных веществ до наблюдательной скважины, м")]
        public RamAccess<string> DistanceToWasteSource
        {
            get => DataAccess.Get<string>(nameof(DistanceToWasteSource));
            set
            {


                {
                    DataAccess.Set(nameof(DistanceToWasteSource), value);
                }
                OnPropertyChanged(nameof(DistanceToWasteSource));
            }
        }

        private bool DistanceToWasteSource_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Equals("прим."))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //DistanceToWasteSource property

        //TestDepth property
        [Attributes.Form_Property("Глубина отбора проб, м")]
        public RamAccess<string> TestDepth
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(TestDepth));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(TestDepth), value);
                }
                OnPropertyChanged(nameof(TestDepth));
            }
        }

        private bool TestDepth_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                return true;
            }
            if (value.Value.Equals("прим."))
            {
                return true;
            }
            try
            {
                int k = int.Parse(value.Value);
                if (k <= 0)
                {
                    value.AddError("Недопустимое значение");
                    return false;
                }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //TestDepth property

        //TestDepthNote property
        public RamAccess<string> TestDepthNote
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(TestDepthNote));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(TestDepthNote), value);
                }
                OnPropertyChanged(nameof(TestDepthNote));
            }
        }

        private bool TestDepthNote_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            return true;
        }
        //TestDepthNote property

        //RadionuclidName property
        [Attributes.Form_Property("Радионуклид")]
        public RamAccess<string> RadionuclidName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(RadionuclidName));
                }

                {

                }
            }
            set
            {
                DataAccess.Set(nameof(RadionuclidName), value);
                OnPropertyChanged(nameof(RadionuclidName));
            }
        }
        //If change this change validation
        private bool RadionuclidName_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            List<string> spr = new List<string>();
            if (!spr.Contains(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //RadionuclidName property

        //AverageYearConcentration property
        [Attributes.Form_Property("Среднегодовое содержание радионуклида, Бк/кг")]
        public RamAccess<string> AverageYearConcentration
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(AverageYearConcentration));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(AverageYearConcentration), value);
                }
                OnPropertyChanged(nameof(AverageYearConcentration));
            }
        }

        private bool AverageYearConcentration_Validation(RamAccess<string> value)//TODO
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
            NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)) { value.AddError("Число должно быть больше нуля"); return false; }
            }
            catch
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //AverageYearConcentration property
    }
}
