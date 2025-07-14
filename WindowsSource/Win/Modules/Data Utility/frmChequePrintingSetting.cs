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
using Bosco.Model.Setting;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmChequePrintingSetting : frmFinanceBase
    {
        private string ChequeFormat = "Default";

        public frmChequePrintingSetting()
        {
            InitializeComponent();
            loadChequePrintingSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidSetting())
            {
                using (UserSystem chequesetting = new UserSystem())
                {
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.Width.ToString(), txtChequeWidth.Text.ToString());
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.Height.ToString(), txtChequeHeight.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.DateTop.ToString(), txtDateTop.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.DateLeft.ToString(), txtDateLeft.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.DateDigitWidth.ToString(), txtDateDigitWidth.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.PartyNameTop.ToString(), txtPartyNameTop.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.PartyNameLeft.ToString(), txtPartyNameLeft.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.AmountWordsTop.ToString(), txtAmountinWordsTop.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.AmountWordsLeft.ToString(), txtAmountinWordsLeft.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.AmountTop.ToString(), txtAmountTop.Text);
                    chequesetting.SaveChequeSetting(ChequeFormat, ChequePrinting.AmountLeft.ToString(), txtAmountLeft.Text);
                    this.ShowSuccessMessage("Saved");                    
                }
            }
        }

        private void loadChequePrintingSetting()
        {
            using (UserSystem chequesetting = new UserSystem())
            {
                ResultArgs resultArg = chequesetting.FetchChequeSetting(ChequeFormat);
                if (resultArg.Success && resultArg.DataSource.Table != null)
                {
                    DataTable dtChequeSetting = resultArg.DataSource.Table;
                    foreach (DataRow dr in dtChequeSetting.Rows)
                    {
                        string settingnmae = dr["SETTING_NAME"].ToString();
                        string value = dr["SETTING_VALUE"].ToString();

                        Bosco.Utility.ChequePrinting chequeprintingsetting = Bosco.Utility.ChequePrinting.Width;
                        chequeprintingsetting = (ChequePrinting)UtilityMember.EnumSet.GetEnumItemType(typeof(ChequePrinting), settingnmae);
                        switch (chequeprintingsetting)
                        {
                            case ChequePrinting.Width:
                                txtChequeWidth.Text = value;
                                break;
                            case ChequePrinting.Height:
                                txtChequeHeight.Text = value;
                                break;
                            case ChequePrinting.DateTop:
                                txtDateTop.Text = value;
                                break;
                            case ChequePrinting.DateLeft:
                                txtDateLeft.Text = value;
                                break;
                            case ChequePrinting.DateDigitWidth:
                                txtDateDigitWidth.Text = value;
                                break;
                            case ChequePrinting.PartyNameTop:
                                txtPartyNameTop.Text = value;
                                break;
                            case ChequePrinting.PartyNameLeft:
                                txtPartyNameLeft.Text = value;
                                break;
                            case ChequePrinting.AmountWordsTop:
                                txtAmountinWordsTop.Text = value;
                                break;
                            case ChequePrinting.AmountWordsLeft:
                                txtAmountinWordsLeft.Text = value;
                                break;
                            case ChequePrinting.AmountTop:
                                txtAmountTop.Text = value;
                                break;
                            case ChequePrinting.AmountLeft:
                                txtAmountLeft.Text = value;
                                break;
                        }
                    }
                }
            }
        }

        private bool CheckValidSetting()
        {
            bool rtn = false;
            bool nullvalue = (string.IsNullOrEmpty(txtChequeWidth.Text) || string.IsNullOrEmpty(txtChequeHeight.Text) ||
                string.IsNullOrEmpty(txtDateTop.Text) || string.IsNullOrEmpty(txtDateLeft.Text) || string.IsNullOrEmpty(txtDateDigitWidth.Text) ||
                string.IsNullOrEmpty(txtPartyNameTop.Text) || string.IsNullOrEmpty(txtPartyNameLeft.Text) ||
                string.IsNullOrEmpty(txtAmountinWordsTop.Text) || string.IsNullOrEmpty(txtAmountinWordsLeft.Text) ||
                string.IsNullOrEmpty(txtAmountTop.Text) || string.IsNullOrEmpty(txtAmountTop.Text));

            if (!nullvalue)
            {

                rtn = true;
            }
            else
            {
                MessageRender.ShowMessage("Fill all the values");
                rtn = false;
            }

            return rtn;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
            report.ShowChequePrint(0);
        }
    }
}