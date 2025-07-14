using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules.Master
{
    public partial class frmAuditorView : frmFinanceBase
    {
        #region Variable Decelartion
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmAuditorView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int auditorId = 0;
        private int AuditorId
        {
            get
            {
                RowIndex = gvAuditorDetails.FocusedRowHandle;
                auditorId = gvAuditorDetails.GetFocusedRowCellValue(colDonAudId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAuditorDetails.GetFocusedRowCellValue(colDonAudId).ToString()) : 0;
                return auditorId;
            }
            set
            {
                auditorId = value;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Load the grid with auditor information while loading the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmAuditorView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmAuditorView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchAuditorDetails();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Auditor.CreateAuditor);
            this.enumUserRights.Add(Auditor.EditAuditor);
            this.enumUserRights.Add(Auditor.DeleteAuditor);
            this.enumUserRights.Add(Auditor.PrintAuditor);
            this.enumUserRights.Add(Auditor.ViewAuditor);
            this.ApplyUserRights(ucAuditorView, enumUserRights, (int)Menus.Auditor);
        }

        /// <summary>
        /// Invoke Auditor form to add the auditor details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucAuditorView_AddClicked(object sender, EventArgs e)
        {
            RowIndex = 0;
            ShowAuditorForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Invoke Auditor form to edit the auditor information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucAuditorView_EditClicked(object sender, EventArgs e)
        {
            ShowAuditorEditForm();
        }

        /// <summary>
        /// To delete the selected auditor information from the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucAuditorView_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                // if (AuditorId != 0)
                // {
                //   using (DonorAuditorSystem donorSystem = new DonorAuditorSystem())
                //   {
                if (gvAuditorDetails.RowCount != 0)
                {
                    if (AuditorId != 0)
                    {
                        using (DonorAuditorSystem donorSystem = new DonorAuditorSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = donorSystem.DeleteDonorAuditorDetails(AuditorId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchAuditorDetails();
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
        /// To print the list of auditor details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucAuditorView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAuditorDetails, this.GetMessage(MessageCatalog.Master.Auditor.AUDITOR_PRINT_CAPTION), PrintType.DT, gvAuditorDetails, true);
        }

        /// <summary>
        /// Close the currently opened form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucAuditorView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Invoke auditor form to edit the auditor details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvAuditorDetails_DoubleClick(object sender, EventArgs e)
        {
            ShowAuditorEditForm();
        }

        /// <summary>
        /// To show the number of records available in the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvAuditorDetails_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvAuditorDetails.RowCount.ToString();
        }

        /// <summary>
        /// To enable auto filter row in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAuditorDetails.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAuditorDetails, colName);
            }
        }

        /// <summary>
        /// To refresh the grid after adding auditor information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchAuditorDetails();
            gvAuditorDetails.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// To Refresh the Auditor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucAuditorView_RefreshClicked(object sender, EventArgs e)
        {
            FetchAuditorDetails();
        }

        private void frmAuditorView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmAuditorView_EnterClicked(object sender, EventArgs e)
        {
            ShowAuditorEditForm();
        }

        #endregion

        #region Methods

        /// <summary>
        /// To load Auditor details in the grid while loading the form
        /// </summary>

        private void FetchAuditorDetails()
        {
            try
            {
                using (DonorAuditorSystem auditorSystem = new DonorAuditorSystem())
                {
                    resultArgs = auditorSystem.FetchAuditorDetails();
                    gcAuditorDetails.DataSource = resultArgs.DataSource.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        /// <summary>
        /// This method is to show the Edit form when Edit/Double clicked.
        /// </summary>

        private void ShowAuditorEditForm()
        {
            if (this.isEditable)
            {
                if (gvAuditorDetails.RowCount != 0)
                {
                    if (AuditorId != 0)
                    {
                        {
                            ShowAuditorForm(auditorId);
                        }
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


        /// <summary>
        /// To Show the Auditor for to add and edit auditor details
        /// </summary>

        private void ShowAuditorForm(int donaudId)
        {
            try
            {
                frmAddAuditor frmAuditor = new frmAddAuditor(donaudId);
                frmAuditor.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAuditor.ShowDialog();
                this.auditorId = 0;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

    }
}