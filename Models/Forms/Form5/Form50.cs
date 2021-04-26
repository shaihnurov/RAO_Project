﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DBRealization;
using Collections.Rows_Collection;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 5.0: Титульный лист годового отчета СГУК РВ и РАО")]
    public class Form50 : Abstracts.Form
    {
        public Form50(IDataAccess Access) : base(Access)
        {
            FormNum = "50";
            NumberOfFields = 11;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }
        public enum Authority
        {
            FederalAuthority,
            CorporationRosatom,
            DepartmentOfDefense,
            None
        }

        //Authority1 property
        [Attributes.Form_Property("ВИАЦ")]
        public Authority Authority1
        {
            get
            {
                if (GetErrors(nameof(Authority1)) == null)
                {
                    return (Authority)_dataAccess.Get(nameof(Authority1))[0][0];
                }
                else
                {
                    return _Authority1_Not_Valid;
                }
            }
            set
            {
                _Authority1_Not_Valid = value;
                if (GetErrors(nameof(Authority1)) == null)
                {
                    _dataAccess.Set(nameof(Authority1), _Authority1_Not_Valid);
                }
                OnPropertyChanged(nameof(Authority1));
            }
        }
        
        private Authority _Authority1_Not_Valid = Authority.None;
        //Authority1 property

        //Yyear property
        [Attributes.Form_Property("Год")]
        public int Yyear
        {
            get
            {
                if (GetErrors(nameof(Yyear)) == null)
                {
                    return (int)_dataAccess.Get(nameof(Yyear))[0][0];
                }
                else
                {
                    return _Yyear_Not_Valid;
                }
            }
            set
            {
                _Yyear_Not_Valid = value;
                if (GetErrors(nameof(Yyear)) == null)
                {
                    _dataAccess.Set(nameof(Yyear), _Yyear_Not_Valid);
                }
                OnPropertyChanged(nameof(Yyear));
            }
        }
        
        private int _Yyear_Not_Valid = -1;
        //Yyear property

        //JurLico property
        [Attributes.Form_Property("Юр. лицо")]
        public string JurLico
        {
            get
            {
                if (GetErrors(nameof(JurLico)) == null)
                {
                    return (string)_dataAccess.Get(nameof(JurLico))[0][0];
                }
                else
                {
                    return _JurLico_Not_Valid;
                }
            }
            set
            {
                _JurLico_Not_Valid = value;
                if (GetErrors(nameof(JurLico)) == null)
                {
                    _dataAccess.Set(nameof(JurLico), _JurLico_Not_Valid);
                }
                OnPropertyChanged(nameof(JurLico));
            }
        }
        
        private string _JurLico_Not_Valid = "";
        //JurLico property

        //ShortJurLico property
        [Attributes.Form_Property("Краткое наименование юр. лица")]
        public string ShortJurLico
        {
            get
            {
                if (GetErrors(nameof(ShortJurLico)) == null)
                {
                    return (string)_dataAccess.Get(nameof(ShortJurLico))[0][0];
                }
                else
                {
                    return _ShortJurLico_Not_Valid;
                }
            }
            set
            {
                _ShortJurLico_Not_Valid = value;
                if (GetErrors(nameof(ShortJurLico)) == null)
                {
                    _dataAccess.Set(nameof(ShortJurLico), _ShortJurLico_Not_Valid);
                }
                OnPropertyChanged(nameof(ShortJurLico));
            }
        }
        
        private string _ShortJurLico_Not_Valid = "";
        //ShortJurLico property

        //JurLicoAddress property
        [Attributes.Form_Property("Адрес юр. лица")]
        public string JurLicoAddress
        {
            get
            {
                if (GetErrors(nameof(JurLicoAddress)) == null)
                {
                    return (string)_dataAccess.Get(nameof(JurLicoAddress))[0][0];
                }
                else
                {
                    return _JurLicoAddress_Not_Valid;
                }
            }
            set
            {
                _JurLicoAddress_Not_Valid = value;
                if (GetErrors(nameof(JurLicoAddress)) == null)
                {
                    _dataAccess.Set(nameof(JurLicoAddress), _JurLicoAddress_Not_Valid);
                }
                OnPropertyChanged(nameof(JurLicoAddress));
            }
        }
        
        private string _JurLicoAddress_Not_Valid = "";
        //JurLicoAddress property

        //JurLicoFactAddress property
        [Attributes.Form_Property("Фактический адрес юр. лица")]
        public string JurLicoFactAddress
        {
            get
            {
                if (GetErrors(nameof(JurLicoFactAddress)) == null)
                {
                    return (string)_dataAccess.Get(nameof(JurLicoFactAddress))[0][0];
                }
                else
                {
                    return _JurLicoFactAddress_Not_Valid;
                }
            }
            set
            {
                _JurLicoFactAddress_Not_Valid = value;
                if (GetErrors(nameof(JurLicoFactAddress)) == null)
                {
                    _dataAccess.Set(nameof(JurLicoFactAddress), _JurLicoFactAddress_Not_Valid);
                }
                OnPropertyChanged(nameof(JurLicoFactAddress));
            }
        }
        
        private string _JurLicoFactAddress_Not_Valid = "";
        //JurLicoFactAddress property

        //GradeFIO property
        [Attributes.Form_Property("ФИО, должность")]
        public string GradeFIO
        {
            get
            {
                if (GetErrors(nameof(GradeFIO)) == null)
                {
                    return (string)_dataAccess.Get(nameof(GradeFIO))[0][0];
                }
                else
                {
                    return _GradeFIO_Not_Valid;
                }
            }
            set
            {
                _GradeFIO_Not_Valid = value;
                if (GetErrors(nameof(GradeFIO)) == null)
                {
                    _dataAccess.Set(nameof(GradeFIO), _GradeFIO_Not_Valid);
                }
                OnPropertyChanged(nameof(GradeFIO));
            }
        }
        
        private string _GradeFIO_Not_Valid = "";
        //GradeFIO property

        //GradeFIOresponsibleExecutor property
        [Attributes.Form_Property("ФИО, должность ответственного исполнителя")]
        public string GradeFIOresponsibleExecutor
        {
            get
            {
                if (GetErrors(nameof(GradeFIOresponsibleExecutor)) == null)
                {
                    return (string)_dataAccess.Get(nameof(GradeFIOresponsibleExecutor))[0][0];
                }
                else
                {
                    return _GradeFIOresponsibleExecutor_Not_Valid;
                }
            }
            set
            {
                _GradeFIOresponsibleExecutor_Not_Valid = value;
                if (GetErrors(nameof(GradeFIOresponsibleExecutor)) == null)
                {
                    _dataAccess.Set(nameof(GradeFIOresponsibleExecutor), _GradeFIOresponsibleExecutor_Not_Valid);
                }
                OnPropertyChanged(nameof(GradeFIOresponsibleExecutor));
            }
        }
        
        private string _GradeFIOresponsibleExecutor_Not_Valid = "";
        //GradeFIOresponsibleExecutor property

        //Telephone property
        [Attributes.Form_Property("Телефон")]
        public string Telephone
        {
            get
            {
                if (GetErrors(nameof(Telephone)) == null)
                {
                    return (string)_dataAccess.Get(nameof(Telephone))[0][0];
                }
                else
                {
                    return _Telephone_Not_Valid;
                }
            }
            set
            {
                _Telephone_Not_Valid = value;
                if (GetErrors(nameof(Telephone)) == null)
                {
                    _dataAccess.Set(nameof(Telephone), _Telephone_Not_Valid);
                }
                OnPropertyChanged(nameof(Telephone));
            }
        }
        
        private string _Telephone_Not_Valid = "";
        //Telephone property

        //Fax property
        [Attributes.Form_Property("Факс")]
        public string Fax
        {
            get
            {
                if (GetErrors(nameof(Fax)) == null)
                {
                    return (string)_dataAccess.Get(nameof(Fax))[0][0];
                }
                else
                {
                    return _Fax_Not_Valid;
                }
            }
            set
            {
                _Fax_Not_Valid = value;
                if (GetErrors(nameof(Fax)) == null)
                {
                    _dataAccess.Set(nameof(Fax), _Fax_Not_Valid);
                }
                OnPropertyChanged(nameof(Fax));
            }
        }
        
        private string _Fax_Not_Valid = "";
        //Fax property

        //Email property
        [Attributes.Form_Property("Эл. почта")]
        public string Email
        {
            get
            {
                if (GetErrors(nameof(Email)) == null)
                {
                    return (string)_dataAccess.Get(nameof(Email))[0][0];
                }
                else
                {
                    return _Email_Not_Valid;
                }
            }
            set
            {
                _Email_Not_Valid = value;
                if (GetErrors(nameof(Email)) == null)
                {
                    _dataAccess.Set(nameof(Email), _Email_Not_Valid);
                }
                OnPropertyChanged(nameof(Email));
            }
        }
        
        private string _Email_Not_Valid = "";
        //Email property
    }
}
