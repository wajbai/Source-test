using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.TallyMigration
{
    public class dbfHandler : IDisposable
    {
        string DB_Folder = string.Empty;
        public dbfHandler(string folder)
        {
            DB_Folder = folder;
        }

        public ResultArgs FetchBySQL(string sql)
        {
            ResultArgs resultarg = new ResultArgs();
            string constr = "Provider=VFPOLEDB.1;Data Source=" + DB_Folder ;
            try
            {
                using (OleDbConnection con = new OleDbConnection(constr))
                {
                    con.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, con);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (ds != null || ds.Tables.Count > 0)
                    {
                        resultarg.DataSource.Data = ds.Tables[0];
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                if (err.Message.ToUpper().Contains("PROVIDER IS NOT REGISTERED ON THE LOCAL MACHINE"))
                {
                    resultarg.Message = "Visual FoxPro Driver is not installed in local system, Try to Install VFPOLEDBSetup.msi";
                }
                else
                {
                    resultarg.Message = err.Message;
                }
            }
            return resultarg;
        }

        public ResultArgs FetchByTblName(string dbfname)
        {
            ResultArgs resultarg = new ResultArgs();
            string constr = "Provider=VFPOLEDB.1;Data Source=" + DB_Folder;
            try
            {
                using (OleDbConnection con = new OleDbConnection(constr))
                {
                    con.Open();
                    string strQuery = "SELECT * FROM [" + dbfname.ToLower() + "]";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, con);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (ds != null || ds.Tables.Count > 0)
                    {
                        resultarg.DataSource.Data = ds.Tables[0];
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                if (err.Message.ToUpper().Contains("PROVIDER IS NOT REGISTERED ON THE LOCAL MACHINE"))
                {
                    resultarg.Message = "Visual FoxPro Driver is not installed in local system, Try to Install VFPOLEDBSetup.msi";
                }
                else
                {
                    resultarg.Message = err.Message;
                }
            }
            return resultarg;
        }

        public virtual void Dispose()
        {
            GC.Collect();
        }
    }
}
