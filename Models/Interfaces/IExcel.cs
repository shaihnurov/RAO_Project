﻿using OfficeOpenXml;

namespace Models.Interfaces;

public interface IExcel
{
    int ExcelRow(ExcelWorksheet worksheet,int Row,int Column,bool Tranpon=true, string SumNumber = "");
    void ExcelGetRow(ExcelWorksheet worksheet, int Row);
}