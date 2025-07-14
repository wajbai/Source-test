using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;


namespace Bosco.Utility
{
    public class GeneralLogger
    {
        public class TallyMigration
        {
            static string FILE_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TallyLog.txt");

            public static void WriteLog(string Message, string MethodName = "", string TargetLine = "", string StackTrace = "", bool DetailedMessage = false)
            {
                try
                {
                    string msg = string.Empty;
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(FILE_PATH,true))
                    {

                        if (!DetailedMessage)
                        {
                            msg = String.Format("Tally Logger: {0} ", Message);
                        }
                        else
                        {
                            msg = "Date:        =>" + DateTime.Now.ToShortDateString() + Environment.NewLine +
                                  "Error Msg =>" + Message + Environment.NewLine +
                                  "Method Name:   =>" + StackTrace + Environment.NewLine +
                                  "Targeted Line: =>" + TargetLine + Environment.NewLine +
                                  "Stack Trace:   =>" + MethodName;
                        }
                        file.WriteLine(msg);
                        file.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("Error in Logger itself: " + ex.Message);
                }
                finally { }
            }

            public static void ClearErrorLog()
            {
                try
                {
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
                    WriteLog("Error in Logger itself: " + ex.Message);
                }
                finally { }
            }
        }


    }
}
