using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using Bosco.Utility;
using DevExpress.Utils;
using System.Text.RegularExpressions;
using ACPP.Modules.Transaction;
using DevExpress.XtraEditors.Controls;
using ACPP.Modules.Master;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors.Repository;

namespace ACPP
{
    public partial class frmFinanceBaseAdd : frmFinanceBase
    {
        public frmFinanceBaseAdd()
        {
            InitializeComponent();
        }

        #region CommonInterface Properties
        public DateEdit dtBaseDate;
        public GridLookUpEdit dtBaseProject;
        public const string PROJECTTAG = "PROJECT";
        public const string DATETAG = "DATE";
        #endregion

        private void frmFinanceBaseAdd_Load(object sender, EventArgs e)
        {
            SetEventHandlers(this);
        }

        /// <summary>
        /// Event occurs when the enter event fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessEnter(object sender, System.EventArgs e)
        {
            ((TextEdit)sender).BackColor = Color.LightBlue;
        }

        /// <summary>
        /// Event occurs when the Leave event fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessLeave(object sender, System.EventArgs e)
        {
            ((TextEdit)sender).BackColor = Color.Empty;
        }

        /// <summary>
        /// To find the focused control of its inherited form
        /// </summary>
        /// <param name="ctrlContainer"></param>
        private void SetEventHandlers(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl != null)
                {
                    if (ctrl is TextEdit)
                    {
                        ctrl.Enter += new System.EventHandler(this.ProcessEnter);
                        ctrl.Leave += new System.EventHandler(this.ProcessLeave);
                    }
                    if (ctrl is DateEdit && dtBaseDate == null)
                    {
                        dtBaseDate = ctrl as DateEdit;
                    }
                    if (ctrl is GridLookUpEdit && ctrl.Tag == "PR")
                    {
                        dtBaseProject = ctrl as GridLookUpEdit;
                    }
                    if (ctrl.HasChildren)
                    {
                        SetEventHandlers(ctrl);
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == Keys.F3)
            {
                TriggerChildControl(DATETAG);
            }
            if (KeyData == Keys.F5)
            {
                TriggerChildControl(PROJECTTAG);
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        public void TriggerChildControl(string Type)
        {
            switch (Type)
            {
                case PROJECTTAG:
                    if (dtBaseProject != null)
                    {
                        frmProjectSelection frmprojectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod,
                            UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId), this.AppSetting.UserProject);
                        frmprojectSelection.ShowDialog();
                        if (frmprojectSelection.ProjectName != string.Empty && frmprojectSelection.DialogResult == DialogResult.OK)
                        {
                            dtBaseProject.EditValue = this.AppSetting.UserProjectId;
                        }
                    }
                    break;
                case DATETAG:
                    if (dtBaseDate != null)
                    {
                        dtBaseDate.Focus();
                        dtBaseDate.Select();
                    }
                    break;
            }
        }

       

        private void frmFinanceBaseAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// On 20/10/2021, To skip Closed Ledgers
        /// </summary>
        /// <param name="dtBaseLedger"></param>
        /// <param name="isCashBankLedgers"></param>
        /// <returns></returns>
        public DataTable FilterLedgerByDateClosed(DataTable dtBaseLedger, bool isCashBankLedgers, DateTime dtDate)
        {
            DataTable dtActiveLedgers = dtBaseLedger;
            try
            {
                DataView dvledger = dtBaseLedger.AsDataView();
                //dvledger.RowFilter = "DATE_CLOSED >='" + dtTransactionDate.DateTime + "' OR DATE_CLOSED IS NULL";

                //On 20/10/2021
                //dvledger.RowFilter = "(DATE_CLOSED >='" + UtilityMember.DateSet.ToDate(dtTransactionDate.DateTime.ToShortDateString()) + "' OR DATE_CLOSED IS NULL) AND " +
                //                     "(DATE_OPENED <='" + UtilityMember.DateSet.ToDate(dtTransactionDate.DateTime.ToShortDateString()) + "' OR DATE_OPENED IS NULL)";

                //On 13/07/2018, fitler Bank Openend date also
                string datecondition = "(DATE_CLOSED >='" + UtilityMember.DateSet.ToDate(dtDate.ToShortDateString()) + "' OR DATE_CLOSED IS NULL)";
                if (isCashBankLedgers)
                {
                    datecondition += " AND " + "(DATE_OPENED <='" + UtilityMember.DateSet.ToDate(dtDate.ToShortDateString()) + "' OR DATE_OPENED IS NULL)";
                }

                //10/07/2024, If other than india country
                if (this.AppSetting.IsCountryOtherThanIndia)
                {
                    datecondition += " AND " + "(LEDGER_ID NOT IN (" + AppSetting.GetDefaultGeneralLedgersIds + "))";
                }
                dvledger.RowFilter = datecondition;
                dtActiveLedgers = dvledger.ToTable();
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }

            return dtActiveLedgers;
        }

        
    }
}
