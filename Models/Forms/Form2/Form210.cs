﻿using Models.DataAccess;
using System;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.10: Территории, загрязненные радионуклидами")]
    public class Form210 : Abstracts.Form2
    {
        public Form210() : base()
        {
            //FormNum.Value = "210";
            //NumberOfFields.Value = 12;
            Init();
            Validate_all();
        }

        private void Init()
        {
            DataAccess.Init<string>(nameof(IndicatorName), IndicatorName_Validation, null);
            IndicatorName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PlotName), PlotName_Validation, null);
            PlotName.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PlotKadastrNumber), PlotKadastrNumber_Validation, null);
            PlotKadastrNumber.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(PlotCode), PlotCode_Validation, null);
            PlotCode.PropertyChanged += InPropertyChanged;
            DataAccess.Init<int?>(nameof(InfectedArea), InfectedArea_Validation, null);
            InfectedArea.PropertyChanged += InPropertyChanged;
            //2301DataAccess.Init<double>(nameof(AvgGammaRaysDosePower), AvgGammaRaysDosePower_Validation, null);
            AvgGammaRaysDosePower.PropertyChanged += InPropertyChanged;
            //2301DataAccess.Init<double>(nameof(MaxGammaRaysDosePower), MaxGammaRaysDosePower_Validation, null);
            MaxGammaRaysDosePower.PropertyChanged += InPropertyChanged;
            //2301DataAccess.Init<double>(nameof(WasteDensityAlpha), WasteDensityAlpha_Validation, null);
            WasteDensityAlpha.PropertyChanged += InPropertyChanged;
            //2301DataAccess.Init<double>(nameof(WasteDensityBeta), WasteDensityBeta_Validation, null);
            WasteDensityBeta.PropertyChanged += InPropertyChanged;
            DataAccess.Init<string>(nameof(FcpNumber), FcpNumber_Validation, null);
            FcpNumber.PropertyChanged += InPropertyChanged;
        }

        private void Validate_all()
        {
            IndicatorName_Validation(IndicatorName);
            PlotName_Validation(PlotName);
            PlotKadastrNumber_Validation(PlotKadastrNumber);
            PlotCode_Validation(PlotCode);
            InfectedArea_Validation(InfectedArea);
            AvgGammaRaysDosePower_Validation(AvgGammaRaysDosePower);
            MaxGammaRaysDosePower_Validation(MaxGammaRaysDosePower);
            WasteDensityAlpha_Validation(WasteDensityAlpha);
            WasteDensityBeta_Validation(WasteDensityBeta);
            FcpNumber_Validation(FcpNumber);
        }

        [Attributes.Form_Property("Форма")]    
        public override bool Object_Validation()
        {
            return false;
        }

        //IndicatorName property
        [Attributes.Form_Property("Наименование показателя")]public int? IndicatorNameId { get; set; }
        public virtual RamAccess<string> IndicatorName
        {
            get => DataAccess.Get<string>(nameof(IndicatorName));
            set
            {
                DataAccess.Set(nameof(IndicatorName), value);
                OnPropertyChanged(nameof(IndicatorName));
            }
        }

        private bool IndicatorName_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //IndicatorName property

        //PlotName property
        [Attributes.Form_Property("Наименование участка")]public int? PlotNameId { get; set; }
        public virtual RamAccess<string> PlotName
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PlotName));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PlotName), value);
                }
                OnPropertyChanged(nameof(PlotName));
            }
        }

        private bool PlotName_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //PlotName property

        //PlotKadastrNumber property
        [Attributes.Form_Property("Кадастровый номер участка")]public int? PlotKadastrNumberId { get; set; }
        public virtual RamAccess<string> PlotKadastrNumber
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PlotKadastrNumber));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PlotKadastrNumber), value);
                }
                OnPropertyChanged(nameof(PlotKadastrNumber));
            }
        }

        private bool PlotKadastrNumber_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //PlotKadastrNumber property

        //PlotCode property
        [Attributes.Form_Property("Код участка")]public int? PlotCodeId { get; set; }
        public virtual RamAccess<string> PlotCode
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(PlotCode));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(PlotCode), value);
                }
                OnPropertyChanged(nameof(PlotCode));
            }
        }
        //6 symbols code
        private bool PlotCode_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //PlotCode property

        //InfectedArea property
        [Attributes.Form_Property("Площадь загрязненной территории, кв. м")]public int? InfectedAreaId { get; set; }
        public virtual RamAccess<int?> InfectedArea
        {
            get
            {

                {
                    return DataAccess.Get<int?>(nameof(InfectedArea));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(InfectedArea), value);
                }
                OnPropertyChanged(nameof(InfectedArea));
            }
        }

        private bool InfectedArea_Validation(RamAccess<int?> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //InfectedArea property

        //AvgGammaRaysDosePower property
        [Attributes.Form_Property("Средняя мощность дозы гамма-излучения, мкЗв/час")]public int? AvgGammaRaysDosePowerId { get; set; }
        public virtual RamAccess<double> AvgGammaRaysDosePower
        {
            get
            {

                {
                    return DataAccess.Get<double>(nameof(AvgGammaRaysDosePower));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(AvgGammaRaysDosePower), value);
                }
                OnPropertyChanged(nameof(AvgGammaRaysDosePower));
            }
        }

        private bool AvgGammaRaysDosePower_Validation(RamAccess<double> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //AvgGammaRaysDosePower property

        //MaxGammaRaysDosePower property
        [Attributes.Form_Property("Максимальная мощность дозы гамма-излучения, мкЗв/час")]public int? MaxGammaRaysDosePowerId { get; set; }
        public virtual RamAccess<double> MaxGammaRaysDosePower
        {
            get
            {

                {
                    return DataAccess.Get<double>(nameof(MaxGammaRaysDosePower));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(MaxGammaRaysDosePower), value);
                }
                OnPropertyChanged(nameof(MaxGammaRaysDosePower));
            }
        }

        private bool MaxGammaRaysDosePower_Validation(RamAccess<double> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //MaxGammaRaysDosePower property

        //WasteDensityAlpha property
        [Attributes.Form_Property("Средняя плотность загрязнения альфа-излучающими радионуклидами, Бк/кв. м")]public int? WasteDensityAlphaId { get; set; }
        public virtual RamAccess<double> WasteDensityAlpha
        {
            get
            {

                {
                    return DataAccess.Get<double>(nameof(WasteDensityAlpha));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(WasteDensityAlpha), value);
                }
                OnPropertyChanged(nameof(WasteDensityAlpha));
            }
        }

        private bool WasteDensityAlpha_Validation(RamAccess<double> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //WasteDensityAlpha property

        //WasteDensityBeta property
        [Attributes.Form_Property("Средняя плотность загрязнения бета-излучающими радионуклидами, Бк/кв. м")]public int? WasteDensityBetaId { get; set; }
        public virtual RamAccess<double> WasteDensityBeta
        {
            get
            {

                {
                    return DataAccess.Get<double>(nameof(WasteDensityBeta));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(WasteDensityBeta), value);
                }
                OnPropertyChanged(nameof(WasteDensityBeta));
            }
        }

        private bool WasteDensityBeta_Validation(RamAccess<double> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //WasteDensityBeta property

        //FcpNumber property
        [Attributes.Form_Property("Номер мероприятия ФЦП")]public int? FcpNumberId { get; set; }
        public virtual RamAccess<string> FcpNumber
        {
            get
            {

                {
                    return DataAccess.Get<string>(nameof(FcpNumber));
                }

                {

                }
            }
            set
            {


                {
                    DataAccess.Set(nameof(FcpNumber), value);
                }
                OnPropertyChanged(nameof(FcpNumber));
            }
        }

        private bool FcpNumber_Validation(RamAccess<string> value)//TODO
        {
            value.ClearErrors(); return true;
        }
        //FcpNumber property
    }
}
