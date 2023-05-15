﻿using Avalonia.Controls;
using MessageBox.Avalonia.DTO;
using Models.Collections;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client_App.Commands.AsyncCommands.ExcelExport;
using Client_App.ViewModels;
using Client_App.Resources;

namespace Client_App.Commands.AsyncCommands.Excel;

//  Excel -> Разрывы и пересечения
public class ExcelExportIntersectionsAsyncCommand : ExcelBaseAsyncCommand
{
    public override async Task AsyncExecute(object? parameter)
    {
        var cts = new CancellationTokenSource();
        ExportType = "Разрывы и пересечения";
        var findRep = 0;

        #region ReportsCountCheck

        foreach (var key in MainWindowVM.LocalReports.Reports_Collection)
        {
            var reps = (Reports)key;
            foreach (var key1 in reps.Report_Collection)
            {
                var rep = (Report)key1;
                if (rep.FormNum_DB.Split('.')[0] == "1")
                {
                    findRep++;
                }
            }
        }
        if (findRep == 0)
        {
            #region MessageRepsNotFound

            await MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.Ok,
                    ContentTitle = "Выгрузка в Excel",
                    ContentHeader = "Уведомление",
                    ContentMessage =
                        "Не удалось совершить выгрузку списка разрывов и пересечений дат," +
                        $"{Environment.NewLine}поскольку в текущей базе отсутствуют отчеты по форме 1",
                    MinWidth = 400,
                    MinHeight = 150,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                })
                .ShowDialog(Desktop.MainWindow);

            #endregion

