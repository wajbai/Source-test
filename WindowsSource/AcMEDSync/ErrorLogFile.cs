using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Reflection;

namespace AcMEDSync
{
    public static class ErrorLogFile
    {
        private static string ERRORFILENAME = "\\AcMEDataSynGeneralLog.txt";
        private static string FILE_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + ERRORFILENAME);

        #region Write Error Log
        public static void WriteErrorLog(string Message, string MethodName = null, string TargetLine = null, string StackTrace = null, bool isMessage = false)
        {
            string msg = string.Empty;
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(FILE_PATH, true))
                {
                    if (!isMessage)
                    {
                        msg = String.Format("Confirmation Msg:{0}" , Message);
                    }
                    else
                    {
                        msg += "Date:        =>" + DateTime.Now.ToShortDateString() + Environment.NewLine +
                             "Error Message: =>" + Message + Environment.NewLine +
                             "Method Name:   =>" + StackTrace + Environment.NewLine +
                             "Targeted Line: =>" + TargetLine + Environment.NewLine +
                             "Stack Trace:   =>" + MethodName;
                    }
                    streamWriter.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                ErrorLogFile.WriteErrorLog(ex.Message, ex.Source.ToString(), ex.TargetSite.ToString(), ex.StackTrace.ToString());
            }
            finally { }
        }

        public static void ClearErrorLog()
        {
            try
            {
                // string ClearErrorFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ERRORFILENAME);
                if (File.Exists(FILE_PATH))
                {
                    using (var file = new FileStream(FILE_PATH, FileMode.Truncate))
                    {
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogFile.WriteErrorLog(ex.Message, ex.Source.ToString(), ex.TargetSite.ToString(), ex.StackTrace.ToString());
            }
            finally { }
        }
        #endregion

    }
}
