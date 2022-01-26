﻿using System;
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive.Subjects;
using Client_App.Converters;

namespace Client_App.Controls.DataGrid
{

    #region DataGridEnums
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
    #endregion

    public class DataGrid<T> : UserControl,IDataGrid where T : class, IKey, IDataGridColumn, new()
    {
        #region Items
        public static readonly DirectProperty<DataGrid<T>, IKeyCollection> ItemsProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, IKeyCollection>(
                nameof(Items),
                o => o.Items,
                (o, v) => o.Items = v, defaultBindingMode: BindingMode.TwoWay);

        private IKeyCollection _items = null;

        public IKeyCollection Items
        {
            get => _items;
            set
            {
                if (value != null)
                {
                    SetAndRaise(ItemsProperty, ref _items, value);
                    UpdateCells();
                    SetSelectedControls();
                }
            }
        }
        #endregion

        #region ItemsCount
        public static readonly DirectProperty<DataGrid<T>, string> ItemsCountProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, string>(
                nameof(ItemsCount),
                o => o.ItemsCount,
                (o, v) => o.ItemsCount = v);

        private string _ItemsCount = "0";
        public string ItemsCount
        {
            get => Items!=null?Items.Count.ToString():"0";
            set
            {
                SetAndRaise(ItemsCountProperty, ref _ItemsCount, Items != null ? Items.Count.ToString() : "0");
            }
        }
        #endregion

        #region SelectedItems
        public static readonly DirectProperty<DataGrid<T>, IKeyCollection> SelectedItemsProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, IKeyCollection>(
                nameof(SelectedItems),
                o => o.SelectedItems,
                (o, v) => o.SelectedItems = v);
        private IKeyCollection _selecteditems =
             new ObservableCollectionWithItemPropertyChanged<IKey>();
        public IKeyCollection SelectedItems
        {
            get => _selecteditems;
            set
            {
                if (value != null) SetAndRaise(SelectedItemsProperty, ref _selecteditems, value);
            }
        }
        #endregion

