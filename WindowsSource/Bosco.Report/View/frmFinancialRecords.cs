using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Repository;
using System.Collections;

using Bosco.Report.SQL;
using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraBars.Docking;
using Bosco.Report.View;
using System.Globalization;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;



namespace Bosco.Report.View
{
    public partial class frmFinancialRecords : Bosco.Utility.Base.frmBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        SettingProperty setttings = new SettingProperty();
        private int UserRoleId { get; set; }
        private string reportId = string.Empty;
        private DataTable dtBankVouchers { get; set; }
        private string VoucherIds = "";
        private DataTable dtVouchers { get; set; }
        private DataTable dtProjectDetails { get; set; }
        private AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        #endregion

        /// <summary>
        /// Get and Assign Legal entity properties
        /// </summary>
        private string LegalEntityProperties
        {
            get
            {
                string legalproperties = string.Empty;
                if (ReportProperty.Current.ReportId == "RPT-024")
                {
                    chkListShowLegalDetails.RefreshEditValue();
                    List<object> selecteditems = chkListShowLegalDetails.Properties.Items.GetCheckedValues();

                    foreach (object item in selecteditems)
                    {
                        legalproperties += item.ToString() + ",";
                    }
                    legalproperties = legalproperties.TrimEnd(',');
                }
                //legalproperties = string.Empty;
                return legalproperties;
            }
            set
            {
                chkListShowLegalDetails.SetEditValue(value);
            }
        }

        #region Constructor
        public frmFinancialRecords()
        {
            InitializeComponent();
        }

        public frmFinancialRecords(string ReportId)
            : this()
        {
            this.reportId = ReportId;

            
        }

        #endregion

        #region Events

