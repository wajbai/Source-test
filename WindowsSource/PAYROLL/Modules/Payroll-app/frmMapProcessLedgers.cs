using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

using Payroll.Model.UIModel;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.Common;
using Bosco.Model.UIModel;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmMapProcessLedgers : frmPayrollBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        CommonMember UtilityMember = new CommonMember();
        public clsPrGateWay objPrGateWay = new clsPrGateWay();
        #endregion

        #region Constructor
        public frmMapProcessLedgers()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int PayrollTypeId { get; set; }
        private string PayrollTypeLedgerName { get; set; }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidationforMapLegdgers())
            {
                if (glkDeductionLedger.EditValue != null)
                {
                    clsPayrollProcess mapledger = new clsPayrollProcess();
                    // mapledger.TypeId = 0;
                    // mapledger.ProcessDate = UtilityMember.DateSet.ToDate(deProcessDate.DateTime.ToString(), false);
                    mapledger.TypeId = UtilityMember.NumberSet.ToInteger(glkIncomeLedger.EditValue.ToString());
                    mapledger.ProcessLedgerId = UtilityMember.NumberSet.ToInteger(glkDeductionLedger.EditValue.ToString());
                    resultArgs = mapledger.SaveMappedLedger();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        LoadMappedLedgers();
                        //this.ShowSuccessMessage("Ledgers Mapped successfully");
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.MapProcessLedgers.MAP_PROCESS_LEDGER_MAP_INFO));
                    }

                    else
                    {
                        //XtraMessageBox.Show("Ledgers not mapped sucessfully", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapProcessLedgers.MAP_PROCESS_LEDGER_UNMAP_INFO));
                    }
                }
            }
        }

        private void LoadMappedLedgers()
        {
            using (clsPayrollProcess mapledger = new clsPayrollProcess())
            {
                resultArgs = mapledger.FetchProcessMappedLedgers();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcMapProcessTypeLedgers.DataSource = resultArgs.DataSource.Table;
                    gcMapProcessTypeLedgers.RefreshDataSource();
                }
            }
        }

        private void frmMapProcessLedgers_Load(object sender, EventArgs e)
        {
            //string sRecentDate = objPrGateWay.GetPreviousValue();
            //if (sRecentDate != "")
            //{
            //    DateTime dNewDate = new DateTime(int.Parse(sRecentDate.Substring(6, 4)), int.Parse(sRecentDate.Substring(3, 2)), int.Parse(sRecentDate.Substring(0, 2)));
            //    deProcessDate.DateTime = deProcessDate.Properties.MinValue = dNewDate;
            //    deProcessDate.Properties.MaxValue = dNewDate.AddMonths(1).AddDays(-1);
            //}
            LoadProcessLedgerValues();
            LoadLedgerValues();
            LoadMappedLedgers(PayrollTypeId);
            LoadMappedLedgers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkIncomeLedger_EditValueChanged(object sender, EventArgs e)
        {
            PayrollTypeId = this.UtilityMember.NumberSet.ToInteger(glkIncomeLedger.EditValue.ToString());
            LoadMappedLedgers(PayrollTypeId);
        }
        #endregion

        #region Methods


        public bool ValidationforMapLegdgers()
        {
            bool isProcessLedger = true;
            if (string.IsNullOrEmpty(glkIncomeLedger.Text.Trim()))
            {
                //XtraMessageBox.Show("Process type is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapProcessLedgers.MAP_PROCESS_TYPE_EMPTY));
                //this.SetBorderColor(glkIncomeLedger);
                glkIncomeLedger.Focus();
                return false;

            }

            else if (string.IsNullOrEmpty(glkDeductionLedger.Text.Trim()))
            {
                //XtraMessageBox.Show("Ledger is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapProcessLedgers.MAP_PROCESS_LEDGER_EMPTY));
                //SetBorderColor(glkDeductionLedger);
                glkDeductionLedger.Focus();
                return false;

            }
            return isProcessLedger;
        }

        private void LoadProcessLedgerValues()
        {
            try
            {
                Processtype LedgerProcessType = new Processtype();
                DataView dvLedgerComponent = GetDescriptionfromEnumType(LedgerProcessType);
                DataTable dtLedgerComponent = dvLedgerComponent.ToTable();
                if (dtLedgerComponent != null && dtLedgerComponent.Rows.Count > 0)
                {
                    glkIncomeLedger.Properties.DataSource = dtLedgerComponent;
                    glkIncomeLedger.Properties.ValueMember = "Id";
                    glkIncomeLedger.Properties.DisplayMember = "Name";
                    glkIncomeLedger.EditValue = glkIncomeLedger.Properties.GetKeyValue(0);
                }
                else
                {
                    glkIncomeLedger.Properties.DataSource = null;
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        private void LoadLedgerValues()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchLedgerDetails();
                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtLedger = resultArgs.DataSource.Table;
                        DataView dvLedgerdetails = dtLedger.AsDataView();
                        dvLedgerdetails.RowFilter = "NATURE_ID IN (4)";
                        dtLedger = dvLedgerdetails.ToTable();

                        glkDeductionLedger.Properties.DataSource = dtLedger;
                        glkDeductionLedger.Properties.ValueMember = "LEDGER_ID";
                        glkDeductionLedger.Properties.DisplayMember = "LEDGER_NAME";
                        // glkDeductionLedger.Refresh();

                        clsPayrollProcess mapledger = new clsPayrollProcess();
                        // resultArgs = mapledger.FetchMappedLedger();
                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 2)
                        {
                            //  glkIncomeLedger.EditValue = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["LEDGER_ID"].ToString());
                            glkDeductionLedger.EditValue = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[1]["LEDGER_ID"].ToString());
                            //DateTime dtProcessdate = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                            //if (dtProcessdate >= deProcessDate.DateTime && dtProcessdate <= deProcessDate.DateTime)
                            //{
                            // deProcessDate.DateTime = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                            //}
                        }
                        else
                        {
                            glkDeductionLedger.EditValue = 0;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        private void LoadMappedLedgers(int PayTypeId)
        {
            try
            {
                using (clsPayrollProcess PayrollProcess = new clsPayrollProcess())
                {
                    DataTable dtProcessType = PayrollProcess.FetchMappedLedgerbyLedgerId(PayTypeId).DataSource.Table;
                    if (dtProcessType != null && dtProcessType.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtProcessType.Rows)
                        {
                            int ProcessLedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                            //DataTable dtProcessLedgerName=PayrollProcess.FetchLedgerByLedgerId(ProcessLedgerId).DataSource.Table;
                            //if (dtProcessLedgerName != null && dtProcessLedgerName.Rows.Count > 0)
                            //{
                            // PayrollTypeLedgerName = dtProcessLedgerName.Rows[0]["LEDGER_NAME"].ToString();
                            glkDeductionLedger.EditValue = ProcessLedgerId;
                            //}
                        }
                    }
                    else
                    {
                        glkDeductionLedger.EditValue = null;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        public DataView GetDescriptionfromEnumType(Enum enumType)
        {
            DataView dvEnumSource = null;
            DataRow drEnumSource = null;
            EnumTypeSchema.EnumTypeDataTable dtEnumSource = new EnumTypeSchema.EnumTypeDataTable();

            if (enumType != null)
            {
                try
                {
                    int enumValue = 0;
                    int i = 0;
                    string[] descs = new string[4];
                    string[] names = enumType.GetType().GetEnumNames();
                    foreach (string name in names)
                    {
                        FieldInfo fi = enumType.GetType().GetField(name);
                        object[] da = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        foreach (DescriptionAttribute ds in da)
                        {
                            descs[i] = ds.Description;
                            i++;
                        }
                    }
                    foreach (string description in descs)
                    {
                        drEnumSource = dtEnumSource.NewRow();
                        drEnumSource[dtEnumSource.IdColumn.ColumnName] = enumValue;
                        drEnumSource[dtEnumSource.NameColumn.ColumnName] = description;
                        dtEnumSource.Rows.Add(drEnumSource);
                        enumValue++;
                    }

                    dtEnumSource.AcceptChanges();
                    dvEnumSource = dtEnumSource.DefaultView;

                }
                catch (Exception e)
                {
                    new ExceptionHandler(e, true);
                }
            }

            return dvEnumSource;
        }
        #endregion

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}