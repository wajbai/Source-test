using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.ASSET;
using Bosco.Model.Asset;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmMaintenanceServiceView : frmBase
    {
        #region Vaiable Decleration
        private int RowIndex = 0;
        ResultArgs resultArgs = null;
        #endregion
       
        #region Properties
        private int serviceId = 0;
        private int ServiceId
        {
            get
            {
                RowIndex = gvMaintenanceorService.FocusedRowHandle;
                serviceId = gvMaintenanceorService.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvMaintenanceorService.GetFocusedRowCellValue(colId).ToString()) : 0;
                return serviceId;
            }
            set
            {
                serviceId = value;
            }
        }
        #endregion

        #region Constructor
        public frmMaintenanceServiceView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        private void frmMaintananceServiceView_Load(object sender, EventArgs e)
        {
            LoadMaintanceService();
        }

        private void ucMaintananceOrServive_AddClicked(object sender, EventArgs e)
        {
            ShowServiceForm((int)AddNewRow.NewRow);
        }

        private void ucMaintananceOrServive_EditClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ucMaintananceOrServive_DeleteClicked(object sender, EventArgs e)
        {
            DeleteMaintanceService();
        }

        private void ucMaintananceOrServive_PrintClicked(object sender, EventArgs e)
        {
            this.PrintGridViewDetails(gcMaintenanceorServiceView, "Maintance / Service", PrintType.DT, gvMaintenanceorService);
        }

        private void ucMaintananceOrServive_RefreshClicked(object sender, EventArgs e)
        {
            LoadMaintanceService();
        }

        private void ucMaintananceOrServive_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvMaintenanceorService_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvMaintenanceorService.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMaintenanceorService.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvMaintenanceorService, colName);
            }
        }

        private void frmMaintenanceServiceView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadMaintanceService();
            gvMaintenanceorService.FocusedRowHandle = RowIndex;
        }

        private void frmMaintenanceServiceView_EnterClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void gvMaintenanceorService_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        #endregion

        #region Methods
        private void LoadMaintanceService()
        {
            try
            {
                using (AssetServiceSystem serviceSystem = new AssetServiceSystem())
                {
                    resultArgs = serviceSystem.FetchMaintanceService();
                    if (resultArgs != null  && resultArgs.Success)
                    {
                        gcMaintenanceorServiceView.DataSource = resultArgs.DataSource.Table;
                        gcMaintenanceorServiceView.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        private void DeleteMaintanceService()
        {
            if (gvMaintenanceorService.RowCount > 0)
            {
                if (ServiceId > 0)
                {
                    using (AssetServiceSystem serviceSystem = new AssetServiceSystem())
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            serviceSystem.ServiceId = this.ServiceId;
                            resultArgs = serviceSystem.DeleteService();
                            if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected>0)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                LoadMaintanceService();
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

        private void ShowForm()
        {
            if (gvMaintenanceorService.RowCount > 0)
            {
                if (ServiceId > 0)
                {
                    using (AssetServiceSystem serviceSystem = new AssetServiceSystem())
                    {
                        ShowServiceForm(ServiceId);
                    }
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

        private void ShowServiceForm(int ServiceId)
        {
            try
            {
                frmMaintenanceOrService frmServiceAdd = new frmMaintenanceOrService(ServiceId);
                frmServiceAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmServiceAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        #endregion

        

        
    }
}
