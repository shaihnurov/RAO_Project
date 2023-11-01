﻿using Models.Collections;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client_App.DBAPIFactory;
using Client_App.ViewModels;

namespace Client_App;

public static class ReportsStorage
{
    private static EssenceMethods.APIFactory<Report> api => new();
    public static CancellationTokenSource _cancellationTokenSource = new();
    public static CancellationToken cancellationToken = _cancellationTokenSource.Token;

    private static DBObservable _localReports = new();
    public static DBObservable LocalReports
    {
        get => _localReports;
        set
        {
            if (_localReports != value && value != null)
            {
                _localReports = value;
            }
        }
    }

    #region GetReport

    public static async Task GetReport(int id, BaseVM? viewModel)
    {
        Report? rep;
        var reps = LocalReports.Reports_Collection
            .FirstOrDefault(reports => reports.Report_Collection
                .Any(report => report.Id == Convert.ToInt32(id)));
        var checkedRep = reps.Report_Collection.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

    }

    #endregion
}