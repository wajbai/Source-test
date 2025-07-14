using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.TDS;

namespace ACPP.Modules.TDS
{
    public partial class frmDutyTaxView : frmFinanceBase
    {
        #region Variable Declarations
        private int RowIndex = 0;
        ResultArgs resultArgs = null;

        #endregion

        #region Constructor
        public frmDutyTaxView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int dutytaxid = 0;
        private int DutyTaxId
        {
            get
            {

                RowIndex = gvTax.FocusedRowHandle;
                dutytaxid = gvTax.GetFocusedRowCellValue(gcolDutyTaxId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTax.GetFocusedRowCellValue(gcolDutyTaxId).ToString()) : 0;
                return dutytaxid;
            }
            set
            {
                dutytaxid = value;
            }
        }
        #endregion

        #region Events

        private void ucDutyTax_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        private void ucDutyTax_EditClicked(object sender, EventArgs e)
        {
            showEditForm();
        }

        private void ucDutyTax_DeleteClicked(object sender, EventArgs e)
        {
            DeleteDetails();
        }

        private void ucDutyTax_RefreshClicked(object sender, EventArgs e)
        {
            FetchDutyTax();
        }

        private void ucDutyTax_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucDutyTax_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcDutyTax, this.GetMessage(MessageCatalog.TDS.DutyTax.TDS_DUTY_TAX_PRINT_CAPTION), PrintType.DT, gvTax);
        }

        private void frmDutyTaxView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmDutyTaxView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmDutyTaxView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false,true);
            FetchDutyTax();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTax.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTax, gcolDutyTax);
            }
        }

        private void gvTax_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvTax.RowCount.ToString();
        }

        private void gvTax_DoubleClick(object sender, EventArgs e)
        {
            showEditForm();
        }

        private void frmDutyTaxView_EnterClicked(object sender, EventArgs e)
        {
            showEditForm();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchDutyTax();
            gvTax.FocusedRowHandle = RowIndex;
        }

        #endregion

        #region Methods
        private void ShowForm(int Taxid)
        {
            try
            {
                frmDutyTaxAdd dutytax = new frmDutyTaxAdd(Taxid);
                dutytax.UpdateHeld += new EventHandler(OnUpdateHeld);
                dutytax.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void DeleteDetails()
        {
            try
            {
                if (gvTax.RowCount != 0)
                {
                    if (isEditableDutyTax())
                    {
                        if (DutyTaxId != 0)
                        {
                            using (DeducteeTaxSystem taxsystem = new DeducteeTaxSystem())
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    resultArgs = taxsystem.DeleteDutyTaxTypeDetails(DutyTaxId);
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        FetchDutyTax();
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
                        //this.ShowMessageBox("There is no rights to delete this Duty Tax");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.DutyTax.TDS_NORIGHTS_DELETE_INFO));
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

        public void showEditForm()
        {
            if (gvTax.RowCount != 0)
            {
                if (isEditableDutyTax())
                {
                    if (DutyTaxId != 0)
                    {
                        ShowForm(DutyTaxId);
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
                    //this.ShowMessageBox("Fixed Duty Tax type cannot be edited.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.DutyTax.TDS_DUTY_TAX_TYPE_CANNOT_EDIT_INFO));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        public void FetchDutyTax()
        {
            try
            {
                using (DeducteeTaxSystem dutytaxsystem = new DeducteeTaxSystem())
                {
                    resultArgs = dutytaxsystem.FetchDutyTaxTypes();
                    gcDutyTax.DataSource = resultArgs.DataSource.Table;
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

        private bool isEditableDutyTax()
        {
            bool isEditable = true;
            if (DutyTaxId.Equals((int)YesNo.Yes) || DutyTaxId.Equals(2))
            {
                isEditable = false;
            }
            return isEditable;
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(TDSDutyTax.EditDutyTax);
            this.enumUserRights.Add(TDSDutyTax.PrintDutyTax);
            this.enumUserRights.Add(TDSDutyTax.ViewDutyTax);
            this.ApplyUserRights(ucDutyTax, enumUserRights, (int)Menus.TDSDutyTax);
        }
        #endregion

    }
}