using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class DepreciationViewScreen : frmBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Property

        #endregion

        #region Constructor

        public DepreciationViewScreen()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void DepreciationView_Load(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
        }

        private void ucToolBarDepreciation_AddClicked(object sender, EventArgs e)
        {
            frmDepreciation Depreciation = new frmDepreciation();
            Depreciation.ShowDialog();
        }
        #endregion

        #region Methods

        /// <summary>
        /// This is to Load the Projects 
        /// </summary>
        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {

                    resultArgs = mappingSystem.FetchProjectsLookup();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                this.ShowMessageBox(Ex.Message + Environment.NewLine + Ex.Source);
            }
            finally { }
        }
        #endregion
    }
}