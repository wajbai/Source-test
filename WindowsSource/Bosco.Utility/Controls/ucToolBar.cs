using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using System.Collections;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraBars;
using DevExpress.Utils;

namespace Bosco.Utility.Controls
{
    public partial class ucToolBar : DevExpress.XtraEditors.XtraUserControl
    {
        #region Variable Declaration
        public string MasterMode = string.Empty;
        public event EventHandler AddClicked;
        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler PrintClicked;
        public event EventHandler CloseClicked;
        public event EventHandler RefreshClicked;
        public event EventHandler MoveTransaction;
        public event EventHandler NatureofPayments;
        public event EventHandler InsertVoucher;
        //   public event EventHandler ExportClicked;
        public event EventHandler DownloadExcel;
        public event EventHandler NegativeBalanceClicked;
        public event EventHandler RestoreVoucherClicked;
        public event EventHandler PostInterestClicked;
        public event EventHandler RenewClicked;
        #endregion

        #region Properties
        public bool DisableAddButton
        {
            get { return btiAdd.Enabled; }
            set { btiAdd.Enabled = value; }
        }

        public string DeleteCaption
        {
            set { btiDelete.Caption = value; }
        }

        public bool DisableEditButton
        {
            get { return btiEdit.Enabled; }
            set { btiEdit.Enabled = value; }
        }

        public bool DisableDeleteButton
        {
            get { return btiDelete.Enabled; }
            set { btiDelete.Enabled = value; }
        }

        public bool DisableCloseButton
        {
            get { return btiClose.Enabled; }
            set { btiClose.Enabled = value; }
        }

        public bool DisableMoveTransaction
        {
            get { return bbiMoveTransaction.Enabled; }
            set { bbiMoveTransaction.Enabled = value; }
        }
        public bool DisableRestoreVoucher
        {
            get { return bbiRestoreVoucher.Enabled; }
            set { bbiRestoreVoucher.Enabled = value; }
        }
        public bool DisablePostInterest
        {
            get { return bbiPostInterest.Enabled; }
            set { bbiPostInterest.Enabled = value; }
        }
        public bool ShowPDF
        {
            get { return barPDF.Enabled; }
            set { barPDF.Enabled = value; }
        }

        public bool ShowXLS
        {
            get { return barExcel.Enabled; }
            set { barExcel.Enabled = value; }
        }

        public bool ShowXLSX
        {
            get { return barXLSX.Enabled; }
            set { barXLSX.Enabled = value; }
        }

        public bool ShowHTML
        {
            get { return barHTML.Enabled; }
            set { barHTML.Enabled = value; }
        }

        public bool ShowMMT
        {
            get { return barMHT.Enabled; }
            set { barMHT.Enabled = value; }
        }

        public bool ShowRTF
        {
            get { return barRTF.Enabled; }
            set { barRTF.Enabled = value; }
        }

        public bool ShowText
        {
            get { return barText.Enabled; }
            set { barText.Enabled = value; }
        }

        public bool DisablePrintButton
        {
            get { return btiPrint.Enabled; }
            set { btiPrint.Enabled = value; }
        }

        public bool DisableDownloadExcel
        {
            get { return bbiDownloadExcel.Enabled; }
            set { bbiDownloadExcel.Enabled = value; }
        }

        public bool DisableNatureofPayments
        {
            get { return bbiFetchNatureofPayments.Enabled; }
            set { bbiFetchNatureofPayments.Enabled = value; }
        }
        public bool DisableAMCRenew
        {
            get { return bbiRenew.Enabled; }
            set { bbiRenew.Enabled = value; }
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
            get { return btiDelete.Visibility; }
            set { btiDelete.Visibility = value; }
        }

        public BarItemVisibility VisibleRefresh
        {
            get { return btiRefresh.Visibility; }
            set { btiRefresh.Visibility = value; }
        }

        public BarItemVisibility VisiblePrintButton
        {
            get { return btiPrint.Visibility; }
            set { btiPrint.Visibility = value; }
        }
        public BarItemVisibility VisibleMoveTrans
        {
            get { return bbiMoveTransaction.Visibility; }
            set { bbiMoveTransaction.Visibility = value; }
        }

