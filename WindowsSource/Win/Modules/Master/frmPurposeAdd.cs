/*  Class Name      : frmPurposeAdd
 *  Purpose         : To Save FC _Purpose Details
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Xml;
namespace ACPP.Modules.Master
{
    public partial class frmPurposeAdd : frmFinanceBaseAdd
    {
        #region EventsDeclaration
        public event EventHandler UpdataHeld;
        #endregion

        #region VariableDeclaration
        private int purposeId = 0;
        #endregion

        #region Constructor
        public frmPurposeAdd()
        {
            InitializeComponent();
        }
        public frmPurposeAdd(int PurposeId)
            : this()
        {
            purposeId = PurposeId;
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the Title and fetch assigning Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPurposeAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignPurposeDetails();
        }

        /// <summary>
        /// To Save the Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatePurposeDetails())
                {
                    ResultArgs resultArgs = null;
                        using (PurposeSystem purposeSystem = new PurposeSystem())
                        {
                            purposeSystem.PurposeId = purposeId == 0 ? (int)AddNewRow.NewRow : purposeId;
                            purposeSystem.purposeCode = txtCode.Text.Trim();
                            purposeSystem.PurposeHead = txtPurpose.Text.Trim();
                            resultArgs = purposeSystem.SavePurposeDetails();
                            if (resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                if (UpdataHeld != null)
                                {
                                    UpdataHeld(this, e);
                                }
                                ClearControl();
                                txtCode.Focus();
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Set Border Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPurpose_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtPurpose);
        }

        /// <summary>
        /// To Close the Purpose Add form
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
        /// To Validate the Purpose Details
        /// </summary>
        /// <returns></returns>
        public bool ValidatePurposeDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtPurpose.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Purposes.PURPOSE_HEAD_EMPTY));
                this.SetBorderColor(txtPurpose);
                isValue = false;
                txtPurpose.Focus();
            }
            return isValue;
        }

        /// <summary>
        /// To Clear the Purpose after Entering 
        /// </summary>
        private void ClearControl()
        {
            if (purposeId == 0)
            {
                txtCode.Text = txtPurpose.Text = string.Empty;
            }
            txtCode.Focus();
        }

        /// <summary>
        /// To Set the Title for add and edit form
        /// </summary>
        private void SetTitle()
        {
            this.Text = purposeId == 0 ? this.GetMessage(MessageCatalog.Master.Purposes.PURPOSE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Purposes.PURPOSE_EDIT_CAPTION);
            txtCode.Focus();
        }

        /// <summary>
        /// To assign the Details to the Controls
        /// </summary>
        public void AssignPurposeDetails()
        {
            try
            {
                if (purposeId != 0)
                {
                    using (PurposeSystem purposeSystem = new PurposeSystem(purposeId))
                    {
                        txtCode.Text = purposeSystem.purposeCode;
                        txtPurpose.Text = purposeSystem.PurposeHead;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion
    }
}
