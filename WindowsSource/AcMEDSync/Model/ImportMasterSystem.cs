using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.DAO.Schema;


namespace AcMEDSync.Model
{
    public class ImportMasterSystem : DsyncSystemBase
    {

        #region Decelaration
        ResultArgs resultArgs = null;
        CommonMethod common = new CommonMethod();
        private const string LEDGER_TABLE_NAME = "Ledger";
        private const string LEGAL_ENTITY_TABLE_NAME = "LegalEntity";
        private const string PROJECT_CATEGORY_TABLE_NAME = "ProjectCatogory";
        private const string PROJECT_TABLE_NAME = "Project";
        private const string FCPURPOSE_TABLE_NAME = "Purposes";
        private const string LEDGERGROUP_TABLE_NAME = "LedgerGroup";
        #endregion

        #region Legal Entity Properties
        private string InsName { get; set; }
        private string SocName { get; set; }
        private DataTable dtLegalEntity { get; set; }
        #endregion

        #region Project Category Properties
        private string ProjectCategoryName { get; set; }
        private DataTable dtProjectCategory { get; set; }
        #endregion

        #region FCPurpose Properties
        private string FCPurpose { get; set; }
        private string FCCode { get; set; }
        private DataTable dtFCPurpose { get; set; }
        #endregion

        #region Project Properties
        private string ProjectName { get; set; }
        private string ProjectCode { get; set; }
        private int ProjectId { get; set; }
        private DataTable dtProjects { get; set; }
        #endregion

        #region Leger Group Properties
        public string LedgerGroup { get; set; }
        public string ParentGroup { get; set; }
        public string Nature { get; set; }
        public string MainGroup { get; set; }
        private DataTable dtLedgerGroup { get; set; }
        #endregion

        #region Ledger Properties
        private string LedgerName { get; set; }
        private DataTable dtLedger { get; set; }
        #endregion

