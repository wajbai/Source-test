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
using Bosco.Model;
using Bosco.Model.UIModel;
using DevExpress.XtraBars.Docking;
using Bosco.Report.Base;
using DevExpress.XtraReports.UI;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmFixedAssetRegister : frmFinanceBase
    {
        ResultArgs resultArgs = new ResultArgs();
        ReportProperty rptProperty;

        #region Properties
        private string SelectedAssetclassIds { get; set; }
        #endregion

        public frmFixedAssetRegister()
        {
            InitializeComponent();
        }

        private void frmFixedAssetRegister_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            this.ShowHideLeftMenuBar = false;
            this.ShowHideDockPanel = DockVisibility.AutoHide;
            this.ShowRibbonHomePage = true;
            dtDateAsOn.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo.ToString(), false);
            LoadProject();
            LoadAssetClassDetails();
            SetTitle();
            // LoadFixedAssetRegister();
        }

        public void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Asset.FixeAssetRegister.FIXED_ASSET_REG_TITLE);
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    ResultArgs resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = this.AppSetting.UserProjectId;
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public string GetAssetClassIds()
        {
            try
            {
                string assetclassId = string.Empty;
                int[] SelectedIds = gvAssetClass.GetSelectedRows();
                int ArrayIndex = 0;
                if (SelectedIds.Count() > 0)
                {
                    foreach (int RowIndex in SelectedIds)
                    {
                        DataRow drProject = gvAssetClass.GetDataRow(RowIndex);
                        if (drProject != null)
                        {
                            assetclassId += drProject["ASSET_CLASS_ID"].ToString() + ",";
                            ArrayIndex++;
                        }
                    }
                }

                SelectedAssetclassIds = assetclassId.TrimEnd(',');
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return SelectedAssetclassIds;
        }

        private void LoadAssetClassDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchClassDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcAssetClass.DataSource = resultArgs.DataSource.Table;
                        gcAssetClass.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void LoadFixedAssetRegister()
        {
            try
            {
                if (glkpProject.EditValue != null)
                {
                    if (!string.IsNullOrEmpty(dtDateAsOn.Text))
                    {
                        Bosco.Report.Base.ReportProperty.Current.ReportId = "RPT-088";
                        Bosco.Report.Base.ReportProperty.Current.Project = glkpProject.EditValue.ToString();
                        Bosco.Report.Base.ReportProperty.Current.DateAsOn = this.UtilityMember.DateSet.ToDate(dtDateAsOn.EditValue.ToString(), "dd/MM/yyyy").ToString();
                        Bosco.Report.Base.ReportProperty.Current.Assetclass = GetAssetClassIds();

                        this.rptViewer.ReportId = "RPT-088";

                        //rptProperty.Project = glkpProject.EditValue.ToString();
                        //rptProperty.DateAsOn = dtDateAsOn.EditValue.ToString();
                        //rptProperty.Assetclass = GetAssetClassIds();

                        //ResultArgs resultArgs = null;
                        //using (AssetInwardOutwardSystem assetSystem = new AssetInwardOutwardSystem())
                        //{
                        //    assetSystem.ProjectId = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                        //    assetSystem.InOutDate = this.UtilityMember.DateSet.ToDate(dtDateAsOn.DateTime.ToShortDateString(), false);
                        //    assetSystem.AssetClassId = GetAssetClassIds();
                        //    resultArgs = assetSystem.FetchFixedAssetRegister();
                        //    if (resultArgs != null && resultArgs.Success)
                        //    {
                        //        gcFixedAssetRegister.DataSource = resultArgs.DataSource.Table;
                        //    }
                        //}
                    }
                    else
                    {
                        //this.ShowMessageBoxWarning("Date As on is Empty");
                        this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Asset.FixeAssetRegister.FIXED_ASSET_REG_DATE_AS_ON_EMPTY));
                    }
                }
                else
                {
                    //this.ShowMessageBoxWarning("Project is not selected");
                    this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Asset.FixeAssetRegister.FIXED_ASSET_REG_PROJECT_SELECT_INFO));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            GetAssetClassIds();
            LoadFixedAssetRegister();
        }

        private void chkAssetClass_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            GetAssetClassIds();
            LoadFixedAssetRegister();
        }

        private void chkAssetClass_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void ucToolBar_RefreshClicked(object sender, EventArgs e)
        {
            LoadFixedAssetRegister();
        }

        private void ucToolBar_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcFixedAssetRegister, this.GetMessage(MessageCatalog.Asset.SalesVoucher.FIXED_ASSET_REGISTER_PRINT_CAPTION), PrintType.DT, advbangvFixedAssetRegister, true);
        }

        private void ucToolBar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvAssetClass_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataTable dtPurpose = (DataTable)gcAssetClass.DataSource;
            if (dtPurpose != null && dtPurpose.Rows.Count > 0)
            {
                GetAssetClassIds();
                LoadFixedAssetRegister();
            }
        }
       
        private void gvAssetClass_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Refresh)
            {
                GetAssetClassIds();
                LoadFixedAssetRegister();
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAssetClass.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAssetClass, colAssetClass);
            }
        }

        private void frmFixedAssetRegister_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
    }
}