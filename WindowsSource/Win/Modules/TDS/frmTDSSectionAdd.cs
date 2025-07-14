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
using Bosco.Model.TDS;
using DevExpress.XtraPivotGrid;

namespace ACPP.Modules.TDS
{
    public partial class frmTDSSectionAdd : frmFinanceBaseAdd
    {
        #region Events Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Declaration
        private int TDSId = 0;
        ResultArgs resultArgs = null;
        private int isActive { get; set; }
        #endregion

        #region constructor
        public frmTDSSectionAdd()
        {
            InitializeComponent();
        }
        public frmTDSSectionAdd(int SectionId)
            : this()
        {
            TDSId = SectionId;
        }
        #endregion

        #region events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateTDSDetails())
                {
                    using (TDSSectionSystem TDSSectionsystem = new TDSSectionSystem())
                    {
                        TDSSectionsystem.TDS_section_Id = TDSId == 0 ? (int)AddNewRow.NewRow : TDSId;
                        TDSSectionsystem.Code = txtCode.Text;
                        TDSSectionsystem.Name = txtName.Text;
                        TDSSectionsystem.IsActive = chkStatus.Checked ? 1 : 0;

                        resultArgs = TDSSectionsystem.SaveTDSSection();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_SAVE_SUCCESS));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTDSSectionAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignTDSSectionDetails();
        }
        #endregion

        #region Methods
        private void SetTitle()
        {
            this.Text = TDSId == 0 ? this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_SECTION_ADD_CAPTION) : this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_SECTION_EDIT_CAPTION);
            txtCode.Focus();
        }

        public void AssignTDSSectionDetails()
        {
            try
            {
                if (TDSId != 0)
                {
                    using (TDSSectionSystem TDSSectionsystem = new TDSSectionSystem(TDSId))
                    {
                        txtCode.Text = TDSSectionsystem.Code;
                        txtName.Text = TDSSectionsystem.Name;
                        chkStatus.Checked = TDSSectionsystem.IsActive.Equals((int)YesNo.Yes) ? true : false;
                        isActive = TDSSectionsystem.IsActive;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

        }

        public void ClearControls()
        {
            if (TDSId == 0)
            {
                txtCode.Text = txtName.Text = string.Empty;
                chkStatus.Checked = false;
            }
            txtCode.Focus();
        }

        public bool ValidateTDSDetails()
        {
            bool isTDSSection = true;
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_CODE_EMPTY));
                this.SetBorderColor(txtCode);
                isTDSSection = false;
                txtCode.Focus();
            }
            else if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_NAME_EMPTY));
                this.SetBorderColor(txtName);
                isTDSSection = false;
                txtName.Focus();
            }
            else
            {
                txtCode.Focus();
            }

            if (TDSId > 0)
            {
                if (IsActiveTDSSection() > 0 && chkStatus.Checked == false)
                {
                    //this.ShowMessageBox("This Section is mapped with Nature of Payments.Cannot set this inactive.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.TDSSection.TDS_CANNOT_SET_INACTIVE));
                    chkStatus.Checked = true;
                    isTDSSection = false;
                }
            }
            return isTDSSection;
        }

        private int IsActiveTDSSection()
        {
            int Count = 0;
            using (TDSSectionSystem tdsSection = new TDSSectionSystem())
            {
                tdsSection.TDS_section_Id = TDSId;
                Count = tdsSection.CheckTDSSection();
            }
            return Count;
        }
        #endregion

        private void txtCode_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCode);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
        }

    }
}
