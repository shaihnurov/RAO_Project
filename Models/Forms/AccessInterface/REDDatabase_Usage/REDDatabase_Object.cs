﻿using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.IO;

namespace Models.AccessInterface.REDDatabase_Usage
{
    public class REDDatabase_Object
    {
        public string ConnectionString { get; set; }
        public REDDatabase_Object(string _path)
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
                Role="ADMIN",
                ClientLibrary = pth + "REDDB\\fbclient.dll"
            }.ToString();
            ConnectionString = connectionString;

            var direct=Path.GetDirectoryName(_path);
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

        void Init()
        {
            using (FbConnection connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new FbCommand("create table reports_forms ( report_id integer not null, form_id integer not null)", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();

                    using (var command = new FbCommand("create table reports ( report_id integer not null AUTO_INCREMENT, masterform_id integer not null)", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();

                    using (var command = new FbCommand("create table forms ( form_id integer not null AUTO_INCREMENT, form_type varchar(255) not null)", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();

                    using (var command = new FbCommand("create table rows ( form_id integer not null, row_id integer not null)", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();

                    using (var command = new FbCommand("create table forms_10 ("+Form10.SQLCommandParams()+")", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();

                    using (var command = new FbCommand("create table forms_11 (" + Form11.SQLCommandParams() + ")", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