        public BarItemVisibility VisibleNatureofPayments
        {
            get { return bbiFetchNatureofPayments.Visibility; }
            set { bbiFetchNatureofPayments.Visibility = value; }
        }

        public BarItemVisibility VisbleInsertVoucher
        {
            get { return btiInsertVoucher.Visibility; }
            set { btiInsertVoucher.Visibility = value; }
        }

        public bool DisableInsertVoucher
        {
            get { return btiInsertVoucher.Enabled; }
            set { btiInsertVoucher.Enabled = value; }
        }

        public BarItemVisibility VisibleDownloadExcel
        {
            get { return bbiDownloadExcel.Visibility; }
            set { bbiDownloadExcel.Visibility = value; }
        }
        public BarItemVisibility VisibleRestoreVoucher
        {
            get { return bbiRestoreVoucher.Visibility; }
            set { bbiRestoreVoucher.Visibility = value; }
        }
        public BarItemVisibility VisibleClose
        {
            get { return btiClose.Visibility; }
            set { btiClose.Visibility = value; }
        }
        public BarItemVisibility VisiblePostInterest
        {
            get { return bbiPostInterest.Visibility; }
            set { bbiPostInterest.Visibility = value; }
        }
        public BarItemVisibility VisibleRenew
        {
            get { return bbiRenew.Visibility; }
            set { bbiRenew.Visibility = value; }
        }
        public string ChangeCaption
        {
            get { return btiEdit.Caption; }
            set
            {
                if (value != "&Edit")
                {
                    btiEdit.Caption = value;
                }
            }
        }

        public string ChangeAddCaption
        {
            get { return btiAdd.Caption; }
            set
            {
                if (value != "&Add")
                {
                    btiAdd.Caption = value;
                }
            }
        }
        public string ChangeDeleteCaption
        {
            get { return btiDelete.Caption; }
            set
            {
                if (value != "&Delete")
                {
                    btiDelete.Caption = value;
                }
            }
        }
        public string ChangePrintCaption
        {
            get { return btiPrint.Caption; }
            set
            {
                if (value != "&Print")
                {
                    btiPrint.Caption = value;
                }
            }
        }

        public string ChangeNatureOfPaymentCaption
        {
            get { return bbiFetchNatureofPayments.Caption; }
            set
            {
                bbiFetchNatureofPayments.Caption = value;
            }
        }
        public string ChangePostInterestCaption
        {
            get { return bbiPostInterest.Caption; }
            set
            {
                bbiPostInterest.Caption = value;
            }
        }

        public SuperToolTip ChangePostInterestSuperToolTip
        {
            get { return bbiPostInterest.SuperTip; }
            set
            {
                bbiPostInterest.SuperTip = value;
            }
        }

        public string ChnageRenewCaption
        {
            get { return bbiRenew.Caption; }
            set
            {
                bbiRenew.Caption = value;
            }
        }
        public BarItemVisibility VisibleNegativeBalance
        {
            get { return bbiNegativeBalance.Visibility; }
            set { bbiNegativeBalance.Visibility = value; }
        }

        public string ChangeMoveVoucherCaption
        {
            get { return bbiMoveTransaction.Caption; }

            set
            {
                if (value != "&Move Voucher")
                {
                    bbiMoveTransaction.Caption = value;
                }
            }
        }

        public SuperToolTip ChangeMoveVoucherTooltip
        {
            get { return bbiMoveTransaction.SuperTip; }
            set { bbiMoveTransaction.SuperTip = value; }
        }
        #endregion

        #region Constructor
        public ucToolBar()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        private void ucToolBar_Load(object sender, EventArgs e)
        {
            // To Change Delete Messages to Re Open Caption
            // Temporary
            if (this.ChangeDeleteCaption.Equals("&Reopen FD"))
                ChangeTooltipDeleteMessages();
        }

