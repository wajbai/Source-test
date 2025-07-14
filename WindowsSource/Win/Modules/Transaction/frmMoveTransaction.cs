using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Transaction;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraPrinting;
using Bosco.DAO.Schema;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Business;
using DevExpress.XtraBars;

namespace ACPP.Modules.Transaction
{
    public partial class frmMoveTransaction : frmFinanceBaseAdd
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        private DataSet dsCostCentre = new DataSet();
        private MoveTransForm moveTransForm = MoveTransForm.Transaction;
        public event EventHandler UpdateHeld;
        #endregion

        private int transFromProjectId = 0;
        private int TransFromProjectId
        {
            get { return transFromProjectId; }
            set { transFromProjectId = value; }
        }

        private int transToProjectId = 0;
        private int TransToProjectId
        {
            get { return transToProjectId; }
            set { transToProjectId = value; }
        }

        private int voucherId;
        private int VoucherId
        {
            get { return voucherId; }
            set { voucherId = value; }
        }

        private int ledgerId;
        private int LedgerId
        {
            get { return ledgerId; }
            set { ledgerId = value; }
        }

        private int transVoucherMethod;
        private int TransVoucherMethod
        {
            get { return transVoucherMethod; }
            set { transVoucherMethod = value; }
        }

        private DataTable dtProjectInfo = null;
        private DataTable ProjectInfo
        {
            get { return dtProjectInfo; }
            set { dtProjectInfo = value; }
        }

        private DateTime TransFromVoucherDate { get; set; }
        #region Constructor

        public frmMoveTransaction()
        {
            InitializeComponent();
        }

        public frmMoveTransaction(int projectId, int voucherId, int ledgerId, MoveTransForm moveForm, Int32 voucherdefinitionid)
            : this()
        {
            VoucherDefinitionId = voucherdefinitionid;
            TransFromProjectId = projectId;
            VoucherId = voucherId;
            LedgerId = ledgerId;
            moveTransForm = moveForm;
        }

