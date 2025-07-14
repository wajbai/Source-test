using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility.ConfigSetting;
using System.Threading;
using System.Globalization;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Model.UIModel;

namespace Bosco.Model.Setting
{
    public class UISetting : SystemBase, ISetting
    {
        ResultArgs resultArgs = null;
        public void ApplySetting()
        {
            try
            {
                UISettingProperty setting = new UISettingProperty();
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(setting.UILanguage);

                //Date format and date separator
                DateTimeFormatInfo date = new DateTimeFormatInfo();
                date.ShortDatePattern = setting.UIDateFormat;
                date.DateSeparator = setting.UIDateSeparator;

                Thread.CurrentThread.CurrentCulture.DateTimeFormat = date;

                new CommonMethod().ApplyTheme(setting.UIThemes);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }


        /// <summary>
        /// On 06/02/2024, To Enable Finance Setting from Project Import
        /// </summary>
        /// <param name="dtSetting"></param>
        /// <returns></returns>
        public ResultArgs SaveSettingFromProjectImport(DataTable dtSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.InsertUpdateUI))
            {
                dataManager.BeginTransaction();

                string SettingName = "";
                string Value = "";
                int UserId = 0;

                DataTable dtUISetting = dtSetting;
                if (dtUISetting != null)
                {
                    foreach (DataRow drSetting in dtUISetting.Rows)
                    {
                        SettingName = drSetting[this.AppSchema.Setting.NameColumn.ColumnName].ToString();
                        Value = drSetting[this.AppSchema.Setting.ValueColumn.ColumnName].ToString();
                        UserId = NumberSet.ToInteger(this.LoginUserId);

                        dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, SettingName);
                        dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, Value);
                        dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UserId);

                        resultArgs = dataManager.UpdateData();

                        if (!resultArgs.Success) { break; }
                        else { dataManager.Parameters.Clear(); }
                    }

