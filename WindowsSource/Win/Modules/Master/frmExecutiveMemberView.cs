/*  Class Name      :frmExecutiveMemberView
 *  Purpose         :To View Executive Member Details
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Data;
using System.Windows.Forms;

using Bosco.Utility;

using Bosco.Model.UIModel;

namespace ACPP.Modules.Master
{
    public partial class frmExecutiveMemberView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmExecutiveMemberView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int executiveId = 0;
        private int ExecutiveId
        {
            get
            {
                RowIndex = gvExecutiveMember.FocusedRowHandle;
                executiveId = gvExecutiveMember.GetFocusedRowCellValue(colExecutiveId) != null ? this.UtilityMember.NumberSet.ToInteger(gvExecutiveMember.GetFocusedRowCellValue(colExecutiveId).ToString()) : 0;
                return executiveId;
            }
            set
            {
                this.executiveId = value;
            }

        }
        private string societyId = string.Empty;
        private string SocietyId
        {
            get
            {
                return societyId;
            }
            set
            {
                societyId = value;
            }
        }

        private DataTable dtSelectedExecutive { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Load the Executive Member Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExecutiveMemberView_Load(object sender, EventArgs e)
        {
            BindSociety();
            LoadDefaultDate();
            ApplyUserRights();
        }

        private void frmExecutiveMemberView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchExecutiveMemberDetails();
            this.Text = this.GetMessage(MessageCatalog.Master.ExecutiveMembers.GOVERNING_BODIES);
        }

        public void LoadDefaultDate()
        {
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(GoverningMembers.CreateGoverningMember);
            this.enumUserRights.Add(GoverningMembers.EditGoverningMember);
            this.enumUserRights.Add(GoverningMembers.DeleteGoverningMember);
            this.enumUserRights.Add(GoverningMembers.PrintGoverningMember);
            this.enumUserRights.Add(GoverningMembers.ViewGoverningMembers);
            this.ApplyUserRights(ucToolBarExecutiveMember, enumUserRights, (int)Menus.GoverningMembers);
        }
        /// <summary>
        /// To add new Executive Member Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarExecutiveMember_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }
        /// <summary>
        /// Edit the Member Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarExecutiveMember_EditClicked(object sender, EventArgs e)
        {
            ShowEditExecutiveForm();
        }
        private void ucToolBarExecutiveMember_DeleteClicked(object sender, EventArgs e)
        {
            DeleteExecutiveMemberDetails();
        }

        /// <summary>
        /// Edit the Member Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcExecutiveMember_DoubleClick(object sender, EventArgs e)
        {
            ShowEditExecutiveForm();
        }

        /// <summary>
        /// set the Record Count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvExecutiveMember_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvExecutiveMember.RowCount.ToString();
        }

        /// <summary>
        /// To Print Executive Member Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarExecutiveMember_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcExecutiveMember, this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_PRINT_CAPTION), PrintType.DT, gvExecutiveMember, true);
        }

        /// <summary>
        /// Close the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarExecutiveMember_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
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
                    this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpSociety, resultArgs.DataSource.Table, projectSystem.AppSchema.ExecutiveMembers.SOCIETYNAMEColumn.ColumnName, projectSystem.AppSchema.ExecutiveMembers.CUSTOMERIDColumn.ColumnName, true, "-- All --");
                    if (ExecutiveId == 0)
                    {
                        glkpSociety.EditValue = glkpSociety.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }
        /// <summary>
        /// Refresh the grid Controls after adding and editing the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchExecutiveMemberDetails();
            gvExecutiveMember.FocusedRowHandle = RowIndex;
        }
        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarExecutiveMember_RefreshClicked(object sender, EventArgs e)
        {
            FetchExecutiveMemberDetails();
        }
        #endregion

        #region Methods
        /// <summary>
        /// fetch the Exeutive Member Details
        /// </summary>
        private void FetchExecutiveMemberDetails()
        {
            try
            {
                using (ExecutiveMemberSystem excutiveMemberSystem = new ExecutiveMemberSystem())
                {
                    DataTable dtSource = new DataTable();
                    excutiveMemberSystem.DateOfAppointment = deDateFrom.DateTime.ToShortDateString();
                    excutiveMemberSystem.DateOfExit = deTo.DateTime.ToShortDateString();
                    int SelectedIndex = cboStatus.SelectedIndex;
                    excutiveMemberSystem.LegalEntityId = this.UtilityMember.NumberSet.ToInteger(SocietyId);
                    resultArgs = excutiveMemberSystem.FetchExecutiveMemberDetails();

                    if (resultArgs.Success)
                    {
                        if (SelectedIndex == 1)
                        {
                            resultArgs.DataSource.Table.DefaultView.RowFilter = "DATE_OF_EXIT IS NOT NULL";
                            dtSource = resultArgs.DataSource.Table.DefaultView.ToTable();
                        }
                        else if (SelectedIndex == 2)
                        {
                            resultArgs.DataSource.Table.DefaultView.RowFilter = "DATE_OF_EXIT IS NULL";
                            dtSource = resultArgs.DataSource.Table.DefaultView.ToTable();
                        }
                        else
                        {
                            dtSource = resultArgs.DataSource.Table;
                        }

                        gcExecutiveMember.DataSource = dtSource;
                        gcExecutiveMember.RefreshDataSource();
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
        /// To delete the Executive Member Details
        /// </summary>
        private void DeleteExecutiveMemberDetails()
        {
            try
            {

                if (gvExecutiveMember.RowCount != 0)
                {
                    if (ExecutiveId != 0)
                    {
                        using (ExecutiveMemberSystem excutiveMemberSystem = new ExecutiveMemberSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = excutiveMemberSystem.DeleteExecuteMember(ExecutiveId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchExecutiveMemberDetails();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }

                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// invoke the member form based on the Id
        /// </summary>
        /// <param name="ExecutiveId"></param>
        private void ShowForm(int executiveId)
        {
            try
            {
                frmEMembers frmmembers = new frmEMembers(executiveId);
                frmmembers.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmmembers.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Get the Executive Member Id Details
        /// </summary>
        private void ShowEditExecutiveForm()
        {
            if (this.isEditable)
            {
                if (gvExecutiveMember.RowCount != 0)
                {
                    if (ExecutiveId != 0)
                    {
                        ShowForm(ExecutiveId);
                    }
                    else
                    {
                        if (!chkShowFilter.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                        }

                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }
        #endregion

        private void frmExecutiveMemberView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmExecutiveMemberView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditExecutiveForm();
        }

        private void pnlExecutivefooter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkShowFilter_CheckedChanged_1(object sender, EventArgs e)
        {
            gvExecutiveMember.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvExecutiveMember, colName);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SocietyId = glkpSociety.EditValue.ToString();
            FetchExecutiveMemberDetails();
        }
    }
}