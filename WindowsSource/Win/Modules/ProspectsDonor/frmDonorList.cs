using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility.ConfigSetting;
using Bosco.Model;
using Bosco.Model.Donor;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;

using Bosco.Utility;
using System.Reflection;
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorList : frmBase
    {
        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion
        
        #region Constructor
        public frmDonorList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private string Donationdate { get; set; }
        #endregion

        #region Methods
        private void SetTitle()
        {
            deDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        }
        private void LoadDonorList()
        {
            try
            {
                using (DonorFrontOfficeSystem donorfrontOfficesys = new DonorFrontOfficeSystem())
                {
                    resultArgs = donorfrontOfficesys.FetchNonPerformingDonors(Donationdate);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        gcDonorList.DataSource = resultArgs.DataSource.Table;
                        gcDonorList.RefreshDataSource();
                    }
                    else
                    {
                        gcDonorList.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally { }
        }
        #endregion

        #region Events
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDonorList();

            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally { }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gvDonorList.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
                if (chkShowFilter.Checked)
                {
                    this.SetFocusRowFilter(gvDonorList, colDonor);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally { }
        }

        private void frmDonorList_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadDonorList();
        }

        private void deDate_EditValueChanged(object sender, EventArgs e)
        {
            Donationdate = this.UtilityMember.DateSet.ToDate(deDate.Text, false).ToString();
        }
        #endregion
    }
}