using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraLayout.Utils;
using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using Bosco.Model.Transaction;
using ACPP.Modules;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;

namespace ACPP.Modules.Master
{
    public partial class frmProjectAdd : frmFinanceBaseAdd
    {

        #region Event Handlers
        public event EventHandler UpdateHeld;
        #endregion

        #region Decelaration
        private static DataTable dtVoucherTypes = new DataTable();
        ResultArgs resultArgs = null;
        private int projectId = 0;
        private int count = 0;
        private int PurposeId = 0;
        DialogResult mappingDialogResult = DialogResult.Cancel;
        public string CheckSelected = "SELECT";
        public string TempSelect = "SELECT_TMP";

        DataTable Mapping = null;
        DataTable dtSelectedLoadLedgers = null;
        private DataTable dtAllProjectLedgerApplicable { get; set; }
        int DivisonId = 0;

        #endregion

        #region Constructor
        public frmProjectAdd()
        {
            InitializeComponent();
            RealColumnEditTransAmount();
        }

        public frmProjectAdd(int ProjectId)
            : this()
        {
            this.projectId = ProjectId;
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the details of projects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProjectAdd_Load(object sender, EventArgs e)
        {
            SetDefaults();
            LoadUserControlInputData();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkSocietyName.Properties.Buttons[1].Visible = true;
                glkpProjectCategory.Properties.Buttons[1].Visible = true;
            }


            txtProjectCode.Focus();
            SendKeys.Send("{tab}");
        }

