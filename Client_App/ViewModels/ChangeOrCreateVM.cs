﻿using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Metadata;
using Collections;
using Models;
using ReactiveUI;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Client_App.ViewModels
{
    public class ChangeOrCreateVM : BaseVM, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _FormType;
        public string FormType
        {
            get => _FormType;
            set
            {
                if (_FormType != value)
                {
                    _FormType = value;
                    NotifyPropertyChanged("FormType");
                }
            }
        }

        private string _DBPath = @"C:\Databases\local.raodb";
        public string DBPath
        {
            get => _DBPath;
            set
            {
                if (_DBPath != value)
                {
                    _DBPath = value;
                    NotifyPropertyChanged("DBPath");
                }
            }
        }

        private Report _Storage;
        public Report Storage
        {
            get => _Storage;
            set
            {
                if (_Storage != value)
                {
                    _Storage = value;
                    NotifyPropertyChanged("Storage");
                }
            }
        }

        public ReactiveCommand<Unit, Unit> CheckReport { get; }

        public ReactiveCommand<string, Unit> AddSort { get; }
        public ReactiveCommand<Unit, Unit> AddRow { get; }
        public ReactiveCommand<IList, Unit> DeleteRow { get; }

        public ReactiveCommand<Unit, Unit> PasteRows { get; }

        private DBRealization.DBModel dbm { get; set; }
        public ChangeOrCreateVM(DBRealization.DBModel dbm)
        {
            this.dbm = dbm;
            AddSort = ReactiveCommand.Create<string>(_AddSort);
            AddRow = ReactiveCommand.Create(_AddRow);
            DeleteRow = ReactiveCommand.Create<IList>(_DeleteRow);
            CheckReport = ReactiveCommand.Create(_CheckReport);
            PasteRows = ReactiveCommand.CreateFromTask(_PasteRows);
        }

        private bool _isCanSaveReportEnabled = false;

        private bool IsCanSaveReportEnabled
        {
            get => _isCanSaveReportEnabled;
            set
            {
                if (value == _isCanSaveReportEnabled)
                {
                    return;
                }

                _isCanSaveReportEnabled = value;
                PropertyChanged?
                    .Invoke(this, new PropertyChangedEventArgs(nameof(IsCanSaveReportEnabled)));
            }
        }

        [DependsOn(nameof(IsCanSaveReportEnabled))]
        private bool CanSaveReport(object parameter)
        {
            return _isCanSaveReportEnabled;
        }
        public void SaveReport()
        {
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                foreach (Avalonia.Controls.Window? item in desktop.Windows)
                {
                    if (item is Views.FormChangeOrCreate)
                    {
                        dbm.SaveChanges();
                        item.Close();
                    }
                }
            }

        }

        private void _CheckReport()
        {
            IsCanSaveReportEnabled = true;
        }

        private void _AddRow()
        {
            //if (FormType == "1/0") { var frm = new Form10(); Storage.Rows10.Add(frm); }
            if (FormType == "1/1") { Form11? frm = new Form11(); Storage.Rows11.Add(frm); }
            if (FormType == "1/2") { Form12? frm = new Form12(); Storage.Rows12.Add(frm); }
            //if (FormType == "1/3") { var frm = new Form13(); Storage.Rows13.Add(frm); }
            //if (FormType == "1/4") { var frm = new Form14(); Storage.Rows14.Add(frm); }
            //if (FormType == "1/5") { var frm = new Form15(); Storage.Rows15.Add(frm); }
            //if (FormType == "1/6") { var frm = new Form16(); Storage.Rows16.Add(frm); }
            //if (FormType == "1/7") { var frm = new Form17(); Storage.Rows17.Add(frm); }
            //if (FormType == "1/8") { var frm = new Form18(); Storage.Rows18.Add(frm); }
            //if (FormType == "1/9") { var frm = new Form19(); Storage.Rows19.Add(frm); }
        }

        private void _DeleteRow(IList param)
        {
            List<Models.Abstracts.Form> lst = new List<Models.Abstracts.Form>();
            foreach (object? item in param)
            {
                lst.Add((Models.Abstracts.Form)item);
            }
            foreach (Models.Abstracts.Form? item in lst)
            {
                //Storage.Rows11.Remove(item);
            }
        }

        private void _AddSort(string param)
        {
            //Storage.Filters.SortPath = param;
        }

        private async Task _PasteRows()
        {
            PasteRealization.Excel ex = new PasteRealization.Excel();

            if (Avalonia.Application.Current.Clipboard is Avalonia.Input.Platform.IClipboard clip)
            {
                string? text = await clip.GetTextAsync();
                List<Models.Abstracts.Form>? lt = ex.Convert(text, FormType);
                if (lt != null)
                {
                    foreach (Models.Abstracts.Form? item in lt)
                    {
                        //Storage.Rows.Add(item);
                    }
                }
            }
        }
    }
}
