using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel;
using Bosco.Model.UIModel.Master;
using Bosco.Utility.ConfigSetting;
using Bosco.Model.TDS;
using Bosco.Model.Transaction;

namespace Bosco.Model.Dsync
{
    public class ImportMasterSystem : SystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = new ResultArgs();
        CommonMethod common = new CommonMethod();
        SettingProperty settingproperty = new SettingProperty();
        private const string LEDGER_TABLE_NAME = "Ledger";
        private const string BUDGETGROUP_TABLE_NAME = "BudgetGroup";
        private const string BUDGETSUBGROUP_TABLE_NAME = "BudgetSubGroup";
        private const string LEGAL_ENTITY_TABLE_NAME = "LegalEntity";
        private const string PROJECT_CATEGORY_TABLE_NAME = "ProjectCategory";
        private const string PROJECT_TABLE_NAME = "Project";
        private const string FCPURPOSE_TABLE_NAME = "Purpose";
        private const string GOVERNING_BODIES = "GoverningMember";
        private const string LEDGERGROUP_TABLE_NAME = "LedgerGroup";
        private const string GENERALATEGROUP_TABLE_NAME = "GeneralateLedger";
        private const string GENERALATEGROUPMAP_TABLE_NAME = "CongregationLedgerMap";
        private const string BRANCHOFFICE_TABLE_NAME = "BranchOffice";
        private const string COUNTRY_TABLE_NAME = "Country";
        private const string TDS_SECTION = "TDSSection";
        private const string TDS_NATURE_OF_PAYMENTS = "TDSNatureOfPayments";
        private const string TDS_DEDUCTEE_TYPES = "TDSDeducteeTypes";
        private const string TDS_DUTY_TAX = "TDSDutyTax";
        private const string TDS_POLICY = "TDSPolicy";
        private const string TDS_TAX_RATE = "TDSTaxRate";
        private const string TDS_POLICY_DEDUCTEES = "TDSPolicyDeductees";
        private Form objMapMisMatchLedgers = null;
        private Form objMapMisMatchProjects = null;
        public bool ProjectLedgerMapping = true;
        #endregion

        #region Properties
        #region Legal Entity Properties
        private string instituteName { get; set; }
        private string SocietyName { get; set; }
        private string ExecutiveMember { get; set; }
        private DataTable dtLegalEntity { get; set; }
        #endregion

        #region Project Category Properties
        private string ProjectCategoryName { get; set; }

        private string ProjectCategoryITRGroup { get; set; }
        #endregion

        #region FCPurpose Properties
        private int PurposeId { get; set; }
        public string FCPurpose { get; set; }
        private string FCCode { get; set; }
        public DataTable dtFCPurpose { get; set; }
        private DataTable dtGoverningBodies { get; set; }
        #endregion

        #region Project Properties
        public string ProjectName { get; set; }
        private string ProjectCode { get; set; }
        private int ProjectId { get; set; }
        private DataTable dtProjects { get; set; }
        #endregion

        #region BranchOffice Propertise
        private string BranchOfficeCode { get; set; }
        private string BranchOfficeName { get; set; }
        private string HeadOfficeCodeBranch { get; set; }
        private int DeploymentType { get; set; }
        private string Address { get; set; }
        private string Pcode { get; set; }
        private string PhoneNumber { get; set; }
        private string MobileNumber { get; set; }
        private string BranchEMailId { get; set; }
        private int Status { get; set; }
        private string City { get; set; }
        private string BranchPartCode { get; set; }
        private string BranchKeyCode { get; set; }

        private DataTable dtBranchOffice { get; set; }
        #endregion

        #region Leger Group Properties
        public int GroupId { get; set; }
        public string LedgerGroup { get; set; }
        public string ParentGroup { get; set; }
        public string Nature { get; set; }
        public string MainGroup { get; set; }
        private DataTable dtLedgerGroup { get; set; }
        private DataTable dtGeneralateGroup { get; set; }
        private DataTable dtGeneralateGroupMap { get; set; }
        #endregion

        #region Ledger Properties
        public int LedgerId { get; set; }
        public int HeadOfficeLedgerId { get; set; }
        public string LedgerName { get; set; }
        private DataTable dtLedger { get; set; }

        public int GeneralateId { get; set; }
        public string GeneralateCode { get; set; }
        public string GeneralateLedger { get; set; }
        public string GenParent { get; set; }
        public string GenMainParent { get; set; }

        public int BudgetGroupId { get; set; }
        public int BudgetSubGroupId { get; set; }
        public string BudgetGroup { get; set; }
        public string BudgetSubGroup { get; set; }
        private DataTable dtBudgetGroup { get; set; }
        private DataTable dtBudgetSubGroup { get; set; }

        public string ImportMasterRemarks { get; set; }
        private string NotUpdtaedLedgerClosedDate = string.Empty;

        private DataTable dtLocalLedger { get; set; }
        #endregion

        #region Country Properties
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        private DataTable dtCountry { get; set; }
        #endregion

        #region TDS
        public int TDSSectionId { get; set; }
        public string TDSSectionCode { get; set; }
        public string TDSSectionName { get; set; }
        public int IsActive { get; set; }
        private DataTable dtTDSSection { get; set; }

        public int NaturePaymentId { get; set; }
        public string PaymentName { get; set; }
        private DataTable dtTDSNatureOfPayments { get; set; }

        public int DeducteeId { get; set; }
        public string DeducteeName { get; set; }
        private int TempTaxPolicyId { get; set; }
        private DataTable dtTDSDeducteeTypes { get; set; }

        public string TaxTypeName { get; set; }
        private DataTable dtTDSDutyTax { get; set; }

        public DateTime TaxApplicableFrom { get; set; }
        private int PolicyId { get; set; }
        private DataTable dtTDSPolicy { get; set; }

        private DataTable dtTDSTaxRate { get; set; }

        private DataTable dtTDSPolicyDeductees { get; set; }
        #endregion

        #endregion

