using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.DAO.Schema;


namespace Bosco.Model.UIModel
{
    public class PurposeSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        public int ProjectId { get; set; }
        #endregion

        #region Constructor
        public PurposeSystem()
        {
        }
        public PurposeSystem(int PurposeId)
        {
            FillPurposeDetails(PurposeId);

        }
        #endregion

        #region Property
        public int PurposeId { get; set; }
        public string purposeCode { get; set; }
        public string PurposeHead { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchPurposeCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.FetchPurposeCodes))
            {
                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPurposeDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPurposeMappedDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.FetchMappedAll))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs CheckPurposeMapped()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.CheckFCPurposeMapped))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Project.CONTRIBUTION_IDColumn, PurposeId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs DeletePurposeDetails(int PurposeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SavePurposeDetails()
        {
            using (DataManager dataManager = new DataManager((PurposeId == 0) ? SQLCommand.Purposes.Add : SQLCommand.Purposes.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.Purposes.CODEColumn, purposeCode);
                dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, PurposeHead);
                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId, true);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillPurposeDetails(int PurposeId)
        {
            resultArgs = PurposeDetailsbyId(PurposeId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                purposeCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Purposes.CODEColumn.ColumnName].ToString();
                PurposeHead = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName].ToString();

            }
            return resultArgs;
        }
        public ResultArgs PurposeDetailsbyId(int purposeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, purposeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs isPurposeExists(string Purpose)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.isPurposeExists))
            {
                dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, Purpose);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;

        }


        #endregion
    }
}
