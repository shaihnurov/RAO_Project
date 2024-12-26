using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Client_App.ViewModels;
using System;

namespace Client_App.Views;

public partial class RadionuclideSelectionCalculator : UserControl
{
    public RadionuclideSelectionCalculator()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}