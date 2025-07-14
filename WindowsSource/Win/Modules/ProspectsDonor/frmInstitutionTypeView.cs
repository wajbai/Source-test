using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Donor;
using Bosco.Utility;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmInstitutionTypeView : frmFinanceBase
    {

        ResultArgs resultArgs = null;
        private int instutionTypeId = 0;
        int RowIndex = 0;
        public int InstutionTypeId
        {
            get
            {
                RowIndex = gvInstitutionType.FocusedRowHandle;
                instutionTypeId = gvInstitutionType.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInstitutionType.GetFocusedRowCellValue(colId).ToString()) : 0;
                return instutionTypeId;
            }
            set { instutionTypeId = value; }
        }

        public frmInstitutionTypeView()
        {
            InitializeComponent();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvInstitutionType.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvInstitutionType, colINSTITUTIONALTYPE);
            }
        }

        private void frmInstitutionTypeView_Load(object sender, EventArgs e)
        {
            LoadDetails();
            SetTitle();
        }

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            DeleteIntitutionTypeDetails();
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            ShowEditInstutionTypeForm();
        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            LoadDetails();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadDetails();
        }
        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Networking.NetworkingDonorInstitutionType.NETWORKING_DONOR_INS_TYPE_VIEW_CATPION);
        }

        private void LoadDetails()
        {
            using (InstitutionType institutionType = new InstitutionType())
            {
                resultArgs = institutionType.FetchAll();
                if (resultArgs.Success && resultArgs != null)
                {
                    gcInstitutionType.DataSource = resultArgs.DataSource.Table;
                    gcInstitutionType.RefreshDataSource();
                }
            }
        }

        private void ShowForm(int Id)
        {
            try
            {
                frmInstitutionType instutionType = new frmInstitutionType(Id);
                instutionType.UpdateHeld += new EventHandler(OnUpdateHeld);
                instutionType.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void DeleteIntitutionTypeDetails()
        {
            try
            {
                if (gvInstitutionType.RowCount != 0)
                {
                    if (this.InstutionTypeId != 0)
                    {
                        using (InstitutionType institutionType = new InstitutionType())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                institutionType.InstutionTypeId = this.InstutionTypeId;
                                resultArgs = institutionType.Delete();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadDetails();
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

        public void ShowEditInstutionTypeForm()
        {
            if (gvInstitutionType.RowCount != 0)
            {
                if (this.InstutionTypeId != 0)
                {
                    ShowForm(this.InstutionTypeId);
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
        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcInstitutionType, this.GetMessage(MessageCatalog.NetworkingSettings.NETWORK_INS_TYPE_CAPTION), PrintType.DT, gvInstitutionType, true);
        }

        private void gcInstitutionType_DoubleClick(object sender, EventArgs e)
        {
            ShowEditInstutionTypeForm();
        }

        private void frmInstitutionTypeView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditInstutionTypeForm();
        }

        private void frmInstitutionTypeView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvInstitutionType_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvInstitutionType.RowCount.ToString();
        }

        private void frmInstitutionTypeView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }
    }
}