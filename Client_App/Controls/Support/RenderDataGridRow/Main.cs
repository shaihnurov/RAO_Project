﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using System;

namespace Client_App.Controls.Support.RenderDataGridRow
{
    public class Main
    {
        public static Control GetControl(string type, int Row, INameScope scp, string TopName)
        {
            switch (type)
            {
                case "0": return Get0(Row, scp, TopName);
                case "1": return Get1(Row, scp, TopName);
                case "2": return Get2();
                case "3": return Get3();
                case "4": return Get4();
                case "5": return Get5();
            }
            return null;
        }

        private static readonly int Wdth0 = 100;
        private static readonly int RowHeight0 = 30;
        private static readonly Color border_color0 = Color.FromArgb(255, 0, 0, 0);

        private static Control Get0Row(int starWidth, int Row, int Column, string Binding, INameScope scp, string TopName)
        {
            DataGrid.Cell? cell = new Controls.DataGrid.Cell(Binding, true)
            {
                Width = starWidth * Wdth0,
                Height = RowHeight0,
                BorderBrush = new SolidColorBrush(border_color0)
            };

            Binding b = new Binding
            {
                Path = "Items[" + (Row - 1).ToString() + "]." + Binding,
                ElementName = TopName,
                NameScope = new WeakReference<INameScope>(scp)
            };

            cell.Bind(DataGrid.Cell.DataContextProperty, b);

            cell.CellRow = Row;
            cell.CellColumn = Column;


            return cell;
        }

        private static Control Get0(int Row, INameScope scp, string TopName)
        {
            DataGrid.Row stck = new DataGrid.Row
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Width = 4 * Wdth0,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                Spacing = -1
            };

            Binding b = new Binding
            {
                Path = "Items[" + (Row - 1).ToString() + "]",
                ElementName = TopName,
                NameScope = new WeakReference<INameScope>(scp)
            };

            stck.Bind(StackPanel.DataContextProperty, b);

            stck.Children.Add(Get0Row(1, Row, 1, "Master.Value.RegNo", scp, TopName));
            stck.Children.Add(Get0Row(2, Row, 2, "Master.Value.ShortJurLico", scp, TopName));
            stck.Children.Add(Get0Row(1, Row, 3, "Master.Value.Okpo", scp, TopName));

            return stck;
        }

        private static readonly int Wdth1 = 100;
        private static readonly int RowHeight1 = 30;
        private static readonly Color border_color1 = Color.FromArgb(255, 0, 0, 0);

        private static Control Get1Row(int starWidth, int Row, int Column, string Binding, INameScope scp, string TopName)
        {
            DataGrid.Cell? cell = new Controls.DataGrid.Cell(Binding, true)
            {
                Width = starWidth * Wdth1,
                Height = RowHeight1,
                BorderBrush = new SolidColorBrush(border_color1)
            };

            Binding b = new Binding
            {
                Path = "Items[" + (Row - 1).ToString() + "]." + Binding,
                ElementName = TopName,
                NameScope = new WeakReference<INameScope>(scp)
            };

            cell.Bind(DataGrid.Cell.DataContextProperty, b);

            cell.CellRow = Row;
            cell.CellColumn = Column;

            return cell;
        }

        private static Control Get1(int Row, INameScope scp, string TopName)
        {
            DataGrid.Row stck = new DataGrid.Row
            {
                Width = 8 * Wdth1,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = -1
            };

            Binding b = new Binding
            {
                Path = "Items[" + (Row - 1).ToString() + "]",
                ElementName = TopName,
                NameScope = new WeakReference<INameScope>(scp)
            };

            stck.Bind(StackPanel.DataContextProperty, b);


            stck.Children.Add(Get1Row(1, Row, 1, "NumberInOrder", scp, TopName));
            stck.Children.Add(Get1Row(1, Row, 2, "FormNum", scp, TopName));

            string? str = "{0:d}";
            stck.Children.Add(Get1Row(1, Row, 3, "StartPeriod", scp, TopName));
            stck.Children.Add(Get1Row(1, Row, 4, "EndPeriod", scp, TopName));
            stck.Children.Add(Get1Row(1, Row, 5, "ExportDate", scp, TopName));
            stck.Children.Add(Get1Row(2, Row, 6, "IsCorrection", scp, TopName));
            stck.Children.Add(Get1Row(1, Row, 7, "Comments", scp, TopName));

            return stck;
        }

        private static Control Get2()
        {
            return null;
        }

        private static Control Get3()
        {
            return null;
        }

        private static Control Get4()
        {
            return null;
        }

        private static Control Get5()
        {
            return null;
        }
    }
}
