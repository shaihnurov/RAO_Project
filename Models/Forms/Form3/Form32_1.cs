﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DBRealization;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("")]
    public class Form32_1: Abstracts.Form3
    {
        //public static string SQLCommandParams()
        //{
        //    return
        //        Abstracts.Form3.SQLCommandParamsBase() +
        //    nameof(CertificateId) + SQLconsts.strNotNullDeclaration +
        //    nameof(NuclearMaterialPresence) + SQLconsts.strNotNullDeclaration +
        //    nameof(Kategory) + SQLconsts.shortNotNullDeclaration +
        //    nameof(ActivityOnCreation) + SQLconsts.strNotNullDeclaration +
        //    nameof(ValidThru) + SQLconsts.dateNotNullDeclaration +
        //    nameof(PassportNumber) + SQLconsts.strNotNullDeclaration +
        //    nameof(PassportNumberNote) + SQLconsts.strNotNullDeclaration +
        //    nameof(Type) + SQLconsts.strNotNullDeclaration +
        //    nameof(TypeRecoded) + SQLconsts.strNotNullDeclaration +
        //    nameof(Radionuclids) + SQLconsts.strNotNullDeclaration +
        //    nameof(FactoryNumber) + SQLconsts.strNotNullDeclaration +
        //    nameof(FactoryNumberRecoded) + SQLconsts.strNotNullDeclaration +
        //    nameof(CreationDate) + SQLconsts.dateNotNullDeclaration +
        //    nameof(CreatorOKPO) + SQLconsts.strNotNullDeclaration +
        //    nameof(CreatorOKPONote) + " varchar(255) not null";
        //}
        public Form32_1(IDataAccess Access) : base(Access)
        {
            FormNum = "32_1";
            NumberOfFields = 15;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //PassportNumber property
        [Attributes.Form_Property("Номер паспорта")]
        public string PassportNumber
        {
            get
            {
                if (GetErrors(nameof(PassportNumber)) != null)
                {
                    return (string)_dataAccess.Get(nameof(PassportNumber));
                }
                else
                {
                    return _PassportNumber_Not_Valid;
                }
            }
            set
            {
                _PassportNumber_Not_Valid = value;
                if (GetErrors(nameof(PassportNumber)) != null)
                {
                    _dataAccess.Set(nameof(PassportNumber), _PassportNumber_Not_Valid);
                }
                OnPropertyChanged(nameof(PassportNumber));
            }
        }
        
        private string _PassportNumber_Not_Valid = "";
        private void PassportNumber_Validation()
        {
            ClearErrors(nameof(PassportNumber));
        }
        //PassportNumber property

        //PassportNumberNote property
        public string PassportNumberNote
        {
            get
            {
                if (GetErrors(nameof(PassportNumberNote)) != null)
                {
                    return (string)_dataAccess.Get(nameof(PassportNumberNote));
                }
                else
                {
                    return _PassportNumberNote_Not_Valid;
                }
            }
            set
            {
                _PassportNumberNote_Not_Valid = value;
                if (GetErrors(nameof(PassportNumberNote)) != null)
                {
                    _dataAccess.Set(nameof(PassportNumberNote), _PassportNumberNote_Not_Valid);
                }
                OnPropertyChanged(nameof(PassportNumberNote));
            }
        }
        
        private string _PassportNumberNote_Not_Valid = "";
        private void PassportNumberNote_Validation()
        {
            ClearErrors(nameof(PassportNumberNote));
        }
        //PassportNumberNote property

        //CreatorOKPO property
        [Attributes.Form_Property("ОКПО изготовителя")]
        public string CreatorOKPO
        {
            get
            {
                if (GetErrors(nameof(CreatorOKPO)) != null)
                {
                    return (string)_dataAccess.Get(nameof(CreatorOKPO));
                }
                else
                {
                    return _CreatorOKPO_Not_Valid;
                }
            }
            set
            {
                _CreatorOKPO_Not_Valid = value;
                if (GetErrors(nameof(CreatorOKPO)) != null)
                {
                    _dataAccess.Set(nameof(CreatorOKPO), _CreatorOKPO_Not_Valid);
                }
                OnPropertyChanged(nameof(CreatorOKPO));
            }
        }
          //If change this change validation
        private string _CreatorOKPO_Not_Valid = "";
        private void CreatorOKPO_Validation(string value)//TODO
        {
            ClearErrors(nameof(CreatorOKPO));
            if (value.Equals("прим.")) return;
            var mask1 = new Regex("[А-Яа-я]*");
            if (mask1.IsMatch(value)) return;
            if ((value.Length != 8) && (value.Length != 14))
                AddError(nameof(CreatorOKPO), "Недопустимое значение");
            else
            {
                var mask = new Regex("[0123456789_]*");
                if (!mask.IsMatch(value))
                    AddError(nameof(CreatorOKPO), "Недопустимое значение");
            }
        }
        //CreatorOKPO property

        //Type property
        [Attributes.Form_Property("Тип")]
        public string Type
        {
            get
            {
                if (GetErrors(nameof(Type)) != null)
                {
                    return (string)_dataAccess.Get(nameof(Type));                }
                else
                {
                    return _Type_Not_Valid;
                }
            }
            set
            {
                _Type_Not_Valid = value;
                if (GetErrors(nameof(Type)) != null)
                {
                    _dataAccess.Set(nameof(Type), _Type_Not_Valid);                }
                OnPropertyChanged(nameof(Type));
            }
        }
        
        private string _Type_Not_Valid = "";
        private void Type_Validation()
        {
            ClearErrors(nameof(Type));
        }
        //Type property

        //TypeRecoded property
        public string TypeRecoded
        {
            get
            {
                if (GetErrors(nameof(TypeRecoded)) != null)
                {
                    return (string)_dataAccess.Get(nameof(TypeRecoded));                }
                else
                {
                    return _TypeRecoded_Not_Valid;
                }
            }
            set
            {
                _TypeRecoded_Not_Valid = value;
                if (GetErrors(nameof(TypeRecoded)) != null)
                {
                    _dataAccess.Set(nameof(TypeRecoded), _TypeRecoded_Not_Valid);                }
                OnPropertyChanged(nameof(TypeRecoded));
            }
        }
        
        private string _TypeRecoded_Not_Valid = "";
        private void TypeRecoded_Validation()
        {
            ClearErrors(nameof(TypeRecoded));
        }
        //TypeRecoded property

        //Radionuclids property
        [Attributes.Form_Property("Радионуклиды")]
        public string Radionuclids
        {
            get
            {
                if (GetErrors(nameof(Radionuclids)) != null)
                {
                    return (string)_dataAccess.Get(nameof(Radionuclids));                }
                else
                {
                    return _Radionuclids_Not_Valid;
                }
            }
            set
            {
                _Radionuclids_Not_Valid = value;
                if (GetErrors(nameof(Radionuclids)) != null)
                {
                    _dataAccess.Set(nameof(Radionuclids), _Radionuclids_Not_Valid);                }
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

        //FactoryNumber property
        [Attributes.Form_Property("Заводской номер")]
        public string FactoryNumber
        {
            get
            {
                if (GetErrors(nameof(FactoryNumber)) != null)
                {
                    return (string)_dataAccess.Get(nameof(FactoryNumber));
                }
                else
                {
                    return _FactoryNumber_Not_Valid;
                }
            }
            set
            {
                _FactoryNumber_Not_Valid = value;
                if (GetErrors(nameof(FactoryNumber)) != null)
                {
                    _dataAccess.Set(nameof(FactoryNumber), _FactoryNumber_Not_Valid);
                }
                OnPropertyChanged(nameof(FactoryNumber));
            }
        }
        
        private string _FactoryNumber_Not_Valid = "";
        private void FactoryNumber_Validation()
        {
            ClearErrors(nameof(FactoryNumber));
        }
        //FactoryNumber property

        //FactoryNumberRecoded property
        public string FactoryNumberRecoded
        {
            get
            {
                if (GetErrors(nameof(FactoryNumberRecoded)) != null)
                {
                    return (string)_dataAccess.Get(nameof(FactoryNumberRecoded));
                }
                else
                {
                    return _FactoryNumberRecoded_Not_Valid;
                }
            }
            set
            {
                _FactoryNumberRecoded_Not_Valid = value;
                if (GetErrors(nameof(FactoryNumberRecoded)) != null)
                {
                    _dataAccess.Set(nameof(FactoryNumberRecoded), _FactoryNumberRecoded_Not_Valid);
                }
                OnPropertyChanged(nameof(FactoryNumberRecoded));
            }
        }
        //If change this change validation
        private string _FactoryNumberRecoded_Not_Valid = "";
        private void FactoryNumberRecoded_Validation(string value)//Ready
        {
            ClearErrors(nameof(FactoryNumberRecoded));
        }
        //FactoryNumberRecoded property

        //ActivityOnCreation property
        [Attributes.Form_Property("Активность на дату создания, Бк")]
        public string ActivityOnCreation
        {
            get
            {
                if (GetErrors(nameof(ActivityOnCreation)) != null)
                {
                    return (string)_dataAccess.Get(nameof(ActivityOnCreation));
                }
                else
                {
                    return _ActivityOnCreation_Not_Valid;
                }
            }
            set
            {
                _ActivityOnCreation_Not_Valid = value;
                if (GetErrors(nameof(ActivityOnCreation)) != null)
                {
                    _dataAccess.Set(nameof(ActivityOnCreation), _ActivityOnCreation_Not_Valid);
                }
                OnPropertyChanged(nameof(ActivityOnCreation));
            }
        }
        
        private string _ActivityOnCreation_Not_Valid = "";
        private void ActivityOnCreation_Validation(string value)//Ready
        {
            ClearErrors(nameof(ActivityOnCreation));
            var styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands |
               NumberStyles.AllowExponent;
            try
            {
                if (!(double.Parse(value, styles, CultureInfo.CreateSpecificCulture("en-GB")) > 0))
                    AddError(nameof(ActivityOnCreation), "Число должно быть больше нуля");
            }
            catch
            {
                AddError(nameof(ActivityOnCreation), "Недопустимое значение");
            }
        }
        //ActivityOnCreation property

        //CreationDate property
        [Attributes.Form_Property("Дата изготовления")]
        public DateTimeOffset CreationDate
        {
            get
            {
                if (GetErrors(nameof(CreationDate)) != null)
                {
                    return (DateTime)_dataAccess.Get(nameof(CreationDate));
                }
                else
                {
                    return _CreationDate_Not_Valid;
                }
            }
            set
            {
                _CreationDate_Not_Valid = value;
                if (GetErrors(nameof(CreationDate)) != null)
                {
                    _dataAccess.Set(nameof(CreationDate), _CreationDate_Not_Valid);
                }
                OnPropertyChanged(nameof(CreationDate));
            }
        }
        //If change this change validation
        private DateTimeOffset _CreationDate_Not_Valid = DateTimeOffset.MinValue;
        private void CreationDate_Validation(DateTimeOffset value)//Ready
        {
            ClearErrors(nameof(CreationDate));
        }
        //CreationDate property

        //CreatorOKPONote property
        public string CreatorOKPONote
        {
            get
            {
                if (GetErrors(nameof(CreatorOKPONote)) != null)
                {
                    return (string)_dataAccess.Get(nameof(CreatorOKPONote));
                }
                else
                {
                    return _CreatorOKPONote_Not_Valid;
                }
            }
            set
            {
                _CreatorOKPONote_Not_Valid = value;
                if (GetErrors(nameof(CreatorOKPONote)) != null)
                {
                    _dataAccess.Set(nameof(CreatorOKPONote), _CreatorOKPONote_Not_Valid);
                }
                OnPropertyChanged(nameof(CreatorOKPONote));
            }
        }
        
        private string _CreatorOKPONote_Not_Valid = "";
        private void CreatorOKPONote_Validation()
        {
            ClearErrors(nameof(CreatorOKPONote));
        }
        //CreatorOKPONote property

        //Kategory property
        [Attributes.Form_Property("Категория")]
        public short Kategory
        {
            get
            {
                if (GetErrors(nameof(Kategory)) != null)
                {
                    return (short)_dataAccess.Get(nameof(Kategory));
                }
                else
                {
                    return _Kategory_Not_Valid;
                }
            }
            set
            {
                _Kategory_Not_Valid = value;
                if (GetErrors(nameof(Kategory)) != null)
                {
                    _dataAccess.Set(nameof(Kategory), _Kategory_Not_Valid);
                }
                OnPropertyChanged(nameof(Kategory));
            }
        }
        
        private short _Kategory_Not_Valid = -1;
        private void Kategory_Validation(short value)//TODO
        {
            ClearErrors(nameof(Kategory));
        }
        //Kategory property

        //NuclearMaterialPresence property
        [Attributes.Form_Property("Содержание ядерных материалов")]
        public double NuclearMaterialPresence
        {
            get
            {
                if (GetErrors(nameof(NuclearMaterialPresence)) != null)
                {
                    return (double)_dataAccess.Get(nameof(NuclearMaterialPresence));
                }
                else
                {
                    return _NuclearMaterialPresence_Not_Valid;
                }
            }
            set
            {
                _NuclearMaterialPresence_Not_Valid = value;
                if (GetErrors(nameof(NuclearMaterialPresence)) != null)
                {
                    _dataAccess.Set(nameof(NuclearMaterialPresence), _NuclearMaterialPresence_Not_Valid);
                }
                OnPropertyChanged(nameof(NuclearMaterialPresence));
            }
        }
        
        private double _NuclearMaterialPresence_Not_Valid = -1;
        //NuclearMaterialPresence property

        //CertificateId property
        [Attributes.Form_Property("Номер сертификата")]
        public string CertificateId
        {
            get
            {
                if (GetErrors(nameof(CertificateId)) != null)
                {
                    return (string)_dataAccess.Get(nameof(CertificateId));
                }
                else
                {
                    return _CertificateId_Not_Valid;
                }
            }
            set
            {
                _CertificateId_Not_Valid = value;
                if (GetErrors(nameof(CertificateId)) != null)
                {
                    _dataAccess.Set(nameof(CertificateId), _CertificateId_Not_Valid);
                }
                OnPropertyChanged(nameof(CertificateId));
            }
        }
        
        private string _CertificateId_Not_Valid = "";
        //CertificateId property

        //ValidThru property
        [Attributes.Form_Property("Действует по")]
        public DateTimeOffset ValidThru
        {
            get
            {
                if (GetErrors(nameof(ValidThru)) != null)
                {
                    return (DateTime)_dataAccess.Get(nameof(ValidThru));
                }
                else
                {
                    return _ValidThru_Not_Valid;
                }
            }
            set
            {
                _ValidThru_Not_Valid = value;
                if (GetErrors(nameof(ValidThru)) != null)
                {
                    _dataAccess.Set(nameof(ValidThru), _ValidThru_Not_Valid);
                }
                OnPropertyChanged(nameof(ValidThru));
            }
        }
        
        private DateTimeOffset _ValidThru_Not_Valid = DateTimeOffset.MinValue;
        //ValidThru property
    }
}
