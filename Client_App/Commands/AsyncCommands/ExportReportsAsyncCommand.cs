﻿using Avalonia.Controls;
using Avalonia.Threading;
using FirebirdSql.Data.FirebirdClient;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;
using Models.Collections;
using Models.DBRealization;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Client_App.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models.Interfaces;

namespace Client_App.Commands.AsyncCommands;

//  Экспорт организации в файл .raodb
internal class ExportReportsAsyncCommand : BaseAsyncCommand
{
    public override async Task AsyncExecute(object? parameter)
    {
        var folderPath = await new OpenFolderDialog().ShowAsync(Desktop.MainWindow);
        if (string.IsNullOrEmpty(folderPath)) return;
        var dt = DateTime.Now;
        Reports exportOrg;
        string fileNameTmp;
        switch (parameter)
        {
            case ObservableCollectionWithItemPropertyChanged<IKey> param:
            {
                foreach (var item in param)
                {
                    ((Reports)item).Master.ExportDate.Value = dt.Date.ToShortDateString();
                }
                fileNameTmp = $"Reports_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}";
                exportOrg = (Reports) param.First();
                await StaticConfiguration.DBModel.SaveChangesAsync();
                break;
            }
            case Reports reps:
                fileNameTmp = $"Reports_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}";
                exportOrg = reps;
                exportOrg.Master.ExportDate.Value = dt.Date.ToShortDateString();
                await StaticConfiguration.DBModel.SaveChangesAsync();
                break;
            default:
                return;
        }
        
        var fullPathTmp = Path.Combine(BaseVM.TmpDirectory, $"{fileNameTmp}_exp.RAODB");
        var filename = $"{BaseVM.RemoveForbiddenChars(exportOrg.Master.RegNoRep.Value)}" +
                       $"_{BaseVM.RemoveForbiddenChars(exportOrg.Master.OkpoRep.Value)}" +
                       $"_{exportOrg.Master.FormNum_DB}" +
                       $"_{BaseVM.Version}";

        var fullPath = Path.Combine(folderPath, $"{filename}.RAODB");

        if (File.Exists(fullPath))
        {
            try
            {
                File.Delete(fullPath);
            }
            catch (Exception)
            {
                #region FailedToSaveFileMessage

                await Dispatcher.UIThread.InvokeAsync(() =>
                    MessageBox.Avalonia.MessageBoxManager
                        .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.Ok,
                            ContentTitle = "Выгрузка",
                            ContentHeader = "Ошибка",
                            ContentMessage =
                                "Не удалось сохранить файл по пути:" +
                                $"{Environment.NewLine}{fullPath}" +
                                $"{Environment.NewLine}" +
                                $"{Environment.NewLine}Файл с таким именем уже существует в этом расположении" +
                                $"{Environment.NewLine}и используется другим процессом.",
                            MinWidth = 400,
                            MinHeight = 150,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        }).ShowDialog(Desktop.MainWindow));

                #endregion
                
                return;
            }
        }
        
        await Task.Run(async () =>
        {
            DBModel db = new(fullPathTmp);
            try
            {
                await db.Database.MigrateAsync();
                await db.ReportsCollectionDbSet.AddAsync(exportOrg);
                await db.SaveChangesAsync();

                var t = db.Database.GetDbConnection() as FbConnection;
                await t.CloseAsync();
                await t.DisposeAsync();

                await db.Database.CloseConnectionAsync();
                await db.DisposeAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            try
            {
                File.Copy(fullPathTmp, fullPath);
                File.Delete(fullPathTmp);
            }
            catch (Exception e)
            {
                #region FailedCopyFromTempMessage

                await Dispatcher.UIThread.InvokeAsync(() =>
                    MessageBox.Avalonia.MessageBoxManager
                        .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.Ok,
                            ContentTitle = "Выгрузка",
                            ContentHeader = "Ошибка",
                            ContentMessage = "При копировании файла базы данных из временной папки возникла ошибка." +
                                             $"{Environment.NewLine}Экспорт не выполнен.",
                            MinWidth = 400,
                            MinHeight = 150,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        }).ShowDialog(Desktop.MainWindow));

                #endregion
            }
        });

        #region ExportDoneMessage

        var answer = await MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxCustomWindow(new MessageBoxCustomParams
            {
                ButtonDefinitions = new[]
                {
                    new ButtonDefinition { Name = "Ок", IsDefault = true },
                    new ButtonDefinition { Name = "Открыть расположение файла" }
                },
                ContentTitle = "Выгрузка",
                ContentHeader = "Уведомление",
                ContentMessage =
                    $"Экспорт завершен. Файл экспорта организации ({exportOrg.Master.FormNum_DB}) сохранен по пути:" +
                    $"{Environment.NewLine}{fullPath}" +
                    $"{Environment.NewLine}" +
                    $"{Environment.NewLine}Регистрационный номер - {exportOrg.Master.RegNoRep.Value}" +
                    $"{Environment.NewLine}ОКПО - {exportOrg.Master.OkpoRep.Value}" +
                    $"{Environment.NewLine}Сокращенное наименование - {exportOrg.Master.ShortJurLicoRep.Value}",
                MinWidth = 400,
                MinHeight = 150,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            }).ShowDialog(Desktop.MainWindow); 

        #endregion

        if (answer is "Открыть расположение файла")
        {
            Process.Start("explorer", folderPath);
        }
    }
}