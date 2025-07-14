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
    public partial class frmLocationFloor : frmBaseAdd
    {
        #region Properties
        private int FloorId { get; set; }
        #endregion

        #region Variable Decelaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmLocationFloor()
        {
            InitializeComponent();
        }

        public frmLocationFloor(int floorId)
            : this()
        {
            FloorId = floorId;
        }
        #endregion

        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFloorSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(glkpFloorBlock.Text))
            {
                if (!string.IsNullOrEmpty(txtFloor.Text))
                {
                    using (LocationSystem location = new LocationSystem())
                    {
                        location.FloorId = FloorId;
                        location.BlockId = this.UtilityMember.NumberSet.ToInteger(glkpFloorBlock.EditValue.ToString());
                        location.Name = txtFloor.Text;

                        resultArgs = location.SaveFloorDetails();
                        if (resultArgs.Success)
                        {
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(sender, e);
                            }
                            this.ShowSuccessMessage("Saved.");
                            glkpFloorBlock.Text = txtFloor.Text = string.Empty;
                            glkpFloorBlock.Focus();
                            if (FloorId > 0) { this.Close(); }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBoxWarning("Floor is Empty.");
                    txtFloor.Focus();
                }
            }
            else
            {
                this.ShowMessageBoxWarning("Block is Empty.");
                glkpFloorBlock.Focus();
            }
        }

        private void frmLocationFloor_Load(object sender, EventArgs e)
        {
            LoadBlockDetails();
            Assignvalues();
        }

        private void glkpFloorBlock_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpFloorBlock);
        }

        private void txtFloor_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtFloor);
        }
        #endregion

        #region Methods
        private void Assignvalues()
        {
            if (FloorId > 0)
            {
                using (LocationSystem location = new LocationSystem())
                {
                    location.FloorId = FloorId;
                    resultArgs = location.FetchFloorById();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpFloorBlock.EditValue = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.BLOCK_IDColumn.ColumnName].ToString());
                        txtFloor.Text = resultArgs.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.FLOOR_NAMEColumn.ColumnName].ToString();
                    }
                }
            }
        }

        private void LoadBlockDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchBlockAll();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFloorBlock, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.BLOCK_NAMEColumn.ColumnName,
                        location.AppSchema.ASSETLocationDetails.BLOCK_IDColumn.ColumnName);
                }
            }
        }
        #endregion
    }
}