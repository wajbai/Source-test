using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorFeastTask : frmFinanceBaseAdd
    {
        public int TagId { get; set; }
        ResultArgs resultArgs = new ResultArgs();
        public CommunicationMode ComMode { get; set; }
        public event EventHandler UpdateHeld;
        List<int> selectedRows = new List<int>();
        List<int> selectedRowsProspects = new List<int>();

        public frmDonorFeastTask()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidInput())
                {
                    using (DonorFrontOfficeSystem donorSystem = new DonorFrontOfficeSystem())
                    {
                        int[] SelectedDonor = GetCheckedDonors();
                        int[] SelectedProspects = GetCheckedProspects();

                        donorSystem.Communicationmode = ComMode;
                        donorSystem.TagId = TagId;
                        donorSystem.TagName = txtTaskName.Text;
                        donorSystem.TemplateType = DonorMailTemplate.Tasks;
                        donorSystem.SelectedDonors = SelectedDonor;
                        donorSystem.TemplateId = glkpTemplate.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpTemplate.EditValue.ToString()) : 0;
                        donorSystem.SelectedProspects = SelectedProspects;

                        resultArgs = donorSystem.SaveFeastTask();

                        if (resultArgs.Success)
                        {
                            //this.ShowMessageBox("Task has been created successfully.");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorFeastTask.DONOR_ANNIVERSARIES_FEAST_TASK_SAVED_INFORMATION));

                            if (UpdateHeld != null)
                            {
                                UpdateHeld(sender, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
        }

        private bool IsValidInput()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtTaskName.Text))
            {
                //this.ShowMessageBoxWarning("Task Name is Empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorFeastTask.DONOR_ANNIVERSARIES_FEAST_TASK_EMPTY));
                isValid = false;
                this.txtTaskName.Focus();
            }
            else if (string.IsNullOrEmpty(glkpTemplate.Text))
            {
                //this.ShowMessageBoxWarning("Template is not selected");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorFeastTask.DONOR_ANNIVERSARIES_FEAST_TASK_TEMPLATE_EMPTY));
                isValid = false;
                this.glkpTemplate.Focus();
            }
            else if (gvDonor.SelectedRowsCount == 0 && gvProspect.SelectedRowsCount == 0)
            {
                //this.ShowMessageBoxWarning("Donor or Prospects are not assigned to the task");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorFeastTask.DONOR_ANNIVERSARIES_FEAST_TASK_ASSIGNED_TASK_INFORMATION));
                isValid = false;
                gvDonor.Focus();
                gvDonor.MoveFirst();
            }
            return isValid;
        }

        private int[] GetCheckedDonors()
        {
            int[] SelectedIds = gvDonor.GetSelectedRows();
            int[] sCheckedProjects = new int[SelectedIds.Count()];
            int ArrayIndex = 0;
            if (SelectedIds.Count() > 0)
            {
                foreach (int RowIndex in SelectedIds)
                {
                    DataRow drDonor = gvDonor.GetDataRow(RowIndex);
                    if (drDonor != null)
                    {
                        sCheckedProjects[ArrayIndex] = UtilityMember.NumberSet.ToInteger(drDonor["DONAUD_ID"].ToString());
                        ArrayIndex++;
                    }
                }
            }
            return sCheckedProjects;
        }

        private int[] GetCheckedProspects()
        {
            int[] SelectedIds = gvProspect.GetSelectedRows();
            int[] sCheckedProjects = new int[SelectedIds.Count()];
            int ArrayIndex = 0;
            if (SelectedIds.Count() > 0)
            {
                foreach (int RowIndex in SelectedIds)
                {
                    DataRow drProspect = gvProspect.GetDataRow(RowIndex);
                    if (drProspect != null)
                    {
                        sCheckedProjects[ArrayIndex] = UtilityMember.NumberSet.ToInteger(drProspect["PROSPECT_ID"].ToString());
                        ArrayIndex++;
                    }
                }
            }
            return sCheckedProjects;
        }

        private void frmDonorFeastTask_Load(object sender, EventArgs e)
        {
            LoadDonorDetails();
            LoadProspectDetails();
            LoadFeastTemplates();
            AssignValues();
        }

        private void AssignValues()
        {
            try
            {
                if (TagId > 0)
                {
                    using (DonorFrontOfficeSystem donorSystem = new DonorFrontOfficeSystem())
                    {
                        donorSystem.TagId = TagId;
                        resultArgs = donorSystem.FetchTaskByTagId();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            txtTaskName.Text = resultArgs.DataSource.Table.Rows[0][donorSystem.AppSchema.DonorTags.TAG_NAMEColumn.ColumnName].ToString();
                            glkpTemplate.EditValue = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][donorSystem.AppSchema.DonorMailTemplateType.IDColumn.ColumnName].ToString());

                            AssignMappedDonors();

                            AssignMappedProspects();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void AssignMappedDonors()
        {
            DataTable dtDonors = gcDonor.DataSource as DataTable;
            if (dtDonors != null && dtDonors.Rows.Count > 0)
            {
                gvDonor.ClearSelection();
                foreach (DataRow dr in dtDonors.Rows)
                {
                    if (this.UtilityMember.NumberSet.ToInteger(dr["MAPPED_STATUS"].ToString()) == 1)
                    {
                        int Index = dtDonors.Rows.IndexOf(dr);
                        gvDonor.SelectRow(Index);
                    }
                }
            }
        }

        private void AssignMappedProspects()
        {
            DataTable dtProspects = gcProspect.DataSource as DataTable;
            if (dtProspects != null && dtProspects.Rows.Count > 0)
            {
                gvProspect.ClearSelection();
                foreach (DataRow dr in dtProspects.Rows)
                {
                    if (this.UtilityMember.NumberSet.ToInteger(dr["MAPPED_STATUS"].ToString()) == 1)
                    {
                        int Index = dtProspects.Rows.IndexOf(dr);
                        gvProspect.SelectRow(Index);
                    }
                }
            }
        }

        private void LoadFeastTemplates()
        {
            try
            {
                using (DonorFrontOfficeSystem donorSystem = new DonorFrontOfficeSystem())
                {
                    donorSystem.LetterTypeId = (int)DonorMailTemplate.Tasks;
                    donorSystem.Communicationmode = ComMode;
                    resultArgs = donorSystem.FetchFeastDonorTemplateTypes();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpTemplate, resultArgs.DataSource.Table, "NAME", "TEMPLATE_ID");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void LoadDonorDetails()
        {
            try
            {
                using (DonorFrontOfficeSystem donaudSystem = new DonorFrontOfficeSystem())
                {
                    donaudSystem.TagId = TagId;
                    donaudSystem.Communicationmode = ComMode;
                    resultArgs = donaudSystem.FetchDonorMappedStatus();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcDonor.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void LoadProspectDetails()
        {
            try
            {
                using (DonorFrontOfficeSystem prospectSystem = new DonorFrontOfficeSystem())
                {
                    prospectSystem.TagId = TagId;
                    prospectSystem.Communicationmode = ComMode;
                    resultArgs = prospectSystem.FetchProspectsMappedStatus();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcProspect.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void HideControls()
        {

        }

        private void chkDonorFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDonor.OptionsView.ShowAutoFilterRow = chkDonorFilter.Checked;
            if (chkDonorFilter.Checked)
            {
                this.SetFocusRowFilter(gvDonor, colDonorName);
            }
        }

        private void chkProspectFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProspect.OptionsView.ShowAutoFilterRow = chkProspectFilter.Checked;
            if (chkProspectFilter.Checked)
            {
                this.SetFocusRowFilter(gvProspect, colProspectName);
            }
        }

        private void frmDonorFeastTask_ShowFilterClicked(object sender, EventArgs e)
        {
            chkDonorFilter.Checked = (chkDonorFilter.Checked) ? false : true;
            chkProspectFilter.Checked = (chkProspectFilter.Checked) ? false : true;
        }

        private void gvDonor_ColumnFilterChanged(object sender, EventArgs e)
        {
            RestoreSelection(sender as GridView);
        }

        private void gvDonor_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.Column != null && hi.Column.FieldName == "DX$CheckboxSelectorColumn")
            {
                if (!hi.InRow)
                {
                    bool allSelected = view.DataController.Selection.Count == view.DataRowCount;
                    if (!allSelected)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            int sourceHandle = view.GetDataSourceRowIndex(i);
                            if (!selectedRows.Contains(sourceHandle))
                                selectedRows.Add(sourceHandle);
                        }
                    }
                    else selectedRows.Clear();
                }
                else
                {
                    int sourceHandle = view.GetDataSourceRowIndex(hi.RowHandle);
                    if (!selectedRows.Contains(sourceHandle))
                        selectedRows.Add(sourceHandle);
                    else
                        selectedRows.Remove(sourceHandle);
                }
            }
            RestoreSelection(view);
        }

        private void gvDonor_MouseUp(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            RestoreSelection(view);
        }

        private void gvDonor_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Refresh)
            {
                gvDonor.UnselectRow(gvDonor.FocusedRowHandle);
            }
        }

        private void RestoreSelection(GridView view)
        {
            BeginInvoke(new Action(() =>
            {
                view.ClearSelection();
                for (int i = 0; i < selectedRows.Count; i++)
                {
                    view.SelectRow(view.GetRowHandle(selectedRows[i]));
                }
            }));
        }

        private void RestoreSelectionProspects(GridView view)
        {
            BeginInvoke(new Action(() =>
            {
                view.ClearSelection();
                for (int i = 0; i < selectedRowsProspects.Count; i++)
                {
                    view.SelectRow(view.GetRowHandle(selectedRowsProspects[i]));
                }
            }));
        }

        private void gvProspect_ColumnFilterChanged(object sender, EventArgs e)
        {
            RestoreSelectionProspects(sender as GridView);
        }

        private void gvProspect_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.Column != null && hi.Column.FieldName == "DX$CheckboxSelectorColumn")
            {
                if (!hi.InRow)
                {
                    bool allSelected = view.DataController.Selection.Count == view.DataRowCount;
                    if (!allSelected)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            int sourceHandle = view.GetDataSourceRowIndex(i);
                            if (!selectedRowsProspects.Contains(sourceHandle))
                                selectedRowsProspects.Add(sourceHandle);
                        }
                    }
                    else selectedRowsProspects.Clear();
                }
                else
                {
                    int sourceHandle = view.GetDataSourceRowIndex(hi.RowHandle);
                    if (!selectedRowsProspects.Contains(sourceHandle))
                        selectedRowsProspects.Add(sourceHandle);
                    else
                        selectedRowsProspects.Remove(sourceHandle);
                }
            }
            RestoreSelectionProspects(view);
        }

        private void gvProspect_MouseUp(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            RestoreSelectionProspects(view);
        }

        private void gvProspect_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Refresh)
            {
                gvProspect.UnselectRow(gvProspect.FocusedRowHandle);
            }
        }

        private void gcDonor_Click(object sender, EventArgs e)
        {

        }
    }
}