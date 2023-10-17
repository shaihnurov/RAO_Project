﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Client_App.ViewModels;
using Models.Collections;
using Models.Forms.Form1;
using Models.Forms;
using Models.JSON;
using Newtonsoft.Json;
using Avalonia.Controls;
using Client_App.Interfaces.Logger;
using Client_App.Resources;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;
using Models.DBRealization;
using Models.DTO;
using Models.Forms.Form2;
using System.Reflection.Metadata;
using System.Diagnostics;

namespace Client_App.Commands.AsyncCommands.Import;

public class ImportJsonAsyncCommand : ImportBaseAsyncCommand
{
    public override async Task AsyncExecute(object? parameter)
    {
        IsFirstLogLine = true;
        CurrentLogLine = 1;
        string[] extensions = { "json", "JSON" };
        var answer = await GetSelectedFilesFromDialog("JSON", extensions);
        if (answer is null) return;
        var countReadFiles = answer.Length;
        var countNewReps = 0;
        var countNewRep = 0;
        SkipNewOrg = false;
        SkipInter = false;
        SkipLess = false;
        SkipNew = false;
        SkipReplace = false;
        HasMultipleReport = false;
        AtLeastOneImportDone = false;
        
        foreach (var path in answer) // Для каждого импортируемого файла JSON
        {
            try
            {
                if (path == "") continue;
                var file = GetRaoFileName();
                SourceFile = new FileInfo(path);
                SourceFile.CopyTo(file, true);
                var jsonString = await File.ReadAllTextAsync(file);
                var jsonObject = JsonConvert.DeserializeObject<JsonModel>(jsonString)!;
                List<Reports> reportsJsonCollection = new();

                foreach (var reps in jsonObject.Orgs)   // Для каждой организации в файле (получаем лист импортируемых организаций)
                {
                    #region GetImpRepsAndAddToRepsCollection

                    string formNumReps;
                    if (jsonObject.Forms.Any(form => form != null && form.FormNum.StartsWith('1')))
                    {
                        formNumReps = "1.0";
                    }
                    else if (jsonObject.Forms.Any(form => form != null && form.FormNum.StartsWith('2')))
                    {
                        formNumReps = "2.0";
                    }
                    else return;

                    var ty1 = FormCreator.Create(formNumReps);
                    ty1.NumberInOrder_DB = 1;
                    var ty2 = FormCreator.Create(formNumReps);
                    ty2.NumberInOrder_DB = 2;

                    var impReps = new Reports
                    {
                        Master_DB = new Report
                        {
                            FormNum_DB = formNumReps
                        },
                        Id = reps[0].Id
                    };
                    switch (formNumReps)
                    {
                        case "1.0":
                        {
                            #region Bindings

                            impReps.Master_DB.Rows10.Add(ty1);
                            impReps.Master_DB.Rows10.Add(ty2);

                            impReps.Master_DB.Rows10[0].RegNo_DB = reps[0].RegNo;
                            impReps.Master_DB.Rows10[0].OrganUprav_DB = reps[0].OrganUprav;
                            impReps.Master_DB.Rows10[0].SubjectRF_DB = reps[0].SubjectRF;
                            impReps.Master_DB.Rows10[0].JurLico_DB = reps[0].JurLico;
                            impReps.Master_DB.Rows10[0].ShortJurLico_DB = reps[0].ShortJurLico;
                            impReps.Master_DB.Rows10[0].JurLicoAddress_DB = reps[0].JurLicoAddress;
                            impReps.Master_DB.Rows10[0].JurLicoFactAddress_DB = reps[0].JurLicoFactAddress;
                            impReps.Master_DB.Rows10[0].GradeFIO_DB = reps[0].GradeFIO;
                            impReps.Master_DB.Rows10[0].Telephone_DB = reps[0].Telephone;
                            impReps.Master_DB.Rows10[0].Fax_DB = reps[0].Fax;
                            impReps.Master_DB.Rows10[0].Email_DB = reps[0].Email;
                            impReps.Master_DB.Rows10[0].Okpo_DB = reps[0].Okpo;
                            impReps.Master_DB.Rows10[0].Okved_DB = reps[0].Okved;
                            impReps.Master_DB.Rows10[0].Okogu_DB = reps[0].Okogu;
                            impReps.Master_DB.Rows10[0].Oktmo_DB = reps[0].Oktmo;
                            impReps.Master_DB.Rows10[0].Inn_DB = reps[0].Inn;
                            impReps.Master_DB.Rows10[0].Kpp_DB = reps[0].Kpp;
                            impReps.Master_DB.Rows10[0].Okopf_DB = reps[0].Okopf;
                            impReps.Master_DB.Rows10[0].Okfs_DB = reps[0].Okfs;
                            if (reps.Length > 1)
                            {
                                impReps.Master_DB.Rows10[1].RegNo_DB = reps[1].RegNo;
                                impReps.Master_DB.Rows10[1].OrganUprav_DB = reps[1].OrganUprav;
                                impReps.Master_DB.Rows10[1].SubjectRF_DB = reps[1].SubjectRF;
                                impReps.Master_DB.Rows10[1].JurLico_DB = reps[1].JurLico;
                                impReps.Master_DB.Rows10[1].ShortJurLico_DB = reps[1].ShortJurLico;
                                impReps.Master_DB.Rows10[1].JurLicoAddress_DB = reps[1].JurLicoAddress;
                                impReps.Master_DB.Rows10[1].GradeFIO_DB = reps[1].GradeFIO;
                                impReps.Master_DB.Rows10[1].Telephone_DB = reps[1].Telephone;
                                impReps.Master_DB.Rows10[1].Fax_DB = reps[1].Fax;
                                impReps.Master_DB.Rows10[1].Email_DB = reps[1].Email;
                                impReps.Master_DB.Rows10[1].Okpo_DB = reps[1].Okpo;
                                impReps.Master_DB.Rows10[1].Okpo_DB = reps[1].Okpo;
                                impReps.Master_DB.Rows10[1].Okved_DB = reps[1].Okved;
                                impReps.Master_DB.Rows10[1].Okogu_DB = reps[1].Okogu;
                                impReps.Master_DB.Rows10[1].Oktmo_DB = reps[1].Oktmo;
                                impReps.Master_DB.Rows10[1].Inn_DB = reps[1].Inn;
                                impReps.Master_DB.Rows10[1].Kpp_DB = reps[1].Kpp;
                                impReps.Master_DB.Rows10[1].Okopf_DB = reps[1].Okopf;
                                impReps.Master_DB.Rows10[1].Okfs_DB = reps[1].Okfs;
                            }

                            #endregion

                            break;
                        }
                        case "2.0":
                        {
                            #region Bindings

                            impReps.Master_DB.Rows20.Add(ty1);
                            impReps.Master_DB.Rows20.Add(ty2);

                            impReps.Master_DB.Rows20[0].RegNo_DB = reps[0].RegNo;
                            impReps.Master_DB.Rows20[0].OrganUprav_DB = reps[0].OrganUprav;
                            impReps.Master_DB.Rows20[0].SubjectRF_DB = reps[0].SubjectRF;
                            impReps.Master_DB.Rows20[0].JurLico_DB = reps[0].JurLico;
                            impReps.Master_DB.Rows20[0].ShortJurLico_DB = reps[0].ShortJurLico;
                            impReps.Master_DB.Rows20[0].JurLicoAddress_DB = reps[0].JurLicoAddress;
                            impReps.Master_DB.Rows20[0].JurLicoFactAddress_DB = reps[0].JurLicoFactAddress;
                            impReps.Master_DB.Rows20[0].GradeFIO_DB = reps[0].GradeFIO;
                            impReps.Master_DB.Rows20[0].Telephone_DB = reps[0].Telephone;
                            impReps.Master_DB.Rows20[0].Fax_DB = reps[0].Fax;
                            impReps.Master_DB.Rows20[0].Email_DB = reps[0].Email;
                            impReps.Master_DB.Rows20[0].Okpo_DB = reps[0].Okpo;
                            impReps.Master_DB.Rows20[0].Okved_DB = reps[0].Okved;
                            impReps.Master_DB.Rows20[0].Okogu_DB = reps[0].Okogu;
                            impReps.Master_DB.Rows20[0].Oktmo_DB = reps[0].Oktmo;
                            impReps.Master_DB.Rows20[0].Inn_DB = reps[0].Inn;
                            impReps.Master_DB.Rows20[0].Kpp_DB = reps[0].Kpp;
                            impReps.Master_DB.Rows20[0].Okopf_DB = reps[0].Okopf;
                            impReps.Master_DB.Rows20[0].Okfs_DB = reps[0].Okfs;
                            if (reps.Length > 1)
                            {
                                impReps.Master_DB.Rows20[1].RegNo_DB = reps[1].RegNo;
                                impReps.Master_DB.Rows20[1].OrganUprav_DB = reps[1].OrganUprav;
                                impReps.Master_DB.Rows20[1].SubjectRF_DB = reps[1].SubjectRF;
                                impReps.Master_DB.Rows20[1].JurLico_DB = reps[1].JurLico;
                                impReps.Master_DB.Rows20[1].ShortJurLico_DB = reps[1].ShortJurLico;
                                impReps.Master_DB.Rows20[1].JurLicoAddress_DB = reps[1].JurLicoAddress;
                                impReps.Master_DB.Rows20[1].GradeFIO_DB = reps[1].GradeFIO;
                                impReps.Master_DB.Rows20[1].Telephone_DB = reps[1].Telephone;
                                impReps.Master_DB.Rows20[1].Fax_DB = reps[1].Fax;
                                impReps.Master_DB.Rows20[1].Email_DB = reps[1].Email;
                                impReps.Master_DB.Rows20[1].Okpo_DB = reps[1].Okpo;
                                impReps.Master_DB.Rows20[1].Okpo_DB = reps[1].Okpo;
                                impReps.Master_DB.Rows20[1].Okved_DB = reps[1].Okved;
                                impReps.Master_DB.Rows20[1].Okogu_DB = reps[1].Okogu;
                                impReps.Master_DB.Rows20[1].Oktmo_DB = reps[1].Oktmo;
                                impReps.Master_DB.Rows20[1].Inn_DB = reps[1].Inn;
                                impReps.Master_DB.Rows20[1].Kpp_DB = reps[1].Kpp;
                                impReps.Master_DB.Rows20[1].Okopf_DB = reps[1].Okopf;
                                impReps.Master_DB.Rows20[1].Okfs_DB = reps[1].Okfs;
                            }

                            #endregion

                            break;
                        }
                    }

                    reportsJsonCollection.Add(impReps);

                    #endregion
                }

                foreach (var rep in jsonObject.Forms.Where(rep => rep is not null)) // Для каждого отчета, добавляем её к соответствующей организации в листе
                {
                    #region GetImpFormsAndAddToRepsCollection

                    var currentOrg = reportsJsonCollection.FirstOrDefault(reps => reps.Id == rep.ReportsId);
                    if (currentOrg is null) continue;

                    #region GetCreationTime
                    
                    var timeCreate = new List<string>()
                    {
                        SourceFile.CreationTime.Day.ToString(),
                        SourceFile.CreationTime.Month.ToString(),
                        SourceFile.CreationTime.Year.ToString()
                    };
                    if (timeCreate[0].Length == 1)
                    {
                        timeCreate[0] = $"0{timeCreate[0]}";
                    }

                    if (timeCreate[1].Length == 1)
                    {
                        timeCreate[1] = $"0{timeCreate[1]}";
                    }

                    #endregion

                    var impRep = new Report
                    {
                        FormNum_DB = rep.FormNum,
                        ExportDate_DB = $"{timeCreate[0]}.{timeCreate[1]}.{timeCreate[2]}",
                        CorrectionNumber_DB = rep.CorrectionNumber
                    };
                    if (impRep.FormNum_DB.StartsWith('1'))
                    {
                        impRep.StartPeriod_DB = rep.StartPeriod;
                        impRep.EndPeriod_DB = rep.EndPeriod;
                    } 
                    else if (impRep.FormNum_DB.StartsWith('2'))
                    {
                        impRep.Year_DB = rep.Year;
                    }
                    var numberInOrder = 1;

                    switch (rep.FormNum)
                    {
                        #region Form1

                        #region Form11

                        case "1.1":
                            {
                                var repForm = (JsonForm11)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows11.Add(new Form11
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        PassportNumber_DB = form.PassportNumber,
                                        Type_DB = form.Type,
                                        Radionuclids_DB = form.Radionuclids,
                                        FactoryNumber_DB = form.Radionuclids,
                                        Quantity_DB = int.TryParse(form.Quantity, out var intValue) ? intValue : null,
                                        Activity_DB = form.Activity,
                                        CreationDate_DB = form.CreationDate,
                                        CreatorOKPO_DB = form.CreatorOKPO,
                                        Category_DB = short.TryParse(form.Category, out var shortValue) ? shortValue : null,
                                        SignedServicePeriod_DB = float.TryParse(form.SignedServicePeriod, out var floatValue)
                                            ? floatValue
                                            : null,
                                        PropertyCode_DB = byte.TryParse(form.PropertyCode, out var byteValue)
                                            ? byteValue
                                            : null,
                                        Owner_DB = form.Owner,
                                        ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO,
                                        TransporterOKPO_DB = form.TransporterOKPO,
                                        PackName_DB = form.PackName,
                                        PackType_DB = form.PackType,
                                        PackNumber_DB = form.PackNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form12

                        case "1.2":
                            {
                                var repForm = (JsonForm12)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows12.Add(new Form12
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        PassportNumber_DB = form.PassportNumber,
                                        NameIOU_DB = form.NameIOU,
                                        FactoryNumber_DB = form.FactoryNumber,
                                        Mass_DB = form.Mass,
                                        CreatorOKPO_DB = form.CreatorOKPO,
                                        CreationDate_DB = form.CreationDate,
                                        SignedServicePeriod_DB = form.SignedServicePeriod,
                                        PropertyCode_DB = byte.TryParse(form.SignedServicePeriod, out var byteValue)
                                            ? byteValue
                                            : null,
                                        Owner_DB = form.Owner,
                                        DocumentVid_DB = byte.TryParse(form.DocumentVid, out byteValue)
                                            ? byteValue
                                            : null,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO,
                                        TransporterOKPO_DB = form.TransporterOKPO,
                                        PackName_DB = form.PackName,
                                        PackType_DB = form.PackType,
                                        PackNumber_DB = form.PackNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form13

                        case "1.3":
                            {
                                var repForm = (JsonForm13)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows13.Add(new Form13
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        PassportNumber_DB = form.PassportNumber,
                                        Type_DB = form.Type,
                                        Radionuclids_DB = form.Radionuclids,
                                        FactoryNumber_DB = form.FactoryNumber,
                                        Activity_DB = form.Activity,
                                        CreatorOKPO_DB = form.CreatorOKPO,
                                        CreationDate_DB = form.CreationDate,
                                        AggregateState_DB = byte.TryParse(form.AggregateState, out var byteValue)
                                            ? byteValue
                                            : null,
                                        PropertyCode_DB = byte.TryParse(form.PropertyCode, out byteValue)
                                            ? byteValue
                                            : null,
                                        Owner_DB = form.Owner,
                                        DocumentVid_DB = byte.TryParse(form.DocumentVid, out byteValue)
                                            ? byteValue
                                            : null,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO,
                                        TransporterOKPO_DB = form.TransporterOKPO,
                                        PackName_DB = form.PackName,
                                        PackType_DB = form.PackType,
                                        PackNumber_DB = form.PackNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form14

                        case "1.4":
                            {
                                var repForm = (JsonForm14)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows14.Add(new Form14
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        PassportNumber_DB = form.PassportNumber,
                                        Name_DB = form.Name,
                                        Sort_DB = byte.TryParse(form.Sort, out var byteValue)
                                            ? byteValue
                                            : null,
                                        Radionuclids_DB = form.Radionuclids,
                                        Activity_DB = form.Activity,
                                        ActivityMeasurementDate_DB = form.ActivityMeasurementDate,
                                        Volume_DB = form.Volume,
                                        Mass_DB = form.Mass,
                                        AggregateState_DB = byte.TryParse(form.AggregateState, out byteValue)
                                            ? byteValue
                                            : null,
                                        PropertyCode_DB = byte.TryParse(form.PropertyCode, out byteValue)
                                            ? byteValue
                                            : null,
                                        Owner_DB = form.Owner,
                                        DocumentVid_DB = byte.TryParse(form.DocumentVid, out byteValue)
                                            ? byteValue
                                            : null,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO,
                                        TransporterOKPO_DB = form.TransporterOKPO,
                                        PackName_DB = form.PackName,
                                        PackType_DB = form.PackType,
                                        PackNumber_DB = form.PackNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form15

                        case "1.5":
                            {
                                var repForm = (JsonForm15)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows15.Add(new Form15
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        PassportNumber_DB = form.PassportNumber,
                                        Type_DB = form.Type,
                                        Radionuclids_DB = form.Radionuclids,
                                        FactoryNumber_DB = form.FactoryNumber,
                                        Quantity_DB = int.TryParse(form.Quantity, out var intValue)
                                            ? intValue
                                            : null,
                                        Activity_DB = form.Activity,
                                        CreationDate_DB = form.CreationDate,
                                        StatusRAO_DB = form.StatusRAO,
                                        DocumentVid_DB = byte.TryParse(form.DocumentVid, out var byteValue)
                                            ? byteValue
                                            : null,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO,
                                        TransporterOKPO_DB = form.TransporterOKPO,
                                        PackName_DB = form.PackName,
                                        PackType_DB = form.PackType,
                                        PackNumber_DB = form.PackNumber,
                                        StoragePlaceName_DB = form.StoragePlaceName,
                                        StoragePlaceCode_DB = form.StoragePlaceCode,
                                        RefineOrSortRAOCode_DB = form.RefineOrSortRAOCode,
                                        Subsidy_DB = form.Subsidy,
                                        FcpNumber_DB = form.FcpNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form16

                        case "1.6":
                            {
                                var repForm = (JsonForm16)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows16.Add(new Form16
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        CodeRAO_DB = form.CodeRAO,
                                        StatusRAO_DB = form.StatusRAO,
                                        Volume_DB = form.Volume,
                                        Mass_DB = form.Mass,
                                        QuantityOZIII_DB = form.QuantityOZIII,
                                        MainRadionuclids_DB = form.MainRadionuclids,
                                        TritiumActivity_DB = form.TritiumActivity,
                                        BetaGammaActivity_DB = form.BetaGammaActivity,
                                        AlphaActivity_DB = form.AlphaActivity,
                                        TransuraniumActivity_DB = form.TransuraniumActivity,
                                        ActivityMeasurementDate_DB = form.ActivityMeasurementDate,
                                        DocumentVid_DB = byte.TryParse(form.DocumentVid, out var byteValue)
                                            ? byteValue
                                            : null,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO,
                                        TransporterOKPO_DB = form.TransporterOKPO,
                                        StoragePlaceName_DB = form.StoragePlaceName,
                                        StoragePlaceCode_DB = form.StoragePlaceCode,
                                        RefineOrSortRAOCode_DB = form.RefineOrSortRAOCode,
                                        PackName_DB = form.PackName,
                                        PackType_DB = form.PackType,
                                        PackNumber_DB = form.PackNumber,
                                        Subsidy_DB = form.Subsidy,
                                        FcpNumber_DB = form.FcpNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form17

                        case "1.7":
                            {
                                var repForm = (JsonForm17)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows17.Add(new Form17
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        PackName_DB = form.PackName,
                                        PackType_DB = form.PackType,
                                        PackFactoryNumber_DB = form.PackFactoryNumber,
                                        PackNumber_DB = form.PackNumber,
                                        FormingDate_DB = form.FormingDate,
                                        PassportNumber_DB = form.PassportNumber,
                                        Volume_DB = form.Volume,
                                        Mass_DB = form.Mass,
                                        Radionuclids_DB = form.Radionuclids,
                                        SpecificActivity_DB = form.SpecificActivity,
                                        DocumentVid_DB = byte.TryParse(form.DocumentVid, out var byteValue)
                                            ? byteValue
                                            : null,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO,
                                        TransporterOKPO_DB = form.TransporterOKPO,
                                        StoragePlaceName_DB = form.StoragePlaceName,
                                        StoragePlaceCode_DB = form.StoragePlaceCode,
                                        CodeRAO_DB = form.CodeRAO,
                                        StatusRAO_DB = form.StatusRAO,
                                        VolumeOutOfPack_DB = form.VolumeOutOfPack,
                                        MassOutOfPack_DB = form.MassOutOfPack,
                                        Quantity_DB = form.Quantity,
                                        TritiumActivity_DB = form.TritiumActivity,
                                        BetaGammaActivity_DB = form.BetaGammaActivity,
                                        AlphaActivity_DB = form.AlphaActivity,
                                        TransuraniumActivity_DB = form.TransuraniumActivity,
                                        RefineOrSortRAOCode_DB = form.RefineOrSortRAOCode,
                                        Subsidy_DB = form.Subsidy,
                                        FcpNumber_DB = form.FcpNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form18

                        case "1.8":
                            {
                                var repForm = (JsonForm18)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows18.Add(new Form18
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        IndividualNumberZHRO_DB = form.IndividualNumberZHRO,
                                        PassportNumber_DB = form.PassportNumber,
                                        Volume6_DB = form.Volume6,
                                        Mass7_DB = form.Mass7,
                                        SaltConcentration_DB = form.SaltConcentration,
                                        Radionuclids_DB = form.Radionuclids,
                                        SpecificActivity_DB = form.SpecificActivity,
                                        DocumentVid_DB = byte.TryParse(form.DocumentVid, out var byteValue)
                                            ? byteValue
                                            : null,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO,
                                        TransporterOKPO_DB = form.TransporterOKPO,
                                        StoragePlaceName_DB = form.StoragePlaceName,
                                        StoragePlaceCode_DB = form.StoragePlaceCode,
                                        CodeRAO_DB = form.CodeRAO,
                                        StatusRAO_DB = form.StatusRAO,
                                        Volume20_DB = form.Volume20,
                                        Mass21_DB = form.Mass21,
                                        TritiumActivity_DB = form.TritiumActivity,
                                        BetaGammaActivity_DB = form.BetaGammaActivity,
                                        AlphaActivity_DB = form.AlphaActivity,
                                        TransuraniumActivity_DB = form.TransuraniumActivity,
                                        RefineOrSortRAOCode_DB = form.RefineOrSortRAOCode,
                                        Subsidy_DB = form.Subsidy,
                                        FcpNumber_DB = form.FcpNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form19

                        case "1.9":
                            {
                                var repForm = (JsonForm19)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows19.Add(new Form19
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        OperationCode_DB = form.OperationCode,
                                        OperationDate_DB = form.OperationDate,
                                        DocumentVid_DB = byte.TryParse(form.DocumentVid, out var byteValue)
                                            ? byteValue
                                            : null,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        CodeTypeAccObject_DB = short.TryParse(form.CodeTypeAccObject, out var shortValue)
                                            ? shortValue
                                            : null,
                                        Radionuclids_DB = form.Radionuclids,
                                        Activity_DB = form.Activity
                                    });
                                }
                                break;
                            }

                            #endregion

                        #endregion

                        #region Form2

                        #region Form21

                        case "2.1":
                            {
                                var repForm = (JsonForm21)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows21.Add(new Form21
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        RefineMachineName_DB = form.RefineMachineName,
                                        MachineCode_DB = byte.TryParse(form.MachineCode, out var byteValue)
                                            ? byteValue
                                            : null,
                                        MachinePower_DB = form.MachinePower,
                                        NumberOfHoursPerYear_DB = form.NumberOfHoursPerYear,
                                        CodeRAOIn_DB = form.CodeRAOIn,
                                        StatusRAOIn_DB = form.StatusRAOIn,
                                        VolumeIn_DB = form.VolumeIn,
                                        MassIn_DB = form.MassIn,
                                        QuantityIn_DB = form.QuantityIn,
                                        TritiumActivityIn_DB = form.TritiumActivityIn,
                                        BetaGammaActivityIn_DB = form.BetaGammaActivityIn,
                                        AlphaActivityIn_DB = form.AlphaActivityIn,
                                        TransuraniumActivityIn_DB = form.TransuraniumActivityIn,
                                        CodeRAOout_DB = form.CodeRAOout,
                                        StatusRAOout_DB = form.StatusRAOout,
                                        VolumeOut_DB = form.VolumeOut,
                                        MassOut_DB = form.MassOut,
                                        QuantityOZIIIout_DB = form.QuantityOZIIIout,
                                        TritiumActivityOut_DB = form.TritiumActivityOut,
                                        BetaGammaActivityOut_DB = form.BetaGammaActivityOut,
                                        AlphaActivityOut_DB = form.AlphaActivityOut,
                                        TransuraniumActivityOut_DB = form.TransuraniumActivityOut
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form22

                        case "2.2":
                            {
                                var repForm = (JsonForm22)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows22.Add(new Form22
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        StoragePlaceName_DB = form.StoragePlaceName,
                                        StoragePlaceCode_DB = form.StoragePlaceCode,
                                        PackName_DB = form.PackName,
                                        PackType_DB = form.PackType,
                                        PackQuantity_DB = form.PackQuantity,
                                        CodeRAO_DB = form.CodeRAO,
                                        StatusRAO_DB = form.StatusRAO,
                                        VolumeOutOfPack_DB = form.VolumeOutOfPack,
                                        VolumeInPack_DB = form.VolumeInPack,
                                        MassOutOfPack_DB = form.MassOutOfPack,
                                        MassInPack_DB = form.MassInPack,
                                        QuantityOZIII_DB = form.QuantityOZIII,
                                        TritiumActivity_DB = form.TritiumActivity,
                                        BetaGammaActivity_DB = form.BetaGammaActivity,
                                        AlphaActivity_DB = form.AlphaActivity,
                                        TransuraniumActivity_DB = form.TransuraniumActivity,
                                        MainRadionuclids_DB = form.MainRadionuclids,
                                        Subsidy_DB = form.Subsidy,
                                        FcpNumber_DB = form.FcpNumber
                                    });
                                }
                                break;
                            }

                        #endregion

                        #region Form23

                        case "2.3":
                            {
                                var repForm = (JsonForm23)rep;
                                impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                                impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                                impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                                impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                                foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                                {
                                    impRep.Rows23.Add(new Form23
                                    {
                                        NumberInOrder_DB = numberInOrder++,
                                        StoragePlaceName_DB = form.StoragePlaceName,
                                        StoragePlaceCode_DB = form.StoragePlaceCode,
                                        ProjectVolume_DB = form.ProjectVolume,
                                        CodeRAO_DB = form.CodeRAO,
                                        Volume_DB = form.Volume,
                                        Mass_DB = form.Mass,
                                        QuantityOZIII_DB = form.QuantityOZIII,
                                        SummaryActivity_DB = form.SummaryActivity,
                                        DocumentNumber_DB = form.DocumentNumber,
                                        DocumentDate_DB = form.DocumentDate,
                                        ExpirationDate_DB = form.ExpirationDate,
                                        DocumentName_DB = form.DocumentName
                                    });
                                }
                                break;
                            }

                        #endregion
                            
                        #region Form24

                        case "2.4":
                        {
                            var repForm = (JsonForm24)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows24.Add(new Form24
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    CodeOYAT_DB = form.CodeOYAT,
                                    FcpNumber_DB = form.FcpNumber,
                                    MassCreated_DB = form.MassCreated,
                                    QuantityCreated_DB = form.QuantityCreated,
                                    MassFromAnothers_DB = form.MassFromAnothers,
                                    QuantityFromAnothers_DB = form.QuantityFromAnothers,
                                    MassFromAnothersImported_DB = form.MassFromAnothersImported,
                                    QuantityFromAnothersImported_DB = form.QuantityFromAnothersImported,
                                    MassAnotherReasons_DB = form.MassAnotherReasons,
                                    QuantityAnotherReasons_DB = form.QuantityAnotherReasons,
                                    MassTransferredToAnother_DB = form.MassTransferredToAnother,
                                    QuantityTransferredToAnother_DB = form.QuantityTransferredToAnother,
                                    MassRefined_DB = form.MassRefined,
                                    QuantityRefined_DB = form.QuantityRefined,
                                    MassRemovedFromAccount_DB = form.MassRemovedFromAccount,
                                    QuantityRemovedFromAccount_DB = form.QuantityRemovedFromAccount
                                });
                            }
                            break;
                        }

                        #endregion

                        #region Form25

                        case "2.5":
                        {
                            var repForm = (JsonForm25)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows25.Add(new Form25
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    StoragePlaceName_DB = form.StoragePlaceName,
                                    StoragePlaceCode_DB = form.StoragePlaceCode,
                                    CodeOYAT_DB = form.CodeOYAT,
                                    FcpNumber_DB = form.FcpNumber,
                                    FuelMass_DB = form.FuelMass,
                                    CellMass_DB = form.CellMass,
                                    Quantity_DB = int.TryParse(form.Quantity, out var intValue)
                                        ? intValue
                                        : null,
                                    AlphaActivity_DB = form.AlphaActivity,
                                    BetaGammaActivity_DB = form.BetaGammaActivity
                                });
                            }
                            break;
                        }

                        #endregion

                        #region Form26

                        case "2.6":
                        {
                            var repForm = (JsonForm26)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows26.Add(new Form26
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    ObservedSourceNumber_DB = form.ObservedSourceNumber,
                                    ControlledAreaName_DB = form.ControlledAreaName,
                                    SupposedWasteSource_DB = form.SupposedWasteSource,
                                    DistanceToWasteSource_DB = form.DistanceToWasteSource,
                                    TestDepth_DB = form.TestDepth,
                                    RadionuclidName_DB = form.RadionuclidName,
                                    AverageYearConcentration_DB = form.AverageYearConcentration
                                });
                            }
                            break;
                        }

                        #endregion

                        #region Form27

                        case "2.7":
                        {
                            var repForm = (JsonForm27)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows27.Add(new Form27
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    ObservedSourceNumber_DB = form.ObservedSourceNumber,
                                    RadionuclidName_DB = form.RadionuclidName,
                                    AllowedWasteValue_DB = form.AllowedWasteValue,
                                    FactedWasteValue_DB = form.FactedWasteValue,
                                    WasteOutbreakPreviousYear_DB = form.WasteOutbreakPreviousYear
                                });
                            }
                            break;
                        }

                        #endregion

                        #region Form28

                        case "2.8":
                        {
                            var repForm = (JsonForm28)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows28.Add(new Form28
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    WasteSourceName_DB = form.WasteSourceName,
                                    WasteRecieverName_DB = form.WasteRecieverName,
                                    RecieverTypeCode_DB = form.RecieverTypeCode,
                                    PoolDistrictName_DB = form.PoolDistrictName,
                                    AllowedWasteRemovalVolume_DB = form.AllowedWasteRemovalVolume,
                                    RemovedWasteVolume_DB = form.RemovedWasteVolume
                                });
                            }
                            break;
                        }

                        #endregion

                        #region Form29

                        case "2.9":
                        {
                            var repForm = (JsonForm29)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows29.Add(new Form29
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    WasteSourceName_DB = form.WasteSourceName,
                                    RadionuclidName_DB = form.RadionuclidName,
                                    AllowedActivity_DB = form.AllowedActivity,
                                    FactedActivity_DB = form.FactedActivity
                                });
                            }
                            break;
                        }

                        #endregion

                        #region Form210

                        case "2.10":
                        {
                            var repForm = (JsonForm210)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows210.Add(new Form210
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    IndicatorName_DB = form.IndicatorName,
                                    PlotName_DB = form.PlotName,
                                    PlotKadastrNumber_DB = form.PlotKadastrNumber,
                                    PlotCode_DB = form.PlotCode,
                                    InfectedArea_DB = form.InfectedArea,
                                    AvgGammaRaysDosePower_DB = form.AvgGammaRaysDosePower,
                                    MaxGammaRaysDosePower_DB = form.MaxGammaRaysDosePower,
                                    WasteDensityAlpha_DB = form.WasteDensityAlpha,
                                    WasteDensityBeta_DB = form.WasteDensityBeta,
                                    FcpNumber_DB = form.FcpNumber
                                });
                            }
                            break;
                        }

                        #endregion

                        #region Form211

                        case "2.11":
                        {
                            var repForm = (JsonForm211)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows211.Add(new Form211
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    PlotName_DB = form.PlotName,
                                    PlotKadastrNumber_DB = form.PlotKadastrNumber,
                                    PlotCode_DB = form.PlotCode,
                                    InfectedArea_DB = form.InfectedArea,
                                    Radionuclids_DB = form.Radionuclids,
                                    SpecificActivityOfPlot_DB = form.SpecificActivityOfPlot,
                                    SpecificActivityOfLiquidPart_DB = form.SpecificActivityOfLiquidPart,
                                    SpecificActivityOfDensePart_DB = form.SpecificActivityOfDensePart
                                });
                            }
                            break;
                        }

                        #endregion
                            
                        #region Form212

                        case "2.12":
                        {
                            var repForm = (JsonForm212)rep;
                            impRep.GradeExecutor_DB = repForm.ExecutorData.GradeExecutor;
                            impRep.FIOexecutor_DB = repForm.ExecutorData.FIOexecutor;
                            impRep.ExecPhone_DB = repForm.ExecutorData.ExecPhone;
                            impRep.ExecEmail_DB = repForm.ExecutorData.ExecEmail;
                            foreach (var form in repForm.TableData.TableData)   // Для каждой строчки формы
                            {
                                impRep.Rows212.Add(new Form212
                                {
                                    NumberInOrder_DB = numberInOrder++,
                                    OperationCode_DB = short.TryParse(form.OperationCode, out var shortValue)
                                        ? shortValue
                                        : null,
                                    ObjectTypeCode_DB = short.TryParse(form.ObjectTypeCode, out shortValue)
                                        ? shortValue
                                        : null,
                                    Radionuclids_DB = form.Radionuclids,
                                    Activity_DB = form.Activity,
                                    ProviderOrRecieverOKPO_DB = form.ProviderOrRecieverOKPO
                                });
                            }
                            break;
                        }

                        #endregion

                        #endregion
                    }
                    currentOrg.Report_Collection.Add(impRep);

                    #endregion
                }

                if (reportsJsonCollection.Count == 0)
                {
                    #region MessageFailedToReadFile

                    await MessageBox.Avalonia.MessageBoxManager
                        .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Импорт из .raodb",
                            ContentHeader = "Ошибка",
                            ContentMessage =
                                $"Не удалось прочесть файл {path}," +
                                $"{Environment.NewLine}файл поврежден или не содержит данных.",
                            MinWidth = 400,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        })
                        .ShowDialog(Desktop.MainWindow); 

