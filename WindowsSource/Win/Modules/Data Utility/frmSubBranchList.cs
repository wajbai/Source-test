using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;

using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.Dsync;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Setting;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmSubBranchList : frmFinanceBase
    {
        #region Varaible Decalration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmSubBranchList()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmSubBranchList_Load(object sender, EventArgs e)
        {
            FetchBranchList();
            
        }
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchBranchList();

            // gvSubBranch.FocusedRowHandle = RowIndex;
        }
        #endregion

        #region Methods
        private void FetchBranchList()
        {
            try
            {
                using (SubBranchSystem subbranchsystem = new SubBranchSystem())
                {
                    resultArgs = subbranchsystem.FetchBranchList();
                    DataTable dt = resultArgs.DataSource.Table;
                     if (resultArgs.Success)
                    {
                        gcSubBranch.DataSource = resultArgs.DataSource.Table;
                        gcSubBranch.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        
        #endregion
    }
}