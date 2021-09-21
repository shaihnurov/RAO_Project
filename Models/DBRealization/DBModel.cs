﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Models.DataAccess;
using Models;

namespace DBRealization
{
    public class DBModel : DbContext
    {
        public string _path { get; set; }
        public DBModel(string Path)
        {
            _path = Path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseFirebird(RedDataBaseCreation.GetConnectionString(_path));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Collections.DBObservable>()
                .ToTable("DBObservable_DbSet");

            modelBuilder.Entity<Collections.Reports>()
                .ToTable("ReportsCollection_DbSet");
            modelBuilder.Entity<Collections.Report>()
                .ToTable("ReportCollection_DbSet");
            modelBuilder.Entity<Models.Note>()
                .ToTable("notes");

            modelBuilder.Entity<Models.Form10>()
                .ToTable("form_10");
            modelBuilder.Entity<Models.Form11>()
                .ToTable("form_11");
            modelBuilder.Entity<Models.Form12>()
                .ToTable("form_12");
            modelBuilder.Entity<Models.Form13>()
                .ToTable("form_13");
            modelBuilder.Entity<Models.Form14>()
                .ToTable("form_14");
            modelBuilder.Entity<Models.Form15>()
                .ToTable("form_15");
            modelBuilder.Entity<Models.Form16>()
                .ToTable("form_16");
            modelBuilder.Entity<Models.Form17>()
                .ToTable("form_17");
            modelBuilder.Entity<Models.Form18>()
                .ToTable("form_18");
            modelBuilder.Entity<Models.Form19>()
                .ToTable("form_19");

            modelBuilder.Entity<Models.Form20>()
                .ToTable("form_20");
            modelBuilder.Entity<Models.Form21>()
                .ToTable("form_21");
            modelBuilder.Entity<Models.Form22>()
                .ToTable("form_22");
            modelBuilder.Entity<Models.Form23>()
                .ToTable("form_23");
            modelBuilder.Entity<Models.Form24>()
                .ToTable("form_24");
            modelBuilder.Entity<Models.Form25>()
                .ToTable("form_25");
            modelBuilder.Entity<Models.Form26>()
                .ToTable("form_26");
            modelBuilder.Entity<Models.Form27>()
                .ToTable("form_27");
            modelBuilder.Entity<Models.Form28>()
                .ToTable("form_28");
            modelBuilder.Entity<Models.Form29>()
                .ToTable("form_29");
            modelBuilder.Entity<Models.Form210>()
                .ToTable("form_210");
            modelBuilder.Entity<Models.Form211>()
                .ToTable("form_211");
            modelBuilder.Entity<Models.Form212>()
                .ToTable("form_212");
        }

        public void LoadTables()
        {
            notes.Load();
            form_10.Load();
            form_11.Load();
            form_12.Load();
            form_13.Load();
            form_14.Load();
            form_15.Load();
            form_16.Load();
            form_17.Load();
            form_18.Load();
            form_19.Load();

            form_20.Load();
            form_21.Load();
            form_22.Load();
            form_23.Load();
            form_24.Load();
            form_25.Load();
            form_26.Load();
            form_27.Load();
            form_28.Load();
            form_29.Load();
            form_210.Load();
            form_211.Load();
            form_212.Load();

            ReportCollectionDbSet.Load();
            ReportsCollectionDbSet.Load();
            DBObservableDbSet.Load();
        }

        public DbSet<Collections.DBObservable> DBObservableDbSet { get; set; }
        public DbSet<Collections.Reports> ReportsCollectionDbSet { get; set; }
        public DbSet<Collections.Report> ReportCollectionDbSet { get; set; }
        public DbSet<Models.Note> notes { get; set; }

        public DbSet<Models.Form10> form_10 { get; set; }
        public DbSet<Models.Form11> form_11 { get; set; }
        public DbSet<Models.Form12> form_12 { get; set; }
        public DbSet<Models.Form13> form_13 { get; set; }
        public DbSet<Models.Form14> form_14 { get; set; }
        public DbSet<Models.Form15> form_15 { get; set; }
        public DbSet<Models.Form16> form_16 { get; set; }
        public DbSet<Models.Form17> form_17 { get; set; }
        public DbSet<Models.Form18> form_18 { get; set; }
        public DbSet<Models.Form19> form_19 { get; set; }

        public DbSet<Models.Form20> form_20 { get; set; }
        public DbSet<Models.Form21> form_21 { get; set; }
        public DbSet<Models.Form22> form_22 { get; set; }
        public DbSet<Models.Form23> form_23 { get; set; }
        public DbSet<Models.Form24> form_24 { get; set; }
        public DbSet<Models.Form25> form_25 { get; set; }
        public DbSet<Models.Form26> form_26 { get; set; }
        public DbSet<Models.Form27> form_27 { get; set; }
        public DbSet<Models.Form28> form_28 { get; set; }
        public DbSet<Models.Form29> form_29 { get; set; }
        public DbSet<Models.Form210> form_210 { get; set; }
        public DbSet<Models.Form211> form_211 { get; set; }
        public DbSet<Models.Form212> form_212 { get; set; }
    }
}
