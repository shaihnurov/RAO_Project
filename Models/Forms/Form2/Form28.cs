﻿using Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.8: Отведение сточных вод, содержащих радионуклиды")]
    public class Form28 : Abstracts.Form2
    {
        public Form28() : base()
        {
            //FormNum.Value = "28";
            //NumberOfFields.Value = 24;
            Init();
            Validate_all();
        }

        private void Init()
        {
            DataAccess.Init<string>(nameof(PermissionNumber2), PermissionNumber2_Validation, null);
            PermissionNumber2.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PermissionIssueDate2), PermissionIssueDate2_Validation, null);
            PermissionIssueDate2.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(ValidBegin2), ValidBegin2_Validation, null);
            ValidBegin2.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(ValidThru2), ValidThru2_Validation, null);
            ValidThru2.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PermissionDocumentName2), PermissionDocumentName2_Validation, null);
            PermissionDocumentName2.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PermissionNumber1), PermissionNumber1_Validation, null);
            PermissionNumber1.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PermissionIssueDate1), PermissionIssueDate1_Validation, null);
            PermissionIssueDate1.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(ValidBegin1), ValidBegin1_Validation, null);
            ValidBegin1.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(ValidThru1), ValidThru1_Validation, null);
            ValidThru1.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PermissionDocumentName1), PermissionDocumentName1_Validation, null);
            PermissionDocumentName1.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PermissionNumber), PermissionNumber_Validation, null);
            PermissionNumber.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PermissionIssueDate), PermissionIssueDate_Validation, null);
            PermissionIssueDate.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(ValidBegin), ValidBegin_Validation, null);
            ValidBegin.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(ValidThru), ValidThru_Validation, null);
            ValidThru.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PermissionDocumentName), PermissionDocumentName_Validation, null);
            PermissionDocumentName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(WasteSourceName), WasteSourceName_Validation, null);
            WasteSourceName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(WasteRecieverName), WasteRecieverName_Validation, null);
            WasteRecieverName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(RecieverTypeCode), RecieverTypeCode_Validation, null);
            RecieverTypeCode.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(AllowedWasteRemovalVolume), AllowedWasteRemovalVolume_Validation, null);
            AllowedWasteRemovalVolume.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(RemovedWasteVolume), RemovedWasteVolume_Validation, null);
            RemovedWasteVolume.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PoolDistrictName), PoolDistrictName_Validation, null);
            PoolDistrictName.PropertyChanged += InPropertyChanged;
        }

        private void Validate_all()
        {
            PermissionNumber2_Validation(PermissionNumber2);
            PermissionIssueDate2_Validation(PermissionIssueDate2);
            ValidBegin2_Validation(ValidBegin2);
            ValidThru2_Validation(ValidThru2);
            PermissionDocumentName2_Validation(PermissionDocumentName2);
            PermissionNumber_Validation(PermissionNumber);
            PermissionIssueDate_Validation(PermissionIssueDate);
            ValidBegin_Validation(ValidBegin);
            ValidThru_Validation(ValidThru);
            PermissionDocumentName_Validation(PermissionDocumentName);
            PermissionNumber1_Validation(PermissionNumber1);
            PermissionIssueDate1_Validation(PermissionIssueDate1);
            ValidBegin1_Validation(ValidBegin1);
            ValidThru1_Validation(ValidThru1);
            PermissionDocumentName1_Validation(PermissionDocumentName1);
            WasteSourceName_Validation(WasteSourceName);
            WasteRecieverName_Validation(WasteRecieverName);
            RecieverTypeCode_Validation(RecieverTypeCode);
            AllowedWasteRemovalVolume_Validation(AllowedWasteRemovalVolume);
            RemovedWasteVolume_Validation(RemovedWasteVolume);
            PoolDistrictName_Validation(PoolDistrictName);
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //PermissionNumber property
        public int? PermissionNumberId { get; set; }
        [Attributes.Form_Property("Номер разрешительного документа")]
        public virtual RamAccess<string> PermissionNumber
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PermissionNumber));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PermissionNumber), value);
                }
                OnPropertyChanged(nameof(PermissionNumber));
            }
        }


        private bool PermissionNumber_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            return true;
        }
        //PermissionNumber property

        //PermissionIssueDate property
        public int? PermissionIssueDateId { get; set; }
        [Attributes.Form_Property("Дата выпуска разрешительного документа")]
        public virtual RamAccess<string> PermissionIssueDate
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PermissionIssueDate));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PermissionIssueDate), value);
                }
                OnPropertyChanged(nameof(PermissionIssueDate));
            }
        }


        private bool PermissionIssueDate_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //PermissionIssueDate property

        //PermissionDocumentName property
        public int? PermissionDocumentNameId { get; set; }
        [Attributes.Form_Property("Наименование разрешительного документа")]
        public virtual RamAccess<string> PermissionDocumentName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PermissionDocumentName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PermissionDocumentName), value);
                }
                OnPropertyChanged(nameof(PermissionDocumentName));
            }
        }


        private bool PermissionDocumentName_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //PermissionDocumentName property

        //ValidBegin property
        public int? ValidBeginId { get; set; }
        [Attributes.Form_Property("Действует с")]
        public virtual RamAccess<string> ValidBegin
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(ValidBegin));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(ValidBegin), value);
                }
                OnPropertyChanged(nameof(ValidBegin));
            }
        }


        private bool ValidBegin_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //ValidBegin property

        //ValidThru property
        public int? ValidThruId { get; set; }
        [Attributes.Form_Property("Действует по")]
        public virtual RamAccess<string> ValidThru
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(ValidThru));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(ValidThru), value);
                }
                OnPropertyChanged(nameof(ValidThru));
            }
        }


        private bool ValidThru_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //ValidThru property

        //PermissionNumber1 property
        public int? PermissionNumber1Id { get; set; }
        [Attributes.Form_Property("Номер разрешительного документа")]
        public virtual RamAccess<string> PermissionNumber1
        {
            get => DataAccess.Get<string>(nameof(PermissionNumber1));
            set
            {
                DataAccess.Set(nameof(PermissionNumber1), value);
                OnPropertyChanged(nameof(PermissionNumber1));
            }
        }


        private bool PermissionNumber1_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //PermissionNumber1 property

        //PermissionIssueDate1 property
        public int? PermissionIssueDate1Id { get; set; }
        [Attributes.Form_Property("Дата выпуска разрешительного документа")]
        public virtual RamAccess<string> PermissionIssueDate1
        {
            get
            {
                {
                    return DataAccess.Get<string>(nameof(PermissionIssueDate1));
                }

                {

                }
            }
            set
            {
                DataAccess.Set(nameof(PermissionIssueDate1), value);
                OnPropertyChanged(nameof(PermissionIssueDate1));
            }
        }


        private bool PermissionIssueDate1_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //PermissionIssueDate1 property

        //PermissionDocumentName1 property
        public int? PermissionDocumentName1Id { get; set; }
        [Attributes.Form_Property("Наименование разрешительного документа")]
        public virtual RamAccess<string> PermissionDocumentName1
        {
            get
            {
                {
                    return DataAccess.Get<string>(nameof(PermissionDocumentName1));
                }

                {

                }
            }
            set
            {
                DataAccess.Set(nameof(PermissionDocumentName1), value);
                OnPropertyChanged(nameof(PermissionDocumentName1));
            }
        }


        private bool PermissionDocumentName1_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //PermissionDocumentName1 property

        //ValidBegin1 property
        public int? ValidBegin1Id { get; set; }
        [Attributes.Form_Property("Действует с")]
        public virtual RamAccess<string> ValidBegin1
        {
            get
            {
                {
                    return DataAccess.Get<string>(nameof(ValidBegin1));
                }

                {

                }
            }
            set
            {
                DataAccess.Set(nameof(ValidBegin1), value);
                OnPropertyChanged(nameof(ValidBegin1));
            }
        }


        private bool ValidBegin1_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //ValidBegin1 property

        //ValidThru1 property
        public int? ValidThru1Id { get; set; }
        [Attributes.Form_Property("Действует по")]
        public virtual RamAccess<string> ValidThru1
        {
            get => DataAccess.Get<string>(nameof(ValidThru1));
            set
            {
                DataAccess.Set(nameof(ValidThru1), value);
                OnPropertyChanged(nameof(ValidThru1));
            }
        }


        private bool ValidThru1_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //ValidThru1 property

        //PermissionNumber2 property
        public int? PermissionNumber2Id { get; set; }
        [Attributes.Form_Property("Номер разрешительного документа")]
        public virtual RamAccess<string> PermissionNumber2
        {
            get => DataAccess.Get<string>(nameof(PermissionNumber2));
            set
            {
                DataAccess.Set(nameof(PermissionNumber2), value);
                OnPropertyChanged(nameof(PermissionNumber2));
            }
        }


        private bool PermissionNumber2_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //PermissionNumber2 property

        //PermissionIssueDate2 property
        public int? PermissionIssueDate2Id { get; set; }
        [Attributes.Form_Property("Дата выпуска разрешительного документа")]
        public virtual RamAccess<string> PermissionIssueDate2
        {
            get
            {
                {
                    return DataAccess.Get<string>(nameof(PermissionIssueDate2));
                }

                {

                }
            }
            set
            {
                DataAccess.Set(nameof(PermissionIssueDate2), value);
                OnPropertyChanged(nameof(PermissionIssueDate2));
            }
        }


        private bool PermissionIssueDate2_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //PermissionIssueDate property

        //PermissionDocumentName2 property
        public int? PermissionDocumentName2Id { get; set; }
        [Attributes.Form_Property("Наименование разрешительного документа")]
        public virtual RamAccess<string> PermissionDocumentName2
        {
            get
            {
                {
                    return DataAccess.Get<string>(nameof(PermissionDocumentName2));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PermissionDocumentName2), value);
                }
                OnPropertyChanged(nameof(PermissionDocumentName2));
            }
        }


        private bool PermissionDocumentName2_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //PermissionDocumentName2 property

        //ValidBegin2 property
        public int? ValidBegin2Id { get; set; }
        [Attributes.Form_Property("Действует с")]
        public virtual RamAccess<string> ValidBegin2
        {
            get => DataAccess.Get<string>(nameof(ValidBegin2));
            set
            {
                DataAccess.Set(nameof(ValidBegin2), value);
                OnPropertyChanged(nameof(ValidBegin2));
            }
        }


        private bool ValidBegin2_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //ValidBegin2 property

        //ValidThru2 property
        public int? ValidThru2Id { get; set; }
        [Attributes.Form_Property("Действует по")]
        public virtual RamAccess<string> ValidThru2
        {
            get
            {
                {
                    return DataAccess.Get<string>(nameof(ValidThru2));
                }

                {

                }
            }
            set
            {
                DataAccess.Set(nameof(ValidThru2), value);
                OnPropertyChanged(nameof(ValidThru2));
            }
        }


        private bool ValidThru2_Validation(RamAccess<string> value)
        {
            value.ClearErrors(); return true;
        }
        //ValidThru2 property

        //WasteSourceName property
        public int? WasteSourceNameId { get; set; }
        [Attributes.Form_Property("Наименование, номер выпуска сточных вод")]
        public virtual RamAccess<string> WasteSourceName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(WasteSourceName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(WasteSourceName), value);
                }
                OnPropertyChanged(nameof(WasteSourceName));
            }
        }


        private bool WasteSourceName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //WasteSourceName property

        //WasteRecieverName property
        public int? WasteRecieverNameId { get; set; }
        [Attributes.Form_Property("Наименование приемника отведенных вод")]
        public virtual RamAccess<string> WasteRecieverName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(WasteRecieverName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(WasteRecieverName), value);
                }
                OnPropertyChanged(nameof(WasteRecieverName));
            }
        }


        private bool WasteRecieverName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            return true;
        }
        //WasteRecieverName property

        //RecieverTypeCode property
        public int? RecieverTypeCodeId { get; set; }
        [Attributes.Form_Property("Код типа приемника отведенных вод")]
        public virtual RamAccess<string> RecieverTypeCode
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(RecieverTypeCode));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(RecieverTypeCode), value);
                }
                OnPropertyChanged(nameof(RecieverTypeCode));
            }
        }


        private bool RecieverTypeCode_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            List<string> spr = new List<string>();
            if (spr.Contains(value.Value))
            {
                return true;
            }
            value.AddError("Недопустимое значение");
            return false;
        }
        //RecieverTypeCode property

        //PoolDistrictName property
        public int? PoolDistrictNameId { get; set; }
        [Attributes.Form_Property("Наименование бассейнового округа приемника отведенных вод")]
        public virtual RamAccess<string> PoolDistrictName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PoolDistrictName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PoolDistrictName), value);
                }
                OnPropertyChanged(nameof(PoolDistrictName));
            }
        }


        private bool PoolDistrictName_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            List<string> spr = new List<string>();
            if (spr.Contains(value.Value))
            {
                return true;
            }
            value.AddError("Недопустимое значение");
            return false;
        }
        //PoolDistrictName property

        //AllowedWasteRemovalVolume property
        public int? AllowedWasteRemovalVolumeId { get; set; }
        [Attributes.Form_Property("Допустимый объем водоотведения за год, тыс. куб. м")]
        public virtual RamAccess<string> AllowedWasteRemovalVolume
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(AllowedWasteRemovalVolume));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(AllowedWasteRemovalVolume), value);
                }
                OnPropertyChanged(nameof(AllowedWasteRemovalVolume));
            }
        }


        private bool AllowedWasteRemovalVolume_Validation(RamAccess<string> value)
        {
            value.ClearErrors();
            if (string.IsNullOrEmpty(value.Value))
            {
                value.AddError("Поле не заполнено");
                return false;
            }
            if (value.Value.Equals("прим."))
            {
                return true;
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
        //AllowedWasteRemovalVolume property

        //RemovedWasteVolume property
        public int? RemovedWasteVolumeId { get; set; }
        [Attributes.Form_Property("Отведено за отчетный период, тыс. куб. м")]
        public virtual RamAccess<string> RemovedWasteVolume
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(RemovedWasteVolume));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(RemovedWasteVolume), value);
                }
                OnPropertyChanged(nameof(RemovedWasteVolume));
            }
        }


        private bool RemovedWasteVolume_Validation(RamAccess<string> value)
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
        //RemovedWasteVolume property

        ////RemovedWasteVolumeNote property
        //public virtual RamAccess<double> RemovedWasteVolumeNote
        //{
        //    get
        //    {

        //        {
        //            return DataAccess.Get<double>(nameof(RemovedWasteVolumeNote));
        //        }

        //        {

        //        }
        //    }
        //    set
        //    {


        //        {
        //            DataAccess.Set(nameof(RemovedWasteVolumeNote), value);
        //        }
        //        OnPropertyChanged(nameof(RemovedWasteVolumeNote));
        //    }
        //}


        //private bool RemovedWasteVolumeNote_Validation(RamAccess<string> value)
        //{
        //    value.ClearErrors(); return true;
        //}
        ////RemovedWasteVolumeNote property
    }
}
