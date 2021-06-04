﻿using Models.DataAccess;
using System;
using System.Text.RegularExpressions;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 3.0: Титульный лист отчета организации-экспортера радиоактивных источников 1 и 2 категории")]
    public class Form30 : Abstracts.Form
    {
        public Form30() : base()
        {
            FormNum.Value = "30";
            NumberOfFields.Value = 19;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //RegNo property
        [Attributes.Form_Property("Рег. №")]
        public RamAccess<string> RegNo
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(RegNo));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(RegNo), value);
                }
                OnPropertyChanged(nameof(RegNo));
            }
        }

                //RegNo property

        //OrganUprav property
        [Attributes.Form_Property("Орган управления")]
        public RamAccess<string> OrganUprav
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(OrganUprav));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(OrganUprav), value);
                }
                OnPropertyChanged(nameof(OrganUprav));
            }
        }

                //OrganUprav property

        //SubjectRF property
        [Attributes.Form_Property("Субъект РФ")]
        public RamAccess<string> SubjectRF
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(SubjectRF));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(SubjectRF), value);
                }
                OnPropertyChanged(nameof(SubjectRF));
            }
        }

                //SubjectRF property

        //JurLico property
        [Attributes.Form_Property("Юр. лицо")]
        public RamAccess<string> JurLico
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(JurLico));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(JurLico), value);
                }
                OnPropertyChanged(nameof(JurLico));
            }
        }

                //JurLico property

        //ShortJurLico property
        [Attributes.Form_Property("Краткое наименование юр. лица")]
        public RamAccess<string> ShortJurLico
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(ShortJurLico));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(ShortJurLico), value);
                }
                OnPropertyChanged(nameof(ShortJurLico));
            }
        }

                //ShortJurLico property

        //JurLicoAddress property
        [Attributes.Form_Property("Адрес юр. лица")]
        public RamAccess<string> JurLicoAddress
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(JurLicoAddress));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(JurLicoAddress), value);
                }
                OnPropertyChanged(nameof(JurLicoAddress));
            }
        }

                //JurLicoAddress property

        //JurLicoFactAddress property
        [Attributes.Form_Property("Фактический адрес юр. лица")]
        public RamAccess<string> JurLicoFactAddress
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(JurLicoFactAddress));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(JurLicoFactAddress), value);
                }
                OnPropertyChanged(nameof(JurLicoFactAddress));
            }
        }

                //JurLicoFactAddress property

        //GradeFIO property
        [Attributes.Form_Property("ФИО, должность")]
        public RamAccess<string> GradeFIO
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(GradeFIO));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(GradeFIO), value);
                }
                OnPropertyChanged(nameof(GradeFIO));
            }
        }

                //GradeFIO property

        //Telephone property
        [Attributes.Form_Property("Телефон")]
        public RamAccess<string> Telephone
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Telephone));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Telephone), value);
                }
                OnPropertyChanged(nameof(Telephone));
            }
        }

                //Telephone property

        //Fax property
        [Attributes.Form_Property("Факс")]
        public RamAccess<string> Fax
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Fax));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Fax), value);
                }
                OnPropertyChanged(nameof(Fax));
            }
        }

                //Fax property

        //Email property
        [Attributes.Form_Property("Эл. почта")]
        public RamAccess<string> Email
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Email));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Email), value);
                }
                OnPropertyChanged(nameof(Email));
            }
        }

                //Email property

        //Okpo property
        [Attributes.Form_Property("ОКПО")]
        public RamAccess<string> Okpo
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Okpo));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Okpo), value);
                }
                OnPropertyChanged(nameof(Okpo));
            }
        }
        private bool Okpo_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if ((value.Value.Length != 8) && (value.Value.Length != 14))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            var mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
            if (!mask.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //Okpo property

        //Okved property
        [Attributes.Form_Property("ОКВЭД")]
        public RamAccess<string> Okved
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Okved));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Okved), value);
                }
                OnPropertyChanged(nameof(Okved));
            }
        }

                private bool Okved_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); if (!ex.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение");
                return false;
            }
            return true;
        }
        //Okved property

        //Okogu property
        [Attributes.Form_Property("ОКОГУ")]
        public RamAccess<string> Okogu
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Okogu));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Okogu), value);
                }
                OnPropertyChanged(nameof(Okogu));
            }
        }

                private bool Okogu_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); if (!ex.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //Okogu property

        //Oktmo property
        [Attributes.Form_Property("ОКТМО")]
        public RamAccess<string> Oktmo
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Oktmo));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Oktmo), value);
                }
                OnPropertyChanged(nameof(Oktmo));
            }
        }

                private bool Oktmo_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); if (!ex.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //Oktmo property

        //Inn property
        [Attributes.Form_Property("ИНН")]
        public RamAccess<string> Inn
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Inn));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Inn), value);
                }
                OnPropertyChanged(nameof(Inn));
            }
        }

                private bool Inn_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); if (!ex.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение");
            }
            return true;
        }
        //Inn property

        //Kpp property
        [Attributes.Form_Property("КПП")]
        public RamAccess<string> Kpp
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Kpp));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Kpp), value);
                }
                OnPropertyChanged(nameof(Kpp));
            }
        }

                private bool Kpp_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); if (!ex.IsMatch(value.Value)){
                value.AddError( "Недопустимое значение"); return false;
            }
            return true;
        }
        //Kpp property

        //Okopf property
        [Attributes.Form_Property("ОКОПФ")]
        public RamAccess<string> Okopf
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Okopf));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Okopf), value);
                }
                OnPropertyChanged(nameof(Okopf));
            }
        }

                private bool Okopf_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); if (!ex.IsMatch(value.Value))
            {
                value.AddError("Недопустимое значение"); return false;
            }
            return true;
        }
        //Okopf property

        //Okfs property
        [Attributes.Form_Property("ОКФС")]
        public RamAccess<string> Okfs
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(Okfs));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(Okfs), value);
                }
                OnPropertyChanged(nameof(Okfs));
            }
        }

                private bool Okfs_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); if (!ex.IsMatch(value.Value)){
                value.AddError( "Недопустимое значение"); return false;
            }
            return true;
        }
        //Okfs property
    }
}
