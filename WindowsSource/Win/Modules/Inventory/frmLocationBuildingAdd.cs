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
    public partial class frmLocationBuilding : frmBaseAdd
    {
        #region Properties
        private int BuildingId { get; set; }
        #endregion

        #region Decelaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmLocationBuilding()
        {
            InitializeComponent();
        }

        public frmLocationBuilding(int buildingId)
            : this()
        {
            BuildingId = buildingId;
        }
        #endregion

        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuildingSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(glkpBuildingArea.Text))
            {
                if (!string.IsNullOrEmpty(txtBuildingName.Text))
                {
                    using (LocationSystem location = new LocationSystem())
                    {
                        location.BuildingId = BuildingId;
                        location.AreaId = this.UtilityMember.NumberSet.ToInteger(glkpBuildingArea.EditValue.ToString());
                        location.Name = txtBuildingName.Text;

                        resultArgs = location.SaveBuildingDetails();
                        if (resultArgs.Success)
                        {
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(sender, e);
                            }
                            this.ShowSuccessMessage("Saved.");
                            glkpBuildingArea.Text = txtBuildingName.Text = string.Empty;
                            glkpBuildingArea.Focus();
                            if (BuildingId > 0) { this.Close(); }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBoxWarning("Building Name is Empty.");
                    txtBuildingName.Focus();
                }
            }
            else
            {
                this.ShowMessageBoxWarning("Area is Empty.");
                glkpBuildingArea.Focus();
            }
        }

        private void frmLocationBuilding_Load(object sender, EventArgs e)
        {
            LoadAreaDetails();
            AssignBuildingValues();
        }

        private void glkpBuildingArea_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpBuildingArea);
        }

        private void txtBuildingName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBuildingName);
        }
        #endregion

        #region Methods
        private void AssignBuildingValues()
        {
            if (BuildingId > 0)
            {
                using (LocationSystem location = new LocationSystem())
                {
                    location.BuildingId = BuildingId;
                    resultArgs = location.FetchBuildingById();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpBuildingArea.EditValue=this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.AREA_IDColumn.ColumnName].ToString());
                        txtBuildingName.Text = resultArgs.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.BUILDING_NAMEColumn.ColumnName].ToString();
                    }
                }
            }
        }

        private void LoadAreaDetails()
        {
            using (LocationSystem location = new LocationSystem())
            {
                resultArgs = location.FetchAreaAll();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBuildingArea, resultArgs.DataSource.Table, location.AppSchema.ASSETLocationDetails.AREA_NAMEColumn.ColumnName,
                        location.AppSchema.ASSETLocationDetails.AREA_IDColumn.ColumnName);
                }
            }
        }
        #endregion
    }
}