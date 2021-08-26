﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using System;
using System.Collections.Generic;
using Client_App.Controls.DataGrid;
using Avalonia.Media;

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

        public static TextBox CreateTextBox(string thickness, int columnProp, int height, string textProp, double width)
        {
            return new TextBox()
            {
                Height = height,
                Width = width,
                Margin = Thickness.Parse(thickness),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                [!TextBox.TextProperty] = new Binding(textProp, BindingMode.TwoWay),
                [Grid.ColumnProperty] = columnProp
            };
        }

        public static TextBlock CreateTextBlock(string thickness, int columnProp, int height, string text)
        {
            return new TextBlock
            {
                Height = height,
                Margin = Thickness.Parse(thickness),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Text = text,
                [Grid.ColumnProperty] = columnProp
            };
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
                Height = new GridLength(0.93, GridUnitType.Star)
            };
            maingrid.RowDefinitions.Add(row);
            //row = new RowDefinition
            //{
            //    Height = new GridLength(5, GridUnitType.Star)
            //};
            //maingrid.RowDefinitions.Add(row);

            //Grid? topPnl1 = new Grid();
            //ColumnDefinition? column = new ColumnDefinition
            //{
            //    Width = new GridLength(0.3, GridUnitType.Star)
            //};
            //topPnl1.ColumnDefinitions.Add(column);
            //column = new ColumnDefinition
            //{
            //    Width = new GridLength(1, GridUnitType.Star)
            //};
            //topPnl1.ColumnDefinitions.Add(column);
            //column = new ColumnDefinition
            //{
            //    Width = new GridLength(1, GridUnitType.Star)
            //};
            //topPnl1.ColumnDefinitions.Add(column);
            //topPnl1.SetValue(Grid.RowProperty, 0);
            //topPnl1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            //topPnl1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            //topPnl1.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            //topPnl1.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", double.NaN));
            //maingrid.Children.Add(topPnl1);

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
            //column = new ColumnDefinition
            //{
            //    Width = new GridLength(1, GridUnitType.Star)
            //};
            //topPnl2.ColumnDefinitions.Add(column);
            //column = new ColumnDefinition
            //{
            //    Width = new GridLength(1, GridUnitType.Star)
            //};
            //topPnl2.ColumnDefinitions.Add(column);
            //column = new ColumnDefinition
            //{
            //    Width = new GridLength(1, GridUnitType.Star)
            //};
            //topPnl2.ColumnDefinitions.Add(column);
            //column = new ColumnDefinition();
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            topPnl2.SetValue(Grid.RowProperty, 0);
            topPnl2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            topPnl2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            //topPnl2.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Номер корректировки:"));
            //topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 0, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 1, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

            Controls.DataGrid.DataGrid grd = new Controls.DataGrid.DataGrid()
            {
                Type = "2.0",
                Name = "Form20Data_",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                MultilineMode = MultilineMode.Multi,
                ChooseMode = ChooseMode.Cell,
                ChooseColor = new SolidColorBrush(new Color(150, 135, 209, 255))
            };
            grd.SetValue(Grid.RowProperty, 1);

            Binding b = new Binding
            {
                Path = "DataContext.Storage.Rows20",
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
            cntx.Items = itms;

            grd.ContextMenu = cntx;

            maingrid.Children.Add(grd);

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
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

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
            grd.SetValue(Grid.RowProperty, 2);

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
        public static Grid Form27_Visual(INameScope scp)
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
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

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
            grd.SetValue(Grid.RowProperty, 2);

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
        public static Grid Form28_Visual(INameScope scp)
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
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
            topPnl2.Children.Add(CreateButton("Проверить", "5,12,0,0", 2, 30, "CheckReport"));
            topPnl2.Children.Add(CreateButton("Сохранить", "5,12,0,0", 3, 30, "SaveReport"));

            maingrid.Children.Add(topPnl2);

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
            grd.SetValue(Grid.RowProperty, 2);

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
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
            var panel = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10
            };
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата начала периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.StartPeriod.Value", 90));
            panel.Children.Add(CreateTextBlock("5,13,0,0", 0, 30, "Дата конца периода:"));
            panel.Children.Add(CreateTextBox("5,0,0,0", 2, 30, "Storage.EndPeriod.Value", 90));
            topPnl1.Children.Add(panel);
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
            topPnl2.Children.Add(CreateTextBox("5,12,0,0", 1, 30, "Storage.CorrectionNumber.Value", 70));
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