        /// <summary>
        /// Save project details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateProjectDetails())
                {
                    using (ProjectSystem projectSystem = new ProjectSystem())
                    {
                        projectSystem.ProjectId = projectId == 0 ? (int)AddNewRow.NewRow : projectId;
                        projectSystem.ProjectCode = txtProjectCode.Text.Trim().ToUpper();
                        projectSystem.ProjectName = txtProject.Text.Trim();
                        projectSystem.ProjectCategroyId = this.UtilityMember.NumberSet.ToInteger(glkpProjectCategory.EditValue.ToString());
                        projectSystem.Description = txtmemoDescription.Text.Trim();
                        projectSystem.StartedOn = detProjectDateStarted.DateTime;
                        projectSystem.Closed_On = detProjectDateClosed.DateTime;
                        projectSystem.DivisionId = this.UtilityMember.NumberSet.ToInteger(lkpDivision.EditValue.ToString());
                        projectSystem.Notes = txtmeNotes.Text.Trim();
                        projectSystem.ClosedBy = detProjectDateClosed.Enabled ? 0 : 1; //0 - Closed By BO, 1- Closed by HO

                        projectSystem.dtAllProjectLedgerApplicable = dtAllProjectLedgerApplicable;
                        DataTable dtSelectedProject = FetchSelectedLedger();
                        projectSystem.dtMapLedger = dtSelectedProject;
                        projectSystem.dtLedgerAmountMadeZero = GetLedgerAmountMadeZero(dtSelectedProject);


                        if (dtSelectedProject != null)
                        {
                            DataView dvSelectedFiltered = new DataView(dtSelectedProject);
                            dvSelectedFiltered.RowFilter = "SELECT=1 AND LEGAL_ENTITY_LEDGER_ID<>0 AND TYPE='Bank Accounts'";
                            dtSelectedProject = dvSelectedFiltered.ToTable();

                            int LegalEntityId = dtSelectedProject.Rows.Count > 0 ? UtilityMember.NumberSet.ToInteger(dtSelectedProject.Rows[0]["CUSTOMER_ID"].ToString()) : 0;
                            DataView dvLegalEntityFilter2 = new DataView(dtSelectedProject);
                            dvLegalEntityFilter2.RowFilter = String.Format("CUSTOMER_ID={0}", LegalEntityId);
                            DataTable dtFilteredEntity2 = dvLegalEntityFilter2.ToTable();

                            if (dtFilteredEntity2.Rows.Count == dtSelectedProject.Rows.Count)
                            {
                                int DivisionId = lkpDivision.EditValue != null ? UtilityMember.NumberSet.ToInteger(lkpDivision.EditValue.ToString()) : 0;
                                if (DivisionId == 1)
                                {
                                    DataView dvDivisionFilter = new DataView(dtFilteredEntity2);
                                    dvDivisionFilter.RowFilter = "LEGAL_ENTITY_LEDGER_ID<>0";
                                    DataTable dtDivsionFiltered = dvDivisionFilter.ToTable();
                                    if (dtDivsionFiltered.Rows.Count == 0)
                                    {
                                        projectSystem.MapProjectId = projectId;
                                        projectSystem.LegalEntityId = (glkSocietyName.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkSocietyName.EditValue.ToString()) : 0;
                                        projectSystem.ContributionId = (glkpPurpose.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpPurpose.EditValue.ToString()) : 0;
                                        resultArgs = projectSystem.SaveProject();
                                        if (resultArgs.Success)
                                        {
                                            this.ReturnValue = resultArgs.RowUniqueId;
                                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
                                            //gcLedgerProject.DataSource = null;
                                            mappingDialogResult = DialogResult.OK;
                                            ClearControls();
                                            txtCode.Focus();
                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                            // SetDefaults();
                                            //   SetTitle();
                                            //   LoadProjectCategory();
                                            //   LoadProjectCodes();
                                            //FetchProjectDetails();
                                            //    BindDivision();
                                            LoadLedgerProject();
                                            //BindSociety();
                                            if (UpdateHeld != null)
                                            {
                                                UpdateHeld(this, e);
                                            }

                                        }
                                    }
                                    else ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.LOCAL_PROJECT_RESTRICTED_WITH_BANK_ACCOUNT));

                                }
                                else
                                {
                                    projectSystem.MapProjectId = projectId;
                                    projectSystem.LegalEntityId = (glkSocietyName.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkSocietyName.EditValue.ToString()) : 0;
                                    projectSystem.ContributionId = (glkpPurpose.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpPurpose.EditValue.ToString()) : 0;
                                    resultArgs = projectSystem.SaveProject();
                                    if (resultArgs.Success)
                                    {
                                        //gcLedgerProject.DataSource = null;
                                        mappingDialogResult = DialogResult.OK;
                                        ClearControls();
                                        txtCode.Focus();
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                        // SetDefaults();
                                        //   SetTitle();
                                        //   LoadProjectCategory();
                                        //   LoadProjectCodes();
                                        //FetchProjectDetails();
                                        //    BindDivision();
                                        LoadLedgerProject();
                                        //   BindSociety();
                                        if (UpdateHeld != null)
                                        {
                                            UpdateHeld(this, e);
                                        }
                                    }
                                }
                            }
                            else ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.BANKACCOUNT_NOT_BELONG_TO_SAME_LEGALENTITY));
                        }
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
        /// Close the form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Set border color for project Name when it is empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProject_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtProject);
            txtProject.Text = this.UtilityMember.StringSet.ToSentenceCase(txtProject.Text);
        }

        private void detProjectDateStarted_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(detProjectDateStarted);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.projectId = 0;
            ClearControls();
            SetDefaults();
            txtCode.Focus();
            SetTitle();
        }

        private void glkpProjectCategory_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadCategory();
            }
        }

        public void LoadCategory()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmProjectCategoryAdd frmProCategoryAdd = new frmProjectCategoryAdd();
                frmProCategoryAdd.ShowDialog();
                if (frmProCategoryAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadProjectCategory();
                    if (frmProCategoryAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmProCategoryAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpProjectCategory.EditValue = this.UtilityMember.NumberSet.ToInteger(frmProCategoryAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
            //glkpProjectCategory.EditValue = glkpProjectCategory.Properties.GetKeyValue(0);
        }

        /// <summary>
        /// On 23/09/2023, To get Project Ledger's Applicable 
        /// </summary>
        private void FetchProjectLedgerApplicable()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                dtAllProjectLedgerApplicable = null;

                resultArgs = mappingSystem.FetchProjectLedgerApplicableByLedgerId(projectId);
                if (resultArgs != null && resultArgs.Success)
                {
                    dtAllProjectLedgerApplicable = resultArgs.DataSource.Table;
                    dtAllProjectLedgerApplicable.DefaultView.Sort = mappingSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName;
                    dtAllProjectLedgerApplicable = dtAllProjectLedgerApplicable.DefaultView.ToTable();
                }
            }
        }

        private void gvLedgerProject_Click(object sender, EventArgs e)
        {
            //DataTable dtLedgers = (DataTable)gcLedgerProject.DataSource;
            //if (dtLedgers != null && dtLedgers.Rows.Count > 0)
            //{
            //    if (this.UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetFocusedRowCellValue(gvColLedgerId).ToString()) != 1)
            //    {
            //        int Selected = (gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, CheckSelected)) != null ? UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, CheckSelected).ToString()) : 0;
            //        Double Amount = (gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColAmount.FieldName)) != null ? UtilityMember.NumberSet.ToDouble(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColAmount.FieldName).ToString()) : 0;
            //        int GroupId = gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColGroupId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColGroupId).ToString()) : 0;
            //        //int select = this.UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetFocusedRowCellValue(colSelect).ToString());
            //        //GroupId = this.UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetFocusedRowCellValue(gvColGroupId).ToString());
            //        if (GroupId == (int)FixedLedgerGroup.FixedDeposit)
            //        {
            //            int FDLedgerId = this.UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetFocusedRowCellValue(gvColLedgerId).ToString());
            //            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            //            {
            //                fdAccountSystem.ProjectId = projectId;
            //                fdAccountSystem.LedgerId = FDLedgerId;
            //                string fdTransType = fdAccountSystem.FetchProjectFDLedgerId();
            //                if (!string.IsNullOrEmpty(fdTransType))
            //                {
            //                    if (fdTransType == FDTypes.OP.ToString())
            //                        //XtraMessageBox.Show("Cannot unmap, opening Balance is set for this ledger.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.FD_OPENING_UNMAPPING));
            //                    else
            //                        // XtraMessageBox.Show("Cannot unmap, Investment is made for this ledger", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.FD_INVESTMENT_UNMAPPING));
            //                }
            //            }
            //        }
            //        else
            //        {
            //            //  gvLedgerProject.SetFocusedRowCellValue(colSelect, select == 0 ? 1 : 0);
            //            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            //            {
            //                int LedgerId = (gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColLedgerId.FieldName)) != null ? UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColLedgerId.FieldName).ToString()) : 0;
            //                voucherTransaction.ProjectId = projectId;
            //                resultArgs = voucherTransaction.MadeTransaction(LedgerId.ToString());
            //                //if row count is zero than no transaction is made
            //                if (resultArgs.DataSource.Table.Rows.Count == 0)
            //                {
            //                    //Ledger Id =1, For Cash ledger validation fails
            //                    //Selected =1, For mapped and Selected =0, unmapped
            //                    if (Amount > 0 && LedgerId != 1 && Selected == 1)
            //                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.FD_CANNOT_UNMAP_MAKE_ZERO));
            //                    else gvLedgerProject.SetFocusedRowCellValue(TempSelect, 1 - Selected);
            //                }
            //                else
            //                    XtraMessageBox.Show("Transaction is made for this ledger already,It can not be unmapped");
            //            }
            //        }
            //    }
            //}
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F2))
            {
                LoadSociety();
            }
            if (KeyData == (Keys.Alt | Keys.A))
            {
                LoadCategory();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void glkpPurpose_EditValueChanged(object sender, EventArgs e)
        {
            //if (isTransactionExistsforPurpose==true)
            //{
            //    CheckTransactionExistsForPurpose();
            //}
        }

        #endregion

        #region Methods
        /// <summary>
        /// Validate the mandatory fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateProjectDetails()
        {
            bool isProject = true;
            if (glkSocietyName.EditValue == null || glkSocietyName.EditValue.ToString() == "0")
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.SOCIETY_NAME_EMPTY));
                this.SetBorderColor(glkSocietyName);
                glkSocietyName.Focus();
                isProject = false;
            }
            else if (string.IsNullOrEmpty(txtProject.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_NAME_EMPTY));
                this.SetBorderColor(txtProject);
                txtProject.Focus();
                isProject = false;
            }
            else if (glkpProjectCategory.EditValue == null || glkpProjectCategory.EditValue.ToString() == "0")
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_CATEGORY_EMPTY));
                this.SetBorderColor(glkpProjectCategory);
                glkpProjectCategory.Focus();
                isProject = false;
            }
            else if (detProjectDateStarted.Text.Trim() == string.Empty)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_START_DATE_EMPTY));
                detProjectDateStarted.Focus();
                isProject = false;
            }
            else if (detProjectDateClosed.Text.Trim() != string.Empty)
            {
                if (!this.UtilityMember.DateSet.ValidateDate(detProjectDateStarted.DateTime, detProjectDateClosed.DateTime))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_DATE_VALIDATION));
                    detProjectDateClosed.Focus();
                    isProject = false;
                }
                else
                {
                    if (projectId > 0)
                    {
                        using (VoucherTransactionSystem vouchertranssystem = new VoucherTransactionSystem())
                        {
                            ResultArgs resultArgs = vouchertranssystem.CheckTransVoucherDetailsByDateProject(projectId, detProjectDateClosed.DateTime);
                            if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                            {
                                this.ShowMessageBox("Transaction is made for this Closed date, Project can not be closed.");
                                detProjectDateClosed.Focus();
                                isProject = false;
                            }
                        }
                    }
                }
            }
            //else if (this.UtilityMember.NumberSet.ToInteger(lkpDivision.EditValue.ToString()) == (int)Division.Foreign)
            //{
            //    if (string.IsNullOrEmpty(glkpPurpose.EditValue.ToString()) || glkpPurpose.EditValue.ToString() == "0")
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_PURPOSE_EMPTY));
            //        this.SetBorderColor(glkpPurpose);
            //        glkpPurpose.Focus();
            //        isProject = false;
            //    }
            //    else if (CheckTransactionExistsForPurpose())
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_PURPOSE_TRANSACTION_MADE));
            //        isProject = false;
            //        glkpPurpose.Focus();
            //    }
            //}
            //else if (this.UtilityMember.NumberSet.ToInteger(lkpDivision.EditValue.ToString()) == (int)Division.Local)
            //{
            //    if (CheckTransactionExistsForPurpose())
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_PURPOSE_TRANSACTION_MADE));
            //        isProject = false;
            //        glkpPurpose.Focus();
            //    }
            //}

            return isProject;
        }

        /// <summary>
        /// Clear the controls only in add mode.
        /// </summary>
        private void ClearControls()
        {
            if (projectId == 0)
            {
                txtProjectCode.Text = txtProject.Text = txtmemoDescription.Text = txtmeNotes.Text = string.Empty;
                detProjectDateClosed.Text = string.Empty;
                detProjectDateStarted.Text = string.Empty;
                chkLedgerSelectAll.Checked = false;
                glkpProjectCategory.EditValue = null;
                glkSocietyName.EditValue = null;
                glkpPurpose.EditValue = null;
                detProjectDateClosed.Enabled = true;
            }
            txtProjectCode.Focus();
        }

        /// <summary>
        /// Set Caption of the title.
        /// </summary>
        private void SetTitle()
        {
            this.Text = this.projectId == 0 ? this.GetMessage(MessageCatalog.Master.Project.PROJECT_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Project.PROJECT_EDIT_CAPTION);
        }

        /// <summary>
        /// Fetch project details based on its project id.
        /// </summary>
        private void FetchProjectDetails()
        {
            string msg = "As this Project is closed by Head Office, You can't modify its closed date.";
            try
            {
                if (this.projectId > 0)
                {
                    btnNew.Enabled = false;

                    using (ProjectSystem projectSystem = new ProjectSystem(this.projectId))
                    {
                        txtProjectCode.Text = projectSystem.ProjectCode;
                        txtProject.Text = projectSystem.ProjectName;
                        glkpProjectCategory.Properties.GetKeyValue(this.UtilityMember.NumberSet.ToInteger(projectSystem.ProjectCategroyId.ToString()));
                        glkpProjectCategory.EditValue = projectSystem.ProjectCategroyId.ToString();
                        DivisonId = projectSystem.DivisionId;
                        LoadProjectCategory();
                        BindSociety();
                        BindPurpose();
                        glkSocietyName.EditValue = projectSystem.LegalEntityId;
                        glkpPurpose.EditValue = projectSystem.ContributionId;
                        txtmemoDescription.Text = projectSystem.Description;
                        if (projectSystem.StartedOn == DateTime.MinValue)
                        {
                            detProjectDateStarted.Text = "";
                        }
                        else
                        {
                            detProjectDateStarted.DateTime = projectSystem.StartedOn;
                        }
                        if (projectSystem.Closed_On == DateTime.MinValue)
                        {
                            detProjectDateClosed.Text = string.Empty;
                        }
                        else
                        {
                            detProjectDateClosed.DateTime = projectSystem.Closed_On;

                            //On 04/07/2023, Lock to remove ledger closed date if it is closed by HO
                            detProjectDateClosed.Enabled = (projectSystem.ClosedBy == 0);
                            detProjectDateClosed.ToolTip = (projectSystem.ClosedBy == 1 ? msg : "");
                            lblProjectDateClosed.OptionsToolTip.ToolTip = detProjectDateClosed.ToolTip; 
                        }

                        // lkpDivision.Text = projectSystem.DivisionName;
                        txtmeNotes.Text = projectSystem.Notes;
                    }
                    // isTransactionExistsforPurpose = true;
                }
                FetchProjectLedgerApplicable();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }


        /// <summary>
        /// Bind Divison to lookup edit contrls
        /// </summary>
        private void BindDivision()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    resultArgs = projectSystem.FetchDivision();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpDivision, resultArgs.DataSource.Table, projectSystem.AppSchema.Division.DIVISIONColumn.ColumnName.ToString(), projectSystem.AppSchema.Division.DIVISION_IDColumn.ColumnName.ToString());
                        if (projectId == 0)
                        {
                            lkpDivision.EditValue = lkpDivision.Properties.GetDataSourceValue(lkpDivision.Properties.ValueMember, 0);
                        }
                        else
                        {
                            lkpDivision.EditValue = DivisonId == 1 ? lkpDivision.Properties.GetDataSourceValue(lkpDivision.Properties.ValueMember, 0) : lkpDivision.Properties.GetDataSourceValue(lkpDivision.Properties.ValueMember, 1);
                        }
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
        /// Bind Society Name to lookup edit contrls
        /// </summary>
        private void BindSociety()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    resultArgs = projectSystem.FetchSocietyName();
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkSocietyName, resultArgs.DataSource.Table, projectSystem.AppSchema.Project.SOCIETYNAMEColumn.ColumnName, projectSystem.AppSchema.Project.CUSTOMERIDColumn.ColumnName);
                    // if (projectId == 0) { glkSocietyName.EditValue = glkSocietyName.Properties.GetKeyValue(0); }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Bind Purpose for projects
        /// </summary>
        private void BindPurpose()
        {
            try
            {
                using (PurposeSystem purposesystem = new PurposeSystem())
                {

                    resultArgs = purposesystem.FetchPurposeDetails();
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPurpose, resultArgs.DataSource.Table, purposesystem.AppSchema.Project.FC_PURPOSEColumn.ColumnName, purposesystem.AppSchema.Project.CONTRIBUTION_IDColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Load Project Category
        /// </summary>
        private void LoadProjectCategory()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                resultArgs = projectSystem.FetchProjectCategroy();
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProjectCategory, resultArgs.DataSource.Table, projectSystem.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName, projectSystem.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName);
                //if (projectId == 0) { glkpProjectCategory.EditValue = glkpProjectCategory.Properties.GetKeyValue(0); }
            }
        }

        /// <summary>
        /// Set Project Start date from the account Period
        /// </summary>
        private void SetDefaults()
        {
            //DateTime dtYearFrom = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            // DateTime dtBookBeginFrom = this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            // detProjectDateStarted.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            //detProjectDateStarted.Properties.MinValue = dtYearFrom > dtBookBeginFrom ? dtYearFrom : dtBookBeginFrom;
            //detProjectDateStarted.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            SetTitle();
            //if (this.projectId == 0) { SetDefaults(); }         
            LoadProjectCategory();
            FetchProjectDetails();
            BindDivision();
            if (this.UtilityMember.NumberSet.ToInteger(lkpDivision.EditValue.ToString()) == (int)Division.Foreign)
            {
                lciPurpose.Text = this.GetMessage(MessageCatalog.Master.Project.PURPOSE) + "<color=red>*";
                lciPurpose.AllowHtmlStringInCaption = true;
                lciPurpose.Visibility = LayoutVisibility.Always;
                glkpPurpose.Visible = true;
                BindPurpose();
            }
            else
            {
                lciPurpose.Text = this.GetMessage(MessageCatalog.Master.Project.PURPOSE);
                lciPurpose.AllowHtmlStringInCaption = false;
                BindPurpose();
            }
            LoadLedgerProject();
            BindSociety();
        }

        private void LoadLedgerProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.LoadAllLedgerByProjectId(projectId);
                    if (resultArgs.DataSource.Table != null)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerProject.DataSource = dv.ToTable();

                        DataView dvSelectedProjects = new DataView(dv.ToTable());
                        dvSelectedProjects.RowFilter = String.Format("{0}=1", CheckSelected);
                        dtSelectedLoadLedgers = dvSelectedProjects.ToTable();
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message);
            }
        }

        private DataTable AddColumns(DataTable dtLedgers)
        {
            DataTable dtAddedledger = dtLedgers;
            if (!dtAddedledger.Columns.Contains(CheckSelected))
            {
                dtAddedledger.Columns.Add(CheckSelected, typeof(int));
            }
            return dtAddedledger;
        }

        private void gvLedgerProject_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (gvLedgerProject.DataSource != null)
                {
                    if (e.RowHandle >= 0)
                    {

                        if (gvLedgerProject.GetRowCellValue(e.RowHandle, gvColGroupId) != null && gvLedgerProject.GetRowCellValue(e.RowHandle, gvColAmount) != null)
                        {
                            int GroupId = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(e.RowHandle, gvColGroupId).ToString());
                            decimal Amount = UtilityMember.NumberSet.ToDecimal(gvLedgerProject.GetRowCellValue(e.RowHandle, gvColAmount).ToString());
                            if (GroupId == 14)
                            {
                                e.Appearance.BackColor = Color.LightGray;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void gvLedgerProject_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataTable dtLedgers = gcLedgerProject.DataSource as DataTable;
            if (dtLedgers != null && dtLedgers.Rows.Count != 0)
            {
                int Selected = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, CheckSelected).ToString());
                Double Amount = UtilityMember.NumberSet.ToDouble(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColAmount.FieldName).ToString());
                int GroupId = gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColGroupId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColGroupId).ToString()) : 0;
                if (GroupId == (int)FixedLedgerGroup.FixedDeposit)
                {
                    if (gvLedgerProject.FocusedColumn.Name.Equals("gvColAmount"))
                    {
                        e.Cancel = true;
                        this.gvLedgerProject.ShowingEditor -= new System.ComponentModel.CancelEventHandler(this.gvLedgerProject_ShowingEditor);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.MAPPING_LEDGER_CANNOT_SET_OP_FD_LEDGER));
                        this.gvLedgerProject.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvLedgerProject_ShowingEditor);
                    }
                    if (gvLedgerProject.FocusedColumn.Name.Equals("colSelect") || gvLedgerProject.FocusedColumn.Name.Equals("gvColFlag"))
                    {
                        int LedgerId = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColLedgerId.FieldName).ToString());
                        //Ledger Id =1, For Cash ledger validation fails
                        //Selected =1, For mapped and Selected =0, unmapped
                        if (Amount > 0 && LedgerId != 1 && Selected == 1)
                        {
                            e.Cancel = true;
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.FD_UNMAPPING));
                        }
                    }
                }
                else
                {
                    if (gvLedgerProject.FocusedColumn.Name.Equals("colSelect") || gvLedgerProject.FocusedColumn.Name.Equals("gvColLedgerName"))
                    {
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            int LedgerId = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColLedgerId.FieldName).ToString());
                            voucherTransaction.ProjectId = projectId;
                            resultArgs = voucherTransaction.MadeTransaction(LedgerId.ToString());
                            //if row count is zero than no transaction is made
                            if (resultArgs.DataSource.Table.Rows.Count == 0)
                            {
                                //Ledger Id =1, For Cash ledger validation fails
                                //Selected =1, For mapped and Selected =0, unmapped
                                if (Amount > 0 && LedgerId != 1 && Selected == 1)
                                {
                                    e.Cancel = true;
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.FD_CANNOT_UNMAP_MAKE_ZERO));
                                }
                            }
                            else this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.TRANSACTION_MADE_CANT_UNMAP));
                        }
                    }
                }
            }
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvLedgerProject.PostEditor();
            gvLedgerProject.UpdateCurrentRow();
            if (gvLedgerProject.ActiveEditor == null)
            {
                gvLedgerProject.ShowEditor();
            }//
            //int Selected = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColchkSelectAll.FieldName).ToString());
            //double Amount = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColAmount.FieldName).ToString());
            int Selected = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, colSelect.FieldName).ToString());
            double Amount = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColAmount.FieldName).ToString());
            if (Selected == 0 && Amount > 0)
                gvLedgerProject.SetFocusedRowCellValue(colSelect, 1);
        }

        private void RealColumnEditTransAmount()
        {
            gvColAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvLedgerProject.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvLedgerProject.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == gvColAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvLedgerProject.ShowEditorByMouse();
                    }));
                }
            };


        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLedgerProject.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLedgerProject, gvColLedgerName);
            }
        }

        private void chkLedgerSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowWaitDialog();
            CheckOrUncheckAllLedger();
            this.CloseWaitDialog();
        }

        private void CheckOrUncheckAllLedger()
        {
            bool HasTransaction = false;
            DataTable dtAllLedger = (DataTable)gcLedgerProject.DataSource;
            if (dtAllLedger != null && dtAllLedger.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAllLedger.Rows)
                {
                    if (projectId != 0)//Validate against transaction only in edit Mode
                    {
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.ProjectId = projectId;
                            resultArgs = voucherTransaction.MadeTransaction(dr["LEDGER_ID"].ToString());
                            //if row count is zero than no transaction is made
                            if (resultArgs.DataSource.Table.Rows.Count == 0)
                            {
                                dr["SELECT"] = (dr["LEDGER_ID"].ToString() == "1") ? true : chkLedgerSelectAll.Checked;
                            }
                            else
                            {
                                HasTransaction = true;
                                ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.TRANSACTION_MADE_SOME_LEDGER_CANT_UNMAP));
                                break;
                            }
                        }
                    }
                    else
                        dr["SELECT"] = (dr["LEDGER_ID"].ToString() == "1") ? true : chkLedgerSelectAll.Checked;
                }
                gcLedgerProject.DataSource = HasTransaction ? (DataTable)gcLedgerProject.DataSource : dtAllLedger;
            }
        }

        private void gvLedgerProject_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == CheckSelected)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void frmProjectAdd_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void rchkSelect_CheckedChanged(object sender, EventArgs e)
        {
            DataRow drLedger = null;
            drLedger = gvLedgerProject.GetDataRow(gvLedgerProject.FocusedRowHandle);
            int accessFlag = drLedger != null ? UtilityMember.NumberSet.ToInteger(drLedger["ACCESS_FLAG"].ToString()) : 0;
            CheckEdit chkselect = (CheckEdit)sender;
            //if (gvLedgerProject.GetFocusedRowCellValue(gvColLedgerId) != null)
            //{
            //    if (gvLedgerProject.GetFocusedRowCellValue(gvColLedgerId).ToString() == "1")
            //        chkselect.Checked = true;
            //}

            //By Aldrin. To validate default ledgers with acces flag.
            if (accessFlag != 0 && accessFlag == (int)AccessFlag.Readonly)
            {
                chkselect.Checked = true;
            }
            if (gvLedgerProject.GetFocusedRowCellValue(gvColLedgerId) != null && this.UtilityMember.NumberSet.ToDecimal(gvLedgerProject.GetFocusedRowCellValue(gvColAmount).ToString()) != 0)
            {
                if (gvLedgerProject.GetFocusedRowCellValue(gvColGroupId).ToString() == "14" && this.UtilityMember.NumberSet.ToDecimal(gvLedgerProject.GetFocusedRowCellValue(gvColAmount).ToString()) > 0)
                    chkselect.Checked = true;
            }

        }

        public DataTable FetchSelectedLedger()
        {
            Mapping = gcLedgerProject.DataSource as DataTable;
            DataView dv = new DataView(Mapping);
            dv.RowFilter = "SELECT=1";
            DataTable SelectedLedgers = dv.ToTable();
            return SelectedLedgers;
        }

        private DataTable GetLedgerAmountMadeZero(DataTable dtSelectedLedgers)
        {
            DataTable dtAmountZeroLedgers = (gcLedgerProject.DataSource as DataTable).Clone();
            if (dtSelectedLoadLedgers != null && dtSelectedLedgers != null)
            {
                foreach (DataRow drItem in dtSelectedLoadLedgers.Rows)
                {
                    DataView dvFindprojects = new DataView(dtSelectedLedgers);
                    dvFindprojects.RowFilter = String.Format("LEDGER_ID={0}", UtilityMember.NumberSet.ToInteger(drItem["LEDGER_ID"].ToString()));
                    if (dvFindprojects.ToTable().Rows.Count == 0)
                    {
                        dtAmountZeroLedgers.ImportRow(drItem);

                    }
                }
                if (dtAmountZeroLedgers != null && dtAmountZeroLedgers.Rows.Count > 0)
                {
                    dtAmountZeroLedgers.Select().ToList<DataRow>().ForEach(r => { r["AMOUNT"] = 0; });
                }
            }
            return dtAmountZeroLedgers;
        }

        private bool CheckTransactionExistsForPurpose()
        {
            bool isTransactionExistsforPurpose = false;
            ResultArgs resultargs = null;
            DataTable dtProjectTransaction = new DataTable();
            using (ProjectSystem projectsystem = new ProjectSystem(this.projectId))
                if (projectId != 0 && projectsystem.ContributionId != 0)
                {
                    resultargs = projectsystem.FetchTansDeatilsByProjectId(this.projectId, projectsystem.ContributionId);
                    dtProjectTransaction = resultargs.DataSource.Table;
                    if (dtProjectTransaction != null & dtProjectTransaction.Rows.Count > 0)
                    {
                        //projectId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.PROJECT_IDColumn.ColumnName].ToString());
                        count = this.UtilityMember.NumberSet.ToInteger(dtProjectTransaction.Rows[0]["PURPOSE"].ToString());
                        if (count > 0)
                        {
                            PurposeId = this.UtilityMember.NumberSet.ToInteger(dtProjectTransaction.Rows[0]["PURPOSE_ID"].ToString());
                            glkpPurpose.EditValue = glkpPurpose.Properties.GetKeyValue(PurposeId - 1);
                            isTransactionExistsforPurpose = true;
                            //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_PURPOSE_TRANSACTION_MADE));
                        }
                    }
                }
            return isTransactionExistsforPurpose;
        }

        /// <summary>
        /// Load Project Used Codes
        /// </summary>
        private void LoadUserControlInputData()
        {
        }
        #endregion

        private void glkSocietyName_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {

                LoadSociety();
            }
        }

        public void LoadSociety()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmLegalEntity frmlegalentity = new frmLegalEntity();
                frmlegalentity.ShowDialog();
                if (frmlegalentity.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    BindSociety();
                    if (frmlegalentity.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmlegalentity.ReturnValue.ToString()) > 0)
                    {
                        glkSocietyName.EditValue = this.UtilityMember.NumberSet.ToInteger(frmlegalentity.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
            //glkSocietyName.EditValue = glkSocietyName.Properties.GetKeyValue(0);
        }

        private void gcLedgerProject_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Tab)
                {
                    if (gvLedgerProject.IsLastRow)
                    {
                        if (gvLedgerProject.FocusedColumn == gvColFlag)
                        {
                            txtmeNotes.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
        }

        #region Rights
        private void ApplyRights()
        {
            bool projectcategoryrights = (CommonMethod.ApplyUserRights((int)ProjectCategory.CreateProjectCategory) != 0);
            glkpProjectCategory.Properties.Buttons[1].Visible = projectcategoryrights;

            bool createlegalEntity = (CommonMethod.ApplyUserRights((int)LegalEntity.CreateLegalEntity) != 0);
            glkSocietyName.Properties.Buttons[1].Visible = createlegalEntity;


            //if (CommonMethod.ApplyUserRights((int)ProjectCategory.CreateProjectCategory) != 0)
            //{
            //    glkpProjectCategory.Properties.Buttons[1].Visible = true;
            //    if (CommonMethod.ApplyUserRights((int)LegalEntity.CreateLegalEntity) != 0)
            //    {
            //        glkSocietyName.Properties.Buttons[1].Visible = true;
            //    }
            //    else
            //    {
            //        glkSocietyName.Properties.Buttons[1].Visible = false;
            //    }
            //}
            //else
            //{
            //    glkpProjectCategory.Properties.Buttons[1].Visible = false;
            //    if (CommonMethod.ApplyUserRights((int)LegalEntity.CreateLegalEntity) != 0)
            //    {
            //        glkSocietyName.Properties.Buttons[1].Visible = true;
            //    }
            //    else
            //    {
            //        glkSocietyName.Properties.Buttons[1].Visible = false;
            //    }
            //}

        }
        #endregion

        private void detProjectDateClosed_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                detProjectDateClosed.Text = null;
            }
        }

        private void gvLedgerProject_RowClick(object sender, RowClickEventArgs e)
        {
            UnmapLedgers();
        }

        private void UnmapLedgers()
        {
            DataTable dtLedger = (DataTable)gcLedgerProject.DataSource;
            if (dtLedger.Rows.Count != 0)
            {
                // int Selected = (gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, TempSelect)) != null ? UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, TempSelect).ToString()) : 0;
                int Selected = (gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, CheckSelected)) != null ? UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, CheckSelected).ToString()) : 0;
                Double Amount = (gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColAmount.FieldName)) != null ? UtilityMember.NumberSet.ToDouble(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColAmount.FieldName).ToString()) : 0;
                int GroupId = gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColGroupId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColGroupId).ToString()) : 0;
                if (GroupId == (int)FixedLedgerGroup.FixedDeposit)
                {
                    if (gvLedgerProject.FocusedColumn.Name.Equals("gvColAmount"))
                    {
                        this.gvLedgerProject.ShowingEditor -= new System.ComponentModel.CancelEventHandler(this.gvLedgerProject_ShowingEditor);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.MAPPING_LEDGER_CANNOT_SET_OP_FD_LEDGER));
                        this.gvLedgerProject.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvLedgerProject_ShowingEditor);
                    }
                    if (gvLedgerProject.FocusedColumn.Name.Equals("colSelect") || gvLedgerProject.FocusedColumn.Name.Equals("gvColFlag"))
                    {
                        int LedgerId = UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColLedgerId.FieldName).ToString());
                        //Ledger Id =1, For Cash ledger validation fails
                        //Selected =1, For mapped and Selected =0, unmapped
                        if (Amount > 0 && LedgerId != 1 && Selected == 1)
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.FD_UNMAPPING));
                    }
                }
                else
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {

                        int LedgerId = (gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColLedgerId.FieldName)) != null ? UtilityMember.NumberSet.ToInteger(gvLedgerProject.GetRowCellValue(gvLedgerProject.FocusedRowHandle, gvColLedgerId.FieldName).ToString()) : 0;
                        voucherTransaction.ProjectId = projectId;
                        resultArgs = voucherTransaction.MadeTransaction(LedgerId.ToString());
                        //if row count is zero than no transaction is made
                        if (resultArgs.DataSource.Table.Rows.Count == 0)
                        {
                            //Ledger Id =1, For Cash ledger validation fails
                            //Selected =1, For mapped and Selected =0, unmapped
                            if (LedgerId != 1)
                            {
                                if (Amount > 0 && Selected == 1)
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.FD_CANNOT_UNMAP_MAKE_ZERO));
                            }
                        }
                        else
                            XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Project.TRANSACTION_MADE_CANT_UNMAP));
                    }
                }
            }
        }

        private void rchkSelect_Click(object sender, EventArgs e)
        {
            if (projectId != 0)//Validate against transaction only in edit Mode
                UnmapLedgers();
        }

        private void lkpDivision_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(lkpDivision.EditValue.ToString()) == (int)Division.Foreign)
            {
                lciPurpose.Text = this.GetMessage(MessageCatalog.Master.Project.PURPOSE) + "<color=red>*";
                lciPurpose.AllowHtmlStringInCaption = true;
                lciPurpose.Visibility = LayoutVisibility.Always;
                glkpPurpose.Visible = true;
                BindPurpose();
            }
            else
            {
                lciPurpose.Text = this.GetMessage(MessageCatalog.Master.Project.PURPOSE);
                lciPurpose.AllowHtmlStringInCaption = false;
                BindPurpose();
            }
        }

        private void glkpPurpose_Leave(object sender, EventArgs e)
        {
            //CheckTransactionExistsForPurpose();
            if (this.UtilityMember.NumberSet.ToInteger(lkpDivision.EditValue.ToString()) == (int)Division.Foreign)
            {
                this.SetBorderColorForGridLookUpEdit(glkpPurpose);
            }
            else
            {
                if (this.UtilityMember.NumberSet.ToInteger(lkpDivision.EditValue.ToString()) == (int)Division.Local)
                {
                    //this.SetBorderColorForGridLookUpEdit(glkpPurpose);
                    glkpPurpose.Properties.Appearance.BorderColor = Color.Empty;
                }
            }
        }

        private void glkSocietyName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkSocietyName);
        }

        private void glkpProjectCategory_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpProjectCategory);
        }

    }
}