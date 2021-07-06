using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Collections;
using System.ComponentModel;
using DBRealization;

namespace Client_App.Views
{
    public class FormChangeOrCreate : ReactiveWindow<ViewModels.ChangeOrCreateVM>
    {
        private readonly string _param = "";

        public FormChangeOrCreate(string param, Report rep)
        {
            ViewModels.ChangeOrCreateVM? tmp = new ViewModels.ChangeOrCreateVM();
            tmp.DBPath = DBRealization.StaticConfiguration.DBPath;
            tmp.Storage = rep;
            tmp.FormType = param;
            DataContext = tmp;
            _param = param;

            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            Init();
        }

        public FormChangeOrCreate()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var dbm = StaticConfiguration.DBModel;
            dbm.UndoChanges();
            dbm.SaveChanges();

            base.OnClosing(e);
        }

        private void Form1Init(in Panel panel)
        {
            if (_param == "1/0")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form10_Visual(this.FindNameScope()));
            }

            if (_param == "1/1")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form11_Visual(this.FindNameScope()));
            }

            if (_param == "1/2")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form12_Visual(this.FindNameScope()));
            }

            if (_param == "1/3")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form13_Visual(this.FindNameScope()));
            }

            if (_param == "1/4")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form14_Visual(this.FindNameScope()));
            }

            if (_param == "1/5")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form15_Visual(this.FindNameScope()));
            }

            if (_param == "1/6")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form16_Visual(this.FindNameScope()));
            }

            if (_param == "1/7")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form17_Visual(this.FindNameScope()));
            }

            if (_param == "1/8")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form18_Visual(this.FindNameScope()));
            }

            if (_param == "1/9")
            {
                panel.Children.Add(Long_Visual.Form1_Visual.Form19_Visual(this.FindNameScope()));
            }
        }

        private void Form2Init(in Panel panel)
        {
            if (_param == "2/0")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form20_Visual(this.FindNameScope()));
            }
            if (_param == "2/1")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form21_Visual(this.FindNameScope()));
            }
            if (_param == "2/2")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form22_Visual(this.FindNameScope()));
            }
            if (_param == "2/9")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form23_Visual(this.FindNameScope()));
            }
            if (_param == "2/4")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form24_Visual(this.FindNameScope()));
            }
            if (_param == "2/5")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form25_Visual(this.FindNameScope()));
            }
            if (_param == "2/6")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form26_Visual(this.FindNameScope()));
            }
            if (_param == "2/7")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form27_Visual(this.FindNameScope()));
            }
            if (_param == "2/8")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form28_Visual(this.FindNameScope()));
            }
            if (_param == "2/9")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form29_Visual(this.FindNameScope()));
            }
            if (_param == "2/10")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form210_Visual(this.FindNameScope()));
            }
            if (_param == "2/11")
            {
                panel.Children.Add(Long_Visual.Form2_Visual.Form211_Visual(this.FindNameScope()));
            }
            if (_param == "2/12")
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

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
