using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Transaction;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraPrinting;
using Bosco.DAO.Schema;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Business;
using DevExpress.XtraBars;

namespace ACPP.Modules.Transaction
{
    public partial class frmVoucherTransaction : frmBaseAdd
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        private int ProjectId = 0;
        private int VoucherId = 0;
        private int LedgerId = 0;
        #endregion

        #region Constructor
        public frmVoucherTransaction()
        {
            InitializeComponent();
        }
        public frmVoucherTransaction(int projectId, int voucherId, int ledgerId)
            : this()
        {
            ProjectId = projectId;
            VoucherId = voucherId;
            LedgerId = ledgerId;
        }


        #endregion

        #region Events
        private void frmVoucherTransaction_Load(object sender, EventArgs e)
        {
            LoadProjects();
            setDefaultValues();
        }

        private void btnMoveTrans_Click(object sender, EventArgs e)
        {

        }

        private void rchkCheckProject_CheckedChanged(object sender, EventArgs e)
        {
            if (gvMoveTrans.RowCount != 1)
            {
                int projectId = gvMoveTrans.GetFocusedRowCellValue(colProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvMoveTrans.GetFocusedRowCellValue(colProjectId).ToString()) : 0;
                int status = gvMoveTrans.GetFocusedRowCellValue(colSelect) != null ? this.UtilityMember.NumberSet.ToInteger(gvMoveTrans.GetFocusedRowCellValue(colSelect).ToString()) : 0;
                MoveTransToSelectedProject(projectId, status);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        private void LoadProjects()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                projectSystem.ProjectId = ProjectId;
                resultArgs = projectSystem.FetchProjectsDetails();
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.Columns.Add("FLAG", typeof(int));
                    gcMoveTrans.DataSource = resultArgs.DataSource.Table;
                    gcMoveTrans.RefreshDataSource();
                }
            }
        }

        private DataTable MoveTransToSelectedProject(int projectId, int status)
        {
            DataTable dtMoveTrans = (DataTable)gcMoveTrans.DataSource;
            for (int i = 0; i < dtMoveTrans.Rows.Count; i++)
            {
                int ProjectId = dtMoveTrans.Rows[i]["PROJECT_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMoveTrans.Rows[i]["PROJECT_ID"].ToString()) : 0;
                if (ProjectId == projectId)
                {
                    dtMoveTrans.Rows[i]["FLAG"] = status;
                }
                else
                {
                    dtMoveTrans.Rows[i]["FLAG"] = (int)YesNo.No;
                }
            }
            return dtMoveTrans;
        }

        private void setDefaultValues()
        {
            MoveDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
        }
        #endregion



    }
}