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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorMailFeast : frmFinanceBase
    {
        ResultArgs resultArgs = new ResultArgs();
        List<int> selectedRows = new List<int>();

        public CommunicationMode communicationmode { get; set; }
        private DataTable dtFeast = new DataTable();

        private string TemplateName
        {
            get
            {
                string templateName = string.Empty;
                if (glkpFeastTask.EditValue != null)
                {
                    DataRowView dv = glkpFeastTask.GetSelectedDataRow() as DataRowView;
                    if (dv != null)
                        templateName = dv.Row["NAME"].ToString();
                }
                return templateName;
            }
        }

        private int FeastTemplateId
        {
            get
            {
                int templateId = 0;
                if (glkpFeastTask.EditValue != null)
                {
                    DataRowView dv = glkpFeastTask.GetSelectedDataRow() as DataRowView;
                    if (dv != null)
                        templateId = this.UtilityMember.NumberSet.ToInteger(dv.Row["TEMPLATE_ID"].ToString());
                }
                return templateId;
            }
        }

        public frmDonorMailFeast()
        {
            InitializeComponent();
        }

        private void glkpFeastTask_EditValueChanged(object sender, EventArgs e)
        {
            LoadDonorsByTask();
        }

        private void frmDonorMailFeast_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadFeastTasks();
            this.gvFeast.OptionsBehavior.Editable = false;
            ApplyRights();
        }

        private void ApplyRights()
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                lciGenerate.Visibility = (communicationmode == CommunicationMode.MailDesk) ? (CommonMethod.ApplyUserRights((int)FeastMail.PreviewFeastMail) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never) :
                    (CommonMethod.ApplyUserRights((int)FeastSMS.PreviewFeastSMS) != 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            }
        }

        private void SetTitle()
        {
            if (communicationmode == CommunicationMode.MailDesk)
            {
                //this.Text = "Tasks - Mail";
                this.Text = this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_FEAST_PREVIEW);
                //btnGenerate.Text = "Preview";
                btnGenerate.Text = this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_FEAST_PREVIEW);
            }
            else
            {
                //this.Text = "Tasks - SMS";
                this.Text = this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_FEAST_TASKSMS_INFORMATION);
                //btnGenerate.Text = "Preview";
                btnGenerate.Text = this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_FEAST_PREVIEW);
            }
        }

        private void LoadDonorsByTask()
        {
            if (glkpFeastTask.EditValue != null)
            {
                rgPreviewType.SelectedIndex = -1;
                using (DonorFrontOfficeSystem donorSystem = new DonorFrontOfficeSystem())
                {
                    donorSystem.TagId = this.UtilityMember.NumberSet.ToInteger(glkpFeastTask.EditValue.ToString());
                    donorSystem.Status = !string.IsNullOrEmpty(cboStatus.Text) ? cboStatus.SelectedIndex : 2;
                    donorSystem.Communicationmode = communicationmode;
                    resultArgs = donorSystem.FetchMappedDonorByTagId();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcFeast.DataSource = dtFeast = resultArgs.DataSource.Table;
                        gcFeast.RefreshDataSource();
                    }
                }
                rgPreviewType.SelectedIndex = 1;
            }
        }

        private void LoadFeastTasks()
        {
            using (DonorFrontOfficeSystem donorSystem = new DonorFrontOfficeSystem())
            {
                donorSystem.TemplateType = DonorMailTemplate.Tasks;
                resultArgs = donorSystem.FetchTaskDetails();
                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFeastTask, resultArgs.DataSource.Table, donorSystem.AppSchema.DonorTags.TAG_NAMEColumn.ColumnName, donorSystem.AppSchema.DonorTags.TAG_IDColumn.ColumnName);
                    glkpFeastTask.EditValue = glkpFeastTask.Properties.GetKeyValue(0);
                }
            }
        }

        private void glkpFeastTask_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                //Add new Task
                frmDonorFeastTask frmTask = new frmDonorFeastTask();
                frmTask.TagId = (int)AddNewRow.NewRow;
                frmTask.ComMode = communicationmode;
                frmTask.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmTask.ShowDialog();
            }
            else if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.OK)
            {
                //Edit Task
                if (glkpFeastTask.EditValue != null)
                {
                    frmDonorFeastTask frmTask = new frmDonorFeastTask();
                    frmTask.TagId = this.UtilityMember.NumberSet.ToInteger(glkpFeastTask.EditValue.ToString());
                    frmTask.ComMode = communicationmode;
                    frmTask.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmTask.ShowDialog();
                }
                else
                {
                    //this.ShowMessageBoxWarning("Task is not selected to edit");
                    this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_FEAST_TASK_WARNING_INFORMATION));
                }
            }
        }



        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDonorsByTask();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ValidateDonor())
            {
                if (communicationmode == CommunicationMode.MailDesk)
                {
                    frmDonorMailMerge frmMailMerge = new frmDonorMailMerge();
                    frmMailMerge.TemplateType = DonorMailTemplate.Tasks;
                    frmMailMerge.DonorPreviewType = (rgPreviewType.SelectedIndex == 0) ? PreviewType.Print : PreviewType.Email;
                    frmMailMerge.LetterTypeId = (int)DonorMailTemplate.Tasks;
                    frmMailMerge.DataSource = GetCheckedList();
                    frmMailMerge.FeastDayTemplate = TemplateName;
                    frmMailMerge.FeastTemplateId = FeastTemplateId;
                    frmMailMerge.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmMailMerge.ShowDialog();
                }
                else if (communicationmode == CommunicationMode.ContactDesk)
                {
                    frmDonorSMSMerge frmMailMerge = new frmDonorSMSMerge();
                    frmMailMerge.TemplateType = DonorMailTemplate.Tasks;
                    frmMailMerge.DonorPreviewType = (rgPreviewType.SelectedIndex == 0) ? PreviewType.Print : PreviewType.Email;
                    frmMailMerge.LetterTypeId = (int)DonorMailTemplate.Tasks;
                    frmMailMerge.DataSource = GetCheckedList();
                    frmMailMerge.FeastDayTemplate = TemplateName;
                    frmMailMerge.FeastTemplateId = FeastTemplateId;
                    frmMailMerge.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmMailMerge.ShowDialog();
                }
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadDonorsByTask();
            LoadFeastTasks();
        }

        private bool ValidateDonor()
        {
            bool isValid = true;
            if (gvFeast.SelectedRowsCount == 0)
            {
                isValid = false;
                //this.ShowMessageBoxWarning("No record is selected to generate.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_FEAST_RECORD_EMPTY));
                gvFeast.Focus();
            }
            else if (string.IsNullOrEmpty(TemplateName))
            {
                isValid = false;
                //this.ShowMessageBoxWarning("Template is not attached");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorMailFeast.DONOR_MAIL_FEAST_TEMPLATE_EMPTY));
            }
            return isValid;
        }

        private DataTable GetCheckedList()
        {
            int[] SelectedIds = null;
            SelectedIds = gvFeast.GetSelectedRows();

            DataTable dtDataSource = gcFeast.DataSource as DataTable;
            DataTable dtSelectedList = new DataTable();
            dtSelectedList = dtDataSource.Clone();

            if (SelectedIds.Count() > 0)
            {
                foreach (int RowIndex in SelectedIds) //selectedRows) ( it is changed by chinna instead of selecting rows )
                {
                    DataRow drDonor = gvFeast.GetDataRow(RowIndex);
                    if (drDonor != null)
                    {
                        dtSelectedList.ImportRow(drDonor);
                    }
                }

                dtSelectedList.DefaultView.Sort = dtDataSource.DefaultView.Sort;
            }
            return dtSelectedList;
        }



        private void gvFeast_ColumnFilterChanged(object sender, EventArgs e)
        {
            RestoreSelection(sender as GridView);
        }

        private void gvFeast_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.Column != null && hi.Column.FieldName == "DX$CheckboxSelectorColumn")
            {
                if (!hi.InRow)
                {
                    //DataView dv = view.DataSource as DataView;
                    bool allSelected = view.DataController.Selection.Count - view.DataController.GroupRowCount == view.DataRowCount;
                    if (!allSelected)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            int sourceHandle = view.GetRowHandle(i);

                            if (view.IsDataRow(sourceHandle))
                            {
                                // string donoremail = view.GetDataRow(sourceHandle)["email"].ToString();
                                if (!selectedRows.Contains(sourceHandle))  //&& !string.IsNullOrEmpty(donoremail))
                                    selectedRows.Add(sourceHandle);
                            }
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

        private void gvFeast_MouseUp(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            RestoreSelection(view);
        }

        private void gvFeast_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Refresh)
            {
                gvFeast.UnselectRow(gvFeast.FocusedRowHandle);
            }
        }

        private void RestoreSelection(GridView view)
        {
            BeginInvoke(new Action(() =>
            {
                view.ClearSelection();
                for (int i = 0; i < selectedRows.Count; i++)
                {
                    int rowhandle = view.GetRowHandle(selectedRows[i]);
                    view.SelectRow(rowhandle);
                }
            }));
        }

        private void gvFeast_ShowingEditor(object sender, CancelEventArgs e)
        {
            //try
            //{
            //    string EmailId = gvFeast.GetRowCellValue(gvFeast.FocusedRowHandle, "EMAIL").ToString();
            //    if (string.IsNullOrEmpty(EmailId) && gvFeast.FocusedColumn.FieldName == "DX$CheckboxSelectorColumn")
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message, true);
            //}
        }

        private void gvFeast_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //try
            //{
            //    GridView View = sender as GridView;
            //    if (e.RowHandle >= 0)
            //    {
            //        DataRow drRow = View.GetDataRow(e.RowHandle);
            //        if (drRow != null)
            //        {
            //            string EmailId = drRow[communicationmode == CommunicationMode.MailDesk ? "EMAIL" : "PHONE"].ToString();

            //            if (string.IsNullOrEmpty(EmailId))
            //            {
            //                e.Appearance.ForeColor = Color.DimGray;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message, true);
            //}
        }

        private void gvFeast_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (e.Column.Name == "DX$CheckboxSelectorColumn")
            //{
            //    string EmailId = (view.GetDataRow(e.RowHandle) as DataRow)[communicationmode == CommunicationMode.MailDesk ? "EMAIL" : "PHONE"].ToString();
            //    if (string.IsNullOrEmpty(EmailId))
            //    {
            //        e.Handled = true;
            //        return;
            //    }
            //}
        }

        private void frmDonorMailFeast_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }

        private void rgPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgPreviewType.SelectedIndex == 0 || rgPreviewType.SelectedIndex == 1)
            {
                if (dtFeast != null && dtFeast.Rows.Count > 0)
                {
                    DataView dvFeast = new DataView(dtFeast);
                    if (rgPreviewType.SelectedIndex == 0)
                    {
                        dvFeast.RowFilter = "ADDRESS<>'Nil' AND ADDRESS<>''";
                    }
                    else if (rgPreviewType.SelectedIndex == 1)
                    {
                        dvFeast.RowFilter = (communicationmode == CommunicationMode.MailDesk) ? "EMAIL<>''" : "[MOBILE NO]<>''";
                    }

                    gcFeast.DataSource = dvFeast.ToTable();
                    gcFeast.RefreshDataSource();
                    dvFeast.RowFilter = "";
                }
            }
        }

        private void ucFeast_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucFeast_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcFeast, "Feast", PrintType.DT, gvFeast, true);
        }

        private void ucFeast_RefreshClicked(object sender, EventArgs e)
        {
            LoadDonorsByTask();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvFeast.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvFeast, colName);
            }
        }

        private void gvFeast_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvFeast.RowCount.ToString();
        }
    }
}