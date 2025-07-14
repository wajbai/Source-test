using System;
using System.Collections.Generic;
using System.Text;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.DAO;
using Bosco.Utility.Common;
using Bosco.Utility;

namespace Payroll.SQL
{
    public class PayrollSQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.Payroll).FullName)
            {
                query = GetPayrollSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetPayrollSQL()
        {
            string query = "";
            string datevalue = string.Empty;
            SQLCommand.Payroll sqlCommandId = (SQLCommand.Payroll)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                #region Payroll GateWay
                case SQLCommand.Payroll.DeletePRStaff:
                    {
                        query = "DELETE FROM PRStaff WHERE PayRollId =?PAYROLLID ";
                        break;
                    }
                case SQLCommand.Payroll.DeletePRStaffTemo:
                    {
                        query = "DELETE FROM PRStaffTemp WHERE PayRollId = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePRStaffTempByNotEditableValue:
                    {
                        query = @"DELETE FROM PRSTAFFTEMP WHERE PAYROLLID = ?PAYROLLID AND 
                                     COMPONENTID IN (SELECT COMPONENTID FROM PRCOMPONENT P WHERE ISEDITABLE=1)";
                        break;
                    }
                case SQLCommand.Payroll.DeletePRStaffGroup:
                    {
                        query = " DELETE FROM PRStaffGroup WHERE PayRollId =?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePRCompMonth:
                    {
                        query = "DELETE FROM PRCompMonth WHERE PayRollId = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePRStatus:
                    {
                        query = "DELETE FROM PRStatus WHERE PayRollId = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.UpdateInstallment:
                    {
                        query = "UPDATE PRLoanGet SET CurrentInstallment = CurrentInstallment - 1, " +
                    "Completed = '1' WHERE PRLoanGetId IN (SELECT PRLoanGetId FROM PRLoanPaid " +
                    "WHERE PayRollId =?PAYROLLID )";
                        break;
                    }
                case SQLCommand.Payroll.DeletePRLoanPaid:
                    {
                        query = @"DELETE FROM PRLoanPaid WHERE PayRollId =?PAYROLLID;";
                        break;
                    }
                case SQLCommand.Payroll.DeletePRCreate:
                    {
                        query = "DELETE FROM PRCreate WHERE PayRollId =?PAYROLLID ";
                        break;
                    }
                case SQLCommand.Payroll.FetchVoucherMastersByPayrollId:
                    {

                        query = "SELECT VOUCHER_ID\n" +
                        "  FROM VOUCHER_MASTER_TRANS\n" +
                        " WHERE VOUCHER_TYPE = \"JN\"\n" +
                        "   AND CLIENT_REFERENCE_ID = ?PAYROLLID\n" +
                        "   AND VOUCHER_SUB_TYPE = \"PAY\" AND STATUS=1;";

                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollGateWay:
                    {
                        query = "SELECT PC.PAYROLLID,PC.PRDATE,PC.PRNAME FROM PRCREATE PC ORDER BY PAYROLLID ASC";
                        break;
                    }
                case SQLCommand.Payroll.GetCurrentPayroll:
                    {
                        query = "SELECT MAX(PayRollId) AS \"PRId\" FROM PRCreate";
                        break;
                    }
                case SQLCommand.Payroll.GetLatestPayroll:
                    {
                        query = "SELECT PAYROLLID, PRDATE, PRNAME FROM PRCREATE ORDER BY PRDATE DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.Payroll.GetPreviousPayrollMonth:
                    {
                        query = "";
                        break;
                    }
                case SQLCommand.Payroll.LockPayroll:
                    {
                        query = "";
                        break;
                    }
                case SQLCommand.Payroll.PayrollAdd:
                    {
                        query = "INSERT INTO PRCREATE(PRDATE,PRNAME) VALUES (?PRDATE,?PRNAME) ";
                        break;
                    }
                case SQLCommand.Payroll.StaffGroupAdd:
                    {
                        query = "";
                        break;
                    }
                case SQLCommand.Payroll.UpdatePayrollStatus:
                    {
                        query = "INSERT INTO PRStatus (PayRollId,CompCreated,Lockedstatus) VALUES(?PAYROLLID,?COMPCREATED,?LOCKEDSTATUS)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollDetailsAdd:
                    {
                        query = "INSERT INTO PAYROLL(PAYROLLID,FROMDATE,TODATE) VALUES (?PAYROLLID,?FROMDATE,?TODATE)";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollDateInterval:
                    {
                        query = " SELECT DATE_FORMAT(FROMDATE,'%d.%m.%Y') AS FROMDATE,DATE_FORMAT(TODATE,'%d.%m.%Y') AS TODATE FROM PAYROLL WHERE PAYROLLID = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollProcessDate:
                    {
                        query = "SELECT DATE_FORMAT(TRANSACTIONDATE,'%d/%m/%Y') as TRANSACTIONDATE  FROM PRSTAFF WHERE  PAYROLLID =?PAYROLLID ";
                        break;
                    }
                case SQLCommand.Payroll.UpdateComponentinEquation:
                    {
                        query = "UPDATE PRCOMPONENT AS PCM\n" +
                                "INNER JOIN (SELECT COMPONENTID,\n" +
                                "                   REPLACE(EQUATION, ?PREVCOMPONENTNAME, ?COMPONENT) AS TEMPEQUATION,\n" +
                                "                   TEMPID\n" +
                                "              FROM (SELECT PC.COMPONENTID, COMPONENT, EQUATION, 0 AS TEMPID\n" +
                                "                      FROM PRCOMPONENT PC\n" +
                                "                     WHERE PC.COMPONENTID IN (?COMPONENTID)\n" +
                                "                    UNION ALL\n" +
                                "                    SELECT 0 AS COMPONENTID,\n" +
                                "                           '' AS COMPONENT,\n" +
                                "                           EQUATION,\n" +
                                "                           PCG.COMPONENTID AS TEMPID\n" +
                                "                      FROM PRCOMPONENT PCG\n" +
                                "                     WHERE FIND_IN_SET(?COMPONENTID,\n" +
                                "                                       REPLACE(RELATEDCOMPONENTS, 'ê', ',')) > 0) AS W) as T ON PCM.COMPONENTID = T.TEMPID\n" +
                                "   SET PCM.EQUATION = T.TEMPEQUATION;";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollGroupByDept:
                    {
                        query = "SELECT distinct PRComponent.Component AS Component," +
                        "(SELECT SUM(PRStaff.CompValue)" +
                        "FROM PRStaff,PRStaffGroup WHERE PRStaff.ComponentId = " +
                        "PRComponent.ComponentId AND PRStaff.PayRollId = ?PAYROLLID " +
                        " and PRStaffGroup.Payrollid = PRStaff.Payrollid " +
                        " AND PRStaff.StaffId = PRStaffGroup.StaffID AND PRStaffGroup.GroupId in (?GROUPID)) " +
                        " From PRComponent,PRStaff,PRCompMonth " +
                        " Where PRStaff.ComponentId = PRCompMonth.ComponentId And " +
                        "PRComponent.Type = 1 And PRComponent.DONT_SHOWINBROWSE = 1 And PRStaff.PayRollId =?PAYROLLID " +
                        " And PRComponent.ComponentId = PRCompMonth.ComponentId " +
                        " And PRCompMonth.SalaryGroupId in (?SALARYGROUPID)";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollGroupByGroup:
                    {
                        //query = "SELECT T.COMPONENT AS Component, SUM(T.AMOUNT) AS Amount\n" +
                        //       "  FROM \n" +
                        //       "  (SELECT PCN.COMPONENT, PRS.COMPONENTID, PRS.COMPVALUE AS AMOUNT,RELATEDCOMPONENTS\n" +
                        //       "          FROM PRSTAFF PRS\n" +
                        //       "         INNER JOIN PRCOMPONENT PCN\n" +
                        //       "            ON PCN.COMPONENTID = PRS.COMPONENTID\n" +
                        //       "         INNER JOIN PRSTAFFGROUP PGP\n" +
                        //       "            ON PGP.PAYROLLID = PRS.PAYROLLID\n" +
                        //       "           AND PRS.PAYROLLID = ?PAYROLLID\n" +
                        //       "           AND PRS.STAFFID = PGP.STAFFID\n" +
                        //       "           AND PGP.GROUPID IN (?GROUPIDS)\n" +
                        //       "           AND PCN.TYPE IN (1)\n" +
                        //       "         INNER JOIN PRPROJECT_STAFF PRPS\n" +
                        //       "           ON PRPS.PROJECT_ID=?PROJECT_ID\n" +
                        //       "           AND PRS.STAFFID = PRPS.STAFFID\n" +
                        //       "AND LENGTH(RELATEDCOMPONENTS) - LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2 " +
                        //       "         GROUP BY PRS.STAFFID,COMPONENTID) AS T\n" +
                        //       "  --  ON T.COMPONENTID = PMC.COMPONENTID\n" +
                        //       " --  AND PMC.SALARYGROUPID IN (?GROUPIDS)\n" +
                        //       " --  AND PMC.PAYROLLID = ?PAYROLLID\n" +
                        //       " GROUP BY T.COMPONENTID";


                        query = "SELECT T.COMPONENT AS Component, SUM(T.AMOUNT) AS Amount\n" +
                               "  FROM \n" +
                               "  (SELECT PCN.COMPONENT, PRS.COMPONENTID, PRS.COMPVALUE AS AMOUNT,PROCESS_COMPONENT_TYPE,RELATEDCOMPONENTS\n" +
                               "          FROM PRSTAFF PRS\n" +
                               "         INNER JOIN PRCOMPONENT PCN\n" +
                               "            ON PCN.COMPONENTID = PRS.COMPONENTID\n" +
                               "         INNER JOIN PRSTAFFGROUP PGP\n" +
                               "            ON PGP.PAYROLLID = PRS.PAYROLLID\n" +
                               "           AND PRS.PAYROLLID = ?PAYROLLID\n" +
                               "           AND PRS.STAFFID = PGP.STAFFID\n" +
                               "           AND PGP.GROUPID IN (?GROUPIDS)\n" +
                               "           AND PCN.TYPE IN (1)\n" +
                               "         LEFT JOIN PRPROJECT_STAFF PRPS\n" +
                               "           ON PRS.STAFFID = PRPS.STAFFID  {WHERE PRPS.PROJECT_ID=?PROJECT_ID} \n" +
                               "         GROUP BY PRS.STAFFID,COMPONENTID) AS T\n" +
                            //" WHERE LENGTH(T.RELATEDCOMPONENTS) - LENGTH(REPLACE(T.RELATEDCOMPONENTS, 'ê', '')) <= 2   " + //On 04/06/2019,
                               " WHERE T.PROCESS_COMPONENT_TYPE NOT IN (" + (int)PayRollProcessComponent.NetPay + "," + (int)PayRollProcessComponent.GrossWages + "," +
                                                             +(int)PayRollProcessComponent.Deductions + ")" +
                               " GROUP BY T.COMPONENTID";

                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollComponentByDept:
                    {
                        query = "SELECT distinct PRComponent.Component AS \"Component\", " +
                        "(SELECT SUM(PRStaff.CompValue) as \"Amount\" " +
                        "FROM PRStaff,PRStaffGroup WHERE PRStaff.ComponentId = " +
                        "PRComponent.ComponentId AND PRStaff.PayRollId = ?PAYROLLID" +
                        " and PRStaffGroup.Payrollid = PRStaff.Payrollid " +
                        " AND PRStaff.StaffId = PRStaffGroup.StaffID AND PRStaffGroup.GroupId in (?SALARYGROUPID))" +
                        " From PRComponent, PRStaff, PRCompMonth " +
                        " Where PRStaff.ComponentId = PRCompMonth.ComponentId And PRComponent.Type = 0 And PRComponent.DONT_SHOWINBROWSE = 1" +
                        " And PRStaff.PayRollId = ?PAYROLLID " +
                        " And PRComponent.ComponentId = PRCompMonth.ComponentId " +
                        " And PRCompMonth.SalaryGroupId in (?SALARYGROUPID)";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollComponentByGroup:
                    {

                        //query = "SELECT T.COMPONENT AS Component, SUM(T.AMOUNT) AS Amount\n" +
                        //        "  FROM \n" +
                        //        "  (SELECT PCN.COMPONENT, PRS.COMPONENTID, PRS.COMPVALUE AS AMOUNT,RELATEDCOMPONENTS\n" +
                        //        "          FROM PRSTAFF PRS\n" +
                        //        "         INNER JOIN PRCOMPONENT PCN\n" +
                        //        "            ON PCN.COMPONENTID = PRS.COMPONENTID\n" +
                        //        "         INNER JOIN PRSTAFFGROUP PGP\n" +
                        //        "            ON PGP.PAYROLLID = PRS.PAYROLLID\n" +
                        //        "           AND PRS.PAYROLLID = ?PAYROLLID\n" +
                        //        "           AND PRS.STAFFID = PGP.STAFFID\n" +
                        //        "           AND PGP.GROUPID IN (?GROUPIDS)\n" +
                        //        "           AND PCN.TYPE IN (0)\n" +
                        //        "         INNER JOIN PRPROJECT_STAFF PRPS\n" +
                        //        "           ON PRPS.PROJECT_ID=?PROJECT_ID\n" +
                        //        "           AND PRS.STAFFID = PRPS.STAFFID\n" +
                        //        "AND LENGTH(RELATEDCOMPONENTS) - LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2 " +
                        //        "         GROUP BY PRS.STAFFID,COMPONENTID) AS T\n" +
                        //        "  --  ON T.COMPONENTID = PMC.COMPONENTID\n" +
                        //        " --  AND PMC.SALARYGROUPID IN (?GROUPIDS)\n" +
                        //        " --  AND PMC.PAYROLLID = ?PAYROLLID\n" +
                        //        " GROUP BY T.COMPONENTID";

                        query = "SELECT T.COMPONENT AS Component, SUM(T.AMOUNT) AS Amount\n" +
                                "  FROM \n" +
                                "  (SELECT PCN.COMPONENT, PRS.COMPONENTID, PRS.COMPVALUE AS AMOUNT,PROCESS_COMPONENT_TYPE,RELATEDCOMPONENTS\n" +
                                "          FROM PRSTAFF PRS\n" +
                                "         INNER JOIN PRCOMPONENT PCN\n" +
                                "            ON PCN.COMPONENTID = PRS.COMPONENTID\n" +
                                "         INNER JOIN PRSTAFFGROUP PGP\n" +
                                "            ON PGP.PAYROLLID = PRS.PAYROLLID\n" +
                                "           AND PRS.PAYROLLID = ?PAYROLLID\n" +
                                "           AND PRS.STAFFID = PGP.STAFFID\n" +
                                "           AND PGP.GROUPID IN (?GROUPIDS)\n" +
                                "           AND PCN.TYPE IN (0)\n" +
                                "         LEFT JOIN PRPROJECT_STAFF PRPS\n" +
                                "           ON PRS.STAFFID = PRPS.STAFFID {WHERE PRPS.PROJECT_ID=?PROJECT_ID} \n" +
                                "         GROUP BY PRS.STAFFID,COMPONENTID) AS T\n" +
                            //" WHERE LENGTH(T.RELATEDCOMPONENTS) - LENGTH(REPLACE(T.RELATEDCOMPONENTS, 'ê', '')) <= 2   " + //On 04/06/2019,
                                " WHERE T.PROCESS_COMPONENT_TYPE NOT IN (" + (int)PayRollProcessComponent.NetPay + "," + (int)PayRollProcessComponent.GrossWages + "," +
                                                             +(int)PayRollProcessComponent.Deductions + ")" +
                                " GROUP BY T.COMPONENTID";

                        break;
                    }
                case SQLCommand.Payroll.ConstructQuery:
                    {
                        query = "SELECT ?FIELDVALUE FROM ?TABLENAME WHERE ?CONDITION";
                        break;
                    }
                case SQLCommand.Payroll.ConstructQueryToaddcombo:
                    {
                        query = "SELECT ?FIELDVALUE FROM ?TABLENAME WHERE ?CONDITION AND ISEDITABLE=0";
                        break;
                    }
                case SQLCommand.Payroll.FetchPrGateWay:
                    {
                        query = "SELECT PAYROLLID, PRDATE, PRNAME FROM PRCreate Order by Payrollid Asc";
                        break;
                    }
                case SQLCommand.Payroll.GetServerDateTimeFormat:
                    {
                        query = "SELECT CURRENT_DATE";
                        break;
                    }
                case SQLCommand.Payroll.UpdatePRStaffGroupByPayrollId:
                    {

                        query = "INSERT INTO PRSTAFFGROUP\n" +
                        "  (GROUPID, STAFFORDER, PAYROLLID, STAFFID)\n" +
                        "  (SELECT GROUPID, STAFFORDER,?PAYROLLID, STAFFID\n" +
                        "     FROM PRSTAFFGROUP\n" +
                        "    GROUP BY STAFFID);";

                        break;
                    }
                #endregion

                #region Payroll Grade
                case SQLCommand.Payroll.PayrollDefineStatus:
                    {
                        query = "INSERT INTO PRSTATUS(PAYROLLID,COMPCREATED,LOCKEDSTATUS) VALUES(?PAYROLLID,'Y','N')";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCheck:
                    {
                        query = " select * from prcreate where payrollid in (select max(payrollid) from prcreate)";
                        break;
                    }
                case SQLCommand.Payroll.GetPayrollCreation:
                    {
                        query = "SELECT PAYROLLID,PRDATE,PRNAME FROM PRCREATE";
                        break;
                    }
                case SQLCommand.Payroll.GetGradeUnalloted:
                    {
                        query = "SELECT DISTINCT '0' as \"SELECT\", stfpersonal.empno AS \"STAFFCODE\", " +
                        "stfpersonal.firstname AS \"NAME\", " +
                        "stfpersonal.staffid as \"STAFFID\"" +
                        "FROM prcreate, stfpersonal " +
                        "WHERE stfpersonal.staffid not in (select distinct staffid from prstaffgroup where payrollid =<pmonthid>) " +
                        "And (Stfpersonal.Leavingdate  is NULL OR prcreate.prdate < stfpersonal.Leavingdate)";
                        break;
                    }
                case SQLCommand.Payroll.ShowAllocatedGrade:
                    {
                        query = "SELECT stfpersonal.staffid as \"STAFF ID\"," +
                        "prsalarygroup.groupname as \"GROUP NAME\", stfpersonal.empno as \"EMP NO\" " +
                        "from stfpersonal,prstaffgroup,prsalarygroup,prcreate " +
                        "where stfpersonal.staffid=prstaffgroup.staffid and " +
                        "prstaffgroup.groupid=prsalarygroup.groupid and " +
                        "prstaffgroup.payrollid in(<pmonthid>) and " +
                            //--prstaffgroup.payrollid in(select max(prcreate.payrollid) from prcreate) and
                        "prstaffgroup.groupid in (select max(prsalarygroup.groupid) from prsalarygroup) order by prstaffgroup.stafforder";
                        break;
                    }
                case SQLCommand.Payroll.GetGradeAlloted:
                    {
                        query = "SELECT stfpersonal.staffid as \"STAFF ID\"," +
                        "prsalarygroup.groupname as \"GROUP NAME\", stfpersonal.empno as \"EMP NO\" " +
                        "from stfpersonal,prstaffgroup,prsalarygroup " +
                        "where stfpersonal.staffid=prstaffgroup.staffid and " +
                        "prstaffgroup.groupid=prsalarygroup.groupid and " +
                        "prstaffgroup.payrollid=<pmonthid> and " +
                        "prstaffgroup.groupid in (<strGradeId>) order by prstaffgroup.stafforder";
                        break;
                    }
                case SQLCommand.Payroll.UpdateStaffGrade:
                    {
                        query = "update prstaffgroup set groupid = <nGroupId> where staffid = <nStaffId> and Payrollid = <PayRollId>";
                        break;
                    }
                case SQLCommand.Payroll.AllocateStaffGrade:
                    {
                        query = "INSERT INTO PRSTAFFGROUP (staffid,groupid,stafforder,payrollid) values (<nStaffId>,<groupid>,<nCount>,<pmonthid>)";
                        break;
                    }
                case SQLCommand.Payroll.UnMapStaffGroup:
                    {
                        query = "DELETE  FROM PRSTAFFGROUP WHERE STAFFID =?STAFFID  AND PAYROLLID =?PAYROLLID"; // AND GROUPID IN (?GROUPID);";
                        break;
                    }
                case SQLCommand.Payroll.GetPayrollId:
                    {
                        query = "SELECT PAYROLLID FROM PRCREATE WHERE PRNAME='<payrollyear>'";
                        break;
                    }
                case SQLCommand.Payroll.GetPayrollData:
                    {
                        query = "SELECT PRComponent.Component AS Component," +
                        "(SELECT SUM( to_number(PRStaff.CompValue ) )" +
                        "FROM PRStaff,PRStaffGroup " +
                        "WHERE PRStaff.ComponentId = PRComponent.ComponentId " +
                        "AND PRStaff.PayRollId = <pid> and PRStaffGroup.Payrollid = PRStaff.Payrollid " +
                        "AND PRStaff.StaffId = PRStaffGroup.StaffID AND PRStaffGroup.GroupId in (<groupid>)) " +
                        "From PRComponent, PRStaff, PRCompMonth " +
                        "Where PRStaff.ComponentId = PRCompMonth.ComponentId And [PRComponent].[Type] = 0 " +
                        "And PRStaff.PayRollId = <pid> And PRComponent.ComponentId = PRCompMonth.ComponentId " +
                        "And PRCompMonth.SalaryGroupId in (<groupid>) GROUP BY PRComponent.Component," +
                        "PRComponent.Type,PRComponent.ComponentId";
                        break;
                    }
                //case SQLCommand.Payroll.NewPayrollCreate:
                //    {
                //        query = "INSERT INTO PRCREATE(PAYROLLID,PRDATE,PRNAME)VALUES (SCQ_PAYROLL_ID.NEXTVAL,to_date('<payrolldate>','dd/MM/YYYY'),'<payrollMonth>')";
                //        break;
                //    }
                #endregion

                #region Payroll Process
                case SQLCommand.Payroll.PayrollProcessList:
                    {
                        query = "select distinct t.componentid,p.component,p.type,p.equation,p.defvalue,p.linkvalue from prstaff t,prcomponent p,prstaffgroup g where p.componentid=t.componentid and g.groupid=  ?GROUPID  and g.payrollid= ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollProcessUpdate:
                    {
                        query = "update prloanget set staffid=<staffid>,loanid=<loanid>,amount=<amount>,installment=<installment>,fromdate=to_date('<fromdate>','dd/MM/YYYY'),todate=to_date('<todate>','dd/MM/YYYY'),interest=<interest>,intrestmode=<intrestmode>," +
                        "intrestamount=<intrestamount>,currentinstallment=<currentinstallment>,completed=<completed> where prloangetid=<prloangetid>";
                        break;
                    }
                case SQLCommand.Payroll.PayrollProcessGet:
                    {
                        query = "";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollLoanDetails:
                    {
                        query = "select prloangetid, staffid, loanid, IFNULL(amount,0) as amount, " +
                    "IFNULL(installment,0) as installment, DATE_FORMAT(fromdate, '%M-%d-%Y') as fromdate, " +
                    "DATE_FORMAT(todate, '%M-%d-%Y') as todate, IFNULL(interest,0) as interest, " +
                    "IFNULL(intrestmode,0) as intrestmode, IFNULL(intrestamount,0) as intrestamount, " +
                    "IFNULL(currentinstallment,0) as currentinstallment, IFNULL(completed,0) as completed " +
                    "from prloanget";
                        break;
                    }
                case SQLCommand.Payroll.InsertPayrollLoanDetails:
                    {
                        query = "insert into prloanget(prloangetid,staffid,loanid,amount,installment,fromdate,todate,interest,intrestmode,intrestamount,currentinstallment,completed)" +
                    "values(scq_prloanmnt.nextval,<staffid>,<loanid>,<amount>,<installment>,to_date('<fromdate>','dd/MM/YYYY'),to_date('<todate>','dd/MM/YYYY'),<interest>,<intrestmode>,<intrestamount>,<currentinstallment>,<completed>)";
                        break;
                    }
                #endregion

                #region Payroll Group
                case SQLCommand.Payroll.PayrollGroupList:
                    {
                        query = "INSERT INTO PRSALARYGROUP (GROUPNAME) VALUES(?GROUPNAME)";
                        break;
                    }
                case SQLCommand.Payroll.InsertPayrollGroup:
                    {
                        query = "INSERT INTO PRSALARYGROUP (GROUPNAME) VALUES(?GROUPNAME) ON DUPLICATE KEY UPDATE GROUPNAME=?GROUPNAME;";
                        break;
                    }
                case SQLCommand.Payroll.GetPayrollGroup:
                    {
                        query = "SELECT GROUPID AS 'GROUP ID',GROUPNAME AS 'Group Name' FROM PRSALARYGROUP ORDER BY GROUPNAME";
                        break;
                    }
                case SQLCommand.Payroll.GetPayrollGroupById:
                    {
                        query = "SELECT GROUPNAME FROM PRSALARYGROUP WHERE GROUPID=?GROUPID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollGroupbyGroupCategory:
                    {
                        query = "SELECT GROUPID FROM PRSALARYGROUP WHERE GROUPNAME =?CATEGORY";
                        break;
                    }
                case SQLCommand.Payroll.PayrollGroupUpdate:
                    {
                        query = "UPDATE PRSALARYGROUP SET GROUPNAME= ?GROUPNAME WHERE GROUPID=?GROUPID ";
                        break;
                    }
                case SQLCommand.Payroll.PayrollGroupDelete:
                    {
                        query = "DELETE FROM PRSALARYGROUP WHERE GROUPID= ?GROUPID;\n" + 
                                "DELETE FROM PRCOMPMONTH WHERE SALARYGROUPID= ?GROUPID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollGroupExist:
                    {
                        query = "SELECT GROUPNAME FROM PRSALARYGROUP WHERE GROUPNAME=?GROUPNAME";
                        break;
                    }
                case SQLCommand.Payroll.PayrollGradeId:
                    {
                        query = "SELECT GROUPID FROM PRSALARYGROUP where GROUPNAME IN(?GROUPNAME)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollThirdPartyGroupId:
                    {
                        query = "SELECT GROUPID FROM PRSALARYGROUP where GROUPNAME IN(?GROUPNAME)";
                        break;
                    }
                case SQLCommand.Payroll.GetGroupByPayrollId:
                    {
                        query = "SELECT PG.GROUPID, GROUPNAME\n" +
                                "  FROM PRSALARYGROUP PG\n" +
                                " INNER JOIN PRSTAFFGROUP PR\n" +
                                "    ON PR.GROUPID = PG.GROUPID {AND PR.PAYROLLID = ?PAYROLLID}\n" +
                                 "  INNER JOIN PRCOMPMONTH PCM \n" +
                               " ON PCM.SALARYGROUPID=PG.GROUPID \n" +
                                " WHERE 1=1 {AND PR.PAYROLLID = ?PAYROLLID}\n" +
                                " GROUP BY PG.GROUPID ORDER BY GROUPNAME;";
                        break;
                    }
                #endregion

                #region  Payroll component

                case SQLCommand.Payroll.PayrollComponenetSelect:
                    {
                        query = "SELECT t.componentid," + "\r\n" +
                                "       t.component AS 'Component'," + "\r\n" +
                                "       t.description AS 'Description'," + "\r\n" +
                                "       CASE" + "\r\n" +
                                "          WHEN t.type = 0 THEN 'Earning'" + "\r\n" +
                                "          ELSE CASE WHEN t.type = 1 THEN 'Deduction' ELSE 'Text' END" + "\r\n" +
                                "       END" + "\r\n" +
                                "          AS Type," + "\r\n" +
                                "       t.defvalue AS \"Fixed Value\"," + "\r\n" +
                                "       t.linkvalue AS 'Link Value'," + "\r\n" +
                                "       t.equation," + "\r\n" +
                                "       t.equationid," + "\r\n" +
                                "       t.maxslap AS 'Max Slab'," + "\r\n" +
                                "       t.compround," + "\r\n" +
                                "       t.ifcondition," + "\r\n" +
                                "       t.DONT_SHOWINBROWSE," + "\r\n" +
                                "       -- t.iseditable" + "\r\n" +
                                "        CASE" + "\r\n" +
                                "            WHEN t.iseditable = 0 THEN 'Editable'" + "\r\n" +
                                "            ELSE case when t.iseditable = 1 THEN 'Non Editable' end" + "\r\n" +
                                "        END as 'Is Edit', PROCESS_COMPONENT_TYPE" + "\r\n" +
                                "  FROM prcomponent t" + "\r\n" +
                                "ORDER BY t.component";
                        break;
                    }
                case SQLCommand.Payroll.PayrollComponent:
                    {
                        query = "SELECT componentid,component,relatedcomponents FROM PRCOMPONENT order by component";
                        break;
                    }

                case SQLCommand.Payroll.ReprocessDate:
                    {
                        query = "UPDATE PRCOMPONENT SET PROCESS_DATE=?PROCESS_DATE";
                        break;
                    }


                case SQLCommand.Payroll.FetchReprocessDate:
                    {
                        query = "SELECT PROCESS_DATE FROM PRCOMPONENT LIMIT 1";
                        break;
                    }

                case SQLCommand.Payroll.PayrollComponentFetchAll:
                    {
                        //query = "SELECT COMPONENTID, COMPONENT, DESCRIPTION, TYPE, DEFVALUE, LINKVALUE, EQUATION, EQUATIONID, truncate(MAXSLAP,0) as MAXSLAP , truncate(COMPROUND,0) as COMPROUND, IFCONDITION, SHOWINBROWSE, RELATEDCOMPONENTS,ISEDITABLE FROM PRCOMPONENT order by component";
                        query = "SELECT COMPONENTID,\n" +
                                " COMPONENT, DESCRIPTION,\n" +
                                " CASE\n" +
                                "    WHEN TYPE = '0' THEN\n" +
                                "       'Earning'\n" +
                                "    WHEN TYPE = '1' THEN\n" +
                                "       'Deduction'\n" +
                                "    WHEN TYPE = '3' THEN\n" +
                                "       'Calculation'\n" +
                                "    ELSE\n" +
                                "       'Text'\n" +
                                " END AS 'Type', DEFVALUE, LINKVALUE, EQUATION, EQUATIONID, \n" +
                                " truncate(MAXSLAP, 0) as MAXSLAP, truncate(COMPROUND, 0) as COMPROUND,\n" +
                                " IFCONDITION, RELATEDCOMPONENTS, IF(DONT_SHOWINBROWSE = 0, 'Yes', 'No') as DONT_SHOWINBROWSE,\n" +
                                " IF(DONT_IMPORT_MODIFIED_VALUE_PREV_PR = 0 AND ISEDITABLE = 0, 'Yes', 'No') AS DONT_IMPORT_MODIFIED_VALUE_PREV_PR,\n" +
                                " IF(ISEDITABLE = 0, 'Yes', 'No') AS ISEDITABLE ,ML.LEDGER_NAME, IFNULL(PR.PROCESS_TYPE_ID, 0) AS PROCESS_TYPE_ID,\n" +
                                " PR.ACCESS_FLAG, CASE WHEN PROCESS_COMPONENT_TYPE=1 THEN '" + PayRollProcessComponent.NetPay.ToString()  + "'\n" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 2 THEN '"+ PayRollProcessComponent.GrossWages.ToString() +"'" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 3 THEN '" + PayRollProcessComponent.Deductions.ToString()  + "'" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 4 THEN '" + PayRollProcessComponent.EPF.ToString() + "'" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 5 THEN '" + PayRollProcessComponent.PT.ToString() + "'" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 6 THEN '" + PayRollProcessComponent.ESI.ToString() + "'" +
                                "  ELSE 'None' END PROCESS_COMPONENT_TYPE," +
                                " IF(PR.PAYABLE=1, 'Yes', 'No') AS PAYABLE,\n" +
                                " CAST((CASE WHEN PR.LINKVALUE = 'Name' THEN -2 WHEN PR.LINKVALUE = 'Designation' THEN -1 ELSE PR.TYPE END) AS SIGNED) AS TYPE_SORT\n" +
                                " FROM PRCOMPONENT PR\n" +
                                " LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID=PR.LEDGER_ID \n" +
                                " order by component;";

                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollcomponentTypeID:
                    {
                        query = "SELECT COMPONENTID, COMPONENT, DESCRIPTION, TYPE, DEFVALUE, LINKVALUE, EQUATION, EQUATIONID,truncate(MAXSLAP,0) as MAXSLAP, \n" +
                                "truncate(COMPROUND,0) as COMPROUND, IFCONDITION, DONT_SHOWINBROWSE, RELATEDCOMPONENTS, ISEDITABLE,\n" +
                                "PROCESS_COMPONENT_TYPE\n" +
                                "FROM PRCOMPONENT order by component";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollComponent:
                    {
                        query = "SELECT COMPONENTID AS COMPONENT_ID,\n" +
                                            "       COMPONENT,\n" +
                                            "       DESCRIPTION,\n" +
                                            "       CASE TYPE\n" +
                                            "         WHEN 0 THEN\n" +
                                            "          'Earning'\n" +
                                            "         WHEN 1 THEN\n" +
                                            "          'Deduction'\n" +
                                            "         WHEN 2 THEN\n" +
                                            "          'Text'\n" +
                                            "       END AS CALCULATION_TYPE\n" +
                                            "  FROM PRCOMPONENT\n" +
                                            " WHERE TYPE IN (0,1,2)\n" +
                                            "   AND LINKVALUE = ''\n" +
                                            "   AND EQUATION = ''\n" +
                                            " ORDER BY COMPONENT";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollComponetName:
                    {
                        query = "SELECT COMPONENT FROM PRCOMPONENT WHERE COMPONENTID = ?COMPONENTID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollComponentWithType:
                    {
                        //On 24/05/2019, to allow TEXT-FIXED VALUE in Forumula
                        //query = "SELECT COMPONENTID,COMPONENT,RELATEDCOMPONENTS FROM PRCOMPONENT WHERE TYPE <> 2 ORDER BY COMPONENT";
                        query = "SELECT COMPONENTID,COMPONENT,RELATEDCOMPONENTS,LINKVALUE\n" +
                                  "FROM PRCOMPONENT WHERE (TYPE <> 2 OR (TYPE = 2 AND DEFVALUE<>'')) ORDER BY COMPONENT";
                        break;
                    }
                case SQLCommand.Payroll.PayrollComponentAdd:
                    {
                        query = "INSERT INTO PRCOMPONENT(" +
                        "COMPONENTID,COMPONENT,DESCRIPTION,TYPE,DEFVALUE," +
                        "LINKVALUE,EQUATION,EQUATIONID,MAXSLAP,COMPROUND,IFCONDITION)" +
                        "VALUES(?COMPONENTID,?COMPONENT,?DESCRIPTION ,?TYPE,?DEFVALUE,?LINKVALUE,?EQUATION,?EQUATIONID,?MAXSLAP,?COMPROUND,?IFCONDITION)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollComponentEdit:
                    {
                        query = "UPDATE PRCOMPONENT P SET P.COMPONENT=?COMPONENT,P.DESCRIPTION=?DESCRIPTION,P.TYPE=?TYPE,P.DEFVALUE=?DEFVALUE,P.LINKVALUE=?LINKVALUE,P.EQUATION=?EQUATION,P.EQUATIONID=?EQUATIONID,P.MAXSLAP=?MAXSLAP,P.COMPROUND=?COMPROUND,P.IFCONDITION=?IFCONDITION WHERE P.COMPONENTID=?COMPONENTID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollComponentDelete:
                    {
                        query = "insert into prcreate(payrollid,prdate,prname) values(?PAYROLLID,?prdate,?prname)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollEditVerifyCompLink:
                    {
                        query = "SELECT COUNT(*) FROM PRCOMPMONTH WHERE PAYROLLID = ?PAYROLLID AND COMPONENTID = ?COMPONENTID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollEditCompUpdate:
                    {
                        query = "UPDATE PRCOMPMONTH " +
                                    "SET TYPE        = ?TYPE," +
                                        "DEFVALUE    = ?DEFVALUE," +
                                        "EQUATION    = ?EQUATION," +
                                        "EQUATIONID  = ?EQUATIONID," +
                                        "MAXSLAB     = ?MAXSLAP," +
                                        "LNKVALUE    = ?LINKVALUE," +
                                        "COMPROUND   = ?COMPROUND," +
                                        "IFCONDITION = ?IFCONDITION " +
                                    "WHERE PAYROLLID = ?PAYROLLID " +
                                    "AND COMPONENTID = ?COMPONENTID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCompSelect:
                    {
                        query = "select pr.componentid,pr.component as 'Component',\n"+
                            " CASE\n" +
                            "    WHEN pr.TYPE = '0' THEN\n" +
                            "       'Earning'\n" +
                            "    WHEN pr.TYPE = '1' THEN\n" +
                            "       'Deduction'\n" +
                            "    WHEN pr.TYPE = '3' THEN\n" +
                            "       'Calculation'\n" +
                            "    ELSE\n" +
                            "       'Text'\n" +
                            " END AS 'Type',\n" +
                            "CASE WHEN PROCESS_COMPONENT_TYPE=1 THEN '" + PayRollProcessComponent.NetPay.ToString() + "'\n" +
                            "  WHEN PROCESS_COMPONENT_TYPE = 2 THEN '" + PayRollProcessComponent.GrossWages.ToString() + "'\n" +
                            "  WHEN PROCESS_COMPONENT_TYPE = 3 THEN '" + PayRollProcessComponent.Deductions.ToString() + "'\n" +
                            "  WHEN PROCESS_COMPONENT_TYPE = 4 THEN '" + PayRollProcessComponent.EPF.ToString() + "'\n" +
                            "  WHEN PROCESS_COMPONENT_TYPE = 5 THEN '" + PayRollProcessComponent.PT.ToString() + "'\n" +
                            "  WHEN PROCESS_COMPONENT_TYPE = 7 THEN '" + PayRollProcessComponent.ESI.ToString() + "'\n" +
                            "  ELSE 'None' END PROCESS_COMPONENT_TYPE,\n" +
                            " pr.defvalue as 'Def.Value',pr.linkvalue as 'Link Value',pr.equation as 'Equation',pr.maxslap as 'Max Slab', pr.relatedcomponents\n" +
                            " from prcompmonth prc,prcomponent pr where { prc.salarygroupid IN(?SALARYGROUPID) and }\n" +
                            " prc.componentid=pr.componentid and prc.payrollid=?PAYROLLID  group by componentid order by prc.comp_order";
                        break;

                    }

                case SQLCommand.Payroll.GetPreviousValue:
                    {
                        query = " SELECT date_format(P.PRDATE, '%d/%m/%Y') as PRDATE,P.PAYROLLID FROM PRCREATE P WHERE P.PAYROLLID IN(SELECT MAX(P.PAYROLLID) FROM PRCREATE P) ORDER BY P.PRDATE DESC";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentByComponentId:
                    {
                        //query = "SELECT COMPONENT,\n" +
                        //        "       DESCRIPTION,\n" +
                        //        "       TYPE,\n" +
                        //        "       DEFVALUE,\n" +
                        //        "       LINKVALUE,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN PRL.LOANNAME = PRC.LINKVALUE THEN\n" +
                        //        "          PRL.LOANID\n" +
                        //        "         ELSE\n" +
                        //        "          CASE\n" +
                        //        "            WHEN PRI.INCOME_NAME = PRC.LINKVALUE THEN\n" +
                        //        "             PRI.INCOME_ID\n" +
                        //        "            ELSE\n" +
                        //        "             PRT.ID\n" +
                        //        "          END\n" +
                        //        "       END 'SELECTED_ID',\n" +
                        //        "       EQUATION,\n" +
                        //        "       EQUATIONID,\n" +
                        //        "       MAXSLAP,\n" +
                        //        "       COMPROUND,\n" +
                        //        "       IFCONDITION,\n" +
                        //        "       SHOWINBROWSE,\n" +
                        //        "       RELATEDCOMPONENTS,\n" +
                        //        "       ISEDITABLE\n" +
                        //        "  FROM PRCOMPONENT PRC\n" +
                        //        "  LEFT JOIN PRLOAN PRL\n" +
                        //        "    ON PRL.LOANNAME = PRC.LINKVALUE\n" +
                        //        "  LEFT JOIN PRINCOME PRI\n" +
                        //        "    ON PRI.INCOME_NAME = PRC.LINKVALUE\n" +
                        //        "  LEFT JOIN PRTEXT PRT\n" +
                        //        "    ON PRT.TNAME = PRC.LINKVALUE\n" +
                        //        " WHERE COMPONENTID =?COMPONENTID;";

                        //query = "SELECT COMPONENT,\n" +
                        //        "       DESCRIPTION,\n" +
                        //        "       TYPE,\n" +
                        //        "       DEFVALUE,\n" +
                        //        "       LINKVALUE,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN PRL.EXPENSE_NAME = PRC.LINKVALUE THEN\n" +
                        //        "          PRL.EXPENSE_ID\n" +
                        //        "         ELSE\n" +
                        //        "          CASE\n" +
                        //        "            WHEN PRI.INCOME_NAME = PRC.LINKVALUE THEN\n" +
                        //        "             PRI.INCOME_ID\n" +
                        //        "            ELSE\n" +
                        //        "             PRT.ID\n" +
                        //        "          END\n" +
                        //        "       END 'SELECTED_ID',\n" +
                        //        "       EQUATION,\n" +
                        //        "       EQUATIONID,\n" +
                        //        "       MAXSLAP,\n" +
                        //        "       COMPROUND,\n" +
                        //        "       IFCONDITION,\n" +
                        //        "       SHOWINBROWSE,\n" +
                        //        "       RELATEDCOMPONENTS,\n" +
                        //        "       ISEDITABLE,ACCESS_FLAG,LEDGER_ID,PROCESS_TYPE_ID, PAYABLE, PROCESS_COMPONENT_TYPE\n" +
                        //        "  FROM PRCOMPONENT PRC\n" +
                        //        "  LEFT JOIN PRLOAN PRL\n" + //15/05/2019 add deduction comp too
                        //        "    ON CONCAT('Loan : ',PRL.LOANNAME) = PRC.LINKVALUE\n" +
                        //        //"   LEFT JOIN (SELECT loanid AS EXPENSE_ID, CONCAT('Loan : ',loanname) AS 'EXPENSE_NAME' FROM prloan " +
                        //        //"    UNION ALL " +
                        //        //"    SELECT EXPENSE_ID AS EXPENSE_ID, EXPENSE_NAME AS 'EXPENSE_NAME' FROM PREXPENSE) DED ON DED.EXPENSE_NAME= PRC.LINKVALUE" +
                        //        "  LEFT JOIN PRINCOME PRI\n" +
                        //        "    ON PRI.INCOME_NAME = PRC.LINKVALUE\n" +
                        //        "  LEFT JOIN PRTEXT PRT\n" +
                        //        "    ON PRT.TNAME = PRC.LINKVALUE\n" +
                        //        " WHERE COMPONENTID =?COMPONENTID;";

                        query = "SELECT COMPONENT,\n" +
                                "       DESCRIPTION,\n" +
                                "       TYPE,\n" +
                                "       DEFVALUE,\n" +
                                "       LINKVALUE,\n" +
                                "       CASE\n" +
                                "         WHEN CONCAT(IF(IS_EXPENSE=0, 'Loan : ', ''),PRL.LOANNAME) = PRC.LINKVALUE THEN\n" +
                                "          PRL.LOANID\n" +
                                "         ELSE\n" +
                                "          CASE\n" +
                                "            WHEN PRI.INCOME_NAME = PRC.LINKVALUE THEN\n" +
                                "             PRI.INCOME_ID\n" +
                                "            ELSE\n" +
                                "             PRT.ID\n" +
                                "          END\n" +
                                "       END 'SELECTED_ID',\n" +
                                "       EQUATION,\n" +
                                "       EQUATIONID,\n" +
                                "       MAXSLAP,\n" +
                                "       COMPROUND,\n" +
                                "       IFCONDITION,\n" +
                                "       DONT_SHOWINBROWSE,\n" +
                                "       RELATEDCOMPONENTS,\n" +
                                "       ISEDITABLE,ACCESS_FLAG,LEDGER_ID,PROCESS_TYPE_ID,PAYABLE,PROCESS_COMPONENT_TYPE, DONT_IMPORT_MODIFIED_VALUE_PREV_PR\n" +
                                "  FROM PRCOMPONENT PRC\n" +
                                "  LEFT JOIN PRLOAN PRL\n" +
                                "    ON CONCAT(IF(IS_EXPENSE=0, 'Loan : ', ''),PRL.LOANNAME) = PRC.LINKVALUE\n" +
                                "  LEFT JOIN PRINCOME PRI\n" +
                                "    ON PRI.INCOME_NAME = PRC.LINKVALUE\n" +
                                "  LEFT JOIN PRTEXT PRT\n" +
                                "    ON PRT.TNAME = PRC.LINKVALUE\n" +
                                " WHERE COMPONENTID =?COMPONENTID;";
                        break;
                    }
                case SQLCommand.Payroll.DeleteComponentById:
                    {
                        query = "DELETE FROM PRCOMPONENT WHERE COMPONENTID=?COMPONENTID;";
                        break;
                    }
                #endregion

                #region Payroll Loan

                case SQLCommand.Payroll.PayrollLoan:
                    {
                        query = "SELECT loanid AS 'LOAN_ID',loanname AS 'LOAN_NAME' FROM prloan WHERE IS_EXPENSE=0 ORDER BY LOANID DESC";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanDetailfroComponent:
                    {
                        query = "SELECT loanid AS 'INCOME_ID',CONCAT(IF(IS_EXPENSE=0, 'Loan : ', ''),loanname) AS 'INCOME_NAME' FROM prloan " +
                            //"UNION ALL " + //15/05/2019 add deduction comp too
                            //"SELECT EXPENSE_ID AS 'INCOME_ID', EXPENSE_NAME AS 'INCOME_NAME' FROM PREXPENSE " +
                                "ORDER BY INCOME_NAME;";
                        break;
                    }
                case SQLCommand.Payroll.PayrollIncomeforComponent:
                    {
                        query = "SELECT INCOME_ID,INCOME_NAME FROM PRINCOME ORDER BY INCOME_NAME;";
                        break;
                    }
                case SQLCommand.Payroll.PayrollTextvalforComponent:
                    {
                        query = "SELECT ID AS 'INCOME_ID', TNAME AS 'INCOME_NAME' FROM PRTEXT ORDER BY TNAME;";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffmodify:
                    {
                        query = "SELECT * FROM PRComponent where component =?COMPONENT ORDER BY Component";
                        break;
                    }
                case SQLCommand.Payroll.FetchprStaffDetails:
                    {
                        query = "SELECT staffid FROM prstafftemp WHERE componentid = ?COMPONENTID " +
                      " AND staffid = ?STAFFID AND PayrollId = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.FetchprDefValueDetails:
                    {
                        query = "SELECT compvalue FROM prstafftemp WHERE componentid = ?COMPONENTID " +
                      " AND staffid = ?STAFFID AND PayrollId = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffUpdate:
                    {
                        query = "UPDATE PRStaffTemp SET compvalue = ?CONDITION " +
                         " WHERE ComponentId =?COMPONENTID " +
                         " AND staffid = ?STAFFID  AND PayrollId = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffInsertDetails:
                    {
                        query = "INSERT INTO prstafftemp (PAYROLLID,STAFFID,COMPONENTID,COMPVALUE) VALUES " +
                             "(?PAYROLLID,?STAFFID,?COMPONENTID,?COMPVALUE)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanList:
                    {
                        //QUERY
                        query = "SELECT LOANID,LOANABBRIVIATION AS 'LOANABBRIVIATION',LOANNAME AS 'LOANNAME' FROM PRLOAN WHERE IS_EXPENSE = 0 ORDER BY LOANABBRIVIATION ASC";
                        //query = "SELECT LOANID,LOANABBRIVIATION AS 'LOANABBRIVIATION',LOANNAME AS 'LOANNAME' FROM PRLOAN WHERE LOANID ORDER BY ASC";
                        break;
                    }


                #endregion

                #region Payroll CompMonth
                case SQLCommand.Payroll.PrCreatecompMonthAdd:
                    {
                        query = "INSERT INTO prcompmonth(payrollid,salarygroupid,componentId,type," +
                                "defvalue,lnkvalue,equation,equationId,maxslab,comp_order," +
                                "compround,ifcondition) " +
                                "SELECT DISTINCT ?NEWPAYROLLID,salarygroupid,componentid,type," +
                                "defvalue,lnkvalue,equation,equationid,maxslab," +
                                "comp_order,compround,ifcondition FROM prcompmonth " +
                                "WHERE payrollid =?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.ImportPreviousPayroll:
                    {
                        query = "INSERT INTO prstafftemp (payrollid,staffid,componentid,compvalue)\n" +
                                "SELECT DISTINCT ?NEWPAYROLLID, PST.STAFFID,PST.COMPONENTID,PST.COMPVALUE\n" +
                                "FROM PRSTAFFTEMP PST INNER JOIN PRCOMPONENT PC ON PC.COMPONENTID = PST.COMPONENTID AND DONT_IMPORT_MODIFIED_VALUE_PREV_PR=0 WHERE payrollid =?PAYROLLID;\n" +
                                "UPDATE PRSTAFFTEMP SET COMPVALUE = ?NUMBER_OF_DAYS WHERE PAYROLLID = ?NEWPAYROLLID AND\n" + //07/10/2021, to reset number of days in pay month
                                "COMPONENTID IN (SELECT COMPONENTID FROM PRcomponent WHERE LINKVALUE = 'TotalDaysInPayMonth')\n";
                        break;
                    }
                case SQLCommand.Payroll.FetchTopTwoPayrollId:
                    {
                        query = "select payrollid from prcreate order by payrollid desc limit 2";
                        break;
                    }
                case SQLCommand.Payroll.FetchPreviousPayrollId:
                    {
                        query = "SELECT PAYROLLID FROM PRCREATE WHERE PAYROLLID < ?PAYROLLID ORDER BY PAYROLLID DESC LIMIT 1";
                        break;
                    }
                #endregion

                #region Staff Details

                case SQLCommand.Payroll.FetchStaffDetails:
                    {
                        // DateTime dt = new DateTime(1990, 1, 1);
                        //dt.ToShortDateString();
                        // datevalue = dt.ToString("yyyy/MM/dd");
                        //  datevalue = clsGeneral.GetMySQLDateTime(dt.ToShortDateString(), DateDataType.DateTime);
                        /*  query = "SELECT stfPersonal.StaffId,stfService.serviceid as ServiceId,EmpNo as \"EmployeeNo\",FirstName,LastName," +
                                  "CONCAT(FIRSTNAME ,' ',LASTNAME)as \"Name\"," +
                                  "KnownAs,Gender,DateofBirth,DateofJoin,DateofAppointment," +
                                  "Designation,Department," +
                                  "RetirementDate,stfService.Pay AS BasicPay," +
                                  "stfService.ScaleofPay,PayIncM1,PayIncM2 ,stfservice.MAXWAGESBASIC,  stfservice.MAXWAGESHRA, stfservice.PFNUMBER " +
                                  "FROM stfPersonal,stfService,PRStaffGroup WHERE " +
                                  "stfPersonal.StaffId = PRStaffGroup.StaffId AND " +
                                  "PRStaffGroup.Payrollid = ?PAYROLLID" +
                                  " and stfPersonal.StaffId = stfService.StaffId AND " +
                                  " stfPersonal.StaffId > 0 ?CONDITION " + //stfPersonal.deptid = hospital_departments.hdept_id AND
                                  " AND " + "DATE_FORMAT(?TODATE, '%Y-%m-%d')" +
                                  " >= DATE_FORMAT(stfPersonal.dateofJoin, '%Y-%m-%d') and ((stfPersonal.LeavingDate is null) or " +
                                 //  " >= DATE_FORMAT(stfPersonal.dateofJoin, '%Y-%m-%d') and ((stfPersonal.LeavingDate  <  '1999-01-01 00:00:00' ) or " +
                                  "DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d') > " + "DATE_FORMAT(?PAYROLLDATE, '%Y-%m-%d')" + ") " +
                                  " AND ((" + "DATE_FORMAT(?TODATE,'%Y-%m-%d')" + " BETWEEN " +
                                  "DATE_FORMAT(stfService.DateofAppointment, '%Y-%m-%d') AND DATE_FORMAT(stfService.DateofTermination, '%Y-%m-%d')) " +
                                  " OR (stfService.DateofTermination is null and " +
                                  "DATE_FORMAT(?TODATE, '%Y-%m-%d')" + " > DATE_FORMAT(stfService.DateofAppointment, '%Y-%m-%d')))" +
                                          " ORDER BY stfPersonal.StaffId ";*/

                        query = "SELECT stfPersonal.StaffId,stfService.serviceid as ServiceId,EmpNo as \"EmployeeNo\",FirstName,LastName," +
                                   "CONCAT(IFNULL(CONCAT(stfPersonal.NAME_TITLE, ' '), ''), firstname,CONCAT(' ', IFNULL(MIDDLE_NAME,'')),CONCAT(' ',lastname)) as \"Name\"," +
                                   "KnownAs,Gender,DateofBirth,DateofJoin,DateofAppointment," +
                                   "Designation, " +
                                   "IFNULL(PRStaffGroup.ACCOUNT_NUMBER, '') AS ACCOUNT_NUMBER , IFNULL(PRStaffGroup.ACCOUNT_IFSC_CODE, '') AS ACCOUNT_IFSC_CODE," +
                                   "IFNULL(PRStaffGroup.ACCOUNT_BANK_BRANCH, '') AS ACCOUNT_BANK_BRANCH, IFNULL(PRStaffGroup.PAYMENT_MODE_ID, 0) AS PAYMENT_MODE_ID," +
                                   "IFNULL(PD.Department, '') AS \"Department/Unit\", IFNULL(PRStaffGroup.DEPARTMENT_ID, 0) AS DEPARTMENT_ID," +
                                   "IFNULL(PWL.WORK_LOCATION, '') AS \"Work/Job Location\", IFNULL(PRStaffGroup.WORK_LOCATION_ID, 0) AS WORK_LOCATION_ID," +
                                   "RetirementDate,stfService.Pay AS BasicPay," +
                                   "stfService.ScaleofPay, stfPersonal.YOS, ROUND(DATEDIFF(?PAYROLLDATE, dateofjoin) / 365) AS Institution_YOS," +
                                   "PayIncM1,PayIncM2 ,stfservice.MAXWAGESBASIC,  stfservice.MAXWAGESHRA, stfservice.UAN," +
                                   "stfservice.EARNING1, stfservice.EARNING2, stfservice.EARNING3," + //15/05/2019 to attach pay extra info
                                   "stfservice.DEDUCTION1, stfservice.DEDUCTION2, PAYING_SALARY_DAYS as PAYINGSALARYDAYS " +
                                   "FROM (SELECT sp.*, PNT.NAME_TITLE FROM stfPersonal as sp LEFT JOIN PR_NAME_TITLE PNT ON PNT.NAME_TITLE_ID = sp.NAME_TITLE_ID) AS stfPersonal," +
                                   "stfService,PRStaffGroup " +
                                   "LEFT JOIN PR_DEPARTMENT PD ON PD.DEPARTMENT_ID = PRStaffGroup.DEPARTMENT_ID " +
                                   "LEFT JOIN PR_WORK_LOCATION PWL ON PWL.WORK_LOCATION_ID = PRStaffGroup.WORK_LOCATION_ID " +
                                   " " +
                                   "WHERE stfPersonal.StaffId = PRStaffGroup.StaffId AND " +
                                   "PRStaffGroup.Payrollid = ?PAYROLLID" +
                                   " and stfPersonal.StaffId = stfService.StaffId AND " +
                                   " stfPersonal.StaffId > 0 ?CONDITION " + 
                                   " AND " + "DATE_FORMAT(?TODATE, '%Y-%m-%d')" +
                                   " >= DATE_FORMAT(stfPersonal.dateofJoin, '%Y-%m-%d') AND (stfPersonal.LeavingDate is NULL OR " +
                                   "(CASE WHEN MONTH(DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d')) = " + "MONTH(DATE_FORMAT(?TODATE, '%Y-%m-%d')) AND " +
                                             "YEAR(DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d')) = " + "YEAR(DATE_FORMAT(?TODATE, '%Y-%m-%d')) " +
                                   "THEN DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d') <= " + "DATE_FORMAT(?TODATE, '%Y-%m-%d')" +
                                   "ELSE DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d') >= " + "DATE_FORMAT(?TODATE, '%Y-%m-%d') END))" +
                                   " AND ((" + "DATE_FORMAT(?TODATE,'%Y-%m-%d')" + " BETWEEN " +
                                   "DATE_FORMAT(stfService.DateofAppointment, '%Y-%m-%d') AND DATE_FORMAT(stfService.DateofTermination, '%Y-%m-%d')) " +
                                   " OR (stfService.DateofTermination is null AND " +
                                   "DATE_FORMAT(?TODATE, '%Y-%m-%d')" + " > DATE_FORMAT(stfService.DateofAppointment, '%Y-%m-%d')))" +
                                    " ORDER BY stfPersonal.StaffId ";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffLoanGet:
                    {
                        query = "SELECT PRLOANGETID, STAFFID, LOANID, AMOUNT, INSTALLMENT, DATE_FORMAT(FROMDATE,'%d/%m/%Y') as FROMDATE,  DATE_FORMAT(TODATE,'%d/%m/%Y') as TODATE, INTEREST, INTRESTMODE, INTRESTAMOUNT, CURRENTINSTALLMENT, COMPLETED FROM PRLoanGet ?CONDITION ORDER BY PRLoanGetId ";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffLoanPaid:
                    {
                        query = "SELECT LOAN_ROWID, PAYROLLID, LOANID, PRLOANGETID, STAFFID, DATE_FORMAT(PAIDDATE,'%d/%m/%Y') as PAIDDATE, AMOUNT, INSTALLMENT FROM PRLoanPaid WHERE ?CONDITION ORDER BY PRLoanGetId";
                        break;
                    }
                case SQLCommand.Payroll.ProcessPayrollInOrder:
                    {
                        query = "SELECT DISTINCT STFPERSONAL.EMPNO, PRSTAFFGROUP.STAFFID, SALARYGROUPID, PRCOMPMONTH.ComponentId," +
                        "PRCOMPMONTH.EQUATIONID, PRCOMPMONTH.MAXSLAB,PRCOMPMONTH.COMPROUND, PRCOMPMONTH.IFCONDITION, PRCOMPMONTH.COMP_ORDER, PRCOMPMONTH.DEFVALUE, PRCOMPONENT.PROCESS_COMPONENT_TYPE " +
                        "FROM PRCOMPMONTH, PRCOMPONENT, PRSTAFFGROUP, STFPERSONAL " +
                        "WHERE ?CONDITION AND PRCompMonth.SalaryGroupId = PRStaffGroup.GroupId AND " +
                        "PRStaffGroup.Payrollid = ?PAYROLLID AND PRSTAFFGROUP.STAFFID = STFPERSONAL.STAFFID AND PRCOMPMONTH.COMPONENTID = PRCOMPONENT.COMPONENTID AND " +
                        "stfPersonal.StaffId > 0 AND ( stfPersonal.LeavingDate  is NULL  OR " +
                            //"(DATE_FORMAT(?PAYROLLDATE,'%Y') <= DATE_FORMAT((stfPersonal.LeavingDate),'%Y')))"; //On 26/07/2019, to show proper leaving date
                        "(CASE WHEN MONTH(DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d')) = " + "MONTH(DATE_FORMAT(?TODATE, '%Y-%m-%d')) AND " +
                                    "YEAR(DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d')) = " + "YEAR(DATE_FORMAT(?TODATE, '%Y-%m-%d')) " +
                        "THEN DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d') <= " + "DATE_FORMAT(?TODATE, '%Y-%m-%d')" +
                        "ELSE DATE_FORMAT(stfPersonal.LeavingDate, '%Y-%m-%d') >= " + "DATE_FORMAT(?TODATE, '%Y-%m-%d') END))";
                        break;
                    }
                case SQLCommand.Payroll.GetComponentIdByGroupId:
                    {
                        query = "SELECT DISTINCT ComponentId FROM PRCompMonth WHERE ?CONDITION ";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentIDbySalaryGroupId:
                    {
                        query = "SELECT DISTINCT ComponentId FROM PRCompMonth WHERE ?CONDITION";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentNotinPRMonth:
                    {
                        query = "Select distinct prstaff.componentid from prstaff,prstaffgroup where prstaff.payrollid = " +
                        " ?PAYROLLID and componentid " +
                        "not in (select componentid from prcompMonth,prstaffgroup where prstaff.payrollid = ?PAYROLLID ?CONDITION " +
                            //nPayRollId + ((sGroupId != "") ? " AND " +
                            //"prstaffgroup.GroupId IN (" + sGroupId + ")" : "") +
                        " and prcompmonth.salarygroupid = prstaffgroup.groupid)" +
                        " and prstaff.staffid = prstaffgroup.staffid " +
                        "and PRStaffGroup.Payrollid = prstaff.payrollid ?CONDITION";
                        //((sGroupId != "") ? " AND " +
                        //"prstaffgroup.GroupId IN (" + sGroupId + ")" : "");
                        break;
                    }
                case SQLCommand.Payroll.DeleteExistingPayroll:
                    {
                        //query = "DELETE FROM PRStaff WHERE ?CONDITION"; //23/01/2017, to aviod inner query in delete condition
                        query = "DELETE PS FROM PRStaff PS INNER JOIN PRStaffGroup SG ON PS.STAFFID = SG.STAFFID AND " +
                                " {SG.GROUPID= ?SALARYGROUPID AND } SG.PAYROLLID= ?PAYROLLID {AND SG.STAFFID IN (?STAFFID)}" +
                                " WHERE PS.PAYROLLId = ?PAYROLLID {AND PS.STAFFID IN (?STAFFID)}";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffDetailsToProcessPayroll:
                    {
                        query = "SELECT * FROM PRStaff WHERE ?CONDITION";
                        break;
                    }
                //case SQLCommand.Payroll.FetchStaffDetailsToProcessPayroll:
                //    {
                //        query = "";
                //        break;
                //    }
                case SQLCommand.Payroll.FetchStaffTempDetailComponent:
                    {
                        query = "SELECT * FROM PRStaffTemp WHERE ?CONDITION";
                        break;
                    }
                case SQLCommand.Payroll.UpdateProcessedPayroll:
                    {
                        query = "INSERT INTO PRSTAFF (PAYROLLID, STAFFID, COMPONENTID, COMPVALUE, ACTUAL_COMPVALUE, COMPORDER, TRANSACTIONDATE)\n" +
                                  "VALUES(?PAYROLLID, ?STAFFID, ?COMPONENTID, ?COMPVALUE, ?ACTUAL_COMPVALUE, ?COMPORDER, ?TRANSACTIONDATE)";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffDetailsAfterProcess:
                    {
                        query = "SELECT * FROM PRStaff WHERE  ?CONDITION";
                        break;
                    }
                case SQLCommand.Payroll.InsertNewDataValueForStaff:
                    {
                        query = "insert into prstafftemp(payrollid,staffid,componentid,compvalue) values(?PAYROLLID,?STAFFID,?COMPONENTID,?COMPVALUE)";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffTempDetails:
                    {
                        query = "select * from prstafftemp";
                        break;
                    }
                case SQLCommand.Payroll.UpdateBasicPay:
                    {
                        query = "update stfservice set pay = ?PAY   where serviceid = ?SERVICEID";
                        break;
                    }
                case SQLCommand.Payroll.UpdateEarning1: //EARNING1
                    {
                        query = "UPDATE STFSERVICE SET EARNING1 = ?EARNING1 WHERE SERVICEID = ?SERVICEID";
                        break;
                    }
                case SQLCommand.Payroll.UpdateEarning2: //EARNING2
                    {
                        query = "UPDATE STFSERVICE SET EARNING2 = ?EARNING2 WHERE SERVICEID = ?SERVICEID";
                        break;
                    }
                case SQLCommand.Payroll.UpdateEarning3: //EARNING3
                    {
                        query = "UPDATE STFSERVICE SET EARNING3 = ?EARNING3 WHERE SERVICEID = ?SERVICEID";
                        break;
                    }
                case SQLCommand.Payroll.UpdateDeduction1: //DEDUCTION1
                    {
                        query = "UPDATE STFSERVICE SET DEDUCTION1 = ?DEDUCTION1 WHERE SERVICEID = ?SERVICEID";
                        break;
                    }
                case SQLCommand.Payroll.UpdateDeduction2: //DEDUCTION2
                    {
                        query = "UPDATE STFSERVICE SET DEDUCTION2 = ?DEDUCTION2 WHERE SERVICEID = ?SERVICEID";
                        break;
                    }
                case SQLCommand.Payroll.UpdatePayingSalaryDays: //
                    {
                        query = "UPDATE STFSERVICE SET PAYING_SALARY_DAYS = ?PAYING_SALARY_DAYS WHERE SERVICEID = ?SERVICEID";
                        break;
                    }
                #endregion

                #region Payroll Loan
                case SQLCommand.Payroll.FetchPayrollLoanId:
                    {
                        query = "SELECT prloangetid from prloanget WHERE " +
                                "loanId =?LOANID AND staffid = ?STAFFID " +
                                " AND DATE_FORMAT(FromDate,'%Y-%m-%d')  <= " + "DATE_FORMAT(?FROMDATE,'%Y-%m-%d') " +
                                " AND completed = 0 AND prloangetid IN (SELECT prloangetid " +
                                "FROM prloanpaid WHERE DATE_FORMAT(paiddate,'%Y-%m-%d') = " +
                                "DATE_FORMAT(?TODATE,'%Y-%m-%d')" + ")";
                        break;
                    }
                case SQLCommand.Payroll.FetchLoanDueAmount:
                    {
                        query = "SELECT prloangetid from prloanget WHERE " +
                                "loanId = ?LOANID AND staffid = ?STAFFID " +
                                " AND DATE_FORMAT(FromDate,'%Y-%m-%d') <= " + "DATE_FORMAT(?FROMDATE,'%Y-%m-%d') " +
                                " AND completed = 0 AND prloangetid NOT IN (SELECT prloangetid " +
                                "FROM prloanpaid WHERE DATE_FORMAT(paiddate,'%Y-%m-%d') = " +
                                "DATE_FORMAT(?TODATE,'%Y-%m-%d')" + ")";
                        break;
                    }
                case SQLCommand.Payroll.FetchPaidAmountForExistingPayroll:
                    {
                        query = "SELECT SUM(Amount) as \"TotPaidAmount\",count(Amount) as \"NoofInstallment\" " +
                                " from prloanpaid where prloangetid =?PRLOANGETID " +
                                " and DATE_FORMAT(paiddate,'%Y-%m-%d') <= " + "DATE_FORMAT(?PAIDDATE,'%Y-%m-%d')" + " AND loanid =?LOANID" +
                                " and staffid = ?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.InsertLoanPaidTable:
                    {
                        query = " insert into prloanpaid(payrollid,loanid,prloangetid,staffid,paiddate,amount,installment)" +
                        " values(?PAYROLLID,?LOANID,?PRLOANGETID,?STAFFID,?PAIDDATE,?AMOUNT,?INSTALLMENT)";
                        break;
                    }
                case SQLCommand.Payroll.FetchpayrollLoanPaid:
                    {
                        query = "SELECT LOAN_ROWID, PAYROLLID, LOANID, PRLOANGETID, STAFFID, DATE_FORMAT(PAIDDATE,'%d/%m/%Y') as PAIDDATE, AMOUNT, INSTALLMENT FROM PRLoanPaid ORDER BY PRLoanGetId";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanUpdate:
                    {
                        query = "UPDATE PRLOANPAID SET AMOUNT=?CONDITION WHERE PRLOANGETID=?PRLOANGETID AND DATE_FORMAT(PAIDDAT,'%Y-%m-%d') = DATE_FORMAT(?PAIDDATE , '%Y-%m-%d')";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanGetUpdate:
                    {
                        query = "update prloanget set completed=?CONDITION where prloangetid=?PRLOANGETID";
                        break;
                    }

                case SQLCommand.Payroll.UpdateInstallmentByLoanId:
                    {
                        query = "update prloanget set CurrentInstallment=?CURRENTINSTALLMENT where prloangetid =?PRLOANGETID";
                        break;
                    }
                case SQLCommand.Payroll.UpdateInstallmentStatusByLoanId:
                    {
                        query = "update prloanget set CurrentInstallment= ?CURRENTINSTALLMENT, completed =1 " +
                               "where prloangetid =?PRLOANGETID";
                        break;
                    }
                case SQLCommand.Payroll.FetchLoanDetails:
                    {
                        query = "select PRLOANGETID, STAFFID, LOANID, AMOUNT, INSTALLMENT, DATE_FORMAT(FROMDATE,'%d/%m/%Y') as FROMDATE, DATE_FORMAT(TODATE,'%d/%m/%Y') as TODATE, INTEREST, INTRESTMODE, INTRESTAMOUNT, CURRENTINSTALLMENT, COMPLETED from prloanget";
                        break;
                    }

                case SQLCommand.Payroll.PayrollLoanInsert:
                    {
                        query = "INSERT INTO PRLOAN(LOANID,LOANNAME,LOANABBRIVIATION)" +
                         " VALUES(?LOANID,?LOANNAME,?LOANABBRIVIATION)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanOccur:
                    {
                        query = "SELECT LOANNAME FROM PRLOAN WHERE UPPER(LOANNAME)=UPPER(?LOANNAME)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanEdit:
                    {
                        query = "UPDATE PRLOAN SET LOANNAME=?LOANNAME,LOANABBRIVIATION=?LOANABBRIVIATION" +
                          " WHERE LOANID=?LOANID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanDelete:
                    {
                        query = "DELETE FROM PRLOAN WHERE LOANID=?LOANID";
                        break;
                    }
                case SQLCommand.Payroll.PayRollLoanDetail:
                    {
                        query = "SELECT LOANABBRIVIATION,LOANNAME FROM PRLOAN WHERE LOANID=?LOANID";
                        break;
                    }

                case SQLCommand.Payroll.PayrollCategoryFetch:
                    {
                        query = "SELECT 0 AS FORMULAGROUPID, '<All>' AS FORMULA_DESC FROM DUAL UNION ALL SELECT FORMULAGROUPID,FORMULA_DESC FROM PRFORMULAGROUP ORDER BY FORMULA_DESC";
                        break;
                    }
                case SQLCommand.Payroll.PayrollComponentFetch:
                    {
                        query = "SELECT COMPONENTID AS 'COMPONENTID',COMPONENT AS 'COMPONENT' FROM PRCOMPONENT ORDER BY COMPONENT";
                        break;
                    }


                //case SQLCommand.Payroll.PayrollFetchDetailsBrowse:
                //    {
                //        query = "select " +
                //           "prloanget.prloangetid as 'PRLOANGETID', stfpersonal.empno as 'STAFFID', " +
                //           "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname) as 'Name', " +
                //           "truncate(prloanget.amount,0) as Amount,truncate(prloanget.installment,0) as installment,truncate(prloanget.interest,0) as 'Rate of Interest'," +
                //           " DATE_FORMAT(prloanget.fromdate, '%m/%d/%Y') as 'Pay From', " +
                //           "DATE_FORMAT(prloanget.todate, '%m/%d/%Y') as 'Pay To',prloanget.intrestmode,CONCAT(IFNULL(prloanget.currentinstallment,0) , '/' , IFNULL(prloanget.installment,0) ) as Installments, " +
                //           "' ' as 'Department'," +
                //           " prloan.loanname as Loan, " +
                //           "IFNULL(prloanget.completed,0) as Completed " +
                //       "from " +
                //           "prloan, prloanget, stfpersonal " + //, hospital_departments
                //       "where " +
                //           "prloanget.completed =?COMPLETED and " +
                //           "prloan.loanid = prloanget.loanid and stfpersonal.staffid = prloanget.staffid  " +
                //            //  "stfpersonal.deptid = hospital_departments.hdept_id " +
                //       "order by " +
                //           "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname)";
                //        break;
                //    }
                // Modified by chinna on Top Old
                case SQLCommand.Payroll.PayrollFetchDetailsBrowse:
                    {
                        // query = "select " +
                        //    "prloanget.prloangetid as 'PRLOANGETID', stfpersonal.STAFFID as 'STAFFID', " +
                        //    "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname) as 'Name', " +
                        //    "truncate(prloanget.amount,0) as Amount,truncate(prloanget.installment,0) as installment,truncate(prloanget.interest,0) as 'Rate_of_Interest'," +
                        //    " DATE_FORMAT(prloanget.fromdate, '%d/%m/%Y') as 'Pay_From', " +
                        //    "DATE_FORMAT(prloanget.todate, '%d/%m/%Y') as 'Pay_To',prloanget.intrestmode,CONCAT(IFNULL(prloanget.currentinstallment,0) , '/' , IFNULL(prloanget.installment,0) ) as Installments, " +
                        //    "' ' as 'Department'," +
                        //    " prloan.LOANID as 'LOANID', " +
                        //    " prloan.loanname as 'LOAN_NAME', " +
                        //    "IFNULL(prloanget.completed,0) as Completed " +
                        //"from " +
                        //    "prloan, prloanget, stfpersonal " + //, hospital_departments
                        //"where " +
                        //    "prloanget.completed =?COMPLETED and " +
                        //    "prloan.loanid = prloanget.loanid and stfpersonal.staffid = prloanget.staffid  " +
                        //     //  "stfpersonal.deptid = hospital_departments.hdept_id " +
                        //"order by " +
                        //    "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname)";
                        query = "select " +
                          "prloanget.prloangetid as 'PRLOANGETID', stfpersonal.STAFFID as 'STAFFID', " +
                          "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname) as 'Name', " +
                          "truncate(prloanget.amount,0) as Amount,truncate(prloanget.installment,0) as installment,prloanget.interest as 'Rate_of_Interest'," +
                          " DATE_FORMAT(prloanget.fromdate, '%d/%m/%Y') as 'Pay_From', " +
                          "DATE_FORMAT(prloanget.todate, '%d/%m/%Y') as 'Pay_To',prloanget.intrestmode,CONCAT(IFNULL(prloanget.currentinstallment,0) , '/' , IFNULL(prloanget.installment,0) ) as Installments, " +
                          "' ' as 'Department'," +
                          " prloan.LOANID as 'LOANID', " +
                          " prloan.loanname as 'LOAN_NAME', " +
                          "IFNULL(prloanget.completed,0) as Completed " +
                      "from " +
                          "prloan, prloanget, stfpersonal " + //, hospital_departments
                      "where " +
                          "prloanget.completed =?COMPLETED and " +
                          "prloan.loanid = prloanget.loanid and stfpersonal.staffid = prloanget.staffid  " +
                            //  "stfpersonal.deptid = hospital_departments.hdept_id " +
                      "order by " +
                          "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollFetchAssignDetiailBrowse:
                    {
                        // query = "select " +
                        //    "prloanget.prloangetid as 'PRLOANGETID', stfpersonal.empno as 'STAFFID', " +
                        //    "prloan.loanid as 'LOAN_ID', " +
                        //    "stfpersonal.staffid as 'STAFF_ID'," +
                        //    "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname) as 'Name', " +
                        //    "truncate(prloanget.amount,0) as Amount,truncate(prloanget.installment,0) as installment,truncate(prloanget.interest,0) as 'Rate_of_Interest'," +
                        //    " DATE_FORMAT(prloanget.fromdate, '%d/%m/%Y') as 'Pay_From', " +
                        //    "DATE_FORMAT(prloanget.todate, '%d/%m/%Y') as 'Pay_To',prloanget.intrestmode,CONCAT(IFNULL(prloanget.currentinstallment,0) , '/' , IFNULL(prloanget.installment,0) ) as Installments, " +
                        //    "' ' as 'Department'," +
                        //     " prloanget.intrestmode as 'intrestmode', " +
                        //    " prloan.loanname as 'LOAN_NAME', " +
                        //    "IFNULL(prloanget.completed,0) as Completed " +
                        //"from " +
                        //    "prloan, prloanget, stfpersonal " + //, hospital_departments
                        //"where " +
                        //    "prloanget.prloangetid  =?PRLOANGETID and " +
                        //    "prloanget.completed =?COMPLETED and " +
                        //    "prloan.loanid = prloanget.loanid and stfpersonal.staffid = prloanget.staffid  " +
                        //     //  "stfpersonal.deptid = hospital_departments.hdept_id " +
                        //"order by " +
                        //    "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname)";

                        query = "select " +
                          "prloanget.prloangetid as 'PRLOANGETID', stfpersonal.empno as 'STAFFID', " +
                          "prloan.loanid as 'LOAN_ID', " +
                          "stfpersonal.staffid as 'STAFF_ID'," +
                          "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname) as 'Name', " +
                          "truncate(prloanget.amount,0) as Amount,truncate(prloanget.installment,0) as installment,prloanget.interest as 'Rate_of_Interest'," +
                          " DATE_FORMAT(prloanget.fromdate, '%d/%m/%Y') as 'Pay_From', " +
                          "DATE_FORMAT(prloanget.todate, '%d/%m/%Y') as 'Pay_To',prloanget.intrestmode,CONCAT(IFNULL(prloanget.currentinstallment,0) , '/' , IFNULL(prloanget.installment,0) ) as Installments, " +
                          "' ' as 'Department'," +
                           " prloanget.intrestmode as 'intrestmode', " +
                          " prloan.loanname as 'LOAN_NAME', " +
                          "IFNULL(prloanget.completed,0) as Completed " +
                      "from " +
                          "prloan, prloanget, stfpersonal " + //, hospital_departments
                      "where " +
                          "prloanget.prloangetid  =?PRLOANGETID and " +
                          "prloanget.completed =?COMPLETED and " +
                          "prloan.loanid = prloanget.loanid and stfpersonal.staffid = prloanget.staffid  " +
                            //  "stfpersonal.deptid = hospital_departments.hdept_id " +
                      "order by " +
                          "CONCAT(stfpersonal.firstname , ' ', stfpersonal.lastname)";
                        break;
                    }
                case SQLCommand.Payroll.FetchCashBankLedgersofSelectedStaff:
                    {

                        query = "SELECT VT.VOUCHER_ID, VT.LEDGER_ID, ML.LEDGER_NAME\n" +
                       "  FROM VOUCHER_TRANS VT\n" +
                       " INNER JOIN MASTER_LEDGER ML\n" +
                       "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                       " WHERE VT.VOUCHER_ID IN (SELECT VOUCHER_ID\n" +
                       "                           FROM VOUCHER_MASTER_TRANS\n" +
                       "                          WHERE VOUCHER_TYPE = \"PY\"\n" +
                       "                            AND VOUCHER_SUB_TYPE = \"PAY\"\n" +
                       "                            AND CLIENT_REFERENCE_ID = ?PRLOANGETID)\n" +
                       "   AND TRANS_MODE = \"CR\";";

                        break;
                    }
                case SQLCommand.Payroll.FetchLoanType:
                    {
                        //FetchLoanType
                        query = "select loanid, loanname as 'Loan Type' " +
                                        "from prloan " +
                                        "where loanid > 0";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaff:
                    {
                        query = "select staffid, CONCAT(stfpersonal.firstname , ' ' , stfpersonal.lastname) as \"Staff Name\", " +
                           "from stfpersonal " +
                           "where staffid > 0 and leavingdate  is NULL  " +
                           "order by CONCAT(stfpersonal.firstname , ' ' , stfpersonal.lastname)";
                        break;
                    }
                case SQLCommand.Payroll.FetchLoanPaid:
                    {
                        query = "select " +
                        "prcreate.prname as Payroll, to_char(prloanpaid.paiddate,'DD-Mon-YYYY') as \"Paid on\", " +
                        "nvl(prloanpaid.amount,0) as \"Amount\", " +
                        "CONCAT(IFNULL(prloanpaid.installment, '') , '/', IFNULL(prloanget.installment, '')) as Installment " +
                    "from " +
                        "prloanpaid, prloanget, prcreate " +
                    "where " +
                        "prloanget.prloangetid = prloanpaid.prloangetid and " +
                        "prloanpaid.payrollid = prcreate.payrollid and " +
                        "prloanget.prloangetid = ?PRLOANGETID";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffLoanType:
                    {
                        query = "select " +
                        "prloanget.prloangetid as LoanId, " +
                        "concat(stfpersonal.firstname,' ',stfpersonal.lastname ,' - ' , prloan.loanname) " +
                        "as \"Staff Name\" " +
                    "from " +
                        "prloan, prloanget, stfpersonal " +
                    "where " +
                        "prloanget.completed = 0 and prloan.loanid = prloanget.loanid " +
                        "and stfpersonal.staffid = prloanget.staffid " +
                    "order by " +
                        "(stfpersonal.firstname,' ',stfpersonal.lastname ,' - ' , prloan.loanname)";
                        break;
                    }
                #endregion

                #region Payroll Comp Month

                case SQLCommand.Payroll.FetchPayrollCompMonthByGroupId:
                    {
                        query = "SELECT PR.component, PRM.* FROM PRCompMonth PRM\n" +
                            "INNER JOIN PRCOMPONENT PR ON PR.COMPONENTID = PRM.COMPONENTID\n" + 
                            "WHERE PayRollId =?PAYROLLID AND SalaryGroupId = ?SALARYGROUPID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePayrollCompMonthByGrouId:
                    {
                        query = "DELETE FROM prcompmonth WHERE PayRollId = ?PAYROLLID AND SalaryGroupId = ?SALARYGROUPID";
                        break;
                    }
                case SQLCommand.Payroll.UpdatePayrollCompMonthByGroupId:
                    {
                        query = "update prcompmonth set comp_order=?COMP_ORDER where PayRollId = ?PAYROLLID AND SalaryGroupId = ?SALARYGROUPID AND componentid=?COMPONENTID";
                        break;
                    }

                case SQLCommand.Payroll.CheckComponentsMappedORNOt:
                    {
                        query = "select count((componentid)>0) as count from prcompmonth prcm where {SALARYGROUPID in(?SALARYGROUPID) and } PAYROLLID=?PAYROLLID;";
                        break;
                    }

                case SQLCommand.Payroll.CheckStaffGroupMapped:
                    {
                        query = "select count(stf.staffid) as count from PRSTAFFGROUP PSG " +
                            "INNER JOIN STFPERSONAL STF " +
                             " ON STF.STAFFID=PSG.STAFFID " +
                            " where {GROUPID in(?SALARYGROUPID) and } PAYROLLID=?PAYROLLID";
                        // query = "select count(staffid) as count from prstaffgroup where GROUPID IN(?GROUPID) and PAYROLLID=?PAYROLLID;";
                        break;
                    }

                #endregion

                #region PayrollComponentAllocate
                case SQLCommand.Payroll.PayrollComponentList:
                    {
                        query = "SELECT t.COMPONENTID AS COMPONENTID,t.COMPONENT AS 'Component Name', t.RELATEDCOMPONENTS,\n" +
                                 "t.EQUATION, t.type, t.LINKVALUE,\n" +
                                 "CASE WHEN PROCESS_COMPONENT_TYPE=1 THEN '" + PayRollProcessComponent.NetPay.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 2 THEN '" + PayRollProcessComponent.GrossWages.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 3 THEN '" + PayRollProcessComponent.Deductions.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 4 THEN '" + PayRollProcessComponent.EPF.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 5 THEN '" + PayRollProcessComponent.PT.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 7 THEN '" + PayRollProcessComponent.ESI.ToString() + "'\n" +
                                 "  ELSE 'None' END PROCESS_COMPONENT_TYPE,\n" +
                                 "CAST((CASE WHEN t.LINKVALUE = 'Name' THEN -2 WHEN t.LINKVALUE = 'Designation' THEN -1 ELSE t.TYPE END) AS SIGNED) AS TYPE_SORT,\n" +
                                 "CASE TYPE\n" +
                                 "  WHEN 0 THEN\n" +
                                 "    'Earning'\n" +
                                 "  WHEN 1 THEN\n" +
                                 "    'Deduction'\n" +
                                 "  WHEN 2 THEN\n" +
                                 "    'Text'\n" +
                                 "  WHEN 3 THEN\n" +
                                 "    'Calculation'\n" +
                                 "END AS CALCULATION_TYPE\n" +
                                 "FROM PRCOMPONENT t order by t.COMPONENT";
                        break;
                    }
                case SQLCommand.Payroll.PayrollGetGroupList:
                    {
                        query = "SELECT DISTINCT '0' as \"Select\",GROUPNAME AS 'Group Name',GROUPID FROM PRSALARYGROUP ";
                        break;
                    }
                case SQLCommand.Payroll.PayrollFullCompList:
                    {
                        query = "select t.type,t.defvalue,t.linkvalue,t.equation,t.equationid,t.maxslap,t.compround,t.ifcondition, t.relatedcomponents" +
                                " from prcomponent t" +
                                " where t.componentid=?COMPONENTID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCompInsert:
                    {
                        query = "INSERT INTO PRCOMPMONTH(PAYROLLID,SALARYGROUPID,COMPONENTID,TYPE,DEFVALUE,LNKVALUE,EQUATION,EQUATIONID,MAXSLAB,COMPROUND,IFCONDITION)" +
                            " VALUES(?PAYROLLID,?SALARYGROUPID,?COMPONENTID,?TYPE,?DEFVALUE,?LNKVALUE,?EQUATION,?EQUATIONID,?MAXSLAB,?COMPROUND,?IFCONDITION)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCompCheckSelect:
                    {
                        query = "select payrollid,componentid,salarygroupid from prcompmonth where payrollid=?PAYROLLID" +
                           " and componentid=?COMPONENTID and salarygroupid=?SALARYGROUPID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCompDelete:
                    {
                        query = "DELETE FROM PRCOMPMONTH  WHERE PAYROLLID=?PAYROLLID and SALARYGROUPID=?SALARYGROUPID AND COMPONENTID=?COMPONENTID;\n" +
                                "DELETE FROM PRSTAFF WHERE PAYROLLID=?PAYROLLID AND COMPONENTID=?COMPONENTID;\n" +
                                "DELETE FROM PRSTAFFTEMP WHERE PAYROLLID=?PAYROLLID AND COMPONENTID=?COMPONENTID;";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCompchange:
                    {
                        query = "select distinct case when prs.groupid in (select prc.SALARYGROUPID from prsalarygroup prs,prcompmonth prc" +
                      " where prs.groupid = prc.salarygroupid and prc.payrollid=?PAYROLLID and prc.componentid=?COMPONENTID ) then '1' else '0' end as \"Select\",prs.groupname AS 'Group Name',prs.groupid" +
                      " from prsalarygroup prs,prcompmonth prc where prc.componentid=?COMPONENTID and (prs.groupid=prc.salarygroupid" +
                      " or prs.groupid not in (prc.salarygroupid)) and prc.payrollid=?PAYROLLID" +
                      " order by prs.groupname";
                        break;
                    }

                case SQLCommand.Payroll.FetchMapComponent:
                    {

                        query = "select distinct case" +
                        "            when prc.componentid in" +
                        "                       (select prc.componentid" +
                        "                          from prcompmonth prs, prcomponent prc" +
                        "                         where prs.componentid = prc.componentid" +
                        "                           and prs.payrollid =?PAYROLLID" +
                        "                           and prs.salarygroupid =?SALARYGROUPID) then" +
                        "                   '1'" +
                        "                  else" +
                        "                   '0'" +
                        "           end as \"SELECT\"," +
                        "           prc.component AS 'Component Name'," +
                        "           prc.componentid, prc.relatedcomponents, " +
                        " CASE WHEN PROCESS_COMPONENT_TYPE=1 THEN '" + PayRollProcessComponent.NetPay.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 2 THEN '" + PayRollProcessComponent.GrossWages.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 3 THEN '" + PayRollProcessComponent.Deductions.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 4 THEN '" + PayRollProcessComponent.EPF.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 5 THEN '" + PayRollProcessComponent.PT.ToString() + "'\n" +
                                 "  WHEN PROCESS_COMPONENT_TYPE = 7 THEN '" + PayRollProcessComponent.ESI.ToString() + "'\n" +
                                 "  ELSE 'None'\n" +
                                 "END PROCESS_COMPONENT_TYPE\n" +
                        "  from prcompmonth prs, prcomponent prc" +
                        " where prs.salarygroupid =?SALARYGROUPID" +
                        "   and (prc.componentid = prs.componentid or" +
                        "       prc.componentid not in (prs.componentid))" +
                        "   and prs.payrollid = ?PAYROLLID" +
                        " order by prc.component;";
                        break;
                    }


                case SQLCommand.Payroll.PayrollCompStaffID:
                    {
                        query = "select prg.staffid from prstaffgroup prg,prcompmonth prc where (prc.salarygroupid=?SALARYGROUPID and prg.groupid=?GROUPID) and (prc.payrollid=?payrollid and prg.payrollid=?PAYROLLID)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCompName:
                    {
                        query = "select  prc.componentid from prcompmonth pr,prcomponent prc where pr.salarygroupid=?SALARYGROUPID and prc.componentid=pr.componentid and pr.payrollid=?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollProcessDelete:
                    {
                        query = "delete from prstaff p where p.payrollid=?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollFormulaForGroup:
                    {
                        query = "SELECT " +
                                  "PRC.SALARYGROUPID, PRC.COMPONENTID,PRC.EQUATION,PRC.EQUATIONID,PRC.TYPE " +
                            "FROM PRSALARYGROUP PRS, PRCOMPMONTH PRC " +
                            "WHERE PRS.GROUPID = PRC.SALARYGROUPID " +
                             "AND PRC.PAYROLLID = ?PAYROLLID " +
                             "AND PRC.SALARYGROUPID = ?SALARYGROUPID " +
                             "ORDER BY PRC.COMPONENTID ";
                        break;
                    }
                case SQLCommand.Payroll.PayrollProcessCheck:
                    {
                        query = "select p.staffid,p.componentid,p.compvalue from prstaff p where payrollid=?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollInsertProcess:
                    {
                        query = "insert into prstaff(payrollid,staffid,componentid,compvalue)" +
                        " values(?PAYROLLID,?staffid,?COMPONENTID,?compvalue)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCompIdReturn:
                    {
                        query = "select t.componentid from prcompmonth t where t.componentid=?COMPONENTID";
                        break;

                    }
                case SQLCommand.Payroll.FetchPRCompMonthByComponentId:
                    {
                        query = "SELECT * FROM PRCompMonth WHERE PayRollId =?PAYROLLID and ComponentId = ?COMPONENTID";
                        break;
                    }
                case SQLCommand.Payroll.PrcompMonthAdd:
                    {
                        query = "INSERT INTO PRCOMPMONTH(SALARYGROUPID,PAYROLLID,COMPONENTID,TYPE,DEFVALUE,LNKVALUE,EQUATION,EQUATIONID,MAXSLAB,COMP_ORDER,COMPROUND,IFCONDITION)" +
                        " VALUES(?SALARYGROUPID,?PAYROLLID,?COMPONENTID,?TYPE,?DEFVALUE,?LNKVALUE,?EQUATION,?EQUATIONID,?MAXSLAB,?COMP_ORDER,?COMPROUND,?IFCONDITION)";
                        break;
                    }
                case SQLCommand.Payroll.ClearPayExtraInfo://On 30/07/2019
                    {
                        query = "DELETE PST.* FROM PRSTAFFTEMP PST\n" +
                                "INNER JOIN (SELECT PRM.COMPONENTID, PSG.STAFFID FROM PRCOMPMONTH PRM\n" +
                                " INNER JOIN PRSTAFFGROUP PSG ON PSG.GROUPID = PRM.SALARYGROUPID AND PSG.PAYROLLID = PRM.PAYROLLID AND PSG.PAYROLLID = ?PAYROLLID AND PSG.GROUPID = ?SALARYGROUPID\n" +
                                " LEFT JOIN PRCOMPONENT PC ON PC.COMPONENTID = PRM.COMPONENTID\n" +
                                " WHERE PRM.PAYROLLID = ?PAYROLLID AND PRM.SALARYGROUPID =?SALARYGROUPID AND \n" +
                                " (PRM.LNKVALUE IN ('" + PayRollExtraPayInfo.EARNING1.ToString() + "','" + PayRollExtraPayInfo.EARNING1.ToString() + "',\n" +
                                         "'" + PayRollExtraPayInfo.EARNING2.ToString() + "','" + PayRollExtraPayInfo.EARNING3.ToString() + "',\n" +
                                         "'" + PayRollExtraPayInfo.DEDUCTION1.ToString() + "','" + PayRollExtraPayInfo.DEDUCTION2.ToString() + "',\n" +
                                         "'" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "") + "','TOTALDAYSINPAYMONTH',\n" +
                                         "'" + PayRollExtraPayInfo.BASICPAY.ToString() + "') OR (PC.ISEDITABLE = 0) )) AS T\n"+ //(PC.ISEDITABLE = 0 AND PRM.EQUATION <> '')
                                "ON T.COMPONENTID = PST.COMPONENTID AND T.STAFFID = PST.STAFFID AND PST.PAYROLLID = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.ResetPayExtraInfo://On 30/07/2019
                    {
                        query = "INSERT INTO PRSTAFFTEMP (PAYROLLID, STAFFID, COMPONENTID, COMPVALUE) \n" +
                                    "SELECT PRS.PAYROLLID, STFS.STAFFID, PRM.COMPONENTID,\n" +
                                    "(CASE WHEN LNKVALUE = '" + PayRollExtraPayInfo.EARNING1 + "' THEN EARNING1\n" +
                                        "WHEN LNKVALUE = '" + PayRollExtraPayInfo.EARNING2 + "' THEN EARNING2\n" +
                                        "WHEN LNKVALUE = '" + PayRollExtraPayInfo.EARNING3 + "' THEN EARNING3\n" +
                                        "WHEN LNKVALUE = '" + PayRollExtraPayInfo.DEDUCTION1 + "' THEN DEDUCTION1\n" +
                                        "WHEN LNKVALUE = '" + PayRollExtraPayInfo.DEDUCTION2 + "' THEN DEDUCTION2\n" +
                                        "WHEN LNKVALUE = '" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "") + "' THEN PAYING_SALARY_DAYS\n" +
                                        "WHEN LNKVALUE = '" + PayRollExtraPayInfo.BASICPAY + "' THEN REPLACE(PAY,',','')\n" +
                                        "WHEN LNKVALUE = 'TOTALDAYSINPAYMONTH' THEN ?TOTALDAYSINPAYMONTH END) AS COMPVALUE\n" +
                                    "FROM PRCOMPMONTH AS PRM\n" +
                                    "INNER JOIN PRSTAFF AS PRS ON PRS.COMPONENTID = PRM.COMPONENTID AND PRM.PAYROLLID = PRS.PAYROLLID AND PRS.PAYROLLId = ?PAYROLLID\n" +
                                    "INNER JOIN STFSERVICE STFS ON STFS.STAFFID = PRS.STAFFID\n" +
                                    "INNER JOIN PRSTAFFGROUP PSG ON PSG.STAFFID = STFS.STAFFID AND PSG.STAFFID = PRS.STAFFID AND\n" +
                                            "PSG.GROUPID = PRM.SALARYGROUPID AND PSG.PAYROLLID = ?PAYROLLID AND PSG.GROUPID =?SALARYGROUPID\n" +
                                    "WHERE PRM.SALARYGROUPID= ?SALARYGROUPID AND PRM.PAYROLLID = ?PAYROLLID AND\n" +
                                    "LNKVALUE IN ('" + PayRollExtraPayInfo.EARNING1.ToString() + "','" + PayRollExtraPayInfo.EARNING1.ToString() + "',\n" +
                                    "'" + PayRollExtraPayInfo.EARNING2.ToString() + "','" + PayRollExtraPayInfo.EARNING3.ToString() + "',\n" +
                                    "'" + PayRollExtraPayInfo.DEDUCTION1.ToString() + "','" + PayRollExtraPayInfo.DEDUCTION2.ToString() + "',\n" +
                                    "'" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "") + "','TOTALDAYSINPAYMONTH',\n" +
                                    "'" + PayRollExtraPayInfo.BASICPAY.ToString() + "')";
                        break;
                    }
                case SQLCommand.Payroll.PRCompMonthDeleteByCompGroup:
                    {
                        query = "DELETE FROM PRCompMonth WHERE PayRollId =?PAYROLLID and ComponentId =?COMPONENTID and SalaryGroupId =?SALARYGROUPID";
                        break;
                    }
                case SQLCommand.Payroll.FetchPrReProcess:
                    {
                        query = "SELECT DISTINCT PRSalaryGroup.GroupId,PRSalaryGroup.GroupName," +
                                "PRCompMonth.ComponentId,PRComponent.Component FROM " +
                                "PRSalaryGroup,PRCompMonth,PRComponent WHERE PRCompMonth.PayRollId = ?PAYROLLID AND PRCompMonth.SalaryGroupId = PRSalaryGroup.GroupId " +
                                "AND PRCompMonth.ComponentId = PRComponent.ComponentId ORDER BY PRSalaryGroup.GroupName,PRComponent.Component";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollAbstractComponent:
                    {
                        //query = "SELECT STFPERSONAL.EMPNO AS 'ID',\n" +
                        //        "       CONCAT(STFPERSONAL.FIRSTNAME, ' ', STFPERSONAL.LASTNAME) AS 'Name',\n" +
                        //        "       STFPERSONAL.DESIGNATION AS 'DESIGNATION',\n" +
                        //        "       SUM(PRSTAFF.COMPVALUE) AS 'AMOUNT'\n" +
                        //        "  FROM PRSTAFF, STFPERSONAL,PRSTAFFGROUP,PRPROJECT_STAFF\n" +
                        //        " WHERE PRSTAFF.PAYROLLID = ?PAYROLLID\n" +
                        //        "   AND PRSTAFF.COMPONENTID IN (?IDs)\n" +
                        //        "   AND PRSTAFFGROUP.GROUPID IN (?GroupId)\n" +
                        //        "   AND PRSTAFF.STAFFID = STFPERSONAL.STAFFID\n" +
                        //        "   AND PRSTAFFGROUP.STAFFID = PRSTAFF.STAFFID\n" +
                        //        " AND PRPROJECT_STAFF.STAFFID=STFPERSONAL.STAFFID \n"+
                        //        " {  AND PRPROJECT_STAFF.PROJECT_ID IN (?PROJECT_ID) }\n" +
                        //        " GROUP BY CONCAT(STFPERSONAL.FIRSTNAME, ' ', STFPERSONAL.LASTNAME),DESIGNATION";

                        query = "SELECT STF.EMPNO AS 'ID',\n" +
                                "  CONCAT(STF.FIRSTNAME,CONCAT(' ', IFNULL(STF.MIDDLE_NAME,'')),CONCAT(' ',STF.LASTNAME)) AS 'Name',\n" +
                                "  STF.DESIGNATION AS 'DESIGNATION',\n" +
                                "  SUM(PRS.COMPVALUE) AS 'AMOUNT'\n" +
                                "  FROM PRSTAFF PRS\n" +
                                " INNER JOIN STFPERSONAL STF\n" +
                                "    ON STF.STAFFID = PRS.STAFFID\n" +
                                " INNER JOIN PRSTAFFGROUP PSG\n" +
                                "    ON PSG.STAFFID = PRS.STAFFID\n" +
                                "  LEFT JOIN PRPROJECT_STAFF PPF\n" +
                                "    ON PPF.STAFFID = STF.STAFFID\n" +
                                " WHERE PRS.COMPONENTID IN (?IDs)\n" +
                                "   AND PRS.PAYROLLID = ?PAYROLLID\n" +
                                "   AND PSG.GROUPID IN (?GroupId)\n" +
                                "{  AND PPF.PROJECT_ID IN (?PROJECT_ID) }\n" +
                                " GROUP BY CONCAT(STF.FIRSTNAME, ' ', STF.LASTNAME), DESIGNATION;";


                        break;
                    }
                #endregion

                #region Payslip

                case SQLCommand.Payroll.FetchReportCode:
                    {
                        query = "SELECT RPT_NAME, RPT_CODE " +
                " FROM REPORT_MAIN WHERE RPT_TYPE = 1 AND " +
                " RPT_MODULE IS NOT NULL";

                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollDate:
                    {
                        query = "Select DATE_FORMAT(prDate,'%d/%m/%Y') as prDate from PRCreate where payrollid = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollDateForPayslip:
                    {
                        query = "SELECT DATE_FORMAT(FROMDATE,'%d/%m/%Y') as FROMDATE,DATE_FORMAT(TODATE,'%d/%m/%Y') as TODATE FROM PAYROLL where payrollid = ?PAYROLLID ";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffDetailsByStaffId:
                    {
                        query = "select distinct pr.staffid, CONCAT(IFNULL(CONCAT(p.NAME_TITLE, ' '), ''), p.firstname,CONCAT(' ', IFNULL(p.MIDDLE_NAME,'')),CONCAT(' ',p.lastname)) as Name, grp.GROUPNAME\n" +
                                  "from prstaff pr, (SELECT sp.*, PNT.NAME_TITLE FROM stfPersonal as sp LEFT JOIN PR_NAME_TITLE PNT ON PNT.NAME_TITLE_ID = sp.NAME_TITLE_ID) AS p,prstaffgroup pg, prsalarygroup grp\n" +
                                 " where pr.staffid = p.staffid and pg.staffid = pr.staffid and pr.payrollid = ?PAYROLLID and\n" +
                                 " pg.payrollid = ?PAYROLLID and pg.groupid IN (?GROUPID)\n" +
                                  "and grp.GROUPID = pg.GROUPID ORDER BY grp.GROUPNAME, IFNULL(pg.STAFFORDER,0)";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffAllDetails:
                    {
                        query = "select distinct pr.staffid, CONCAT(IFNULL(CONCAT(p.NAME_TITLE, ' '), ''), p.firstname,CONCAT(' ', IFNULL(p.MIDDLE_NAME,'')),CONCAT(' ',p.lastname)) as Name " +
                            " from prstaff pr,(SELECT sp.*, PNT.NAME_TITLE FROM stfPersonal as sp LEFT JOIN PR_NAME_TITLE PNT ON PNT.NAME_TITLE_ID = sp.NAME_TITLE_ID) AS p, prstaffgroup pg" +
                            " where pr.staffid = p.staffid and pg.staffid = pr.staffid and pr.payrollid = ?PAYROLLID and pg.payrollid = ?PAYROLLID ";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffDetailsForDailyReport:
                    {
                        query = "select distinct pr.staffid,CONCAT(p.FIRSTNAME ,' ',p.LASTNAME) as Name from prstaff pr,stfpersonal p,prstaffgroup pg" +
                                 " where pr.staffid = p.staffid and pg.staffid = pr.staffid and pr.payrollid = ?PAYROLLID and pg.payrollid = ?PAYROLLID ";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffDetailsByDepartmentId:
                    {
                        query = " select distinct pr.staffid,CONCAT(p.FIRSTNAME ,' ',p.LASTNAME) as Name from prstaff pr,stfpersonal p" +
                    " where pr.staffid = p.staffid and pr.payrollid =?PAYROLLID and p.deptid =?deptid";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollCompMonth:
                    {
                        query = "SELECT GROUPID,GROUPNAME FROM PRSALARYGROUP";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentForReport:
                    {
                        query = "SELECT DISTINCT PRComponent.ComponentId,PRComponent.Component,PRComponent.Type," +
                                "PRCompMonth.Comp_Order,TRUNCATE(PRComponent.CompRound,0) AS CompRound  FROM PRComponent,PRCompMonth " +
                                "WHERE PRComponent.ComponentId = PRCompMonth.ComponentId AND " +
                                "PRCompMonth.PayRollId =?PAYROLLID " +
                                " ORDER BY PRCompMonth.Comp_Order";
                        break;
                    }

                case SQLCommand.Payroll.FetchValuesbyComponent:
                    {
                        query = "SELECT PRStaffGroup.StaffId,PRStaffGroup.GroupId," +
                                "PRStaff.ComponentId,PRStaff.CompValue," +
                                "PRComponent.Component FROM PRStaffGroup,PRStaff,PRComponent" +
                                " WHERE PRComponent.ComponentId = PRStaff.ComponentId" +
                                " AND PRStaff.PayRollId = ?PAYROLLID " +
                                " AND PRStaffGroup.Payrollid = PRStaff.Payrollid" +
                                " AND PRStaff.StaffId = PRStaffGroup.StaffId" +
                                " ORDER BY PRStaffGroup.StaffId";
                        break;
                    }
                case SQLCommand.Payroll.FetchValuesbyComponentStaffGroup:
                    {

                        query = "SELECT D.STAFFID, D.STAFFORDER, D.GROUPNAME,\n" +
                                "       TRIM(GROUP_CONCAT(D.EARNINGS ORDER BY D.STAFFID SEPARATOR ' ')) AS EARNINGS,\n" +
                                "       TRIM(GROUP_CONCAT(D.EARNINGS_DESCRIPTION ORDER BY D.STAFFID SEPARATOR ' ')) AS EARNINGS_DESCRIPTION,\n" +
                                "       CASE\n" +
                                "         WHEN TRIM(GROUP_CONCAT(D.EARNINGS ORDER BY D.STAFFID SEPARATOR ' ')) = '' THEN\n" +
                                "          0.00\n" +
                                "         ELSE\n" +
                                "          SUM(D.EAMOUNT)\n" +
                                "       END AS EAMOUNT,\n" +
                                "       TRIM(GROUP_CONCAT(D.DEDUCTIONS ORDER BY D.STAFFID SEPARATOR ' ')) AS DEDUCTIONS,\n" +
                                "       TRIM(GROUP_CONCAT(D.DEDUCTION_DESCRIPTION ORDER BY D.STAFFID SEPARATOR ' ')) AS DEDUCTION_DESCRIPTION,\n" +
                                "       CASE\n" +
                                "         WHEN TRIM(GROUP_CONCAT(D.DEDUCTIONS ORDER BY D.STAFFID SEPARATOR ' ')) = '' THEN\n" +
                                "          0.00\n" +
                                "         ELSE\n" +
                                "          SUM(D.DAMOUNT)\n" +
                                "       END AS DAMOUNT,\n" +
                                "       TRIM(GROUP_CONCAT(D.TEXTNAME ORDER BY D.STAFFID SEPARATOR ' ')) AS TEXTNAME,\n" +
                                "       TRIM(GROUP_CONCAT(D.TEXTVALUE ORDER BY D.STAFFID SEPARATOR ' ')) AS TEXTVALUE,\n" +
                                "       SUM(D.NETPAY) AS 'NET PAY', TRIM(GROUP_CONCAT(D.EARNINGS_ORDER ORDER BY D.STAFFID SEPARATOR ' ')) AS EARNINGS_ORDER,\n" +
                                "       TRIM(GROUP_CONCAT(D.DEDUCTION_ORDER ORDER BY D.STAFFID SEPARATOR ' ')) AS DEDUCTION_ORDER,\n" +
                                "       TEXT_ORDER, NETPAY_ORDER\n" +
                                "  FROM (SELECT T.STAFFID, IFNULL(PG.STAFFORDER,0) AS STAFFORDER, SG.GROUPNAME, PR.COMPONENT AS EARNINGS, T.AMOUNT AS EAMOUNT, '' AS DEDUCTIONS,\n" +
                                "               0 AS DAMOUNT,PR.DESCRIPTION AS EARNINGS_DESCRIPTION, '' AS DEDUCTION_DESCRIPTION,\n" +
                                "               @VNO := IF(@VPREV_VALUE = PS.STAFFID, @VNO + 1, 1) AS SNO,\n" +
                                "               @VPREV_VALUE := PS.STAFFID AS PVAL,\n" +
                                "               '' AS TEXTNAME,\n" +
                                "               '' AS TEXTVALUE,\n" +
                                "               0 AS NETPAY, PRM.COMP_ORDER AS EARNINGS_ORDER, '' AS DEDUCTION_ORDER, '' AS TEXT_ORDER, '' AS NETPAY_ORDER\n" +
                                "          FROM PRSTAFF PS\n" +
                                "         INNER JOIN (SELECT STAFFID, COMPONENTID, COMPVALUE AS AMOUNT\n" +
                                "                      FROM PRSTAFF,\n" +
                                "                           (SELECT @VNO := 0) X,\n" +
                                "                           (SELECT @VPREV_VALUE := 0) Y\n" +
                                "                     WHERE PAYROLLID = ?PAYROLLID\n" +
                                "                       AND STAFFID IN (?StaffId)\n" +
                                "                     ORDER BY STAFFID) AS T\n" +
                                "            ON T.STAFFID = PS.STAFFID\n" +
                                "         INNER JOIN PRCOMPONENT PR\n" +
                                "            ON PR.COMPONENTID = T.COMPONENTID\n" +
                                "           AND PS.PAYROLLID = ?PAYROLLID\n" +
                                "         INNER JOIN prcompmonth PRM ON PRM.COMPONENTID = PR.COMPONENTID AND PRM.PAYROLLID = ?PAYROLLID {AND PRM.SALARYGROUPID=?GroupId}\n" +
                                "         INNER JOIN PRSTAFFGROUP PG ON PG.STAFFID = PS.STAFFID AND PG.PAYROLLID = ?PAYROLLID\n" +
                                "         INNER JOIN PRSALARYGROUP SG ON SG.GROUPID = PG.GROUPID\n" +
                                "         WHERE PR.TYPE IN (0)\n" +
                                "       { AND PG.GROUPID IN (?GroupId) } \n" +
                            //On 03/06/2019, To get all incomes component except (GROSS WAGES and NETPAY) (discussion alwar, chinna and kalis)
                            //"  AND LENGTH(RELATEDCOMPONENTS)-LENGTH(REPLACE(RELATEDCOMPONENTS,'ê',''))<=2 -- AND PR.COMPONENT NOT IN ('GROSS WAGES', 'NETPAY')\n" +
                            //" AND PR.COMPONENT NOT IN ('GROSS WAGES', 'NETPAY')\n" +
                                "   AND PR.PROCESS_COMPONENT_TYPE NOT IN (" + (int)PayRollProcessComponent.GrossWages + "," + (int)PayRollProcessComponent.NetPay + ")\n" +
                                "         GROUP BY STAFFID, COMPONENT\n" +
                                "        UNION ALL\n" +
                                "        SELECT T.STAFFID, IFNULL(PG.STAFFORDER,0) AS STAFFORDER, SG.GROUPNAME,'' AS EARNING, 0 AS EAMOUNT, PR.COMPONENT AS DEDUCTION,\n" +
                                "               T.AMOUNT AS DAMOUNT,'' AS EARNINGS_DESCRIPTION, PR.DESCRIPTION AS DEDUCTION_DESCRIPTION,\n" +
                                "               @RNO := IF(@PREV_VALUE = PS.STAFFID, @RNO + 1, 1) AS SNO,\n" +
                                "               @PREV_VALUE := PS.STAFFID AS PVAL,\n" +
                                "               '' AS TEXTNAME,\n" +
                                "               '' AS TEXTVALUE,\n" +
                                "               0 AS NETPAY, '' AS EARNINGS_ORDER, PRM.COMP_ORDER AS DEDUCTION_ORDER, '' AS TEXT_ORDER, '' AS NETPAY_ORDER\n" +
                                "          FROM PRSTAFF PS\n" +
                                "         INNER JOIN (SELECT STAFFID, COMPONENTID, COMPVALUE AS AMOUNT\n" +
                                "                       FROM PRSTAFF,\n" +
                                "                            (SELECT @RNO := 0) X,\n" +
                                "                            (SELECT @PREV_VALUE := 0) Y\n" +
                                "                     WHERE PAYROLLID = ?PAYROLLID\n" +
                                "                       AND STAFFID IN (?StaffId)\n" +
                                "                      ORDER BY STAFFID) AS T\n" +
                                "            ON T.STAFFID = PS.STAFFID\n" +
                                "         INNER JOIN PRCOMPONENT PR\n" +
                                "            ON PR.COMPONENTID = T.COMPONENTID\n" +
                                "           AND PS.PAYROLLID = ?PAYROLLID\n" +
                                "         INNER JOIN prcompmonth PRM ON PRM.COMPONENTID = PR.COMPONENTID AND PRM.PAYROLLID = ?PAYROLLID {AND PRM.SALARYGROUPID=?GroupId}\n" +
                                "         INNER JOIN PRSTAFFGROUP PG ON PG.STAFFID = PS.STAFFID AND PG.PAYROLLID = ?PAYROLLID\n" +
                                "         INNER JOIN PRSALARYGROUP SG ON SG.GROUPID = PG.GROUPID\n" +
                                "         WHERE PR.TYPE IN (1)\n" +
                                "       { AND PG.GROUPID IN (?GroupId) } \n" +
                            //On 03/06/2019, To get all incomes component except (GROSS WAGES and NETPAY) (discussion alwar, chinna and kalis)
                            //"    AND LENGTH(RELATEDCOMPONENTS)-LENGTH(REPLACE(RELATEDCOMPONENTS,'ê',''))<=2 --   AND PR.COMPONENT NOT IN ('DEDUCTIONS', 'PF WAGES')\n" +
                            //"     AND PR.COMPONENT NOT IN ('DEDUCTIONS', 'PF WAGES')\n" +
                                "       AND PR.PROCESS_COMPONENT_TYPE NOT IN (" + (int)PayRollProcessComponent.Deductions + ")\n" +
                                "         GROUP BY STAFFID, COMPONENT\n" +
                                "        ) AS D\n" +
                                " GROUP BY D.STAFFID, D.SNO";
                        break;
                    }
                case SQLCommand.Payroll.PostedPayrollVouchers:
                    {
                        query = @"SELECT VM.VOUCHER_ID, MP.PROJECT_ID, PR.PRNAME, GROUP_CONCAT(DISTINCT PG.GROUPNAME ORDER BY PG.GROUPNAME SEPARATOR  ', ') AS PAYROLL_GROUP,
                                  VOUCHER_DATE, VOUCHER_NO, VOUCHER_SUB_TYPE, RCPYCN.LEDGER_NAME AS LEDGER_NAME, CASHBANK.LEDGER_NAME AS CASHBANK,    
                                  CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',
                                       CASE VM.VOUCHER_TYPE
                                         WHEN 'RC' THEN
                                          'Receipt'
                                         WHEN 'PY' THEN
                                          'Payment'
                                         WHEN 'CN' THEN
                                          'Contra'
                                         ELSE
                                          'JOURNAL'
                                       END AS VOUCHERTYPE, IF(VM.VOUCHER_TYPE = 'PY', CASHBANK.AMOUNT, 0) AS DEBIT,
                                  IF(VM.VOUCHER_TYPE = 'RC' OR VM.VOUCHER_TYPE = 'CN', CASHBANK.AMOUNT, 0) AS CREDIT, VM.NARRATION,
                                  IF(VM.VOUCHER_DEFINITION_ID<=4, CASE VM.VOUCHER_TYPE
                                         WHEN 'RC' THEN 'Receipt'
                                         WHEN 'PY' THEN 'Payment'
                                         WHEN 'CN' THEN 'Contra'
                                         ELSE 'Journal' END, 
                                       MV.VOUCHER_NAME) AS VOUCHER_TYPE, VM.VOUCHER_DEFINITION_ID 
                                 FROM VOUCHER_MASTER_TRANS AS VM
                                 INNER JOIN MASTER_PROJECT AS MP ON VM.PROJECT_ID = MP.PROJECT_ID
                                 INNER JOIN MASTER_DIVISION AS MD ON MP.DIVISION_ID = MD.DIVISION_ID
                                 INNER JOIN PAYROLL_VOUCHER PV  ON PV.VOUCHER_ID = VM.VOUCHER_ID
                                 INNER JOIN PRCREATE PR ON PR.PAYROLLID = PV.PAYROLL_ID
                                 INNER JOIN PRSALARYGROUP PG ON PG.GROUPID = PV.SALARY_GROUP_ID
                                 LEFT JOIN MASTER_VOUCHER AS MV ON MV.VOUCHER_ID = VM.VOUCHER_DEFINITION_ID

                                 LEFT JOIN (SELECT T.VOUCHER_ID, T.LEDGER_NAME, SUM(T.AMOUNT) AS AMOUNT
                                               FROM (SELECT VT.VOUCHER_ID,
                                                            CASE
                                                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),
                                                                      CONCAT(MB.BANK, ' - '),
                                                                      MB.BRANCH)
                                                              ELSE
                                                               ML.LEDGER_NAME END AS LEDGER_NAME, VT.TRANS_MODE, VT.AMOUNT
                                                      FROM VOUCHER_TRANS VT
                                                      INNER JOIN VOUCHER_MASTER_TRANS VM
                                                         ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VM.VOUCHER_TYPE ='PY' AND VM.VOUCHER_SUB_TYPE ='PAY'
                                                      LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID
                                                      LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID
                                                      LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID
                                                      LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID
                                                      WHERE IF(VM.VOUCHER_TYPE = 'CN', VT.TRANS_MODE = 'CR', ML.GROUP_ID NOT IN (12, 13))
                                                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T
                                            GROUP BY T.VOUCHER_ID) AS RCPYCN ON RCPYCN.VOUCHER_ID = VM.VOUCHER_ID

                                 LEFT JOIN (SELECT T.VOUCHER_ID, T.LEDGER_NAME, SUM(T.AMOUNT) AS AMOUNT, T.TRANS_MODE
                                               FROM (SELECT VT.VOUCHER_ID,
                                                            CASE
                                                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN
                                                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),
                                                                      CONCAT(MB.BANK, ' - '),
                                                                      MB.BRANCH)
                                                              ELSE
                                                               ML.LEDGER_NAME
                                                            END AS LEDGER_NAME, VT.AMOUNT AS AMOUNT, TRANS_MODE
                                                      FROM VOUCHER_TRANS VT
                                                      INNER JOIN VOUCHER_MASTER_TRANS VM ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VM.VOUCHER_TYPE ='PY' AND VM.VOUCHER_SUB_TYPE ='PAY'
                                                      LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID
                                                      LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID
                                                      LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID
                                                      LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID
                                                      WHERE IF(VM.VOUCHER_TYPE = 'CN', VT.TRANS_MODE = 'DR', ML.GROUP_ID IN (12, 13))
                                                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T
                                              GROUP BY T.VOUCHER_ID) AS CASHBANK ON CASHBANK.VOUCHER_ID = VM.VOUCHER_ID

                                 WHERE VM.VOUCHER_DATE BETWEEN ?FROMDATE AND ?TODATE AND VM.VOUCHER_TYPE ='PY' AND VM.VOUCHER_SUB_TYPE ='PAY' AND VM.STATUS = 1
                                 GROUP BY VM.VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.Payroll.PostedPayrollVoucherDetail:
                    {
                        query = @"SELECT VT.VOUCHER_ID, IFNULL(PVDetails.COMPONENT,'') AS COMPONENT, 
                                           CASE WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN CONCAT(CONCAT(ML.LEDGER_NAME, ' - '), CONCAT(MB.BANK, ' - '), MB.BRANCH)
                                           ELSE ML.LEDGER_NAME END AS LEDGER_NAME, 
                                           VT.LEDGER_ID,VT.SEQUENCE_NO, VT.TRANS_MODE,
                                           CASE VT.TRANS_MODE
                                             WHEN 'CR' THEN
                                              VT.AMOUNT
                                             ELSE
                                              0.00
                                           END AS 'CREDIT',
                                           CASE VT.TRANS_MODE
                                             WHEN 'DR' THEN
                                              VT.AMOUNT
                                             ELSE
                                              0.00
                                           END AS 'DEBIT',
                                           MBA.ACCOUNT_NUMBER,
                                           CONCAT(CHEQUE_NO, CONCAT(CONCAT(IF(CHEQUE_REF_DATE IS NULL OR CHEQUE_NO='','', CONCAT(' - ',DATE_FORMAT(CHEQUE_REF_DATE,'%d/%m/%Y'))),
                                           IF(CHEQUE_REF_DATE IS NULL,'', CONCAT(', ', CHEQUE_REF_BANKNAME))),
                                           IF(CHEQUE_REF_BANKNAME IS NULL OR CHEQUE_REF_BANKNAME='', '', CONCAT(', ', CHEQUE_REF_BRANCH)))) AS CHEQUE_NO,
                                           MATERIALIZED_ON
                                    FROM VOUCHER_TRANS AS VT
                                    INNER JOIN VOUCHER_MASTER_TRANS VMT ON VT.VOUCHER_ID = VMT.VOUCHER_ID
                                    INNER JOIN MASTER_LEDGER AS ML     ON VT.LEDGER_ID = ML.LEDGER_ID
                                    LEFT JOIN (SELECT PV.VOUCHER_ID, PV.COMPONENT_ID, PV.LEDGER_ID, GROUP_CONCAT(PC.COMPONENT) AS COMPONENT , IF(PC.TYPE=0, 'DR', 'CR') AS TRANS_MODE
                                    FROM PAYROLL_VOUCHER PV
                                    INNER JOIN PRCOMPONENT PC  ON PC.COMPONENTID = PV.COMPONENT_ID
                                    GROUP BY PV.VOUCHER_ID, PV.LEDGER_ID, TRANS_MODE) AS PVDetails ON PVDetails.VOUCHER_ID = VT.VOUCHER_ID AND PVDetails.LEDGER_ID = VT.LEDGER_ID AND PVDetails.TRANS_MODE = VT.TRANS_MODE
                                    LEFT JOIN MASTER_BANK_ACCOUNT AS MBA ON MBA.LEDGER_ID = ML.LEDGER_ID
                                    LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID
                                    WHERE VMT.VOUCHER_DATE BETWEEN ?FROMDATE AND ?TODATE AND VMT.STATUS = 1 AND VOUCHER_TYPE  = 'PY' AND VOUCHER_SUB_TYPE = 'PAY'"; //ORDER BY VOUCHER_ID,SEQUENCE_NO,ML.GROUP_ID NOT IN(12,13) DESC;
                        break;
                    }
                case SQLCommand.Payroll.RemoveDefaultComponentsForMultiCurrency:
                    {
                        query = @"DELETE PR.* FROM PRCOMPONENT PR LEFT JOIN PRCOMPMONTH PRM ON PR.COMPONENTID = PRM.COMPONENTID
                                   WHERE PRM.COMPONENTID IS NULL AND COMPONENT IN ('DA', 'HRA', 'EPF', 'GROSS WAGES', 'DEDUCTIONS',
                                    'NETPAY', 'LOPDAYS', 'TotalDays', 'WorkingDays','LeaveDays') AND ACCESS_FLAG = 1;";
                        break;
                    }
                case SQLCommand.Payroll.FetchValuesForPaySlip:
                    {
                        query = "SELECT SS.STAFFID," + "\r\n" +
                                "       MAXWAGESBASIC AS MINWAGESBASIC," + "\r\n" +
                                "       MAXWAGESHRA AS MINWAGESDA," + "\r\n" +
                                "       MAXWAGESBASIC + MAXWAGESHRA AS TOTAL," + "\r\n" +
                                "       DATE_FORMAT(TRANSACTIONDATE, '%d/%m/%Y') AS SALARYDATE," + "\r\n" +
                                "       DESIGNATION," + "\r\n" +
                                "       GROUPID" + "\r\n" +
                                "  FROM stfservice SS, PRSTAFF PS, PRSTAFFGROUP SG, STFPERSONAL SP" + "\r\n" +
                                " WHERE     SS.STAFFID = PS.STAFFID" + "\r\n" +
                                "       AND SG.STAFFID = SS.STAFFID" + "\r\n" +
                                "       AND SP.STAFFID = SS.STAFFID" + "\r\n" +
                                "       AND PS.PAYROLLID = ?PAYROLLID " + "\r\n" +
                                "       ?CONDITION " + "\r\n" +
                                "     GROUP BY STAFFID";
                        break;
                    }
                #endregion

                #region Report

                case SQLCommand.Payroll.FetchGroupByForReport:
                    {
                        query = "Select group_by from report_main where rpt_code=?rpt_code";
                        break;
                    }
                case SQLCommand.Payroll.FetchReportPropertiesForReport:
                    {
                        query = "SELECT PB.BILLTYPE, UPPER(PB.FIELDNAME) AS FIELDNAME, NVL(PB.WIDTH,0) AS WIDTH, PB.FIELDTYPE, " +
                                "NVL(PB.WORDWRAP,0) AS WORDWRAP, PB.MERGE, " +
                                "PB.REPEAT, PB.DISPLAYNAME, PB.EXPORDER, NVL(PB.HIDECOLUMN,0) AS HIDECOLUMN, " +
                                "NVL(PB.ALIGNVERTICAL,0) AS ALIGNVERTICAL " +
                                "FROM PRINTBILLING PB WHERE " +
                                "PB.BILLTYPE=?BILLTYPE ORDER BY PB.EXPORDER";
                        break;
                    }
                case SQLCommand.Payroll.FetchReportFieldsForReport:
                    {
                        query = "SELECT RP.HEAD1, RP.HEAD2 FROM REPORT_PROPERTIES RP WHERE RP.RPT_CODE=?RPT_CODE";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentReport:
                    {
                        query = " select " +
                                " distinct prcomponent.componentid, prcomponent.component, prcomponent.Type," +
                                " prcompmonth.comp_order,truncate(prcomponent.CompRound,0) as CompRound, PROCESS_COMPONENT_TYPE, dont_showinbrowse " +
                                " from prcomponent,prcompmonth " +
                                " where 1=1 {AND prcompmonth.payrollid = ?PAYROLLID} {AND prcompmonth.SALARYGROUPID IN (?SALARYGROUPID)}" +
                                " AND prcomponent.componentid=prcompmonth.componentid  " +
                                " order by prcompmonth.comp_order";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponent01:
                    {
                        query = "SELECT DISTINCT PRSTAFF.COMPONENTID AS \"COMP_ID\",PRCOMPONENT.COMPONENT AS \"COMPONENT\" " +
                        " FROM PRSTAFF,PRCOMPONENT WHERE PRCOMPONENT.COMPONENTID = PRSTAFF.COMPONENTID AND PRCOMPONENT.TYPE <> 2 " +
                        " AND PRSTAFF.PAYROLLID = ?PAYROLLID ORDER BY PRCOMPONENT.COMPONENT ASC";
                        break;
                    }
                case SQLCommand.Payroll.FetchDept01:
                    {
                        query = "SELECT HDEPT_ID,HDEPT_DESC FROM HOSPITAL_DEPARTMENTS" +
                           " WHERE HDEPT_ID > 0 ORDER BY HDEPT_DESC";
                        break;
                    }
                case SQLCommand.Payroll.FetchGroup01:
                    {
                        query = "SELECT GROUPID,GROUPNAME FROM PRSALARYGROUP ORDER BY GROUPNAME";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayroll:
                    {
                        query = "SELECT PAYROLLID AS \"PAYROLLID\",PRNAME AS \"PRNAME\" FROM PRCREATE ORDER BY PAYROLLID DESC";
                        break;
                    }
                case SQLCommand.Payroll.FetchReportFieldsWithoutGroup:
                    {
                        query = "select " +
                        "hospital_departments.hdept_desc as \"Department\"" +
                        " from stfpersonal,hospital_departments,?sReportTable " +
                        " where ?sReportTable.groupid = ?GROUPID " +
                        " and ?sReportTable.staffid = stfpersonal.staffid " +
                        " and stfpersonal.deptid = hospital_departments.hdept_id " +
                        " group by hospital_departments.hdept_desc";
                        break;
                    }
                case SQLCommand.Payroll.FetchReportFieldsWithGroup:
                    {
                        query = "select hospital_departments.hdept_desc as \"Department\",?sRptField " +
                        " from stfpersonal,hospital_departments,?sReportTable " +
                        " where ?sReportTable.groupid = ?GROUPID " +
                        " and ?sReportTable.staffid = stfpersonal.staffid" +
                        " and stfpersonal.deptid = hospital_departments.hdept_id " +
                        " group by hospital_departments.hdept_desc ?sGroupFields";
                        break;
                    }
                case SQLCommand.Payroll.FetchAbstractComponent:
                    {
                        query = "select stfpersonal.empno as \"Id\",?sStaffName " +
                          " as \"Name\",hospital_departments.hdept_desc as \"Department\",to_char(prstaff.compvalue" +
                          " ,'9,99,99,990.00')as \"Amount\" " +
                          " from prstaff,stfpersonal,hospital_departments" +
                          " where prstaff.payrollid = ?PAYROLLID " +
                          " and prstaff.componentid = ?COMPONENTID " +
                          " and prstaff.staffid = stfpersonal.staffid and " +
                          " stfpersonal.deptid = hospital_departments.hdept_id ";
                        break;
                    }
                case SQLCommand.Payroll.DropTable:
                    {
                        query = "DROP TABLE IF EXISTS ?TABLENAME ";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentByCompOrder:
                    {
                        query = " select " +
                                "prcomponent.component,prcomponent.Type,truncate(prcomponent.CompRound,0) as CompRound " +
                                "from prcomponent,prcompmonth " +
                                "where " +
                                "prcompmonth.payrollid = ?PAYROLLID AND " +
                                "prcompmonth.componentid=prcomponent.componentid AND " +
                                "prcompmonth.salarygroupid in(?IDs) ?CONDITION " +
                                "order by prcompmonth.comp_order";  //   "-- and prcompmonth.comp_order > 0 " +
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentByCompOrderWithoutGroup:
                    {
                        query = " select " +
                                "prcomponent.component,prcomponent.Type,truncate(prcomponent.CompRound,0) as CompRound " +
                                "from prcomponent,prcompmonth " +
                                "where " +
                                "prcompmonth.payrollid = ?PAYROLLID AND " +
                                "prcompmonth.componentid=prcomponent.componentid  " + //AND prcompmonth.salarygroupid= ?SALARYGROUPID
                                " ?CONDITION " +
                                " and prcompmonth.comp_order > 0 GROUP BY prcomponent.component " +
                                "order by prcompmonth.comp_order";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentByCompOrderForWages:
                    {
                        query = " select " +
                                "prcomponent.component,prcomponent.Type,truncate(prcomponent.CompRound,0) as CompRound " +
                                "from prcomponent,prcompmonth " +
                                "where " +
                                "prcompmonth.payrollid = ?PAYROLLID AND " +
                                "prcompmonth.componentid=prcomponent.componentid AND " +
                                "prcompmonth.salarygroupid IN (?IDs) ?CONDITION " +
                                "   and prcompmonth.type <> 2 " +   // and prcompmonth.comp_order > 0
                                "order by prcompmonth.comp_order";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentByCompOrderForWagesWithoutGroup:
                    {
                        query = " select " +
                                "prcomponent.component,prcomponent.Type,truncate(prcomponent.CompRound,0) as CompRound " +
                                "from prcomponent,prcompmonth " +
                                "where " +
                                "prcompmonth.payrollid = ?PAYROLLID AND " +
                                "prcompmonth.componentid=prcomponent.componentid " + //AND prcompmonth.salarygroupid= ?SALARYGROUPID
                                " ?CONDITION " +
                                "   and prcompmonth.type <> 2 " +   // and prcompmonth.comp_order > 0
                                "order by prcompmonth.comp_order";
                        break;
                    }
                case SQLCommand.Payroll.DeleteTable:
                    {
                        query = "DELETE FROM ?TABLENAME";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffByGroupByDate:
                    {
                        query = "SELECT DISTINCT PRStaffGroup.StaffId,PRStaffGroup.GroupId," +
                                "PRStaff.ComponentId,PRStaff.CompValue," +
                                "PRComponent.Component FROM PRStaffGroup,PRStaff,PRComponent" +
                                " WHERE PRComponent.ComponentId = PRStaff.ComponentId" +
                                " AND PRStaff.PayRollId =?PAYROLLID AND {PRStaffGroup.GROUPID=?GROUPID AND}" +
                                " PRStaffGroup.Payrollid = PRStaff.Payrollid" +
                                " AND PRStaff.StaffId = PRStaffGroup.StaffId" +
                                " AND PRSTAFF.TRANSACTIONDATE BETWEEN ?TRANSACTIONDATE1 AND ?TRANSACTIONDATE2 " +
                                " ORDER BY PRStaffGroup.StaffId ";

                        break;
                    }
                case SQLCommand.Payroll.FetchStaffByGroup:
                    {
                        query = "SELECT DISTINCT PRStaff.Payrollid, PR.PRNAME, PRStaffGroup.StaffId, PRStaffGroup.GroupId," +
                                 "pg.groupname as groupname, IFNULL(PRSTAFFGROUP.STAFFORDER, 0) AS STAFFORDER," +
                                 "CONCAT(sp.firstname,CONCAT(' ', IFNULL(sp.MIDDLE_NAME,'')),CONCAT(' ',sp.lastname)) as Name," +
                                 "PRStaff.ComponentId, PRStaff.CompValue, PRComponent.Component, PRStaff.TRANSACTIONDATE," +
                                 "IFNULL(sp.ESI_IP_NO, '') AS ESI_IP_NO, IFNULL(ss.UAN, '') AS UAN," +
                                 "(SELECT STAFFORDER FROM PRStaffGroup AS PG WHERE PAYROLLID=?RECENT_PAYROLL_ID AND PG.STAFFID = PRStaffGroup.STAFFID AND" +
                                 "     PG.GROUPID = PRStaffGroup.GROUPID GROUP BY PG.STAFFID) AS RECENT_STAFF_ORDER" +
                                 " FROM PRStaffGroup, PRStaff, PRComponent, PRCREATE AS PR, stfPersonal sp, stfservice ss, prsalarygroup pg" +
                                 " WHERE PRComponent.ComponentId = PRStaff.ComponentId AND sp.staffid = PRStaff.StaffId AND ss.StaffId = sp.StaffId" +
                                 " AND pg.groupid = PRStaffGroup.groupid" +
                                 " {AND PRStaff.STAFFID IN (?STAFFID)} " +
                                 " {AND PRStaff.PayRollId =?PAYROLLID} {AND PRStaffGroup.GROUPID IN (?GROUPID)} {AND PRStaffGroup.DEPARTMENT_ID IN (?DEPARTMENT_ID)}" +
                                 " AND PRStaffGroup.Payrollid = PRStaff.Payrollid" +
                                 " AND PRStaff.StaffId = PRStaffGroup.StaffId AND PR.PAYROLLID = PRSTAFF.PAYROLLID" +
                                 " {AND PR.PRDATE BETWEEN ?FROMDATE AND ?TODATE}" +
                              // " AND PRSTAFF.TRANSACTIONDATE BETWEEN ?TRANSACTIONDATE1 AND ?TRANSACTIONDATE2 " +
                                 " ORDER BY PR.PAYROLLID, PRStaffGroup.StaffId ";

                        break;
                    }
                case SQLCommand.Payroll.FetchFromTable:
                    {
                        query = "SELECT * FROM ?TABLENAME";
                        break;
                    }
                case SQLCommand.Payroll.FetchRPTLLED01:
                    {
                        query = "select " +
                           "stfpersonal.empno as \"Id\",?sStaffName  as \"Name\"," +
                           "hospital_departments.hdept_desc as \"Department\",prloan.loanname as \"Loan\", " +
                           "nvl(to_char(prloanpaid.amount,'9,99,99,990.00'),'0.00') as \"Amount\"  ?sloanDate " +
                           "from prloan,prloanpaid,stfpersonal,prloanget,hospital_departments where " +
                           "prloan.loanid = prloanpaid.loanid and " +
                           "stfpersonal.staffid = prloanpaid.staffid and prloanget.prloangetid = " +
                           "prloanpaid.prloangetid and prloanpaid.amount > 0 and " +
                           "stfpersonal.deptid = hospital_departments.hdept_id ?sWhere";
                        break;
                    }
                case SQLCommand.Payroll.FetchRPTLLED02:
                    {
                        query = "";
                        break;
                    }
                case SQLCommand.Payroll.FetchDuplicateComponent:
                    {
                        query = "SELECT COMPONENT, EQUATION FROM PRCOMPONENT ORDER BY COMPONENT";
                        break;
                    }
                case SQLCommand.Payroll.FetchDuplicateCaption:
                    {
                        query = "SELECT CAPTION FROM PRCOMPONENT ORDER BY CAPTION";
                        break;
                    }
                case SQLCommand.Payroll.CheckEditComponent:
                    {
                        query = "SELECT COMPONENT FROM PRCOMPONENT WHERE COMPONENTID NOT IN (?COMPONENTID) ORDER BY COMPONENT";
                        break;
                    }
                case SQLCommand.Payroll.InsertPrComponent:
                    {
                        query = "INSERT INTO PRCOMPONENT(COMPONENT,DESCRIPTION,TYPE,LEDGER_ID,DEFVALUE," +
                                  "LINKVALUE,EQUATION,EQUATIONID,MAXSLAP,COMPROUND,IFCONDITION,DONT_SHOWINBROWSE,RELATEDCOMPONENTS,PROCESS_TYPE_ID," +
                                  "ISEDITABLE,PAYABLE,PROCESS_COMPONENT_TYPE,DONT_IMPORT_MODIFIED_VALUE_PREV_PR)" + //,RELATEDCOMPONENTS
                                  "VALUES(?COMPONENT,?DESCRIPTION,?TYPE,?LEDGER_ID,?DEFVALUE," +
                                  "?LINKVALUE,?EQUATION,?EQUATIONID,?MAXSLAP,?COMPROUND,?IFCONDITION,?DONT_SHOWINBROWSE,?RELATEDCOMPONENTS,?PROCESS_TYPE_ID," +
                                  "?ISEDITABLE,?PAYABLE,?PROCESS_COMPONENT_TYPE,?DONT_IMPORT_MODIFIED_VALUE_PREV_PR)"; //,'<RelatedComponents>'
                        break;
                    }
                case SQLCommand.Payroll.UpdatePrComponent:
                    {
                        query = "UPDATE PRCOMPONENT P SET P.COMPONENT=?COMPONENT," +
                                "P.DESCRIPTION=?DESCRIPTION,P.TYPE=?TYPE,P.LEDGER_ID=?LEDGER_ID,P.DEFVALUE=?DEFVALUE," +
                                "P.LINKVALUE=?LINKVALUE,P.EQUATION=?EQUATION,P.EQUATIONID=?EQUATIONID," +
                                "P.MAXSLAP=?MAXSLAP,P.COMPROUND=?COMPROUND,P.IFCONDITION=?IFCONDITION,P.PROCESS_TYPE_ID=?PROCESS_TYPE_ID, P.DONT_SHOWINBROWSE=?DONT_SHOWINBROWSE," +
                                "P.RELATEDCOMPONENTS=?RELATEDCOMPONENTS, ISEDITABLE= ?ISEDITABLE, PAYABLE=?PAYABLE, " + 
                                "PROCESS_COMPONENT_TYPE=?PROCESS_COMPONENT_TYPE," +
                                "DONT_IMPORT_MODIFIED_VALUE_PREV_PR=?DONT_IMPORT_MODIFIED_VALUE_PREV_PR" +
                                " WHERE P.COMPONENTID=?COMPONENTID ";
                        break;
                    }
                case SQLCommand.Payroll.UpdatePrCompMonth:
                    {
                        query = " UPDATE PRCOMPMONTH SET TYPE=?TYPE, " +
                              " DEFVALUE=?DEFVALUE, EQUATION=?EQUATION, " +
                              " EQUATIONID=?EQUATIONID, MAXSLAB=?MAXSLAP, " +
                              " LNKVALUE=?LINKVALUE, " +
                              " COMPROUND=?COMPROUND, " +
                              " IFCONDITION=?IFCONDITION" +
                              " WHERE PAYROLLID=?PAYROLLID AND COMPONENTID =?COMPONENTID";
                        break;
                    }
                case SQLCommand.Payroll.FetchLedger:
                    {
                        query = "SELECT LEDGER_ID, LEDGER_NAME FROM MASTER_LEDGER ORDER BY LEDGER_NAME";
                        break;
                    }
                case SQLCommand.Payroll.FetchRangeListbyCompId:
                    {
                        query = "SELECT PC.COMPONENTID,\n" +
                                "       PRF.LINK_COMPONENT_ID,\n" +
                                "       MIN_VALUE,\n" +
                                "       MAX_VALUE,\n" +
                                "       MAX_SLAB\n" +
                                "  FROM PRCOMPONENT PC\n" +
                                " INNER JOIN PAYROLL_RANGE_FORMULA PRF\n" +
                                "    ON PC.COMPONENTID = PRF.COMPONENTID\n" +
                                " WHERE PRF.COMPONENTID = ?COMPONENTID";
                        break;
                    }

                case SQLCommand.Payroll.FetchRangeComponentById:
                    {
                        query = "SELECT COUNT(*) FROM PAYROLL_RANGE_FORMULA WHERE COMPONENTID IN (?COMPONENTID);";
                        break;
                    }

                case SQLCommand.Payroll.PaymentAdviceBank:
                    {
                        query = "SELECT SF.STAFFID, SF.ACCOUNT_NUMBER, CAST(PS.COMPVALUE AS DECIMAL(12,2)) AS \"NET PAY\"," +
                                "CONCAT(IFNULL(CONCAT(PNT.NAME_TITLE, ' '), ''), firstname,CONCAT(' ', IFNULL(MIDDLE_NAME,'')),CONCAT(' ',lastname)) as \"Name\"," +
                                "CONCAT(IFNULL(SF.ACCOUNT_IFSC_CODE,'')," +
                                "IF(SF.ACCOUNT_IFSC_CODE='' OR SF.ACCOUNT_IFSC_CODE IS NULL OR SF.ACCOUNT_BANK_BRANCH IS NULL OR SF.ACCOUNT_BANK_BRANCH = '', '', ' - ')," +
                                "IFNULL(SF.ACCOUNT_BANK_BRANCH,'')) AS ACCOUNT_BANK_BRANCH, IFNULL(ST.PAYMENT_MODE_ID,0) AS PAYMENT_MODE_ID " +
                                "FROM STFPERSONAL SF\n" +
                                "INNER JOIN PRSTAFF PS ON PS.STAFFID = SF.STAFFID\n" +
                                "INNER JOIN PRCOMPONENT PR ON PR.COMPONENTID = PS.COMPONENTID\n" +
                                "INNER JOIN PRStaffGroup ST ON ST.STAFFID = SF.STAFFID AND PS.PAYROLLID = ST.PAYROLLID\n" +
                                "INNER JOIN prsalarygroup p ON P.GROUPID = ST.GROUPID\n" +
                                "LEFT JOIN PR_NAME_TITLE PNT ON PNT.NAME_TITLE_ID = SF.NAME_TITLE_ID\n"+
                                "WHERE PS.PAYROLLID = ?PAYROLLID AND ST.GROUPID IN (?GROUPID) AND PR.PROCESS_COMPONENT_TYPE = " + (int)PayRollProcessComponent.NetPay + "\n"+
                                " ORDER BY P.GROUPNAME, ST.STAFFORDER";
                        break;
                    }
                case SQLCommand.Payroll.StaffEPF:
                    {
                        query = "SELECT ST.PAYROLLID, SF.STAFFID, SS.UAN,\n" +
                                "CONCAT(firstname,CONCAT(' ', IFNULL(MIDDLE_NAME,'')),CONCAT(' ',lastname)) as \"Name\", SF.LAST_DATE_OF_CONTRACT,\n" +
                                "CAST(PRS.EARNED_BASIC AS DECIMAL(12,2)) AS EARNED_BASIC, CAST(PRS.ACTUAL_BASIC AS DECIMAL(12,2)) AS ACTUAL_BASIC,\n" +
                                "CAST(PRS.EPF AS DECIMAL(12,2)) AS EPF, CAST(PF_SA.PF_SALARY AS DECIMAL(12,2)) AS PF_SALARY, \n" +
                                "CAST(PRS.EARNED_GROSS AS DECIMAL(12,2)) AS EARNED_GROSS, CAST(PRS.ACTUAL_GROSS AS DECIMAL(12,2)) AS ACTUAL_GROSS,\n" +
                                "(CAST(PRS.ACTUAL_DAYS  AS DECIMAL(12,2)) - CAST(PRS.SALARY_DAYS  AS DECIMAL(12,2)) ) AS ABSENT_DAYS, IFNULL(ST.STAFFORDER, 0) AS STAFFORDER\n" +
                                "FROM STFPERSONAL SF\n" +
                                "INNER JOIN STFSERVICE SS ON SS.STAFFID = SF.STAFFID\n" +
                                "INNER JOIN PRSTAFFGROUP ST ON ST.STAFFID = SF.STAFFID AND ST.PAYROLLID IN (?PAYROLLID)\n" +
                                "INNER JOIN (SELECT PAYROLLID, STAFFID,\n" + //1. For Earned Values, 2. For Actual Values
                                    "SUM(CASE WHEN PR.LINKVALUE ='" + PayRollExtraPayInfo.BASICPAY.ToString() + "' THEN COMPVALUE ELSE 0 END) EARNED_BASIC,\n" + //Basic Pay
                                    "SUM(CASE WHEN PR.LINKVALUE ='" + PayRollExtraPayInfo.BASICPAY.ToString() + "' THEN ACTUAL_COMPVALUE ELSE 0 END) ACTUAL_BASIC,\n" + //Basic Pay
                                    "SUM(CASE WHEN (PR.PROCESS_COMPONENT_TYPE = " + (int)PayRollProcessComponent.EPF + " AND PR.TYPE = 1) THEN COMPVALUE ELSE 0 END) EPF,\n" +
                                    "SUM(CASE WHEN PR.PROCESS_COMPONENT_TYPE = " + (int)PayRollProcessComponent.GrossWages + " THEN COMPVALUE ELSE 0 END) EARNED_GROSS,\n" +
                                    "SUM(CASE WHEN PR.PROCESS_COMPONENT_TYPE = " + (int)PayRollProcessComponent.GrossWages + " THEN ACTUAL_COMPVALUE ELSE 0 END) ACTUAL_GROSS,\n" +
                                    "SUM(CASE WHEN PR.LINKVALUE ='" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "") + "' THEN COMPVALUE ELSE 0 END) SALARY_DAYS,\n" +
                                    "SUM(CASE WHEN PR.LINKVALUE ='" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "") + "' THEN ACTUAL_COMPVALUE ELSE 0 END) ACTUAL_DAYS\n" +
                                    "FROM PRSTAFF PRS\n" +
                                    "INNER JOIN PRCOMPONENT PR ON PR.COMPONENTID = PRS.COMPONENTID \n" +
                                    " AND (PR.LINKVALUE ='" + PayRollExtraPayInfo.BASICPAY.ToString() + "'\n" +
                                    "      OR PR.PROCESS_COMPONENT_TYPE IN(" + (int)PayRollProcessComponent.GrossWages + ","+ (int)PayRollProcessComponent.EPF + ")\n" +
                                    "      OR PR.LINKVALUE ='" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "") + "')\n" +
                                    "WHERE PRS.PAYROLLID = ?PAYROLLID GROUP BY PRS.STAFFID) PRS\n" +
                                    "ON PRS.PAYROLLID = ST.PAYROLLID AND PRS.STAFFID = SF.STAFFID\n" +
                                "INNER JOIN (SELECT PAYROLLID, STAFFID, SUM(COMPVALUE) AS PF_SALARY\n" +
                                "FROM PRSTAFF WHERE COMPONENTID IN (?COMPONENTID) AND PAYROLLID = ?PAYROLLID  GROUP BY STAFFID) PF_SA\n" +
                                    "   ON PF_SA.PAYROLLID = ST.PAYROLLID AND PF_SA.STAFFID = SF.STAFFID\n" +
                                "WHERE ST.PAYROLLID IN (?PAYROLLID) AND  ST.GROUPID IN (?GROUPID) ORDER BY IFNULL(ST.STAFFORDER, 0)";
                        break;
                    }
                case SQLCommand.Payroll.StaffPTRegister:
                    {
                        query = "SELECT SF.STAFFID, CONCAT(FIRSTNAME,CONCAT(' ', IFNULL(MIDDLE_NAME,'')),CONCAT(' ',LASTNAME)) AS \"NAME\"," +
                                "CAST(PS.COMPVALUE AS DECIMAL(12,2)) AS EARNED_GROSS, PT.PT_AMOUNT\n" +
                                "FROM STFPERSONAL SF\n" +
                                "INNER JOIN PRSTAFF PS ON PS.STAFFID = SF.STAFFID\n" +
                                "INNER JOIN PRCOMPONENT PR ON PR.COMPONENTID = PS.COMPONENTID\n" +
                                "INNER JOIN PRSTAFFGROUP ST ON ST.STAFFID = SF.STAFFID AND PS.PAYROLLID = ST.PAYROLLID\n" +
                                "INNER JOIN PRSALARYGROUP SY ON SY.GROUPID = ST.GROUPID\n" +
                                "INNER JOIN (SELECT PAYROLLID, STAFFID,\n" + //For PT Values
                                "SUM(CASE WHEN PR.PROCESS_COMPONENT_TYPE = " + (int)PayRollProcessComponent.PT + " THEN COMPVALUE ELSE 0 END) PT_AMOUNT\n" +
                                "FROM PRSTAFF PRS\n" +
                                "INNER JOIN PRCOMPONENT PR ON PR.COMPONENTID = PRS.COMPONENTID \n" +
                                " AND (PR.PROCESS_COMPONENT_TYPE= " + (int)PayRollProcessComponent.PT + ")\n" +
                                "WHERE PRS.PAYROLLID = ?PAYROLLID GROUP BY PRS.STAFFID) PT \n" +
                                "ON PT.PAYROLLID = PS.PAYROLLID AND PT.STAFFID = PS.STAFFID\n" +
                                "WHERE PS.PAYROLLID = ?PAYROLLID AND ST.GROUPID IN (?GROUPID) AND \n" +
                                "PR.PROCESS_COMPONENT_TYPE = " + (int)PayRollProcessComponent.GrossWages + " ORDER BY SY.GROUPNAME, IFNULL(ST.STAFFORDER, 0)";
                        break;
                    }
                case SQLCommand.Payroll.PTRateDetails:
                    {
                        query = "SELECT CAST(PS.COMPVALUE AS DECIMAL(12,2)) PT_AMOUNT,\n" +
                                 "CONCAT('PT @ Rs ', PS.COMPVALUE) AS RATE_OF_PT,\n" +
                                 "COUNT(PS.STAFFID) AS NO_STAFFS, SUM(CAST(PS.COMPVALUE AS DECIMAL(12,2))) AS TOTAL_PT_AMOUNT\n" +
                                 "FROM PRSTAFF PS\n" +
                                 "INNER JOIN PRCOMPONENT PR ON PR.COMPONENTID = PS.COMPONENTID\n" +
                                 "INNER JOIN PRSTAFFGROUP SG ON SG.STAFFID = PS.STAFFID AND SG.PAYROLLID = PS.PAYROLLID\n" +
                                 "WHERE PR.PROCESS_COMPONENT_TYPE= " + (int)PayRollProcessComponent.PT + " AND PS.PAYROLLID IN (?PAYROLLID) AND SG.GROUPID IN (?GROUPID)\n" +
                                 "GROUP BY PS.COMPVALUE;";
                        break;
                    }
                #endregion

                #region Payroll Staff
                case SQLCommand.Payroll.AddPrStaff:
                    {
                        query = "INSERT INTO PRStaffGroup(StaffId, GroupId, StaffOrder, ACCOUNT_NUMBER, ACCOUNT_IFSC_CODE, ACCOUNT_BANK_BRANCH, PAYMENT_MODE_ID,\n" +
                                    "DEPARTMENT_ID, WORK_LOCATION_ID, PayrollId)\n" +
                                    "SELECT StaffId, GroupId, StaffOrder, ACCOUNT_NUMBER, ACCOUNT_IFSC_CODE, ACCOUNT_BANK_BRANCH, PAYMENT_MODE_ID,\n" +
                                    "DEPARTMENT_ID, WORK_LOCATION_ID,  ?NEWPAYROLLID FROM PRStaffGroup\n" +
                                    "WHERE Payrollid = ?PAYROLLID;\n" +
                                    "INSERT INTO PRSTAFF_STATUTORY_COMPLIANCE (STAFF_ID, PAYROLL_ID, STATUTORY_COMPLIANCE)\n" +
                                    "SELECT STAFF_ID, ?NEWPAYROLLID, STATUTORY_COMPLIANCE FROM PRSTAFF_STATUTORY_COMPLIANCE WHERE PAYROLL_ID = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.UpdatePrStaffForLastPayroll:
                    {
                        query = @"UPDATE PRSTAFFGROUP SET PAYROLLID=?PAYROLLID WHERE (SELECT COUNT(*) FROM PRCREATE)=1;
                                  UPDATE PRCOMPMONTH SET PAYROLLID=?PAYROLLID WHERE (SELECT COUNT(*) FROM PRCREATE)=1;
                                  UPDATE PRSTAFF_STATUTORY_COMPLIANCE SET PAYROLL_ID=?PAYROLLID WHERE (SELECT COUNT(*) FROM PRCREATE)=1;";

                        break;
                    }
                case SQLCommand.Payroll.ClearInvalidPaydetails:
                    {
                        query = @"DELETE FROM PRSTAFFGROUP WHERE PAYROLLID NOT IN (SELECT PAYROLLID FROM PRCREATE);
                                    DELETE FROM PRCOMPMONTH WHERE PAYROLLID NOT IN (SELECT PAYROLLID FROM PRCREATE);
                                    DELETE FROM PRSTAFF_STATUTORY_COMPLIANCE WHERE PAYROLL_ID NOT IN (SELECT PAYROLLID FROM PRCREATE);
                                    DELETE FROM PRSTATUS WHERE PAYROLLID NOT IN (SELECT PAYROLLID FROM PRCREATE);
                                    DELETE FROM PRSTAFF WHERE PAYROLLID NOT IN (SELECT PAYROLLID FROM PRCREATE);
                                    DELETE FROM PRSTAFFTEMP WHERE PAYROLLID NOT IN (SELECT PAYROLLID FROM PRCREATE);";
                        break;
                    }
                case SQLCommand.Payroll.GetStaffComponentId:
                    {
                        query = "SELECT DISTINCT P.COMPONENTID,PRCOMPONENT.COMPONENT, " +
                        "(SELECT distinct max(COMPORDER) FROM PRSTAFF " +
                        "WHERE PRSTAFF.COMPONENTID = P.COMPONENTID) AS COMPORDER " +
                        "FROM PRSTAFF P, PRCOMPONENT WHERE PRCOMPONENT.COMPONENTID = P.COMPONENTID " +
                        "AND P.PAYROLLID = ?PAYROLLID ORDER BY COMPORDER";
                        break;
                    }
                case SQLCommand.Payroll.FetchFromTableWithWhere:
                    {
                        query = "select * from ?TABLENAME where ?CONDITION";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffList:
                    {
                        //query = "SELECT SF.staffid AS 'STAFF ID'," + "\r\n" +
                        //    "       empno AS 'Staff Code'," + "\r\n" +
                        //    "       MP.PROJECT,\n" +
                        //    "       SY.GROUPNAME AS 'GROUP',\n" + 
                        //    "       CONCAT(firstname, lastname) AS 'Staff Name'," + "\r\n" +
                        //    "       knownas AS 'Known As'," + "\r\n" +
                        //    "       gender AS Gender," + "\r\n" +
                        //    "       ROUND(DATEDIFF(current_date, dateofbirth) / 365) AS Age," + "\r\n" +
                        //    "       dateofbirth AS 'Date Of Birth',Replace(SFS.SCALEOFPAY,'-',' ') as 'SCALEOFPAY'," + "\r\n" +
                        //    "       dateofjoin AS 'Date Of Join'," + "\r\n" +
                        //    "       CASE" + "\r\n" +
                        //    "          WHEN retirementdate  is NULL  THEN NULL" + "\r\n" +
                        //    "          ELSE DATE_FORMAT(retirementdate, '%d/%m/%y')" + "\r\n" +
                        //    "       END" + "\r\n" +
                        //    "          AS 'Retirement Date'," + "\r\n" +
                        //    "       CASE" + "\r\n" +
                        //    "          WHEN leavingdate  is NULL  THEN NULL" + "\r\n" +
                        //    "          ELSE DATE_FORMAT(leavingdate, '%d/%m/%y')" + "\r\n" +
                        //    "       END" + "\r\n" +
                        //    "          AS 'Leaving Date'," + "\r\n" +
                        //    "       degree AS Degree," + "\r\n" +
                        //    "       designation AS Designation," + "\r\n" +
                        //    "       department AS Department," + "\r\n" +
                        //    "       ACCOUNT_NUMBER\n" + 
                        //    "  FROM STFPERSONAL SF \n" +
                        //    "  LEFT JOIN STFSERVICE SFS\n" +
                        //    "    ON SF.STAFFID = SFS.STAFFID\n" + 
                        //    "  LEFT JOIN PRPROJECT_STAFF PF\n" + 
                        //    "    ON SF.STAFFID = PF.STAFFID\n" + 
                        //    "  LEFT JOIN MASTER_PROJECT MP\n" + 
                        //    "    ON PF.PROJECT_ID = MP.PROJECT_ID\n" + 
                        //    "  LEFT JOIN PRSTAFFGROUP PG\n" + 
                        //    "    ON SF.STAFFID = PG.STAFFID\n" + 
                        //    "  LEFT JOIN PRSALARYGROUP SY\n" + 
                        //    "    ON SY.GROUPID = PG.GROUPID\n" +
                        //    " GROUP BY SF.STAFFID;";
                        //break;


                        query = "SELECT SF.staffid AS 'STAFF ID',\n" +
                        "       empno AS 'Staff Code',\n" +
                        "       MP.PROJECT, IFNULL(PG.GROUPID, 0) AS GROUPID,\n" +
                        "       SY.GROUPNAME AS 'GROUP',\n" +
                        "       CONCAT(IFNULL(CONCAT(PNT.NAME_TITLE, ' '), ''), firstname,CONCAT(' ', IFNULL(MIDDLE_NAME,'')),CONCAT(' ',lastname)) AS 'Staff Name',\n" +
                        "       knownas AS 'Known As',\n" +
                        "       gender AS Gender,\n" +
                        "       ROUND(DATEDIFF(current_date, dateofbirth) / 365) AS Age,\n" +
                        "       dateofbirth AS 'Date Of Birth',\n" +
                            //  "       CAST(TRIM(Replace(SFS.SCALEOFPAY, '-', ' ')) AS DECIMAL(13,2)) as 'SCALEOFPAY',\n" +
                        "       CAST(TRIM(REPLACE(Replace(SFS.SCALEOFPAY, '-', ' '),',', '')) AS DECIMAL(13,2)) as 'SCALEOFPAY',\n" +
                        "       dateofjoin AS 'Date Of Join',\n" +
                        "       CASE\n" +
                        "         WHEN retirementdate is NULL THEN\n" +
                        "          NULL\n" +
                        "         ELSE\n" +
                        "          retirementdate\n" +
                        "       END AS 'Retirement Date',\n" +
                        "       CASE\n" +
                        "         WHEN leavingdate is NULL THEN\n" +
                        "          NULL\n" +
                        "         ELSE\n" +
                        "          leavingdate\n" +
                        "       END AS 'Leaving Date',\n" +
                        "       degree AS Degree,\n" +
                        "       designation AS Designation,\n" +
                        "       department AS Department,\n" +
                        "       IFNULL(PG.ACCOUNT_NUMBER, '') AS ACCOUNT_NUMBER, YOS, IFNULL(psc.STATUTORY_COMPLIANCE, '') AS STATUTORY_COMPLIANCE, IFNULL(PG.STAFFORDER,0) AS STAFFORDER\n" +
                        "  FROM STFPERSONAL SF\n" +
                        "  LEFT JOIN STFSERVICE SFS ON SF.STAFFID = SFS.STAFFID\n" +
                        "  LEFT JOIN PRPROJECT_STAFF PF ON SF.STAFFID = PF.STAFFID\n" +
                        "  LEFT JOIN MASTER_PROJECT MP ON PF.PROJECT_ID = MP.PROJECT_ID\n" +
                        "  LEFT JOIN PRSTAFFGROUP PG ON SF.STAFFID = PG.STAFFID AND PG.PAYROLLID = ?PAYROLLID\n" +
                        "  LEFT JOIN PRSALARYGROUP SY ON SY.GROUPID = PG.GROUPID\n" +
                        "  LEFT JOIN PR_NAME_TITLE PNT ON PNT.NAME_TITLE_ID = SF.NAME_TITLE_ID\n" +
                        "  LEFT JOIN (SELECT STAFF_ID, GROUP_CONCAT(CASE\n" +
                        "            WHEN STATUTORY_COMPLIANCE="+ (int)PayRollProcessComponent.EPF +" THEN  '"+ PayRollProcessComponent.EPF.ToString() +"'\n" +
                        "            WHEN STATUTORY_COMPLIANCE=" + (int)PayRollProcessComponent.ESI + " THEN  '" + PayRollProcessComponent.ESI.ToString() + "'\n" +
                        "            WHEN STATUTORY_COMPLIANCE=" + (int)PayRollProcessComponent.PT + " THEN  '" + PayRollProcessComponent.PT.ToString() + "'\n" +
                        "            END) AS STATUTORY_COMPLIANCE FROM prstaff_Statutory_Compliance\n" +
                        "            WHERE PAYROLL_ID = ?PAYROLLID GROUP BY STAFF_ID) psc ON psc.STAFF_ID = SF.STAFFID\n" +
                        //"  WHERE PAYROLLID =?PAYROLLID\n" +
                        "  GROUP BY SF.STAFFID ORDER BY SY.GROUPNAME, IFNULL(PG.STAFFORDER,0);";
                        break;
                    }
                case SQLCommand.Payroll.PaymonthStaffProfile:
                    {
                        query = @"SELECT PR.PAYROLLID, PR.PRNAME, SF.staffid, PR.PRDATE, SF.empno, IFNULL(PG.GROUPID, 0) AS GROUPID, SY.GROUPNAME,
                                IFNULL(PRD.DEPARTMENT, '') AS DEPARTMENT, IFNULL(PRW.WORK_LOCATION, '') AS WORK_LOCATION,
                                CONCAT(firstname,CONCAT(' ', IFNULL(MIDDLE_NAME,'')),CONCAT(' ',lastname)) AS 'Staff Name', knownas AS 'Known As',
                                Gender, ROUND(DATEDIFF(current_date, dateofbirth) / 365) AS Age,
                                dateofbirth AS 'Date Of Birth', CAST(TRIM(REPLACE(Replace(SFS.SCALEOFPAY, '-', ' '),',', '')) AS DECIMAL(13,2)) as 'SCALEOFPAY',
                                dateofjoin AS 'Date Of Join',
                                CASE WHEN retirementdate is NULL THEN NULL ELSE retirementdate END AS 'Retirement Date',
                                CASE WHEN leavingdate is NULL THEN NULL ELSE leavingdate END AS 'Leaving Date',
                                degree AS Degree, designation AS Designation, 
                                 YOS, IFNULL(psc.STATUTORY_COMPLIANCE, '') AS STATUTORY_COMPLIANCE,
                                IFNULL(PG.ACCOUNT_NUMBER, '') AS ACCOUNT_NUMBER, PG.ACCOUNT_IFSC_CODE, PG.ACCOUNT_BANK_BRANCH,  
                                PG.PAYMENT_MODE_ID, PP.PAYMENT_MODE, IFNULL(PG.STAFFORDER,0) AS STAFFORDER
                                FROM STFPERSONAL SF
                                INNER JOIN STFSERVICE SFS ON SF.STAFFID = SFS.STAFFID
                                INNER JOIN PRSTAFFGROUP PG ON SF.STAFFID = PG.STAFFID
                                INNER JOIN PRCREATE PR ON PR.PAYROLLID = PG.PAYROLLID
                                LEFT JOIN PRSALARYGROUP SY ON SY.GROUPID = PG.GROUPID
                                LEFT JOIN PR_PAYMENT_MODE PP ON PP.PAYMENT_MODE_ID = PG.PAYMENT_MODE_ID
                                LEFT JOIN PR_DEPARTMENT PRD ON PRD.DEPARTMENT_ID = SF.DEPARTMENT_ID
                                LEFT JOIN PR_WORK_LOCATION PRW ON PRW.WORK_LOCATION_ID = SF.WORK_LOCATION_ID
                                LEFT JOIN (SELECT PAYROLL_ID, STAFF_ID, GROUP_CONCAT(CASE
                                            WHEN STATUTORY_COMPLIANCE=4 THEN  'EPF'
                                            WHEN STATUTORY_COMPLIANCE=6 THEN  'ESI'
                                            WHEN STATUTORY_COMPLIANCE=5 THEN  'PT'
                                            END) AS STATUTORY_COMPLIANCE FROM prstaff_Statutory_Compliance
                                            GROUP BY PAYROLL_ID, STAFF_ID) psc ON psc.STAFF_ID = SF.STAFFID AND PSC.PAYROLL_ID = PR.PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffInsert:
                    {
                        query = "INSERT INTO STFPERSONAL(EMPNO, FIRSTNAME, MIDDLE_NAME, FATHER_HUSBAND_NAME, MOTHER_NAME, NO_OF_CHILDREN, BLOOD_GROUP,\n" +
                                "LAST_DATE_OF_CONTRACT, LASTNAME,GENDER, DATEOFBIRTH, DATEOFJOIN, RETIREMENTDATE, KNOWNAS, LEAVINGDATE, LEAVEREMARKS, DESIGNATION,\n" +
                                "DEGREE,DEPARTMENT, PAYINCM1, ACCOUNT_NUMBER, YOS,ADDRESS, TELEPHONE_NO, MOBILE_NO, EMERGENCY_CONTACT_NO, EMAIL_ID,\n" +
                                "DEPENDENT1, DEPENDENT2, DEPENDENT3, WORK_EXPERIENCE, PAN_NO, AADHAR_NO, THIRD_PARTY_ID, ACCOUNT_IFSC_CODE, ACCOUNT_BANK_BRANCH, ESI_IP_NO,\n" +
                                "DEPARTMENT_ID, WORK_LOCATION_ID, NAME_TITLE_ID)\n"+
                                "VALUES(?EMPNO, ?FIRSTNAME, ?MIDDLE_NAME, ?FATHER_HUSBAND_NAME, ?MOTHER_NAME, ?NO_OF_CHILDREN, ?BLOOD_GROUP, ?LAST_DATE_OF_CONTRACT,\n" +
                                "?LASTNAME, ?GENDER, ?DATEOFBIRTH, ?DATEOFJOIN, ?RETIREMENTDATE, ?KNOWNAS, ?LEAVINGDATE, ?LEAVEREMARKS, ?DESIGNATION, ?DEGREE,?DEPARTMENT,\n" +
                                "?PAYINCM1, ?ACCOUNT_NUMBER, ?YOS, ?ADDRESS, ?TELEPHONE_NO, ?MOBILE_NO, ?EMERGENCY_CONTACT_NO, ?EMAIL_ID, ?DEPENDENT1, ?DEPENDENT2,\n" +
                                "?DEPENDENT3, ?WORK_EXPERIENCE, ?PAN_NO, ?AADHAR_NO, ?THIRD_PARTY_ID, ?ACCOUNT_IFSC_CODE, ?ACCOUNT_BANK_BRANCH, ?ESI_IP_NO," +
                                "IF(?DEPARTMENT_ID=0, NULL, ?DEPARTMENT_ID), IF(?WORK_LOCATION_ID = 0, NULL, ?WORK_LOCATION_ID), IF(?TITLE_ID=0, NULL, ?TITLE_ID))\n" +
                                "ON DUPLICATE KEY UPDATE EMPNO=?EMPNO,\n" +
                                "   FIRSTNAME=?FIRSTNAME,\n" +
                                "   MIDDLE_NAME=?MIDDLE_NAME,\n" +
                                "   FATHER_HUSBAND_NAME=?FATHER_HUSBAND_NAME,\n" +
                                "   MOTHER_NAME=?MOTHER_NAME,\n" +
                                "   NO_OF_CHILDREN=?NO_OF_CHILDREN,\n" +
                                "   BLOOD_GROUP=?BLOOD_GROUP,\n" +
                                "   LAST_DATE_OF_CONTRACT=?LAST_DATE_OF_CONTRACT,\n" +
                                "   LASTNAME=?LASTNAME,\n" +
                                "   GENDER=?GENDER,\n" +
                                "   DATEOFBIRTH=?DATEOFBIRTH,\n" +
                                "   DATEOFJOIN=?DATEOFJOIN,\n" +
                                "   RETIREMENTDATE=?RETIREMENTDATE,\n" +
                                "   KNOWNAS=?KNOWNAS,\n" +
                                "   LEAVINGDATE=?LEAVINGDATE,\n" +
                                "   LEAVEREMARKS=?LEAVEREMARKS,\n" +
                                "   DESIGNATION=?DESIGNATION,\n" +
                                "   DEGREE=?DEGREE,\n" +
                                "   DEPARTMENT=?DEPARTMENT,\n" +
                                "   PAYINCM1=?PAYINCM1,\n" +
                                "   ACCOUNT_NUMBER=?ACCOUNT_NUMBER,\n" +
                                 "  YOS=?YOS,\n" +
                                 "  ADDRESS=?ADDRESS,\n" +
                                 "  TELEPHONE_NO=?TELEPHONE_NO,\n" +
                                 "  MOBILE_NO=?MOBILE_NO,\n" +
                                 "  EMERGENCY_CONTACT_NO=?EMERGENCY_CONTACT_NO,\n" +
                                 "  EMAIL_ID=?EMAIL_ID,\n" +
                                 "  DEPENDENT1=?DEPENDENT1,\n" +
                                 "  DEPENDENT2=?DEPENDENT2,\n" +
                                 "  DEPENDENT3=?DEPENDENT3,\n" +
                                 "  WORK_EXPERIENCE=?WORK_EXPERIENCE,\n" +
                                 "  PAN_NO=?PAN_NO,\n" +
                                 "  AADHAR_NO=?AADHAR_NO,\n" +
                                 "  ACCOUNT_IFSC_CODE = ?ACCOUNT_IFSC_CODE,\n" +
                                 "  ACCOUNT_BANK_BRANCH = ?ACCOUNT_BANK_BRANCH,\n" +
                                 "  THIRD_PARTY_ID=?THIRD_PARTY_ID,\n" +
                                 "  ESI_IP_NO=?ESI_IP_NO, DEPARTMENT_ID= IF(?DEPARTMENT_ID=0, NULL, ?DEPARTMENT_ID),\n" +
                                 "  WORK_LOCATION_ID = IF(?WORK_LOCATION_ID=0, NULL, ?WORK_LOCATION_ID),\n" +
                                 "  NAME_TITLE_ID = IF(?TITLE_ID=0, NULL, ?TITLE_ID)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffEdit:
                    {
                        query = "UPDATE STFPERSONAL SET EMPNO = ?EMPNO,FIRSTNAME= ?FIRSTNAME,MIDDLE_NAME= ?MIDDLE_NAME,FATHER_HUSBAND_NAME=?FATHER_HUSBAND_NAME,\n" +
                                "MOTHER_NAME=?MOTHER_NAME,NO_OF_CHILDREN=?NO_OF_CHILDREN,BLOOD_GROUP=?BLOOD_GROUP,LAST_DATE_OF_CONTRACT=?LAST_DATE_OF_CONTRACT,\n" +
                                "LASTNAME = ?LASTNAME,GENDER = ?GENDER,DATEOFBIRTH= ?DATEOFBIRTH,DATEOFJOIN= ?DATEOFJOIN,\n" +
                                "KNOWNAS= ?KNOWNAS,LEAVEREMARKS= ?LEAVEREMARKS,DEGREE = ?DEGREE,DEPARTMENT = ?DEPARTMENT ,DESIGNATION = ?DESIGNATION ,\n" +
                                "RETIREMENTDATE= ?RETIREMENTDATE,LEAVINGDATE= ?LEAVINGDATE,PAYINCM1=?PAYINCM1,ACCOUNT_NUMBER=?ACCOUNT_NUMBER,YOS=?YOS,\n" +
                                "ADDRESS=?ADDRESS,TELEPHONE_NO=?TELEPHONE_NO,MOBILE_NO=?MOBILE_NO,EMERGENCY_CONTACT_NO=?EMERGENCY_CONTACT_NO,EMAIL_ID=?EMAIL_ID,\n" +
                                "DEPENDENT1=?DEPENDENT1,DEPENDENT2=?DEPENDENT2,DEPENDENT3=?DEPENDENT3,WORK_EXPERIENCE=?WORK_EXPERIENCE,PAN_NO=?PAN_NO,AADHAR_NO =?AADHAR_NO,\n" +
                                "ACCOUNT_IFSC_CODE=?ACCOUNT_IFSC_CODE, ACCOUNT_BANK_BRANCH=?ACCOUNT_BANK_BRANCH,ESI_IP_NO=?ESI_IP_NO,\n" +
                                "DEPARTMENT_ID= IF(?DEPARTMENT_ID=0, NULL,?DEPARTMENT_ID), WORK_LOCATION_ID= IF(?WORK_LOCATION_ID=0, NULL, ?WORK_LOCATION_ID),\n" +
                                "NAME_TITLE_ID = IF(?TITLE_ID=0, NULL, ?TITLE_ID)\n" +
                                "WHERE STAFFID = ?STAFFID ";
                        break;
                    }
                case
                SQLCommand.Payroll.PayrollStaffserviceInsert:
                    {
                        query = " INSERT INTO STFSERVICE (STAFFID,DATEOFAPPOINTMENT,SCALEOFPAY,PAY,MAXWAGESHRA,UAN,MAXWAGESBASIC, EARNING1, EARNING2, EARNING3, DEDUCTION1, DEDUCTION2, PAYING_SALARY_DAYS) " +
                            "VALUES(?STAFFID,?DATEOFJOIN,?SCALEOFPAY,?PAY,?MAXWAGESHRA,?UAN,?MAXWAGESBASIC,?EARNING1, ?EARNING2, ?EARNING3, ?DEDUCTION1, ?DEDUCTION2, ?PAYING_SALARY_DAYS) ";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffserviceEdit:
                    {
                        query = "UPDATE STFSERVICE SET DATEOFAPPOINTMENT= ?DATEOFJOIN,SCALEOFPAY = ?SCALEOFPAY,PAY=?PAY,MAXWAGESHRA = ?MAXWAGESHRA,  " +
                            "  UAN = ?UAN ,MAXWAGESBASIC = ?MAXWAGESBASIC,EARNING1 =?EARNING1, EARNING2=?EARNING2, EARNING3=?EARNING3, DEDUCTION1=?DEDUCTION1, DEDUCTION2=?DEDUCTION2, PAYING_SALARY_DAYS =?PAYING_SALARY_DAYS WHERE STAFFID= ?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCommentPerformanceInsert:
                    {
                        query = "INSERT INTO PR_STAFF_PERFORMANCE(STAFFID,ACCOUNT_YEAR_ID,COMMENT_ON_PERFORMANCE) VALUES (?STAFFID,?ACCOUNT_YEAR_ID,?COMMENT_ON_PERFORMANCE)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCommentPerformanceEdit:
                    {
                        query = "UPDATE PR_STAFF_PERFORMANCE SET COMMENT_ON_PERFORMANCE= ?COMMENT_ON_PERFORMANCE " +
                            " WHERE STAFFID= ?STAFFID AND ACCOUNT_YEAR_ID = ?ACCOUNT_YEAR_ID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffIdByStaffRefUniqueId:
                    {
                        query = "SELECT STAFFID FROM STFPERSONAL WHERE THIRD_PARTY_ID= ?THIRD_PARTY_ID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffOccur:
                    {
                        query = "SELECT EMPNO FROM STFPERSONAL WHERE EMPNO= ?EMPNO";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDegreeOccur:
                    {
                        query = "SELECT DEGREE FROM STFDEGREE WHERE DEGREE=?DEGREE";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDesigOccur:
                    {
                        query = "SELECT DESIGNATION FROM STFDESIGNATION WHERE DESIGNATION=?DESIGNATION";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDegreeList:
                    {
                        query = "SELECT DEGREEID,DEGREE FROM STFDEGREE";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDesigList:
                    {
                        query = "SELECT DESIGNATIONID,DESIGNATION FROM STFDESIGNATION";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDetails:
                    {
                        query = "SELECT st.staffid AS 'STAFF ID'," + "\r\n" +
                            "       st.empno AS 'STAFF CODE'," + "\r\n" +
                            "       st.firstname AS 'STAFF NAME'," + "\r\n" +
                            "       st.middle_name AS 'MIDDLE NAME'," + "\r\n" +
                            "       st.father_husband_name AS 'FATHER HUSBAND NAME'," + "\r\n" +
                            "       st.mother_name AS 'MOTHER NAME'," + "\r\n" +
                            "       st.no_of_children AS 'NO OF CHILDREN'," + "\r\n" +
                            "       st.blood_group AS 'BLOOD GROUP'," + "\r\n" +
                            "       st.LAST_DATE_OF_CONTRACT AS 'LAST DATE OF CONTRACT'," + "\r\n" +
                            "       st.lastname AS 'LAST NAME'," + "\r\n" +
                            "       st.gender AS GENDER," + "\r\n" +
                            "       DATE_FORMAT(" + "\r\n" +
                            "         st.dateofbirth, '%d/%m/%Y')" + "\r\n" +
                            "         AS 'DATE OF BIRTH'," + "\r\n" +
                            "       DATE_FORMAT(" + "\r\n" +
                            "         st.dateofjoin, '%d/%m/%Y')" + "\r\n" +
                            "         AS 'DATE OF JOIN'," + "\r\n" +
                            "       st.category AS CATEGORY," + "\r\n" +
                            "       DATE_FORMAT(" + "\r\n" +
                            "         st.retirementdate, '%d/%m/%Y')" + "\r\n" +
                            "         AS 'RETIREMENT DATE'," + "\r\n" +
                            "       st.knownas AS 'KNOWN AS'," + "\r\n" +
                            "       DATE_FORMAT(" + "\r\n" +
                            "         st.leavingdate, '%d/%m/%Y')" + "\r\n" +
                            "         AS 'LEAVING DATE'," + "\r\n" +
                            "       SS.UAN AS 'UAN'," + "\r\n" +
                            "       st.degree AS DEGREE," + "\r\n" +
                            "       st.designation AS DESIGNATION," + "\r\n" +
                            "       ss.scaleofpay AS 'SCALE OF PAY'," + "\r\n" +
                            "       st.PAN_NO AS 'PAN NO'," + "\r\n" +
                            "       st.AADHAR_NO AS 'AADHAR NO'," + "\r\n" +
                            "       st.department AS DEPARTMENT," + "\r\n" +
                            "       st.payincm1 AS PAYINCM1,sS.MAXWAGESBASIC AS 'MAXIMUM WAGES BASIC' , ss.MAXWAGESHRA AS 'MAXIMUM WAGES HRA', ST.LEAVEREMARKS," + "\r\n" +
                            "       ss.EARNING1, ss.EARNING2, ss.EARNING3, ss.DEDUCTION1, ss.DEDUCTION2, ss.PAYING_SALARY_DAYS AS 'PAYING SALARY DAYS'," + "\r\n" +
                            "       st.YOS AS YOS, IFNULL(PG.ACCOUNT_NUMBER, '') AS ACCOUNT_NUMBER , IFNULL(PG.ACCOUNT_IFSC_CODE, '') AS ACCOUNT_IFSC_CODE, " + "\r\n" +
                            "       IFNULL(PG.ACCOUNT_BANK_BRANCH, '') AS ACCOUNT_BANK_BRANCH, IFNULL(PG.PAYMENT_MODE_ID, 0) AS PAYMENT_MODE_ID," + "\r\n" +
                            "       IFNULL(PG.DEPARTMENT_ID, 0) AS DEPARTMENT_ID, IFNULL(PG.WORK_LOCATION_ID, 0) AS WORK_LOCATION_ID, IFNULL(st.NAME_TITLE_ID,0) AS NAME_TITLE_ID," + "\r\n" + 
                            "       sp.COMMENT_ON_PERFORMANCE AS 'COMMENT ON PERFORMANCE'," + "\r\n" +
                            "       st.ADDRESS AS ADDRESS," + "\r\n" +
                            "       st.TELEPHONE_NO AS TELEPHONE_NO," + "\r\n" +
                            "       st.MOBILE_NO AS MOBILE_NO," + "\r\n" +
                            "       st.EMERGENCY_CONTACT_NO AS EMERGENCY_CONTACT_NO," + "\r\n" +
                            "       st.EMAIL_ID AS EMAIL_ID," + "\r\n" +
                            "       st.DEPENDENT1 AS DEPENDENT1," + "\r\n" +
                            "       st.DEPENDENT2 AS DEPENDENT2," + "\r\n" +
                            "       st.DEPENDENT3 AS DEPENDENT3," + "\r\n" +
                            "       st.WORK_EXPERIENCE AS WORK_EXPERIENCE," + "\r\n" +
                            "       IFNULL(psc.STATUTORY_COMPLIANCE, '') AS STATUTORY_COMPLIANCE, IFNULL(st.ESI_IP_NO,'') AS ESI_IP_NO"+ "\r\n" +
                            "       FROM stfpersonal st inner join stfservice ss on st.staffid = ss.staffid" + "\r\n" +
                            "  LEFT JOIN (SELECT STAFF_ID, GROUP_CONCAT(STATUTORY_COMPLIANCE) AS STATUTORY_COMPLIANCE FROM PRSTAFF_STATUTORY_COMPLIANCE\n" +
                            "            WHERE STAFF_ID=?STAFFID AND PAYROLL_ID = ?PAYROLL_ID ) psc ON psc.STAFF_ID = St.STAFFID\n" +
                            "  LEFT JOIN (SELECT STAFFID, IFNULL(ACCOUNT_NUMBER, '') AS ACCOUNT_NUMBER, IFNULL(ACCOUNT_IFSC_CODE, '') AS ACCOUNT_IFSC_CODE,\n" +
                            "             IFNULL(ACCOUNT_BANK_BRANCH, '') AS ACCOUNT_BANK_BRANCH, IFNULL(PAYMENT_MODE_ID,0) AS PAYMENT_MODE_ID,\n" +
                            "             IFNULL(DEPARTMENT_ID,0) AS DEPARTMENT_ID, IFNULL(WORK_LOCATION_ID,0) AS WORK_LOCATION_ID FROM PRSTAFFGROUP PG\n" +
                            "             WHERE STAFFID=?STAFFID AND PAYROLLID = ?PAYROLL_ID ) PG ON PG.STAFFID = St.STAFFID\n" +
                            "  LEFT JOIN PR_STAFF_PERFORMANCE SP on SP.STAFFID = SS.STAFFID WHERE SS.STAFFID =?STAFFID { AND SP.ACCOUNT_YEAR_ID=?ACCOUNT_YEAR_ID }";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffOutOfService:
                    {
                        //query = "SELECT tb.staffid as \"STAFF ID\",tb.empno AS \"STAFF CODE\",CONCAT(tb.FIRSTNAME ,' ',tb.LASTNAME) AS \"STAFF NAME\",tb.knownas AS \"KNOWN AS\",tb.gender AS GENDER,round( to_number(SYSDATE-tb.dateofbirth)/365) AS AGE," +
                        // "TO_CHAR(tb.dateofbirth,'DD/MM/YYYY') AS \"DATE OF BIRTH\",TO_CHAR(tb.dateofjoin,'DD/MM/YYYY') AS \"DATE OF JOIN\",TO_CHAR(tb.retirementdate,'DD/MM/YYYY') " +
                        // "AS \"RETIREMENT DATE\",TO_CHAR(tb.leavingdate,'DD/MM/YYYY') AS \"LEAVING DATE\",tb.degree AS DEGREE,tb.designation AS DESIGNATION,tb.department AS DEPARTMENT " +
                        // "FROM stfpersonal tb " +
                        // "WHERE " +
                        //    //"tb.leavingdate is null or "+
                        // "tb.leavingdate < TO_DATE(?OUTSERDATE,'DD/MM/YYYY') " +
                        // "and tb.dateofjoin <= TO_DATE(?OUTSERDATE,'DD/MM/YYYY')";
                        query = "SELECT tb.staffid AS 'STAFF ID',\n" +
                                "       tb.empno AS 'Staff Code',\n" +
                                "       MP.PROJECT,\n" +
                                "       SY.GROUPNAME AS 'GROUP',\n" +
                                "       CONCAT(IFNULL(tb.firstname, ' '), ' ', IFNULL(tb.lastname, ' ')) AS 'Staff Name',\n" +
                                "       tb.knownas AS 'Known As',\n" +
                                "       tb.gender AS 'Gender',\n" +
                                "       ROUND(DATEDIFF(current_date, tb.dateofbirth) / 365) AS Age,\n" +
                                "       DATE_FORMAT(tb.dateofbirth, '%d/%m/%y') AS 'Date Of Birth',CAST(TRIM(Replace(SFS.SCALEOFPAY, '-', ' ')) AS DECIMAL(13,2)) as 'SCALEOFPAY',\n" +
                                "       tb.dateofjoin AS 'Date Of Join',\n" +
                                "       DATE_FORMAT(tb.retirementdate, '%d/%m/%y') AS 'Retirement Date',\n" +
                                "       tb.leavingdate AS 'Leaving Date',\n" +
                                "       tb.degree AS Degree,\n" +
                                "       tb.designation AS Designation,\n" +
                                "       tb.department AS Department, IFNULL(PG.ACCOUNT_NUMBER, '') AS ACCOUNT_NUMBER, IFNULL(psc.STATUTORY_COMPLIANCE, '') AS STATUTORY_COMPLIANCE, IFNULL(PG.STAFFORDER,0) AS STAFFORDER\n" +
                                "  FROM stfpersonal tb\n" +
                                "  LEFT JOIN STFSERVICE SFS ON tb.STAFFID = SFS.STAFFID\n" +
                                "  LEFT JOIN PRPROJECT_STAFF PF ON tb.STAFFID = PF.STAFFID\n" +
                                "  LEFT JOIN MASTER_PROJECT MP ON PF.PROJECT_ID = MP.PROJECT_ID\n" +
                                "  LEFT JOIN PRSTAFFGROUP PG ON tb.STAFFID = PG.STAFFID AND PG.PAYROLLID = ?PAYROLLID\n" +
                                "  LEFT JOIN PRSALARYGROUP SY ON SY.GROUPID = PG.GROUPID\n" +
                                "  LEFT JOIN (SELECT STAFF_ID, GROUP_CONCAT(CASE\n" +
                                "            WHEN STATUTORY_COMPLIANCE=" + (int)PayRollProcessComponent.EPF + " THEN  '" + PayRollProcessComponent.EPF.ToString() + "'\n" +
                                "            WHEN STATUTORY_COMPLIANCE=" + (int)PayRollProcessComponent.ESI + " THEN  '" + PayRollProcessComponent.ESI.ToString() + "'\n" +
                                "            WHEN STATUTORY_COMPLIANCE=" + (int)PayRollProcessComponent.PT + " THEN  '" + PayRollProcessComponent.PT.ToString() + "'\n" +
                                "            END) AS STATUTORY_COMPLIANCE FROM prstaff_Statutory_Compliance\n" +
                                "            WHERE PAYROLL_ID = ?PAYROLLID GROUP BY STAFF_ID) psc ON psc.STAFF_ID = tb.STAFFID\n" +
                                " WHERE (DATE_FORMAT(tb.leavingdate,'%Y-%m-%d') < DATE_FORMAT(?OUTSERDATE,'%Y-%m-%d'))\n" +
                                "   and (DATE_FORMAT(tb.dateofjoin,'%Y-%m-%d') <= DATE_FORMAT(?OUTSERDATE,'%Y-%m-%d')) GROUP BY tb.STAFFID ORDER BY SY.GROUPNAME, IFNULL(PG.STAFFORDER,0);";

                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffInservice:
                    {
                        //query = "SELECT t.staffid AS 'STAFF ID',t.empno AS 'STAFF CODE',CONCAT(t.firstname,t.lastname) AS 'STAFF NAME',t.knownas AS 'KNOWN AS',t.gender AS GENDER,round(to_number(SYSDATE - t.dateofbirth) / 365) AS AGE," +
                        //  "DATE_FORMAT(t.dateofbirth,'%H:%i:%s') AS 'DATE OF BIRTH',DATE_FORMAT(t.dateofjoin,'%H:%i:%s') AS 'DATE OF JOIN',DATE_FORMAT(t.retirementdate,'%H:%i:%s') AS 'RETIREMENT DATE',DATE_FORMAT(t.leavingdate,'%H:%i:%s') AS 'LEAVING DATE',t.degree AS DEGREE,t.designation AS DESIGNATION," +
                        //  "t.department AS DEPARTMENT FROM stfpersonal t WHERE t.staffid <> 0 and (t.leavingdate is null or t.leavingdate >= TO_DATE(?inserdate,'%H:%i:%s')) " +
                        //  "and t.dateofjoin <= DATE_FORMAT(?inserdate,'%d/%m/%Y') order by 'STAFF NAME'";

                        //query = "SELECT t.staffid AS 'STAFF ID'," + "\r\n" +
                        //            "       t.empno AS 'Staff Code'," + "\r\n" +
                        //            "       CONCAT(IFNULL(t.firstname,' '),' ',IFNULL(t.lastname,' ')) AS 'Staff Name'," + "\r\n" +
                        //            "       t.knownas AS 'Known As'," + "\r\n" +
                        //            "       t.gender AS 'Gender'," + "\r\n" +
                        //            "       ROUND(DATEDIFF(current_date, t.dateofbirth) / 365) AS Age," + "\r\n" +
                        //            "       DATE_FORMAT(t.dateofbirth, '%d/%m/%y') AS 'Date Of Birth'," + "\r\n" +
                        //            "       DATE_FORMAT(t.dateofjoin, '%d/%m/%y') AS 'Date Of Join'," + "\r\n" +
                        //            "       CASE" + "\r\n" +
                        //            "          WHEN retirementdate is NULL  THEN NULL" + "\r\n" +
                        //            "          ELSE DATE_FORMAT(retirementdate, '%d/%m/%y')" + "\r\n" +
                        //            "       END" + "\r\n" +
                        //            "          AS 'Retirement Date'," + "\r\n" +
                        //            "       CASE" + "\r\n" +
                        //            "          WHEN leavingdate  is NULL  THEN NULL" + "\r\n" +
                        //            "          ELSE DATE_FORMAT(leavingdate, '%d/%m/%y')" + "\r\n" +
                        //            "       END" + "\r\n" +
                        //            "          AS 'Leaving Date'," + "\r\n" +
                        //            "       t.degree AS Degree," + "\r\n" +
                        //            "       t.designation AS Designation," + "\r\n" +
                        //            "       t.department AS Department" + "\r\n" +
                        //            "  FROM stfpersonal t" + "\r\n" +
                        //            " WHERE t.staffid <> 0" + "\r\n" +
                        //            "       AND (t.leavingdate  is NULL " + "\r\n" +
                        //            "            OR DATE_FORMAT(t.leavingdate, '%Y-%m-%d') >=" + "\r\n" +
                        //            "                  DATE_FORMAT(?INSERDATE, '%Y-%m-%d'))" + "\r\n" +
                        //            "       AND DATE_FORMAT(t.dateofjoin, '%Y-%m-%d') <=" + "\r\n" +
                        //            "              DATE_FORMAT(?INSERDATE, '%Y-%m-%d')" + "\r\n" +
                        //            "ORDER BY 'STAFF NAME'";
                        query = "SELECT t.staffid AS 'STAFF ID',\n" +
                                "       t.empno AS 'Staff Code',\n" +
                                "       MP.PROJECT,\n" +
                                "       SY.GROUPNAME AS 'GROUP',\n" +
                                "       CONCAT(IFNULL(t.firstname, ' '), ' ', IFNULL(t.lastname, ' ')) AS 'Staff Name',\n" +
                                "       t.knownas AS 'Known As',\n" +
                                "       t.gender AS 'Gender',\n" +
                                "       ROUND(DATEDIFF(current_date, t.dateofbirth) / 365) AS Age,\n" +
                                "       DATE_FORMAT(t.dateofbirth, '%d/%m/%y') AS 'Date Of Birth',\n" +
                                "       t.dateofjoin AS 'Date Of Join',CAST(TRIM(Replace(SFS.SCALEOFPAY, '-', ' ')) AS DECIMAL(13,2)) as 'SCALEOFPAY',\n" +
                                "       CASE\n" +
                                "         WHEN retirementdate then\n" +
                                "          DATE_FORMAT(retirementdate, '%d/%m/%y')\n" +
                            //"         retirementdate, \n" +
                                "       END AS 'Retirement Date',\n" +
                                "       CASE\n" +
                                "         WHEN leavingdate THEN\n" +
                                "          leavingdate\n" +
                            // "         leavingdate,\n" +
                                "       END AS 'Leaving Date',\n" +
                                "       t.degree AS Degree,\n" +
                                "       t.designation AS Designation,\n" +
                                "       t.department AS Department,\n" +
                                "       IFNULL(PG.ACCOUNT_NUMBER, '') AS ACCOUNT_NUMBER, IFNULL(psc.STATUTORY_COMPLIANCE, '') AS STATUTORY_COMPLIANCE, IFNULL(PG.STAFFORDER,0) AS STAFFORDER\n" +
                                "  FROM stfpersonal t\n" +
                                "  LEFT JOIN STFSERVICE SFS ON t.STAFFID = SFS.STAFFID\n" +
                                "  LEFT JOIN PRPROJECT_STAFF PF ON t.STAFFID = PF.STAFFID\n" +
                                "  LEFT JOIN MASTER_PROJECT MP ON PF.PROJECT_ID = MP.PROJECT_ID\n" +
                                "  LEFT JOIN PRSTAFFGROUP PG ON t.STAFFID = PG.STAFFID AND PG.PAYROLLID = ?PAYROLLID\n" +
                                "  LEFT JOIN PRSALARYGROUP SY ON SY.GROUPID = PG.GROUPID\n" +
                                "  LEFT JOIN (SELECT STAFF_ID, GROUP_CONCAT(CASE\n" +
                                "            WHEN STATUTORY_COMPLIANCE=" + (int)PayRollProcessComponent.EPF + " THEN  '" + PayRollProcessComponent.EPF.ToString() + "'\n" +
                                "            WHEN STATUTORY_COMPLIANCE=" + (int)PayRollProcessComponent.ESI + " THEN  '" + PayRollProcessComponent.ESI.ToString() + "'\n" +
                                "            WHEN STATUTORY_COMPLIANCE=" + (int)PayRollProcessComponent.PT + " THEN  '" + PayRollProcessComponent.PT.ToString() + "'\n" +
                                "            END) AS STATUTORY_COMPLIANCE FROM prstaff_Statutory_Compliance\n" +
                                "            WHERE PAYROLL_ID = ?PAYROLLID GROUP BY STAFF_ID) psc ON psc.STAFF_ID = T.STAFFID\n" +
                                " WHERE t.staffid <> 0\n" +
                                "   AND (DATE_FORMAT(t.leavingdate, '%Y-%m-%d') >=\n" +
                                "       DATE_FORMAT(?INSERDATE, '%Y-%m-%d'))\n" +
                                "   AND DATE_FORMAT(t.dateofjoin, '%Y-%m-%d') <=\n" +
                                "       DATE_FORMAT(?INSERDATE, '%Y-%m-%d') OR t.leavingdate is NULL GROUP BY t.STAFFID\n" +
                                " ORDER BY SY.GROUPNAME, IFNULL(PG.STAFFORDER,0) ;";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDeptList:
                    {
                        //query = "SELECT HDEPT_ID,HDEPT_DESC FROM HOSPITAL_DEPARTMENTS";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffScale:
                    {
                        query = "select t.scaleofpay from stfpersonal t where t.staffid=<staffid>";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDelete:
                    {
                        query = "delete from stfpersonal where staffid= ?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.DeleteStaffProfile:
                    {
                        query = "DELETE FROM prstaff_statutory_compliance where staff_id = ?STAFFID;\n" +
                                "DELETE FROM pr_staff_performance where staffid = ?STAFFID;\n" +
                                "DELETE FROM prstafftemp where staffid = ?STAFFID;\n" +
                                "DELETE FROM prstaff where staffid = ?STAFFID;\n" +
                                "DELETE FROM prloanget where staffid = ?STAFFID;\n" +
                                "DELETE FROM prloanpaid where staffid =  ?STAFFID;\n" +
                                "DELETE FROM prstaffgroup where staffid = ?STAFFID;\n" +
                                "DELETE FROM stfservice where staffid = ?STAFFID;\n" +
                                "DELETE FROM stfpersonal where staffid = ?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDel:
                    {
                        query = "delete from stfservice where staffid=?STAFFID" +
                           " and staffid not in(select prg.staffid from prstaffgroup prg)" +
                           " and staffid not in(select prt.staffid from prstafftemp prt)" +
                           " and staffid not in(select prl.staffid from prloanget prl)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffDelSel:
                    {
                        query = "DELETE FROM STFPERSONAL WHERE STAFFID = ?STAFFID";
                        //" and staffid not in(select prg.staffid from prstaffgroup prg)" +
                        //" and staffid not in(select prt.staffid from prstafftemp prt)" +
                        //" and staffid not in(select prl.staffid from prloanget prl)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffCommendsDelete:
                    {
                        query = "DELETE FROM PR_STAFF_PERFORMANCE WHERE STAFFID = ?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffNamesAndIds:
                    {
                        query = "SELECT '0' as \"Select\",t.staffid,CONCAT(t.FIRSTNAME ,' ',t.LASTNAME) AS \"Staff Name\" FROM stfpersonal t WHERE (t.leavingdate  is NULL  or t.leavingdate >= TO_DATE(?current_date,'DD/MM/YYYY')) " +
                          "and t.dateofjoin <= TO_DATE(?current_date,'DD/MM/YYYY') order by CONCAT(t.FIRSTNAME ,' ',t.LASTNAME)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffSelectedNamesAndIds:
                    {
                        query = "select case when p.staffid in(<formulagroupstaffids>) " +
                         "then '1' else '0' end as \"Select\",p.staffid,p.firstname||' '||p.lastname " +
                      "as \"Staff Name\" from stfpersonal p WHERE (p.leavingdate is 00000000000000 or p.leavingdate >= TO_DATE(?current_date,'DD/MM/YYYY')) " +
                      "and p.dateofjoin <= TO_DATE(?current_date,'DD/MM/YYYY') order by p.firstname||' '||p.lastname";
                        break;
                    }
                case SQLCommand.Payroll.FetchFormulaGroup:
                    {
                        query = "SELECT FORMULAGROUPID,FORMULA_DESC FROM PRFORMULAGROUP";
                        break;
                    }


                case SQLCommand.Payroll.AutoFetchDesignation:
                    query = "SELECT DESIGNATION FROM STFPERSONAL  GROUP BY DESIGNATION";
                    break;

                case SQLCommand.Payroll.FetchStaffidBystaffCode:
                    {
                        query = "SELECT STAFFID FROM STFPERSONAL WHERE EMPNO IN (?EMPNO);";
                        break;
                    }
                case SQLCommand.Payroll.FetchIdByStaffIDAccountId:
                    {
                        query = "SELECT count(*) FROM PR_STAFF_PERFORMANCE WHERE STAFFID =?STAFFID AND ACCOUNT_YEAR_ID =?ACCOUNT_YEAR_ID;";
                        break;
                    }
                case SQLCommand.Payroll.FetchPaymonthStaffProfile:
                    {
                        query = "SELECT SP.StaffId, PG.GROUPID, EmpNo,\n" +
                                "CONCAT(SP.firstname,CONCAT(' ', IFNULL(SP.MIDDLE_NAME,'')),CONCAT(' ',SP.lastname)) as 'Name',\n"+
                                "IFNULL(PG.ACCOUNT_NUMBER, '') AS ACCOUNT_NUMBER ,\n"+
                                "IFNULL(PG.ACCOUNT_IFSC_CODE, '') AS ACCOUNT_IFSC_CODE,\n"+
                                "IFNULL(PG.ACCOUNT_BANK_BRANCH, '') AS ACCOUNT_BANK_BRANCH,\n"+
                                "IFNULL(PP.PAYMENT_MODE_ID, 0) AS PAYMENT_MODE_ID, IFNULL(PP.PAYMENT_MODE, '') AS PAYMENT_MODE,\n" +
                                "IFNULL(PG.DEPARTMENT_ID, 0) AS DEPARTMENT_ID, IFNULL(PG.WORK_LOCATION_ID, 0) AS WORK_LOCATION_ID,\n" +
                                "IFNULL(PSC.STATUTORY_COMPLIANCE, '') AS STATUTORY_COMPLIANCE\n" +
                                "FROM STFPERSONAL SP\n"+
                                "INNER JOIN PRSTAFFGROUP PG ON PG.STAFFID = SP.STAFFID AND PG.PAYROLLID = ?PAYROLLID AND PG.STAFFID = ?STAFFID\n"+
                                "INNER JOIN PRSALARYGROUP SG ON SG.GROUPID = PG.GROUPID\n"+
                                "LEFT JOIN PR_PAYMENT_MODE PP ON PP.PAYMENT_MODE_ID = PG.PAYMENT_MODE_ID\n"+
                                "LEFT JOIN (SELECT STAFF_ID, GROUP_CONCAT(STATUTORY_COMPLIANCE) AS STATUTORY_COMPLIANCE\n" +
                                "           FROM PRSTAFF_STATUTORY_COMPLIANCE\n"+
                                "           WHERE PAYROLL_ID = ?PAYROLLID AND STAFF_ID = ?STAFFID GROUP BY STAFF_ID) PSC ON PSC.STAFF_ID = SP.STAFFID\n" +
                                "WHERE SP.STAFFID = ?STAFFID ;";
                        break;
                    }
                #endregion

                #region payroll Activites
                case SQLCommand.Payroll.PayrollExistOpen:
                    {
                        //On 11/01/20201
                        //query = "SELECT PAYROLLID,PRNAME FROM PRCREATE ORDER by PAYROLLID DESC";
                        query = "SELECT PAYROLLID, PRNAME, PRDATE FROM PRCREATE ORDER by PAYROLLID DESC";
                        break;
                    }
                case SQLCommand.Payroll.PayrollList:
                    {
                        query = "SELECT PAYROLLID AS PAYROLL_ID,PRNAME AS PAYROLL_NAME,prdate AS PAYROLL_FROM_DATE, '0000-01-01 00:00:00' PAYROLL_TO_DATE" +
                                   " FROM PRCREATE ORDER by PAYROLLID ASC";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLockStatus:
                    {
                        query = "SELECT t.LOCKEDSTATUS FROM PRSTATUS t" +
                           " WHERE t.PAYROLLID =?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollSetlockStatus:
                    {
                        query = "UPDATE PRSTATUS t SET t.LOCKEDSTATUS=?LOCKEDSTATUS WHERE t.PAYROLLID=?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollCreatedDelete:
                    {
                        query = "DELETE FROM PRCREATE pc WHERE pc.PAYROLLID=?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStatusDelete:
                    {
                        query = "DELETE FROM PRSTATUS ps WHERE ps.PAYROLLID=?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffIdColl:
                    {
                        query = "SELECT STAFFID_COLLECTION FROM PRFORMULAGROUP fg WHERE fg.FORMULAGROUPID=?FORMULAGROUPID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollFormulaGroupId:
                    {
                        query = "insert into prformulagroup(formulagroupid,formula_desc,staffid_collection)values(?formulagroupid,?formula_desc,?staffid_collection)";
                        break;
                    }
                case SQLCommand.Payroll.PayrollFormulaUpdateGroupId:
                    {
                        query = "UPDATE prformulagroup SET formula_desc =?formula_desc WHERE formulagroupid =?formulagroupid ";
                        break;
                    }
                case SQLCommand.Payroll.PayrollStaffSelectedUpdatedNamesAndIds:
                    {
                        query = "UPDATE prformulagroup SET formula_desc =?formula_desc,staffid_collection =?staffid_collection WHERE formulagroupid =<formulagroupid> ";
                        break;
                    }
                case SQLCommand.Payroll.LockUnlockPayroll:
                    {
                        query = " UPDATE PRStatus SET Lockedstatus = ?LOCKEDSTATUS WHERE  PayRollId = ?PAYROLLID";
                        break;
                    }
                //chinna 
                case SQLCommand.Payroll.PayrollLoanManagementStaff:
                    {
                        query = "SELECT t.staffid AS 'STAFF_ID',concat(t.firstname,' ',t.lastname) AS 'FIRST_NAME' " +
                            " FROM stfpersonal t WHERE t.staffid <> 0 ";
                        //" FROM stfpersonal t WHERE t.staffid <> 0 ";
                        //" and (t.leavingdate is null or DATE_FORMAT(t.leavingdate,'%Y-%m-%d') >= DATE_FORMAT(?LEAVINGDATE,'%Y-%m-%d')) " +
                        //"and DATE_FORMAT(t.dateofjoin,'%Y-%m-%d') <= DATE_FORMAT(?LEAVINGDATE,'%Y-%m-%d') order by 'FIRST NAME'";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanMgtDetails:
                    {
                        query = "SELECT sp.staffid AS 'STAFF_ID',concat(IFNULL(CONCAT(PNT.NAME_TITLE, ' '), ''), sp.firstname,' ',sp.lastname) AS 'FIRST_NAME' " +
                       " FROM stfpersonal sp LEFT JOIN PR_NAME_TITLE PNT ON PNT.NAME_TITLE_ID = sp.NAME_TITLE_ID";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanMntList:
                    {
                        query = "select t.prloangetid as \"PRLOANGETID\",t.staffid as \"STAFFID\",t.loanid,s.Firstname || ' ' || s.Lastname as \"NAME\",t.amount,t.installment,t.interest,t.fromdate,t.todate,t.intrestmode,t.currentinstallment || '/' || t.installment as Installments from prloanget t, Stfpersonal s where t.staffid=s.Staffid";
                        break;
                    }
                case SQLCommand.Payroll.PayrollLoanMntAdd:
                    {
                        query = "insert into prloanget(staffid,loanid,amount,installment,fromdate,todate,interest,intrestmode,intrestamount,currentinstallment,completed)" +
                            " values(?STAFFID,?LOANID,?AMOUNT,?INSTALLMENT,?FROMDATE,?TODATE,?INTEREST,?INTRESTMODE,?INTRESTAMOUNT,?CURRENTINSTALLMENT,?COMPLETED)";
                        //query = "insert into prloanget(prloangetid,staffid,loanid,amount,installment,fromdate,todate,interest,intrestmode,intrestamount,currentinstallment,completed)" +
                        //"values(scq_prloanmnt.nextval,<staffid>,<loanid>,<amount>,<installment>,to_date('<fromdate>','dd/MM/YYYY'),to_date('<todate>','dd/MM/YYYY'),<interest>,<intrestmode>,<intrestamount>,<currentinstallment>,<completed>)";
                        break;
                    }

                case SQLCommand.Payroll.PayrollLoanMntEdit:
                    {
                        query = "update prloanget set staffid=?STAFFID,loanid=?LOANID,amount=?AMOUNT,installment=?INSTALLMENT,fromdate=?FROMDATE,todate=?TODATE,interest=?INTEREST,intrestmode=?INTRESTMODE," +
                        "intrestamount=?INTRESTAMOUNT,currentinstallment=?CURRENTINSTALLMENT,completed=?COMPLETED where prloangetid=?PRLOANGETID ";
                        break;
                    }
                case SQLCommand.Payroll.GetStaffRetirementDate:
                    {
                        query = "SELECT S.RETIREMENTDATE FROM STFPERSONAL S WHERE STAFFID =?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePayrollLoan:
                    {
                        query = "delete from prloanget where prloanget.prloangetid =?PRLOANGETID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePrComponent:
                    {
                        query = "DELETE FROM PRComponent WHERE ComponentId =?COMPONENTID";
                        break;
                    }
                case SQLCommand.Payroll.FetchNoOfAbsents:
                    {
                        query = "Select component from PRComponent Where Componentid=?COMPONENTID and component='NoOfAbsent'";
                        break;
                    }
                case SQLCommand.Payroll.FetchPrStaffGroup:
                    {
                        query = "select t.* from prstaffgroup t where t.groupid =?GROUPID";
                        break;
                    }
                case SQLCommand.Payroll.FetchCurrentInstallment:
                    {
                        query = "select t.currentinstallment from prloanget t where t.prloangetid =?PRLOANGETID and t.loanid=?LOANID";
                        break;
                    }
                case SQLCommand.Payroll.DeleteVoucherTransByPrloanGetId:
                    {

                        query = "DELETE FROM VOUCHER_TRANS\n" +
                                " WHERE VOUCHER_ID IN (SELECT VOUCHER_ID\n" +
                                "                        FROM VOUCHER_MASTER_TRANS\n" +
                                "                       WHERE CLIENT_REFERENCE_ID = ?PRLOANGETID\n" +
                                "                         AND VOUCHER_TYPE = \"PY\"\n" +
                                "                         AND VOUCHER_SUB_TYPE = \"PAY\");";

                        break;
                    }
                case SQLCommand.Payroll.DeleteVoucherMasterTransByPrloanGetId:
                    {

                        query = "DELETE FROM VOUCHER_MASTER_TRANS\n" +
                                " WHERE CLIENT_REFERENCE_ID = ?PRLOANGETID\n" +
                                "   AND VOUCHER_TYPE = \"PY\"\n" +
                                "   AND VOUCHER_SUB_TYPE = \"PAY\";";

                        break;
                    }
                case SQLCommand.Payroll.FetchLoanStaffDetailsByStaffId:
                    {
                        query = "SELECT STF.STAFFID,STF.GENDER AS GENDER,\n" +
                        "       ROUND(DATEDIFF(CURRENT_DATE, STF.DATEOFBIRTH) / 365) AS AGE,\n" +
                        "       (SELECT GROUPNAME\n" +
                        "          FROM PRSALARYGROUP PRSAL\n" +
                        "         WHERE PRSAL.GROUPID = PRSG.GROUPID) AS STAFF_GROUP,\n" +
                        "       STF.DESIGNATION\n" +
                        "  FROM STFPERSONAL STF\n" +
                        "{INNER JOIN PRPROJECT_STAFF PRPS\n" +
                        "    ON STF.STAFFID = PRPS.STAFFID}\n" +
                        " INNER JOIN STFSERVICE SSERV\n" +
                        "    ON STF.STAFFID = SSERV.STAFFID\n" +
                        " INNER JOIN PRSTAFFGROUP PRSG\n" +
                        "    ON PRSG.STAFFID = STF.STAFFID\n" +
                        " WHERE STF.STAFFID = ?STAFFID GROUP BY STF.STAFFID;";

                        break;
                    }
                #endregion

                #region Payroll Grade
                case SQLCommand.Payroll.GetGroupStaffSQL:
                    {
                        query = "SELECT STFPERSONAL.STAFFID AS \"Staff ID\",PRSALARYGROUP.GROUPID AS \"Group ID\"," +
                            "PRSALARYGROUP.GROUPNAME AS \"Group\" ,STFPERSONAL.EMPNO AS \"Staff Code\"," +
                            "CONCAT(STFPERSONAL.FIRSTNAME ,' ',STFPERSONAL.LASTNAME) AS \"Name\",DEPARTMENT AS 'Department'" +
                            "FROM STFPERSONAL,PRSTAFFGROUP,PRSALARYGROUP " + //,HOSPITAL_DEPARTMENTS " +
                            "WHERE STFPERSONAL.STAFFID = PRSTAFFGROUP.STAFFID AND " +
                            "PRSTAFFGROUP.GROUPID = PRSALARYGROUP.GROUPID AND " +
                            "PRSTAFFGROUP.PAYROLLID = ?PAYROLLID " +
                            // " AND STFPERSONAL.DEPTID = HOSPITAL_DEPARTMENTS.HDEPT_ID AND " +
                            " AND PRSTAFFGROUP.GROUPID IN (?GROUPID) ORDER BY PRSTAFFGROUP.STAFFORDER";
                        break;
                    }
                case SQLCommand.Payroll.GetUnDefinedStaffGroupSQL:
                    {
                        query = "SELECT DISTINCT '0' AS \"Select\",STFPERSONAL.EMPNO AS \"Staff Code\"," +
                            "CONCAT(STFPERSONAL.FIRSTNAME ,' ',STFPERSONAL.LASTNAME) AS \"Name\",DEPARTMENT AS 'Department',STFPERSONAL.STAFFID AS 'Staff ID' " +
                            "FROM PRCREATE,STFPERSONAL WHERE " + //,HOSPITAL_DEPARTMENTS
                            //   "STFPERSONAL.DEPTID = HOSPITAL_DEPARTMENTS.HDEPT_ID AND " + 
                            " STFPERSONAL.STAFFID > 0 " +
                            "AND STFPERSONAL.STAFFID NOT IN (SELECT DISTINCT STAFFID FROM PRSTAFFGROUP WHERE PAYROLLID = ?PAYROLLID) " +
                            " AND (STFPERSONAL.LEAVINGDATE   is NULL   OR PRCREATE.PRDATE < STFPERSONAL.LEAVINGDATE) ORDER BY NAME ";
                        break;
                    }
                case SQLCommand.Payroll.GetUnassignedStaff:
                    {

                        query = "SELECT CONCAT(SF.FIRSTNAME, ' ', SF.LASTNAME) AS 'NAME',\n" +
                        "       SF.DEPARTMENT,\n" +
                        "       SF.STAFFID\n" +
                        "  FROM STFPERSONAL SF\n" +
                        "  LEFT JOIN PRPROJECT_STAFF PS\n" +
                        "    ON PS.STAFFID <> SF.STAFFID\n" +
                        " INNER JOIN PRCREATE PRC\n" +
                        "    ON (SF.LEAVINGDATE IS NULL OR PRC.PRDATE < SF.LEAVINGDATE)\n" +
                        " WHERE SF.STAFFID NOT IN (SELECT STAFFID FROM PRPROJECT_STAFF)\n" +
                        "   AND (SF.LEAVINGDATE is NULL OR PRC.PRDATE < SF.LEAVINGDATE)\n" +
                        " GROUP BY SF.STAFFID ORDER BY NAME;";
                        break;

                    }
                case SQLCommand.Payroll.GetMappedStaffs:
                    {
                        query = "SELECT SF.STAFFID, CONCAT(SF.FIRSTNAME, ' ', SF.LASTNAME) AS 'NAME'\n" +
                                "  FROM STFPERSONAL SF\n" +
                                " INNER JOIN PRPROJECT_STAFF PRPS\n" +
                                "    ON PRPS.STAFFID = SF.STAFFID\n" +
                                " WHERE PRPS.PROJECT_ID = ?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Payroll.GetProjectGroupMappedStaffs:
                    {
                        query = "SELECT SF.STAFFID, CONCAT(SF.FIRSTNAME, ' ', SF.LASTNAME) AS 'NAME'\n" +
                                "  FROM STFPERSONAL SF\n" +
                                " INNER JOIN PRPROJECT_STAFF PRPS\n" +
                                "    ON PRPS.STAFFID = SF.STAFFID\n" +
                                 "INNER JOIN PRSTAFFGROUP PSG \n" +
                                  "  ON PSG.GROUPID IN (?GROUP_ID) \n" +
                                   " AND PRPS.STAFFID = PSG.STAFFID \n" +
                                " WHERE PRPS.PROJECT_ID = ?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Payroll.GetUnMappedStaffs:
                    {
                        query = "SELECT SF.STAFFID, CONCAT(SF.FIRSTNAME, ' ', SF.LASTNAME) AS 'NAME'\n" +
                        "  FROM STFPERSONAL SF\n" +
                            //" INNER JOIN PRSTAFFGROUP PRSG\n" +
                            //"    ON SF.STAFFID NOT IN (SELECT STAFFID FROM PRSTAFFGROUP)\n" +
                        " INNER JOIN PRPROJECT_STAFF PRPS\n" +
                        "    ON PRPS.STAFFID = SF.STAFFID\n" +
                        " WHERE PRPS.PROJECT_ID = ?PROJECT_ID AND SF.STAFFID NOT IN (SELECT STAFFID FROM PRSTAFFGROUP) GROUP BY SF.STAFFID;";
                        break;
                    }
                case SQLCommand.Payroll.GetAllUnMappedStaffs:
                    {
                        query = "SELECT SF.STAFFID, CONCAT(SF.FIRSTNAME, ' ', SF.LASTNAME) AS 'NAME'\n" +
                        "  FROM STFPERSONAL SF\n" +
                        "  LEFT JOIN PRPROJECT_STAFF PRPS\n" +
                        "    ON SF.STAFFID = PRPS.STAFFID\n" +
                        " WHERE SF.STAFFID NOT IN (SELECT STAFFID FROM PRPROJECT_STAFF)\n" +
                        " GROUP BY SF.STAFFID;";
                        break;
                    }
                case SQLCommand.Payroll.GetGroupSQL:
                    {
                        query = "SELECT GROUPNAME AS \"GROUP NAME\",GROUPID FROM PRSALARYGROUP ORDER BY GROUPNAME";
                        break;
                    }
                case SQLCommand.Payroll.GetMaxStaffSortOrder:
                    {
                        query = "SELECT MAX(STAFFORDER) AS STAFFORDER FROM PRSTAFFGROUP WHERE GROUPID = ?GROUPID AND PAYROLLID = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.InsertPRStaffGroup:
                    {
                        query = @"INSERT INTO PRSTAFFGROUP(STAFFID, GROUPID, STAFFORDER, ACCOUNT_NUMBER, ACCOUNT_IFSC_CODE, 
                                    ACCOUNT_BANK_BRANCH, PAYMENT_MODE_ID, DEPARTMENT_ID, WORK_LOCATION_ID, PAYROLLID) 
                                    VALUES (?STAFFID, ?GROUPID, ?STAFFORDER, ?ACCOUNT_NUMBER, ?ACCOUNT_IFSC_CODE, 
                                    ?ACCOUNT_BANK_BRANCH, ?PAYMENT_MODE_ID, IF(?DEPARTMENT_ID=0, NULL, ?DEPARTMENT_ID) , IF(?WORK_LOCATION_ID=0, NULL, ?WORK_LOCATION_ID), ?PAYROLLID ) ";
                        break;
                    }
                case SQLCommand.Payroll.InsertPRStaffGroupForAllMonths:
                    {
                        query = "INSERT INTO PRSTAFFGROUP (GROUPID, STAFFORDER, ACCOUNT_NUMBER, ACCOUNT_IFSC_CODE,\n" + 
                                "ACCOUNT_BANK_BRANCH, PAYMENT_MODE_ID, DEPARTMENT_ID, WORK_LOCATION_ID, PAYROLLID, STAFFID)\n" +
                                "SELECT ?GROUPID AS GROUPID, ?STAFFORDER AS STAFFORDER, ?ACCOUNT_NUMBER AS ACCOUNT_NUMBER, ?ACCOUNT_IFSC_CODE AS ACCOUNT_IFSC_CODE, \n" +
                                "?ACCOUNT_BANK_BRANCH AS ACCOUNT_BANK_BRANCH , ?PAYMENT_MODE_ID AS PAYMENT_MODE_ID,\n" +
                                "IF(?DEPARTMENT_ID=0, NULL, ?DEPARTMENT_ID), IF(?WORK_LOCATION_ID=0, NULL, ?WORK_LOCATION_ID), PAYROLLID, ?STAFFID AS STAFFID \n" + 
                                "FROM PRCREATE PR WHERE PAYROLLID <> ?PAYROLLID;";
                        break;
                    }
                case SQLCommand.Payroll.InsertPRStaffStatutoryCompliance:
                    {
                        query = "INSERT INTO PRSTAFF_STATUTORY_COMPLIANCE (STAFF_ID, PAYROLL_ID, STATUTORY_COMPLIANCE)\n"+
                                "VALUES (?STAFF_ID, ?PAYROLL_ID, ?STATUTORY_COMPLIANCE) \n" +
                                "ON DUPLICATE KEY UPDATE STAFF_ID = ?STAFF_ID, PAYROLL_ID = ?PAYROLL_ID, STATUTORY_COMPLIANCE=?STATUTORY_COMPLIANCE";
                        break;
                    }
                case SQLCommand.Payroll.DeletePRStaffStatutoryCompliance:
                    {
                        query = "DELETE FROM PRSTAFF_STATUTORY_COMPLIANCE WHERE STAFF_ID=?STAFF_ID AND PAYROLL_ID=?PAYROLL_ID";
                        break;
                    }
                case SQLCommand.Payroll.FetchPRStaffStatutoryComplianceByPayrollId:
                    {
                        query = "SELECT * FROM PRSTAFF_STATUTORY_COMPLIANCE WHERE PAYROLL_ID=?PAYROLL_ID";
                        break;
                    }
                case SQLCommand.Payroll.DeleteProjectIdStaff:
                    {
                        query = "DELETE FROM PRPROJECT_STAFF WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Payroll.DeleteProjectStaff:
                    {
                        query = "DELETE FROM PRPROJECT_STAFF WHERE STAFFID IN (?STAFFID);";
                        break;
                    }
                case SQLCommand.Payroll.InsertPRProjectStaff:
                    {
                        query = "INSERT INTO PRPROJECT_STAFF(PROJECT_ID,STAFFID)VALUES(?PROJECT_ID,?STAFFID);";
                        break;
                    }
                case SQLCommand.Payroll.UpdatePRStaffGroup:
                    {
                        //query = " UPDATE PRSTAFFGROUP SET GROUPID = ?GROUPID WHERE STAFFID =?STAFFID AND PAYROLLID = PAYROLLID ";

                        query = @"UPDATE PRSTAFFGROUP SET GROUPID = ?GROUPID, ACCOUNT_NUMBER=?ACCOUNT_NUMBER, ACCOUNT_IFSC_CODE=?ACCOUNT_IFSC_CODE,
                                    ACCOUNT_BANK_BRANCH=?ACCOUNT_BANK_BRANCH, PAYMENT_MODE_ID=?PAYMENT_MODE_ID, DEPARTMENT_ID= IF(?DEPARTMENT_ID=0, NULL,?DEPARTMENT_ID), 
                                    WORK_LOCATION_ID= IF(?WORK_LOCATION_ID=0, NULL, ?WORK_LOCATION_ID) 
                                    WHERE STAFFID =?STAFFID AND PAYROLLID = ?PAYROLLID";
                        break;
                    }
                case SQLCommand.Payroll.SaveStaffGroupOrder:
                    {
                        query = "UPDATE PRSTAFFGROUP SET STAFFORDER =?STAFFORDER " +
                            " WHERE GROUPID =?GROUPID AND STAFFID =?STAFFID " +
                            " AND PAYROLLID = ?PAYROLLID";
                        break;
                    }

                case SQLCommand.Payroll.DeletePRStaffByStaffId:
                    {
                        query = "DELETE FROM PRSTAFF WHERE STAFFID =?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePrLoanGetByStaffId:
                    {
                        query = "DELETE FROM PRLOANGET WHERE STAFFID = ?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePrLoanPaidbyStaffId:
                    {
                        query = "DELETE FROM PRLOANPAID WHERE STAFFID = STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePrLoanPaidbyLoanId:
                    {
                        query = "DELETE FROM PRLOANPAID WHERE PRLOANGETID = ?PRLOANGETID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePrStaffGroupByStaffId:
                    {
                        query = "DELETE FROM PRSTAFFGROUP WHERE STAFFID = ?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePayrollByPayrollId:
                    {
                        query = "DELETE FROM PRSTAFF WHERE PAYROLLID = ?PAYROLLID;\n" +
                                "DELETE FROM PRStaffTemp WHERE PAYROLLID = ?PAYROLLID;\n" +
                                "DELETE FROM PRStaffGroup WHERE PAYROLLID =?PAYROLLID AND (SELECT COUNT(*) FROM PRCREATE)>1;\n" + //To retain Mapp Staff Group with payroll
                                "DELETE FROM PRCompMonth WHERE PAYROLLID = ?PAYROLLID AND (SELECT COUNT(*) FROM PRCREATE)>1;\n" + //To retain compontents with payroll
                                "DELETE FROM PRStatus WHERE PAYROLLID = ?PAYROLLID AND (SELECT COUNT(*) FROM PRCREATE)>1;\n" + //To retain status of the payroll
                                "DELETE FROM PRSTAFF_STATUTORY_COMPLIANCE WHERE PAYROLL_ID =?PAYROLLID AND (SELECT COUNT(*) FROM PRCREATE)>1;\n" + //To retain statuory with payroll
                                //"UPDATE PRLoanGet SET CurrentInstallment = CurrentInstallment - 1, Completed = '1'\n" +
                                //" WHERE PRLoanGetId IN (SELECT PRLoanGetId FROM PRLoanPaid  WHERE PAYROLLID = ?PAYROLLID);\n" +
                                "DELETE FROM prloanget WHERE PRLOANGETID IN (select PRLOANGETID FROM PRLoanPaid WHERE PayRollId =?PAYROLLID);\n" +
                                "DELETE FROM PRLoanPaid WHERE PAYROLLID = ?PAYROLLID;\n" +
                                "DELETE FROM PAYROLL_VOUCHER WHERE PAYROLL_ID = ?PAYROLLID;\n" +
                                "DELETE FROM PAYROLL WHERE PAYROLLID = ?PAYROLLID;\n" +
                                "DELETE FROM PRCreate WHERE PAYROLLID = ?PAYROLLID;";
                                
                        break;
                    }
                case SQLCommand.Payroll.FetchProjectIdByStaffId:
                    {
                        query = "SELECT PROJECT_ID FROM PRPROJECT_STAFF WHERE STAFFID = ?STAFFID";
                        break;
                    }
                case SQLCommand.Payroll.FetchGroupIdByStaffId:
                    {
                        query = "SELECT GROUPID FROM PRSTAFFGROUP WHERE STAFFID = ?STAFFID AND PAYROLLID=?PAYROLLID";
                        break;
                    }

                #endregion

                #region Payroll Process

                case SQLCommand.Payroll.FetchPRStaff:
                    {
                        //query = "Select prstaffgroup.staffid,concat(stfpersonal.firstname,concat( ' ',stfpersonal.lastname)) as Name, " +
                        //         "PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService,prproject_staff " +
                        //         "where {prstaffgroup.Groupid =?SALARYGROUPID and }prstaffgroup.payrollid = ?PAYROLLID " +
                        //         " and {prproject_staff.project_id=?PROJECT_ID AND prproject_staff.staffid=prstaffgroup.staffid " +
                        //         " and} stfpersonal.staffid = prstaffgroup.staffid AND stfService.StaffId = stfPersonal.StaffId AND " +
                        //          "DATE_FORMAT(?ENDDATE,'%Y-%m-%d')  >= DATE_FORMAT(stfPersonal.dateofJoin,'%Y-%m-%d') " +
                        //           " and (stfPersonal.LeavingDate  is NULL  or DATE_FORMAT(stfPersonal.LeavingDate,'%Y-%m-%d') > DATE_FORMAT(?STARTDATE,'%Y-%m-%d')" +
                        //          " AND ((DATE_FORMAT(?ENDDATE,'%Y-%m-%d') BETWEEN " +
                        //       "DATE_FORMAT(stfService.DateofAppointment,'%Y-%m-%d') AND DATE_FORMAT(stfService.DateofTermination,'%Y-%m-%d')) " +
                        //       " OR (stfService.DateofTermination  is null AND DATE_FORMAT(?ENDDATE,'%Y-%m-%d') > DATE_FORMAT(stfService.DateofAppointment,'%Y-%m-%d')))) " +
                        //          "group by staffid order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";

                        query = "SELECT PSG.STAFFID,\n" +
                        "       concat(STF.firstname, concat(' ', STF.lastname)) as Name,\n" +
                        "       PSG.Groupid, PSG.StaffOrder\n" +
                        "  FROM PRStaffGroup PSG\n" +
                        "\n" +
                        " INNER JOIN stfpersonal STF\n" +
                        "    ON STF.staffid = PSG.staffid\n" +
                        " INNER JOIN stfService SSF\n" +
                        "    ON SSF.StaffId = STF.StaffId\n" +
                        "  LEFT JOIN PRPROJECT_STAFF PPS\n" +
                        "    ON PPS.STAFFID = STF.STAFFID\n" +
                            //" where PSG.Groupid = ?SALARYGROUPID\n" +
                        " where PSG.payrollid = ?PAYROLLID\n" +
                        "  { and PSG.Groupid = ?SALARYGROUPID}\n" +
                        "   and STF.staffid = PSG.staffid\n" +
                        "   AND SSF.StaffId = STF.StaffId\n" +
                        "   AND DATE_FORMAT(?ENDDATE, '%Y-%m-%d') >=\n" +
                        "       DATE_FORMAT(STF.dateofJoin, '%Y-%m-%d')\n" +
                            //"      AND (STF.LeavingDate is NULL or DATE_FORMAT(STF.LeavingDate, '%Y-%m-%d') > DATE_FORMAT(?STARTDATE, '%Y-%m-%d') AND\n" +
                        " AND (STF.LeavingDate is NULL OR \n" + //On 26/07/2019, to show proper leaving date
                        "        (CASE WHEN MONTH(DATE_FORMAT(STF.LeavingDate, '%Y-%m-%d')) = " + "MONTH(DATE_FORMAT(?ENDDATE, '%Y-%m-%d')) AND " +
                                 "YEAR(DATE_FORMAT(STF.LeavingDate, '%Y-%m-%d')) = " + "YEAR(DATE_FORMAT(?ENDDATE, '%Y-%m-%d')) " +
                        "        THEN DATE_FORMAT(STF.LeavingDate, '%Y-%m-%d') <= " + "DATE_FORMAT(?ENDDATE, '%Y-%m-%d')" +
                        "        ELSE DATE_FORMAT(STF.LeavingDate, '%Y-%m-%d') >= " + "DATE_FORMAT(?ENDDATE, '%Y-%m-%d') END) AND " +
                        "       ((DATE_FORMAT(?ENDDATE, '%Y-%m-%d') BETWEEN\n" +
                        "       DATE_FORMAT(SSF.DateofAppointment, '%Y-%m-%d') AND\n" +
                        "       DATE_FORMAT(SSF.DateofTermination, '%Y-%m-%d')) OR\n" +
                        "       (SSF.DateofTermination is null AND\n" +
                        "       DATE_FORMAT(?ENDDATE, '%Y-%m-%d') >\n" +
                        "       DATE_FORMAT(SSF.DateofAppointment, '%Y-%m-%d'))))\n" +
                        "   {AND PPS.PROJECT_ID IN (?PROJECT_ID)}\n" +
                        " group by staffid\n" +
                        " order by PSG.GroupId, PSG.StaffOrder;";
                        break;
                    }
                //case SQLCommand.Payroll.FetchPProcessStaffGroup:
                //    {
                //        query = "Select distinct PRComponent.component,prcompMonth.Comp_Order,PRComponent.componentid,PRComponent.iseditable  from PRComponent," +
                //   "PRStaff,PRCompMonth,PRSalaryGroup where " +
                //   "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
                //   "PRCompMonth.PayRollId =?PAYROLLID AND " +
                //   "PRCompMonth.Payrollid = PRStaff.Payrollid and " +
                //   "prstaff.componentid = PRCompMonth.Componentid and " +
                //   "PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid {and " +
                //   "prCompMonth.SalaryGroupid = ?SALARYGROUPID} ORDER BY PRCOMPMONTH.COMP_ORDER ";
                //        break;
                //    }

                case SQLCommand.Payroll.FetchPProcessStaffGroup:
                    {

                        query = "Select distinct PRComponent.component,\n" +
                                 "                prcompMonth.Comp_Order,\n" +
                                 "                PRComponent.componentid,\n" +
                                 "                PRComponent.iseditable\n" +
                                 "  from PRComponent, PRStaff, PRCompMonth, PRSalaryGroup\n" +
                                 " where PRCompMonth.ComponentId = PRComponent.ComponentId\n" +
                                 "   AND PRCompMonth.PAYROLLID =?PAYROLLID\n" +
                                 "   AND PRCompMonth.PAYROLLID = PRStaff.PAYROLLID\n" +
                                 "   and prstaff.componentid = PRCompMonth.Componentid\n" +
                                 "   and PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid\n" +
                                 "       { AND prCompMonth.SalaryGroupid = ?SALARYGROUPID }\n" +
                                 " group by prcompmonth.componentid\n" +
                                 " order by Comp_order;";
                        break;


                    }

                case SQLCommand.Payroll.FetchPayrollStaffGroup:
                    {
                        //query = "Select distinct PRComponent.component,PRStaff.CompValue," +

                        //On 12/01/2017, take for given salary group too ------------------------------------------------------------------

                        //query = "Select distinct PRComponent.component,PRStaff.CompValue,Prcomponent.LinkValue," +
                        //"PRCompMonth.Comp_Order,PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth," +
                        //"PRStaffGroup where PRCompMonth.PayRollId =?PAYROLLID AND " +
                        //"PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
                        //"PRCompMonth.Payrollid = PRStaff.Payrollid and " +
                        //"PRStaff.staffid = ?STAFFID and " +
                        //"PRStaff.Staffid = PRStaffGroup.Staffid and " +
                        //"PRStaffGroup.Groupid = PRCompMonth.SalaryGroupid and " + //PRCompMonth.SalaryGroupid ="+ dtGrp.Rows[i][0].ToString() + "and 
                        //"PRStaffGroup.Payrollid = PRStaff.Payrollid and " +
                        //"prstaff.componentid = prcomponent.componentid " +
                        //"ORDER BY PRCOMPMONTH.COMP_ORDER ";

                        query = "Select distinct PRComponent.component,PRStaff.CompValue,Prcomponent.LinkValue," +
                             "PRCompMonth.Comp_Order,PRCompMonth.Type, PRComponent.componentid, PRStaff.staffid from PRComponent,PRStaff,PRCompMonth," +
                             "PRStaffGroup where PRCompMonth.PayRollId =?PAYROLLID AND " +
                             "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
                             "PRCompMonth.Payrollid = PRStaff.Payrollid and " +
                             "{PRStaff.staffid = ?STAFFID and }" +
                             "PRStaff.Staffid = PRStaffGroup.Staffid and " +
                             "PRStaffGroup.Groupid = PRCompMonth.SalaryGroupid and " +
                             "{PRCompMonth.SalaryGroupid =?SALARYGROUPID and }" +
                             "PRStaffGroup.Payrollid = PRStaff.Payrollid and " +
                             "prstaff.componentid = prcomponent.componentid " +
                             "ORDER BY PRStaff.staffid, PRCOMPMONTH.COMP_ORDER ";

                        //---------------------------------------------------------------------------------------------------------------
                        break;
                    }
                case SQLCommand.Payroll.FetchPrCompMonthStaffOrder:
                    {
                        query = "Select distinct PRComponent.component,prcompMonth.Comp_Order " +
                    "from PRComponent,PRCompMonth,PRSalaryGroup where " +
                    "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
                    "PRCompMonth.PayRollId = ?PAYROLLID AND " +
                    "PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid and " +
                    "{prCompMonth.SalaryGroupid = ?SALARYGROUPID} " +
                    " ORDER BY PRCompMonth.Comp_Order";
                        break;
                    }
                #endregion

                #region Payroll Reports
                case SQLCommand.Payroll.DailyPFReport:
                    {
                        query = "SELECT " +
                               "PRT.NAME AS 'STAFF_NAME', " +
                               "ROUND(`PF WAGES`,2) AS 'WAGES', " +
                               "CASE WHEN PRT.PF='' THEN ROUND(0,2) ELSE ROUND(PRT.PF,2) END AS PF, " +
                               "ROUND((PRT.PF*(3.67/12))) AS EPF, " +
                               "ROUND((PRT.PF*(8.33/12))) AS EPS " +
                               "FROM STFPERSONAL ST LEFT JOIN PRREPORTTABLE PRT ON  PRT.STAFFID = ST.STAFFID " +
                               "WHERE ?CONDITION " +
                               "GROUP BY PRT.Name";

                        break;
                    }

                case SQLCommand.Payroll.DailyPFReports:
                    {
                        query = "SELECT " +
                               "PRT.NAME AS 'STAFF_NAME', " +
                               "ROUND(`PF WAGES`,2) AS 'WAGES', " +
                               "CASE WHEN PRT.PF='' THEN ROUND(0,2) ELSE ROUND(PRT.PF,2) END AS PF, " +
                               "ROUND((PRT.PF*(3.67/12))) AS EPF, " +
                               "ROUND((PRT.PF*(8.33/12))) AS EPS " +
                               "FROM STFPERSONAL ST, PRREPORTTABLE PRT " +
                               "WHERE PRT.STAFFID = ST.STAFFID ?CONDITION " +
                               "GROUP BY PRT.Name";

                        break;
                    }
                case SQLCommand.Payroll.DailyPFReportsForAll:
                    {
                        query = "SELECT " +
                               "PRT.NAME AS 'STAFF_NAME', " +
                               "ROUND(`PF WAGES`,2) AS 'WAGES', " +
                               "CASE WHEN PRT.PF='' THEN ROUND(0,2) ELSE ROUND(PRT.PF,2) END AS PF, " +
                               "ROUND((PRT.PF*(3.67/12))) AS EPF, " +
                               "ROUND((PRT.PF*(8.33/12))) AS EPS " +
                               "FROM STFPERSONAL ST, PRREPORTTABLE PRT " +
                               "WHERE PRT.STAFFID = ST.STAFFID GROUP BY PRT.Name";

                        break;
                    }
                case SQLCommand.Payroll.MonthlyPFReports:
                    {
                        query = "SELECT " +
                               "DATE_FORMAT(TRANSACTIONDATE,'%M - %Y') AS MONTH, " +
                               "PRT.NAME AS 'STAFF_NAME', " +
                               "ROUND(`PF WAGES`,2) AS 'WAGES', " +
                               "CASE WHEN PRT.PF='' THEN ROUND(0,2) ELSE ROUND(PRT.PF,2) END AS PF, " +
                               "ROUND((PRT.PF*(3.67/12))) AS EPF, " +
                               "ROUND((PRT.PF*(8.33/12))) AS EPS " +
                               "FROM STFPERSONAL ST, PRREPORTTABLE PRT, PRSTAFF PS WHERE PRT.GROUPID = ?GROUPID " +
                               "AND PRT.STAFFID = ST.STAFFID AND PS.STAFFID = PRT.STAFFID " +
                               "AND TRANSACTIONDATE BETWEEN ?DATEFROM AND ?DATETO GROUP BY PRT.Name";

                        break;
                    }
                case SQLCommand.Payroll.MonthlyPFReportsForAll:
                    {
                        query = "SELECT " +
                                "DATE_FORMAT(TRANSACTIONDATE,'%M - %Y') AS MONTH, " +
                               "PRT.NAME AS 'STAFF_NAME', " +
                               "ROUND(`PF WAGES`,2) AS 'WAGES', " +
                               "CASE WHEN PRT.PF='' THEN ROUND(0,2) ELSE ROUND(PRT.PF,2) END AS PF, " +
                               "ROUND((PRT.PF*(3.67/12))) AS EPF, " +
                               "ROUND((PRT.PF*(8.33/12))) AS EPS " +
                               "FROM STFPERSONAL ST, PRREPORTTABLE PRT, PRSTAFF PS " +
                               "WHERE PRT.STAFFID = ST.STAFFID AND PS.STAFFID = PRT.STAFFID " +
                               "AND TRANSACTIONDATE BETWEEN ?DATEFROM AND ?DATETO GROUP BY PRT.Name";

                        break;
                    }
                case SQLCommand.Payroll.EmployeePFReport:
                    {
                        query = "SELECT UAN AS UAN,RT. NAME AS STAFF_NAME,\n" +
                            "       ROUND(`PF WAGES`, 2) AS 'WAGES',\n" +
                            "       ROUND(RT.PF, 2) AS PF,\n" +
                            "       ROUND((RT. PF) * (3.67 / 12), 2) AS EPF,\n" +
                            "       ROUND((RT. PF) * (8.33 / 12), 2) AS EPS\n" +
                            "  FROM STFPERSONAL SP, PRREPORTTABLE RT, PRSTAFF PS,STFSERVICE SS\n" +
                            " WHERE RT.STAFFID = SP.STAFFID\n" +
                            "   AND PS.STAFFID = RT.STAFFID\n" +
                            "   AND SS.STAFFID = SP.STAFFID\n" +
                            "   AND PS.PAYROLLID =?PAYROLLID \n" +
                            "   ?CONDITION\n" +
                            " GROUP BY RT.NAME;";
                        break;
                    }
                case SQLCommand.Payroll.FetchWagesReport:
                    {
                        //  query = "select ?FIELDVALUE ,' ' as SIGNATURE from ?TABLENAME,prstaffgroup " +
                        //    " where ?TABLENAME.groupid = ?GROUPID "+
                        //   " AND ?TABLENAME.staffid = prstaffgroup.staffid " +
                        //    " AND prstaffgroup.payrollid =?PAYROLLID "+
                        //  " order by prstaffgroup.groupid,prstaffgroup.stafforder";

                        query = "SELECT DISTINCT PRStaffGroup.StaffId, PRStaffGroup.GroupId," +
                              " CONCAT(STFPERSONAL.FIRSTNAME, ' ' , STFPERSONAL.LASTNAME) as STAFFNAME," +
                              " PRStaff.ComponentId, PRStaff.CompValue, PRComponent.Component, '' as signature " +
                              " FROM PRStaffGroup, PRStaff, PRComponent, stfpersonal " +
                              " WHERE PRComponent.ComponentId = PRStaff.ComponentId " +
                              "  AND stfpersonal.STAFFID = prstaff.STAFFID " +
                              "  AND PRStaff.PayRollId =?PAYROLLID " +
                              "  AND PRStaffGroup.Payrollid = PRStaff.Payrollid " +
                              "  AND PRStaff.StaffId = PRStaffGroup.StaffId  " +
                              " AND prstaffgroup.groupid =?GROUPID" +
                              " ORDER BY prstaff.staffid, prstaff.COMPORDER ";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentByGroupIds:
                    {
                        query = "SELECT PRCM.SALARYGROUPID, PRCM.COMPONENTID, PRC.COMPONENT, PRCM.TYPE, PROCESS_COMPONENT_TYPE,\n" +
                                "CASE WHEN PRCM.TYPE = '0'\n" + 
                                "    THEN 'Earning'\n" +
                                "WHEN PRCM.TYPE = '1'\n" + 
                                "    THEN 'Deduction'\n" +
                                "WHEN PRCM.TYPE = '3'\n" + 
                                "    THEN 'Calculation' ELSE 'Text' END AS TYPE_NAME,\n" +
                                "CASE WHEN PROCESS_COMPONENT_TYPE=1 THEN '" + PayRollProcessComponent.NetPay.ToString()  + "'\n" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 2 THEN '"+ PayRollProcessComponent.GrossWages.ToString() +"'" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 3 THEN '" + PayRollProcessComponent.Deductions.ToString()  + "'" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 4 THEN '" + PayRollProcessComponent.EPF.ToString() + "'" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 5 THEN '" + PayRollProcessComponent.PT.ToString() + "'" +
                                "  WHEN PROCESS_COMPONENT_TYPE = 6 THEN '" + PayRollProcessComponent.ESI.ToString() + "'" +
                                "  ELSE 'None' END PROCESS_COMPONENT_TYPE_NAME," +
                                "PRCM.COMP_ORDER AS COMPORDER\n" + 
                                "FROM PRCOMPMONTH PRCM\n" +
                                "INNER JOIN PRCOMPONENT PRC ON PRC.COMPONENTID = PRCM.COMPONENTID\n" +
                                "WHERE PRCM.SALARYGROUPID IN(?IDs)AND PRCM.PAYROLLID =?PAYROLLID GROUP BY PRC.COMPONENT";
                        break;
                    }
                case SQLCommand.Payroll.FetchStaffByGroupIds:
                    {
                        //query = "SELECT PRSG.GROUPID,\n" +
                        //"       PRSG.STAFFID,\n" +
                        //"       CONCAT(PRS.FIRSTNAME, CONCAT(' ', PRS.LASTNAME)) AS 'STAFFNAME'\n" +
                        //"  FROM prstaffgroup PRSG\n" +
                        //" INNER JOIN stfpersonal PRS\n" +
                        //"    ON PRS.STAFFID = PRSG.STAFFID\n" +
                        //" WHERE FIND_IN_SET(PRSG.GROUPID,?IDs)\n" +
                        //"   AND PRSG.PAYROLLID =?PAYROLLID;";
                        
                        //On 01/11/2023
                        query = "SELECT DISTINCT PRSG.GROUPID, PRSG.STAFFID, PRS.EMPNO, IFNULL(PRSG.STAFFORDER,0) AS STAFFORDER, \n" +
                               " CONCAT(IFNULL(CONCAT(PNT.NAME_TITLE, ' '), ''), PRS.FIRSTNAME,CONCAT(' ', IFNULL(PRS.MIDDLE_NAME,'')),CONCAT(' ',PRS.lastname)) AS 'STAFFNAME',\n" +
                               "PRS.DESIGNATION, SS.UAN, PRS.PAN_NO, PRS.AADHAR_NO, SG.GROUPNAME, PRS.ACCOUNT_NUMBER, IFNULL(PRD.DEPARTMENT,'') AS DEPARTMENT\n" +
                               " FROM PRSTAFFGROUP AS PRSG\n" +
                               " INNER JOIN PRSALARYGROUP SG ON SG.GROUPID = PRSG.GROUPID\n" +
                               " INNER JOIN STFPERSONAL PRS ON PRS.STAFFID = PRSG.STAFFID\n" +
                               " LEFT JOIN STFSERVICE SS ON SS.STAFFID = PRS.STAFFID\n" +
                               " LEFT JOIN PRPROJECT_STAFF PSS ON PSS.STAFFID = PRSG.STAFFID \n" +
                               " LEFT JOIN PR_NAME_TITLE PNT ON PNT.NAME_TITLE_ID = PRS.NAME_TITLE_ID \n" +
                               " LEFT JOIN PR_DEPARTMENT PRD ON PRD.DEPARTMENT_ID = PRSG.DEPARTMENT_ID \n" +
                               /*" LEFT JOIN PRSTAFFGROUP AS PRSG1 ON PRSG1.STAFFID = PRSG.STAFFID AND PRSG1.GROUPID = PRSG.GROUPID\n" +
                               " {AND PRSG1.PAYROLLID = ?RECENT_PAYROLL_ID} \n" + //{AND PRSG1.PAYROLLID =?PAYROLLID} On 03/03/2023*/

                               " WHERE 1=1 {AND PRSG.PAYROLLID =?PAYROLLID} {AND FIND_IN_SET(PRSG.GROUPID,?IDs)}  {AND FIND_IN_SET(PRSG.GROUPID,?IDs)}\n" +
                               " { AND PSS.PROJECT_ID IN (?PROJECT_ID) } GROUP BY PRSG.STAFFID ORDER BY SG.GROUPNAME, IFNULL(PRSG.STAFFORDER,0)";
                        break;
                    }
                case SQLCommand.Payroll.DeleteMapLedger:
                    {
                        query = "DELETE FROM PAYROLL_LEDGER WHERE TYPE_ID IN (?TYPE_ID)";
                        break;
                    }
                case SQLCommand.Payroll.AddMapLedger:
                    {
                        query = "INSERT INTO PAYROLL_LEDGER(TYPE_ID, LEDGER_ID) VALUES(?TYPE_ID,?LEDGER_ID)";
                        break;
                    }
                case SQLCommand.Payroll.FetchMappedLedger:
                    {
                        query = "SELECT TYPE_ID,LEDGER_ID FROM PAYROLL_LEDGER";
                        break;
                    }

                case SQLCommand.Payroll.FetchProcessByMappedLedger:
                    {
                        //query =  "SELECT TYPE_ID,PL.LEDGER_ID,LEDGER_NAME FROM payroll_ledger PL\n" +
                        //        "INNER JOIN master_ledger ML\n" + 
                        //        "ON PL.LEDGER_ID=ML.LEDGER_ID;";
                        query = "SELECT PROCESS_TYPE,LEDGER_NAME FROM PAYROLL_LEDGER PL\n" +
                                "INNER JOIN MASTER_LEDGER ML\n" +
                                "ON PL.LEDGER_ID=ML.LEDGER_ID\n" +
                                "INNER JOIN PROCESS_TYPE PT\n" +
                                "ON PL.TYPE_ID=PT.TYPE_ID;";
                        break;
                    }
                case SQLCommand.Payroll.FetchMappedLedgersByTypeId:
                    {
                        query = "SELECT TYPE_ID,LEDGER_ID FROM PAYROLL_LEDGER WHERE TYPE_ID=?TYPE_ID;";
                        break;
                    }
                case SQLCommand.Payroll.FetchLedgerByLedgerId:
                    {
                        query = "SELECT LEDGER_NAME FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Payroll.FetchMappedComponentsbyProjectId:
                    {

                        query = "SELECT PRPS.STAFFID, PRSG.GROUPID, PRCM.COMPONENTID\n" +
                        "  FROM PRPROJECT_STAFF PRPS\n" +
                        " INNER JOIN PRSTAFFGROUP PRSG\n" +
                        "    ON PRSG.STAFFID = PRPS.STAFFID\n" +
                        " INNER JOIN PRCOMPMONTH PRCM\n" +
                        "    ON PRCM.SALARYGROUPID = PRSG.GROUPID\n" +
                        " WHERE PRPS.PROJECT_ID = ?PROJECT_ID\n" +
                        " GROUP BY GROUPID;";

                        break;
                    }
                case SQLCommand.Payroll.DeleteVoucherTransPayrollVoucher:
                    {
                        query = "DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID IN " +
                                "(SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE CLIENT_REFERENCE_ID=?PROCESS_REF_PAY_ID AND PROJECT_ID=?PROJECT_ID)";
                        //query = "UPDATE VOUCHER_MASTER_TRANS SET STATUS=0 WHERE CLIENT_REFERENCE_ID=?PROCESS_REF_PAY_ID";
                        break;
                    }
                case SQLCommand.Payroll.FetchVoucherMasterPayrollVoucher:
                    {
                        //query = "DELETE FROM VOUCHER_MASTER_TRANS WHERE CLIENT_REFERENCE_ID=?PROCESS_REF_PAY_ID AND PROJECT_ID=?PROJECT_ID";
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE CLIENT_REFERENCE_ID=?PROCESS_REF_PAY_ID AND PROJECT_ID=?PROJECT_ID AND STATUS=1";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentValuetoProcess:
                    {
                        query = "SELECT STAFFID,\n" +
                                "       PS.COMPONENTID,PR.COMPONENT,\n" +
                                "       PR.TYPE,\n" +
                                "       SUM(PS.COMPVALUE) AS AMOUNT,\n" +
                                "       PR.LEDGER_ID,PL.LEDGER_ID AS PROCESS_LEDGER_ID,PROCESS_TYPE_ID\n" +
                                "  FROM PRSTAFF PS\n" +
                                " INNER JOIN PRCOMPONENT PR\n" +
                                "    ON PR.COMPONENTID = PS.COMPONENTID\n" +
                                  " INNER JOIN PAYROLL_LEDGER PL\n" +
                                "    ON PL.TYPE_ID=PR.PROCESS_TYPE_ID\n" +
                                " WHERE PR.TYPE IN (0, 1)\n" +
                                "   AND STAFFID IN (?STAFFID)\n" +
                                 "   AND PS.PAYROLLID IN (?PAYROLLID)\n" +
                                "   AND LENGTH(RELATEDCOMPONENTS) -\n" +
                                "       LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2   AND COMPVALUE<>'0'  \n" +    // ÿ
                                " GROUP BY PS.COMPONENTID;";
                        break;
                    }
                case SQLCommand.Payroll.FetchComponentInEquationAndMapped:
                    {
                        query = "SELECT COMPONENTID, EQUATIONID FROM PRCOMPONENT WHERE EQUATIONID LIKE '%<?COMPONENTID>%' UNION ALL\n" +
                                "SELECT COMPONENTID, EQUATIONID FROM PRCOMPMONTH WHERE (EQUATIONID LIKE '%<?COMPONENTID>%' OR COMPONENTID = ?COMPONENTID);";
                        break;
                    }
                case SQLCommand.Payroll.FetchLoanComponentValue:
                    {
                        //query = "SELECT PS.STAFFID,\n" +
                        //"       PS.COMPONENTID,\n" +
                        //"       PR.COMPONENT,\n" +
                        //"       PR.TYPE,\n" +
                        //"       SUM(PS.COMPVALUE) AS AMOUNT,\n" +
                        //"        SUM(PRLG.INTRESTAMOUNT) AS INRESTAMOUNT,\n" +
                        //"       (SUM(PS.COMPVALUE) - SUM(PRLG.INTRESTAMOUNT)) AS 'ACTUAL_AMOUNT',\n" +
                        //"       PR.LEDGER_ID,\n" +
                        //"       PL.LEDGER_ID AS PROCESS_LEDGER_ID,\n" +
                        //"       PROCESS_TYPE_ID\n" +
                        //"  FROM PRSTAFF PS\n" +
                        //" INNER JOIN PRCOMPONENT PR\n" +
                        //"    ON PR.COMPONENTID = PS.COMPONENTID\n" +
                        //" INNER JOIN PAYROLL_LEDGER PL\n" +
                        //"    ON PL.TYPE_ID = PR.PROCESS_TYPE_ID\n" +
                        //" LEFT JOIN PRLOANGET PRLG\n" +
                        //"    ON PRLG.STAFFID = PS.STAFFID\n" +
                        //" WHERE PR.TYPE IN (0,1)\n" +
                        //"   AND PS.STAFFID IN (?STAFFID)\n" +
                        //"   AND PS.PAYROLLID IN (?PAYROLLID)\n" +
                        //"   AND LENGTH(RELATEDCOMPONENTS) -\n" +
                        //"       LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2\n" +
                        //"   AND COMPVALUE <> '0'\n" +
                        //" GROUP BY PS.COMPONENTID;";
                        query = "SELECT T.STAFFID,\n" +
                                "       T.COMPONENTID,\n" +
                                "       T.COMPONENT,\n" +
                                "       T.TYPE,\n" +
                                "       SUM(T.AMOUNT) AS AMOUNT,\n" +
                                "       T.INTRESTAMOUNT,\n" +
                                "       T.LEDGER_ID,\n" +
                                "       T.PROCESS_LEDGER_ID,\n" +
                                "       T.PROCESS_TYPE_ID\n" +
                                "  FROM (SELECT PS.STAFFID,\n" +
                                "               PS.COMPONENTID,\n" +
                                "               PR.COMPONENT,\n" +
                                "               PR.TYPE,\n" +
                                "               PS.COMPVALUE AS AMOUNT,\n" +
                                "               IFNULL(FNL.INTAMT, 0.00) AS INTRESTAMOUNT,\n" +
                                "               PR.LEDGER_ID,\n" +
                                "               PL.LEDGER_ID AS PROCESS_LEDGER_ID,\n" +
                                "               PR.PROCESS_TYPE_ID\n" +
                                "          FROM PRSTAFF PS\n" +
                                "         INNER JOIN PRCOMPONENT PR\n" +
                                "            ON PR.COMPONENTID = PS.COMPONENTID\n" +
                                "         INNER JOIN PAYROLL_LEDGER PL\n" +
                                "            ON PL.TYPE_ID = PR.PROCESS_TYPE_ID\n" +
                                "          LEFT JOIN (SELECT PG.STAFFID,\n" +
                                "                           PC.COMPONENTID,\n" +
                                "                           '' AS COMPONENT,\n" +
                                "                           '' AS TYPE,\n" +
                                "                           0 AS AMOUNT,\n" +
                                "                           SUM(PG.INTRESTAMOUNT) AS INTAMT,\n" +
                                "                           PC.LEDGER_ID,\n" +
                                "                           0 AS PROCESS_LEDGER_ID,\n" +
                                "                           PROCESS_TYPE_ID\n" +
                                "                      FROM PRCOMPONENT PC\n" +
                                "                     INNER JOIN PRSTAFF PS\n" +
                                "                        ON PS.COMPONENTID = PC.COMPONENTID\n" +
                                "                       AND PC.TYPE = 1\n" +
                                "                       AND PC.LINKVALUE LIKE 'Loan%'\n" +
                                "                       AND PS.PAYROLLID IN (?PAYROLLID)\n" +
                                "                     INNER JOIN PRLOANGET PG\n" +
                                "                        ON PG.STAFFID = PS.STAFFID\n" +
                                "                     WHERE PG.STAFFID IN (?STAFFID)) AS FNL\n" +
                                "            ON FNL.COMPONENTID = PR.COMPONENTID\n" +
                                "         WHERE PR.TYPE IN (0, 1)\n" +
                                "           AND PS.STAFFID IN (?STAFFID)\n" +
                                "           AND PS.PAYROLLID IN (?PAYROLLID)\n" +
                                "           AND LENGTH(RELATEDCOMPONENTS) -\n" +
                                "               LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2\n" +
                                "           AND COMPVALUE <> '0'\n" +
                                "        ) AS T\n" +
                                " GROUP BY T.LEDGER_ID";
                        break;
                    }
                case SQLCommand.Payroll.IsComponentLedgerExists:
                    {
                        query = "SELECT PC.COMPONENTID, COMPONENT, LEDGER_ID\n" +
                                " FROM PRCOMPONENT PC\n" +
                                "INNER JOIN PRCOMPMONTH PCM  \n" +
                                " ON PCM.COMPONENTID=PC.COMPONENTID \n" +
                                " WHERE PC.TYPE IN (0, 1)\n" +
                                " AND LENGTH(RELATEDCOMPONENTS) -\n" +
                                " LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2 ";

                        break;
                    }
                case SQLCommand.Payroll.IsLedgerMappedWithProject:
                    {
                        query = "SELECT LEDGER_ID AS LEDGERID\n" +
                                "  FROM PROJECT_LEDGER\n" +
                                " WHERE LEDGER_ID IN (?LEDGER_ID_COLLECTION)\n" +
                                "   AND PROJECT_ID IN (?PROJECT_ID)";
                        break;
                    }
                case SQLCommand.Payroll.IsLoanledgerMappedwithProject:
                    {
                        query = "SELECT LEDGER_ID AS LEDGERID\n" +
                                "  FROM PROJECT_LEDGER\n" +
                                " WHERE LEDGER_ID IN (1001,1002)\n" +
                                "   AND PROJECT_ID IN (?PROJECT_ID)";
                        break;
                    }
                #endregion

                #region Payroll Transaction
                case SQLCommand.Payroll.IsLedgerExists:
                    {
                        query = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_LEDGER\n" +
                                            " WHERE  LEDGER_NAME =?LEDGER_NAME";
                        break;
                    }
                case SQLCommand.Payroll.FetchLedgerByLedgerName:
                    {
                        query = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.Payroll.FetchLedgerIdLedgerNameByLedgerId:
                    {
                        query = "SELECT LEDGER_ID,LEDGER_NAME FROM MASTER_LEDGER WHERE LEDGER_ID IN (?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.Payroll.ProjectLedgerMapping:
                    {
                        query = "INSERT INTO PROJECT_LEDGER(PROJECT_ID,LEDGER_ID) VALUES(?PROJECT_ID,?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.Payroll.ProjectLedgerMappingDelete:
                    {
                        query = "DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Payroll.CheckLoanExists:
                    {
                        query = "SELECT COUNT(*) FROM PRLOANGET WHERE STAFFID IN (?STAFFID);";
                        break;
                    }
                #endregion

                #region Map Project payroll
                case SQLCommand.Payroll.MapProjectToPayroll:
                    {
                        query = "INSERT INTO PAYROLL_PROJECT(PROJECT_ID,PAYROLLID) VALUES(?PROJECT_ID,?PAYROLLID);";
                        break;
                    }
                case SQLCommand.Payroll.DeleteProjectpayroll:
                    {
                        query = "DELETE FROM PAYROLL_PROJECT;";
                        break;
                    }
                case SQLCommand.Payroll.FetchMappedPayrollProjects:
                    {

                        //query = "SELECT PROJECT_ID,PROJECT,\n" +
                        //"       CASE\n" +
                        //"         WHEN PROJECT_ID IN (SELECT PROJECT_ID FROM PAYROLL_PROJECT) THEN\n" +
                        //"          '1'\n" +
                        //"         ELSE\n" +
                        //"          '0'\n" +
                        //"       END AS 'SELECT'\n" +
                        //"  FROM MASTER_PROJECT WHERE DELETE_FLAG <> 1 ORDER BY PROJECT ASC;";
                        query = "SELECT PROJECT_ID,PROJECT\n" +
                            //"       CASE\n" +
                            //  "         WHEN PROJECT_ID IN (SELECT PROJECT_ID FROM PAYROLL_PROJECT) THEN\n" +
                            // "          '1'\n" +
                            // "         ELSE\n" +
                            // "          '0'\n" +
                            // "       END AS 'SELECT'\n" +
                        "  FROM MASTER_PROJECT WHERE PROJECT_ID IN (SELECT PROJECT_ID FROM PRPROJECT_STAFF GROUP BY PROJECT_ID) AND DELETE_FLAG <> 1 ORDER BY PROJECT ASC;";
                        break;

                    }

                case SQLCommand.Payroll.FetchMappedProjectCashBankLedgers:
                    {
                        query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID,\n" +
                                "CASE\n" +
                                "         WHEN ML.GROUP_ID = 12 THEN\n" +
                                "          CONCAT(ML.LEDGER_NAME, ' ( ', MB.BANK, ' - ', MB.BRANCH, ')')\n" +
                                "         ELSE\n" +
                                "          ML.LEDGER_NAME\n" +
                                "       END AS LEDGER_NAME, MBA.DATE_CLOSED, MBA.DATE_OPENED\n" +
                                "  FROM PROJECT_LEDGER PL\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                "    ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MB.BANK_ID = MBA.BANK_ID\n" +
                                " WHERE ML.GROUP_ID IN (13, 12)\n" +
                                "   AND PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " {AND (MBA.DATE_CLOSED IS NULL OR MBA.DATE_CLOSED >= ?BANK_CLOSED_DATE) } " + //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                                " ORDER BY LEDGER_ID ASC;";
                        break;

                    }
                case SQLCommand.Payroll.FetchProjectsforPayroll:
                    {

                        query = "SELECT MP.PROJECT_ID, MP.PROJECT\n" +
                        "  FROM MASTER_PROJECT MP\n" +
                        " INNER JOIN PAYROLL_PROJECT PP\n" +
                        "    ON PP.PROJECT_ID = MP.PROJECT_ID\n" +
                        " WHERE MP.PROJECT_ID IN (PP.PROJECT_ID);";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollPaymentMode:
                    {
                        query = "SELECT PAYMENT_MODE_ID, PAYMENT_MODE, IS_BANK FROM PR_PAYMENT_MODE ORDER BY PAYMENT_MODE";
                        break;
                    }
                case SQLCommand.Payroll.CheckComponentsProcessedforProject:
                    {
                        query = "SELECT COUNT(*) FROM PRPROJECT_STAFF WHERE PROJECT_ID IN (?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.Payroll.CheckProjectExists:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_PROJECT WHERE PROJECT IN (?GROUPNAME);";
                        break;
                    }
                case SQLCommand.Payroll.CheckProjectExistforPayroll:
                    {
                        query = "SELECT COUNT(*) FROM PAYROLL_PROJECT WHERE PROJECT_ID IN (?STAFFID);";
                        break;
                    }
                case SQLCommand.Payroll.FetchprojectIdByProjectName:
                    {
                        query = "SELECT PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT IN (?GROUPNAME);";
                        break;
                    }
                #endregion

                #region ConstructRangeFormula
                case SQLCommand.Payroll.InsertRangeCondtions:
                    {
                        query = "INSERT INTO PAYROLL_RANGE_FORMULA(COMPONENTID,LINK_COMPONENT_ID,MIN_VALUE,MAX_VALUE,MAX_SLAB) VALUES(?COMPONENTID,?LINK_COMPONENT_ID,?MIN_VALUE,?MAX_VALUE,?MAX_SLAB);";
                        break;
                    }
                case SQLCommand.Payroll.FetchRangeComponents:
                    {

                        query = "SELECT COMPONENTID,LINK_COMPONENT_ID,MIN_VALUE,MAX_VALUE,MAX_SLAB\n" +
                                "FROM\n" +
                                "PAYROLL_RANGE_FORMULA";
                        break;
                    }
                case SQLCommand.Payroll.FetchRangeValuesByComponentId:
                    {

                        query = "SELECT PRF.LINK_COMPONENT_ID,\n" +
                                "       (SELECT COMPONENT\n" +
                                "          FROM PRCOMPONENT\n" +
                                "         WHERE COMPONENTID IN (PRF.LINK_COMPONENT_ID)) AS 'LINK_COMPONENT',\n" +
                                "       PRF.MIN_VALUE,\n" +
                                "       PRF.MAX_VALUE,\n" +
                                "       PRF.MAX_SLAB\n" +
                                "  FROM payroll_range_formula PRF\n" +
                                " INNER JOIN PRCOMPONENT PRC\n" +
                                "    ON PRC.COMPONENTID = PRF.COMPONENTID\n" +
                                " WHERE PRF.COMPONENTID = ?COMPONENTID;";
                        break;
                    }
                case SQLCommand.Payroll.DeleteRangeByComponentId:
                    {
                        query = "DELETE FROM PAYROLL_RANGE_FORMULA WHERE COMPONENTID = ?COMPONENTID";
                        break;
                    }
                #endregion

                case SQLCommand.Payroll.FetchPayrollComponentforpayment:
                    {
                        query = "SELECT COMPONENTID,COMPONENT\n" +
                        "  FROM PRCOMPONENT\n" +
                        " WHERE LENGTH(RELATEDCOMPONENTS) -\n" +
                        "       LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2\n" +
                        "   AND TYPE IN (?SALARYGROUPID);";
                        break;
                    }

                #region payroll Post payemnt
                case SQLCommand.Payroll.FetchLegderDetailsByPorjectId:
                    {

                        query = "SELECT ML.LEDGER_ID, LEDGER_NAME, MLG.LEDGER_GROUP\n" +
                        "  FROM MASTER_LEDGER ML\n" +
                        " INNER JOIN PROJECT_LEDGER PLG\n" +
                        "    ON PLG.LEDGER_ID = ML.LEDGER_ID\n" +
                        " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        " WHERE MLG.NATURE_ID IN (2)\n" +
                        "   AND ML.GROUP_ID NOT IN (12,13,14) AND PLG.PROJECT_ID IN (?PROJECT_ID);";

                        break;
                    }
                case SQLCommand.Payroll.FetchLiabilityLedgersByProjectId:
                    {
                        query = "SELECT ML.LEDGER_ID, LEDGER_NAME, MLG.LEDGER_GROUP\n" +
                        "  FROM MASTER_LEDGER ML\n" +
                        " INNER JOIN PROJECT_LEDGER PLG\n" +
                        "    ON PLG.LEDGER_ID = ML.LEDGER_ID\n" +
                        " INNER JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "    ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        " WHERE MLG.NATURE_ID IN (4)\n" +
                        "   AND ML.GROUP_ID NOT IN (12,13,14) AND PLG.PROJECT_ID IN (?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.Payroll.FetchProcessedValuesBySelectedComponentID:
                    {

                        //query = "SELECT SUM(PRS.COMPVALUE)\n" +
                        //"  FROM PRCOMPONENT PRC\n" +
                        //" INNER JOIN PRSTAFF PRS\n" +
                        //"    ON PRS.COMPONENTID = PRC.COMPONENTID\n" +
                        //" WHERE LENGTH(RELATEDCOMPONENTS) -\n" +
                        //"       LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2\n" +
                        //"   AND TYPE IN (?SALARYGROUPID)\n" +
                        //"   AND PRS.PAYROLLID = ?PAYROLLID;";

                        query = "SELECT SUM(PRS.COMPVALUE)\n" +
                                "  FROM PRCOMPONENT PRC\n" +
                                " INNER JOIN PRSTAFF PRS\n" +
                                "    ON PRS.COMPONENTID = PRC.COMPONENTID\n" +
                                " WHERE\n" +
                                " TYPE IN (?SALARYGROUPID) AND PRS.COMPONENTID=21\n" +
                                "   AND PRS.PAYROLLID = ?PAYROLLID;";

                        break;
                    }
                case SQLCommand.Payroll.InsertPostPaymentDetails:
                    {
                        query = "INSERT INTO PAYROLL_VOUCHER\n" +
                                "(PAYROLL_ID, SALARY_GROUP_ID, COMPONENT_ID, LEDGER_ID, VOUCHER_ID, AMOUNT)\n" +
                                "VALUES(?PAYROLL_ID, ?SALARY_GROUP_ID, ?COMPONENT_ID, ?LEDGER_ID, ?VOUCHER_ID, ?AMOUNT);";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollPostPaymentsByPayrollId:
                    {
                        query = "SELECT VOUCHER_ID FROM PAYROLL_VOUCHER WHERE PAYROLL_ID=?PAYROLL_ID";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollPostPaymentsByPayrollIdCompId:
                    {
                        query = "SELECT VOUCHER_ID FROM PAYROLL_VOUCHER WHERE 1=1 {AND PAYROLL_ID=?PAYROLL_ID} AND COMPONENT_ID=?COMPONENT_ID";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollPostPaymentVouhcerMaster:
                    {
                        query = query = "SELECT VM.VOUCHER_ID, MP.PROJECT_ID, PR.PRNAME, GROUP_CONCAT(DISTINCT PG.GROUPNAME ORDER BY PG.GROUPNAME SEPARATOR  ', ') AS PAYROLL_GROUP, \n" +
                              " VOUCHER_DATE, VOUCHER_NO, VOUCHER_SUB_TYPE, \n" +
                              "       RCPYCN.LEDGER_NAME AS LEDGER_NAME, CASHBANK.LEDGER_NAME AS CASHBANK, \n" +
                              "       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                              "       CASE VM.VOUCHER_TYPE\n" +
                              "         WHEN 'RC' THEN\n" +
                              "          'Receipt'\n" +
                              "         WHEN 'PY' THEN\n" +
                              "          'Payment'\n" +
                              "         WHEN 'CN' THEN\n" +
                              "          'Contra'\n" +
                              "         ELSE\n" +
                              "          'JOURNAL'\n" +
                              "       END AS VOUCHERTYPE,\n" +
                              "       IF(VM.VOUCHER_TYPE = 'PY', CASHBANK.AMOUNT, 0) AS DEBIT_AMOUNT,\n" +
                              "       IF(VM.VOUCHER_TYPE = 'RC' OR VM.VOUCHER_TYPE = 'CN', CASHBANK.AMOUNT, 0) AS CREDIT_AMOUNT, VM.NARRATION,\n" +
                              " IF(VM.VOUCHER_DEFINITION_ID<=4, CASE VM.VOUCHER_TYPE\n" +
                              "         WHEN 'RC' THEN 'Receipt'\n" +
                              "         WHEN 'PY' THEN 'Payment'\n" +
                              "         WHEN 'CN' THEN 'Contra'\n" +
                              "         ELSE 'Journal' END, \n" +
                              "       MV.VOUCHER_NAME) AS VOUCHER_TYPE, VM.VOUCHER_DEFINITION_ID \n" +
                              "  FROM VOUCHER_MASTER_TRANS AS VM\n" +
                              " INNER JOIN MASTER_PROJECT AS MP\n" +
                              "    ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                              " INNER JOIN MASTER_DIVISION AS MD\n" +
                              "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                              " INNER JOIN PAYROLL_VOUCHER PV  ON PV.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              " INNER JOIN PRCREATE PR ON PR.PAYROLLID = PV.PAYROLL_ID\n" +
                              " INNER JOIN PRSALARYGROUP PG ON PG.GROUPID = PV.SALARY_GROUP_ID\n" +
                              "  LEFT JOIN MASTER_VOUCHER AS MV\n" +
                              "    ON MV.VOUCHER_ID = VM.VOUCHER_DEFINITION_ID\n" +
                              "\n" +
                              "  LEFT JOIN (SELECT T.VOUCHER_ID, T.LEDGER_NAME, SUM(T.AMOUNT) AS AMOUNT\n" +
                              "               FROM (SELECT VT.VOUCHER_ID,\n" +
                              "                            CASE\n" +
                              "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                              "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                              "                                      CONCAT(MB.BANK, ' - '),\n" +
                              "                                      MB.BRANCH)\n" +
                              "                              ELSE\n" +
                              "                               ML.LEDGER_NAME\n" +
                              "                            END AS LEDGER_NAME,\n" +
                              "                            VT.TRANS_MODE,\n" +
                              "                            VT.AMOUNT\n" +
                              "                       FROM VOUCHER_TRANS VT\n" +
                              "                      INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                              "                         ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VM.VOUCHER_TYPE ='" + VoucherSubTypes.PY.ToString() + "' AND VM.VOUCHER_SUB_TYPE ='" + VoucherSubTypes.PAY.ToString() + "'\n" +
                              "                       LEFT JOIN MASTER_LEDGER ML\n" +
                              "                         ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "                         ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "                       LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                              "                         ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_BANK MB\n" +
                              "                         ON MB.BANK_ID = MBA.BANK_ID\n" +
                              "                      WHERE IF(VM.VOUCHER_TYPE = 'CN', VT.TRANS_MODE = 'CR', ML.GROUP_ID NOT IN (12, 13))\n" + //VM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO
                              "                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                              "              GROUP BY T.VOUCHER_ID) AS RCPYCN\n" +
                              "    ON RCPYCN.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "\n" +
                              "  LEFT JOIN (SELECT T.VOUCHER_ID,\n" +
                              "                    T.LEDGER_NAME,\n" +
                              "                    SUM(T.AMOUNT) AS AMOUNT,\n" +
                              "                    T.TRANS_MODE\n" +
                              "               FROM (SELECT VT.VOUCHER_ID,\n" +
                              "                            CASE\n" +
                              "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                              "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                              "                                      CONCAT(MB.BANK, ' - '),\n" +
                              "                                      MB.BRANCH)\n" +
                              "                              ELSE\n" +
                              "                               ML.LEDGER_NAME\n" +
                              "                            END AS LEDGER_NAME,\n" +
                              "                            VT.AMOUNT AS AMOUNT,\n" +
                              "                            TRANS_MODE\n" +
                              "                       FROM VOUCHER_TRANS VT\n" +
                              "                      INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                              "                         ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VM.VOUCHER_TYPE ='" + VoucherSubTypes.PY.ToString() + "' AND VM.VOUCHER_SUB_TYPE ='" + VoucherSubTypes.PAY.ToString() + "'\n" +
                              "                       LEFT JOIN MASTER_LEDGER ML\n" +
                              "                         ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "                         ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "                       LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                              "                         ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_BANK MB\n" +
                              "                         ON MB.BANK_ID = MBA.BANK_ID\n" +
                              "                      WHERE IF(VM.VOUCHER_TYPE = 'CN', VT.TRANS_MODE = 'DR', ML.GROUP_ID IN (12, 13))\n" + //VM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND 
                              "                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                              "              GROUP BY T.VOUCHER_ID) AS CASHBANK\n" +
                              "    ON CASHBANK.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "\n" +
                              " WHERE VM.VOUCHER_TYPE ='" + VoucherSubTypes.PY.ToString() + "' AND VM.VOUCHER_SUB_TYPE ='" + VoucherSubTypes.PAY.ToString() + "'\n" +
                              " AND PV.PAYROLL_ID = ?PAYROLL_ID AND VM.STATUS = 1\n" + //AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND 
                              " GROUP BY VM.VOUCHER_ID\n" +
                              " ORDER BY VOUCHER_DATE, LENGTH(VOUCHER_NO), VOUCHER_NO ASC;";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollPostPaymentVouhcerDetails:
                    {
                        query = "SELECT VT.VOUCHER_ID, IFNULL(PVDetails.COMPONENT,'') AS COMPONENT,\n"+ 
                            "  CASE WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN CONCAT(CONCAT(ML.LEDGER_NAME, ' - '), CONCAT(MB.BANK, ' - '), MB.BRANCH)\n"+
                            "    ELSE ML.LEDGER_NAME END AS LEDGER_NAME,\n" + 
                            "        VT.LEDGER_ID,VT.SEQUENCE_NO, VT.TRANS_MODE,\n" +
                            "       CASE VT.TRANS_MODE\n" +
                            "         WHEN 'CR' THEN\n" +
                            "          VT.AMOUNT\n" +
                            "         ELSE\n" +
                            "          0.00\n" +
                            "       END AS 'CREDIT',\n" +
                            "       CASE VT.TRANS_MODE\n" +
                            "         WHEN 'DR' THEN\n" +
                            "          VT.AMOUNT\n" +
                            "         ELSE\n" +
                            "          0.00\n" +
                            "       END AS 'DEBIT',\n" +
                            "       MBA.ACCOUNT_NUMBER,\n" +
                            "       CONCAT(CHEQUE_NO, CONCAT(CONCAT(IF(CHEQUE_REF_DATE IS NULL OR CHEQUE_NO='','', CONCAT(' - ',DATE_FORMAT(CHEQUE_REF_DATE,'%d/%m/%Y'))),\n" +
                            "       IF(CHEQUE_REF_DATE IS NULL,'', CONCAT(', ', CHEQUE_REF_BANKNAME))),\n" +
                            "       IF(CHEQUE_REF_BANKNAME IS NULL OR CHEQUE_REF_BANKNAME='', '', CONCAT(', ', CHEQUE_REF_BRANCH)))) AS CHEQUE_NO,\n" +
                            "       MATERIALIZED_ON\n" +
                            "FROM VOUCHER_TRANS AS VT\n" +
                            "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                            "INNER JOIN MASTER_LEDGER AS ML     ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                            "LEFT JOIN (SELECT PV.VOUCHER_ID, PV.COMPONENT_ID, PV.LEDGER_ID, GROUP_CONCAT(PC.COMPONENT) AS COMPONENT , IF(PC.TYPE=0, '" + TransactionMode.DR + "', '" + TransactionMode.CR + "') AS TRANS_MODE\n" +
                            "FROM PAYROLL_VOUCHER PV \n" +
                            "INNER JOIN PRCOMPONENT PC  ON PC.COMPONENTID = PV.COMPONENT_ID\n" +
                            "WHERE PV.PAYROLL_ID = ?PAYROLL_ID\n" + 
                            "GROUP BY PV.VOUCHER_ID, PV.LEDGER_ID, TRANS_MODE) AS PVDetails ON PVDetails.VOUCHER_ID = VT.VOUCHER_ID AND PVDetails.LEDGER_ID = VT.LEDGER_ID AND PVDetails.TRANS_MODE = VT.TRANS_MODE\n" +
                            "LEFT JOIN MASTER_BANK_ACCOUNT AS MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                            "LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                            "WHERE VMT.STATUS = 1 AND VOUCHER_TYPE  = '" + VoucherSubTypes.PY + "' AND VOUCHER_SUB_TYPE = '" + VoucherSubTypes.PAY + "'\n" +
                            "ORDER BY VOUCHER_ID,SEQUENCE_NO,ML.GROUP_ID NOT IN(12,13) DESC;";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollpostpayment:
                    {
                        query = "SELECT DATE, PAYROLL_ID, PRNAME, LEDGER_ID, LEDGER, AMOUNT,CASHBANK_LEDGER_ID,CASHBANK_LEDGER,TYPE_ID,PROJECT_ID,NARRATION\n" +
                        "  FROM PAYROLL_FINANCE\n" +
                        " WHERE POST_ID = ?POST_ID;";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollGroupByPosting:
                    {
                        query = "SELECT GROUPID, GROUPNAME, IF(PV.SALARY_GROUP_ID IS NULL, 0, 1) AS IS_PAYROLL_POSTED\n" +
                                    "FROM PRSALARYGROUP\n" +
                                    "LEFT JOIN PAYROLL_VOUCHER PV ON PV.SALARY_GROUP_ID = PRSALARYGROUP.GROUPID AND PV.PAYROLL_ID = ?PAYROLL_ID\n" +
                                    "GROUP BY GROUPID\n" +
                                    "ORDER BY GROUPNAME";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollPostPending:
                    {
                        query = "SELECT PS.PAYROLLID, PC.COMPONENTID, SG.GROUPID, SG.GROUPNAME,\n" +
                             "IF(PC.TYPE=0, " + (int)TransSource.Dr + ", " + (int)TransSource.Cr + ") AS SOURCE,\n" +
                             "PC.TYPE, IF(PC.TYPE=0, '" + TransSource.Dr.ToString() + "', '" + TransSource.Cr.ToString() + "') AS TRANS_MODE, \n" +
                             "PC.COMPONENT, IFNULL(PV.LEDGER_ID, 0) AS LEDGER_ID,\n" +
                             "SUM(CAST(PS.COMPVALUE as DECIMAL)) AS TOTAL_AMOUNT,  SUM(CAST(PS.COMPVALUE as DECIMAL)) AS AMOUNT,\n" + //IF(IFNULL(PV.AMOUNT, 0)=0, SUM(CAST(PS.COMPVALUE as DECIMAL)), IFNULL(PV.AMOUNT, 0) )
                             "(SUM(CAST(PS.COMPVALUE as DECIMAL))  - IFNULL(PV.AMOUNT, 0)) AS BALANCE, IFNULL(PV.VOUCHER_ID,0) AS VOUCHER_ID, IFNULL(VM.VOUCHER_NO,'') AS VOUCHER_NO,\n" +
                             "PC.PROCESS_COMPONENT_TYPE\n" +
                            //"IFNULL(PV.VOUCHER_ID,0) AS VOUCHER_ID, IFNULL(VM.VOUCHER_NO,'') AS VOUCHER_NO\n" +
                             "FROM PRSTAFF PS\n" +
                             "INNER JOIN PRSTAFFGROUP PG ON PG.STAFFID = PS.STAFFID\n" +
                             "INNER JOIN PRSALARYGROUP SG ON SG.GROUPID = PG.GROUPID\n" +
                             "INNER JOIN PRCOMPONENT PC ON PC.COMPONENTID = PS.COMPONENTID\n" +
                             "INNER JOIN PRCOMPMONTH PM ON PM.COMPONENTID = PS.COMPONENTID AND PM.PAYROLLID = PS.PAYROLLID AND PM.SALARYGROUPID = PG.GROUPID\n" +
                             "LEFT JOIN PAYROLL_VOUCHER PV ON PV.PAYROLL_ID = PS.PAYROLLID AND PV.COMPONENT_ID = PS.COMPONENTID AND\n" +
                             " PV.SALARY_GROUP_ID = PG.GROUPID AND PV.SALARY_GROUP_ID IN (?SALARYGROUPID)\n" +
                             "LEFT JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = PV.VOUCHER_ID AND VM.PROJECT_ID IN (?PROJECT_ID) AND STATUS = 1\n" +
                             "WHERE PS.PAYROLLID = ?PAYROLLID  AND PM.SALARYGROUPID IN (?SALARYGROUPID) AND PG.PAYROLLID = ?PAYROLLID AND \n" +
                             "(PC.PROCESS_COMPONENT_TYPE IN (1, 2) OR PC.PAYABLE=1)\n" +
                             "GROUP BY SG.GROUPNAME, PC.COMPONENT ORDER BY SG.GROUPNAME, PC.TYPE;";
                        break;
                    }
                case SQLCommand.Payroll.UpdatePostPaymentDetails:
                    {
                        query = "UPDATE PAYROLL_FINANCE\n" +
                            "   SET DATE=?DATE,\n" +
                            "       PAYROLL_ID=?PAYROLL_ID,\n" +
                            "       PRNAME=?PRNAME,\n" +
                            "       LEDGER_ID=?LEDGER_ID,\n" +
                            "       LEDGER=?LEDGER,\n" +
                            "       AMOUNT=?AMOUNT,\n" +
                            "       CASHBANK_LEDGER_ID=?CASHBANK_LEDGER_ID,\n" +
                            "       CASHBANK_LEDGER=?CASHBANK_LEDGER,\n" +
                            "       PROJECT_ID=?PROJECT_ID,\n" +
                            "       TYPE_ID=?TYPE_ID,\n" +
                            "       NARRATION=?NARRATION\n" +
                            " WHERE POST_ID = ?POST_ID;";
                        break;
                    }
                case SQLCommand.Payroll.FetchVoucherIdByPostId:
                    {
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE CLIENT_REFERENCE_ID = ?POST_ID AND STATUS=1;";
                        break;
                    }
                case SQLCommand.Payroll.DeletePostPaymentDetails:
                    {
                        query = "DELETE FROM PAYROLL_FINANCE WHERE POST_ID=?POST_ID;";
                        break;
                    }
                case SQLCommand.Payroll.DeletePayrollPostByVoucherId:
                    {
                        query = @"DELETE FROM PAYROLL_VOUCHER WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.Payroll.DeletePayrollPostPaymentVouchersByVoucherId:
                    {
                        query = @"DELETE FROM PAYROLL_VOUCHER WHERE VOUCHER_ID=?VOUCHER_ID;
                                  DELETE FROM VOUCHER_CC_TRANS WHERE VOUCHER_ID=?VOUCHER_ID;
                                  DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID;
                                  DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.Payroll.FetchPostVoucherBalanceDetails:
                    {
                        query = "SELECT POST_ID,PRNAME,PAYROLL_ID,TYPE_ID,AMOUNT FROM PAYROLL_FINANCE WHERE PAYROLL_ID=16 AND TYPE_ID=0;";
                        break;
                    }
                case SQLCommand.Payroll.FetchPayrollPostPaymentByVoucherId:
                    {
                        query = "SELECT PV.PAYROLL_ID, GROUP_CONCAT(DISTINCT PV.SALARY_GROUP_ID SEPARATOR  ',') AS SALARY_GROUP_ID, VM.PROJECT_ID, VM.VOUCHER_DATE, VT.LEDGER_ID,\n" +
                                " VT.CHEQUE_NO, VT.MATERIALIZED_ON, VM.NARRATION\n" +
                                " FROM PAYROLL_VOUCHER PV\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = PV.VOUCHER_ID\n" +
                                " INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VT.TRANS_MODE='" + TransactionMode.CR.ToString() + "'\n" +
                                " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID AND ML.GROUP_ID IN (" + (Int32)FixedLedgerGroup.Cash + "," + (Int32)FixedLedgerGroup.BankAccounts + ")\n" +
                                " WHERE VM.STATUS=1 AND PV.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.Payroll.IsPayrollPostPaymentPosted:
                    {
                        query = "SELECT PAYROLL_ID, SALARY_GROUP_ID, COMPONENT_ID, VOUCHER_ID\n" +
                                  "FROM PAYROLL_VOUCHER\n" +
                                  "WHERE PAYROLL_ID =?PAYROLL_ID AND SALARY_GROUP_ID = ?SALARY_GROUP_ID";
                        break;
                    }
                case SQLCommand.Payroll.CheckComponentMappedToGroup:
                    {
                        query = "SELECT COUNT(*)\n" +
                        "  FROM PRCOMPMONTH\n" +
                        " WHERE COMPONENTID = ?COMPONENTID\n" +
                        "   AND SALARYGROUPID = ?SALARYGROUPID AND PAYROLLID=?PAYROLLID;";

                        break;
                    }
                case SQLCommand.Payroll.FetchDecutionComponents:
                    {
                        query = "SELECT COMPONENTID,COMPONENT\n" +
                        "  FROM PRCOMPONENT\n" +
                        " WHERE\n" +
                            //  "       LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2\n" +
                        " TYPE IN (?SALARYGROUPID) AND PAYABLE=1;";
                        break;
                    }
                case SQLCommand.Payroll.FetchProcessedValuesofDeductionComponents:
                    {

                        //query = "SELECT SUM(PRS.COMPVALUE)\n" +
                        //"  FROM PRCOMPONENT PRC\n" +
                        //" INNER JOIN PRSTAFF PRS\n" +
                        //"    ON PRS.COMPONENTID = PRC.COMPONENTID\n" +
                        //" WHERE LENGTH(RELATEDCOMPONENTS) -\n" +
                        //"       LENGTH(REPLACE(RELATEDCOMPONENTS, 'ê', '')) <= 2\n" +
                        //"   AND TYPE IN (?SALARYGROUPID)\n" +
                        //"   AND PRS.PAYROLLID = ?PAYROLLID;";

                        query = "SELECT SUM(PRS.COMPVALUE)\n" +
                                "  FROM PRCOMPONENT PRC\n" +
                                " INNER JOIN PRSTAFF PRS\n" +
                                "    ON PRS.COMPONENTID = PRC.COMPONENTID\n" +
                                " WHERE\n" +
                                " TYPE IN (1) AND PRS.COMPONENTID=?COMPONENTID\n" +
                                "   AND PRS.PAYROLLID = ?PAYROLLID;";

                        break;
                    }
                case SQLCommand.Payroll.FetchsumofPostPaymentAmountByPayrollId:
                    {
                        query = "SELECT SUM(AMOUNT) FROM PAYROLL_FINANCE WHERE PAYROLL_ID =?PAYROLLID;";
                        break;
                    }

                case SQLCommand.Payroll.ValidateSumofPostvoucheramountBypayrollid:
                    {
                        query = "SELECT SUM(AMOUNT) FROM PAYROLL_FINANCE WHERE PAYROLL_ID =?PAYROLLID AND TYPE_ID=0;";
                        break;
                    }

                #endregion

                #region payroll Settings
                case SQLCommand.Payroll.FetchPayrollSetting:
                    {
                        query = "SELECT  SETTING_NAME AS Name,VALUE AS Value FROM MASTER_SETTING WHERE USER_ID=?USER_ID";
                        break;
                    }

                case SQLCommand.Payroll.InsertPayrollSetting:
                    {
                        query = "INSERT INTO MASTER_SETTING (SETTING_NAME, VALUE, USER_ID) VALUES( " +
                               "?SETTING_NAME, ?VALUE, ?USER_ID) ON DUPLICATE KEY UPDATE VALUE=?VALUE";
                        break;
                    }
                #endregion

            }

            return query;
        }
        #endregion User SQL
    }
}


//Before Finetune PR 20/05/2019, 
//# PaySlip
//query = "SELECT D.STAFFID,\n" +
//                                "       TRIM(GROUP_CONCAT(D.EARNINGS ORDER BY D.STAFFID SEPARATOR ' ')) AS EARNINGS,\n" +
//                                "       CASE\n" +
//                                "         WHEN TRIM(GROUP_CONCAT(D.EARNINGS ORDER BY D.STAFFID SEPARATOR ' ')) = '' THEN\n" +
//                                "          0.00\n" +
//                                "         ELSE\n" +
//                                "          SUM(D.EAMOUNT)\n" +
//                                "       END AS EAMOUNT,\n" +
//                                "       TRIM(GROUP_CONCAT(D.DEDUCTIONS ORDER BY D.STAFFID SEPARATOR ' ')) AS DEDUCTIONS,\n" +
//                                "       CASE\n" +
//                                "         WHEN TRIM(GROUP_CONCAT(D.DEDUCTIONS ORDER BY D.STAFFID SEPARATOR ' ')) = '' THEN\n" +
//                                "          0.00\n" +
//                                "         ELSE\n" +
//                                "          SUM(D.DAMOUNT)\n" +
//                                "       END AS DAMOUNT,\n" +
//                                "       TRIM(GROUP_CONCAT(D.TEXTNAME ORDER BY D.STAFFID SEPARATOR ' ')) AS TEXTNAME,\n" +
//                                "       TRIM(GROUP_CONCAT(D.TEXTVALUE ORDER BY D.STAFFID SEPARATOR ' ')) AS TEXTVALUE,\n" +
//                                "       TRIM(GROUP_CONCAT(D.DESIGNATION ORDER BY D.STAFFID SEPARATOR ' ')) AS DESIGNATION,\n" +
//                                "       SUM(D.NETPAY) AS 'NET PAY'\n" +
//                                "  FROM (SELECT T.STAFFID,\n" +
//                                "               PR.COMPONENT AS EARNINGS,\n" +
//                                "               T.AMOUNT AS EAMOUNT,\n" +
//                                "               '' AS DEDUCTIONS,\n" +
//                                "               0 AS DAMOUNT,\n" +
//                                "               @VNO := IF(@VPREV_VALUE = PS.STAFFID, @VNO + 1, 1) AS SNO,\n" +
//                                "               @VPREV_VALUE := PS.STAFFID AS PVAL,\n" +
//                                "               '' AS TEXTNAME,\n" +
//                                "               '' AS TEXTVALUE,\n" +
//                                "               '' AS DESIGNATION,\n" +
//                                "               0 AS NETPAY\n" +
//                                "          FROM PRSTAFF PS\n" +
//                                "         INNER JOIN (SELECT STAFFID, COMPONENTID, COMPVALUE AS AMOUNT\n" +
//                                "                      FROM PRSTAFF,\n" +
//                                "                           (SELECT @VNO := 0) X,\n" +
//                                "                           (SELECT @VPREV_VALUE := 0) Y\n" +
//                                "                     WHERE PAYROLLID = ?PAYROLLID\n" +
//                                "                       AND STAFFID IN (?StaffId)\n" +
//                                "                     ORDER BY STAFFID) AS T\n" +
//                                "            ON T.STAFFID = PS.STAFFID\n" +
//                                "         INNER JOIN PRCOMPONENT PR\n" +
//                                "            ON PR.COMPONENTID = T.COMPONENTID\n" +
//                                "           AND PS.PAYROLLID = ?PAYROLLID\n" +
//                                "         INNER JOIN PRSTAFFGROUP PG\n" +
//                                "            ON PG.STAFFID = PS.STAFFID\n" +
//                                "         WHERE PR.TYPE IN (0)\n" +
//                                "       { AND PG.GROUPID IN (?GroupId) } \n" +
//                                "  AND LENGTH(RELATEDCOMPONENTS)-LENGTH(REPLACE(RELATEDCOMPONENTS,'ê',''))<=2 -- AND PR.COMPONENT NOT IN ('GROSS WAGES', 'NETPAY')\n" +
//                                "         GROUP BY STAFFID, COMPONENT\n" +
//                                "        UNION ALL\n" +
//                                "        SELECT T.STAFFID,\n" +
//                                "               '' AS EARNING,\n" +
//                                "               0 AS EAMOUNT,\n" +
//                                "               PR.COMPONENT AS DEDUCTION,\n" +
//                                "               T.AMOUNT AS DAMOUNT,\n" +
//                                "               @RNO := IF(@PREV_VALUE = PS.STAFFID, @RNO + 1, 1) AS SNO,\n" +
//                                "               @PREV_VALUE := PS.STAFFID AS PVAL,\n" +
//                                "               '' AS TEXTNAME,\n" +
//                                "               '' AS TEXTVALUE,\n" +
//                                "               '' AS DESIGNATION,\n" +
//                                "               0 AS NETPAY\n" +
//                                "          FROM PRSTAFF PS\n" +
//                                "         INNER JOIN (SELECT STAFFID, COMPONENTID, COMPVALUE AS AMOUNT\n" +
//                                "                       FROM PRSTAFF,\n" +
//                                "                            (SELECT @RNO := 0) X,\n" +
//                                "                            (SELECT @PREV_VALUE := 0) Y\n" +
//                                "                     WHERE PAYROLLID = ?PAYROLLID\n" +
//                                "                       AND STAFFID IN (?StaffId)\n" +
//                                "                      ORDER BY STAFFID) AS T\n" +
//                                "            ON T.STAFFID = PS.STAFFID\n" +
//                                "         INNER JOIN PRCOMPONENT PR\n" +
//                                "            ON PR.COMPONENTID = T.COMPONENTID\n" +
//                                "           AND PS.PAYROLLID = ?PAYROLLID\n" +
//                                "         INNER JOIN PRSTAFFGROUP PG\n" +
//                                "            ON PG.STAFFID = PS.STAFFID\n" +
//                                "         WHERE PR.TYPE IN (1)\n" +
//                                "       { AND PG.GROUPID IN (?GroupId) } \n" +
//                                "    AND LENGTH(RELATEDCOMPONENTS)-LENGTH(REPLACE(RELATEDCOMPONENTS,'ê',''))<=2 --   AND PR.COMPONENT NOT IN ('DEDUCTIONS', 'PF WAGES')\n" +
//                                "         GROUP BY STAFFID, COMPONENT\n" +
//                                "        UNION\n" +
//                                "        SELECT T.STAFFID,\n" +
//                                "               '' AS EARNINGS,\n" +
//                                "               0 AS EAMOUNT,\n" +
//                                "               '' AS DEDUCTION,\n" +
//                                "               0 AS DAMOUNT,\n" +
//                                "               @DNO := IF(@DPREV_VALUE = PS.STAFFID, @DNO + 1, 1) AS SNO,\n" +
//                                "               @DPREV_VALUE := PS.STAFFID AS PVAL,\n" +
//                                "               PR.COMPONENT AS TEXTNAME,\n" +
//                                "               T.AMOUNT AS TEXTVALUE,\n" +
//                                "               '' AS DESIGNATION,\n" +
//                                "               0 AS NETPAY\n" +
//                                "          FROM PRSTAFF PS\n" +
//                                "         INNER JOIN (SELECT STAFFID, COMPONENTID, COMPVALUE AS AMOUNT\n" +
//                                "                       FROM PRSTAFF,\n" +
//                                "                            (SELECT @DNO := 0) X,\n" +
//                                "                            (SELECT @DPREV_VALUE := 0) Y\n" +
//                                "                     WHERE PAYROLLID = ?PAYROLLID\n" +
//                                "                       AND STAFFID IN (?StaffId)\n" +
//                                "                      ORDER BY STAFFID) AS T\n" +
//                                "            ON T.STAFFID = PS.STAFFID\n" +
//                                "         INNER JOIN PRCOMPONENT PR\n" +
//                                "            ON PR.COMPONENTID = T.COMPONENTID\n" +
//                                "           AND PS.PAYROLLID = ?PAYROLLID\n" +
//                                "         INNER JOIN PRSTAFFGROUP PG\n" +
//                                "            ON PG.STAFFID = PS.STAFFID\n" +
//                                "         WHERE PR.TYPE IN (2)\n" +
//                                "       { AND PG.GROUPID IN (?GroupId) } \n" +
//                                "           AND PR.COMPONENT NOT IN ('DESIGNATION')\n" +
//                                "         GROUP BY STAFFID, COMPONENT\n" +
//                                "        UNION ALL\n" +
//                                "        SELECT T.STAFFID,\n" +
//                                "               '' AS EARNINGS,\n" +
//                                "               0 AS EAMOUNT,\n" +
//                                "               '' AS DEDUCTION,\n" +
//                                "               0 AS DAMOUNT,\n" +
//                                "               @QNO := IF(@QPREV_VALUE = PS.STAFFID, @QNO + 1, 1) AS SNO,\n" +
//                                "               @QPREV_VALUE := PS.STAFFID AS PVAL,\n" +
//                                "               '' AS TEXTNAME,\n" +
//                                "               '' AS TEXTVALUE,\n" +
//                                "               T.AMOUNT AS DESIGNATION,\n" +
//                                "               0 AS NETPAY\n" +
//                                "          FROM PRSTAFF PS\n" +
//                                "         INNER JOIN (SELECT STAFFID, COMPONENTID, COMPVALUE AS AMOUNT\n" +
//                                "                       FROM PRSTAFF,\n" +
//                                "                            (SELECT @QNO := 0) X,\n" +
//                                "                            (SELECT @QPREV_VALUE := 0) Y\n" +
//                                "                     WHERE PAYROLLID = ?PAYROLLID\n" +
//                                "                       AND STAFFID IN (?StaffId)\n" +
//                                "                      ORDER BY STAFFID) AS T\n" +
//                                "            ON T.STAFFID = PS.STAFFID\n" +
//                                "         INNER JOIN PRCOMPONENT PR\n" +
//                                "            ON PR.COMPONENTID = T.COMPONENTID\n" +
//                                "           AND PS.PAYROLLID = ?PAYROLLID\n" +
//                                "         INNER JOIN PRSTAFFGROUP PG\n" +
//                                "            ON PG.STAFFID = PS.STAFFID\n" +
//                                "         WHERE PR.TYPE IN (2)\n" +
//                                "       { AND PG.GROUPID IN (?GroupId) } \n" +
//                                "           AND PR.COMPONENT NOT IN ('NAME')\n" +
//                                "         GROUP BY STAFFID, COMPONENT\n" +
//                                "        UNION ALL\n" +
//                                "        SELECT T.STAFFID,\n" +
//                                "               '' AS EARNINGS,\n" +
//                                "               0 AS EAMOUNT,\n" +
//                                "               '' AS DEDUCTION,\n" +
//                                "               0 AS DAMOUNT,\n" +
//                                "               @NNO := IF(@NPREV_VALUE = PS.STAFFID, @NNO + 1, 1) AS SNO,\n" +
//                                "               @NPREV_VALUE := PS.STAFFID AS PVAL,\n" +
//                                "               '' AS TEXTNAME,\n" +
//                                "               '' AS TEXTVALUE,\n" +
//                                "               '' AS DESIGNATION,\n" +
//                                "               T.AMOUNT AS NETPAY\n" +
//                                "          FROM PRSTAFF PS\n" +
//                                "         INNER JOIN (SELECT STAFFID, COMPONENTID, COMPVALUE AS AMOUNT\n" +
//                                "                       FROM PRSTAFF,\n" +
//                                "                            (SELECT @NNO := 0) X,\n" +
//                                "                            (SELECT @NPREV_VALUE := 0) Y\n" +
//                                "                     WHERE PAYROLLID = ?PAYROLLID\n" +
//                                "                       AND STAFFID IN (?StaffId)\n" +
//                                "                      ORDER BY STAFFID) AS T\n" +
//                                "            ON T.STAFFID = PS.STAFFID\n" +
//                                "         INNER JOIN PRCOMPONENT PR\n" +
//                                "            ON PR.COMPONENTID = T.COMPONENTID\n" +
//                                "           AND PS.PAYROLLID = ?PAYROLLID\n" +
//                                "         INNER JOIN PRSTAFFGROUP PG\n" +
//                                "            ON PG.STAFFID = PS.STAFFID\n" +
//                                "         WHERE PR.TYPE IN (0)\n" +
//                                "       { AND PG.GROUPID IN (?GroupId) } \n" +
//                                "           AND PR.COMPONENT IN ('NETPAY')\n" +
//                                "         GROUP BY STAFFID, COMPONENT\n" +
//                                "        ) AS D\n" +
//                                " GROUP BY D.STAFFID, D.SNO";