        #region Public Method
        /// <summary>
        /// Convert the XML file to DataSet to import head office Masters to branch office database.
        /// </summary>
        /// <param name="HeadOfficeXML"></param>
        /// <returns></returns>
        public ResultArgs ImportHeadOfficeMasters(string xmlFile)
        {
            try
            {
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.DATAFROM_HEADOFFICE_BRANCHOFFICE));
                resultArgs = common.ValidateXml(xmlFile);
                if (resultArgs.Success)
                {
                    DataSet dsReadXML = XMLConverter.ConvertXMLToDataSet(xmlFile);
                    if (dsReadXML != null && dsReadXML.Tables.Count != 0)
                    {
                        resultArgs = ImportMasters(dsReadXML);
                    }
                }
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.SUCCESS_DATAFROM_HEADOFFICE_BRANCHOFFICE));
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import All the master details to branch office from head office
        /// </summary>
        /// <param name="dsReadXML">This XML file contains all these tables (HeadOffice,Legal Entity,Project Category,Project,Ledger,FCPurpose)</param>
        /// <returns></returns>
        public ResultArgs ImportMasters(DataSet dsReadXML)
        {
            try
            {
                resultArgs = common.ValidateDataSet(dsReadXML);
                if (resultArgs != null && resultArgs.Success)
                {
                    if (dsReadXML != null && dsReadXML.Tables.Count != 0)
                    {
                        AssignDataTable(dsReadXML);
                        using (DataManager dataManager = new DataManager())
                        {
                            dataManager.BeginTransaction();
                            resultArgs = ImportLedgerGroupDetails(dataManager);
                            if (resultArgs.Success)
                            {
                                resultArgs = ImportLedgerDetails(dataManager);
                                if (resultArgs.Success)
                                {
                                    resultArgs = ImportLegalEntity(dataManager);
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = ImportProjectCategory(dataManager);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = ImportProject(dataManager);
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = ImportFCPurpose(dataManager);
                                            }
                                        }
                                    }
                                }
                            }
                            dataManager.EndTransaction();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Get Master Id Based on the given values.
        /// </summary>
        /// <param name="enumMasters"></param>
        /// <returns></returns>
        public int GetMasterId(DataSync enumMasters)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.HOSQL;
                    switch (enumMasters)
                    {
                        case DataSync.LegalEntity:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetLegalEntityId;
                                dataManager.Parameters.Add(this.AppSchema.LegalEntity.SOCIETYNAMEColumn, SocName);
                                break;
                            }
                        case DataSync.ProjectCategory:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetProjectCategoryId;
                                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategoryName);
                                break;
                            }
                        case DataSync.LedgerGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetLedgerGroupId;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                                break;
                            }
                        case DataSync.ParentGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetParentId;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, ParentGroup);
                                break;
                            }
                        case DataSync.Nature:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetNatureId;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATUREColumn, Nature);
                                break;
                            }
                        case DataSync.MainGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetMainParentId;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, MainGroup);
                                break;
                            }
                        case DataSync.Project:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetProjectId;
                                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                                break;
                            }
                        case DataSync.Ledger:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetLedgerId;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                                break;
                            }
                        case DataSync.FCPurpose:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetFCPurposeId;
                                dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, FCPurpose);
                                break;
                            }
                    }
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }

            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger != 0 ? resultArgs.DataSource.Sclar.ToInteger : 0;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Import Legal Entity from head office to branch office.
        /// </summary>
        /// <param name="dataLegalEntity"></param>
        /// <param name="dtLegalEntity">DataTable contains the values of Legal Entity</param>
        /// <returns></returns>
        private ResultArgs ImportLegalEntity(DataManager dataLegalEntity)
        {
            DataTable dtLegalEntitySource = new DataTable();
            try
            {
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.IMPORT_LEGALENTITY_HEADOFFICE_BRACHOFFICE));
                if (dtLegalEntity != null && dtLegalEntity.Rows.Count != 0)
                {
                    foreach (DataRow drLegalEntity in dtLegalEntity.Rows)
                    {
                        using (DataManager dataManagerLegalEntity = new DataManager(SQLCommand.ImportMaster.AddLegalEntity, SQLAdapterType.HOSQL))
                        {
                            dataManagerLegalEntity.Database = dataLegalEntity.Database;
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.INSTITUTENAMEColumn, InsName = drLegalEntity[AppSchema.LegalEntity.INSTITUTENAMEColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.SOCIETYNAMEColumn, SocName = drLegalEntity[AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.CONTACTPERSONColumn, drLegalEntity[AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.ADDRESSColumn, drLegalEntity[AppSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.PLACEColumn, drLegalEntity[AppSchema.LegalEntity.PLACEColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.PHONEColumn, drLegalEntity[AppSchema.LegalEntity.PHONEColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.FAXColumn, drLegalEntity[AppSchema.LegalEntity.FAXColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.COUNTRYColumn, drLegalEntity[AppSchema.LegalEntity.COUNTRYColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.A12NOColumn, drLegalEntity[AppSchema.LegalEntity.A12NOColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.GIRNOColumn, drLegalEntity[AppSchema.LegalEntity.GIRNOColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.TANNOColumn, drLegalEntity[AppSchema.LegalEntity.TANNOColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.PANNOColumn, drLegalEntity[AppSchema.LegalEntity.PANNOColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.STATEColumn, drLegalEntity[AppSchema.LegalEntity.STATEColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.EMAILColumn, drLegalEntity[AppSchema.LegalEntity.EMAILColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.PINCODEColumn, drLegalEntity[AppSchema.LegalEntity.PINCODEColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.URLColumn, drLegalEntity[AppSchema.LegalEntity.URLColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.REGNOColumn, drLegalEntity[AppSchema.LegalEntity.REGNOColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.PERMISSIONNOColumn, drLegalEntity[AppSchema.LegalEntity.PERMISSIONNOColumn.ColumnName].ToString());
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.REGDATEColumn, this.DateSet.ToDate(drLegalEntity[AppSchema.LegalEntity.REGDATEColumn.ColumnName].ToString(), false));
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.PERMISSIONDATEColumn, this.DateSet.ToDate(drLegalEntity[AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName].ToString(), false));
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.ASSOCIATIONNATUREColumn, drLegalEntity[AppSchema.LegalEntity.ASSOCIATIONNATUREColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(drLegalEntity[AppSchema.LegalEntity.ASSOCIATIONNATUREColumn.ColumnName].ToString()) : 0);
                            dataManagerLegalEntity.Parameters.Add(AppSchema.LegalEntity.DENOMINATIONColumn, drLegalEntity[AppSchema.LegalEntity.DENOMINATIONColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(drLegalEntity[AppSchema.LegalEntity.DENOMINATIONColumn.ColumnName].ToString()) : 0);
                            if (!isLegalEntityExist(dataManagerLegalEntity))
                            {
                                // sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.AddLegalEntity);
                                resultArgs = dataManagerLegalEntity.UpdateData();
                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.SUCCESS_LEGALENTITY_HEADOFFICE_BRACHOFFICE));
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import Projet Category from head office to branch office.
        /// </summary>
        /// <param name="dataProjectCategory"></param>
        /// <param name="dtProjectCategory"></param>
        /// <returns></returns>
        private ResultArgs ImportProjectCategory(DataManager dataProjectCategory)
        {
            try
            {
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.IMPORT_PROJECTCATOGORY_HEADOFFICE_BRACHOFFICE));
                if (dtProjectCategory != null && dtProjectCategory.Rows.Count != 0)
                {
                    foreach (DataRow drProCategory in dtProjectCategory.Rows)
                    {
                        using (DataManager dataManagerProjectCategory = new DataManager(SQLCommand.ImportMaster.AddProjectCatogory, SQLAdapterType.HOSQL))
                        {
                            dataManagerProjectCategory.Database = dataProjectCategory.Database;
                            dataManagerProjectCategory.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategoryName = drProCategory[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString());
                            if (!isProjectCategoryExist(dataManagerProjectCategory))
                            {
                                // sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.AddProjectCatogory);
                                resultArgs = dataManagerProjectCategory.UpdateData();
                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.SUCCESS_PROJECTCATOGORY_HEADOFFICE_BRACHOFFICE));
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import Project from Head office to Branch Office
        /// </summary>
        /// <param name="dataProject"></param>
        /// <param name="dtProject"></param>
        /// <returns></returns>
        private ResultArgs ImportProject(DataManager dataProject)
        {
            try
            {
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.IMPORT_PROJECT_HEADOFFICE_BRACHOFFICE));
                if (dtProjects != null && dtProjects.Rows.Count != 0)
                {
                    foreach (DataRow drProject in dtProjects.Rows)
                    {
                        using (DataManager dataProjectManager = new DataManager(SQLCommand.ImportMaster.AddProject, SQLAdapterType.HOSQL))
                        {
                            dataProjectManager.Database = dataProject.Database;
                            dataProjectManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, ProjectCode = drProject[this.AppSchema.Project.PROJECT_CODEColumn.ColumnName].ToString());
                            dataProjectManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString());
                            dataProjectManager.Parameters.Add(this.AppSchema.Project.DIVISION_IDColumn, this.NumberSet.ToInteger(drProject[this.AppSchema.Project.DIVISION_IDColumn.ColumnName].ToString()));
                            dataProjectManager.Parameters.Add(this.AppSchema.Project.ACCOUNT_DATEColumn, drProject[this.AppSchema.Project.ACCOUNT_DATEColumn.ColumnName].ToString());
                            dataProjectManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, this.DateSet.ToDate(drProject[this.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false));
                            dataProjectManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, drProject[this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString());
                            dataProjectManager.Parameters.Add(this.AppSchema.Project.DESCRIPTIONColumn, drProject[this.AppSchema.Project.DESCRIPTIONColumn.ColumnName].ToString());
                            dataProjectManager.Parameters.Add(this.AppSchema.Project.NOTESColumn, drProject[this.AppSchema.Project.NOTESColumn.ColumnName].ToString());

                            //Get Project Category Id based on the Project Category Name from branch office database.
                            if (drProject[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName] != DBNull.Value)
                            {
                                ProjectCategoryName = drProject[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();
                                dataProjectManager.Parameters.Add(this.AppSchema.Project.PROJECT_CATEGORY_IDColumn, GetMasterId(DataSync.ProjectCategory));
                            }

                            dataProjectManager.Parameters.Add(this.AppSchema.Project.DELETE_FLAGColumn, drProject[this.AppSchema.Project.DELETE_FLAGColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(drProject[this.AppSchema.Project.DELETE_FLAGColumn.ColumnName].ToString()) : 0);

                            //Get Legal entity id based on the society name from branch office database.
                            if (drProject[this.AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName] != DBNull.Value)
                            {
                                SocName = drProject[this.AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString();
                                dataProjectManager.Parameters.Add(this.AppSchema.Project.CUSTOMERIDColumn, GetMasterId(DataSync.LegalEntity));
                            }
                            else
                            {
                                int iProjectCategory = 0;
                                dataProjectManager.Parameters.Add(this.AppSchema.Project.CUSTOMERIDColumn, iProjectCategory);
                            }
                            if (!isProjectExists(dataProjectManager)) //Check is project exist or not .
                            {
                                //sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.AddProject);
                                resultArgs = dataProjectManager.UpdateData();
                                if (resultArgs.Success)
                                {
                                    ProjectId = (!string.IsNullOrEmpty(ProjectName)) ? GetMasterId(DataSync.Project) : 0;//Fetch project id by Project Name
                                    if (ProjectId != 0)
                                    {
                                        if (!MapCashLedgerToProject(dataProjectManager))// Map default cash ledger to project
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.SUCCESS_PROJECT_HEADOFFICE_BRACHOFFICE));
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import Ledger from head office to branch office.
        /// </summary>
        /// <param name="dataLedger"></param>
        /// <param name="dtLedger"></param>
        /// <returns></returns>
        private ResultArgs ImportLedgerDetails(DataManager dataLedger)
        {
            try
            {
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.IMPORT_LEDGER_HEADOFFICE_BRACHOFFICE));
                using (DataManager dataLedgerManager = new DataManager())
                {
                    dataLedgerManager.Database = dataLedger.Database;
                    if (dtLedger != null && dtLedger.Rows.Count != 0)
                    {
                        using (HeadOfficeLedgersSystem headOffieLedgerSystem = new HeadOfficeLedgersSystem())
                        {
                            resultArgs = headOffieLedgerSystem.ImportLedgerDetails(dataLedgerManager, dtLedger);
                        }
                    }
                }
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.SUCCESS_LEDGER_HEADOFFICE_BRACHOFFICE));
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import LedgerGroup from head office to branch office.
        /// </summary>
        /// <param name="dataLedger"></param>
        /// <param name="dtLedger"></param>
        /// <returns></returns>
        private ResultArgs ImportLedgerGroupDetails(DataManager dataGroupLedger)
        {
            try
            {
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.IMPORT_LEDGERGROUP_HEADOFFICE_BRACHOFFICE));
                using (DataManager dataLedgerGroupManager = new DataManager())
                {
                    dataLedgerGroupManager.Database = dataGroupLedger.Database;
                    if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count != 0)
                    {
                        using (HeadOfficeLedgersSystem headOffieLedgerSystem = new HeadOfficeLedgersSystem())
                        {
                            resultArgs = headOffieLedgerSystem.InsertMasterLedgerGroup(dataLedgerGroupManager, dtLedgerGroup);

                        }
                    }
                }
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.SUCCESS_LEDGERGROUP_HEADOFFICE_BRACHOFFICE));
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }
        /// <summary>
        /// Import FC Purpose from Head office to branch office.
        /// </summary>
        /// <param name="dataPurpose"></param>
        /// <param name="dtFCPurpose"></param>
        /// <returns></returns>
        private ResultArgs ImportFCPurpose(DataManager dataPurpose)
        {
            try
            {
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.IMPORT_FCPURPOSE_HEADOFFICE_BRACHOFFICE));
                if (dtFCPurpose != null && dtFCPurpose.Rows.Count != 0)
                {
                    foreach (DataRow drFCPurpose in dtFCPurpose.Rows)
                    {
                        using (DataManager dataFCPurposeManager = new DataManager(SQLCommand.ImportMaster.AddFCPurpose, SQLAdapterType.HOSQL))
                        {
                            dataFCPurposeManager.Database = dataPurpose.Database;
                            dataFCPurposeManager.Parameters.Add(AppSchema.Purposes.CODEColumn, FCCode = drFCPurpose[AppSchema.Purposes.CODEColumn.ColumnName].ToString());
                            dataFCPurposeManager.Parameters.Add(AppSchema.Purposes.FC_PURPOSEColumn, FCPurpose = drFCPurpose[AppSchema.Purposes.FC_PURPOSEColumn.ColumnName].ToString());
                            if (!isFCPurposeExists(dataFCPurposeManager))
                            {
                                //  sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.AddFCPurpose);
                                resultArgs = dataFCPurposeManager.UpdateData();
                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                AcMEDataSynLog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Import.SUCCESS_FCPURPOSE_HEADOFFICE_BRACHOFFICE));
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Check Whether the legal entity is already exist in the branch database table.
        /// </summary>
        /// <param name="dataLegalEntityExists"></param>
        /// <returns></returns>
        private bool isLegalEntityExist(DataManager dataLegalEntityExists)
        {
            try
            {
                // sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.IsLegalEntityExists);
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.IsLegalEntityExists, SQLAdapterType.HOSQL))
                {
                    dataManager.Database = dataLegalEntityExists.Database;
                    dataManager.Parameters.Add(this.AppSchema.LegalEntity.INSTITUTENAMEColumn, InsName);
                    dataManager.Parameters.Add(this.AppSchema.LegalEntity.SOCIETYNAMEColumn, SocName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger > 0;
        }

        /// <summary>
        /// Check whether Project Category is Exist in the branch database table
        /// </summary>
        /// <param name="dataManagerProjectCategory"></param>
        /// <returns></returns>
        private bool isProjectCategoryExist(DataManager dataManagerProjectCategory)
        {
            try
            {
                // sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.IsProjectCatogoryExists);
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.IsProjectCatogoryExists, SQLAdapterType.HOSQL))
                {
                    dataManager.Database = dataManagerProjectCategory.Database;
                    dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategoryName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger > 0;
        }

        /// <summary>
        /// Check whether Project is alreay exists or not in the branch office database table.
        /// </summary>
        /// <param name="dataManagerProject"></param>
        /// <returns></returns>
        private bool isProjectExists(DataManager dataManagerProject)
        {
            try
            {
                // sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.IsProjectExists);
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.IsProjectExists, SQLAdapterType.HOSQL))
                {
                    dataManager.Database = dataManagerProject.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, ProjectCode);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger > 0;
        }

        /// <summary>
        /// Check whehter FC Purpose is already exists or not in the branch office database table.
        /// </summary>
        /// <param name="dataFCPurposeExists"></param>
        /// <returns></returns>
        private bool isFCPurposeExists(DataManager dataFCPurposeExists)
        {
            try
            {
                // sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.IsFCPurposeExists);
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.IsFCPurposeExists, SQLAdapterType.HOSQL))
                {
                    dataManager.Database = dataFCPurposeExists.Database;
                    dataManager.Parameters.Add(this.AppSchema.Purposes.CODEColumn, FCCode);
                    dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, FCPurpose);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger > 0;
        }

        /// <summary>
        /// Map default Cash Ledger with project.
        /// </summary>
        /// <param name="dataManagerMapping"></param>
        /// <returns></returns>
        private bool MapCashLedgerToProject(DataManager dataManagerMapping)
        {
            List<int> voucherTypes = new List<int>();
            voucherTypes.Add((int)DefaultVoucherTypes.Receipts);
            voucherTypes.Add((int)DefaultVoucherTypes.Payments);
            voucherTypes.Add((int)DefaultVoucherTypes.Contra);
            voucherTypes.Add((int)DefaultVoucherTypes.Journal);

            try
            {
                // sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.MapLedgers);
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.MapLedgers, SQLAdapterType.HOSQL))
                {
                    dataManager.Database = dataManagerMapping.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, (int)DefaultLedgers.Cash);
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        DataView dv = this.EnumSet.GetEnumDataSource(DefaultVoucherTypes.Contra.ToString());
                        for (int i = 1; i <= voucherTypes.Count; i++)
                        {
                            if (!MapVoucherToProject(dataManager, i))
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs.Success;
        }

        /// <summary>
        /// Map default voucher with Project.
        /// </summary>
        /// <param name="dataManagerVoucher"></param>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        private bool MapVoucherToProject(DataManager dataManagerVoucher, int VoucherId)
        {
            try
            {
                // sqlQuery = this.GetMasterSQL(EnumDataSyncSQLCommand.ImportSQL.MapVouchers);
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.MapVouchers, SQLAdapterType.HOSQL))
                {
                    dataManager.Database = dataManagerVoucher.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs.Success;
        }

        /// <summary>
        /// Fill the datatable with proper table.
        /// </summary>
        /// <param name="dsReadXML"></param>
        private void AssignDataTable(DataSet dsReadXML)
        {
            try
            {
                var query = dsReadXML.Tables.OfType<DataTable>().Select(dt => dt.TableName);
                foreach (var item in query)
                {
                    switch (item)
                    {
                        case LEGAL_ENTITY_TABLE_NAME:
                            {
                                dtLegalEntity = dsReadXML.Tables[LEGAL_ENTITY_TABLE_NAME];
                                break;
                            }
                        case PROJECT_CATEGORY_TABLE_NAME:
                            {
                                dtProjectCategory = dsReadXML.Tables[PROJECT_CATEGORY_TABLE_NAME];
                                break;
                            }
                        case PROJECT_TABLE_NAME:
                            {
                                dtProjects = dsReadXML.Tables[PROJECT_TABLE_NAME];
                                break;
                            }
                        case LEDGER_TABLE_NAME:
                            {
                                dtLedger = dsReadXML.Tables[LEDGER_TABLE_NAME];
                                break;
                            }
                        case FCPURPOSE_TABLE_NAME:
                            {
                                dtFCPurpose = dsReadXML.Tables[FCPURPOSE_TABLE_NAME];
                                break;
                            }
                        case LEDGERGROUP_TABLE_NAME:
                            {
                                dtLedgerGroup = dsReadXML.Tables[LEDGERGROUP_TABLE_NAME];
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
        }

        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
