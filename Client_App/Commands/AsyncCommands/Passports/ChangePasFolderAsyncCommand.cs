﻿using Avalonia.Controls;
using System.Threading.Tasks;
using Client_App.Properties;
using Client_App.ViewModels;

namespace Client_App.Commands.AsyncCommands.Passports;

//  Excel -> Паспорта -> Изменить расположение паспортов по умолчанию
public class ChangePasFolderAsyncCommand : BaseAsyncCommand
{
    public override async Task AsyncExecute(object? parameter)
    {
        OpenFolderDialog openFolderDialog = new() { Directory = Settings.Default.PasFolderDefaultPath };
        BaseVM.PasFolderPath = await openFolderDialog.ShowAsync(Desktop.MainWindow) ?? BaseVM.PasFolderPath;
        Settings.Default.PasFolderDefaultPath = BaseVM.PasFolderPath;
        Settings.Default.Save();
    }
}