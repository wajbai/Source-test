using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Runtime.InteropServices;

namespace Bosco.Model.UIModel
{
    public class ProjectSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ProjectSystem()
        {
        }

        public ProjectSystem(int ProjectId)
        {
            FillProjectProperties(ProjectId);
        }
        #endregion

        #region Project Properties
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public DateTime AccountDate { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime Closed_On { get; set; }
        public string ClosedOn { get; set; }
        public Int32 ClosedBy { get; set; }
        public string Description { get; set; }
        public int ProjectVoucherId { get; set; }
        public int VoucherId { get; set; }
        public string Notes { get; set; }
        public int ProjectCategroyId { get; set; }
        public DataTable dtProjectVouchers { get; set; }
        public string VoucherProjectId { get; set; }
        public int MapProjectId { get; set; }
        public int LegalEntityId { get; set; }
        public int ContributionId { get; set; }
        public DataTable dtMapLedger { get; set; }
        public DataTable dtLedgerAmountMadeZero { get; set; }

        /// <summary>
        /// On 28/06/2018, This property is used to skip projects which is closed on or equal to this date
        /// </summary>
        public string ProjectClosedDate { get; set; }

        public DataTable dtAllProjectLedgerApplicable { get; set; }
        
        #endregion

        #region Methods

        public ResultArgs FetchDivision()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchDivision))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchVoucherTypes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchProjectCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectCodes))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchProjectByProjectCode(string Projectcode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectnameByProjectCode))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, Projectcode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchSocietyName()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchSocietyNames))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDefaultProjectVouchers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchDefaultProjectVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        //public ResultArgs FetchVoucherTypes(string VoucherId)
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchAvailableVouchers))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs FetchProjectlistDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
            {
                if (!IsFullRightsReservedUser)
                {
                    // dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs SavePJDetails()
        {
            using (DataManager projectdataManager = new DataManager())
            {
                resultArgs = SaveProjectDetails(projectdataManager);
                if (resultArgs.Success & resultArgs.RowsAffected > 0)
                {
                    MapProjectId = MapProjectId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapProjectId;
                }
            }
            return resultArgs;

        }
        public ResultArgs SaveProject()
        {
            using (DataManager projectdataManager = new DataManager())
            {
                projectdataManager.BeginTransaction();
                resultArgs = SaveProjectDetails(projectdataManager);
                if (resultArgs.Success & resultArgs.RowsAffected > 0)
                {
                    MapProjectId = MapProjectId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapProjectId;
                    resultArgs = MapProject(projectdataManager);
                }
                projectdataManager.EndTransaction();
            }
            return resultArgs;
        }


        private ResultArgs MapProject(DataManager dataManagers)
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.ProjectId = MapProjectId;
                mappingSystem.OpeningBalanceDate = BookBeginFrom;
                mappingSystem.dtLedgerIDCollection = dtMapLedger;
                mappingSystem.dtMovedLedgerIDCollection = dtLedgerAmountMadeZero;
                mappingSystem.dtProjectLedgerApplicableDetails = dtAllProjectLedgerApplicable;
                if (dtMapLedger.Rows.Count > 0 && dtMapLedger != null)
                {
                    resultArgs = mappingSystem.AccountMappingLedger(dataManagers);
                }
            }
            return resultArgs;
        }
        public ResultArgs SaveProjectDetails(DataManager projectDataManager)
        {
            List<int> voucherTypes = new List<int>();
            voucherTypes.Add((int)DefaultVoucherTypes.Receipt);
            voucherTypes.Add((int)DefaultVoucherTypes.Payment);
            voucherTypes.Add((int)DefaultVoucherTypes.Contra);
            voucherTypes.Add((int)DefaultVoucherTypes.Journal);

            using (DataManager dataManager = new DataManager(ProjectId == 0 ? SQLCommand.Project.Add : SQLCommand.Project.Update))
            {
                dataManager.Database = projectDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId, true);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, ProjectCode);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                dataManager.Parameters.Add(this.AppSchema.Project.CUSTOMERIDColumn, LegalEntityId);
                dataManager.Parameters.Add(this.AppSchema.Project.CONTRIBUTION_IDColumn, ContributionId);
                if (AccountDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.ACCOUNT_DATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.ACCOUNT_DATEColumn, AccountDate);
                }
                dataManager.Parameters.Add(this.AppSchema.Project.DESCRIPTIONColumn, Description);
                if (StartedOn == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, StartedOn);
                }
                if (Closed_On == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, Closed_On);
                }
                dataManager.Parameters.Add(this.AppSchema.Project.DIVISION_IDColumn, DivisionId);
                dataManager.Parameters.Add(this.AppSchema.Project.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CATEGORY_IDColumn, ProjectCategroyId);
                dataManager.Parameters.Add(this.AppSchema.Project.CLOSED_BYColumn, ClosedBy);

                resultArgs = dataManager.UpdateData();
                if (ProjectId == 0)
                {
                    if (resultArgs.Success)
                    {
                        ProjectId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        this.ProjectId = ProjectId == 0 ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : ProjectId;

                        for (int i = 1; i <= voucherTypes.Count; i++)
                        {
                            VoucherId = i;
                            resultArgs = SaveProjectVoucherDetails(dataManager);
                            if (!resultArgs.Success)
                                break;
                        }
                    }
                }
                return resultArgs;
            }
        }

        public ResultArgs SaveProjectVoucherDetails(DataManager voucherDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.AddProjectVouchers))
            {
                dataManager.Database = voucherDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateProjectClosedDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.UpdateClosedDate))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                if (Closed_On == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, Closed_On);
                }
                dataManager.Parameters.Add(this.AppSchema.Project.CLOSED_BYColumn, ClosedBy);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs MapProjectVoucher(int ProjectId, int VoucherTypeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.AddProjectVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.VOUCHER_IDColumn, VoucherTypeId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteProjectDetails()
        {
            using (DataManager deletedataManager = new DataManager())
            {
                resultArgs = DeleteVoucherStatus(deletedataManager);
                if (resultArgs.Success)
                {
                    resultArgs = DeleteProjectLedger(deletedataManager);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteVouchersbyProject(deletedataManager);
                        if (resultArgs.Success)
                        {
                            using (MappingSystem mappingsystem = new MappingSystem())
                            {
                                resultArgs = mappingsystem.DeleteProjectLedgerApplicableByProject(ProjectId, null);
                                if (resultArgs.Success)
                                {
                                    using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProject))
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                                        resultArgs = dataManager.UpdateData();
                                    }
                                    if (resultArgs.Success)
                                    {
                                        using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectLedgerBalance))
                                        {
                                            dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                                            resultArgs = dataManager.UpdateData();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjects))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }

                if (!string.IsNullOrEmpty(ProjectClosedDate))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ProjectClosedDate);
                }

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsIntegration()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectsIntegration))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }

                if (!string.IsNullOrEmpty(ProjectClosedDate))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ProjectClosedDate);
                }

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVouchers()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.Project.FetchVouchers))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManger.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FillProjectProperties(int ProjectId)
        {
            resultArgs = FetchProjectDetailsById(ProjectId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                ProjectCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_CODEColumn.ColumnName].ToString();
                ProjectName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                ProjectCategroyId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_CATEGORY_IDColumn.ColumnName].ToString());
                DivisionId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Division.DIVISION_IDColumn.ColumnName].ToString());
                DivisionName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Division.DIVISIONColumn.ColumnName].ToString();
                LegalEntityId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.CUSTOMERIDColumn.ColumnName].ToString());
                ContributionId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.CONTRIBUTION_IDColumn.ColumnName].ToString());
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.ACCOUNT_DATEColumn.ColumnName] != DBNull.Value)
                {
                    AccountDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.ACCOUNT_DATEColumn.ColumnName].ToString(), false);
                }
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DATE_STARTEDColumn.ColumnName] != DBNull.Value)
                {
                    StartedOn = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false);
                }
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName] != DBNull.Value)
                {
                    Closed_On = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString(), false);
                }
                Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DESCRIPTIONColumn.ColumnName].ToString();
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.NOTESColumn.ColumnName].ToString();
                ClosedBy = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.CLOSED_BYColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        private ResultArgs FetchProjectDetailsById(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AvailableVouchers(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.AvailableVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedProjectVouchers(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchSelectedProjectVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs ProjectVouchers(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.ProjectVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteProjectVouchers(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgersForProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchLedgers))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataSet LoadProjectsDetails()
        {
            DataSet ds = new DataSet();
            resultArgs = FetchProjects();
            if (resultArgs.Success)
            {

                resultArgs.DataSource.Table.TableName = "Project";
                ds.Tables.Add(resultArgs.DataSource.Table);
                resultArgs = FetchLedgersForProject();
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Table.TableName = "Ledger";
                    ds.Tables.Add(resultArgs.DataSource.Table);
                    resultArgs = FetchVouchers();
                    if (resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "Voucher";
                        ds.Tables.Add(resultArgs.DataSource.Table);

                        ds.Relations.Add(ds.Tables[1].TableName, ds.Tables[0].Columns[this.AppSchema.Project.PROJECT_IDColumn.ToString()], ds.Tables[1].Columns[this.AppSchema.Project.PROJECT_IDColumn.ToString()]);
                        ds.Relations.Add(ds.Tables[2].TableName, ds.Tables[0].Columns[this.AppSchema.Project.PROJECT_IDColumn.ToString()], ds.Tables[2].Columns[this.AppSchema.ProjectVoucher.PROJECT_IDColumn.ToString()]);
                    }
                }
            }
            return ds;
        }

        public ResultArgs FetchProjectCategroy()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.ProjectCategory))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadAllLedgerByProId(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.LoadAllLedgerByProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDefaultVouchers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchDefaultVouchers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherByProjectId(int ProjectId, string VoucherType, Int32 voucherdefinitionid = 0)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchVoucherDetailsByProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, VoucherType);
                if (voucherdefinitionid > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, voucherdefinitionid);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs FetchRecentProject(string UserId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchRecentProject))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, UserId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int CheckProjectExits()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckProjectExist))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteVoucherStatus(DataManager deleteDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteVoucher))
            {
                dataManager.Database = deleteDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success)
                {
                    if (dtProjectVouchers != null && dtProjectVouchers.Rows.Count != 0)
                    {
                        foreach (DataRow drProject in dtProjectVouchers.Rows)
                        {
                            VoucherId = drProject[AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] != null ? NumberSet.ToInteger(drProject[AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                            resultArgs = SaveProjectVoucherDetails(dataManager);
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteProjectLedger(DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectLedger))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int CheckLedgerProjectExist()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckProjectExist))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchProjectsDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveProjectVouchers()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                DeleteVoucherStatus(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public double CheckLedgerBalance()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.Project.CheckLedgerBalanceForProject))
            {
                dataManger.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManger.FetchData(DataSource.Scalar);
            }
            return NumberSet.ToDouble(resultArgs.DataSource.Data.ToString());
        }
        public ResultArgs UpdateImportMasterProjects(int ProjectId, string MProjectName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.UpdateImportMasterProjectNames))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, MProjectName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs FetchTansDeatilsByProjectId(int Projectid, int PurposeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchTransactionDeatilsByProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, Projectid);
                dataManager.Parameters.Add(this.AppSchema.Project.CONTRIBUTION_IDColumn, PurposeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectIdByProjectName(string ProjectName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectIdByProjectName))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherIdByProjectId(DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchDeletedVouchersByProject))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        private ResultArgs DeleteVoucherMasterByVoucherId(string VoucherId, DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectMasterVoucher))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        private ResultArgs DeleteVoucherTransByVoucherId(string VoucherId, DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectTransVoucher))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteCostCentreByVoucherId(string VoucherId, DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectTransCostCentre))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteRenewalByVoucherId(string VoucherId, DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectTransRenewal))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteFDAccountByVoucherId(string VoucherId, DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectTransFDAccount))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        // if Cost Centre is exists not able to delete the voucher master trans so only voucher Trans are deleted, Not able to delete the Project .... Chinna

        private ResultArgs DeleteVouchersbyProject(DataManager dataProjectLedger)
        {
            resultArgs = FetchVoucherIdByProjectId(dataProjectLedger);
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataTable dtVouhers = resultArgs.DataSource.Table;
                if (dtVouhers != null && dtVouhers.Rows.Count > 0)
                {
                    string tmpvid = string.Empty;
                    foreach (DataRow drvid in dtVouhers.Rows)
                    {
                        tmpvid += drvid[this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] + ','.ToString();
                    }
                    tmpvid = tmpvid.TrimEnd(',');
                    if (!string.IsNullOrEmpty(tmpvid))
                    {
                        resultArgs = DeleteRenewalByVoucherId(tmpvid, dataProjectLedger);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = DeleteFDAccountByVoucherId(tmpvid, dataProjectLedger);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs = DeleteCostCentreByVoucherId(tmpvid, dataProjectLedger);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = DeleteVoucherTransByVoucherId(tmpvid, dataProjectLedger);
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        resultArgs = DeleteVoucherMasterByVoucherId(tmpvid, dataProjectLedger);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }
        #endregion

    }
}
