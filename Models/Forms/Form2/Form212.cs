﻿using Models.DataAccess; using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using Spravochniki;
using System.Linq;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.12: Суммарные сведения о РВ не в составе ЗРИ")]
    public class Form212 : Abstracts.Form2
    {
        public Form212() : base()
        {
            FormNum.Value = "2.12";
            //NumberOfFields.Value = 8;
            Validate_all();
        }

        private void Validate_all()
        {
            Radionuclids_Validation(Radionuclids);
            OperationCode_Validation(OperationCode);
            ObjectTypeCode_Validation(ObjectTypeCode);
            Activity_Validation(Activity);
            ProviderOrRecieverOKPO_Validation(ProviderOrRecieverOKPO);
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //OperationCode property
#region  
public int _DB { get; set; } = 0; [NotMapped]        [Attributes.Form_Property("Код")]
        public RamAccess<string> OperationCode
        {
            get => new RamAccess<string>(OperationCode_Validation, _DB);
            set
            {
                OperationCode_DB = value.Value;
                OnPropertyChanged(nameof(OperationCode));
            }
        }


        private bool OperationCode_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //OperationCode property
        #endregion

        //ObjectTypeCode property
        #region  
public int _DB { get; set; } = 0; [NotMapped]        [Attributes.Form_Property("Код типа объектов учета")]
        public RamAccess<string> ObjectTypeCode
        {
            get
            {

                {
                    var tmp = new RamAccess<string>(ObjectTypeCode_Validation, _DB);
                    tmp.PropertyChanged += ValueChanged;
                    return tmp;
                }

                {

                }
            }
            set
            {


                {
                    ObjectTypeCode_DB = value.Value;
                }
                OnPropertyChanged(nameof(ObjectTypeCode));
            }
        }
        //2 digit code

        private bool ObjectTypeCode_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //ObjectTypeCode property
        #endregion

        //Radionuclids property
        #region  
public int _DB { get; set; } = 0; [NotMapped]        [Attributes.Form_Property("Радионуклиды")]
        public RamAccess<string> Radionuclids
        {
            get
            {

                {
                    var tmp = new RamAccess<string>(Radionuclids_Validation, _DB);//OK
                    tmp.PropertyChanged += ValueChanged;
                    return tmp;

                }

                {

                }
            }
            set
            {



                {
                    Radionuclids_DB = value.Value;
                }
                OnPropertyChanged(nameof(Radionuclids));
            }
        }
        //If change this change validation

        private bool Radionuclids_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            string[] nuclids = value.Value.Split("; ");
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
        //Radionuclids property
        #endregion

        //Activity property
        #region  
public int _DB { get; set; } = 0; [NotMapped]        [Attributes.Form_Property("Активность, Бк")]
        public RamAccess<string> Activity
        {
            get
            {

                {
                    var tmp = new RamAccess<string>(Activity_Validation, _DB);
                    tmp.PropertyChanged += ValueChanged;
                    return tmp;
                }

                {

                }
            }
            set
            {


                {
                    Activity_DB = value.Value;
                }
                OnPropertyChanged(nameof(Activity));
            }
        }


        private bool Activity_Validation(RamAccess<string> value)//Ready
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
        //Activity property
        #endregion

        //ProviderOrRecieverOKPO property
        #region  
public int _DB { get; set; } = 0; [NotMapped]        [Attributes.Form_Property("ОКПО поставщика/получателя")]
        public RamAccess<string> ProviderOrRecieverOKPO
        {
            get
            {

                {
                    var tmp = new RamAccess<string>(ProviderOrRecieverOKPO_Validation, _DB);//OK
                    tmp.PropertyChanged += ValueChanged;
                    return tmp;

                }

                {

                }
            }
            set
            {



                {
                    ProviderOrRecieverOKPO_DB = value.Value;
                }
                OnPropertyChanged(nameof(ProviderOrRecieverOKPO));
            }
        }


        private bool ProviderOrRecieverOKPO_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
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
        //ProviderOrRecieverOKPO property
        #endregion

        protected List<string> OKSM = new List<string>
            {
                "АФГАНИСТАН","АЛБАНИЯ","АНТАРКТИДА","АЛЖИР","АМЕРИКАНСКОЕ САМОА","АНДОРРА","АНГОЛА","АНТИГУА И БАРБУДА","АЗЕРБАЙДЖАН","АРГЕНТИНА","АВСТРАЛИЯ","АВСТРИЯ","БАГАМЫ","БАХРЕЙН",
                "БАНГЛАДЕШ","АРМЕНИЯ","БАРБАДОС","БЕЛЬГИЯ","БЕРМУДЫ","БУТАН","БОЛИВИЯ, МНОГОНАЦИОНАЛЬНОЕ ГОСУДАРСТВО","БОСНИЯ И ГЕРЦЕГОВИНА","БОТСВАНА","ОСТРОВ БУВЕ","БРАЗИЛИЯ","БЕЛИЗ",
                "БРИТАНСКАЯ ТЕРРИТОРИЯ В ИНДИЙСКОМ ОКЕАНЕ","СОЛОМОНОВЫ ОСТРОВА","ВИРГИНСКИЕ ОСТРОВА (БРИТАНСКИЕ)","БРУНЕЙ-ДАРУССАЛАМ","БОЛГАРИЯ","МЬЯНМА","БУРУНДИ","БЕЛАРУСЬ","КАМБОДЖА",
                "КАМЕРУН","КАНАДА","КАБО-ВЕРДЕ","ОСТРОВА КАЙМАН","ЦЕНТРАЛЬНО-АФРИКАНСКАЯ РЕСПУБЛИКА","ШРИ-ЛАНКА","ЧАД","ЧИЛИ","КИТАЙ","ТАЙВАНЬ (КИТАЙ)","ОСТРОВ РОЖДЕСТВА","КОКОСОВЫЕ (КИЛИНГ) ОСТРОВА",
                "КОЛУМБИЯ","КОМОРЫ","МАЙОТТА","КОНГО","КОНГО, ДЕМОКРАТИЧЕСКАЯ РЕСПУБЛИКА","ОСТРОВА КУКА","КОСТА-РИКА","ХОРВАТИЯ","КУБА","КИПР","ЧЕХИЯ","БЕНИН","ДАНИЯ","ДОМИНИКА","ДОМИНИКАНСКАЯ РЕСПУБЛИКА",
                "ЭКВАДОР","ЭЛЬ-САЛЬВАДОР","ЭКВАТОРИАЛЬНАЯ ГВИНЕЯ","ЭФИОПИЯ","ЭРИТРЕЯ","ЭСТОНИЯ","ФАРЕРСКИЕ ОСТРОВА","ФОЛКЛЕНДСКИЕ ОСТРОВА (МАЛЬВИНСКИЕ)","ЮЖНАЯ ДЖОРДЖИЯ И ЮЖНЫЕ САНДВИЧЕВЫ ОСТРОВА",
                "ФИНЛЯНДИЯ","ЭЛАНДСКИЕ ОСТРОВА","ФРАНЦИЯ","ФРАНЦУЗСКАЯ ГВИАНА","БОНЭЙР, СИНТ-ЭСТАТИУС И САБА","НОВАЯ КАЛЕДОНИЯ","ВАНУАТУ","НОВАЯ ЗЕЛАНДИЯ","НИКАРАГУА","НИГЕР","ФИДЖИ",
                "ФРАНЦУЗСКАЯ ПОЛИНЕЗИЯ","ФРАНЦУЗСКИЕ ЮЖНЫЕ ТЕРРИТОРИИ","ДЖИБУТИ","ГАБОН","ГРУЗИЯ","ГАМБИЯ","ПАЛЕСТИНА, ГОСУДАРСТВО","ГЕРМАНИЯ","ГАНА","ГИБРАЛТАР","КИРИБАТИ","МАЛИ","МАЛЬТА",
                "ГРЕЦИЯ","ГРЕНЛАНДИЯ","ГРЕНАДА","ГВАДЕЛУПА","ГУАМ","ГВАТЕМАЛА","ГВИНЕЯ","ГАЙАНА","ГАИТИ","ОСТРОВ ХЕРД И ОСТРОВА МАКДОНАЛЬД","ПАПСКИЙ ПРЕСТОЛ (ГОСУДАРСТВО - ГОРОД ВАТИКАН)",
                "ГОНДУРАС","ГОНКОНГ","ВЕНГРИЯ","ИСЛАНДИЯ","ИНДИЯ","ИНДОНЕЗИЯ","ИРАН (ИСЛАМСКАЯ РЕСПУБЛИКА)","ИРАК","ИРЛАНДИЯ","ИЗРАИЛЬ","ИТАЛИЯ","КОТ Д'ИВУАР","ЯМАЙКА","ЯПОНИЯ","МАЛЬДИВЫ",
                "КАЗАХСТАН","ИОРДАНИЯ","КЕНИЯ","КОРЕЯ, НАРОДНО-ДЕМОКРАТИЧЕСКАЯ РЕСПУБЛИКА","КОРЕЯ, РЕСПУБЛИКА","КУВЕЙТ","КИРГИЗИЯ","НИГЕРИЯ","НИУЭ","ОСТРОВ НОРФОЛК","НОРВЕГИЯ","СЕВЕРНЫЕ МАРИАНСКИЕ ОСТРОВА",
                "ЛАОССКАЯ НАРОДНО-ДЕМОКРАТИЧЕСКАЯ РЕСПУБЛИКА","ЛИВАН","ЛЕСОТО","ЛАТВИЯ","ЛИБЕРИЯ","ЛИВИЯ","ЛИХТЕНШТЕЙН","ЛИТВА","ЛЮКСЕМБУРГ","МАКАО","МАДАГАСКАР","МАЛАВИ","МАЛАЙЗИЯ",
                "МАРТИНИКА","МАВРИТАНИЯ","МАВРИКИЙ","МЕКСИКА","МОНАКО","МОНГОЛИЯ","МОЛДОВА, РЕСПУБЛИКА","ЧЕРНОГОРИЯ","МОНТСЕРРАТ","МАРОККО","МОЗАМБИК","ОМАН","НАМИБИЯ","НАУРУ","НЕПАЛ",
                "АРУБА","СЕН-МАРТЕН (нидерландская часть)","МАЛЫЕ ТИХООКЕАНСКИЕ ОТДАЛЕННЫЕ ОСТРОВА СОЕДИНЕННЫХ ШТАТОВ","МИКРОНЕЗИЯ, ФЕДЕРАТИВНЫЕ ШТАТЫ","МАРШАЛЛОВЫ ОСТРОВА","КЮРАСАО",
                "ПАЛАУ","ПАКИСТАН","ПАНАМА","ПАПУА-НОВАЯ ГВИНЕЯ","ПАРАГВАЙ","ПЕРУ","ФИЛИППИНЫ","ПИТКЕРН","ПОЛЬША","ПОРТУГАЛИЯ","ГВИНЕЯ-БИСАУ","ТИМОР-ЛЕСТЕ","ШВЕЦИЯ","ШВЕЙЦАРИЯ","НИДЕРЛАНДЫ",
                "ПУЭРТО-РИКО","КАТАР","РЕЮНЬОН","РУМЫНИЯ","РОССИЯ","РУАНДА","СЕН-БАРТЕЛЕМИ","СВЯТАЯ ЕЛЕНА, ОСТРОВ ВОЗНЕСЕНИЯ, ТРИСТАН-ДА-КУНЬЯ","СЕНТ-КИТС И НЕВИС","АНГИЛЬЯ","СЕНТ-ЛЮСИЯ",
                "СЕН-МАРТЕН (французская часть)","СЕН-ПЬЕР И МИКЕЛОН","СЕНТ-ВИНСЕНТ И ГРЕНАДИНЫ","САН-МАРИНО","САН-ТОМЕ И ПРИНСИПИ","САУДОВСКАЯ АРАВИЯ","СЕНЕГАЛ","СЕРБИЯ","СЕЙШЕЛЫ","ЮЖНЫЙ СУДАН",
                "СЬЕРРА-ЛЕОНЕ","СИНГАПУР","СЛОВАКИЯ","ВЬЕТНАМ","СЛОВЕНИЯ","СОМАЛИ","ЮЖНАЯ АФРИКА","ЗИМБАБВЕ","ИСПАНИЯ","ЗАПАДНАЯ САХАРА","СУДАН","СУРИНАМ","ШПИЦБЕРГЕН И ЯН МАЙЕН","ЭСВАТИНИ",
                "СИРИЙСКАЯ АРАБСКАЯ РЕСПУБЛИКА","ТАДЖИКИСТАН","ТАИЛАНД","ТОГО","ТОКЕЛАУ","ТОНГА","ТРИНИДАД И ТОБАГО","ОБЪЕДИНЕННЫЕ АРАБСКИЕ ЭМИРАТЫ","ТУНИС","ТУРЦИЯ","ТУРКМЕНИСТАН","ОСТРОВА ТЕРКС И КАЙКОС",
                "ТУВАЛУ","УГАНДА","УКРАИНА","СЕВЕРНАЯ МАКЕДОНИЯ","ЕГИПЕТ","СОЕДИНЕННОЕ КОРОЛЕВСТВО","ГЕРНСИ","ДЖЕРСИ","ОСТРОВ МЭН","ТАНЗАНИЯ, ОБЪЕДИНЕННАЯ РЕСПУБЛИКА","СОЕДИНЕННЫЕ ШТАТЫ",
                "ВИРГИНСКИЕ ОСТРОВА (США)","БУРКИНА-ФАСО","УРУГВАЙ","УЗБЕКИСТАН","ВЕНЕСУЭЛА (БОЛИВАРИАНСКАЯ РЕСПУБЛИКА)","УОЛЛИС И ФУТУНА","САМОА","ЙЕМЕН","ЗАМБИЯ","АБХАЗИЯ","ЮЖНАЯ ОСЕТИЯ"
            };

        ////ProviderOrRecieverOKPONote property
        //public RamAccess<string> ProviderOrRecieverOKPONote
        //{
        //    get
        //    {

        //        {
        //            var tmp = new RamAccess<string>(ProviderOrRecieverOKPONote_Validation, _DB);//OK

        //        }

        //        {

        //        }
        //    }
        //    set
        //    {


        //        {
        //            ProviderOrRecieverOKPONote_DB = value.Value;
        //        }
        //        OnPropertyChanged(nameof(ProviderOrRecieverOKPONote));
        //    }
        //}


        //private bool ProviderOrRecieverOKPONote_Validation(RamAccess<string> value)
        //{
        //    value.ClearErrors(); return true;
        //}
        ////ProviderOrRecieverOKPONote property
    }
}
