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

namespace ACPP.Modules
{
    public partial class frmChequePrintingSetting : frmFinanceBase
    {
        private Int32 BankId = 0;

        public frmChequePrintingSetting()
        {
            InitializeComponent();

        }
        public frmChequePrintingSetting(Int32 bankid, string bankname, string branch):this()
        {
            lcgrp.Text = bankname + " (" + branch + ")";
            BankId = bankid;
            loadChequePrintingSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave_ClickExtracted();
        }


        private bool btnSave_ClickExtracted()
        {
            bool rtn = false;
            if (CheckValidSetting())
            {
                using (UserSystem chequesetting = new UserSystem())
                {
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.Width.ToString(), txtChequeWidth.Text.ToString());
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.Height.ToString(), txtChequeHeight.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.DateTop.ToString(), txtDateTop.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.DateLeft.ToString(), txtDateLeft.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.DateDigitWidth.ToString(), txtDateDigitWidth.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.PartyNameTop.ToString(), txtPartyNameTop.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.PartyNameLeft.ToString(), txtPartyNameLeft.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.AmountWordsTop.ToString(), txtAmountinWordsTop.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.AmountWordsLeft.ToString(), txtAmountinWordsLeft.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.AmountTop.ToString(), txtAmountTop.Text);
                    chequesetting.SaveChequeSetting(BankId, ChequePrinting.AmountLeft.ToString(), txtAmountLeft.Text);
                    rtn = true;
                    this.ShowSuccessMessage("Saved");
                }
            }
            return rtn;
        }
        
        private void loadChequePrintingSetting()
        {
            using (UserSystem chequesetting = new UserSystem())
            {
                ResultArgs resultArg = chequesetting.FetchChequeSetting(BankId);

                //If there is no settting for given bank, it will retrivew default cheque setting
                if (resultArg.Success && resultArg.DataSource.Table != null && resultArg.DataSource.Table.Rows.Count==0)
                {
                    if (this.ShowConfirmationMessage("Cheque Setting is not available, Do you want to load default setting ?", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        resultArg = chequesetting.FetchChequeSetting(0);
                    }
                }

                if (resultArg.Success && resultArg.DataSource.Table != null)
                {
                    DataTable dtChequeSetting = resultArg.DataSource.Table;
                    foreach (DataRow dr in dtChequeSetting.Rows)
                    {
                        string settingnmae = dr[chequesetting.AppSchema.ChequePrinting.SETTING_NAMEColumn.Caption].ToString();
                        string value = dr[chequesetting.AppSchema.ChequePrinting.SETTING_VALUEColumn.Caption].ToString();

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
            bool rtn = false; //string.IsNullOrEmpty(txtDateDigitWidth.Text) ||
            bool nullvalue = (string.IsNullOrEmpty(txtChequeWidth.Text) || string.IsNullOrEmpty(txtChequeHeight.Text) ||
                string.IsNullOrEmpty(txtDateTop.Text) || string.IsNullOrEmpty(txtDateLeft.Text) || 
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
                txtChequeWidth.Select();
                txtChequeWidth.Focus();
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
            bool chequesettingavilable = false;

            //Check Bank setting available
            if (btnSave_ClickExtracted())
            {
                using (UserSystem chequesetting = new UserSystem())
                {
                    ResultArgs resultarg = chequesetting.FetchChequeSetting(BankId);
                    chequesettingavilable = (resultarg.Success && resultarg.DataSource.Table != null && resultarg.DataSource.Table.Rows.Count > 0);
                }

                if (chequesettingavilable)
                {
                    Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                    report.ShowChequePrint(0, BankId);
                }
                else
                {
                    MessageRender.ShowMessage("Cheque Printing setting is not available");
                }
            }
        }
    }
}