using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using System.IO;
using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using System.Reflection;

namespace Bosco.Model.Donor
{
    public class DonorSetting:SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        string DestinationFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DonorSetting.xml"); //"DonorSetting.xml"; //Application.StartupPath.ToString()// Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        #endregion

        #region Properties
        
        #endregion

        #region Methods
        public ResultArgs WriteDonorSettingsAsXML(DataSet dsDonorSetting)
        {
            try
            {
                if (true) //Path Exists
                {
                    XMLConverter.WriteToXMLFile(dsDonorSetting, DestinationFilePath);
                   
                }
                return resultArgs; 
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}
