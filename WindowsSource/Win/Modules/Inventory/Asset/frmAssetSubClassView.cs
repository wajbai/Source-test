using System;
using System.Windows.Forms;

using Bosco.Model.UIModel;
using Bosco.Model;
using Bosco.Utility;

using DevExpress.XtraTreeList.Nodes;
using Bosco.DAO.Schema;
using ACPP.Modules.Data_Utility;
using DevExpress.XtraBars;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;

namespace ACPP.Modules.Inventory
{
    public partial class frmAssetSubClassView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        private int EditclassId = 0;
        #endregion

        #region Constructor
        public frmAssetSubClassView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property

        private int classId = 0;
        public int ClassId
        {
            get
            {
                RowIndex = gvAssetSubcls.FocusedRowHandle;
                classId = gvAssetSubcls.GetFocusedRowCellValue(colAssetClassID) != null ? this.UtilityMember.NumberSet.ToInteger(gvAssetSubcls.GetFocusedRowCellValue(colAssetClassID).ToString()) : 0;
                return classId;
            }
            set
            {
                classId = value;
            }
        }
        public int ParentClassId
        {
            get
            {
                RowIndex = gvAssetSubcls.FocusedRowHandle;
                classId = gvAssetSubcls.GetFocusedRowCellValue(colParentClassId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAssetSubcls.GetFocusedRowCellValue(colParentClassId).ToString()) : 0;
                return classId;
            }
            set
            {
                classId = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the AssetSubClass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmAssetSubClassView_Load(object sender, EventArgs e)
        {
            GetAssetSubClsDetails();
            SetTitle();
        }
        /// <summary>
        /// load the AssetSubClass details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssetSubClassView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            GetAssetSubClsDetails();
        }

        /// <summary>
        /// To Show the Add form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarAssetSubcls_AddClicked(object sender, EventArgs e)
        {
            ShowForm(ClassId, FormMode.Add);
        }

        /// <summary>
        /// To Edit the AssetSubClass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarAssetSubcls_EditClicked(object sender, EventArgs e)
        {
            EditAssetSubCls();
        }

        /// <summary>
        /// To Edit the AssetSubClass while press the Enter Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssetSubClassView_EnterClicked(object sender, EventArgs e)
        {
            EditAssetSubCls();
        }
        /// <summary>
        /// To Edit the AssetSubClass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAssetSubcls_DoubleClick(object sender, EventArgs e)
        {
            EditAssetSubCls();
        }


        /// <summary>
        /// To View the Number of Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProjectCategory_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvAssetSubcls.RowCount.ToString();
        }

        /// <summary>
        /// To Enable the AutoFilter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAssetSubcls.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAssetSubcls, colAssetClass);
            }
        }


        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void ucToolBarAssetSubcls_RefreshClicked(object sender, EventArgs e)
        {
            GetAssetSubClsDetails();
        }
        #endregion

        #region Methods
        /// <summary>
        /// To Get the AssetSubClsDetails
        /// </summary>
        public void GetAssetSubClsDetails()
        {
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchClassDetails();
                    gcSubClass.DataSource = resultArgs.DataSource.Table;
                    gcSubClass.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Show form based on the Selection
        /// </summary>
        /// <param name="projectCategoryId"></param>
        public void ShowForm(int ClassId, FormMode FrmMode)
        {
            try
            {
                frmAssetClassAdd frmAssetsubcls = new frmAssetClassAdd(ClassId, FrmMode);
                frmAssetsubcls.UpdateHeld += new EventHandler(OnUpdateHeld);
               
                    frmAssetsubcls.ShowDialog();
               
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// To refresh the Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            GetAssetSubClsDetails();
            gvAssetSubcls.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// To Edit the AssetSubClass
        /// </summary>
        public void EditAssetSubCls()
        {
            using (AssetClassSystem assetClassSystem = new AssetClassSystem())
            {
                try
                {
                    if (gvAssetSubcls.RowCount != 0 && ParentClassId != 1)
                    {
                     
                        if (gvAssetSubcls.RowCount != 0)
                        {
                            if (ClassId != 0)
                            {
                                ShowForm(ClassId, FormMode.Edit);
                            }
                            else
                            {
                                if (!chkShowFilter.Checked)
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                        }
                    }
                    else
                    {
                      //  this.ShowMessageBox("");
                    }
                }
                catch (Exception ex)
                {
                    MessageRender.ShowMessage(ex.ToString(), true);
                }
                finally { }
            }
        }

        
        private void SetTitle()
        {
            // this.Text = "Sub Class";
            this.Text = this.GetMessage(MessageCatalog.Asset.SubClass.ASSETSUBCLASS_VIEW_CAPTION);
        }
        #endregion

        /// <summary>
        /// To Close AssetSubClass System
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   

        private void ucToolBarAssetSubcls_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// To Delete AssetSubClass System
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      

        private void ucToolBarAssetSubcls_DeleteClicked(object sender, EventArgs e)
        {
            try
            {

                if (gvAssetSubcls.RowCount != 0)
                {
                    if (ClassId != 0)
                    {
                        using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                assetClassSystem.ClassId = classId.ToString();
                                resultArgs = assetClassSystem.DeleteSubClassDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    GetAssetSubClsDetails();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

        }


    }

}