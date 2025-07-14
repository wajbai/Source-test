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
using Bosco.Model;
using System.Xml;

namespace ACPP.Modules.Inventory
{
    public partial class frmBlockAdd : frmBaseAdd
    {
        #region Events Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Declaration
        int BlockId = 0;
        public string Block = string.Empty;
        #endregion

        #region Constructor
        public frmBlockAdd()
        {
            InitializeComponent();
        }
        public frmBlockAdd(int blockId)
            : this()
        {
            BlockId = blockId;
        }

        #endregion

        #region Events
        /// <summary>
        /// To load the Block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBlockAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignBlockDetails();
        }

        /// <summary>
        /// To Save Block Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateBlockDetails())
                {
                    using (DataManager dm = new DataManager())
                    {
                        ResultArgs resultArgs = null;
                        using (BlockSystem Blocksystem = new BlockSystem())
                        {
                            Blocksystem.BlockId = BlockId == 0 ? (int)AddNewRow.NewRow : BlockId;
                            Blocksystem.Block = txtBlock.Text.Trim();
                            resultArgs = Blocksystem.SaveBlockDetails();
                            if (resultArgs.Success)
                            {

                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(this, e);
                                }
                                Block = txtBlock.Text.Trim();
                                ClearControl();
                                txtBlock.Focus();
                            }
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
        /// To Set Color for 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlock_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtBlock);
            txtBlock.Text = this.UtilityMember.StringSet.ToSentenceCase(txtBlock.Text);
        }


        /// <summary>
        /// Close the block
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
        /// To Validate the block
        /// </summary>
        /// <returns></returns>
        public bool ValidateBlockDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtBlock.Text.Trim()))
            {
                this.ShowMessageBox(GetMessage(MessageCatalog.Asset.Block.ASSETBLOCK_NAME_EMPTY));
                this.SetBorderColor(txtBlock);
                isValue = false;
                txtBlock.Focus();
            }
            return isValue;
        }

        /// <summary>
        /// To Clear the block
        /// </summary>
        private void ClearControl()
        {
            if (BlockId == 0)
            {
                txtBlock.Text = string.Empty;
            }
            txtBlock.Focus();
        }

        /// <summary>
        /// To Set the Caption for block
        /// </summary>
        private void SetTitle()
        {
            this.Text = BlockId == 0 ? this.GetMessage(MessageCatalog.Asset.Block.ASSETBLOCK_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Block.ASSETBLOCK_EDIT_CAPTION);
            txtBlock.Focus();
        }

        /// <summary>
        /// To Assign the block
        /// </summary>
        public void AssignBlockDetails()
        {
            try
            {
                if (BlockId != 0)
                {
                    using (BlockSystem blocksystem = new BlockSystem(BlockId))
                    {
                        txtBlock.Text = blocksystem.Block;
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