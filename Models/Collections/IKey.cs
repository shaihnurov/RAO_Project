﻿using System.ComponentModel;
using Models.Collections;

namespace Collections
{
    public interface IKey:INotifyPropertyChanged,IExcel
    {
        int Id { get; set; }
    }
}
