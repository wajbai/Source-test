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
    public partial class frmInsuranceVouView : frmBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmInsuranceVouView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Load Insurance Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInsuranceVouView_Load(object sender, EventArgs e)
        {
            LoadProject();
        }
        private void ucInsuranceView_AddClicked(object sender, EventArgs e)
        {
            frmInsuranceVoucher frmVoucher = new frmInsuranceVoucher();
            frmVoucher.ShowDialog();
        }

        /// <summary>
        /// Editvalue Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectName = glkpProject.SelectedText.ToString();
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
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
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                        glkpProject.EditValue = (ProjectId != 0) ? ProjectId : glkpProject.Properties.GetKeyValue(0);
                        this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
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
        #endregion


    }
}