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
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Text.RegularExpressions;
using Bosco.Model.UIModel.Master;


namespace ACPP.Modules.Master
{
    public partial class frmGST : frmFinanceBaseAdd
    {
        #region Property
        public event EventHandler updateheld;
        ResultArgs resultArgs = new ResultArgs();
        private int GSTID = 0;
        #endregion

        #region Constructor

        public frmGST()
        {
            InitializeComponent();
        }
        public frmGST(int gstId)
            : this()
        {
            GSTID = gstId;
        }

        #endregion

        #region Events
        private void frmGST_Load(object sender, EventArgs e)
        {
            LoadDefaults();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValidateGSt())
                {
                    using (GSTClassSystem gstClass = new GSTClassSystem())
                    {
                        ResultArgs resultArgs = null;
                        gstClass.GstId = GSTID == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : GSTID;
                        gstClass.Slab = txtGStSlab.Text;
                        gstClass.Gst = this.UtilityMember.NumberSet.ToDecimal(txtGSt.Text.Trim());
                        gstClass.CGst = this.UtilityMember.NumberSet.ToDecimal(txtCGSt.Text.Trim());
                        gstClass.SGst = this.UtilityMember.NumberSet.ToDecimal(txtSGSt.Text.Trim());
                        gstClass.ApplicableFrom = dtApplicableFrom.DateTime;
                        gstClass.Status = chkGSTActive.Checked ? 1 : 0;
                        resultArgs = gstClass.SaveGStDetails();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (updateheld != null)
                            {
                                updateheld(this, e);
                            }

                            if (GSTID == 0)
                            {
                                txtGStSlab.Text = txtGSt.Text = txtCGSt.Text = txtSGSt.Text = string.Empty;
                            }

                            txtGStSlab.Select();
                            txtGStSlab.Focus();
                        }
                        else
                        {
                            txtGStSlab.Select();
                            txtGStSlab.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private bool isValidateGSt()
        {
            bool isGSt = true;
            if (string.IsNullOrEmpty(txtGSt.Text.Trim()) || txtGSt.Text == "0.00" || txtGSt.Text == "0"
                    || this.UtilityMember.NumberSet.ToDouble(txtGSt.Text) < 0)
            {
                this.ShowMessageBox("GST is Empty");
                this.SetBorderColor(txtGSt);
                txtGSt.Focus();
                isGSt = false;
            }
            else if (string.IsNullOrEmpty(dtApplicableFrom.Text))
            {
                this.ShowMessageBox("Applicable From is empty");
                this.SetBorderColor(dtApplicableFrom);
                dtApplicableFrom.Focus();
                isGSt = false;
            }
            else if (this.UtilityMember.NumberSet.ToDouble(txtGSt.Text) > 0)
            {
                double gst = this.UtilityMember.NumberSet.ToDouble(txtGSt.Text);
                double cgst = this.UtilityMember.NumberSet.ToDouble(txtCGSt.Text);
                double sgst = this.UtilityMember.NumberSet.ToDouble(txtSGSt.Text);

                if ((gst / 2) != cgst || (gst / 2) != sgst)
                {
                    this.ShowMessageBox("CGST, SGST is not equally distributed");
                    this.SetBorderColor(txtCGSt);
                    txtCGSt.Focus();
                    isGSt = false;
                }
            }
            else
            {
                // to be written
            }
            return isGSt;
        }

        private void LoadDefaults()
        {
            SetTitle();
            dtApplicableFrom.Properties.MinValue = this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
            AssignGStDetails();
        }

        private void AssignGStDetails()
        {
            using (GSTClassSystem gstClassSystem = new GSTClassSystem(GSTID))
            {
                txtGStSlab.Text = gstClassSystem.Slab;
                txtGSt.Text = gstClassSystem.Gst.ToString();
                txtCGSt.Text = gstClassSystem.CGst.ToString();
                txtSGSt.Text = gstClassSystem.SGst.ToString();
                if (UtilityMember.DateSet.ToDate(gstClassSystem.ApplicableFrom.ToShortDateString()) == "01/01/0001")
                {
                    dtApplicableFrom.DateTime = this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
                }
                else
                {
                    dtApplicableFrom.DateTime = gstClassSystem.ApplicableFrom;
                }


                chkGSTActive.Checked = gstClassSystem.Status == 0 ? false : true;
            }
        }

        private void SetTitle()
        {
            this.Text = GSTID == 0 ? "Add" : "Edit";
            txtGStSlab.Focus();
        }

        #endregion

    }
}