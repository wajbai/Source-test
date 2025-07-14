using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Alerter;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;


namespace ACPP.Modules.Master
{
    public partial class frmInKindArticleAdd : frmFinanceBaseAdd
    {
        #region Event Handler
        public event EventHandler UpdateHeld;

        #endregion

        #region variable Decelartion
        ResultArgs resultArgs = null;
        private int inKindArticle = 0;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for add.
        /// </summary>

        public frmInKindArticleAdd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for Edit.
        /// </summary>
        /// <param name="InKindArticleId"></param>

        public frmInKindArticleAdd(int InKindArticleId)
            : this()
        {
            inKindArticle = InKindArticleId;
        }
        #endregion

        #region Events

        /// <summary>
        /// Load the details of the inKindArticles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmInKindArticleAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignInKindArticleDetails();
        }

        /// <summary>
        /// Save the details of the InKindArticle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInKindArticles())
                {
                    using (InKindArticleSystem inKindArticleSystem = new InKindArticleSystem(inKindArticle))
                    {
                        inKindArticleSystem.ArticleId = inKindArticle == 0 ? (int)AddNewRow.NewRow : inKindArticle;
                        inKindArticleSystem.Abbrevation = txtCode.Text.Trim().ToUpper();
                        inKindArticleSystem.Article = txtMemoArticle.Text.Trim();
                        inKindArticleSystem.OpQuantity = this.LoginUser.NumberSet.ToDouble(txtOpQuantity.Text);
                        inKindArticleSystem.OpValue = this.LoginUser.NumberSet.ToDouble(txtOpValue.Text);
                        inKindArticleSystem.Notes = txtmeNotes.Text.Trim();

                        resultArgs = inKindArticleSystem.SaveInKindArticleDetails();

                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();

                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage(resultArgs.Message);
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
        /// Set code border color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtCode_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCode);
        }

        /// <summary>
        /// Set Article Border Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMemoArticle_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtMemoArticle);
        }


        /// <summary>
        /// Close the form.
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
        /// Validate the mandatory fields of the InKind Articles.
        /// </summary>
        /// <returns></returns>

        public bool ValidateInKindArticles()
        {
            bool isInkindArticle = true;

            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.InKindArticle.INKINDARTICLE_ABBREVATION_EMPTY));
                this.SetBorderColor(txtCode);
                isInkindArticle = false;
                txtCode.Focus();
            }
            else if (string.IsNullOrEmpty(txtMemoArticle.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.InKindArticle.INKINDARTICLE_NAME_EMPTY));
                this.SetBorderColor(txtMemoArticle);
                isInkindArticle = false;
                txtMemoArticle.Focus();
            }
            else
            {
                txtOpQuantity.Focus();
            }
            return isInkindArticle;
        }

        /// <summary>
        /// Load the details while in edit mode based on the inKindArticle id.
        /// </summary>

        public void AssignInKindArticleDetails()
        {
            try
            {
                if (inKindArticle != 0)
                {
                    using (InKindArticleSystem inKindArticleSystem = new InKindArticleSystem(inKindArticle))
                    {
                        txtCode.Text = inKindArticleSystem.Abbrevation;
                        txtMemoArticle.Text = inKindArticleSystem.Article;
                        txtOpQuantity.Text = inKindArticleSystem.OpQuantity.ToString();
                        txtOpValue.Text = inKindArticleSystem.OpValue.ToString();
                        txtmeNotes.Text = inKindArticleSystem.Notes;
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
        /// set form title based on the InKindArticle Id.
        /// </summary>

        public void SetTitle()
        {
            this.Text = inKindArticle == 0 ? this.GetMessage(MessageCatalog.Master.InKindArticle.INKINDARTICLE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.InKindArticle.INKINDARTICLE_EDIT_CAPTION);
            txtCode.Focus();
        }

        /// <summary>
        /// Clear the controls after adding.
        /// </summary>

        public void ClearControls()
        {
            if (inKindArticle == 0) { txtCode.Text = txtMemoArticle.Text = txtOpQuantity.Text = txtOpValue.Text = txtmeNotes.Text = string.Empty; }
            txtCode.Focus();
        }
        #endregion
    }
}