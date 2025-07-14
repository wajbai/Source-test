/*  Class Name      : frmPurposeAdd
 *  Purpose         : To Save FC _Purpose Details
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Xml;
namespace ACPP.Modules.Master
{
    public partial class frmProjectLedgerApplicableDate : frmFinanceBaseAdd
    {
        #region EventsDeclaration
        public event EventHandler UpdataHeld;
        #endregion

        #region VariableDeclaration
        private MapForm TypeofMap = MapForm.Ledger;
        private int LedgerId = 0;
        private int ProjectId = 0;
        private string ProjectName = string.Empty;
        private DataTable AllLedgerApplicaiton = new DataTable();
        private DataTable LedgerApplicaiton = new DataTable();
        #endregion

        #region Constructor
        public frmProjectLedgerApplicableDate(MapForm maptype, int ledgerid, int projectid, string projectname,  string ledgername , 
            DataTable allledgerapplicaiton, string deOpenedDate, string deClosedDate )
        {
            InitializeComponent();
            TypeofMap = maptype;
            LedgerId = ledgerid;
            ProjectId = projectid;
            lblLedgerProject.Text = projectname;
            lblLedgerName.Text = ledgername;
            lblOpenedOn.Text = lblLedgerClosed.Text = string.Empty;
            if (UtilityMember.DateSet.ToDate(deOpenedDate,false)!=DateTime.MinValue)
                lblOpenedOn.Text = UtilityMember.DateSet.ToDate(deOpenedDate, false).ToShortDateString();

            if (UtilityMember.DateSet.ToDate(deClosedDate, false) != DateTime.MinValue)
                lblLedgerClosed.Text = UtilityMember.DateSet.ToDate(deClosedDate, false).ToShortDateString();

            AllLedgerApplicaiton = LedgerApplicaiton  = allledgerapplicaiton;
                        
            lcOpenedOnCaption.Visibility = lcOpenedOn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            /*if (groupid == (Int32)FixedLedgerGroup.BankAccounts)
            {
                lcOpenedOnCaption.Visibility = lcOpenedOn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }*/

            using (LedgerSystem lsystem = new LedgerSystem())
            {
                if (LedgerApplicaiton != null)
                {
                    if (TypeofMap == MapForm.Project)
                        LedgerApplicaiton.DefaultView.RowFilter = lsystem.AppSchema.Project.PROJECT_IDColumn.ColumnName + " =" + projectid;
                    else
                        LedgerApplicaiton.DefaultView.RowFilter = lsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " =" + ledgerid;
                                        
                    LedgerApplicaiton = LedgerApplicaiton.DefaultView.ToTable();
                }
            }
        }
        
        #endregion

        #region Events
        /// <summary>
        /// To load the Title and fetch assigning Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPurposeAdd_Load(object sender, EventArgs e)
        {
            BindApplicableDateRange();
        }

        /// <summary>
        /// To Save the Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gcProjectLedgerApplicableDate.DataSource != null && ValidateLedgerApplicable())
                {
                    DataTable dtledgerapplicable = gcProjectLedgerApplicableDate.DataSource as DataTable;

                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        if (TypeofMap == MapForm.Project)
                        {
                            AllLedgerApplicaiton.DefaultView.RowFilter = ledgerSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName + " <> " + ProjectId;
                            dtledgerapplicable.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName + " IS NOT NULL "; ;
                        }
                        else
                        {
                            AllLedgerApplicaiton.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " <> " + LedgerId + " AND " +
                                                        ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName + " IS NOT NULL ";
                            dtledgerapplicable.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName + " IS NOT NULL "; ;
                        }

                        AllLedgerApplicaiton = AllLedgerApplicaiton.DefaultView.ToTable();
                        dtledgerapplicable = dtledgerapplicable.DefaultView.ToTable();

                        foreach (DataRow dr in dtledgerapplicable.Rows)
                        {
                            DateTime deApplicableFrom = UtilityMember.DateSet.ToDate(dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName].ToString(), false);
                            DateTime deApplicableTo = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName].ToString()))
                            {
                                deApplicableTo = UtilityMember.DateSet.ToDate(dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName].ToString(), false);
                            }

                            dr.BeginEdit();
                            dr[ledgerSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName] = ProjectId;
                            dr[ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] = LedgerId;
                            dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName] = deApplicableFrom;
                            if (deApplicableTo == DateTime.MinValue)
                                dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName] = DBNull.Value;
                            else
                                dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName] = deApplicableTo;

                            dr.EndEdit();
                        }
                        dtledgerapplicable.AcceptChanges();
                        AllLedgerApplicaiton.Merge(dtledgerapplicable);
                    }

                    this.ReturnValue = AllLedgerApplicaiton;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void gcProjectLedgerApplicableDate_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && gvProjectLedgerApplicableDate.FocusedColumn == colLedgerApplicableTo)
            {
                if (gvProjectLedgerApplicableDate.IsLastRow)
                {
                    btnOk.Select();
                    btnOk.Focus();
                }
            }
        }

        private void rbnLedgerApplicableDelete_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvProjectLedgerApplicableDate.DeleteRow(gvProjectLedgerApplicableDate.FocusedRowHandle);
                if (gvProjectLedgerApplicableDate.RowCount == 0)
                {
                    //gvPurposeCC.AddNewRow();
                    BindApplicableDateRange();
                }
            }
        }

        /// <summary>
        /// To Close the Purpose Add form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        /// <summary>
        /// To Validate the Ledger Application details Details
        /// </summary>
        /// <returns></returns>
        public bool ValidateLedgerApplicable()
        {
            bool isValue = true;
            Int32 row = 0;
            DateTime deOpenedDate = AppSetting.FirstFYDateFrom;
            DateTime deClosedDate = DateTime.MinValue;

            if (!string.IsNullOrEmpty(lblOpenedOn.Text))
                deOpenedDate =  UtilityMember.DateSet.ToDate(lblOpenedOn.Text, false);

            if (!string.IsNullOrEmpty(lblLedgerClosed.Text))
                deClosedDate = UtilityMember.DateSet.ToDate(lblLedgerClosed.Text, false);

            DataTable dtledgerapplicable = gcProjectLedgerApplicableDate.DataSource as DataTable;
            dtledgerapplicable = dtledgerapplicable.DefaultView.ToTable();

            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                if (TypeofMap == MapForm.Project)
                {
                    dtledgerapplicable.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName + " IS NOT NULL "; ;
                }
                else
                {
                    dtledgerapplicable.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName + " IS NOT NULL "; ;
                }
                dtledgerapplicable = dtledgerapplicable.DefaultView.ToTable();

                foreach (DataRow dr in dtledgerapplicable.Rows)
                {
                    DateTime deApplicableFrom = UtilityMember.DateSet.ToDate(dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName].ToString(), false);
                    DateTime deApplicableTo = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName].ToString()))
                    {
                        deApplicableTo = UtilityMember.DateSet.ToDate(dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName].ToString(), false);
                    }

                    if (deOpenedDate != DateTime.MinValue &&
                         (deApplicableFrom < deOpenedDate) || (deApplicableTo != DateTime.MinValue && deApplicableTo < deOpenedDate))
                    {
                        row = dtledgerapplicable.Rows.IndexOf(dr);
                        this.ShowMessageBoxWarning("Ledger \"Applicable From\" or \"Applicable To\" must be greather than Opened Date .");
                        isValue = false;
                        break;
                    }
                    else if (deClosedDate != DateTime.MinValue &&
                            ((deApplicableFrom > deClosedDate) || (deApplicableTo != DateTime.MinValue && deApplicableTo > deClosedDate)))
                    {
                        row = dtledgerapplicable.Rows.IndexOf(dr);
                        this.ShowMessageBoxWarning("Ledger \"Applicable From\" or \"Applicable To\" must be less than Closed Date .");
                        isValue = false;
                        break;
                    }
                    else if (LedgerId > 0 && deApplicableTo != DateTime.MinValue)
                    {
                        ResultArgs result = ledgerSystem.CheckLedgerClosedDate(ProjectId, LedgerId, deApplicableTo);
                        if (!result.Success)
                        {
                            this.ShowMessageBoxWarning(result.Message);
                            isValue = false;
                            break;
                        }
                    }
                    else if (LedgerId > 0 && deApplicableFrom != DateTime.MinValue)
                    {
                        ResultArgs result = ledgerSystem.CheckLedgerDateFrom(ProjectId, LedgerId, deApplicableFrom);
                        if (!result.Success)
                        {
                            this.ShowMessageBoxWarning(result.Message);
                            isValue = false;
                            break;
                        }
                    }
                }
                
                if (isValue)
                {
                    dtledgerapplicable.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName + " > " +
                                                       ledgerSystem.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName;

                    if (dtledgerapplicable.DefaultView.Count > 0)
                    {
                        this.ShowMessageBoxWarning("Applicable From should be less than or equal to Applicable To.");
                        isValue = false;
                    }
                }
            }

            if (!isValue)
            {
                FocusGrid(row);
            }
            
            return isValue;
        }


        /// <summary>
        /// 
        /// </summary>
        private void BindApplicableDateRange()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                if (LedgerApplicaiton.Rows.Count == 0)
                {
                    DataRow dr = LedgerApplicaiton.NewRow();
                    dr[ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] = LedgerId;
                    dr[ledgerSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName] = ProjectId;
                    dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName] = DBNull.Value; //AppSetting.FirstFYDateFrom;
                    dr[ledgerSystem.AppSchema.Ledger.APPLICABLE_TOColumn.ColumnName] = DBNull.Value ;
                    LedgerApplicaiton.Rows.Add(dr);
                }
            }
            FocusGrid(0);
        }

        private void FocusGrid(Int32 row)
        {
            gcProjectLedgerApplicableDate.Select();
            gcProjectLedgerApplicableDate.DataSource = LedgerApplicaiton;
            gvProjectLedgerApplicableDate.FocusedRowHandle = row;
            gvProjectLedgerApplicableDate.FocusedColumn = colLedgerApplicableFrom;
            gvProjectLedgerApplicableDate.ShowEditor();
        }

        
        private void frmProjectLedgerApplicableDate_Activated(object sender, EventArgs e)
        {
            FocusGrid(0);
        }
        #endregion

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

    }
}
