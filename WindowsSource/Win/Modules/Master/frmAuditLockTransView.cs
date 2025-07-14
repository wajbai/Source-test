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
    public partial class frmAuditLockTransView : frmFinanceBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        int RowIndex = 0;
        #endregion

        #region Constructors
        public frmAuditLockTransView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int LockTransId = 0;
        private int LockTransTypeId
        {
            get
            {
                RowIndex = gvAuditLockTransView.FocusedRowHandle;
                return gvAuditLockTransView.GetFocusedRowCellValue(colLockTransId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAuditLockTransView.GetFocusedRowCellValue(colLockTransId).ToString()) : 0;
            }
        }

        private string LockByPortal
        {
            get
            {
                RowIndex = gvAuditLockTransView.FocusedRowHandle;
                return gvAuditLockTransView.GetFocusedRowCellValue(colLockByPortal) != null ? gvAuditLockTransView.GetFocusedRowCellValue(colLockByPortal).ToString() : string.Empty;
            }
        }
        private bool isValidPassword = false;
        #endregion

        #region Events

        private void frmAuditLockTransView_Load(object sender, EventArgs e)
        {
           // FetchAuditTransType();
            ApplyUserRights();
        }

        private void ucAuditLockTransView_AddClicked(object sender, EventArgs e)
        {
            ShowAuditLockTransFrom((int)AddNewRow.NewRow);
        }

        private void ucAuditLockTransView_EditClicked(object sender, EventArgs e)
        {
            if (LockTransTypeId > 0)
            {
                if (ShowValidateForm(LockTransTypeId, AuditLockType.Edit))
                {
                    ShowAuditLockTransFrom(LockTransTypeId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void gcAuditLockTransView_DoubleClick(object sender, EventArgs e)
        {
            if (ShowValidateForm(LockTransTypeId, AuditLockType.Edit))
            {
                ShowAuditLockTransFrom(LockTransTypeId);
            }
            //else
            //{
            //    this.ShowMessageBox("Cannot edit.The given password is invalid");
            //}
        }
        private void ucAuditLockTransView_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (LockTransTypeId > 0)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        if (ShowValidateForm(LockTransTypeId, AuditLockType.Delete))
                        {
                            using (AuditLockTransSystem AuditLockSystem = new AuditLockTransSystem())
                            {
                                AuditLockSystem.LockTransId = LockTransTypeId;
                                resultArgs = AuditLockSystem.DeleteAduitTrans();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchAuditTransType();
                                }
                            }
                        }
                        //else
                        //{
                        //    this.ShowMessageBox("Cannot delete.The given password is invalid");
                        //}
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

        private void ucAuditLockTransView_PrintClicked(object sender, EventArgs e)
        {
            this.PrintGridViewDetails(gcAuditLockTransView, this.GetMessage(MessageCatalog.Master.AuditLockType.LOCK_TRANS_TYPE), PrintType.DT, gvAuditLockTransView);
        }

        private void ucAuditLockTransView_RefreshClicked(object sender, EventArgs e)
        {
            FetchAuditTransType();
        }

        private void ucAuditLockTransView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchAuditTransType();
            gvAuditLockTransView.FocusedRowHandle = RowIndex;
        }

        private void gvAuditLockTransView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = "# " + gvAuditLockTransView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAuditLockTransView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAuditLockTransView, colProject);
            }
        }

        private void frmAuditLockTransView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchAuditTransType();
        }

        private void rbtnResetPassword_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
                {
                    // int LockTransId = gvAuditLockTransView.GetFocusedRowCellValue(colLockTransId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAuditLockTransView.GetFocusedRowCellValue(colLockTransId).ToString()) : 0;
                    if (LockTransTypeId > 0)
                    {
                        if (LockByPortal == YesNo.No.ToString())
                        {
                            frmResetAuditLock objResetAuditLock = new frmResetAuditLock(LockTransTypeId, true);
                            objResetAuditLock.ShowDialog();
                        }
                        else
                        {
                            MessageRender.ShowMessage("This lock is created by Portal, it can't be Edited/Deleted");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        #endregion

        #region Methods

        private void FetchAuditTransType()
        {
            try
            {
                using (AuditLockTransSystem AuditTranSystem = new AuditLockTransSystem())
                {
                    resultArgs = AuditTranSystem.FetchAllAuditTrans();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcAuditLockTransView.DataSource = resultArgs.DataSource.Table;
                        gcAuditLockTransView.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ShowAuditLockTransFrom(int LockTransId)
        {
            try
            {
                frmAuditLockTransAdd frmAuditTransAdd = new frmAuditLockTransAdd(LockTransId);
                frmAuditTransAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAuditTransAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private bool ShowValidateForm(int LockTransId, AuditLockType auditType)
        {
            bool isSucess = false;
            try
            {
                if (LockByPortal == YesNo.No.ToString())
                {
                    frmResetAuditLock frmReset = new frmResetAuditLock(LockTransId);
                    frmReset.ShowDialog();
                    isSucess = frmReset.isValidPassword;
                }
                else
                {
                    MessageRender.ShowMessage("This lock is created by Portal, it can't be Edited/Deleted");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return isSucess;
        }
        #endregion

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(AuditLockTrans.CreateLockTrans);
            this.enumUserRights.Add(AuditLockTrans.EditLockTrans);
            this.enumUserRights.Add(AuditLockTrans.DeleteLockTrans);
            this.enumUserRights.Add(AuditLockTrans.PrintLockTrans);
            this.enumUserRights.Add(AuditLockTrans.ViewLockTrans);
            this.ApplyUserRights(ucAuditLockTransView, enumUserRights, (int)Menus.AuditLockTrans);
        }

        private void frmAuditLockTransView_EnterClicked(object sender, EventArgs e)
        {
            if (LockTransTypeId > 0)
            {
                if (ShowValidateForm(LockTransTypeId, AuditLockType.Edit))
                {
                    ShowAuditLockTransFrom(LockTransTypeId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }
    }
}