        /// <summary>
        /// This is to Set tooltip
        /// </summary>
        private void ChangeTooltipDeleteMessages()
        {
            SuperToolTip sTooltip1 = new SuperToolTip();
            // Create a tooltip item that represents a header.
            ToolTipTitleItem titleItem1 = new ToolTipTitleItem();
            titleItem1.Text = "Reopen FD Details";
            // Add the tooltip items to the SuperTooltip.
            sTooltip1.Items.Add(titleItem1);


            //ToolTipController tooltipcontroler = new ToolTipController();
            //tooltipcontroler.AutoPopDelay = 15000;
            //sTooltip1.FixedTooltipWidth = false;
            //sTooltip1.DistanceBetweenItems = 5;
            //this.barManager1.ToolTipController = tooltipcontroler;
            btiDelete.SuperTip = sTooltip1;
            //-----------------------------------------------------------------------------------------------------------------

            //(barEditItem2.Edit as RepositoryItemPictureEdit).SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;

            /*tileCtlLatestVersion.Top = (pceLoggedUser.Top + pceLoggedUser.Height);
            tileCtlLatestVersion.Left = this.Width - (tileCtlLatestVersion.Width + 25);
            tltItemLatestVersion.Frames.Clear();
            tltItemLatestVersion.Frames.Add(GetAlertEmptyTileItem("Latest Version is available", "Latest Version is available", ""));
            tileCtlLatestVersion.Visible = true;
            tileCtlLatestVersion.BringToFront();*/
        }

        /// <summary>
        /// Purpose:To view the Add/Edit screen form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Purpose:To Close the form based on the types.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CloseClicked != null)
            {
                CloseClicked(this, e);
            }
        }

        private void btiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PrintClicked != null)
            {
                PrintClicked(this, e);
            }
        }

        private void btiRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (RefreshClicked != null)
            {
                RefreshClicked(this, e);
            }
        }

        private void bbiMoveTransaction_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MoveTransaction != null)
            {
                MoveTransaction(this, e);
            }
        }

        /// <summary>
        /// This event is to fetch default nature of payments through service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiFetchNatureofPayments_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (NatureofPayments != null)
            {
                NatureofPayments(this, e);
            }
        }

        private void btiInsertVoucher_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (InsertVoucher != null)
            {
                InsertVoucher(this, e);
            }
        }

        private void bbiDownloadExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DownloadExcel != null)
            {
                DownloadExcel(this, e);
            }
        }

        private void bbiNegativeBalance_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (NegativeBalanceClicked != null)
            {
                NegativeBalanceClicked(this, e);
            }
        }

        private void bbiRestoreVoucher_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (RestoreVoucherClicked != null)
            {
                RestoreVoucherClicked(this, e);
            }
        }
        private void bbiPostInterest_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PostInterestClicked != null)
            {
                PostInterestClicked(this, e);
            }
        }
        private void bbiRenew_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (RenewClicked != null)
            {
                RenewClicked(this, e);
            }
        }
        #endregion

        #region Methods
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            EventArgs e = new EventArgs();
            if (keyData == (Keys.Alt | Keys.A))
            {
                if (AddClicked != null)
                {
                    AddClicked(keyData, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.R))
            {
                if (RefreshClicked != null)
                {
                    RefreshClicked(this, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.E))
            {
                if (EditClicked != null)
                {
                    EditClicked(this, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D))
            {
                if (DeleteClicked != null)
                {
                    DeleteClicked(this, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.P))
            {
                if (PrintClicked != null)
                {
                    PrintClicked(this, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.C))
            {
                if (CloseClicked != null)
                {
                    CloseClicked(this, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.M))
            {
                if (MoveTransaction != null)
                {
                    MoveTransaction(this, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.H))
            {
                if (NegativeBalanceClicked != null)
                {
                    NegativeBalanceClicked(keyData, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.O))
            {
                if (PostInterestClicked != null)
                {
                    PostInterestClicked(keyData, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.N))
            {
                if (RenewClicked != null)
                {
                    RenewClicked(keyData, e);
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
