﻿using Models.DataAccess;
using System;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 4.0: Титульный лист годового отчета СГУК РВ и РАО")]
    public class Form40 : Abstracts.Form
    {
        public Form40() : base()
        {
            //FormNum.Value = "40";
            //NumberOfFields.Value = 18;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //SubjectRF property
        [Attributes.Form_Property("Субъект РФ")]public int? SubjectRFId { get; set; }
        public virtual RamAccess<string> SubjectRF
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(SubjectRF));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(SubjectRF), value);
                }
                OnPropertyChanged(nameof(SubjectRF));
            }
        }


        //SubjectRF property

        //Yyear property
        [Attributes.Form_Property("Год")]public int? YyearId { get; set; }
        public virtual RamAccess<int> Yyear
        {
            get
            {

                {
                    return DataAccess.Get<int>(nameof(Yyear));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(Yyear), value);
                }
                OnPropertyChanged(nameof(Yyear));
            }
        }


        //Yyear property

        //SubjectAuthorityName property
        [Attributes.Form_Property("Наименование органа исполнительной власти")]public int? SubjectAuthorityNameId { get; set; }
        public virtual RamAccess<int> SubjectAuthorityName
        {
            get
            {

                {
                    return DataAccess.Get<int>(nameof(SubjectAuthorityName));
                }

                {

                }
            }
            set
            {
                DataAccess.Set(nameof(SubjectAuthorityName), value);
                OnPropertyChanged(nameof(SubjectAuthorityName));
            }
        }


        //SubjectAuthorityName property

        //ShortSubjectAuthorityName property
        [Attributes.Form_Property("Краткое наименование органа исполнительной власти")]public int? ShortSubjectAuthorityNameId { get; set; }
        public virtual RamAccess<int> ShortSubjectAuthorityName
        {
            get
            {

                {
                    return DataAccess.Get<int>(nameof(ShortSubjectAuthorityName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(ShortSubjectAuthorityName), value);
                }
                OnPropertyChanged(nameof(ShortSubjectAuthorityName));
            }
        }


        //ShortSubjectAuthorityName property

        //FactAddress property
        [Attributes.Form_Property("Фактический адрес")]public int? FactAddressId { get; set; }
        public virtual RamAccess<string> FactAddress
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(FactAddress));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(FactAddress), value);
                }
                OnPropertyChanged(nameof(FactAddress));
            }
        }


        //FactAddress property

        //GradeFIOchef property
        [Attributes.Form_Property("ФИО, должность руководителя")]public int? GradeFIOchefId { get; set; }
        public virtual RamAccess<string> GradeFIOchef
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(GradeFIOchef));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(GradeFIOchef), value);
                }
                OnPropertyChanged(nameof(GradeFIOchef));
            }
        }


        //GradeFIOchef property

        //GradeFIOresponsibleExecutor property
        [Attributes.Form_Property("ФИО, должность ответственного исполнителя")]public int? GradeFIOresponsibleExecutorId { get; set; }
        public virtual RamAccess<string> GradeFIOresponsibleExecutor
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(GradeFIOresponsibleExecutor));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(GradeFIOresponsibleExecutor), value);
                }
                OnPropertyChanged(nameof(GradeFIOresponsibleExecutor));
            }
        }


        //GradeFIOresponsibleExecutor property

        //Telephone property
        [Attributes.Form_Property("Телефон")]public int? TelephoneId { get; set; }
        public virtual RamAccess<string> Telephone
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(Telephone));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(Telephone), value);
                }
                OnPropertyChanged(nameof(Telephone));
            }
        }


        //Telephone property

        //Fax property
        [Attributes.Form_Property("Факс")]public int? FaxId { get; set; }
        public virtual RamAccess<string> Fax
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(Fax));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(Fax), value);
                }
                OnPropertyChanged(nameof(Fax));
            }
        }


        //Fax property

        //Email property
        [Attributes.Form_Property("Эл. почта")]public int? EmailId { get; set; }
        public virtual RamAccess<string> Email
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(Email));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(Email), value);
                }
                OnPropertyChanged(nameof(Email));
            }
        }


        //Email property

        //Telephone1 property
        [Attributes.Form_Property("Телефон")]public int? Telephone1Id { get; set; }
        public virtual RamAccess<string> Telephone1
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(Telephone1));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(Telephone1), value);
                }
                OnPropertyChanged(nameof(Telephone1));
            }
        }


        //Telephone1 property

        //Fax1 property
        [Attributes.Form_Property("Факс")]public int? Fax1Id { get; set; }
        public virtual RamAccess<string> Fax1
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(Fax1));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(Fax1), value);
                }
                OnPropertyChanged(nameof(Fax1));
            }
        }


        //Fax1 property

        //Email1 property
        [Attributes.Form_Property("Эл. почта")]public int? Email1Id { get; set; }
        public virtual RamAccess<string> Email1
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(Email1));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(Email1), value);
                }
                OnPropertyChanged(nameof(Email1));
            }
        }


        //Email1 property

        //OrgName property
        [Attributes.Form_Property("Название организации")]public int? OrgNameId { get; set; }
        public virtual RamAccess<string> OrgName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(OrgName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(OrgName), value);
                }
                OnPropertyChanged(nameof(OrgName));
            }
        }


        //OrgName property

        //ShortOrgName property
        [Attributes.Form_Property("Краткое название организации")]public int? ShortOrgNameId { get; set; }
        public virtual RamAccess<string> ShortOrgName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(ShortOrgName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(ShortOrgName), value);
                }
                OnPropertyChanged(nameof(ShortOrgName));
            }
        }


        //ShortOrgName property

        //FactAddress1 property
        [Attributes.Form_Property("Фактический адрес")]public int? FactAddress1Id { get; set; }
        public virtual RamAccess<string> FactAddress1
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(FactAddress1));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(FactAddress1), value);
                }
                OnPropertyChanged(nameof(FactAddress1));
            }
        }


        //FactAddress1 property

        //GradeFIOchef1 property
        [Attributes.Form_Property("ФИО, должность руководителя")]public int? GradeFIOchef1Id { get; set; }
        public virtual RamAccess<string> GradeFIOchef1
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(GradeFIOchef1));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(GradeFIOchef1), value);
                }
                OnPropertyChanged(nameof(GradeFIOchef1));
            }
        }


        //GradeFIOchef1 property

        //GradeFIOresponsibleExecutor1 property
        [Attributes.Form_Property("ФИО, должность ответственного исполнителя")]public int? GradeFIOresponsibleExecutor1Id { get; set; }
        public virtual RamAccess<string> GradeFIOresponsibleExecutor1
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(GradeFIOresponsibleExecutor1));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(GradeFIOresponsibleExecutor1), value);
                }
                OnPropertyChanged(nameof(GradeFIOresponsibleExecutor1));
            }
        }


        //GradeFIOresponsibleExecutor1 property
    }
}
