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
    public partial class frmLocationArea : frmBaseAdd
    {
        #region Event Decelaration
        ResultArgs resultArgs = new ResultArgs();
        public event EventHandler UpdateHeld;
        #endregion

        #region Properties
        private int AreaId { get; set; }
        #endregion

        #region Constructor
        public frmLocationArea()
        {
            InitializeComponent();
        }

        public frmLocationArea(int areaId)
            : this()
        {
            AreaId = areaId;
        }
        #endregion

        #region Events
        private void frmLocationArea_Load(object sender, EventArgs e)
        {
            AssignAreaDetails();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtArea.Text))
            {
                using (LocationSystem location = new LocationSystem())
                {
                    location.AreaId = AreaId;
                    location.Name = txtArea.Text;

                    resultArgs = location.SaveAreaDetails();
                    if (resultArgs.Success)
                    {
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(sender, e);
                        }
                        this.ShowSuccessMessage("Saved.");
                        txtArea.Text = string.Empty;
                        txtArea.Focus();
                        if (AreaId > 0) { this.Close(); }
                    }
                }
            }
            else
            {
                this.ShowMessageBoxWarning("Area Name is Empty.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtArea_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtArea);
        }
        #endregion

        #region Methods
        private void AssignAreaDetails()
        {
            if (AreaId > 0)
            {
                using (LocationSystem location = new LocationSystem())
                {
                    location.AreaId = AreaId;
                    resultArgs = location.FetchAreaById();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        txtArea.Text = resultArgs.DataSource.Table.Rows[0][location.AppSchema.ASSETLocationDetails.AREA_NAMEColumn.ColumnName].ToString();
                    }
                }
            }
        }
        #endregion
    }
}