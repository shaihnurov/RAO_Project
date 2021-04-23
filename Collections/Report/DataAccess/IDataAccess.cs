﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Report_Collection
{
    public interface IDataAccess
    {
        string DBPath { get; set; }
        string PathToData { get; set; }

        int ReportsID { get; }
        int ReportID { get; }

        List<object[]> Get(string ParamName);
        void Set(string ParamName, object obj);
    }
}
