using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.Transaction;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using ACPP.Modules.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;

namespace ACPP.Modules.UIControls
{
    public partial class UcAccountMapping : DevExpress.XtraEditors.XtraUserControl
    {

        #region Variables
        public event EventHandler ProcessGridKey;
        public event EventHandler ApplicableDateButtonClick;
        ResultArgs resultArgs = null;
        CommonMember utilityMember = null;
        DataTable dtAllSelectedProjects = null;
        public string sViewTypeValue = string.Empty;
        const string AMOUNT_COL = "AMOUNT";
        const string SELECT_COL = "SELECT";
        bool showFilter = true;
        MessageRender msgRender = new MessageRender();
        #endregion

        #region Properties

        public DataTable GetProjectAmountMadeZero
        {
            get
            {
                DataTable dtAmountZeroLedgers = (gcMapProject.DataSource as DataTable).Clone();
                DataTable dtCheckedProjects = GetMappingDetails;
                if (dtAllSelectedProjects != null && dtCheckedProjects != null)
                {
                    foreach (DataRow drProject in dtAllSelectedProjects.Rows)
                    {
                        DataView dvFindprojects = new DataView(dtCheckedProjects);
                        dvFindprojects.RowFilter = String.Format("PROJECT_ID={0}", UtilityMember.NumberSet.ToInteger(drProject["PROJECT_ID"].ToString()));
                        if (dvFindprojects.ToTable().Rows.Count == 0)
                        {
                            dtAmountZeroLedgers.ImportRow(drProject);

                        }
                    }
                    if (dtAmountZeroLedgers != null && dtAmountZeroLedgers.Rows.Count > 0)
                    {
                        dtAmountZeroLedgers.Select().ToList<DataRow>().ForEach(r => { r["AMOUNT"] = 0; });
                    }
                }
                return dtAmountZeroLedgers;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable GetMappingDetails
        {
            get
            {
                DataTable dtMapping = null;
                if (gcMapProject.DataSource != null)
                {
                    dtMapping = gcMapProject.DataSource as DataTable;
                    DataView dvMapping = new DataView(dtMapping);
                    dvMapping.RowFilter = String.Format(SELECT_COL + "={0}", 1);
                    if (dvMapping != null)
                    {
                        dtMapping = dvMapping.ToTable();
                    }
                    else
                    {
                        dtMapping = dtMapping.Clone();
                    }
                    //var MappedProject = (from ledger in dtMapping.AsEnumerable()
                    //                     where (ledger.Field<Int64?>(SELECT_COL) == 1)
                    //                     select ledger);
                    //if (MappedProject.Count() > 0)
                    //    dtMapping = MappedProject.CopyToDataTable();
                    //else
                    //    dtMapping = dtMapping.Clone();
                }
                return (dtMapping);
            }
            set { gcMapProject.DataSource = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable GetLedgerMappingDetails
        {
            get
            {
                DataTable dtMapping = gcMapProject.DataSource as DataTable;
                DataView dvMapping = new DataView(dtMapping);
                //dvMapping.RowFilter = String.Format(SELECT_COL + "={0}", 1);
                dvMapping.RowFilter = " SELECT = 1 OR SELECT_TEMP_EDIT=1";
                if (dvMapping != null)
                {
                    dtMapping = dvMapping.ToTable();
                }
                else
                {
                    dtMapping = dtMapping.Clone();
                }
                return (dtMapping);
            }
            set { gcMapProject.DataSource = value; }
        }

        public bool GridClear
        {
            set
            {
                if (value)
                {
                    DataTable dtMapping = gcMapProject.DataSource as DataTable;
                    if (dtMapping.Columns.Contains(AMOUNT_COL))
                        dtMapping.Select().ToList<DataRow>().ForEach(r => { r[SELECT_COL] = 0; r[AMOUNT_COL] = 0.00; });
                    else
                        dtMapping.Select().ToList<DataRow>().ForEach(r => r[SELECT_COL] = 0);

                    CheckAllVisible = false;
                    gcMapProject.DataSource = dtMapping;
                }
            }
        }

        int id = 0;
        public int Id
        {
            set { id = value; }
            get { return id; }
        }

        int projectId = 0;
        public int ProjectId
        {
            set { projectId = value; }
            get { return projectId; }
        }

        public bool VisibleUserControl
        {
            set
            {
                if (value.Equals(true))
                {
                    // LayGroup.Visibility =  layoutShowFilter.Visibility = emtpSpaceFilter.Visibility = value.Equals(true) ? LayoutVisibility.Always : LayoutVisibility.Never;
                    LayGroup.Visibility = layoutShowFilter.Visibility = emtpSpaceFilter.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    LayGroup.Visibility = layoutShowFilter.Visibility = emtpSpaceFilter.Visibility = LayoutVisibility.Never;
                }
            }
        }

        public bool CheckAllVisible
        {
            set { chkSelectAll.Checked = value; }
        }

        MapForm formtype;
        public MapForm FormType
        {
            set { formtype = value; }
            get { return formtype; }
        }

        ledgerSubType LedgerSubType;
        public ledgerSubType FDLedgerSubType
        {
            get { return LedgerSubType; }
            set { LedgerSubType = value; }
        }

        private int FDAccId;
        public int FDAccountID
        {
            get { return FDAccId; }
            set { FDAccId = value; }
        }
        public bool OpBalanceVisible
        {
            set
            {
                gvColOPBalance.Visible = value;
            }
        }
        public bool TransModeVisible
        {
            set { gvColFlag.Visible = value; }
        }

        public bool SelectFixedWidth
        {
            set { gvColSelect.OptionsColumn.FixedWidth = value; }
        }
        public bool SetOpBalVisible
        {
            set { gvColSetOPBalance.Visible = value; }
        }
        public bool RefreshGrid { get; set; }

        public GridView gcUserControl;
        public GridView ucGridControl
        {
            get { return gvMapProject; }
        }
        public GridColumn colTransMode;
        public GridColumn ucTransMode
        {
            get { return gvColFlag; }
        }

        public GridColumn colProject;
        public GridColumn ucProject
        {
            get { return gvColProject; }
        }

        private bool showapplicableoption = false;
        public bool ShowApplicableOption
        {
            get { return showapplicableoption; }
            set
            {
                showapplicableoption = value;
                colProjectLedgerApplicableDate.Visible = value;}
        }
        
        private string activemappname = string.Empty;
        public string ActiveMappName
        {
            get { return activemappname; }
            set
            {
                activemappname = value;
            }
        }

        private Int32 activemappid = 0;
        public Int32 ActiveMappId
        {
            get { return activemappid; }
            set
            {
                activemappid = value;
            }
        }
        #endregion

        #region Constructor
        public UcAccountMapping()
        {
            InitializeComponent();
            RealColumnEditTransAmount();
        }
        #endregion

        #region Methods

        #region Map Ledgers

        #region General Ledgers
        private void FetchMappedLedgers()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.LedgerId = Id;
                resultArgs = mappingSystem.LoadProjectMappingGrid();
                if (resultArgs.DataSource.Table != null)
                    BindGridView(resultArgs.DataSource.Table);
            }
        }
        #endregion
        #endregion

        #region Map Cost Centre
        private void FetchMappedCostCenter()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                resultArgs = mappingSystem.LoadProjectCostCentreMappingGrid(id);
                if (resultArgs.DataSource.Table != null)
                    BindGridView(resultArgs.DataSource.Table);
            }
        }
        #endregion