        #region Public Method
        /// <summary>
        /// Import All the master details to branch office from head office
        /// </summary>
        /// <param name="dsReadXML">This XML file contains all these tables (HeadOffice,Legal Entity,Project Category,Project,Ledger,FCPurpose)</param>
        /// <returns></returns>
        public ResultArgs ImportMasters(DataSet dsMasters, Form objMapLedgers, Form objMapProjects)
        {
            NotUpdtaedLedgerClosedDate = string.Empty;
            ImportMasterRemarks = string.Empty;
            using (DataManager dataManager = new DataManager())
            {
                try
                {
                    objMapMisMatchLedgers = objMapLedgers;
                    objMapMisMatchProjects = objMapProjects;
                    dataManager.BeginTransaction();
                    resultArgs = ValidateMasterXMLFile(dsMasters);
                    if (resultArgs.Success)  // Validate Branch Office Code and Head Office Code
                    {
                        resultArgs = common.ValidateLicenseInformation(dsMasters);
                        if (resultArgs.Success)  // Separate the Dataset into appropriate tables
                        {
                            resultArgs = AssignDataTable(dsMasters);
                            if (resultArgs.Success)  // Ledger Group
                            {
                                resultArgs = ImportMasterLedgerGroup();
                                if (resultArgs.Success)   //Ledger Details
                                {
                                    resultArgs = ImportMasterLedger(); // Generalate Ledgers
                                    if (resultArgs.Success)
                                    {
                                        if (settingproperty.IS_SAPPIC || settingproperty.IS_FDCCSI)
                                            resultArgs = ImportGeneralateGroupLedger();
                                        if (resultArgs.Success)  //Legal Entity
                                        {
                                            resultArgs = ImportLegalEntity();
                                            if (resultArgs.Success)   //Project Category
                                            {
                                                resultArgs = ImportProjectCategory();
                                                if (resultArgs.Success)  //Sub Branch Office Details, If available.
                                                {
                                                    resultArgs = ImportProjectCategoryLedgers(); // Map Project Category With Ledgers
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = ImportBranchOffice();
                                                        if (resultArgs.Success)   //Project Details
                                                        {
                                                            resultArgs = ImportProject();
                                                            if (resultArgs.Success)  //FC Purpose Details.
                                                            {
                                                                if (ProjectLedgerMapping) { resultArgs = MapProjectLedger(); }
                                                                if (resultArgs.Success)
                                                                {
                                                                    resultArgs = ImportFCPurpose();
                                                                    if (resultArgs.Success)   //TDS Section Details.
                                                                    {
                                                                        resultArgs = ImportGoverningBodies();
                                                                        if (resultArgs.Success)
                                                                        {
                                                                            resultArgs = ImportTDSSection();
                                                                            if (resultArgs.Success)  //TDS Nature Of Payments
                                                                            {
                                                                                resultArgs = ImportTDSNatureOfPayments();
                                                                                if (resultArgs.Success)  //TDS Deductee Types
                                                                                {
                                                                                    resultArgs = ImportTDSDeducteeTypes();
                                                                                    if (resultArgs.Success)  //TDS Duty Tax Types
                                                                                    {
                                                                                        resultArgs = ImportDutyTax();
                                                                                        if (resultArgs.Success)
                                                                                        {
                                                                                            resultArgs = ImportTDSTaxPolicy();
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //On 05/03/2020, To Map all recurring ledgers to all projects
                    //For mysore 
                    if (this.IS_DIOMYS_DIOCESE)
                    {
                        using (MappingSystem map = new MappingSystem())
                        {
                            map.MapBudgetRecLedgerForAll();
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = "Error in Import Masters" + ex.ToString();
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                        AcMELog.WriteLog("Error in Import Masters: " + resultArgs.Message);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(NotUpdtaedLedgerClosedDate))
                        {
                            ImportMasterRemarks = string.Empty;
                            ImportMasterRemarks = "Ledger Closed Date is not updated to the following Ledgers as they have Voucher in Branch/Local Community : ";
                            ImportMasterRemarks += System.Environment.NewLine;
                            ImportMasterRemarks += NotUpdtaedLedgerClosedDate.Trim(',');
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is an entry point to import TDS masters.
        /// Calling the appropriate method to import TDS masters one by one.
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportTDSMasters(DataSet dsMasters)
        {
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager())
            {
                try
                {
                    dataManager.BeginTransaction();
                    resultArgs = ValidateTDSMasterXMLFile(dsMasters);
                    if (resultArgs.Success)  // Validate Branch Office Code and Head Office Code
                    {
                        resultArgs = common.ValidateLicenseInformation(dsMasters);
                        if (resultArgs.Success)
                        {
                            resultArgs = AssignDataTable(dsMasters);
                            if (resultArgs.Success)
                            {
                                resultArgs = ImportGoverningBodies();
                                if (resultArgs.Success)  //TDS Nature Of Payments
                                {
                                    resultArgs = ImportTDSSection();
                                    if (resultArgs.Success)  //TDS Nature Of Payments
                                    {
                                        resultArgs = ImportTDSNatureOfPayments();
                                        if (resultArgs.Success)  //TDS Deductee Types
                                        {
                                            resultArgs = ImportTDSDeducteeTypes();
                                            if (resultArgs.Success)  //TDS Duty Tax Types
                                            {
                                                resultArgs = ImportDutyTax();
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = ImportTDSTaxPolicy();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = "Error in Import TDS Masters" + ex.ToString();
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                        AcMELog.WriteLog("Error in Import Masters: " + resultArgs.Message);
                    }
                    dataManager.EndTransaction();
                }
            }
            return result;
        }

        private ResultArgs ValidateMasterXMLFile(DataSet dsMasters)
        {
            try
            {
                if (dsMasters != null && dsMasters.Tables.Count > 0)
                {
                    if (dsMasters.Tables.Contains("Header") && dsMasters.Tables.Contains(PROJECT_TABLE_NAME) &&
                        dsMasters.Tables.Contains(LEDGER_TABLE_NAME) && dsMasters.Tables.Contains(FCPURPOSE_TABLE_NAME))
                    {
                        resultArgs.Success = true;
                    }
                    else
                    {
                        resultArgs.Message = "The Master File is invalid. It does not contain master details.";
                    }
                }
                else
                {
                    resultArgs.Message = "File does not contain head office master data. File is empty.";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs ValidateTDSMasterXMLFile(DataSet dsMasters)
        {
            try
            {
                if (dsMasters != null && dsMasters.Tables.Count > 0)
                {
                    if (dsMasters.Tables.Contains("Header") && dsMasters.Tables.Contains(TDS_SECTION) &&
                        dsMasters.Tables.Contains(TDS_NATURE_OF_PAYMENTS) && dsMasters.Tables.Contains(TDS_DUTY_TAX))
                    {
                        resultArgs.Success = true;
                    }
                    else
                    {
                        resultArgs.Message = "The Master File is invalid. It does not contain master details.";
                    }
                }
                else
                {
                    resultArgs.Message = "File does not contain head office master data. File is empty.";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// Insert Master Ledger Group Details in the Branch Office 
        /// </summary>
        /// <param name="dataGroupmanager"></param>
        /// <param name="dtGroup"></param>
        /// <returns></returns>
        private ResultArgs ImportMasterLedgerGroup()
        {
            try
            {
                AcMELog.WriteLog("InsertMasterLedgerGroup Started");
                if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count > 0)
                {
                    foreach (DataRow drRowLedgerGroup in dtLedgerGroup.Rows)
                    {
                        using (LedgerGroupSystem MasterLedgerGroup = new LedgerGroupSystem())
                        {
                            MasterLedgerGroup.Group = LedgerGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(MasterLedgerGroup.Group))
                            {
                                MasterLedgerGroup.Abbrevation = drRowLedgerGroup[this.AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName].ToString();
                                //ParentGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.ParentGroupColumn.ColumnName].ToString();
                                //MasterLedgerGroup.ParentGroupId = GetMasterId(DataSync.ParentGroup);
                                Nature = drRowLedgerGroup[this.AppSchema.LedgerGroup.NATUREColumn.ColumnName].ToString();
                                MasterLedgerGroup.NatureId = GetMasterId(DataSync.Nature);
                                //MainGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.MainGroupColumn.ColumnName].ToString();
                                //MasterLedgerGroup.MainGroupId = GetMasterId(DataSync.MainGroup);
                                MasterLedgerGroup.SortOrder = this.NumberSet.ToInteger(drRowLedgerGroup[this.AppSchema.LedgerGroup.SORT_ORDERColumn.ColumnName].ToString());

                                //Ledger Group Validations
                                //if (MasterLedgerGroup.ParentGroupId == 0)
                                //{
                                //    resultArgs.Message = "Parent Group '" + ParentGroup + "' does not exists in Branch Office.";
                                //}
                                if (MasterLedgerGroup.NatureId == 0)
                                {
                                    resultArgs.Message = "Nature '" + Nature + "' does not exists in Branch Office.";
                                }
                                //else if (MasterLedgerGroup.MainGroupId == 0)
                                //{
                                //    resultArgs.Message = "Main Group '" + MainGroup + "' does not exist in Branch Office.";
                                //}
                                else
                                {
                                    resultArgs = IsExist(Bosco.Utility.DataSync.IsLedgerGroupExists);
                                    if (resultArgs.Success)
                                    {
                                        if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                        {
                                            MasterLedgerGroup.GroupId = GroupId = GetMasterId(DataSync.LedgerGroup);
                                        }
                                        resultArgs = MasterLedgerGroup.SaveLedgerGroupDetails();
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            resultArgs.Message = "Problem in InsertMasterLedgerGroup: " + resultArgs.Message;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing Master Ledger Group. " + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("InsertMasterLedgerGroup Ended");
                resultArgs = UpdateLedgerGroupParentId();

                //On 22/12/2021, To update Budget Group and Budget Sub Group from portal ------------------------------
                if (resultArgs.Success)
                {
                    resultArgs = ImportBudgetGroup();
                    if (resultArgs.Success)
                    {
                        resultArgs = ImportBudgetSubGroup();

                        //On 20/10/2022, Delete Budget Group and Sub Groups which are not available in Acme.erp portal
                        if (resultArgs.Success)
                        {
                            DeleteInValidBudgetGroupAndSubGroup();
                        }
                    }
                }
                //--------------------------------------------------------------------------------------------------

            }
            return resultArgs;
        }

        private ResultArgs UpdateLedgerGroupParentId()
        {
            try
            {
                AcMELog.WriteLog("UpdateLedgerGroupParentId Started");
                if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count > 0)
                {
                    foreach (DataRow drRowLedgerGroup in dtLedgerGroup.Rows)
                    {
                        using (LedgerGroupSystem MasterLedgerGroup = new LedgerGroupSystem())
                        {
                            LedgerGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(LedgerGroup))
                            {
                                ParentGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.ParentGroupColumn.ColumnName].ToString();
                                MasterLedgerGroup.ParentGroupId = MasterLedgerGroup.MainGroupId = GetMasterId(DataSync.ParentGroup);
                                MasterLedgerGroup.GroupId = GetMasterId(DataSync.LedgerGroup);

                                //Ledger Group Validations
                                if (MasterLedgerGroup.GroupId == 0)
                                {
                                    resultArgs.Message = "Ledger Group " + LedgerGroup + " is not avaiable in Branch Office";
                                }
                                else if (string.IsNullOrEmpty(ParentGroup))
                                {
                                    resultArgs.Message = "Parent Group is empty in the master file.";
                                }
                                else if (MasterLedgerGroup.ParentGroupId == 0)
                                {
                                    resultArgs.Message = "Parent Group '" + ParentGroup + "' does not exists in Branch Office.";
                                }
                                else
                                {
                                    resultArgs = MasterLedgerGroup.UpdateParentGroupId();
                                }
                            }
                        }
                        if (!resultArgs.Success)
                        {
                            resultArgs.Message = "Problem in UpdateLedgerGroupParentId: " + resultArgs.Message;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Updating ledger group parent id. " + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("UpdateLedgerGroupParentId Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Insert Master Ledger in the Branch Office
        /// </summary>
        /// <param name="LedgerManager"></param>
        /// <returns></returns>
        public ResultArgs ImportMasterLedger()
        {
            AcMELog.WriteLog("ImportMasterLedger Started");
            try
            {
                resultArgs = SaveMasterHeadOfficeLedgers();
                if (resultArgs.Success)
                {
                    resultArgs = RemoveUnUsedHeadOfficeLedgers();

                    // 11/11/2024, to Remove Ledger and Ledger Group which is not available, no transactio exists---------
                    if (settingproperty.IS_SDB_INM && settingproperty.LockMasters == 1)
                    {
                        if (resultArgs.Success)
                        {
                            resultArgs = UpdateBranchLedgerFlag();
                            if (resultArgs.Success)
                            {
                                resultArgs = FetchBranchLedgers();

                                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    dtLocalLedger = resultArgs.DataSource.Table;

                                    resultArgs = DeleteLedgerNotExistsPortal();
                                }
                            }
                        }
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteLedgerGroupNotExistsPortal();
                        }

                        if (resultArgs.Success)
                        {
                            resultArgs = UpdateGroupParentMainParent();
                        }

                    }
                    //    ---------------------------------------------------------------------------------------------------
                }
                //DataTable dtBranchHeadOfficeLedgers = null;
                //if (dtLedger != null && dtLedger.Rows.Count > 0)
                //{
                //    DataTable dtHeadOfficeLedgers = dtLedger;
                //    dtHeadOfficeLedgers.AsEnumerable().ToList()
                //     .ForEach(row =>
                //    {
                //        var cellList = row.ItemArray.ToList();
                //        row.ItemArray = cellList.Select(x => x.ToString().Trim()).ToArray();
                //    });

                //    resultArgs = FetchBrachHeadOfficeLedgers();
                //    if (resultArgs.Success)
                //    {
                //        dtBranchHeadOfficeLedgers = resultArgs.DataSource.Table;

                //        if (dtBranchHeadOfficeLedgers != null && dtBranchHeadOfficeLedgers.Rows.Count > 0)
                //        {
                //            dtBranchHeadOfficeLedgers.AsEnumerable().ToList()
                //             .ForEach(row =>
                //             {
                //                 var cellList = row.ItemArray.ToList();
                //                 row.ItemArray = cellList.Select(x => x.ToString().Trim()).ToArray();
                //             });

                //            DataTable dtMisMatchedLedgers = FetchMisMatchedLedgers(dtHeadOfficeLedgers, dtBranchHeadOfficeLedgers);
                //            DataTable dtModifiedLedgers = FetchModifiedLedgers(dtHeadOfficeLedgers, dtBranchHeadOfficeLedgers);

                //            if ((dtMisMatchedLedgers != null && dtMisMatchedLedgers.Rows.Count > 0) || (dtModifiedLedgers != null && dtModifiedLedgers.Rows.Count > 0))
                //            {
                //                if ((dtMisMatchedLedgers != null && dtMisMatchedLedgers.Rows.Count > 0))
                //                {
                //                    dtMisMatchedLedgers.Columns.Add("LEDGER_NAME", typeof(string));
                //                    this.ModifiedLedgers = dtModifiedLedgers;
                //                    this.MisMatchedLedgers = dtMisMatchedLedgers;
                //                    objMapMisMatchLedgers.ShowDialog();
                //                    if (objMapMisMatchLedgers.DialogResult == DialogResult.Cancel)
                //                    {
                //                        resultArgs.Message = "Import Master process is cancelled";
                //                    }
                //                }
                //                else if (dtModifiedLedgers != null && dtModifiedLedgers.Rows.Count > 0)
                //                {
                //                    resultArgs = SaveMasterHeadOfficeLedgers(dtModifiedLedgers);
                //                }
                //            }
                //            else
                //            {
                //                resultArgs = SaveMasterHeadOfficeLedgers(dtHeadOfficeLedgers);
                //            }
                //        }
                //        else
                //        {
                //            resultArgs = SaveMasterHeadOfficeLedgers(dtHeadOfficeLedgers);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing master ledger details." + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("ImportMasterLedger Ended");
            }
            return resultArgs;
        }



        /// <summary>
        /// Insert Master Generalate Details in the Branch Office 
        /// </summary>
        /// <param name="dataGroupmanager"></param>
        /// <param name="dtGroup"></param>
        /// <returns></returns>
        private ResultArgs ImportGeneralateGroupLedger()
        {
            try
            {
                AcMELog.WriteLog("Insert Generalate Group Started");
                if (dtGeneralateGroup != null && dtGeneralateGroup.Rows.Count > 0)
                {
                    foreach (DataRow drRowGeneralateGroup in dtGeneralateGroup.Rows)
                    {
                        using (LedgerGroupSystem GeneralateLedgerGroup = new LedgerGroupSystem())
                        {
                            GeneralateLedgerGroup.GeneralateName = GeneralateLedger = drRowGeneralateGroup[this.AppSchema.GeneralateGroupLedger.CON_LEDGER_NAMEColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(GeneralateLedgerGroup.GeneralateName))
                            {
                                GeneralateLedgerGroup.GeneralateCode = drRowGeneralateGroup[this.AppSchema.GeneralateGroupLedger.CON_LEDGER_CODEColumn.ColumnName].ToString();
                                GenParent = drRowGeneralateGroup[this.AppSchema.GeneralateGroupLedger.PARENTColumn.ColumnName].ToString();
                                GeneralateLedgerGroup.GenParentId = GetMasterId(DataSync.GeneralateParent);
                                GenMainParent = drRowGeneralateGroup[this.AppSchema.GeneralateGroupLedger.MAINPARENTColumn.ColumnName].ToString();
                                GeneralateLedgerGroup.GenMainParentId = GetMasterId(DataSync.GeneralateMainParent);
                                //if (GeneralateLedgerGroup.GenParentId == 0)
                                //{
                                //    resultArgs.Message = "ParentGroup '" + GenParent + "' does not exists in Branch Office.";
                                //}
                                //if (GeneralateLedgerGroup.GenMainParentId == 0)
                                //{
                                //    resultArgs.Message = "MainParentGroup '" + GenMainParent + "' does not exists in Branch Office.";
                                //}
                                // else
                                //  {
                                resultArgs = IsExist(Bosco.Utility.DataSync.IsGeneralateLedgersExists);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        GeneralateLedgerGroup.GeneralateId = GeneralateId = GetMasterId(DataSync.GeneralateLedger);
                                    }
                                    resultArgs = GeneralateLedgerGroup.SaveGeneralateLedgerGroupDetails();
                                }
                            }
                        }
                        if (!resultArgs.Success)
                        {
                            resultArgs.Message = "Problem in InsertMasterLedgerGroup: " + resultArgs.Message;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing Master Ledger Group. " + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("InsertMasterLedgerGroup Ended");
                resultArgs = UpdateGeneralateGroupParentId();

                // Map Generalate Ledger with HO Ledgers
                resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteHeadOfficewithGeneralateMappedLedgers);
                if (resultArgs.Success)
                {
                    if (dtGeneralateGroupMap != null && dtGeneralateGroupMap.Rows.Count > 0)
                    {
                        foreach (DataRow drGeneralateGroupMapdetails in dtGeneralateGroupMap.Rows)
                        {
                            GeneralateLedger = drGeneralateGroupMapdetails[this.AppSchema.GeneralateGroupLedger.CON_LEDGER_NAMEColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(GeneralateLedger))
                            {
                                resultArgs = IsExist(Bosco.Utility.DataSync.IsGeneralateLedgersExists);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        GeneralateId = GetMasterId(DataSync.GeneralateLedger);
                                    }
                                    LedgerName = drGeneralateGroupMapdetails[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();

                                    // 07/04/2025, *Chinna, This is to get the Master_ledger ids for sap HO and FDCCSI we get the master_headoffice_ledger,
                                    // in order to update the if sap means local ledgers mapping with portal_congregation_ledger_map, fdccsi means ho ledger mapping with 
                                    // portal_congregation_ledger_map

                                    // HeadOfficeLedgerId = GetMasterId(DataSync.Ledger);
                                    // To Map Headoffice Ledger for mapping ( Generalate and HO Ledgers)
                                    // 18/09/2024

                                    if (this.settingproperty.IS_SAPPIC)
                                    {
                                        HeadOfficeLedgerId = GetMasterId(DataSync.Ledger);
                                    }
                                    else // for SDBINM, FDCCSI,FMA, for other all
                                    {
                                        HeadOfficeLedgerId = GetMasterId(DataSync.HeadOfficeLedger);
                                    }
                                    resultArgs = MapHeadOfficewithGeneralateLedger();
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateGeneralateGroupParentId()
        {
            try
            {
                AcMELog.WriteLog("UpdateLedgerGroupParentId Started");
                if (dtGeneralateGroup != null && dtGeneralateGroup.Rows.Count > 0)
                {
                    foreach (DataRow drRowLedgerGroup in dtGeneralateGroup.Rows)
                    {
                        using (LedgerGroupSystem GeneralateLedgerGroup = new LedgerGroupSystem())
                        {
                            GeneralateLedger = drRowLedgerGroup[this.AppSchema.GeneralateGroupLedger.CON_LEDGER_NAMEColumn.ColumnName].ToString(); ;
                            if (!string.IsNullOrEmpty(GeneralateLedger))
                            {
                                //ParentGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.ParentGroupColumn.ColumnName].ToString();
                                //GeneralateLedgerGroup.ParentGroupId = GeneralateLedgerGroup.MainGroupId = GetMasterId(DataSync.ParentGroup);

                                GenParent = drRowLedgerGroup[this.AppSchema.GeneralateGroupLedger.PARENTColumn.ColumnName].ToString();
                                GeneralateLedgerGroup.GenParentId = GeneralateLedgerGroup.GenMainParentId = GetMasterId(DataSync.GeneralateParent);

                                GeneralateLedgerGroup.GeneralateId = GetMasterId(DataSync.GeneralateLedger);

                                //Ledger Group Validations
                                if (GeneralateLedgerGroup.GeneralateId == 0)
                                {
                                    resultArgs.Message = "Generalate Group " + LedgerGroup + " is not avaiable in Branch Office";
                                }
                                else if (string.IsNullOrEmpty(GenParent))
                                {
                                    resultArgs.Message = "Generalate Parent Group is empty in the master file.";
                                }
                                else if (GeneralateLedgerGroup.GenParentId == 0)
                                {
                                    resultArgs.Message = "Generalate Group '" + GenParent + "' does not exists in Branch Office.";
                                }
                                else
                                {
                                    resultArgs = GeneralateLedgerGroup.UpdateGeneralateParentGroupId();
                                }
                            }
                        }
                        if (!resultArgs.Success)
                        {
                            resultArgs.Message = "Problem in UpdateLedgerGroupParentId: " + resultArgs.Message;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Updating ledger group parent id. " + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("UpdateLedgerGroupParentId Ended");
            }
            return resultArgs;
        }

        private ResultArgs ImportCountry()
        {
            try
            {
                if (dtCountry != null && dtCountry.Rows.Count != 0)
                {
                    foreach (DataRow drCountry in dtCountry.Rows)
                    {
                        using (CountrySystem countrySystem = new CountrySystem())
                        {
                            // countrySystem.CountryId = countryId == 0 ? (int)AddNewRow.NewRow : countryId;
                            countrySystem.CountryName = CountryName = drCountry[AppSchema.Country.COUNTRYColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(CountryName))
                            {
                                resultArgs = IsExist(DataSync.IsCountryExists);
                                if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger == 0)
                                {
                                    countrySystem.CountryCode = drCountry[AppSchema.Country.COUNTRY_CODEColumn.ColumnName].ToString();
                                    countrySystem.CurrencyCode = drCountry[AppSchema.Country.CURRENCY_CODEColumn.ColumnName].ToString();
                                    countrySystem.CurrencySymbol = drCountry[AppSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
                                    countrySystem.CurrencyName = drCountry[AppSchema.Country.CURRENCY_NAMEColumn.ColumnName].ToString();

                                    resultArgs = countrySystem.SaveCountryDetails();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Import country. " + ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Legal Entity from head office to branch office.
        /// </summary>
        /// <param name="dataLegalEntity"></param>
        /// <param name="dtLegalEntity">DataTable contains the values of Legal Entity</param>
        /// <returns></returns>
        private ResultArgs ImportLegalEntity()
        {
            DataTable dtLegalEntitySource = new DataTable();
            try
            {
                AcMELog.WriteLog("Ledgal Entity Started");
                if (dtLegalEntity != null && dtLegalEntity.Rows.Count != 0)
                {
                    foreach (DataRow drLegalEntity in dtLegalEntity.Rows)
                    {
                        using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                        {
                            legalEntitySystem.SocietyName = SocietyName = drLegalEntity[AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(SocietyName))
                            {
                                legalEntitySystem.ContactPerson = drLegalEntity[AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName].ToString();
                                legalEntitySystem.Address = drLegalEntity[AppSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString();
                                legalEntitySystem.Place = drLegalEntity[AppSchema.LegalEntity.PLACEColumn.ColumnName].ToString();
                                legalEntitySystem.Phone = drLegalEntity[AppSchema.LegalEntity.PHONEColumn.ColumnName].ToString();
                                legalEntitySystem.Fax = drLegalEntity[AppSchema.LegalEntity.FAXColumn.ColumnName].ToString();
                                CountryName = drLegalEntity[AppSchema.LegalEntity.COUNTRYColumn.ColumnName].ToString();
                                legalEntitySystem.CountryId = GetMasterId(DataSync.Country);
                                legalEntitySystem.A12No = drLegalEntity[AppSchema.LegalEntity.A12NOColumn.ColumnName].ToString();
                                legalEntitySystem.GIRNo = drLegalEntity[AppSchema.LegalEntity.GIRNOColumn.ColumnName].ToString();
                                legalEntitySystem.TANNo = drLegalEntity[AppSchema.LegalEntity.TANNOColumn.ColumnName].ToString();
                                legalEntitySystem.PANNo = drLegalEntity[AppSchema.LegalEntity.PANNOColumn.ColumnName].ToString();
                                State = drLegalEntity[AppSchema.LegalEntity.STATEColumn.ColumnName].ToString();
                                legalEntitySystem.StateId = GetMasterId(DataSync.State);
                                legalEntitySystem.EMail = drLegalEntity[AppSchema.LegalEntity.EMAILColumn.ColumnName].ToString();
                                legalEntitySystem.Pincode = drLegalEntity[AppSchema.LegalEntity.PINCODEColumn.ColumnName].ToString();
                                legalEntitySystem.URL = drLegalEntity[AppSchema.LegalEntity.URLColumn.ColumnName].ToString();
                                legalEntitySystem.RegNo = drLegalEntity[AppSchema.LegalEntity.REGNOColumn.ColumnName].ToString();
                                legalEntitySystem.PermissionNo = drLegalEntity[AppSchema.LegalEntity.PERMISSIONNOColumn.ColumnName].ToString();

                                if (!String.IsNullOrEmpty(drLegalEntity[AppSchema.LegalEntity.REGDATEColumn.ColumnName].ToString()))
                                {
                                    legalEntitySystem.RegDate = this.DateSet.ToDate(drLegalEntity[AppSchema.LegalEntity.REGDATEColumn.ColumnName].ToString(), false);
                                }

                                if (!String.IsNullOrEmpty(drLegalEntity[AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName].ToString()))
                                {
                                    legalEntitySystem.PermissionDate = this.DateSet.ToDate(drLegalEntity[AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName].ToString(), false);
                                }

                                legalEntitySystem.AssoicationNature = drLegalEntity[AppSchema.LegalEntity.ASSOCIATIONNATUREColumn.ColumnName] != DBNull.Value ? drLegalEntity[AppSchema.LegalEntity.ASSOCIATIONNATUREColumn.ColumnName].ToString() : string.Empty;
                                legalEntitySystem.Denomination = drLegalEntity[AppSchema.LegalEntity.DENOMINATIONColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(drLegalEntity[AppSchema.LegalEntity.DENOMINATIONColumn.ColumnName].ToString()) : 0;
                                legalEntitySystem.FCRINo = drLegalEntity[AppSchema.LegalEntity.FCRINOColumn.ColumnName].ToString();
                                if (!String.IsNullOrEmpty(drLegalEntity[AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName].ToString()))
                                {
                                    legalEntitySystem.FCRIRegDate = this.DateSet.ToDate(drLegalEntity[AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName].ToString(), false);
                                }

                                legalEntitySystem.EightyGNo = drLegalEntity[AppSchema.LegalEntity.EIGHTYGNOColumn.ColumnName].ToString();
                                legalEntitySystem.OtherAssociationNature = drLegalEntity[AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn.ColumnName].ToString();
                                legalEntitySystem.OtherDenomination = drLegalEntity[AppSchema.LegalEntity.OTHER_DENOMINATIONColumn.ColumnName].ToString();

                                if (dtLegalEntity.Columns.Contains(AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName))
                                {
                                    if (!String.IsNullOrEmpty(drLegalEntity[AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName].ToString()))
                                    {
                                        legalEntitySystem.EightyGNoRegDate = this.DateSet.ToDate(drLegalEntity[AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName].ToString(), false);
                                    }
                                }

                                if (legalEntitySystem.CountryId == 0)
                                {
                                    CountryName = "India";
                                    legalEntitySystem.CountryId = GetMasterId(DataSync.Country);
                                }

                                if (legalEntitySystem.CountryId == 0)
                                {
                                    resultArgs.Message = "Country Name is not available in Branch Office to import legal entity details.";
                                }
                                else
                                {
                                    legalEntitySystem.CustomerId = 0;
                                    resultArgs = IsExist(DataSync.IsLegalEntityExist);
                                    if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        legalEntitySystem.CustomerId = GetMasterId(DataSync.LegalEntity);

                                    }
                                    resultArgs = legalEntitySystem.SaveLegalEntityDetails();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Importing legal entity details." + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Legal Entity Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Projet Category from head office to branch office.
        /// </summary>
        /// <param name="dataProjectCategory"></param>
        /// <param name="dtProjectCategory"></param>
        /// <returns></returns>
        private ResultArgs ImportProjectCategory()
        {
            try
            {
                AcMELog.WriteLog("Project Category Started");
                if (dtProjects != null && dtProjects.Rows.Count != 0)
                {
                    DataView dvProjectCategory = dtProjects.DefaultView;
                    if (dtProjects.Columns.Contains("LOCATION_NAME")) { dvProjectCategory.RowFilter = "LOCATION_NAME='" + CommonMethod.EscapeLikeValue(this.Location) + "'"; }
                    if (dvProjectCategory.Count > 0)
                    {
                        foreach (DataRow drProCategory in dvProjectCategory.ToTable().Rows)
                        {
                            using (ProjectCatogorySystem projetCategorySystem = new ProjectCatogorySystem())
                            {
                                projetCategorySystem.ProjectCatogoryName = ProjectCategoryName = drProCategory[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();

                                if (dtProjects.Columns.Contains(this.AppSchema.ProjectCatogoryITRGroup.PROJECT_CATOGORY_ITRGROUPColumn.ColumnName))
                                {
                                    ProjectCategoryITRGroup = drProCategory[this.AppSchema.ProjectCatogoryITRGroup.PROJECT_CATOGORY_ITRGROUPColumn.ColumnName].ToString();
                                    projetCategorySystem.ProjectCategoryITRGroupId = GetMasterId(DataSync.ProjectCategoryITRGroup);
                                }

                                if (!string.IsNullOrEmpty(ProjectCategoryName))
                                {
                                    resultArgs = IsExist(DataSync.isProjectCategoryExist);

                                    if (resultArgs.Success)
                                    {
                                        projetCategorySystem.ProjectCatogoryId = GetMasterId(DataSync.ProjectCategory);
                                        resultArgs = projetCategorySystem.SaveProjectCatogoryDetails();
                                    }

                                    //if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger == 0)
                                    //{
                                    //    resultArgs = projetCategorySystem.SaveProjectCatogoryDetails();
                                    //}
                                }
                            }
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                    dvProjectCategory.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Importing Project category details. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Project Category Ended");
            }
            return resultArgs;
        }


        /// <summary>
        /// Import Budget Group from head office to branch office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportBudgetGroup()
        {
            try
            {
                AcMELog.WriteLog("Budget Group Started");
                if (dtBudgetGroup != null && dtBudgetGroup.Rows.Count != 0)
                {
                    foreach (DataRow drBudgetGroup in dtBudgetGroup.Rows)
                    {
                        using (BudgetSystem budgetsystem = new BudgetSystem())
                        {
                            string budgetgrp = drBudgetGroup[this.AppSchema.BudgetGroup.BUDGET_GROUPColumn.ColumnName].ToString().Trim();
                            Int32 budgetgrpsortid = 0;

                            if (dtBudgetGroup.Columns.Contains(this.AppSchema.BudgetGroup.BUDGET_GROUP_SORT_IDColumn.ColumnName))
                            {
                                budgetgrpsortid = NumberSet.ToInteger(drBudgetGroup[this.AppSchema.BudgetGroup.BUDGET_GROUP_SORT_IDColumn.ColumnName].ToString());
                            }

                            if (!string.IsNullOrEmpty(budgetgrp))
                            {
                                resultArgs = budgetsystem.SaveBudgetGroup(budgetgrp, budgetgrpsortid);
                            }
                        }
                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Importing Budget Group details. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Budget Group Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Budget Group from head office to branch office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportBudgetSubGroup()
        {
            try
            {
                AcMELog.WriteLog("Budget Sub Group Started");
                if (dtBudgetSubGroup != null && dtBudgetSubGroup.Rows.Count != 0)
                {
                    foreach (DataRow drBudgetSubGroup in dtBudgetSubGroup.Rows)
                    {
                        using (BudgetSystem budgetsystem = new BudgetSystem())
                        {
                            string budgetsubgrp = drBudgetSubGroup[this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn.ColumnName].ToString().Trim();
                            Int32 budgetsubgrpsortid = 0;

                            if (dtBudgetSubGroup.Columns.Contains(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUP_SORT_IDColumn.ColumnName))
                            {
                                budgetsubgrpsortid = NumberSet.ToInteger(drBudgetSubGroup[this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUP_SORT_IDColumn.ColumnName].ToString());
                            }

                            if (!string.IsNullOrEmpty(budgetsubgrp))
                            {
                                budgetsystem.SaveBudgetSubGroup(budgetsubgrp, budgetsubgrpsortid);
                            }
                        }
                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Importing Budget Sub Group details. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Budget Sub Group Ended");
            }
            return resultArgs;
        }


        /// <summary>
        /// On 20/02/2022, To delete Budget Group and Budget Sub Group which are not available in Acme.erp portal
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteInValidBudgetGroupAndSubGroup()
        {
            try
            {
                AcMELog.WriteLog("DeleteInValidBudgetGroupAndSubGroup Started");

                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    //For Budget Group
                    ResultArgs result = ledgersystem.FetchBudgetGroupLookup();
                    if (dtBudgetGroup != null && dtBudgetGroup.Rows.Count > 0)
                    {
                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtBG = result.DataSource.Table;
                            foreach (DataRow dr in dtBG.Rows)
                            {
                                string bgname = dr[this.AppSchema.BudgetGroup.BUDGET_GROUPColumn.ColumnName].ToString().Trim();
                                ResultArgs resultFind = common.CheckValueCotainsInDataTable(dtBudgetGroup, AppSchema.BudgetGroup.BUDGET_GROUPColumn.ColumnName, bgname);
                                if (resultFind.DataSource.Data == null && resultFind.Success)
                                {
                                    using (BudgetSystem budgetsys = new BudgetSystem())
                                    {
                                        budgetsys.DeleteBudgetGroup(bgname);
                                    }
                                }
                            }
                        }
                    }

                    //For Budget Sub Group
                    result = ledgersystem.FetchBudgetSubGroupLookup();
                    if (dtBudgetSubGroup != null && dtBudgetSubGroup.Rows.Count > 0)
                    {
                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtBSG = result.DataSource.Table;
                            foreach (DataRow dr in dtBSG.Rows)
                            {
                                string bsgname = dr[this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn.ColumnName].ToString().Trim();
                                ResultArgs resultFind = common.CheckValueCotainsInDataTable(dtBudgetSubGroup, AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn.ColumnName, bsgname);
                                if (resultFind.DataSource.Data == null && resultFind.Success)
                                {
                                    using (BudgetSystem budgetsys = new BudgetSystem())
                                    {
                                        budgetsys.DeleteBudgetSubGroup(bsgname);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in deleting Budget Group and Sub Groups which are not availble in Acmeerp portal. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("DeleteInValidBudgetGroupAndSubGroup Ended");
            }
            return resultArgs;
        }



        /// <summary>
        /// Import Project from Head office to Branch Office
        /// </summary>
        /// <param name="dataProject"></param>
        /// <param name="dtProject"></param>
        /// <returns></returns>
        private ResultArgs ImportProject()
        {
            try
            {
                AcMELog.WriteLog("Project Started");
                DataTable dtBranchHeadOfficeProjects = null;
                DataTable dtHeadOfficeProjects = null;
                if (dtProjects != null && dtProjects.Rows.Count > 0)
                {
                    DataView dvHeadOfficeProject = dtProjects.DefaultView;
                    if (dtProjects.Columns.Contains("LOCATION_NAME")) { dvHeadOfficeProject.RowFilter = "LOCATION_NAME='" + CommonMethod.EscapeLikeValue(this.Location) + "'"; }
                    if (dvHeadOfficeProject.Count > 0)
                    {
                        dtHeadOfficeProjects = dvHeadOfficeProject.ToTable();
                        resultArgs = FetchBrachHeadOfficeProjects();
                        if (resultArgs.Success)
                        {
                            dtBranchHeadOfficeProjects = resultArgs.DataSource.Table;

                            if (dtBranchHeadOfficeProjects != null && dtBranchHeadOfficeProjects.Rows.Count > 0)
                            {
                                DataTable dtMisMatchedProjects = FetchMisMatchedProjects(dtHeadOfficeProjects, dtBranchHeadOfficeProjects);
                                DataTable dtModifiedProjects = FetchModifiedProjects(dtHeadOfficeProjects, dtBranchHeadOfficeProjects);

                                if ((dtMisMatchedProjects != null && dtMisMatchedProjects.Rows.Count > 0) && (dtModifiedProjects != null && dtModifiedProjects.Rows.Count > 0))
                                {
                                    if ((dtMisMatchedProjects != null && dtMisMatchedProjects.Rows.Count > 0))
                                    {
                                        dtMisMatchedProjects.Columns.Add("PROJECT", typeof(string));
                                        this.ModifiedProjects = dtModifiedProjects;
                                        this.MisMatchedProjects = dtMisMatchedProjects;
                                        objMapMisMatchProjects.ShowDialog();
                                        if (objMapMisMatchProjects.DialogResult == DialogResult.Cancel)
                                        {
                                            resultArgs.Success = false;
                                        }
                                    }
                                    else if (dtModifiedProjects != null && dtModifiedProjects.Rows.Count > 0)
                                    {
                                        resultArgs = SaveMasterHeadOfficeProjects(dtModifiedProjects);
                                    }
                                }
                                else
                                {
                                    resultArgs = SaveMasterHeadOfficeProjects(dtHeadOfficeProjects);
                                }
                            }
                            else
                            {
                                resultArgs = SaveMasterHeadOfficeProjects(dtHeadOfficeProjects);
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Projects are not mapped to this location in Portal.";
                    }
                    dvHeadOfficeProject.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing Project details. " + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Project Endeded");
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Branch Office from Head Office to Branch Office
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportBranchOffice()
        {
            try
            {
                AcMELog.WriteLog("Start Import Head Office");

                if (settingproperty.MultiLocation == 1)
                {
                    if (dtBranchOffice != null && dtBranchOffice.Rows.Count != 0)
                    {
                        foreach (DataRow drSubBranchOffice in dtBranchOffice.Rows)
                        {
                            BranchOfficeCode = drSubBranchOffice[this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(BranchOfficeCode))
                            {
                                resultArgs = IsExist(DataSync.IsBranchOfficeExists);
                                if (resultArgs.Success)
                                {
                                    using (DataManager dataManager = new DataManager(resultArgs.DataSource.Sclar.ToInteger == 0 ? SQLCommand.SubBranchList.AddBranch : SQLCommand.SubBranchList.UpdateBranch))
                                    {
                                        BranchOfficeName = drSubBranchOffice[this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString();
                                        HeadOfficeCodeBranch = drSubBranchOffice[this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                                        DeploymentType = this.NumberSet.ToInteger(drSubBranchOffice[this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName].ToString());
                                        Address = drSubBranchOffice[this.AppSchema.BranchOffice.ADDRESSColumn.ColumnName].ToString();
                                        Pcode = drSubBranchOffice[this.AppSchema.BranchOffice.PINCODEColumn.ColumnName].ToString();
                                        PhoneNumber = drSubBranchOffice[this.AppSchema.BranchOffice.PHONE_NOColumn.ColumnName].ToString();
                                        MobileNumber = drSubBranchOffice[this.AppSchema.BranchOffice.MOBILE_NOColumn.ColumnName].ToString();
                                        BranchEMailId = drSubBranchOffice[this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn.ColumnName].ToString();
                                        Status = this.NumberSet.ToInteger(drSubBranchOffice[this.AppSchema.BranchOffice.STATUSColumn.ColumnName].ToString());
                                        City = drSubBranchOffice[this.AppSchema.BranchOffice.CITYColumn.ColumnName].ToString();
                                        BranchPartCode = drSubBranchOffice[this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn.ColumnName].ToString();
                                        CountryCode = drSubBranchOffice[this.AppSchema.BranchOffice.COUNTRY_CODEColumn.ColumnName].ToString();
                                        BranchKeyCode = drSubBranchOffice[this.AppSchema.BranchOffice.BRANCH_KEY_CODEColumn.ColumnName].ToString();

                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn, BranchOfficeName);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCodeBranch);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn, DeploymentType);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.ADDRESSColumn, Address);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.PINCODEColumn, Pcode);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.PHONE_NOColumn, PhoneNumber);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.MOBILE_NOColumn, MobileNumber);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn, BranchEMailId);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.STATUSColumn.ColumnName, Status);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.CITYColumn, City);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn, BranchPartCode);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_CODEColumn, CountryCode);
                                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_KEY_CODEColumn, BranchKeyCode);
                                        resultArgs = dataManager.UpdateData();
                                    }
                                }
                            }
                            if (!resultArgs.Success)
                            {
                                resultArgs.Message = "Exception in importing sub branch details. " + resultArgs.Message;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("BranchOffice Ended Successfully");
            }
            return resultArgs;
        }

        /// <summary>
        /// Import FC Purpose from Head office to branch office.
        /// </summary>
        /// <param name="dataPurpose"></param>
        /// <param name="dtFCPurpose"></param>
        /// <returns></returns>
        public ResultArgs ImportFCPurpose()
        {
            try
            {
                AcMELog.WriteLog("FC Purpose Started");
                if (dtFCPurpose != null && dtFCPurpose.Rows.Count != 0)
                {
                    foreach (DataRow drFCPurpose in dtFCPurpose.Rows)
                    {
                        using (PurposeSystem purposeSystem = new PurposeSystem())
                        {
                            purposeSystem.purposeCode = FCCode = (dtFCPurpose.Columns.Contains(AppSchema.Purposes.CODEColumn.ColumnName)) ? drFCPurpose[AppSchema.Purposes.CODEColumn.ColumnName].ToString() : string.Empty;
                            purposeSystem.PurposeHead = FCPurpose = drFCPurpose[AppSchema.Purposes.FC_PURPOSEColumn.ColumnName].ToString().Trim();
                            if (!string.IsNullOrEmpty(FCPurpose))
                            {
                                resultArgs = IsExist(DataSync.IsFCPurposeExists);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        purposeSystem.PurposeId = PurposeId = GetMasterId(DataSync.FCPurpose);
                                    }
                                    resultArgs = purposeSystem.SavePurposeDetails();

                                    //resultArgs = IsExist(DataSync.ISFCPurposeCodeExists);
                                    //if (resultArgs.Success)
                                    //{
                                    //    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    //    {
                                    //        resultArgs = FetchPurposeCodes();
                                    //        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                    //        {
                                    //            purposeSystem.purposeCode = CommonMethod.GetPredictedCode(FCCode, resultArgs.DataSource.Table);
                                    //        }
                                    //    }
                                    //}
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            resultArgs.Message = "Problem in ImportFCPurpose: " + resultArgs.Message;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing FC purposes. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FC Purpose Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Governing Bodies Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportGoverningBodies()
        {
            try
            {
                AcMELog.WriteLog("Governing Bodies details Started");
                resultArgs = DeleteGoverningMember();
                if (resultArgs.Success)
                {
                    if (dtGoverningBodies != null && dtGoverningBodies.Rows.Count > 0)
                    {
                        using (ExecutiveMemberSystem Members = new ExecutiveMemberSystem())
                        {
                            foreach (DataRow drGoveringBodies in dtGoverningBodies.Rows)
                            {
                                Members.ExecutiveName = ExecutiveMember = drGoveringBodies[AppSchema.ExecutiveMembers.EXECUTIVEColumn.ColumnName].ToString();
                                Members.ExecutiveId = GetMasterId(DataSync.ExecutiveMember);
                                Members.FatherName = drGoveringBodies[AppSchema.ExecutiveMembers.NAMEColumn.ColumnName].ToString();
                                Members.DateOfBirth = drGoveringBodies[AppSchema.ExecutiveMembers.DATE_OF_BIRTHColumn.ColumnName].ToString();
                                Members.Religion = drGoveringBodies[AppSchema.ExecutiveMembers.RELIGIONColumn.ColumnName].ToString().ToString();
                                Members.Role = drGoveringBodies[AppSchema.ExecutiveMembers.ROLEColumn.ColumnName].ToString();
                                Members.Nationality = drGoveringBodies[AppSchema.ExecutiveMembers.NATIONALITYColumn.ColumnName].ToString();
                                Members.Occupation = drGoveringBodies[AppSchema.ExecutiveMembers.OCCUPATIONColumn.ColumnName].ToString();
                                Members.Association = drGoveringBodies[AppSchema.ExecutiveMembers.ASSOCIATIONColumn.ColumnName].ToString();
                                Members.OfficeBearer = drGoveringBodies[AppSchema.ExecutiveMembers.OFFICE_BEARERColumn.ColumnName].ToString();
                                Members.Place = drGoveringBodies[AppSchema.ExecutiveMembers.PLACEColumn.ColumnName].ToString();
                                Members.State = drGoveringBodies[AppSchema.ExecutiveMembers.STATEColumn.ColumnName].ToString();
                                CountryName = drGoveringBodies[AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                                Members.CountryId = GetMasterId(DataSync.Country);
                                Members.Address = drGoveringBodies[AppSchema.ExecutiveMembers.ADDRESSColumn.ColumnName].ToString();
                                Members.PinCode = drGoveringBodies[AppSchema.ExecutiveMembers.PIN_CODEColumn.ColumnName].ToString();
                                Members.Pan_SSN = drGoveringBodies[AppSchema.ExecutiveMembers.PAN_SSNColumn.ColumnName].ToString();
                                Members.Phone = drGoveringBodies[AppSchema.ExecutiveMembers.PHONEColumn.ColumnName].ToString();
                                Members.Fax = drGoveringBodies[AppSchema.ExecutiveMembers.FAXColumn.ColumnName].ToString();
                                Members.Email = drGoveringBodies[AppSchema.ExecutiveMembers.EMAILColumn.ColumnName].ToString();
                                Members.URL = drGoveringBodies[AppSchema.ExecutiveMembers.URLColumn.ColumnName].ToString();
                                Members.DateOfAppointment = drGoveringBodies[AppSchema.ExecutiveMembers.DATE_OF_APPOINTMENTColumn.ColumnName].ToString();
                                Members.DateOfExit = drGoveringBodies[AppSchema.ExecutiveMembers.DATE_OF_EXITColumn.ColumnName].ToString();
                                Members.Notes = drGoveringBodies[AppSchema.ExecutiveMembers.NOTESColumn.ColumnName].ToString();
                                SocietyName = drGoveringBodies[AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString();
                                Members.LegalEntityId = GetMasterId(DataSync.LegalEntity);
                                State = drGoveringBodies[AppSchema.ExecutiveMembers.STATEColumn.ColumnName].ToString();
                                Members.StateId = GetMasterId(DataSync.State);
                                if (Members.CountryId == 0)
                                {
                                    resultArgs.Message = "Country Name is not available in Branch Office to import Governing Members.";
                                }

                                resultArgs = IsExist(DataSync.IsLegalEntityExist);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        resultArgs = Members.SaveExecutiveMemberDetails();
                                    }
                                }
                                if (!resultArgs.Success)
                                {
                                    resultArgs.Message = "Problem in Governing Members Details: " + resultArgs.Message;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Importing Governing Bodies details." + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Governing Bodies Ended");
            }
            return resultArgs;
        }

        private ResultArgs DeleteGoverningMember()
        {
            using (ExecutiveMemberSystem MemberSystem = new ExecutiveMemberSystem())
            {
                resultArgs = MemberSystem.DeleteGoveringDetails();
            }
            return resultArgs;
        }
        /// <summary>
        /// Import TDS SECTION from Head office to branch office.
        /// </summary>
        /// <param name="dataTDSSection"></param>
        /// <param name="dtTDSSection"></param>
        /// <returns></returns>
        private ResultArgs ImportTDSSection()
        {
            try
            {
                AcMELog.WriteLog("ImportTDSSection Started");
                if (dtTDSSection != null && dtTDSSection.Rows.Count != 0)
                {
                    using (TDSSectionSystem tdsSection = new TDSSectionSystem())
                    {
                        foreach (DataRow drTDSSection in dtTDSSection.Rows)
                        {
                            tdsSection.Code = drTDSSection[AppSchema.TDSSection.CODEColumn.ColumnName].ToString();
                            tdsSection.Name = TDSSectionName = drTDSSection[AppSchema.TDSSection.SECTION_NAMEColumn.ColumnName].ToString();
                            tdsSection.IsActive = this.NumberSet.ToInteger(drTDSSection[AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString());

                            if (!string.IsNullOrEmpty(TDSSectionName))
                            {
                                resultArgs = IsExist(DataSync.IsTDSSectionExists);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        tdsSection.TDS_section_Id = GetMasterId(DataSync.TDSSection);
                                    }
                                    resultArgs = tdsSection.SaveTDSSection();
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                resultArgs.Message = "Problem in Import TDS Section: " + resultArgs.Message;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing TDS Section details. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FC Purpose Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Import TDS NATURE OF PAYMENTS from Head office to branch office.
        /// </summary>
        /// <param name="dataTDSNatureOfPayments"></param>
        /// <param name="dtNatureOfPayments"></param>
        /// <returns></returns>
        private ResultArgs ImportTDSNatureOfPayments()
        {
            try
            {
                AcMELog.WriteLog("ImportNatureOfPayments Started");
                if (dtTDSNatureOfPayments != null && dtTDSNatureOfPayments.Rows.Count != 0)
                {
                    using (NatureofPaymentsSystem natureOfPayment = new NatureofPaymentsSystem())
                    {
                        foreach (DataRow drNatureOfPayment in dtTDSNatureOfPayments.Rows)
                        {
                            natureOfPayment.PaymentCode = drNatureOfPayment[AppSchema.NatureofPayment.PAYMENT_CODEColumn.ColumnName].ToString();
                            natureOfPayment.PaymentName = PaymentName = drNatureOfPayment[AppSchema.NatureofPayment.PAYMENT_NAMEColumn.ColumnName].ToString();
                            TDSSectionName = drNatureOfPayment[AppSchema.TDSSection.SECTION_NAMEColumn.ColumnName].ToString();
                            natureOfPayment.TdsSectionID = GetMasterId(DataSync.TDSSection);
                            natureOfPayment.Notes = drNatureOfPayment[AppSchema.NatureofPayment.NOTESColumn.ColumnName].ToString();
                            natureOfPayment.IsActive = this.NumberSet.ToInteger(drNatureOfPayment[this.AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString());

                            //Validations
                            if (!string.IsNullOrEmpty(PaymentName))
                            {
                                resultArgs = IsExist(DataSync.IsTDSNatureOfPaymentExists);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        natureOfPayment.NatureofPaymentId = GetMasterId(DataSync.NatureOfPayment);
                                    }
                                    resultArgs = natureOfPayment.SavePaymentDetails();
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                resultArgs.Message = "Problem in Import TDS Nature of payment: " + resultArgs.Message;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing TDS Nature of Payment details. " + ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Import Nature of Payment Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Import TDS DEDUCTEE TYPES from Head office to branch office.
        /// </summary>
        /// <param name="dataTDSNatureOfPayments"></param>
        /// <param name="dtNatureOfPayments"></param>
        /// <returns></returns>
        private ResultArgs ImportTDSDeducteeTypes()
        {
            try
            {
                AcMELog.WriteLog("ImportTDSDeducteeTypes Started");
                if (dtTDSDeducteeTypes != null && dtTDSDeducteeTypes.Rows.Count != 0)
                {
                    using (DeducteeTypeSystem deducteeType = new DeducteeTypeSystem())
                    {
                        foreach (DataRow drDeducteeType in dtTDSDeducteeTypes.Rows)
                        {
                            deducteeType.ResidentialStatus = this.NumberSet.ToInteger(drDeducteeType[AppSchema.DeducteeTypes.RESIDENTIAL_STATUSColumn.ColumnName].ToString());
                            deducteeType.DeducteeStatus = this.NumberSet.ToInteger(drDeducteeType[AppSchema.DeducteeTypes.DEDUCTEE_TYPEColumn.ColumnName].ToString());
                            deducteeType.DeducteeName = DeducteeName = drDeducteeType[AppSchema.DeducteeTypes.NAMEColumn.ColumnName].ToString();
                            deducteeType.status = this.NumberSet.ToInteger(drDeducteeType[this.AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString());

                            //Validations
                            if (!string.IsNullOrEmpty(DeducteeName))
                            {
                                resultArgs = IsExist(DataSync.IsDeducteeTypeExists);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        deducteeType.DeducteeTypeid = GetMasterId(DataSync.TDSDeducteeType);
                                    }
                                    resultArgs = deducteeType.SaveDeducteeDetails();
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                resultArgs.Message = "Problem in Import Deductee Type: " + resultArgs.Message;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing TDS Deductee Type details. " + ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Import Deductee Type Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Import TDS DEDUCTEE TYPES from Head office to branch office.
        /// </summary>
        /// <param name="dataTDSNatureOfPayments"></param>
        /// <param name="dtNatureOfPayments"></param>
        /// <returns></returns>
        private ResultArgs ImportDutyTax()
        {
            try
            {
                AcMELog.WriteLog("ImportDutyTax Started");
                if (dtTDSDutyTax != null && dtTDSDutyTax.Rows.Count != 0)
                {
                    using (DeducteeTaxSystem dutyTax = new DeducteeTaxSystem())
                    {
                        foreach (DataRow drDutyTax in dtTDSDutyTax.Rows)
                        {
                            // dutyTax.TaxTypeId
                            dutyTax.TaxTypeName = TaxTypeName = drDutyTax[this.AppSchema.DutyTaxType.TAX_TYPE_NAMEColumn.ColumnName].ToString();
                            dutyTax.IsActive = this.NumberSet.ToInteger(drDutyTax[AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString());

                            //Validations
                            if (!string.IsNullOrEmpty(TaxTypeName))
                            {
                                resultArgs = IsExist(DataSync.IsDutyTaxExists);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        dutyTax.TaxTypeId = GetMasterId(DataSync.DutyTax);
                                    }
                                    resultArgs = dutyTax.SaveTaxTypeDetails();
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                resultArgs.Message = "Problem in Import Duty Tax: " + resultArgs.Message;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing Duty Tax Type details. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Import ImportDutyTax Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Import TDS TAX POLICY from Head office to branch office.
        /// </summary>
        /// <param name="dataTDSNatureOfPayments"></param>
        /// <param name="dtNatureOfPayments"></param>
        /// <returns></returns>
        private ResultArgs ImportTDSTaxPolicy()
        {
            try
            {
                AcMELog.WriteLog("ImportTDSTaxPolicy Started");
                using (DeducteeTaxSystem TdSPolicy = new DeducteeTaxSystem())
                {
                    if (dtTDSPolicyDeductees != null && dtTDSPolicyDeductees.Rows.Count > 0)
                    {
                        foreach (DataRow drDeducteeType in dtTDSPolicyDeductees.Rows)
                        {
                            DeducteeName = drDeducteeType[this.AppSchema.DeducteeTypes.DEDUCTEE_TYPEColumn.ColumnName].ToString();
                            TdSPolicy.DeducteeTypeId = DeducteeId = GetMasterId(DataSync.TDSDeducteeType);
                            if (DeducteeId > 0)
                            {
                                using (DataManager dataManager = new DataManager())
                                {
                                    resultArgs = TdSPolicy.DeleteTaxDetails(dataManager);
                                }

                                if (resultArgs != null && resultArgs.Success)
                                {
                                    if (dtTDSPolicy != null && dtTDSPolicy.Rows.Count != 0)
                                    {
                                        DataView dvTDSPolicy = dtTDSPolicy.Copy().DefaultView;
                                        dvTDSPolicy.RowFilter = "DEDUCTEE_TYPE='" + CommonMethod.EscapeLikeValue(DeducteeName) + "'";
                                        if (dvTDSPolicy.Count > 0)
                                        {
                                            foreach (DataRow drTDSPolicy in dvTDSPolicy.ToTable().Rows)
                                            {
                                                PaymentName = drTDSPolicy[this.AppSchema.NatureofPayment.PAYMENT_NAMEColumn.ColumnName].ToString();
                                                TdSPolicy.TaxPolicyId = DeducteeId;
                                                TdSPolicy.NaturePaymentId = NaturePaymentId = GetMasterId(DataSync.NatureOfPayment);
                                                TdSPolicy.ApplicableFrom = TaxApplicableFrom = this.DateSet.ToDate(drTDSPolicy[this.AppSchema.DutyTax.APPLICABLE_FROMColumn.ColumnName].ToString(), false);
                                                TempTaxPolicyId = this.NumberSet.ToInteger(drTDSPolicy[this.AppSchema.DutyTax.TDS_POLICY_IDColumn.ColumnName].ToString());

                                                if (DeducteeId > 0 && NaturePaymentId > 0 && TaxApplicableFrom != null)
                                                {
                                                    resultArgs = TdSPolicy.SaveDeducteeTaxDetails();
                                                    if (resultArgs.Success)
                                                    {
                                                        PolicyId = this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                                        resultArgs = ImportTDSTaxRateDetails();
                                                    }
                                                }
                                                if (!resultArgs.Success)
                                                {
                                                    resultArgs.Message = "Problem in Import Tax Policy : " + resultArgs.Message;
                                                    break;
                                                }
                                            }
                                        }
                                        dvTDSPolicy.RowFilter = "";
                                    }
                                }
                                if (!resultArgs.Success)
                                {
                                    resultArgs.Message = "Problem in Import Tax Policy : " + resultArgs.Message;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing Tax Policy details. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Import ImportTDSTaxPolicy Ended");
            }
            return resultArgs;
        }

        private ResultArgs ImportTDSTaxRateDetails()
        {
            try
            {
                if (dtTDSTaxRate != null && dtTDSTaxRate.Rows.Count > 0)
                {
                    DataView dvTaxRate = dtTDSTaxRate.DefaultView;
                    dvTaxRate.RowFilter = "TDS_POLICY_ID=" + TempTaxPolicyId;
                    if (dvTaxRate.Count > 0)
                    {
                        using (DeducteeTaxSystem TdSPolicy = new DeducteeTaxSystem())
                        {
                            foreach (DataRow drTaxRate in dvTaxRate.ToTable().Rows)
                            {
                                TdSPolicy.TaxPolicyId = PolicyId;
                                TdSPolicy.Rate = this.NumberSet.ToDecimal(drTaxRate[this.AppSchema.DutyTax.TDS_RATEColumn.ColumnName].ToString());
                                TdSPolicy.ExemptionLimit = this.NumberSet.ToDecimal(drTaxRate[this.AppSchema.DutyTax.TDS_EXEMPTION_LIMITColumn.ColumnName].ToString());
                                TaxTypeName = drTaxRate[this.AppSchema.DutyTaxType.TAX_TYPE_NAMEColumn.ColumnName].ToString();
                                TdSPolicy.TaxTypeId = GetMasterId(DataSync.DutyTax);

                                resultArgs = TdSPolicy.SaveTaxRateDetailsByType();
                                if (!resultArgs.Success)
                                {
                                    resultArgs.Message = "Problem in Import Tax Policy : " + resultArgs.Message;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Fetch Collection of Branch Office Ledger 
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchBrachHeadOfficeLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.FetchHeadOfficeLedgers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Get MisMatched Ledger by sending HeadOffice Ledger and Branch Office Ledger
        /// </summary>
        /// <param name="dtHeadOfficeLedgers"></param>
        /// <param name="dtBrachHeadOfficeLedgers"></param>
        /// <returns></returns>
        public DataTable FetchMisMatchedLedgers(DataTable dtHeadOfficeLedgers, DataTable dtBrachHeadOfficeLedgers)
        {
            DataTable dtLedgers = new DataTable();

            var matched = from table1 in dtBrachHeadOfficeLedgers.AsEnumerable()
                          join table2 in dtHeadOfficeLedgers.AsEnumerable() on table1.Field<string>("HEADOFFICE_LEDGER_NAME") equals table2.Field<string>("LEDGER_NAME")
                          // where table1.Field<string>("LEDGER_GROUP") != table2.Field<string>("LEDGER_GROUP") //|| table1.Field<int>("ColumnB") != table2.Field<int>("ColumnB")
                          //where table1.Field<UInt32>("ACCESS_FLAG") == 0 && table2.Field<UInt32>("ACCESS_FLAG") == 0
                          select table1;


            var missing = from table1 in dtBrachHeadOfficeLedgers.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtLedgers = missing.CopyToDataTable();

                //On 22/08/2020, to skip default Ledgers ------------------------------------------------------------
                if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                {
                    string defaultledgers = this.GetDefaultLedgersNamesCondition;
                    dtLedgers.DefaultView.RowFilter = "ACCESS_FLAG = 0 AND HEADOFFICE_LEDGER_NAME NOT IN (" + defaultledgers + ")";
                    //ACCESS_FLAG = 0 AND HEADOFFICE_LEDGER_NAME NOT IN ('" + SettingProperty.TDS_ON_FD_INTEREST_LEDGER + "'," +
                    //                    "'" + SettingProperty.CGST_LEDGER + "','" + SettingProperty.SGST_LEDGER + "','" + SettingProperty.IGST_LEDGER + "')";

                    dtLedgers = dtLedgers.DefaultView.ToTable();
                }
                //---------------------------------------------------------------------------------------------------
            }
            return dtLedgers;
        }

        /// <summary>
        /// Get Modify Ledger by sending HeadOffice Ledger and Branch Office Ledgers
        /// </summary>
        /// <param name="dtHeadOfficeLedgers"></param>
        /// <param name="dtBrachHeadOfficeLedgers"></param>
        /// <returns></returns>
        public DataTable FetchModifiedLedgers(DataTable dtHeadOfficeLedgers, DataTable dtBrachHeadOfficeLedgers)
        {
            DataTable dtLedgers = null;

            var matched = from table1 in dtHeadOfficeLedgers.AsEnumerable()
                          join table2 in dtBrachHeadOfficeLedgers.AsEnumerable() on table1.Field<string>("LEDGER_NAME") equals table2.Field<string>("HEADOFFICE_LEDGER_NAME")
                          //where table1.Field<string>("LEDGER_GROUP") != table2.Field<string>("LEDGER_GROUP")
                          select table1;


            var missing = from table1 in dtHeadOfficeLedgers.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtLedgers = missing.CopyToDataTable();
            }
            return dtLedgers;
        }

        private ResultArgs MapProjectLedger()
        {
            DataTable dtHeadOfficeProjects = null;
            DataTable dtHeadOfficeLedgers = null;
            dtHeadOfficeProjects = dtProjects;
            dtHeadOfficeLedgers = dtLedger;
            if (dtHeadOfficeProjects != null && dtHeadOfficeProjects.Rows.Count > 0)
            {
                foreach (DataRow drProject in dtHeadOfficeProjects.Rows)
                {
                    ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                    ProjectCategoryName = drProject[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();
                    if (!string.IsNullOrEmpty(ProjectName))
                    {
                        ProjectId = GetMasterId(DataSync.Project);
                        if (ProjectId > 0)
                        {
                            //On 28/01/2021, to unmap unused project ledgers and unmap unused project budget ledgers
                            resultArgs = DeleteUnusedAllMappedLedgersByProject();

                            if (dtHeadOfficeLedgers != null && dtHeadOfficeLedgers.Rows.Count > 0 && resultArgs.Success)
                            {
                                if (dtHeadOfficeLedgers.Columns.Contains(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName))
                                {
                                    DataView dvLedger = dtHeadOfficeLedgers.DefaultView;
                                    dvLedger.RowFilter = "PROJECT_CATOGORY_NAME LIKE '%" + CommonMethod.EscapeLikeValue(ProjectCategoryName) + "%'";
                                    if (dvLedger != null && dvLedger.Count > 0)
                                    {
                                        foreach (DataRow drLedger in dvLedger.ToTable().Rows)
                                        {
                                            LedgerName = drLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                            LedgerId = GetMasterId(DataSync.Ledger);

                                            if (ProjectId > 0 && LedgerId > 0)
                                            {
                                                resultArgs = MapAllProjectLedger();
                                            }

                                            //On 28/01/2021
                                            if (!resultArgs.Success)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    dvLedger.RowFilter = "";
                                }
                            }

                            //On 28/01/2021
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs RemoveUnUsedHeadOfficeLedgers()
        {
            DataTable dtBranchHeadOfficeLedgers = null;
            if (dtLedger != null && dtLedger.Rows.Count > 0)
            {
                DataTable dtHeadOfficeLedgers = dtLedger;
                resultArgs = FetchBrachHeadOfficeLedgers();
                int count = dtLedger.Rows.Count;
                if (resultArgs.Success)
                {
                    dtBranchHeadOfficeLedgers = resultArgs.DataSource.Table;
                    int rowcount = dtBranchHeadOfficeLedgers.Rows.Count;
                    if (dtBranchHeadOfficeLedgers != null && dtBranchHeadOfficeLedgers.Rows.Count > 0)
                    {
                        DataTable dtMisMatchedLedgers = FetchMisMatchedLedgers(dtHeadOfficeLedgers, dtBranchHeadOfficeLedgers);
                        if (dtMisMatchedLedgers != null && dtMisMatchedLedgers.Rows.Count > 0)
                        {
                            foreach (DataRow drLedger in dtMisMatchedLedgers.Rows)
                            {
                                int HeadOfficeLedgerId = this.NumberSet.ToInteger(drLedger["HEADOFFICE_LEDGER_ID"].ToString());

                                //On 22/08/2020, to check unused leder is default ledger
                                int Accessflag = (drLedger["ACCESS_FLAG"] != null ? this.NumberSet.ToInteger(drLedger["ACCESS_FLAG"].ToString()) : 0);
                                string HOLdgerName = drLedger["HEADOFFICE_LEDGER_NAME"].ToString();
                                bool isDefaultLedger = CheckDefaultLedgerName(HOLdgerName);

                                //if (HeadOfficeLedgerId > 0)
                                if (HeadOfficeLedgerId > 0 && Accessflag == 0 && isDefaultLedger == false)
                                {
                                    LedgerId = HeadOfficeLedgerId;
                                    resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteHeadOfficeMappedLedger);
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteHeadOfficeLedger);
                                    }
                                }


                                string HeadOfficeLedgerName = drLedger["HEADOFFICE_LEDGER_NAME"].ToString();
                                //if (!string.IsNullOrEmpty(HeadOfficeLedgerName))
                                if (!string.IsNullOrEmpty(HeadOfficeLedgerName) && Accessflag == 0 && isDefaultLedger == false)
                                {
                                    resultArgs = FetchLedgerIdByLedgerName(HeadOfficeLedgerName);
                                    if (resultArgs.Success)
                                    {
                                        if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                        {
                                            LedgerId = resultArgs.DataSource.Sclar.ToInteger;
                                            if (LedgerId > 3)  //1-Cash, 2- Fixed Deposit, 3 - Capital Fund
                                            {
                                                bool hasVouchers = HasLedgerEntries(LedgerId);
                                                if (!hasVouchers)
                                                {
                                                    resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteProjectMappedLedger);
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteLedgerBalance);
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteBudgetLedger);
                                                            if (resultArgs.Success)
                                                            {
                                                                resultArgs = DeleteVouchers();
                                                                if (resultArgs.Success)
                                                                {
                                                                    resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteTDSCreditorProfile);
                                                                    if (resultArgs.Success)
                                                                    {
                                                                        resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteHeadOfficeMappedLedger);
                                                                        if (resultArgs.Success)
                                                                        {
                                                                            resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteProjectCategoryByLedgerId);
                                                                            if (resultArgs.Success)
                                                                            {
                                                                                resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteMasterLedger);
                                                                            }
                                                                            if (resultArgs.Success)
                                                                            {
                                                                                resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteBankAccountByLedger);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 28/01/2021 - UnMapp Unused Project Ledgers when we import master with allow mapping 
        /// 
        /// If will delete/unmapp project ledger except the following cases
        /// 
        /// 1. There are Ledgers, do not have Voucher entries
        /// 2. There are Ledgers, do not have Opening Balance (
        ///  Few Ledges will not have Vouchers but they may have Opening Balance alone, It should not be unmapped)
        /// 3. There are Ledges (Interest Ledgers), not used as Intestet Ledgers in FD module 
        /// 4. There are Ledges, not Budgeted for the Project
        /// 5. There are Ledges, do not have Budget Sub Ledgers
        /// 5. Few Ledgers may have Budget but not have Vouchers, It should not un-Mapp6. Few Ledgers may mapp fpr Budget Ledgers but not used for Vouchers and Budget, It should un map both Project Ledger and Budget Ledger7. Strictly Cash/Bank/FD Ledgers and Default Ledgers should not be unmapped 8. If Unmap in Project Ledger, should also unmap Budget Ledger
        /// 
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteUnusedAllMappedLedgersByProject()
        {
            if (ProjectLedgerMapping && ProjectId > 0)
            {
                //# Delete or UnMapp Project Ledgers
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteUnusedAllMappedLedgersByProject))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }

                //# Delete or UnMapp Project Budget Ledgers
                if (resultArgs.Success)
                {
                    resultArgs = DeleteUnusedAllBudgetMappedLedgersByProject();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// DeleteLedger
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteLedgerNotExistsPortal()
        {
            if (dtLocalLedger != null && dtLocalLedger.Rows.Count > 0)
            {
                foreach (DataRow drLedger in dtLocalLedger.Rows)
                {
                    int LedgerId = this.NumberSet.ToInteger(drLedger["LEDGER_ID"].ToString());

                    bool hasVouchers = HasLedgerEntries(LedgerId);
                    if (!hasVouchers)
                    {
                        using (LedgerSystem ledgersystem = new LedgerSystem())
                        {
                            resultArgs = ledgersystem.DeleteLedgerDetails(LedgerId);
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// if it is HO Ledger,Make it Zero and if it Branch Ledger make it 1
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateBranchLedgerFlag()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.UpdateLocalHeadofficeLedgerFlag))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        ///  fetch Local Ledgera
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchBranchLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.FetchBranchLedgerLocalDatabase))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        /// <summary>
        /// DeleteLedgerGroup
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteLedgerGroupNotExistsPortal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteLedgerGroupNotExistPortal))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Update the Parent Group and Main Group Id's is not available in the Group Id 
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateGroupParentMainParent()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.UpdateParentGroupMainGroupwithGroupId))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 28/01/2021 - UnMapp Unused Project Budget Ledgers when we import master with allow mapping 
        /// 
        /// If will delete/unmapp project Budget ledger except the following cases
        /// 
        /// 1. There are Ledges, not Budgeted for the Project
        /// 2. There are Ledges, do not have Budget Sub Ledgers
        /// 3. Few Ledgers may have Budget but not have Vouchers, It should not un-Mapp6. Few Ledgers may mapp fpr Budget Ledgers but not used for Vouchers and Budget, It should un map both Project Ledger and Budget Ledger7. Strictly Cash/Bank/FD Ledgers and Default Ledgers should not be unmapped 8. If Unmap in Project Ledger, should also unmap Budget Ledger
        /// 
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteUnusedAllBudgetMappedLedgersByProject()
        {
            if (ProjectLedgerMapping && ProjectId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteUnusedAllBudgetMappedLedgersByProject))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteVouchers()
        {
            string voucherId = string.Empty;
            resultArgs = FetchVoucherIdByLedgerId();
            if (resultArgs.Success)
            {
                if (!string.IsNullOrEmpty(resultArgs.DataSource.Sclar.ToString))
                {
                    voucherId = resultArgs.DataSource.Sclar.ToString;

                    resultArgs = RemoveVoucherByVoucherId(voucherId);
                }
            }
            return resultArgs;
        }

        public ResultArgs RemoveVoucherByVoucherId(string voucherId)
        {
            resultArgs = DeleteVoucherCcTrans(voucherId);
            if (resultArgs.Success)
            {
                resultArgs = DeleteVoucherTrans(voucherId);
                if (resultArgs.Success)
                {
                    resultArgs = DeleteVoucherMasterTrans(voucherId);
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchVoucherIdByLedgerId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.FetchVoucherIdByLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucherCcTrans(string vId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteVoucherCostCenter))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, vId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucherTrans(string vId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteVoucherTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, vId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucherMasterTrans(string vId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteVoucherMasterTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, vId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Save Ledger Details
        /// </summary>
        /// <param name="dtLedger"></param>
        /// <returns></returns>
        public ResultArgs SaveMasterHeadOfficeLedgers()    //DataTable dtLedger)
        {

            if (dtLedger != null && dtLedger.Rows.Count > 0)
            {
                DataView dvLedger = dtLedger.DefaultView;
                if (dtLedger.Columns.Contains("LOCATION_NAME")) { dvLedger.RowFilter = "LOCATION_NAME LIKE '%" + CommonMethod.EscapeLikeValue(this.Location) + "%'"; }
                if (dvLedger.Count > 0)
                {
                    foreach (DataRow drLedger in dvLedger.ToTable().Rows)
                    {
                        using (LedgerSystem ledgerSystem = new LedgerSystem())
                        {
                            LedgerName = drLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(LedgerName))
                            {
                                LedgerGroup = drLedger[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                                ledgerSystem.GroupId = GetMasterId(DataSync.LedgerGroup);
                                if (ledgerSystem.GroupId != (int)FixedLedgerGroup.BankAccounts)
                                {
                                    ledgerSystem.LedgerCode = drLedger[this.AppSchema.Ledger.LEDGER_CODEColumn.ColumnName].ToString();
                                    ledgerSystem.LedgerName = LedgerName = drLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();

                                    ledgerSystem.LedgerType = drLedger[this.AppSchema.Ledger.LEDGER_TYPEColumn.ColumnName].ToString();
                                    ledgerSystem.LedgerSubType = drLedger[this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn.ColumnName].ToString();
                                    ledgerSystem.BankAccountId = 0;
                                    //ledgerSystem.IsCostCentre = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                                    //ledgerSystem.IsBankInterestLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn.ColumnName].ToString());
                                    ledgerSystem.LedgerNotes = drLedger[this.AppSchema.Ledger.NOTESColumn.ColumnName].ToString();
                                    ledgerSystem.SortId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.SORT_IDColumn.ColumnName].ToString());
                                    ledgerSystem.IsFCRAAccount = 0;    //this.NumberSet.ToInteger(drLedger[this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn.ColumnName].ToString());
                                    //ledgerSystem.IsTDSLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());

                                    //13/02/2020, to get Budget Group/Budget Sub Group --------------------------------------------------------------------

                                    BudgetGroup = string.Empty;
                                    ledgerSystem.BudgetGroupId = 0;
                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.BUDGET_GROUP_IDColumn.ColumnName) &&
                                        drLedger.Table.Columns.Contains(this.AppSchema.BudgetGroup.BUDGET_GROUPColumn.ColumnName))
                                    {
                                        if (drLedger[this.AppSchema.BudgetGroup.BUDGET_GROUPColumn.ColumnName] != null)
                                        {
                                            BudgetGroup = drLedger[this.AppSchema.BudgetGroup.BUDGET_GROUPColumn.ColumnName].ToString();
                                            ledgerSystem.BudgetGroupId = GetMasterId(DataSync.BudgetGroup);
                                        }
                                        //ledgerSystem.BudgetGroupId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.BUDGET_GROUP_IDColumn.ColumnName].ToString());
                                    }

                                    BudgetSubGroup = string.Empty;
                                    ledgerSystem.BudgetSubGroupId = 0;
                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn.ColumnName) &&
                                        drLedger.Table.Columns.Contains(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn.ColumnName))
                                    {
                                        if (drLedger[this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn.ColumnName] != null)
                                        {
                                            BudgetSubGroup = drLedger[this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn.ColumnName].ToString();
                                            ledgerSystem.BudgetSubGroupId = GetMasterId(DataSync.BudgetSubGroup);
                                        }
                                        //ledgerSystem.BudgetSubGroupId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn.ColumnName].ToString());
                                    }
                                    //-------------------------------------------------------------------------------------------------------------------------------------------

                                    //24/11/2021 Enable ledger options ----------------------------------------------------------------------------------------------------------
                                    //For new Ledger no problem, it will just update ledger options
                                    //For existing ledgers, It will enable only from portal, it will not disable becasue branch may have ledger option related vouchers
                                    //Later it should check each options vouchers entries and disable it.
                                    ledgerSystem.IsCostCentre = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                                    ledgerSystem.IsBankInterestLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn.ColumnName].ToString());
                                    ledgerSystem.IsTDSLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());
                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsDepriciationLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn.ColumnName].ToString());
                                    }

                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsAssetGainLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn.ColumnName].ToString());
                                    }

                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsAssetLossLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn.ColumnName].ToString());
                                    }

                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsAssetDisposalLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn.ColumnName].ToString());
                                    }

                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_INKIND_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsInKindLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_INKIND_LEDGERColumn.ColumnName].ToString());
                                    }

                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsSubsidyLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn.ColumnName].ToString());
                                    }

                                    //19/07/2022 for Bank SB Interest Ledger and Bank Commission Ledgers ------------------------------------------
                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsBankSBInterestLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn.ColumnName].ToString());
                                    }

                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsBankCommissionLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn.ColumnName].ToString());
                                    }
                                    //-------------------------------------------------------------------------------------------------------------

                                    //On 09/09/2022, for Bank FD penalty Ledgers --------------------------------------------------------------------
                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName))
                                    {
                                        ledgerSystem.IsBankFDPenaltyLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName].ToString());
                                    }
                                    //----------------------------------------------------------------------------------------------------------------

                                    //On 09/11/2022 - GST HSN/SAC Code -------------------------------------------------------------------------------
                                    ledgerSystem.GST_HSN_SAC_CODE = string.Empty;
                                    //if (ledgerSystem.isGSTApplicable)
                                    //{
                                    ledgerSystem.GST_HSN_SAC_CODE = string.Empty;
                                    //}
                                    //-----------------------------------------------------------------------------------------------------------------

                                    //On 07/10/2024, Currency Details  ---------------------------------------------------------------------------------
                                    ledgerSystem.LedgerCurrencyCountryId = 0;
                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName))
                                    {
                                        ledgerSystem.LedgerCurrencyCountryId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName].ToString());
                                    }
                                    //------------------------------------------------------------------------------------------------------------------

                                    //Ledger Closed Date, If vouchers exits, dont update it------------------------------------------------------------
                                    ledgerSystem.LedgerDateClosed = DateTime.MinValue;
                                    if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn.ColumnName))
                                    {
                                        if (drLedger[this.AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn.ColumnName] != null
                                            && !string.IsNullOrEmpty(drLedger[this.AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn.ColumnName].ToString()))
                                        {
                                            ledgerSystem.LedgerDateClosed = DateSet.ToDate(drLedger[this.AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn.ColumnName].ToString(), false);

                                            using (LedgerSystem ledgersystem = new LedgerSystem())
                                            {
                                                resultArgs = FetchLedgerIdByLedgerName(LedgerName);
                                                if (resultArgs.Success)
                                                {
                                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                                    {
                                                        int ledgerid = resultArgs.DataSource.Sclar.ToInteger;

                                                        ResultArgs result = ledgersystem.CheckLedgerClosedDate(ledgerid, ledgerSystem.LedgerDateClosed);
                                                        if (!result.Success)
                                                        {
                                                            ledgerSystem.LedgerDateClosed = DateTime.MinValue;
                                                            NotUpdtaedLedgerClosedDate += LedgerName + ",";
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }

                                    //O4/07/2023, To update closed by 
                                    ledgerSystem.LedgerClosedBy = 0;
                                    if (ledgerSystem.LedgerDateClosed != DateTime.MinValue &&
                                          drLedger.Table.Columns.Contains(this.AppSchema.Ledger.CLOSED_BYColumn.ColumnName))
                                    {
                                        ledgerSystem.LedgerClosedBy = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.CLOSED_BYColumn.ColumnName].ToString());
                                    }
                                    //----------------------------------------------------------------------------------------------------------------------
                                    //----------------------------------------------------------------------------------------------------------------------

                                    //On 10/05/2024, To Update FD Ledger Investment Type -------------------------------------------------------------------
                                    ledgerSystem.FDInvestmentTypeId = 0; //For all Ledgers
                                    if (ledgerSystem.LedgerSubType.ToUpper() == ledgerSubType.FD.ToString().ToUpper())
                                    {
                                        ledgerSystem.FDInvestmentTypeId = (Int32)FDInvestmentType.FD;
                                        if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName))
                                        {
                                            //Assign portal branch investment type id
                                            ledgerSystem.FDInvestmentTypeId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());

                                            //Dont change FD Investment Type if that ledger has FD Accoutns
                                            resultArgs = IsExist(DataSync.IsLedgerExists);
                                            if (resultArgs.Success)
                                            {
                                                Int32 lid = GetMasterId(DataSync.Ledger);

                                                if (lid > 0)
                                                {
                                                    using (FDAccountSystem fdsystem = new FDAccountSystem())
                                                    {
                                                        resultArgs = fdsystem.FetchByLedgerId(lid);

                                                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                                        {
                                                            //Assign back local branch investment type id
                                                            DataTable dt = resultArgs.DataSource.Table;
                                                            ledgerSystem.FDInvestmentTypeId = this.NumberSet.ToInteger(dt.Rows[0][this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //----------------------------------------------------------------------------------------------------------------------------

                                    if (ledgerSystem.GroupId == 0)
                                    {
                                        resultArgs.Message = "There is no LedgerGroup in Branch Office. " + LedgerGroup;
                                    }
                                    else if (string.IsNullOrEmpty(LedgerName))
                                    {
                                        resultArgs.Message = "Ledger Name is Empty in Master XML.";
                                    }
                                    else
                                    {
                                        LedgerId = HeadOfficeLedgerId = 0;
                                        //Master Ledger
                                        resultArgs = IsExist(DataSync.IsLedgerExists);
                                        if (resultArgs.Success) // Ledger does not exists, New Ledger Add
                                        {
                                            if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                            {
                                                ledgerSystem.LedgerId = LedgerId = GetMasterId(DataSync.Ledger); // Ledger exists, Update ledger details
                                            }
                                            //On 04/07/2019, to skip updation of BO ledger options
                                            resultArgs = ledgerSystem.SaveLedgerDetailsByImportMaster();//ledgerSystem.SaveLedgerDetails();
                                            if (resultArgs.Success)
                                            {
                                                LedgerId = (LedgerId > 0) ? LedgerId : this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                                // Head Office Ledger
                                                resultArgs = IsExist(DataSync.IsHeadOfficeLedgerExists);
                                                if (resultArgs.Success)
                                                {
                                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                                    {
                                                        ledgerSystem.HeadofficeLedgerId = HeadOfficeLedgerId = GetMasterId(DataSync.HeadOfficeLedger);
                                                    }
                                                    resultArgs = ledgerSystem.SaveBranchHeadOfficeLedger();

                                                    if (resultArgs.Success)
                                                    {
                                                        HeadOfficeLedgerId = (HeadOfficeLedgerId > 0) ? HeadOfficeLedgerId : this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                                        //Mapping Process
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = MapHeadOfficeLedger();
                                                        }
                                                    }
                                                }
                                            }

                                            //13/02/2020, To Update BudgetGroupId, BudgetSubGroupId and Ledger Order
                                            if (this.IS_DIOMYS_DIOCESE)
                                            {
                                                if (ledgerSystem.GroupId != (int)FixedLedgerGroup.Cash && ledgerSystem.GroupId != (int)FixedLedgerGroup.BankAccounts &&
                                                    ledgerSystem.GroupId != (int)FixedLedgerGroup.FixedDeposit)
                                                {
                                                    ledgerSystem.UpdateBudgetGroupDetails(LedgerId, ledgerSystem.BudgetGroupId, ledgerSystem.BudgetSubGroupId, ledgerSystem.SortId);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!resultArgs.Success)
                        {
                            resultArgs.Message = "Error in Importing Ledger details. " + resultArgs.Message;
                            break;
                        }
                    }
                }
                dvLedger.RowFilter = "";

            }
            return resultArgs;
        }

        //private ResultArgs FetchPurposeCodes()
        //{
        //    using (PurposeSystem purpose = new PurposeSystem())
        //    {
        //        purpose.PurposeId = PurposeId;
        //        resultArgs = purpose.FetchPurposeCodes();
        //    }
        //    return resultArgs;
        //}

        //private ResultArgs FetchProjectCodes()
        //{
        //    using (ProjectSystem projectSystem = new ProjectSystem())
        //    {
        //        projectSystem.ProjectId = ProjectId;
        //        resultArgs = projectSystem.FetchProjectCodes();
        //    }
        //    return resultArgs;
        //}

        //private ResultArgs FetchLedgerCodes()
        //{
        //    using (LedgerSystem ledgersystem = new LedgerSystem())
        //    {
        //        ledgersystem.LedgerId = LedgerId;
        //        resultArgs = ledgersystem.FetchLedgerCodes();
        //    }
        //    return resultArgs;
        //}

        //private ResultArgs FetchHeadOfficeLedgerCode()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.FetchHeadOfficeLedgerCode))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, HeadOfficeLedgerId);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        //private ResultArgs FetchLedgerGroupCode()
        //{
        //    using (LedgerGroupSystem ledgerGroup = new LedgerGroupSystem())
        //    {
        //        ledgerGroup.GroupId = GroupId;
        //        resultArgs = ledgerGroup.FecthLedgerGroupCodes();
        //    }
        //    return resultArgs;
        //}

        /// <summary>
        /// To find out Has Ledger Entries 
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        public bool HasLedgerEntries(int LedgerId)
        {
            bool isValid = false;
            int TransCount = FetchTransactionCount(LedgerId);
            double BalanceAmt = FetchLedgerBalance(LedgerId);
            int BudgetTransCount = FetchProjectBudgetLedger(LedgerId);
            //   double BudgetAmt = FetchBudgetLedgerAmount(LedgerId);


            if (BalanceAmt > 0)
            {
                isValid = true;
            }
            else if (TransCount > 0)
            {
                isValid = true;
            }
            else if (BudgetTransCount > 0)
            {
                isValid = true;
            }
            //else if (BudgetAmt > 0)
            //{
            //    isValid = true;
            //}
            return isValid;
        }

        /// <summary>
        /// To get Count of Head Office Ledgers
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        public int FetchTransactionCount(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.CheckTransactionCount))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Check Ledger Balance
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        public double FetchLedgerBalance(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.CheckLedgerBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return this.NumberSet.ToDouble(resultArgs.DataSource.Sclar.ToString);
        }


        /// <summary>
        /// Check Ledger Balance
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        public int FetchProjectBudgetLedger(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.CheckProjectBudgetLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// To get Budget Ledger Amount
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        public double FetchBudgetLedgerAmount(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.CheckBudgetLedgerAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return this.NumberSet.ToDouble(resultArgs.DataSource.Sclar.ToString);
        }

        /// <summary>
        /// Map Head office Ledger
        /// </summary>
        /// <returns></returns>
        public ResultArgs MapHeadOfficeLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.MapHeadOfficeLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, HeadOfficeLedgerId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Map Head office Ledger
        /// </summary>
        /// <returns></returns>
        public ResultArgs MapHeadOfficewithGeneralateLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.MapHeadOfficeLedgetWithGenealateLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_IDColumn, GeneralateId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, HeadOfficeLedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 02/07/2019, Map Head office Ledger and given HO LegerId
        /// 
        /// # Delete already mapped HO Ledger for given BO ledger
        /// # Map HO Ledger for given BO ledger
        /// </summary>
        /// <returns></returns>
        public ResultArgs MapHeadOfficeLedger(Int32 BOLedgerId, Int32 HOLedgerId, DataManager dManagerLedger)
        {
            ResultArgs resultArgs = new ResultArgs();
            if (BOLedgerId > 0 && HOLedgerId > 0)
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Database = dManagerLedger.Database;
                    dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ExportVouchers.DeleteMappingLedgersAll;
                    //#. Delete existing Mapping for given BO ledger
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, BOLedgerId);
                    resultArgs = dataManager.UpdateData();
                }

                //# Map BO ledger wtih HO ledger
                if (resultArgs.Success)
                {
                    using (DataManager dataManager = new DataManager())
                    {
                        dataManager.Database = dManagerLedger.Database;
                        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.MapHeadOfficeLedger;
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, BOLedgerId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, HOLedgerId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            else
            {
                resultArgs.Success = true;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 06/07/2018, update newly created BO ledger's Budget Group and Budget Sub Group from HO ledger's Budget Group and Budget Sub Group
        /// 
        /// For Updating Budget group and budget sub group
        /// Get mapped HO ledger's Budget group and budget sub group
        /// and update to newly created BO Ledger
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateLedgerBudgetGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.UpdateLedgerBudgetGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, HeadOfficeLedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteMappedLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.DeleteMappingLedgersAll))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        private ResultArgs ImportProjectCategoryLedgers()
        {
            AcMELog.WriteLog("ImportMapCategory Ledger started");
            try
            {
                if (dtLedger != null && dtLedger.Rows.Count > 0)
                {
                    DataTable dtHeadLedgers = dtLedger;
                    resultArgs = DeleteAllMappedCategoryLedger();

                    //05/08/2020, Fix: If one Ledger mapped with more than one Project Category
                    DataTable dtCopyHeadLedgers = dtLedger.DefaultView.ToTable();
                    DataView dvProjectCategory = dtProjects.DefaultView;
                    if (dtProjects.Columns.Contains("LOCATION_NAME")) { dvProjectCategory.RowFilter = "LOCATION_NAME='" + CommonMethod.EscapeLikeValue(this.Location) + "'"; }
                    DataTable dtProjectCategory = dvProjectCategory.ToTable(true, new string[] { this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName });

                    if (resultArgs.Success)
                    {
                        int ProjCategoryId = 0;
                        int ProjCategoryLedId = 0;
                        ProjectCategoryName = "";
                        foreach (DataRow dritem in dtHeadLedgers.Rows)
                        {
                            if (resultArgs.Success)
                            {
                                //05/08/2020, Fix: If one Ledger mapped with more than one Project Category
                                LedgerName = dritem[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                foreach (DataRow drProjectCategory in dtProjectCategory.Rows)
                                {
                                    string projectcategory = drProjectCategory[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();
                                    dtCopyHeadLedgers.DefaultView.RowFilter = string.Empty;
                                    dtCopyHeadLedgers.DefaultView.RowFilter = "LEDGER_NAME = '" + CommonMethod.EscapeLikeValue(LedgerName) + "' AND PROJECT_CATOGORY_NAME LIKE '%" + CommonMethod.EscapeLikeValue(projectcategory) + "%'";

                                    //05/08/2020, Fix: If one Ledger mapped with more than one Project Category
                                    if (dtCopyHeadLedgers.DefaultView.Count > 0)
                                    {
                                        //ProjectCategoryName = dritem[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();
                                        ProjectCategoryName = projectcategory;
                                        if (!string.IsNullOrEmpty(ProjectCategoryName))
                                        {
                                            ProjCategoryId = GetMasterId(DataSync.ProjectCategory);
                                            //LedgerName = dritem[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                            if (LedgerName == "Contributions from the Houses")
                                            {

                                            }
                                            if (!string.IsNullOrEmpty(LedgerName))
                                            {
                                                ProjCategoryLedId = GetMasterId(DataSync.Ledger);
                                                if (ProjCategoryId > 0 && ProjCategoryLedId > 0)
                                                {
                                                    resultArgs = MapCategoryBOLedgers(ProjCategoryLedId, ProjCategoryId);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ed)
            {
                resultArgs.Message = ed.Message;
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("ImportMapCategroy Ledger Ended");
            }
            return resultArgs;
        }

        /// <summary>
        /// Map Project Category with Branch Office Ledgers
        /// </summary>
        /// <returns></returns>
        public ResultArgs MapCategoryBOLedgers(int LedgerID, int ProjcatID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.MapCategoryLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerID);
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjcatID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAllMappedCategoryLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteAllCategoryLedgers))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }




        /// <summary>
        /// Fetch Mapped Ledger Id
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        private ResultArgs FetchMappedLedgersById(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.FetchMappedLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchLedgerIdByLedgerName(string LedgerName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.FetchLedgerIdByLedgerName))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete Ledger if Ledger is not available in the Master XML
        /// </summary>
        /// <param name="dtBranchLedger"></param>
        /// <returns></returns>
        private ResultArgs DeleteMasterLedger(DataTable dtBranchLedger)
        {
            DataTable dtBranchLedgers = resultArgs.DataSource.Table;
            if (dtBranchLedgers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBranchLedger.Rows)
                {
                    LedgerId = this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());

                    resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteProjectMappedLedger);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteLedgerBalance);
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteBudgetLedger);
                            if (resultArgs.Success)
                            {
                                resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteHeadOfficeMappedLedger);
                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteMasterLedger);
                                }
                            }
                        }
                    }
                    if (!resultArgs.Success) { break; }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteLedger(int LedgId)
        {
            resultArgs = FetchMappedLedgersById(LedgId);
            if (resultArgs.Success)
            {
                resultArgs = DeleteMasterLedger(resultArgs.DataSource.Table);
                if (resultArgs.Success)
                {
                    LedgerId = LedgId;
                    resultArgs = DeleteLedger(SQLCommand.ImportMaster.DeleteHeadOfficeLedger);
                }
            }
            return resultArgs;
        }


        public ResultArgs DeleteHeadOfficeLedgers(DataTable dtLedgers)
        {
            foreach (DataRow dr in dtLedgers.Rows)
            {
                HeadOfficeLedgerId = this.NumberSet.ToInteger(dr["HEADOFFICE_LEDGER_ID"].ToString());

                if (HeadOfficeLedgerId > 0)
                {
                    resultArgs = DeleteLedger(HeadOfficeLedgerId);

                    if (!resultArgs.Success)
                    {
                        break;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveMismatchedLedgers(DataTable dtLedgers, DataTable dtDeleteLedgers)
        {
            resultArgs = DeleteHeadOfficeLedgers(dtDeleteLedgers);
            if (resultArgs.Success)
            {
                resultArgs = UpdateHeadOfficeLedgers(dtLedgers);
            }
            return resultArgs;
        }

        /// <summary>
        /// Update Head Office Ledgers
        /// </summary>
        /// <param name="dtLedgers"></param>
        /// <returns></returns>
        private ResultArgs UpdateHeadOfficeLedgers(DataTable dtLedgers)
        {
            // int HeadOfficeLedgerId = 0;
            string HeadOfficeLedgerName = string.Empty;
            string BranchHeadOfficeLedgerName = string.Empty;

            foreach (DataRow dr in dtLedgers.Rows)
            {
                // HeadOfficeLedgerId = this.NumberSet.ToInteger(dr["HEADOFFICE_LEDGER_ID"].ToString());
                BranchHeadOfficeLedgerName = dr["HEADOFFICE_LEDGER_NAME"].ToString();
                HeadOfficeLedgerName = dr["LEDGER_NAME"].ToString();

                //if (HeadOfficeLedgerName == "---Delete Ledger---")
                //{
                //    resultArgs = DeleteLedger(HeadOfficeLedgerId);
                //}
                //else
                //{
                resultArgs = UpdateHeadOfficeLedgers(HeadOfficeLedgerName, BranchHeadOfficeLedgerName);
                if (resultArgs.Success)
                {
                    resultArgs = UpdateMasterLedgers(HeadOfficeLedgerName, BranchHeadOfficeLedgerName);
                }
                //}
                if (!resultArgs.Success) { break; }
            }
            return resultArgs;
        }

        private ResultArgs UpdateHeadOfficeLedgers(string HeadOfficeLedgerName, string BranchHeadOfficeLedgerName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.UpdateHeadOfficeLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, HeadOfficeLedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_NAMEColumn, BranchHeadOfficeLedgerName);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateMasterLedgers(string HeadOfficeLedgerName, string BranchMasterLedgerName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.UpdateMasterLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, HeadOfficeLedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_NAMEColumn, BranchMasterLedgerName);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Map default Cash Ledger with project.
        /// </summary>
        /// <param name="dataManagerMapping"></param>
        /// <returns></returns>
        private ResultArgs MapCashLedgerToProject()
        {
            AcMELog.WriteLog("MapCashLedgerToProject Started.");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.MapLedgers))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, (int)DefaultLedgers.Cash);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in MapCashLedgerToProject " + ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("MapCashLedgerToProject Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// On 01/07/2019, Map TDS_on_FD_Interest by default for all projects
        /// </summary>
        /// <returns></returns>
        private ResultArgs MapTDSOnFDInterestLedgerToProject()
        {
            AcMELog.WriteLog("MapTDSOnFDInterestLedgerToProject Started.");
            try
            {
                //22/08/2020, Again get tds on fd interest ledger
                TDSOnFDInterestLedgerId = GetDefaultLedgers(SettingProperty.TDS_ON_FD_INTEREST_LEDGER);
                if (TDSOnFDInterestLedgerId > 0)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.MapLedgers))
                    {
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, TDSOnFDInterestLedgerId);

                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in MapTDSOnFDInterestLedgerToProject " + ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("MapTDSOnFDInterestLedgerToProject Ended.");
            }
            return resultArgs;
        }

        private int GetDefaultLedgers(string LedgerName)
        {
            int LedgerId = 0;
            try
            {

                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    ledgersystem.LedgerName = LedgerName;
                    ResultArgs resultArg = ledgersystem.FetchLedgerIdByLedgerName(LedgerName);
                    if (resultArg != null && resultArg.Success)
                    {
                        LedgerId = resultArg.DataSource.Sclar.ToInteger;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

            return LedgerId;
        }

        private ResultArgs MapAllProjectLedger()
        {
            AcMELog.WriteLog("MapProjectLedger Started.");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.MapLedgers))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in MapProjectLedger " + ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("MapProjectLedger Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Collection of Branch Office Projects 
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchBrachHeadOfficeProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.FetchBranchOfficeProjects))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        /// <summary>
        /// Get MisMatched projects by sending HeadOffice project and Branch Office project
        /// </summary>
        /// <param name="dtHeadOfficeProject"></param>
        /// <param name="dtBrachHeadOfficeProject"></param>
        /// <returns></returns>
        public DataTable FetchMisMatchedProjects(DataTable dtHeadOfficeProject, DataTable dtBrachHeadOfficeProject)
        {
            DataTable dtProject = new DataTable();

            var matched = from table1 in dtBrachHeadOfficeProject.AsEnumerable()
                          join table2 in dtHeadOfficeProject.AsEnumerable() on table1.Field<string>("PROJECT_NAME") equals table2.Field<string>("PROJECT")
                          select table1;


            var missing = from table1 in dtBrachHeadOfficeProject.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtProject = missing.CopyToDataTable();
            }
            return dtProject;
        }
        /// <summary>
        /// Get Modify project by sending HeadOffice project and Branch Office project
        /// </summary>
        /// <param name="dtHeadOfficeLedgers"></param>
        /// <param name="dtBrachHeadOfficeLedgers"></param>
        /// <returns></returns>
        public DataTable FetchModifiedProjects(DataTable dtHeadOfficeProject, DataTable dtBrachHeadOfficeProject)
        {
            DataTable dtProject = null;

            var matched = from table1 in dtHeadOfficeProject.AsEnumerable()
                          join table2 in dtBrachHeadOfficeProject.AsEnumerable() on table1.Field<string>("PROJECT") equals table2.Field<string>("PROJECT_NAME")
                          //where table1.Field<string>("LEDGER_GROUP") != table2.Field<string>("LEDGER_GROUP")
                          select table1;


            var missing = from table1 in dtHeadOfficeProject.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtProject = missing.CopyToDataTable();
            }
            return dtProject;
        }
        /// <summary>
        /// Save Project Details
        /// </summary>
        /// <param name="dtProjects"></param>
        /// <returns></returns>
        public ResultArgs SaveMasterHeadOfficeProjects(DataTable dtProjects)
        {

            foreach (DataRow drProject in dtProjects.Rows)
            {
                using (ProjectSystem project = new ProjectSystem())
                {
                    ProjectName = project.ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                    if (!string.IsNullOrEmpty(ProjectName))
                    {
                        project.ProjectCode = drProject[this.AppSchema.Project.PROJECT_CODEColumn.ColumnName].ToString();
                        project.DivisionId = this.NumberSet.ToInteger(drProject[this.AppSchema.Project.DIVISION_IDColumn.ColumnName].ToString());
                        project.AccountDate = (!string.IsNullOrEmpty(drProject[this.AppSchema.Project.ACCOUNT_DATEColumn.ColumnName].ToString())) ? this.DateSet.ToDate(drProject[this.AppSchema.Project.ACCOUNT_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                        project.StartedOn = (!string.IsNullOrEmpty(drProject[this.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString())) ? this.DateSet.ToDate(drProject[this.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                        project.Closed_On = (!string.IsNullOrEmpty(drProject[this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString())) ? this.DateSet.ToDate(drProject[this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                        project.Description = drProject[this.AppSchema.Project.DESCRIPTIONColumn.ColumnName].ToString();
                        project.Notes = drProject[this.AppSchema.Project.NOTESColumn.ColumnName].ToString();
                        ProjectCategoryName = drProject[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();
                        project.ProjectCategroyId = GetMasterId(DataSync.ProjectCategory);
                        SocietyName = drProject[this.AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString();
                        project.LegalEntityId = GetMasterId(DataSync.LegalEntity);
                        project.ClosedBy = 0;
                        if (dtProjects.Columns.Contains(this.AppSchema.Project.CLOSED_BYColumn.ColumnName))
                        {
                            project.ClosedBy = this.NumberSet.ToInteger(drProject[this.AppSchema.Project.CLOSED_BYColumn.ColumnName].ToString()); ;
                        }

                        if (project.ProjectCategroyId == 0)
                        {
                            resultArgs.Message = "Project Category does not exists in Branch Office" + ProjectCategoryName;
                        }
                        else
                        {
                            resultArgs = IsExist(DataSync.IsProjectExists);
                            if (resultArgs.Success)
                            {
                                if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                {
                                    project.ProjectId = ProjectId = GetMasterId(DataSync.Project);
                                }

                                //resultArgs = IsExist(DataSync.IsProjectCodeExists);
                                //if (resultArgs.Success)
                                //{
                                //    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                //    {
                                //        resultArgs = FetchProjectCodes();
                                //        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                //        {
                                //            project.ProjectCode = CommonMethod.GetPredictedCode(ProjectCode, resultArgs.DataSource.Table);
                                //        }
                                //    }
                                resultArgs = project.SavePJDetails();
                                if (resultArgs.Success)
                                {
                                    //On 21/05/2019, To fix to map Cash Ledger with all projects ------------------------------------------------------
                                    //ProjectId = (ProjectId > 0) ? ProjectId : this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    ProjectId = project.ProjectId;
                                    //-----------------------------------------------------------------------------------------------------------------
                                    resultArgs = MapCashLedgerToProject();

                                    //On 01/07/2019, To map TDS on FD Interest ledger to all projects --------------------------------------------------
                                    resultArgs = MapTDSOnFDInterestLedgerToProject();
                                    //------------------------------------------------------------------------------------------------------------------
                                }
                            }
                        }
                    }
                    if (!resultArgs.Success)
                    {
                        resultArgs.Message = "Error in  ImportProject" + resultArgs.Message;
                        break;
                    }
                }
            }
            return resultArgs;
        }
        /// <summary>
        /// Save Import Project Details
        /// </summary>
        /// <param name="dtProjects"></param>
        /// <returns></returns>
        public ResultArgs UpdateImportMasterProjects(int Pid, string PName)
        {
            using (ProjectSystem projectsys = new ProjectSystem())
            {
                resultArgs = projectsys.UpdateImportMasterProjects(Pid, PName);
            }
            return resultArgs;
        }

        #region Common Methods
        /// <summary>
        /// Fill the datatable with proper table.
        /// </summary>
        /// <param name="dsReadXML"></param>
        private ResultArgs AssignDataTable(DataSet dsReadXML)
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
                        case PROJECT_TABLE_NAME:
                            {
                                dtProjects = dsReadXML.Tables[PROJECT_TABLE_NAME];

                                //On 14/02/2024 - Skip closed projects based on first fy of current branch ----------------------
                                string filter = AppSchema.Project.DATE_CLOSEDColumn.ColumnName + " IS NULL OR " +
                                    "(" + AppSchema.Project.DATE_CLOSEDColumn.ColumnName + " >='" + FirstFYDateFrom + "' )"; //AND DATE_CLOSED >='" + dtTo + "'
                                dtProjects.DefaultView.RowFilter = filter;
                                dtProjects = dtProjects.DefaultView.ToTable();
                                //-------------------------------------------------------------------------------------------------
                                break;
                            }
                        case LEDGER_TABLE_NAME:
                            {
                                dtLedger = dsReadXML.Tables[LEDGER_TABLE_NAME];

                                //On 14/02/2024 - Skip closed projects based on first fy of current branch ----------------------
                                string filter = AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn.ColumnName + " IS NULL OR " +
                                    "(" + AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn.ColumnName + " >='" + FirstFYDateFrom + "' )"; //AND DATE_CLOSED >='" + dtTo + "'
                                dtLedger.DefaultView.RowFilter = filter;
                                dtLedger = dtLedger.DefaultView.ToTable();
                                //-------------------------------------------------------------------------------------------------
                                break;
                            }
                        case FCPURPOSE_TABLE_NAME:
                            {
                                dtFCPurpose = dsReadXML.Tables[FCPURPOSE_TABLE_NAME];
                                break;
                            }
                        case GOVERNING_BODIES:
                            {
                                dtGoverningBodies = dsReadXML.Tables[GOVERNING_BODIES];
                                break;
                            }
                        case LEDGERGROUP_TABLE_NAME:
                            {
                                dtLedgerGroup = dsReadXML.Tables[LEDGERGROUP_TABLE_NAME];
                                break;
                            }
                        case GENERALATEGROUP_TABLE_NAME:
                            {
                                dtGeneralateGroup = dsReadXML.Tables[GENERALATEGROUP_TABLE_NAME];
                                break;
                            }
                        case GENERALATEGROUPMAP_TABLE_NAME:
                            {
                                dtGeneralateGroupMap = dsReadXML.Tables[GENERALATEGROUPMAP_TABLE_NAME];
                                break;
                            }
                        case BRANCHOFFICE_TABLE_NAME:
                            {
                                dtBranchOffice = dsReadXML.Tables[BRANCHOFFICE_TABLE_NAME];
                                break;
                            }
                        case COUNTRY_TABLE_NAME:
                            {
                                dtCountry = dsReadXML.Tables[COUNTRY_TABLE_NAME];
                                break;
                            }
                        case TDS_SECTION:
                            {
                                dtTDSSection = dsReadXML.Tables[TDS_SECTION];
                                break;
                            }
                        case TDS_NATURE_OF_PAYMENTS:
                            {
                                dtTDSNatureOfPayments = dsReadXML.Tables[TDS_NATURE_OF_PAYMENTS];
                                break;
                            }
                        case TDS_DEDUCTEE_TYPES:
                            {
                                dtTDSDeducteeTypes = dsReadXML.Tables[TDS_DEDUCTEE_TYPES];
                                break;
                            }
                        case TDS_DUTY_TAX:
                            {
                                dtTDSDutyTax = dsReadXML.Tables[TDS_DUTY_TAX];
                                break;
                            }
                        case TDS_POLICY:
                            {
                                dtTDSPolicy = dsReadXML.Tables[TDS_POLICY];
                                break;
                            }
                        case TDS_TAX_RATE:
                            {
                                dtTDSTaxRate = dsReadXML.Tables[TDS_TAX_RATE];
                                break;
                            }
                        case TDS_POLICY_DEDUCTEES:
                            {
                                dtTDSPolicyDeductees = dsReadXML.Tables[TDS_POLICY_DEDUCTEES];
                                break;
                            }
                        case BUDGETGROUP_TABLE_NAME:
                            {
                                dtBudgetGroup = dsReadXML.Tables[BUDGETGROUP_TABLE_NAME];
                                break;
                            }
                        case BUDGETSUBGROUP_TABLE_NAME:
                            {
                                dtBudgetSubGroup = dsReadXML.Tables[BUDGETSUBGROUP_TABLE_NAME];
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Assigning Dataset into tables. " + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Get Master Id Based on the given Input values.
        /// </summary>
        /// <param name="enumMasters"></param>
        /// <returns></returns>
        public int GetMasterId(DataSync enumMasters)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    //dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.HOSQL;
                    switch (enumMasters)
                    {
                        case DataSync.LegalEntity:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetLegalEntityId;
                                dataManager.Parameters.Add(this.AppSchema.LegalEntity.SOCIETYNAMEColumn, SocietyName);
                                break;
                            }
                        case DataSync.ExecutiveMember:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetExecutiveMemberId;
                                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVEColumn, ExecutiveMember);
                                break;
                            }
                        case DataSync.ProjectCategory:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetProjectCategoryId;
                                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategoryName);
                                break;
                            }
                        case DataSync.ProjectCategoryITRGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetProjectCategoryITRId;
                                dataManager.Parameters.Add(this.AppSchema.ProjectCatogoryITRGroup.PROJECT_CATOGORY_ITRGROUPColumn, ProjectCategoryITRGroup);
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
                        case DataSync.GeneralateLedger:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetGeneralateId;
                                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_NAMEColumn, GeneralateLedger);
                                break;
                            }
                        case DataSync.GeneralateParent:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetGeneralateParentId;
                                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_NAMEColumn, GenParent);
                                break;
                            }
                        case DataSync.GeneralateMainParent:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetGeneralateMainParentId;
                                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_NAMEColumn, GenMainParent);
                                break;
                            }
                        case DataSync.HeadOfficeLedger:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetHeadOfficeLedgerId;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                                break;
                            }
                        case DataSync.FCPurpose:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetFCPurposeId;
                                dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, FCPurpose);
                                break;
                            }
                        case DataSync.Country:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetCountryId;
                                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRYColumn, CountryName);
                                break;
                            }
                        case DataSync.State:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetStateId;
                                dataManager.Parameters.Add(this.AppSchema.State.STATE_NAMEColumn, State);
                                break;
                            }
                        case DataSync.TDSSection:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetTDSSectionId;
                                dataManager.Parameters.Add(this.AppSchema.TDSSection.SECTION_NAMEColumn, TDSSectionName);
                                break;
                            }
                        case DataSync.NatureOfPayment:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetTDSNatureOfPaymentId;
                                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.PAYMENT_NAMEColumn, PaymentName);
                                break;
                            }
                        case DataSync.TDSDeducteeType:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetTDSDeducteeTypeId;
                                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.NAMEColumn, DeducteeName);
                                break;
                            }
                        case DataSync.DutyTax:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetDutyTaxId;
                                dataManager.Parameters.Add(this.AppSchema.DutyTaxType.TAX_TYPE_NAMEColumn, TaxTypeName);
                                break;
                            }
                        case DataSync.TDSPolicy:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetTDSPolicyId;
                                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeducteeId);
                                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NATURE_PAY_IDColumn, NaturePaymentId);
                                dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, TaxApplicableFrom);
                                break;
                            }
                        case DataSync.BudgetGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetBudgetGroupId;
                                dataManager.Parameters.Add(this.AppSchema.BudgetGroup.BUDGET_GROUPColumn, BudgetGroup);
                                break;
                            }
                        case DataSync.BudgetSubGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.GetBudgetSubGroupId;
                                dataManager.Parameters.Add(this.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn, BudgetSubGroup);
                                break;
                            }

                    }
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }

            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger != 0 ? resultArgs.DataSource.Sclar.ToInteger : 0;
        }

        /// <summary>
        ///  Check Whether the Particular Item is already exist in the branch database table.
        /// </summary>
        /// <param name="ExistData"></param>
        /// <returns></returns>
        public ResultArgs IsExist(DataSync ExistData)
        {
            using (DataManager dataManager = new DataManager())
            {
                //  dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.HOSQL;
                switch (ExistData)
                {
                    //case DataSync.IsLedgerGroupCodeExists:
                    //    {
                    //        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsGroupCodeExist;
                    //        dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_CODEColumn, GroupCode);
                    //        dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                    //        break;
                    //    }
                    case DataSync.IsLedgerGroupExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsLedgerGroupExists;
                            dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                            break;
                        }
                    case DataSync.IsLedgerExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsLedgerExists;
                            dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                            break;
                        }
                    case DataSync.IsGeneralateLedgersExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsGeneralateLedgerExists;
                            dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_NAMEColumn, GeneralateLedger);
                            break;
                        }
                    case DataSync.IsHeadOfficeLedgerExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsHeadOfficeLedgerExists;
                            dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_NAMEColumn, LedgerName);
                            break;
                        }
                    //case DataSync.IsHeadOfficeCodeExists:
                    //    {
                    //        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsHeadOfficeCodeExists;
                    //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, Ledgercode);
                    //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    //        break;
                    //    }
                    //case DataSync.IsLedgerCodeExists:
                    //    {
                    //        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsLedgerCodeExists;
                    //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, Ledgercode);
                    //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    //        break;
                    //    }
                    case DataSync.isProjectCategoryExist:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsProjectCatogoryExists;
                            dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategoryName);
                            break;
                        }
                    case DataSync.IsProjectExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsProjectExists;
                            dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                            break;
                        }
                    //case DataSync.IsProjectCodeExists:
                    //    {
                    //        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsProjectCodeExists;
                    //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, ProjectCode);
                    //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    //        break;
                    //    }
                    case DataSync.IsLegalEntityExist:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsLegalEntityExists;
                            // dataManager.Parameters.Add(this.AppSchema.LegalEntity.INSTITUTENAMEColumn, InstituteName);
                            dataManager.Parameters.Add(this.AppSchema.LegalEntity.SOCIETYNAMEColumn, SocietyName);
                            break;
                        }
                    case DataSync.IsExecutiveMemberExist:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsExecutiveMembers;
                            dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVEColumn, ExecutiveMember);
                            break;
                        }
                    case DataSync.IsFCPurposeExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsFCPurposeExists;
                            dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, FCPurpose);
                            break;
                        }
                    //case DataSync.ISFCPurposeCodeExists:
                    //    {
                    //        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsFCPurposeCodeExists;
                    //        dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId);
                    //        dataManager.Parameters.Add(this.AppSchema.Purposes.CODEColumn, FCCode);
                    //        break;
                    //    }
                    case DataSync.IsBranchOfficeExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsBranchOfficeExist;
                            dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                            break;
                        }
                    case DataSync.IsCountryExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsCountryExists;
                            dataManager.Parameters.Add(this.AppSchema.Country.COUNTRYColumn, CountryName);
                            break;
                        }
                    case DataSync.IsTDSSectionExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsTDSSectionExists;
                            dataManager.Parameters.Add(this.AppSchema.TDSSection.SECTION_NAMEColumn, TDSSectionName);
                            break;
                        }
                    case DataSync.IsTDSNatureOfPaymentExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsTDSNatureOfPaymentExists;
                            dataManager.Parameters.Add(this.AppSchema.NatureofPayment.PAYMENT_NAMEColumn, PaymentName);
                            break;
                        }
                    case DataSync.IsDeducteeTypeExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsTDSDeducteeTypeExists;
                            dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.NAMEColumn, DeducteeName);
                            break;
                        }
                    case DataSync.IsDutyTaxExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsDutyTaxExists;
                            dataManager.Parameters.Add(this.AppSchema.DutyTaxType.TAX_TYPE_NAMEColumn, TaxTypeName);
                            break;
                        }
                    case DataSync.IsTDSPolicyExists:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.IsTaxPolicyExists;
                            dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeducteeId);
                            dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NATURE_PAY_IDColumn, NaturePaymentId);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, TaxApplicableFrom);
                            break;
                        }
                }
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }


        /// <summary>
        /// Delete based on the Id
        /// </summary>
        /// <param name="Ledger"></param>
        /// <returns></returns>
        public ResultArgs DeleteLedger(SQLCommand.ImportMaster Ledger, bool DontShowErrorMessage = false)
        {
            string sQuery = string.Empty;
            using (DataManager dataManager = new DataManager())
            {
                switch (Ledger)
                {
                    case SQLCommand.ImportMaster.DeleteHeadOfficeLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteHeadOfficeLedger;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteHeadOfficeMappedLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteHeadOfficeMappedLedger;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteHeadOfficewithGeneralateMappedLedgers:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteHeadOfficewithGeneralateMappedLedgers;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteMasterLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteMasterLedger;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteProjectMappedLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteProjectMappedLedger;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteLedgerBalance:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteLedgerBalance;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteProjectBudgetMappedLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteProjectBudgetMappedLedger;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteBudgetLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteBudgetLedger;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteTDSCreditorProfile:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteTDSCreditorProfile;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteProjectCategoryByLedgerId:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteProjectCategoryByLedgerId;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteFDLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteFDLedger;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteBankAccountByLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteBankAccountByLedger;
                            break;
                        }
                    case SQLCommand.ImportMaster.DeleteLedgerOtherMappedDetails:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportMaster.DeleteLedgerOtherMappedDetails;
                            break;
                        }
                }
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData(DontShowErrorMessage);
            }
            return resultArgs;
        }


        #endregion

        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}



//resultArgs = IsExist(Bosco.Utility.DataSync.IsLedgerGroupCodeExists);
//if (resultArgs.Success) //Ledger Group Code Exists in the Database.
//{
//    if (resultArgs.DataSource.Sclar.ToInteger > 0)
//    {
//        resultArgs = FetchLedgerGroupCode();
//        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
//        {
//            MasterLedgerGroup.Abbrevation = CommonMethod.GetPredictedCode(GroupCode, resultArgs.DataSource.Table);
//        }
//    }
//}