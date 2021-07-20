﻿using Models.DataAccess;
using System;
using System.Collections.Generic;
using ClassLibrary1;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.11: Радионуклидный состав загрязненных участков территорий")]
    public class Form211 : Abstracts.Form2
    {
        public Form211() : base()
        {
            //FormNum.Value = "211";
            //NumberOfFields.Value = 11;
            Init();
            Validate_all();
        }

        private void Init()
        {
            DataAccess.Init<string>(nameof(Radionuclids), Radionuclids_Validation, null);
            Radionuclids.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PlotName), PlotName_Validation, null);
            PlotName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PlotKadastrNumber), PlotKadastrNumber_Validation, null);
            PlotKadastrNumber.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PlotCode), PlotCode_Validation, null);
            PlotCode.PropertyChanged += InPropertyChanged;
            DataAccess.Init<int?>(nameof(InfectedArea), InfectedArea_Validation, null);
            InfectedArea.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(SpecificActivityOfPlot), SpecificActivityOfPlot_Validation, null);
            SpecificActivityOfPlot.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(SpecificActivityOfLiquidPart), SpecificActivityOfLiquidPart_Validation, null);
            SpecificActivityOfLiquidPart.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(SpecificActivityOfDensePart), SpecificActivityOfDensePart_Validation, null);
            SpecificActivityOfDensePart.PropertyChanged += InPropertyChanged;
        }

        private void Validate_all()
        {
            Radionuclids_Validation(Radionuclids);
            PlotName_Validation(PlotName);
            PlotKadastrNumber_Validation(PlotKadastrNumber);
            PlotCode_Validation(PlotCode);
            InfectedArea_Validation(InfectedArea);
            SpecificActivityOfPlot_Validation(SpecificActivityOfPlot);
            SpecificActivityOfLiquidPart_Validation(SpecificActivityOfLiquidPart);
            SpecificActivityOfDensePart_Validation(SpecificActivityOfDensePart);
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //PlotName property
        public int? PlotNameId { get; set; }
        [Attributes.Form_Property("Наименование участка")]
        public virtual RamAccess<string> PlotName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PlotName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PlotName), value);
                }
                OnPropertyChanged(nameof(PlotName));
            }
        }

        private bool PlotName_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //PlotName property

        //PlotKadastrNumber property
        public int? PlotKadastrNumberId { get; set; }
        [Attributes.Form_Property("Кадастровый номер участка")]
        public virtual RamAccess<string> PlotKadastrNumber
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PlotKadastrNumber));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PlotKadastrNumber), value);
                }
                OnPropertyChanged(nameof(PlotKadastrNumber));
            }
        }

        private bool PlotKadastrNumber_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //PlotKadastrNumber property

        //PlotCode property
        public int? PlotCodeId { get; set; }
        [Attributes.Form_Property("Код участка")]
        public virtual RamAccess<string> PlotCode
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PlotCode));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PlotCode), value);
                }
                OnPropertyChanged(nameof(PlotCode));
            }
        }
        //6 symbols code
        private bool PlotCode_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //PlotCode property

        //InfectedArea property
        public int? InfectedAreaId { get; set; }
        [Attributes.Form_Property("Площадь загрязненной территории, кв. м")]
        public virtual RamAccess<int?> InfectedArea
        {
            get
            {

                {
                    return DataAccess.Get<int?>(nameof(InfectedArea));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(InfectedArea), value);
                }
                OnPropertyChanged(nameof(InfectedArea));
            }
        }

        private bool InfectedArea_Validation(RamAccess<int?> value)//TODO
        {
            value.ClearErrors();
            if (value.Value == null)
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //InfectedArea property

        //Radionuclids property
        public int? RadionuclidsId { get; set; }
        [Attributes.Form_Property("Наименования радионуклидов")]
        public virtual RamAccess<string> Radionuclids
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(Radionuclids));//OK

                }

                {

                }
            }
            set
            {



                {
                    DataAccess.Set(nameof(Radionuclids), value);
                }
                OnPropertyChanged(nameof(Radionuclids));
            }
        }
        //If change this change validation
        private bool Radionuclids_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if ((value.Value == null) || value.Value.Equals(""))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            foreach (var item in Spravochniki.SprRadionuclids)
            {
                if (item.Item1.Equals(value.Value))
                {
                    return true;
                }
            }
            value.AddError("Недопустимое значение");
            return false;
        }
        //Radionuclids property

        ////RadionuclidNameNote property
        //public virtual RamAccess<string> RadionuclidNameNote
        //{
        //    get
        //    {

        //        {
        //            return DataAccess.Get<string>(nameof(RadionuclidNameNote));
        //        }

        //        {

        //        }
        //    }
        //    set
        //    {


        //        {
        //            DataAccess.Set(nameof(RadionuclidNameNote), value);
        //        }
        //        OnPropertyChanged(nameof(RadionuclidNameNote));
        //    }
        //}

        //private bool RadionuclidNameNote_Validation(RamAccess<string> value)
        //{
        //    value.ClearErrors(); return true;
        //}
        ////RadionuclidNameNote property

        //SpecificActivityOfPlot property
        public int? SpecificActivityOfPlotId { get; set; }
        [Attributes.Form_Property("Удельная активность, Бк/г")]
        public virtual RamAccess<string> SpecificActivityOfPlot
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(SpecificActivityOfPlot));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(SpecificActivityOfPlot), value);
                }
                OnPropertyChanged(nameof(SpecificActivityOfPlot));
            }
        }

        private bool SpecificActivityOfPlot_Validation(RamAccess<string> value)//TODO
        {
            return true;
            //value.ClearErrors();
            //var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
            //   NumberStyles.AllowExponent;
            //try
            //{
            //    if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)){value.AddError("Число должно быть больше нуля");return false;}
            //}
            //catch
            //{
            //    value.AddError( "Недопустимое значение");
            //}
        }
        //SpecificActivityOfPlot property

        //SpecificActivityOfLiquidPart property
        public int? SpecificActivityOfLiquidPartId { get; set; }
        [Attributes.Form_Property("Удельная активность жидкой части, Бк/г")]
        public virtual RamAccess<string> SpecificActivityOfLiquidPart
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(SpecificActivityOfLiquidPart));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(SpecificActivityOfLiquidPart), value);
                }
                OnPropertyChanged(nameof(SpecificActivityOfLiquidPart));
            }
        }

        private bool SpecificActivityOfLiquidPart_Validation(RamAccess<string> value)//TODO
        {
            return true;
            //value.ClearErrors();
            //var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
            //   NumberStyles.AllowExponent;
            //try
            //{
            //    if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)){value.AddError("Число должно быть больше нуля");return false;}
            //}
            //catch
            //{
            //    value.AddError( "Недопустимое значение");
            //}
        }
        //SpecificActivityOfLiquidPart property

        //SpecificActivityOfDensePart property
        public int? SpecificActivityOfDensePartId { get; set; }
        [Attributes.Form_Property("Удельная активность твердой части, Бк/г")]
        public virtual RamAccess<string> SpecificActivityOfDensePart
        {
            get => DataAccess.Get<string>(nameof(SpecificActivityOfDensePart));
            set
            {


                {
                    DataAccess.Set(nameof(SpecificActivityOfDensePart), value);
                }
                OnPropertyChanged(nameof(SpecificActivityOfDensePart));
            }
        }

        private bool SpecificActivityOfDensePart_Validation(RamAccess<string> value)//TODO
        {
            return true;
            //value.ClearErrors();
            //var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
            //   NumberStyles.AllowExponent;
            //try
            //{
            //    if (!(double.Parse(value.Value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0)){value.AddError("Число должно быть больше нуля");return false;}
            //}
            //catch
            //{
            //    value.AddError( "Недопустимое значение");
            //}
        }
        //SpecificActivityOfDensePart property
    }
}
