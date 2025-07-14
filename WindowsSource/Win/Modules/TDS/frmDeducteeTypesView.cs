using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ACPP.Modules.TDS;
using Bosco.Utility;
using Bosco.Model.TDS;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Setting;

namespace ACPP.Modules.TDS
{
    public partial class frmDeducteeTypesView : frmFinanceBase
    {
        #region Variable Declarations
        private int RowIndex = 0;
        ResultArgs resultArgs = null;
        #endregion

        #region Constructors
        public frmDeducteeTypesView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmDeducteeTypesView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchDeducteeTypes();
        }

        private void ucDeducteeType_AddClicked(object sender, EventArgs e)
        {
            ShowAddForm((int)AddNewRow.NewRow);
        }

        private void ucDeducteeType_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucDeducteeType_DeleteClicked(object sender, EventArgs e)
        {
            DeleteDetails();
        }

        private void ucDeducteeType_EditClicked(object sender, EventArgs e)
        {
            showEditForm();
        }

        private void ucDeducteeType_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcDeducteeTypes, this.GetMessage(MessageCatalog.TDS.DeducteeTypes.TDS_DEDUCTEETYPES_PRINT_CAPTION), PrintType.DT, gvDeductee);
        }

        private void ucDeducteeType_RefreshClicked(object sender, EventArgs e)
        {
            FetchDeducteeTypes();
        }
        #endregion

        #region Properties
        private int deducteeid = 0;
        private int DeducteeId
        {
            get
            {

                RowIndex = gvDeductee.FocusedRowHandle;
                deducteeid = gvDeductee.GetFocusedRowCellValue(gcolDeducteeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvDeductee.GetFocusedRowCellValue(gcolDeducteeId).ToString()) : 0;
                return deducteeid;
            }
            set
            {
                deducteeid = value;
            }
        }
        #endregion

        #region Methods
        private void ShowAddForm(int deduteetypeid)
        {
            try
            {
                frmDeducteeTypesAdd frmdeducteetype = new frmDeducteeTypesAdd(deduteetypeid);
                frmdeducteetype.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmdeducteetype.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void FetchDeducteeTypes()
        {
            try
            {
                using (DeducteeTypeSystem deducteesystem = new DeducteeTypeSystem())
                {
                    resultArgs = deducteesystem.FetchDeducteeTypes();
                    gcDeducteeTypes.DataSource = resultArgs.DataSource.Table;
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

        public void showEditForm()
        {
            if (gvDeductee.RowCount != 0)
            {
                if (DeducteeId != 0)
                {
                    ShowAddForm(DeducteeId);
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

        private void DeleteDetails()
        {
            try
            {
                if (gvDeductee.RowCount != 0)
                {
                    if (DeducteeId != 0)
                    {
                        if (ValidateDeducteeType() == 0)
                        {
                            using (DeducteeTypeSystem deducteeTypeSystem = new DeducteeTypeSystem())
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    deducteeTypeSystem.DeducteeTypeid = DeducteeId;
                                    resultArgs = deducteeTypeSystem.DeleteDeducteeTypeDetails();
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        FetchDeducteeTypes();
                                    }
                                }
                            }
                        }
                        else
                        {
                            //this.ShowMessageBox("Cannot delete.Voucher is made for this Deductee");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.DeducteeTypes.TDS_VOUCHER_CANNOT_DELETE_INFO));
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

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchDeducteeTypes();
            gvDeductee.FocusedRowHandle = RowIndex;
        }

        #endregion

        private void frmDeducteeTypesView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvDeductee_RowCountChanged(object sender, EventArgs e)
        {
            lblRowcount.Text = gvDeductee.RowCount.ToString();
        }

        private void frmDeducteeTypesView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void gvDeductee_DoubleClick(object sender, EventArgs e)
        {
            showEditForm();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDeductee.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDeductee, gcolDeducteeName);
            }
        }

        private void frmDeducteeTypesView_EnterClicked(object sender, EventArgs e)
        {
            showEditForm();
        }

        private int ValidateDeducteeType()
        {
            using (DeducteeTypeSystem DeducteeSystem = new DeducteeTypeSystem())
            {
                DeducteeSystem.DeducteeTypeid = DeducteeId;
                return DeducteeSystem.ValidateDeducteeType();
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(TDSDeducteeType.CreateDeducteeType);
            this.enumUserRights.Add(TDSDeducteeType.EditDeducteeType);
            this.enumUserRights.Add(TDSDeducteeType.DeleteDeducteeType);
            this.enumUserRights.Add(TDSDeducteeType.PrintDeducteeType);
            this.enumUserRights.Add(TDSDeducteeType.ViewDeducteeType);
            this.ApplyUserRights(ucDeducteeType, enumUserRights, (int)Menus.TDSDeducteeType);
        }
    }
}