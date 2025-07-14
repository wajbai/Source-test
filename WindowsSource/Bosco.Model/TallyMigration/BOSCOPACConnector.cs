/*
* This class is used to get data from BOSCOPAC application and migrate by using Microsoft Visual FoxPro OLEDB Provider
 * 
 * dbf database
 * 
 * 1. House         : Branch
 * 2. Trust         : Projects
 * 3. Accounts      : Ledger Group (If grphead = 1)
 * 4. Accounts      : Ledger (If grphead = 0)
 * 5. lmain, lacdet : List of Donors (if FCamount and currency are not empty in voucher, treat as FC entries
 *                      Consider "paid to" as  donor name.
 *                      IF If there is donor name is empty but FC details found, we fix donor name is Unknown) 
 * 6. lmain, lacdet : List of Countries 
 * 7. lmain, lacdet : Master Voucher, Voucher Detials and FC details 
 *                   (In BOSCOPAC, FC purpose is not avilable, so we fixed as  "Provision of free clothing / food to the poor, needy and destitute")
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using System.IO;

namespace Bosco.Model.TallyMigration
{
    public class BOSCOPACConnector : IDisposable
    {
        public string MSG_TITLE = "BOSCOPAC Viewer";
        public enum BOSCOPACMasters
        {
            House,
            Activities,
            Groups,
            Ledgers,
            Donors,
            Country,
            Voucher,
        }

        private string acyear = "1718"; //Fixed Active Finance Year As 2017-2018
        private string boscopac_base_path = string.Empty;
        private string boscopac_ac_path = string.Empty;

        public BOSCOPACConnector(string basepath)
        {
            boscopac_base_path = basepath;
            boscopac_ac_path = Path.Combine(basepath, Path.Combine("ACCOUNTS", acyear));
        }

        /// <summary>
        /// This method is going to check all mandatory db files are avilabe in the selected path
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckValidBOSCOPACPath()
        {
            ResultArgs resultarg = new ResultArgs();
            bool alldbfexists =  File.Exists(Path.Combine(boscopac_base_path,  "house.DBF")) &&
                                 File.Exists(Path.Combine(boscopac_base_path, "trust.DBF")) &&
                                 File.Exists(Path.Combine(boscopac_ac_path, "lmain.dbf")) &&
                                 File.Exists(Path.Combine(boscopac_ac_path, "lacdet.dbf"));

            resultarg.Success = alldbfexists;
            if (!resultarg.Success)
            {
                resultarg.Message = "Not valid BOSCOPAC path or A/c year (2017-2018) is not found";
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of houses from BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchHouses()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_base_path))
                {
                    //Cast(h.Code as int)  
                    string sql = "SELECT h.code, h.house, h.Region, TRIM(h.Add1) as Add1, TRIM(h.Add2) as Add2, " +
                                    "TRIM(h.Add3) as Add3, TRIM(h.Add4) as Add4, h.sdate , h.SocDate, h.TrustNo, h.TrustDate, h.fcrno, h.fcrdate," +
                                    "h.pan, h.tan from house h " +
                                    "ORDER BY h.house";

                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of Activities with house from BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchActivities()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_base_path))
                {
                    string sql = "SELECT h.code, t.tcode, t.prefix, t.activity, h.house FROM trust t " +
                                    "INNER JOIN house h on h.code = t.code " +
                                    "ORDER BY t.activity, h.house";
                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of Ledger Group from BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchLedgerGroup()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_ac_path))
                {
                    string sql = "SELECT a.code, IIF(a.account = 'CASH ON HAND', 'Cash-in-hand', account) as account," +
                                    "IIF(a.account='CASH ON HAND','Current Assets', IIF(a.group = 'INCOME', 'INCOMES', IIF(a.group = 'EXPENSE', 'EXPENSES', a.group))) as group " +
                                    "FROM accounts a "+
                                    "WHERE a.grphead = 1 AND a.code > 4 " +
                                    "ORDER BY a.group, a.account, a.code";
                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of Ledgers from BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchLedger()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_ac_path))
                {
                    //string sql = "SELECT a.hcode, a.tcode, a.code, IIF(a.account ='CASH ON HAND','Cash', a.account) as account, " +
                    //                "IIF(a.group = 'CASH ON HAND', 'Cash-in-hand', a.group) as group, a.amount, dbcr, " +
                    //                "a.accname, a.bankname, a.branch, a.pincode "+
                    //                "FROM accounts a " +
                    //                "WHERE a.grphead = 0 " +  //+ "WHERE a.grphead = 0 and a.code>0 " +
                    //                "ORDER BY a.account";

                    //a.accname On 28/08/2017, Field "accname" is not availble in some boscopac dbs
                    string sql = "SELECT a.hcode, a.tcode, a.code, IIF(a.account ='CASH ON HAND','Cash', a.account) as account, " +
                                    "IIF(a.group = 'CASH ON HAND', 'Cash-in-hand', a.group) as group, a.amount, dbcr " +
                                    "FROM accounts a " +
                                    "WHERE a.grphead = 0 " +  //+ "WHERE a.grphead = 0 and a.code>0 " +
                                    "ORDER BY a.account";
                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of Donor from BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchDonor()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_ac_path))
                {
                    string sql = "SELECT m.hcode, m.tcode, m.paidto, m.iotype, m.fcamount, m.country, fadd1, fadd2, fadd3, fadd4 " +
                                    "FROM lmain m " +
                                    "WHERE m.fcamount > 0 " + //AND TRIM(m.country) <> '' 
                                    "ORDER BY m.paidto";
                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of Country from BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchCountry()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_ac_path))
                {
                    string sql = "SELECT m.hcode, m.tcode, m.country, m.fcurr " +
                                    "FROM lmain m " +
                                    "WHERE TRIM(m.country) <> ''" +
                                    "ORDER BY m.country";
                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of Master vouchers from BOSCOPAC
        /// </summary>
        /// <returns></returns> 
        public ResultArgs FetchMasterVouchers()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_ac_path))
                {
                    string sql = "SELECT m.hcode, m.tcode, m.vtype, m.genno, m.vno, m.prefix, m.vdate, " +
                                    "m.narration, m.paidto, m.chequeno, m.chqdate, m.branch, m.pincode, m.place, m.slipno," +
                                    "iotype, m.iamount, m.fcamount, m.fcurr, m.country, fadd1, fadd2, fadd3, fadd4 " +
                                    "FROM lmain m " +
                                    "ORDER BY m.vdate";
                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of Voucher Detail from BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchDetailVouchers()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_ac_path))
                {
                    string sql = "SELECT d.hcode, d.tcode, d.vtype, d.genno, d.vno, d.prefix, d.vdate, " +
                                    "d.srno, a.code, IIF(d.acchead ='CASH ON HAND','Cash', d.acchead) as acchead, IIF(a.group = 'CASH ON HAND', 'Cash-in-hand', a.group) as group, d.toby, d.debit, d.credit " +
                                    "FROM lacdet d INNER JOIN accounts a ON a.hcode = d.hcode AND a.tcode = d.tcode AND " + 
                                    "d.acchead = a.account";
                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get list of FD Register from BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchFDRegister()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (dbfHandler dbf = new dbfHandler(boscopac_ac_path))
                {
                    string sql = "SELECT d.hcode, d.tcode, d.vtype, d.genno, d.vno, d.prefix, d.vdate, " +
                                   "d.srno, d.acchead, d.toby, d.debit, d.credit " +
                                   "FROM lacdet d INNER JOIN ";
                    resultarg = dbf.FetchBySQL(sql);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        public virtual void Dispose()
        {
            GC.Collect();
        }
    }
}
