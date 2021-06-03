﻿using Models.DataAccess;
using System;
using System.Text.RegularExpressions;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 4.1: Перечень организаций, зарегистрированных в СГУК РВ и РАО на региональном уровне")]
    public class Form41 : Abstracts.Form
    {
        public Form41() : base()
        {
            FormNum = "41";
            NumberOfFields = 10;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //NumberInOrder property
        [Attributes.Form_Property("№ п/п")]
        public int NumberInOrder
        {
            get
            {
                if (GetErrors(nameof(NumberInOrder)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(NumberInOrder));
                    return tmp != null ? (int)tmp : -1;
                }
                else
                {
                    return _NumberInOrder_Not_Valid;
                }
            }
            set
            {
                _NumberInOrder_Not_Valid = value;
                if (GetErrors(nameof(NumberInOrder)) == null)
                {
                    _dataAccess.Set(nameof(NumberInOrder), value);
                }
                OnPropertyChanged(nameof(NumberInOrder));
            }
        }

        private int _NumberInOrder_Not_Valid = -1;
        private void NumberInOrder_Validation()
        {
            ClearErrors(nameof(NumberInOrder));
        }
        //NumberInOrder property

        //RegNo property
        [Attributes.Form_Property("Регистрационный номер")]
        public string RegNo
        {
            get
            {
                if (GetErrors(nameof(RegNo)) == null)
                {
                    return (string)_dataAccess.Get(nameof(RegNo));
                }
                else
                {
                    return _RegNo_Not_Valid;
                }
            }
            set
            {
                _RegNo_Not_Valid = value;
                if (GetErrors(nameof(RegNo)) == null)
                {
                    _dataAccess.Set(nameof(RegNo), value);
                }
                OnPropertyChanged(nameof(RegNo));
            }
        }

        private string _RegNo_Not_Valid = "";
        //RegNo property

        //Okpo property
        [Attributes.Form_Property("ОКПО")]
        public string Okpo
        {
            get
            {
                if (GetErrors(nameof(Okpo)) == null)
                {
                    return (string)_dataAccess.Get(nameof(Okpo));
                }
                else
                {
                    return _Okpo_Not_Valid;
                }
            }
            set
            {
                _Okpo_Not_Valid = value;
                if (GetErrors(nameof(Okpo)) == null)
                {
                    _dataAccess.Set(nameof(Okpo), value);
                }
                OnPropertyChanged(nameof(Okpo));
            }
        }
        private string _Okpo_Not_Valid = "";
        private void Okpo_Validation(string value)
        {
            ClearErrors(nameof(Okpo));
            if ((value.Length != 8) && (value.Length != 14))
                AddError(nameof(Okpo), "Недопустимое значение");
            else
            {
                var mask = new Regex("^[0123456789]{8}([0123456789_][0123456789]{5}){0,1}$");
                if (!mask.IsMatch(value))
                    AddError(nameof(Okpo), "Недопустимое значение");
            }
        }
        //Okpo property

        //OrgName property
        [Attributes.Form_Property("Наименование организации")]
        public string OrgName
        {
            get
            {
                if (GetErrors(nameof(OrgName)) == null)
                {
                    return (string)_dataAccess.Get(nameof(OrgName));
                }
                else
                {
                    return _OrgName_Not_Valid;
                }
            }
            set
            {
                _OrgName_Not_Valid = value;
                if (GetErrors(nameof(OrgName)) == null)
                {
                    _dataAccess.Set(nameof(OrgName), value);
                }
                OnPropertyChanged(nameof(OrgName));
            }
        }

        private string _OrgName_Not_Valid = "";
        //OrgName property

        //LicenseInfo property
        [Attributes.Form_Property("Сведения о лицензии")]
        public string LicenseInfo
        {
            get
            {
                if (GetErrors(nameof(LicenseInfo)) == null)
                {
                    return (string)_dataAccess.Get(nameof(LicenseInfo));
                }
                else
                {
                    return _LicenseInfo_Not_Valid;
                }
            }
            set
            {
                _LicenseInfo_Not_Valid = value;
                if (GetErrors(nameof(LicenseInfo)) == null)
                {
                    _dataAccess.Set(nameof(LicenseInfo), value);
                }
                OnPropertyChanged(nameof(LicenseInfo));
            }
        }

        private string _LicenseInfo_Not_Valid = "";
        //LicenseInfo property

        //QuantityOfFormsInv property
        [Attributes.Form_Property("Количество отчетных форм по инвентаризации, шт.")]
        public int QuantityOfFormsInv
        {
            get
            {
                if (GetErrors(nameof(QuantityOfFormsInv)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(QuantityOfFormsInv));//OK
                    return tmp != null ? (int)tmp : -1;
                }
                else
                {
                    return _QuantityOfFormsInv_Not_Valid;
                }
            }
            set
            {
                QuantityOfFormsInv_Validation(value);

                if (GetErrors(nameof(QuantityOfFormsInv)) == null)
                {
                    _dataAccess.Set(nameof(QuantityOfFormsInv), value);
                }
                OnPropertyChanged(nameof(QuantityOfFormsInv));
            }
        }
        // positive int.
        private int _QuantityOfFormsInv_Not_Valid = -1;
        private void QuantityOfFormsInv_Validation(int value)//Ready
        {
            ClearErrors(nameof(QuantityOfFormsInv));
            if (value <= 0)
            {
                AddError(nameof(QuantityOfFormsInv), "Недопустимое значение");
                return;
            }
        }
        //QuantityOfFormsInv property

        //QuantityOfFormsOper property
        [Attributes.Form_Property("Количество форм оперативных отчетов, шт.")]
        public int QuantityOfFormsOper
        {
            get
            {
                if (GetErrors(nameof(QuantityOfFormsOper)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(QuantityOfFormsOper));//OK
                    return tmp != null ? (int)tmp : -1;
                }
                else
                {
                    return _QuantityOfFormsOper_Not_Valid;
                }
            }
            set
            {
                QuantityOfFormsOper_Validation(value);

                if (GetErrors(nameof(QuantityOfFormsOper)) == null)
                {
                    _dataAccess.Set(nameof(QuantityOfFormsOper), value);
                }
                OnPropertyChanged(nameof(QuantityOfFormsOper));
            }
        }
        // positive int.
        private int _QuantityOfFormsOper_Not_Valid = -1;
        private void QuantityOfFormsOper_Validation(int value)//Ready
        {
            ClearErrors(nameof(QuantityOfFormsOper));
            if (value <= 0)
                AddError(nameof(QuantityOfFormsOper), "Недопустимое значение");
        }
        //QuantityOfFormsOper property

        //QuantityOfFormsYear property
        [Attributes.Form_Property("Количество форм годовых отчетов, шт.")]
        public int QuantityOfFormsYear
        {
            get
            {
                if (GetErrors(nameof(QuantityOfFormsYear)) == null)
                {
                    var tmp = _dataAccess.Get(nameof(QuantityOfFormsYear));//OK
                    return tmp != null ? (int)tmp : -1;
                }
                else
                {
                    return _QuantityOfFormsYear_Not_Valid;
                }
            }
            set
            {
                QuantityOfFormsYear_Validation(value);

                if (GetErrors(nameof(QuantityOfFormsYear)) == null)
                {
                    _dataAccess.Set(nameof(QuantityOfFormsYear), value);
                }
                OnPropertyChanged(nameof(QuantityOfFormsYear));
            }
        }
        // positive int.
        private int _QuantityOfFormsYear_Not_Valid = -1;
        private void QuantityOfFormsYear_Validation(int value)//Ready
        {
            ClearErrors(nameof(QuantityOfFormsYear));
            if (value <= 0)
                AddError(nameof(QuantityOfFormsYear), "Недопустимое значение");
        }
        //QuantityOfFormsYear property

        //Notes property
        [Attributes.Form_Property("Примечания")]
        public string Notes
        {
            get
            {
                if (GetErrors(nameof(Notes)) == null)
                {
                    return (string)_dataAccess.Get(nameof(Notes));
                }
                else
                {
                    return _Notes_Not_Valid;
                }
            }
            set
            {
                _Notes_Not_Valid = value;
                if (GetErrors(nameof(Notes)) == null)
                {
                    _dataAccess.Set(nameof(Notes), value);
                }
                OnPropertyChanged(nameof(Notes));
            }
        }

        private string _Notes_Not_Valid = "";
        //Notes property
    }
}