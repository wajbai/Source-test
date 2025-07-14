using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.TallyMigration;
using System.Data.OleDb;

namespace ACPP.Modules.Tally_To_AcMEERP
{
    public partial class frmImportMasters : frmBaseAdd
    {
        #region Common Decelaration
        EventHandler EventArg;
        ResultArgs resultArgs = null;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private string SheetName = string.Empty;
        private const string TABLECOLUMNNAME = "TABLE_NAME";
        private DataTable dtMasterDetails { get; set; }
        private DataTable dtLedgerGroup { get; set; }
        private DataTable dtLedger { get; set; }
        private DataTable dtCostCentre { get; set; }
        DataTable dtVoucherTransaction { get; set; }
        private bool isTrue = true;
        #endregion

        #region Constructors
        public frmImportMasters()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmImportMasters_Load(object sender, EventArgs e)
        {
            GeneralLogger.TallyMigration.ClearErrorLog();
        }

        private void btnLedgerGroup_Click(object sender, EventArgs e)
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Started Ledger Group import Successfully ");
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dtLedgerGroup = ConvertExcelToDataTable(openFileDialog.FileName);
                    txtLedgerGroup.Text = openFileDialog.FileName;
                    if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count != 0)
                    {
                        gcLedgerGroup.DataSource = dtLedgerGroup;
                    }
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Ended Ledger Group import Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Started Ledger import Successfully ");
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dtLedger = ConvertExcelToDataTable(openFileDialog.FileName);
                    txtLedgerPath.Text = openFileDialog.FileName;
                    if (dtLedger != null && dtLedger.Rows.Count != 0)
                    {
                        dtLedger.Columns.Add(new DataColumn("OPBalance", typeof(double)));
                        dtLedger.Columns.Add(new DataColumn("TransMode", typeof(string)));
                        dtLedger.Select().ToList<DataRow>().ForEach(r => r["OPBalance"] = 0.00);
                        dtLedger.Select().ToList<DataRow>().ForEach(r => r["TransMode"] = "DR");
                        gcLedger.DataSource = dtLedger;
                    }
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Ended Ledger import Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }

        }

        private void btnCostCentre_Click(object sender, EventArgs e)
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Started Cost Center import Successfully ");
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dtCostCentre = ConvertExcelToDataTable(openFileDialog.FileName);
                    txtCostCentre.Text = openFileDialog.FileName;
                    if (dtCostCentre != null && dtCostCentre.Rows.Count != 0)
                    {
                        dtCostCentre.Columns.Add(new DataColumn("OPBalance", typeof(double)));
                        dtCostCentre.Columns.Add(new DataColumn("TransMode", typeof(string)));
                        dtCostCentre.Select().ToList<DataRow>().ForEach(r => r["OPBalance"] = 0.00);
                        dtCostCentre.Select().ToList<DataRow>().ForEach(r => r["TransMode"] = "DR");
                        gcCostCentre.DataSource = dtCostCentre;
                    }
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Ended Cost Center import Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }

        }

        private void btnVouchers_Click(object sender, EventArgs e)
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Started Voucher Transaction import Successfully ");
                openFileDialog.Filter = "Excel Files (.xlsx)|*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtVoucherPath.Text = Path.GetFullPath(openFileDialog.FileName);
                    dtVoucherTransaction = ConvertExcelToDataTable(Path.GetFullPath(openFileDialog.FileName));
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Ended Voucher Transaction import Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
        }

        private void txtProjectCode_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtProjectCode);
        }

        private void txtProjectCategory_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtProjectCategory);
        }

        private void txtProjectName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtProjectName);
        }

        private void dteStartDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(dteStartDate);
        }

        private void mtxtNotes_Leave(object sender, EventArgs e)
        {
            xtbImportMasters.SelectedTabPageIndex = 0;
        }

        private void btnMasters_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateProjectDetails())
                {
                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Started Tally Master and voucher import Successfully ");
                    EnableProjectDetails(false);
                    if (gcLedgerGroup.DefaultView.RowCount != 0 || gcLedger.DefaultView.RowCount != 0 || gcCostCentre.DefaultView.RowCount != 0)
                    {
                        using (ImportMasterDetailSystem importSystem = new ImportMasterDetailSystem())
                        {
                            importSystem.dtProject = ConstructTable();
                            importSystem.dtLedgerGroup = gcLedgerGroup.DataSource as DataTable;
                            importSystem.dtLedger = gcLedger.DataSource as DataTable;
                            importSystem.dtCostCentre = gcCostCentre.DataSource as DataTable;
                            importSystem.dtVoucherDetails = dtVoucherTransaction;
                            importSystem.UpdateHeld += new EventHandler(OnUpdateHeld);
                            resultArgs = importSystem.ImportMasters();
                            if (resultArgs.Success)
                            {
                                XtraMessageBox.Show("Successfully imported Tally Data to AcME ERP.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                if (!importSystem.isProjectSuccess)
                                {
                                    XtraMessageBox.Show("Project Code/Project Name is already exist.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    EnableProjectDetails(true);
                                }
                                else
                                {
                                    XtraMessageBox.Show("There is a problem in import Tally ERP to AcME ERP", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Select any of the master details to import.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Ended Tally Master and voucher import Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
        }

        private void gcLedgerGroup_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                if (gvLedgerGroup.IsLastRow)
                {
                    if (gvLedgerGroup.FocusedColumn == colLedgerGroup)
                    {
                        gvLedgerGroup.MovePrev();
                        btnMasters.Focus();
                    }
                }
            }
        }

        private void gcLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                if (gvLedger.IsLastRow)
                {
                    if (gvLedger.FocusedColumn == colTransMode)
                    {
                        gvLedger.MovePrev();
                        btnMasters.Focus();
                    }
                }
            }
        }

        private void gcCostCentre_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                if (gvCostCentre.IsLastRow)
                {
                    if (gvCostCentre.FocusedColumn == colCostCentreTransMode)
                    {
                        gvCostCentre.MovePrev();
                        btnMasters.Focus();
                    }
                }
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            using (ImportMasterDetailSystem importSystem = new ImportMasterDetailSystem())
            {
                gcVouchers.Visible = true;
                gcVouchers.DataSource = importSystem.dtErrorTransaction;
            }
        }
        #endregion

        #region Methods
        private DataTable ConvertExcelToDataTable(string FilePath)
        {
            DataTable dtMasters = new DataTable();
            try
            {
                using (ImportMasterDetailSystem importSystem = new ImportMasterDetailSystem())
                {
                    importSystem.FilePath = Path.GetFullPath(FilePath);
                    OleDbConnection conn = importSystem.ConfigurationHandler();
                    dtMasters = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (dtMasters != null)
                    {
                        foreach (DataRow dr in dtMasters.Rows)
                        {
                            SheetName = dr[TABLECOLUMNNAME] != DBNull.Value ? dr[TABLECOLUMNNAME].ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(SheetName))
                            {
                                dtMasterDetails = importSystem.ExcelToDataTable("SELECT * FROM [" + SheetName + "] ");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dtMasterDetails;
        }

        private bool ValidateProjectDetails()
        {
            bool isProject = true;
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Started Validating user input Successfully ");
                if (string.IsNullOrEmpty(txtProjectCode.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_CODE_EMPTY));
                    txtProjectCode.Focus();
                    isProject = false;
                }
                else if (string.IsNullOrEmpty(txtProjectCategory.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_CATEGORY_EMPTY));
                    txtProjectCategory.Focus();
                    isProject = false;
                }
                else if (string.IsNullOrEmpty(txtProjectName.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_NAME_EMPTY));
                    txtProjectName.Focus();
                    isProject = false;
                }
                else if (string.IsNullOrEmpty(dteStartDate.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_START_DATE_EMPTY));
                    dteStartDate.Focus();
                    isProject = false;
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Validating Completed Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return isProject;
        }

        private void EnableProjectDetails(bool isProject)
        {
            txtProjectCode.Enabled = txtProjectCategory.Enabled = txtProjectName.Enabled = dteClosedDate.Enabled =
            dteStartDate.Enabled = cboDivision.Enabled = mtxtNotes.Enabled = isProject;
        }

        private DataTable ConstructTable()
        {
            DataTable dtProject = new DataTable();
            try
            {
                dtProject.Columns.AddRange(new DataColumn[]{
                new DataColumn("PROJECT_CODE",typeof(string)),new DataColumn("PROJECT_CATOGORY_NAME",typeof(string)), new DataColumn("PROJECT",typeof(string)),new DataColumn("DIVISION_ID",typeof(int)),
                new DataColumn("ACCOUNT_DATE",typeof(DateTime)),new DataColumn("DATE_STARTED",typeof(DateTime)),
                new DataColumn("DATE_CLOSED",typeof(DateTime)),new DataColumn("DESCRIPTION",typeof(string)),
                new DataColumn("NOTES",typeof(string)),new DataColumn("CUSTOMERID",typeof(int))});

                dtProject.Rows.Add(txtProjectCode.Text, txtProjectCategory.Text, txtProjectName.Text, cboDivision.SelectedIndex == 0 ? 1 : 2, null,
                this.UtilityMember.DateSet.ToDate(dteStartDate.DateTime.ToShortDateString()),
                dteClosedDate.DateTime != DateTime.MinValue ? this.UtilityMember.DateSet.ToDate(dteClosedDate.DateTime.ToShortDateString()) : null,
                mtxtNotes.Text, string.Empty, 0);

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dtProject;
        }
        #endregion
    }
}