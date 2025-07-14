using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

namespace ACPP.Modules.UIControls
{
    public partial class UCAssetVoucherShortcuts : DevExpress.XtraEditors.XtraUserControl
    {
        #region Events
        public event EventHandler BankAccountClicked;
        public event EventHandler LocationMappingClicked;
        public event EventHandler ConfigureClicked;
        public event EventHandler DateClicked;
        public event EventHandler NextDateClicked;
        public event EventHandler ProjectClicked;
        public event EventHandler LocationClicked;
        public event EventHandler CustodianClicked;
        public event EventHandler VendorClicked;
        public event EventHandler AssetItemClicked;
        public event EventHandler ManufacturerClicked;
        public event EventHandler DonorClicked;
        public event EventHandler AccountMappingClicked;
        public event EventHandler PurchaseClicked;
        public event EventHandler InkindClicked;
        public event EventHandler SalesClicked;
        public event EventHandler DonationClicked;
        public event EventHandler DisposeClicked;
        public event EventHandler CostCentreClicked;
        public event EventHandler LedgerClicked;
        public event EventHandler LedgerOptionClicked;
        public event EventHandler AssetVoucherViewClicked;
        #endregion

        public UCAssetVoucherShortcuts()
        {
            InitializeComponent();
        }

        #region Properties

        public BarItemVisibility DisableDate
        {
            get { return bbiDate.Visibility; }
            set { bbiDate.Visibility = value; }
        }

        public BarItemVisibility DisableNextDate
        {
            get { return bbiNextDate.Visibility; }
            set { bbiNextDate.Visibility = value; }
        }

        public BarItemVisibility DisableProject
        {
            get { return bbiProject.Visibility; }
            set { bbiProject.Visibility = value; }
        }

        public BarItemVisibility DisableVendor
        {
            get { return bbiVendor.Visibility; }
            set { bbiVendor.Visibility = value; }
        }

        public BarItemVisibility DisableCustodian
        {
            get { return bbiCustodian.Visibility; }
            set { bbiCustodian.Visibility = value; }
        }

        public BarItemVisibility DisableManufacturer
        {
            get { return bbiManufacturer.Visibility; }
            set { bbiManufacturer.Visibility = value; }
        }

        public BarItemVisibility DisableBankAccount
        {
            get { return bbiBankAccount.Visibility; }
            set { bbiBankAccount.Visibility = value; }
        }

        public BarItemVisibility DisableMappLocation
        {
            get { return bbiMappLocation.Visibility; }
            set { bbiMappLocation.Visibility = value; }
        }

        public BarItemVisibility DisableConfigure
        {
            get { return bbiConfigure.Visibility; }
            set { bbiConfigure.Visibility = value; }
        }

        public BarItemVisibility DisableAssetItem
        {
            get { return bbiAssetItem.Visibility; }
            set { bbiAssetItem.Visibility = value; }
        }

        public BarItemVisibility DisableLocation
        {
            get { return bbiLocation.Visibility; }
            set { bbiLocation.Visibility = value; }
        }

        public BarItemVisibility DisableDonor
        {
            get { return bbiDonor.Visibility; }
            set { bbiDonor.Visibility = value; }
        }

        public BarItemVisibility DisableMapping
        {
            get { return bbiMapping.Visibility; }
            set { bbiMapping.Visibility = value; }
        }

        public BarItemVisibility DisablePUrchase
        {
            get { return bbiPurchase.Visibility; }
            set { bbiPurchase.Visibility = value; }
        }

        public BarItemVisibility DisableInkind
        {
            get { return bbiInkind.Visibility; }
            set { bbiInkind.Visibility = value; }
        }

        public BarItemVisibility DisableSales
        {
            get { return bbiSales.Visibility; }
            set { bbiSales.Visibility = value; }
        }

        public BarItemVisibility DisableDonation
        {
            get { return bbiDonation.Visibility; }
            set { bbiDonation.Visibility = value; }
        }

        public BarItemVisibility DisableDispose
        {
            get { return bbiDispose.Visibility; }
            set { bbiDispose.Visibility = value; }
        }

        public BarItemVisibility DisableCostCentre
        {
            get { return bbiCostcentre.Visibility; }
            set { bbiCostcentre.Visibility = value; }
        }

        public BarItemVisibility DisableLedger
        {
            get { return bbiLedger.Visibility; }
            set { bbiLedger.Visibility = value; }
        }

        public BarItemVisibility DisableLedgerOption
        {
            get { return bbiLedgerOption.Visibility; }
            set { bbiLedgerOption.Visibility = value; }
        }
        public BarItemVisibility DisableAssetVoucherView
        {
            get { return bbiAssetVoucherView.Visibility; }
            set { bbiAssetVoucherView.Visibility = value; }
        }
        #endregion

        private void bbiDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DateClicked != null)
            {
                DateClicked(this, e);
            }
        }

        private void bbiProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ProjectClicked != null)
            {
                ProjectClicked(this, e);
            }
        }

        private void bbiBankAccount_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (BankAccountClicked != null)
            {
                BankAccountClicked(this, e);
            }
        }

        private void bbiConfigure_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ConfigureClicked != null)
            {
                ConfigureClicked(this, e);
            }
        }

        private void bbiMapping_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (AccountMappingClicked != null)
            {
                AccountMappingClicked(this, e);
            }
        }

        private void bbiCustodian_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CustodianClicked != null)
            {
                CustodianClicked(this, e);
            }
        }

        private void bbiLocation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (LocationClicked != null)
            {
                LocationClicked(this, e);
            }
        }

        private void bbiManufacturer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ManufacturerClicked != null)
            {
                ManufacturerClicked(this, e);
            }
        }

        private void bbiAssetItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (AssetItemClicked != null)
            {
                AssetItemClicked(this, e);
            }
        }

        private void bbiVendor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (VendorClicked != null)
            {
                VendorClicked(this, e);
            }
        }

        private void bbiDonor_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DonorClicked != null)
            {
                DonorClicked(this, e);
            }
        }

        private void bbiNextDate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (NextDateClicked != null)
            {
                NextDateClicked(this, e);
            }
        }

        private void bbiLocationMapping_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (LocationMappingClicked != null)
            {
                LocationMappingClicked(this, e);
            }
        }

        private void bbiPurchase_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PurchaseClicked != null)
            {
                PurchaseClicked(this, e);
            }
        }

        private void bbiInkind_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (InkindClicked != null)
            {
                InkindClicked(this, e);
            }
        }

        private void bbiSales_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SalesClicked != null)
            {
                SalesClicked(this, e);
            }
        }

        private void bbiDispose_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DisposeClicked != null)
            {
                DisposeClicked(this, e);
            }
        }

        private void bbiDonation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DonationClicked != null)
            {
                DonationClicked(this, e);
            }
        }

        private void bbiLedger_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (LedgerClicked != null)
            {
                LedgerClicked(this, e);
            }
        }

        private void bbiCostcenter_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CostCentreClicked != null)
            {
                CostCentreClicked(this, e);
            }
        }

        private void bbiLedgerOption_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (LedgerOptionClicked != null)
            {
                LedgerOptionClicked(this, e);
            }
        }

        private void bbtnVoucherView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (AssetVoucherViewClicked != null)
            {
                AssetVoucherViewClicked(this, e);
            }
        }
    }
}
