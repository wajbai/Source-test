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
using Bosco.Utility.ConfigSetting;
using Bosco.Model.UIModel.Master;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmManageMultiDB : frmFinanceBaseAdd
    {
        ResultArgs resultArgs = new ResultArgs();
        public frmManageMultiDB()
        {
            InitializeComponent();
        }

        private void frmManageMultiDB_Load(object sender, EventArgs e)
        {
            LoadMultiDB();
        }

        public DataSet LoadMultiDB()
        {
            DataSet dsDb = new DataSet();
            gcMultiBranch.DataSource = null;
            try
            {
                dsDb = XMLConverter.ConvertXMLToDataSet(SettingProperty.RestoreMultipleDBPath);
                if (dsDb.Tables[0].Rows.Count > 0)
                {
                    gcMultiBranch.DataSource = dsDb.Tables[0];
                    gcMultiBranch.RefreshDataSource();
                }
            }
            catch (Exception)
            {
            }
            finally { }
            return dsDb;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsXml = new DataSet();
                DataTable dttemp = gcMultiBranch.DataSource as DataTable;
                dsXml.Tables.Add(dttemp.Copy());
                if (dsXml != null && dsXml.Tables[0].Rows.Count > 0)
                {
                    XMLConverter.WriteToXMLFile(dsXml, SettingProperty.RestoreMultipleDBPath);

                    //On 06/05/2024, Update Multi DB xml file into database
                    using (SettingSystem settingsys = new SettingSystem())
                    {
                        settingsys.UpdateMultiDBXMLConfigurationInAcperp();
                    }

                    //On 15/12/2020, To take multiple db backup file for saftey puropose
                    this.TakeBackup_MultipleDBXML();
                    //-------------------------------------------------------------------

                    //this.ShowSuccessMessage("Branch database saved.");
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.BRANCH_DB_SAVE));
                    // this.Close();
                }
            }
            catch (Exception err)
            {
                this.ShowSuccessMessage(err.Message);
            }
        }

        private void rbtDelete_Click(object sender, EventArgs e)
        {
            LoadDeleteMultiDB();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.D))
            {
                LoadDeleteMultiDB();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void LoadDeleteMultiDB()
        {
            try
            {
                string DbName = gvMultiBranch.GetFocusedRowCellValue(colDatabasename) != null ? gvMultiBranch.GetRowCellValue(gvMultiBranch.FocusedRowHandle, colDatabasename).ToString() : string.Empty;
                if (!string.IsNullOrEmpty(DbName) && DbName != SettingProperty.ActiveDatabaseName.ToString())
                {
                    //if (this.ShowConfirmationMessage("Are you sure you want to delete the selected branch database?  " +
                      //" \n\n Once the database is deleted, can't be retrieved.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.BRANCH_DB_DELETE_CONFIRMATION) +
                      this.GetMessage(MessageCatalog.Master.DataUtilityForms.ONCE_DB_DELETE_CANNOT_RETRIVE), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DashBoardSystem dashboardsystem = new DashBoardSystem())
                        {
                            this.ShowWaitDialog();
                            resultArgs = dashboardsystem.DropDatabase(DbName);
                            if (resultArgs.Success)
                            {
                                gvMultiBranch.DeleteRow(gvMultiBranch.FocusedRowHandle);
                                DataView dvfilter = gvMultiBranch.DataSource as DataView;
                                dvfilter.RowFilter = "Restore_Db <> '" + DbName.Trim() + "'";
                                DataSet dsMultiDb = new DataSet();
                                dsMultiDb.Tables.Add(dvfilter.ToTable());
                                XMLConverter.WriteToXMLFile(dsMultiDb, SettingProperty.RestoreMultipleDBPath);
                                this.CloseWaitDialog();
                                //this.ShowSuccessMessage("Branch database deleted.");
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.BRANCH_DB_DELETED));
                            }
                        }
                    }
                }
                else
                {
                    //this.ShowMessageBox("Current Database can't be deleted.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.CURRENT_DB_CANNOT_BE_DELETED));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
        }


        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMultiBranch.OptionsFind.HighlightFindResults = true;
            gvMultiBranch.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvMultiBranch, colDBName);
            }
        }

        private void frmManageMultiDB_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvMultiBranch_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvMultiBranch.RowCount.ToString();
        }

        private void gvMultiBranch_LostFocus(object sender, EventArgs e)
        {

        }

        private void gvMultiBranch_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string fg = gvMultiBranch.GetFocusedRowCellValue(colDBName).ToString().Trim();
            if (string.IsNullOrEmpty(fg))
            {
                //this.ShowMessageBox("Branch Name is empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.BRANCH_NAME_EMPTY));
            }

        }

        private void gvMultiBranch_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            DataView currentDataView = (sender as GridView).DataSource as DataView;
            if (view.FocusedColumn.FieldName == "RestoreDBName")
            {
                //check duplicate code
                string currentCode = e.Value.ToString().Trim();
                for (int i = 0; i < currentDataView.Count; i++)
                {
                    if (i != view.GetDataSourceRowIndex(view.FocusedRowHandle))
                    {
                        if (currentDataView[i]["RestoreDBName"].ToString() == currentCode)
                        {
                            //this.ShowMessageBox("Branch Name exists already.");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.BRANCH_NAME_EXISTS_ALREADY));
                            e.Valid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void gvMultiBranch_BeforePrintRow(object sender, DevExpress.XtraGrid.Views.Printing.CancelPrintRowEventArgs e)
        {

        }

    }
}