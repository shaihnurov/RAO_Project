﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Client_App.Commands.SyncCommands.CheckForm;
using Client_App.Interfaces.Logger;
using Client_App.ViewModels;
using DynamicData;
using MessageBox.Avalonia.DTO;
using Microsoft.EntityFrameworkCore;
using Models.CheckForm;
using Models.Collections;
using Models.DBRealization;
using Models.Interfaces;

namespace Client_App.Commands.AsyncCommands.CheckForm;

//  Проверяет форму из главного окна, не открывая её
public class CheckFormFromMainAsyncCommand : BaseAsyncCommand
{
    public override async Task AsyncExecute(object? parameter)
    {
        if (parameter is not IKeyCollection collection) return;
        var par = collection.ToList<Report>().First();
        await using var db = new DBModel(StaticConfiguration.DBPath);

        #region GetReportFromDB

        var rep = await db.ReportCollectionDbSet
        .AsNoTracking()
        .AsQueryable()
        .AsSplitQuery()
        .Include(x => x.Reports).ThenInclude(x => x.Master_DB).ThenInclude(x => x.Rows10)
        //.Include(x => x.Reports).ThenInclude(x => x.Master_DB).ThenInclude(x => x.Rows20)
        .Include(x => x.Rows11)
        .Include(x => x.Rows12)
        .Include(x => x.Rows13)
        .Include(x => x.Rows14)
        .Include(x => x.Rows15)
        .Include(x => x.Rows16)
        .Include(x => x.Rows17)
        .Include(x => x.Rows18)
        //.Include(x => x.Rows19)
        //.Include(x => x.Rows21)
        //.Include(x => x.Rows22)
        //.Include(x => x.Rows23)
        //.Include(x => x.Rows24)
        //.Include(x => x.Rows25)
        //.Include(x => x.Rows26)
        //.Include(x => x.Rows27)
        //.Include(x => x.Rows28)
        //.Include(x => x.Rows29)
        //.Include(x => x.Rows210)
        //.Include(x => x.Rows211)
        //.Include(x => x.Rows212)
        .Include(x => x.Notes)
        .FirstOrDefaultAsync(x => x.Id == par.Id); 
        
        #endregion

        if (rep is null) return;
        List<CheckError> errorList = [];
        try
        {
            errorList.Add(rep.FormNum_DB switch
            {
                "1.1" => CheckF11.Check_Total(rep.Reports, rep),
                "1.2" => CheckF12.Check_Total(rep.Reports, rep),
                "1.3" => CheckF13.Check_Total(rep.Reports, rep),
                "1.4" => CheckF14.Check_Total(rep.Reports, rep),
                "1.5" => CheckF15.Check_Total(rep.Reports, rep),
                "1.6" => CheckF16.Check_Total(rep.Reports, rep),
                "1.7" => CheckF17.Check_Total(rep.Reports, rep),
                //"1.8" => CheckF18.Check_Total(rep.Reports, rep),
                _ => throw new Exception()
            });
        }
        catch (Exception ex)
        {
            var msg = $"{Environment.NewLine}Message: {ex.Message}" + 
                      $"{Environment.NewLine}StackTrace: {ex.StackTrace}";
            ServiceExtension.LoggerManager.Warning(msg);

            #region MessageCheckFailed

            await Dispatcher.UIThread.InvokeAsync(() => MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.Ok,
                    ContentTitle = $"Проверка формы {rep.FormNum_DB}",
                    ContentHeader = "Уведомление",
                    ContentMessage = "В ходе выполнения проверки формы возникла непредвиденная ошибка.",
                    MinWidth = 400,
                    MinHeight = 150,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                })
                .ShowDialog(Desktop.MainWindow));

            #endregion

            return;
        }
        if (errorList.Count == 0)
        {
            #region MessageSourceTransmissionFailed

                await Dispatcher.UIThread.InvokeAsync(() => MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.Ok,
                        ContentTitle = $"Проверка формы {rep.FormNum_DB}",
                        ContentHeader = "Уведомление",
                        ContentMessage = "По результатам проверки формы, ошибок не выявлено.",
                        MinWidth = 400,
                        MinHeight = 150,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    })
                    .ShowDialog(Desktop.MainWindow));

                #endregion
        }
        else
        {
            if (Desktop.Windows.Any(x => x.Name == "FormCheckerWindow"))
            {
                Desktop.Windows.First(x => x.Name == "FormCheckerWindow").Close();
            }
            _ = new Views.CheckForm(new ChangeOrCreateVM(rep.FormNum_DB, rep), errorList);
        }
    }
}