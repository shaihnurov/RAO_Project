using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Models.Collections;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Metadata;
using Models;
using ReactiveUI;
using System.Reactive;
using System.Runtime.CompilerServices;
using Avalonia.LogicalTree;
using Client_App.Controls.DataGrid;
using Models.DBRealization;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Models.Abstracts;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Models.DataAccess;

namespace Client_App.Controls.DataGrid
{
    public class DataGridReports : DataGrid<Reports>
    {
        public DataGridReports():base()
        {
            InitializeComponent();

            this.Init();
        }
        public DataGridReports(string Name, bool IsReadable) : base(Name, IsReadable)
        {
            InitializeComponent();

            this.Init();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

}