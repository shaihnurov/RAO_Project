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
    [Attributes.Form_Class("Идентификаторы:")]
    public class Form32_3: Abstracts.Form3
    {
        public Form32_3(IDataAccess Access) : base(Access)
        {
            FormNum = "32_3";
            NumberOfFields = 2;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //IdName property
        [Attributes.Form_Property("Идентификатор")]
        public string IdName
        {
            get
            {
                if (GetErrors(nameof(IdName)) == null)
                {
                    return (string)_dataAccess.Get(nameof(IdName))[0][0];
                }
                else
                {
                    return _IdName_Not_Valid;
                }
            }
            set
            {
                _IdName_Not_Valid = value;
                if (GetErrors(nameof(IdName)) == null)
                {
                    _dataAccess.Set(nameof(IdName), _IdName_Not_Valid);
                }
                OnPropertyChanged(nameof(IdName));
            }
        }
        
        private string _IdName_Not_Valid = "";
        //IdName Property

        //Value property
        [Attributes.Form_Property("Значение")]
        public string Value
        {
            get
            {
                if (GetErrors(nameof(Value)) == null)
                {
                    return (string)_dataAccess.Get(nameof(Value))[0][0];
                }
                else
                {
                    return _Value_Not_Valid;
                }
            }
            set
            {
                _Value_Not_Valid = value;
                if (GetErrors(nameof(Value)) == null)
                {
                    _dataAccess.Set(nameof(Value), _Value_Not_Valid);
                }
                OnPropertyChanged(nameof(Value));
            }
        }
        
        private string _Value_Not_Valid = "";
        //Value property
    }
}
