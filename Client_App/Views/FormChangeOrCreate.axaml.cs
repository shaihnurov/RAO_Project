using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Models.Collections;
using System.ComponentModel;
using System.Linq;
using Models.DBRealization;
using Models;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Client_App.Views
{
    public class FormChangeOrCreate : ReactiveWindow<ViewModels.ChangeOrCreateVM>
    {
        private readonly string _param = "";

        public FormChangeOrCreate(string param)
        {
            _param = param;
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
            this.WhenActivated(d => d(ViewModel!.ShowMessage.RegisterHandler(DoShowDialogAsync)));
            this.Closing += OnStandartClosing;
            Init();
        }
        public FormChangeOrCreate()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        System.Reactive.Subjects.AsyncSubject<string> Answ { get; set; } = null;

        protected void OnStandartClosing(object sender, CancelEventArgs args)
        {
            if (Answ == null)
            {
                var tmp = this.DataContext as ViewModels.ChangeOrCreateVM;
                Answ = tmp.ShowMessage.Handle("���������?").GetAwaiter();
                Answ.OnCompleted(() =>
                {
                    this.Close();
                });
                Answ.Subscribe(x =>
                {
                    if (x == "��")
                    {
                        tmp.SaveReport();
                    }
                    if (x == "���")
                    {
                        var dbm = StaticConfiguration.DBModel;
                        dbm.Restore();
                        dbm.LoadTables();
                        dbm.SaveChanges();
                        if (tmp.FormType != "1.0" && tmp.FormType != "2.0")
                        {
                            if (tmp.FormType.Split('.')[0] == "1")
                            {
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.StartPeriod));
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.EndPeriod));
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.CorrectionNumber));
                            }
                            if (tmp.FormType.Split('.')[0] == "2")
                            {
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.Year));
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.CorrectionNumber));
                            }
                        }
                        else
                        {
                            if (tmp.FormType == "1.0")
                            {
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.RegNoRep));
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.ShortJurLicoRep));
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.OkpoRep));
                            }
                            if (tmp.FormType == "2.0")
                            {
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.RegNoRep1));
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.ShortJurLicoRep1));
                                tmp.Storage.OnPropertyChanged(nameof(tmp.Storage.OkpoRep1));
                            }
                        }
                    }
                });

                args.Cancel = true;
            }
        }

        private void Form1Init(in Panel panel)
        {
            if (_param == "1.0")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form10_Visual(this.FindNameScope()));
            }

            if (_param == "1.1")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form11_Visual(this.FindNameScope()));
            }

            if (_param == "1.2")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form12_Visual(this.FindNameScope()));
            }

            if (_param == "1.3")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form13_Visual(this.FindNameScope()));
            }

            if (_param == "1.4")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form14_Visual(this.FindNameScope()));
            }

            if (_param == "1.5")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form15_Visual(this.FindNameScope()));
            }

            if (_param == "1.6")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form16_Visual(this.FindNameScope()));
            }

            if (_param == "1.7")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form17_Visual(this.FindNameScope()));
            }

            if (_param == "1.8")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form18_Visual(this.FindNameScope()));
            }

            if (_param == "1.9")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form19_Visual(this.FindNameScope()));
            }
        }

        private void Form2Init(in Panel panel)
        {
            if (_param == "2.0")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form20_Visual(this.FindNameScope()));
            }
            if (_param == "2.1")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form21_Visual(this.FindNameScope()));
            }
            if (_param == "2.2")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form22_Visual(this.FindNameScope()));
            }
            if (_param == "2.3")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form23_Visual(this.FindNameScope()));
            }
            if (_param == "2.4")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form24_Visual(this.FindNameScope()));
            }
            if (_param == "2.5")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form25_Visual(this.FindNameScope()));
            }
            if (_param == "2.6")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form26_Visual(this.FindNameScope()));
            }
            if (_param == "2.7")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form27_Visual(this.FindNameScope()));
            }
            if (_param == "2.8")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form28_Visual(this.FindNameScope()));
            }
            if (_param == "2.9")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form29_Visual(this.FindNameScope()));
            }
            if (_param == "2.10")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form210_Visual(this.FindNameScope()));
            }
            if (_param == "2.11")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form211_Visual(this.FindNameScope()));
            }
            if (_param == "2.12")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form212_Visual(this.FindNameScope()));
            }
        }

        private void Init()
        {
            Panel? panel = this.FindControl<Panel>("ChangingPanel");
            Form1Init(panel);
            Form2Init(panel);
        }
        private async Task DoShowDialogAsync(InteractionContext<object, int> interaction)
        {
            RowNumber frm = new RowNumber();

            await frm.ShowDialog(this);

            interaction.SetOutput(Convert.ToInt32(frm.Number));
        }
        private async Task DoShowDialogAsync(InteractionContext<string, string> interaction)
        {
            MessageBox.Avalonia.DTO.MessageBoxCustomParams par = new MessageBox.Avalonia.DTO.MessageBoxCustomParams();
            List<MessageBox.Avalonia.Models.ButtonDefinition> lt = new List<MessageBox.Avalonia.Models.ButtonDefinition>();
            lt.Add(new MessageBox.Avalonia.Models.ButtonDefinition
            {
                Type = MessageBox.Avalonia.Enums.ButtonType.Default,
                Name = "��"
            });
            lt.Add(new MessageBox.Avalonia.Models.ButtonDefinition
            {
                Type = MessageBox.Avalonia.Enums.ButtonType.Default,
                Name = "���"
            });
            par.ButtonDefinitions = lt;
            par.ContentTitle = "�����������";
            par.ContentHeader = "�����������";
            par.ContentMessage = interaction.Input;
            var mssg = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxCustomWindow(par);
            var answ = await mssg.ShowDialog(this);

            interaction.SetOutput(answ);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
