﻿using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Models.DBRealization
{
    public class RedDataBaseCreation
    {
        public static string GetConnectionString(string _path)
        {
            string direct = Path.GetDirectoryName(_path);
            if (!Directory.Exists(direct))
            {
                Directory.CreateDirectory(direct);
            }
#if DEBUG
            string pth = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\")), "data"), "REDDB"), "win-x64"), "fbclient.dll");
                }
                if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\")), "data"), "REDDB"), "win-x32"), "fbclient.dll");
                }
            }
            else
            {
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\")), "data"), "REDDB"), "linux-x64"), "lib"), "libfbclient.so");
                }
                if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\")), "data"), "REDDB"), "linux-x32"), "lib"), "libfbclient.so");
                }
            }
#else
            string pth = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), "data"), "REDDB"), "win-x64"), "fbclient.dll");
                }
                if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), "data"), "REDDB"), "win-x32"), "fbclient.dll");
                }
            }
            else
            {
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), "data"), "REDDB"), "linux-x64"), "lib"), "libfbclient.so");
                }
                if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), "data"), "REDDB"), "linux-x32"), "lib"), "libfbclient.so");
                }
            }
#endif
            Console.WriteLine(_path);
            return new FbConnectionStringBuilder
            {
                
                Database = _path,
                ServerType = FbServerType.Embedded,
                UserID = "SYSDBA",
                Password = "masterkey",
                Role = "ADMIN",
                ClientLibrary = Path.GetFullPath(pth)
            }.ToString();
        }
        public static void CreateDB(string _path)
        {
            int pageSize = 4096;
            bool forcedWrites = true;
            bool overwrite = true;
            string connectionString = GetConnectionString(_path);

            string direct = Path.GetDirectoryName(_path);
            if (!Directory.Exists(direct))
            {
                Directory.CreateDirectory(direct);
            }
            if (!System.IO.File.Exists(_path))
            {
                FbConnection.CreateDatabase(connectionString, pageSize, forcedWrites, overwrite);
                //Init(connectionString);
            }
        }

        public static object DoCommand(string Query, Func<FbDataReader, List<object[]>> Func, string ConnectionString)
        {
            using (FbConnection connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                using (FbTransaction transaction = connection.BeginTransaction())
                {
                    using (FbCommand command = new FbCommand(Query, connection, transaction))
                    {
                        if (Func == null)
                        {
                            command.ExecuteNonQuery();
                            return null;
                        }
                        else
                        {
                            using (FbDataReader reader = command.ExecuteReader())
                            {
                                return Func.Invoke(reader);
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        private static string ComaInit(string input)
        {
            if (input == "")
            {
                return "";
            }
            else
            {
                return ", " + input;
            }
        }

        private static void Init(string ConnectionString)
        {
            DoCommand("create table reports ( reports_id integer not null, report_id integer not null)", null, ConnectionString);

            DoCommand("create table reports_data ( reports_id integer generated by default as identity primary key, masterreport_id integer not null " + ComaInit(ComaInit(SQLFormConsts.Reports())) + ")", null, ConnectionString);

            DoCommand("create table report ( report_id integer generated by default as identity primary key, row_id integer not null)", null, ConnectionString);

            DoCommand("create table report_data ( report_id integer generated by default as identity primary key, form_type varchar(255) not null " + ComaInit(SQLFormConsts.Report()) + ")", null, ConnectionString);

            DoCommand("create table forms_10 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form10()) + ")", null, ConnectionString);
            DoCommand("create table forms_11 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form11()) + ")", null, ConnectionString);
            DoCommand("create table forms_12 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form12()) + ")", null, ConnectionString);
            DoCommand("create table forms_13 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form13()) + ")", null, ConnectionString);
            DoCommand("create table forms_14 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form14()) + ")", null, ConnectionString);
            DoCommand("create table forms_15 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form15()) + ")", null, ConnectionString);
            DoCommand("create table forms_16 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form16()) + ")", null, ConnectionString);
            DoCommand("create table forms_17 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form17()) + ")", null, ConnectionString);
            DoCommand("create table forms_18 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form18()) + ")", null, ConnectionString);
            DoCommand("create table forms_19 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form19()) + ")", null, ConnectionString);

            DoCommand("create table forms_20 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form20()) + ")", null, ConnectionString);
            DoCommand("create table forms_21 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form21()) + ")", null, ConnectionString);
            DoCommand("create table forms_22 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form22()) + ")", null, ConnectionString);
            DoCommand("create table forms_23 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form23()) + ")", null, ConnectionString);
            DoCommand("create table forms_24 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form24()) + ")", null, ConnectionString);
            DoCommand("create table forms_25 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form25()) + ")", null, ConnectionString);
            DoCommand("create table forms_26 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form26()) + ")", null, ConnectionString);
            DoCommand("create table forms_27 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form27()) + ")", null, ConnectionString);
            DoCommand("create table forms_28 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form28()) + ")", null, ConnectionString);
            DoCommand("create table forms_29 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form29()) + ")", null, ConnectionString);
            DoCommand("create table forms_210 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form210()) + ")", null, ConnectionString);
            DoCommand("create table forms_211 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form211()) + ")", null, ConnectionString);
            DoCommand("create table forms_212 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form212()) + ")", null, ConnectionString);

            DoCommand("create table forms_30 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form30()) + ")", null, ConnectionString);
            DoCommand("create table forms_31 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form31()) + ")", null, ConnectionString);
            DoCommand("create table forms_31_1 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form31_1()) + ")", null, ConnectionString);
            DoCommand("create table forms_32 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form32()) + ")", null, ConnectionString);
            DoCommand("create table forms_32_1 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form32_1()) + ")", null, ConnectionString);
            DoCommand("create table forms_32_2 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form32_2()) + ")", null, ConnectionString);
            DoCommand("create table forms_32_3 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form32_3()) + ")", null, ConnectionString);

            DoCommand("create table forms_40 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form40()) + ")", null, ConnectionString);
            DoCommand("create table forms_41 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form41()) + ")", null, ConnectionString);

            DoCommand("create table forms_50 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form50()) + ")", null, ConnectionString);
            DoCommand("create table forms_51 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form51()) + ")", null, ConnectionString);
            DoCommand("create table forms_52 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form52()) + ")", null, ConnectionString);
            DoCommand("create table forms_53 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form53()) + ")", null, ConnectionString);
            DoCommand("create table forms_54 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form54()) + ")", null, ConnectionString);
            DoCommand("create table forms_55 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form55()) + ")", null, ConnectionString);
            DoCommand("create table forms_56 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form56()) + ")", null, ConnectionString);
            DoCommand("create table forms_57 (row_id integer generated by default as identity primary key " + ComaInit(SQLFormConsts.Form57()) + ")", null, ConnectionString);
        }
    }
}
