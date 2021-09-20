﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Layout;
using Client_App.Controls.DataGrid;
using Avalonia.Media;
using Models.Attributes;

namespace Client_App.Long_Visual
{
    public class Form2_Visual
    {
        public static Button CreateButton(string content, string thickness, int columnProp, int height, string commProp)
        {
            return new Button()
            {
                Height = height,
                Margin = Thickness.Parse(thickness),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Content = content,
                [!Button.CommandProperty] = new Binding(commProp),
                [Grid.ColumnProperty] = columnProp
            };
        }

        public static Cell CreateTextBox(string thickness, int columnProp, int height, string textProp, double width)
        {
            return new Cell(textProp, false)
            {
                Height = height,
                Width = width,
                Margin = Thickness.Parse(thickness),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                [!Cell.DataContextProperty] = new Binding(textProp, BindingMode.TwoWay),
                [Grid.ColumnProperty] = columnProp
            };
        }

        public static TextBlock CreateTextBlock(string margin, int columnProp, int height, string text)
        {
            return new TextBlock
            {
                Height = height,
                Margin = Thickness.Parse(margin),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                Text = text,
                [Grid.ColumnProperty] = columnProp
            };
        }

        static StackPanel Create20Row(string Property, string BindingPrefix)
        {
            StackPanel pnl = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Spacing = 30
            };

            Grid grd = new Grid()
            {
                Width = 400
            };
            grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            grd.Children.Add(CreateTextBlock("5,0,0,0", 0, 30,
                ((Form_PropertyAttribute)Type.GetType("Models.Form10,Models").GetProperty(Property)
                    .GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Name
            ));

            grd.Children.Add(CreateTextBox("5,0,10,0", 1, 30, BindingPrefix + "[0]." + Property, 200));

            Grid grd2 = new Grid()
            {
                Width = 400
            };
            grd2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grd2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            grd2.Children.Add(CreateTextBlock("5,0,0,0", 0, 30,
                ((Form_PropertyAttribute)Type.GetType("Models.Form10,Models").GetProperty(Property)
                    .GetCustomAttributes(typeof(Form_PropertyAttribute), false).First()).Name
            ));

            grd2.Children.Add(CreateTextBox("5,0,10,0", 1, 30, BindingPrefix + "[1]." + Property, 200));

            pnl.Children.Add(grd);
            pnl.Children.Add(grd2);
            return pnl;
        }

        public static Grid Form20_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.07, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.07, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.86, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl2 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 0);

            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 0, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 1, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            var topPnl3 = new StackPanel();
            topPnl3.Orientation = Orientation.Horizontal;
            topPnl3.Spacing = 30;
            //ColumnDefinition? column3 = new ColumnDefinition();
            //topPnl3.ColumnDefinitions.Add(column3);
            //column3 = new ColumnDefinition();
            //topPnl3.ColumnDefinitions.Add(column3);

            topPnl3.SetValue(Grid.RowProperty, 1);

            var pnl1 = new Panel();
            pnl1.Width = 400;
            pnl1.Children.Add(
            new TextBlock
            {
                Height = 30,
                Margin = Thickness.Parse("0,0,0,0"),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeight.Bold,
                FontSize = 16,
                Text = "Юридическое лицо",
                //[Grid.ColumnProperty] = 0
            });
            topPnl3.Children.Add(pnl1);

            var pnl2 = new Panel();
            pnl2.Width = 400;
            pnl2.Children.Add(
            new TextBlock
            {
                Height = 30,
                Margin = Thickness.Parse("0,0,0,0"),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeight.Bold,
                FontSize = 16,
                Text = "Обособленное подразделение",
                //[Grid.ColumnProperty] = 1
            });
            topPnl3.Children.Add(pnl2);

            maingrid.Children.Add(topPnl3);

            StackPanel pnl = new StackPanel()
            {
                [Grid.ColumnProperty] = 0,
                [Grid.RowProperty] = 2
            };
            maingrid.Children.Add(pnl);


            string BindingPrefix = "Storage.Rows20";

