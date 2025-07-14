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
using Bosco.Model;

namespace ACPP.Modules.Inventory
{
    public partial class frmLocationRoomNo : frmBaseAdd
    {
        #region Properties
        private int RoomId { get; set; }
        #endregion

        #region Variable Decelaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmLocationRoomNo()
        {
            InitializeComponent();
        }

        public frmLocationRoomNo(int roomId)
            : this()
        {
            RoomId = roomId;
        }
        #endregion

        #region Events
        private void frmLocationRoomNo_Load(object sender, EventArgs e)
        {
            LoadBlockDetails();
            AssignRoomValues();
        }

        private void btnRoomSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(glkpRoomBlock.Text))
            {
                if (!string.IsNullOrEmpty(glkpRoomFloor.Text))
                {
                    if (!string.IsNullOrEmpty(txtRoomNo.Text))
                    {
                        using (LocationSystem location = new LocationSystem())
                        {
                            location.RoomId = RoomId;
                            location.BlockId = this.UtilityMember.NumberSet.ToInteger(glkpRoomBlock.EditValue.ToString());
                            location.FloorId = this.UtilityMember.NumberSet.ToInteger(glkpRoomFloor.EditValue.ToString());
                            location.Name = txtRoomNo.Text;

                            resultArgs = location.SaveRoomDetails();
                            if (resultArgs.Success)
                            {
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(sender, e);
                                }
                                this.ShowSuccessMessage("Saved.");
                                glkpRoomBlock.Text = glkpRoomFloor.Text = txtRoomNo.Text = string.Empty;
                                glkpRoomBlock.Focus();
                                if (RoomId > 0) { this.Close(); }
                            }
                            else
                            {
                                this.ShowMessageBoxError(resultArgs.Message);
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBoxWarning("Room No is Empty.");
                        txtRoomNo.Focus();
                    }
                }
                else
                {
                    this.ShowMessageBoxWarning("Block Name is Empty.");
                    glkpRoomFloor.Focus();
                }
            }
            else
            {
                this.ShowMessageBoxWarning("Building is Empty.");
                glkpRoomBlock.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpRoomBlock_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpRoomBlock);
        }

        private void glkpRoomFloor_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpRoomFloor);
        }

        private void txtRoomNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRoomNo);
        }
        #endregion

        #region Methods
        private void LoadBlockDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchBlockAll();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpRoomBlock, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.BLOCK_NAMEColumn.ColumnName,
                        location.AppSchema.ASSETLocationDetails.BLOCK_IDColumn.ColumnName);
                }
            }
        }

        private void glkpRoomBlock_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpRoomBlock.EditValue != null)
            {
                glkpRoomFloor.EditValue = null;
                int BlockId = this.UtilityMember.NumberSet.ToInteger(glkpRoomBlock.EditValue.ToString());
                if (BlockId > 0)
                {
                    using (LocationSystem location = new LocationSystem())
                    {
                        location.BlockId = BlockId;
                        ResultArgs result = location.FetchFloorByBlock();
                        if (result != null && result.Success && result.DataSource.Table.Rows.Count > 0)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpRoomFloor, result.DataSource.Table, location.AppSchema.ASSETLocationDetails.FLOOR_NAMEColumn.ColumnName,
                        location.AppSchema.ASSETLocationDetails.FLOOR_IDColumn.ColumnName);
                        }
                    }
                }
            }
        }

        private void AssignRoomValues()
        {
            if (RoomId > 0)
            {
                using (LocationSystem location = new LocationSystem())
                {
                    location.RoomId = RoomId;
                    ResultArgs result = location.FetchRoomById();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpRoomBlock.EditValue = this.UtilityMember.NumberSet.ToInteger(result.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.BLOCK_IDColumn.ColumnName].ToString());
                        glkpRoomFloor.EditValue = this.UtilityMember.NumberSet.ToInteger(result.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.FLOOR_IDColumn.ColumnName].ToString());
                        txtRoomNo.Text = result.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.ROOM_NOColumn.ColumnName].ToString();
                    }
                }
            }
        }
        #endregion
    }
}