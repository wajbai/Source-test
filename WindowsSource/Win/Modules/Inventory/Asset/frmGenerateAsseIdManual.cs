using System;
using System.Data;
using System.Linq;
using Bosco.Utility;
using DevExpress.XtraGrid;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmGenerateAsseIdManual : frmFinanceBaseAdd
    {
        #region Declaration
        GridControl gcAssetItem = new GridControl();
        #endregion

        #region Constructors

        public frmGenerateAsseIdManual()
        {
            InitializeComponent();
        }

        public frmGenerateAsseIdManual(int ItemId, string AssetItem, GridControl gcData)
            : this()
        {
            lblAssetItem.Text = AssetItem;
            gcAssetItem = gcData;
        }
        

        #endregion

        #region Methods

        private bool Validate()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtPrefix.Text.Trim()))
            {
                this.ShowMessageBox(GetMessage(MessageCatalog.Asset.AssetItem.ASSET_PREFIX_EMPTY));
                this.SetBorderColor(txtPrefix);
                isValue = false;
                txtPrefix.Focus();
            }
            else if (string.IsNullOrEmpty(txtStartingNo.Text.Trim()))
            {
                this.ShowMessageBox(GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_STARTINNO_EMPTY));
                this.SetBorderColor(txtStartingNo);
                isValue = false;
                txtStartingNo.Focus();
            }
            else if (string.IsNullOrEmpty(txtWidth.Text.Trim()))
            {
                this.ShowMessageBox(GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_WIDTH_EMPTY));
                this.SetBorderColor(txtWidth);
                isValue = false;
                txtWidth.Focus();
            }
            return isValue;
        }

        private DataTable UpdateDataTableValues(DataTable dtAssetList)
        {
            try
            {
                if (dtAssetList != default(DataTable))
                {
                    string Prefix = txtPrefix.Text.Trim();
                    string Suffix = txtSuffix.Text.Trim();
                    int StartingNo = this.UtilityMember.NumberSet.ToInteger(txtStartingNo.Text.Trim());
                    int Width = this.UtilityMember.NumberSet.ToInteger(txtWidth.Text.Trim());

                    Func<string> GenereateAssetId = () => Prefix + GenerateWidth(StartingNo++, Width) + Suffix;
                    dtAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                    {
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            dr["ASSET_ID"] = GenereateAssetId.Invoke();
                        }
                    });
                    gcAssetItem.DataSource = dtAssetList;
                    gcAssetItem.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return dtAssetList;
        }

        public string GenerateWidth(int StarNo, int Width)
        {
            string GeneratedNo = string.Empty;
            string TempNo = string.Empty;
            GeneratedNo = StarNo.ToString().PadLeft(Width, '0');
            return GeneratedNo;
        }

        #endregion

        #region Events

        private void frmGenerateAsseIdManual_Load(object sender, EventArgs e)
        {

        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Validate();
            DataTable dtgenerate = new DataTable();
            dtgenerate=gcAssetItem.DataSource as DataTable;
            UpdateDataTableValues(dtgenerate);
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtPrefix_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPrefix);
        }

        private void txtStartingNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtStartingNo);
        }

        private void txtWidth_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtWidth);
        }

       #endregion
    }
}