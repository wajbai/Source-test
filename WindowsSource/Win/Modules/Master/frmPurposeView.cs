/*  Class Name      : frmPurposeView
 *  Purpose         : To view the Purpose Browse Screen
 *  Author          : Chinna
 *  Created on      : 
 */
using System;

using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.XtraBars;
using System.Windows.Forms;

namespace ACPP.Modules.Master
{
    public partial class frmPurposeView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmPurposeView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int purposeId = 0;
        public int PurposeId
        {
            get
            {
                RowIndex = gvPurposeView.FocusedRowHandle;
                purposeId = gvPurposeView.GetFocusedRowCellValue(colContributionHead) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurposeView.GetFocusedRowCellValue(colContributionHead).ToString()) : 0;
                return purposeId;
            }
            set
            {
                purposeId = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPurposeView_Load(object sender, EventArgs e)
        {
           // SetRights();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyUserRights();
            }
        }

        private void frmPurposeView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, false);
            GetPurposeDetails();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Purpose.PrintPurpose);
            this.enumUserRights.Add(Purpose.ViewPurpose);
            this.ApplyUserRights(ucPurposeToolBar, this.enumUserRights, (int)Menus.Purpose);
        }

        /// <summary>
        /// Add the Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPurposeToolBar_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Edit the Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPurposeToolBar_EditClicked(object sender, EventArgs e)
        {
            EditPurposeForm();
        }

        /// <summary>
        /// Edit the Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcPurposeView_DoubleClick(object sender, EventArgs e)
        {
            EditPurposeForm();
        }

        /// <summary>
        /// Delete the Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPurposeToolBar_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                using (PurposeSystem purposeSystem = new PurposeSystem())
                {
                    if (gvPurposeView.RowCount != 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            resultArgs = purposeSystem.DeletePurposeDetails(PurposeId);
                            if (resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Purposes.PURPOSE_DELETE_SUCCESS));
                                GetPurposeDetails();
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
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
        /// Print the Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPurposeToolBar_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcPurposeView, this.GetMessage(MessageCatalog.Master.Purposes.PURPOSE_PRINT_CAPTION), PrintType.DT, gvPurposeView);
        }

        /// <summary>
        /// To enable the AutoFilter for Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPurposeView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvPurposeView, colPurposes);
            }
        }

        /// <summary>
        /// To have Count of Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurposeView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvPurposeView.RowCount.ToString();
        }

        /// <summary>
        /// Close the Purpose Add Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPurposeToolBar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPurposeToolBar_RefreshClicked(object sender, EventArgs e)
        {
            GetPurposeDetails();
        }

        private void frmPurposeView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load the Purpose Details
        /// </summary>
        public void GetPurposeDetails()
        {
            try
            {
                using (PurposeSystem purposeSystem = new PurposeSystem())
                {
                    resultArgs = purposeSystem.FetchPurposeDetails();
                    gcPurposeView.DataSource = resultArgs.DataSource.Table;
                    gcPurposeView.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Show Popup Purpose form based on the Id
        /// </summary>
        /// <param name="purposeId"></param>
        public void ShowForm(int purposeId)
        {
            try
            {
                frmPurposeAdd frmpurposeAdd = new frmPurposeAdd(purposeId);
                frmpurposeAdd.UpdataHeld += new EventHandler(OnUpdateHeld);
                frmpurposeAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Refresh the Purpose Grid after adding and editing the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            GetPurposeDetails();
            gvPurposeView.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// To Identify the Id and Show the form
        /// </summary>
        public void EditPurposeForm()
        {
            try
            {
                if (gvPurposeView.RowCount != 0)
                {
                    ShowForm(PurposeId);
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

        private void SetRights()
        {
            int AccessRights = 0;
            using (MasterRightsSystem masterRightsSystem = new MasterRightsSystem())
            {
                masterRightsSystem.MasterName = this.Text;
                AccessRights = masterRightsSystem.MasterRights();
                if (AccessRights == (int)MasterRights.ReadOnly)
                {
                    ucPurposeToolBar.VisibleDeleteButton = ucPurposeToolBar.VisibleAddButton = ucPurposeToolBar.VisibleEditButton = BarItemVisibility.Never;
                }
            }
        }
        #endregion

    }
}