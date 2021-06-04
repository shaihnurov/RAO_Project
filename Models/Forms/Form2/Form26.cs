﻿using Models.DataAccess;
using System;

namespace Models
{
    [Serializable]
    [Attributes.Form_Class("Форма 2.6: Контроль загрязнения подземных вод РВ")]
    public class Form26 : Abstracts.Form2
    {
        public Form26() : base()
        {
            FormNum.Value = "26";
            NumberOfFields.Value = 11;
        }

        [Attributes.Form_Property("Форма")]
        public override bool Object_Validation()
        {
            return false;
        }

        //SourcesQuantity property
        [Attributes.Form_Property("Количество источников, шт.")]
        public RamAccess<int> SourcesQuantity
        {
            get
            {
                
                {
                    return _dataAccess.Get<int>(nameof(SourcesQuantity));
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(SourcesQuantity), value);
                }
                OnPropertyChanged(nameof(SourcesQuantity));
            }
        }
        // positive int.
                private void SourcesQuantity_Validation(IDataAccess<int?> value)//Ready
        {
            value.ClearErrors();
            if (value.Value <= 0)
                value.AddError( "Недопустимое значение");
        }
        //SourcesQuantity property

        //ObservedSourceNumber property
        [Attributes.Form_Property("Номер наблюдательной скважины")]
        public RamAccess<string> ObservedSourceNumber
        {
            get
            {
                    return _dataAccess.Get<string>(nameof(ObservedSourceNumber));
            }
            set
            {
                    _dataAccess.Set(nameof(ObservedSourceNumber), value);
                OnPropertyChanged(nameof(ObservedSourceNumber));
            }
        }
        //If change this change validation
                private void ObservedSourceNumber_Validation(IDataAccess<string> value)//Ready
        {
            value.ClearErrors();
        }
        //ObservedSourceNumber property

        //ControlledAreaName property
        [Attributes.Form_Property("Наименование зоны контроля")]
        public RamAccess<string> ControlledAreaName
        {
            get
            {
                    return _dataAccess.Get<string>(nameof(ControlledAreaName));
            }
            set
            {
                    _dataAccess.Set(nameof(ControlledAreaName), value);
                OnPropertyChanged(nameof(ControlledAreaName));
            }
        }
        //If change this change validation
                private void ControlledAreaName_Validation(IDataAccess<string> value)//Ready
        {
            value.ClearErrors();
        }
        //ControlledAreaName property

        //SupposedWasteSource property
        [Attributes.Form_Property("Предполагаемый источник поступления радиоактивных веществ")]
        public RamAccess<string> SupposedWasteSource
        {
            get
            {
                    return _dataAccess.Get<string>(nameof(SupposedWasteSource));
            }
            set
            {
                    _dataAccess.Set(nameof(SupposedWasteSource), value);
                OnPropertyChanged(nameof(SupposedWasteSource));
            }
        }

                private void SupposedWasteSource_Validation(IDataAccess<string> value)//Ready
        {
            value.ClearErrors();
        }
        //SupposedWasteSource property

        //DistanceToWasteSource property
        [Attributes.Form_Property("Расстояние от источника поступления радиоактивных веществ до наблюдательной скважины, м")]
        public RamAccess<int> DistanceToWasteSource
        {
            get
            {
                    return _dataAccess.Get<int>(nameof(DistanceToWasteSource));
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(DistanceToWasteSource), value);
                }
                OnPropertyChanged(nameof(DistanceToWasteSource));
            }
        }

                private void DistanceToWasteSource_Validation(IDataAccess<int?> value)//Ready
        {
            value.ClearErrors();
        }
        //DistanceToWasteSource property

        //TestDepth property
        [Attributes.Form_Property("Глубина отбора проб, м")]
        public RamAccess<int> TestDepth
        {
            get
            {
                
                {
                    return _dataAccess.Get<int>(nameof(TestDepth));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(TestDepth), value);
                }
                OnPropertyChanged(nameof(TestDepth));
            }
        }

                private void TestDepth_Validation(IDataAccess<int?> value)//Ready
        {
            value.ClearErrors();
        }
        //TestDepth property

        //TestDepthNote property
        public RamAccess<int> TestDepthNote
        {
            get
            {
                
                {
                    return _dataAccess.Get<int>(nameof(TestDepthNote));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(TestDepthNote), value);
                }
                OnPropertyChanged(nameof(TestDepthNote));
            }
        }

                private void TestDepthNote_Validation(IDataAccess<int?> value)//Ready
        {
            value.ClearErrors();
        }
        //TestDepthNote property

        //RadionuclidName property
        [Attributes.Form_Property("Радионуклид")]
        public RamAccess<string> RadionuclidName
        {
            get
            {
                
                {
                    return _dataAccess.Get<string>(nameof(RadionuclidName));
                }
                
                {
                    
                }
            }
            set
            {
                    _dataAccess.Set(nameof(RadionuclidName), value);
                OnPropertyChanged(nameof(RadionuclidName));
            }
        }
        //If change this change validation
                private void RadionuclidName_Validation(IDataAccess<string> value)//TODO
        {
            value.ClearErrors();
        }
        //RadionuclidName property

        //AverageYearConcentration property
        [Attributes.Form_Property("Среднегодовое содержание радионуклида, Бк/кг")]
        public RamAccess<double> AverageYearConcentration
        {
            get
            {
                
                {
                    return _dataAccess.Get<double>(nameof(AverageYearConcentration));
                }
                
                {
                    
                }
            }
            set
            {

                
                {
                    _dataAccess.Set(nameof(AverageYearConcentration), value);
                }
                OnPropertyChanged(nameof(AverageYearConcentration));
            }
        }

                private void AverageYearConcentration_Validation(IDataAccess<double> value)//TODO
        {
            value.ClearErrors();
        }
        //AverageYearConcentration property
    }
}
