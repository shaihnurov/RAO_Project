﻿using Models.DataAccess;
using System;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Идентификаторы:")]
    public class Form32_3 : Abstracts.Form3
    {
        public Form32_3() : base()
        {
            FormNum.Value = "32_3";
            NumberOfFields.Value = 2;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //IdName property
        [Attributes.Form_Property("Идентификатор")]
        public RamAccess<string> IdName
        {
            get
            {

                {
                    return _dataAccess.Get<string>(nameof(IdName));
                }

                {

                }
            }
            set
            {


                {
                    _dataAccess.Set(nameof(IdName), value);
                }
                OnPropertyChanged(nameof(IdName));
            }
        }


        //IdName Property

        //Val property
        [Attributes.Form_Property("Значение")]
        public RamAccess<string> Val
        {
            get
            {

                {
                    return _dataAccess.Get<string>(nameof(Val));
                }

                {

                }
            }
            set
            {


                {
                    _dataAccess.Set(nameof(Val), value);
                }
                OnPropertyChanged(nameof(Val));
            }
        }


        //Val property
    }
}