            pnl.Children.Add(Create20Row("OrganUprav", BindingPrefix));
            pnl.Children.Add(Create20Row("JurLico", BindingPrefix));
            pnl.Children.Add(Create20Row("ShortJurLico", BindingPrefix));
            pnl.Children.Add(Create20Row("JurLicoAddress", BindingPrefix));
            pnl.Children.Add(Create20Row("JurLicoFactAddress", BindingPrefix));
            pnl.Children.Add(Create20Row("GradeFIO", BindingPrefix));
            pnl.Children.Add(Create20Row("Telephone", BindingPrefix));
            pnl.Children.Add(Create20Row("Fax", BindingPrefix));
            pnl.Children.Add(Create20Row("Email", BindingPrefix));
            pnl.Children.Add(Create20Row("RegNo", BindingPrefix));
            pnl.Children.Add(Create20Row("Okpo", BindingPrefix));
            pnl.Children.Add(Create20Row("Okved", BindingPrefix));
            pnl.Children.Add(Create20Row("Okogu", BindingPrefix));
            pnl.Children.Add(Create20Row("Oktmo", BindingPrefix));
            pnl.Children.Add(Create20Row("Inn", BindingPrefix));
            pnl.Children.Add(Create20Row("Kpp", BindingPrefix));
            pnl.Children.Add(Create20Row("Okopf", BindingPrefix));
            pnl.Children.Add(Create20Row("Okfs", BindingPrefix));

