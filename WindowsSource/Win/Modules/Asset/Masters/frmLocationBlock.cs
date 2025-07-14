using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Asset;
using Bosco.Utility;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmLocationBlock : frmBaseAdd
    {
        #region Decelaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Properties
        private int BlockId { get; set; }
        #endregion

        #region Constructor
        public frmLocationBlock()
        {
            InitializeComponent();
        }

        public frmLocationBlock(int blockId)
            : this()
        {
            BlockId = blockId;
        }
        #endregion

        #region Events
        private void frmLocationBlock_Load(object sender, EventArgs e)
        {
            LoadBuildingDetails();
            AssignBlockValues();
        }

        private void btnBlockSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(glkpBlockBuilding.Text))
            {
                if (!string.IsNullOrEmpty(txtBlock.Text))
                {
                    using (LocationSystem location = new LocationSystem())
                    {
                        location.BlockId = BlockId;
                        location.BuildingId = this.UtilityMember.NumberSet.ToInteger(glkpBlockBuilding.EditValue.ToString());
                        location.Name = txtBlock.Text;

                        resultArgs = location.SaveBlockDetails();
                        if (resultArgs.Success)
                        {
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(sender, e);
                            }
                            this.ShowSuccessMessage("Saved.");
                            glkpBlockBuilding.Text = txtBlock.Text = string.Empty;
                            glkpBlockBuilding.Focus();
                            if (BlockId > 0)
                            {
                                this.Close();
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBoxWarning("Block Name is Empty.");
                    glkpBlockBuilding.Focus();
                }
            }
            else
            {
                this.ShowMessageBoxWarning("Building is Empty.");
                txtBlock.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Events
        private void AssignBlockValues()
        {
            if (BlockId > 0)
            {
                using (LocationSystem location = new LocationSystem())
                {
                    location.BlockId = BlockId;
                    resultArgs = location.FetchBlockById();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpBlockBuilding.EditValue = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.BUILDING_IDColumn.ColumnName].ToString());
                        txtBlock.Text = resultArgs.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.BLOCK_NAMEColumn.ColumnName].ToString();
                    }
                }
            }
        }

        private void LoadBuildingDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchBuildingAll();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBlockBuilding, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.BUILDING_NAMEColumn.ColumnName,
                        location.AppSchema.ASSETLocationDetails.BUILDING_IDColumn.ColumnName);
                }
            }
        }

        private void glkpBlockBuilding_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpBlockBuilding);
        }

        private void txtBlock_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBlock);
        }
        #endregion
    }
}