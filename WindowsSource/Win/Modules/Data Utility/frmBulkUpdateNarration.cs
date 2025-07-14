// On 03/03/2021, In Budget Annual Summary - New Project, 
/// Few Clients asks us to name it as New Budget with income, expenditure and Province Help, but 
/// Manfort asks us to name it as Development Projects and wanted few more extra details like local fund and govt fund


using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


using Bosco.Utility;
using Bosco.Model.Setting;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using ACPP.Modules.Data_Utility;
using DevExpress.XtraEditors;


namespace ACPP.Modules
{
    public partial class frmBulkUpdateNarration: frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Property

        private string ExistingNarration
        {
            get
            {
                string Rtn = string.Empty;

                if (glkpNarration.EditValue != null)
                {
                    Rtn = glkpNarration.Text.ToString();
                }

                return Rtn;
            }
        }

        private Int32 ExistingNarrationNoOfVouchers
        {
            get
            {
                Int32 Rtn = 0; ;
                if (glkpNarration.EditValue != null)
                {
                    if (glkpNarration.GetSelectedDataRow()!=null)
                    {
                        DataRowView drv = glkpNarration.GetSelectedDataRow() as DataRowView;
                        Rtn = UtilityMember.NumberSet.ToInteger(drv["No_of_Vouchers"].ToString());
                    }
                }

                return Rtn;
            }
        }
        #endregion

        #region Constructors
        public frmBulkUpdateNarration()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        /// <summary>
        /// This Method is used to load all unique Voucher narrations
        /// </summary>
        private void LoadNarration()
        {
            using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
            {
                resultArgs = vouchermastersystem.FetchNarration();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table!=null)
                {
                    DataTable dtNarrations = resultArgs.DataSource.Table;
                    
                    //To have uniqueNo
                    //dtNarrations.Columns.Add(vouchermastersystem.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName, typeof(System.Int32));
                    //dtNarrations.Columns[vouchermastersystem.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].AutoIncrement = true;
                    //dtNarrations.Columns[vouchermastersystem.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].AutoIncrementSeed = 1;
                    //dtNarrations.Columns[vouchermastersystem.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].AutoIncrementStep = 1;
                    
                    ////To take unique narrations
                    //string[] uniqueflds = new string[] { vouchermastersystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName};
                    //dtNarrations = dtNarrations.DefaultView.ToTable(true, uniqueflds);

                    UtilityMember.ComboSet.BindGridLookUpCombo(glkpNarration, dtNarrations, vouchermastersystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName, "ROW");
                    glkpNarration.Properties.PopupFormSize = new System.Drawing.Size(glkpNarration.Width, glkpNarration.Properties.PopupFormSize.Height);
                    colRow.Visible = false;
                }
            }
        }
        #endregion

        #region Events
        
        private void frmFinanceSetting_Load(object sender, EventArgs e)
        {
            LoadNarration();
            lblNoofVouchers.Text = string.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ExistingNarrationNoOfVouchers > 0)
            {
                string msg = "There are " + ExistingNarrationNoOfVouchers + " Voucher(s) available. Are you sure to change/update Narration for all affected Vouchers ?";
                if (string.IsNullOrEmpty(txtNarration.Text))
                {
                    msg += System.Environment.NewLine + System.Environment.NewLine + "Note : '" + ExistingNarration + "' will be deleting for all affected Vouchers.";
                }
                else
                {
                    msg += System.Environment.NewLine + System.Environment.NewLine + "Note : '" + ExistingNarration + "' will be replacing by '" + txtNarration.Text + "'";
                }
                
                if (this.ShowConfirmationMessage(msg, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question) 
                                    == System.Windows.Forms.DialogResult.Yes)
                {
                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                    {
                       ResultArgs resultarg =  vouchersystem.BulKUpdateNarration(ExistingNarration, txtNarration.Text.Trim());
                       if (resultarg.Success)
                       {
                           LoadNarration();
                           txtNarration.Text = string.Empty;
                           glkpNarration.Select();
                           glkpNarration.Focus();
                       }
                    }
                }
            }
            else
            {
                MessageRender.ShowMessage("Select Narration");
                glkpNarration.Select();
                glkpNarration.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpNarration_EditValueChanged(object sender, EventArgs e)
        {
            txtNarration.Text = ExistingNarration;
            lblNoofVouchers.Text = ExistingNarrationNoOfVouchers.ToString() + " Voucher(s)";
            lblNoofVouchers.Font = new System.Drawing.Font(lblNoofVouchers.Font.FontFamily, lblNoofVouchers.Font.Size, System.Drawing.FontStyle.Bold);
        }
        #endregion   

        private void glkpNarration_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        
    }
}