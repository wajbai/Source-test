using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel.Master;
using Bosco.Utility;

namespace ACPP.Modules.Master
{
    public partial class frmAuditLockTypeView : frmFinanceBase
    {
        #region Variable
        ResultArgs resultArgs = null;
        int RowIndex = 0;
        #endregion

        #region  Constructor
        public frmAuditLockTypeView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int LockTypeId = 0;
        private int LockId
        {
            get
            {
                RowIndex = gvAuditTypeView.FocusedRowHandle;
                return gvAuditTypeView.GetFocusedRowCellValue(colLockTypeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAuditTypeView.GetFocusedRowCellValue(colLockTypeId).ToString()) : 0;
            }
        }
        #endregion

        #region Events
        private void frmAuditLockTypeView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmAuditLockTypeView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchAuditType();
            SetTitle();
        }

        private void ucToolBarAuditType_AddClicked(object sender, EventArgs e)
        {
            ShowAuditLockTypeFrom((int)AddNewRow.NewRow);
        }

        private void ucToolBarAuditType_EditClicked(object sender, EventArgs e)
        {
            if (LockId > 0)
            {
                ShowAuditLockTypeFrom(LockId);
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void gcAuditTypeView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void ucToolBarAuditType_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (LockId > 0)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        using (AuditLockTransSystem AuditLockSystem = new AuditLockTransSystem())
                        {
                            AuditLockSystem.LockTypeId = LockId;
                            resultArgs = AuditLockSystem.DeleteAduitType();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                FetchAuditType();
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
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucToolBarAuditType_PrintClicked(object sender, EventArgs e)
        {
            this.PrintGridViewDetails(gcAuditTypeView, this.GetMessage(MessageCatalog.Master.AuditLockType.AUDIT_LOCK_TYPE), PrintType.DT, gvAuditTypeView);
        }

        private void ucToolBarAuditType_RefreshClicked(object sender, EventArgs e)
        {
            FetchAuditType();
        }

        private void ucToolBarAuditType_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchAuditType();
            gvAuditTypeView.FocusedRowHandle = RowIndex;
        }

        private void gvAuditTypeView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = "# " + gvAuditTypeView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAuditTypeView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAuditTypeView, colLockType);
            }
        }
        #endregion

        #region Methods

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Master.AuditLockType.AUDIT_LOCK_TYPE_VIEW_CAPTION);
        }

        private void FetchAuditType()
        {
            try
            {
                using (AuditLockTransSystem AuditTranSystem = new AuditLockTransSystem())
                {
                    resultArgs = AuditTranSystem.FetchAllAuditType();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcAuditTypeView.DataSource = resultArgs.DataSource.Table;
                        gcAuditTypeView.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ShowAuditLockTypeFrom(int LockTypeId)
        {
            try
            {
                frmAuditLockAdd frmAuditAdd = new frmAuditLockAdd(LockTypeId);
                frmAuditAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAuditAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(AuditLockTypes.CreateLockType);
            this.enumUserRights.Add(AuditLockTypes.EditLockType);
            this.enumUserRights.Add(AuditLockTypes.DeleteLockType);
            this.enumUserRights.Add(AuditLockTypes.PrintLockType);
            this.enumUserRights.Add(AuditLockTypes.ViewLockType);
            this.ApplyUserRights(ucToolBarAuditType, enumUserRights, (int)Menus.AuditLockTypes);
        }

        #endregion
    }
}