using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using Payroll.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Payroll.Model.UIModel
{
    public class SettingSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        public SettingSystem()
        {

        }

        public ResultArgs SaveSetting(DataTable dtSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.InsertPayrollSetting))
            {
                dataManager.BeginTransaction();
                string SettingName = "";
                string Value = "";
                DataTable dtSet = dtSetting;

                if (dtSet != null)
                {
                    foreach (DataRow drSetting in dtSet.Rows)
                    {
                        SettingName = drSetting[this.AppSchema.PayrollSetting.SETTING_NAMEColumn.ColumnName].ToString();
                        Value = drSetting[this.AppSchema.PayrollSetting.VALUEColumn.ColumnName].ToString();

                        dataManager.Parameters.Add(this.AppSchema.PayrollSetting.SETTING_NAMEColumn, SettingName);
                        dataManager.Parameters.Add(this.AppSchema.PayrollSetting.VALUEColumn, Value);

                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success) { break; }
                        else
                            dataManager.Parameters.Clear();
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs FetchSettingDetails(int UserID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollSetting))
            {
                dataManager.Parameters.Add(this.AppSchema.PayrollSetting.USER_IDColumn, UserID);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        
    }
}