                    #endregion

                    countReadFiles--;
                    continue;
                }
                if (!HasMultipleReport)
                {
                    HasMultipleReport = reportsJsonCollection.Sum(x => x.Report_Collection.Count) > 1 || answer.Length > 1;
                }

                foreach (var impReps in reportsJsonCollection)
                {
                    var baseReps11 = GetReports11FromLocalEqual(impReps);
                    var baseReps21 = GetReports21FromLocalEqual(impReps);
                    FillEmptyRegNo(ref baseReps11);
                    FillEmptyRegNo(ref baseReps21);
                    impReps.CleanIds();
                    ProcessIfNoteOrder0(impReps);

                    ImpRepFormCount = impReps.Report_Collection.Count;
                    ImpRepFormNum = impReps.Master.FormNum_DB;
                    BaseRepsOkpo = impReps.Master.OkpoRep.Value;
                    BaseRepsRegNum = impReps.Master.RegNoRep.Value;
                    BaseRepsShortName = impReps.Master.ShortJurLicoRep.Value;

                    if (baseReps11 != null)
                    {
                        await ProcessIfHasReports11(baseReps11, impReps);
                    }
                    else if (baseReps21 != null)
                    {
                        await ProcessIfHasReports21(baseReps21, impReps);
                    }
                    else if (baseReps11 == null && baseReps21 == null)
                    {
                        #region AddNewOrg

                        var an = "Добавить";
                        if (!SkipNewOrg)
                        {
                            if (answer.Length > 1)
                            {
                                #region MessageNewOrg

                                an = await MessageBox.Avalonia.MessageBoxManager
                                    .GetMessageBoxCustomWindow(new MessageBoxCustomParams
                                    {
                                        ButtonDefinitions = new[]
                                        {
                                            new ButtonDefinition { Name = "Добавить", IsDefault = true },
                                            new ButtonDefinition { Name = "Да для всех" },
                                            new ButtonDefinition { Name = "Отменить импорт", IsCancel = true }
                                        },
                                        ContentTitle = "Импорт из .raodb",
                                        ContentHeader = "Уведомление",
                                        ContentMessage =
                                            $"Будет добавлена новая организация ({ImpRepFormNum}) содержащая {ImpRepFormCount} форм отчетности." +
                                            $"{Environment.NewLine}" +
                                            $"{Environment.NewLine}Регистрационный номер - {BaseRepsRegNum}" +
                                            $"{Environment.NewLine}ОКПО - {BaseRepsOkpo}" +
                                            $"{Environment.NewLine}Сокращенное наименование - {BaseRepsShortName}" +
                                            $"{Environment.NewLine}" +
                                            $"{Environment.NewLine}Кнопка \"Да для всех\" позволяет без уведомлений " +
                                            $"{Environment.NewLine}импортировать все новые организации.",
                                        MinWidth = 400,
                                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                                    })
                                    .ShowDialog(Desktop.MainWindow);

                                #endregion

                                if (an is "Да для всех") SkipNewOrg = true;
                            }
                            else
                            {
                                #region MessageNewOrg

                                an = await MessageBox.Avalonia.MessageBoxManager
                                    .GetMessageBoxCustomWindow(new MessageBoxCustomParams
                                    {
                                        ButtonDefinitions = new[]
                                        {
                                            new ButtonDefinition { Name = "Добавить", IsDefault = true },
                                            new ButtonDefinition { Name = "Отменить импорт", IsCancel = true }
                                        },
                                        ContentTitle = "Импорт из .raodb",
                                        ContentHeader = "Уведомление",
                                        ContentMessage =
                                            $"Будет добавлена новая организация ({ImpRepFormNum}) содержащая {ImpRepFormCount} форм отчетности." +
                                            $"{Environment.NewLine}" +
                                            $"{Environment.NewLine}Регистрационный номер - {BaseRepsRegNum}" +
                                            $"{Environment.NewLine}ОКПО - {BaseRepsOkpo}" +
                                            $"{Environment.NewLine}Сокращенное наименование - {BaseRepsShortName}",
                                        MinWidth = 400,
                                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                                    })
                                    .ShowDialog(Desktop.MainWindow);

                                #endregion
                            }
                        }
                        if (an is "Добавить" or "Да для всех")
                        {
                            MainWindowVM.LocalReports.Reports_Collection.Add(impReps);
                            countNewReps++;
                            AtLeastOneImportDone = true;

                            #region LoggerImport

                            var sortedRepList = impReps.Report_Collection
                                .OrderBy(x => x.FormNum_DB)
                                .ThenBy(x => StaticStringMethods.StringReverse(x.StartPeriod_DB))
                                .ToList();
                            foreach (var rep in sortedRepList)
                            {
                                ImpRepCorNum = rep.CorrectionNumber_DB;
                                ImpRepFormCount = rep.Rows11.Count + rep.Rows12.Count + rep.Rows13.Count + rep.Rows14.Count + rep.Rows15.Count + rep.Rows16.Count + rep.Rows17.Count + rep.Rows18.Count + rep.Rows19.Count;
                                ImpRepFormNum = rep.FormNum_DB;
                                ImpRepStartPeriod = rep.StartPeriod_DB;
                                ImpRepEndPeriod = rep.EndPeriod_DB;
                                Act = "\t\t\t";
                                LoggerImportDTO = new LoggerImportDTO
                                {
                                    Act = Act, CorNum = ImpRepCorNum, CurrentLogLine = CurrentLogLine,
                                    EndPeriod = ImpRepEndPeriod,
                                    FormCount = ImpRepFormCount, FormNum = ImpRepFormNum,
                                    StartPeriod = ImpRepStartPeriod,
                                    Okpo = BaseRepsOkpo, OperationDate = OperationDate, RegNum = BaseRepsRegNum,
                                    ShortName = BaseRepsShortName, SourceFileFullPath = SourceFile!.FullName,
                                    Year = ImpRepYear
                                };
                                ServiceExtension.LoggerManager.Import(LoggerImportDTO);
                                IsFirstLogLine = false;
                                CurrentLogLine++;
                            }

                            #endregion
                        }

                        #endregion
                    }
                }
            }
            catch (Exception)
            {
                //ignore
            }
        }    
        await MainWindowVM.LocalReports.Reports_Collection.QuickSortAsync();
        await StaticConfiguration.DBModel.SaveChangesAsync().ConfigureAwait(false);

        #region Suffix

        var suffix1 = answer.Length.ToString().EndsWith('1') && !answer.Length.ToString().EndsWith("11")
            ? "а"
            : "ов";
        var suffix2 = countNewReps.ToString().EndsWith('1') && !countNewReps.ToString().EndsWith("11")
            ? "ая"
            : countNewReps.ToString().EndsWith('2') && !countNewReps.ToString().EndsWith("12")
              || countNewReps.ToString().EndsWith('3') && !countNewReps.ToString().EndsWith("13")
              || countNewReps.ToString().EndsWith('4') && !countNewReps.ToString().EndsWith("14")
                ? "ые"
                : "ых";
        var suffix3 = countNewReps.ToString().EndsWith('1') && !countNewReps.ToString().EndsWith("11")
            ? "я"
            : countNewReps.ToString().EndsWith('2') && !countNewReps.ToString().EndsWith("12")
              || countNewReps.ToString().EndsWith('3') && !countNewReps.ToString().EndsWith("13")
              || countNewReps.ToString().EndsWith('4') && !countNewReps.ToString().EndsWith("14")
                ? "и"
                : "й";

        #endregion

        if (AtLeastOneImportDone)
        {
            #region MessageImportDone
            
            await MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = "Импорт из .raodb",
                    ContentHeader = "Уведомление",
                    ContentMessage = $"Импорт {countReadFiles} из {answer.Length} файл{suffix1} .json успешно завершен." +
                    $"{Environment.NewLine}Импортировано {countNewReps} нов{suffix2} организаци{suffix3}",
                    MinWidth = 400,
                    MinHeight = 150,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                })
                .ShowDialog(Desktop.MainWindow);

            #endregion
        }
        else
        {
            #region MessageImportCancel

            await MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = "Импорт из .raodb",
                    ContentHeader = "Уведомление",
                    ContentMessage = $"Импорт из {answer.Length} файл{suffix1} .json был отменен.",
                    MinWidth = 400,
                    MinHeight = 150,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                })
                .ShowDialog(Desktop.MainWindow);

            #endregion
        }
    }
}