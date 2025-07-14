using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel.Master;
using Bosco.Utility;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Master
{
    public partial class frmAuditingInfoView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmAuditingInfoView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int AuditId = 0;
        public int AuditInfoId
        {
            get
            {
                RowIndex = gvAuditingInfoView.FocusedRowHandle;
                AuditId = gvAuditingInfoView.GetFocusedRowCellValue(colAuditInfoId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAuditingInfoView.GetFocusedRowCellValue(colAuditInfoId).ToString()) : 0;
                return AuditId;
            }
            set
            {
                AuditId = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the Auditor Info Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAuditingInfoView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();

        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(AuditInfo.CreateAuditInfo);
            this.enumUserRights.Add(AuditInfo.EditAuditInfo);
            this.enumUserRights.Add(AuditInfo.DeleteAuditInfo);
            this.enumUserRights.Add(AuditInfo.PrintAuditInfo);
            this.enumUserRights.Add(AuditInfo.ViewAuditInfo);
            this.ApplyUserRights(ucToolbarAuditingInfo, enumUserRights, (int)Menus.AuditInfo);
        }

        /// <summary>
        /// Add the Auditor Info Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolbarAuditingInfo_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Edit the Auditor Info Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolbarAuditingInfo_EditClicked(object sender, EventArgs e)
        {
            ShowEditAuditInfoForm();
        }
        private void gvAuditingInfoView_DoubleClick(object sender, EventArgs e)
        {
            ShowEditAuditInfoForm();
        }

        /// <summary>
        /// Delete Auditor Info Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolbarAuditingInfo_DeleteClicked(object sender, EventArgs e)
        {
            DeleteAuditorInfoDetails();
        }

        /// <summary>
        /// To Measure the Gridview Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAuditingInfoView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvAuditingInfoView.RowCount.ToString();
        }

        /// <summary>
        /// To Print the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolbarAuditingInfo_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAuditingInfoView, this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_INFO_PRINT_CAPTION), PrintType.DT, gvAuditingInfoView, true);
        }
        /// <summary>
        /// To enable AutoFilter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged_1(object sender, EventArgs e)
        {
            gvAuditingInfoView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAuditingInfoView, colProject);
            }
        }

        /// <summary>
        /// To Refresh the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolbarAuditingInfo_RefreshClicked(object sender, EventArgs e)
        {
            GetAuditorInfoDetails();
        }
        /// <summary>
        /// To Close the Auditor Info form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolbarAuditingInfo_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// refresh the Grid after editing or adding the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            GetAuditorInfoDetails();
            RowIndex = gvAuditingInfoView.FocusedRowHandle;
        }

        private void frmAuditingInfoView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        //Added by Carmel Raj M
        private void frmAuditingInfoView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            GetAuditorInfoDetails();
        }
        #endregion

        #region Methods
        /// <summary>
        /// To get list of Auditor Details
        /// </summary>
        public void GetAuditorInfoDetails()
        {
            try
            {
                using (AuditInfoSystem auditInfoSystem = new AuditInfoSystem())
                {
                    resultArgs = auditInfoSystem.FetchAuditorInfoDetails();
                    gcAuditingInfoView.DataSource = resultArgs.DataSource.Table;
                    gcAuditingInfoView.RefreshDataSource();

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Show Module popup based on the Id
        /// </summary>
        /// <param name="AuditorId"></param>
        private void ShowForm(int AuditorId)
        {
            try
            {
                frmAuditingInfo frmAudit = new frmAuditingInfo(AuditorId);
                frmAudit.OnUpdate += new EventHandler(OnUpdateHeld);
                frmAudit.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To get edit details of Auditor Info 
        /// </summary>
        public void ShowEditAuditInfoForm()
        {
            if (this.isEditable)
            {
                if (gvAuditingInfoView.RowCount != 0)
                {
                    if (AuditInfoId != 0)
                    {
                        ShowForm(AuditInfoId);
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
        /// Delete the Auditor info Details
        /// </summary>
        private void DeleteAuditorInfoDetails()
        {
            try
            {

                if (gvAuditingInfoView.RowCount != 0)
                {
                    if (AuditInfoId != 0)
                    {
                        using (AuditInfoSystem auditInfoSystem = new AuditInfoSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = auditInfoSystem.DeleteAuditorInfoDetials(AuditInfoId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    GetAuditorInfoDetails();
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
        #endregion

        private void frmAuditingInfoView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditAuditInfoForm();
        }




    }
}