        #region Map Donor
        private void FetchMappedDonor()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                resultArgs = mappingSystem.LoadProjectDonorMappingGrid(id);
                if (resultArgs.DataSource.Table != null)
                    BindGridView(resultArgs.DataSource.Table);
            }
        }
        #endregion

        #region Common methods
        private void BindGridView(DataTable dtProject)
        {
            dtProject.Columns.Add(SELECT_COL, typeof(Int32));
            foreach (DataRow dr in dtProject.Rows)
            {
                dr[SELECT_COL] = dr["SELECT_TEMP"];
            }
            DataView dvOrderBy = new DataView(dtProject);
            dvOrderBy.Sort = "SELECT DESC";
            // This is to check default projects for  Transaction Add
            if (ProjectId != 0)
            {
                foreach (DataRow Irow in dtProject.Rows)
                {
                    if (Irow["PROJECT_ID"].ToString() == ProjectId.ToString())
                    {
                        Irow[SELECT_COL] = 1;
                    }
                }
            }
            gcMapProject.DataSource = GetMappingDetails = dvOrderBy.ToTable();
            dtAllSelectedProjects = GetMappingDetails;
            dvOrderBy.RowFilter = "SELECT=0";
            if (dvOrderBy.ToTable().Rows.Count.Equals(0))
                CheckAllVisible = true;
        }

        private void LoadValues()
        {
            switch (FormType)
            {
                case MapForm.BankAccount:
                case MapForm.Ledger:
                    gvColSetOPBalance.Visible = false;
                    gvColOPBalance.Visible = true;
                    gvColFlag.Visible = true;
                    FetchMappedLedgers();
                    break;
                case MapForm.CostCentre:
                    gvColOPBalance.Visible = true;
                    gvColSetOPBalance.Visible = false;
                    gvColFlag.Visible = true;
                    FetchMappedCostCenter();
                    break;
                case MapForm.Donor:
                    gvColOPBalance.Visible = false;
                    gvColFlag.Visible = false;
                    chkShowFilter.Visible = true;
                    gvColSetOPBalance.Visible = false;
                    FetchMappedDonor();
                    break;
                case MapForm.FDLedger:
                    gvColOPBalance.OptionsColumn.AllowEdit = false;
                    FetchMappedLedgers();
                    break;
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
            //XtraMessageBox.Show(Message, "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XtraMessageBox.Show(Message,MessageRender.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RealColumnEditTransAmount()
        {
            gvColOPBalance.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvMapProject.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvMapProject.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == gvColOPBalance)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvMapProject.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void SelectAllProjects()
        {
            try
            {
                DataTable dtAllLedger = (DataTable)gcMapProject.DataSource;
                if (chkSelectAll.Checked)
                {
                    if (dtAllLedger != null && dtAllLedger.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAllLedger.Rows)
                        {
                            dr[SELECT_COL] = chkSelectAll.Checked;
                        }
                        gcMapProject.DataSource = GetMappingDetails = dtAllLedger;
                    }
                }
                else
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        if (dtAllLedger != null)
                        {
                            foreach (DataRow dr in dtAllLedger.Rows)
                            {
                                double Amount = 0;
                                if (!FormType.Equals(MapForm.Ledger))
                                {
                                    if (dtAllLedger.Columns.Contains("AMOUNT"))
                                    {
                                        Amount = UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                                    }

                                    if (Amount > 0)
                                    {
                                        dr[SELECT_COL] = 1;
                                    }
                                    else
                                    {
                                        using (MappingSystem mappingsystem = new MappingSystem())
                                        {
                                            mappingsystem.ProjectId = UtilityMember.NumberSet.ToInteger(dr["PROJECT_ID"].ToString()); ;
                                            if (FormType.Equals(MapForm.CostCentre))
                                            {
                                                mappingsystem.CostCenterIDs = Id.ToString();
                                                resultArgs = mappingsystem.FetchCostCentreTransaction();
                                            }
                                            if (FormType.Equals(MapForm.Donor))
                                            {
                                                if (id != 0)
                                                {
                                                    resultArgs = voucherTransaction.MadeTransactionDonor(id.ToString());
                                                }
                                            }
                                        }
                                        
                                        if (resultArgs != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count == 0)// if there is no transaction
                                        {
                                            dr[SELECT_COL] = 0;
                                        }
                                        else
                                        {
                                            dr[SELECT_COL] = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    if (dtAllLedger.Columns.Contains("AMOUNT"))
                                    {
                                        Amount = UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                                    }

                                    if (Amount > 0)
                                    {
                                        dr[SELECT_COL] = 1;
                                    }
                                    else
                                    {
                                        voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(dr["PROJECT_ID"].ToString());
                                        string LedgerId = Id.ToString();
                                        resultArgs = voucherTransaction.MadeTransaction(LedgerId);
                                        if (resultArgs != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count == 0)// if there is no transaction
                                        {
                                            dr[SELECT_COL] = 0;
                                        }
                                        else
                                        {
                                            dr[SELECT_COL] = 1;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void UnmapProjects()
        {
            if (gvMapProject.FocusedColumn == gvColSelect)
            {
                DataTable dtProject = (DataTable)gcMapProject.DataSource;
                if (ledgerSubType.FD == FDLedgerSubType)
                {
                    if (FDAccountID != 0)
                    {
                        if (dtProject != null && dtProject.Rows.Count > 0)
                        {
                            int projectId = gvMapProject.GetFocusedRowCellValue(gvColProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProjectId).ToString()) : 0;
                            if (!IsFDVouchersExisting(projectId, true))
                            {
                                int select = gvMapProject.GetFocusedRowCellValue(gvColSelect) != DBNull.Value ? UtilityMember.NumberSet.ToInteger(gvMapProject.GetFocusedRowCellValue(gvColSelect).ToString()) : 0;
                                gvMapProject.SetFocusedRowCellValue(gvColSelect, 1 - select);
                            }

                            /*int projectId = gvMapProject.GetFocusedRowCellValue(gvColProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProjectId).ToString()) : 0;
                            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                            {
                                fdAccountSystem.ProjectId = projectId;
                                fdAccountSystem.LedgerId = FDAccountID;
                                string fdTransType = fdAccountSystem.FetchProjectFDLedgerId();
                                if (!string.IsNullOrEmpty(fdTransType))
                                {
                                    // e.can = true;
                                    if (fdTransType == FDTypes.OP.ToString())
                                    {
                                        //ShowMessageBox("Opening Balance is set, can not unmap the ledger");
                                        ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_OPENNNG_BAL_SET_CANNOT_UNMAP_LEDGER));
                                        //XtraMessageBox.Show("Cannot unmap, opening Balance is set for this ledger.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        //ShowMessageBox("Investment is made, can not unmap the ledger");
                                        ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_INVESTMENT_MADE_CANNOT_UNMAP_LEDGER));
                                        //XtraMessageBox.Show("Cannot unmap, Investment is made for this ledger", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    int select = gvMapProject.GetFocusedRowCellValue(gvColSelect) != DBNull.Value ? UtilityMember.NumberSet.ToInteger(gvMapProject.GetFocusedRowCellValue(gvColSelect).ToString()) : 0;
                                    gvMapProject.SetFocusedRowCellValue(gvColSelect, 1 - select);
                                }
                            }*/

                        }

                    }
                    else
                    {
                        int select = gvMapProject.GetFocusedRowCellValue(gvColSelect) != DBNull.Value ? UtilityMember.NumberSet.ToInteger(gvMapProject.GetFocusedRowCellValue(gvColSelect).ToString()) : 0;
                        gvMapProject.SetFocusedRowCellValue(gvColSelect, 1 - select);
                    }
                }
                else
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        int ProjectId = gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProjectId.FieldName) != null ? UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProjectId.FieldName).ToString()) : 0;
                        voucherTransaction.ProjectId = ProjectId;
                        int Selected = gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColSelect.FieldName) != null ? UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColSelect.FieldName).ToString()) : 0;
                        Double Amount = gvColOPBalance.Visible == true ? gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColOPBalance.FieldName) != null ? UtilityMember.NumberSet.ToDouble(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColOPBalance.FieldName).ToString()) : 0.00 : 0.00;
                        //resultArgs = voucherTransaction.MadeTransaction(Id.ToString());
                        using (MappingSystem mappingsystem = new MappingSystem())
                        {
                            if (FormType.Equals(MapForm.CostCentre))
                            {
                                mappingsystem.CostCenterIDs = id.ToString();
                                mappingsystem.ProjectId = ProjectId;
                                resultArgs = mappingsystem.FetchCostCentreTransaction();
                            }
                            if (FormType.Equals(MapForm.Donor))
                            {
                                if (id != 0)
                                {
                                    resultArgs = voucherTransaction.MadeTransactionDonor(id.ToString());
                                }
                            }
                            else
                            {
                                if (FormType.Equals(MapForm.Ledger) || FormType.Equals(MapForm.BankAccount))
                                {
                                    resultArgs = voucherTransaction.MadeTransaction(Id.ToString());
                                }
                            }
                        }
                        //if row count is zero than no transaction is made
                        if (id != 0)
                        {
                            if (resultArgs.DataSource.Table.Rows.Count == 0)
                            {
                                if (Amount > 0 && Selected == 1)
                                {
                                    //ShowMessageBox("Avail zero balance to unmap the ledger.");
                                    ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_AVAIL_ZERO_BAL_UNMAP_LEDGER));
                                    //XtraMessageBox.Show("Cannot unmap make the amount zero and try again.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    int select = gvMapProject.GetFocusedRowCellValue(gvColSelect) != null ? UtilityMember.NumberSet.ToInteger(gvMapProject.GetFocusedRowCellValue(gvColSelect).ToString()) : 0;
                                    gvMapProject.SetFocusedRowCellValue(gvColSelect, 1 - select);
                                }
                            }
                            else
                            {
                                gvMapProject.SetFocusedRowCellValue(gvColSelect, 1);
                                //ShowMessageBox("Transaction is made, can not unmap the ledger");
                                ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_TRANS_MADE_LEDGER_CANNOT_UNMAP_LEDGER));
                                //XtraMessageBox.Show("Transaction is made,It can not be unmapped", "AcME ERP");
                            }
                        }
                    }
                }
            }
        }

        private bool IsFDVouchersExisting(int projectId, bool showmessage = false)
        {
            bool rtn = false;

             DataTable dtProject = (DataTable)gcMapProject.DataSource;
             if (ledgerSubType.FD == FDLedgerSubType)
             {
                 if (FDAccountID != 0)
                 {
                     if (dtProject != null && dtProject.Rows.Count > 0)
                     {
                         using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                         {
                             fdAccountSystem.ProjectId = projectId;
                             fdAccountSystem.LedgerId = FDAccountID;
                             string fdTransType = fdAccountSystem.FetchProjectFDLedgerId();
                             if (!string.IsNullOrEmpty(fdTransType))
                             {
                                 rtn = true;
                                 if (showmessage)
                                 {
                                     if (fdTransType == FDTypes.OP.ToString())
                                     {
                                         ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_OPENNNG_BAL_SET_CANNOT_UNMAP_LEDGER));
                                     }
                                     else
                                     {
                                         ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_INVESTMENT_MADE_CANNOT_UNMAP_LEDGER));
                                     }
                                 }
                             }
                         }
                     }
                 }
             }

            return rtn;
        }

        private void SetUIChanges()
        {
            if (FormType.Equals(MapForm.BankAccount))
            {
                colLegalEntity.Visible = true;
                colLegalEntity.VisibleIndex = 2;
            }
            else if (FormType.Equals(MapForm.Ledger))
            {
                colSocietyName.Visible = true;
                colSocietyName.VisibleIndex = 2;
                gvColOPBalance.VisibleIndex = 3;
                gvColFlag.VisibleIndex = 4;
            }
            else
            {
                colLegalEntity.Visible = false;
            }
        }
        #endregion

        #endregion

        #region Events
        private void UcAccountMapping_Load(object sender, EventArgs e)
        {
            //On 12/09/2022, to skip this methods in design mode, to avoid unncessary design issue
            if (!this.DesignMode)
            {
                LoadValues();
                SetUIChanges();
            }

            colProjectLedgerApplicableDate.Visible = showapplicableoption;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMapProject.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            gvMapProject.Focus();
            if (chkShowFilter.Checked)
            {
                gvMapProject.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvMapProject.FocusedColumn = gvMapProject.VisibleColumns[1];
                gvMapProject.ShowEditor();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ledgerSubType.FD == FDLedgerSubType && FDAccountID != 0)
                {
                    using (MappingSystem mappingSystem = new MappingSystem())
                    {
                        mappingSystem.LedgerId = FDAccountID;
                        DataTable dtAllProject = gcMapProject.DataSource as DataTable;
                        if (dtAllProject != null && dtAllProject.Rows.Count != 0)
                        {
                            for (int i = 0; i < dtAllProject.Rows.Count; i++)
                            {
                                int projectId = this.UtilityMember.NumberSet.ToInteger(dtAllProject.Rows[i][gvColProjectId.FieldName].ToString()); ;
                                bool fdexisting = IsFDVouchersExisting(projectId);

                                if (UtilityMember.NumberSet.ToDouble(dtAllProject.Rows[i]["AMOUNT"].ToString()) <= 0 && !fdexisting)
                                {
                                    dtAllProject.Rows[i][SELECT_COL] = chkSelectAll.Checked;
                                }
                            }
                        }
                    }
                }
                else
                {
                    SelectAllProjects();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
        }

        private void gvMapProject_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == SELECT_COL)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvMapProject_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.RowCount != 0 && view.FocusedColumn == gvColSelect)
            {
                if (!FormType.Equals(MapForm.Donor))
                {
                    int Selected = UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColSelect.FieldName).ToString());
                    int ProjectId = UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProjectId.FieldName).ToString());
                    Double Amount = UtilityMember.NumberSet.ToDouble(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColOPBalance.FieldName).ToString());

                    if (ledgerSubType.FD == FDLedgerSubType)
                    {
                        if (FDAccountID != 0)
                        {
                            int projectId = gvMapProject.GetFocusedRowCellValue(gvColProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProjectId).ToString()) : 0;
                            if (!IsFDVouchersExisting(projectId, true))
                            {
                                int select = gvMapProject.GetFocusedRowCellValue(gvColSelect) != DBNull.Value ? UtilityMember.NumberSet.ToInteger(gvMapProject.GetFocusedRowCellValue(gvColSelect).ToString()) : 0;
                                gvMapProject.SetFocusedRowCellValue(gvColSelect, 1 - select);
                            }

                            /*int projectId = view.GetRowCellValue(view.FocusedRowHandle, gvColProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(view.GetRowCellValue(view.FocusedRowHandle, gvColProjectId).ToString()) : 0;
                            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                            {
                                fdAccountSystem.ProjectId = projectId;
                                fdAccountSystem.LedgerId = FDAccountID;
                                string fdTransType = fdAccountSystem.FetchProjectFDLedgerId();
                                if (!string.IsNullOrEmpty(fdTransType))
                                {
                                    e.Cancel = true;
                                    if (fdTransType == FDTypes.OP.ToString())
                                        //ShowMessageBox("Cannot unmap, opening Balance is set for this ledger.");
                                        this.ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_OPENNING_BAL_SET_FOR_LEDGER));
                                    //XtraMessageBox.Show("Cannot unmap, opening Balance is set for this ledger.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        //ShowMessageBox("Cannot unmap, Investment is made for this ledger");
                                        this.ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_CANNOT_UNMAP_INVERST_MADE_LEDGER));
                                    // XtraMessageBox.Show("Cannot unmap, Investment is made for this ledger", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }*/
                        }
                    }
                    else
                    {
                        if (!gvMapProject.FocusedColumn.Name.Equals("gvColOPBalance"))
                        {
                            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                            {
                                voucherTransaction.ProjectId = ProjectId;
                                //resultArgs = voucherTransaction.MadeTransaction(Id.ToString());
                                using (MappingSystem mappingsystem = new MappingSystem())
                                {
                                    if (FormType.Equals(MapForm.CostCentre))
                                    {
                                        mappingsystem.CostCenterIDs = id.ToString();
                                        mappingsystem.ProjectId = ProjectId;
                                        resultArgs = mappingsystem.FetchCostCentreTransaction();
                                    }
                                    if (FormType.Equals(MapForm.Donor))
                                    {
                                        resultArgs = voucherTransaction.MadeTransactionDonor(id.ToString());
                                    }
                                    else
                                    {
                                        if (FormType.Equals(MapForm.Ledger) || FormType.Equals(MapForm.BankAccount))
                                        {
                                            resultArgs = voucherTransaction.MadeTransaction(Id.ToString());
                                        }
                                    }
                                }
                                //if row count is zero than no transaction is made
                                if (resultArgs.DataSource.Table.Rows.Count == 0)
                                {
                                    if (Amount > 0 && Selected == 1)
                                    {
                                        e.Cancel = true;
                                        //ShowMessageBox("Cannot unmap make the amount zero and try again.");
                                        this.ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_CANNOT_UNMAP_MAKE_AMOUNT_ZERO));
                                        //XtraMessageBox.Show("Cannot unmap make the amount zero and try again.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    //else
                                    //    {
                                    //    int select = gvMapProject.GetFocusedRowCellValue(gvColSelect) != DBNull.Value ? UtilityMember.NumberSet.ToInteger(gvMapProject.GetFocusedRowCellValue(gvColSelect).ToString()) : 0;
                                    //    if (select > 0)
                                    //            gvMapProject.SetFocusedRowCellValue(gvColSelect, 1 - select);
                                    //}

                                }
                                else
                                    //ShowMessageBox("Transaction is made,It can not be unmapped");
                                    this.ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.Mapping.ACC_MAP_TRANS_MADE_CANNOT_UNMAP));
                                //XtraMessageBox.Show("Transaction is made,It can not be unmapped");
                            }
                        }
                    }
                }
            }
        }

        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvMapProject.PostEditor();
            gvMapProject.UpdateCurrentRow();
            if (gvMapProject.ActiveEditor == null)
            {
                gvMapProject.ShowEditor();
            }
            int Selected = UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColSelect.FieldName).ToString());
            double Amount = UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColOPBalance.FieldName).ToString());
            if (Selected == 0 && Amount > 0)
                gvMapProject.SetFocusedRowCellValue(gvColSelect, 1);
        }

        private void gvMapProject_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (ledgerSubType.FD == LedgerSubType && FDAccountID != 0)
                {
                    if (view.RowCount != 0)
                    {
                        using (MappingSystem mappingSystem = new MappingSystem())
                        {
                            mappingSystem.LedgerId = FDAccountID;
                            if (resultArgs!=null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                int projectId = view.GetRowCellDisplayText(e.RowHandle, view.Columns[mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName]) != null ? this.UtilityMember.NumberSet.ToInteger(view.GetRowCellDisplayText(e.RowHandle, view.Columns[mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName])) : 0;
                                foreach (DataRow drProject in resultArgs.DataSource.Table.Rows)
                                {
                                    int ProId = drProject[mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(drProject[mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                                    if (projectId == ProId)
                                    {
                                        //e.Appearance.BackColor = Color.LightGray;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void gvMapProject_RowClick(object sender, RowClickEventArgs e)
        {
            UnmapProjects();
        }

        private void gcMapProject_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (ProcessGridKey != null)
            {
                if (e.KeyData == Keys.Tab || e.KeyData == Keys.Enter)
                {
                    if (gvMapProject.IsLastRow)
                    {
                        if (gvMapProject.FocusedColumn == gvColOPBalance)
                        {
                            ProcessGridKey(this, e);
                            gvMapProject.MoveFirst();
                            gvMapProject.MovePrev();
                            gvMapProject.FocusedColumn = gvColOPBalance;
                        }
                        else if (sViewTypeValue == ViewDetails.Donor.ToString())
                        {
                            ProcessGridKey(this, e);
                        }
                    }
                }
                if (showFilter)
                {
                    if (e.KeyData == (Keys.Control | Keys.W))
                    {
                        chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
                        showFilter = false;
                    }
                }
                else
                {
                    showFilter = true;
                }
            }
        }

        private void chkSelect_Click(object sender, EventArgs e)
        {
            // this.chkSelect.CheckedChanged -= new System.EventHandler(this.chkSelect_CheckedChanged);
            UnmapProjects();
            // this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
        }

        private void btnSetOPBalance_Click(object sender, EventArgs e)
        {
            frmFDAccount frmFDAccount = new frmFDAccount();
            frmFDAccount.ShowDialog();
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtProject = (DataTable)gcMapProject.DataSource;
            this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
            chkSelectAll.Checked = false;
            //this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
            //chkSelectAll.Checked = false;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
        }
        #endregion

        private void rbnProjectLedgerApplicableDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (ApplicableDateButtonClick != null)
            {
                activemappid = gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProjectId) == null ? 0 :
                        UtilityMember.NumberSet.ToInteger(gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProjectId).ToString());
                activemappname = gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProject) == null ? string.Empty :
                    gvMapProject.GetRowCellValue(gvMapProject.FocusedRowHandle, gvColProject).ToString();

                ApplicableDateButtonClick(this, e);
            }

        }
    }
}
