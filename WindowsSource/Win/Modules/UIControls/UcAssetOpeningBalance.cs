/*************************************************************************************************************************
 *                                              Purpose     : A User control to get Asset Item details of Purchase,Asset Opening Balance, 
 *                                                            Insurance and AMC
 *                                              Author      : Carmel Raj M
 *                                              Created On  : 28-October-2015
 *************************************************************************************************************************/
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

namespace ACPP.Modules.UIControls
{
    public partial class UcAssetOpeningBalance : DevExpress.XtraEditors.XtraUserControl
    {
        #region Variables
        CommonMember utilityMember = null;
        #endregion

        #region Properties
        public int AssetId { private get; set; }
        public int ItemCount { private get; set; }
        public decimal Amount { private get; set; }
        public string Prefix { get; set; }
        public string Sufix { get; set; }
        public int StartingNo { get; set; }

        private int GetCustodianId { get { return glkCustodian.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkCustodian.EditValue.ToString()) : 0; } }
        private int GetLocationId { get { return glkLocation.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkLocation.EditValue.ToString()) : 0; } }
        private int GetManufacturerId { get { return glkManufacturer.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkManufacturer.EditValue.ToString()) : 0; } }

        #endregion

        #region Constructor
        public UcAssetOpeningBalance()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void UcAssetOpeningBalance_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //Using func delete to generate Asset Id which returns a genereted string based on Prefix and Sufix and Starting no
            Func<string> GenereateAssetId = () => Prefix + StartingNo++ + Sufix;
            //Update all the row of grid with the selected value for the concern columns
            (gvItem.DataSource as DataTable).AsEnumerable().ToList<DataRow>().ForEach(dr =>
            {
                dr["ASSET_ID"] = GenereateAssetId.Invoke();
                dr["CUSTODIANS_ID"] = GetCustodianId;
                dr["LOCATION_ID"] = GetLocationId;
                dr["MANUFACTURER_ID"] = GetManufacturerId;
                dr["AMOUNT"] = UtilityMember.NumberSet.ToDecimal(txtRatePerItem.Text);
            });
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvItem.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                gvItem.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvItem.FocusedColumn = gvItem.VisibleColumns[1];
                gvItem.ShowEditor();
            }
        }
        #endregion

        #region Methods
        private void LoadData()
        {
            if (AssetId > 0)
            {
                LoadLocations();
                LoadCustodians();
                LoadManufacturers();
            }
        }

        private void LoadLocations()
        {

        }

        private void LoadManufacturers()
        {

        }

        private void LoadCustodians()
        {

        }

        private void GenerateItem()
        {
            if (ItemCount > 0)
            {
                DataTable dtAssetItem = new DataTable("AssetItem");
                dtAssetItem.Columns.Add("ASSET_ID", typeof(string));
                dtAssetItem.Columns.Add("LOCATION_ID", typeof(Int32));
                dtAssetItem.Columns.Add("CUSTODIANS_ID", typeof(Int32));
                dtAssetItem.Columns.Add("MANUFACTURER_ID", typeof(Int32));
                dtAssetItem.Columns.Add("AMOUNT", typeof(decimal));
            }
            else
            {
                ShowMessageBox("No of item should be greater than zero");
            }
        }

        private CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        private void ShowMessageBox(string Message)
        {
            XtraMessageBox.Show(Message, "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion


    }
}
