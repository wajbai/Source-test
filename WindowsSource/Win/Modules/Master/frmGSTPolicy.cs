using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Transaction;


namespace ACPP.Modules.Master
{
    public partial class frmGSTPolicy : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        private int RowIndex = 0;
        #endregion

        #region Property

        private int gstid;
        public int GSTId
        {
            get
            {
                gstid = gvGSTPolicy.GetFocusedRowCellValue(colGSTId) != null ? this.UtilityMember.NumberSet.ToInteger(gvGSTPolicy.GetFocusedRowCellValue(colGSTId).ToString()) : 0;
                return gstid;
            }
            set
            {
                gstid = value;
            }
        }

        private string gstslab;
        public string GSTSlab
        {
            get
            {
                gstslab = gvGSTPolicy.GetFocusedRowCellValue(colSlab) != null ? gvGSTPolicy.GetFocusedRowCellValue(colSlab).ToString() : "";
                return gstslab;
            }
        }

        private bool IsDefaultGSTClass
        { 
            get
            {
                bool rtn=false;
                if (gstid > 0)
                {
                    if (GSTSlab == GSTDefaultClass.GST0.ToString() || GSTSlab == GSTDefaultClass.GST05.ToString() ||
                        GSTSlab == GSTDefaultClass.GST12.ToString() || GSTSlab == GSTDefaultClass.GST18.ToString() || GSTSlab == GSTDefaultClass.GST28.ToString())
                    {
                        rtn = true;
                    }
                }
                return rtn;
            }
        }
        #endregion

        #region Constructor

        public frmGSTPolicy()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadGSTDetails()
        {
            //UcGSTPolicy.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            //UcGSTPolicy.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            //UcGSTPolicy.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;

            using (GSTClassSystem GSTSystem = new GSTClassSystem())
            {
                resultArgs = GSTSystem.GetGSTDetails();
                if (resultArgs.Success)
                {
                    gcGSTPolicy.DataSource = resultArgs.DataSource.Table;
                    gcGSTPolicy.RefreshDataSource();
                }
            }
        }

        private void LoadGStRights()
        {
            this.enumUserRights.Add(GST.CreateGST);
            this.enumUserRights.Add(GST.EditGSt);
            this.enumUserRights.Add(GST.DeleteGST);
            this.enumUserRights.Add(GST.PrintGST);
            this.enumUserRights.Add(GST.ViewGSt);
            this.ApplyUserRights(UcGSTPolicy, enumUserRights, (int)Menus.GST);
        }

        private void ShowGST(int gstid)
        {
            bool rtn = false;
            try
            {
                if (gstid>0 && IsDefaultGSTClass)
                {
                    this.ShowMessageBox("Standard/Default GST Class can't be deleted");
                    rtn = false;
                }
                else if (gstid > 0 && GSTVouchersExists())
                {
                    this.ShowMessageBoxWarning("GST enabled Vouchers are already made for this GST class, You can't Modify/Delete.");
                    rtn = false;
                }
                else
                    rtn = true;

                if (rtn)
                {
                    frmGST gst = new frmGST(gstid);
                    gst.updateheld += new EventHandler(OnUpdateHeld);
                    gst.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        

        /// <summary>
        /// Delete GST Details
        /// </summary>
        private void DeleteGSTDetails()
        {
            try
            {
                if (gvGSTPolicy.RowCount != 0)
                {
                    if (GSTId != 0)
                    {
                        bool rtn = false;
                        if (gstid > 0 && IsDefaultGSTClass)
                        {
                            this.ShowMessageBox("Standard/Default GST Class can't be deleted");
                            rtn = false;
                        }
                        else
                            rtn = true;

                        if (rtn)
                        {
                            using (GSTClassSystem gstSystem = new GSTClassSystem())
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    resultArgs = gstSystem.DeleteGStDetails(GSTId);
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        LoadGSTDetails();
                                    }

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
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadGSTDetails();
        }

        private bool GSTVouchersExists()
        {
            bool rtn = true;

            if (GSTId > 0)
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    bool gstvouchersexists = vouchersystem.IsExistsGSTVouchersByGSTClassId(GSTId);

                    if (gstvouchersexists)
                    {
                        rtn = true; 
                    }
                    else
                        rtn = false;
                }
            }
            return rtn;
        }

        #endregion

        #region Events

        private void frmGSTPolicy_Load(object sender, EventArgs e)
        {
            LoadGStRights();
            LoadGSTDetails();
        }

        private void UcGSTPolicy_AddClicked(object sender, EventArgs e)
        {
            ShowGST((int)AddNewRow.NewRow);
        }

        private void gvGSTPolicy_DoubleClick(object sender, EventArgs e)
        {
            ShowGST(GSTId);
        }

        private void UcGSTPolicy_EditClicked(object sender, EventArgs e)
        {
            ShowGST(GSTId);
        }

        private void UcGSTPolicy_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UcGSTPolicy_DeleteClicked(object sender, EventArgs e)
        {
            DeleteGSTDetails();
        }

        private void UcGSTPolicy_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcGSTPolicy, this.GetMessage(MessageCatalog.Master.GST.GST_PRINT_CAPTION), PrintType.DT, gvGSTPolicy, true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvGSTPolicy.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvGSTPolicy, colSlab);
            }
        }

        private void gvGSTPolicy_RowCountChanged(object sender, EventArgs e)
        {
            labelControl1.Text = gvGSTPolicy.RowCount.ToString();
        }

        private void frmGSTPolicy_EnterClicked(object sender, EventArgs e)
        {
            ShowGST(GSTId);
        }

        #endregion
    }
}