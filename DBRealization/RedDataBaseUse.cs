﻿using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.IO;

namespace DBRealization
{
    public class RedDataBaseUse
    {
        public string ConnectionString { get; set; }
        public RedDataBaseUse(string _path)
        {
            int pageSize = 4096;
            bool forcedWrites = true;
            bool overwrite = true;
            var pth = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
            var connectionString = new FbConnectionStringBuilder
            {
                Database = _path,
                ServerType = FbServerType.Embedded,
                UserID = "SYSDBA",
                Password = "masterkey",
                Role = "ADMIN",
                ClientLibrary = pth + "REDDB\\fbclient.dll"
            }.ToString();
            ConnectionString = connectionString;

            var direct = Path.GetDirectoryName(_path);
            if (!Directory.Exists(direct))
            {
                Directory.CreateDirectory(direct);
            }
            if (!System.IO.File.Exists(_path))
            {
                FbConnection.CreateDatabase(connectionString, pageSize, forcedWrites, overwrite);
                Init();
            }
        }

        public object DoCommand(string Query, Func<FbDataReader, List<object[]>> Func)
        {
            using (FbConnection connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new FbCommand(Query, connection, transaction))
                    {
                        if (Func == null)
                        {
                            command.ExecuteNonQuery();
                            return null;
                        }
                        else
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                return Func.Invoke(reader);
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        void Init()
        {
            DoCommand("create table reports ( reports_id integer not null, report_id integer not null)", null);

            DoCommand("create table reports_data ( reports_id integer generated by default as identity primary key, masterreport_id integer not null, " + SQLFormConsts.Reports() + ")", null);

            DoCommand("create table report ( report_id integer generated by default as identity primary key, row_id integer not null)", null);

            DoCommand("create table report_data ( form_id integer generated by default as identity primary key, form_type varchar(255) not null, " + SQLFormConsts.Report() + ")", null);

            DoCommand("create table forms_10 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form10() + ")", null);
            DoCommand("create table forms_11 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form11() + ")", null);
            DoCommand("create table forms_12 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form12() + ")", null);
            DoCommand("create table forms_13 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form13() + ")", null);
            DoCommand("create table forms_14 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form14() + ")", null);
            DoCommand("create table forms_15 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form15() + ")", null);
            DoCommand("create table forms_16 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form16() + ")", null);
            DoCommand("create table forms_17 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form17() + ")", null);
            DoCommand("create table forms_18 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form18() + ")", null);
            DoCommand("create table forms_19 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form19() + ")", null);

            DoCommand("create table forms_20 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form20() + ")", null);
            DoCommand("create table forms_21 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form21() + ")", null);
            DoCommand("create table forms_22 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form22() + ")", null);
            DoCommand("create table forms_23 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form23() + ")", null);
            DoCommand("create table forms_24 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form24() + ")", null);
            DoCommand("create table forms_25 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form25() + ")", null);
            DoCommand("create table forms_26 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form26() + ")", null);
            DoCommand("create table forms_27 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form27() + ")", null);
            DoCommand("create table forms_28 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form28() + ")", null);
            DoCommand("create table forms_29 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form29() + ")", null);
            DoCommand("create table forms_210 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form210() + ")", null);
            DoCommand("create table forms_211 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form211() + ")", null);
            DoCommand("create table forms_212 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form212() + ")", null);

            DoCommand("create table forms_30 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form30() + ")", null);
            DoCommand("create table forms_31 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form31() + ")", null);
            DoCommand("create table forms_31_1 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form31_1() + ")", null);
            DoCommand("create table forms_32 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form32() + ")", null);
            DoCommand("create table forms_32_1 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form32_1() + ")", null);
            DoCommand("create table forms_32_2 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form32_2() + ")", null);
            DoCommand("create table forms_32_3 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form32_3() + ")", null);

            DoCommand("create table forms_40 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form40() + ")", null);
            DoCommand("create table forms_41 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form41() + ")", null);
            
            DoCommand("create table forms_50 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form50() + ")", null);
            DoCommand("create table forms_51 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form51() + ")", null);
            DoCommand("create table forms_52 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form52() + ")", null);
            DoCommand("create table forms_53 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form53() + ")", null);
            DoCommand("create table forms_54 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form54() + ")", null);
            DoCommand("create table forms_55 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form55() + ")", null);
            DoCommand("create table forms_56 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form56() + ")", null);
            DoCommand("create table forms_57 (row_id integer generated by default as identity primary key, " + SQLFormConsts.Form57() + ")", null);
        }
    }
}
