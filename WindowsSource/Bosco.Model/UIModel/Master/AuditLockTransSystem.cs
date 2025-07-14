using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.Dsync;

namespace Bosco.Model.UIModel.Master
{
    public class AuditLockTransSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Audit Type Properties
        public int LockTypeId { get; set; }
        public string LockType { get; set; }
        #endregion

        #region Audit Trans Type Properties
        public int LockTransId { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime VoucherDateTo { get; set; }
        public string Password { get; set; }
        public string Reason { get; set; }
        public string PasswordHint { get; set; }
        public Int32 LockByPortal { get; set; }
        public Int32 BranchId { get; set; }
        #endregion

        #region Constructors
        public AuditLockTransSystem()
        {

        }
        public AuditLockTransSystem(int LockTransId)
        {
            this.LockTransId = LockTransId;
            AssignAuditTrans();
        }
        #endregion

        #region Audit Type Methods
        public ResultArgs SaveAuditType()
        {
            using (DataManager dataManager = new DataManager(LockTypeId == 0 ? SQLCommand.AuditLockTrans.AddAuditType : SQLCommand.AuditLockTrans.UpdateAuditType))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockType.LOCK_TYPE_IDColumn, LockTypeId, true);
                dataManager.Parameters.Add(this.AppSchema.AuditLockType.LOCK_TYPEColumn, LockType);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAduitType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.DeleteAuditType))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockType.LOCK_TYPE_IDColumn, LockTypeId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchAuditType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditType))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockType.LOCK_TYPE_IDColumn, LockTypeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllAuditType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAllAuditType))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public Int32 GetAuditTypeIdByLockType()
        {
            Int32 locktypeid = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditTypeByType))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockType.LOCK_TYPEColumn, LockType);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return locktypeid = resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region Audit Trans Methods

        public ResultArgs SaveAuditTrans()
        {
            using (DataManager dataManager = new DataManager(LockTransId == 0 ? SQLCommand.AuditLockTrans.AddAuditTrans : SQLCommand.AuditLockTrans.UpdateAuditTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_TRANS_IDColumn, LockTransId);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_TYPE_IDColumn, LockTypeId);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.DATE_TOColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PASSWORDColumn, Password);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.REASONColumn, Reason);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PASSWORD_HINTColumn, PasswordHint);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_BY_PORTALColumn, LockByPortal);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAduitTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.DeleteAuditTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_TRANS_IDColumn, LockTransId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAduitTransByProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.DeleteAuditTransByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAduitTransByLockbyPortal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.DeleteAuditTransByLockbyPortal))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_BY_PORTALColumn, LockByPortal);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchAuditTransType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_TRANS_IDColumn, LockTransId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs AssignAuditTrans()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = FetchAuditTransType();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        LockTransId = this.NumberSet.ToInteger(dr[this.AppSchema.AuditLockTransType.LOCK_TRANS_IDColumn.ColumnName].ToString());
                        LockTypeId = this.NumberSet.ToInteger(dr[this.AppSchema.AuditLockTransType.LOCK_TYPE_IDColumn.ColumnName].ToString());
                        ProjectId = this.NumberSet.ToInteger(dr[this.AppSchema.AuditLockTransType.PROJECT_IDColumn.ColumnName].ToString());
                        DateFrom = this.DateSet.ToDate(dr[this.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        DateTo = this.DateSet.ToDate(dr[this.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                        Password = dr[this.AppSchema.AuditLockTransType.PASSWORDColumn.ColumnName].ToString();
                        Reason = dr[this.AppSchema.AuditLockTransType.REASONColumn.ColumnName].ToString();
                        PasswordHint = dr[this.AppSchema.AuditLockTransType.PASSWORD_HINTColumn.ColumnName].ToString();
                        LockByPortal = this.NumberSet.ToInteger(dr[this.AppSchema.AuditLockTransType.LOCK_BY_PORTALColumn.ColumnName].ToString());
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchAllAuditTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAllAuditTrans))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAuditLockDetailsForProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditLockDetailsForProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAuditLockDetailsForProjectAndDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditLockDetailsForProjectAndDate))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAuditDetailByProjectDateRange()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditLockDetailByProjectDateRange))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.DATE_TOColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public Int32 FetchAuditDetailIdByProjectDateRange()
        {
            //bool isExists = false;
            Int32 Existinglockid = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditLockDetailIdByProjectDateRange))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.DATE_TOColumn, DateTo);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                {
                    Existinglockid = resultArgs.DataSource.Sclar.ToInteger;
                    //isExists = true;
                }
            }
            return Existinglockid;
        }

        public int GetProjectId(string projectName)
        {
            using (ImportMasterSystem importSystem = new ImportMasterSystem())
            {
                importSystem.ProjectName = projectName;
                return importSystem.GetMasterId(DataSync.Project);
            }
        }
        
        public bool IsValidPassword()
        {
            bool isValidPassword = false;
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.IsValidPassword))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PASSWORDColumn, Password);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_TRANS_IDColumn, LockTransId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                {
                    isValidPassword = true;
                }
            }
            return isValidPassword;
        }

        public bool ResetPassword()
        {
            bool isResetPassword = false;
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.ResetPassword))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PASSWORDColumn, Password);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_TRANS_IDColumn, LockTransId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs != null && resultArgs.Success)
                {
                    isResetPassword = true;
                }
            }
            return isResetPassword;
        }

        public int ValidatePasswordHint()
        {
            int Hint = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.ValidatePasswordHint))
            {
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.PASSWORD_HINTColumn, PasswordHint);
                dataManager.Parameters.Add(this.AppSchema.AuditLockTransType.LOCK_TRANS_IDColumn, LockTransId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                if (resultArgs != null && resultArgs.Success)
                {
                    Hint = resultArgs.DataSource.Sclar.ToInteger;
                }
                return Hint;
            }
        }

        public ResultArgs FetchLockMastersInVoucherEntry(Id SourceMasterType, DataTable dtEntyMaster, DateTime VoucherDate)
        {
            ResultArgs result = new ResultArgs();
            result = FetchLockMastersInVoucherEntry(SourceMasterType, 0, dtEntyMaster, VoucherDate);
            return result;
        }

        public ResultArgs FetchLockMastersInVoucherEntry(Id SourceMasterType, Int32 SourceMasterId, DataTable dtEntyMaster, DateTime VoucherDate)
        {
            ResultArgs result = new ResultArgs();
            int sourcemastertype = 0;
            if (SourceMasterType == Id.Ledger)
            {
                sourcemastertype = 0;
            }
            else if (SourceMasterType == Id.CostCentre)
            {
                sourcemastertype = 1;
            }

            using (DataManager dataManager = new DataManager(SQLCommand.AuditLockTrans.FetchLockMastersInVoucherEntry))
            {
                dataManager.Parameters.Add(this.AppSchema.LockVoucherEntry.MASTER_SOURCE_TYPEColumn, sourcemastertype);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                if (SourceMasterId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LockVoucherEntry.MASTER_SOURCE_IDColumn, SourceMasterId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }
            return result;
        }

        public ResultArgs EnforceLockMastersInVoucherEntry(Id SourceMasterType, DataTable dtEntyMaster, DateTime VoucherDate)
        {
            ResultArgs result = new ResultArgs();
            DataTable dtRtn = dtEntyMaster.DefaultView.ToTable();
            try
            {
                result = EnforceLockMastersInVoucherEntry(SourceMasterType, 0, dtEntyMaster, VoucherDate);
            }
            catch (Exception err)
            {
                result.DataSource.Data = dtEntyMaster.DefaultView.ToTable();
                result.Message = err.Message;
            }
            return result;
        }

        public ResultArgs EnforceLockMastersInVoucherEntry(Id SourceMasterType, Int32 SourceMasterId, DataTable dtEntyMaster, DateTime VoucherDate)
        {
            ResultArgs result = new ResultArgs();
            DataTable dtRtn = dtEntyMaster.DefaultView.ToTable();
            try
            {
                using (AuditLockTransSystem auditlocksystem = new AuditLockTransSystem())
                {
                    result = auditlocksystem.FetchLockMastersInVoucherEntry(SourceMasterType, SourceMasterId, dtEntyMaster, VoucherDate);

                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        string rtnids = string.Empty;
                        foreach (DataRow dr in result.DataSource.Table.Rows)
                        {
                            rtnids += dr[auditlocksystem.AppSchema.LockVoucherEntry.MASTER_SOURCE_IDColumn.ColumnName].ToString() + ",";
                        }
                        rtnids = rtnids.TrimEnd(',');

                        if (!string.IsNullOrEmpty(rtnids))
                        {
                            dtEntyMaster.DefaultView.RowFilter = auditlocksystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " NOT IN (" + rtnids + ")";
                            result.DataSource.Data = dtEntyMaster.DefaultView.ToTable();
                            result.Success = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            finally
            {
                result.DataSource.Data = dtRtn;
            }
            return result;
        }
        #endregion
    }
}
