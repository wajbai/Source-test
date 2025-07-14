using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using System.Collections;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using System.Xml;

namespace ACPP.Modules.Master
{

    public partial class frmLedgerGroupAdd : frmFinanceBaseAdd
    {
        #region Variables
        private ResultArgs resultArgs;
        private int LedgerParentId = 0;
        private int code = 0;
        public int SortOrder = 0;
        public int GetSortOrder = 0;
        FormMode FrmMode;
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor
        public frmLedgerGroupAdd()
        {
            InitializeComponent();
        }

        public frmLedgerGroupAdd(int LedParId, FormMode Mode)
            : this()
        {
            LedgerParentId = LedParId;
            FrmMode = Mode;
            AssignLedgerGroupDetails();
        }
        #endregion

        #region Page Events

        /// <summary>
        /// Load the details of ledger group.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmLedgerGroupAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadLedgerGroup();
        }

        /// <summary>
        /// To save the Group details in MASTER_LEDGER_GROUP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
        {
            // this.ShowWaitDialog();
            if (ValidateGroupDetails())
            {
                try
                {
                    using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                    {
                        ledgerSystem.Abbrevation = txtLedgerCode.Text.Trim().ToUpper();
                        ledgerSystem.Group = txtLedgerName.Text.Trim();
                        ledgerSystem.ParentGroupId = this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString());
                        ledgerSystem.NatureId = ledgerSystem.GetNatureId(this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString()));
                        ledgerSystem.MainGroupId = this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString());
                        ledgerSystem.GroupId = (FrmMode == FormMode.Add) ? (int)AddNewRow.NewRow : LedgerParentId;
                        ledgerSystem.ImageId = 1;
                        ledgerSystem.SortOrder = SortOrder = ledgerSystem.FetchSortOrder();
                        if (SortOrder != 0 && ledgerSystem.ParentGroupId.Equals(ledgerSystem.NatureId))
                        {
                            ledgerSystem.SortOrder = GenerateSortOrder(SortOrder);
                        }
                        else if (!ledgerSystem.ParentGroupId.Equals(ledgerSystem.NatureId))
                        {
                            if (SortOrder == 0)
                            {
                                SortOrder = ledgerSystem.FetchMainGroupSortOrder();
                            }
                            ledgerSystem.SortOrder = GenerateSortOrderSquence(SortOrder);
                        }
                        resultArgs = ledgerSystem.SaveLedgerGroupDetails();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            txtLedgerCode.Focus();
                            UpdateImageIndex();
                            LoadLedgerGroup();
                            if (FrmMode == FormMode.Add)
                            {
                                txtLedgerCode.Text = txtLedgerName.Text = string.Empty;
                            }
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageRender.ShowMessage(Ex.Message);
                }
                finally
                {
                    //  this.CloseWaitDialog();
                }
            }
        }

        /// <summary>
        /// Generate Numbers
        /// </summary>
        /// <param name="GetSortOrder"></param>
        /// <returns></returns>
        public int GenerateSortOrderSquence(int GetSortOrder)
        {
            SortOrder = GetSortOrder + 1;
            return SortOrder;
        }
        /// <summary>
        /// Generate Numbers 
        /// </summary>
        /// <param name="GetNatureSortOrder"></param>
        /// <returns></returns>
        public int GenerateSortOrder(int GetNatureSortOrder)
        {
            SortOrder = GetNatureSortOrder + 100;
            return SortOrder;
        }

        /// <summary>
        /// Set border color for mandatory fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtLedgerName);
            txtLedgerName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtLedgerName.Text);
        }

        /// <summary>
        /// close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region Methods

        /// <summary>
        /// To load the Ledger Group
        /// </summary>

        private void LoadLedgerGroup()
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.LoadLedgerGroupSource();
                    //lkpLedgerGroup.Properties.DataSource = null ;
                    // lkpLedgerGroup.Properties.Columns.Clear();                   
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpLedgerGroup, resultArgs.DataSource.Table, ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(), ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ToString());
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }



        /// <summary>
        /// To set the form title in runtime
        /// </summary>

        private void SetTitle()
        {
            this.Text = (FrmMode == FormMode.Add) ? this.GetMessage(MessageCatalog.Master.Group.GROUP_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Group.GROUP_EDIT_CAPTION);
        }

        /// <summary>
        /// To assign the details in the clrts
        /// </summary>

        private void AssignLedgerGroupDetails()
        {
            if (FrmMode == FormMode.Edit)
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem(LedgerParentId))
                {
                    txtLedgerCode.Text  = ledgerSystem.Abbrevation;
                    txtLedgerName.Text = ledgerSystem.Group;
                    lkpLedgerGroup.EditValue = ledgerSystem.ParentGroupId.ToString();
                    // code =Convert.ToInt32(ledgerSystem.Abbrevation);
                }
            }
            else
            {
                lkpLedgerGroup.EditValue = LedgerParentId.ToString();
            }
        }

        /// <summary>
        /// To ValidateGroupDetails
        /// </summary>
        /// <returns></returns>

        private bool ValidateGroupDetails()
        {
            bool IsGroudValid = true;
            if (string.IsNullOrEmpty(txtLedgerName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Group.GROUP_NAME_EMPTY));
                this.SetBorderColor(txtLedgerName);
                IsGroudValid = false;
                txtLedgerName.Focus();
            }
            else if (lkpLedgerGroup.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Group.GROUP_PARENT_EMPTY));
                this.SetBorderColor(lkpLedgerGroup);
                IsGroudValid = false;
                lkpLedgerGroup.Focus();
            }
            else
            {
                if (!ValidateGroupLevel())
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Group.GROUP_LEVEL_CHECK));
                    IsGroudValid = false;
                    lkpLedgerGroup.Focus();
                }
            }
            return IsGroudValid;
        }

        private bool ValidateGroupLevel()
        {
            bool IsGroupLevel = true;
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                resultArgs = ledgerSystem.ValidateGroupId(this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString()));
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    if (resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ColumnName].ToString() != resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString())
                    {
                        IsGroupLevel = false;
                    }
                }
            }
            return IsGroupLevel;
        }

        private void UpdateImageIndex()
        {
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                resultArgs = ledgerSystem.UpdateImageIndex(this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString()));
            }
        }
        #endregion
    }
}