        private Int32 voucherdefinitionid = 0;
        private Int32 VoucherDefinitionId
        {
            set
            {
                voucherdefinitionid = value;

                using (VoucherSystem vouchersystem = new VoucherSystem())
                {
                    vouchersystem.VoucherId = voucherdefinitionid;
                    ResultArgs result  = vouchersystem.VoucherDetailsById();
                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtVoucherTypeDetail = result.DataSource.Table;
                        Int32 basevouchertype = vouchersystem.NumberSet.ToInteger(dtVoucherTypeDetail.Rows[0][vouchersystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName].ToString());
                        lblVoucherType.Text = dtVoucherTypeDetail.Rows[0][vouchersystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName].ToString();
                    }
                }
            }
            get
            {
                return voucherdefinitionid;
            }
        }

        #endregion

        #region Events
        private void frmVoucherTransaction_Load(object sender, EventArgs e)
        {
            LoadProjects();
            setDefaultValues();
        }

        private void btnMoveTrans_Click(object sender, EventArgs e)
        {
            DataTable dt = gcMoveTrans.DataSource as DataTable;
            if (ProjectInfo != null)
            {
                if (gvMoveTrans.RowCount > 0)
                {
                    DataView dvProjectInfo = ProjectInfo.DefaultView;
                    dvProjectInfo.RowFilter = "FLAG=1";

                    DataTable dtProject = dvProjectInfo.ToTable();
                    if (dtProject != null && dtProject.Rows.Count > 0)
                    {
                        dvProjectInfo.RowFilter = "";
                        TransToProjectId = this.UtilityMember.NumberSet.ToInteger(dtProject.Rows[0]["PROJECT_ID"].ToString());

                        // resultArgs = CheckAlltheLedgersMapped();
                        // Result equals "1" It means that all the Ledgers are mapped to the selected Project
                        // Result equals "0" Some Ledgers are not mapped
                        //if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                        //{
                        resultArgs = MoveVoucherDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_MOVE_SUCCESS));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                                this.Close();
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(resultArgs.Message);
                        }
                        //}
                        //else
                        //{
                        //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.MOVE_TRANSACTION_ALL_THE_LEDGER_NOT_MAPPED));
                        //}

                    }
                    else
                    {
                        dvProjectInfo.RowFilter = "";
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_MOVE_GREATER_THAN_ONE));
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_MOVE_GREATER_THAN_ONE));
            }
        }

        private ResultArgs CheckAlltheLedgersMapped()
        {
            try
            {
                using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
                {
                    resultArgs = voucherTrans.CheckLedgerareMappedByVoucher(VoucherId, TransToProjectId);
                }
            }
            catch (Exception ed)
            {
                this.ShowMessageBoxError(ed.Message);
            }
            return resultArgs;
        }

        private void rchkCheckProject_CheckedChanged(object sender, EventArgs e)
        {
            if (gvMoveTrans.RowCount > 0)
            {
                int projectId = gvMoveTrans.GetFocusedRowCellValue(colProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvMoveTrans.GetFocusedRowCellValue(colProjectId).ToString()) : 0;
                CheckEdit chkEdit = sender as CheckEdit;
                int status = Convert.ToInt32(chkEdit.CheckState);
                ProjectInfo = MoveTransToSelectedProject(projectId, status);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        private void LoadProjects()
        {
            using (MappingSystem projectSystem = new MappingSystem())
            {
                resultArgs = projectSystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DataView dvProjects = resultArgs.DataSource.Table.DefaultView;
                    dvProjects.RowFilter = "PROJECT_ID<>" + TransFromProjectId;
                    if (dvProjects != null && dvProjects.Count > 0)
                    {
                        DataTable dtProjects = dvProjects.ToTable();
                        dtProjects.Columns.Add("FLAG", typeof(int));
                        gcMoveTrans.DataSource = dtProjects;
                        gcMoveTrans.RefreshDataSource();
                    }
                    dvProjects.RowFilter = "";
                }
            }
        }

        private DataTable MoveTransToSelectedProject(int projectId, int status)
        {
            DataTable dtMoveTrans = (DataTable)gcMoveTrans.DataSource;

            for (int i = 0; i < dtMoveTrans.Rows.Count; i++)
            {
                int ProjectId = dtMoveTrans.Rows[i]["PROJECT_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMoveTrans.Rows[i]["PROJECT_ID"].ToString()) : 0;
                if (ProjectId == projectId)
                {
                    dtMoveTrans.Rows[i]["FLAG"] = status;
                }
                else
                {
                    dtMoveTrans.Rows[i]["FLAG"] = (int)YesNo.No;
                }
            }
            return dtMoveTrans;
        }

        private ResultArgs MoveVoucherDetails()
        {
            ResultArgs resultArgs = null;
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem(VoucherId))
            {
                TransFromVoucherDate = voucherTransaction.VoucherDate;
                voucherTransaction.VoucherId = VoucherId;
                voucherTransaction.ProjectId = TransToProjectId;
                voucherTransaction.VoucherDate = MoveDate.DateTime;
                voucherTransaction.Status = 1;

                resultArgs = voucherTransaction.FetchVoucherNumberDefinition(); //  FetchVoucherMethod();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    TransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherTransaction.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                    if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                    {
                        voucherTransaction.TransVoucherMethod = TransVoucherMethod;
                    }
                    else
                    {
                        voucherTransaction.VoucherNo = string.Empty;
                    }
                }

                resultArgs = voucherTransaction.MoveVoucherDetails(moveTransForm, TransFromVoucherDate, TransFromProjectId);
            }
            return resultArgs;
        }

        private void setDefaultValues()
        {
            if (VoucherId > 0)
            {
                MoveDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                MoveDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(VoucherId))
                {
                    MoveDate.DateTime = voucherSystem.VoucherDate;
                    lblVoucherDate.Text = voucherSystem.VoucherDate.ToShortDateString();
                    lblVoucherNo.Text = voucherSystem.VoucherNo;
                    lblProject.Text = voucherSystem.ProjectName;
                }
            }
        }
        #endregion

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMoveTrans.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                MoveDate.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }
    }
}