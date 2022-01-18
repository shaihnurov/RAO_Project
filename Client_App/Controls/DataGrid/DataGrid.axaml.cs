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
    public enum ChooseMode
    {
        Cell = 0,
        Line
    }

    public enum MultilineMode
    {
        Multi = 0,
        Single
    }

    public class DataGrid : UserControl
    {
        #region DirectProperty AddReports, AddNote, AddRow
        public static readonly DirectProperty<DataGrid, ReactiveCommand<String, Unit>> CtrlACommandProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, ReactiveCommand<String, Unit>>(
         nameof(CtrlACommand),
         o => o.CtrlACommand,
        (o, v) => o.CtrlACommand = v);

        private ReactiveCommand<String, Unit> _CtrlACommand = null;

        public ReactiveCommand<String, Unit> CtrlACommand
        {
            get => _CtrlACommand;
            set
            {
                SetAndRaise(CtrlACommandProperty, ref _CtrlACommand, value);
            }

        }
        #endregion

        #region DirectProperty Export
        public static readonly DirectProperty<DataGrid, ReactiveCommand<ObservableCollectionWithItemPropertyChanged<IKey>, Unit>> CtrlECommandProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, ReactiveCommand<ObservableCollectionWithItemPropertyChanged<IKey>, Unit>>(
            nameof(CtrlECommand),
            o => o.CtrlECommand,
        (o, v) => o.CtrlECommand = v);

        private ReactiveCommand<ObservableCollectionWithItemPropertyChanged<IKey>, Unit> _CtrlECommand = null;

        public ReactiveCommand<ObservableCollectionWithItemPropertyChanged<IKey>, Unit> CtrlECommand
        {
            get => _CtrlECommand;
            set
            {
                SetAndRaise(CtrlECommandProperty, ref _CtrlECommand, value);
            }

        }
        #endregion

        #region DirectProperty DeleteForm, DeleteReport, DeleteNote, DeleteRow
        public static readonly DirectProperty<DataGrid, ReactiveCommand<IEnumerable, Unit>> CtrlDCommandProperty =
    AvaloniaProperty.RegisterDirect<DataGrid, ReactiveCommand<IEnumerable, Unit>>(
         nameof(CtrlDCommand),
         o => o.CtrlDCommand,
        (o, v) => o.CtrlDCommand = v);

        private ReactiveCommand<IEnumerable, Unit> _CtrlDCommand = null;

        public ReactiveCommand<IEnumerable, Unit> CtrlDCommand
        {
            get => _CtrlDCommand;
            set
            {
                SetAndRaise(CtrlDCommandProperty, ref _CtrlDCommand, value);
            }

        }
        #endregion

        #region DirectProperty DuplicateRowsx1, DuplicateNote
        public static readonly DirectProperty<DataGrid, ReactiveCommand<Unit, Unit>> CtrlNCommandProperty =
    AvaloniaProperty.RegisterDirect<DataGrid, ReactiveCommand<Unit, Unit>>(
         nameof(CtrlNCommand),
         o => o.CtrlNCommand,
        (o, v) => o.CtrlNCommand = v);

        private ReactiveCommand<Unit, Unit> _CtrlNCommand = null;

        public ReactiveCommand<Unit, Unit> CtrlNCommand
        {
            get => _CtrlNCommand;
            set
            {
                SetAndRaise(CtrlNCommandProperty, ref _CtrlNCommand, value);
            }

        }
        #endregion

        #region DirectProperty DoubleClick
        public static readonly DirectProperty<DataGrid, ReactiveCommand<ObservableCollectionWithItemPropertyChanged<IKey>, Unit>> DoubleClickCommandProperty =
    AvaloniaProperty.RegisterDirect<DataGrid, ReactiveCommand<ObservableCollectionWithItemPropertyChanged<IKey>, Unit>>(
         nameof(DoubleClickCommand),
         o => o.DoubleClickCommand,
        (o, v) => o.DoubleClickCommand = v);

        private ReactiveCommand<ObservableCollectionWithItemPropertyChanged<IKey>, Unit> _DoubleClickCommand = null;

        public ReactiveCommand<ObservableCollectionWithItemPropertyChanged<IKey>, Unit> DoubleClickCommand
        {
            get => _DoubleClickCommand;
            set
            {
                SetAndRaise(DoubleClickCommandProperty, ref _DoubleClickCommand, value);
            }

        }
        #endregion

        #region AddRowIn
        public static readonly DirectProperty<DataGrid, ReactiveCommand<IEnumerable, Unit>> CtrlICommandProperty =
    AvaloniaProperty.RegisterDirect<DataGrid, ReactiveCommand<IEnumerable, Unit>>(
         nameof(CtrlICommand),
         o => o.CtrlICommand,
        (o, v) => o.CtrlICommand = v);

        private ReactiveCommand<IEnumerable, Unit> _CtrlICommand = null;

        public ReactiveCommand<IEnumerable, Unit> CtrlICommand
        {
            get => _CtrlICommand;
            set
            {
                SetAndRaise(CtrlICommandProperty, ref _CtrlICommand, value);
            }

        }
        #endregion

        #region Items
        public static readonly DirectProperty<DataGrid, IEnumerable<IKey>> ItemsProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, IEnumerable<IKey>>(
                nameof(Items),
                o => o.Items,
                (o, v) => o.Items = v, defaultBindingMode: BindingMode.TwoWay);

        private IEnumerable<IKey> _items =
            new ObservableCollectionWithItemPropertyChanged<IKey>();

        public IEnumerable<IKey> Items
        {
            get => _items;
            set
            {
                if (value != null)
                {
                    SetAndRaise(ItemsProperty, ref _items, value);
                    UpdateCells();
                }
            }
        }

        private void ItemsChanged(object sender, PropertyChangedEventArgs args)
        {
            PageCount = "0";
            ItemsCount = "0";
            if (Items.Count() > 0)
            {
                UpdateCells();
            }
            else
            {
                UpdateAllCells();
            }
        }
        #endregion

        #region ItemsCount
        public static readonly DirectProperty<DataGrid, string> ItemsCountProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, string>(
                nameof(ItemsCount),
                o => o.ItemsCount,
                (o, v) => o.ItemsCount = v);

        private string _ItemsCount = "0";
        public string ItemsCount
        {
            get => Items.Count().ToString();
            set
            {
                SetAndRaise(ItemsCountProperty, ref _ItemsCount, Items.Count().ToString());
            }
        }
        #endregion

        #region SelectedItems
        public static readonly DirectProperty<DataGrid, IEnumerable<IKey>> SelectedItemsProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, IEnumerable<IKey>>(
                nameof(SelectedItems),
                o => o.SelectedItems,
                (o, v) => o.SelectedItems = v);
        private IEnumerable<IKey> _selecteditems =
             new ObservableCollectionWithItemPropertyChanged<IKey>();
        public IEnumerable<IKey> SelectedItems
        {
            get => _selecteditems;
            set
            {
                if (value != null) SetAndRaise(SelectedItemsProperty, ref _selecteditems, value);
            }
        }
        #endregion

        #region SelectedCells
        public static readonly DirectProperty<DataGrid, IList<Control>> SelectedCellsProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, IList<Control>>(
                nameof(SelectedCells),
                o => o.SelectedCells,
                (o, v) => o.SelectedCells = v);

        private IList<Control> _selectedCells =
                new List<Control>();
        public IList<Control> SelectedCells
        {
            get => _selectedCells;
            set
            {
                if (value != null) SetAndRaise(SelectedCellsProperty, ref _selectedCells, value);
            }
        }
        #endregion

        #region Type
        public static readonly DirectProperty<DataGrid, string> TypeProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, string>(
                nameof(Type),
                o => o.Type,
                (o, v) => o.Type = v);
        private string _type = "";
        public string Type
        {
            get => _type;
            set
            {
                SetAndRaise(TypeProperty, ref _type, value);
                MakeHeader();
            }
        }
        #endregion

        #region ChooseMode
        public static readonly StyledProperty<ChooseMode> ChooseModeProperty =
            AvaloniaProperty.Register<DataGrid, ChooseMode>(nameof(ChooseMode));

        public ChooseMode ChooseMode
        {
            get => GetValue(ChooseModeProperty);
            set => SetValue(ChooseModeProperty, value);
        }
        #endregion

        #region MultilineMode
        public static readonly StyledProperty<MultilineMode> MultilineModeProperty =
            AvaloniaProperty.Register<DataGrid, MultilineMode>(nameof(MultilineMode));
        public MultilineMode MultilineMode
        {
            get => GetValue(MultilineModeProperty);
            set => SetValue(MultilineModeProperty, value);
        }
        #endregion

        #region ChooseColor
        public static readonly StyledProperty<Brush> ChooseColorProperty =
            AvaloniaProperty.Register<DataGrid, Brush>(nameof(ChooseColor));
        public Brush ChooseColor
        {
            get => GetValue(ChooseColorProperty);
            set => SetValue(ChooseColorProperty, value);
        }
        #endregion

        #region Pagination
        public static readonly DirectProperty<DataGrid, bool> PaginationProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, bool>(
                nameof(Pagination),
                o => o.Pagination,
                (o, v) => o.Pagination = v);

        private bool _pagination = true;
        public bool Pagination
        {
            get => _pagination;
            set
            {
                SetAndRaise(PaginationProperty, ref _pagination, value);
                UpdateCells();
            }
        }
        #endregion

        #region PageSize
        public static readonly DirectProperty<DataGrid, int> PageSizeProperty =
             AvaloniaProperty.RegisterDirect<DataGrid, int>(
                nameof(PageSize),
                o => o.PageSize,
                (o, v) => o.PageSize = v);

        private int _pageSize = 30;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                SetAndRaise(PageSizeProperty, ref _pageSize, value);
                UpdateCells();
            }
        }
        #endregion

        #region PageCount
        public static readonly DirectProperty<DataGrid, string> PageCountProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, string>(
                nameof(PageCount),
                o => o.PageCount,
                (o, v) => o.PageCount = v);

        private string _PageCount = "0";
        public string PageCount
        {
            get => (Items.Count()/PageSize+1).ToString();
            set
            {
                SetAndRaise(PageCountProperty, ref _PageCount, (Items.Count() / PageSize + 1).ToString());
            }
        }
        #endregion

        #region NowPage
        public static readonly DirectProperty<DataGrid, string> NowPageProperty =
            AvaloniaProperty.RegisterDirect<DataGrid, string>(
                nameof(NowPage),
                o => o.NowPage,
                (o, v) => o.NowPage = v,defaultBindingMode:BindingMode.TwoWay);

        private string _nowPage = "1";
        public string NowPage
        {
            get => _nowPage.ToString();
            set
            {
                try
                {
                    var val = Convert.ToInt32(value);

                    if (val != null)
                    {
                        int maxpage = (Items.Count() / PageSize) + 1;
                        if (val.ToString() != _nowPage)
                        {
                            if (val <= maxpage && val >= 1)
                            {
                                SetAndRaise(NowPageProperty, ref _nowPage, value);
                                UpdateCells();
                            }
                            else
                            {
                                if (val > maxpage)
                                {
                                    if (_nowPage != maxpage.ToString())
                                    {
                                        SetAndRaise(NowPageProperty, ref _nowPage, maxpage.ToString());
                                        UpdateCells();
                                    }
                                }
                                if (val < 1)
                                {
                                    if (_nowPage != "1")
                                    {
                                        SetAndRaise(NowPageProperty, ref _nowPage, "1");
                                        UpdateCells();
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        public void NowPageDown(object sender, RoutedEventArgs args)
        {
            NowPage = (Convert.ToInt32(NowPage) - 1).ToString();
        }
        public void NowPageUp(object sender, RoutedEventArgs args)
        {
            NowPage = (Convert.ToInt32(NowPage) + 1).ToString();
        }
        #endregion

        private RowCollection Columns { get; set; }
        private RowCollection Rows { get; set; }
        private Grid MainGrid { get; set; }

        public DataGrid()
        {
            InitializeComponent();

            ItemsProperty.Changed.Subscribe(new ItemsObserver(ItemsChanged));
            this.DoubleTapped += DataGrid_DoubleTapped;
        }

        #region SetSelectedControls
        private void SetSelectedControls()
        {
            if (ChooseMode == ChooseMode.Cell)
            {
                if (MultilineMode == MultilineMode.Multi) SetSelectedControls_CellMulti();
                if (MultilineMode == MultilineMode.Single) SetSelectedControls_CellSingle();
            }

            if (ChooseMode == ChooseMode.Line)
            {
                if (MultilineMode == MultilineMode.Multi) SetSelectedControls_LineMulti();
                if (MultilineMode == MultilineMode.Single) SetSelectedControls_LineSingle();
            }
        }

        private void SetSelectedControls_LineSingle()
        {
            var Row = LastPressedItem[0];
            var sel = SelectedCells.ToArray();
            foreach (Row item in sel)
                if (item.SRow != Row)
                {
                    var cells = item.Children;
                    foreach (Cell it in cells)
                    {
                        it.Background = Background;
                    }
                }

            SelectedCells.Clear();
            if (!SelectedCells.Contains(Rows[Row] == null ? null : Rows[Row].SCells))
                if (Rows[Row] != null)
                {
                    foreach (var item in Rows[Row].Cells)
                    {
                        item.Value.Background = ChooseColor;
                    }
                    SelectedCells.Add(Rows[Row].SCells);
                }
        }

        private void SetSelectedControls_CellSingle()
        {
            var Row = LastPressedItem[0];
            var Column = LastPressedItem[1];
            var sel = SelectedCells.ToArray();
            foreach (Cell item in sel)
                if (item.CellRow != Row && item.CellColumn != Column)
                {
                    item.Background = Background;
                }

            SelectedCells.Clear();
            if (!SelectedCells.Contains(Rows[Row, Column]))
                if (Rows[Row, Column] != null)
                {
                    if (Column >= 2)
                    {
                        Rows[Row, Column].Background = ChooseColor;
                        SelectedCells.Add(Rows[Row, Column]);
                    }
                }
        }

        private void SetSelectedControls_LineMulti()
        {
            var minRow = Math.Min(FirstPressedItem[0], LastPressedItem[0]);
            var maxRow = Math.Max(FirstPressedItem[0], LastPressedItem[0]);
            var sel = SelectedCells.ToArray();

            foreach (Row item in sel)
                if (!(item.SRow >= minRow && item.SRow <= maxRow))
                {
                    var cells = item.Children;
                    foreach (Cell it in cells)
                    {
                        it.Background = Background;
                    }
                }

            SelectedCells.Clear();
            for (var i = minRow; i <= maxRow; i++)
                if (!SelectedCells.Contains(Rows[i].SCells))
                    if (Rows[i] != null)
                    {
                        foreach (var item in Rows[i].Cells)
                        {
                            item.Value.Background = ChooseColor;
                        }
                        SelectedCells.Add(Rows[i].SCells);
                    }
        }

        private void SetSelectedControls_CellMulti()
        {
            var minRow = Math.Min(FirstPressedItem[0], LastPressedItem[0]);
            var maxRow = Math.Max(FirstPressedItem[0], LastPressedItem[0]);
            var minColumn = Math.Min(FirstPressedItem[1], LastPressedItem[1]);
            var maxColumn = Math.Max(FirstPressedItem[1], LastPressedItem[1]);
            var sel = SelectedCells.ToArray();
            foreach (Cell item in sel)
            {
                if (!(item.CellRow >= minRow && item.CellRow <= maxRow))
                {
                    item.Background = Background;
                }

                if (!(item.CellColumn >= minColumn && item.CellColumn <= maxColumn))
                {
                    item.Background = Background;
                }
            }
            SelectedCells.Clear();
            for (var i = minRow; i <= maxRow; i++)
            {
                for (var j = minColumn; j <= maxColumn; j++)
                {
                    if (Rows[i, j] != null)
                    {
                        if (j == 1)
                        {
                            if (!Name.Contains("Note"))
                            {
                                var lst = Rows[i].Cells;
                                var cnt = lst.Count();
                                for (int n =2;n<=cnt;n++)
                                {
                                    Rows[i,n].Background = ChooseColor;
                                    SelectedCells.Add(Rows[i,n]);
                                }
                            }
                            else
                            {
                                Rows[i, j].Background = ChooseColor;
                                SelectedCells.Add(Rows[i, j]);
                            }
                        }
                        else
                        {
                            if (j >= 2)
                            {
                                Rows[i, j].Background = ChooseColor;
                                SelectedCells.Add(Rows[i, j]);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region SetSelectedItems
        public void SetSelectedItems()
        {
            var lst = new ObservableCollectionWithItemPropertyChanged<IKey>();
            foreach (var item in SelectedCells)
            {
                if (item is Cell)
                {
                    var ch = (Border)((Cell)item).Content;
                    var ch2 = (Panel)ch.Child;
                    if (ch2.DataContext != null)
                    {
                        lst.Add((IKey)(ch2.DataContext));
                    }
                }

                if (item is StackPanel)
                {
                    if ((item as StackPanel).DataContext != null)
                    {
                        var ch = (IKey)((item as StackPanel).DataContext);
                        lst.Add(ch);
                    }
                }

            }
            _selecteditems = lst;
        }

        private void SetSelectedItemsWithHandler()
        {
            var lst = new ObservableCollectionWithItemPropertyChanged<IKey>();
            foreach (var item in SelectedCells)
            {
                if (item is Cell)
                {
                    var ch = (Border)((Cell)item).Content;
                    var ch2 = (Panel)ch.Child;
                    if (ch2.DataContext != null)
                    {
                        try
                        {
                            lst.Add((IKey)(ch2.DataContext));
                        }
                        catch
                        { }
                    }
                }

                if (item is StackPanel)
                {
                    if ((item as StackPanel).DataContext != null)
                    {
                        var ch = (IKey)((item as StackPanel).DataContext);
                        lst.Add(ch);
                    }
                }
            }

            SelectedItems = lst;
        }
        #endregion

        private int[] FindCell(PointerPoint Mainmouse)
        {
            PointerPoint mouse = Mainmouse;

            var num = Convert.ToInt32(_nowPage);
            var offset = Rows.Offset;
            var Rws = Rows[1 + offset, 1];
            double h = 0;
            h = Rws.Bounds.Size.Height;
            int[] ret = new int[2];

            var t1 = 0;
            double sum_y = 0;
            for (var i = 1; i <= Rows.Count; i++) 
            {
                var tp = Rows[i];
                if (tp != null)
                {
                    sum_y += tp.SCells.Bounds.Height;
                }

                if (mouse.Position.Y <= sum_y)
                {
                    t1 = i;
                    break;
                }
            }

            //var t1 = (int)Math.Round(mouse.Position.Y / h, 0, MidpointRounding.ToNegativeInfinity) + 1 + offset;
            if (t1 <= Rows.Count + offset && t1 > offset)
            {
                ret[0] = t1;
                double sum = 0;
                for (var i = 1; i <= Rows[t1].Count; i++)
                {
                    var tp = Rows[t1, i];
                    if (tp != null)
                    {
                        sum += tp.Width;
                    }

                    if (mouse.Position.X <= sum)
                    {
                        ret[1] = i;
                        break;
                    }
                }
            }

            return ret;
        }

        #region DataGridPoiter Down/Moved/Up/DoubleTapped
        public bool DownFlag { get; set; }
        public int[] FirstPressedItem { get; set; } = new int[2];
        public int[] LastPressedItem { get; set; } = new int[2];

        public void DataGridPointerDown(object sender, PointerPressedEventArgs args)
        {
            var mouse = args.GetCurrentPoint((StackPanel)sender);
            if (mouse.Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed ||
                mouse.Properties.PointerUpdateKind == PointerUpdateKind.RightButtonPressed)
                if (mouse.Properties.PointerUpdateKind == PointerUpdateKind.RightButtonPressed)
                {
                    if (Rows.Count > 0)
                    {
                        var tmp = FindCell(mouse);

                        var minRow = Math.Min(FirstPressedItem[0], LastPressedItem[0]);
                        var maxRow = Math.Max(FirstPressedItem[0], LastPressedItem[0]);
                        var minColumn = Math.Min(FirstPressedItem[1], LastPressedItem[1]);
                        var maxColumn = Math.Max(FirstPressedItem[1], LastPressedItem[1]);

                        if (!(maxColumn == 1 && minColumn == 1))
                        {
                            if ((!(tmp[0] >= minRow && tmp[0] <= maxRow)) || (!(tmp[1] >= minColumn && tmp[1] <= maxColumn)))
                            {
                                FirstPressedItem = tmp;
                                LastPressedItem = tmp;
                                DownFlag = true;
                                SetSelectedControls();
                                var item = SelectedCells.FirstOrDefault();
                                if (item != null)
                                {
                                    if (item is (Cell))
                                    {
                                        var bd = (Cell)item;
                                        if ((!bd.IsReadOnly))
                                        {
                                            var t = ((TextBox)((Panel)((Border)bd
                                                .GetLogicalChildren().First())
                                                .Child)
                                                .Children[0]);
                                            t.Focus();
                                        }
                                    }
                                    else
                                    {
                                        var bd = (Row)item;
                                        bd.Focus();
                                    }
                                }
                                SetSelectedItemsWithHandler();
                            }
                        }
                        else
                        {
                            if ((!(tmp[0] >= minRow && tmp[0] <= maxRow)))
                            {
                                FirstPressedItem = tmp;
                                LastPressedItem = tmp;
                                DownFlag = true;
                                SetSelectedControls();
                                var item = SelectedCells.FirstOrDefault();
                                if (item != null)
                                {
                                    if (item is (Cell))
                                    {
                                        var bd = (Cell)item;
                                        if ((!bd.IsReadOnly))
                                        {
                                            var t = ((TextBox)((Panel)((Border)bd
                                                .GetLogicalChildren().First())
                                                .Child)
                                                .Children[0]);
                                            t.Focus();
                                        }
                                    }
                                    else
                                    {
                                        var bd = (Row)item;
                                        bd.Focus();
                                    }
                                }
                                SetSelectedItemsWithHandler();
                            }
                        }
                        this.ContextMenu.Close();
                        this.ContextMenu.PlacementTarget = Rows[tmp[0], tmp[1]];
                        this.ContextMenu.Open();
                    }
                }
                else
                {
                    this.ContextMenu.Close();
                    if (Rows.Count > 0)
                    {
                        var tmp = FindCell(mouse);
                        FirstPressedItem = tmp;
                        LastPressedItem = tmp;

                        DownFlag = true;
                        SetSelectedControls();
                        var item = SelectedCells.FirstOrDefault();
                        if (item != null)
                        {
                            if (item is (Cell))
                            {
                                var bd = (Cell)item;
                                if ((!bd.IsReadOnly))
                                {
                                    var t = ((TextBox)((Panel)((Border)bd
                                        .GetLogicalChildren().First())
                                        .Child)
                                        .Children[0]);
                                    t.Focus();
                                }
                            }
                            else
                            {
                                var bd = (Row)item;
                                bd.Focus();
                            }
                        }
                        SetSelectedItemsWithHandler();
                    }
                }
        }

        public void DataGridPointerMoved(object sender, PointerEventArgs args)
        {
            var mouse = args.GetCurrentPoint((StackPanel)sender);
            if (DownFlag)
            {
                if (Rows.Count > 0)
                {
                    var tmp = FindCell(mouse);
                    LastPressedItem = tmp;

                    SetSelectedControls();
                    SetSelectedItemsWithHandler();
                }
            }
        }

        public void DataGridPointerUp(object sender, PointerReleasedEventArgs args)
        {
            var mouse = args.GetCurrentPoint((StackPanel)sender);
            if (mouse.Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonReleased ||
                mouse.Properties.PointerUpdateKind == PointerUpdateKind.RightButtonReleased)
            {
                if (mouse.Properties.PointerUpdateKind == PointerUpdateKind.RightButtonReleased)
                {
                    this.ContextMenu.Open(this);
                    if (Rows.Count > 0)
                    {
                        var tmp = FindCell(mouse);

                        var minRow = Math.Min(FirstPressedItem[0], LastPressedItem[0]);
                        var maxRow = Math.Max(FirstPressedItem[0], LastPressedItem[0]);
                        var minColumn = Math.Min(FirstPressedItem[1], LastPressedItem[1]);
                        var maxColumn = Math.Max(FirstPressedItem[1], LastPressedItem[1]);

                        if (!(maxColumn ==1&&minColumn == 1))
                        {
                            if ((!(tmp[0] >= minRow && tmp[0] <= maxRow)) || (!(tmp[1] >= minColumn && tmp[1] <= maxColumn)))
                            {
                                LastPressedItem = tmp;
                                DownFlag = false;
                                SetSelectedControls();
                                SetSelectedItemsWithHandler();
                            }
                        }
                        else
                        {
                            if((!(tmp[0] >= minRow && tmp[0] <= maxRow)))
                            {
                                LastPressedItem = tmp;
                                DownFlag = false;
                                SetSelectedControls();
                                SetSelectedItemsWithHandler();
                            }
                        }
                    }
                }
                else
                {
                    if (Rows.Count > 0)
                    {
                        var tmp = FindCell(mouse);
                        LastPressedItem = tmp;

                        DownFlag = false;

                        SetSelectedControls();
                        SetSelectedItemsWithHandler();
                    }
                }
            }
        }

        private void DataGrid_DoubleTapped(object? sender, RoutedEventArgs e)
        {
            if (DoubleClickCommand != null)
            {
                DownFlag = false;
                DoubleClickCommand.Execute(new ObservableCollectionWithItemPropertyChanged<IKey>(this.SelectedItems));
            }
        }
        #endregion

        #region UpdateCells
        private void UpdateAllCells()
        {
            NameScope scp = new();
            scp.Register(Name, this);
            Rows.Clear();

            var num = Convert.ToInt32(_nowPage);
            var offset = (num - 1) * PageSize;
            var count = 1;

            var its = Items as IList;
            for(int i = offset; i < num * PageSize; i++)
            {
                var tmp = (Row)Support.RenderDataGridRow.Render.GetControl(Type, count, scp, Name);
                if((i)>=Items.Count())
                {
                    tmp.RowHide = true;
                }
                else
                {
                    tmp.DataContext = its[i];
                }
                Rows.Add(new CellCollection(tmp), count);
                count++;
            }

            SetSelectedControls();
            SetSelectedItemsWithHandler();
        }

        private void UpdateCells()
        {
            //UpdateAllCells();
            NameScope scp = new();
            if (Name != null)
            {
                scp.Register(Name, this);
                if (Items.Count() == 0)
                {
                    UpdateAllCells();
                    return;
                }

                var num = Convert.ToInt32(_nowPage);
                var offset = (num - 1) * PageSize;
                var its = Items as IList;

                for (int i = offset; i < num * PageSize; i++)
                {
                    if (i >= its.Count)
                    {
                        Rows[i - offset + 1].SCells.DataContext = null;
                        Rows[i - offset + 1].SCells.RowHide = true;
                    }
                    else
                    {
                        if (its[i] != Rows[i - offset + 1].SCells.DataContext)
                        {
                            Rows[i - offset + 1].SCells.DataContext = its[i];
                            Rows[i - offset + 1].SCells.RowHide = false;
                        }
                        else
                        {
                            Rows[i - offset + 1].SCells.RowHide = false;
                        }
                    }
                }

                SetSelectedControls();
                SetSelectedItemsWithHandler();
            }
        }

        public void MakeHeader()
        {
            Columns.Clear();

            var lst = Support.RenderDataGridHeader.Render.GetControl(Type);
            if (lst != null)
            {
                foreach (var item in lst)
                {
                    Columns.Add(new CellCollection(item));
                }
                MainGrid.RowDefinitions[0].Height = GridLength.Parse((lst.First().Children.Count * 30).ToString());
            }
        }
        #endregion

        #region KeyDown/Up
        bool ctrlFlag { get; set; } = false;
        bool shiftFlag { get; set; } = false;
        private void ChangeSelectedCellsByKeys(Key PressedKey)
        {
            if(shiftFlag)
            {
                ChangeSelectedCellsByKeyWithShift(PressedKey);
            }
            else
            {
                ChangeSelectedCellsByKey(PressedKey);
            }

            SetSelectedControls();
            SetSelectedItemsWithHandler();
        }
        private void ChangeSelectedCellsByKey(Key PressedKey)
        {
            var num = Convert.ToInt32(_nowPage);
            if (PressedKey == Key.Left)
            {
                var n = FirstPressedItem[1]-1;
                if (n <= 1)
                    n = 1;
                FirstPressedItem[1]=n;
                LastPressedItem[0] = FirstPressedItem[0];
                LastPressedItem[1] = FirstPressedItem[1];
            }
            if (PressedKey == Key.Right|| PressedKey == Key.Tab)
            {
                var n = FirstPressedItem[1]+1;
                var maxn = 0;
                foreach (var column in Columns)
                {
                    var cell =(column.Value.Cells.LastOrDefault()).Value;
                    if (cell is CustomCell)
                    {
                        var inner = (StackPanel)(cell as CustomCell).Control;
                        maxn += ((StackPanel)(cell as CustomCell).Control).Children.Count;
                    }
                    else
                    {
                        if (cell is Cell)
                        {
                            maxn++;
                        }
                    }
                }
                if (n >= maxn)
                    n = maxn;
                FirstPressedItem[1] = n;
                LastPressedItem[0] = FirstPressedItem[0];
                LastPressedItem[1] = FirstPressedItem[1];
            }
            if (PressedKey == Key.Up)
            {
                var n = FirstPressedItem[0]-1;
                var minn = PageSize * (num-1)+1;
                if (n <= minn)
                    n = minn;
                FirstPressedItem[0] = n;
                LastPressedItem[0] = FirstPressedItem[0];
                LastPressedItem[1] = FirstPressedItem[1];
            }
            if (PressedKey == Key.Down)
            {
                var n = FirstPressedItem[0]+1;
                var maxn = Math.Min(PageSize*num,Items.Count());
                if (n >= maxn)
                    n = maxn;
                FirstPressedItem[0] = n;
                LastPressedItem[0] = FirstPressedItem[0];
                LastPressedItem[1] = FirstPressedItem[1];
            }
            if (PressedKey != Key.Tab)
            {
                var bd = (Cell)Rows[FirstPressedItem[0], FirstPressedItem[1]];
                if (bd != null)
                {
                    if (!bd.IsReadOnly)
                    {
                        var t = ((TextBox)((Panel)((Border)bd
                            .GetLogicalChildren().First())
                            .Child)
                            .Children[0]);
                        t.Focus();
                        if (t.Text != null)
                        {
                            t.SelectionStart = 0;
                            t.SelectionEnd = t.Text.Length;
                        }
                    }
                }
            }
        }
        private void ChangeSelectedCellsByKeyWithShift(Key PressedKey)
        {
            var num = Convert.ToInt32(_nowPage);
            int[] tmp = null;
            tmp = LastPressedItem;
            if (PressedKey == Key.Left)
            {
                var n = tmp[1] - 1;
                if (n <= 1)
                    n = 1;
                tmp[1] = n;
            }
            if (PressedKey == Key.Right)
            {
                var n = tmp[1] + 1;
                var maxn = 0;
                foreach (var column in Columns)
                {
                    var cell = (column.Value.Cells.LastOrDefault()).Value;
                    if (cell is CustomCell)
                    {
                        var inner = (StackPanel)(cell as CustomCell).Control;
                        maxn += ((StackPanel)(cell as CustomCell).Control).Children.Count;
                    }
                    else
                    {
                        if (cell is Cell)
                        {
                            maxn++;
                        }
                    }
                }
                if (n >= maxn)
                    n = maxn;
                tmp[1] = n;
            }
            if (PressedKey == Key.Up)
            {
                var n = tmp[0] - 1;
                var minn = PageSize * (num - 1) + 1;
                if (n <= minn)
                    n = minn;
                tmp[0] = n;
            }
            if (PressedKey == Key.Down)
            {
                var n = tmp[0] + 1;
                var maxn = Math.Min(PageSize * num, Items.Count());
                if (n >= maxn)
                    n = maxn;
                tmp[0] = n;
            }
        }
        private void KeyDownEventHandler(object sender,KeyEventArgs args)
        {
            if(args.Key==Key.LeftCtrl)
            {
                ctrlFlag = true;
            }
            if (args.Key == Key.LeftShift)
            {
                shiftFlag = true;
            }

            if (args.Key == Key.Left)
                ChangeSelectedCellsByKeys(Key.Left);
            if (args.Key == Key.Right)
                ChangeSelectedCellsByKeys(Key.Right);
            if (args.Key == Key.Tab)
                ChangeSelectedCellsByKeys(Key.Tab);
            if (args.Key == Key.Up)
                ChangeSelectedCellsByKeys(Key.Up);
            if (args.Key == Key.Down)
                ChangeSelectedCellsByKeys(Key.Down);

            if (ctrlFlag==true)
            {
                if (args.Key == Key.C)
                {
                    _CopyRows(SelectedCells);
                    ctrlFlag = false;
                }
                if (args.Key == Key.V)
                {
                    _PasteRows(SelectedCells);
                    ctrlFlag = false;
                }
                if (args.Key == Key.A) 
                {
                    var t = this.Type;
                    if (CtrlACommand != null)
                    {
                        if (t == "0.0")
                            t = "1.0";
                        if (t == "0.2")
                            t = "2.0";
                        CtrlACommand.Execute(t);
                        ctrlFlag = false;
                    }
                }
                if (args.Key == Key.E)
                {
                    if (CtrlECommand != null)
                    {
                        CtrlECommand.Execute(new ObservableCollectionWithItemPropertyChanged<IKey>(this.SelectedItems));
                        ctrlFlag = false;
                    }
                }
                if (args.Key == Key.N)
                {
                    if (CtrlNCommand != null)
                    {
                        CtrlNCommand.Execute();
                        ctrlFlag = false;
                    }
                }
                if (args.Key == Key.D)
                {
                    if (CtrlDCommand != null)
                    {
                        CtrlDCommand.Execute(new ObservableCollectionWithItemPropertyChanged<IKey>(this.SelectedItems));
                        ctrlFlag = false;
                    }
                }
                if (args.Key == Key.I)
                {
                    if (CtrlICommand != null)
                    {
                        CtrlICommand.Execute(new ObservableCollectionWithItemPropertyChanged<IKey>(this.SelectedItems));
                        ctrlFlag = false;
                    }
                }
            }

            if (args.Key == Key.Delete)
            {
                var lst = SelectedCells.ToList();
                foreach (var item in lst)
                {
                    if (item is Cell)
                    {
                        var bd = (Cell)item;
                        if (!bd.IsReadOnly)
                        {
                            var t = ((TextBox)((Panel)((Border)bd
                                .GetLogicalChildren().First())
                                .Child)
                                .Children[0]);
                            t.Text = "";
                        }
                    }
                }
            }
        }
        private void KeyUpEventHandler(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.LeftCtrl)
            {
                ctrlFlag = false;
            }
            if (args.Key == Key.LeftShift)
            {
                shiftFlag = false;
            }
        }
        #endregion

        #region Copy/Paste
        private async Task _PasteRows(IEnumerable<Control> param)
        {
            if (Avalonia.Application.Current.Clipboard is Avalonia.Input.Platform.IClipboard clip)
            {
                var first = param.FirstOrDefault();
                if (first is Cell)
                {
                    string? text = await clip.GetTextAsync();
                    bool _flag = false;
                    Cell cl = null;
                    foreach (var item in param)
                    {
                        cl = (Cell)item;
                        break;
                    }

                    if (cl != null)
                    {
                        int Row = cl.CellRow;
                        int Column = cl.CellColumn;

                        if (text != null && text != "")
                        {
                            string rt = "";
                            for(int i=0;i<text.Length;i++)
                            {
                                var item = text[i];
                                if (item == '\"')
                                {
                                    _flag = !_flag;
                                }
                                else
                                {
                                    if (item == '\r'||item == '\n')
                                    {
                                        if (item =='\r')
                                        {
                                            if (i + 1 < text.Length)
                                            {
                                                if (text[i + 1] == '\n')
                                                {
                                                    i++;
                                                    if (_flag)
                                                    {
                                                        rt += text[i + 1];
                                                    }
                                                }
                                            }
                                        }
                                        if (!_flag)
                                        {
                                            foreach (var it in param)
                                            {
                                                var cell = (Cell)it;
                                                if (cell.CellColumn == Column && cell.CellRow == Row)
                                                {
                                                    var child = (Border)cell.GetLogicalChildren().FirstOrDefault();
                                                    if (child != null)
                                                    {
                                                        var panel = (Panel)child.Child;
                                                        var textbox = (TextBox)panel.Children.FirstOrDefault();

                                                        if (textbox.TextWrapping==TextWrapping.Wrap)
                                                        {
                                                            textbox.Text = rt;
                                                        }
                                                        else
                                                        {
                                                            textbox.Text = rt.Replace("\t", "").Replace("\r", "").Replace("\n", "");
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            rt = "";
                                            Row++;
                                            Column = cl.CellColumn;
                                        }
                                        else
                                        {
                                            rt += item;
                                        }
                                    }
                                    else
                                    {
                                        if (!_flag)
                                        {
                                            if (item == '\t')
                                            {
                                                foreach (var it in param)
                                                {
                                                    var cell = (Cell)it;
                                                    if (cell.CellColumn == Column && cell.CellRow == Row)
                                                    {
                                                        var child = (Border)cell.GetLogicalChildren().FirstOrDefault();
                                                        if (child != null)
                                                        {
                                                            var panel = (Panel)child.Child;
                                                            var textbox = (TextBox)panel.Children.FirstOrDefault();
                                                            if (textbox.TextWrapping == TextWrapping.Wrap)
                                                            {
                                                                textbox.Text = rt;
                                                            }
                                                            else
                                                            {
                                                                textbox.Text = rt.Replace("\t", "").Replace("\r", "").Replace("\n","");
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                rt = "";
                                                Column++;
                                            }
                                            else
                                            {
                                                rt += item;
                                            }
                                        }
                                        else
                                        {
                                            rt += item;
                                        }
                                    }
                                }
                            }
                            foreach (var it in param)
                            {
                                var cell = (Cell)it;
                                if (cell.CellColumn == Column && cell.CellRow == Row)
                                {
                                    var child = (Border)cell.GetLogicalChildren().FirstOrDefault();
                                    if (child != null)
                                    {
                                        var panel = (Panel)child.Child;
                                        var textbox = (TextBox)panel.Children.FirstOrDefault();
                                        textbox.Text = rt.Replace("\n", "").Replace("\t", "").Replace("\r", "");
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private async Task _CopyRows(IEnumerable<Control> param)
        {
            if (Avalonia.Application.Current.Clipboard is Avalonia.Input.Platform.IClipboard clip)
            {
                string txt = "";

                var first = param.FirstOrDefault();
                if (first is Cell)
                {

                    var ord = param.GroupBy(x => ((Cell)x).CellRow);
                    foreach (var item in ord)
                    {
                        var t = item.OrderBy(x => ((Cell)x).CellColumn);
                        foreach (var it in t)
                        {
                            var cell = (Cell)it;
                            var child = (Border)cell.GetLogicalChildren().FirstOrDefault();
                            if (child != null)
                            {
                                var panel = (Panel)child.Child;
                                var textbox = (TextBox)panel.Children.FirstOrDefault();
                                if (textbox != null)
                                {
                                    if (textbox.Text != null)
                                    {
                                        if (textbox.Text.Contains("\n") || textbox.Text.Contains("\t") || textbox.Text.Contains("\r"))
                                        {
                                            txt += "\"" + textbox.Text + "\"";
                                        }
                                        else
                                        {
                                            txt += textbox.Text;
                                        }
                                    }
                                    txt += "\t";
                                }
                            }
                        }
                        txt += "\r";
                    }
                }        
                await clip.ClearAsync();
                await clip.SetTextAsync(txt);
            }
        }
        #endregion

        #region Init
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Init();

            this.AddHandler(KeyDownEvent, KeyDownEventHandler, handledEventsToo: true);
            this.AddHandler(KeyUpEvent, KeyUpEventHandler, handledEventsToo: true);
        }

        private void Init()
        {
            Border brd = new()
            {
                BorderThickness = Thickness.Parse("1"),
                BorderBrush = new SolidColorBrush(Color.Parse("Gray"))
            };

            Panel p = new()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            brd.Child = p;

            Grid grde = new();
            RowDefinition rde = new()
            {
                Height = GridLength.Parse("40")
            };
            grde.RowDefinitions.Add(new RowDefinition());
            grde.RowDefinitions.Add(rde);
            p.Children.Add(grde);

            Panel pan1 = new Panel();
            pan1.HorizontalAlignment = HorizontalAlignment.Stretch;
            pan1.VerticalAlignment = VerticalAlignment.Stretch;
            pan1.SetValue(Grid.RowProperty, 0);
            grde.Children.Add(pan1);

            ScrollViewer vw = new();
            vw.Background = new SolidColorBrush(Color.Parse("WhiteSmoke"));
            vw.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            vw.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            pan1.Children.Add(vw);

            Grid grd = new();
            RowDefinition rd = new()
            {
                Height = GridLength.Parse("0")
            };
            grd.RowDefinitions.Add(rd);
            grd.RowDefinitions.Add(new RowDefinition());
            MainGrid = grd;
            Panel pan3 = new Panel();
            pan3.HorizontalAlignment = HorizontalAlignment.Stretch;
            pan3.VerticalAlignment = VerticalAlignment.Stretch;
            vw.Content = pan3;
            pan3.Children.Add(grd);

            Panel pnl = new();
            pnl.SetValue(Grid.RowProperty, 0);
            pnl.HorizontalAlignment = HorizontalAlignment.Stretch;
            grd.Children.Add(pnl);

            StackPanel stckC = new()
            {
                Margin = Thickness.Parse("0,0,0,0"),
                Spacing = 0,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            pnl.Children.Add(stckC);
            Columns = new RowCollection(stckC);

            Panel pan = new Panel();
            pan.HorizontalAlignment = HorizontalAlignment.Stretch;
            pan.VerticalAlignment = VerticalAlignment.Stretch;
            pan.SetValue(Grid.RowProperty, 1);
            grd.Children.Add(pan);

            ScrollViewer vw2 = new();
            vw2.Background = new SolidColorBrush(Color.Parse("WhiteSmoke"));
            vw2.Offset = new Vector(-100, 0);
            vw2.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            vw2.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            pan.Children.Add(vw2);

            Panel pn = new Panel();
            pn.HorizontalAlignment = HorizontalAlignment.Stretch;
            pn.VerticalAlignment = VerticalAlignment.Stretch;
            //pn.AddHandler(PointerPressedEvent, DataGridPointerDown, handledEventsToo: true);
            //pn.AddHandler(PointerMovedEvent, DataGridPointerMoved, handledEventsToo: true);
            //pn.AddHandler(PointerReleasedEvent, DataGridPointerUp, handledEventsToo: true);
            vw2.Content = pn;
            StackPanel stck = new()
            {
                Margin = Thickness.Parse("0,-1,0,0"),
                Spacing = 0,
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            stck.AddHandler(PointerPressedEvent, DataGridPointerDown, handledEventsToo: true);
            stck.AddHandler(PointerMovedEvent, DataGridPointerMoved, handledEventsToo: true);
            stck.AddHandler(PointerReleasedEvent, DataGridPointerUp, handledEventsToo: true);
            pn.Children.Add(stck);
            Rows = new RowCollection(stck);

            Panel pnle = new Panel();
            pnle.HorizontalAlignment = HorizontalAlignment.Stretch;
            pnle.VerticalAlignment = VerticalAlignment.Stretch;
            pnle.SetValue(Grid.RowProperty, 1);
            pnle.Background = new SolidColorBrush(Color.Parse("LightGray"));
            grde.Children.Add(pnle);

            StackPanel s = new StackPanel()
            {
                Margin = Thickness.Parse("5,0,0,0"),
                Orientation = Orientation.Horizontal,
                Spacing = 5
            };
            pnle.Children.Add(s);

            Button btnDown = new Button
            {
                Content = "<",
                Width = 30,
                Height = 30
            };
            btnDown.Click += NowPageDown;
            s.Children.Add(btnDown);

            TextBox box = new TextBox()
            {
                [!TextBox.TextProperty] = this[!DataGrid.NowPageProperty],
                TextAlignment = TextAlignment.Center
            };
            box.Width = 30;
            box.Height = 30;
            s.Children.Add(box);

            Button btnUp = new Button
            {
                Content = ">",
                Width = 30,
                Height = 30
            };
            btnUp.Click += NowPageUp;
            s.Children.Add(btnUp);
            s.Children.Add(new TextBlock() { Text = "���-�� �������:" });
            s.Children.Add(new TextBlock() { [!TextBox.TextProperty] = this[!DataGrid.PageCountProperty] });

            s.Children.Add(new TextBlock() { Text = "���-�� �������:" });
            s.Children.Add(new TextBlock() { [!TextBox.TextProperty] = this[!DataGrid.ItemsCountProperty] });

            Content = brd;
        }
        #endregion
    }
}