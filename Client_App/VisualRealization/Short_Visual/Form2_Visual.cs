﻿using Avalonia.Controls;

namespace Client_App.Short_Visual
{
    public class Form2_Visual
    {
        public static void FormF_Visual(in Panel pnl0, in Panel pnlx, in Panel pnlb)
        {
            pnl0.Children.Add(Form0_Visual());
            pnlx.Children.Add(FormX_Visual());
            pnlb.Children.Add(FormB_Visual());
        }

        //Форма 10
        static DataGrid Form0_Visual()
        {
            DataGrid grd = new DataGrid();

            return grd;
        }

        //Форма 1X
        static DataGrid FormX_Visual()
        {
            DataGrid grd = new DataGrid();

            return grd;
        }

        //Кнопки создания или изменения формы
        static Panel FormB_Visual()
        {
            Panel panel = new Panel();

            return panel;
        }
    }
}