            return maingrid;
        }

        public static Grid Form21_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1",
                Name = "Form21Data_",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows21",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);
            
            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
        public static Grid Form22_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form22Data_",
                Type = "2.2",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows22",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);
            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
        public static Grid Form23_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form23Data_",
                Type = "2.3",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows23",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
        public static Grid Form24_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form24Data_",
                Type = "2.4",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows24",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
        public static Grid Form25_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form25Data_",
                Type = "2.5",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows25",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
        public static Grid Form26_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.1, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.1, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.1, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.8, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.8, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl0 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            topPnl0.SetValue(Grid.RowProperty, 0);
            topPnl0.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl0.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl0.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl0.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl0);

            Grid? topPnl1 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.SetValue(Grid.RowProperty, 1);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl1.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl1.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl1.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 2);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Количество наблюдательных скважин, принадлежащих организации:"));
            topPnl2.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.SourcesQuantity26", 100));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form26Data_",
                Type = "2.6",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 3);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows26",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 4);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
        public static Grid Form27_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.1, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.1, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.25, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.8, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.8, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl0 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            topPnl0.SetValue(Grid.RowProperty, 0);
            topPnl0.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl0.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl0.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl0.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl0);

            Grid? topPnl1 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.SetValue(Grid.RowProperty, 1);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl1.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl1.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl1.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl1);

            StackPanel a = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Spacing = -1
            };
            a.SetValue(Grid.RowProperty, 2);
            StackPanel b2 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            b2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Разрешение на допустимые выбросы радионуклидов в атмосферу №"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.PermissionNumber27", 100));
            b2.Children.Add(CreateTextBlock("5,13,0,0", 2, 30, "от"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 3, 30, "Storage.PermissionIssueDate27", 100));
            b2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, ". Срок действия с"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.ValidBegin27", 100));
            b2.Children.Add(CreateTextBlock("5,13,0,0", 2, 30, "по"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 3, 30, "Storage.ValidThru27", 100));
            a.Children.Add(b2);
            StackPanel b4 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            b4.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Наименование разрешительного документа на допустимые выбросы радионуклидов в атмосферу:"));
            b4.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.PermissionDocumentName27", 100));

            a.Children.Add(b4);
            maingrid.Children.Add(a);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form27Data_",
                Type = "2.7",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 3);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows27",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 4);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
        public static Grid Form28_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.1, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.13, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.55, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.8, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.8, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            Grid? topPnl0 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl0.ColumnDefinitions.Add(column);
            topPnl0.SetValue(Grid.RowProperty, 0);
            topPnl0.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl0.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl0.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl0.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl0);

            Grid? topPnl1 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.SetValue(Grid.RowProperty, 1);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl1.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl1.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl1.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl1);

            StackPanel a = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Spacing = -1
            };
            a.SetValue(Grid.RowProperty, 2);
            StackPanel b2 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            b2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Разрешение на сброс радионуклидов в водные объекты №"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.PermissionNumber_28", 100));
            b2.Children.Add(CreateTextBlock("5,13,0,0", 2, 30, "от"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 3, 30, "Storage.PermissionIssueDate_28", 100));
            b2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, ". Срок действия с"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.ValidBegin_28", 100));
            b2.Children.Add(CreateTextBlock("5,13,0,0", 2, 30, "по"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 3, 30, "Storage.ValidThru_28", 100));
            b2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, ". Наименование разрешительного документа на сброс:"));
            b2.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.PermissionDocumentName_28", 100));
            a.Children.Add(b2);

            StackPanel b5 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            b5.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Разрешение на сброс радионуклидов на рельеф местности №"));
            b5.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.PermissionNumber1_28", 100));
            b5.Children.Add(CreateTextBlock("5,13,0,0", 2, 30, "от"));
            b5.Children.Add(CreateTextBox("5,0,0,0", 3, 30, "Storage.PermissionIssueDate1_28", 100));
            b5.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, ". Срок действия с"));
            b5.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.ValidBegin1_28", 100));
            b5.Children.Add(CreateTextBlock("5,13,0,0", 2, 30, "по"));
            b5.Children.Add(CreateTextBox("5,0,0,0", 3, 30, "Storage.ValidThru1_28", 100));
            b5.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, ". Наименование разрешительного документа на сброс:"));
            b5.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.PermissionDocumentName1_28", 100));
            a.Children.Add(b5);

            StackPanel b8 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            b8.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Договор на передачу сточных вод в сети канализации №"));
            b8.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.ContractNumber_28", 100));
            b8.Children.Add(CreateTextBlock("5,13,0,0", 2, 30, "от"));
            b8.Children.Add(CreateTextBox("5,0,0,0", 3, 30, "Storage.ContractIssueDate2_28", 100));
            b8.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, ". Срок действия с"));
            b8.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.ValidBegin2_28", 100));
            b8.Children.Add(CreateTextBlock("5,13,0,0", 2, 30, "по"));
            b8.Children.Add(CreateTextBox("5,0,0,0", 3, 30, "Storage.ValidThru2_28", 100));
            a.Children.Add(b8);

            StackPanel b10 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            b10.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Организация, осуществляющая прием сточных вод:"));
            b10.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.OrganisationReciever_28", 100));
            a.Children.Add(b10);
            maingrid.Children.Add(a);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form28Data_",
                Type = "2.8",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 3);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows28",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 4);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
        public static Grid Form29_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form29Data_",
                Type = "2.9",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows29",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }

        public static Grid Form210_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form210Data_",
                Type = "2.10",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows210",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }

        public static Grid Form211_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form211Data_",
                Type = "2.11",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows211",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);


            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }

        public static Grid Form212_Visual(INameScope scp)
        {
            Grid maingrid = new Grid();
            RowDefinition? row = new RowDefinition
            {
                Height = new GridLength(0.5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(0.7, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(5, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            row = new RowDefinition
            {
                Height = new GridLength(2, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);

            Grid? topPnl1 = new Grid();
            ColumnDefinition? column = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl1.ColumnDefinitions.Add(column);
            topPnl1.SetValue(Grid.RowProperty, 0);
            topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Отчетный год:"));
            topPnl1.Children.Add(CreateTextBox("5,0,0,0", 1, 30, "Storage.Year", 100));
            maingrid.Children.Add(topPnl1);

            Grid? topPnl2 = new Grid();
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            topPnl2.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 1);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid
            {
                Name = "Form212Data_",
                Type = "2.12",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 2);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows212",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b);


            ContextMenu? cntx = new ContextMenu();
            List<MenuItem> itms = new List<MenuItem>
            {
                new MenuItem
                {
                    Header = "Добавить строку",
                    [!MenuItem.CommandProperty] = new Binding("AddRow"),
                },
                new MenuItem
                {
                    Header = "Копировать",
                    [!MenuItem.CommandProperty] = new Binding("CopyRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Вставить",
                    [!MenuItem.CommandProperty] = new Binding("PasteRows"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedCells"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteRow"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

            Controls.DataGrid.DataGrid grd1 = new Controls.DataGrid.DataGrid()
            {
                Type = "2.1*",
                Name = "Form21Notes_",
                Focusable = true,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd1.SetValue(Grid.RowProperty, 3);

            Binding b1 = new Binding
            {
                Path = "DataContext.Storage.Notes",
                ElementName = "ChangingPanel",
                NameScope = new WeakReference<INameScope>(scp)
            };
            grd1.Bind(Controls.DataGrid.DataGrid.ItemsProperty, b1);

            ContextMenu? cntx1 = new ContextMenu();
            var mn = new MenuItem
            {
                Header = "Добавить строку",
                [!MenuItem.CommandProperty] = new Binding("AddNote")
            };
            mn.SetValue(MenuItem.CommandParameterProperty, "2.1*");
            List<MenuItem> itms1 = new List<MenuItem>
            {
                mn,
                new MenuItem
                {
                    Header = "Вставить из буфера",
                    [!MenuItem.CommandProperty] = new Binding("PasteNotes"),
                },
                new MenuItem
                {
                    Header = "Удалить строки",
                    [!MenuItem.CommandProperty] = new Binding("DeleteNote"),
                    [!MenuItem.CommandParameterProperty] = new Binding("$parent[2].SelectedItems"),
                }
            };
            cntx1.Items = itms1;

            grd1.ContextMenu = cntx1;

            maingrid.Children.Add(grd1);

            return maingrid;
        }
    }
}
