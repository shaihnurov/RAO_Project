﻿using Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Характеристики экспортируемых ЗРИ/ОЗИИИ:")]
    public class Form31_1 : Abstracts.Form3
    {
        public Form31_1() : base()
        {
            //FormNum.Value = "31_1";
            //NumberOfFields.Value = 3;
        }

        [Attributes.Form_Property("Форма")]

        public override bool Object_Validation()
        {
            return false;
        }

        //Radionuclids property
        public int? RadionuclidsId { get; set; }
        [Attributes.Form_Property("Радионуклиды")]
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
            List<Tuple<string, string>> spr = new List<Tuple<string, string>>();//Here binds spravochnik
            foreach (Tuple<string, string> item in spr)
            {
                if (item.Item2.Equals(value))
                {
                    Radionuclids.Value = item.Item2;
                    return true;
                }
            }
            return false;
        }
        //Radionuclids property

        //Quantity property
        public int? QuantityId { get; set; }
        [Attributes.Form_Property("Количество, шт.")]
        public virtual RamAccess<int> Quantity
        {
            get
            {

                {
                    return DataAccess.Get<int>(nameof(Quantity));//OK

                }

                {

                }
            }
            set
            {



                {
                    DataAccess.Set(nameof(Quantity), value);
                }
                OnPropertyChanged(nameof(Quantity));
            }
        }
        // positive int.

        private bool Quantity_Validation(RamAccess<int> value)//Ready
        {
            value.ClearErrors();
            if (value.Value <= 0)
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //Quantity property

        //SummaryActivity property
        public int? SummaryActivityId { get; set; }
        [Attributes.Form_Property("Суммарная активность, Бк")]
        public virtual RamAccess<string> SummaryActivity
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(SummaryActivity));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(SummaryActivity), value);
                }
                OnPropertyChanged(nameof(SummaryActivity));
            }
        }


        private bool SummaryActivity_Validation(RamAccess<string> value)//Ready
        {
            value.ClearErrors();
            if ((value.Value == null) || (value.Value.Equals("")))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (!(value.Value.Contains('e')))
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
        //SummaryActivity property
    }
}
