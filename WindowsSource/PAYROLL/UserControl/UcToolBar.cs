using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DevExpress.XtraLayout.Utils;
using DevExpress.XtraBars;

namespace PAYROLL.UserControl
{
    public partial class UcToolBar : DevExpress.XtraEditors.XtraUserControl
    {
        #region Variable Declaration
        public string MasterMode = string.Empty;
        public event EventHandler AddClicked;
        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler PrintClicked;
        public event EventHandler CloseClicked;
        public event EventHandler RefreshClicked;
        public event EventHandler ImportClicked;
        #endregion

        #region Properties
        public bool DisableAddButton
        {
            get { return btiAdd.Enabled; }
            set { btiAdd.Enabled = value; }
        }

        public bool DisableEditButton
        {
            get { return btiEdit.Enabled; }
            set { btiEdit.Enabled = value; }
        }

        public bool DisableDeleteButton
        {
            get { return bbiDelete.Enabled; }
            set { bbiDelete.Enabled = value; }
        }

        public bool DisableCloseButton
        {
            get { return btnClose.Enabled; }
            set { btnClose.Enabled = value; }
        }
        public bool DisableRefreshButton
        {
            get { return bbiRefresh.Enabled; }
            set { bbiRefresh.Enabled = value; }
        }
        public bool DisablePrintButton
        {
            get { return btiPrint.Enabled; }
            set { btiPrint.Enabled = value; }
        }
        public bool DisableImportButton
        {
            get { return bbiImport.Enabled; }
            set { bbiImport.Enabled = value; }
        }
        public string ChangeAddCaption
        {
            get { return btiAdd.Caption; }
            set { btiAdd.Caption = value; }
        }
        public BarItemVisibility VisibleAddButton
        {
            get { return btiAdd.Visibility; }
            set { btiAdd.Visibility = value; }
        }

        public BarItemVisibility VisibleEditButton
        {
            get { return btiEdit.Visibility; }
            set { btiEdit.Visibility = value; }
        }

        public BarItemVisibility VisibleDeleteButton
        {
            get { return bbiDelete.Visibility; }
            set { bbiDelete.Visibility = value; }
        }

        public BarItemVisibility VisibleRefresh
        {
            get { return bbiRefresh.Visibility; }
            set { bbiRefresh.Visibility = value; }
        }

        public BarItemVisibility VisiblePrintButton
        {
            get { return btiPrint.Visibility; }
            set { btiPrint.Visibility = value; }
        }
      
        public BarItemVisibility VisibleClose
        {
            get { return btnClose.Visibility; }
            set { btnClose.Visibility = value; }
        }
        public BarItemVisibility VisibleImport
        {
            get { return bbiImport.Visibility; }
            set { bbiImport.Visibility = value; }
        }

        #endregion

        #region Constructor
        public UcToolBar()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void btiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (AddClicked != null)
            {
                AddClicked(this, e);
            }
        }

        private void btiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (EditClicked != null)
            {
                EditClicked(this, e);
            }
        }

        private void btiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DeleteClicked != null)
            {
                DeleteClicked(this, e);
            }
        }

        private void btiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PrintClicked != null)
            {
                PrintClicked(this, e);
            }
        }

        private void btiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (RefreshClicked != null)
            {
                RefreshClicked(this, e);
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CloseClicked != null)
            {
                CloseClicked(this, e);
            }
        }

        private void bbiImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ImportClicked != null)
            {
                ImportClicked(this, e);
            }
        }
        #endregion

    }
}
