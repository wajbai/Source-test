using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model;
using ACPP.Modules.Data_Utility;

namespace ACPP.Modules.Inventory
{
    public partial class frmVendorInfoView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int vendor_Id = 0;
        private int RowIndex = 0;
        public VendorManufacture vendorManufacture { get; set; }
        private AssetStockProduct.IVendorManufacture IVendor = null;
        FinanceModule FrmModule;
        #endregion

        #region Properties
        public int VendorId
        {
            get
            {
                RowIndex = gvVendorInfo.FocusedRowHandle;
                vendor_Id = gvVendorInfo.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvVendorInfo.GetFocusedRowCellValue(colId).ToString()) : 0;
                return vendor_Id;
            }
            set
            {
                vendor_Id = value;
            }
        }
        public string SheetName { get; set; }
        public MasterImport FrmName { get; set; }
        #endregion

        #region Constructor

        public frmVendorInfoView()
        {
        }

        public frmVendorInfoView(string SheetName, VendorManufacture module, MasterImport Vendor, FinanceModule moduledefinition)
        {
            this.SheetName = SheetName + "$";
            this.FrmName = Vendor;
            vendorManufacture = module;
            FrmModule = moduledefinition;
            IVendor = AssetStockFactory.GetUnitVendorInstance(module);
            InitializeComponent();

        }
        #endregion

        #region Events
        /// <summary>
        /// Load the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVendorInfoView_Load(object sender, EventArgs e)
        {
            //SetVisibileShortCuts(false, true);
            FetchVendorDetails();
            SetTitle();
            if (FrmModule.ToString().Equals("Asset"))
                ApplyUserRights();
            else
                ApplyStockUserRights();

            ucVendorInfo.VisbleInsertVoucher  =  ucVendorInfo.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                colGstNo.Visible = colPanNo.Visible = false;
            }

        }

        /// <summary>
        /// Add the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucVendorInfo_AddClicked(object sender, EventArgs e)
        {
            ShowVendor((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Edit the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucVendorInfo_EditClicked(object sender, EventArgs e)
        {
            ShowEditVendor();
        }

        /// <summary>
        /// Double the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvVendorInfo_DoubleClick(object sender, EventArgs e)
        {
            ShowEditVendor();
        }

        /// <summary>
        /// Delete the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucVendorInfo_DeleteClicked(object sender, EventArgs e)
        {
            DeleteVendor();
        }

        /// <summary>
        /// refresh the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucVendorInfo_RefreshClicked(object sender, EventArgs e)
        {
            FetchVendorDetails();
        }

        /// <summary>
        /// Print the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucVendorInfo_PrintClicked(object sender, EventArgs e)
        {
            if (vendorManufacture == VendorManufacture.Vendor)
            {
                //PrintGridViewDetails(gcVendorInfo, "Vendor Details", PrintType.DT, gvVendorInfo, true);
                PrintGridViewDetails(gcVendorInfo, this.GetMessage(MessageCatalog.Asset.VendorInfo.VENDOR_PRINT_CAPTION), PrintType.DT, gvVendorInfo, true);
            }
            if (vendorManufacture == VendorManufacture.Manufacture)
            {
                //PrintGridViewDetails(gcVendorInfo, "Manufacture Details", PrintType.DT, gvVendorInfo, true);
                PrintGridViewDetails(gcVendorInfo, this.GetMessage(MessageCatalog.Asset.VendorInfo.MANUFACTURER_PRINT_CAPTION), PrintType.DT, gvVendorInfo, true);
            }
        }

        /// <summary>
        /// Row count the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvVendorInfo_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvVendorInfo.RowCount.ToString();
        }

        /// <summary>
        /// Close the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucVendorInfo_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchVendorDetails();
            gvVendorInfo.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// Update the refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvVendorInfo.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvVendorInfo, colName);
            }
        }
        private void frmVendorInfoView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// show the form
        /// </summary>
        /// <param name="VendorID"></param>
        private void ShowVendor(int VendorID)
        {
            try
            {
                frmVendorInfoAdd VendorAdd = new frmVendorInfoAdd(VendorID, vendorManufacture);
                VendorAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                VendorAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        /// <summary>
        /// Fetch the Details
        /// </summary>
        public void FetchVendorDetails()
        {
            resultArgs = IVendor.FetchDetails();
            if (resultArgs.Success && resultArgs != null)
            {
                gcVendorInfo.DataSource = resultArgs.DataSource.Table;
                gcVendorInfo.RefreshDataSource();
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Vendor.CreateVendor);
            this.enumUserRights.Add(Vendor.EditVendor);
            this.enumUserRights.Add(Vendor.DeleteVendor);
            this.enumUserRights.Add(Vendor.ViewVendor);
            this.ApplyUserRights(ucVendorInfo, enumUserRights, (int)Menus.Vendor);
        }

        private void ApplyStockUserRights()
        {
            this.enumUserRights.Add(StockVendor.CreateStockVendor);
            this.enumUserRights.Add(StockVendor.EditStockVendor);
            this.enumUserRights.Add(StockVendor.DeleteStockVendor);
            this.enumUserRights.Add(StockVendor.ViewStockVendor);
            this.ApplyUserRights(ucVendorInfo, enumUserRights, (int)Menus.StockVendor);
        }


        /// <summary>
        /// Edit the Details
        /// </summary>
        private void ShowEditVendor()
        {
            if (this.isEditable)
            {
                if (gvVendorInfo.RowCount != 0)
                {
                    if (VendorId != 0)
                    {
                        ShowVendor(VendorId);
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
        /// Delete the vendor
        /// </summary>
        private void DeleteVendor()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvVendorInfo.RowCount != 0)
                {
                    if (VendorId != 0)
                    {

                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            IVendor.Id = VendorId;
                            resultArgs = IVendor.DeleteDetails();
                            if (resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                FetchVendorDetails();
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
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        public void SetTitle()
        {
            if (vendorManufacture.Equals(VendorManufacture.Vendor))
            {
                this.Text = this.GetMessage(MessageCatalog.Asset.VendorInfo.VENDOR_VIEW_CAPTION);
            }
            else
            {
                this.Text = this.GetMessage(MessageCatalog.Asset.Manufacture.MANUFACTURE_VIEW_CAPTION);
            }
        }
        #endregion

        private void frmVendorInfoView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditVendor();
        }

        private void ucVendorInfo_DownloadExcel(object sender, EventArgs e)
        {
            if (MasterImport.Vendor.Equals(FrmName))
            {
                using (frmExcelSupport excelSupport = new frmExcelSupport("Vendor", FrmName))
                {
                    excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                    excelSupport.ShowDialog();
                }
            }
            else
            {
                using (frmExcelSupport excelSupport = new frmExcelSupport("Manufacturer", FrmName))
                {
                    excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                    excelSupport.ShowDialog();
                }
            }
        }

        private void frmVendorInfoView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            FetchVendorDetails();
            SetTitle();
        }
    }
}