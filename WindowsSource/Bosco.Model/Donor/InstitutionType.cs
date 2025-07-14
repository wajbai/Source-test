using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Donor
{
    public class InstitutionType : SystemBase
    {
        ResultArgs resulrArgs = null;

        public int InstutionTypeId { get; set; }
        public string InstutionType { get; set; }

        public ResultArgs Save()
        {
            using (DataManager datamanager = new DataManager(InstutionTypeId == 0 ? SQLCommand.InstutionType.Add : SQLCommand.InstutionType.Update))
            {
                datamanager.Parameters.Add(AppSchema.DonorProspects.INSTITUTIONAL_TYPE_IDColumn, InstutionTypeId,true);
                datamanager.Parameters.Add(AppSchema.DonorProspects.INSTITUTIONAL_TYPEColumn, InstutionType);
                resulrArgs = datamanager.UpdateData();
            }
            return resulrArgs;
        }

        public ResultArgs FetchById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.InstutionType.FetchById))
            {
                datamanager.Parameters.Add(AppSchema.DonorProspects.INSTITUTIONAL_TYPE_IDColumn, InstutionTypeId);
                resulrArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resulrArgs;
        }

        public ResultArgs Delete()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.InstutionType.Delete))
            {
                datamanager.Parameters.Add(AppSchema.DonorProspects.INSTITUTIONAL_TYPE_IDColumn, InstutionTypeId);
                resulrArgs = datamanager.UpdateData();
            }
            return resulrArgs;
        }

        public ResultArgs FetchAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.InstutionType.FetchAll))
            {
                resulrArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resulrArgs;
        }

        public void AssignValues()
        {
            resulrArgs = FetchById();
            if (resulrArgs.Success && resulrArgs != null && resulrArgs.DataSource.Table.Rows.Count > 0)
            {
                this.InstutionTypeId = NumberSet.ToInteger(resulrArgs.DataSource.Table.Rows[0][AppSchema.DonorProspects.INSTITUTIONAL_TYPE_IDColumn.ColumnName].ToString());
                this.InstutionType = resulrArgs.DataSource.Table.Rows[0][AppSchema.DonorProspects.INSTITUTIONAL_TYPEColumn.ColumnName].ToString();
            }
        }
    }
}
