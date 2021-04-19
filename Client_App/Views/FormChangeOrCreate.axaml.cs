using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Models;
using Models.Attributes;
using Avalonia.Data;
using Collections;
using DBRealization;

namespace Client_App.Views
{
    public class FormChangeOrCreate : Window
    {
        string _param = "";
        public FormChangeOrCreate(string DBPath,int ReportID,string param)
        {
            var tmp= new ViewModels.ChangeOrCreateVM();
            tmp.FormType = param;
            if(DBPath!=null)
            {
                tmp.DBPath = DBPath;
                if (ReportID != -1)
                {
                    tmp.Storage = new Report(new RedDataBase(DBPath, ReportID));
                }
                else
                {
                    tmp.Storage = new Report(new RedDataBase(DBPath));
                }
            }
            else
            {
                if (ReportID != -1)
                {
                    tmp.Storage = new Report(new RedDataBase(tmp.DBPath, ReportID));
                }
                else
                {
                    tmp.Storage = new Report(new RedDataBase(tmp.DBPath));
                }
            }

            this.DataContext = tmp;
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

        void Form1Init(in Panel panel)
        {
            if (_param == "10")
                panel.Children.Add(Long_Visual.Form1_Visual.Form10_Visual());
            if (_param == "11")
                panel.Children.Add(Long_Visual.Form1_Visual.Form11_Visual());
            if (_param == "12")
                panel.Children.Add(Long_Visual.Form1_Visual.Form12_Visual());
            if (_param == "13")
                panel.Children.Add(Long_Visual.Form1_Visual.Form13_Visual());
            if (_param == "14")
                panel.Children.Add(Long_Visual.Form1_Visual.Form14_Visual());
            if (_param == "15")
                panel.Children.Add(Long_Visual.Form1_Visual.Form15_Visual());
            if (_param == "16")
                panel.Children.Add(Long_Visual.Form1_Visual.Form16_Visual());
            if (_param == "17")
                panel.Children.Add(Long_Visual.Form1_Visual.Form17_Visual());
            if (_param == "18")
                panel.Children.Add(Long_Visual.Form1_Visual.Form18_Visual());
            if (_param == "19")
                panel.Children.Add(Long_Visual.Form1_Visual.Form19_Visual());
        }

        void Init()
        {
            var panel=this.FindControl<Panel>("ChangingPanel");
            Form1Init(panel);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
