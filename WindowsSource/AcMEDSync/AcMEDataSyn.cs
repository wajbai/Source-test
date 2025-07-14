using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Data;
using DevExpress.XtraEditors;
using System.Reflection;
using System.Net.Mail;
using System.Net;
using System.Configuration;


using Bosco.Utility;
using AcMEDSync.Model;

namespace AcMEDSync
{
    public class AcMEDataSyn : IAcMEDataSyn
    {
        #region Decelaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public AcMEDataSyn()
        {

        }
        #endregion

        #region Import Vouchers
        public ResultArgs SynchronizeVouchers(string VoucherXml)
        {
            try
            {
                using (ImportVoucherSystem importSystem = new ImportVoucherSystem())
                {
                    resultArgs = importSystem.ImportVouchers(VoucherXml);
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.ToString());
            }
            finally { }
            return resultArgs;
        }
        #endregion
    }
}