        private void frmFinancialRecords_Load(object sender, EventArgs e)
        {
            SetDefaults();
            FetchProjects();
            LoadLegalEntityProperties();
            LoadVoucherPrintSetting();
            AssignSelectedVouchers();
            //rgbReportTitle.SelectedIndex = ReportProperty.Current.HeaderInstituteSocietyName;
            
            //For GST Invoice
            colGSTInvoiceNo.Visible = false;
            colGSTAmount.Visible = false;
            if (ReportProperty.Current.ReportId == "RPT-207" || ReportProperty.Current.ReportId == "RPT-212")
            {
                //lcTitle.Visibility = LayoutVisibility.Never;
                //lcPrinterSetting.Visibility = LayoutVisibility.Never;
                lcIncludeSign1.Visibility = lcIncludeSign2.Visibility = lcIncludeSign3.Visibility = LayoutVisibility.Never;
                colGSTInvoiceNo.Visible = true;
                colGSTAmount.Visible = true;

                colGSTInvoiceNo.Width = 125;
                colDate.VisibleIndex = colvoucherNo.VisibleIndex + 1;
                //colGSTAmount.VisibleIndex = colGSTInvoiceNo.VisibleIndex + 1;
            }

            //On 02/03/2021, to hide Row1, Row2 and Row3 Voucher Print settings and will have common Sign Details from Finance Settings) 
            lcgSign1.Visibility = lcgSign2.Visibility = lcgSign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcSign1Space.Visibility = lcSign2Space.Visibility = LayoutVisibility.Never;
            this.Height = 350;
            this.CenterToScreen();
            chkProjectSelect.Location = new Point(gcProject.Left + 20, gcProject.Top + 2);

            //for Temp
            //lcShowCostCentre.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void LoadVoucherPrintSetting()
        {
            try
            {
                resultArgs = FetchReportSetting();
                if (resultArgs != null && resultArgs.Success)
                {
                    ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                    txtSign1Row1.Text = ReportProperty.Current.VoucherPrintSign1Row1;
                    txtSign1Row2.Text = ReportProperty.Current.VoucherPrintSign1Row2;
                    txtSign2Row1.Text = ReportProperty.Current.VoucherPrintSign2Row1;
                    txtSign2Row2.Text = ReportProperty.Current.VoucherPrintSign2Row2;
                    txtSign3Row1.Text = ReportProperty.Current.VoucherPrintSign3Row1;
                    txtSign3Row2.Text = ReportProperty.Current.VoucherPrintSign3Row2;
                    chkCaptionFontStyle.Checked = (ReportProperty.Current.VoucherPrintCaptionBold == "1");
                    chkValueBold.Checked = (ReportProperty.Current.VoucherPrintValueBold == "1");
                    chkLogo.Checked = (ReportProperty.Current.VoucherPrintShowLogo == "1");
                    chkProject.Checked = (ReportProperty.Current.VoucherPrintProject == "1");
                    chkShowCostCentre.Checked = (ReportProperty.Current.VoucherPrintShowCostCentre == 1);
                    chkHideVoucherReceiptNo.Checked = (ReportProperty.Current.VoucherPrintHideVoucherReceiptNo == 1);
                    
                    //On 04/05/2022----------------------------------------------------------------------------------------------------------------
                    rgbReportTitle.SelectedIndex=0;
                    if (!string.IsNullOrEmpty(ReportProperty.Current.VoucherPrintReportTitleType))
                    {
                        if (ReportProperty.Current.VoucherPrintReportTitleType == "0" || ReportProperty.Current.VoucherPrintReportTitleType == "1")
                        {
                            rgbReportTitle.SelectedIndex = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.VoucherPrintReportTitleType);
                        }
                    }
                    //-------------------------------------------------------------------------------------------------------------------------------

                    //On 18/11/2024----------------------------------------------------------------------------------------------------------------
                    rgbReportAddress.SelectedIndex = 0;
                    if (!string.IsNullOrEmpty(ReportProperty.Current.VoucherPrintReportTitleAddress))
                    {
                        if (ReportProperty.Current.VoucherPrintReportTitleAddress== "0" || ReportProperty.Current.VoucherPrintReportTitleAddress== "1")
                        {
                            rgbReportAddress.SelectedIndex = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.VoucherPrintReportTitleAddress);
                        }
                    }
                    //-------------------------------------------------------------------------------------------------------------------------------
                    
                    //Set Include sign details
                    chkIncludeSign1.Checked = chkIncludeSign2.Checked = chkIncludeSign3.Checked = false;
                    if (!String.IsNullOrEmpty(ReportProperty.Current.VoucherPrintIncludeSigns))
                    {
                        string[] includesign = ReportProperty.Current.VoucherPrintIncludeSigns.Split(',');
                        foreach (string sign in includesign)
                        {
                            switch (sign)
                            {
                                case "1":
                                    chkIncludeSign1.Checked = true;
                                    break;
                                case "2":
                                    chkIncludeSign2.Checked = true;
                                    break;
                                case "3":
                                    chkIncludeSign3.Checked = true;
                                    break;
                            }
                        }
                    }

                    LegalEntityProperties = ReportProperty.Current.VoucherPrintLegalEntityFields;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }

        private ResultArgs FetchReportSetting()
        {
            string rptid = ReportProperty.Current.ReportId;

            if (rptid == "RPT-212")
            {
                rptid = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS);
            }
            else if (rptid == "RPT-207")
            {
                rptid = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.JOURNALVOUCHER);
            }

            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchReportSetting))
            {
                dataManager.Parameters.Add(this.appSchema.Settings.REPORT_IDColumn, rptid);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        private void rchkSelectProject_CheckedChanged(object sender, EventArgs e)
        {
            gvProject.Focus();
        }

        private void chkProjectSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtProject = gcProject.DataSource as DataTable;
                if (dtProject != null && dtProject.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProject.Rows)
                    {
                        dr["SELECT"] = chkProjectSelect.Checked;
                    }
                    gcProject.DataSource = dtProject;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private ResultArgs SaveReportSetting()
        {
            try
            {
                AcmeReportSetting setting = new AcmeReportSetting();
                DataView dvSetting = null;
                dvSetting = this.UtilityMember.EnumSet.GetEnumDataSource(setting, Sorting.None);
                DataTable dtSetting = dvSetting.ToTable();
                if (dtSetting != null)
                {
                    dtSetting.Columns.Add(this.appSchema.Setting.ValueColumn.ColumnName, typeof(string));
                    dtSetting.Columns.Add("ReportId", typeof(string));
                    for (int i = 0; i < dtSetting.Rows.Count; i++)
                    {
                        AcmeReportSetting SettingName = (AcmeReportSetting)Enum.Parse(typeof(AcmeReportSetting), dtSetting.Rows[i][1].ToString());
                        string Value = "";
                        switch (SettingName)
                        {
                            case AcmeReportSetting.VoucherPrintSign1Row1:
                                {
                                    Value = txtSign1Row1.Text;
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintSign1Row2:
                                {
                                    Value = txtSign1Row2.Text;
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintSign2Row1:
                                {
                                    Value = txtSign2Row1.Text;
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintSign2Row2:
                                {
                                    Value = txtSign2Row2.Text;
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintSign3Row1:
                                {
                                    Value = txtSign3Row1.Text;
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintSign3Row2:
                                {
                                    Value = txtSign3Row2.Text;
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintCaptionBold:
                                {
                                    Value = chkCaptionFontStyle.Checked ? "1" : "0";
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintValueBold:
                                {
                                    Value = chkValueBold.Checked ? "1" : "0";
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintShowLogo:
                                {
                                    Value = chkLogo.Checked ? "1" : "0";
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintProject:
                                {
                                    Value = chkProject.Checked ? "1" : "0";
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintIncludeSigns:
                                {
                                    Value = string.Empty;
                                    Value = chkIncludeSign1.Checked ? "1" : string.Empty;
                                    Value += (chkIncludeSign2.Checked ?  (string.IsNullOrEmpty(Value)? "":",") + "2" : string.Empty);
                                    Value += (chkIncludeSign3.Checked ? (string.IsNullOrEmpty(Value) ? "" : ",") + "3" : string.Empty);
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintReportTitleType:
                                {
                                    Value = rgbReportTitle.SelectedIndex.ToString();
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintReportTitleAddress:
                                {
                                    Value = rgbReportAddress.SelectedIndex.ToString();
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintLegalEntityDetails:
                                {
                                    Value = string.Empty;
                                    Value = LegalEntityProperties;
                                    break;
                                }
                            case AcmeReportSetting.VoucherPrintShowCostCentre:
                                Value = "0";
                                if (lcShowCostCentre.Visibility == LayoutVisibility.Always)
                                {
                                    Value = chkShowCostCentre.Checked ? "1" : "0";
                                }
                                break;
                            case AcmeReportSetting.VoucherPrintHideVoucherReceiptNo:
                                Value = "0";
                                if (lcHideVoucherReceiptNo.Visibility == LayoutVisibility.Always)
                                {
                                    Value = chkHideVoucherReceiptNo.Checked ? "1" : "0";
                                }
                                break;
                        }
                        dtSetting.Rows[i][this.appSchema.Setting.ValueColumn.ColumnName] = Value;
                        dtSetting.Rows[i]["ReportId"] = ReportProperty.Current.ReportId;

                        if (ReportProperty.Current.ReportId == "RPT-212")
                        {
                            dtSetting.Rows[i]["ReportId"] =  UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS);
                        }
                        else if (ReportProperty.Current.ReportId == "RPT-207")
                        {
                            dtSetting.Rows[i]["ReportId"] = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.JOURNALVOUCHER);
                        }
                    }

                    if (dtSetting != null && dtSetting.Rows.Count > 0)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdateReportSetting))
                        {
                            dataManager.BeginTransaction();
                            foreach (DataRow drSetting in dtSetting.Rows)
                            {
                                dataManager.Parameters.Add(this.appSchema.Settings.SETTING_NAMEColumn, drSetting[this.appSchema.EnumSchema.NameColumn.ColumnName].ToString());
                                dataManager.Parameters.Add(this.appSchema.Settings.VALUEColumn, drSetting[this.appSchema.Setting.ValueColumn.ColumnName].ToString());
                                dataManager.Parameters.Add(this.appSchema.Settings.REPORT_IDColumn, drSetting["ReportId"].ToString());

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

                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs = FetchReportSetting();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally
            {
            }
            return resultArgs;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (deDateTo.DateTime >= deDateFrom.DateTime)
            {
                if (gcProject.DataSource != null && gvProject.RowCount != 0)
                {
                    ReportProperty.Current.HeaderInstituteSocietyName = rgbReportTitle.SelectedIndex == 0 ? 0 : 1;
                    ReportProperty.Current.HeaderInstituteSocietyAddress = rgbReportAddress.SelectedIndex == 0 ? 0 : 1;
                    SelectedVouchers();
                    if (!(string.IsNullOrEmpty(VoucherIds)))
                    {
                        ReportProperty.Current.CashBankProjectId = glkpProject.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;

                        ReportProperty.Current.CashBankVoucherDateFrom = deDateFrom.DateTime;
                        ReportProperty.Current.CashBankVoucherDateTo = deDateTo.DateTime;
                        ReportProperty.Current.ProjectTitle = glkpProject.Text;
                        GetLegalEntityId(ReportProperty.Current.CashBankProjectId);
                        ReportProperty.Current.SelectedProjectCount = 1;
                        ReportProperty.Current.CashBankVoucher = dtVouchers;
                        //DataTable dtdistinct = dtVouchers.DefaultView.ToTable(true, new string[] { "VOUCHER_NO" });
                        ReportProperty.Current.PrintCashBankVoucherId = VoucherIds;
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

                        resultArgs = SaveReportSetting();
                        if (resultArgs.Success)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                        {
                            MessageRender.ShowMessage(resultArgs.Message);
                            //XtraMessageBox.Show(resultArgs.Message, "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        //XtraMessageBox.Show(ReportProperty.Current.GetMessage(MessageCatalog.ReportMessage.REPORT_FINACIAL_RECORDS_CASH_BANK_EMPTY), ReportProperty.Current.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //XtraMessageBox.Show("Select Cash/Bank voucher", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageRender.ShowMessage("Select Cash/Bank voucher");
                        CashBankVoucherTransType(Bosco.Report.SQL.ReportSQLCommand.FianacialMode.Add);
                    }
                }
                else
                {
                    //XtraMessageBox.Show(ReportProperty.Current.GetMessage(MessageCatalog.ReportMessage.REPORT_FINACIAL_RECORDS_CASH_BANK_EMPTY), ReportProperty.Current.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //XtraMessageBox.Show("Select Cash/Bank voucher", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageRender.ShowMessage("Select Cash/Bank voucher");
                }
            }
            else
            {
                //XtraMessageBox.Show("Date To is Less than Date From", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageRender.ShowMessage("Date To is Less than Date From");
            }
        }

        private void dteMonths_EditValueChanged(object sender, EventArgs e)
        {
            CashBankVoucherTransType(Bosco.Report.SQL.ReportSQLCommand.FianacialMode.Add);
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            CashBankVoucherTransType(Bosco.Report.SQL.ReportSQLCommand.FianacialMode.Add);
        }

        private void dteMonths_Properties_Popup(object sender, EventArgs e)
        {
            //DateEdit edit = sender as DateEdit;
            //PopupDateEditForm form = (edit as IPopupControl).PopupWindow as PopupDateEditForm;
            //form.Calendar.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.YearInfo;
        }

        private void gvProject_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == "SELECT")
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            FetchProjects();
            CashBankVoucherTransType(Bosco.Report.SQL.ReportSQLCommand.FianacialMode.Add);
        }

        private void deDateTo_EditValueChanged(object sender, EventArgs e)
        {
            CashBankVoucherTransType(Bosco.Report.SQL.ReportSQLCommand.FianacialMode.Add);
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProject.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
        }

        private void gvProject_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordsCount.Text = "# " + gvProject.RowCount.ToString();
        }

        #endregion

        #region Methods
        private void FetchProjects()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, setttings.RoleId);
                    }

                    dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, deDateFrom.Text);
                    
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            dtProjectDetails = resultArgs.DataSource.Table;
                            ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, "PROJECT_NAME", "PROJECT_ID");
                            glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }


        private void LoadLegalEntityProperties()
        {
            using (CommonMethod cm = new CommonMethod())
            {
                ResultArgs result =  cm.FetchLegalEntityProperties();
                if (result.Success)
                {
                    DataTable dtLegalEntityProperties = result.DataSource.Table;
                    chkListShowLegalDetails.Properties.DataSource = dtLegalEntityProperties;
                    chkListShowLegalDetails.Properties.ValueMember =  appSchema.LegalEntity.LEGALENTITY_FIELD_NAMEColumn.ColumnName;
                    chkListShowLegalDetails.Properties.DisplayMember = appSchema.LegalEntity.LEGALENTITY_DISPLAY_NAMEColumn.ColumnName;
                }
            }
        }
   

        private void BindCashBankReceipts(Bosco.Utility.FinacialTransType transType, Bosco.Report.SQL.ReportSQLCommand.FianacialMode finacialMode)
        {
            DataTable dtVouchers = new DataTable();
            int projectId = 0;
            string DateFrom = string.Empty;
            string DateTo = string.Empty;

            if (finacialMode == Bosco.Report.SQL.ReportSQLCommand.FianacialMode.Add)
            {
                projectId = glkpProject.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                DateTime dteFirstDay = deDateFrom.DateTime;
                DateFrom = dteFirstDay.Date.ToShortDateString();
                DateTo = deDateTo.DateTime.ToShortDateString();
            }
            else
            {
                projectId = ReportProperty.Current.CashBankProjectId;
                DateTime dteFirstDay = deDateFrom.DateTime;
                DateFrom = dteFirstDay.Date.ToShortDateString();
                DateTo = deDateTo.DateTime.ToShortDateString();
            }
            //On 01/02/2018, to show contra voucher also
            Bosco.DAO.Schema.SQLCommand.Voucher sqlcmd = (transType == FinacialTransType.RC? 
                                    SQLCommand.Voucher.CashBankVoucherReceipts : 
                                 transType == FinacialTransType.PY ? SQLCommand.Voucher.CashBankVoucher :
                                 transType == FinacialTransType.CN ? 
                                 SQLCommand.Voucher.CashBankVoucherContra : SQLCommand.Voucher.JournalVoucher);

            using (DataManager dataManager = new DataManager(sqlcmd))
            {
                dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.appSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, DateTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            dtVouchers = resultArgs.DataSource.Table;
            dtVouchers.Columns.Add("SELECT", typeof(Int32));
            if (dtVouchers != null)
            {
                dtBankVouchers = dtVouchers;
                gcProject.DataSource = dtVouchers;
                gcProject.RefreshDataSource();
            }
            chkProjectSelect.Checked = false;
        }
        
        /// <summary>
        /// Bind GST Invoice
        /// </summary>
        /// <param name="transType"></param>
        /// <param name="finacialMode"></param>
        private void BindGSTInvoices(Bosco.Report.SQL.ReportSQLCommand.FianacialMode finacialMode)
        {
            DataTable dtVouchers = new DataTable();
            int projectId = 0;
            string DateFrom = string.Empty;
            string DateTo = string.Empty;
            string vtype = VoucherSubTypes.JN.ToString();

            if (ReportProperty.Current.ReportId == "RPT-212")
            {
                vtype = VoucherSubTypes.RC.ToString();
            }
            
            if (finacialMode == Bosco.Report.SQL.ReportSQLCommand.FianacialMode.Add)
            {
                projectId = glkpProject.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                DateTime dteFirstDay = deDateFrom.DateTime;
                DateFrom = dteFirstDay.Date.ToShortDateString();
                DateTo = deDateTo.DateTime.ToShortDateString();
            }
            else
            {
                projectId = ReportProperty.Current.CashBankProjectId;
                DateTime dteFirstDay = deDateFrom.DateTime;
                DateFrom = dteFirstDay.Date.ToShortDateString();
                DateTo = deDateTo.DateTime.ToShortDateString();
            }

            if (projectId > 0 && UtilityMember.DateSet.ToDate(DateFrom, false) != DateTime.MinValue && UtilityMember.DateSet.ToDate(DateTo,false) != DateTime.MinValue)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Voucher.GSTInvoiceVoucher))
                {
                    dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                    dataManager.Parameters.Add(this.appSchema.Project.DATE_STARTEDColumn, DateFrom);
                    dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, DateTo);
                    dataManager.Parameters.Add(this.appSchema.VoucherMaster.VOUCHER_TYPEColumn, vtype);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                dtVouchers = resultArgs.DataSource.Table;
            }
                        
            dtVouchers.Columns.Add("SELECT", typeof(Int32));
            if (dtVouchers != null)
            {
                dtBankVouchers = dtVouchers;
                gcProject.DataSource = dtVouchers;
                gcProject.RefreshDataSource();
            }

            chkProjectSelect.Checked = false;
        }

        private void SelectedVouchers()
        {
            try
            {
                DataTable dtVoucherSource = gcProject.DataSource as DataTable;
                if (dtVoucherSource != null && dtVoucherSource.Rows.Count != 0)
                {
                    DataView dvSelectedVouchers = dtVoucherSource.DefaultView;
                    dvSelectedVouchers.RowFilter = "SELECT =1";

                    if (dvSelectedVouchers != null && dvSelectedVouchers.Count != 0)
                    {
                        dtVouchers = dvSelectedVouchers.ToTable();
                        foreach (DataRow dr in dvSelectedVouchers.ToTable().Rows)
                        {
                            VoucherIds += dr["Voucher_Id"] + ",";
                        }
                        VoucherIds = VoucherIds.TrimEnd(',');
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void CashBankVoucherTransType(Bosco.Report.SQL.ReportSQLCommand.FianacialMode finacailMode)
        {
            try
            {
                lcShowLegalDetails.Visibility = LayoutVisibility.Never;
                lcShowCostCentre.Visibility = LayoutVisibility.Never;
                switch (ReportProperty.Current.ReportId)
                {
                    case "RPT-025":
                        {
                            lcShowCostCentre.Visibility = LayoutVisibility.Always;
                            BindCashBankReceipts(Bosco.Utility.FinacialTransType.PY, finacailMode);
                            this.Text = "Payment Voucher";
                            break;
                        }
                    case "RPT-024":
                        {
                            lcShowLegalDetails.Visibility = LayoutVisibility.Always;
                            lcShowCostCentre.Visibility = LayoutVisibility.Always;
                            BindCashBankReceipts(Bosco.Utility.FinacialTransType.RC, finacailMode);
                            this.Text = "Receipt Voucher";
                            break;
                        }
                    case "RPT-026":
                        {
                            lcShowCostCentre.Visibility = LayoutVisibility.Always;
                            BindCashBankReceipts(Bosco.Utility.FinacialTransType.JN, finacailMode);
                            this.Text = "Journal Voucher";
                            break;
                        }
                    case "RPT-144":
                        {
                            BindCashBankReceipts(Bosco.Utility.FinacialTransType.RC, finacailMode);
                            this.Text = "Receipt Voucher";
                            break;
                        }
                    case "RPT-151": //On 01/02/2018, to show contra voucher also
                        {
                            BindCashBankReceipts(Bosco.Utility.FinacialTransType.CN, finacailMode);
                            this.Text = "Contra Voucher";
                            break;
                        }
                    case "RPT-207":
                        {
                            BindGSTInvoices(finacailMode);
                            this.Text = "Journal - GST Invoice";
                            break;
                        }
                    case "RPT-212":
                        {
                            BindGSTInvoices(finacailMode);
                            this.Text = "Receipt - GST Invoice";
                            break;
                        }
                    //case "RPT-145":
                    //    {
                    //        BindCashBankReceipts(Bosco.Utility.FinacialTransType.RC, finacailMode);
                    //        this.Text = "Origional Receipt Voucher";
                    //        break;
                    //    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }

        private void SetDefaults()
        {
            deDateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(setttings.YearFrom, false);
            deDateTo.Properties.MaxValue = deDateTo.DateTime = ReportProperty.Current.DateSet.ToDate(setttings.YearTo, false);
            UserRoleId = setttings.RoleId;
        }

        private void AssignSelectedVouchers()
        {
            try
            {
                if (ReportProperty.Current.CashBankVoucher != null && ReportProperty.Current.CashBankVoucher.Rows.Count != 0)
                {
                    CashBankVoucherTransType(Bosco.Report.SQL.ReportSQLCommand.FianacialMode.Edit);
                    glkpProject.EditValue = ReportProperty.Current.CashBankProjectId;
                    deDateFrom.DateTime = ReportProperty.Current.CashBankVoucherDateFrom;
                    deDateTo.DateTime = ReportProperty.Current.CashBankVoucherDateTo;
                    if (dtBankVouchers.Rows.Count != ReportProperty.Current.CashBankVoucher.Rows.Count)
                    {
                        for (int i = 0; i < dtBankVouchers.Rows.Count; i++)
                        {
                            for (int j = 0; j < ReportProperty.Current.CashBankVoucher.Rows.Count; j++)
                            {
                                if (dtBankVouchers.Rows[i]["VOUCHER_ID"].ToString() == ReportProperty.Current.CashBankVoucher.Rows[j]["VOUCHER_ID"].ToString())
                                {
                                    dtBankVouchers.Rows[i]["SELECT"] = 1;
                                    break;
                                }
                            }
                        }

                        gcProject.DataSource = dtBankVouchers;
                        gcProject.RefreshDataSource();
                    }
                    else
                    {
                        gcProject.DataSource = ReportProperty.Current.CashBankVoucher;
                        gcProject.RefreshDataSource();
                        chkProjectSelect.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }

        private void GetLegalEntityId(int ProjectId)
        {
            try
            {
                //For Legal entity
                using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.FetchAll))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                    ReportProperty.dtLedgerEntity = resultArgs.DataSource.Table;
                }


                if (dtProjectDetails != null && dtProjectDetails.Rows.Count != 0)
                {
                    dtProjectDetails.DefaultView.RowFilter = "PROJECT_ID IN (" + ProjectId + ")";
                    if (dtProjectDetails.DefaultView != null && dtProjectDetails.DefaultView.Count != 0)
                    {
                        ReportProperty.Current.LedgalEntityId = dtProjectDetails.DefaultView.ToTable().Rows[0]["CUSTOMERID"] != DBNull.Value ? dtProjectDetails.DefaultView.ToTable().Rows[0]["CUSTOMERID"].ToString() : string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }
        #endregion

        private void chkIncludeSign1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}