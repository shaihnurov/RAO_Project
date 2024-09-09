﻿using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia;
using Avalonia.ReactiveUI;
using Client_App.ViewModels;
using Avalonia.Controls.ApplicationLifetimes;

namespace Client_App.Views;

public abstract class BaseWindow<T> : ReactiveWindow<BaseVM>
{
    public override async void Show()
    {
        base.Show();
        await Task.Delay(1);
        SetWindowStartupLocationWorkaroundForLinux();
    }

    private void SetWindowStartupLocationWorkaroundForLinux()
    {
        if(OperatingSystem.IsWindows()) return;

        var scale = PlatformImpl?.DesktopScaling ?? 1.0;
        var windowBase = Owner?.PlatformImpl;
        if(windowBase != null) 
        {
            scale = windowBase.DesktopScaling;
        }
        var rect = new PixelRect(PixelPoint.Origin, PixelSize.FromSize(ClientSize, scale));
        if(WindowStartupLocation == WindowStartupLocation.CenterScreen) 
        {
            var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow;
            var screens = mainWindow.Screens;
            var screen = screens.ScreenFromWindow(PlatformImpl ?? mainWindow.PlatformImpl);
            //var screen = Screens.ScreenFromPoint(windowBase?.Position ?? Position);
            if(screen == null) return;
            Position = screen.WorkingArea.CenterRect(rect).Position;
        }
        else 
        {
            if(windowBase == null || WindowStartupLocation != WindowStartupLocation.CenterOwner) return;
            Position = new PixelRect(windowBase.Position, PixelSize.FromSize(windowBase.ClientSize, scale))
                .CenterRect(rect).Position;
        }
    }
}