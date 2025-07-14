using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Reflection;
using System.Configuration;
using Bosco.Utility;

namespace AcMEDSync
{
    public static class AcMEDataSynLog
    {
        public static string ACPERP_DSYNC_LOG = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DsynLog.txt");
        private static Int32 ACPERP_DSYNC_LOG_RESET_FILE_SIZE = 2; //define file size in mb

        #region Write Log
        public static void WriteLog(string msg)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(ACPERP_DSYNC_LOG, true))
                {
                    if (msg.Replace("-", "").Length > 0)
                    {
                        msg = (DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")) + " || " + msg;
                    }
                    sw.WriteLine(msg);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in writing log " + ex.Message);
            }
        }

        public static void ClearLog()
        {
            try
            {
                if (File.Exists(ACPERP_DSYNC_LOG))
                {
                    using (var file = new FileStream(ACPERP_DSYNC_LOG, FileMode.Truncate))
                    {
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in clearing log " + ex.Message);
            }
        }
        
        /// <summary>
        /// On 23/01/2018, When datasync log size reaches defined size (in mb), it will clear or reset the file
        /// </summary>
        public static void ClearLogbySize()
        {
            try
            {
                if (File.Exists(ACPERP_DSYNC_LOG))
                {
                    double len = new FileInfo(ACPERP_DSYNC_LOG).Length;
                    double size = Math.Round((double)len / (1024 * 1024)); //Convert size to mb
                    if (size >= ACPERP_DSYNC_LOG_RESET_FILE_SIZE)
                    {
                        ClearLog();
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Could not clear data sync log file," + err.Message);
            }
        }
        #endregion

    }
}
