﻿using Collections.Rows_Collection;
using System;
using System.Text.RegularExpressions;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.12: Суммарные сведения о РВ не в составе ЗРИ")]
    public class Form212 : Abstracts.Form2
    {
        public Form212(IDataAccess Access) : base(Access)
        {
            FormNum = "212";
            NumberOfFields = 8;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //OperationCode property
        [Attributes.Form_Property("Код")]
        public string OperationCode
        {
            get
            {
                if (GetErrors(nameof(OperationCode)) == null)
                {
                    return (string)_dataAccess.Get(nameof(OperationCode))[0][0];
                }
                else
                {
                    return _OperationCode_Not_Valid;
                }
            }
            set
            {
                _OperationCode_Not_Valid = value;
                if (GetErrors(nameof(OperationCode)) == null)
                {
                    _dataAccess.Set(nameof(OperationCode), _OperationCode_Not_Valid);
                }
                OnPropertyChanged(nameof(OperationCode));
            }
        }

        private string _OperationCode_Not_Valid = "-1";
        private void OperationCode_Validation()
        {
            ClearErrors(nameof(OperationCode));
        }
        //OperationCode property

        //ObjectTypeCode property
        [Attributes.Form_Property("Код типа объектов учета")]
        public string ObjectTypeCode
        {
            get
            {
                if (GetErrors(nameof(ObjectTypeCode)) == null)
                {
                    return (string)_dataAccess.Get(nameof(ObjectTypeCode))[0][0];
                }
                else
                {
                    return _ObjectTypeCode_Not_Valid;
                }
            }
            set
            {
                _ObjectTypeCode_Not_Valid = value;
                if (GetErrors(nameof(ObjectTypeCode)) == null)
                {
                    _dataAccess.Set(nameof(ObjectTypeCode), _ObjectTypeCode_Not_Valid);
                }
                OnPropertyChanged(nameof(ObjectTypeCode));
            }
        }
        //2 digit code
        private string _ObjectTypeCode_Not_Valid = ""; //2 digit code
        private void ObjectTypeCode_Validation(string value)//TODO
        {
            ClearErrors(ObjectTypeCode);
        }
        //ObjectTypeCode property

        //Radionuclids property
        [Attributes.Form_Property("Радионуклиды")]
        public string Radionuclids
        {
            get
            {
                if (GetErrors(nameof(Radionuclids)) == null)
                {
                    return (string)_dataAccess.Get(nameof(Radionuclids))[0][0];
                }
                else
                {
                    return _Radionuclids_Not_Valid;
                }
            }
            set
            {
                _Radionuclids_Not_Valid = value;
                if (GetErrors(nameof(Radionuclids)) == null)
                {
                    _dataAccess.Set(nameof(Radionuclids), _Radionuclids_Not_Valid);
                }
                OnPropertyChanged(nameof(Radionuclids));
            }
        }
        //If change this change validation
        private string _Radionuclids_Not_Valid = "";
        private void Radionuclids_Validation()//TODO
        {
            ClearErrors(nameof(Radionuclids));
        }
        //Radionuclids property

        //Activity property
        [Attributes.Form_Property("Активность, Бк")]
        public double Activity
        {
            get
            {
                if (GetErrors(nameof(Activity)) == null)
                {
                    return (double)_dataAccess.Get(nameof(Activity))[0][0];
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

        private double _Activity_Not_Valid = -1;
        private void Activity_Validation(double value)//Ready
        {
            ClearErrors(nameof(Activity));
            if (!(value > 0))
                AddError(nameof(Activity), "Число должно быть больше нуля");
        }
        //Activity property

        //ProviderOrRecieverOKPO property
        [Attributes.Form_Property("ОКПО поставщика/получателя")]
        public string ProviderOrRecieverOKPO
        {
            get
            {
                if (GetErrors(nameof(ProviderOrRecieverOKPO)) == null)
                {
                    return (string)_dataAccess.Get(nameof(ProviderOrRecieverOKPO))[0][0];
                }
                else
                {
                    return _ProviderOrRecieverOKPO_Not_Valid;
                }
            }
            set
            {
                _ProviderOrRecieverOKPO_Not_Valid = value;
                if (GetErrors(nameof(ProviderOrRecieverOKPO)) == null)
                {
                    _dataAccess.Set(nameof(ProviderOrRecieverOKPO), _ProviderOrRecieverOKPO_Not_Valid);
                }
                OnPropertyChanged(nameof(ProviderOrRecieverOKPO));
            }
        }

        private string _ProviderOrRecieverOKPO_Not_Valid = "";
        private void ProviderOrRecieverOKPO_Validation(string value)//TODO
        {
            ClearErrors(nameof(ProviderOrRecieverOKPO));
            if (value.Equals("Минобороны") || value.Equals("прим.")) return;
            if ((value.Length != 8) && (value.Length != 14))
                AddError(nameof(ProviderOrRecieverOKPO), "Недопустимое значение");
            else
            {
                var mask = new Regex("[0123456789_]*");
                if (!mask.IsMatch(value))
                    AddError(nameof(ProviderOrRecieverOKPO), "Недопустимое значение");
            }
        }
        //ProviderOrRecieverOKPO property

        //ProviderOrRecieverOKPONote property
        public string ProviderOrRecieverOKPONote
        {
            get
            {
                if (GetErrors(nameof(ProviderOrRecieverOKPONote)) == null)
                {
                    return (string)_dataAccess.Get(nameof(ProviderOrRecieverOKPONote))[0][0];
                }
                else
                {
                    return _ProviderOrRecieverOKPONote_Not_Valid;
                }
            }
            set
            {
                _ProviderOrRecieverOKPONote_Not_Valid = value;
                if (GetErrors(nameof(ProviderOrRecieverOKPONote)) == null)
                {
                    _dataAccess.Set(nameof(ProviderOrRecieverOKPONote), _ProviderOrRecieverOKPONote_Not_Valid);
                }
                OnPropertyChanged(nameof(ProviderOrRecieverOKPONote));
            }
        }

        private string _ProviderOrRecieverOKPONote_Not_Valid = "";
        private void ProviderOrRecieverOKPONote_Validation()
        {
            ClearErrors(nameof(ProviderOrRecieverOKPONote));
        }
        //ProviderOrRecieverOKPONote property
    }
}
