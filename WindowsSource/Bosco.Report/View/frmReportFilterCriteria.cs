using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using Bosco.Report.SQL;

namespace Bosco.Report.View
{
    public partial class frmReportFilterCriteria : DevExpress.XtraEditors.XtraForm
    {

        #region Variable Decelaration
        string ReportId = string.Empty;
        string ReportCriteria = string.Empty;
        string[] Criteria;
        string PhysicalPath = string.Empty;
        private GridEditorCollection gridEditors;
        private ReportSetting.ReportSettingDataTable dtReportSettingSchema = new ReportSetting.ReportSettingDataTable();
        string reportXMLFile = "ReportSetting.xml";
        #endregion

        #region Constructor
        public frmReportFilterCriteria()
        {
            InitializeComponent();
            this.gridEditors = new GridEditorCollection();
        }
        #endregion

        #region Events
        private void frmReportFilter_Load(object sender, EventArgs e)
        {
            EnableTabs();
            gcReportCriteria.DataSource = gridEditors;
            FetchProject();
            FetchCostCentre();
            FetchLedgers();
            FetchBankAccounts();
        }

        private void gvReportCriteria_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == this.colCriteriaType)
            {
                GridEditorItem item = gvReportCriteria.GetRow(e.RowHandle) as GridEditorItem;
                if (item != null) e.RepositoryItem = item.RepositoryItem;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            gridEditors = gcReportCriteria.DataSource as GridEditorCollection;

            ReportProperty.Current.DateFrom = dteDateFrom.Text;
            ReportProperty.Current.DateTo = dteDateTo.Text;

            for (int i = 0; i < gridEditors.Count; i++)
            {

                switch (gridEditors[i].Criteria.Trim())
                {
                    case "AL":
                        ReportProperty.Current.IncludeAllLedger = gridEditors[i].Value == "False" ? 0 : 1;
                        break;
                    case "BL":
                        ReportProperty.Current.ShowByLedger = gridEditors[i].Value == "False" ? 0 : 1;
                        break;
                    case "BG":
                        ReportProperty.Current.ShowByLedgerGroup = gridEditors[i].Value == "False" ? 0 : 1;
                        break;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        private void SetReportFilterParameter()
        {
            //base.ReportFilterParameter.Clear();
            //base.AddReportFilterParameter("Name1", "Value1");
        }

        private void EnableTabs()
        {
            DataSet ds = new DataSet();

            ds.ReadXml(reportXMLFile);
            ReportId = "RPT-001";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string FetchReportId = ds.Tables[0].Rows[i]["ReportId"].ToString();
                if (FetchReportId == ReportId)
                {
                    ReportCriteria = ds.Tables[0].Rows[i]["ReportCriteria"].ToString();
                    break;
                }
            }

            Criteria = ReportCriteria.Split('ÿ');

            VisibleTabs(Criteria);
        }

        private void VisibleTabs(string[] ReportCriteria)
        {
            for (int i = 0; i < ReportCriteria.Length; i++)
            {
                switch (ReportCriteria[i].ToString())
                {
                    case "DF":
                        dteDateFrom.Visible = true;
                        break;
                    case "DT":
                        dteDateTo.Visible = true;
                        break;
                    case "DA":
                        dteDateFrom.Visible = true;
                        dteDateTo.Visible = false;
                        break;
                    case "AL":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "AL", "Attach All Ledger", false);
                        break;
                    case "BL":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "BL ", "By Ledger", false);
                        break;
                    case "BG":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "BG", "By Group", false);
                        break;
                    case "DB":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "DB", "Daily Balance", false);
                        break;
                    case "IK":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "IK", "Include In Kind", false);
                        break;
                    case "IJ":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "IJ", "Include Journal", false);
                        break;
                    case "GT":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "GT", "Group Total", false);
                        break;
                    case "AG":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "AG", "Attach Group", false);
                        break;
                    case "AC":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "AC", "Attach Cost Centre", false);
                        break;
                    case "MT":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "MT", "Monthwise Total", false);
                        break;
                    case "AD":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "AD", "Donor Address", false);
                        break;
                    case "AB":
                        this.gridEditors.Add(this.repositoryItemCheckEdit, "AB", "Attach A/c No", false);
                        break;
                    case "CD":
                        break;
                    case "PJ":
                        xtpProject.PageVisible = true;
                        lcgBankAccount.Visibility = LayoutVisibility.Never;
                        break;
                    case "BK":
                        lcgBankAccount.Visibility = LayoutVisibility.Always;
                        break;
                    case "LG":
                        xtpLedger.PageVisible = true;
                        break;
                    case "CC":
                        xtpCostCentre.PageVisible = true;
                        break;
                    case "NN":
                        xtpNarration.PageVisible = true;
                        break;
                }
            }
        }

        public class GridEditorItem
        {
            string fName;
            object fValue;
            string CriteriaValue;
            RepositoryItem fRepositoryItem;

            public GridEditorItem(RepositoryItem fRepositoryItem, string CriteriaName, string fName, object fValue)
            {
                this.fRepositoryItem = fRepositoryItem;
                CriteriaValue = CriteriaName;
                this.fName = fName;
                this.fValue = fValue;
            }
            public string Name { get { return this.fName; } }
            public string Criteria { get { return this.CriteriaValue; } }
            public object Value { get { return this.fValue; } set { this.fValue = value; } }
            public RepositoryItem RepositoryItem { get { return this.fRepositoryItem; } }
        }

        class GridEditorCollection : ArrayList
        {
            public GridEditorCollection()
            {
            }
            public new GridEditorItem this[int index] { get { return base[index] as GridEditorItem; } }
            public void Add(RepositoryItem fRepositoryItem, string Criteria, string fName, object fValue)
            {
                base.Add(new GridEditorItem(fRepositoryItem, Criteria, fName, fValue));
            }
        }


        private void FetchProject()
        {
            using (ReportBase reportBase = new ReportBase())
            {
                ResultArgs resultArgs = reportBase.FetchMasterDetails(ReportSQLCommand.MasterSQL.Project);
                if (resultArgs.Success)
                {
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        gcProject.DataSource = resultArgs.DataSource.Table;
                        gcProject.RefreshDataSource();
                    }
                }
            }
        }

        private void FetchCostCentre()
        {
            using (ReportBase reportBase = new ReportBase())
            {
                ResultArgs resultArgs = reportBase.FetchMasterDetails(ReportSQLCommand.MasterSQL.CostCentre);
                if (resultArgs.Success)
                {
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        gcCostCentre.DataSource = resultArgs.DataSource.Table;
                        gcCostCentre.RefreshDataSource();
                    }
                }
            }
        }

        private void FetchLedgers()
        {
            using (ReportBase reportBase = new ReportBase())
            {
                ResultArgs resultArgs = reportBase.FetchMasterDetails(ReportSQLCommand.MasterSQL.Ledger);
                if (resultArgs.Success)
                {
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        gcLedger.DataSource = resultArgs.DataSource.Table;
                        gcLedger.RefreshDataSource();
                    }
                }
            }
        }

        private void FetchBankAccounts()
        {
            using (ReportBase reportBase = new ReportBase())
            {
                ResultArgs resultArgs = reportBase.FetchMasterDetails(ReportSQLCommand.MasterSQL.BankAccount);
                if (resultArgs.Success)
                {
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        gcBank.DataSource = resultArgs.DataSource.Table;
                        gcBank.RefreshDataSource();
                    }
                }
            }
        }
        #endregion
    }
}