                    if (resultArgs.Success)
                    {
                        // User Level settings
                        resultArgs = FetchSettingDetails(NumberSet.ToInteger(this.LoginUserId.ToString()));
                        if (resultArgs.Success && resultArgs.DataSource.TableView != null && resultArgs.DataSource.TableView.Count > 0)
                        {
                            this.UISettingInfo = resultArgs.DataSource.TableView;
                            this.SettingInfo = resultArgs.DataSource.TableView;
                        }

                    }
                    else
                    {
                        this.UISettingInfo = null;
                    }
                }
                dataManager.EndTransaction();
            }
            
            return resultArgs;
        }

        public ResultArgs SaveSetting(DataTable dtSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.InsertUpdateUI))
            {
                dataManager.BeginTransaction();

                string SettingName = "";
                string Value = "";
                int UserId = 0;

                DataTable dtUISetting = dtSetting;
                if (dtUISetting != null)
                {
                    foreach (DataRow drSetting in dtUISetting.Rows)
                    {
                        SettingName = drSetting[this.AppSchema.Setting.NameColumn.ColumnName].ToString();
                        Value = drSetting[this.AppSchema.Setting.ValueColumn.ColumnName].ToString();
                        UserId = this.NumberSet.ToInteger(drSetting["UserId"].ToString());

                        dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, SettingName);
                        dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, Value);
                        dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UserId);

                        resultArgs = dataManager.UpdateData();

                        if (!resultArgs.Success) { break; }
                        else { dataManager.Parameters.Clear(); }
                    }

                    if (resultArgs.Success)
                    {
                        // User Level settings
                        resultArgs = FetchSettingDetails(NumberSet.ToInteger(this.LoginUserId.ToString()));
                        if (resultArgs.Success && resultArgs.DataSource.TableView != null && resultArgs.DataSource.TableView.Count > 0)
                        {
                            this.UISettingInfo = resultArgs.DataSource.TableView;
                        }
                    }
                    else
                    {
                        this.UISettingInfo = null;
                    }
                }
                dataManager.EndTransaction();
            }

            //20/06/2022, To update LC reference details in based db for enable request rightrs
            if (IS_SDB_INM)
            {
                using (UISettingSystem uisystemsetting = new UISettingSystem())
                {
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef1, BaseLCRef1, LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef2, BaseLCRef2, LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef3, BaseLCRef3, LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef4, BaseLCRef4, LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef5, BaseLCRef5, LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef6, BaseLCRef6, LoginUserId);
                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef7, BaseLCRef7, LoginUserId);

                    uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.BranchReceiptModuleStatus, ((Int32)FINAL_RECEIPT_MODULE_STATUS).ToString(), LoginUserId.ToString());
                }
            }

            return resultArgs;
        }

        public ResultArgs FetchSettingDetails(int UserID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.FetchUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UserID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public ResultArgs SaveSettingDetails(string settingName, string value, int userId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.InsertUpdateUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, settingName);
                dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, value);
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, userId);

                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

       
        public ResultArgs FetchVoucherPrintSetting(DefaultVoucherTypes vouchertype)
        {
            ResultArgs resultArgs = new ResultArgs();
            string reportId = "RPT-024"; //Receipts
            switch(vouchertype)
            {
                case (DefaultVoucherTypes.Receipt):
                    reportId = "RPT-024";
                    break;
                case (DefaultVoucherTypes.Payment):
                    reportId = "RPT-025";
                    break;
                case (DefaultVoucherTypes.Contra):
                    reportId = "RPT-151";
                    break;
                case (DefaultVoucherTypes.Journal):
                    reportId = "RPT-026";
                    break;
            }
            
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchReportSetting))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.REPORT_IDColumn, reportId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveVoucherPrintSetting( DataTable dtSetting)
        {
            ResultArgs resultArgs = new ResultArgs();
            string reportId = "RPT-024"; //Receipts
            
            if (dtSetting != null && dtSetting.Rows.Count > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdateReportSetting))
                {
                    dataManager.BeginTransaction();
                    foreach (DataRow drSetting in dtSetting.Rows)
                    {
                        string vouchertypename = drSetting["ReportId"].ToString();
                        DefaultVoucherTypes vouchertype = (DefaultVoucherTypes)Enum.Parse(typeof(DefaultVoucherTypes), vouchertypename);
                        switch (vouchertype)
                        {
                            case (DefaultVoucherTypes.Receipt):
                                reportId = "RPT-024";
                                break;
                            case (DefaultVoucherTypes.Payment):
                                reportId = "RPT-025";
                                break;
                            case (DefaultVoucherTypes.Contra):
                                reportId = "RPT-151";
                                break;
                            case (DefaultVoucherTypes.Journal):
                                reportId = "RPT-026";
                                break;
                        }

                        dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, drSetting[this.AppSchema.EnumSchema.NameColumn.ColumnName].ToString());
                        dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, drSetting[this.AppSchema.Setting.ValueColumn.ColumnName].ToString());
                        dataManager.Parameters.Add(this.AppSchema.Settings.REPORT_IDColumn, reportId);

                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success)
                        {
                            break;
                        }
                        else
                        {
                            dataManager.Parameters.Clear();
                        }
                    }

                    dataManager.EndTransaction();
                }
            }

            return resultArgs;
        }


        /// <summary>
        /// On 27/03/2020, If there is no setting for EnableShowCCOPBalanceInCCReports
        /// but current db has CC opening balance, this method will create setting for EnableShowCCOPBalanceInCCReports.
        /// 
        /// If already exists setting EnableShowCCOPBalanceInCCReports, but it has been disabled, it means just leave it as it is
        /// 
        /// It will run only one time
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultArgs EnableShowCCOPBalanceInCCReports()
        {
            bool CreateSetting = false;
            string settingname = string.Empty;
            ResultArgs resultArgs = new ResultArgs();
            
            resultArgs = FetchSettingDetails(NumberSet.ToInteger(this.LoginUserId));
            if (resultArgs.Success && resultArgs.DataSource.TableView != null)
            {
                DataView dvSetting = resultArgs.DataSource.TableView;
                settingname = FinanceSetting.ShowCCOpeningBalanceInReports.ToString();
                //1. Check if ShowCCOpeningBalanceInReports setting
                dvSetting.RowFilter = "NAME ='" + settingname + "'";
                //2. Check if ShowCCOpeningBalanceInReports setting not exists
                if (dvSetting.Count == 0)
                {
                    //3. check CC opening Balance, If exists, enable ShowCCOpeningBalanceInReports
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedCostCenter))
                    {
                        dataManager.Parameters.Add(this.AppSchema.CostCentre.COSTCENTRE_MAPPINGColumn, this.CostCeterMapping);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);

                        if (resultArgs.Success && resultArgs.DataSource.Table != null)
                        {
                            DataTable dtCCOpeningBalance = resultArgs.DataSource.Table;
                            dtCCOpeningBalance.DefaultView.RowFilter = this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName + ">0";
                            CreateSetting = (dtCCOpeningBalance.DefaultView.Count > 0);
                        }
                    }
                }

                //4. Create and Enable Setting for EnableShowCCOPBalanceInCCReports
                if (CreateSetting)
                {
                    SaveSettingDetails(settingname, "1", NumberSet.ToInteger(this.LoginUserId)); //Create Admin User, it will be usual for all
                }
            }
            return resultArgs;
        }
              

        /// <summary>
        /// On 25/02/2021, To update singature details
        /// </summary>
        /// <param name="RoleName1"></param>
        /// <param name="Role1"></param>
        /// <param name="bytesign1Image"></param>
        /// <param name="RoleName2"></param>
        /// <param name="Role2"></param>
        /// <param name="bytesign2Image"></param>
        /// <param name="RoleName3"></param>
        /// <param name="Role3"></param>
        /// <param name="bytesign3Image"></param>
        public ResultArgs SaveSignDetails(DataTable dtSignDetails, Int32 ProjectId, Int32 HideRequireSignNote, string SignNote, Int32 SignNoteAlignment, Int32 SignNoteLocation)
        {
            string rolename = string.Empty;
            string role = string.Empty;
            byte[] bytesignImage  =null;
            Int32 signorder =1;

            if (dtSignDetails != null && dtSignDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSignDetails.Rows)
                {
                    signorder = dtSignDetails.Rows.IndexOf(dr)+1;
                    rolename = dr[AppSchema.ReportSign.ROLE_NAMEColumn.ColumnName].ToString();
                    role = dr[AppSchema.ReportSign.ROLEColumn.ColumnName].ToString();
                    bytesignImage = null;
                    if (dr[AppSchema.ReportSign.SIGN_IMAGEColumn.ColumnName] != null && dr[AppSchema.ReportSign.SIGN_IMAGEColumn.ColumnName]!=DBNull.Value)
                    {
                        bytesignImage = (byte[])dr[AppSchema.ReportSign.SIGN_IMAGEColumn.ColumnName];
                    }
                    resultArgs = ReportSignInsertUpdate(ProjectId, rolename, role, bytesignImage, HideRequireSignNote, 
                                    SignNote, SignNoteAlignment, SignNoteLocation, signorder);
                    if (!resultArgs.Success)
                    {
                        MessageRender.ShowMessage(resultArgs.Message);
                        break;
                    }
                }
            }

            return resultArgs;
        }

        /// <summary>
        /// Update Report Sign details
        /// </summary>
        /// <param name="rolename"></param>
        /// <param name="role"></param>
        /// <param name="bytesignImage"></param>
        /// <param name="signorder"></param>
        /// <returns></returns>
        public ResultArgs ReportSignInsertUpdate(Int32 projectid, string rolename, string role,
            byte[] bytesignImage, Int32 HideRequireSignNote, string SignNote, Int32 SignNoteAlignment, Int32 SignNoteLocation, Int32 signorder)
        {
            ResultArgs resultArgs = new ResultArgs();

            if (string.IsNullOrEmpty(rolename) && string.IsNullOrEmpty(role) && (bytesignImage == null) && HideRequireSignNote == 0 && string.IsNullOrEmpty(SignNote))
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteSign))
                {
                    //1.Sign 1
                    dataManager.Parameters.Clear();
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                    dataManager.Parameters.Add(AppSchema.ReportSign.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                    dataManager.Parameters.Add(AppSchema.ReportSign.PROJECT_IDColumn, projectid);
                    dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_ORDERColumn, signorder);
                    resultArgs = dataManager.UpdateData();
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdateSignDetail))
                {
                    //1.Sign 1
                    dataManager.Parameters.Clear();
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                    dataManager.Parameters.Add(AppSchema.ReportSign.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                    dataManager.Parameters.Add(AppSchema.ReportSign.PROJECT_IDColumn, projectid);
                    dataManager.Parameters.Add(AppSchema.ReportSign.ROLE_NAMEColumn, rolename);
                    dataManager.Parameters.Add(AppSchema.ReportSign.ROLEColumn, role);
                    dataManager.Parameters.Add(AppSchema.ReportSign.HIDE_REQUIRE_SIGN_NOTEColumn, HideRequireSignNote);
                    dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTEColumn, SignNote.Trim());
                    dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTE_ALIGNMENTColumn, SignNoteAlignment);
                    dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTE_LOCATIONColumn, SignNoteLocation);
                    dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_ORDERColumn, signorder);
                    resultArgs = dataManager.UpdateData();
                }
            }

            if (resultArgs.Success)
            {
                UpdateSignDetails(projectid, bytesignImage, signorder, HideRequireSignNote, SignNote, SignNoteAlignment ,SignNoteLocation);
            }
            return resultArgs;
        }

        private ResultArgs UpdateSignDetails(Int32 projectid, byte[] bytesignImage, Int32 signorder, Int32 HideRequireSignNote,
                string SignNote, Int32 SignNoteAlignment, Int32 SignNoteLocation)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.Setting.UpdateSignDetails))
            {
                dataManager.Parameters.Clear();
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                dataManager.Parameters.Add(AppSchema.ReportSign.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                dataManager.Parameters.Add(AppSchema.ReportSign.PROJECT_IDColumn, projectid);
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_ORDERColumn, signorder);
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_IMAGEColumn, bytesignImage);
                dataManager.Parameters.Add(AppSchema.ReportSign.HIDE_REQUIRE_SIGN_NOTEColumn, HideRequireSignNote);
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTEColumn, SignNote.Trim());
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTE_ALIGNMENTColumn, SignNoteAlignment);
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTE_LOCATIONColumn, SignNoteLocation);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateSignDetailsForAllProjects(Int32 signorder, string rolename, string role, byte[] bytesignImage, Int32 HideRequireSignNote, 
                               string SignNote, Int32 SignNoteAlignment, Int32 SignNoteLocation)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.Setting.UpdateSignDetailsForAllProjects))
            {
                dataManager.Parameters.Clear();
                dataManager.Parameters.Add(AppSchema.ReportSign.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                dataManager.Parameters.Add(AppSchema.ReportSign.ROLE_NAMEColumn, rolename);
                dataManager.Parameters.Add(AppSchema.ReportSign.ROLEColumn, role);
                dataManager.Parameters.Add(AppSchema.ReportSign.HIDE_REQUIRE_SIGN_NOTEColumn, HideRequireSignNote);
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTEColumn, SignNote.Trim());
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTE_ALIGNMENTColumn, SignNoteAlignment);
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_NOTE_LOCATIONColumn, SignNoteLocation);
                dataManager.Parameters.Add(AppSchema.ReportSign.SIGN_ORDERColumn, signorder);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveAuditorNoteSign(DataTable dtAuditorNoteSignDetails)
        {
            string SettingName = "";
            string SettingValue = "";
            
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertAuditorNoteSign))
            {
                dataManager.BeginTransaction();

                if (dtAuditorNoteSignDetails != null)
                {
                    foreach (DataRow drSetting in dtAuditorNoteSignDetails.Rows)
                    {
                        SettingName = drSetting[this.AppSchema.AuditorNoteSign.AUDITOR_NOTE_SETTINGColumn.ColumnName].ToString().Trim();
                        SettingValue = drSetting[this.AppSchema.AuditorNoteSign.AUDITOR_NOTE_SETTING_VALUEColumn.ColumnName].ToString().Trim();

                        dataManager.Parameters.Add(this.AppSchema.AuditorNoteSign.AUDITOR_NOTE_SETTINGColumn, SettingName, DataType.String);
                        dataManager.Parameters.Add("VALUE", SettingValue, DataType.String);
                        dataManager.Parameters.Add(this.AppSchema.AuditorNoteSign.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                        //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();

                        if (!resultArgs.Success) { break; }
                        else { dataManager.Parameters.Clear(); }
                    }
                }
                dataManager.EndTransaction();
            }

            return resultArgs;
        }

        public ResultArgs FetchAuditorSignNote()
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.ReturnValue = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchAuditorNoteSign))
            {
                dataManager.Parameters.Add(AppSchema.ReportSign.ACC_YEAR_IDColumn, AccPeriodId);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            if (resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                DataTable dtAuditorSignNote = resultArgs.DataSource.Table;
               
            }
            return resultArgs;
        }

        public ResultArgs FetchReportSignDetails()
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchSignDetails))
            {
                dataManager.Parameters.Add(AppSchema.ReportSign.ACC_YEAR_IDColumn, AccPeriodId);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 23/01/2025 - Update Voucher grace details in gobale properties
        /// enforcegracedays ( 0- None, 1- No, 2-Yes)
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateVoucherGraceDetailsInGlobalSetting(Int32 enforcegracedays, Int32 gracedays, string tmpdatefrom = "", string tmpdateto = "", string tmpvalidupto = "")
        {
            resultArgs = new ResultArgs();
            if (IS_SDB_INM)
            {
                resultArgs = SaveSettingDetails(Bosco.Utility.Setting.VoucherEnforceGraceMode.ToString(),
                                    CommonMethod.Encrept(enforcegracedays.ToString()), DEFAULT_ADMIN_USER_ID);
                resultArgs = SaveSettingDetails(Bosco.Utility.Setting.VoucherGraceDays.ToString(),
                                    CommonMethod.Encrept(gracedays.ToString()), DEFAULT_ADMIN_USER_ID);
                resultArgs = SaveSettingDetails(Bosco.Utility.Setting.VoucherGraceTmpDateFrom.ToString(),
                                    CommonMethod.Encrept(tmpdatefrom), DEFAULT_ADMIN_USER_ID);
                resultArgs = SaveSettingDetails(Bosco.Utility.Setting.VoucherGraceTmpDateTo.ToString(),
                                    CommonMethod.Encrept(tmpdateto), DEFAULT_ADMIN_USER_ID);
                resultArgs = SaveSettingDetails(Bosco.Utility.Setting.VoucherGraceTmpValidUpTo.ToString(),
                                    CommonMethod.Encrept(tmpvalidupto), DEFAULT_ADMIN_USER_ID);
            }
            else
            {
                resultArgs.Success = true;
            }
            return resultArgs;
        }


    }
}
