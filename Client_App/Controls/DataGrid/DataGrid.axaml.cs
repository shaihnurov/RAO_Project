using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Collections;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Avalonia.Collections;
using System.ComponentModel;
using Avalonia.Input;
using Avalonia.Media;
using System;
using Models.Collections;

namespace Client_App.Controls.DataGrid
{
    public enum ChooseMode
    {
        Cell=0,
        Line
    }
    public enum MultilineMode
    {
        Multi=0,
        Single
    }
    public class DataGrid : UserControl
    {
        public static readonly DirectProperty<DataGrid, IEnumerable<IChanged>> ItemsProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, IEnumerable<IChanged>>(
                nameof(Items),
                o => o.Items,
                (o, v) => o.Items = v, defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

        private IEnumerable<IChanged> _items = new ObservableCollectionWithItemPropertyChanged<IChanged>();

        public IEnumerable<IChanged> Items
        {
            get { return _items; }
            set
            {
                if (value != null)
                {
                    if (!InterTwoCollections(value))
                    {
                        SetAndRaise(ItemsProperty, ref _items, value);
                    }
                }
            }
        }

        public static readonly DirectProperty<DataGrid, IEnumerable<IChanged>> SelectedItemsProperty =
                AvaloniaProperty.RegisterDirect<DataGrid, IEnumerable<IChanged>>(
                    nameof(SelectedItems),
                    o => o.SelectedItems,
                    (o, v) => o.SelectedItems = v);

        private IEnumerable<IChanged> _selecteditems = new ObservableCollectionWithItemPropertyChanged<IChanged>();
        public IEnumerable<IChanged> SelectedItems
        {
            get
            {
                return _selecteditems;
            }
            set
            {
                if (value != null)
                {
                    if (!InterTwoCollections(value))
                    {
                        SetAndRaise(SelectedItemsProperty, ref _selecteditems, value);
                    }
                }
            }
        }

        bool InterTwoCollections(IEnumerable<IChanged> one)
        {
            if (one != null)
            {
                foreach (var item in one)
                {
                    if (item.IsChanged)
                    {
                        foreach (var it in one)
                        {
                            it.IsChanged = false;
                        }
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        public static readonly DirectProperty<DataGrid, string> TypeProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, string>(
                nameof(Type),
                o => o.Type,
                (o, v) => o.Type = v);
        private string _type = "";
        public string Type
        {
            get { return _type; }
            set
            {
                SetAndRaise(TypeProperty, ref _type, value);
                MakeHeader();
            }
        }

        public static readonly StyledProperty<ChooseMode> ChooseModeProperty =
            AvaloniaProperty.Register<DataGrid, ChooseMode>(nameof(ChooseMode));

        public ChooseMode ChooseMode
        {
            get { return GetValue(ChooseModeProperty); }
            set { SetValue(ChooseModeProperty, value); }
        }

        public static readonly StyledProperty<MultilineMode> MultilineModeProperty =
            AvaloniaProperty.Register<DataGrid, MultilineMode>(nameof(MultilineMode));

        public MultilineMode MultilineMode
        {
            get { return GetValue(MultilineModeProperty); }
            set { SetValue(MultilineModeProperty, value); }
        }

        public static readonly StyledProperty<Brush> ChooseColorProperty =
            AvaloniaProperty.Register<DataGrid, Brush>(nameof(ChooseColor));

        public Brush ChooseColor
        {
            get { return GetValue(ChooseColorProperty); }
            set { SetValue(ChooseColorProperty, value); }
        }


        public Panel Columns { get; set; }
        RowCollection Rows { get; set; }

        public DataGrid()
        {
            InitializeComponent();

            ItemsProperty.Changed.Subscribe(new ItemsObserver(ItemsChanged));
        }
        List<Control> SelectedCells = new List<Control>();
        void SetSelectedControls()
        {
            if(ChooseMode==ChooseMode.Cell)
            {
                if(MultilineMode==MultilineMode.Multi)
                {
                    SetSelectedControls_CellMulti();
                }
                if (MultilineMode == MultilineMode.Single)
                {
                    SetSelectedControls_CellSingle();
                }
            }
            if (ChooseMode == ChooseMode.Line)
            {
                if (MultilineMode == MultilineMode.Multi)
                {
                    SetSelectedControls_LineMulti();
                }
                if (MultilineMode == MultilineMode.Single)
                {
                    SetSelectedControls_LineSingle();
                }
            }
        }
        void SetSelectedControls_LineSingle()
        {
            var Row = FirstPressedItem[0];
            var sel = SelectedCells.ToArray();
            foreach (Row item in sel)
            {
                if (item.SRow != Row)
                {
                    item.Background = this.Background ;
                    SelectedCells.Remove(item);
                }
            }
            if (!SelectedCells.Contains(Rows[Row].SCells))
            {
                Rows[Row].SCells.Background = ChooseColor;
                SelectedCells.Add(Rows[Row].SCells);
            }
        }
        void SetSelectedControls_CellSingle()
        {
            var Row = FirstPressedItem[0];
            var Column = FirstPressedItem[1];
            var sel = SelectedCells.ToArray();
            foreach (Cell item in sel)
            {
                if (item.CellRow != Row && item.CellColumn != Column)
                {
                    item.Background = this.Background;
                    SelectedCells.Remove(item);
                }
            }
            if (!SelectedCells.Contains(Rows[Row, Column]))
            {
                Rows[Row, Column].Background = ChooseColor;
                SelectedCells.Add(Rows[Row, Column]);
            }
        }
        void SetSelectedControls_LineMulti()
        {
            var minRow = Math.Min(FirstPressedItem[0], LastPressedItem[0]);
            var maxRow = Math.Max(FirstPressedItem[0], LastPressedItem[0]);
            var sel = SelectedCells.ToArray();
            foreach (Row item in sel)
            {
                if (!(item.SRow >= minRow && item.SRow <= maxRow))
                {
                    item.Background = this.Background ;
                    SelectedCells.Remove(item);
                }
            }
            for (int i = minRow; i <= maxRow; i++)
            {
                if (!SelectedCells.Contains(Rows[i].SCells))
                {
                    Rows[i].SCells.Background = ChooseColor;
                    SelectedCells.Add(Rows[i].SCells);
                }
            }
        }
        void SetSelectedControls_CellMulti()
        {
            var minRow = Math.Min(FirstPressedItem[0], LastPressedItem[0]);
            var maxRow = Math.Max(FirstPressedItem[0], LastPressedItem[0]);
            var minColumn = Math.Min(FirstPressedItem[1], LastPressedItem[1]);
            var maxColumn = Math.Max(FirstPressedItem[1], LastPressedItem[1]);
            var sel = SelectedCells.ToArray();
            foreach(Cell item in sel)
            {
                if(!(item.CellRow>=minRow&&item.CellRow<=maxRow))
                {
                    item.Background = this.Background;
                    SelectedCells.Remove(item);
                }
                if (!(item.CellColumn >= minColumn && item.CellColumn <= maxColumn))
                {
                    item.Background = this.Background;
                    SelectedCells.Remove(item);
                }
            }
            for (int i = minRow; i <= maxRow; i++)
            {
                for (int j = minColumn; j <= maxColumn; j++)
                {
                    if (!SelectedCells.Contains(Rows[i, j]))
                    {
                        Rows[i, j].Background = ChooseColor;
                        SelectedCells.Add(Rows[i, j]);
                    }
                }
            }
        }
        void SetSelectedItems()
        {
            var lst = new ObservableCollectionWithItemPropertyChanged<IChanged>();
            if (FirstPressedItem[0] != 0 && FirstPressedItem[1] != 0)
            {
                if (LastPressedItem[0] != 0 && LastPressedItem[1] != 0)
                {
                    foreach (var item in SelectedCells)
                    {
                        if (item is Cell)
                        {
                            var ch = (Border)((Cell)item).Content;
                            var ch2 = (Panel)ch.Child;
                            var text = (TextBox)ch2.Children[0];
                            lst.Add((IChanged)text.DataContext);
                        }
                        if (item is StackPanel)
                        {
                            var ch = (Cell)((StackPanel)item).Children[0];
                            lst.Add((IChanged)ch.DataContext);
                        }
                    }
                }
            }
            _selecteditems = lst;
        }
        void SetSelectedItemsWithHandler()
        {
            var lst = new ObservableCollectionWithItemPropertyChanged<IChanged>();
            if(FirstPressedItem[0]!=0&&FirstPressedItem[1]!=0)
            {
                if (LastPressedItem[0] != 0 && LastPressedItem[1] != 0)
                {
                    foreach(var item in SelectedCells)
                    {
                        if (item is Cell)
                        {
                            var ch = (Border)((Cell)item).Content;
                            var ch2 = (Panel)ch.Child;
                            var text = (TextBox)ch2.Children[0];
                            lst.Add((IChanged)text.DataContext);
                        }
                        if(item is StackPanel)
                        {
                            var ch = (Cell)((StackPanel)item).Children[0];
                            lst.Add((IChanged)ch.DataContext);
                        }
                    }
                }
            }
            SelectedItems = lst;
        }

        int[] FirstPressedItem { get; set; } = new int[2];
        int[] LastPressedItem { get; set; } = new int[2];

        void CellPropChangeEventHandler(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Down")
            {
                FirstPressedItem[0] = ((Cell)sender).CellRow;
                FirstPressedItem[1] = ((Cell)sender).CellColumn;
                LastPressedItem[0] = ((Cell)sender).CellRow;
                LastPressedItem[1] = ((Cell)sender).CellColumn;
            }
            if (args.PropertyName == "DownMove")
            {
                LastPressedItem[0] = ((Cell)sender).CellRow;
                LastPressedItem[1] = ((Cell)sender).CellColumn;
            }
            if (args.PropertyName == "Up")
            {
                LastPressedItem[0] = ((Cell)sender).CellRow;
                LastPressedItem[1] = ((Cell)sender).CellColumn;
            }
            if (args.PropertyName == "Down"||
                args.PropertyName == "DownMove"||
                args.PropertyName == "Up")
            {
                SetSelectedControls();
                SetSelectedItemsWithHandler();
            }
        }

        void UpdateAllCells()
        {
            Rows.Clear();
            int count = 1;
            foreach (var item in _items)
            {
                var tmp = (Row)Support.RenderDataGridRow.Render.GetControl(Type, count, item);
                Rows.Add(new CellCollection(tmp, CellPropChangeEventHandler), count);
                count++;
            }
        }


        void UpdateCells()
        {

        }
        void ItemsChanged(object sender, PropertyChangedEventArgs args)
        {
            if(Rows.Count>0)
            {
                UpdateCells();
            }
            else
            {
                UpdateAllCells();
            }
            SetSelectedItems();
        }

        public void MakeHeader()
        {
            Columns.Children.Clear();
            Columns.Children.Add(Support.RenderDataGridHeader.Render.GetControl(Type));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Init();
        }
        void Init()
        {

            Border brd = new Border();
            brd.BorderThickness = Thickness.Parse("1");
            brd.BorderBrush = new SolidColorBrush(Color.Parse("Gray"));

            ScrollViewer vwm = new ScrollViewer();
            //vw.SetValue(Grid.RowProperty, 1);
            vwm.Background = new SolidColorBrush(Color.Parse("WhiteSmoke"));
            vwm.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            vwm.HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            vwm.VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Disabled;
            vwm.HorizontalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto;
            brd.Child = vwm;

            Panel p = new Panel();
            p.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            vwm.Content = p;

            Grid grd = new Grid();
            RowDefinition rd = new RowDefinition();
            rd.Height = GridLength.Parse("30");
            grd.RowDefinitions.Add(rd);
            grd.RowDefinitions.Add(new RowDefinition());
            p.Children.Add(grd);

            Panel pnl = new Panel();
            pnl.SetValue(Grid.RowProperty, 0);
            pnl.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            grd.Children.Add(pnl);
            Columns = pnl;

            ScrollViewer vw = new ScrollViewer();
            vw.SetValue(Grid.RowProperty, 1);
            vw.Background = new SolidColorBrush(Color.Parse("WhiteSmoke"));
            vw.HorizontalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Disabled;
            vw.VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto;
            grd.Children.Add(vw);

            StackPanel stck = new StackPanel();
            stck.Margin = Thickness.Parse("0,-1,0,0");
            stck.Spacing = -1;
            stck.Orientation = Avalonia.Layout.Orientation.Vertical;
            stck.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            stck.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
            vw.Content = stck;
            Rows = new RowCollection(stck, CellPropChangeEventHandler);

            this.Content = brd;
        }
    }
}