            return;
        }

        #endregion
        
        var fileName = $"{ExportType}_{BaseVM.DbFileName}_{BaseVM.Version}";
        (string fullPath, bool openTemp) result;
        try
        {
            result = await ExcelGetFullPath(fileName, cts);
        }
        catch
        {
            return;
        }
        finally
        {
            cts.Dispose();
        }
        var fullPath = result.fullPath;
        var openTemp = result.openTemp;
        if (string.IsNullOrEmpty(fullPath)) return;

        using ExcelPackage excelPackage = new(new FileInfo(fullPath));
        excelPackage.Workbook.Properties.Author = "RAO_APP";
        excelPackage.Workbook.Properties.Title = "Report";
        excelPackage.Workbook.Properties.Created = DateTime.Now;
        if (MainWindowVM.LocalReports.Reports_Collection.Count == 0) return;

        Worksheet = excelPackage.Workbook.Worksheets.Add("Разрывы и пересечения");

        #region Headers

        Worksheet.Cells[1, 1].Value = "Рег.№";
        Worksheet.Cells[1, 2].Value = "ОКПО";
        Worksheet.Cells[1, 3].Value = "Сокращенное наименование";
        Worksheet.Cells[1, 4].Value = "Форма";
        Worksheet.Cells[1, 5].Value = "Начало прошлого периода";
        Worksheet.Cells[1, 6].Value = "Конец прошлого периода";
        Worksheet.Cells[1, 7].Value = "Начало периода";
        Worksheet.Cells[1, 8].Value = "Конец периода";
        Worksheet.Cells[1, 9].Value = "Зона разрыва";
        Worksheet.Cells[1, 10].Value = "Вид несоответствия"; 

        #endregion

        if (OperatingSystem.IsWindows()) Worksheet.Column(3).AutoFit();   // Под Astra Linux эта команда крашит программу без GDI дров

        var listSortRep = new List<ReportForSort>();
        foreach (var key in MainWindowVM.LocalReports.Reports_Collection)
        {
            var item = (Reports)key;
            if (item.Master_DB.FormNum_DB.Split('.')[0] != "1") continue;
            foreach (var key1 in item.Report_Collection)
            {
                var rep = (Report)key1;
                var start = StaticStringMethods.StringReverse(rep.StartPeriod_DB);
                var end = StaticStringMethods.StringReverse(rep.EndPeriod_DB);
                if (!long.TryParse(start, out var startL) || !long.TryParse(end, out var endL)) continue;
                listSortRep.Add(new ReportForSort
                {
                    RegNoRep = item.Master_DB.RegNoRep.Value ?? "",
                    OkpoRep = item.Master_DB.OkpoRep.Value ?? "",
                    FormNum = rep.FormNum_DB,
                    StartPeriod = startL,
                    EndPeriod = endL,
                    ShortYr = item.Master_DB.ShortJurLicoRep.Value
                });
            }
        }

        var newGen = listSortRep
            .GroupBy(x => x.RegNoRep)
            .ToDictionary(gr => gr.Key, gr => gr
                .ToList()
                .GroupBy(x => x.FormNum)
                .ToDictionary(gr => gr.Key, gr => gr
                    .ToList()
                    .OrderBy(elem => elem.EndPeriod)));
        var row = 2;
        foreach (var grp in newGen)
        {
            foreach (var gr in grp.Value)
            {
                var prevEnd = gr.Value.FirstOrDefault()?.EndPeriod;
                var prevStart = gr.Value.FirstOrDefault()?.StartPeriod;
                var newGr = gr.Value.Skip(1).ToList();
                foreach (var g in newGr)
                {
                    if (g.StartPeriod != prevEnd && g.StartPeriod != prevStart && g.EndPeriod != prevEnd)
                    {
                        if (g.StartPeriod < prevEnd)
                        {
                            var prevEndN = prevEnd.ToString()?.Length == 8
                                ? prevEnd.ToString()
                                : prevEnd == 0
                                    ? "нет даты конца периода"
                                    : prevEnd.ToString()?.Insert(6, "0");
                            var prevStartN = prevStart.ToString().Length == 8
                                ? prevStart.ToString()
                                : prevStart == 0
                                    ? "нет даты начала периода"
                                    : prevStart.ToString()?.Insert(6, "0");
                            var stPer = g.StartPeriod.ToString().Length == 8
                                ? g.StartPeriod.ToString()
                                : g.StartPeriod.ToString().Insert(6, "0");
                            var endPer = g.EndPeriod.ToString().Length == 8
                                ? g.EndPeriod.ToString()
                                : g.EndPeriod.ToString().Insert(6, "0");
                            Worksheet.Cells[row, 1].Value = g.RegNoRep;
                            Worksheet.Cells[row, 2].Value = g.OkpoRep;
                            Worksheet.Cells[row, 3].Value = g.ShortYr;
                            Worksheet.Cells[row, 4].Value = g.FormNum;
                            Worksheet.Cells[row, 5].Value = prevStartN.Equals("нет даты начала периода")
                                ? prevStartN
                                : $"{prevStartN[6..8]}.{prevStartN[4..6]}.{prevStartN[..4]}";
                            Worksheet.Cells[row, 6].Value = prevEndN.Equals("нет даты конца периода")
                                ? prevEndN
                                : $"{prevEndN[6..8]}.{prevEndN[4..6]}.{prevEndN[..4]}";
                            Worksheet.Cells[row, 7].Value = $"{stPer[6..8]}.{stPer[4..6]}.{stPer[..4]}";
                            Worksheet.Cells[row, 8].Value = $"{endPer[6..8]}.{endPer[4..6]}.{endPer[..4]}";
                            Worksheet.Cells[row, 9].Value =
                                $"{Worksheet.Cells[row, 7].Value}-{Worksheet.Cells[row, 6].Value}";
                            Worksheet.Cells[row, 10].Value = "пересечение";
                            row++;
                        }
                        else
                        {
                            var prevEndN = prevEnd?.ToString().Length == 8
                                ? prevEnd.ToString()
                                : prevEnd == 0
                                    ? "нет даты конца периода"
                                    : prevEnd?.ToString().Insert(6, "0");
                            var prevStartN = prevStart?.ToString().Length == 8
                                ? prevStart.ToString()
                                : prevStart == 0
                                    ? "нет даты начала периода"
                                    : prevStart?.ToString().Insert(6, "0");
                            var stPer = g.StartPeriod.ToString().Length == 8
                                ? g.StartPeriod.ToString()
                                : g.StartPeriod.ToString().Insert(6, "0");
                            var endPer = g.EndPeriod.ToString().Length == 8
                                ? g.EndPeriod.ToString()
                                : g.EndPeriod.ToString().Insert(6, "0");
                            Worksheet.Cells[row, 1].Value = g.RegNoRep;
                            Worksheet.Cells[row, 2].Value = g.OkpoRep;
                            Worksheet.Cells[row, 3].Value = g.ShortYr;
                            Worksheet.Cells[row, 4].Value = g.FormNum;
                            Worksheet.Cells[row, 5].Value = prevStartN.Equals("нет даты начала периода")
                                ? prevStartN
                                : $"{prevStartN[6..8]}.{prevStartN[4..6]}.{prevStartN[..4]}";
                            Worksheet.Cells[row, 6].Value = prevEndN.Equals("нет даты конца периода")
                                ? prevEndN
                                : $"{prevEndN[6..8]}.{prevEndN[4..6]}.{prevEndN[..4]}";
                            Worksheet.Cells[row, 7].Value = $"{stPer[6..8]}.{stPer[4..6]}.{stPer[..4]}";
                            Worksheet.Cells[row, 8].Value =
                                $"{endPer[6..8]}.{endPer[4..6]}.{endPer[..4]}";
                            Worksheet.Cells[row, 9].Value =
                                $"{Worksheet.Cells[row, 6].Value}-{Worksheet.Cells[row, 7].Value}";
                            Worksheet.Cells[row, 10].Value = "разрыв";
                            row++;
                        }
                    }

                    prevEnd = g.EndPeriod;
                    prevStart = g.StartPeriod;
                }
            }
        }

        for (var col = 1; col <= Worksheet.Dimension.End.Column; col++)
        {
            if (Worksheet.Cells[1, col].Value is "Сокращенное наименование") continue;

            if (OperatingSystem.IsWindows()) Worksheet.Column(col).AutoFit();
        }

        Worksheet.View.FreezePanes(2, 1);
        
        await ExcelSaveAndOpen(excelPackage, fullPath, openTemp);
    }
}