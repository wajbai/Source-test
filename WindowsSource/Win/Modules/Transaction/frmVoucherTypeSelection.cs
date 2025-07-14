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
using DevExpress.XtraLayout.Utils;
using Bosco.Model.UIModel.Master;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Transaction
{
    public partial class frmVoucherTypeSelection : frmFinanceBase
    {

        #region Properties
        Int32 ProjectId = 0;
        string BaseVoucherTypes = DefaultVoucherTypes.Receipt.ToString();
        Int32 DefinitionVouhcerTypeId = 0;
        #endregion

        #region Constractor

        public frmVoucherTypeSelection()
        {
            InitializeComponent();
        }

        public frmVoucherTypeSelection(Int32 projectId, string basevouchertypes = "", Int32 definitionvouhcertypeid = 0)
            : this()
        {
            try
            {
                ProjectId = projectId;
                BaseVoucherTypes = basevouchertypes;
                DefinitionVouhcerTypeId = definitionvouhcertypeid;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }

        #endregion

        #region Events



        #endregion
        #region Run Time Events and methods
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern System.IntPtr CreateRoundRectRgn
        (
         int nLeftRect, // x-coordinate of upper-left corner
         int nTopRect, // y-coordinate of upper-left corner
         int nRightRect, // x-coordinate of lower-right corner
         int nBottomRect, // y-coordinate of lower-right corner
         int nWidthEllipse, // height of ellipse
         int nHeightEllipse // width of ellipse
        );

        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(System.IntPtr hObject);
        #endregion

        private void frmVoucherTypeSelection_Load(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 2, Height - 2, 5, 5));

            if (ProjectId > 0)
            {
                using (ProjectSystem projectsys = new ProjectSystem())
                {
                    ResultArgs resultArg = projectsys.ProjectVouchers(ProjectId);
                    if (resultArg != null && resultArg.Success)
                    {
                        DataTable dtVoucherTypes = resultArg.DataSource.Table;
                        if (!string.IsNullOrEmpty(BaseVoucherTypes))
                        {
                            dtVoucherTypes.DefaultView.RowFilter = projectsys.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName + " IN (" + BaseVoucherTypes + ")";
                        }
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpVoucherType, dtVoucherTypes, projectsys.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName,
                            projectsys.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName);
                        if (DefinitionVouhcerTypeId > 0)
                        {
                            glkpVoucherType.EditValue = DefinitionVouhcerTypeId;
                        }

                    }
                }
            }
        }

        private void glkpVoucherType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyVoucherType();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                CancelVoucherType();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ApplyVoucherType();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelVoucherType();
        }

        private void CancelVoucherType()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ReturnValue = null;
            this.Close();
        }

        private void ApplyVoucherType()
        {
            string[] RtnVoucherType;
            if (glkpVoucherType.GetSelectedDataRow() != null)
            {
                DataRowView datarowview = glkpVoucherType.GetSelectedDataRow() as DataRowView;
                RtnVoucherType = new string[] { datarowview["VOUCHER_ID"].ToString(), datarowview["VOUCHER_TYPE"].ToString() };
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.ReturnValue = RtnVoucherType;
            }
            this.Close();

        }

       
    }
}