        #region SelectedCells
        public static readonly DirectProperty<DataGrid<T>, IList<Control>> SelectedCellsProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, IList<Control>>(
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
        public static readonly DirectProperty<DataGrid<T>, string> TypeProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, string>(
                nameof(Type),
                o => o.Type,
                (o, v) => o.Type = v);
        public string Type
        {
            get
            {

                return typeof(T).Name;
            }
            set
            {

            }
        }
        #endregion

        #region ChooseMode
        public static readonly StyledProperty<ChooseMode> ChooseModeProperty =
            AvaloniaProperty.Register<DataGrid<T>, ChooseMode>(nameof(ChooseMode));

        public ChooseMode ChooseMode
        {
            get => GetValue(ChooseModeProperty);
            set => SetValue(ChooseModeProperty, value);
        }
        #endregion

        #region MultilineMode
        public static readonly StyledProperty<MultilineMode> MultilineModeProperty =
            AvaloniaProperty.Register<DataGrid<T>, MultilineMode>(nameof(MultilineMode));
        public MultilineMode MultilineMode
        {
            get => GetValue(MultilineModeProperty);
            set => SetValue(MultilineModeProperty, value);
        }
        #endregion

        #region ChooseColor
        public static readonly StyledProperty<Brush> ChooseColorProperty =
            AvaloniaProperty.Register<DataGrid<T>, Brush>(nameof(ChooseColor));
        public Brush ChooseColor
        {
            get => GetValue(ChooseColorProperty);
            set => SetValue(ChooseColorProperty, value);
        }
        #endregion

        #region Pagination
        public static readonly DirectProperty<DataGrid<T>, bool> PaginationProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, bool>(
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

        #region IsReadable
        public static readonly DirectProperty<DataGrid<T>, bool> IsReadableProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, bool>(
                nameof(IsReadable),
                o => o.IsReadable,
                (o, v) => o.IsReadable = v);

        private bool _IsReadable = false;
        public bool IsReadable
        {
            get => _IsReadable;
            set
            {
                SetAndRaise(IsReadableProperty, ref _IsReadable, value);

                Init();
            }
        }
        #endregion

        #region IsColumnResize
        public static readonly DirectProperty<DataGrid<T>, bool> IsColumnResizeProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, bool>(
                nameof(IsColumnResize),
                o => o.IsColumnResize,
                (o, v) => o.IsColumnResize = v);

        private bool _IsColumnResize = true;
        public bool IsColumnResize
        {
            get => _IsColumnResize;
            set
            {
                SetAndRaise(IsColumnResizeProperty, ref _IsColumnResize, value);

                Init();
            }
        }
        #endregion

        #region PageSize
        public static readonly DirectProperty<DataGrid<T>, int> PageSizeProperty =
             AvaloniaProperty.RegisterDirect<DataGrid<T>, int>(
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
                Init();
            }
        }
        #endregion

        #region PageCount
        public static readonly DirectProperty<DataGrid<T>, string> PageCountProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, string>(
                nameof(PageCount),
                o => o.PageCount,
                (o, v) => o.PageCount = v);

        private string _PageCount = "0";
        public string PageCount
        {
            get => (Items!=null?Items.Count / PageSize + 1:0).ToString();
            set
            {
                SetAndRaise(PageCountProperty, ref _PageCount, (Items != null ? Items.Count / PageSize + 1 : 0).ToString());
            }
        }
        #endregion

        #region NowPage
        public static readonly DirectProperty<DataGrid<T>, string> NowPageProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, string>(
                nameof(NowPage),
                o => o.NowPage,
                (o, v) => o.NowPage = v, defaultBindingMode: BindingMode.TwoWay);

        private string _nowPage = "1";
        public string NowPage
        {
            get => _nowPage.ToString();
            set
            {
                try
                {
                    var val = Convert.ToInt32(value);

                    if (val != null&&Items!=null)
                    {
                        int maxpage = (Items.Count / PageSize) + 1;
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

        #region CommandsList
        public static readonly DirectProperty<DataGrid<T>, ObservableCollection<KeyComand>> CommandsListProperty =
            AvaloniaProperty.RegisterDirect<DataGrid<T>, ObservableCollection<KeyComand>>(
                nameof(CommandsList),
                o => o.CommandsList,
                (o, v) => o.CommandsList = v);

        private ObservableCollection<KeyComand> _CommandsList = new ObservableCollection<KeyComand>();
        public ObservableCollection<KeyComand> CommandsList
        {
            get => _CommandsList;
            set
            {
                SetAndRaise(CommandsListProperty, ref _CommandsList, value);
            }
        }
        private void CommandListChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            foreach (KeyComand item in args.NewItems)
            {
                if (item.IsContextMenuCommand)
                {
                    MakeContextMenu();
                    break;
                }
            }
        }
        private object GetParamByParamName(KeyComand param)
        {
            if(param.ParamName=="SelectedItems")
            {
                return SelectedItems;
            }
            if (param.ParamName == "SelectedCells")
            {
                return SelectedCells;
            }
            if (param.ParamName == "FormType")
            {
                return SelectedCells;
            }
            if(param.ParamName ==null|| param.ParamName =="")
            {
                return param.Param;
            }
            if (param.ParamName == "1.0" || param.ParamName == "2.0")
            {
                return param.ParamName;
            }
            return null;
        }

        private void ComandTapped(object sender,RoutedEventArgs args)
        {
            if (sender != null)
            {
                var selectItem = (string)((MenuItem)sender).Header;
                if (selectItem != null)
                {
                    var rt = CommandsList.Where(item => item.IsContextMenuCommand && item.ContextMenuText.Contains(selectItem));
                    foreach (var item in rt)
                    {
                        item.DoCommand(GetParamByParamName(item));
                        if (item.IsUpdateCells)
                        {
                            UpdateCells();
                        }
                    }
                }
            }
        }
        #endregion

        private DataGridColumns _Columns = null;
        private DataGridColumns Columns
        {
            get
            {
                if(_Columns==null)
                {
                    var t = new T();
                    string tmp = "";
                    if (Name == "Form1AllDataGrid_")
                    {
                        tmp = "1.0";
                    }
                    if (Name == "Form2AllDataGrid_")
                    {
                        tmp = "2.0";
                    }
                    _Columns = t.GetColumnStructure(tmp);
                }
                return _Columns;
            }
            set
            {
                if(_Columns!=value)
                {
                    _Columns = value;
                    Init();
                }
            }
        }
        private List<DataGridRow> Rows { get; set; } = new List<DataGridRow>();

        private StackPanel HeaderStackPanel { get; set; }
        private StackPanel CenterStackPanel { get; set; }
        public DataGrid(string Name = "")
        {
            this.Name = Name;

            //this.

            this.AddHandler(PointerPressedEvent,MousePressed,handledEventsToo:true);
            this.AddHandler(PointerMovedEvent, MouseMoved, handledEventsToo: true);
            this.AddHandler(PointerReleasedEvent, MouseReleased, handledEventsToo: true);
            this.AddHandler(DoubleTappedEvent, MouseDoublePressed, handledEventsToo: true);

            this.AddHandler(KeyDownEvent, OnDataGridKeyDown, handledEventsToo: true);

            _CommandsList.CollectionChanged += CommandListChanged;
        }

        #region SetSelectedControls
        private void SetSelectedControls()
        {
            if (Items != null)
            {
                if (Items.Count != 0)
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
            }
        }

        private void SetSelectedControls_LineSingle()
        {
            var Row = LastPressedItem[0];

            var tmp1 = Rows.SelectMany(x => x.Children).Where(item => ((Cell)item).Row != Row);

            foreach (Cell item in tmp1)
            {
                item.ChooseColor = (SolidColorBrush)Background;
            }

            SelectedCells.Clear();

            ObservableCollectionWithItemPropertyChanged<IKey> tmpSelectedItems = new ObservableCollectionWithItemPropertyChanged<IKey>();

            var tmp2 = Rows.SelectMany(x => x.Children).Where(item => ((Cell)item).Row == Row);

            foreach (Cell item in tmp2)
            {
                item.ChooseColor = (SolidColorBrush)ChooseColor;
                SelectedCells.Add(item);
                tmpSelectedItems.Add((T)item.DataContext);
            }
            SelectedItems = tmpSelectedItems;
        }

        //Not Done
        private void SetSelectedControls_CellSingle()
        {
            var Row = LastPressedItem[0];
            var Column = LastPressedItem[1];

            var tmp1 = Rows.Where(item => ((Cell)item.Children.FirstOrDefault()).Row != Row&&((Cell)item.Children.FirstOrDefault()).Column != Column);

            foreach (DataGridRow item in tmp1)
            {
                item.ChooseColor = (SolidColorBrush)Background;
            }

            SelectedCells.Clear();

            ObservableCollectionWithItemPropertyChanged<IKey> tmpSelectedItems = new ObservableCollectionWithItemPropertyChanged<IKey>();

            var tmp2 = Rows.Where(item => ((Cell)item.Children.FirstOrDefault()).Row == Row && ((Cell)item.Children.FirstOrDefault()).Column == Column);

            foreach (DataGridRow item in tmp2)
            {
                item.ChooseColor = (SolidColorBrush)ChooseColor;
                SelectedCells.Add(item);
                tmpSelectedItems.Add((T)item.DataContext);
            }
            SelectedItems = tmpSelectedItems;
        }

        private void SetSelectedControls_LineMulti()
        {
            var minRow = Math.Min(FirstPressedItem[0],LastPressedItem[0]);
            var maxRow = Math.Max(FirstPressedItem[0], LastPressedItem[0]);

            var tmp1 = Rows.SelectMany(x => x.Children).Where(item => !(((Cell)item).Row >= minRow && ((Cell)item).Row <= maxRow));

            foreach (Cell item in tmp1)
            {
                item.ChooseColor = (SolidColorBrush)Background;
            }

            SelectedCells.Clear();

            ObservableCollectionWithItemPropertyChanged<IKey> tmpSelectedItems = new ObservableCollectionWithItemPropertyChanged<IKey>();

            var tmp2 = Rows.SelectMany(x => x.Children).Where(item => (((Cell)item).Row >= minRow && ((Cell)item).Row <= maxRow));

            foreach (Cell item in tmp2)
            {
                item.ChooseColor = (SolidColorBrush)ChooseColor;
                SelectedCells.Add(item);
                tmpSelectedItems.Add((T)item.DataContext);
            }
            SelectedItems = tmpSelectedItems;
        }

        private void SetSelectedControls_CellMulti()
        {
            var minRow = Math.Min(FirstPressedItem[0], LastPressedItem[0]);
            var maxRow = Math.Max(FirstPressedItem[0], LastPressedItem[0]);
            var minColumn = Math.Min(FirstPressedItem[1], LastPressedItem[1]);
            var maxColumn = Math.Max(FirstPressedItem[1], LastPressedItem[1]);

            var tmp1 = Rows.SelectMany(x => x.Children).Where(item => !((((Cell)item).Row >= minRow && ((Cell)item).Row <= maxRow)&&
                                            (((Cell)item).Column >= minColumn && ((Cell)item).Column<= maxColumn)));

            foreach (Cell item in tmp1)
            {
                item.ChooseColor = (SolidColorBrush)Background;
            }

            SelectedCells.Clear();

            ObservableCollectionWithItemPropertyChanged<IKey> tmpSelectedItems = new ObservableCollectionWithItemPropertyChanged<IKey>();

            var tmp2 = Rows.SelectMany(x=>x.Children).Where(item=> ((((Cell)item).Row >= minRow && ((Cell)item).Row <= maxRow) &&
                                            (((Cell)item).Column >= minColumn && ((Cell)item).Column <= maxColumn)));

            foreach (Cell item in tmp2)
            {
                item.ChooseColor = (SolidColorBrush)ChooseColor;
                SelectedCells.Add(item);
                tmpSelectedItems.Add((T)item.DataContext);
            }
            SelectedItems = tmpSelectedItems;
        }
        #endregion

        #region DataGridPoiter
        public bool DownFlag { get; set; }
        public int[] FirstPressedItem { get; set; } = new int[2];
        public int[] LastPressedItem { get; set; } = new int[2];
        private int[] FindMousePress(double[] mouse)
        {
            var tmp = new int[2];

            var sumy = 0.0;
            var flag = false;
            foreach(var item in Rows)
            {
                sumy += item.Bounds.Height;
                if(mouse[0]<sumy)
                {
                    var sumx = 0.0;
                    foreach(Cell it in item.Children)
                    {
                        sumx+= it.Bounds.Width;
                        if (mouse[1]<sumx)
                        {
                            tmp[0] = it.Row;
                            tmp[1] = it.Column;
                            flag = true;
                            break;
                        }
                    }
                    if(flag)
                        break;
                }
            }

            return tmp;
        }
        private void MousePressed(object sender,PointerPressedEventArgs args)
        {
            var paramKey = args.GetPointerPoint(this).Properties.PointerUpdateKind;
            var paramPos = args.GetCurrentPoint(CenterStackPanel).Position;
            var paramRowColumn = FindMousePress(new double[] { paramPos.Y, paramPos.X });

            if (paramKey == PointerUpdateKind.LeftButtonPressed|| paramKey == PointerUpdateKind.RightButtonPressed)
            {
                FirstPressedItem = paramRowColumn;
                LastPressedItem = paramRowColumn;
                SetSelectedControls();
                if (paramKey == PointerUpdateKind.RightButtonPressed)
                {
                    if (!(FirstPressedItem[0] == 0 && FirstPressedItem[1] == 0))
                    {
                        this.ContextMenu.Close();
                        var tmp1 = (Cell)Rows.SelectMany(x => x.Children).Where(item => (((Cell)item).Row == FirstPressedItem[0] && ((Cell)item).Column == FirstPressedItem[1])).FirstOrDefault();
                        this.ContextMenu.PlacementTarget = tmp1;
                        this.ContextMenu.Open();
                    }
                }
                else
                {
                    this.ContextMenu.Close();
                }
            }
        }
        private void MouseDoublePressed(object sender, EventArgs args)
        {
            var commands = _CommandsList.Where(item=>item.IsDoubleTappedCommand);
            foreach(var item in commands)
            {
                item.DoCommand(GetParamByParamName(item));
            }
        }
        private void MouseReleased(object sender, PointerReleasedEventArgs args)
        {
            var paramKey = args.GetPointerPoint(this).Properties.PointerUpdateKind;
            var paramPos = args.GetCurrentPoint(CenterStackPanel).Position;
            var paramRowColumn = FindMousePress(new double[] { paramPos.Y, paramPos.X });

            if (paramKey == PointerUpdateKind.LeftButtonReleased)
            {
                LastPressedItem = paramRowColumn;

                SetSelectedControls();
            }
        }
        private void MouseMoved(object sender, PointerEventArgs args)
        {
            var paramKey = args.GetPointerPoint(this).Properties;
            var paramPos = args.GetCurrentPoint(CenterStackPanel).Position;
            var paramRowColumn = FindMousePress(new double[] { paramPos.Y, paramPos.X });

            if (paramKey.IsLeftButtonPressed)
            {
                LastPressedItem = paramRowColumn;

                SetSelectedControls();
            }
        }
        #endregion

        #region UpdateCells

        private void UpdateCells()
        {
            var count = 0;

            var num = Convert.ToInt32(_nowPage);
            var offset = (num-1) * (PageSize);
            var offsetMax= num * (PageSize);

            if (Items != null)
            {
                if (Items.Count != 0)
                {
                    for (int i = offset; i < offsetMax; i++)
                    {
                        if (count < PageSize&&i<Items.Count)
                        {
                            Rows[count].DataContext = Items.Get<T>(i);
                            Rows[count].IsVisible = true;
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (Items.Count < offsetMax)
                {
                    for (int i = Items.Count; i < offsetMax; i++)
                    {
                        try
                        {
                            if (Rows[i - offset].IsVisible)
                            {
                                Rows[i - offset].IsVisible = false;
                            }
                        }
                        catch { }
                    }
                }
            }

            PageCount = "0";
            ItemsCount = "0";
        }
        #endregion

        #region KeyDown
        private void OnDataGridKeyDown(object sender,KeyEventArgs args)
        {
            if(args.Key==Key.Left)
            {
                LastPressedItem[1] += -1;
                if(args.KeyModifiers!=KeyModifiers.Shift)
                {
                    FirstPressedItem[0] = LastPressedItem[0];
                    FirstPressedItem[1] = LastPressedItem[1];
                }
                SetSelectedControls();
            }
            if (args.Key == Key.Right)
            {
                LastPressedItem[1] += 1;
                if (args.KeyModifiers != KeyModifiers.Shift)
                {
                    FirstPressedItem[0] = LastPressedItem[0];
                    FirstPressedItem[1] = LastPressedItem[1];
                }
                SetSelectedControls();
            }
            if (args.Key == Key.Down)
            {
                LastPressedItem[0] += 1;
                if (args.KeyModifiers != KeyModifiers.Shift)
                {
                    FirstPressedItem[0] = LastPressedItem[0];
                    FirstPressedItem[1] = LastPressedItem[1];
                }
                SetSelectedControls();
            }
            if (args.Key == Key.Up)
            {
                LastPressedItem[0] += -1;
                if (args.KeyModifiers != KeyModifiers.Shift)
                {
                    FirstPressedItem[0] = LastPressedItem[0];
                    FirstPressedItem[1] = LastPressedItem[1];
                }
                SetSelectedControls();
            }

            var rt = CommandsList.Where(item=>item.Key==args.Key&&item.KeyModifiers==args.KeyModifiers);

            foreach (var item in rt)
            {
                item.DoCommand(GetParamByParamName(item));
            }
        }
        #endregion

        #region Init

        public void ChooseAllRow(object sender,RoutedEventArgs args)
        {
            SetSelectedControls_LineMulti();
        }

        public void Init()
        {
            MakeAll();
            MakeHeaderRows();
            MakeCenterRows();

            UpdateCells();
            MakeContextMenu();
            //this.DoubleTapped += DataGrid_DoubleTapped;
            //this.AddHandler(KeyDownEvent, KeyDownEventHandler, handledEventsToo: true);
            //this.AddHandler(KeyUpEvent, KeyUpEventHandler, handledEventsToo: true);
        }

        private void MakeContextMenu()
        {
            var lst = CommandsList.Where(item=>item.IsContextMenuCommand).GroupBy(item=>item.ContextMenuText[0]);

            ContextMenu menu = new ContextMenu();
            List<MenuItem> lr = new List<MenuItem>();
            foreach (var item in lst)
            {
                if (item.Count() == 1)
                {
                    var tmp = new MenuItem { Header = item.First().ContextMenuText[0] };
                    tmp.Tapped += ComandTapped;
                    lr.Add(tmp) ;
                }
                if (item.Count() == 2)
                {
                    List<MenuItem> inlr = new List<MenuItem>();
                    foreach (var it in item)
                    {
                        var tmp = new MenuItem { Header = it.ContextMenuText[1] };
                        tmp.Tapped += ComandTapped;
                        inlr.Add(tmp);
                    }
                    lr.Add(new MenuItem { Header = item.Key,Items=inlr});
                }
            }
            menu.Items = lr;
            this.ContextMenu = menu;
        }

        List<ColumnDefinition> HeadersColumns = new List<ColumnDefinition>();
        int GridSplitterSize = 2;
        private void MakeHeaderInner(DataGridColumns ls)
        {
            
            if (ls==null)
            {
                return;
            }
            else
            {
                int Level = ls.Level;

                Binding bd = new Binding()
                {
                    Source = ls,
                    Path = "SizeCol",
                    Mode = BindingMode.TwoWay
                   
                };
                this[!Control.WidthProperty] = bd;


                HeadersColumns.Clear();
                var tre = ls.GetLevel(Level-1);
                for (int i = Level-1; i >= 1; i--)
                {
                    Grid HeaderRow = new Grid();
                    var count = 0;
                    foreach (var item in tre)
                    {
                        Binding b = new Binding()
                        {
                            Source = item,
                            Path = "GridLength",
                            Mode = BindingMode.TwoWay,
                            Converter = new stringToGridLength_Converter()
                        };
                        var Column = new ColumnDefinition() { 
                            [!ColumnDefinition.WidthProperty]=b
                        };
                        HeaderRow.ColumnDefinitions.Add(Column);

                        Cell cell = new Cell() {
                            [Grid.ColumnProperty]=count
                        };
                        cell.HorizontalAlignment = HorizontalAlignment.Stretch;
                        cell.Height = 30;
                        cell.BorderColor = new SolidColorBrush(Color.Parse("Gray"));
                        cell.Background = new SolidColorBrush(Color.Parse("White"));

                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = item.name.Contains("null") ?"": item.name;
                        textBlock.TextAlignment = TextAlignment.Center;
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        textBlock.HorizontalAlignment = HorizontalAlignment.Stretch;

                        cell.Control = textBlock;

                        HeaderRow.Children.Add(cell);
                        if (count + 1 < tre.Count * 2 - 1)
                        {
                            HeaderRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Parse(GridSplitterSize.ToString()) });
                            if (i == 1&&IsColumnResize)
                            {
                                HeaderRow.Children.Add(new GridSplitter()
                                {
                                    [Grid.ColumnProperty] = count + 1,
                                    ResizeDirection = GridResizeDirection.Columns,
                                    Background = new SolidColorBrush(Color.Parse("Gray")),
                                    ResizeBehavior = GridResizeBehavior.BasedOnAlignment
                                });
                            }
                            count += 2;
                        }
                        else
                        {
                            count++;
                        }
                        if (i == 1)
                        {
                            HeadersColumns.Add(Column);
                        }
                    }
                    HeaderStackPanel.Children.Add(HeaderRow);
                    if (i - 1 >= 1)
                    {
                        tre = ls.GetLevel(i - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void MakeCenterInner(DataGridColumns ls)
        {
            if (ls == null)
            {
                return;
            }
            else
            {
                Rows.Clear();
                var lst = ls.GetLevel(1);
                for (int i = 0; i < PageSize; i++)
                {
                    var Column = 0;
                    var count = 0;
                    DataGridRow RowStackPanel = new();
                    RowStackPanel.Row = i;

                    foreach (var item in lst)
                    {
                        Binding b = new Binding()
                        {
                            Source= HeadersColumns[Column],
                            Path=nameof(ColumnDefinition.Width)
                        };
                        var Columnq = new ColumnDefinition() { [!ColumnDefinition.WidthProperty]=b};
                        RowStackPanel.ColumnDefinitions.Add(Columnq);

                        Control textBox = null;

                        Cell cell = new Cell() {
                            [Grid.ColumnProperty]=count
                        };
                        cell.HorizontalAlignment = HorizontalAlignment.Stretch;
                        cell.Row = i;
                        cell.Column = Column;
                        cell.Height = 30;
                        cell.BorderColor = new SolidColorBrush(Color.Parse("Gray"));
                        cell.Background = new SolidColorBrush(Color.Parse("White"));
                        if (item.ChooseLine)
                        {
                            cell.Tapped += ChooseAllRow;
                        }

                        if (IsReadable||item.Blocked)
                        {
                            textBox = new TextBlock()
                            {
                                [!TextBlock.DataContextProperty] = new Binding(item.Binding),
                                [!TextBlock.TextProperty] = new Binding("Value"),
                                [!TextBox.BackgroundProperty] = cell[!Cell.ChooseColorProperty]
                            };
                            if(item.Blocked)
                            {
                                textBox[!TextBox.BackgroundProperty] = cell[!Cell.ChooseColorProperty];
                            }
                            ((TextBlock)textBox).TextAlignment = TextAlignment.Center;
                            textBox.VerticalAlignment = VerticalAlignment.Center;
                            textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                            ((TextBlock)textBox).Padding = new Thickness(0,5,0,5);
                            textBox.Height = 30;
                            textBox.ContextMenu = new ContextMenu() { Width = 0, Height = 0 };
                        }
                        else
                        {
                            textBox = new TextBox()
                            {
                                [!TextBox.DataContextProperty] = new Binding(item.Binding),
                                [!TextBox.TextProperty] = new Binding("Value"),
                                [!TextBox.BackgroundProperty]=cell[!Cell.ChooseColorProperty]
                            };
                            ((TextBox)textBox).TextAlignment = TextAlignment.Center;
                            textBox.VerticalAlignment = VerticalAlignment.Center;
                            textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                            textBox.Height = 30;
                            textBox.ContextMenu = new ContextMenu() { Width = 0, Height = 0 };
                        }
                        cell.Control = textBox;

                        RowStackPanel.Children.Add(cell);
                        if (count + 1 < lst.Count * 2 - 1)
                        {
                            RowStackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Parse(GridSplitterSize.ToString()) });
                            count += 2;
                        }
                        else
                        {
                            count++;
                        }
                        Column++;
                    }
                    RowStackPanel.IsVisible = false;
                    CenterStackPanel.Children.Add(RowStackPanel);
                    Rows.Add(RowStackPanel);
                }
            }
        }

        private void MakeHeaderRows()
        {
            var Columns = this.Columns;
            MakeHeaderInner(Columns);
        }

        private void MakeCenterRows()
        {
            var Columns = this.Columns;
            MakeCenterInner(Columns);
        }

        private void MakeAll()
        {
            #region Main_<MainStackPanel>
            Panel MainPanel = new()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            StackPanel MainStackPanel = new StackPanel();
            MainStackPanel.Orientation = Orientation.Vertical;
            MainPanel.Children.Add(MainStackPanel);
            #endregion

            #region Header
            Border HeaderBorder = new()
            {
                BorderThickness = Thickness.Parse("1"),
                BorderBrush = new SolidColorBrush(Color.Parse("Gray")),
                CornerRadius = CornerRadius.Parse("2,2,2,2")
            };
            MainStackPanel.Children.Add(HeaderBorder);

            Panel HeaderPanel = new();
            HeaderPanel.Background = new SolidColorBrush(Color.FromArgb(150,180, 154, 255));
            HeaderBorder.Child = HeaderPanel;

            HeaderStackPanel = new();
            HeaderStackPanel.Margin = Thickness.Parse("2,2,2,2");
            HeaderStackPanel.Orientation = Orientation.Vertical;
            HeaderPanel.Children.Add(HeaderStackPanel);
            #endregion

            #region Center
            Border CenterBorder = new()
            {
                Margin=Thickness.Parse("0,5,0,0"),
                BorderThickness = Thickness.Parse("1"),
                BorderBrush = new SolidColorBrush(Color.Parse("Gray")),
                CornerRadius = CornerRadius.Parse("2,2,2,2")
            };
            MainStackPanel.Children.Add(CenterBorder);

            Panel CenterPanel = new()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            CenterPanel.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            ScrollViewer CenterScrollViewer = new ScrollViewer();
            CenterScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            CenterScrollViewer.Content = CenterPanel;

            CenterBorder.Child = CenterScrollViewer;

            CenterStackPanel = new();
            CenterStackPanel.Orientation = Orientation.Vertical;
            CenterStackPanel.Margin = Thickness.Parse("2,2,2,2");

            CenterPanel.Children.Add(CenterStackPanel);
            #endregion

            #region MiddleFooter
            Border MiddleFooterBorder = new()
            {
                Margin = Thickness.Parse("0,5,0,0"),
                BorderThickness = Thickness.Parse("1"),
                BorderBrush = new SolidColorBrush(Color.Parse("Gray")),
                CornerRadius = CornerRadius.Parse("2,2,2,2")
            };
            MainStackPanel.Children.Add(MiddleFooterBorder);

            StackPanel MiddleFooterStackPanel = new();
            MiddleFooterStackPanel.Background = new SolidColorBrush(Color.FromArgb(150, 180, 154, 255));
            MiddleFooterStackPanel.Orientation = Orientation.Vertical;
            MiddleFooterBorder.Child = MiddleFooterStackPanel;

            StackPanel MiddleFooterStackPanel1 = new();
            MiddleFooterStackPanel1.Orientation = Orientation.Horizontal;
            MiddleFooterStackPanel1.Children.Add(new TextBlock() { Text = "Кол-во страниц:",Margin=Thickness.Parse("5,0,0,0") });
            MiddleFooterStackPanel1.Children.Add(new TextBlock() { [!TextBox.TextProperty] = this[!DataGrid<T>.PageCountProperty], Margin = Thickness.Parse("5,0,0,0") });
            MiddleFooterStackPanel.Children.Add(MiddleFooterStackPanel1);

            StackPanel MiddleFooterStackPanel2 = new();
            MiddleFooterStackPanel2.Orientation = Orientation.Horizontal;
            MiddleFooterStackPanel2.Children.Add(new TextBlock() { Text = "Кол-во строчек:", Margin = Thickness.Parse("5,0,0,0") });
            MiddleFooterStackPanel2.Children.Add(new TextBlock() { [!TextBox.TextProperty] = this[!DataGrid<T>.ItemsCountProperty], Margin = Thickness.Parse("5,0,0,0") });
            MiddleFooterStackPanel.Children.Add(MiddleFooterStackPanel2);
            #endregion

            #region Footer
            Border FooterBorder = new()
            {
                Margin = Thickness.Parse("0,5,0,0"),
                BorderThickness = Thickness.Parse("1"),
                BorderBrush = new SolidColorBrush(Color.Parse("Gray")),
                CornerRadius = CornerRadius.Parse("2,2,2,2")
            };
            MainStackPanel.Children.Add(FooterBorder);

            Panel FooterPanel = new();
            FooterPanel.Background = new SolidColorBrush(Color.FromArgb(150, 180, 154, 255));
            FooterPanel.Height = 40;
            FooterBorder.Child=FooterPanel;

            StackPanel FooterStackPanel = new StackPanel()
            {
                Margin = Thickness.Parse("5,0,0,0"),
                Orientation = Orientation.Horizontal,
                Spacing = 5
            };
            FooterPanel.Children.Add(FooterStackPanel);

            Button btnDown = new Button
            {
                Content = "<",
                Width = 30,
                Height = 30,
                CornerRadius = CornerRadius.Parse("2,2,2,2")
            };
            btnDown.Click += NowPageDown;
            FooterStackPanel.Children.Add(btnDown);

            TextBox box = new TextBox()
            {
                [!TextBox.TextProperty] = this[!DataGrid<T>.NowPageProperty],
                TextAlignment = TextAlignment.Center,
                CornerRadius = CornerRadius.Parse("2,2,2,2")
            };
            box.Width = 30;
            box.Height = 30;
            FooterStackPanel.Children.Add(box);

            Button btnUp = new Button
            {
                Content = ">",
                Width = 30,
                Height = 30,
                CornerRadius = CornerRadius.Parse("2,2,2,2")
            };
            btnUp.Click += NowPageUp;
            FooterStackPanel.Children.Add(btnUp);

            #endregion

            ////pn.AddHandler(PointerPressedEvent, DataGridPointerDown, handledEventsToo: true);
            ////pn.AddHandler(PointerMovedEvent, DataGridPointerMoved, handledEventsToo: true);
            ////pn.AddHandler(PointerReleasedEvent, DataGridPointerUp, handledEventsToo: true);

            Content = MainPanel;
        }
        #endregion
    }
}