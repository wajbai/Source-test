using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model
{
    public class UISettingSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        public UISettingSystem()
        {
        }

        public ResultArgs SaveUISettingDetails(FinanceSetting financesetting, string value, String userlogin)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.InsertUpdateUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, financesetting.ToString());
                dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, value);
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, userlogin);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        
        public ResultArgs SaveUISettingDetails(DataTable dvSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.InsertUpdateUI))
            {
                for (int i = 0; i < dvSetting.Rows.Count; i++)
                {
                    dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, dvSetting.Rows[i][1]);
                    dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, dvSetting.Rows[i][2]);
                    dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, dvSetting.Rows[i][3]);

                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        //Keep the setting Info into session
                        this.UISettingInfo = dvSetting.DefaultView;
                    }
                    else
                    {
                        this.UISettingInfo = null;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteUISettingDetails(int UId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.DeleteUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchUISettingDetails(int UserID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.FetchUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn,UserID);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }


        /// <summary>
        /// If multi database enabled licnese, take base database setting information 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public ResultArgs BaseAcmeerpFetchUISettingDetails(int UserID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.BaseAcmeerpFetchUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UserID);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        /// <summary>
        /// If multi database enabled licnese, updae base database setting information 
        /// </summary>
        /// <param name="financesetting"></param>
        /// <param name="value"></param>
        /// <param name="userlogin"></param>
        /// <returns></returns>
        public ResultArgs BaseAcmeerpSaveUISettingDetails(FinanceSetting financesetting, string value, String userlogin)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.BaseAcmeerpInsertUpdateUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, financesetting.ToString());
                dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, value);
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, userlogin);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
    }
}
