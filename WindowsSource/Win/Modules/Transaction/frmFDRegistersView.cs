//This is to show all the active and CLosed FDs based on the Seletced project and Date range which for "Invested/Openin date to Date of maturity"
//And also it shows the current closing balance Fixed Deposits Assets.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Transaction;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraPrinting;
using Bosco.DAO.Schema;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Business;
using DevExpress.XtraBars;
using ACPP.Modules.Data_Utility;


namespace ACPP.Modules.Transaction
{
    public partial class frmFDRegistersView : frmFinanceBase
    {

        #region Declaration
        ResultArgs resultArgs = null;
        private string SelectedLang = string.Empty;
        public bool IsDateLoaded = false;
        
        #endregion

        #region Properties
        private string projectId = string.Empty;
        private string ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }

        public Int32 FDInvestmentTypeId
        {
            get
            {
                Int32 InvestmentTypId = glkpFDInvestmentType.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpFDInvestmentType.EditValue.ToString()) : 0;
                return InvestmentTypId;
            }
        }

        public DataTable ProID = null;
        private string projectName = string.Empty;
        string pid = string.Empty;
        private string ProjectName
        {
            set
            {
                projectName = value;
            }
            get
            {
                return projectName;
            }
        }
        private DataTable dtRegisters { get; set; }

        private Int32 ProjectId_In_Grid
        {
            get
            {
                Int32 RowIndex = gvTransaction.FocusedRowHandle;
                Int32 pid = gvTransaction.GetFocusedRowCellValue(ColProject_Id) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(ColProject_Id).ToString()) : 0;
                return pid;
            }
        }

        private int FDAccountId
        {
            get
            {
                Int32 RowIndex = gvTransaction.FocusedRowHandle;
                Int32 AccountId = gvTransaction.GetFocusedRowCellValue(colFDAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colFDAccountId).ToString()) : 0;
                return AccountId;
            }
        }

        private string FDAccountNumber
        {
            get
            {
                Int32 RowIndex = gvTransaction.FocusedRowHandle;
                string AccountNo = gvTransaction.GetFocusedRowCellValue(colFDNo) != null ? gvTransaction.GetFocusedRowCellValue(colFDNo).ToString() : string.Empty;
                return AccountNo;
            }
        }

        private string FDAccountCreatedOn
        {
            get
            {
                Int32 RowIndex = gvTransaction.FocusedRowHandle;
                string AccountCreatedOn= gvTransaction.GetFocusedRowCellValue(colDepositOn) != null ? gvTransaction.GetFocusedRowCellValue(colDepositOn).ToString() : string.Empty;
                return AccountCreatedOn;
            }
        }
        #endregion

        #region Constructor

        public frmFDRegistersView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (deDateFrom.DateTime > deTo.DateTime)
            {
                DateTime dt = deTo.DateTime;
                deTo.DateTime = deDateFrom.DateTime;
                deDateFrom.DateTime = dt;
                deTo.DateTime = deDateFrom.DateTime.AddMonths(-1);
                if (glkpProject.EditValue != null)
                {
                    ProjectId = glkpProject.EditValue.ToString();
                    LoadRegisters();
                }
            }
            else
            {
                if (glkpProject.EditValue != null)
                {
                    ProjectId = glkpProject.EditValue.ToString().Equals("0") ? this.AppSetting.UserAllProjectId : glkpProject.EditValue.ToString();
                    LoadRegisters();
                }
            }
        }

        private void frmFDRegistersView_Load(object sender, EventArgs e)
        {
            //dockFDHPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            SetVisibileShortCuts(true, true);
            SelectedLang = this.AppSetting.LanguageId;
            if (SelectedLang == "en-US")
            {
                emptySpaceItem4.Width = 200;
                emptySpaceItem3.Width = 50;
                emptySpaceItem8.Width = 70;
                layoutControlItem2.Width = 200;
                layoutControlItem3.Width = 200;
            }

            LoadProject();
            LoadFDInvestmentType();
            LoadCurrency();
            LoadDefaultDate();
            LoadFDRegisters();
            ApplyUserRights();
            SetAlignment();

            //31/07/2024, Other than India, let us lock TDS Amount
            colTdsAmount.Visible = !(this.AppSetting.IsCountryOtherThanIndia);
            lcCurrency.Visibility = (this.AppSetting.AllowMultiCurrency == 1 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            this.AttachGridContextMenu(this.gcTransaction);
            this.AttachGridContextMenu(ucFDHistory1.GridFDHistory);

            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ucToolBar1.VisibleEditButton = BarItemVisibility.Never;
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTransaction.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTransaction, colFDNo);
                
            }            
        }

        private void frmFDRegistersView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvTransaction_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvTransaction.RowCount.ToString();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcTransaction, "Fixed Deposit Registers", PrintType.DT, gvTransaction, true);
            PrintGridViewDetails(gcTransaction, this.GetMessage(MessageCatalog.Master.Transaction.FDREGISTER_VIEW_PRINT_CAPTION), PrintType.DT, gvTransaction, true);
        }

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                if (FDAccountId > 0 && !string.IsNullOrEmpty(FDAccountNumber))
                {
                    if (ShowConfirmationMessage("Are you sure to delete '" + FDAccountNumber + "' account and its history?\n\n" +
                        "Yes : Delete '" + FDAccountNumber + "' Account and its all details (Opening, Investment and all Renewals)\n\n" +
                        "No  : Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.ShowWaitDialog("Deleting FD Account and refreshing balance");
                        ResultArgs resultArgs = fdAccountSystem.DropFDAccountHistory(FDAccountId, ProjectId_In_Grid,FDAccountCreatedOn);
                        if (resultArgs.Success)
                        {
                            LoadRegisters();
                        }
                        else
                        {
                            this.CloseWaitDialog();
                            MessageRender.ShowMessage("Could not delete selected FD Account " + resultArgs.Message);
                        }
                        this.CloseWaitDialog();
                    }
                }
                else
                {
                    MessageRender.ShowMessage("Select FD Account");
                }
            }
        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            LoadProject();
            LoadFDRegisters();
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            int ProId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            if (ProId > 0)
            {
                ProjectId = glkpProject.EditValue.ToString();
                LoadRegisters();
            }
            else
            {
                ProjectId = !string.IsNullOrEmpty(this.AppSetting.UserAllProjectId) ? this.AppSetting.UserAllProjectId : "0";
                LoadRegisters();
            }
        }

        private void deTo_Leave(object sender, EventArgs e)
        {
            if (deDateFrom.DateTime > deTo.DateTime)
            {
                DateTime dateTo = deTo.DateTime;
                deTo.DateTime = deDateFrom.DateTime;
                deDateFrom.DateTime = dateTo.Date;
            }
        }
        #endregion

        #region Methods

        public void LoadDefaultDate()
        {
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }
        /// <summary>
        /// This is to load the project  to view the FD accounts mapped to the selected Project.
        /// </summary>
        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = deDateFrom.Text;
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            DataTable dtFDregisters = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName);
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, dtFDregisters, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                            glkpProject.EditValue = this.UtilityMember.NumberSet.ToInteger(projectId) != 0 ? projectId : glkpProject.Properties.GetKeyValue(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadCurrency()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryCurrencyDetails(UtilityMember.DateSet.ToDate(AppSetting.YearFrom,false));
                    if (resultArgs.Success)
                    {
                        DataTable dtCurrency = resultArgs.DataSource.Table;
                        dtCurrency.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " > 0" ;
                        dtCurrency = dtCurrency.DefaultView.ToTable();

                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpCurrencyCountry, dtCurrency,
                            countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString(), false,"");
                        glkpCurrencyCountry.EditValue = AppSetting.Country;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Load FD Investment Type
        /// </summary>
        private void LoadFDInvestmentType()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchInvestmentType();
                    glkpFDInvestmentType.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table!=null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtFDinvestmentType = resultArgs.DataSource.Table;
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpFDInvestmentType, dtFDinvestmentType,
                                    mappingSystem.AppSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName, mappingSystem.AppSchema.FDInvestmentType.INVESTMENT_TYPE_IDColumn.ColumnName, true, "<--All-->");
                            glkpFDInvestmentType.EditValue = (Int32)FDInvestmentType.None;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// This is to load the FD Accounts based on the selected Project and Date Range which are 	closed and Active.
        /// </summary>
        /// <params>Project_id,Datefrom and DateTo</params>
        private void LoadRegisters()
        {
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    resultArgs = fdAccountSystem.FetchFDRegistersView(deDateFrom.DateTime, deTo.DateTime, ProjectId, FDInvestmentTypeId);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtRegisters = resultArgs.DataSource.Table;

                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
                            dtRegisters.DefaultView.RowFilter = fdAccountSystem.AppSchema.FDAccount.CURRENCY_COUNTRY_IDColumn.ColumnName + "=" + CountryId;
                            dtRegisters = dtRegisters.DefaultView.ToTable();
                        }

                        gcTransaction.DataSource = dtRegisters;
                        gcTransaction.RefreshDataSource();
                    }
                    else
                    {
                        gcTransaction.DataSource = dtRegisters = null;
                        gcTransaction.RefreshDataSource();
                    }
                    gvTransaction.FocusedRowHandle = 0;
                    gvTransaction.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                    
                    ucFDHistory1.FDAccountId = FDAccountId;
                    ucFDHistory1.ShowPanelCaptionHeader = false;

                    colFDScheme.Visible = false;
                    if (this.UIAppSetting.EnableFlexiFD == "1")
                    {
                        colFDScheme.Visible = true;
                        colFDScheme.VisibleIndex = colFDNo.VisibleIndex + 1;
                    }
                    colReinvestmentAmt.Visible = (this.UIAppSetting.EnableFlexiFD == "1"); //28/11/2018, to lock reinvestment feature based on setting
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }

        

        private void SetAlignment()
        {
            if (!this.AppSetting.LanguageId.Equals("en-US"))
            {
                if (this.AppSetting.LanguageId.Equals("pt-PT"))
                {
                    layoutControlItem1.Width = 400;
                    layoutControlItem2.Width = 190;
                    layoutControlItem3.Width = 190;
                }
                else
                {
                    layoutControlItem1.Width = 400;
                    layoutControlItem2.Width = 230;
                    layoutControlItem3.Width = 230;
                }
            }
        }

        private void LoadFDRegisters()
        {
            if (glkpProject.EditValue != null)
            {
                if (UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()).Equals(0))
                {
                    ProjectId = !string.IsNullOrEmpty(this.AppSetting.UserAllProjectId) ? this.AppSetting.UserAllProjectId : "0";
                    LoadRegisters();
                }
                else
                {
                    ProjectId = glkpProject.EditValue.ToString();
                    gcTransaction.DataSource = null;
                    LoadRegisters();
                }
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(FDRegister.PrintFixedDepositRegister);
            this.enumUserRights.Add(FDRegister.ViewFixedDepositRegister);
            this.enumUserRights.Add(FDInvestment.DeleteFixedInvestment);
            this.enumUserRights.Add(FDInvestment.EditFixedInvestment);

            this.ApplyUserRights(ucToolBar1, this.enumUserRights, (int)Menus.FixedDepositRegister);
            ucToolBar1.VisibleMoveTrans = ucToolBar1.VisibleAddButton = ucToolBar1.VisibleDownloadExcel = ucToolBar1.VisbleInsertVoucher = BarItemVisibility.Never;
            //ucToolBar1.VisibleDeleteButton = BarItemVisibility.Never;
        }
        #endregion

        private void deDateFrom_Leave(object sender, EventArgs e)
        {
            if (IsDateLoaded)
            {
                deTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
                IsDateLoaded = true;
            }
            if (deDateFrom.DateTime > deTo.DateTime)
            {
                //DateTime dateTo = deTo.DateTime;
                //deTo.DateTime = deDateFrom.DateTime;
                //deDateFrom.DateTime = dateTo.Date;
                deDateTo.DateTime = deDateFrom.DateTime;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
              //  deDateFrom.Focus();
                frmDatePicker datePicker = new frmDatePicker(deDateFrom.DateTime, deTo.DateTime, DatePickerType.ChangePeriod);
                datePicker.ShowDialog();
                deDateFrom.DateTime = AppSetting.VoucherDateFrom;
                deTo.DateTime = AppSetting.VoucherDateTo;
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects----
            LoadProject();
            //--------------------------------------
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            FrmFDChangeProject frmFDChangeProject = new FrmFDChangeProject(FDAccountId);
            frmFDChangeProject.ShowDialog();

            if (frmFDChangeProject.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                LoadRegisters();
            }
        }

        private void glkpProject_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void gvTransaction_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucFDHistory1.FDAccountId = FDAccountId;           
        }
       
        private void dockManager1_ActivePanelChanged(object sender, DevExpress.XtraBars.Docking.ActivePanelChangedEventArgs e)
        {
            if (dockManager1.ActivePanel != null)
            {
                dockFDHPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
        }
    }
}