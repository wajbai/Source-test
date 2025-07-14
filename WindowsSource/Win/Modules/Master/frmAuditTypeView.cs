using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;


namespace ACPP.Modules.Master
{
    public partial class frmAuditTypeView : frmFinanceBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Consttructors
        public frmAuditTypeView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        private int AuditId = 0;
        private int AuditID
        {
            get
            {
                RowIndex = gvAudit.FocusedRowHandle;
                AuditId = gvAudit.GetFocusedRowCellValue(colAuditTypeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAudit.GetFocusedRowCellValue(colAuditTypeId).ToString()) : 0;
                return AuditId;
            }
            set
            {
                AuditId = value;
            }
        }
        #endregion

        #region Events
        private void frmAuditTypeView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmAuditTypeView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchAuditDetails();
        }

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowAuditForm((int)AddNewRow.NewRow);
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            DeleteAuditDetails();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            ShowAuditEditForm();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAudit, this.GetMessage(MessageCatalog.Master.Audit.AUDITTYPE_PRINT_CAPTION), PrintType.DT, gvAudit, true);
        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchAuditDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Fetch the Audit Details from DB
        /// </summary>

        private void FetchAuditDetails()
        {
            try
            {
                using (AuditSystem auditSystem = new AuditSystem())
                {
                    resultArgs = auditSystem.FetchAuditTypeDetails();
                    gcAudit.DataSource = resultArgs.DataSource.Table;
                    gcAudit.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ShowAuditForm(int AuditId)
        {
            try
            {
                frmAuditTypeAdd frmAudit_Add = new frmAuditTypeAdd(AuditId);
                frmAudit_Add.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAudit_Add.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }


        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchAuditDetails();
            gvAudit.FocusedRowHandle = RowIndex;
        }

        private void DeleteAuditDetails()
        {
            try
            {

                if (gvAudit.RowCount != 0)
                {
                    if (AuditID != 0)
                    {
                        using (AuditSystem auditSystem = new AuditSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = auditSystem.DeleteAuditDetails(AuditID);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchAuditDetails();
                                }
                                else
                                {
                                    this.ShowMessageBoxError("" + resultArgs.Message);

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

        public void ShowAuditEditForm()
        {
            if (gvAudit.RowCount != 0)
            {
                if (AuditID != 0)
                {
                    ShowAuditForm(AuditID);
                }

                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                }

            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

         private void ApplyUserRights()
        {
            this.enumUserRights.Add(AuditType.CreateAuditType);
            this.enumUserRights.Add(AuditType.EditAuditType);
            this.enumUserRights.Add(AuditType.DeleteAuditType);
            this.enumUserRights.Add(AuditType.PrintAuditType);
            this.enumUserRights.Add(AuditType.ViewAuditType);
            this.ApplyUserRights(ucToolBar1, enumUserRights, (int)Menus.AuditType);
        }

        #endregion

        private void gvAudit_DoubleClick(object sender, EventArgs e)
        {
            ShowAuditEditForm();
        }

        private void gvAudit_RowCountChanged(object sender, EventArgs e)
        {
            lblAuditRowCount.Text = gvAudit.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAudit.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAudit, colAuditType);
            }
        }

        private void frmAuditTypeView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmAuditTypeView_EnterClicked(object sender, EventArgs e)
        {
            ShowAuditEditForm();
        }

    }
}