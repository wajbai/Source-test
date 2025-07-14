using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ACPP.Modules.Transaction;
using Bosco.Model.Business;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;

namespace ACPP.Modules.UIControls
{
    public partial class UcFrontOfficeDonorList : UserControl
    {
        #region Declaration
        CommonMember UtilityMemeber = new CommonMember();
        #endregion

        #region Properties
        public CommunicationMode FrontOfficeActivity { get; set; }
        public DataTable DataSource
        {
            get
            {
                return gcDonor.DataSource as DataTable;
            }
            set
            {
                gcDonor.DataSource = FilterSelectedSource(value);
                gcDonor.RefreshDataSource();
            }
        }
        #endregion

        #region Constructor
        public UcFrontOfficeDonorList()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private DataTable FilterSelectedSource(DataTable dtDonorList)
        {
            DataTable dtDonor = new DataTable();
            if (dtDonorList != null && dtDonorList.Rows.Count > 0)
            {
                DataView dvDonorlist = dtDonor.AsDataView();
                dvDonorlist.RowFilter = "SELECT=1";
                dtDonor = dvDonorlist.ToTable();
                dvDonorlist.RowFilter = "";
            }
            return dtDonor;
        }
        #endregion

        private void gvDonor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (gvDonor.Columns.Contains(gvDonor.Columns["Status"]))
                {
                    if (e.RowHandle >= 0)
                    {
                        int MailStatus = this.UtilityMemeber.NumberSet.ToInteger(gvDonor.GetRowCellValue(e.RowHandle, gvDonor.Columns["Status"]).ToString());
                        if (MailStatus == 0)
                        {
                            e.Appearance.BackColor = Color.Salmon;
                            e.Appearance.BackColor2 = Color.SeaShell;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }


    }
}