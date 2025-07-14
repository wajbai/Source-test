using System;
using System.Data;
using Bosco.Utility;
using Bosco.Model;
using DevExpress.XtraGrid.Views.Grid;
using ACPP.Modules.Asset;
using ACPP.Modules.Asset.Transactions;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetVoucherView : frmFinanceBase
    {
        #region VariableDeclaration
        public event EventHandler VoucherEditHeld;
        public string ProjectName = string.Empty;
        public int ProjectId = 0;
        DateTime LastVoucheDate;
        private int RowIndex = 0;
        bool vtype = true;
        public AssetInOut Flag { get; set; }
        ResultArgs resulArgs = null;
        #endregion

        #region Construction
        public frmAssetVoucherView(string ProName, int proid, DateTime dtfrom, AssetInOut flag)
            : this()
        {
            ProjectName = ProName;
            ProjectId = proid;
            LastVoucheDate = dtfrom;
            Flag = flag;
        }
        public frmAssetVoucherView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int inOutId = 0;
        public int InOutId
        {
            get
            {
                RowIndex = gvAssetVoucherView.FocusedRowHandle;
                inOutId = gvAssetVoucherView.GetFocusedRowCellValue(colInOutID) != null ? this.UtilityMember.NumberSet.ToInteger(gvAssetVoucherView.GetFocusedRowCellValue(colInOutID).ToString()) : 0;
                return inOutId;
            }
            set
            {
                inOutId = value;
            }
        }
        #endregion

        #region Events

        private void gcAssetVoucherView_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
            if (InOutId > 0)
            {
                ShowEditForm();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void frmAssetVoucherView_Load(object sender, EventArgs e)
        {
            SetDefaults();
            LoadAssetInOutwardDetails();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAssetVoucherView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAssetVoucherView, colDate);
            }
        }
        #endregion

        #region Methods
        private void SetDefaults()
        {
            ucProjectName.Caption = ProjectName;
            ucProjectName.CaptionSize = 12;

            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

            if (LastVoucheDate.ToString() != string.Empty)
            {
                deDateTo.DateTime = LastVoucheDate.Date;
                deDateFrom.DateTime = (new DateTime(deDateTo.DateTime.Year, deDateTo.DateTime.Month, 1) < deDateFrom.Properties.MinValue) ? UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) : new DateTime(deDateTo.DateTime.Year, deDateTo.DateTime.Month, 1);
            }
            else
            {
                deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                deDateTo.DateTime = (deDateTo.DateTime > deDateTo.Properties.MaxValue) ? UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false) : deDateFrom.DateTime.AddMonths(1).AddDays(-1);
            }
        }

        private void LoadAssetInOutwardDetails()
        {
            DataSet dsAssetVoucherView = new DataSet();
            try
            {
                using (AssetInwardOutwardSystem assetInOutSystem = new AssetInwardOutwardSystem())
                {
                    assetInOutSystem.ProjectId = ProjectId;
                    assetInOutSystem.DateFrom = deDateFrom.DateTime;
                    assetInOutSystem.DateTo = deDateTo.DateTime;
                    assetInOutSystem.Flag = Flag.ToString();
                    dsAssetVoucherView = assetInOutSystem.FetchAssetInOutDetailsByFlag();
                    if (dsAssetVoucherView != null && dsAssetVoucherView.Tables.Count > 0)
                    {
                        gcAssetVoucherView.DataSource = dsAssetVoucherView;
                        gcAssetVoucherView.DataMember = "Master";
                        gcAssetVoucherView.RefreshDataSource();
                    }
                    else
                    {
                        gcAssetVoucherView.DataSource = null;
                        gcAssetVoucherView.RefreshDataSource();
                    }
                    gvAssetVoucherView.FocusedRowHandle = 0;
                    gvAssetVoucherView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void ShowEditForm()
        {
            if (Flag == AssetInOut.PU || Flag == AssetInOut.IK)
            {
                frmInwardVoucherAdd purchaseAdd = new frmInwardVoucherAdd(LastVoucheDate.ToString(), ProjectId, ProjectName, InOutId, this.Flag);
                purchaseAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                purchaseAdd.ShowDialog();
            }
            else if (Flag == AssetInOut.SL || Flag == AssetInOut.DS || Flag == AssetInOut.DN)
            {
                frmAssetOutward SalesVoucherAdd = new frmAssetOutward(ProjectId, ProjectName, InOutId, LastVoucheDate.ToString());
                SalesVoucherAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                SalesVoucherAdd.ShowDialog();
            }
        }
        #endregion

        private void gvAssetVoucherView_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvAssetVoucherView.RowCount.ToString();
        }
    }
}