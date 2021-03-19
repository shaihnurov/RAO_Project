﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using Avalonia;
using Models.Client_Model;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Data.Converters;
using System.Reflection;
using Avalonia.Collections;
using Avalonia.Markup.Xaml;
using System.Collections;
using Models.Attributes;
using Excel_Export_Import;
using Models.LocalStorage;
using System.IO;

namespace Client_App.ViewModels
{
    public class MainWindowVM : BaseVM, INotifyPropertyChanged
    {
        LocalDictionary _FormModel_Local;
        public LocalDictionary FormModel_Local 
        {
            get
            {
                return _FormModel_Local;
            }
            set
            {
                if(_FormModel_Local!=value)
                {
                    _FormModel_Local = value;
                    NotifyPropertyChanged("FormModel_Local");
                }
            }
        }

        public ReactiveCommand<Unit, Unit> Save_Local { get; }
        public ReactiveCommand<Unit, Unit> Load_Local { get; }

        public ReactiveCommand<Unit, Unit> Save_ToFile { get; }

        public ReactiveCommand<Unit, Unit> OpenSettings { get; }

        public ReactiveCommand<string, Unit> AddSort { get; }

        public ReactiveCommand<string, Unit> ChooseForm { get;}

        public ReactiveCommand<string, Unit> AddForm { get; }
        public ReactiveCommand<Unit, Unit> AddTestForm0 { get; }
        public ReactiveCommand<Unit, Unit> AddTestForm1 { get; }
        public ReactiveCommand<Unit, Unit> AddTestForm2 { get; }
        public ReactiveCommand<string, Unit> ChangeForm { get; }
        public ReactiveCommand<Form, Unit> DeleteForm { get; }
        public ReactiveCommand<Unit, Unit> Excel_Export { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainWindowVM()
        {
            _FormModel_Local = new LocalDictionary();
            FormModel_Local.PropertyChanged += FormModelChanged;

            Save_Local = ReactiveCommand.CreateFromTask(SaveForms);
            Load_Local = ReactiveCommand.CreateFromTask(LoadForms);
            Save_ToFile = ReactiveCommand.Create(_SaveToFile);
            AddSort = ReactiveCommand.Create<string>(_AddSort);
;
            AddForm = ReactiveCommand.CreateFromTask<string>(_AddForm);
            AddTestForm0 = ReactiveCommand.CreateFromTask(_AddTestForm0);
            AddTestForm1 = ReactiveCommand.CreateFromTask(_AddTestForm1);
            AddTestForm2 = ReactiveCommand.CreateFromTask(_AddTestForm2);
            ChangeForm = ReactiveCommand.CreateFromTask<string>(_ChangeForm);
            DeleteForm = ReactiveCommand.Create<Form>(_DeleteForm);

            Excel_Export= ReactiveCommand.CreateFromTask(_Excel_Export);
        }

        void _AddSort(string param)
        {
            var type = param.Split('/')[0];
            var path = param.Split('/')[1];

            if (type.Length == 1)
            {
                foreach(var item in FormModel_Local.Forms)
                {
                    var ty = item.Key.Replace("Forms", "");
                    if (ty[0]==type[0])
                    {
                        if (ty.Count() > 1)
                        {
                            if (ty[1] != '0')
                            {
                                item.Value.Filters.SortPath = path;
                            }
                        }
                    }
                }
            }
            else
            {
                var str = FormModel_Local.Forms[type];
                str.Filters.SortPath = path;
            }
        }

        void _SaveToFile()
        {
            FormModel_Local.Path = "";
            Save_Local.Execute().Subscribe();
        }

        public async Task SaveForms()
        {
            if (FormModel_Local.Path != "" && Directory.Exists(FormModel_Local.Path))
            {
                await FormModel_Local.SaveForms();
            }
            else
            {
                if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    OpenFolderDialog dial = new OpenFolderDialog();
                    var res = await dial.ShowAsync(desktop.MainWindow);
                    FormModel_Local.Path = res;
                    await FormModel_Local.SaveForms();
                }
            }
        }
        public async Task LoadForms()
        {
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                OpenFolderDialog dial = new OpenFolderDialog();
                var res = await dial.ShowAsync(desktop.MainWindow);
                FormModel_Local.Path = res;
                await FormModel_Local.LoadForms();
            }
        }

        async Task _AddForm(string param)
        {
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Views.FormChangeOrCreate frm = new Views.FormChangeOrCreate(FormModel_Local.Forms[param],param);
                await frm.ShowDialog(desktop.MainWindow);
            }
        }

        async Task _AddTestForm0()
        {
            Form10 frm = new Form10();
            frm.RegistrNumber = "test_1";
            FormModel_Local.Forms["10"].Storage.Add(frm);
        }
        async Task _AddTestForm1()
        {
            Form11 frm = new Form11();
            FormModel_Local.Forms["11"].Storage.Add(frm);
        }
        async Task _AddTestForm2()
        {
            Form12 frm = new Form12();
            FormModel_Local.Forms["12"].Storage.Add(frm);
        }

        async Task _ChangeForm(string param)
        {
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Views.FormChangeOrCreate frm = new Views.FormChangeOrCreate(FormModel_Local.Forms[param],param);
                await frm.ShowDialog(desktop.MainWindow);
            }
        }
        void _DeleteForm(Form param)
        {
            if (param != null)
            {
                var forms = FormModel_Local.GetType().GetProperty(param.GetType().Name.Replace("Form", "Forms")).GetValue(FormModel_Local);
                var store = forms.GetType().GetProperty("Storage").GetValue(forms);
                var removemeth = store.GetType().GetMethod("Remove");
                List<object> tp = new List<object>();
                tp.Add(param);
                removemeth.Invoke(store, tp.ToArray());
            }
        }

        async Task _Excel_Export()
        {
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var res = "";
                OpenFolderDialog dial = new OpenFolderDialog();
                res = await dial.ShowAsync(desktop.MainWindow);
                res += "\\export.xlsx";
                //var res = "file.xlsx";

                if (res != "")
                {
                    Export exp = new Export(FormModel_Local.ToList());
                    exp.DoExport(res);
                }
            }
        }

        void FormModelChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("FormModel_Local");
        }
    }
}
