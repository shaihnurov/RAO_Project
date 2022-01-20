using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia;
using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;

namespace Client_App.Controls.DataGrid
{
    public class Cell : UserControl
    {

        public Cell()
        {
            InitializeComponent();
        }

        public Cell(Control ctrl)
        {
            this.Control = ctrl;
            InitializeComponent();
        }

        #region BorderColor
        public static readonly DirectProperty<Cell, SolidColorBrush> BorderColorProperty =
                AvaloniaProperty.RegisterDirect<Cell, SolidColorBrush>(
        nameof(BorderColor),
        o => o.BorderColor,
        (o, v) => o.BorderColor = v);

        private SolidColorBrush _BorderColor = null;

        public SolidColorBrush BorderColor
        {
            get => _BorderColor;
            set
            {
                if (value != null)
                {
                    SetAndRaise(BorderColorProperty, ref _BorderColor, value);
                }
            }
        }
        #endregion

        public int Row { get; set; }
        public int Column { get; set; }

        Control _Control = null;
        public Control Control 
        {
            get 
            { 
                return _Control;
            }
            set 
            {
                if(_Control!=value&&value!=null)
                {
                    _Control = value;

                    var t = ((Panel)((Border)Content).Child);
                    t.Children.Add(_Control);

                    if (!(Control is TextBlock))
                    {
                        this.PointerPressed += PointerPressedEventHandler;
                        this.PointerMoved += PointerMovedEventHandler;
                        this.PointerReleased += PointerReleasedEventHandler;
                    }
                }
            } 
        }

        public void PointerPressedEventHandler(object sender,PointerEventArgs args)
        {
            var t = this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
        }
        public void PointerMovedEventHandler(object sender, PointerEventArgs args)
        {
            var t = this.Parent.Parent;
        }
        public void PointerReleasedEventHandler(object sender, PointerEventArgs args)
        {
            var t = this.Parent.Parent;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            if (Control != null)
            {
                var t = ((Panel)((Border)Content).Child);
                t.Children.Add(Control);
            }
        }
    }
}