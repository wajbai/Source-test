-- MySQL dump 10.13  Distrib 5.6.10, for Win32 (x86)
--
-- Host: localhost    Database: acperp
-- ------------------------------------------------------
-- Server version	5.6.10

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `acperp`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `acperp` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `acperp`;

--
-- Table structure for table `accounting_year`
--

DROP TABLE IF EXISTS `accounting_year`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `accounting_year` (
  `ACC_YEAR_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `YEAR_FROM` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `YEAR_TO` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `BOOKS_BEGINNING_FROM` datetime DEFAULT NULL,
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '1-Active,0-Inactive',
  `IS_FIRST_ACCOUNTING_YEAR` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ACC_YEAR_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounting_year`
--


--
-- Table structure for table `acme_config`
--

DROP TABLE IF EXISTS `acme_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `acme_config` (
  `NAME` varchar(100) DEFAULT NULL,
  `VALUE` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `acme_config`
--

LOCK TABLES `acme_config` WRITE;
/*!40000 ALTER TABLE `acme_config` DISABLE KEYS */;
INSERT INTO `acme_config` VALUES ('Code','NES -MON'),('Name','Institute of the Brothers of St. Gabriel Society'),('Version','1.0.0');
/*!40000 ALTER TABLE `acme_config` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `activitiy_rights`
--

DROP TABLE IF EXISTS `activitiy_rights`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `activitiy_rights` (
  `ACTIVITY_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PARENT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `OBJECT_NAME` varchar(50) NOT NULL DEFAULT '',
  `ENUMTYPE` varchar(100) NOT NULL DEFAULT '',
  `OBJECT_TYPE` varchar(50) NOT NULL DEFAULT '',
  `OBJECT_SUB_TYPE` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ACTIVITY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `activitiy_rights`
--

LOCK TABLES `activitiy_rights` WRITE;
/*!40000 ALTER TABLE `activitiy_rights` DISABLE KEYS */;
INSERT INTO `activitiy_rights` VALUES (1,0,'Settings','Settings','Settings','Settings'),(2,1,'Master Setting','MasterSetting','Settings','Master Settings'),(3,2,'UI Settings','UISettings','Settings','Master Settings'),(4,2,'Global Settings','GlobalSettings','Settings','Master Settings'),(5,1,'Account Mapping','AccountMapping','Settings','Account Mapping'),(6,5,'Map Project','MapProject','Settings','Account Mapping'),(7,5,'Map Ledger','MapLedger','Settings','Account Mapping'),(8,5,'Map Cost Centre','MapCostCentre','Settings','Account Mapping'),(9,5,'Map Donor','MapDonor','Settings','Account Mapping'),(10,5,'Map Vouchers','MapVouchers','Settings','Account Mapping'),(11,1,'Transaction Period','TransactionPeriod','Settings','Transaction Period'),(12,11,'Create Transaction','CreateTransaction','Settings','Transaction Period'),(13,11,'Edit Transaction','EditTransaction','Settings','Transaction Period'),(14,11,'Delete Transaction','DeleteTransaction','Settings','Transaction Period'),(15,11,'Print Transaction','PrintTransaction','Settings','Transaction Period'),(16,1,'Legal Entity','LegalEntity','Settings','Legal Entity'),(17,16,'Create Legal Entity','CreateLegalEntity','Settings','Legal Entity'),(18,16,'Edit Legal Entity','EditLegalEntity','Settings','Legal Entity'),(19,16,'Delete Legal Entity','DeleteLegalEntity','Settings','Legal Entity'),(20,16,'Print Legal Entity','PrintLegalEntity','Settings','Legal Entity'),(21,0,'Masters','Masters','Masters','Masters'),(22,21,'Project Category','ProjectCategory','Masters','Project Category'),(23,22,'Create Project Category','CreateProjectCategory','Masters','Project Category'),(24,22,'Edit Project Category','EditProjectCategory','Masters','Project Category'),(25,22,'Delete Project Category','DeleteProjectCategory','Masters','Project Category'),(26,22,'Print Project Category','PrintProjectCategory','Masters','Project Category'),(27,21,'Project','Project','Masters','Project'),(28,27,'Create Project','CreateProject','Masters','Project'),(29,27,'Edit Project','EditProject','Masters','Project'),(30,27,'Delete Project','DeleteProject','Masters','Project'),(31,27,'Print Project','PrintProject','Masters','Project'),(32,21,'Ledger Group','LedgerGroup','Masters','Ledger Group'),(33,32,'Create Ledger Group','CreateLedgerGroup','Masters','Ledger Group'),(34,32,'Edit Ledger Group','EditLedgerGroup','Masters','Ledger Group'),(35,32,'Delete Ledger Group','DeleteLedgerGroup','Masters','Ledger Group'),(36,32,'Print Ledger Group','PrintLedgerGroup','Masters','Ledger Group'),(37,21,'Ledger','Ledger','Masters','Ledger'),(38,37,'Create Ledger','CreateLedger','Masters','Ledger'),(39,37,'Edit Ledger','EditLedger','Masters','Ledger'),(40,37,'Delete Ledger','DeleteLedger','Masters','Ledger'),(41,37,'Print Ledger','PrintLedger','Masters','Ledger'),(42,21,'Bank Accounts','BankAccounts','Masters','Bank Accounts'),(43,42,'Create Bank Account','CreateBankAccount','Masters','Bank Accounts'),(44,42,'Edit Bank Account','EditBankAccount','Masters','Bank Accounts'),(45,42,'Delete Bank Account','DeleteBankAccount','Masters','Bank Accounts'),(46,42,'Print Bank Account','PrintBankAccount','Masters','Bank Accounts'),(47,21,'Voucher Number Definition','VoucherNumberDefinition','Masters','Voucher Number Definition'),(48,47,'Create Voucher ','CreateVoucher ','Masters','Voucher Number Definition'),(49,47,'Edit Voucher','EditVoucher','Masters','Voucher Number Definition'),(50,47,'Delete Voucher','DeleteVoucher','Masters','Voucher Number Definition'),(51,47,'Print Voucher','PrintVoucher','Masters','Voucher Number Definition'),(52,21,'Cost Centre','CostCentre','Masters','Cost Centre'),(53,52,'Create Cost Centre','CreateCostCentre','Masters','Cost Centre'),(54,52,'Edit Cost Centre','EditCostCentre','Masters','Cost Centre'),(55,52,'Delete Cost Centre','DeleteCostCentre','Masters','Cost Centre'),(56,52,'Print Cost Centre','PrintCostCentre','Masters','Cost Centre'),(57,21,'Bank','Bank','Masters','Bank '),(58,57,'Create Bank','CreateBank','Masters','Bank '),(59,57,'Edit Bank','EditBank','Masters','Bank '),(60,57,'Delete Bank','DeleteBank','Masters','Bank '),(61,57,'Print Bank','PrintBank','Masters','Bank '),(62,21,'Country','Country','Masters','Country'),(63,62,'Create Country','CreateCountry','Masters','Country'),(64,62,'Edit Country','EditCountry','Masters','Country'),(65,62,'Delete Country','DeleteCountry','Masters','Country'),(66,62,'Print Country','PrintCountry','Masters','Country'),(67,21,'Audit Info','Audit Info','Masters','Audit Info'),(68,67,'Create Audit Info','CreateAuditInfo','Masters','Audit Info'),(69,67,'Edit Audit Info','EditAuditInfo','Masters','Audit Info'),(70,67,'Delete Audit Info','DeleteAuditInfo','Masters','Audit Info'),(71,67,'Print Audit Info','PrintAuditInfo','Masters','Audit Info'),(72,21,'Governing Members','GoverningMembers','Masters','Governing Members'),(73,72,'Create Governing Member','CreateGoverningMember','Masters','Governing Members'),(74,72,'Edit Governing Member','EditGoverningMember','Masters','Governing Members'),(75,72,'Delete Governing Member','DeleteGoverningMember','Masters','Governing Members'),(76,72,'Print Governing Member','PrintGoverningMember','Masters','Governing Members'),(77,21,'Auditor','Auditor','Masters','Auditor'),(78,77,'Create Auditor','CreateAuditor','Masters','Auditor'),(79,77,'Edit Auditor','EditAuditor','Masters','Auditor'),(80,77,'Delete Auditor','DeleteAuditor','Masters','Auditor'),(81,77,'Print Auditor','PrintAuditor','Masters','Auditor'),(82,21,'Donor','Donor','Masters','Donor'),(83,82,'Create Donor','CreateDonor','Masters','Donor'),(84,82,'Edit Donor','EditDonor','Masters','Donor'),(85,82,'Delete Donor','DeleteDonor','Masters','Donor'),(86,82,'Print Donor','PrintDonor','Masters','Donor'),(87,21,'Purpose','Purpose','Masters','Purpose'),(88,87,'Print Purpose','PrintPurpose','Masters','Purpose'),(89,0,'Finance','Finance','Finance','Finance'),(90,89,'Receipt','Receipt','Finance','Receipt'),(91,90,'Create Receipt Voucher','CreateReceiptVoucher','Finance','Receipt'),(92,90,'Edit Receipt Voucher','EditReceiptVoucher','Finance','Receipt'),(93,90,'Delete Receipt Voucher','DeleteReceiptVoucher','Finance','Receipt'),(94,90,'View Receipt Voucher','ViewReceiptVoucher','Finance','Receipt'),(95,90,'Print Receipt Voucher','PrintReceiptVoucher','Finance','Receipt'),(97,89,'Payments','Payments','Finance','Payments'),(98,97,'Create Payment Voucher','CreatePaymentVoucher','Finance','Payments'),(99,97,'Edit Payment Voucher','EditPaymentVoucher','Finance','Payments'),(100,97,'Delete Payment Voucher','DeletePaymentVoucher','Finance','Payments'),(101,97,'Move Payment Voucher','MovePaymentVoucher','Finance','Payments'),(102,97,'Print Payment Voucher','PrintPaymentVoucher','Finance','Payments'),(103,89,'Contra','Contra','Finance','Contra'),(104,103,'Create Contra Voucher','CreateContraVoucher','Finance','Contra'),(105,103,'Edit Contra Voucher','EditContraVoucher','Finance','Contra'),(106,103,'Delete Contra Voucher','DeleteContraVoucher','Finance','Contra'),(107,103,'Move Contra Voucher','MoveContraVoucher','Finance','Contra'),(108,103,'Print Contra Voucher','PrintContraVoucher','Finance','Contra'),(109,90,'Move Receipt  Voucher','MoveReceiptVoucher','Finance','Receipt'),(110,89,'Journal','Journal','Finance','Journal'),(111,110,'Create Journal Voucher','CreateJournalVoucher','Finance','Journal'),(112,110,'Edit Journal Voucher','EditJournalVoucher','Finance','Journal'),(113,110,'Delete Journal Voucher','DeleteJournalVoucher','Finance','Journal'),(114,110,'Print Journal Voucher','PrintJournalVoucher','Finance','Journal'),(115,89,'Bank Reconciliation','BankReconciliation','Finance','Bank Reconciliation'),(116,115,'Print Bank Reconciliation','PrintBankReconciliation','Finance','Bank Reconciliation'),(117,115,'Bank Reconciled','BankReconciled','Finance','Bank Reconciliation'),(118,115,'Bank UnReconcilied','BankUnReconcilied','Finance','Bank Reconciliation'),(119,115,'Bank Cleared','BankCleared','Finance','Bank Reconciliation'),(120,115,'Bank UnCleared','BankUnCleared','Finance','Bank Reconciliation'),(121,89,'Budget','Budget','Finance','Budget'),(122,121,'Create Budget','CreateBudget','Finance','Budget'),(123,121,'Edit Budget','EditBudget','Finance','Budget'),(124,121,'Delete Budget','DeleteBudget','Finance','Budget'),(125,121,'Print Budget','PrintBudget','Finance','Budget'),(126,89,'Fixed Ledger','FixedDepositLedger','Finance','Fixed Ledger'),(127,126,'Create FD Ledger','CreateFDLedger','Finance','Fixed Ledger'),(128,126,'Edit FD Ledger','EditFDLedger','Finance','Fixed Ledger'),(129,126,'Delete FD Ledger','DeleteFDLedger','Finance','Fixed Ledger'),(130,126,'Print FD Ledger','PrintFDLedger','Finance','Fixed Ledger'),(131,89,'Fixed Deposit','FixedDeposit','Finance','Fixed Deposit'),(132,131,'Create Fixed Deposit','CreateFixedDeposit','Finance','Fixed Deposit'),(133,131,'Edit Fixed Deposit','EditFixedDeposit','Finance','Fixed Deposit'),(134,131,'Delete Fixed Deposit','DeleteFixedDeposit','Finance','Fixed Deposit'),(135,131,'Print Fixed Deposit','PrintFixedDeposit','Finance','Fixed Deposit'),(136,89,'Fixed Investment','FixedInvestment','Finance','Fixed Investment'),(137,136,'Create Fixed Investment','CreateFixedInvestment','Finance','Fixed Investment'),(138,136,'Edit Fixed Investment','EditFixedInvestment','Finance','Fixed Investment'),(139,136,'Delete Fixed Investment','DeleteFixedInvestment','Finance','Fixed Investment'),(140,136,'Print Fixed Investment','PrintFixedInvestment','Finance','Fixed Investment'),(141,89,'Fixed Deposit Renewal','FixedDepositRenewal','Finance','Fixed Deposit Renewal'),(142,141,'Renew Fixed Deposit','RenewFixedDeposit','Finance','Fixed Deposit Renewal'),(143,141,'Modify Fixed Deposit Renewal','ModifyFixedDepostRenewal','Finance','Fixed Deposit Renewal'),(144,141,'Delete Fixed Deposit Renewal','DeleteFixedDepositRenewal','Finance','Fixed Deposit Renewal'),(145,141,'Print Fixed Deposit Renewal','PrintFixedDepositRenewal','Finance','Fixed Deposit Renewal'),(146,89,'FD Withdrawal','FDWithdrawal','Finance','Fixed Deposit Withdraw'),(147,146,' Withdraw Fixed Deposit','WithdrawFixedDeposit','Finance','Fixed Deposit Withdraw'),(148,146,'Print Fixed Deposit Withdraw','PrintFixedDepositWithdraw','Finance','Fixed Deposit Withdraw'),(149,89,'Fixed Deposit Register','FixedDepositRegister','Finance','Fixed Deposit Register'),(150,149,'Print Fixed Deposit Register','PrintFixedDepositRegister','Finance','Fixed Deposit Register'),(151,0,'User Management','UserManagement','User Management','User Management'),(152,151,'User','User','User Management','User '),(153,152,'Create User','CreateUser','User Management','User'),(154,152,'Edit User','EditUser','User Management','User'),(155,152,'Delete User','DeleteUser','User Management','User'),(156,152,'Print User','PrintUser','User Management','User'),(157,151,'User Role','UserRole','User Management','User Role'),(158,157,'Create User Role','CreateUserRole','User Management','User Role'),(159,157,'Edit User Role','EditUserRole','User Management','User Role'),(160,157,'Delete User Role','DeleteUserRole','User Management','User Role'),(161,157,'Print User Role','PrintUserRole','User Management','User Role'),(162,151,'User Right Management','UserRightsManagement','User Management','User Rights'),(163,162,'Assign User Rights','AssignUserRights','User Management','User Rights'),(164,151,'ManageSecurity','ManageSecurity','User Management','Manage Security'),(165,164,'Reset Password','ResetPassword','User Management','Manage Security'),(166,157,'View User Role','ViewUserRole','User Management','User Role'),(167,152,'View User','ViewUser','User Management','User'),(168,11,'View Transaction','ViewTransaction','Settings','Transaction Period'),(169,16,'View Legal Entity','ViewLedgalEntity','Settings','Legal Entity'),(170,22,'View Project Category','ViewProjectCategory','Masters','Project Category'),(171,27,'View Project','ViewProject','Masters','Project'),(172,32,'View Ledger Group','ViewLedgerGroup','Masters','Ledger Group'),(173,37,'View Ledger','ViewLedger','Masters','Ledger'),(174,42,'View Bank Accounts','ViewBankAccounts','Masters','Bank Accounts'),(175,47,'View Voucher','ViewVoucher','Masters','Voucher Number Definition'),(176,52,'View Cost Cetre','ViewCostCentre','Masters','Cost Centre'),(177,57,'View Bank','ViewBank','Masters','Bank '),(178,62,'View Country','ViewCountry','Masters','Country'),(179,67,'View Audit Info','ViewAuditInfo','Masters','Audit Info'),(180,72,'View Governing Members','ViewGoverningMembers','Masters','Governing Members'),(181,77,'View Auditor','ViewAuditor','Masters','Auditor'),(182,82,'View Donor','ViewDonor','Masters','Donor'),(183,87,'View Purpose','ViewPurpose','Masters','Purpose'),(184,97,'View Payment Voucher','ViewPaymentVoucher','Finance','Payments'),(185,103,'View Contra Voucher','ViewContraVoucher','Finance','Contra'),(186,110,'View Journal Voucher','ViewJournalVoucher','Finance','Journal'),(187,115,'View Bank Reconciliation','ViewBankReconciliation','Finance','Bank Reconciliation'),(188,121,'View Budget','ViewBudget','Finance','Budget'),(189,126,'View FD Ledger','ViewFDLedger','Finance','Fixed Ledger'),(190,131,'View Fixed Deposit','ViewFixedDeposit','Finance','Fixed Deposit'),(191,136,'View Fixed Investment','ViewFixedInvestment','Finance','Fixed Investment'),(192,141,'View Fixed Deposit Renewal','ViewFixedDepositRenewal','Finance','Fixed Deposit Renewal'),(193,146,'View Fixed Deposit Withdraw','ViewFixedDepositWithdraw','Finance','Fixed Deposit Withdraw'),(194,149,'View Fixed Deposit Register','ViewFixedDepositRegister','Finance','Fixed Deposit Register'),(195,164,'View Manage Security','ViewManageSecurity','User Management','Manage Security'),(196,0,'Reports','Reports','Reports','Reports'),(197,196,'Abstract','Abstract','Reports','Activities'),(198,196,'Bank Activities','BankActivities','Reports','Activities'),(199,196,'Book of Accounts','BookofAccounts','Reports','Activities'),(200,196,'Final Accounts','FinalAccounts','Reports','Activities'),(201,196,'Foregin Contribution','ForeginContribution','Reports','Activities'),(202,196,'Cost Centre','CostCentre','Reports','Activities'),(203,196,'Financial Records','FinancialRecords','Reports','Activities'),(204,196,'Budget','Budget','Reports','Activities'),(205,0,'Data Utility','DataUtility','Data Utility','Data Utility'),(206,205,'Backup','Backup','Data Utility','Activities'),(207,205,'Restore','Restore','Data Utility','Activities'),(208,205,'Refresh Balance','RefreshBalance','Data Utility','Activities'),(209,205,'Regenarate Voucher ','RegenarateVoucher','Data Utility','Activities'),(210,205,'Data Migration','DataMigration','Data Utility','Activities'),(211,205,'Data Export','DataExport','Data Utility','Activities'),(212,205,'Migration Mapping','MigrationMapping','Data Utility','Activities'),(213,89,'DashBoard','DashBoard','Finance','Dash Board'),(214,213,'View Receipt and Payments','ViewReceiptPayments','Finance','Dash Board'),(215,213,'Show FD Alert','ShowFDAlert','Finance','Dash Board'),(216,213,'Show Bank Reconciliation','ShowBankReconciliation','Finance','Dash Board'),(217,213,'Show Project Details','ShowProjectDetails','Finance','Dash Board');
/*!40000 ALTER TABLE `activitiy_rights` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `allot_fund`
--

DROP TABLE IF EXISTS `allot_fund`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `allot_fund` (
  `BUDGET_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `MONTH1` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH2` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH3` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH4` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH5` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH6` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH7` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH8` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH9` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH10` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH11` decimal(13,2) NOT NULL DEFAULT '0.00',
  `MONTH12` decimal(13,2) NOT NULL DEFAULT '0.00',
  KEY `FK_BUDGET_ID` (`BUDGET_ID`),
  KEY `FK_LEDGER_ID` (`LEDGER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `allot_fund`
--

LOCK TABLES `allot_fund` WRITE;
/*!40000 ALTER TABLE `allot_fund` DISABLE KEYS */;
/*!40000 ALTER TABLE `allot_fund` ENABLE KEYS */;
UNLOCK TABLES;


DROP TABLE IF EXISTS `branch_office`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `branch_office` (
  `BRANCH_OFFICE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_OFFICE_CODE` varchar(12) NOT NULL DEFAULT '',
  `BRANCH_OFFICE_NAME` varchar(50) NOT NULL DEFAULT '',
  `HEAD_OFFICE_CODE` varchar(6) NOT NULL DEFAULT '0',
  `CREATED_DATE` datetime DEFAULT NULL,
  `CREATED_BY` int(10) unsigned DEFAULT NULL,
  `DEPLOYMENT_TYPE` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0 Standalone 1    Client-Server',
  `ADDRESS` text,
  `STATE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PINCODE` varchar(6) NOT NULL DEFAULT '',
  `COUNTRY_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PHONE_NO` varchar(20) NOT NULL DEFAULT '',
  `MOBILE_NO` varchar(15) NOT NULL DEFAULT '',
  `BRANCH_EMAIL_ID` varchar(100) NOT NULL DEFAULT '',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '1 - Created\r\n2 - Activated\r\n3 - De Activated',
  `MODIFIED_DATE` datetime DEFAULT NULL,
  `MODIFIED_BY` int(10) unsigned DEFAULT NULL,
  `USER_CREATED_STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0 - Created (Head office)\r\n1 - Communicated for approval (Head office Approval)\r\n2 - User created and Not communicated (User- Email Comm)\r\n3 - Communicated to head office',
  `CITY` varchar(100) DEFAULT NULL,
  `BRANCH_PART_CODE` varchar(6) NOT NULL DEFAULT '' COMMENT 'To save branch code of 6 digits',
  `COUNTRY_CODE` varchar(5) DEFAULT NULL,
  `BRANCH_KEY_CODE` varchar(25) NOT NULL DEFAULT '',
  PRIMARY KEY (`BRANCH_OFFICE_ID`),
  UNIQUE KEY `Index_BranchCode` (`BRANCH_OFFICE_CODE`),
  KEY `Unique_Branch_Key_Code` (`BRANCH_KEY_CODE`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `branch_office`
--

LOCK TABLES `branch_office` WRITE;
/*!40000 ALTER TABLE `branch_office` DISABLE KEYS */;
/*!40000 ALTER TABLE `branch_office` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `budget_ledger`
--

DROP TABLE IF EXISTS `budget_ledger`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `budget_ledger` (
  `BUDGET_ID` int(11) NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `AMOUNT` decimal(13,2) NOT NULL DEFAULT '0.00',
  `TRANS_MODE` varchar(2) NOT NULL DEFAULT '',
  KEY `FK_BUDGET_LEDGER_BUDGET_ID` (`BUDGET_ID`),
  KEY `FK_budget_ledger_LEDGER_ID` (`LEDGER_ID`),
  CONSTRAINT `FK_budget_ledger_LEDGER_ID` FOREIGN KEY (`LEDGER_ID`) REFERENCES `master_ledger` (`LEDGER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `budget_ledger`
--

LOCK TABLES `budget_ledger` WRITE;
/*!40000 ALTER TABLE `budget_ledger` DISABLE KEYS */;
/*!40000 ALTER TABLE `budget_ledger` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `budget_master`
--

DROP TABLE IF EXISTS `budget_master`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `budget_master` (
  `BUDGET_ID` int(11) NOT NULL AUTO_INCREMENT,
  `BUDGET_NAME` varchar(150) NOT NULL DEFAULT '' COMMENT 'Name of the budget',
  `BUDGET_TYPE_ID` int(11) NOT NULL DEFAULT '0' COMMENT 'Accounting Year/ Period',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'Project for which the budget is prepared',
  `DATE_FROM` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `DATE_TO` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `REMARKS` varchar(500) DEFAULT NULL COMMENT 'Comments on the budget',
  `IS_ACTIVE` int(11) NOT NULL DEFAULT '1' COMMENT '1- Active  0 - Inactive',
  PRIMARY KEY (`BUDGET_ID`),
  KEY `FK_budget_master_Project_id` (`PROJECT_ID`),
  CONSTRAINT `FK_budget_master_PROJECT_ID` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `budget_master`
--

LOCK TABLES `budget_master` WRITE;
/*!40000 ALTER TABLE `budget_master` DISABLE KEYS */;
/*!40000 ALTER TABLE `budget_master` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `budget_type`
--

DROP TABLE IF EXISTS `budget_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `budget_type` (
  `BUDGET_TYPE_ID` int(11) NOT NULL AUTO_INCREMENT,
  `BUDGET_TYPE` varchar(30) NOT NULL DEFAULT '',
  PRIMARY KEY (`BUDGET_TYPE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `budget_type`
--

LOCK TABLES `budget_type` WRITE;
/*!40000 ALTER TABLE `budget_type` DISABLE KEYS */;
INSERT INTO `budget_type` VALUES (1,'Accounting Year'),(2,'Period');
/*!40000 ALTER TABLE `budget_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `costcategory_costcentre`
--

DROP TABLE IF EXISTS `costcategory_costcentre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `costcategory_costcentre` (
  `COST_CATEGORY_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `COST_CENTRE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`COST_CATEGORY_ID`,`COST_CENTRE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `costcategory_costcentre`
--

LOCK TABLES `costcategory_costcentre` WRITE;
/*!40000 ALTER TABLE `costcategory_costcentre` DISABLE KEYS */;
/*!40000 ALTER TABLE `costcategory_costcentre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `country_symbols`
--

DROP TABLE IF EXISTS `country_symbols`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `country_symbols` (
  `Currency_Symbols` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `country_symbols`
--

LOCK TABLES `country_symbols` WRITE;
/*!40000 ALTER TABLE `country_symbols` DISABLE KEYS */;
INSERT INTO `country_symbols` VALUES ('Ø±.Ø³.â€'),('Ð»Ð².'),('â‚¬'),('NT$'),('KÄ'),('kr.'),('â‚¬'),('â‚¬'),('$'),('â‚¬'),('â‚¬'),('â‚ª'),('Ft'),('kr.'),('â‚¬'),('Â¥'),('â‚©'),('â‚¬'),('kr'),('zÅ‚'),('R$'),('fr.'),('lei'),('Ñ€.'),('kn'),('â‚¬'),('Lek'),('kr'),('à¸¿'),('TL'),('Rs'),('Rp'),('â‚´'),('Ñ€.'),('â‚¬'),('kr'),('Ls'),('Lt'),('Ñ‚.Ñ€.'),('Ø±ÙŠØ§Ù„'),('â‚«'),('Õ¤Ö€.'),('man.'),('â‚¬'),('â‚¬'),('Ð´ÐµÐ½.'),('R'),('R'),('R'),('R'),('Lari'),('kr.'),('à¤°à¥'),('â‚¬'),('kr'),('RM'),('Ð¢'),('ÑÐ¾Ð¼'),('S'),('m.'),('so'),('Ñ€.'),('à¦Ÿà¦¾'),('à¨°à©'),('àª°à«‚'),('à¬Ÿ'),('à®°à¯‚'),('à°°à±‚'),('à²°à³‚'),('à´•'),('à¦Ÿ'),('à¤°à¥'),('à¤°à¥'),('â‚®'),('Â¥'),('Â£'),('áŸ›'),('â‚­'),('â‚¬'),('à¤°à¥'),('Ù„.Ø³.â€'),('à¶»à·”.'),('$'),('ETB'),('à¤°à¥'),('â‚¬'),('Ø‹'),('PhP'),('Þƒ.'),('N'),('N'),('$b'),('R'),('Ò».'),('â‚¬'),('kr.'),('N'),('Â¥'),('$'),('$'),('â‚¬'),('Â¥'),('$'),('â‚¬'),('â‚¬'),('â‚¬'),('Ñ.'),('Q'),('RWF'),('XOF'),('Ø‹'),('Â£'),('Ø¯.Ø¹.â€'),('Â¥'),('Fr.'),('Â£'),('$'),('â‚¬'),('fr.'),('â‚¬'),('kr'),('â‚¬'),('Din.'),('â‚¬'),('Ð¼Ð°Ð½.'),('â‚¬'),('kr'),('â‚¬'),('$'),('ÑÑžÐ¼'),('à§³'),('Â¥'),('$'),('DZD'),('$'),('Ø¬.Ù….â€'),('HK$'),('â‚¬'),('$'),('â‚¬'),('$'),('Ð”Ð¸Ð½.'),('â‚¬'),('S/.'),('Ø¯.Ù„.â€'),('$'),('â‚¬'),('$'),('Q'),('fr.'),('KM'),('kr'),('Ø¯.Ø¬.â€'),('MOP'),('CHF'),('$'),('â‚¡'),('â‚¬'),('KM'),('kr'),('Ø¯.Ù….â€'),('â‚¬'),('B/.'),('â‚¬'),('KM'),('kr'),('Ø¯.Øª.â€'),('R'),('RD$'),('ÐšÐœ'),('kr'),('Ø±.Ø¹.â€'),('J$'),('Bs. F.'),('ÐšÐœ'),('â‚¬'),('Ø±.ÙŠ.â€'),('$'),('$'),('Din.'),('â‚¬'),('Ù„.Ø³.â€'),('BZ$'),('S/.'),('Ð”Ð¸Ð½.'),('Ø¯.Ø§.â€'),('TT$'),('$'),('â‚¬'),('Ù„.Ù„.â€'),('Z$'),('$'),('â‚¬'),('Ø¯.Ùƒ.â€'),('Php'),('$'),('Ø¯.Ø¥.â€'),('$U'),('Ø¯.Ø¨.â€'),('Gs'),('Ø±.Ù‚.â€'),('Rs.'),('$b'),('RM'),('$'),('$'),('L.'),('C$'),('$'),('$'),('à¤°');
/*!40000 ALTER TABLE `country_symbols` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `datasync_status`
--

DROP TABLE IF EXISTS `datasync_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `datasync_status` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Status` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `datasync_status`
--

LOCK TABLES `datasync_status` WRITE;
/*!40000 ALTER TABLE `datasync_status` DISABLE KEYS */;
INSERT INTO `datasync_status` VALUES (1,'Received'),(2,'InProgress'),(3,'Closed'),(4,'Falied');
/*!40000 ALTER TABLE `datasync_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `datasync_task`
--

DROP TABLE IF EXISTS `datasync_task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `datasync_task` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `HEAD_OFFICE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BRANCH_OFFICE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `UPLOADED_ON` datetime DEFAULT NULL,
  `STARTED_ON` datetime DEFAULT NULL,
  `COMPLETED_ON` datetime DEFAULT NULL,
  `XML_FILENAME` varchar(250) NOT NULL DEFAULT '',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0',
  `REMARKS` varchar(500) DEFAULT NULL,
  `TRANS_DATE_FROM` datetime DEFAULT NULL COMMENT 'Transaction Date from (Branch Office Transaction)',
  `TRANS_DATE_TO` datetime DEFAULT NULL COMMENT 'Transaction Date To (Branch Office Transaction)',
  PRIMARY KEY (`ID`),
  KEY `FK_HEAD_OFFICE_ID` (`HEAD_OFFICE_ID`),
  KEY `FK_BRANCH_OFFICE_ID` (`BRANCH_OFFICE_ID`),
  KEY `FK_datasync_task_Status` (`STATUS`),
  CONSTRAINT `FK_BRANCH_OFFICE_ID` FOREIGN KEY (`BRANCH_OFFICE_ID`) REFERENCES `branch_office` (`BRANCH_OFFICE_ID`),
  CONSTRAINT `FK_datasync_task_Status` FOREIGN KEY (`STATUS`) REFERENCES `datasync_status` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `datasync_task`
--

LOCK TABLES `datasync_task` WRITE;
/*!40000 ALTER TABLE `datasync_task` DISABLE KEYS */;
/*!40000 ALTER TABLE `datasync_task` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsafds`
--

DROP TABLE IF EXISTS `dsafds`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `dsafds` (
  `id var` int(10) unsigned NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id var`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsafds`
--

LOCK TABLES `dsafds` WRITE;
/*!40000 ALTER TABLE `dsafds` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsafds` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fd_account`
--

DROP TABLE IF EXISTS `fd_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fd_account` (
  `FD_ACCOUNT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `FD_ACCOUNT_NUMBER` varchar(100) NOT NULL DEFAULT '',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BANK_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BANK_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `FD_VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `TRANS_MODE` varchar(2) NOT NULL DEFAULT 'DR',
  `TRANS_TYPE` varchar(2) NOT NULL DEFAULT '' COMMENT 'OP/IN (Opening / Investment)',
  `RECEIPT_NO` varchar(50) DEFAULT NULL,
  `ACCOUNT_HOLDER` varchar(100) DEFAULT NULL,
  `INVESTMENT_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `MATURED_ON` datetime DEFAULT NULL,
  `INTEREST_RATE` double NOT NULL DEFAULT '0',
  `INTEREST_AMOUNT` double NOT NULL DEFAULT '0',
  `INTEREST_TYPE` varchar(50) DEFAULT NULL,
  `STATUS` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '1-ACTIVE, 0-CANCELLED',
  `FD_STATUS` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '1= ACTIVE ,0 =CLOSED',
  `FD_SUB_TYPES` varchar(4) NOT NULL DEFAULT 'FD-I',
  `NOTES` varchar(500) DEFAULT NULL,
  `BRANCH_ID` varchar(45) NOT NULL DEFAULT '0',
  PRIMARY KEY (`FD_ACCOUNT_ID`,`BRANCH_ID`),
  KEY `FK_FD_PROJECT_ID` (`PROJECT_ID`),
  KEY `FK_FD_LEDGER_ID` (`LEDGER_ID`),
  KEY `FK_FD_BANK_ID` (`BANK_ID`),
  KEY `FK_FD_VOUCHER_ID` (`FD_VOUCHER_ID`),
  CONSTRAINT `FK_BANK_ID` FOREIGN KEY (`BANK_ID`) REFERENCES `master_bank` (`BANK_ID`),
  CONSTRAINT `FK_fd_account_LEDGER_ID` FOREIGN KEY (`LEDGER_ID`) REFERENCES `master_ledger` (`LEDGER_ID`),
  CONSTRAINT `FK_fd_account_PROJECT_ID` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fd_account`
--

LOCK TABLES `fd_account` WRITE;
/*!40000 ALTER TABLE `fd_account` DISABLE KEYS */;
/*!40000 ALTER TABLE `fd_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fd_registers`
--

DROP TABLE IF EXISTS `fd_registers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fd_registers` (
  `ACCOUNT_NO` varchar(25) NOT NULL DEFAULT '0',
  `FD_NO` varchar(25) DEFAULT NULL,
  `INVESTED_ON` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `MATURITY_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `INTEREST_RATE` decimal(15,2) NOT NULL DEFAULT '0.00',
  `INTEREST_AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `BANK_ACCOUNT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '1=Renewal, 2=Closed',
  `TRANS_MODE` varchar(2) NOT NULL DEFAULT 'TR' COMMENT 'OP =Opening,TR=Transaction',
  `PERIOD_YEAR` int(10) unsigned NOT NULL DEFAULT '0',
  `PERIOD_MTH` int(10) unsigned NOT NULL DEFAULT '0',
  `PERIOD_DAY` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_INTEREST_RECEIVED_PERIODICALLY` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0=Yes,1=No',
  `INTEREST_TERM` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0 =Days ,1=Months, 2=Years',
  `INTEREST_PERIOD` int(10) unsigned NOT NULL DEFAULT '0',
  `FD_REGISTER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`FD_REGISTER_ID`),
  KEY `FK_fd_registers` (`BANK_ACCOUNT_ID`),
  CONSTRAINT `FK_BANK_ACCOUNT_ID` FOREIGN KEY (`BANK_ACCOUNT_ID`) REFERENCES `master_bank_account` (`BANK_ACCOUNT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fd_registers`
--

LOCK TABLES `fd_registers` WRITE;
/*!40000 ALTER TABLE `fd_registers` DISABLE KEYS */;
/*!40000 ALTER TABLE `fd_registers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fd_renewal`
--

DROP TABLE IF EXISTS `fd_renewal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fd_renewal` (
  `FD_ACCOUNT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `FD_RENEWAL_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RENEWAL_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `MATURITY_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `INTEREST_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BANK_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `FD_INTEREST_VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `FD_VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `INTEREST_AMOUNT` double NOT NULL DEFAULT '0',
  `WITHDRAWAL_AMOUNT` double NOT NULL DEFAULT '0',
  `INTEREST_RATE` double NOT NULL DEFAULT '0',
  `INTEREST_TYPE` int(10) unsigned NOT NULL DEFAULT '0',
  `RECEIPT_NO` varchar(50) DEFAULT NULL,
  `RENEWAL_TYPE` varchar(4) NOT NULL DEFAULT '' COMMENT 'IRI=INTEREST RECEIVED\r\nACI=ACCUMULATED iNTEREST\r\nWDI=WITHDRAWAL INTEREST\r\nPWD=PARTIAL WITHDRAW INTEREST',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '1-ACTIVE, 0-CANCELLED',
  `IS_DELETED` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '1-ACTIVE, 0-DELETED',
  `BRANCH_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`FD_RENEWAL_ID`),
  UNIQUE KEY `IndexFDACCOUNT` (`FD_ACCOUNT_ID`,`RENEWAL_DATE`) USING BTREE,
  CONSTRAINT `FK_FD_ACCOUNT_ID` FOREIGN KEY (`FD_ACCOUNT_ID`) REFERENCES `fd_account` (`FD_ACCOUNT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fd_renewal`
--

LOCK TABLES `fd_renewal` WRITE;
/*!40000 ALTER TABLE `fd_renewal` DISABLE KEYS */;
/*!40000 ALTER TABLE `fd_renewal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `headoffice_mapped_ledger`
--

DROP TABLE IF EXISTS `headoffice_mapped_ledger`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `headoffice_mapped_ledger` (
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `HEADOFFICE_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`LEDGER_ID`,`HEADOFFICE_LEDGER_ID`),
  KEY `FK_HEADOFFICE_LEDGER_ID` (`HEADOFFICE_LEDGER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `headoffice_mapped_ledger`
--

LOCK TABLES `headoffice_mapped_ledger` WRITE;
/*!40000 ALTER TABLE `headoffice_mapped_ledger` DISABLE KEYS */;
/*!40000 ALTER TABLE `headoffice_mapped_ledger` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inkindtrans`
--

DROP TABLE IF EXISTS `inkindtrans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `inkindtrans` (
  `INKIND_TRANS_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `INKIND_TRANS_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `SEQUENCE_NO` int(10) unsigned NOT NULL DEFAULT '0',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `ARTICLE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PURPOSE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `CONTRIBUTION_TYPE` varchar(1) NOT NULL DEFAULT 'N',
  `INKIND_QUANTITY` decimal(15,2) DEFAULT NULL,
  `INKIND_UNIT` varchar(10) NOT NULL DEFAULT '',
  `INKIND_VALUE` decimal(15,2) NOT NULL DEFAULT '0.00',
  `RECEIVED_INFORMATION` varchar(1) NOT NULL DEFAULT '' COMMENT 'R =Received,U=Utilised,S=Sold and T=Transferred',
  `DONOR_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BANK_ACCOUNT_NO` int(10) unsigned NOT NULL DEFAULT '0',
  `CHEQUE_NO` varchar(25) NOT NULL DEFAULT '',
  `NARRATION` varchar(500) NOT NULL DEFAULT '',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '1',
  `CREATED_ON` datetime DEFAULT NULL,
  `MODIFIED_ON` datetime DEFAULT NULL,
  `CREATED_BY` int(10) unsigned NOT NULL DEFAULT '0',
  `MODIFIED_BY` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`INKIND_TRANS_ID`),
  KEY `FK_InKindTrans_Ledger` (`LEDGER_ID`),
  KEY `FK_InKindTrans_Project` (`PROJECT_ID`),
  CONSTRAINT `FK_PROJECT_ID` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inkindtrans`
--

LOCK TABLES `inkindtrans` WRITE;
/*!40000 ALTER TABLE `inkindtrans` DISABLE KEYS */;
/*!40000 ALTER TABLE `inkindtrans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ledger_balance`
--

DROP TABLE IF EXISTS `ledger_balance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ledger_balance` (
  `BALANCE_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00' COMMENT 'ONLY DATE',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'BALANCE_DATE,PROJECT_ID,LEDGER_ID(PK)',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `TRANS_MODE` varchar(2) NOT NULL DEFAULT '' COMMENT 'CR-CREDIT, DR-DEBIT\r\n',
  `TRANS_FLAG` varchar(2) NOT NULL DEFAULT 'TR' COMMENT 'OP-OPENING BALANCE, TR-TRANSACTION',
  `BRANCH_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`BALANCE_DATE`,`PROJECT_ID`,`LEDGER_ID`,`BRANCH_ID`),
  KEY `Index_Ledger_Id` (`LEDGER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ledger_balance`
--

LOCK TABLES `ledger_balance` WRITE;
/*!40000 ALTER TABLE `ledger_balance` DISABLE KEYS */;
/*!40000 ALTER TABLE `ledger_balance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mas_user`
--

DROP TABLE IF EXISTS `mas_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mas_user` (
  `USER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `USER_NAME` varchar(45) DEFAULT NULL,
  `PASSWORD` varchar(45) DEFAULT NULL,
  `STATUS` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `FULL_NAME` varchar(45) NOT NULL DEFAULT '',
  `ROLE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`USER_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mas_user`
--

LOCK TABLES `mas_user` WRITE;
/*!40000 ALTER TABLE `mas_user` DISABLE KEYS */;
INSERT INTO `mas_user` VALUES (1,'admin','admin',0000000001,'',0);
/*!40000 ALTER TABLE `mas_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_account_type`
--

DROP TABLE IF EXISTS `master_account_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_account_type` (
  `ACCOUNT_TYPE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ACCOUNT_TYPE` varchar(45) NOT NULL DEFAULT '',
  PRIMARY KEY (`ACCOUNT_TYPE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_account_type`
--

LOCK TABLES `master_account_type` WRITE;
/*!40000 ALTER TABLE `master_account_type` DISABLE KEYS */;
INSERT INTO `master_account_type` VALUES (1,'Bank Account'),(2,'Fixed Deposit');
/*!40000 ALTER TABLE `master_account_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_audit_type`
--

DROP TABLE IF EXISTS `master_audit_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_audit_type` (
  `AUDIT_TYPE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `AUDIT_TYPE` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`AUDIT_TYPE_ID`),
  UNIQUE KEY `UNQ_AUDIT_TYPE` (`AUDIT_TYPE`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_audit_type`
--

LOCK TABLES `master_audit_type` WRITE;
/*!40000 ALTER TABLE `master_audit_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_audit_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_auditing_info`
--

DROP TABLE IF EXISTS `master_auditing_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_auditing_info` (
  `AUDIT_INFO_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `AUDIT_BEGIN` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `AUDIT_END` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `AUDIT_TYPE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `DONAUD_ID` int(11) DEFAULT NULL,
  `NOTES` varchar(500) DEFAULT NULL,
  `AUDITED_ON` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`AUDIT_INFO_ID`),
  UNIQUE KEY `UNQ_PROJECT_ID` (`PROJECT_ID`,`AUDIT_BEGIN`,`AUDIT_END`) USING BTREE,
  KEY `FK_master_auditing_info_2` (`DONAUD_ID`),
  KEY `FK_master_auditing_info_audit_ype` (`AUDIT_TYPE_ID`),
  CONSTRAINT `FK_master_auditing_info_audit_ype` FOREIGN KEY (`AUDIT_TYPE_ID`) REFERENCES `master_audit_type` (`AUDIT_TYPE_ID`),
  CONSTRAINT `FK_DONAUD_ID` FOREIGN KEY (`DONAUD_ID`) REFERENCES `master_donaud` (`DONAUD_ID`),
  CONSTRAINT `FK_master_auditing_info_PROJECT_ID` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_auditing_info`
--

LOCK TABLES `master_auditing_info` WRITE;
/*!40000 ALTER TABLE `master_auditing_info` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_auditing_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_bank`
--

DROP TABLE IF EXISTS `master_bank`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_bank` (
  `BANK_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BANK_CODE` varchar(10) DEFAULT NULL,
  `BANK` varchar(50) NOT NULL DEFAULT '',
  `BRANCH` varchar(50) NOT NULL DEFAULT '',
  `ADDRESS` varchar(100) NOT NULL DEFAULT '',
  `IFSCCODE` varchar(25) DEFAULT NULL,
  `MICRCODE` varchar(25) DEFAULT NULL,
  `CONTACTNUMBER` varchar(15) DEFAULT NULL,
  `ACCOUNTNAME` varchar(50) DEFAULT NULL,
  `SWIFTCODE` varchar(25) DEFAULT NULL,
  `NOTES` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`BANK_ID`),
  UNIQUE KEY `UNQ_BANK_BRANCH` (`BANK`,`BRANCH`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_bank`
--

LOCK TABLES `master_bank` WRITE;
/*!40000 ALTER TABLE `master_bank` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_bank` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_bank_account`
--

DROP TABLE IF EXISTS `master_bank_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_bank_account` (
  `BANK_ACCOUNT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `ACCOUNT_CODE` varchar(25) DEFAULT NULL,
  `ACCOUNT_NUMBER` varchar(50) NOT NULL DEFAULT '',
  `ACCOUNT_HOLDER_NAME` varchar(50) DEFAULT NULL,
  `ACCOUNT_TYPE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BANK_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `DATE_OPENED` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `DATE_CLOSED` datetime DEFAULT NULL,
  `OPERATED_BY` varchar(200) DEFAULT NULL,
  `PERIOD_YEAR` int(10) unsigned NOT NULL DEFAULT '0',
  `PERIOD_MTH` int(10) unsigned NOT NULL DEFAULT '0',
  `PERIOD_DAY` int(10) unsigned NOT NULL DEFAULT '0',
  `INTEREST_RATE` decimal(15,2) NOT NULL DEFAULT '0.00',
  `MATURITY_DATE` datetime DEFAULT NULL,
  `NOTES` varchar(500) DEFAULT NULL,
  `AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `BRANCH_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_FCRA_ACCOUNT` int(1) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`BANK_ACCOUNT_ID`),
  KEY `FK_master_bank_bank_id` (`BANK_ID`),
  CONSTRAINT `FK_master_bank_id` FOREIGN KEY (`BANK_ID`) REFERENCES `master_bank` (`BANK_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_bank_account`
--

LOCK TABLES `master_bank_account` WRITE;
/*!40000 ALTER TABLE `master_bank_account` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_bank_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_contribution_head`
--

DROP TABLE IF EXISTS `master_contribution_head`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_contribution_head` (
  `CONTRIBUTION_ID` int(11) NOT NULL AUTO_INCREMENT,
  `CODE` varchar(15) DEFAULT NULL,
  `FC_PURPOSE` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`CONTRIBUTION_ID`),
  UNIQUE KEY `HEAD` (`FC_PURPOSE`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_contribution_head`
--

LOCK TABLES `master_contribution_head` WRITE;
/*!40000 ALTER TABLE `master_contribution_head` DISABLE KEYS */;
INSERT INTO `master_contribution_head` VALUES (1,'1','Celebration of national events (Independence / Republic day) / festivals etc.'),(2,'2','Theatre / Films'),(3,'3','Maintenance of place of historical and cultural importance'),(4,'4','Preservation of ancient / tribal art forms'),(5,'5','Research'),(6,'6','Cultural shows'),(7,'7','Setting up and running handicraft centre / cottage and Khadi industry / social forestry projects'),(8,'8','Animal husbandry projects'),(9,'9','Income generation projects / schemes'),(10,'10','Micro-finance projects, including setting up banking co-operatives and self-help groups'),(11,'11','Agricultural activity'),(12,'12','Rural Development'),(13,'13','Construction and maintenance of school / college'),(14,'14','Construction and running of hostel for poor students'),(15,'15','Grant of stipend / Scholarship / assistance in cash and kind to poor / deserving children'),(16,'16','Purchase and supply of educational material - books, notebooks etc.'),(17,'17','Conducting adult literacy programs'),(18,'18','Education / Schools for the mentally challenged'),(19,'19','Non-formal education projects / coaching classes'),(20,'20','Construction / Repair / Maintenance of places of worship'),(21,'21','Religious schools / education of priests and preachers'),(22,'22','Publication and distribution of religious literature'),(23,'23','Religious functions'),(24,'24','Maintenance of priests / preachers / other religious functionaries'),(25,'25','Construction / Running of hospital / dispensary / clinic'),(26,'26','Construction of community halls etc.'),(27,'27','Construction and Management of old age home'),(28,'28','Welfare of the aged / widows'),(29,'29','Construction and Management of Orphanage'),(30,'30','Welfare of the orphans'),(31,'31','Construction and Management of dharamshala / shelter'),(32,'32','Holding of free medical / health / family welfare / immunisation camps'),(33,'33','Supply of free medicine, and medical aid, including hearing aids, visual aids, family planning aids etc.'),(34,'34','Provision of aids such as Tricycles, calipers etc. to the handicapped'),(35,'35','Treatment / Rehabilitation of persons suffering from leprosy'),(36,'36','Treatment / Rehabilitation of drug addicts'),(37,'37','Welfare / Empowerment of women'),(38,'38','Welfare of children'),(39,'39','Provision of free clothing / food to the poor, needy and destitute'),(40,'40','Relief / Rehabilitation of victims of natural calamities'),(41,'41','Help to the victims of riots / other disturbances'),(42,'42','Digging of bore wells'),(43,'43','Sanitation including community toilets etc.'),(44,'44','Vocational training - tailoring, motor repairs, computers etc.'),(45,'45','Awareness Camp / Seminar / Workshop / Meeting / Conference'),(46,'46','Providing free legal aid / Running legal aid centre'),(47,'47','Holding sports meet'),(48,'48','Awareness about Acquired Immune Deficiency Syndrome (AIDS) / Treatment and rehabilitation of persons affected by AIDS'),(49,'49','Welfare of the physically and mentally challenged'),(50,'50','Welfare of the Scheduled Castes'),(51,'51','Welfare of the Scheduled Tribes'),(52,'52','Welfare of the Other Backward Classes'),(53,'53','Environmental programs'),(54,'54','Survey for socio-economic and other welfare programs'),(55,'55','Establishment expenses'),(56,'56','Activities other than those mentioned above (Furnish details)');
/*!40000 ALTER TABLE `master_contribution_head` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_cost_centre`
--

DROP TABLE IF EXISTS `master_cost_centre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_cost_centre` (
  `COST_CENTRE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ABBREVATION` varchar(10) DEFAULT NULL,
  `COST_CENTRE_NAME` varchar(50) NOT NULL DEFAULT '',
  `NOTES` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`COST_CENTRE_ID`),
  UNIQUE KEY `UNQ_COST_CENTRE_NAME` (`COST_CENTRE_NAME`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_cost_centre`
--

LOCK TABLES `master_cost_centre` WRITE;
/*!40000 ALTER TABLE `master_cost_centre` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_cost_centre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_cost_centre_category`
--

DROP TABLE IF EXISTS `master_cost_centre_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_cost_centre_category` (
  `COST_CENTRECATEGORY_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `COST_CENTRE_CATEGORY_NAME` varchar(45) NOT NULL DEFAULT '',
  PRIMARY KEY (`COST_CENTRECATEGORY_ID`),
  UNIQUE KEY `UNQ_COST_CATEGORY` (`COST_CENTRE_CATEGORY_NAME`) USING HASH
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_cost_centre_category`
--

LOCK TABLES `master_cost_centre_category` WRITE;
/*!40000 ALTER TABLE `master_cost_centre_category` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_cost_centre_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_country`
--

DROP TABLE IF EXISTS `master_country`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_country` (
  `COUNTRY_ID` int(11) NOT NULL AUTO_INCREMENT,
  `COUNTRY` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `COUNTRY_CODE` varchar(5) DEFAULT NULL,
  `CURRENCY_CODE` varchar(8) DEFAULT NULL,
  `CURRENCY_SYMBOL` varchar(8) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `CURRENCY_NAME` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`COUNTRY_ID`),
  UNIQUE KEY `UNQ_COUNTRY` (`COUNTRY`) USING BTREE,
  UNIQUE KEY `UNQ_COUNTRY_CODE` (`COUNTRY_CODE`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=127 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_country`
--

LOCK TABLES `master_country` WRITE;
/*!40000 ALTER TABLE `master_country` DISABLE KEYS */;
INSERT INTO `master_country` VALUES (1,'India','IND',NULL,'à¤°',NULL),(9,'Saudi Arabia','SAU','SAR','Ø±.Ø³.â€','Saudi Riyal'),(10,'Bulgaria','BGR','BGN','Ð»Ð².','Bulgarian Lev'),(11,'Spain','ESP','EUR','â‚¬','Euro'),(12,'Taiwan','TWN','TWD','NT$','New Taiwan Dollar'),(13,'Czech Republic','CZE','CZK','KÄ','Czech Koruna'),(14,'Denmark','DNK','DKK','kr.','Danish Krone'),(15,'Germany','DEU','EUR','â‚¬','Euro'),(16,'Greece','GRC','EUR','â‚¬','Euro'),(17,'United States','USA','USD','$','US Dollar'),(18,'Finland','FIN','EUR','â‚¬','Euro'),(19,'France','FRA','EUR','â‚¬','Euro'),(20,'Israel','ISR','ILS','â‚ª','Israeli New Shekel'),(21,'Hungary','HUN','HUF','Ft','Hungarian Forint'),(22,'Iceland','ISL','ISK','kr.','Icelandic Krona'),(23,'Italy','ITA','EUR','â‚¬','Euro'),(24,'Japan','JPN','JPY','Â¥','Japanese Yen'),(25,'Korea','KOR','KRW','â‚©','Korean Won'),(26,'Netherlands','NLD','EUR','â‚¬','Euro'),(27,'Norway','NOR','NOK','kr','Norwegian Krone'),(28,'Poland','POL','PLN','zÅ‚','Polish Zloty'),(29,'Brazil','BRA','BRL','R$','Real'),(30,'Switzerland','CHE','CHF','fr.','Swiss Franc'),(31,'Romania','ROU','RON','lei','Romanian Leu'),(32,'Russia','RUS','RUB','Ñ€.','Russian Ruble'),(33,'Croatia','HRV','HRK','kn','Croatian Kuna'),(34,'Slovakia','SVK','EUR','â‚¬','Euro'),(35,'Albania','ALB','ALL','Lek','Albanian Lek'),(36,'Sweden','SWE','SEK','kr','Swedish Krona'),(37,'Thailand','THA','THB','à¸¿','Thai Baht'),(38,'Turkey','TUR','TRY','TL','Turkish Lira'),(39,'Islamic Republic of Pakistan','PAK','PKR','Rs','Pakistan Rupee'),(40,'Indonesia','IDN','IDR','Rp','Indonesian Rupiah'),(41,'Ukraine','UKR','UAH','â‚´','Ukrainian Grivna'),(42,'Belarus','BLR','BYR','Ñ€.','Belarusian Ruble'),(43,'Slovenia','SVN','EUR','â‚¬','Euro'),(44,'Estonia','EST','EEK','kr','Estonian Kroon'),(45,'Latvia','LVA','LVL','Ls','Latvian Lats'),(46,'Lithuania','LTU','LTL','Lt','Lithuanian Litas'),(47,'Tajikistan','TAJ','TJS','Ñ‚.Ñ€.','Ruble'),(48,'Iran','IRN','IRR','Ø±ÙŠØ§Ù„','Iranian Rial'),(49,'Vietnam','VNM','VND','â‚«','Vietnamese Dong'),(50,'Armenia','ARM','AMD','Õ¤Ö€.','Armenian Dram'),(51,'Azerbaijan','AZE','AZN','man.','Azerbaijanian Manat'),(52,'Macedonia (FYROM)','MKD','MKD','Ð´ÐµÐ½.','Macedonian Denar'),(53,'South Africa','ZAF','ZAR','R','South African Rand'),(54,'Georgia','GEO','GEL','Lari','Lari'),(55,'Faroe Islands','FRO','DKK','kr.','Danish Krone'),(56,'Puerto Rico','PRI','USD','$','US Dollar'),(57,'Malta','MLT','EUR','â‚¬','Euro'),(58,'Malaysia','MYS','MYR','RM','Malaysian Ringgit'),(59,'Kazakhstan','KAZ','KZT','Ð¢','Tenge'),(60,'Kyrgyzstan','KGZ','KGS','ÑÐ¾Ð¼','som'),(61,'Kenya','KEN','KES','S','Kenyan Shilling'),(62,'Turkmenistan','TKM','TMT','m.','Turkmen manat'),(63,'United Kingdom','GBR','GBP','Â£','UK Pound Sterling'),(64,'Cambodia','KHM','KHR','áŸ›','Riel'),(65,'Lao P.D.R.','LAO','LAK','â‚­','Kip'),(66,'Syria','SYR','SYP','Ù„.Ø³.â€','Syrian Pound'),(67,'Sri Lanka','LKA','LKR','à¶»à·”.','Sri Lanka Rupee'),(68,'Canada','CAN','CAD','$','Canadian Dollar'),(69,'Ethiopia','ETH','ETB','ETB','Ethiopian Birr'),(70,'Nepal','NPL','NPR','à¤°à¥','Nepalese Rupees'),(71,'Afghanistan','AFG','AFN','Ø‹','Afghani'),(72,'Philippines','PHL','PHP','PhP','Philippine Peso'),(73,'Maldives','MDV','MVR','Þƒ.','Rufiyaa'),(74,'Nigeria','NGA','NIO','N','Nigerian Naira'),(75,'Bolivia','BOL','BOB','$b','Boliviano'),(76,'Luxembourg','LUX','EUR','â‚¬','Euro'),(77,'Greenland','GRL','DKK','kr.','Danish Krone'),(78,'New Zealand','NZL','NZD','$','New Zealand Dollar'),(79,'Guatemala','GTM','GTQ','Q','Guatemalan Quetzal'),(80,'Rwanda','RWA','RWF','RWF','Rwandan Franc'),(81,'Senegal','SEN','XOF','XOF','XOF Senegal'),(82,'Iraq','IRQ','IQD','Ø¯.Ø¹.â€','Iraqi Dinar'),(83,'Algeria','DZA','DZD','DZD','Algerian Dinar'),(84,'Ecuador','ECU','USD','$','US Dollar'),(85,'Egypt','EGY','EGP','Ø¬.Ù….â€','Egyptian Pound'),(86,'Hong Kong S.A.R.','HKG','HKD','HK$','Hong Kong Dollar'),(87,'Austria','AUT','EUR','â‚¬','Euro'),(88,'Australia','AUS','AUD','$','Australian Dollar'),(89,'Serbia and Montenegro (Former)','SCG','CSD','Ð”Ð¸Ð½.','Serbian Dinar'),(90,'Peru','PER','PEN','S/.','Peruvian Nuevo Sol'),(91,'Libya','LBY','LYD','Ø¯.Ù„.â€','Libyan Dinar'),(92,'Singapore','SGP','SGD','$','Singapore Dollar'),(93,'Bosnia and Herzegovina','BIH','BAM','KM','Convertible Marks'),(94,'Macao S.A.R.','MAC','MOP','MOP','Macao Pataca'),(95,'Liechtenstein','LIE','CHF','CHF','Swiss Franc'),(96,'Costa Rica','CRI','CRC','â‚¡','Costa Rican Colon'),(97,'Morocco','MAR','MAD','Ø¯.Ù….â€','Moroccan Dirham'),(98,'Ireland','IRL','EUR','â‚¬','Euro'),(99,'Panama','PAN','PAB','B/.','Panamanian Balboa'),(100,'Principality of Monaco','MCO','EUR','â‚¬','Euro'),(101,'Tunisia','TUN','TND','Ø¯.Øª.â€','Tunisian Dinar'),(102,'Dominican Republic','DOM','DOP','RD$','Dominican Peso'),(103,'Oman','OMN','OMR','Ø±.Ø¹.â€','Omani Rial'),(104,'Jamaica','JAM','JMD','J$','Jamaican Dollar'),(105,'Bolivarian Republic of Venezuela','VEN','VEF','Bs. F.','Venezuelan Bolivar'),(106,'Yemen','YEM','YER','Ø±.ÙŠ.â€','Yemeni Rial'),(107,'Caribbean','CR','USD','$','US Dollar'),(108,'Colombia','COL','COP','$','Colombian Peso'),(109,'Serbia','SRB','RSD','Din.','Serbian Dinar'),(110,'Belize','BLZ','BZD','BZ$','Belize Dollar'),(111,'Jordan','JOR','JOD','Ø¯.Ø§.â€','Jordanian Dinar'),(112,'Trinidad and Tobago','TTO','TTD','TT$','Trinidad Dollar'),(113,'Argentina','ARG','ARS','$','Argentine Peso'),(114,'Montenegro','MNE','EUR','â‚¬','Euro'),(115,'Lebanon','LBN','LBP','Ù„.Ù„.â€','Lebanese Pound'),(116,'Zimbabwe','ZWE','ZWL','Z$','Zimbabwe Dollar'),(117,'Kuwait','KWT','KWD','Ø¯.Ùƒ.â€','Kuwaiti Dinar'),(118,'Chile','CHL','CLP','$','Chilean Peso'),(119,'U.A.E.','ARE','AED','Ø¯.Ø¥.â€','UAE Dirham'),(120,'Uruguay','URY','UYU','$U','Peso Uruguayo'),(121,'Bahrain','BHR','BHD','Ø¯.Ø¨.â€','Bahraini Dinar'),(122,'Paraguay','PRY','PYG','Gs','Paraguay Guarani'),(123,'Qatar','QAT','QAR','Ø±.Ù‚.â€','Qatari Rial'),(124,'El Salvador','SLV','USD','$','US Dollar'),(125,'Honduras','HND','HNL','L.','Honduran Lempira'),(126,'Nicaragua','NIC','NIO','C$','Nicaraguan Cordoba Oro');
/*!40000 ALTER TABLE `master_country` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_division`
--

DROP TABLE IF EXISTS `master_division`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_division` (
  `DIVISION_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `DIVISION` varchar(45) NOT NULL DEFAULT '',
  PRIMARY KEY (`DIVISION_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_division`
--

LOCK TABLES `master_division` WRITE;
/*!40000 ALTER TABLE `master_division` DISABLE KEYS */;
INSERT INTO `master_division` VALUES (1,'Local'),(2,'Foreign');
/*!40000 ALTER TABLE `master_division` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_donaud`
--

DROP TABLE IF EXISTS `master_donaud`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_donaud` (
  `DONAUD_ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(50) DEFAULT NULL,
  `TYPE` int(10) unsigned DEFAULT NULL COMMENT 'Institutional -1   Individual -2 ',
  `PLACE` varchar(30) DEFAULT NULL,
  `COMPANY_NAME` varchar(45) DEFAULT NULL,
  `COUNTRY_ID` int(11) DEFAULT NULL,
  `PINCODE` varchar(10) DEFAULT NULL,
  `PHONE` varchar(20) DEFAULT NULL,
  `FAX` varchar(50) DEFAULT NULL,
  `EMAIL` varchar(50) DEFAULT NULL,
  `IDENTITYKEY` int(10) unsigned DEFAULT NULL COMMENT '0 -Donor   1- Auditor',
  `URL` varchar(50) DEFAULT NULL,
  `FCDONOR` int(10) unsigned DEFAULT NULL COMMENT '0 - No    1- yes',
  `STATE` varchar(30) DEFAULT NULL,
  `ADDRESS` varchar(100) DEFAULT NULL,
  `NOTES` varchar(500) DEFAULT NULL,
  `PAN` varchar(20) DEFAULT NULL,
  `BRANCH_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `CUSTOMERID` int(10) unsigned NOT NULL DEFAULT '0',
  `STATE_ID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`DONAUD_ID`),
  KEY `FK_master_donaud_1` (`COUNTRY_ID`),
  CONSTRAINT `FK_master_donaud_1` FOREIGN KEY (`COUNTRY_ID`) REFERENCES `master_country` (`COUNTRY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_donaud`
--

LOCK TABLES `master_donaud` WRITE;
/*!40000 ALTER TABLE `master_donaud` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_donaud` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_executive_committee`
--

DROP TABLE IF EXISTS `master_executive_committee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_executive_committee` (
  `EXECUTIVE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `EXECUTIVE` varchar(50) NOT NULL DEFAULT '',
  `NAME` varchar(50) DEFAULT NULL,
  `DATE_OF_BIRTH` datetime DEFAULT NULL,
  `RELIGION` varchar(30) DEFAULT NULL,
  `ROLE` varchar(30) DEFAULT NULL,
  `NATIONALITY` varchar(25) NOT NULL DEFAULT '',
  `OCCUPATION` varchar(25) DEFAULT NULL,
  `ASSOCIATION` varchar(50) DEFAULT NULL,
  `OFFICE_BEARER` varchar(50) DEFAULT NULL,
  `PLACE` varchar(30) DEFAULT NULL,
  `STATE` varchar(30) DEFAULT NULL,
  `COUNTRY_ID` int(11) DEFAULT NULL,
  `ADDRESS` varchar(150) DEFAULT NULL,
  `PIN_CODE` varchar(10) DEFAULT NULL,
  `PAN_SSN` varchar(10) DEFAULT NULL,
  `PHONE` varchar(20) DEFAULT NULL,
  `FAX` varchar(20) DEFAULT NULL,
  `EMAIL` varchar(30) DEFAULT NULL,
  `URL` varchar(30) DEFAULT NULL,
  `DATE_OF_APPOINTMENT` datetime DEFAULT NULL,
  `DATE_OF_EXIT` datetime DEFAULT NULL,
  `IMAGES` longblob,
  `NOTES` varchar(500) DEFAULT NULL,
  `CUSTOMERID` int(10) unsigned NOT NULL DEFAULT '0',
  `STATE_ID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`EXECUTIVE_ID`),
  KEY `FK_MASTER_COUNTRY` (`COUNTRY_ID`),
  CONSTRAINT `FK_MASTER_COUNTRY` FOREIGN KEY (`COUNTRY_ID`) REFERENCES `master_country` (`COUNTRY_ID`),
  CONSTRAINT `FK_master_executive_committee_1` FOREIGN KEY (`COUNTRY_ID`) REFERENCES `master_country` (`COUNTRY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_executive_committee`
--

LOCK TABLES `master_executive_committee` WRITE;
/*!40000 ALTER TABLE `master_executive_committee` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_executive_committee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_headoffice_ledger`
--

DROP TABLE IF EXISTS `master_headoffice_ledger`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_headoffice_ledger` (
  `HEADOFFICE_LEDGER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LEDGER_CODE` varchar(15) NOT NULL DEFAULT '',
  `LEDGER_NAME` varchar(100) NOT NULL DEFAULT '',
  `GROUP_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_TYPE` varchar(3) NOT NULL DEFAULT 'GN' COMMENT '''CA'',''BK'',''FD'',''GN'',''IK''',
  `LEDGER_SUB_TYPE` varchar(3) NOT NULL DEFAULT 'GN' COMMENT '''LQ'',GN LQ(CA,BK,FD)',
  `BANK_ACCOUNT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_COST_CENTER` int(1) unsigned NOT NULL DEFAULT '0',
  `NOTES` varchar(500) DEFAULT NULL,
  `IS_BANK_INTEREST_LEDGER` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0-No,1-Yes',
  `SORT_ID` int(10) unsigned NOT NULL DEFAULT '255',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0-Active,1-InActive',
  `ACCESS_FLAG` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`HEADOFFICE_LEDGER_ID`),
  KEY `FK_master_bank_account_id` (`BANK_ACCOUNT_ID`),
  KEY `FK_master_ledger_group_id` (`GROUP_ID`),
  KEY `UNQ_LEDGER` (`LEDGER_NAME`) USING BTREE,
  CONSTRAINT `MASTER_LEDGER_GROUP` FOREIGN KEY (`GROUP_ID`) REFERENCES `master_ledger_group` (`GROUP_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_headoffice_ledger`
--

LOCK TABLES `master_headoffice_ledger` WRITE;
/*!40000 ALTER TABLE `master_headoffice_ledger` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_headoffice_ledger` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_inkind_article`
--

DROP TABLE IF EXISTS `master_inkind_article`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_inkind_article` (
  `ARTICLE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ABBREVATION` varchar(10) NOT NULL DEFAULT '',
  `ARTICLE` varchar(100) NOT NULL DEFAULT '',
  `OP_QUANTITY` float DEFAULT NULL,
  `OP_VALUE` float DEFAULT NULL,
  `NOTES` varchar(500) DEFAULT NULL,
  `RECORDSTATUS` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000' COMMENT '0 =ACTIVE AND 1 =INACTIVE ',
  PRIMARY KEY (`ARTICLE_ID`),
  UNIQUE KEY `UNQ_ABBREVATION` (`ABBREVATION`,`ARTICLE`) USING HASH,
  KEY `UNQ_ARTICLE` (`ARTICLE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_inkind_article`
--

LOCK TABLES `master_inkind_article` WRITE;
/*!40000 ALTER TABLE `master_inkind_article` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_inkind_article` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_insti_perference`
--

DROP TABLE IF EXISTS `master_insti_perference`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_insti_perference` (
  `CUSTOMERID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `INSTITUTENAME` varchar(150) NOT NULL DEFAULT '0',
  `SOCIETYNAME` varchar(100) NOT NULL DEFAULT '0',
  `CONTACTPERSON` varchar(100) DEFAULT NULL,
  `ADDRESS` varchar(200) DEFAULT NULL,
  `PLACE` varchar(100) DEFAULT NULL,
  `STATE` varchar(100) DEFAULT NULL,
  `COUNTRY_ID` int(11) DEFAULT NULL,
  `PINCODE` varchar(20) DEFAULT NULL,
  `PHONE` varchar(20) DEFAULT NULL,
  `FAX` varchar(20) DEFAULT NULL,
  `EMAIL` varchar(100) DEFAULT NULL,
  `URL` varchar(100) DEFAULT NULL,
  `REGNO` varchar(100) DEFAULT NULL,
  `REGDATE` datetime DEFAULT NULL,
  `PERMISSIONNO` varchar(50) DEFAULT NULL,
  `PERMISSIONDATE` datetime DEFAULT NULL,
  `A12NO` varchar(100) DEFAULT NULL,
  `PANNO` varchar(50) DEFAULT NULL,
  `GIRNO` varchar(50) DEFAULT NULL,
  `TANNO` varchar(50) DEFAULT NULL,
  `ASSOCIATIONNATURE` varchar(15) DEFAULT NULL COMMENT '0-Cultural,1-Ecomomic,2-Educational,3-Religious,4-Social',
  `DENOMINATION` int(11) DEFAULT NULL COMMENT '0-Hindu,1-Sikh,2-Muslim,3-Christian,4-Buddhist,5-Others',
  `OTHER_ASSOCIATION_NATURE` varchar(100) DEFAULT NULL,
  `OTHER_DENOMINATION` varchar(100) DEFAULT NULL,
  `FCRINO` varchar(30) DEFAULT NULL,
  `FCRIREGDATE` datetime DEFAULT NULL,
  `EIGHTYGNO` varchar(30) DEFAULT NULL,
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `STATE_ID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`CUSTOMERID`),
  KEY `FK_COUNTRY_ID` (`COUNTRY_ID`),
  CONSTRAINT `FK_COUNTRY_ID` FOREIGN KEY (`COUNTRY_ID`) REFERENCES `master_country` (`COUNTRY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_insti_perference`
--

LOCK TABLES `master_insti_perference` WRITE;
/*!40000 ALTER TABLE `master_insti_perference` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_insti_perference` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_ledger`
--

DROP TABLE IF EXISTS `master_ledger`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_ledger` (
  `LEDGER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LEDGER_CODE` varchar(15) DEFAULT NULL,
  `LEDGER_NAME` varchar(100) NOT NULL DEFAULT '',
  `GROUP_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_TYPE` varchar(3) NOT NULL DEFAULT 'GN' COMMENT '''CA'',''BK'',''FD'',''GN'',''IK''',
  `LEDGER_SUB_TYPE` varchar(3) NOT NULL DEFAULT 'GN' COMMENT '''LQ'',GN LQ(CA,BK,FD)',
  `BANK_ACCOUNT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_COST_CENTER` int(1) unsigned NOT NULL DEFAULT '0',
  `IS_TDS_LEDGER` int(10) unsigned NOT NULL DEFAULT '0',
  `NOTES` varchar(500) DEFAULT NULL,
  `IS_BANK_INTEREST_LEDGER` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0-No,1-Yes',
  `SORT_ID` int(10) unsigned NOT NULL DEFAULT '255',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0-Active,1-InActive',
  `ACCESS_FLAG` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_BRANCH_LEDGER` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`LEDGER_ID`),
  UNIQUE KEY `LEDGER_NAME` (`LEDGER_NAME`) USING HASH,
  KEY `FK_master_bank_account_id` (`BANK_ACCOUNT_ID`),
  KEY `UNQ_LEDGER` (`LEDGER_NAME`) USING BTREE,
  KEY `FK_master_ledger_group_id` (`GROUP_ID`),
  CONSTRAINT `FK_master_ledger_group_id` FOREIGN KEY (`GROUP_ID`) REFERENCES `master_ledger_group` (`GROUP_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_ledger`
--

LOCK TABLES `master_ledger` WRITE;
/*!40000 ALTER TABLE `master_ledger` DISABLE KEYS */;
INSERT INTO `master_ledger` VALUES (1,'CS911','Cash',13,'GN','GN',0,0,0,'',0,1,0,0,0),(2,'FD912','Fixed Deposit',14,'GN','FD',0,0,0,'',0,2,0,0,0),(3,'CF913','Capital Fund',21,'GN','GN',0,0,0,'',0,3,0,0,0);
/*!40000 ALTER TABLE `master_ledger` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_ledger_group`
--

DROP TABLE IF EXISTS `master_ledger_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_ledger_group` (
  `GROUP_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `GROUP_CODE` varchar(10) DEFAULT NULL,
  `LEDGER_GROUP` varchar(100) NOT NULL DEFAULT '',
  `PARENT_GROUP_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `NATURE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `MAIN_GROUP_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `IMAGE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `ACCESS_FLAG` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0-Access,1-Editable,2-Readonly',
  `SORT_ORDER` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`GROUP_ID`),
  UNIQUE KEY `UNQ_GRP` (`LEDGER_GROUP`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_ledger_group`
--

LOCK TABLES `master_ledger_group` WRITE;
/*!40000 ALTER TABLE `master_ledger_group` DISABLE KEYS */;
INSERT INTO `master_ledger_group` VALUES (1,'01','Income',1,1,1,0,2,0),(2,'02','Expenses',2,2,2,0,2,0),(3,'03','Assets',3,3,3,0,2,0),(4,'04','Liabilities',4,4,4,0,2,0),(5,'05','Direct Incomes',1,1,5,0,0,0),(6,'06','Indirect Incomes',1,1,6,0,0,0),(7,'07','Sales Accounts',1,1,7,0,0,0),(8,'08','Direct Expenses',2,2,8,0,0,0),(9,'09','Indirect Expenses',2,2,9,0,0,0),(10,'10','Purchase Accounts',2,2,10,0,0,0),(11,'18','Current Assets',3,3,11,0,2,0),(12,'101','Bank Accounts',11,3,11,0,2,0),(13,'100','Cash-in-hand',11,3,11,0,2,0),(14,'102','Deposits (Asset)',11,3,11,0,2,0),(15,'16','Loans and Advances (Asset)',11,3,11,0,0,0),(16,'14','Stock-in-hand',11,3,11,0,0,0),(17,'15','Sundry Debtors',11,3,11,0,0,0),(18,'11','Fixed Assets',3,3,18,0,0,0),(19,'12','Investments',3,3,19,0,0,0),(20,'13','Misc. Expenses (Asset)',3,3,20,0,0,0),(21,'21','Capital Fund',4,4,21,0,2,0),(22,'22','Reserves and Surplus (Fixed Assets)',4,4,22,0,0,0),(23,'27','Current Liabilities',4,4,23,0,0,0),(24,'24','Duties and Taxes',23,4,23,0,0,0),(25,'25','Provisions',23,4,23,0,0,0),(26,'26','Sundry Creditors',23,4,23,0,0,0),(27,'23','Loans and Advances (Liability)',4,4,27,0,0,0),(28,'28','Bank OD A/c',27,4,27,0,0,0),(29,'29','Secured Loans',27,4,27,0,0,0),(30,'30','Unsecured Loans',27,4,27,0,0,0),(31,'31','Suspense A/c',4,4,4,0,0,0);
/*!40000 ALTER TABLE `master_ledger_group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_lock_trans`
--

DROP TABLE IF EXISTS `master_lock_trans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_lock_trans` (
  `LOCK_TRANS_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LOCK_TYPE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `DATE_FROM` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `DATE_TO` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `PASSWORD` varchar(500) NOT NULL DEFAULT '',
  `REASON` varchar(500) NOT NULL DEFAULT '',
  `PASSWORD_HINT` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`LOCK_TRANS_ID`),
  KEY `PK_Master_Lock_Trans` (`LOCK_TYPE_ID`),
  CONSTRAINT `PK_Master_Lock_Trans` FOREIGN KEY (`LOCK_TYPE_ID`) REFERENCES `master_lock_type` (`LOCK_TYPE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_lock_trans`
--

LOCK TABLES `master_lock_trans` WRITE;
/*!40000 ALTER TABLE `master_lock_trans` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_lock_trans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_lock_type`
--

DROP TABLE IF EXISTS `master_lock_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_lock_type` (
  `LOCK_TYPE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LOCK_TYPE` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`LOCK_TYPE_ID`),
  UNIQUE KEY `uqi_lock_type` (`LOCK_TYPE`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_lock_type`
--

LOCK TABLES `master_lock_type` WRITE;
/*!40000 ALTER TABLE `master_lock_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_lock_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_nature`
--

DROP TABLE IF EXISTS `master_nature`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_nature` (
  `NATURE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `NATURE` varchar(45) NOT NULL DEFAULT '',
  PRIMARY KEY (`NATURE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_nature`
--

LOCK TABLES `master_nature` WRITE;
/*!40000 ALTER TABLE `master_nature` DISABLE KEYS */;
INSERT INTO `master_nature` VALUES (1,'Income'),(2,'Expenses'),(3,'Assets'),(4,'Liabilities');
/*!40000 ALTER TABLE `master_nature` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_project`
--

DROP TABLE IF EXISTS `master_project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_project` (
  `PROJECT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PROJECT_CODE` varchar(15) DEFAULT NULL,
  `PROJECT` varchar(100) NOT NULL DEFAULT '',
  `DIVISION_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `ACCOUNT_DATE` datetime DEFAULT NULL,
  `DATE_STARTED` datetime DEFAULT NULL,
  `DATE_CLOSED` datetime DEFAULT NULL,
  `DESCRIPTION` varchar(200) DEFAULT NULL,
  `NOTES` varchar(500) DEFAULT NULL,
  `PROJECT_CATEGORY_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `DELETE_FLAG` int(10) unsigned NOT NULL DEFAULT '0',
  `CUSTOMERID` int(10) unsigned NOT NULL DEFAULT '0',
  `CONTRIBUTION_ID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'FC6 Purpose Id',
  PRIMARY KEY (`PROJECT_ID`),
  UNIQUE KEY `unq_project` (`PROJECT`) USING HASH,
  KEY `FK_master_project_Category_ID` (`PROJECT_CATEGORY_ID`),
  CONSTRAINT `FK_Pro_cate_Id` FOREIGN KEY (`PROJECT_CATEGORY_ID`) REFERENCES `master_project_catogory` (`PROJECT_CATOGORY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_project`
--

LOCK TABLES `master_project` WRITE;
/*!40000 ALTER TABLE `master_project` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_project` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_project_catogory`
--

DROP TABLE IF EXISTS `master_project_catogory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_project_catogory` (
  `PROJECT_CATOGORY_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PROJECT_CATOGORY_NAME` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`PROJECT_CATOGORY_ID`),
  UNIQUE KEY `UNQ_PROJECT_CATOGORY_NAME` (`PROJECT_CATOGORY_NAME`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_project_catogory`
--

LOCK TABLES `master_project_catogory` WRITE;
/*!40000 ALTER TABLE `master_project_catogory` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_project_catogory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_rights`
--

DROP TABLE IF EXISTS `master_rights`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_rights` (
  `MASTER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `MASTER_NAME` varchar(150) NOT NULL DEFAULT '',
  `ALLOW_ACCESS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0- Read only,1 Full Access',
  PRIMARY KEY (`MASTER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_rights`
--

LOCK TABLES `master_rights` WRITE;
/*!40000 ALTER TABLE `master_rights` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_rights` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_setting`
--

DROP TABLE IF EXISTS `master_setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_setting` (
  `SETTING_NAME` varchar(100) NOT NULL DEFAULT '',
  `VALUE` varchar(50) NOT NULL DEFAULT '',
  `USER_ID` int(10) unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`USER_ID`,`SETTING_NAME`),
  KEY `SETTING_NAME` (`SETTING_NAME`) USING BTREE,
  CONSTRAINT `USER` FOREIGN KEY (`USER_ID`) REFERENCES `user_info` (`USER_ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_setting`
--

LOCK TABLES `master_setting` WRITE;
/*!40000 ALTER TABLE `master_setting` DISABLE KEYS */;
INSERT INTO `master_setting` VALUES ('Country','1',1),('Currency','à¤°',1),('CurrencyCode','',1),('CurrencyCodePosition','',1),('CurrencyNegativePattern','14',1),('CurrencyNegativeSign','( )',1),('CurrencyPosition','Before',1),('CurrencyPositivePattern','2',1),('DecimalPlaces','2',1),('DecimalSeparator','.',1),('DigitGrouping','3,2,2',1),('GroupingSeparator',',',1),('HighNaturedAmt','500',1),('TransEntryMethod','',1),('UIDateFormat','dd/MM/yyyy',1),('UIDateSeparator','/',1),('UIFilterMode','Starts',1),('UILanguage','en-US',1),('UIProjSelection','1',1),('UIThemes','Office 2010 Silver',1),('UITransClose','1',1),('UITransType','Codeless',1);
/*!40000 ALTER TABLE `master_setting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_state`
--

DROP TABLE IF EXISTS `master_state`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_state` (
  `STATE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `STATE_NAME` varchar(100) NOT NULL DEFAULT '',
  `COUNTRY_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`STATE_ID`),
  UNIQUE KEY `UNQ_STATE_NAME` (`STATE_NAME`),
  KEY `FK_master_state_Country` (`COUNTRY_ID`),
  CONSTRAINT `FK_master_state_Country` FOREIGN KEY (`COUNTRY_ID`) REFERENCES `master_country` (`COUNTRY_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_state`
--

LOCK TABLES `master_state` WRITE;
/*!40000 ALTER TABLE `master_state` DISABLE KEYS */;
INSERT INTO `master_state` VALUES (1,' Andhra Pradesh ',1),(2,'Arunachal Pradesh AR ',1),(3,'Assam AS ',1),(4,' Bihar BR ',1),(5,' Chhattisgarh CT ',1),(6,' Goa GA ',1),(7,' Gujarat GJ ',1),(8,'Haryana HR ',1),(9,'Himachal Pradesh HP ',1),(10,' Jammu and Kashmir JK ',1),(11,' Jharkhand JH ',1),(12,'Karnataka KA ',1),(13,' Kerala KL ',1),(14,' Madhya Pradesh MP ',1),(15,'Maharashtra MH ',1),(16,' Manipur MN ',1),(17,' Meghalaya ML ',1),(18,'Mizoram MZ ',1),(19,' Nagaland NL ',1),(20,' Orissa OR ',1),(21,'Punjab PB ',1),(22,' Rajasthan RJ ',1),(23,' Sikkim SK ',1),(24,'Tamil Nadu TN ',1),(25,' Tripura TR ',1),(26,' Uttarakhand UT ',1),(27,'Uttar Pradesh UP ',1),(28,' West Bengal WB ',1),(29,'Andaman and Nicobar Islands AN ',1),(30,'Chandigarh CH ',1),(31,' Dadra and Nagar Haveli DN ',1),(32,'Daman and Diu DD ',1),(33,' Delhi DL ',1),(34,' Lakshadweep LD ',1),(35,' Puducherry PY ',1);
/*!40000 ALTER TABLE `master_state` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_voucher`
--

DROP TABLE IF EXISTS `master_voucher`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_voucher` (
  `VOUCHER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `VOUCHER_NAME` varchar(45) NOT NULL DEFAULT '',
  `VOUCHER_TYPE` int(10) unsigned NOT NULL DEFAULT '0',
  `VOUCHER_METHOD` int(10) unsigned NOT NULL DEFAULT '0',
  `PREFIX_CHAR` varchar(10) NOT NULL DEFAULT '',
  `SUFFIX_CHAR` varchar(10) NOT NULL DEFAULT '',
  `STARTING_NUMBER` int(10) unsigned NOT NULL DEFAULT '0',
  `NUMBERICAL_WITH` int(10) unsigned NOT NULL DEFAULT '0',
  `PREFIX_WITH_ZERO` int(10) unsigned NOT NULL DEFAULT '0',
  `MONTH` varchar(25) NOT NULL DEFAULT '',
  `DURATION` int(10) unsigned NOT NULL DEFAULT '0',
  `ALLOW_DUPLICATE` int(10) unsigned NOT NULL DEFAULT '0',
  `NOTE` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`VOUCHER_ID`),
  UNIQUE KEY `VOUCHER_NAME` (`VOUCHER_NAME`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_voucher`
--

LOCK TABLES `master_voucher` WRITE;
/*!40000 ALTER TABLE `master_voucher` DISABLE KEYS */;
INSERT INTO `master_voucher` VALUES (1,'Receipts',1,1,'','',1,2,0,'January',1,0,''),(2,'Payments',2,1,'','',1,2,0,'January',1,0,''),(3,'Contra',3,1,'','',1,2,0,'January',1,0,''),(4,'Journal',4,1,'','',1,2,0,'January',1,0,'');
/*!40000 ALTER TABLE `master_voucher` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payroll`
--

DROP TABLE IF EXISTS `payroll`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payroll` (
  `PAYROLLID` int(10) unsigned NOT NULL DEFAULT '0',
  `FROMDATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `TODATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payroll`
--

LOCK TABLES `payroll` WRITE;
/*!40000 ALTER TABLE `payroll` DISABLE KEYS */;
/*!40000 ALTER TABLE `payroll` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payroll_ledger`
--

DROP TABLE IF EXISTS `payroll_ledger`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payroll_ledger` (
  `TYPE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PROCESS_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`TYPE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payroll_ledger`
--

LOCK TABLES `payroll_ledger` WRITE;
/*!40000 ALTER TABLE `payroll_ledger` DISABLE KEYS */;
/*!40000 ALTER TABLE `payroll_ledger` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prcompmonth`
--

DROP TABLE IF EXISTS `prcompmonth`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prcompmonth` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PAYROLLID` int(10) unsigned NOT NULL DEFAULT '0',
  `SALARYGROUPID` int(10) unsigned DEFAULT NULL,
  `COMPONENTID` int(10) unsigned DEFAULT NULL,
  `TYPE` varchar(20) DEFAULT NULL,
  `DEFVALUE` varchar(50) DEFAULT NULL,
  `EQUATION` varchar(500) DEFAULT NULL,
  `EQUATIONID` varchar(500) DEFAULT NULL,
  `MAXSLAB` decimal(15,2) DEFAULT NULL,
  `LNKVALUE` varchar(50) DEFAULT NULL,
  `COMP_ORDER` int(10) unsigned DEFAULT NULL,
  `COMPROUND` int(10) unsigned DEFAULT NULL,
  `IFCONDITION` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_PRCOMPMONTH_1` (`COMPONENTID`),
  KEY `FK_prcompmonth_2` (`PAYROLLID`),
  CONSTRAINT `FK_PRCOMPMONTH_1` FOREIGN KEY (`COMPONENTID`) REFERENCES `prcomponent` (`COMPONENTID`),
  CONSTRAINT `FK_prcompmonth_2` FOREIGN KEY (`PAYROLLID`) REFERENCES `prcreate` (`PAYROLLID`)
) ENGINE=InnoDB AUTO_INCREMENT=978 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prcompmonth`
--

LOCK TABLES `prcompmonth` WRITE;
/*!40000 ALTER TABLE `prcompmonth` DISABLE KEYS */;
/*!40000 ALTER TABLE `prcompmonth` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prcomponent`
--

DROP TABLE IF EXISTS `prcomponent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prcomponent` (
  `COMPONENTID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `COMPONENT` varchar(50) DEFAULT NULL,
  `DESCRIPTION` varchar(100) DEFAULT NULL,
  `TYPE` varchar(50) DEFAULT NULL,
  `DEFVALUE` varchar(100) DEFAULT NULL,
  `LINKVALUE` varchar(50) DEFAULT NULL,
  `EQUATION` varchar(4000) DEFAULT NULL,
  `EQUATIONID` varchar(4000) DEFAULT NULL,
  `MAXSLAP` decimal(15,2) DEFAULT NULL,
  `COMPROUND` decimal(15,2) DEFAULT NULL,
  `IFCONDITION` varchar(50) DEFAULT NULL,
  `SHOWINBROWSE` int(10) unsigned DEFAULT NULL,
  `RELATEDCOMPONENTS` varchar(40) DEFAULT NULL,
  `ISEDITABLE` int(10) unsigned NOT NULL DEFAULT '0',
  `ACCESS_FLAG` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`COMPONENTID`),
  UNIQUE KEY `unq_component` (`COMPONENT`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prcomponent`
--

LOCK TABLES `prcomponent` WRITE;
/*!40000 ALTER TABLE `prcomponent` DISABLE KEYS */;
INSERT INTO `prcomponent` VALUES (13,'BASIC','Basic Pay','0','0','Basicpay','','',0.00,1.00,'0',0,'',0,2,0),(14,'DA','Dearness Allowance','0','1653','','','',0.00,1.00,'0',0,'',0,2,0),(15,'HRA','House Rent Allowance','0','0','','<BASIC>*40/100','<13>*40/100~0~0',0.00,1.00,'1',0,'ÃƒÆ’Ã‚Âª13ÃƒÆ’Ã‚Âª',0,2,0),(16,'PF WAGES','Basic and Dearness Allowance','1','0','','<BASIC>+<DA>','<13>+<14>~0~0',0.00,1.00,'1',0,'ÃƒÆ’Ã‚Âª13ÃƒÆ’Ã‚Âª14ÃƒÆ’Ã‚Âª',0,2,0),(17,'PF','Provident Fund','1','0','','<PF WAGES>*12/100','<16>*12/100~0~0',0.00,1.00,'1',0,'ÃƒÆ’Ã‚Âª16ÃƒÆ’Ã‚Âª',0,2,0),(18,'PT','Professional Tax','1','0','','','',0.00,1.00,'0',0,'',0,2,0),(19,'GROSS WAGES','GROSS SALARY','0','0','','<BASIC>+<DA>+<HRA>','<13>+<14>+<15>~0~0',0.00,1.00,'1',0,'ÃƒÆ’Ã‚Âª13ÃƒÆ’Ã‚Âª14ÃƒÆ’Ã‚Âª15ÃƒÆ’Ã‚Âª',0,2,0),(20,'DEDUCTIONS','DEDUCTIONS','1','0','','<PF>+<PT>','<17>+<18>~0~0',0.00,1.00,'1',0,'ÃƒÆ’Ã‚Âª17ÃƒÆ’Ã‚Âª18ÃƒÆ’Ã‚Âª',0,2,0),(21,'NETPAY','Net Payable Amount to the Employee','0','0','','<GROSS WAGES>-<DEDUCTIONS>','<19>-<20>~0~0',0.00,1.00,'1',0,'ÃƒÆ’Ã‚Âª19ÃƒÆ’Ã‚Âª20ÃƒÆ’Ã‚Âª',0,2,0),(22,'NAME','Name of the Employee','2','0','Name','','',0.00,1.00,'0',0,'',0,2,0),(23,'DESIGNATION','DESIGNATION','2','0','Designation','','',0.00,1.00,'0',0,'',0,2,0);
/*!40000 ALTER TABLE `prcomponent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prcreate`
--

DROP TABLE IF EXISTS `prcreate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prcreate` (
  `PAYROLLID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PRDATE` datetime DEFAULT NULL,
  `PRNAME` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`PAYROLLID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prcreate`
--

LOCK TABLES `prcreate` WRITE;
/*!40000 ALTER TABLE `prcreate` DISABLE KEYS */;
/*!40000 ALTER TABLE `prcreate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prformulagroup`
--

DROP TABLE IF EXISTS `prformulagroup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prformulagroup` (
  `FORMULAGROUPID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `FORMULA_DESC` varchar(200) DEFAULT NULL,
  `STAFFID_COLLECTION` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`FORMULAGROUPID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prformulagroup`
--

LOCK TABLES `prformulagroup` WRITE;
/*!40000 ALTER TABLE `prformulagroup` DISABLE KEYS */;
/*!40000 ALTER TABLE `prformulagroup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `princome`
--

DROP TABLE IF EXISTS `princome`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `princome` (
  `INCOME_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `INCOME_NAME` varchar(45) NOT NULL DEFAULT '',
  PRIMARY KEY (`INCOME_ID`),
  UNIQUE KEY `Unique_IncomeName` (`INCOME_NAME`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `princome`
--

LOCK TABLES `princome` WRITE;
/*!40000 ALTER TABLE `princome` DISABLE KEYS */;
INSERT INTO `princome` VALUES (1,'Basicpay');
/*!40000 ALTER TABLE `princome` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prloan`
--

DROP TABLE IF EXISTS `prloan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prloan` (
  `LOANID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LOANNAME` varchar(100) DEFAULT NULL,
  `LOANABBRIVIATION` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`LOANID`),
  UNIQUE KEY `UK_LOAN_LOAN_NAME_1` (`LOANNAME`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prloan`
--

LOCK TABLES `prloan` WRITE;
/*!40000 ALTER TABLE `prloan` DISABLE KEYS */;
/*!40000 ALTER TABLE `prloan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prloanget`
--

DROP TABLE IF EXISTS `prloanget`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prloanget` (
  `PRLOANGETID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `STAFFID` int(10) unsigned NOT NULL DEFAULT '0',
  `LOANID` int(10) unsigned NOT NULL DEFAULT '0',
  `AMOUNT` decimal(15,2) DEFAULT NULL,
  `INSTALLMENT` int(10) unsigned DEFAULT NULL,
  `FROMDATE` datetime DEFAULT NULL,
  `TODATE` datetime DEFAULT NULL,
  `INTEREST` decimal(15,2) DEFAULT NULL,
  `INTRESTMODE` int(10) unsigned DEFAULT NULL,
  `INTRESTAMOUNT` decimal(15,2) DEFAULT NULL,
  `CURRENTINSTALLMENT` int(10) unsigned DEFAULT NULL,
  `COMPLETED` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`PRLOANGETID`),
  KEY `LOANID_1` (`LOANID`),
  KEY `FK_prloanget_StaffId` (`STAFFID`),
  CONSTRAINT `LOANID_1` FOREIGN KEY (`LOANID`) REFERENCES `prloan` (`LOANID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_prloanget_LoanId` FOREIGN KEY (`LOANID`) REFERENCES `prloan` (`LOANID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_prloanget_StaffId` FOREIGN KEY (`STAFFID`) REFERENCES `stfpersonal` (`STAFFID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prloanget`
--

LOCK TABLES `prloanget` WRITE;
/*!40000 ALTER TABLE `prloanget` DISABLE KEYS */;
/*!40000 ALTER TABLE `prloanget` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project_branch`
--

DROP TABLE IF EXISTS `project_branch`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `project_branch` (
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BRANCH_ID` int(10) unsigned NOT NULL DEFAULT '0',
  KEY `FK_project_branch_master_project` (`PROJECT_ID`),
  KEY `FK_project_branch_branch_office` (`BRANCH_ID`),
  CONSTRAINT `FK_project_branch_master_project` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`),
  CONSTRAINT `FK_project_branch_branch_office` FOREIGN KEY (`BRANCH_ID`) REFERENCES `branch_office` (`BRANCH_OFFICE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project_branch`
--

LOCK TABLES `project_branch` WRITE;
/*!40000 ALTER TABLE `project_branch` DISABLE KEYS */;
/*!40000 ALTER TABLE `project_branch` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project_costcentre`
--

DROP TABLE IF EXISTS `project_costcentre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `project_costcentre` (
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `COST_CENTRE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `TRANS_MODE` varchar(2) NOT NULL DEFAULT '',
  PRIMARY KEY (`PROJECT_ID`,`COST_CENTRE_ID`),
  KEY `FK_PROJECT_COSTCENTRE_MAPPING` (`PROJECT_ID`),
  KEY `FK_PROJECT_COSTCENTRE_MAPPING_COSTCENTRE` (`COST_CENTRE_ID`) USING BTREE,
  CONSTRAINT `FK_project_costcentre_PROJECT_ID` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`),
  CONSTRAINT `FK_PROJECT_COST_CENTRE_COSTCENTRE_ID` FOREIGN KEY (`COST_CENTRE_ID`) REFERENCES `master_cost_centre` (`COST_CENTRE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project_costcentre`
--

LOCK TABLES `project_costcentre` WRITE;
/*!40000 ALTER TABLE `project_costcentre` DISABLE KEYS */;
/*!40000 ALTER TABLE `project_costcentre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project_donor`
--

DROP TABLE IF EXISTS `project_donor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `project_donor` (
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `DONOR_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`PROJECT_ID`,`DONOR_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project_donor`
--

LOCK TABLES `project_donor` WRITE;
/*!40000 ALTER TABLE `project_donor` DISABLE KEYS */;
/*!40000 ALTER TABLE `project_donor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project_ledger`
--

DROP TABLE IF EXISTS `project_ledger`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `project_ledger` (
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  UNIQUE KEY `UK_PROJ_LEDGER` (`PROJECT_ID`,`LEDGER_ID`),
  KEY `LEDGER_ID` (`LEDGER_ID`),
  CONSTRAINT `FK_project_ledger_LEDGER_ID` FOREIGN KEY (`LEDGER_ID`) REFERENCES `master_ledger` (`LEDGER_ID`),
  CONSTRAINT `FK_project_id_MASTER_PROJECT` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project_ledger`
--

LOCK TABLES `project_ledger` WRITE;
/*!40000 ALTER TABLE `project_ledger` DISABLE KEYS */;
/*!40000 ALTER TABLE `project_ledger` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project_purpose`
--

DROP TABLE IF EXISTS `project_purpose`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `project_purpose` (
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `CONTRIBUTION_ID` int(11) NOT NULL DEFAULT '0',
  `AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `TRANS_MODE` varchar(2) NOT NULL DEFAULT '',
  PRIMARY KEY (`PROJECT_ID`,`CONTRIBUTION_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project_purpose`
--

LOCK TABLES `project_purpose` WRITE;
/*!40000 ALTER TABLE `project_purpose` DISABLE KEYS */;
/*!40000 ALTER TABLE `project_purpose` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project_voucher`
--

DROP TABLE IF EXISTS `project_voucher`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `project_voucher` (
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  KEY `FK_MASTER_PROJECT_ID` (`PROJECT_ID`),
  KEY `FK_MASTER_VOUCHER_ID` (`VOUCHER_ID`),
  CONSTRAINT `FK_project_voucher_PROJECT_ID` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`),
  CONSTRAINT `FK_MASTER_VOUCHER_VOUCHER_ID` FOREIGN KEY (`VOUCHER_ID`) REFERENCES `master_voucher` (`VOUCHER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project_voucher`
--

LOCK TABLES `project_voucher` WRITE;
/*!40000 ALTER TABLE `project_voucher` DISABLE KEYS */;
/*!40000 ALTER TABLE `project_voucher` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prproject_staff`
--

DROP TABLE IF EXISTS `prproject_staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prproject_staff` (
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `STAFFID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`PROJECT_ID`,`STAFFID`),
  KEY `FK_prproject_staff_staffid` (`STAFFID`),
  CONSTRAINT `FK_prproject_staff_projectid` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`),
  CONSTRAINT `FK_prproject_staff_staffid` FOREIGN KEY (`STAFFID`) REFERENCES `stfpersonal` (`STAFFID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prproject_staff`
--

LOCK TABLES `prproject_staff` WRITE;
/*!40000 ALTER TABLE `prproject_staff` DISABLE KEYS */;
/*!40000 ALTER TABLE `prproject_staff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prsalarygroup`
--

DROP TABLE IF EXISTS `prsalarygroup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prsalarygroup` (
  `GROUPID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `GROUPNAME` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`GROUPID`),
  UNIQUE KEY `Uqi_GROUP_NAME` (`GROUPNAME`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prsalarygroup`
--

LOCK TABLES `prsalarygroup` WRITE;
/*!40000 ALTER TABLE `prsalarygroup` DISABLE KEYS */;
/*!40000 ALTER TABLE `prsalarygroup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prstaff`
--

DROP TABLE IF EXISTS `prstaff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prstaff` (
  `PAYROLLID` int(10) unsigned NOT NULL DEFAULT '0',
  `STAFFID` int(10) unsigned DEFAULT NULL,
  `COMPVALUE` varchar(200) DEFAULT NULL,
  `COMPORDER` int(10) unsigned DEFAULT NULL,
  `COMPONENTID` int(10) unsigned DEFAULT NULL,
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TRANSACTIONDATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prstaff`
--

LOCK TABLES `prstaff` WRITE;
/*!40000 ALTER TABLE `prstaff` DISABLE KEYS */;
/*!40000 ALTER TABLE `prstaff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prstaffgroup`
--

DROP TABLE IF EXISTS `prstaffgroup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prstaffgroup` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `GROUPID` int(10) unsigned DEFAULT NULL,
  `STAFFORDER` int(10) unsigned DEFAULT NULL,
  `PAYROLLID` int(10) unsigned DEFAULT NULL,
  `STAFFID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prstaffgroup`
--

LOCK TABLES `prstaffgroup` WRITE;
/*!40000 ALTER TABLE `prstaffgroup` DISABLE KEYS */;
/*!40000 ALTER TABLE `prstaffgroup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prstafftemp`
--

DROP TABLE IF EXISTS `prstafftemp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prstafftemp` (
  `PAYROLLID` int(10) unsigned NOT NULL,
  `STAFFID` int(10) unsigned NOT NULL DEFAULT '0',
  `COMPONENTID` int(10) unsigned NOT NULL DEFAULT '0',
  `COMPVALUE` varchar(100) NOT NULL DEFAULT '',
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prstafftemp`
--

LOCK TABLES `prstafftemp` WRITE;
/*!40000 ALTER TABLE `prstafftemp` DISABLE KEYS */;
/*!40000 ALTER TABLE `prstafftemp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prstatus`
--

DROP TABLE IF EXISTS `prstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prstatus` (
  `PAYROLLID` int(10) unsigned NOT NULL,
  `COMPCREATED` varchar(5) NOT NULL DEFAULT '',
  `PRCREATED` varchar(50) DEFAULT NULL,
  `LOCKEDSTATUS` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`PAYROLLID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prstatus`
--

LOCK TABLES `prstatus` WRITE;
/*!40000 ALTER TABLE `prstatus` DISABLE KEYS */;
/*!40000 ALTER TABLE `prstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prtext`
--

DROP TABLE IF EXISTS `prtext`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prtext` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TNAME` varchar(45) NOT NULL DEFAULT '',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Unique_Tname` (`TNAME`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prtext`
--

LOCK TABLES `prtext` WRITE;
/*!40000 ALTER TABLE `prtext` DISABLE KEYS */;
INSERT INTO `prtext` VALUES (10,'Basic Pay'),(7,'Date of Appointment'),(5,'Date of Birth'),(6,'Date of Join'),(8,'Designation'),(1,'Employee No'),(4,'Gender'),(15,'Increment Date'),(3,'Known As'),(12,'Maximum wages Basic'),(13,'Maximum wages HRA'),(2,'Name'),(14,'PF Number'),(9,'Retirement Date'),(11,'Scale of Pay');
/*!40000 ALTER TABLE `prtext` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stfpersonal`
--

DROP TABLE IF EXISTS `stfpersonal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stfpersonal` (
  `STAFFID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `EMPNO` varchar(50) NOT NULL DEFAULT '',
  `FIRSTNAME` varchar(100) DEFAULT NULL,
  `LASTNAME` varchar(100) DEFAULT NULL,
  `GENDER` varchar(10) DEFAULT NULL,
  `DATEOFBIRTH` datetime DEFAULT NULL,
  `DATEOFJOIN` datetime DEFAULT NULL,
  `CATEGORY` varchar(100) DEFAULT NULL,
  `RETIREMENTDATE` datetime DEFAULT NULL,
  `KNOWNAS` varchar(100) DEFAULT NULL,
  `LEAVINGDATE` datetime DEFAULT NULL,
  `LEAVEREMARKS` varchar(500) DEFAULT NULL,
  `DEGREE` varchar(100) DEFAULT NULL,
  `DESIGNATION` varchar(100) DEFAULT NULL,
  `DEPARTMENT` varchar(100) DEFAULT NULL,
  `DEPTID` int(10) unsigned DEFAULT NULL,
  `PAYINCM1` int(10) unsigned zerofill DEFAULT NULL,
  `PAYINCM2` int(10) unsigned zerofill DEFAULT NULL,
  PRIMARY KEY (`STAFFID`),
  UNIQUE KEY `UNI_EMPNO` (`EMPNO`)
) ENGINE=InnoDB AUTO_INCREMENT=160 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stfpersonal`
--

LOCK TABLES `stfpersonal` WRITE;
/*!40000 ALTER TABLE `stfpersonal` DISABLE KEYS */;
/*!40000 ALTER TABLE `stfpersonal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stfservice`
--

DROP TABLE IF EXISTS `stfservice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stfservice` (
  `SERVICEID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `STAFFID` int(10) unsigned NOT NULL DEFAULT '0',
  `SCALEOFPAY` varchar(100) DEFAULT NULL,
  `REMARKS` varchar(200) DEFAULT NULL,
  `PAY` varchar(50) DEFAULT NULL,
  `DATEOFTERMINATION` datetime DEFAULT NULL,
  `DATEOFAPPOINTMENT` datetime DEFAULT NULL,
  `MAXWAGESBASIC` decimal(15,2) DEFAULT NULL,
  `PFNUMBER` varchar(50) DEFAULT NULL,
  `MAXWAGESHRA` decimal(15,2) DEFAULT NULL,
  PRIMARY KEY (`SERVICEID`),
  KEY `FK_stfservice_1` (`STAFFID`),
  CONSTRAINT `FK_stfservice_1` FOREIGN KEY (`STAFFID`) REFERENCES `stfpersonal` (`STAFFID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stfservice`
--

LOCK TABLES `stfservice` WRITE;
/*!40000 ALTER TABLE `stfservice` DISABLE KEYS */;
/*!40000 ALTER TABLE `stfservice` ENABLE KEYS */;
UNLOCK TABLES;


DROP TABLE IF EXISTS `tds_booking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_booking` (
  `BOOKING_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BOOKING_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `PROJECT_ID` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `EXPENSE_LEDGER_ID` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `PARTY_LEDGER_ID` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `AMOUNT` decimal(15,2) unsigned zerofill NOT NULL DEFAULT '0000000000000.00',
  `VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_DELETED` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '1=ACTIVE,0=INACTIVE',
  `DEDUCTEE_TYPE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`BOOKING_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_booking`
--

LOCK TABLES `tds_booking` WRITE;
/*!40000 ALTER TABLE `tds_booking` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_booking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_booking_detail`
--

DROP TABLE IF EXISTS `tds_booking_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_booking_detail` (
  `BOOKING_DETAIL_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BOOKING_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `NATURE_OF_PAYMENT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `ASSESS_AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00' COMMENT 'EXPENSES AMOUNT',
  `IS_TDS_DEDUCTED` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000' COMMENT '1 - DEDUCTED WHILE BOOKING, 0 - DEDUCTED LATER',
  PRIMARY KEY (`BOOKING_DETAIL_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_booking_detail`
--

LOCK TABLES `tds_booking_detail` WRITE;
/*!40000 ALTER TABLE `tds_booking_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_booking_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_company_deductors`
--

DROP TABLE IF EXISTS `tds_company_deductors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_company_deductors` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TAX_DEDUCTION_ACCOUNT_NO` varchar(50) NOT NULL DEFAULT '',
  `HEAD_OFFICE_TAN_NO` varchar(10) NOT NULL DEFAULT '',
  `PAN_NO` varchar(10) NOT NULL DEFAULT '',
  `TAN_REGISTRATION_NO` varchar(50) NOT NULL DEFAULT '',
  `INCOME_TAX_CIRCLE` varchar(50) NOT NULL DEFAULT '',
  `DEDUCTOR_TYPE` varchar(50) NOT NULL DEFAULT '',
  `RESPONSIBLE_PERSON` varchar(50) NOT NULL DEFAULT '',
  `SON_DAUGHTER_OF` varchar(50) NOT NULL DEFAULT '',
  `DESIGNATION` varchar(100) NOT NULL DEFAULT '',
  `ADDRESS` varchar(100) NOT NULL DEFAULT '',
  `FLAT_NO` varchar(50) NOT NULL DEFAULT '',
  `PREMISES` varchar(50) NOT NULL DEFAULT '',
  `STREET` varchar(50) NOT NULL DEFAULT '',
  `LOCATION` varchar(50) NOT NULL DEFAULT '',
  `DISTRICT` varchar(50) NOT NULL DEFAULT '',
  `STATE` varchar(50) NOT NULL DEFAULT '',
  `PINCODE` varchar(6) NOT NULL DEFAULT '',
  `TELEPHONE_NO` varchar(15) NOT NULL DEFAULT '',
  `EMAIL` varchar(50) NOT NULL DEFAULT '',
  `FULL_NAME` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_company_deductors`
--

LOCK TABLES `tds_company_deductors` WRITE;
/*!40000 ALTER TABLE `tds_company_deductors` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_company_deductors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_credtiors_profile`
--

DROP TABLE IF EXISTS `tds_credtiors_profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_credtiors_profile` (
  `DEDUTEE_TYPE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `NATURE_OF_PAYMENT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `CREDITORS_PROFILE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `NAME` varchar(250) DEFAULT NULL,
  `ADDRESS` varchar(250) DEFAULT NULL,
  `STATE_ID` int(10) unsigned DEFAULT NULL,
  `PIN_CODE` varchar(10) DEFAULT NULL,
  `CONTACT_PERSON` varchar(100) DEFAULT NULL,
  `CONTACT_NUMBER` varchar(15) DEFAULT NULL,
  `EMAIL` varchar(100) DEFAULT NULL,
  `LEDGER_ID` int(10) unsigned DEFAULT NULL,
  `IS_BANK_DETAILS` int(10) unsigned DEFAULT NULL,
  `NICK_NAME` varchar(100) DEFAULT NULL,
  `FAVOURING_NAME` varchar(250) DEFAULT NULL,
  `TRANSACTION_TYPE` int(10) unsigned DEFAULT NULL,
  `BANK_NAME` varchar(100) DEFAULT NULL,
  `ACCOUNT_NUMBER` varchar(20) DEFAULT NULL,
  `IFS_CODE` varchar(10) DEFAULT NULL,
  `PAN_NUMBER` varchar(10) DEFAULT NULL,
  `PAN_IT_HOLDER_NAME` varchar(100) DEFAULT NULL,
  `SALES_TAX_NO` varchar(10) DEFAULT NULL,
  `CST_NUMBER` varchar(10) DEFAULT NULL,
  `COUNTRY_ID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`CREDITORS_PROFILE_ID`),
  KEY `FK_TDS_CREDTIORS_PROFILE` (`DEDUTEE_TYPE_ID`),
  KEY `FK_tds_ledger_id` (`LEDGER_ID`),
  CONSTRAINT `FK_tds_ledger_id` FOREIGN KEY (`LEDGER_ID`) REFERENCES `master_ledger` (`LEDGER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_credtiors_profile`
--

LOCK TABLES `tds_credtiors_profile` WRITE;
/*!40000 ALTER TABLE `tds_credtiors_profile` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_credtiors_profile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_deductee_type`
--

DROP TABLE IF EXISTS `tds_deductee_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_deductee_type` (
  `DEDUCTEE_TYPE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `NAME` varchar(100) NOT NULL DEFAULT '',
  `RESIDENTIAL_STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0 - Resident, 1- Non-Resident',
  `DEDUCTEE_TYPE` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0- Company, 1- Non-Company',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0= INACTIVE ,1 =ACTIVE',
  PRIMARY KEY (`DEDUCTEE_TYPE_ID`),
  UNIQUE KEY `UNQ-NAME` (`NAME`) USING HASH
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_deductee_type`
--

LOCK TABLES `tds_deductee_type` WRITE;
/*!40000 ALTER TABLE `tds_deductee_type` DISABLE KEYS */;
INSERT INTO `tds_deductee_type` VALUES (2,'Artificial Juridical Person',0,1,1),(3,'Association of Persons',0,1,1),(4,'Body of Individuals',0,1,1),(5,'Company Non-Resident',1,0,1),(6,'Company Resident',0,0,1),(7,'Co-Operative Society',0,1,1),(8,'Individual HUF -Non Resident',1,1,1),(9,'Individual HUF -Resident',0,1,1),(10,'Local Authority',0,1,1),(11,'Patnership Firm',0,1,1);
/*!40000 ALTER TABLE `tds_deductee_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_deduction`
--

DROP TABLE IF EXISTS `tds_deduction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_deduction` (
  `DEDUCTION_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `DEDUCTION_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `PROJECT_ID` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `PARTY_LEDGER_ID` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `AMOUNT` decimal(15,2) unsigned zerofill NOT NULL DEFAULT '0000000000000.00',
  `VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_DELETED` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '1=ACTIVE,0=INACTIVE',
  PRIMARY KEY (`DEDUCTION_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_deduction`
--

LOCK TABLES `tds_deduction` WRITE;
/*!40000 ALTER TABLE `tds_deduction` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_deduction` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_deduction_detail`
--

DROP TABLE IF EXISTS `tds_deduction_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_deduction_detail` (
  `DEDUCTION_DETAIL_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `DEDUCTION_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BOOKING_DETAIL_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `TAX_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `TAX_AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`DEDUCTION_DETAIL_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_deduction_detail`
--

LOCK TABLES `tds_deduction_detail` WRITE;
/*!40000 ALTER TABLE `tds_deduction_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_deduction_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_dedutee_type`
--

DROP TABLE IF EXISTS `tds_dedutee_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_dedutee_type` (
  `DEDUTEE_TYPE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `NAME` varchar(100) NOT NULL DEFAULT '',
  `RESIDENTIAL_STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0 - Resident, 1- Non-Resident',
  `DETUTEE_TYPE` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0- Company, 1- Non-Company',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0= INACTIVE ,1 =ACTIVE',
  PRIMARY KEY (`DEDUTEE_TYPE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_dedutee_type`
--

LOCK TABLES `tds_dedutee_type` WRITE;
/*!40000 ALTER TABLE `tds_dedutee_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_dedutee_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_duty_taxtype`
--

DROP TABLE IF EXISTS `tds_duty_taxtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_duty_taxtype` (
  `TDS_DUTY_TAXTYPE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TAX_TYPE_NAME` varchar(50) NOT NULL DEFAULT '0',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '1=ACTIVE,0=INACTIVE',
  PRIMARY KEY (`TDS_DUTY_TAXTYPE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_duty_taxtype`
--

LOCK TABLES `tds_duty_taxtype` WRITE;
/*!40000 ALTER TABLE `tds_duty_taxtype` DISABLE KEYS */;
INSERT INTO `tds_duty_taxtype` VALUES (1,'TDS With PAN',1),(2,'TDS Without PAN',1),(3,'Surcharge',1),(4,'Ed Cess',1),(5,'Sec Ed Cess',1);
/*!40000 ALTER TABLE `tds_duty_taxtype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_nature_payment`
--

DROP TABLE IF EXISTS `tds_nature_payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_nature_payment` (
  `NATURE_PAY_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `NAME` varchar(150) NOT NULL DEFAULT '',
  `SECTION` varchar(7) DEFAULT NULL,
  `SECTION_NAME` varchar(100) DEFAULT NULL,
  `PAYMENT_CODE` varchar(7) NOT NULL DEFAULT '',
  `DESCRIPTION` varchar(400) DEFAULT NULL,
  `TDS_SECTION_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '1=ACTIVE,0=INACTIVE',
  PRIMARY KEY (`NATURE_PAY_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=72 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_nature_payment`
--

LOCK TABLES `tds_nature_payment` WRITE;
/*!40000 ALTER TABLE `tds_nature_payment` DISABLE KEYS */;
INSERT INTO `tds_nature_payment` VALUES (1,'Any other income',NULL,NULL,'195','',15,1),(13,'Income by Way of Long-Term Capital Gains Referred to in Section 115E',NULL,NULL,'195','',15,1),(14,'Income From Foreign Currency Bonds Or Shares of...',NULL,NULL,'196C','',22,1),(15,'Income From Foreign Currency,Bonds or Shares of Indian Company',NULL,NULL,'194LC','',23,1),(16,'Income From Foreign Exchange Assets Payable to an Indian Citizen',NULL,NULL,'195','',15,1),(19,'Insurance Commission',NULL,NULL,'194D',' ',25,1),(20,'Interest on 8% Savings(Taxable) Bonds, 2003',NULL,NULL,'193','',16,1),(21,'Interest on Infrastructure Debt Fund',NULL,NULL,'194LD','',26,1),(22,'Interest on Securites',NULL,NULL,'193','',16,1),(23,'Interest other than Interest on Securites',NULL,NULL,'194A','',27,1),(24,'Interest Payable by Government or Indian concern in Foreign Currency',NULL,NULL,'195','',15,1),(25,'Long-Term Capital Gains[Not Being Covered by Sec 10(33)(36)(38)]',NULL,NULL,'195','',15,1),(27,'Payment of Compensation on Acquisition of Immovable Property',NULL,NULL,'194LA','',28,1),(28,'Payment of Transfer of Certain Immovable property other than Agricultural Land',NULL,NULL,'194IA','',29,1),(29,'Payments in Respect of Deposits Under NSS',NULL,NULL,'194EE','',30,1),(30,'Payments in Respect of Units to an Offshore Fund',NULL,NULL,'196B','',31,1),(31,'Payments on account of repurchase of units of MF or UTI to a resident / non-resident',NULL,NULL,'194F','',32,1),(32,'Payments to Contractors(Other than Advertisements)',NULL,NULL,'194C','',33,1),(38,'Payment \\Royalty on Acquisition of Software U/s 194J',NULL,NULL,'194J',' ',26,1),(39,'Rent of Land, Building Or Furniture',NULL,NULL,'194I','',35,1),(40,'Rent of Plant, Machinery Or Equipment',NULL,NULL,'194I','',35,1),(41,'Royalty(F) Agreement is Made After May 31, 1997 Before June 1, 2005',NULL,NULL,'195','',15,1),(42,'Royalty(F) Agreement is Made Before June 1, 1997',NULL,NULL,'195','',15,1),(43,'Royalty(F) Agreement is Made on Or After June 1, 2005',NULL,NULL,'195','',15,1),(44,'Royalty(G) Agreement is Made After March 31, 1961 Before April 1, 1976',NULL,NULL,'195','',15,1),(45,'Royalty(G) Agreement is Made After March 31, 1976 Before June 1,1997',NULL,NULL,'195','',15,1),(46,'Royalty(G) Agreement is Made After March 31, 1997 Before April 1, 2005',NULL,NULL,'195','',15,1),(47,'Royalty(G) Agreement is Made on Or After June 1,2005',NULL,NULL,'195','',15,1),(48,'Short-Term Capital Gains U/s 111A',NULL,NULL,'195','',15,1),(49,'Winnings From Horse Race',NULL,NULL,'194BB','',36,1),(50,'Winnings From Lotteries and CrossWord Puzzles',NULL,NULL,'194B','',37,1),(51,'horse race in Bankgalore',NULL,NULL,'0908','',26,0),(52,'Horse Race in Bangalore',NULL,NULL,'456','',25,0),(53,'Any Other Interest on Securities As Per Sec.193',NULL,NULL,'193','',16,1),(54,'Any Remuneration Or Commission Paid to Director of the Company',NULL,NULL,'194J','',17,1),(55,'Commission on Sale of Lottery Tickets',NULL,NULL,'194G','',18,1),(56,'Commission or Brokerage',NULL,NULL,'194H','',19,1),(57,'Deemed Dividend  U/s  2(22)(E)',NULL,NULL,'194','',20,1),(58,'Fees for Prfessional Or Technical Services',NULL,NULL,'194J',' ',17,1),(59,'Fees for Tech. Services Agreement Is Made After Feb 29, 1964 Before April 1, 1976',NULL,NULL,'195','',15,1),(60,'Fees for Tech. Services Agreement Is Made After Mar 31, 1976 Before Jun 1, 1997',NULL,NULL,'195','',15,1),(61,'Fees for Tech Services Agreement Is Made After May 31, 1997 Before June 1, 2005',NULL,NULL,'195','',15,1),(62,'Fees for Tech Services Agreement Is Made on Or After June 1, 2005',NULL,NULL,'195','',15,1),(63,'Income by Way of Interest on Certain Bonds and Government Securities',NULL,NULL,'194LD','',21,1),(64,'Income of Foreign Institutional Investors From....',NULL,NULL,'196D','',24,1),(65,'Payments to Non-Resident Sportsmen/Sports Assoc...',NULL,NULL,'194E','',34,1),(66,'Payments to Contractors(Advertisement Contractors)',NULL,NULL,'194C','',33,1),(67,'Payments to Sub-Contractors',NULL,NULL,'194C','',33,1),(68,'Payments to Transporters',NULL,NULL,'194C','',33,1),(69,'Payments to Transporters (Sub-Contractors)',NULL,NULL,'194C','',33,1),(70,'Other Sums Payable to A Non-Resident',NULL,NULL,'195','',15,1),(71,'Income in Respect of Units of Non-Residents',NULL,NULL,'196','',38,1);
/*!40000 ALTER TABLE `tds_nature_payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_party_payment`
--

DROP TABLE IF EXISTS `tds_party_payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_party_payment` (
  `PARTY_PAYMENT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PAYMENT_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PARTY_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PAYMENT_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_DELETED` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '0-Deleted,1-Active',
  PRIMARY KEY (`PARTY_PAYMENT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_party_payment`
--

LOCK TABLES `tds_party_payment` WRITE;
/*!40000 ALTER TABLE `tds_party_payment` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_party_payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_party_payment_detail`
--

DROP TABLE IF EXISTS `tds_party_payment_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_party_payment_detail` (
  `PARTY_PAYMENT_DETAIL_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PARTY_PAYMENT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `BOOKING_DETAIL_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `DEDUCTION_DETAIL_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PAID_AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `IS_ADVANCE_PAID` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `IS_ADVANCE_ADJUSTED` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  PRIMARY KEY (`PARTY_PAYMENT_DETAIL_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_party_payment_detail`
--

LOCK TABLES `tds_party_payment_detail` WRITE;
/*!40000 ALTER TABLE `tds_party_payment_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_party_payment_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_payment`
--

DROP TABLE IF EXISTS `tds_payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_payment` (
  `TDS_PAYMENT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PAYMENT_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PAYMENT_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `IS_DELETED` int(10) unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`TDS_PAYMENT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_payment`
--

LOCK TABLES `tds_payment` WRITE;
/*!40000 ALTER TABLE `tds_payment` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_payment_detail`
--

DROP TABLE IF EXISTS `tds_payment_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_payment_detail` (
  `TDS_PAYMENT_DETAIL_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TDS_PAYMENT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `DEDUCTION_DETAIL_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PAID_AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `IS_ADVANCE_PAID` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `IS_ADVANCE_ADJUSTED` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  `LEDGER_ID` int(11) DEFAULT '0',
  `FLAG` int(11) DEFAULT '0',
  PRIMARY KEY (`TDS_PAYMENT_DETAIL_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_payment_detail`
--

LOCK TABLES `tds_payment_detail` WRITE;
/*!40000 ALTER TABLE `tds_payment_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `tds_payment_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_policy`
--

DROP TABLE IF EXISTS `tds_policy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_policy` (
  `TDS_POLICY_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TDS_DEDUCTEE_TYPE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `TDS_NATURE_PAYMENT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `APPLICABLE_FROM` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`TDS_POLICY_ID`),
  KEY `FK_TDS_DEDUTEE_DETAILS_PAYMENT_ID` (`TDS_NATURE_PAYMENT_ID`),
  KEY `FK_TDS_DEDUTEE_DETAILS_DEDUTEE_TYPE_ID` (`TDS_DEDUCTEE_TYPE_ID`) USING BTREE,
  CONSTRAINT `FK_tds_policy_DEDUCTEE_TYPE_ID` FOREIGN KEY (`TDS_DEDUCTEE_TYPE_ID`) REFERENCES `tds_deductee_type` (`DEDUCTEE_TYPE_ID`),
  CONSTRAINT `FK_tds_policy_NATURE_PAYMENT_ID` FOREIGN KEY (`TDS_NATURE_PAYMENT_ID`) REFERENCES `tds_nature_payment` (`NATURE_PAY_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=41170 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_policy`
--

LOCK TABLES `tds_policy` WRITE;
/*!40000 ALTER TABLE `tds_policy` DISABLE KEYS */;
INSERT INTO `tds_policy` VALUES (4340,3,19,'2004-04-01 00:00:00'),(4341,3,19,'2007-04-01 00:00:00'),(4342,3,19,'2009-04-01 00:00:00'),(4343,3,19,'2010-07-01 00:00:00'),(4344,3,20,'2008-04-01 00:00:00'),(4345,3,20,'2009-04-01 00:00:00'),(4346,3,22,'2004-04-01 00:00:00'),(4347,3,22,'2007-04-01 00:00:00'),(4348,3,22,'2009-04-01 00:00:00'),(4349,3,22,'2012-07-01 00:00:00'),(4350,3,23,'2004-04-01 00:00:00'),(4351,3,23,'2007-04-01 00:00:00'),(4352,3,23,'2009-04-01 00:00:00'),(4353,3,27,'2004-10-01 00:00:00'),(4354,3,27,'2007-04-01 00:00:00'),(4355,3,27,'2009-04-01 00:00:00'),(4356,3,27,'2012-07-01 00:00:00'),(4357,3,28,'2013-06-01 00:00:00'),(4358,3,29,'2004-04-01 00:00:00'),(4359,3,29,'2007-04-01 00:00:00'),(4360,3,29,'2009-04-01 00:00:00'),(4361,3,31,'2004-04-01 00:00:00'),(4362,3,31,'2007-04-01 00:00:00'),(4363,3,31,'2009-04-01 00:00:00'),(4364,3,32,'2004-04-01 00:00:00'),(4365,3,32,'2007-04-01 00:00:00'),(4366,3,32,'2009-04-01 00:00:00'),(4367,3,32,'2009-10-01 00:00:00'),(4368,3,32,'2010-07-01 00:00:00'),(4369,3,38,'2012-07-01 00:00:00'),(4370,3,39,'2004-04-01 00:00:00'),(4371,3,39,'2007-04-01 00:00:00'),(4372,3,39,'2007-06-01 00:00:00'),(4373,3,39,'2009-04-01 00:00:00'),(4374,3,39,'2009-10-01 00:00:00'),(4375,3,39,'2010-07-01 00:00:00'),(4376,3,40,'2007-06-01 00:00:00'),(4377,3,40,'2009-04-01 00:00:00'),(4378,3,40,'2009-10-01 00:00:00'),(4379,3,40,'2010-07-01 00:00:00'),(4380,3,49,'2004-04-01 00:00:00'),(4381,3,49,'2007-04-01 00:00:00'),(4382,3,49,'2009-04-01 00:00:00'),(4383,3,49,'2010-07-01 00:00:00'),(4384,3,50,'2004-04-01 00:00:00'),(4385,3,50,'2007-04-01 00:00:00'),(4386,3,50,'2009-04-01 00:00:00'),(4387,3,50,'2010-07-01 00:00:00'),(4388,3,53,'2004-04-01 00:00:00'),(4389,3,53,'2007-04-01 00:00:00'),(4390,3,53,'2008-04-01 00:00:00'),(4391,3,53,'2009-04-01 00:00:00'),(4392,3,53,'2012-04-01 00:00:00'),(4393,3,54,'2012-07-01 00:00:00'),(4394,3,55,'2007-04-01 00:00:00'),(4395,3,55,'2009-04-01 00:00:00'),(4396,3,56,'2004-04-01 00:00:00'),(4397,3,56,'2007-04-01 00:00:00'),(4398,3,56,'2007-06-01 00:00:00'),(4399,3,56,'2009-04-01 00:00:00'),(4400,3,56,'2010-07-01 00:00:00'),(4401,3,57,'2007-04-01 00:00:00'),(4402,3,57,'2009-04-01 00:00:00'),(4403,3,58,'2004-04-01 00:00:00'),(4404,3,58,'2007-04-01 00:00:00'),(4405,3,58,'2007-06-01 00:00:00'),(4406,3,58,'2009-04-01 00:00:00'),(4407,3,58,'2010-07-01 00:00:00'),(4408,3,66,'2004-04-01 00:00:00'),(4409,3,66,'2007-04-01 00:00:00'),(4410,3,66,'2009-04-01 00:00:00'),(4411,3,66,'2009-10-01 00:00:00'),(4412,3,66,'2010-07-01 00:00:00'),(4413,3,67,'2004-04-01 00:00:00'),(4414,3,67,'2007-04-01 00:00:00'),(4415,3,67,'2009-04-01 00:00:00'),(4416,3,67,'2009-10-01 00:00:00'),(4417,3,67,'2010-07-01 00:00:00'),(4418,3,68,'2009-10-01 00:00:00'),(4419,3,68,'2010-04-01 00:00:00'),(4420,3,68,'2010-07-01 00:00:00'),(4421,3,69,'2009-10-01 00:00:00'),(4422,3,69,'2010-04-01 00:00:00'),(4423,3,69,'2010-07-01 00:00:00'),(8120,4,13,'2009-04-01 00:00:00'),(8121,4,19,'2004-04-01 00:00:00'),(8122,4,19,'2007-04-01 00:00:00'),(8123,4,19,'2010-07-01 00:00:00'),(8124,4,20,'2008-04-01 00:00:00'),(8125,4,20,'2010-07-01 00:00:00'),(8126,4,22,'2004-04-01 00:00:00'),(8127,4,22,'2007-04-01 00:00:00'),(8128,4,22,'2009-04-01 00:00:00'),(8129,4,22,'2012-07-01 00:00:00'),(8130,4,23,'2004-04-01 00:00:00'),(8131,4,23,'2007-04-01 00:00:00'),(8132,4,23,'2009-04-01 00:00:00'),(8133,4,27,'2004-10-01 00:00:00'),(8134,4,27,'2007-04-01 00:00:00'),(8135,4,27,'2009-04-01 00:00:00'),(8136,4,27,'2012-07-01 00:00:00'),(8137,4,28,'2013-06-01 00:00:00'),(8138,4,29,'2004-04-01 00:00:00'),(8139,4,29,'2007-04-01 00:00:00'),(8140,4,29,'2009-04-01 00:00:00'),(8141,4,31,'2004-04-01 00:00:00'),(8142,4,31,'2007-04-01 00:00:00'),(8143,4,31,'2009-04-01 00:00:00'),(8144,4,32,'2004-04-01 00:00:00'),(8145,4,32,'2007-04-01 00:00:00'),(8146,4,32,'2009-04-01 00:00:00'),(8147,4,32,'2009-10-01 00:00:00'),(8148,4,32,'2010-07-01 00:00:00'),(8149,4,38,'2012-07-01 00:00:00'),(8150,4,39,'2004-04-01 00:00:00'),(8151,4,39,'2007-04-01 00:00:00'),(8152,4,39,'2007-06-01 00:00:00'),(8153,4,39,'2009-04-01 00:00:00'),(8154,4,39,'2009-10-01 00:00:00'),(8155,4,39,'2010-07-01 00:00:00'),(8156,4,40,'2007-06-01 00:00:00'),(8157,4,40,'2009-04-01 00:00:00'),(8158,4,40,'2009-10-01 00:00:00'),(8159,4,40,'2010-07-01 00:00:00'),(8160,4,49,'2004-04-01 00:00:00'),(8161,4,49,'2004-07-01 00:00:00'),(8162,4,49,'2007-04-01 00:00:00'),(8163,4,49,'2009-04-01 00:00:00'),(8164,4,50,'2004-04-01 00:00:00'),(8165,4,50,'2007-04-01 00:00:00'),(8166,4,50,'2010-07-01 00:00:00'),(8167,4,53,'2004-04-01 00:00:00'),(8168,4,53,'2007-04-01 00:00:00'),(8169,4,53,'2008-04-01 00:00:00'),(8170,4,53,'2009-04-01 00:00:00'),(8171,4,54,'2012-07-01 00:00:00'),(8172,4,55,'2004-04-01 00:00:00'),(8173,4,55,'2007-04-01 00:00:00'),(8174,4,55,'2009-04-01 00:00:00'),(8175,4,56,'2004-04-01 00:00:00'),(8176,4,56,'2007-04-01 00:00:00'),(8177,4,56,'2007-06-01 00:00:00'),(8178,4,56,'2009-04-01 00:00:00'),(8179,4,56,'2010-07-01 00:00:00'),(8180,4,57,'2007-04-01 00:00:00'),(8181,4,57,'2009-04-01 00:00:00'),(8182,4,58,'2004-04-01 00:00:00'),(8183,4,58,'2007-04-01 00:00:00'),(8184,4,58,'2007-06-01 00:00:00'),(8185,4,58,'2009-04-01 00:00:00'),(8186,4,58,'2010-07-01 00:00:00'),(8187,4,66,'2004-04-01 00:00:00'),(8188,4,66,'2007-04-01 00:00:00'),(8189,4,66,'2009-04-01 00:00:00'),(8190,4,66,'2009-10-01 00:00:00'),(8191,4,66,'2010-07-01 00:00:00'),(8192,4,67,'2004-04-01 00:00:00'),(8193,4,67,'2007-04-01 00:00:00'),(8194,4,67,'2009-04-01 00:00:00'),(8195,4,67,'2009-10-01 00:00:00'),(8196,4,68,'2009-10-01 00:00:00'),(8197,4,68,'2010-04-01 00:00:00'),(8198,4,68,'2010-07-01 00:00:00'),(8199,4,69,'2009-10-01 00:00:00'),(8200,4,69,'2010-07-01 00:00:00'),(13627,5,1,'2008-04-01 00:00:00'),(13628,5,1,'2011-04-01 00:00:00'),(13629,5,1,'2012-04-01 00:00:00'),(13630,5,1,'2013-04-01 00:00:00'),(13631,5,13,'2008-04-01 00:00:00'),(13632,5,13,'2011-04-01 00:00:00'),(13633,5,13,'2012-04-01 00:00:00'),(13634,5,13,'2013-04-01 00:00:00'),(13635,5,14,'2004-04-01 00:00:00'),(13636,5,14,'2007-04-01 00:00:00'),(13637,5,15,'2012-07-01 00:00:00'),(13638,5,16,'2008-04-01 00:00:00'),(13639,5,16,'2011-04-01 00:00:00'),(13640,5,16,'2012-04-01 00:00:00'),(13641,5,16,'2013-04-01 00:00:00'),(13642,5,21,'2011-06-01 00:00:00'),(13643,5,24,'2008-04-01 00:00:00'),(13644,5,24,'2011-04-01 00:00:00'),(13645,5,24,'2012-04-01 00:00:00'),(13646,5,24,'2013-04-01 00:00:00'),(13647,5,25,'2008-04-01 00:00:00'),(13648,5,25,'2011-04-01 00:00:00'),(13649,5,25,'2012-04-01 00:00:00'),(13650,5,25,'2013-04-01 00:00:00'),(13651,5,29,'2011-04-01 00:00:00'),(13652,5,29,'2013-04-01 00:00:00'),(13653,5,30,'2004-04-01 00:00:00'),(13654,5,30,'2007-04-01 00:00:00'),(13655,5,30,'2012-04-01 00:00:00'),(13656,5,41,'2008-04-01 00:00:00'),(13657,5,41,'2011-04-01 00:00:00'),(13658,5,41,'2012-04-01 00:00:00'),(13659,5,42,'2008-04-01 00:00:00'),(13660,5,42,'2011-04-01 00:00:00'),(13661,5,42,'2012-04-01 00:00:00'),(13662,5,43,'0511-04-01 00:00:00'),(13663,5,43,'2008-04-01 00:00:00'),(13664,5,43,'2011-04-01 00:00:00'),(13665,5,43,'2012-04-01 00:00:00'),(13666,5,44,'2008-04-01 00:00:00'),(13667,5,44,'2011-04-01 00:00:00'),(13668,5,44,'2012-04-01 00:00:00'),(13669,5,44,'2013-04-01 00:00:00'),(13670,5,45,'2008-04-01 00:00:00'),(13671,5,45,'2011-04-01 00:00:00'),(13672,5,45,'2012-04-01 00:00:00'),(13673,5,45,'2013-04-01 00:00:00'),(13674,5,46,'2008-04-01 00:00:00'),(13675,5,46,'2011-04-01 00:00:00'),(13676,5,46,'2012-04-01 00:00:00'),(13677,5,47,'2008-04-01 00:00:00'),(13678,5,47,'2012-04-01 00:00:00'),(13679,5,48,'2008-04-01 00:00:00'),(13680,5,48,'2011-04-01 00:00:00'),(13681,5,48,'2012-04-01 00:00:00'),(13682,5,48,'2013-04-01 00:00:00'),(13683,5,49,'2007-04-01 00:00:00'),(13684,5,49,'2011-04-01 00:00:00'),(13685,5,49,'2012-04-01 00:00:00'),(13686,5,49,'2013-04-01 00:00:00'),(13687,5,50,'2007-04-01 00:00:00'),(13688,5,50,'2011-04-01 00:00:00'),(13689,5,50,'2012-04-01 00:00:00'),(13690,5,50,'2013-04-01 00:00:00'),(13691,5,55,'2007-04-01 00:00:00'),(13692,5,55,'2011-04-01 00:00:00'),(13693,5,55,'2012-04-01 00:00:00'),(13694,5,55,'2013-04-01 00:00:00'),(13695,5,59,'2008-04-01 00:00:00'),(13696,5,59,'2011-04-01 00:00:00'),(13697,5,59,'2012-04-01 00:00:00'),(13698,5,59,'2013-04-01 00:00:00'),(13699,5,60,'2008-04-01 00:00:00'),(13700,5,60,'2011-04-01 00:00:00'),(13701,5,60,'2012-04-01 00:00:00'),(13702,5,60,'2013-04-01 00:00:00'),(13703,5,61,'2008-04-01 00:00:00'),(13704,5,61,'2011-04-01 00:00:00'),(13705,5,61,'2012-04-01 00:00:00'),(13706,5,62,'2008-04-01 00:00:00'),(13707,5,62,'2011-04-01 00:00:00'),(13708,5,62,'2012-04-01 00:00:00'),(13709,5,63,'2013-06-01 00:00:00'),(13710,5,65,'2004-04-01 00:00:00'),(13711,5,65,'2007-04-01 00:00:00'),(13712,5,65,'2011-04-01 00:00:00'),(13713,5,65,'2012-04-01 00:00:00'),(13714,5,65,'2012-07-01 00:00:00'),(13715,5,65,'2013-04-01 00:00:00'),(13716,5,70,'2004-04-01 00:00:00'),(13717,5,70,'2007-04-01 00:00:00'),(13718,5,70,'2011-04-01 00:00:00'),(13719,5,70,'2012-04-01 00:00:00'),(13720,5,71,'2004-04-01 00:00:00'),(13721,5,71,'2007-04-01 00:00:00'),(13722,5,71,'2011-04-01 00:00:00'),(13723,5,71,'2012-04-01 00:00:00'),(17135,6,19,'2004-04-01 00:00:00'),(17136,6,19,'2007-04-01 00:00:00'),(17137,6,19,'2009-04-01 00:00:00'),(17138,6,19,'2010-07-01 00:00:00'),(17139,6,20,'2008-04-01 00:00:00'),(17140,6,20,'2009-04-01 00:00:00'),(17141,6,22,'2004-04-01 00:00:00'),(17142,6,22,'2007-04-01 00:00:00'),(17143,6,22,'2009-04-01 00:00:00'),(17144,6,22,'2012-07-01 00:00:00'),(17145,6,23,'2004-04-01 00:00:00'),(17146,6,23,'2007-04-01 00:00:00'),(17147,6,23,'2009-04-01 00:00:00'),(17148,6,27,'2004-10-01 00:00:00'),(17149,6,27,'2007-04-01 00:00:00'),(17150,6,27,'2009-04-01 00:00:00'),(17151,6,27,'2012-07-01 00:00:00'),(17152,6,28,'2013-06-01 00:00:00'),(17153,6,29,'2007-04-01 00:00:00'),(17154,6,29,'2009-04-01 00:00:00'),(17155,6,31,'2004-04-01 00:00:00'),(17156,6,31,'2007-04-01 00:00:00'),(17157,6,31,'2009-04-01 00:00:00'),(17158,6,32,'2004-04-01 00:00:00'),(17159,6,32,'2007-04-01 00:00:00'),(17160,6,32,'2009-04-01 00:00:00'),(17161,6,32,'2009-10-01 00:00:00'),(17162,6,32,'2010-07-01 00:00:00'),(17163,6,38,'2012-07-01 00:00:00'),(17164,6,39,'2004-04-01 00:00:00'),(17165,6,39,'2007-04-01 00:00:00'),(17166,6,39,'2009-04-01 00:00:00'),(17167,6,39,'2010-07-01 00:00:00'),(17168,6,39,'2012-10-01 00:00:00'),(17169,6,40,'2007-06-01 00:00:00'),(17170,6,40,'2009-04-01 00:00:00'),(17171,6,40,'2009-10-01 00:00:00'),(17172,6,40,'2010-07-01 00:00:00'),(17173,6,49,'2004-04-01 00:00:00'),(17174,6,49,'2007-04-01 00:00:00'),(17175,6,49,'2009-04-01 00:00:00'),(17176,6,49,'2010-07-01 00:00:00'),(17177,6,50,'2004-04-01 00:00:00'),(17178,6,50,'2007-04-01 00:00:00'),(17179,6,50,'2009-04-01 00:00:00'),(17180,6,50,'2010-07-01 00:00:00'),(17181,6,53,'2004-04-01 00:00:00'),(17182,6,53,'2007-04-01 00:00:00'),(17183,6,53,'2008-04-01 00:00:00'),(17184,6,53,'2009-04-01 00:00:00'),(17185,6,54,'2012-07-01 00:00:00'),(17186,6,55,'2004-04-01 00:00:00'),(17187,6,55,'2007-04-01 00:00:00'),(17188,6,55,'2009-04-01 00:00:00'),(17189,6,56,'2004-04-01 00:00:00'),(17190,6,56,'2007-04-01 00:00:00'),(17191,6,56,'2007-06-01 00:00:00'),(17192,6,56,'2009-04-01 00:00:00'),(17193,6,56,'2010-07-01 00:00:00'),(17194,6,57,'2007-04-01 00:00:00'),(17195,6,57,'2009-04-01 00:00:00'),(17196,6,58,'2004-04-01 00:00:00'),(17197,6,58,'2007-04-01 00:00:00'),(17198,6,58,'2007-06-01 00:00:00'),(17199,6,58,'2009-04-01 00:00:00'),(17200,6,58,'2010-07-01 00:00:00'),(17201,6,66,'2004-04-01 00:00:00'),(17202,6,66,'2007-04-01 00:00:00'),(17203,6,66,'2009-04-01 00:00:00'),(17204,6,66,'2009-10-01 00:00:00'),(17205,6,66,'2010-07-01 00:00:00'),(17206,6,67,'2004-04-01 00:00:00'),(17207,6,67,'2007-04-01 00:00:00'),(17208,6,67,'2009-04-01 00:00:00'),(17209,6,67,'2009-10-01 00:00:00'),(17210,6,67,'2010-07-01 00:00:00'),(17211,6,68,'2009-10-01 00:00:00'),(17212,6,68,'2010-04-01 00:00:00'),(17213,6,68,'2010-07-01 00:00:00'),(17214,6,69,'2009-10-01 00:00:00'),(17215,6,69,'2010-04-01 00:00:00'),(17216,6,69,'2010-07-01 00:00:00'),(22167,7,19,'2004-04-01 00:00:00'),(22168,7,19,'2005-04-01 00:00:00'),(22169,7,19,'2007-04-01 00:00:00'),(22170,7,19,'2009-04-01 00:00:00'),(22171,7,19,'2010-07-01 00:00:00'),(22172,7,20,'2008-04-01 00:00:00'),(22173,7,20,'2009-04-01 00:00:00'),(22174,7,22,'2004-04-01 00:00:00'),(22175,7,22,'2005-04-01 00:00:00'),(22176,7,22,'2007-04-01 00:00:00'),(22177,7,22,'2009-04-01 00:00:00'),(22178,7,22,'2012-07-01 00:00:00'),(22179,7,23,'2004-04-01 00:00:00'),(22180,7,23,'2005-04-01 00:00:00'),(22181,7,23,'2007-04-01 00:00:00'),(22182,7,23,'2009-04-01 00:00:00'),(22183,7,27,'2004-10-01 00:00:00'),(22184,7,27,'2005-04-01 00:00:00'),(22185,7,27,'2007-04-01 00:00:00'),(22186,7,27,'2009-04-01 00:00:00'),(22187,7,27,'2012-07-01 00:00:00'),(22188,7,28,'2013-06-01 00:00:00'),(22189,7,29,'2004-04-01 00:00:00'),(22190,7,29,'2005-04-01 00:00:00'),(22191,7,29,'2007-04-01 00:00:00'),(22192,7,29,'2009-04-01 00:00:00'),(22193,7,31,'2004-04-01 00:00:00'),(22194,7,31,'2005-04-01 00:00:00'),(22195,7,31,'2007-04-01 00:00:00'),(22196,7,31,'2009-04-01 00:00:00'),(22197,7,32,'2004-04-01 00:00:00'),(22198,7,32,'2005-04-01 00:00:00'),(22199,7,32,'2007-04-01 00:00:00'),(22200,7,32,'2009-04-01 00:00:00'),(22201,7,32,'2009-10-01 00:00:00'),(22202,7,32,'2010-07-01 00:00:00'),(22203,7,38,'2012-07-01 00:00:00'),(22204,7,39,'2004-04-01 00:00:00'),(22205,7,39,'2005-04-01 00:00:00'),(22206,7,39,'2007-04-01 00:00:00'),(22207,7,39,'2009-04-01 00:00:00'),(22208,7,39,'2009-10-01 00:00:00'),(22209,7,39,'2010-07-01 00:00:00'),(22210,7,40,'2007-06-01 00:00:00'),(22211,7,40,'2009-04-01 00:00:00'),(22212,7,40,'2009-10-01 00:00:00'),(22213,7,40,'2010-07-01 00:00:00'),(22214,7,49,'2004-04-01 00:00:00'),(22215,7,49,'2005-04-01 00:00:00'),(22216,7,49,'2007-04-01 00:00:00'),(22217,7,49,'2009-04-01 00:00:00'),(22218,7,49,'2010-07-01 00:00:00'),(22219,7,50,'2004-04-01 00:00:00'),(22220,7,50,'2005-04-01 00:00:00'),(22221,7,50,'2007-04-01 00:00:00'),(22222,7,50,'2009-04-01 00:00:00'),(22223,7,50,'2010-07-01 00:00:00'),(22224,7,53,'2004-04-01 00:00:00'),(22225,7,53,'2005-04-01 00:00:00'),(22226,7,53,'2007-04-01 00:00:00'),(22227,7,53,'2008-04-01 00:00:00'),(22228,7,53,'2009-04-01 00:00:00'),(22229,7,54,'2012-07-01 00:00:00'),(22230,7,55,'2004-04-01 00:00:00'),(22231,7,55,'2005-04-01 00:00:00'),(22232,7,55,'2007-04-01 00:00:00'),(22233,7,55,'2009-04-01 00:00:00'),(22234,7,56,'2004-04-01 00:00:00'),(22235,7,56,'2005-04-01 00:00:00'),(22236,7,56,'2007-04-01 00:00:00'),(22237,7,56,'2007-06-01 00:00:00'),(22238,7,56,'2009-04-01 00:00:00'),(22239,7,56,'2010-07-01 00:00:00'),(22240,7,57,'2007-04-01 00:00:00'),(22241,7,57,'2009-04-01 00:00:00'),(22242,7,58,'2004-04-01 00:00:00'),(22243,7,58,'2005-04-01 00:00:00'),(22244,7,58,'2007-04-01 00:00:00'),(22245,7,58,'2007-06-01 00:00:00'),(22246,7,58,'2009-04-01 00:00:00'),(22247,7,58,'2010-07-01 00:00:00'),(22248,7,66,'2004-04-01 00:00:00'),(22249,7,66,'2005-04-01 00:00:00'),(22250,7,66,'2007-04-01 00:00:00'),(22251,7,66,'2009-04-01 00:00:00'),(22252,7,66,'2009-10-01 00:00:00'),(22253,7,66,'2010-07-01 00:00:00'),(22254,7,67,'2004-04-01 00:00:00'),(22255,7,67,'2005-04-01 00:00:00'),(22256,7,67,'2007-04-01 00:00:00'),(22257,7,67,'2009-04-01 00:00:00'),(22258,7,67,'2009-10-01 00:00:00'),(22259,7,67,'2010-07-01 00:00:00'),(22260,7,68,'2009-10-01 00:00:00'),(22261,7,68,'2010-04-01 00:00:00'),(22262,7,68,'2010-07-01 00:00:00'),(22263,7,69,'2009-10-01 00:00:00'),(22264,7,69,'2010-04-01 00:00:00'),(22265,7,69,'2010-07-01 00:00:00'),(25961,8,1,'2008-04-01 00:00:00'),(25962,8,1,'2012-04-01 00:00:00'),(25963,8,1,'2013-04-01 00:00:00'),(25964,8,13,'0013-04-01 00:00:00'),(25965,8,13,'2008-04-01 00:00:00'),(25966,8,13,'2012-04-01 00:00:00'),(25967,8,14,'2008-04-01 00:00:00'),(25968,8,14,'2012-04-01 00:00:00'),(25969,8,15,'2012-07-01 00:00:00'),(25970,8,15,'2013-04-01 00:00:00'),(25971,8,16,'2008-04-01 00:00:00'),(25972,8,16,'2012-04-01 00:00:00'),(25973,8,16,'2013-04-01 00:00:00'),(25974,8,21,'2011-06-01 00:00:00'),(25975,8,21,'2013-04-01 00:00:00'),(25976,8,24,'2008-04-01 00:00:00'),(25977,8,24,'2012-04-01 00:00:00'),(25978,8,24,'2013-04-01 00:00:00'),(25979,8,25,'2008-04-01 00:00:00'),(25980,8,25,'2012-04-01 00:00:00'),(25981,8,25,'2013-04-01 00:00:00'),(25982,8,29,'2007-04-01 00:00:00'),(25983,8,29,'2012-04-01 00:00:00'),(25984,8,30,'2004-04-01 00:00:00'),(25985,8,30,'2007-04-01 00:00:00'),(25986,8,30,'2012-04-01 00:00:00'),(25987,8,30,'2013-04-01 00:00:00'),(25988,8,31,'2007-04-01 00:00:00'),(25989,8,31,'2012-04-01 00:00:00'),(25990,8,31,'2013-04-01 00:00:00'),(25991,8,41,'2008-04-01 00:00:00'),(25992,8,41,'2012-04-01 00:00:00'),(25993,8,42,'2008-04-01 00:00:00'),(25994,8,42,'2012-04-01 00:00:00'),(25995,8,43,'2008-04-01 00:00:00'),(25996,8,43,'2012-04-01 00:00:00'),(25997,8,44,'2008-04-01 00:00:00'),(25998,8,44,'2012-04-01 00:00:00'),(25999,8,44,'2013-04-01 00:00:00'),(26000,8,45,'2008-04-01 00:00:00'),(26001,8,45,'2012-04-01 00:00:00'),(26002,8,45,'2013-04-01 00:00:00'),(26003,8,46,'2008-04-01 00:00:00'),(26004,8,46,'2012-04-01 00:00:00'),(26005,8,47,'2008-04-01 00:00:00'),(26006,8,47,'2012-04-01 00:00:00'),(26007,8,48,'2008-04-01 00:00:00'),(26008,8,48,'2012-04-01 00:00:00'),(26009,8,48,'2013-04-01 00:00:00'),(26010,8,49,'2007-04-01 00:00:00'),(26011,8,49,'2012-04-01 00:00:00'),(26012,8,49,'2013-04-01 00:00:00'),(26013,8,50,'2007-04-01 00:00:00'),(26014,8,50,'2012-04-01 00:00:00'),(26015,8,50,'2013-04-01 00:00:00'),(26016,8,55,'2007-04-01 00:00:00'),(26017,8,55,'2012-04-01 00:00:00'),(26018,8,55,'2013-04-01 00:00:00'),(26019,8,59,'2008-04-01 00:00:00'),(26020,8,59,'2012-04-01 00:00:00'),(26021,8,59,'2013-04-01 00:00:00'),(26022,8,60,'2008-04-01 00:00:00'),(26023,8,60,'2012-04-01 00:00:00'),(26024,8,60,'2013-04-01 00:00:00'),(26025,8,61,'2008-04-01 00:00:00'),(26026,8,61,'2012-12-04 00:00:00'),(26027,8,62,'2008-04-01 00:00:00'),(26028,8,62,'2012-04-01 00:00:00'),(26029,8,63,'2013-06-01 00:00:00'),(26030,8,64,'2004-04-01 00:00:00'),(26031,8,64,'2012-04-01 00:00:00'),(26032,8,64,'2013-04-01 00:00:00'),(26033,8,64,'2020-07-14 00:00:00'),(26034,8,65,'2007-04-01 00:00:00'),(26035,8,65,'2010-04-01 00:00:00'),(26036,8,65,'2012-04-01 00:00:00'),(26037,8,65,'2012-07-01 00:00:00'),(26038,8,65,'2013-04-01 00:00:00'),(26039,8,70,'2004-04-01 00:00:00'),(26040,8,70,'2007-04-01 00:00:00'),(26041,8,70,'2012-07-01 00:00:00'),(26042,8,71,'2004-04-01 00:00:00'),(26043,8,71,'2007-04-01 00:00:00'),(26044,8,71,'2008-04-01 00:00:00'),(26045,8,71,'2012-04-01 00:00:00'),(29449,9,19,'2004-04-01 00:00:00'),(29450,9,19,'2007-04-01 00:00:00'),(29451,9,19,'2009-04-01 00:00:00'),(29452,9,19,'2010-07-01 00:00:00'),(29453,9,20,'2008-04-01 00:00:00'),(29454,9,20,'2009-04-01 00:00:00'),(29455,9,22,'2004-04-01 00:00:00'),(29456,9,22,'2007-04-01 00:00:00'),(29457,9,22,'2009-04-01 00:00:00'),(29458,9,22,'2012-07-01 00:00:00'),(29459,9,23,'2004-04-01 00:00:00'),(29460,9,23,'2007-04-01 00:00:00'),(29461,9,23,'2009-04-01 00:00:00'),(29462,9,27,'2004-10-01 00:00:00'),(29463,9,27,'2007-04-01 00:00:00'),(29464,9,27,'2009-04-01 00:00:00'),(29465,9,27,'2012-07-01 00:00:00'),(29466,9,28,'2013-06-01 00:00:00'),(29467,9,29,'2004-04-01 00:00:00'),(29468,9,29,'2007-04-01 00:00:00'),(29469,9,29,'2009-04-01 00:00:00'),(29470,9,31,'2007-04-01 00:00:00'),(29471,9,31,'2009-04-01 00:00:00'),(29472,9,32,'2004-04-01 00:00:00'),(29473,9,32,'2007-04-01 00:00:00'),(29474,9,32,'2009-04-01 00:00:00'),(29475,9,32,'2009-10-01 00:00:00'),(29476,9,32,'2010-07-01 00:00:00'),(29477,9,38,'2012-07-01 00:00:00'),(29478,9,39,'2004-04-01 00:00:00'),(29479,9,39,'2007-04-01 00:00:00'),(29480,9,39,'2009-04-01 00:00:00'),(29481,9,39,'2009-10-01 00:00:00'),(29482,9,39,'2010-07-01 00:00:00'),(29483,9,40,'2007-06-01 00:00:00'),(29484,9,40,'2009-04-01 00:00:00'),(29485,9,40,'2009-10-01 00:00:00'),(29486,9,40,'2010-07-01 00:00:00'),(29487,9,49,'2004-04-01 00:00:00'),(29488,9,49,'2007-04-01 00:00:00'),(29489,9,49,'2009-04-01 00:00:00'),(29490,9,49,'2010-07-01 00:00:00'),(29491,9,50,'2004-04-01 00:00:00'),(29492,9,50,'2007-04-01 00:00:00'),(29493,9,50,'2009-04-01 00:00:00'),(29494,9,50,'2010-07-01 00:00:00'),(29495,9,53,'2004-04-01 00:00:00'),(29496,9,53,'2005-04-01 00:00:00'),(29497,9,53,'2008-04-01 00:00:00'),(29498,9,53,'2009-04-01 00:00:00'),(29499,9,54,'2012-07-01 00:00:00'),(29500,9,55,'2004-04-01 00:00:00'),(29501,9,55,'2007-04-01 00:00:00'),(29502,9,55,'2009-04-01 00:00:00'),(29503,9,56,'2004-04-01 00:00:00'),(29504,9,56,'2007-04-01 00:00:00'),(29505,9,56,'2007-06-01 00:00:00'),(29506,9,56,'2009-04-01 00:00:00'),(29507,9,56,'2010-07-01 00:00:00'),(29508,9,57,'2007-04-01 00:00:00'),(29509,9,57,'2009-04-01 00:00:00'),(29510,9,58,'2004-04-01 00:00:00'),(29511,9,58,'2007-04-01 00:00:00'),(29512,9,58,'2007-06-01 00:00:00'),(29513,9,58,'2009-04-01 00:00:00'),(29514,9,58,'2010-07-01 00:00:00'),(29515,9,66,'2004-04-01 00:00:00'),(29516,9,66,'2007-04-01 00:00:00'),(29517,9,66,'2009-04-01 00:00:00'),(29518,9,66,'2009-10-01 00:00:00'),(29519,9,66,'2010-07-01 00:00:00'),(29520,9,67,'2004-04-01 00:00:00'),(29521,9,67,'2007-04-01 00:00:00'),(29522,9,67,'2009-04-01 00:00:00'),(29523,9,67,'2009-10-01 00:00:00'),(29524,9,67,'2010-07-01 00:00:00'),(29525,9,68,'2009-10-01 00:00:00'),(29526,9,68,'2010-04-01 00:00:00'),(29527,9,68,'2010-07-01 00:00:00'),(29528,9,69,'2009-10-01 00:00:00'),(29529,9,69,'2010-04-01 00:00:00'),(29530,9,69,'2010-07-01 00:00:00'),(34456,10,19,'2004-04-01 00:00:00'),(34457,10,19,'2005-04-01 00:00:00'),(34458,10,19,'2007-04-01 00:00:00'),(34459,10,19,'2009-04-01 00:00:00'),(34460,10,19,'2010-07-01 00:00:00'),(34461,10,20,'2008-04-01 00:00:00'),(34462,10,20,'2009-04-01 00:00:00'),(34463,10,22,'2004-04-01 00:00:00'),(34464,10,22,'2005-04-01 00:00:00'),(34465,10,22,'2007-04-01 00:00:00'),(34466,10,22,'2009-04-01 00:00:00'),(34467,10,22,'2012-07-01 00:00:00'),(34468,10,23,'2004-04-01 00:00:00'),(34469,10,23,'2005-04-01 00:00:00'),(34470,10,23,'2007-04-01 00:00:00'),(34471,10,23,'2009-04-01 00:00:00'),(34472,10,27,'2004-04-01 00:00:00'),(34473,10,27,'2005-04-01 00:00:00'),(34474,10,27,'2007-04-01 00:00:00'),(34475,10,27,'2009-04-01 00:00:00'),(34476,10,27,'2012-07-01 00:00:00'),(34477,10,28,'2013-06-01 00:00:00'),(34478,10,29,'2004-04-01 00:00:00'),(34479,10,29,'2005-04-01 00:00:00'),(34480,10,29,'2007-04-01 00:00:00'),(34481,10,29,'2009-04-01 00:00:00'),(34482,10,31,'2004-04-01 00:00:00'),(34483,10,31,'2005-04-01 00:00:00'),(34484,10,31,'2007-04-01 00:00:00'),(34485,10,31,'2009-04-01 00:00:00'),(34486,10,32,'2004-04-01 00:00:00'),(34487,10,32,'2005-04-01 00:00:00'),(34488,10,32,'2007-04-01 00:00:00'),(34489,10,32,'2009-04-01 00:00:00'),(34490,10,32,'2009-10-01 00:00:00'),(34491,10,32,'2010-07-01 00:00:00'),(34492,10,38,'2012-07-01 00:00:00'),(34493,10,39,'2004-04-01 00:00:00'),(34494,10,39,'2005-04-01 00:00:00'),(34495,10,39,'2007-04-01 00:00:00'),(34496,10,39,'2009-04-01 00:00:00'),(34497,10,39,'2009-10-01 00:00:00'),(34498,10,39,'2010-07-01 00:00:00'),(34499,10,40,'2007-06-01 00:00:00'),(34500,10,40,'2009-04-01 00:00:00'),(34501,10,40,'2009-10-01 00:00:00'),(34502,10,40,'2010-07-01 00:00:00'),(34503,10,49,'2004-04-01 00:00:00'),(34504,10,49,'2005-04-01 00:00:00'),(34505,10,49,'2007-04-01 00:00:00'),(34506,10,49,'2009-04-01 00:00:00'),(34507,10,49,'2010-07-01 00:00:00'),(34508,10,50,'2004-04-01 00:00:00'),(34509,10,50,'2005-04-01 00:00:00'),(34510,10,50,'2007-04-01 00:00:00'),(34511,10,50,'2009-04-01 00:00:00'),(34512,10,50,'2010-07-01 00:00:00'),(34513,10,53,'2004-04-01 00:00:00'),(34514,10,53,'2005-04-01 00:00:00'),(34515,10,53,'2007-04-01 00:00:00'),(34516,10,53,'2008-04-01 00:00:00'),(34517,10,53,'2009-04-01 00:00:00'),(34518,10,54,'2012-07-01 00:00:00'),(34519,10,55,'2004-04-01 00:00:00'),(34520,10,55,'2005-04-01 00:00:00'),(34521,10,55,'2007-04-01 00:00:00'),(34522,10,55,'2009-04-01 00:00:00'),(34523,10,56,'2004-04-01 00:00:00'),(34524,10,56,'2005-04-01 00:00:00'),(34525,10,56,'2007-04-01 00:00:00'),(34526,10,56,'2007-06-01 00:00:00'),(34527,10,56,'2009-04-01 00:00:00'),(34528,10,56,'2010-07-01 00:00:00'),(34529,10,57,'2007-04-01 00:00:00'),(34530,10,57,'2009-04-01 00:00:00'),(34531,10,58,'2004-04-01 00:00:00'),(34532,10,58,'2005-04-01 00:00:00'),(34533,10,58,'2007-04-01 00:00:00'),(34534,10,58,'2007-06-01 00:00:00'),(34535,10,58,'2009-04-01 00:00:00'),(34536,10,58,'2010-07-01 00:00:00'),(34537,10,66,'2004-04-01 00:00:00'),(34538,10,66,'2005-04-01 00:00:00'),(34539,10,66,'2007-04-01 00:00:00'),(34540,10,66,'2009-04-01 00:00:00'),(34541,10,66,'2009-10-01 00:00:00'),(34542,10,66,'2010-07-01 00:00:00'),(34543,10,67,'2004-04-01 00:00:00'),(34544,10,67,'2005-04-01 00:00:00'),(34545,10,67,'2007-04-01 00:00:00'),(34546,10,67,'2009-04-01 00:00:00'),(34547,10,67,'2009-10-01 00:00:00'),(34548,10,67,'2010-07-01 00:00:00'),(34549,10,68,'2009-10-01 00:00:00'),(34550,10,68,'2010-04-01 00:00:00'),(34551,10,68,'2010-07-01 00:00:00'),(34552,10,69,'2009-10-01 00:00:00'),(34553,10,69,'2010-04-01 00:00:00'),(34554,10,69,'2010-07-01 00:00:00'),(37170,11,19,'2004-04-01 00:00:00'),(37171,11,19,'2009-04-01 00:00:00'),(37172,11,19,'2010-07-01 00:00:00'),(37173,11,19,'2012-07-04 00:00:00'),(37174,11,22,'2004-04-01 00:00:00'),(37175,11,22,'2007-04-01 00:00:00'),(37176,11,22,'2009-04-01 00:00:00'),(37177,11,22,'2012-07-01 00:00:00'),(37178,11,23,'2004-04-01 00:00:00'),(37179,11,23,'2007-04-01 00:00:00'),(37180,11,23,'2009-04-01 00:00:00'),(37181,11,27,'2004-04-01 00:00:00'),(37182,11,27,'2007-04-01 00:00:00'),(37183,11,27,'2009-04-01 00:00:00'),(37184,11,27,'2012-07-01 00:00:00'),(37185,11,29,'2004-04-01 00:00:00'),(37186,11,29,'2007-04-01 00:00:00'),(37187,11,29,'2009-04-01 00:00:00'),(37188,11,31,'2004-04-01 00:00:00'),(37189,11,31,'2007-04-01 00:00:00'),(37190,11,31,'2009-04-01 00:00:00'),(37191,11,32,'2004-04-01 00:00:00'),(37192,11,32,'2007-04-01 00:00:00'),(37193,11,32,'2009-04-01 00:00:00'),(37194,11,32,'2009-10-01 00:00:00'),(37195,11,32,'2010-07-01 00:00:00'),(37196,11,39,'2004-04-01 00:00:00'),(37197,11,39,'2007-04-01 00:00:00'),(37198,11,39,'2009-04-01 00:00:00'),(37199,11,39,'2009-10-01 00:00:00'),(37200,11,39,'2010-07-01 00:00:00'),(37201,11,40,'2007-06-01 00:00:00'),(37202,11,40,'2009-04-01 00:00:00'),(37203,11,40,'2009-10-01 00:00:00'),(37204,11,40,'2010-07-01 00:00:00'),(37205,11,49,'2004-04-01 00:00:00'),(37206,11,49,'2007-04-01 00:00:00'),(37207,11,49,'2009-04-01 00:00:00'),(37208,11,49,'2010-07-01 00:00:00'),(37209,11,50,'2004-04-01 00:00:00'),(37210,11,50,'2007-04-01 00:00:00'),(37211,11,50,'2009-04-01 00:00:00'),(37212,11,50,'2010-07-01 00:00:00'),(37213,11,53,'2004-04-01 00:00:00'),(37214,11,53,'2007-04-01 00:00:00'),(37215,11,53,'2008-04-01 00:00:00'),(37216,11,53,'2009-04-01 00:00:00'),(37217,11,55,'2004-04-01 00:00:00'),(37218,11,55,'2007-04-01 00:00:00'),(37219,11,55,'2009-04-01 00:00:00'),(37220,11,56,'2004-04-01 00:00:00'),(37221,11,56,'2007-04-01 00:00:00'),(37222,11,56,'2007-06-01 00:00:00'),(37223,11,56,'2009-04-01 00:00:00'),(37224,11,56,'2010-07-01 00:00:00'),(37225,11,57,'2007-04-01 00:00:00'),(37226,11,57,'2009-04-01 00:00:00'),(37227,11,57,'2012-01-01 00:00:00'),(37228,11,58,'2004-04-01 00:00:00'),(37229,11,58,'2007-04-01 00:00:00'),(37230,11,58,'2007-06-01 00:00:00'),(37231,11,58,'2009-04-01 00:00:00'),(37232,11,58,'2010-07-01 00:00:00'),(37233,11,66,'2004-04-01 00:00:00'),(37234,11,66,'2007-04-01 00:00:00'),(37235,11,66,'2009-04-01 00:00:00'),(37236,11,66,'2009-10-01 00:00:00'),(37237,11,66,'2010-07-01 00:00:00'),(37238,11,67,'2004-04-01 00:00:00'),(37239,11,67,'2007-04-01 00:00:00'),(37240,11,67,'2009-04-01 00:00:00'),(37241,11,67,'2009-10-01 00:00:00'),(37242,11,67,'2010-07-01 00:00:00'),(40588,2,13,'2013-04-01 00:00:00'),(40589,2,14,'2013-04-01 00:00:00'),(40590,2,15,'2013-04-01 00:00:00'),(40591,2,16,'2013-04-01 00:00:00'),(40592,2,19,'2007-04-01 00:00:00'),(40593,2,19,'2009-04-01 00:00:00'),(40594,2,19,'2010-07-01 00:00:00'),(40595,2,22,'2007-04-01 00:00:00'),(40596,2,22,'2009-04-01 00:00:00'),(40597,2,22,'2012-07-01 00:00:00'),(40598,2,23,'2007-04-01 00:00:00'),(40599,2,23,'2009-04-01 00:00:00'),(40600,2,23,'2013-04-01 00:00:00'),(40601,2,24,'2013-04-01 00:00:00'),(40602,2,27,'2007-04-01 00:00:00'),(40603,2,27,'2009-04-01 00:00:00'),(40604,2,27,'2012-07-01 00:00:00'),(40605,2,28,'2013-06-01 00:00:00'),(40606,2,29,'2007-04-01 00:00:00'),(40607,2,29,'2009-04-01 00:00:00'),(40608,2,31,'2007-04-01 00:00:00'),(40609,2,31,'2009-04-01 00:00:00'),(40610,2,31,'2009-10-01 00:00:00'),(40611,2,31,'2010-07-01 00:00:00'),(40612,2,32,'2007-04-01 00:00:00'),(40613,2,38,'2012-07-01 00:00:00'),(40614,2,39,'2007-04-01 00:00:00'),(40615,2,39,'2009-04-01 00:00:00'),(40616,2,39,'2009-10-01 00:00:00'),(40617,2,39,'2010-07-01 00:00:00'),(40618,2,40,'2007-06-01 00:00:00'),(40619,2,40,'2009-04-01 00:00:00'),(40620,2,40,'2009-10-01 00:00:00'),(40621,2,40,'2010-07-01 00:00:00'),(40622,2,49,'2007-04-01 00:00:00'),(40623,2,49,'2009-04-01 00:00:00'),(40624,2,49,'2010-07-01 00:00:00'),(40625,2,50,'2007-04-01 00:00:00'),(40626,2,50,'2009-04-01 00:00:00'),(40627,2,50,'2010-07-01 00:00:00'),(40628,2,53,'2007-04-01 00:00:00'),(40629,2,53,'2009-04-01 00:00:00'),(40630,2,54,'2012-07-01 00:00:00'),(40631,2,54,'2013-04-01 00:00:00'),(40632,2,55,'2007-04-01 00:00:00'),(40633,2,55,'2009-04-01 00:00:00'),(40634,2,56,'2007-04-01 00:00:00'),(40635,2,56,'2007-06-01 00:00:00'),(40636,2,56,'2009-04-01 00:00:00'),(40637,2,56,'2010-07-01 00:00:00'),(40638,2,57,'2007-04-01 00:00:00'),(40639,2,57,'2009-04-01 00:00:00'),(40640,2,58,'2006-04-01 00:00:00'),(40641,2,58,'2007-04-01 00:00:00'),(40642,2,58,'2009-04-01 00:00:00'),(40643,2,58,'2010-07-01 00:00:00'),(40644,2,67,'2009-10-01 00:00:00'),(40645,2,67,'2010-07-01 00:00:00'),(40646,2,68,'2009-10-01 00:00:00'),(40647,2,68,'2010-04-01 00:00:00'),(40648,2,68,'2010-07-01 00:00:00');
/*!40000 ALTER TABLE `tds_policy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_section`
--

DROP TABLE IF EXISTS `tds_section`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_section` (
  `TDS_SECTION_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CODE` varchar(45) NOT NULL DEFAULT '',
  `SECTION_NAME` varchar(100) NOT NULL DEFAULT '',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '1=ACTIVE,0=INACTIVE',
  PRIMARY KEY (`TDS_SECTION_ID`),
  UNIQUE KEY `Index_CODE` (`CODE`) USING HASH,
  UNIQUE KEY `Index_SECTION_NAME` (`SECTION_NAME`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_section`
--

LOCK TABLES `tds_section` WRITE;
/*!40000 ALTER TABLE `tds_section` DISABLE KEYS */;
INSERT INTO `tds_section` VALUES (15,'195','Payment to Other Sum of A Non-Resident',1),(16,'193','Interest on Securities',1),(17,'194J','Fee for Professional Or Technical Services ',1),(18,'194G','Commission on Sale of Lotteries',1),(19,'194H','Commission or Brokerage',1),(20,'194','Dividend',1),(21,'194LD','Income by Way of Interest on Certain Bonds and Government Securities',1),(22,'196C','Income From Foreign Currency Bonds',1),(23,'194LC','Income From Foreign Currency, Bonds Or Shares of Indian Company',1),(24,'196D','Income of Foreign Institutional Investors',1),(25,'194D','Insurance Commission ',1),(26,'194LB','Interest on Infrastructure Debt Fund',1),(27,'194A','Interest Other Than Interest on Securities',1),(28,'194LA','Aquisition of Immovable Property',1),(29,'194IA','Payment on Transfer of Certain Immovable Perperty Other Than Agricultural Land',1),(30,'194EE','Deposit Under NSS',1),(31,'196B','Income From Units to an Offshore Fund',1),(32,'194F','Repurchase of Units of MF/UTI',1),(33,'194C ','Contractors & Sub-Contractors',1),(34,'194E','Non-Resident Sportsmen/Spots Association',1),(35,'194I','Rent',1),(36,'194BB','Winning From Horse Races',1),(37,'194B','Winnings From Lotteries',1),(38,'196A','196A',1);
/*!40000 ALTER TABLE `tds_section` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tds_tax_rate`
--

DROP TABLE IF EXISTS `tds_tax_rate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tds_tax_rate` (
  `TAX_RATE_ID` int(11) NOT NULL AUTO_INCREMENT,
  `TDS_POLICY_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `TDS_RATE` decimal(10,2) NOT NULL DEFAULT '0.00',
  `TDS_EXEMPTION_LIMIT` decimal(10,2) NOT NULL DEFAULT '0.00',
  `TDS_TAX_TYPE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`TAX_RATE_ID`),
  KEY `FK_TDS_POLICY_ID` (`TDS_POLICY_ID`),
  KEY `FK_TAX_TYPE_ID` (`TDS_TAX_TYPE_ID`),
  CONSTRAINT `FK_tds_tax_rate_POLICY_ID` FOREIGN KEY (`TDS_POLICY_ID`) REFERENCES `tds_policy` (`TDS_POLICY_ID`),
  CONSTRAINT `FK_tds_tax_rate_TAX_TYPE_ID` FOREIGN KEY (`TDS_TAX_TYPE_ID`) REFERENCES `tds_duty_taxtype` (`TDS_DUTY_TAXTYPE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=204486 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tds_tax_rate`
--

LOCK TABLES `tds_tax_rate` WRITE;
/*!40000 ALTER TABLE `tds_tax_rate` DISABLE KEYS */;
INSERT INTO `tds_tax_rate` VALUES (21101,4340,10.00,5000.00,1),(21102,4340,10.00,1000000.00,3),(21103,4340,2.00,0.00,4),(21104,4340,20.00,0.00,2),(21105,4340,0.00,0.00,5),(21106,4341,10.00,5000.00,1),(21107,4341,10.00,1000000.00,3),(21108,4341,2.00,0.00,4),(21109,4341,20.00,0.00,2),(21110,4341,1.00,0.00,5),(21111,4342,10.00,5000.00,1),(21112,4342,0.00,0.00,3),(21113,4342,0.00,0.00,4),(21114,4342,20.00,0.00,2),(21115,4342,0.00,0.00,5),(21116,4343,10.00,20000.00,1),(21117,4343,0.00,0.00,3),(21118,4343,0.00,0.00,4),(21119,4343,0.00,0.00,2),(21120,4343,0.00,0.00,5),(21121,4344,10.00,10000.00,1),(21122,4344,10.00,1000000.00,3),(21123,4344,2.00,0.00,4),(21124,4344,20.00,0.00,2),(21125,4344,1.00,0.00,5),(21126,4345,10.00,10000.00,1),(21127,4345,0.00,0.00,3),(21128,4345,0.00,0.00,4),(21129,4345,20.00,0.00,2),(21130,4345,0.00,0.00,5),(21131,4346,10.00,2500.00,1),(21132,4346,10.00,1000000.00,3),(21133,4346,2.00,0.00,4),(21134,4346,20.00,0.00,2),(21135,4346,0.00,0.00,5),(21136,4347,10.00,2500.00,1),(21137,4347,10.00,1000000.00,3),(21138,4347,2.00,0.00,4),(21139,4347,20.00,0.00,2),(21140,4347,1.00,0.00,5),(21141,4348,10.00,2500.00,1),(21142,4348,0.00,0.00,3),(21143,4348,0.00,0.00,4),(21144,4348,20.00,0.00,2),(21145,4348,0.00,0.00,5),(21146,4349,10.00,5000.00,1),(21147,4349,0.00,0.00,3),(21148,4349,0.00,0.00,4),(21149,4349,20.00,0.00,2),(21150,4349,0.00,0.00,5),(21151,4350,10.00,5000.00,1),(21152,4350,10.00,1000000.00,3),(21153,4350,2.00,0.00,4),(21154,4350,20.00,0.00,2),(21155,4350,0.00,0.00,5),(21156,4351,10.00,5000.00,1),(21157,4351,10.00,1000000.00,3),(21158,4351,2.00,0.00,4),(21159,4351,20.00,0.00,2),(21160,4351,1.00,0.00,5),(21161,4352,10.00,5000.00,1),(21162,4352,0.00,0.00,3),(21163,4352,0.00,0.00,4),(21164,4352,20.00,0.00,2),(21165,4352,0.00,0.00,5),(21166,4353,10.00,10000000.00,1),(21167,4353,10.00,1000000.00,3),(21168,4353,2.00,0.00,4),(21169,4353,20.00,0.00,2),(21170,4353,0.00,0.00,5),(21171,4354,10.00,10000000.00,1),(21172,4354,10.00,1000000.00,3),(21173,4354,2.00,0.00,4),(21174,4354,20.00,0.00,2),(21175,4354,1.00,0.00,5),(21176,4355,10.00,10000000.00,1),(21177,4355,0.00,0.00,3),(21178,4355,0.00,0.00,4),(21179,4355,20.00,0.00,2),(21180,4355,0.00,0.00,5),(21181,4356,10.00,20000000.00,1),(21182,4356,0.00,0.00,3),(21183,4356,0.00,0.00,4),(21184,4356,20.00,0.00,2),(21185,4356,0.00,0.00,5),(21186,4357,1.00,5000000.00,1),(21187,4357,0.00,0.00,3),(21188,4357,0.00,0.00,4),(21189,4357,20.00,0.00,2),(21190,4357,0.00,0.00,5),(21191,4358,20.00,2500.00,1),(21192,4358,10.00,1000000.00,3),(21193,4358,2.00,0.00,4),(21194,4358,20.00,0.00,2),(21195,4358,0.00,0.00,5),(21196,4359,20.00,0.00,1),(21197,4359,10.00,1000000.00,3),(21198,4359,2.00,0.00,4),(21199,4359,20.00,0.00,2),(21200,4359,1.00,0.00,5),(21201,4360,20.00,0.00,1),(21202,4360,0.00,0.00,3),(21203,4360,0.00,0.00,4),(21204,4360,20.00,0.00,2),(21205,4360,0.00,0.00,5),(21206,4361,20.00,0.00,1),(21207,4361,10.00,1000000.00,3),(21208,4361,2.00,0.00,4),(21209,4361,20.00,0.00,2),(21210,4361,0.00,0.00,5),(21211,4362,20.00,0.00,1),(21212,4362,10.00,1000000.00,3),(21213,4362,2.00,0.00,4),(21214,4362,20.00,0.00,2),(21215,4362,1.00,0.00,5),(21216,4363,20.00,0.00,1),(21217,4363,0.00,0.00,3),(21218,4363,0.00,0.00,4),(21219,4363,0.00,0.00,2),(21220,4363,0.00,0.00,5),(21221,4364,2.00,50000.00,1),(21222,4364,10.00,1000000.00,3),(21223,4364,2.00,0.00,4),(21224,4364,20.00,0.00,2),(21225,4364,0.00,0.00,5),(21226,4365,2.00,50000.00,1),(21227,4365,10.00,1000000.00,3),(21228,4365,2.00,0.00,4),(21229,4365,20.00,0.00,2),(21230,4365,1.00,0.00,5),(21231,4366,2.00,50000.00,1),(21232,4366,0.00,0.00,3),(21233,4366,0.00,0.00,4),(21234,4366,20.00,0.00,2),(21235,4366,0.00,0.00,5),(21236,4367,2.00,50000.00,1),(21237,4367,0.00,0.00,3),(21238,4367,0.00,0.00,4),(21239,4367,20.00,0.00,2),(21240,4367,0.00,0.00,5),(21241,4368,2.00,75000.00,1),(21242,4368,0.00,0.00,3),(21243,4368,0.00,0.00,4),(21244,4368,20.00,0.00,2),(21245,4368,0.00,0.00,5),(21246,4369,10.00,30000.00,1),(21247,4369,0.00,0.00,3),(21248,4369,0.00,0.00,4),(21249,4369,20.00,0.00,2),(21250,4369,0.00,0.00,5),(21251,4370,15.00,120000.00,1),(21252,4370,10.00,1000000.00,3),(21253,4370,2.00,0.00,4),(21254,4370,20.00,0.00,2),(21255,4370,0.00,0.00,5),(21256,4371,15.00,120000.00,1),(21257,4371,10.00,1000000.00,3),(21258,4371,2.00,0.00,4),(21259,4371,20.00,0.00,2),(21260,4371,1.00,0.00,5),(21261,4372,20.00,120000.00,1),(21262,4372,10.00,1000000.00,3),(21263,4372,2.00,0.00,4),(21264,4372,20.00,0.00,2),(21265,4372,1.00,0.00,5),(21266,4373,20.00,120000.00,1),(21267,4373,0.00,0.00,3),(21268,4373,0.00,0.00,4),(21269,4373,20.00,0.00,2),(21270,4373,0.00,0.00,5),(21271,4374,10.00,120000.00,1),(21272,4374,0.00,0.00,3),(21273,4374,0.00,0.00,4),(21274,4374,20.00,0.00,2),(21275,4374,0.00,0.00,5),(21276,4375,10.00,180000.00,1),(21277,4375,0.00,0.00,3),(21278,4375,0.00,0.00,4),(21279,4375,20.00,0.00,2),(21280,4375,0.00,0.00,5),(21281,4376,10.00,120000.00,1),(21282,4376,10.00,1000000.00,3),(21283,4376,2.00,0.00,4),(21284,4376,20.00,0.00,2),(21285,4376,1.00,0.00,5),(21286,4377,10.00,120000.00,1),(21287,4377,0.00,0.00,3),(21288,4377,0.00,0.00,4),(21289,4377,20.00,0.00,2),(21290,4377,0.00,0.00,5),(21291,4378,2.00,120000.00,1),(21292,4378,0.00,0.00,3),(21293,4378,0.00,0.00,4),(21294,4378,20.00,0.00,2),(21295,4378,0.00,0.00,5),(21296,4379,2.00,180000.00,1),(21297,4379,0.00,0.00,3),(21298,4379,0.00,0.00,4),(21299,4379,20.00,0.00,2),(21300,4379,0.00,0.00,5),(21301,4380,30.00,2500.00,1),(21302,4380,10.00,1000000.00,3),(21303,4380,2.00,0.00,4),(21304,4380,20.00,0.00,2),(21305,4380,0.00,0.00,5),(21306,4381,30.00,2500.00,1),(21307,4381,10.00,1000000.00,3),(21308,4381,2.00,0.00,4),(21309,4381,20.00,0.00,2),(21310,4381,1.00,0.00,5),(21311,4382,30.00,2500.00,1),(21312,4382,0.00,0.00,3),(21313,4382,0.00,0.00,4),(21314,4382,20.00,0.00,2),(21315,4382,0.00,0.00,5),(21316,4383,30.00,5000.00,1),(21317,4383,0.00,0.00,3),(21318,4383,0.00,0.00,4),(21319,4383,20.00,0.00,2),(21320,4383,0.00,0.00,5),(21321,4384,30.00,5000.00,1),(21322,4384,10.00,1000000.00,3),(21323,4384,2.00,0.00,4),(21324,4384,20.00,0.00,2),(21325,4384,0.00,0.00,5),(21326,4385,30.00,5000.00,1),(21327,4385,10.00,1000000.00,3),(21328,4385,2.00,0.00,4),(21329,4385,20.00,0.00,2),(21330,4385,0.00,0.00,5),(21331,4386,30.00,5000.00,1),(21332,4386,0.00,0.00,3),(21333,4386,0.00,0.00,4),(21334,4386,20.00,0.00,2),(21335,4386,0.00,0.00,5),(21336,4387,30.00,10000.00,1),(21337,4387,0.00,0.00,3),(21338,4387,0.00,0.00,4),(21339,4387,20.00,0.00,2),(21340,4387,0.00,0.00,5),(21341,4388,20.00,2500.00,1),(21342,4388,10.00,1000000.00,3),(21343,4388,2.00,0.00,4),(21344,4388,20.00,0.00,2),(21345,4388,0.00,0.00,5),(21346,4389,20.00,2500.00,1),(21347,4389,10.00,1000000.00,3),(21348,4389,2.00,0.00,4),(21349,4389,20.00,0.00,2),(21350,4389,1.00,0.00,5),(21351,4390,20.00,0.00,1),(21352,4390,10.00,1000000.00,3),(21353,4390,2.00,0.00,4),(21354,4390,20.00,0.00,2),(21355,4390,1.00,0.00,5),(21356,4391,10.00,0.00,1),(21357,4391,0.00,0.00,3),(21358,4391,0.00,0.00,4),(21359,4391,20.00,0.00,2),(21360,4391,0.00,0.00,5),(21361,4393,10.00,0.00,1),(21362,4393,0.00,0.00,3),(21363,4393,0.00,0.00,4),(21364,4393,0.00,0.00,2),(21365,4393,0.00,0.00,5),(21366,4394,10.00,1000.00,1),(21367,4394,10.00,1000000.00,3),(21368,4394,2.00,0.00,4),(21369,4394,20.00,0.00,2),(21370,4394,0.00,0.00,5),(21371,4395,10.00,1000.00,1),(21372,4395,0.00,1000000.00,3),(21373,4395,0.00,0.00,4),(21374,4395,20.00,0.00,2),(21375,4395,0.00,0.00,5),(21376,4396,5.00,2500.00,1),(21377,4396,10.00,1000000.00,3),(21378,4396,2.00,0.00,4),(21379,4396,20.00,0.00,2),(21380,4396,0.00,0.00,5),(21381,4397,5.00,2500.00,1),(21382,4397,10.00,1000000.00,3),(21383,4397,2.00,0.00,4),(21384,4397,20.00,0.00,2),(21385,4397,1.00,0.00,5),(21386,4398,10.00,2500.00,1),(21387,4398,10.00,1000000.00,3),(21388,4398,2.00,0.00,4),(21389,4398,20.00,0.00,2),(21390,4398,1.00,0.00,5),(21391,4399,10.00,2500.00,1),(21392,4399,0.00,0.00,3),(21393,4399,0.00,0.00,4),(21394,4399,20.00,0.00,2),(21395,4399,0.00,0.00,5),(21396,4400,10.00,5000.00,1),(21397,4400,0.00,0.00,3),(21398,4400,0.00,0.00,4),(21399,4400,0.00,0.00,2),(21400,4400,0.00,0.00,5),(21401,4401,20.00,2500.00,1),(21402,4401,10.00,1000000.00,3),(21403,4401,2.00,0.00,4),(21404,4401,20.00,0.00,2),(21405,4401,1.00,0.00,5),(21406,4402,10.00,2500.00,1),(21407,4402,0.00,0.00,3),(21408,4402,0.00,0.00,4),(21409,4402,20.00,0.00,2),(21410,4402,0.00,0.00,5),(21411,4403,5.00,20000.00,1),(21412,4403,10.00,1000000.00,3),(21413,4403,2.00,0.00,4),(21414,4403,20.00,0.00,2),(21415,4403,0.00,0.00,5),(21416,4404,5.00,20000.00,1),(21417,4404,10.00,1000000.00,3),(21418,4404,2.00,0.00,4),(21419,4404,20.00,0.00,2),(21420,4404,1.00,0.00,5),(21421,4405,10.00,20000.00,1),(21422,4405,10.00,1000000.00,3),(21423,4405,2.00,0.00,4),(21424,4405,20.00,0.00,2),(21425,4405,1.00,0.00,5),(21426,4406,10.00,20000.00,1),(21427,4406,0.00,0.00,3),(21428,4406,0.00,0.00,4),(21429,4406,20.00,0.00,2),(21430,4406,0.00,0.00,5),(21431,4407,10.00,30000.00,1),(21432,4407,0.00,0.00,3),(21433,4407,0.00,0.00,4),(21434,4407,20.00,0.00,2),(21435,4407,0.00,0.00,5),(21436,4408,1.00,20000.00,1),(21437,4408,10.00,1000000.00,3),(21438,4408,2.00,0.00,4),(21439,4408,20.00,0.00,2),(21440,4408,0.00,0.00,5),(21441,4409,1.00,20000.00,1),(21442,4409,10.00,1000000.00,3),(21443,4409,0.00,0.00,4),(21444,4409,20.00,0.00,2),(21445,4409,0.00,0.00,5),(21446,4410,1.00,20000.00,1),(21447,4410,0.00,0.00,3),(21448,4410,0.00,0.00,4),(21449,4410,20.00,0.00,2),(21450,4410,0.00,0.00,5),(21451,4411,2.00,20000.00,1),(21452,4411,0.00,0.00,3),(21453,4411,0.00,0.00,4),(21454,4411,20.00,0.00,2),(21455,4411,0.00,0.00,5),(21456,4412,2.00,75000.00,1),(21457,4412,0.00,0.00,3),(21458,4412,0.00,0.00,4),(21459,4412,20.00,0.00,2),(21460,4412,0.00,0.00,5),(21461,4413,1.00,20000.00,1),(21462,4413,10.00,1000000.00,3),(21463,4413,2.00,0.00,4),(21464,4413,20.00,0.00,2),(21465,4413,0.00,0.00,5),(21466,4414,1.00,50000.00,1),(21467,4414,10.00,1000000.00,3),(21468,4414,2.00,0.00,4),(21469,4414,20.00,0.00,2),(21470,4414,1.00,0.00,5),(21471,4415,1.00,50000.00,1),(21472,4415,0.00,0.00,3),(21473,4415,0.00,0.00,4),(21474,4415,20.00,0.00,2),(21475,4415,0.00,0.00,5),(21476,4416,2.00,50000.00,1),(21477,4416,0.00,0.00,3),(21478,4416,0.00,0.00,4),(21479,4416,20.00,0.00,2),(21480,4416,0.00,0.00,5),(21481,4417,2.00,75000.00,1),(21482,4417,0.00,0.00,3),(21483,4417,0.00,0.00,4),(21484,4417,20.00,0.00,2),(21485,4417,0.00,0.00,5),(21486,4418,2.00,50000.00,1),(21487,4418,0.00,0.00,3),(21488,4418,0.00,0.00,4),(21489,4418,20.00,0.00,2),(21490,4418,0.00,0.00,5),(21491,4420,20.00,75000.00,1),(21492,4420,0.00,0.00,3),(21493,4420,0.00,0.00,4),(21494,4420,20.00,0.00,2),(21495,4420,0.00,0.00,5),(21496,4421,2.00,50000.00,1),(21497,4421,0.00,0.00,3),(21498,4421,0.00,0.00,4),(21499,4421,20.00,0.00,2),(21500,4421,0.00,0.00,5),(21501,4423,20.00,75000.00,1),(21502,4423,0.00,0.00,3),(21503,4423,0.00,0.00,4),(21504,4423,20.00,0.00,2),(21505,4423,0.00,0.00,5),(39956,8120,10.00,10000.00,1),(39957,8120,0.00,0.00,3),(39958,8120,0.00,0.00,4),(39959,8120,20.00,0.00,2),(39960,8120,0.00,0.00,5),(39961,8121,10.00,5000.00,1),(39962,8121,10.00,1000000.00,3),(39963,8121,2.00,0.00,4),(39964,8121,20.00,0.00,2),(39965,8121,0.00,0.00,5),(39966,8122,10.00,5000.00,1),(39967,8122,10.00,1000000.00,3),(39968,8122,2.00,0.00,4),(39969,8122,20.00,0.00,2),(39970,8122,1.00,0.00,5),(39971,8123,10.00,20000.00,1),(39972,8123,0.00,0.00,3),(39973,8123,0.00,0.00,4),(39974,8123,20.00,0.00,2),(39975,8123,0.00,0.00,5),(39976,8124,10.00,10000.00,1),(39977,8124,10.00,1000000.00,3),(39978,8124,2.00,0.00,4),(39979,8124,20.00,0.00,2),(39980,8124,1.00,0.00,5),(39981,8125,20.00,75000.00,1),(39982,8125,0.00,0.00,3),(39983,8125,0.00,0.00,4),(39984,8125,20.00,0.00,2),(39985,8125,0.00,0.00,5),(39986,8126,10.00,2500.00,1),(39987,8126,10.00,1000000.00,3),(39988,8126,2.00,0.00,4),(39989,8126,20.00,0.00,2),(39990,8126,0.00,0.00,5),(39991,8127,10.00,2500.00,1),(39992,8127,10.00,1000000.00,3),(39993,8127,2.00,0.00,4),(39994,8127,20.00,0.00,2),(39995,8127,1.00,0.00,5),(39996,8128,10.00,25000.00,1),(39997,8128,0.00,0.00,3),(39998,8128,0.00,0.00,4),(39999,8128,20.00,0.00,2),(40000,8128,0.00,0.00,5),(40001,8129,10.00,5000.00,1),(40002,8129,0.00,0.00,3),(40003,8129,0.00,0.00,4),(40004,8129,20.00,0.00,2),(40005,8129,0.00,0.00,5),(40006,8130,10.00,5000.00,1),(40007,8130,10.00,1000000.00,3),(40008,8130,2.00,0.00,4),(40009,8130,20.00,0.00,2),(40010,8130,0.00,0.00,5),(40011,8131,10.00,5000.00,1),(40012,8131,10.00,1000000.00,3),(40013,8131,2.00,0.00,4),(40014,8131,20.00,0.00,2),(40015,8131,1.00,0.00,5),(40016,8132,10.00,5000.00,1),(40017,8132,0.00,0.00,3),(40018,8132,0.00,0.00,4),(40019,8132,20.00,0.00,2),(40020,8132,0.00,0.00,5),(40021,8133,10.00,100000.00,1),(40022,8133,10.00,1000000.00,3),(40023,8133,2.00,0.00,4),(40024,8133,20.00,0.00,2),(40025,8133,0.00,0.00,5),(40026,8134,10.00,100000.00,1),(40027,8134,10.00,1000000.00,3),(40028,8134,2.00,0.00,4),(40029,8134,20.00,0.00,2),(40030,8134,1.00,0.00,5),(40031,8135,10.00,100000.00,1),(40032,8135,0.00,0.00,3),(40033,8135,0.00,0.00,4),(40034,8135,20.00,0.00,2),(40035,8135,0.00,0.00,5),(40036,8136,10.00,200000.00,1),(40037,8136,0.00,0.00,3),(40038,8136,0.00,0.00,4),(40039,8136,20.00,0.00,2),(40040,8136,0.00,0.00,5),(40041,8137,1.00,5000000.00,1),(40042,8137,0.00,0.00,3),(40043,8137,0.00,0.00,4),(40044,8137,20.00,0.00,2),(40045,8137,0.00,0.00,5),(40046,8138,20.00,2500.00,1),(40047,8138,10.00,1000000.00,3),(40048,8138,2.00,0.00,4),(40049,8138,20.00,0.00,2),(40050,8138,0.00,0.00,5),(40051,8139,20.00,0.00,1),(40052,8139,10.00,1000000.00,3),(40053,8139,2.00,0.00,4),(40054,8139,20.00,0.00,2),(40055,8139,1.00,0.00,5),(40056,8140,20.00,0.00,1),(40057,8140,0.00,0.00,3),(40058,8140,0.00,0.00,4),(40059,8140,20.00,0.00,2),(40060,8140,0.00,0.00,5),(40061,8141,20.00,0.00,1),(40062,8141,10.00,1000000.00,3),(40063,8141,2.00,0.00,4),(40064,8141,20.00,0.00,2),(40065,8141,0.00,0.00,5),(40066,8142,20.00,0.00,1),(40067,8142,10.00,1000000.00,3),(40068,8142,2.00,0.00,4),(40069,8142,20.00,0.00,2),(40070,8142,1.00,0.00,5),(40071,8143,20.00,0.00,1),(40072,8143,0.00,0.00,3),(40073,8143,0.00,0.00,4),(40074,8143,20.00,0.00,2),(40075,8143,0.00,0.00,5),(40076,8144,2.00,20000.00,1),(40077,8144,10.00,100000.00,3),(40078,8144,2.00,0.00,4),(40079,8144,20.00,0.00,2),(40080,8144,0.00,0.00,5),(40081,8145,2.00,50000.00,1),(40082,8145,10.00,1000000.00,3),(40083,8145,2.00,0.00,4),(40084,8145,20.00,0.00,2),(40085,8145,1.00,0.00,5),(40086,8146,2.00,50000.00,1),(40087,8146,0.00,0.00,3),(40088,8146,0.00,0.00,4),(40089,8146,20.00,0.00,2),(40090,8146,0.00,0.00,5),(40091,8147,2.00,50000.00,1),(40092,8147,0.00,0.00,3),(40093,8147,0.00,0.00,4),(40094,8147,20.00,0.00,2),(40095,8147,0.00,0.00,5),(40096,8148,2.00,75000.00,1),(40097,8148,0.00,0.00,3),(40098,8148,0.00,0.00,4),(40099,8148,20.00,0.00,2),(40100,8148,0.00,0.00,5),(40101,8149,10.00,30000.00,1),(40102,8149,0.00,0.00,3),(40103,8149,0.00,0.00,4),(40104,8149,20.00,0.00,2),(40105,8149,0.00,0.00,5),(40106,8150,15.00,120000.00,1),(40107,8150,10.00,1000000.00,3),(40108,8150,2.00,0.00,4),(40109,8150,20.00,0.00,2),(40110,8150,0.00,0.00,5),(40111,8151,15.00,120000.00,1),(40112,8151,10.00,1000000.00,3),(40113,8151,2.00,0.00,4),(40114,8151,20.00,0.00,2),(40115,8151,1.00,0.00,5),(40116,8152,20.00,120000.00,1),(40117,8152,10.00,1000000.00,3),(40118,8152,2.00,0.00,4),(40119,8152,20.00,0.00,2),(40120,8152,1.00,0.00,5),(40121,8153,20.00,120000.00,1),(40122,8153,0.00,0.00,3),(40123,8153,0.00,0.00,4),(40124,8153,20.00,0.00,2),(40125,8153,0.00,0.00,5),(40126,8154,10.00,120000.00,1),(40127,8154,0.00,0.00,3),(40128,8154,0.00,0.00,4),(40129,8154,20.00,0.00,2),(40130,8154,0.00,0.00,5),(40131,8155,10.00,180000.00,1),(40132,8155,0.00,0.00,3),(40133,8155,0.00,0.00,4),(40134,8155,20.00,0.00,2),(40135,8155,0.00,0.00,5),(40136,8156,10.00,120000.00,1),(40137,8156,10.00,1000000.00,3),(40138,8156,2.00,0.00,4),(40139,8156,20.00,0.00,2),(40140,8156,1.00,0.00,5),(40141,8157,10.00,120000.00,1),(40142,8157,0.00,0.00,3),(40143,8157,0.00,0.00,4),(40144,8157,20.00,0.00,2),(40145,8157,0.00,0.00,5),(40146,8158,2.00,120000.00,1),(40147,8158,0.00,0.00,3),(40148,8158,0.00,0.00,4),(40149,8158,20.00,0.00,2),(40150,8158,0.00,0.00,5),(40151,8159,2.00,180000.00,1),(40152,8159,0.00,0.00,3),(40153,8159,0.00,0.00,4),(40154,8159,20.00,0.00,2),(40155,8159,0.00,0.00,5),(40156,8160,30.00,2500.00,1),(40157,8160,10.00,1000000.00,3),(40158,8160,2.00,0.00,4),(40159,8160,20.00,0.00,2),(40160,8160,0.00,0.00,5),(40161,8161,30.00,5000.00,1),(40162,8161,0.00,0.00,3),(40163,8161,0.00,0.00,4),(40164,8161,20.00,0.00,2),(40165,8161,0.00,0.00,5),(40166,8162,30.00,2500.00,1),(40167,8162,10.00,1000000.00,3),(40168,8162,2.00,0.00,4),(40169,8162,20.00,0.00,2),(40170,8162,1.00,0.00,5),(40171,8163,30.00,2500.00,1),(40172,8163,0.00,0.00,3),(40173,8163,0.00,0.00,4),(40174,8163,20.00,0.00,2),(40175,8163,0.00,0.00,5),(40176,8164,30.00,5000.00,1),(40177,8164,10.00,1000000.00,3),(40178,8164,2.00,0.00,4),(40179,8164,20.00,0.00,2),(40180,8164,0.00,0.00,5),(40181,8165,30.00,5000.00,1),(40182,8165,10.00,1000000.00,3),(40183,8165,2.00,0.00,4),(40184,8165,20.00,0.00,2),(40185,8165,1.00,0.00,5),(40186,8166,30.00,10000.00,1),(40187,8166,0.00,0.00,3),(40188,8166,0.00,0.00,4),(40189,8166,20.00,0.00,2),(40190,8166,0.00,0.00,5),(40191,8167,20.00,2500.00,1),(40192,8167,10.00,1000000.00,3),(40193,8167,2.00,0.00,4),(40194,8167,20.00,0.00,2),(40195,8167,0.00,0.00,5),(40196,8168,20.00,2500.00,1),(40197,8168,10.00,1000000.00,3),(40198,8168,2.00,0.00,4),(40199,8168,20.00,0.00,2),(40200,8168,1.00,0.00,5),(40201,8169,20.00,0.00,1),(40202,8169,10.00,1000000.00,3),(40203,8169,2.00,0.00,4),(40204,8169,20.00,0.00,2),(40205,8169,1.00,0.00,5),(40206,8170,10.00,0.00,1),(40207,8170,0.00,0.00,3),(40208,8170,0.00,0.00,4),(40209,8170,20.00,0.00,2),(40210,8170,0.00,0.00,5),(40211,8171,10.00,0.00,1),(40212,8171,0.00,0.00,3),(40213,8171,0.00,0.00,4),(40214,8171,20.00,0.00,2),(40215,8171,0.00,0.00,5),(40216,8172,10.00,1000.00,1),(40217,8172,10.00,1000000.00,3),(40218,8172,1.00,0.00,4),(40219,8172,20.00,0.00,2),(40220,8172,0.00,0.00,5),(40221,8173,10.00,1000.00,1),(40222,8173,10.00,1000000.00,3),(40223,8173,2.00,0.00,4),(40224,8173,20.00,0.00,2),(40225,8173,1.00,0.00,5),(40226,8174,10.00,1000.00,1),(40227,8174,0.00,0.00,3),(40228,8174,0.00,0.00,4),(40229,8174,20.00,0.00,2),(40230,8174,0.00,0.00,5),(40231,8175,5.00,2500.00,1),(40232,8175,10.00,1000000.00,3),(40233,8175,2.00,0.00,4),(40234,8175,20.00,0.00,2),(40235,8175,0.00,0.00,5),(40236,8176,5.00,2500.00,1),(40237,8176,10.00,1000000.00,3),(40238,8176,2.00,0.00,4),(40239,8176,20.00,0.00,2),(40240,8176,1.00,0.00,5),(40241,8177,10.00,2500.00,1),(40242,8177,10.00,1000000.00,3),(40243,8177,2.00,0.00,4),(40244,8177,20.00,0.00,2),(40245,8177,1.00,0.00,5),(40246,8178,10.00,2500.00,1),(40247,8178,0.00,0.00,3),(40248,8178,0.00,0.00,4),(40249,8178,20.00,0.00,2),(40250,8178,0.00,0.00,5),(40251,8179,10.00,5000.00,1),(40252,8179,0.00,0.00,3),(40253,8179,0.00,0.00,4),(40254,8179,20.00,0.00,2),(40255,8179,0.00,0.00,5),(40256,8180,20.00,2500.00,1),(40257,8180,10.00,1000000.00,3),(40258,8180,2.00,0.00,4),(40259,8180,20.00,0.00,2),(40260,8180,1.00,0.00,5),(40261,8181,10.00,2500.00,1),(40262,8181,0.00,0.00,3),(40263,8181,0.00,0.00,4),(40264,8181,20.00,0.00,2),(40265,8181,0.00,0.00,5),(40266,8182,5.00,20000.00,1),(40267,8182,10.00,1000000.00,3),(40268,8182,2.00,0.00,4),(40269,8182,20.00,0.00,2),(40270,8182,1.00,0.00,5),(40271,8183,5.00,20000.00,1),(40272,8183,10.00,1000000.00,3),(40273,8183,2.00,0.00,4),(40274,8183,20.00,0.00,2),(40275,8183,1.00,0.00,5),(40276,8184,10.00,20000.00,1),(40277,8184,10.00,1000000.00,3),(40278,8184,2.00,0.00,4),(40279,8184,20.00,0.00,2),(40280,8184,1.00,0.00,5),(40281,8185,10.00,20000.00,1),(40282,8185,0.00,0.00,3),(40283,8185,0.00,0.00,4),(40284,8185,20.00,0.00,2),(40285,8185,0.00,0.00,5),(40286,8186,10.00,30000.00,1),(40287,8186,0.00,0.00,3),(40288,8186,0.00,0.00,4),(40289,8186,20.00,0.00,2),(40290,8186,0.00,0.00,5),(40291,8187,1.00,20000.00,1),(40292,8187,10.00,1000000.00,3),(40293,8187,2.00,0.00,4),(40294,8187,20.00,0.00,2),(40295,8187,1.00,0.00,5),(40296,8188,1.00,20000.00,1),(40297,8188,10.00,1000000.00,3),(40298,8188,2.00,0.00,4),(40299,8188,20.00,0.00,2),(40300,8188,1.00,0.00,5),(40301,8189,1.00,20000.00,1),(40302,8189,0.00,0.00,3),(40303,8189,0.00,0.00,4),(40304,8189,20.00,0.00,2),(40305,8189,0.00,0.00,5),(40306,8190,2.00,20000.00,1),(40307,8190,0.00,0.00,3),(40308,8190,0.00,0.00,4),(40309,8190,20.00,0.00,2),(40310,8190,0.00,0.00,5),(40311,8191,2.00,75000.00,1),(40312,8191,0.00,0.00,3),(40313,8191,0.00,0.00,4),(40314,8191,20.00,0.00,2),(40315,8191,0.00,0.00,5),(40316,8192,1.00,20000.00,1),(40317,8192,10.00,1000000.00,3),(40318,8192,2.00,0.00,4),(40319,8192,20.00,0.00,2),(40320,8192,0.00,0.00,5),(40321,8193,1.00,50000.00,1),(40322,8193,10.00,1000000.00,3),(40323,8193,2.00,0.00,4),(40324,8193,20.00,0.00,2),(40325,8193,1.00,0.00,5),(40326,8194,1.00,50000.00,1),(40327,8194,0.00,0.00,3),(40328,8194,0.00,0.00,4),(40329,8194,20.00,0.00,2),(40330,8194,0.00,0.00,5),(40331,8195,2.00,50000.00,1),(40332,8195,0.00,0.00,3),(40333,8195,0.00,0.00,4),(40334,8195,20.00,0.00,2),(40335,8195,0.00,0.00,5),(40336,8196,2.00,50000.00,1),(40337,8196,0.00,0.00,3),(40338,8196,0.00,0.00,4),(40339,8196,20.00,0.00,2),(40340,8196,0.00,0.00,5),(40341,8198,20.00,75000.00,1),(40342,8198,0.00,0.00,3),(40343,8198,0.00,0.00,4),(40344,8198,20.00,0.00,2),(40345,8198,0.00,0.00,5),(40346,8199,1.00,50000.00,1),(40347,8199,0.00,0.00,3),(40348,8199,0.00,0.00,4),(40349,8199,20.00,0.00,2),(40350,8199,0.00,0.00,5),(40351,8200,20.00,75000.00,1),(40352,8200,0.00,0.00,3),(40353,8200,0.00,0.00,4),(40354,8200,20.00,0.00,2),(40355,8200,0.00,0.00,5),(67136,13627,40.00,0.00,1),(67137,13627,2.50,10000000.00,3),(67138,13627,2.00,0.00,4),(67139,13627,20.00,0.00,2),(67140,13627,1.00,0.00,5),(67141,13628,40.00,0.00,1),(67142,13628,2.00,10000000.00,3),(67143,13628,2.00,0.00,4),(67144,13628,20.00,0.00,2),(67145,13628,1.00,0.00,5),(67146,13629,40.00,0.00,1),(67147,13629,2.50,10000000.00,3),(67148,13629,2.00,0.00,4),(67149,13629,20.00,0.00,2),(67150,13629,1.00,0.00,5),(67151,13630,40.00,0.00,1),(67152,13630,2.00,10000000.00,3),(67153,13630,2.00,0.00,4),(67154,13630,20.00,0.00,2),(67155,13630,1.00,0.00,5),(67156,13631,10.00,0.00,1),(67157,13631,0.00,0.00,3),(67158,13631,2.00,0.00,4),(67159,13631,0.00,0.00,2),(67160,13631,1.00,0.00,5),(67161,13632,10.00,0.00,1),(67162,13632,2.00,10000000.00,3),(67163,13632,2.00,0.00,4),(67164,13632,20.00,0.00,2),(67165,13632,1.00,0.00,5),(67166,13633,10.00,0.00,1),(67167,13633,2.50,10000000.00,3),(67168,13633,2.00,0.00,4),(67169,13633,20.00,0.00,2),(67170,13633,2.00,0.00,5),(67171,13634,10.00,0.00,1),(67172,13634,2.00,10000000.00,3),(67173,13634,2.00,0.00,4),(67174,13634,20.00,0.00,2),(67175,13634,1.00,0.00,5),(67176,13635,10.00,0.00,1),(67177,13635,2.50,0.00,3),(67178,13635,2.00,0.00,4),(67179,13635,20.00,0.00,2),(67180,13635,0.00,0.00,5),(67181,13636,10.00,0.00,1),(67182,13636,2.50,10000000.00,3),(67183,13636,2.00,0.00,4),(67184,13636,20.00,0.00,2),(67185,13636,1.00,0.00,5),(67186,13637,5.00,0.00,1),(67187,13637,2.00,10000000.00,3),(67188,13637,2.00,0.00,4),(67189,13637,20.00,0.00,2),(67190,13637,1.00,0.00,5),(67191,13638,20.00,0.00,1),(67192,13638,0.00,0.00,3),(67193,13638,2.00,0.00,4),(67194,13638,20.00,0.00,2),(67195,13638,1.00,0.00,5),(67196,13639,20.00,0.00,1),(67197,13639,2.00,10000000.00,3),(67198,13639,2.00,0.00,4),(67199,13639,20.00,0.00,2),(67200,13639,1.00,0.00,5),(67201,13640,20.00,0.00,1),(67202,13640,2.50,10000000.00,3),(67203,13640,2.00,0.00,4),(67204,13640,20.00,0.00,2),(67205,13640,1.00,0.00,5),(67206,13642,5.00,0.00,1),(67207,13642,2.00,10000000.00,3),(67208,13642,2.00,0.00,4),(67209,13642,20.00,0.00,2),(67210,13642,1.00,0.00,5),(67211,13643,20.00,0.00,1),(67212,13643,2.50,10000000.00,3),(67213,13643,2.00,0.00,4),(67214,13643,20.00,0.00,2),(67215,13643,1.00,0.00,5),(67216,13644,20.00,0.00,1),(67217,13644,2.00,10000000.00,3),(67218,13644,2.00,0.00,4),(67219,13644,20.00,0.00,2),(67220,13644,1.00,0.00,5),(67221,13645,20.00,0.00,1),(67222,13645,2.50,10000000.00,3),(67223,13645,2.00,0.00,4),(67224,13645,20.00,0.00,2),(67225,13645,1.00,0.00,5),(67226,13646,20.00,0.00,1),(67227,13646,2.00,10000000.00,3),(67228,13646,2.00,0.00,4),(67229,13646,20.00,0.00,2),(67230,13646,1.00,0.00,5),(67231,13647,20.00,0.00,1),(67232,13647,2.50,10000000.00,3),(67233,13647,2.00,0.00,4),(67234,13647,20.00,0.00,2),(67235,13647,1.00,0.00,5),(67236,13648,20.00,0.00,1),(67237,13648,2.00,10000000.00,3),(67238,13648,2.00,0.00,4),(67239,13648,20.00,0.00,2),(67240,13648,1.00,0.00,5),(67241,13649,20.00,0.00,1),(67242,13649,2.50,10000000.00,3),(67243,13649,2.00,0.00,4),(67244,13649,20.00,0.00,2),(67245,13649,1.00,0.00,5),(67246,13650,20.00,0.00,1),(67247,13650,2.00,10000000.00,3),(67248,13650,2.00,0.00,4),(67249,13650,20.00,0.00,2),(67250,13650,1.00,0.00,5),(67251,13651,10.00,0.00,1),(67252,13651,2.00,10000000.00,3),(67253,13651,2.00,0.00,4),(67254,13651,20.00,0.00,2),(67255,13651,1.00,0.00,5),(67256,13652,10.00,0.00,1),(67257,13652,2.00,10000000.00,3),(67258,13652,2.00,0.00,4),(67259,13652,20.00,0.00,2),(67260,13652,1.00,0.00,5),(67261,13653,10.00,0.00,1),(67262,13653,2.50,0.00,3),(67263,13653,2.00,0.00,4),(67264,13653,20.00,0.00,2),(67265,13653,0.00,0.00,5),(67266,13654,10.00,0.00,1),(67267,13654,2.50,10000000.00,3),(67268,13654,2.00,0.00,4),(67269,13654,20.00,0.00,2),(67270,13654,1.00,0.00,5),(67271,13655,10.00,0.00,1),(67272,13655,2.50,10000000.00,3),(67273,13655,2.00,0.00,4),(67274,13655,20.00,0.00,2),(67275,13655,1.00,0.00,5),(67276,13656,20.00,0.00,1),(67277,13656,2.50,10000000.00,3),(67278,13656,2.00,0.00,4),(67279,13656,20.00,0.00,2),(67280,13656,1.00,0.00,5),(67281,13657,20.00,0.00,1),(67282,13657,2.50,10000000.00,3),(67283,13657,2.00,0.00,4),(67284,13657,20.00,0.00,2),(67285,13657,1.00,0.00,5),(67286,13658,20.00,0.00,1),(67287,13658,2.50,10000000.00,3),(67288,13658,2.00,0.00,4),(67289,13658,20.00,0.00,2),(67290,13658,1.00,0.00,5),(67291,13659,30.00,0.00,1),(67292,13659,2.50,10000000.00,3),(67293,13659,2.00,0.00,4),(67294,13659,20.00,0.00,2),(67295,13659,1.00,0.00,5),(67296,13660,30.00,0.00,1),(67297,13660,2.00,10000000.00,3),(67298,13660,2.00,0.00,4),(67299,13660,20.00,0.00,2),(67300,13660,1.00,0.00,5),(67301,13661,30.00,0.00,1),(67302,13661,2.50,10000000.00,3),(67303,13661,2.00,0.00,4),(67304,13661,20.00,0.00,2),(67305,13661,1.00,0.00,5),(67306,13662,10.00,0.00,1),(67307,13662,2.00,10000000.00,3),(67308,13662,2.00,0.00,4),(67309,13662,20.00,0.00,2),(67310,13662,1.00,0.00,5),(67311,13663,10.00,0.00,1),(67312,13663,2.50,10000000.00,3),(67313,13663,3.00,0.00,4),(67314,13663,20.00,0.00,2),(67315,13663,1.00,0.00,5),(67316,13664,10.00,0.00,1),(67317,13664,2.00,10000000.00,3),(67318,13664,2.00,0.00,4),(67319,13664,20.00,0.00,2),(67320,13664,1.00,0.00,5),(67321,13665,10.00,0.00,1),(67322,13665,2.50,10000000.00,3),(67323,13665,2.00,0.00,4),(67324,13665,20.00,0.00,2),(67325,13665,1.00,0.00,5),(67326,13666,50.00,0.00,1),(67327,13666,2.50,10000000.00,3),(67328,13666,2.00,0.00,4),(67329,13666,20.00,0.00,2),(67330,13666,1.00,0.00,5),(67331,13667,50.00,0.00,1),(67332,13667,2.00,10000000.00,3),(67333,13667,2.00,0.00,4),(67334,13667,20.00,0.00,2),(67335,13667,1.00,0.00,5),(67336,13668,50.00,0.00,1),(67337,13668,2.50,10000000.00,3),(67338,13668,2.00,0.00,4),(67339,13668,20.00,0.00,2),(67340,13668,1.00,0.00,5),(67341,13669,50.00,0.00,1),(67342,13669,2.00,10000000.00,3),(67343,13669,2.00,0.00,4),(67344,13669,20.00,0.00,2),(67345,13669,1.00,0.00,5),(67346,13670,30.00,0.00,1),(67347,13670,2.50,10000000.00,3),(67348,13670,2.00,0.00,4),(67349,13670,20.00,0.00,2),(67350,13670,1.00,0.00,5),(67351,13671,30.00,0.00,1),(67352,13671,2.00,10000000.00,3),(67353,13671,2.00,0.00,4),(67354,13671,20.00,0.00,2),(67355,13671,1.00,0.00,5),(67356,13672,30.00,0.00,1),(67357,13672,2.50,10000000.00,3),(67358,13672,2.00,0.00,4),(67359,13672,20.00,0.00,2),(67360,13672,1.00,0.00,5),(67361,13673,25.00,0.00,1),(67362,13673,2.00,10000000.00,3),(67363,13673,2.00,0.00,4),(67364,13673,20.00,0.00,2),(67365,13673,1.00,0.00,5),(67366,13674,20.00,0.00,1),(67367,13674,2.50,10000000.00,3),(67368,13674,2.00,0.00,4),(67369,13674,20.00,0.00,2),(67370,13674,1.00,0.00,5),(67371,13675,20.00,0.00,1),(67372,13675,2.00,10000000.00,3),(67373,13675,2.00,0.00,4),(67374,13675,20.00,0.00,2),(67375,13675,1.00,0.00,5),(67376,13676,20.00,0.00,1),(67377,13676,2.50,10000000.00,3),(67378,13676,2.00,0.00,4),(67379,13676,20.00,0.00,2),(67380,13676,1.00,0.00,5),(67381,13677,10.00,0.00,1),(67382,13677,2.50,10000000.00,3),(67383,13677,2.00,0.00,4),(67384,13677,20.00,0.00,2),(67385,13677,1.00,0.00,5),(67386,13678,10.00,0.00,1),(67387,13678,2.50,10000000.00,3),(67388,13678,2.00,0.00,4),(67389,13678,20.00,0.00,2),(67390,13678,1.00,0.00,5),(67391,13679,10.00,0.00,1),(67392,13679,2.50,10000000.00,3),(67393,13679,2.00,0.00,4),(67394,13679,20.00,0.00,2),(67395,13679,1.00,0.00,5),(67396,13680,10.00,0.00,1),(67397,13680,2.00,10000000.00,3),(67398,13680,2.00,0.00,4),(67399,13680,20.00,0.00,2),(67400,13680,1.00,0.00,5),(67401,13681,10.00,0.00,1),(67402,13681,2.50,10000000.00,3),(67403,13681,2.00,0.00,4),(67404,13681,20.00,0.00,2),(67405,13681,1.00,0.00,5),(67406,13682,15.00,0.00,1),(67407,13682,2.00,10000000.00,3),(67408,13682,2.00,0.00,4),(67409,13682,20.00,0.00,2),(67410,13682,1.00,0.00,5),(67411,13683,30.00,2500.00,1),(67412,13683,2.50,10000000.00,3),(67413,13683,2.00,0.00,4),(67414,13683,20.00,0.00,2),(67415,13683,1.00,0.00,5),(67416,13684,30.00,2500.00,1),(67417,13684,2.00,10000000.00,3),(67418,13684,2.00,0.00,4),(67419,13684,20.00,0.00,2),(67420,13684,1.00,0.00,5),(67421,13685,30.00,2500.00,1),(67422,13685,2.50,10000000.00,3),(67423,13685,2.00,0.00,4),(67424,13685,20.00,0.00,2),(67425,13685,1.00,0.00,5),(67426,13686,30.00,2500.00,1),(67427,13686,2.00,10000000.00,3),(67428,13686,2.00,0.00,4),(67429,13686,20.00,0.00,2),(67430,13686,1.00,0.00,5),(67431,13687,30.00,5000.00,1),(67432,13687,2.50,10000000.00,3),(67433,13687,2.00,0.00,4),(67434,13687,20.00,0.00,2),(67435,13687,1.00,0.00,5),(67436,13688,30.00,5000.00,1),(67437,13688,2.00,10000000.00,3),(67438,13688,2.00,0.00,4),(67439,13688,20.00,0.00,2),(67440,13688,1.00,0.00,5),(67441,13689,30.00,5000.00,1),(67442,13689,2.50,10000000.00,3),(67443,13689,2.00,0.00,4),(67444,13689,20.00,0.00,2),(67445,13689,1.00,0.00,5),(67446,13690,30.00,5000.00,1),(67447,13690,2.00,10000000.00,3),(67448,13690,2.00,0.00,4),(67449,13690,20.00,0.00,2),(67450,13690,1.00,0.00,5),(67451,13691,10.00,1000.00,1),(67452,13691,2.50,10000000.00,3),(67453,13691,2.00,0.00,4),(67454,13691,20.00,0.00,2),(67455,13691,1.00,0.00,5),(67456,13692,10.00,1000.00,1),(67457,13692,2.00,10000000.00,3),(67458,13692,2.00,0.00,4),(67459,13692,20.00,0.00,2),(67460,13692,1.00,0.00,5),(67461,13693,10.00,1000.00,1),(67462,13693,2.50,10000000.00,3),(67463,13693,2.00,0.00,4),(67464,13693,20.00,0.00,2),(67465,13693,1.00,0.00,5),(67466,13694,10.00,1000.00,1),(67467,13694,2.00,10000000.00,3),(67468,13694,2.00,0.00,4),(67469,13694,20.00,0.00,2),(67470,13694,1.00,0.00,5),(67471,13695,50.00,0.00,1),(67472,13695,2.50,10000000.00,3),(67473,13695,2.00,0.00,4),(67474,13695,20.00,0.00,2),(67475,13695,1.00,0.00,5),(67476,13696,50.00,0.00,1),(67477,13696,2.00,10000000.00,3),(67478,13696,2.00,0.00,4),(67479,13696,20.00,0.00,2),(67480,13696,1.00,0.00,5),(67481,13697,50.00,0.00,1),(67482,13697,2.50,10000000.00,3),(67483,13697,2.00,0.00,4),(67484,13697,20.00,0.00,2),(67485,13697,1.00,0.00,5),(67486,13698,50.00,0.00,1),(67487,13698,2.00,10000000.00,3),(67488,13698,2.00,0.00,4),(67489,13698,20.00,0.00,2),(67490,13698,1.00,0.00,5),(67491,13699,30.00,0.00,1),(67492,13699,2.50,10000000.00,3),(67493,13699,2.00,0.00,4),(67494,13699,20.00,0.00,2),(67495,13699,1.00,0.00,5),(67496,13700,30.00,0.00,1),(67497,13700,2.00,10000000.00,3),(67498,13700,2.00,0.00,4),(67499,13700,20.00,0.00,2),(67500,13700,1.00,0.00,5),(67501,13701,30.00,0.00,1),(67502,13701,2.50,10000000.00,3),(67503,13701,2.00,0.00,4),(67504,13701,20.00,0.00,2),(67505,13701,1.00,0.00,5),(67506,13702,25.00,0.00,1),(67507,13702,2.00,10000000.00,3),(67508,13702,2.00,0.00,4),(67509,13702,20.00,0.00,2),(67510,13702,1.00,0.00,5),(67511,13703,30.00,0.00,1),(67512,13703,2.50,10000000.00,3),(67513,13703,2.00,0.00,4),(67514,13703,20.00,0.00,2),(67515,13703,1.00,0.00,5),(67516,13704,30.00,0.00,1),(67517,13704,2.00,10000000.00,3),(67518,13704,2.00,0.00,4),(67519,13704,20.00,0.00,2),(67520,13704,1.00,0.00,5),(67521,13705,30.00,0.00,1),(67522,13705,2.50,10000000.00,3),(67523,13705,2.00,0.00,4),(67524,13705,20.00,0.00,2),(67525,13705,1.00,0.00,5),(67526,13706,10.00,0.00,1),(67527,13706,2.50,10000000.00,3),(67528,13706,2.00,0.00,4),(67529,13706,20.00,0.00,2),(67530,13706,1.00,0.00,5),(67531,13707,10.00,0.00,1),(67532,13707,2.00,10000000.00,3),(67533,13707,2.00,0.00,4),(67534,13707,20.00,0.00,2),(67535,13707,1.00,0.00,5),(67536,13708,10.00,0.00,1),(67537,13708,2.50,10000000.00,3),(67538,13708,2.00,0.00,4),(67539,13708,20.00,0.00,2),(67540,13708,1.00,0.00,5),(67541,13709,5.00,0.00,1),(67542,13709,2.00,10000000.00,3),(67543,13709,2.00,0.00,4),(67544,13709,20.00,0.00,2),(67545,13709,1.00,0.00,5),(67546,13710,10.00,0.00,1),(67547,13710,2.50,0.00,3),(67548,13710,2.00,0.00,4),(67549,13710,20.00,0.00,2),(67550,13710,0.00,0.00,5),(67551,13711,10.00,0.00,1),(67552,13711,2.50,10000000.00,3),(67553,13711,2.00,0.00,4),(67554,13711,20.00,0.00,2),(67555,13711,0.00,0.00,5),(67556,13712,10.00,0.00,1),(67557,13712,2.00,10000000.00,3),(67558,13712,2.00,0.00,4),(67559,13712,20.00,0.00,2),(67560,13712,1.00,0.00,5),(67561,13713,10.00,0.00,1),(67562,13713,2.50,10000000.00,3),(67563,13713,2.00,0.00,4),(67564,13713,20.00,0.00,2),(67565,13713,1.00,0.00,5),(67566,13714,20.00,0.00,1),(67567,13714,2.50,10000000.00,3),(67568,13714,2.00,0.00,4),(67569,13714,20.00,0.00,2),(67570,13714,1.00,0.00,5),(67571,13715,20.00,0.00,1),(67572,13715,2.00,10000000.00,3),(67573,13715,2.00,0.00,4),(67574,13715,20.00,0.00,2),(67575,13715,1.00,0.00,5),(67576,13716,30.00,0.00,1),(67577,13716,2.50,0.00,3),(67578,13716,2.00,0.00,4),(67579,13716,20.00,0.00,2),(67580,13716,0.00,0.00,5),(67581,13717,30.00,0.00,1),(67582,13717,2.50,10000000.00,3),(67583,13717,2.00,0.00,4),(67584,13717,20.00,0.00,2),(67585,13717,1.00,0.00,5),(67586,13718,30.00,0.00,1),(67587,13718,2.00,10000000.00,3),(67588,13718,2.00,0.00,4),(67589,13718,20.00,0.00,2),(67590,13718,1.00,0.00,5),(67591,13719,30.00,0.00,1),(67592,13719,2.50,10000000.00,3),(67593,13719,2.00,0.00,4),(67594,13719,20.00,0.00,2),(67595,13719,1.00,0.00,5),(67596,13720,20.00,0.00,1),(67597,13720,2.50,0.00,3),(67598,13720,2.00,0.00,4),(67599,13720,20.00,0.00,2),(67600,13720,0.00,0.00,5),(67601,13721,20.00,0.00,1),(67602,13721,2.50,10000000.00,3),(67603,13721,2.00,0.00,4),(67604,13721,20.00,0.00,2),(67605,13721,1.00,0.00,5),(67606,13722,20.00,0.00,1),(67607,13722,2.00,10000000.00,3),(67608,13722,2.00,0.00,4),(67609,13722,20.00,0.00,2),(67610,13722,1.00,0.00,5),(67611,13723,20.00,0.00,1),(67612,13723,2.50,10000000.00,3),(67613,13723,2.00,0.00,4),(67614,13723,20.00,0.00,2),(67615,13723,1.00,0.00,5),(84666,17135,20.00,5000.00,1),(84667,17135,10.00,0.00,3),(84668,17135,2.00,0.00,4),(84669,17135,20.00,0.00,2),(84670,17135,0.00,0.00,5),(84671,17136,20.00,5000.00,1),(84672,17136,10.00,10000000.00,3),(84673,17136,2.00,0.00,4),(84674,17136,20.00,0.00,2),(84675,17136,1.00,0.00,5),(84676,17137,10.00,5000.00,1),(84677,17137,0.00,0.00,3),(84678,17137,0.00,0.00,4),(84679,17137,20.00,0.00,2),(84680,17137,0.00,0.00,5),(84681,17138,10.00,20000.00,1),(84682,17138,0.00,0.00,3),(84683,17138,0.00,0.00,4),(84684,17138,20.00,0.00,2),(84685,17138,0.00,0.00,5),(84686,17139,10.00,10000.00,1),(84687,17139,10.00,10000000.00,3),(84688,17139,2.00,0.00,4),(84689,17139,20.00,0.00,2),(84690,17139,1.00,0.00,5),(84691,17140,10.00,10000.00,1),(84692,17140,0.00,0.00,3),(84693,17140,0.00,0.00,4),(84694,17140,20.00,0.00,2),(84695,17140,0.00,0.00,5),(84696,17141,20.00,2500.00,1),(84697,17141,10.00,0.00,3),(84698,17141,2.00,0.00,4),(84699,17141,20.00,0.00,2),(84700,17141,0.00,0.00,5),(84701,17142,20.00,2500.00,1),(84702,17142,10.00,10000000.00,3),(84703,17142,2.00,0.00,4),(84704,17142,20.00,0.00,2),(84705,17142,1.00,0.00,5),(84706,17143,10.00,2500.00,1),(84707,17143,0.00,0.00,3),(84708,17143,0.00,0.00,4),(84709,17143,20.00,0.00,2),(84710,17143,0.00,0.00,5),(84711,17144,10.00,5000.00,1),(84712,17144,0.00,0.00,3),(84713,17144,0.00,0.00,4),(84714,17144,20.00,0.00,2),(84715,17144,0.00,0.00,5),(84716,17145,20.00,5000.00,1),(84717,17145,10.00,0.00,3),(84718,17145,2.00,0.00,4),(84719,17145,20.00,0.00,2),(84720,17145,0.00,0.00,5),(84721,17146,20.00,5000.00,1),(84722,17146,10.00,10000000.00,3),(84723,17146,2.00,0.00,4),(84724,17146,20.00,0.00,2),(84725,17146,1.00,0.00,5),(84726,17147,10.00,5000.00,1),(84727,17147,0.00,0.00,3),(84728,17147,0.00,0.00,4),(84729,17147,20.00,0.00,2),(84730,17147,0.00,0.00,5),(84731,17148,10.00,100000.00,1),(84732,17148,10.00,0.00,3),(84733,17148,2.00,0.00,4),(84734,17148,20.00,0.00,2),(84735,17148,0.00,0.00,5),(84736,17149,10.00,100000.00,1),(84737,17149,10.00,1000000.00,3),(84738,17149,2.00,0.00,4),(84739,17149,20.00,0.00,2),(84740,17149,1.00,0.00,5),(84741,17150,10.00,100000.00,1),(84742,17150,0.00,0.00,3),(84743,17150,0.00,0.00,4),(84744,17150,20.00,0.00,2),(84745,17150,0.00,0.00,5),(84746,17151,10.00,200000.00,1),(84747,17151,0.00,0.00,3),(84748,17151,0.00,0.00,4),(84749,17151,20.00,0.00,2),(84750,17151,0.00,0.00,5),(84751,17152,1.00,5000000.00,1),(84752,17152,0.00,0.00,3),(84753,17152,0.00,0.00,4),(84754,17152,20.00,0.00,2),(84755,17152,0.00,0.00,5),(84756,17153,20.00,0.00,1),(84757,17153,10.00,1000000.00,3),(84758,17153,2.00,0.00,4),(84759,17153,20.00,0.00,2),(84760,17153,1.00,0.00,5),(84761,17154,20.00,0.00,1),(84762,17154,0.00,0.00,3),(84763,17154,0.00,0.00,4),(84764,17154,20.00,0.00,2),(84765,17154,0.00,0.00,5),(84766,17155,20.00,0.00,1),(84767,17155,10.00,0.00,3),(84768,17155,2.00,0.00,4),(84769,17155,20.00,0.00,2),(84770,17155,0.00,0.00,5),(84771,17156,20.00,2500.00,1),(84772,17156,10.00,10000000.00,3),(84773,17156,2.00,0.00,4),(84774,17156,20.00,0.00,2),(84775,17156,1.00,0.00,5),(84776,17157,10.00,0.00,1),(84777,17157,0.00,0.00,3),(84778,17157,0.00,0.00,4),(84779,17157,20.00,0.00,2),(84780,17157,0.00,0.00,5),(84781,17158,2.00,20000.00,1),(84782,17158,10.00,0.00,3),(84783,17158,2.00,0.00,4),(84784,17158,20.00,0.00,2),(84785,17158,0.00,0.00,5),(84786,17159,2.00,50000.00,1),(84787,17159,10.00,10000000.00,3),(84788,17159,2.00,0.00,4),(84789,17159,20.00,0.00,2),(84790,17159,1.00,0.00,5),(84791,17160,2.00,50000.00,1),(84792,17160,0.00,0.00,3),(84793,17160,0.00,0.00,4),(84794,17160,20.00,0.00,2),(84795,17160,0.00,0.00,5),(84796,17161,2.00,50000.00,1),(84797,17161,0.00,0.00,3),(84798,17161,0.00,0.00,4),(84799,17161,20.00,0.00,2),(84800,17161,0.00,0.00,5),(84801,17162,2.00,75000.00,1),(84802,17162,0.00,0.00,3),(84803,17162,0.00,0.00,4),(84804,17162,20.00,0.00,2),(84805,17162,0.00,0.00,5),(84806,17163,10.00,30000.00,1),(84807,17163,0.00,0.00,3),(84808,17163,0.00,0.00,4),(84809,17163,20.00,0.00,2),(84810,17163,0.00,0.00,5),(84811,17164,20.00,120000.00,1),(84812,17164,10.00,0.00,3),(84813,17164,2.00,0.00,4),(84814,17164,20.00,0.00,2),(84815,17164,0.00,0.00,5),(84816,17165,20.00,120000.00,1),(84817,17165,10.00,10000000.00,3),(84818,17165,2.00,0.00,4),(84819,17165,20.00,0.00,2),(84820,17165,1.00,0.00,5),(84821,17166,20.00,120000.00,1),(84822,17166,0.00,0.00,3),(84823,17166,0.00,0.00,4),(84824,17166,20.00,0.00,2),(84825,17166,0.00,0.00,5),(84826,17167,10.00,180000.00,1),(84827,17167,0.00,0.00,3),(84828,17167,0.00,0.00,4),(84829,17167,20.00,0.00,2),(84830,17167,0.00,0.00,5),(84831,17168,10.00,120000.00,1),(84832,17168,0.00,0.00,3),(84833,17168,0.00,0.00,4),(84834,17168,20.00,0.00,2),(84835,17168,0.00,0.00,5),(84836,17169,10.00,120000.00,1),(84837,17169,10.00,10000000.00,3),(84838,17169,2.00,0.00,4),(84839,17169,20.00,0.00,2),(84840,17169,1.00,0.00,5),(84841,17170,10.00,120000.00,1),(84842,17170,0.00,0.00,3),(84843,17170,0.00,0.00,4),(84844,17170,20.00,0.00,2),(84845,17170,0.00,0.00,5),(84846,17171,2.00,120000.00,1),(84847,17171,0.00,0.00,3),(84848,17171,0.00,0.00,4),(84849,17171,20.00,0.00,2),(84850,17171,0.00,0.00,5),(84851,17172,2.00,180000.00,1),(84852,17172,0.00,0.00,3),(84853,17172,0.00,0.00,4),(84854,17172,20.00,0.00,2),(84855,17172,0.00,0.00,5),(84856,17173,30.00,2500.00,1),(84857,17173,10.00,0.00,3),(84858,17173,2.00,0.00,4),(84859,17173,20.00,0.00,2),(84860,17173,0.00,0.00,5),(84861,17174,30.00,2500.00,1),(84862,17174,10.00,10000000.00,3),(84863,17174,2.00,0.00,4),(84864,17174,20.00,0.00,2),(84865,17174,1.00,0.00,5),(84866,17175,30.00,2500.00,1),(84867,17175,0.00,0.00,3),(84868,17175,0.00,0.00,4),(84869,17175,20.00,0.00,2),(84870,17175,0.00,0.00,5),(84871,17176,30.00,5000.00,1),(84872,17176,0.00,0.00,3),(84873,17176,0.00,0.00,4),(84874,17176,20.00,0.00,2),(84875,17176,0.00,0.00,5),(84876,17177,30.00,5000.00,1),(84877,17177,10.00,0.00,3),(84878,17177,2.00,0.00,4),(84879,17177,20.00,0.00,2),(84880,17177,1.00,0.00,5),(84881,17178,30.00,5000.00,1),(84882,17178,10.00,10000000.00,3),(84883,17178,2.00,0.00,4),(84884,17178,20.00,0.00,2),(84885,17178,1.00,0.00,5),(84886,17179,30.00,5000.00,1),(84887,17179,0.00,0.00,3),(84888,17179,0.00,0.00,4),(84889,17179,20.00,0.00,2),(84890,17179,0.00,0.00,5),(84891,17180,30.00,10000.00,1),(84892,17180,0.00,0.00,3),(84893,17180,0.00,0.00,4),(84894,17180,20.00,0.00,2),(84895,17180,0.00,0.00,5),(84896,17181,20.00,2500.00,1),(84897,17181,10.00,0.00,3),(84898,17181,2.00,0.00,4),(84899,17181,20.00,0.00,2),(84900,17181,0.00,0.00,5),(84901,17182,20.00,2500.00,1),(84902,17182,10.00,10000000.00,3),(84903,17182,2.00,0.00,4),(84904,17182,20.00,0.00,2),(84905,17182,1.00,0.00,5),(84906,17183,20.00,0.00,1),(84907,17183,10.00,10000000.00,3),(84908,17183,2.00,0.00,4),(84909,17183,20.00,0.00,2),(84910,17183,1.00,0.00,5),(84911,17184,10.00,0.00,1),(84912,17184,0.00,0.00,3),(84913,17184,0.00,0.00,4),(84914,17184,20.00,0.00,2),(84915,17184,0.00,0.00,5),(84916,17185,10.00,0.00,1),(84917,17185,0.00,0.00,3),(84918,17185,0.00,0.00,4),(84919,17185,20.00,0.00,2),(84920,17185,0.00,0.00,5),(84921,17186,10.00,1000.00,1),(84922,17186,10.00,0.00,3),(84923,17186,2.00,0.00,4),(84924,17186,20.00,0.00,2),(84925,17186,0.00,0.00,5),(84926,17187,10.00,1000.00,1),(84927,17187,10.00,10000000.00,3),(84928,17187,2.00,0.00,4),(84929,17187,20.00,0.00,2),(84930,17187,1.00,0.00,5),(84931,17188,10.00,1000.00,1),(84932,17188,0.00,0.00,3),(84933,17188,0.00,0.00,4),(84934,17188,20.00,0.00,2),(84935,17188,0.00,0.00,5),(84936,17189,5.00,2500.00,1),(84937,17189,10.00,0.00,3),(84938,17189,2.00,0.00,4),(84939,17189,20.00,0.00,2),(84940,17189,0.00,0.00,5),(84941,17190,5.00,2500.00,1),(84942,17190,10.00,10000000.00,3),(84943,17190,2.00,0.00,4),(84944,17190,20.00,0.00,2),(84945,17190,1.00,0.00,5),(84946,17191,10.00,2500.00,1),(84947,17191,10.00,10000000.00,3),(84948,17191,2.00,0.00,4),(84949,17191,20.00,0.00,2),(84950,17191,1.00,0.00,5),(84951,17192,10.00,2500.00,1),(84952,17192,0.00,0.00,3),(84953,17192,0.00,0.00,4),(84954,17192,20.00,0.00,2),(84955,17192,0.00,0.00,5),(84956,17193,10.00,5000.00,1),(84957,17193,0.00,0.00,3),(84958,17193,0.00,0.00,4),(84959,17193,20.00,0.00,2),(84960,17193,0.00,0.00,5),(84961,17194,20.00,2500.00,1),(84962,17194,10.00,10000000.00,3),(84963,17194,2.00,0.00,4),(84964,17194,20.00,0.00,2),(84965,17194,1.00,0.00,5),(84966,17195,10.00,2500.00,1),(84967,17195,0.00,0.00,3),(84968,17195,0.00,0.00,4),(84969,17195,20.00,0.00,2),(84970,17195,0.00,0.00,5),(84971,17196,5.00,20000.00,1),(84972,17196,10.00,0.00,3),(84973,17196,2.00,0.00,4),(84974,17196,20.00,0.00,2),(84975,17196,0.00,0.00,5),(84976,17197,5.00,20000.00,1),(84977,17197,10.00,10000000.00,3),(84978,17197,2.00,0.00,4),(84979,17197,20.00,0.00,2),(84980,17197,1.00,0.00,5),(84981,17198,10.00,20000.00,1),(84982,17198,10.00,10000000.00,3),(84983,17198,2.00,0.00,4),(84984,17198,20.00,0.00,2),(84985,17198,1.00,0.00,5),(84986,17199,10.00,20000.00,1),(84987,17199,0.00,0.00,3),(84988,17199,0.00,0.00,4),(84989,17199,20.00,0.00,2),(84990,17199,0.00,0.00,5),(84991,17200,10.00,30000.00,1),(84992,17200,0.00,0.00,3),(84993,17200,0.00,0.00,4),(84994,17200,20.00,0.00,2),(84995,17200,0.00,0.00,5),(84996,17201,1.00,20000.00,1),(84997,17201,10.00,0.00,3),(84998,17201,2.00,0.00,4),(84999,17201,20.00,0.00,2),(85000,17201,0.00,0.00,5),(85001,17202,1.00,20000.00,1),(85002,17202,10.00,10000000.00,3),(85003,17202,2.00,0.00,4),(85004,17202,20.00,0.00,2),(85005,17202,1.00,0.00,5),(85006,17203,1.00,20000.00,1),(85007,17203,0.00,0.00,3),(85008,17203,0.00,0.00,4),(85009,17203,20.00,0.00,2),(85010,17203,0.00,0.00,5),(85011,17204,2.00,20000.00,1),(85012,17204,0.00,0.00,3),(85013,17204,0.00,0.00,4),(85014,17204,20.00,0.00,2),(85015,17204,0.00,0.00,5),(85016,17205,2.00,75000.00,1),(85017,17205,0.00,0.00,3),(85018,17205,0.00,0.00,4),(85019,17205,20.00,0.00,2),(85020,17205,0.00,0.00,5),(85021,17206,1.00,20000.00,1),(85022,17206,10.00,0.00,3),(85023,17206,2.00,0.00,4),(85024,17206,20.00,0.00,2),(85025,17206,0.00,0.00,5),(85026,17207,1.00,50000.00,1),(85027,17207,10.00,10000000.00,3),(85028,17207,2.00,0.00,4),(85029,17207,20.00,0.00,2),(85030,17207,1.00,0.00,5),(85031,17208,1.00,50000.00,1),(85032,17208,0.00,0.00,3),(85033,17208,0.00,0.00,4),(85034,17208,20.00,0.00,2),(85035,17208,0.00,0.00,5),(85036,17209,2.00,50000.00,1),(85037,17209,0.00,0.00,3),(85038,17209,0.00,0.00,4),(85039,17209,20.00,0.00,2),(85040,17209,0.00,0.00,5),(85041,17210,2.00,75000.00,1),(85042,17210,0.00,0.00,3),(85043,17210,0.00,0.00,4),(85044,17210,20.00,0.00,2),(85045,17210,0.00,0.00,5),(85046,17211,2.00,50000.00,1),(85047,17211,0.00,0.00,3),(85048,17211,0.00,0.00,4),(85049,17211,20.00,0.00,2),(85050,17211,0.00,0.00,5),(85051,17212,0.00,0.00,1),(85052,17212,0.00,0.00,3),(85053,17212,0.00,0.00,4),(85054,17212,20.00,0.00,2),(85055,17212,0.00,0.00,5),(85056,17213,20.00,75000.00,1),(85057,17213,0.00,0.00,3),(85058,17213,0.00,0.00,4),(85059,17213,20.00,0.00,2),(85060,17213,0.00,0.00,5),(85061,17214,1.00,50000.00,1),(85062,17214,0.00,0.00,3),(85063,17214,0.00,0.00,4),(85064,17214,20.00,0.00,2),(85065,17214,0.00,0.00,5),(85066,17215,0.00,0.00,1),(85067,17215,0.00,0.00,3),(85068,17215,0.00,0.00,4),(85069,17215,20.00,0.00,2),(85070,17215,0.00,0.00,5),(85071,17216,20.00,75000.00,1),(85072,17216,0.00,0.00,3),(85073,17216,0.00,0.00,4),(85074,17216,20.00,0.00,2),(85075,17216,0.00,0.00,5),(109826,22167,10.00,5000.00,1),(109827,22167,2.50,0.00,3),(109828,22167,2.00,0.00,4),(109829,22167,20.00,0.00,2),(109830,22167,0.00,0.00,5),(109831,22168,10.00,5000.00,1),(109832,22168,0.00,0.00,3),(109833,22168,2.00,0.00,4),(109834,22168,20.00,0.00,2),(109835,22168,0.00,0.00,5),(109836,22169,10.00,5000.00,1),(109837,22169,0.00,0.00,3),(109838,22169,2.00,0.00,4),(109839,22169,20.00,0.00,2),(109840,22169,1.00,0.00,5),(109841,22170,10.00,5000.00,1),(109842,22170,0.00,0.00,3),(109843,22170,0.00,0.00,4),(109844,22170,20.00,0.00,2),(109845,22170,0.00,0.00,5),(109846,22171,10.00,20000.00,1),(109847,22171,0.00,0.00,3),(109848,22171,0.00,0.00,4),(109849,22171,20.00,0.00,2),(109850,22171,0.00,0.00,5),(109851,22172,10.00,10000.00,1),(109852,22172,0.00,0.00,3),(109853,22172,2.00,0.00,4),(109854,22172,20.00,0.00,2),(109855,22172,1.00,0.00,5),(109856,22173,10.00,10000.00,1),(109857,22173,0.00,0.00,3),(109858,22173,0.00,0.00,4),(109859,22173,20.00,0.00,2),(109860,22173,0.00,0.00,5),(109861,22174,10.00,2500.00,1),(109862,22174,2.50,0.00,3),(109863,22174,2.00,0.00,4),(109864,22174,20.00,0.00,2),(109865,22174,0.00,0.00,5),(109866,22175,10.00,2500.00,1),(109867,22175,0.00,0.00,3),(109868,22175,2.00,0.00,4),(109869,22175,20.00,0.00,2),(109870,22175,0.00,0.00,5),(109871,22176,10.00,2500.00,1),(109872,22176,0.00,0.00,3),(109873,22176,2.00,0.00,4),(109874,22176,20.00,0.00,2),(109875,22176,1.00,0.00,5),(109876,22177,10.00,2500.00,1),(109877,22177,0.00,0.00,3),(109878,22177,0.00,0.00,4),(109879,22177,20.00,0.00,2),(109880,22177,0.00,0.00,5),(109881,22178,10.00,5000.00,1),(109882,22178,0.00,0.00,3),(109883,22178,0.00,0.00,4),(109884,22178,20.00,0.00,2),(109885,22178,0.00,0.00,5),(109886,22179,10.00,5000.00,1),(109887,22179,2.50,0.00,3),(109888,22179,2.00,0.00,4),(109889,22179,20.00,0.00,2),(109890,22179,0.00,0.00,5),(109891,22180,10.00,5000.00,1),(109892,22180,0.00,0.00,3),(109893,22180,2.00,0.00,4),(109894,22180,20.00,0.00,2),(109895,22180,0.00,0.00,5),(109896,22181,10.00,5000.00,1),(109897,22181,0.00,0.00,3),(109898,22181,2.00,0.00,4),(109899,22181,20.00,0.00,2),(109900,22181,1.00,0.00,5),(109901,22182,10.00,5000.00,1),(109902,22182,0.00,0.00,3),(109903,22182,0.00,0.00,4),(109904,22182,20.00,0.00,2),(109905,22182,0.00,0.00,5),(109906,22183,10.00,100000.00,1),(109907,22183,2.50,0.00,3),(109908,22183,2.00,0.00,4),(109909,22183,20.00,0.00,2),(109910,22183,0.00,0.00,5),(109911,22184,10.00,100000.00,1),(109912,22184,0.00,0.00,3),(109913,22184,2.00,0.00,4),(109914,22184,20.00,0.00,2),(109915,22184,0.00,0.00,5),(109916,22185,10.00,100000.00,1),(109917,22185,0.00,0.00,3),(109918,22185,2.00,0.00,4),(109919,22185,20.00,0.00,2),(109920,22185,1.00,0.00,5),(109921,22186,10.00,100000.00,1),(109922,22186,0.00,0.00,3),(109923,22186,0.00,0.00,4),(109924,22186,20.00,0.00,2),(109925,22186,0.00,0.00,5),(109926,22187,10.00,200000.00,1),(109927,22187,0.00,0.00,3),(109928,22187,0.00,0.00,4),(109929,22187,20.00,0.00,2),(109930,22187,0.00,0.00,5),(109931,22188,1.00,5000000.00,1),(109932,22188,0.00,0.00,3),(109933,22188,0.00,0.00,4),(109934,22188,20.00,0.00,2),(109935,22188,0.00,0.00,5),(109936,22189,20.00,2500.00,1),(109937,22189,2.50,0.00,3),(109938,22189,2.00,0.00,4),(109939,22189,20.00,0.00,2),(109940,22189,0.00,0.00,5),(109941,22190,20.00,2500.00,1),(109942,22190,0.00,0.00,3),(109943,22190,2.00,0.00,4),(109944,22190,20.00,0.00,2),(109945,22190,0.00,0.00,5),(109946,22191,20.00,0.00,1),(109947,22191,0.00,0.00,3),(109948,22191,2.00,0.00,4),(109949,22191,20.00,0.00,2),(109950,22191,1.00,0.00,5),(109951,22192,20.00,0.00,1),(109952,22192,0.00,0.00,3),(109953,22192,0.00,0.00,4),(109954,22192,20.00,0.00,2),(109955,22192,0.00,0.00,5),(109956,22193,20.00,0.00,1),(109957,22193,2.50,0.00,3),(109958,22193,2.00,0.00,4),(109959,22193,20.00,0.00,2),(109960,22193,0.00,0.00,5),(109961,22194,20.00,0.00,1),(109962,22194,0.00,0.00,3),(109963,22194,2.00,0.00,4),(109964,22194,20.00,0.00,2),(109965,22194,0.00,0.00,5),(109966,22195,20.00,0.00,1),(109967,22195,0.00,0.00,3),(109968,22195,2.00,0.00,4),(109969,22195,20.00,0.00,2),(109970,22195,1.00,0.00,5),(109971,22196,20.00,0.00,1),(109972,22196,0.00,0.00,3),(109973,22196,0.00,0.00,4),(109974,22196,20.00,0.00,2),(109975,22196,0.00,0.00,5),(109976,22197,2.20,20.00,1),(109977,22197,2.50,0.00,3),(109978,22197,2.00,0.00,4),(109979,22197,0.00,0.00,2),(109980,22197,0.00,0.00,5),(109981,22198,2.00,20000.00,1),(109982,22198,0.00,0.00,3),(109983,22198,2.00,0.00,4),(109984,22198,20.00,0.00,2),(109985,22198,0.00,0.00,5),(109986,22199,2.00,50000.00,1),(109987,22199,0.00,0.00,3),(109988,22199,2.00,0.00,4),(109989,22199,20.00,0.00,2),(109990,22199,1.00,0.00,5),(109991,22200,2.00,50000.00,1),(109992,22200,0.00,0.00,3),(109993,22200,0.00,0.00,4),(109994,22200,20.00,0.00,2),(109995,22200,0.00,0.00,5),(109996,22201,2.00,50000.00,1),(109997,22201,0.00,0.00,3),(109998,22201,0.00,0.00,4),(109999,22201,20.00,0.00,2),(110000,22201,0.00,0.00,5),(110001,22202,2.00,75000.00,1),(110002,22202,0.00,0.00,3),(110003,22202,0.00,0.00,4),(110004,22202,20.00,0.00,2),(110005,22202,0.00,0.00,5),(110006,22203,10.00,30000.00,1),(110007,22203,0.00,0.00,3),(110008,22203,0.00,0.00,4),(110009,22203,20.00,0.00,2),(110010,22203,0.00,0.00,5),(110011,22204,20.00,120000.00,1),(110012,22204,2.50,0.00,3),(110013,22204,2.00,0.00,4),(110014,22204,20.00,0.00,2),(110015,22204,0.00,0.00,5),(110016,22205,20.00,120000.00,1),(110017,22205,0.00,0.00,3),(110018,22205,2.00,0.00,4),(110019,22205,20.00,0.00,2),(110020,22205,0.00,0.00,5),(110021,22206,20.00,120000.00,1),(110022,22206,0.00,0.00,3),(110023,22206,2.00,0.00,4),(110024,22206,20.00,0.00,2),(110025,22206,1.00,0.00,5),(110026,22207,20.00,120000.00,1),(110027,22207,0.00,0.00,3),(110028,22207,0.00,0.00,4),(110029,22207,20.00,0.00,2),(110030,22207,0.00,0.00,5),(110031,22208,10.00,120000.00,1),(110032,22208,0.00,0.00,3),(110033,22208,0.00,0.00,4),(110034,22208,20.00,0.00,2),(110035,22208,0.00,0.00,5),(110036,22209,10.00,180000.00,1),(110037,22209,0.00,0.00,3),(110038,22209,0.00,0.00,4),(110039,22209,20.00,0.00,2),(110040,22209,0.00,0.00,5),(110041,22210,10.00,120000.00,1),(110042,22210,0.00,0.00,3),(110043,22210,2.00,0.00,4),(110044,22210,20.00,0.00,2),(110045,22210,1.00,0.00,5),(110046,22211,10.00,120000.00,1),(110047,22211,0.00,0.00,3),(110048,22211,0.00,0.00,4),(110049,22211,20.00,0.00,2),(110050,22211,0.00,0.00,5),(110051,22212,2.00,120000.00,1),(110052,22212,0.00,0.00,3),(110053,22212,0.00,0.00,4),(110054,22212,20.00,0.00,2),(110055,22212,0.00,0.00,5),(110056,22213,2.00,180000.00,1),(110057,22213,0.00,0.00,3),(110058,22213,0.00,0.00,4),(110059,22213,20.00,0.00,2),(110060,22213,0.00,0.00,5),(110061,22214,30.00,2500.00,1),(110062,22214,2.50,0.00,3),(110063,22214,2.00,0.00,4),(110064,22214,20.00,0.00,2),(110065,22214,0.00,0.00,5),(110066,22215,30.00,2500.00,1),(110067,22215,0.00,0.00,3),(110068,22215,2.00,0.00,4),(110069,22215,20.00,0.00,2),(110070,22215,0.00,0.00,5),(110071,22216,30.00,2500.00,1),(110072,22216,0.00,0.00,3),(110073,22216,2.00,0.00,4),(110074,22216,20.00,0.00,2),(110075,22216,1.00,0.00,5),(110076,22217,30.00,2500.00,1),(110077,22217,0.00,0.00,3),(110078,22217,0.00,0.00,4),(110079,22217,20.00,0.00,2),(110080,22217,0.00,0.00,5),(110081,22218,30.00,5000.00,1),(110082,22218,0.00,0.00,3),(110083,22218,0.00,0.00,4),(110084,22218,20.00,0.00,2),(110085,22218,0.00,0.00,5),(110086,22219,30.00,5000.00,1),(110087,22219,2.50,0.00,3),(110088,22219,2.00,0.00,4),(110089,22219,20.00,0.00,2),(110090,22219,0.00,0.00,5),(110091,22220,30.00,5000.00,1),(110092,22220,0.00,0.00,3),(110093,22220,2.00,0.00,4),(110094,22220,20.00,0.00,2),(110095,22220,0.00,0.00,5),(110096,22221,30.00,5000.00,1),(110097,22221,0.00,0.00,3),(110098,22221,2.00,0.00,4),(110099,22221,20.00,0.00,2),(110100,22221,1.00,0.00,5),(110101,22222,30.00,5000.00,1),(110102,22222,0.00,0.00,3),(110103,22222,0.00,0.00,4),(110104,22222,20.00,0.00,2),(110105,22222,0.00,0.00,5),(110106,22223,30.00,10000.00,1),(110107,22223,0.00,0.00,3),(110108,22223,0.00,0.00,4),(110109,22223,20.00,0.00,2),(110110,22223,0.00,0.00,5),(110111,22224,20.00,2500.00,1),(110112,22224,2.50,0.00,3),(110113,22224,2.00,0.00,4),(110114,22224,20.00,0.00,2),(110115,22224,0.00,0.00,5),(110116,22225,20.00,2500.00,1),(110117,22225,0.00,0.00,3),(110118,22225,2.00,0.00,4),(110119,22225,20.00,0.00,2),(110120,22225,0.00,0.00,5),(110121,22226,20.00,2500.00,1),(110122,22226,0.00,0.00,3),(110123,22226,2.00,0.00,4),(110124,22226,20.00,0.00,2),(110125,22226,1.00,0.00,5),(110126,22227,20.00,0.00,1),(110127,22227,0.00,0.00,3),(110128,22227,2.00,0.00,4),(110129,22227,20.00,0.00,2),(110130,22227,1.00,0.00,5),(110131,22228,10.00,0.00,1),(110132,22228,0.00,0.00,3),(110133,22228,0.00,0.00,4),(110134,22228,20.00,0.00,2),(110135,22228,0.00,0.00,5),(110136,22229,10.00,0.00,1),(110137,22229,0.00,0.00,3),(110138,22229,0.00,0.00,4),(110139,22229,20.00,0.00,2),(110140,22229,0.00,0.00,5),(110141,22230,10.00,1000.00,1),(110142,22230,2.50,0.00,3),(110143,22230,2.00,0.00,4),(110144,22230,20.00,0.00,2),(110145,22230,0.00,0.00,5),(110146,22231,10.00,1000.00,1),(110147,22231,0.00,0.00,3),(110148,22231,2.00,0.00,4),(110149,22231,20.00,0.00,2),(110150,22231,0.00,0.00,5),(110151,22232,10.00,1000.00,1),(110152,22232,0.00,0.00,3),(110153,22232,2.00,0.00,4),(110154,22232,20.00,0.00,2),(110155,22232,1.00,0.00,5),(110156,22233,10.00,1000.00,1),(110157,22233,0.00,0.00,3),(110158,22233,0.00,0.00,4),(110159,22233,20.00,0.00,2),(110160,22233,0.00,0.00,5),(110161,22234,5.00,2500.00,1),(110162,22234,2.50,0.00,3),(110163,22234,2.00,0.00,4),(110164,22234,20.00,0.00,2),(110165,22234,0.00,0.00,5),(110166,22235,5.00,2500.00,1),(110167,22235,0.00,0.00,3),(110168,22235,2.00,0.00,4),(110169,22235,20.00,0.00,2),(110170,22235,0.00,0.00,5),(110171,22236,5.00,2500.00,1),(110172,22236,0.00,0.00,3),(110173,22236,2.00,0.00,4),(110174,22236,20.00,0.00,2),(110175,22236,1.00,0.00,5),(110176,22237,10.00,2500.00,1),(110177,22237,0.00,0.00,3),(110178,22237,2.00,0.00,4),(110179,22237,20.00,0.00,2),(110180,22237,1.00,0.00,5),(110181,22238,10.00,2500.00,1),(110182,22238,0.00,0.00,3),(110183,22238,0.00,0.00,4),(110184,22238,20.00,0.00,2),(110185,22238,0.00,0.00,5),(110186,22239,10.00,5000.00,1),(110187,22239,0.00,0.00,3),(110188,22239,0.00,0.00,4),(110189,22239,20.00,0.00,2),(110190,22239,0.00,0.00,5),(110191,22240,5.00,20000.00,1),(110192,22240,2.50,0.00,3),(110193,22240,2.00,0.00,4),(110194,22240,20.00,0.00,2),(110195,22240,1.00,0.00,5),(110196,22241,10.00,20000.00,1),(110197,22241,0.00,0.00,3),(110198,22241,0.00,0.00,4),(110199,22241,20.00,0.00,2),(110200,22241,0.00,0.00,5),(110201,22242,5.00,20000.00,1),(110202,22242,2.50,0.00,3),(110203,22242,2.00,0.00,4),(110204,22242,20.00,0.00,2),(110205,22242,0.00,0.00,5),(110206,22243,5.00,20000.00,1),(110207,22243,0.00,0.00,3),(110208,22243,2.00,0.00,4),(110209,22243,20.00,0.00,2),(110210,22243,0.00,0.00,5),(110211,22244,5.00,20000.00,1),(110212,22244,0.00,0.00,3),(110213,22244,2.00,0.00,4),(110214,22244,20.00,0.00,2),(110215,22244,1.00,0.00,5),(110216,22245,10.00,20000.00,1),(110217,22245,0.00,0.00,3),(110218,22245,2.00,0.00,4),(110219,22245,20.00,0.00,2),(110220,22245,1.00,0.00,5),(110221,22246,10.00,20000.00,1),(110222,22246,0.00,0.00,3),(110223,22246,0.00,0.00,4),(110224,22246,20.00,0.00,2),(110225,22246,0.00,0.00,5),(110226,22247,10.00,30000.00,1),(110227,22247,0.00,0.00,3),(110228,22247,0.00,0.00,4),(110229,22247,20.00,0.00,2),(110230,22247,0.00,0.00,5),(110231,22248,1.00,20000.00,1),(110232,22248,2.50,0.00,3),(110233,22248,2.00,0.00,4),(110234,22248,20.00,0.00,2),(110235,22248,0.00,0.00,5),(110236,22249,1.00,20000.00,1),(110237,22249,0.00,0.00,3),(110238,22249,2.00,0.00,4),(110239,22249,20.00,0.00,2),(110240,22249,0.00,0.00,5),(110241,22250,1.00,20000.00,1),(110242,22250,0.00,0.00,3),(110243,22250,2.00,0.00,4),(110244,22250,20.00,0.00,2),(110245,22250,1.00,0.00,5),(110246,22251,1.00,20000.00,1),(110247,22251,0.00,0.00,3),(110248,22251,0.00,0.00,4),(110249,22251,20.00,0.00,2),(110250,22251,0.00,0.00,5),(110251,22252,2.00,20000.00,1),(110252,22252,0.00,0.00,3),(110253,22252,0.00,0.00,4),(110254,22252,20.00,0.00,2),(110255,22252,0.00,0.00,5),(110256,22253,2.00,75000.00,1),(110257,22253,0.00,0.00,3),(110258,22253,0.00,0.00,4),(110259,22253,20.00,0.00,2),(110260,22253,0.00,0.00,5),(110261,22254,1.00,20000.00,1),(110262,22254,2.50,0.00,3),(110263,22254,2.00,0.00,4),(110264,22254,20.00,0.00,2),(110265,22254,0.00,0.00,5),(110266,22255,1.00,20000.00,1),(110267,22255,0.00,0.00,3),(110268,22255,2.00,0.00,4),(110269,22255,20.00,0.00,2),(110270,22255,0.00,0.00,5),(110271,22256,1.00,50000.00,1),(110272,22256,0.00,0.00,3),(110273,22256,2.00,0.00,4),(110274,22256,20.00,0.00,2),(110275,22256,1.00,0.00,5),(110276,22257,1.00,50000.00,1),(110277,22257,0.00,0.00,3),(110278,22257,0.00,0.00,4),(110279,22257,20.00,0.00,2),(110280,22257,0.00,0.00,5),(110281,22258,2.00,50000.00,1),(110282,22258,0.00,0.00,3),(110283,22258,0.00,0.00,4),(110284,22258,20.00,0.00,2),(110285,22258,0.00,0.00,5),(110286,22259,2.00,75000.00,1),(110287,22259,0.00,0.00,3),(110288,22259,0.00,0.00,4),(110289,22259,20.00,0.00,2),(110290,22259,0.00,0.00,5),(110291,22260,2.00,50000.00,1),(110292,22260,0.00,0.00,3),(110293,22260,0.00,0.00,4),(110294,22260,20.00,0.00,2),(110295,22260,0.00,0.00,5),(110296,22261,0.00,0.00,1),(110297,22261,0.00,0.00,3),(110298,22261,0.00,0.00,4),(110299,22261,20.00,0.00,2),(110300,22261,0.00,0.00,5),(110301,22262,20.00,75000.00,1),(110302,22262,0.00,0.00,3),(110303,22262,0.00,0.00,4),(110304,22262,20.00,0.00,2),(110305,22262,0.00,0.00,5),(110306,22263,1.00,50000.00,1),(110307,22263,0.00,0.00,3),(110308,22263,0.00,0.00,4),(110309,22263,20.00,0.00,2),(110310,22263,0.00,0.00,5),(110311,22264,0.00,0.00,1),(110312,22264,0.00,0.00,3),(110313,22264,0.00,0.00,4),(110314,22264,20.00,0.00,2),(110315,22264,0.00,0.00,5),(110316,22265,20.00,75000.00,1),(110317,22265,0.00,0.00,3),(110318,22265,0.00,0.00,4),(110319,22265,20.00,0.00,2),(110320,22265,0.00,0.00,5),(128796,25961,30.00,0.00,1),(128797,25961,10.00,1000000.00,3),(128798,25961,2.00,0.00,4),(128799,25961,20.00,0.00,2),(128800,25961,1.00,0.00,5),(128801,25962,3.00,0.00,1),(128802,25962,0.00,0.00,3),(128803,25962,2.00,0.00,4),(128804,25962,20.00,0.00,2),(128805,25962,1.00,0.00,5),(128806,25963,30.00,0.00,1),(128807,25963,10.00,10000000.00,3),(128808,25963,2.00,0.00,4),(128809,25963,20.00,0.00,2),(128810,25963,1.00,0.00,5),(128811,25964,10.00,0.00,1),(128812,25964,10.00,10000000.00,3),(128813,25964,2.00,0.00,4),(128814,25964,20.00,0.00,2),(128815,25964,1.00,0.00,5),(128816,25965,10.00,0.00,1),(128817,25965,10.00,1000000.00,3),(128818,25965,2.00,0.00,4),(128819,25965,20.00,0.00,2),(128820,25965,1.00,0.00,5),(128821,25966,10.00,0.00,1),(128822,25966,0.00,0.00,3),(128823,25966,2.00,0.00,4),(128824,25966,20.00,0.00,2),(128825,25966,1.00,0.00,5),(128826,25967,10.00,0.00,1),(128827,25967,10.00,1000000.00,3),(128828,25967,2.00,0.00,4),(128829,25967,20.00,0.00,2),(128830,25967,1.00,0.00,5),(128831,25968,10.00,0.00,1),(128832,25968,0.00,0.00,3),(128833,25968,2.00,0.00,4),(128834,25968,20.00,0.00,2),(128835,25968,1.00,0.00,5),(128836,25969,5.00,0.00,1),(128837,25969,2.00,10000000.00,3),(128838,25969,2.00,0.00,4),(128839,25969,20.00,0.00,2),(128840,25969,1.00,0.00,5),(128841,25970,5.00,0.00,1),(128842,25970,10.00,10000000.00,3),(128843,25970,2.00,0.00,4),(128844,25970,20.00,0.00,2),(128845,25970,1.00,0.00,5),(128846,25971,20.00,0.00,1),(128847,25971,10.00,1000000.00,3),(128848,25971,2.00,0.00,4),(128849,25971,20.00,0.00,2),(128850,25971,1.00,0.00,5),(128851,25972,20.00,0.00,1),(128852,25972,0.00,0.00,3),(128853,25972,2.00,0.00,4),(128854,25972,0.00,0.00,2),(128855,25972,1.00,0.00,5),(128856,25973,20.00,0.00,1),(128857,25973,10.00,1000000.00,3),(128858,25973,2.00,0.00,4),(128859,25973,20.00,0.00,2),(128860,25973,1.00,0.00,5),(128861,25974,5.00,0.00,1),(128862,25974,2.00,10000000.00,3),(128863,25974,2.00,0.00,4),(128864,25974,20.00,0.00,2),(128865,25974,1.00,0.00,5),(128866,25975,5.00,0.00,1),(128867,25975,10.00,10000000.00,3),(128868,25975,2.00,0.00,4),(128869,25975,20.00,0.00,2),(128870,25975,1.00,0.00,5),(128871,25976,20.00,0.00,1),(128872,25976,10.00,1000000.00,3),(128873,25976,2.00,0.00,4),(128874,25976,20.00,0.00,2),(128875,25976,1.00,0.00,5),(128876,25977,20.00,0.00,1),(128877,25977,0.00,0.00,3),(128878,25977,2.00,0.00,4),(128879,25977,20.00,0.00,2),(128880,25977,1.00,0.00,5),(128881,25978,20.00,0.00,1),(128882,25978,10.00,10000000.00,3),(128883,25978,2.00,0.00,4),(128884,25978,20.00,0.00,2),(128885,25978,1.00,0.00,5),(128886,25979,20.00,0.00,1),(128887,25979,10.00,1000000.00,3),(128888,25979,2.00,0.00,4),(128889,25979,20.00,0.00,2),(128890,25979,1.00,0.00,5),(128891,25980,20.00,0.00,1),(128892,25980,0.00,0.00,3),(128893,25980,2.00,0.00,4),(128894,25980,20.00,0.00,2),(128895,25980,1.00,0.00,5),(128896,25981,20.00,0.00,1),(128897,25981,10.00,10000000.00,3),(128898,25981,2.00,0.00,4),(128899,25981,20.00,0.00,2),(128900,25981,1.00,0.00,5),(128901,25982,20.00,0.00,1),(128902,25982,10.00,1000000.00,3),(128903,25982,2.00,0.00,4),(128904,25982,20.00,0.00,2),(128905,25982,0.00,0.00,5),(128906,25983,20.00,0.00,1),(128907,25983,0.00,0.00,3),(128908,25983,2.00,0.00,4),(128909,25983,20.00,0.00,2),(128910,25983,0.00,0.00,5),(128911,25984,10.00,0.00,1),(128912,25984,10.00,1000000.00,3),(128913,25984,2.00,0.00,4),(128914,25984,20.00,0.00,2),(128915,25984,0.00,0.00,5),(128916,25985,10.00,0.00,1),(128917,25985,10.00,1000000.00,3),(128918,25985,2.00,0.00,4),(128919,25985,20.00,0.00,2),(128920,25985,1.00,0.00,5),(128921,25986,10.00,0.00,1),(128922,25986,0.00,0.00,3),(128923,25986,2.00,0.00,4),(128924,25986,20.00,0.00,2),(128925,25986,1.00,0.00,5),(128926,25987,10.00,0.00,1),(128927,25987,10.00,1000000.00,3),(128928,25987,2.00,0.00,4),(128929,25987,20.00,0.00,2),(128930,25987,1.00,0.00,5),(128931,25988,20.00,0.00,1),(128932,25988,10.00,1000000.00,3),(128933,25988,2.00,0.00,4),(128934,25988,20.00,0.00,2),(128935,25988,1.00,0.00,5),(128936,25989,20.00,0.00,1),(128937,25989,0.00,0.00,3),(128938,25989,2.00,0.00,4),(128939,25989,0.00,0.00,2),(128940,25989,1.00,0.00,5),(128941,25990,20.00,0.00,1),(128942,25990,10.00,1000000.00,3),(128943,25990,2.00,0.00,4),(128944,25990,20.00,0.00,2),(128945,25990,1.00,0.00,5),(128946,25991,20.00,0.00,1),(128947,25991,10.00,1000000.00,3),(128948,25991,2.00,0.00,4),(128949,25991,20.00,0.00,2),(128950,25991,1.00,0.00,5),(128951,25992,20.00,0.00,1),(128952,25992,0.00,0.00,3),(128953,25992,2.00,0.00,4),(128954,25992,20.00,0.00,2),(128955,25992,1.00,0.00,5),(128956,25993,30.00,0.00,1),(128957,25993,10.00,1000000.00,3),(128958,25993,2.00,0.00,4),(128959,25993,20.00,0.00,2),(128960,25993,1.00,0.00,5),(128961,25994,30.00,0.00,1),(128962,25994,0.00,0.00,3),(128963,25994,2.00,0.00,4),(128964,25994,20.00,0.00,2),(128965,25994,1.00,0.00,5),(128966,25995,10.00,0.00,1),(128967,25995,10.00,1000000.00,3),(128968,25995,2.00,0.00,4),(128969,25995,20.00,0.00,2),(128970,25995,1.00,0.00,5),(128971,25996,10.00,0.00,1),(128972,25996,0.00,0.00,3),(128973,25996,2.00,0.00,4),(128974,25996,20.00,0.00,2),(128975,25996,1.00,0.00,5),(128976,25997,30.00,0.00,1),(128977,25997,10.00,1000000.00,3),(128978,25997,2.00,0.00,4),(128979,25997,20.00,0.00,2),(128980,25997,1.00,0.00,5),(128981,25998,30.00,0.00,1),(128982,25998,0.00,0.00,3),(128983,25998,2.00,0.00,4),(128984,25998,20.00,0.00,2),(128985,25998,1.00,0.00,5),(128986,25999,30.00,0.00,1),(128987,25999,10.00,10000000.00,3),(128988,25999,2.00,0.00,4),(128989,25999,20.00,0.00,2),(128990,25999,1.00,0.00,5),(128991,26000,30.00,0.00,1),(128992,26000,10.00,1000000.00,3),(128993,26000,2.00,0.00,4),(128994,26000,20.00,0.00,2),(128995,26000,1.00,0.00,5),(128996,26001,30.00,0.00,1),(128997,26001,0.00,0.00,3),(128998,26001,2.00,0.00,4),(128999,26001,20.00,0.00,2),(129000,26001,1.00,0.00,5),(129001,26002,25.00,0.00,1),(129002,26002,10.00,10000000.00,3),(129003,26002,2.00,0.00,4),(129004,26002,20.00,0.00,2),(129005,26002,1.00,0.00,5),(129006,26003,20.00,0.00,1),(129007,26003,10.00,1000000.00,3),(129008,26003,2.00,0.00,4),(129009,26003,20.00,0.00,2),(129010,26003,1.00,0.00,5),(129011,26004,20.00,0.00,1),(129012,26004,0.00,0.00,3),(129013,26004,2.00,0.00,4),(129014,26004,20.00,0.00,2),(129015,26004,1.00,0.00,5),(129016,26005,10.00,0.00,1),(129017,26005,10.00,1000000.00,3),(129018,26005,2.00,0.00,4),(129019,26005,20.00,0.00,2),(129020,26005,1.00,0.00,5),(129021,26006,10.00,0.00,1),(129022,26006,0.00,0.00,3),(129023,26006,2.00,0.00,4),(129024,26006,20.00,0.00,2),(129025,26006,1.00,0.00,5),(129026,26007,10.00,0.00,1),(129027,26007,10.00,1000000.00,3),(129028,26007,2.00,0.00,4),(129029,26007,20.00,0.00,2),(129030,26007,1.00,0.00,5),(129031,26008,10.00,0.00,1),(129032,26008,0.00,0.00,3),(129033,26008,2.00,0.00,4),(129034,26008,20.00,0.00,2),(129035,26008,1.00,0.00,5),(129036,26009,15.00,0.00,1),(129037,26009,10.00,10000000.00,3),(129038,26009,2.00,0.00,4),(129039,26009,20.00,0.00,2),(129040,26009,1.00,0.00,5),(129041,26010,30.00,2500.00,1),(129042,26010,10.00,100000.00,3),(129043,26010,2.00,0.00,4),(129044,26010,20.00,0.00,2),(129045,26010,1.00,0.00,5),(129046,26011,30.00,2500.00,1),(129047,26011,0.00,0.00,3),(129048,26011,2.00,0.00,4),(129049,26011,20.00,0.00,2),(129050,26011,1.00,0.00,5),(129051,26012,30.00,2500.00,1),(129052,26012,10.00,10000000.00,3),(129053,26012,2.00,0.00,4),(129054,26012,20.00,0.00,2),(129055,26012,1.00,0.00,5),(129056,26013,30.00,5000.00,1),(129057,26013,10.00,1000000.00,3),(129058,26013,2.00,0.00,4),(129059,26013,20.00,0.00,2),(129060,26013,1.00,0.00,5),(129061,26014,30.00,5000.00,1),(129062,26014,0.00,0.00,3),(129063,26014,2.00,0.00,4),(129064,26014,20.00,0.00,2),(129065,26014,1.00,0.00,5),(129066,26015,30.00,5000.00,1),(129067,26015,10.00,1000000.00,3),(129068,26015,2.00,0.00,4),(129069,26015,20.00,0.00,2),(129070,26015,1.00,0.00,5),(129071,26016,10.00,1000.00,1),(129072,26016,10.00,1000000.00,3),(129073,26016,2.00,0.00,4),(129074,26016,20.00,0.00,2),(129075,26016,1.00,0.00,5),(129076,26017,10.00,1000.00,1),(129077,26017,0.00,0.00,3),(129078,26017,2.00,0.00,4),(129079,26017,20.00,0.00,2),(129080,26017,1.00,0.00,5),(129081,26018,10.00,1000.00,1),(129082,26018,10.00,1000000.00,3),(129083,26018,2.00,0.00,4),(129084,26018,20.00,0.00,2),(129085,26018,1.00,0.00,5),(129086,26019,30.00,0.00,1),(129087,26019,10.00,1000000.00,3),(129088,26019,2.00,0.00,4),(129089,26019,20.00,0.00,2),(129090,26019,1.00,0.00,5),(129091,26020,30.00,0.00,1),(129092,26020,0.00,0.00,3),(129093,26020,2.00,0.00,4),(129094,26020,20.00,0.00,2),(129095,26020,1.00,0.00,5),(129096,26021,30.00,0.00,1),(129097,26021,10.00,10000000.00,3),(129098,26021,2.00,0.00,4),(129099,26021,20.00,0.00,2),(129100,26021,1.00,0.00,5),(129101,26022,30.00,0.00,1),(129102,26022,10.00,1000000.00,3),(129103,26022,2.00,0.00,4),(129104,26022,20.00,0.00,2),(129105,26022,1.00,0.00,5),(129106,26023,30.00,0.00,1),(129107,26023,0.00,0.00,3),(129108,26023,2.00,0.00,4),(129109,26023,20.00,0.00,2),(129110,26023,1.00,0.00,5),(129111,26024,25.00,0.00,1),(129112,26024,10.00,10000000.00,3),(129113,26024,2.00,0.00,4),(129114,26024,20.00,0.00,2),(129115,26024,1.00,0.00,5),(129116,26025,20.00,0.00,1),(129117,26025,10.00,1000000.00,3),(129118,26025,2.00,0.00,4),(129119,26025,20.00,0.00,2),(129120,26025,1.00,0.00,5),(129121,26026,20.00,0.00,1),(129122,26026,0.00,0.00,3),(129123,26026,2.00,0.00,4),(129124,26026,20.00,0.00,2),(129125,26026,1.00,0.00,5),(129126,26027,10.00,0.00,1),(129127,26027,10.00,1000000.00,3),(129128,26027,2.00,0.00,4),(129129,26027,20.00,0.00,2),(129130,26027,1.00,0.00,5),(129131,26028,10.00,0.00,1),(129132,26028,0.00,0.00,3),(129133,26028,2.00,0.00,4),(129134,26028,20.00,0.00,2),(129135,26028,1.00,0.00,5),(129136,26029,5.00,0.00,1),(129137,26029,10.00,10000000.00,3),(129138,26029,2.00,0.00,4),(129139,26029,20.00,0.00,2),(129140,26029,1.00,0.00,5),(129141,26030,20.00,0.00,1),(129142,26030,10.00,1000000.00,3),(129143,26030,2.00,0.00,4),(129144,26030,20.00,0.00,2),(129145,26030,0.00,0.00,5),(129146,26031,20.00,0.00,1),(129147,26031,0.00,0.00,3),(129148,26031,2.00,0.00,4),(129149,26031,20.00,0.00,2),(129150,26031,1.00,0.00,5),(129151,26032,20.00,0.00,1),(129152,26032,10.00,10000000.00,3),(129153,26032,2.00,0.00,4),(129154,26032,20.00,0.00,2),(129155,26032,1.00,0.00,5),(129156,26033,20.00,0.00,1),(129157,26033,10.00,1000000.00,3),(129158,26033,2.00,0.00,4),(129159,26033,20.00,0.00,2),(129160,26033,1.00,0.00,5),(129161,26034,10.00,0.00,1),(129162,26034,10.00,1000000.00,3),(129163,26034,2.00,0.00,4),(129164,26034,20.00,0.00,2),(129165,26034,1.00,0.00,5),(129166,26035,10.00,0.00,1),(129167,26035,10.00,1000000.00,3),(129168,26035,2.00,0.00,4),(129169,26035,20.00,0.00,2),(129170,26035,0.00,0.00,5),(129171,26036,10.00,0.00,1),(129172,26036,0.00,0.00,3),(129173,26036,2.00,0.00,4),(129174,26036,20.00,0.00,2),(129175,26036,1.00,0.00,5),(129176,26037,20.00,0.00,1),(129177,26037,0.00,0.00,3),(129178,26037,2.00,0.00,4),(129179,26037,20.00,0.00,2),(129180,26037,1.00,0.00,5),(129181,26038,20.00,0.00,1),(129182,26038,10.00,10000000.00,3),(129183,26038,2.00,0.00,4),(129184,26038,20.00,0.00,2),(129185,26038,1.00,0.00,5),(129186,26039,30.00,0.00,1),(129187,26039,10.00,1000000.00,3),(129188,26039,2.00,0.00,4),(129189,26039,20.00,0.00,2),(129190,26039,0.00,0.00,5),(129191,26040,30.00,0.00,1),(129192,26040,10.00,1000000.00,3),(129193,26040,2.00,0.00,4),(129194,26040,20.00,0.00,2),(129195,26040,1.00,0.00,5),(129196,26041,30.00,0.00,1),(129197,26041,0.00,0.00,3),(129198,26041,2.00,0.00,4),(129199,26041,20.00,0.00,2),(129200,26041,1.00,0.00,5),(129201,26042,20.00,0.00,1),(129202,26042,10.00,1000000.00,3),(129203,26042,2.00,0.00,4),(129204,26042,20.00,0.00,2),(129205,26042,0.00,0.00,5),(129206,26043,20.00,0.00,1),(129207,26043,10.00,1000000.00,3),(129208,26043,2.00,0.00,4),(129209,26043,20.00,0.00,2),(129210,26043,1.00,0.00,5),(129211,26044,0.00,0.00,1),(129212,26044,0.00,0.00,3),(129213,26044,0.00,0.00,4),(129214,26044,20.00,0.00,2),(129215,26044,0.00,0.00,5),(129216,26045,0.00,0.00,1),(129217,26045,0.00,0.00,3),(129218,26045,0.00,0.00,4),(129219,26045,20.00,0.00,2),(129220,26045,0.00,0.00,5),(146236,29449,10.00,5000.00,1),(146237,29449,10.00,1000000.00,3),(146238,29449,2.00,0.00,4),(146239,29449,20.00,0.00,2),(146240,29449,0.00,0.00,5),(146241,29450,10.00,5000.00,1),(146242,29450,10.00,1000000.00,3),(146243,29450,2.00,0.00,4),(146244,29450,20.00,0.00,2),(146245,29450,1.00,0.00,5),(146246,29451,10.00,5000.00,1),(146247,29451,0.00,0.00,3),(146248,29451,0.00,0.00,4),(146249,29451,20.00,0.00,2),(146250,29451,0.00,0.00,5),(146251,29452,10.00,20000.00,1),(146252,29452,0.00,0.00,3),(146253,29452,0.00,0.00,4),(146254,29452,20.00,0.00,2),(146255,29452,0.00,0.00,5),(146256,29453,10.00,10000.00,1),(146257,29453,10.00,1000000.00,3),(146258,29453,2.00,0.00,4),(146259,29453,20.00,0.00,2),(146260,29453,1.00,0.00,5),(146261,29454,10.00,10000.00,1),(146262,29454,0.00,0.00,3),(146263,29454,0.00,0.00,4),(146264,29454,20.00,0.00,2),(146265,29454,0.00,0.00,5),(146266,29455,10.00,2500.00,1),(146267,29455,10.00,1000000.00,3),(146268,29455,2.00,0.00,4),(146269,29455,20.00,0.00,2),(146270,29455,0.00,0.00,5),(146271,29456,10.00,2500.00,1),(146272,29456,10.00,1000000.00,3),(146273,29456,2.00,0.00,4),(146274,29456,20.00,0.00,2),(146275,29456,1.00,0.00,5),(146276,29457,10.00,2500.00,1),(146277,29457,0.00,0.00,3),(146278,29457,0.00,0.00,4),(146279,29457,20.00,0.00,2),(146280,29457,0.00,0.00,5),(146281,29458,10.00,5000.00,1),(146282,29458,0.00,0.00,3),(146283,29458,0.00,0.00,4),(146284,29458,20.00,0.00,2),(146285,29458,0.00,0.00,5),(146286,29459,10.00,5000.00,1),(146287,29459,10.00,1000000.00,3),(146288,29459,2.00,0.00,4),(146289,29459,20.00,0.00,2),(146290,29459,0.00,0.00,5),(146291,29460,10.00,5000.00,1),(146292,29460,10.00,1000000.00,3),(146293,29460,2.00,0.00,4),(146294,29460,20.00,0.00,2),(146295,29460,1.00,0.00,5),(146296,29461,10.00,5000.00,1),(146297,29461,0.00,0.00,3),(146298,29461,0.00,0.00,4),(146299,29461,20.00,0.00,2),(146300,29461,0.00,0.00,5),(146301,29462,10.00,100000.00,1),(146302,29462,10.00,1000000.00,3),(146303,29462,2.00,0.00,4),(146304,29462,20.00,0.00,2),(146305,29462,0.00,0.00,5),(146306,29463,10.00,100000.00,1),(146307,29463,10.00,1000000.00,3),(146308,29463,2.00,0.00,4),(146309,29463,20.00,0.00,2),(146310,29463,1.00,0.00,5),(146311,29464,10.00,100000.00,1),(146312,29464,0.00,0.00,3),(146313,29464,0.00,0.00,4),(146314,29464,20.00,0.00,2),(146315,29464,0.00,0.00,5),(146316,29465,10.00,200000.00,1),(146317,29465,0.00,0.00,3),(146318,29465,0.00,0.00,4),(146319,29465,20.00,0.00,2),(146320,29465,0.00,0.00,5),(146321,29466,1.00,5000000.00,1),(146322,29466,0.00,0.00,3),(146323,29466,0.00,0.00,4),(146324,29466,20.00,0.00,2),(146325,29466,0.00,0.00,5),(146326,29467,20.00,2500.00,1),(146327,29467,10.00,1000000.00,3),(146328,29467,2.00,0.00,4),(146329,29467,20.00,0.00,2),(146330,29467,0.00,0.00,5),(146331,29468,20.00,0.00,1),(146332,29468,10.00,1000000.00,3),(146333,29468,2.00,0.00,4),(146334,29468,20.00,0.00,2),(146335,29468,1.00,0.00,5),(146336,29469,20.00,0.00,1),(146337,29469,0.00,0.00,3),(146338,29469,0.00,0.00,4),(146339,29469,20.00,0.00,2),(146340,29469,0.00,0.00,5),(146341,29470,20.00,0.00,1),(146342,29470,10.00,1000000.00,3),(146343,29470,2.00,0.00,4),(146344,29470,20.00,0.00,2),(146345,29470,1.00,0.00,5),(146346,29471,20.00,0.00,1),(146347,29471,0.00,0.00,3),(146348,29471,0.00,0.00,4),(146349,29471,20.00,0.00,2),(146350,29471,0.00,0.00,5),(146351,29472,2.00,20000.00,1),(146352,29472,10.00,1000000.00,3),(146353,29472,2.00,0.00,4),(146354,29472,20.00,0.00,2),(146355,29472,0.00,0.00,5),(146356,29473,2.00,50000.00,1),(146357,29473,10.00,1000000.00,3),(146358,29473,2.00,0.00,4),(146359,29473,20.00,0.00,2),(146360,29473,1.00,0.00,5),(146361,29474,2.00,50000.00,1),(146362,29474,0.00,0.00,3),(146363,29474,0.00,0.00,4),(146364,29474,20.00,0.00,2),(146365,29474,0.00,0.00,5),(146366,29475,1.00,50000.00,1),(146367,29475,0.00,0.00,3),(146368,29475,0.00,0.00,4),(146369,29475,20.00,0.00,2),(146370,29475,0.00,0.00,5),(146371,29476,1.00,75000.00,1),(146372,29476,0.00,0.00,3),(146373,29476,0.00,0.00,4),(146374,29476,20.00,0.00,2),(146375,29476,0.00,0.00,5),(146376,29477,10.00,30000.00,1),(146377,29477,0.00,0.00,3),(146378,29477,0.00,0.00,4),(146379,29477,20.00,0.00,2),(146380,29477,0.00,0.00,5),(146381,29478,15.00,120000.00,1),(146382,29478,10.00,1000000.00,3),(146383,29478,2.00,0.00,4),(146384,29478,20.00,0.00,2),(146385,29478,0.00,0.00,5),(146386,29479,15.00,120000.00,1),(146387,29479,10.00,1000000.00,3),(146388,29479,2.00,0.00,4),(146389,29479,20.00,0.00,2),(146390,29479,1.00,0.00,5),(146391,29480,15.00,120000.00,1),(146392,29480,0.00,0.00,3),(146393,29480,0.00,0.00,4),(146394,29480,20.00,0.00,2),(146395,29480,0.00,0.00,5),(146396,29481,10.00,120000.00,1),(146397,29481,0.00,0.00,3),(146398,29481,0.00,0.00,4),(146399,29481,20.00,0.00,2),(146400,29481,0.00,0.00,5),(146401,29482,10.00,180000.00,1),(146402,29482,0.00,0.00,3),(146403,29482,0.00,0.00,4),(146404,29482,20.00,0.00,2),(146405,29482,0.00,0.00,5),(146406,29483,10.00,120000.00,1),(146407,29483,10.00,1000000.00,3),(146408,29483,2.00,0.00,4),(146409,29483,20.00,0.00,2),(146410,29483,1.00,0.00,5),(146411,29484,10.00,120000.00,1),(146412,29484,0.00,0.00,3),(146413,29484,0.00,0.00,4),(146414,29484,20.00,0.00,2),(146415,29484,0.00,0.00,5),(146416,29485,2.00,120000.00,1),(146417,29485,0.00,0.00,3),(146418,29485,0.00,0.00,4),(146419,29485,20.00,0.00,2),(146420,29485,0.00,0.00,5),(146421,29486,2.00,180000.00,1),(146422,29486,0.00,0.00,3),(146423,29486,0.00,0.00,4),(146424,29486,20.00,0.00,2),(146425,29486,0.00,0.00,5),(146426,29487,30.00,2500.00,1),(146427,29487,10.00,1000000.00,3),(146428,29487,2.00,0.00,4),(146429,29487,20.00,0.00,2),(146430,29487,0.00,0.00,5),(146431,29488,30.00,2500.00,1),(146432,29488,10.00,1000000.00,3),(146433,29488,2.00,0.00,4),(146434,29488,20.00,0.00,2),(146435,29488,1.00,0.00,5),(146436,29489,30.00,2500.00,1),(146437,29489,0.00,0.00,3),(146438,29489,0.00,0.00,4),(146439,29489,20.00,0.00,2),(146440,29489,0.00,0.00,5),(146441,29490,30.00,5000.00,1),(146442,29490,0.00,0.00,3),(146443,29490,0.00,0.00,4),(146444,29490,20.00,0.00,2),(146445,29490,0.00,0.00,5),(146446,29491,30.00,5000.00,1),(146447,29491,10.00,1000000.00,3),(146448,29491,2.00,0.00,4),(146449,29491,20.00,0.00,2),(146450,29491,0.00,0.00,5),(146451,29492,30.00,5000.00,1),(146452,29492,10.00,1000000.00,3),(146453,29492,2.00,0.00,4),(146454,29492,20.00,0.00,2),(146455,29492,1.00,0.00,5),(146456,29493,30.00,5000.00,1),(146457,29493,0.00,0.00,3),(146458,29493,0.00,0.00,4),(146459,29493,20.00,0.00,2),(146460,29493,0.00,0.00,5),(146461,29494,30.00,10000.00,1),(146462,29494,0.00,0.00,3),(146463,29494,0.00,0.00,4),(146464,29494,20.00,0.00,2),(146465,29494,0.00,0.00,5),(146466,29495,20.00,2500.00,1),(146467,29495,10.00,1000000.00,3),(146468,29495,2.00,0.00,4),(146469,29495,20.00,0.00,2),(146470,29495,0.00,0.00,5),(146471,29496,20.00,2500.00,1),(146472,29496,10.00,1000000.00,3),(146473,29496,2.00,0.00,4),(146474,29496,20.00,0.00,2),(146475,29496,1.00,0.00,5),(146476,29497,20.00,0.00,1),(146477,29497,10.00,1000000.00,3),(146478,29497,2.00,0.00,4),(146479,29497,20.00,0.00,2),(146480,29497,1.00,0.00,5),(146481,29498,10.00,0.00,1),(146482,29498,0.00,0.00,3),(146483,29498,0.00,0.00,4),(146484,29498,20.00,0.00,2),(146485,29498,0.00,0.00,5),(146486,29499,10.00,0.00,1),(146487,29499,0.00,0.00,3),(146488,29499,0.00,0.00,4),(146489,29499,20.00,0.00,2),(146490,29499,0.00,0.00,5),(146491,29500,10.00,1000.00,1),(146492,29500,10.00,1000000.00,3),(146493,29500,2.00,0.00,4),(146494,29500,20.00,0.00,2),(146495,29500,0.00,0.00,5),(146496,29501,10.00,1000.00,1),(146497,29501,10.00,1000000.00,3),(146498,29501,2.00,0.00,4),(146499,29501,20.00,0.00,2),(146500,29501,1.00,0.00,5),(146501,29502,10.00,1000.00,1),(146502,29502,0.00,0.00,3),(146503,29502,0.00,0.00,4),(146504,29502,20.00,0.00,2),(146505,29502,0.00,0.00,5),(146506,29503,5.00,2500.00,1),(146507,29503,10.00,1000000.00,3),(146508,29503,2.00,0.00,4),(146509,29503,20.00,0.00,2),(146510,29503,0.00,0.00,5),(146511,29504,5.00,2500.00,1),(146512,29504,10.00,1000000.00,3),(146513,29504,2.00,0.00,4),(146514,29504,20.00,0.00,2),(146515,29504,1.00,0.00,5),(146516,29505,10.00,2500.00,1),(146517,29505,10.00,1000000.00,3),(146518,29505,2.00,0.00,4),(146519,29505,20.00,0.00,2),(146520,29505,1.00,0.00,5),(146521,29506,10.00,2500.00,1),(146522,29506,0.00,0.00,3),(146523,29506,0.00,0.00,4),(146524,29506,20.00,0.00,2),(146525,29506,0.00,0.00,5),(146526,29507,10.00,5000.00,1),(146527,29507,0.00,0.00,3),(146528,29507,0.00,0.00,4),(146529,29507,20.00,0.00,2),(146530,29507,0.00,0.00,5),(146531,29508,20.00,2500.00,1),(146532,29508,10.00,1000000.00,3),(146533,29508,2.00,0.00,4),(146534,29508,20.00,0.00,2),(146535,29508,1.00,0.00,5),(146536,29509,10.00,2500.00,1),(146537,29509,0.00,0.00,3),(146538,29509,0.00,0.00,4),(146539,29509,20.00,0.00,2),(146540,29509,0.00,0.00,5),(146541,29510,5.00,20000.00,1),(146542,29510,10.00,1000000.00,3),(146543,29510,2.00,0.00,4),(146544,29510,20.00,0.00,2),(146545,29510,0.00,0.00,5),(146546,29511,5.00,20000.00,1),(146547,29511,10.00,1000000.00,3),(146548,29511,2.00,0.00,4),(146549,29511,20.00,0.00,2),(146550,29511,1.00,0.00,5),(146551,29512,10.00,20000.00,1),(146552,29512,10.00,1000000.00,3),(146553,29512,2.00,0.00,4),(146554,29512,20.00,0.00,2),(146555,29512,1.00,0.00,5),(146556,29513,10.00,20000.00,1),(146557,29513,0.00,0.00,3),(146558,29513,0.00,0.00,4),(146559,29513,20.00,0.00,2),(146560,29513,0.00,0.00,5),(146561,29514,10.00,30000.00,1),(146562,29514,0.00,0.00,3),(146563,29514,0.00,0.00,4),(146564,29514,20.00,0.00,2),(146565,29514,0.00,0.00,5),(146566,29515,1.00,20000.00,1),(146567,29515,10.00,1000000.00,3),(146568,29515,2.00,0.00,4),(146569,29515,20.00,0.00,2),(146570,29515,0.00,0.00,5),(146571,29516,1.00,20000.00,1),(146572,29516,10.00,1000000.00,3),(146573,29516,2.00,0.00,4),(146574,29516,20.00,0.00,2),(146575,29516,1.00,0.00,5),(146576,29517,1.00,20000.00,1),(146577,29517,0.00,0.00,3),(146578,29517,0.00,0.00,4),(146579,29517,20.00,0.00,2),(146580,29517,0.00,0.00,5),(146581,29518,1.00,20000.00,1),(146582,29518,0.00,0.00,3),(146583,29518,0.00,0.00,4),(146584,29518,20.00,0.00,2),(146585,29518,0.00,0.00,5),(146586,29519,1.00,75000.00,1),(146587,29519,0.00,0.00,3),(146588,29519,0.00,0.00,4),(146589,29519,20.00,0.00,2),(146590,29519,0.00,0.00,5),(146591,29520,1.00,20000.00,1),(146592,29520,10.00,1000000.00,3),(146593,29520,2.00,0.00,4),(146594,29520,20.00,0.00,2),(146595,29520,0.00,0.00,5),(146596,29521,1.00,20000.00,1),(146597,29521,10.00,1000000.00,3),(146598,29521,2.00,0.00,4),(146599,29521,20.00,0.00,2),(146600,29521,1.00,0.00,5),(146601,29522,1.00,50000.00,1),(146602,29522,0.00,0.00,3),(146603,29522,0.00,0.00,4),(146604,29522,20.00,0.00,2),(146605,29522,0.00,0.00,5),(146606,29523,1.00,50000.00,1),(146607,29523,0.00,0.00,3),(146608,29523,0.00,0.00,4),(146609,29523,20.00,0.00,2),(146610,29523,0.00,0.00,5),(146611,29524,1.00,75000.00,1),(146612,29524,0.00,0.00,3),(146613,29524,0.00,0.00,4),(146614,29524,20.00,0.00,2),(146615,29524,0.00,0.00,5),(146616,29525,2.00,50000.00,1),(146617,29525,0.00,0.00,3),(146618,29525,0.00,0.00,4),(146619,29525,20.00,0.00,2),(146620,29525,0.00,0.00,5),(146621,29526,0.00,0.00,1),(146622,29526,0.00,0.00,3),(146623,29526,0.00,0.00,4),(146624,29526,20.00,0.00,2),(146625,29526,0.00,0.00,5),(146626,29527,20.00,75000.00,1),(146627,29527,0.00,0.00,3),(146628,29527,0.00,0.00,4),(146629,29527,20.00,0.00,2),(146630,29527,0.00,0.00,5),(146631,29528,1.00,50000.00,1),(146632,29528,0.00,0.00,3),(146633,29528,0.00,0.00,4),(146634,29528,20.00,0.00,2),(146635,29528,0.00,0.00,5),(146636,29529,0.00,0.00,1),(146637,29529,0.00,0.00,3),(146638,29529,0.00,0.00,4),(146639,29529,20.00,0.00,2),(146640,29529,0.00,0.00,5),(146641,29530,20.00,75000.00,1),(146642,29530,0.00,0.00,3),(146643,29530,0.00,0.00,4),(146644,29530,20.00,0.00,2),(146645,29530,0.00,0.00,5),(171271,34456,10.00,5000.00,1),(171272,34456,2.50,0.00,3),(171273,34456,2.00,0.00,4),(171274,34456,20.00,0.00,2),(171275,34456,0.00,0.00,5),(171276,34457,10.00,5000.00,1),(171277,34457,0.00,0.00,3),(171278,34457,2.00,0.00,4),(171279,34457,20.00,0.00,2),(171280,34457,1.00,0.00,5),(171281,34458,10.00,5000.00,1),(171282,34458,0.00,0.00,3),(171283,34458,2.00,0.00,4),(171284,34458,20.00,0.00,2),(171285,34458,1.00,0.00,5),(171286,34459,10.00,5000.00,1),(171287,34459,0.00,0.00,3),(171288,34459,0.00,0.00,4),(171289,34459,20.00,0.00,2),(171290,34459,0.00,0.00,5),(171291,34460,10.00,20000.00,1),(171292,34460,0.00,0.00,3),(171293,34460,0.00,0.00,4),(171294,34460,20.00,0.00,2),(171295,34460,0.00,0.00,5),(171296,34461,10.00,10000.00,1),(171297,34461,0.00,0.00,3),(171298,34461,2.00,0.00,4),(171299,34461,20.00,0.00,2),(171300,34461,1.00,0.00,5),(171301,34462,10.00,10000.00,1),(171302,34462,0.00,0.00,3),(171303,34462,0.00,0.00,4),(171304,34462,20.00,0.00,2),(171305,34462,0.00,0.00,5),(171306,34463,10.00,2500.00,1),(171307,34463,2.50,2.00,3),(171308,34463,0.00,0.00,4),(171309,34463,20.00,0.00,2),(171310,34463,0.00,0.00,5),(171311,34464,10.00,2500.00,1),(171312,34464,0.00,0.00,3),(171313,34464,2.00,0.00,4),(171314,34464,20.00,0.00,2),(171315,34464,0.00,0.00,5),(171316,34465,10.00,2500.00,1),(171317,34465,0.00,0.00,3),(171318,34465,2.00,0.00,4),(171319,34465,20.00,0.00,2),(171320,34465,1.00,0.00,5),(171321,34466,10.00,2500.00,1),(171322,34466,0.00,0.00,3),(171323,34466,0.00,0.00,4),(171324,34466,20.00,0.00,2),(171325,34466,0.00,0.00,5),(171326,34467,10.00,5000.00,1),(171327,34467,0.00,0.00,3),(171328,34467,0.00,0.00,4),(171329,34467,20.00,0.00,2),(171330,34467,0.00,0.00,5),(171331,34468,10.00,5000.00,1),(171332,34468,2.50,0.00,3),(171333,34468,2.00,0.00,4),(171334,34468,20.00,0.00,2),(171335,34468,0.00,0.00,5),(171336,34469,10.00,5000.00,1),(171337,34469,0.00,0.00,3),(171338,34469,2.00,0.00,4),(171339,34469,20.00,0.00,2),(171340,34469,0.00,0.00,5),(171341,34470,10.00,5000.00,1),(171342,34470,0.00,0.00,3),(171343,34470,2.00,0.00,4),(171344,34470,20.00,0.00,2),(171345,34470,1.00,0.00,5),(171346,34471,10.00,5000.00,1),(171347,34471,0.00,0.00,3),(171348,34471,0.00,0.00,4),(171349,34471,20.00,0.00,2),(171350,34471,0.00,0.00,5),(171351,34472,10.00,100000.00,1),(171352,34472,2.50,0.00,3),(171353,34472,2.00,0.00,4),(171354,34472,20.00,0.00,2),(171355,34472,0.00,0.00,5),(171356,34473,10.00,100000.00,1),(171357,34473,0.00,0.00,3),(171358,34473,2.00,0.00,4),(171359,34473,20.00,0.00,2),(171360,34473,1.00,0.00,5),(171361,34474,10.00,100000.00,1),(171362,34474,0.00,0.00,3),(171363,34474,2.00,0.00,4),(171364,34474,20.00,0.00,2),(171365,34474,1.00,0.00,5),(171366,34475,10.00,100000.00,1),(171367,34475,0.00,0.00,3),(171368,34475,0.00,0.00,4),(171369,34475,20.00,0.00,2),(171370,34475,0.00,0.00,5),(171371,34476,10.00,200000.00,1),(171372,34476,0.00,0.00,3),(171373,34476,0.00,0.00,4),(171374,34476,20.00,0.00,2),(171375,34476,0.00,0.00,5),(171376,34477,1.00,50000.00,1),(171377,34477,0.00,0.00,3),(171378,34477,0.00,0.00,4),(171379,34477,20.00,0.00,2),(171380,34477,0.00,0.00,5),(171381,34478,20.00,2500.00,1),(171382,34478,2.50,0.00,3),(171383,34478,2.00,0.00,4),(171384,34478,20.00,0.00,2),(171385,34478,0.00,0.00,5),(171386,34479,20.00,2500.00,1),(171387,34479,0.00,0.00,3),(171388,34479,2.00,0.00,4),(171389,34479,20.00,0.00,2),(171390,34479,0.00,0.00,5),(171391,34480,20.00,0.00,1),(171392,34480,0.00,0.00,3),(171393,34480,2.00,0.00,4),(171394,34480,20.00,0.00,2),(171395,34480,1.00,0.00,5),(171396,34481,20.00,0.00,1),(171397,34481,0.00,0.00,3),(171398,34481,0.00,0.00,4),(171399,34481,20.00,0.00,2),(171400,34481,0.00,0.00,5),(171401,34482,20.00,0.00,1),(171402,34482,2.50,0.00,3),(171403,34482,2.00,0.00,4),(171404,34482,20.00,0.00,2),(171405,34482,0.00,0.00,5),(171406,34483,20.00,0.00,1),(171407,34483,0.00,0.00,3),(171408,34483,2.00,0.00,4),(171409,34483,20.00,0.00,2),(171410,34483,0.00,0.00,5),(171411,34484,20.00,0.00,1),(171412,34484,0.00,0.00,3),(171413,34484,2.00,0.00,4),(171414,34484,20.00,0.00,2),(171415,34484,1.00,0.00,5),(171416,34485,20.00,0.00,1),(171417,34485,0.00,0.00,3),(171418,34485,0.00,0.00,4),(171419,34485,20.00,0.00,2),(171420,34485,0.00,0.00,5),(171421,34486,2.00,20000.00,1),(171422,34486,2.50,0.00,3),(171423,34486,2.00,0.00,4),(171424,34486,20.00,0.00,2),(171425,34486,0.00,0.00,5),(171426,34487,2.00,50000.00,1),(171427,34487,0.00,0.00,3),(171428,34487,2.00,0.00,4),(171429,34487,20.00,0.00,2),(171430,34487,0.00,0.00,5),(171431,34488,2.00,50000.00,1),(171432,34488,0.00,0.00,3),(171433,34488,2.00,0.00,4),(171434,34488,20.00,0.00,2),(171435,34488,1.00,0.00,5),(171436,34489,2.00,50000.00,1),(171437,34489,0.00,0.00,3),(171438,34489,0.00,0.00,4),(171439,34489,20.00,0.00,2),(171440,34489,0.00,0.00,5),(171441,34490,2.00,50000.00,1),(171442,34490,0.00,0.00,3),(171443,34490,0.00,0.00,4),(171444,34490,20.00,0.00,2),(171445,34490,0.00,0.00,5),(171446,34491,2.00,75000.00,1),(171447,34491,0.00,0.00,3),(171448,34491,0.00,0.00,4),(171449,34491,20.00,0.00,2),(171450,34491,0.00,0.00,5),(171451,34492,10.00,30000.00,1),(171452,34492,0.00,0.00,3),(171453,34492,0.00,0.00,4),(171454,34492,20.00,0.00,2),(171455,34492,0.00,0.00,5),(171456,34493,20.00,120000.00,1),(171457,34493,2.50,0.00,3),(171458,34493,2.00,0.00,4),(171459,34493,20.00,0.00,2),(171460,34493,0.00,0.00,5),(171461,34494,20.00,120000.00,1),(171462,34494,0.00,0.00,3),(171463,34494,2.00,0.00,4),(171464,34494,20.00,0.00,2),(171465,34494,0.00,0.00,5),(171466,34495,20.00,120000.00,1),(171467,34495,0.00,0.00,3),(171468,34495,2.00,0.00,4),(171469,34495,20.00,0.00,2),(171470,34495,1.00,0.00,5),(171471,34496,20.00,120000.00,1),(171472,34496,0.00,0.00,3),(171473,34496,0.00,0.00,4),(171474,34496,20.00,0.00,2),(171475,34496,0.00,0.00,5),(171476,34497,10.00,120000.00,1),(171477,34497,0.00,0.00,3),(171478,34497,0.00,0.00,4),(171479,34497,20.00,0.00,2),(171480,34497,0.00,0.00,5),(171481,34498,10.00,180000.00,1),(171482,34498,0.00,0.00,3),(171483,34498,0.00,0.00,4),(171484,34498,20.00,0.00,2),(171485,34498,0.00,0.00,5),(171486,34499,10.00,120000.00,1),(171487,34499,0.00,0.00,3),(171488,34499,2.00,0.00,4),(171489,34499,20.00,0.00,2),(171490,34499,1.00,0.00,5),(171491,34500,10.00,120000.00,1),(171492,34500,0.00,0.00,3),(171493,34500,0.00,0.00,4),(171494,34500,20.00,0.00,2),(171495,34500,0.00,0.00,5),(171496,34501,2.00,120000.00,1),(171497,34501,0.00,0.00,3),(171498,34501,0.00,0.00,4),(171499,34501,20.00,0.00,2),(171500,34501,0.00,0.00,5),(171501,34502,2.00,180000.00,1),(171502,34502,0.00,0.00,3),(171503,34502,0.00,0.00,4),(171504,34502,20.00,0.00,2),(171505,34502,0.00,0.00,5),(171506,34503,30.00,2500.00,1),(171507,34503,2.50,0.00,3),(171508,34503,2.00,0.00,4),(171509,34503,20.00,0.00,2),(171510,34503,0.00,0.00,5),(171511,34504,30.00,2500.00,1),(171512,34504,0.00,0.00,3),(171513,34504,2.00,0.00,4),(171514,34504,20.00,0.00,2),(171515,34504,0.00,0.00,5),(171516,34505,30.00,2500.00,1),(171517,34505,0.00,0.00,3),(171518,34505,2.00,0.00,4),(171519,34505,20.00,0.00,2),(171520,34505,1.00,0.00,5),(171521,34506,30.00,2500.00,1),(171522,34506,0.00,0.00,3),(171523,34506,0.00,0.00,4),(171524,34506,20.00,0.00,2),(171525,34506,0.00,0.00,5),(171526,34507,30.00,5000.00,1),(171527,34507,0.00,0.00,3),(171528,34507,0.00,0.00,4),(171529,34507,20.00,0.00,2),(171530,34507,0.00,0.00,5),(171531,34508,30.00,5000.00,1),(171532,34508,2.50,0.00,3),(171533,34508,2.00,0.00,4),(171534,34508,20.00,0.00,2),(171535,34508,0.00,0.00,5),(171536,34509,30.00,5000.00,1),(171537,34509,0.00,0.00,3),(171538,34509,2.00,0.00,4),(171539,34509,20.00,0.00,2),(171540,34509,0.00,0.00,5),(171541,34510,30.00,5000.00,1),(171542,34510,0.00,0.00,3),(171543,34510,2.00,0.00,4),(171544,34510,20.00,0.00,2),(171545,34510,1.00,0.00,5),(171546,34511,30.00,5000.00,1),(171547,34511,0.00,0.00,3),(171548,34511,0.00,0.00,4),(171549,34511,20.00,0.00,2),(171550,34511,0.00,0.00,5),(171551,34512,30.00,10000.00,1),(171552,34512,0.00,0.00,3),(171553,34512,0.00,0.00,4),(171554,34512,20.00,0.00,2),(171555,34512,0.00,0.00,5),(171556,34513,20.00,2500.00,1),(171557,34513,2.50,0.00,3),(171558,34513,2.00,0.00,4),(171559,34513,20.00,0.00,2),(171560,34513,0.00,0.00,5),(171561,34514,20.00,2500.00,1),(171562,34514,0.00,0.00,3),(171563,34514,2.00,0.00,4),(171564,34514,20.00,0.00,2),(171565,34514,0.00,0.00,5),(171566,34515,20.00,2500.00,1),(171567,34515,0.00,0.00,3),(171568,34515,2.00,0.00,4),(171569,34515,20.00,0.00,2),(171570,34515,1.00,0.00,5),(171571,34516,20.00,0.00,1),(171572,34516,0.00,0.00,3),(171573,34516,2.00,0.00,4),(171574,34516,20.00,0.00,2),(171575,34516,1.00,0.00,5),(171576,34517,10.00,0.00,1),(171577,34517,0.00,0.00,3),(171578,34517,0.00,0.00,4),(171579,34517,20.00,0.00,2),(171580,34517,0.00,0.00,5),(171581,34518,10.00,0.00,1),(171582,34518,0.00,0.00,3),(171583,34518,0.00,0.00,4),(171584,34518,20.00,0.00,2),(171585,34518,0.00,0.00,5),(171586,34519,10.00,1000.00,1),(171587,34519,2.50,0.00,3),(171588,34519,2.00,0.00,4),(171589,34519,20.00,0.00,2),(171590,34519,0.00,0.00,5),(171591,34520,10.00,1000.00,1),(171592,34520,0.00,0.00,3),(171593,34520,2.00,0.00,4),(171594,34520,20.00,0.00,2),(171595,34520,0.00,0.00,5),(171596,34521,10.00,1000.00,1),(171597,34521,0.00,0.00,3),(171598,34521,2.00,0.00,4),(171599,34521,20.00,0.00,2),(171600,34521,1.00,0.00,5),(171601,34522,10.00,1000.00,1),(171602,34522,0.00,0.00,3),(171603,34522,0.00,0.00,4),(171604,34522,20.00,0.00,2),(171605,34522,0.00,0.00,5),(171606,34523,5.00,2500.00,1),(171607,34523,2.50,0.00,3),(171608,34523,2.00,0.00,4),(171609,34523,20.00,0.00,2),(171610,34523,0.00,0.00,5),(171611,34524,5.00,2500.00,1),(171612,34524,0.00,0.00,3),(171613,34524,2.00,0.00,4),(171614,34524,20.00,0.00,2),(171615,34524,0.00,0.00,5),(171616,34525,5.00,2500.00,1),(171617,34525,0.00,0.00,3),(171618,34525,2.00,0.00,4),(171619,34525,20.00,0.00,2),(171620,34525,1.00,0.00,5),(171621,34526,10.00,2500.00,1),(171622,34526,0.00,0.00,3),(171623,34526,2.00,0.00,4),(171624,34526,20.00,0.00,2),(171625,34526,1.00,0.00,5),(171626,34527,10.00,2500.00,1),(171627,34527,0.00,0.00,3),(171628,34527,0.00,0.00,4),(171629,34527,20.00,0.00,2),(171630,34527,0.00,0.00,5),(171631,34528,10.00,5000.00,1),(171632,34528,0.00,0.00,3),(171633,34528,0.00,0.00,4),(171634,34528,20.00,0.00,2),(171635,34528,0.00,0.00,5),(171636,34529,20.00,2500.00,1),(171637,34529,0.00,0.00,3),(171638,34529,2.00,0.00,4),(171639,34529,20.00,0.00,2),(171640,34529,1.00,0.00,5),(171641,34530,10.00,2500.00,1),(171642,34530,0.00,0.00,3),(171643,34530,0.00,0.00,4),(171644,34530,20.00,0.00,2),(171645,34530,0.00,0.00,5),(171646,34531,5.00,20000.00,1),(171647,34531,2.50,0.00,3),(171648,34531,2.00,0.00,4),(171649,34531,20.00,0.00,2),(171650,34531,1.00,0.00,5),(171651,34532,5.00,20000.00,1),(171652,34532,0.00,0.00,3),(171653,34532,2.00,0.00,4),(171654,34532,20.00,0.00,2),(171655,34532,0.00,0.00,5),(171656,34533,5.00,20000.00,1),(171657,34533,0.00,0.00,3),(171658,34533,2.00,0.00,4),(171659,34533,20.00,0.00,2),(171660,34533,1.00,0.00,5),(171661,34534,10.00,20000.00,1),(171662,34534,0.00,0.00,3),(171663,34534,2.00,0.00,4),(171664,34534,20.00,0.00,2),(171665,34534,1.00,0.00,5),(171666,34535,10.00,20000.00,1),(171667,34535,0.00,0.00,3),(171668,34535,0.00,0.00,4),(171669,34535,20.00,0.00,2),(171670,34535,0.00,0.00,5),(171671,34536,10.00,30000.00,1),(171672,34536,0.00,0.00,3),(171673,34536,0.00,0.00,4),(171674,34536,20.00,0.00,2),(171675,34536,0.00,0.00,5),(171676,34537,1.00,20000.00,1),(171677,34537,2.50,0.00,3),(171678,34537,2.00,0.00,4),(171679,34537,20.00,0.00,2),(171680,34537,0.00,0.00,5),(171681,34538,1.00,20000.00,1),(171682,34538,0.00,0.00,3),(171683,34538,2.00,0.00,4),(171684,34538,20.00,0.00,2),(171685,34538,0.00,0.00,5),(171686,34539,1.00,20000.00,1),(171687,34539,0.00,0.00,3),(171688,34539,2.00,0.00,4),(171689,34539,20.00,0.00,2),(171690,34539,1.00,0.00,5),(171691,34540,1.00,20000.00,1),(171692,34540,0.00,0.00,3),(171693,34540,0.00,0.00,4),(171694,34540,20.00,0.00,2),(171695,34540,0.00,0.00,5),(171696,34541,2.00,20000.00,1),(171697,34541,0.00,0.00,3),(171698,34541,0.00,0.00,4),(171699,34541,20.00,0.00,2),(171700,34541,0.00,0.00,5),(171701,34542,2.00,75000.00,1),(171702,34542,0.00,0.00,3),(171703,34542,0.00,0.00,4),(171704,34542,20.00,0.00,2),(171705,34542,0.00,0.00,5),(171706,34543,1.00,20000.00,1),(171707,34543,2.50,0.00,3),(171708,34543,2.00,0.00,4),(171709,34543,20.00,0.00,2),(171710,34543,0.00,0.00,5),(171711,34544,1.00,20000.00,1),(171712,34544,0.00,0.00,3),(171713,34544,2.00,0.00,4),(171714,34544,20.00,0.00,2),(171715,34544,0.00,0.00,5),(171716,34545,1.00,20000.00,1),(171717,34545,0.00,0.00,3),(171718,34545,2.00,0.00,4),(171719,34545,20.00,0.00,2),(171720,34545,1.00,0.00,5),(171721,34546,1.00,20000.00,1),(171722,34546,0.00,0.00,3),(171723,34546,0.00,0.00,4),(171724,34546,20.00,0.00,2),(171725,34546,0.00,0.00,5),(171726,34547,2.00,50000.00,1),(171727,34547,0.00,0.00,3),(171728,34547,0.00,0.00,4),(171729,34547,20.00,0.00,2),(171730,34547,0.00,0.00,5),(171731,34548,2.00,75000.00,1),(171732,34548,0.00,0.00,3),(171733,34548,0.00,0.00,4),(171734,34548,20.00,0.00,2),(171735,34548,0.00,0.00,5),(171736,34549,2.00,50000.00,1),(171737,34549,0.00,0.00,3),(171738,34549,0.00,0.00,4),(171739,34549,20.00,0.00,2),(171740,34549,0.00,0.00,5),(171741,34550,0.00,0.00,1),(171742,34550,0.00,0.00,3),(171743,34550,0.00,0.00,4),(171744,34550,20.00,0.00,2),(171745,34550,0.00,0.00,5),(171746,34551,20.00,75000.00,1),(171747,34551,0.00,0.00,3),(171748,34551,0.00,0.00,4),(171749,34551,20.00,0.00,2),(171750,34551,0.00,0.00,5),(171751,34552,1.00,50000.00,1),(171752,34552,0.00,0.00,3),(171753,34552,0.00,0.00,4),(171754,34552,20.00,0.00,2),(171755,34552,0.00,0.00,5),(171756,34553,0.00,0.00,1),(171757,34553,0.00,0.00,3),(171758,34553,0.00,0.00,4),(171759,34553,20.00,0.00,2),(171760,34553,0.00,0.00,5),(171761,34554,20.00,75000.00,1),(171762,34554,0.00,0.00,3),(171763,34554,0.00,0.00,4),(171764,34554,20.00,0.00,2),(171765,34554,0.00,0.00,5),(184546,37170,10.00,5000.00,1),(184547,37170,10.00,0.00,3),(184548,37170,2.00,0.00,4),(184549,37170,20.00,0.00,2),(184550,37170,0.00,0.00,5),(184551,37171,10.00,5000.00,1),(184552,37171,0.00,0.00,3),(184553,37171,0.00,0.00,4),(184554,37171,20.00,0.00,2),(184555,37171,0.00,0.00,5),(184556,37172,10.00,20000.00,1),(184557,37172,0.00,0.00,3),(184558,37172,0.00,0.00,4),(184559,37172,20.00,0.00,2),(184560,37172,0.00,0.00,5),(184561,37173,10.00,5000.00,1),(184562,37173,10.00,10000000.00,3),(184563,37173,2.00,0.00,4),(184564,37173,20.00,0.00,2),(184565,37173,1.00,0.00,5),(184566,37174,10.00,2500.00,1),(184567,37174,10.00,0.00,3),(184568,37174,2.00,0.00,4),(184569,37174,20.00,0.00,2),(184570,37174,0.00,0.00,5),(184571,37175,10.00,2500.00,1),(184572,37175,10.00,10000000.00,3),(184573,37175,2.00,0.00,4),(184574,37175,20.00,0.00,2),(184575,37175,1.00,0.00,5),(184576,37176,10.00,2500.00,1),(184577,37176,0.00,0.00,3),(184578,37176,0.00,0.00,4),(184579,37176,20.00,0.00,2),(184580,37176,0.00,0.00,5),(184581,37177,10.00,5000.00,1),(184582,37177,0.00,0.00,3),(184583,37177,0.00,0.00,4),(184584,37177,20.00,0.00,2),(184585,37177,0.00,0.00,5),(184586,37178,10.00,5000.00,1),(184587,37178,10.00,0.00,3),(184588,37178,2.00,0.00,4),(184589,37178,20.00,0.00,2),(184590,37178,0.00,0.00,5),(184591,37179,10.00,5000.00,1),(184592,37179,10.00,10000000.00,3),(184593,37179,2.00,0.00,4),(184594,37179,20.00,0.00,2),(184595,37179,1.00,0.00,5),(184596,37180,10.00,5000.00,1),(184597,37180,0.00,0.00,3),(184598,37180,0.00,0.00,4),(184599,37180,20.00,0.00,2),(184600,37180,0.00,0.00,5),(184601,37181,10.00,100000.00,1),(184602,37181,10.00,0.00,3),(184603,37181,2.00,0.00,4),(184604,37181,20.00,0.00,2),(184605,37181,0.00,0.00,5),(184606,37182,10.00,100000.00,1),(184607,37182,10.00,10000000.00,3),(184608,37182,2.00,0.00,4),(184609,37182,20.00,0.00,2),(184610,37182,1.00,0.00,5),(184611,37183,10.00,100000.00,1),(184612,37183,0.00,0.00,3),(184613,37183,0.00,0.00,4),(184614,37183,20.00,0.00,2),(184615,37183,0.00,0.00,5),(184616,37184,10.00,200000.00,1),(184617,37184,0.00,0.00,3),(184618,37184,0.00,0.00,4),(184619,37184,20.00,0.00,2),(184620,37184,0.00,0.00,5),(184621,37185,20.00,2500.00,1),(184622,37185,10.00,0.00,3),(184623,37185,2.00,0.00,4),(184624,37185,20.00,0.00,2),(184625,37185,0.00,0.00,5),(184626,37186,20.00,0.00,1),(184627,37186,10.00,10000000.00,3),(184628,37186,2.00,0.00,4),(184629,37186,20.00,0.00,2),(184630,37186,1.00,0.00,5),(184631,37187,20.00,0.00,1),(184632,37187,0.00,0.00,3),(184633,37187,0.00,0.00,4),(184634,37187,20.00,0.00,2),(184635,37187,0.00,0.00,5),(184636,37188,20.00,0.00,1),(184637,37188,10.00,0.00,3),(184638,37188,2.00,0.00,4),(184639,37188,20.00,0.00,2),(184640,37188,0.00,0.00,5),(184641,37189,20.00,0.00,1),(184642,37189,10.00,10000000.00,3),(184643,37189,2.00,0.00,4),(184644,37189,20.00,0.00,2),(184645,37189,1.00,0.00,5),(184646,37190,20.00,0.00,1),(184647,37190,0.00,0.00,3),(184648,37190,0.00,0.00,4),(184649,37190,20.00,0.00,2),(184650,37190,0.00,0.00,5),(184651,37191,2.00,20000.00,1),(184652,37191,10.00,0.00,3),(184653,37191,2.00,0.00,4),(184654,37191,20.00,0.00,2),(184655,37191,0.00,0.00,5),(184656,37192,2.00,20000.00,1),(184657,37192,10.00,10000000.00,3),(184658,37192,2.00,0.00,4),(184659,37192,20.00,0.00,2),(184660,37192,1.00,0.00,5),(184661,37193,2.00,20000.00,1),(184662,37193,0.00,0.00,3),(184663,37193,0.00,0.00,4),(184664,37193,20.00,0.00,2),(184665,37193,0.00,0.00,5),(184666,37194,2.00,20000.00,1),(184667,37194,0.00,0.00,3),(184668,37194,0.00,0.00,4),(184669,37194,20.00,0.00,2),(184670,37194,0.00,0.00,5),(184671,37195,2.00,75000.00,1),(184672,37195,0.00,0.00,3),(184673,37195,0.00,0.00,4),(184674,37195,20.00,0.00,2),(184675,37195,0.00,0.00,5),(184676,37196,20.00,120000.00,1),(184677,37196,10.00,0.00,3),(184678,37196,2.00,0.00,4),(184679,37196,20.00,0.00,2),(184680,37196,0.00,0.00,5),(184681,37197,20.00,120000.00,1),(184682,37197,10.00,10000000.00,3),(184683,37197,2.00,0.00,4),(184684,37197,20.00,0.00,2),(184685,37197,1.00,0.00,5),(184686,37198,20.00,120000.00,1),(184687,37198,0.00,0.00,3),(184688,37198,0.00,0.00,4),(184689,37198,20.00,0.00,2),(184690,37198,0.00,0.00,5),(184691,37199,10.00,120000.00,1),(184692,37199,0.00,0.00,3),(184693,37199,0.00,0.00,4),(184694,37199,20.00,0.00,2),(184695,37199,0.00,0.00,5),(184696,37200,10.00,180000.00,1),(184697,37200,0.00,0.00,3),(184698,37200,0.00,0.00,4),(184699,37200,20.00,0.00,2),(184700,37200,0.00,0.00,5),(184701,37201,10.00,120000.00,1),(184702,37201,10.00,10000000.00,3),(184703,37201,2.00,0.00,4),(184704,37201,20.00,0.00,2),(184705,37201,1.00,0.00,5),(184706,37202,10.00,120000.00,1),(184707,37202,0.00,0.00,3),(184708,37202,0.00,0.00,4),(184709,37202,20.00,0.00,2),(184710,37202,0.00,0.00,5),(184711,37203,2.00,120000.00,1),(184712,37203,0.00,0.00,3),(184713,37203,0.00,0.00,4),(184714,37203,20.00,0.00,2),(184715,37203,0.00,0.00,5),(184716,37204,2.00,180000.00,1),(184717,37204,0.00,0.00,3),(184718,37204,0.00,0.00,4),(184719,37204,20.00,0.00,2),(184720,37204,0.00,0.00,5),(184721,37205,30.00,2500.00,1),(184722,37205,10.00,0.00,3),(184723,37205,2.00,0.00,4),(184724,37205,20.00,0.00,2),(184725,37205,0.00,0.00,5),(184726,37206,30.00,2500.00,1),(184727,37206,10.00,10000000.00,3),(184728,37206,2.00,0.00,4),(184729,37206,20.00,0.00,2),(184730,37206,1.00,0.00,5),(184731,37207,30.00,2500.00,1),(184732,37207,0.00,0.00,3),(184733,37207,0.00,0.00,4),(184734,37207,20.00,0.00,2),(184735,37207,0.00,0.00,5),(184736,37208,30.00,5000.00,1),(184737,37208,0.00,0.00,3),(184738,37208,0.00,0.00,4),(184739,37208,20.00,0.00,2),(184740,37208,0.00,0.00,5),(184741,37209,30.00,5000.00,1),(184742,37209,10.00,0.00,3),(184743,37209,2.00,0.00,4),(184744,37209,20.00,0.00,2),(184745,37209,0.00,0.00,5),(184746,37210,30.00,5000.00,1),(184747,37210,10.00,10000000.00,3),(184748,37210,2.00,0.00,4),(184749,37210,20.00,0.00,2),(184750,37210,1.00,0.00,5),(184751,37211,30.00,5000.00,1),(184752,37211,0.00,0.00,3),(184753,37211,0.00,0.00,4),(184754,37211,20.00,0.00,2),(184755,37211,0.00,0.00,5),(184756,37212,30.00,10000.00,1),(184757,37212,0.00,0.00,3),(184758,37212,0.00,0.00,4),(184759,37212,20.00,0.00,2),(184760,37212,0.00,0.00,5),(184761,37213,20.00,2500.00,1),(184762,37213,10.00,0.00,3),(184763,37213,2.00,0.00,4),(184764,37213,20.00,0.00,2),(184765,37213,0.00,0.00,5),(184766,37214,20.00,2500.00,1),(184767,37214,10.00,10000000.00,3),(184768,37214,2.00,0.00,4),(184769,37214,20.00,0.00,2),(184770,37214,1.00,0.00,5),(184771,37215,20.00,0.00,1),(184772,37215,10.00,10000000.00,3),(184773,37215,2.00,0.00,4),(184774,37215,20.00,0.00,2),(184775,37215,1.00,0.00,5),(184776,37216,10.00,0.00,1),(184777,37216,0.00,0.00,3),(184778,37216,0.00,0.00,4),(184779,37216,20.00,0.00,2),(184780,37216,0.00,0.00,5),(184781,37217,10.00,1000.00,1),(184782,37217,10.00,0.00,3),(184783,37217,2.00,0.00,4),(184784,37217,20.00,0.00,2),(184785,37217,0.00,0.00,5),(184786,37218,10.00,1000.00,1),(184787,37218,10.00,10000000.00,3),(184788,37218,2.00,0.00,4),(184789,37218,20.00,0.00,2),(184790,37218,1.00,0.00,5),(184791,37219,10.00,1000.00,1),(184792,37219,0.00,0.00,3),(184793,37219,0.00,0.00,4),(184794,37219,20.00,0.00,2),(184795,37219,0.00,0.00,5),(184796,37220,5.00,2500.00,1),(184797,37220,2.00,0.00,3),(184798,37220,0.00,0.00,4),(184799,37220,20.00,0.00,2),(184800,37220,0.00,0.00,5),(184801,37221,5.00,2500.00,1),(184802,37221,10.00,10000000.00,3),(184803,37221,2.00,0.00,4),(184804,37221,20.00,0.00,2),(184805,37221,1.00,0.00,5),(184806,37222,10.00,2500.00,1),(184807,37222,10.00,10000000.00,3),(184808,37222,2.00,0.00,4),(184809,37222,20.00,0.00,2),(184810,37222,1.00,0.00,5),(184811,37223,10.00,2500.00,1),(184812,37223,0.00,0.00,3),(184813,37223,0.00,0.00,4),(184814,37223,20.00,0.00,2),(184815,37223,0.00,0.00,5),(184816,37224,10.00,5000.00,1),(184817,37224,0.00,0.00,3),(184818,37224,0.00,0.00,4),(184819,37224,20.00,0.00,2),(184820,37224,0.00,0.00,5),(184821,37225,20.00,2500.00,1),(184822,37225,10.00,10000000.00,3),(184823,37225,2.00,0.00,4),(184824,37225,20.00,0.00,2),(184825,37225,1.00,0.00,5),(184826,37226,10.00,2500.00,1),(184827,37226,0.00,0.00,3),(184828,37226,0.00,0.00,4),(184829,37226,20.00,0.00,2),(184830,37226,0.00,0.00,5),(184831,37228,5.00,20000.00,1),(184832,37228,10.00,0.00,3),(184833,37228,2.00,0.00,4),(184834,37228,20.00,0.00,2),(184835,37228,0.00,0.00,5),(184836,37229,5.00,20000.00,1),(184837,37229,10.00,10000000.00,3),(184838,37229,2.00,0.00,4),(184839,37229,20.00,0.00,2),(184840,37229,1.00,0.00,5),(184841,37230,10.00,20000.00,1),(184842,37230,10.00,10000000.00,3),(184843,37230,2.00,0.00,4),(184844,37230,20.00,0.00,2),(184845,37230,1.00,0.00,5),(184846,37231,10.00,20000.00,1),(184847,37231,0.00,0.00,3),(184848,37231,0.00,0.00,4),(184849,37231,20.00,0.00,2),(184850,37231,0.00,0.00,5),(184851,37232,10.00,30000.00,1),(184852,37232,0.00,0.00,3),(184853,37232,0.00,0.00,4),(184854,37232,20.00,0.00,2),(184855,37232,0.00,0.00,5),(184856,37233,1.00,20000.00,1),(184857,37233,10.00,0.00,3),(184858,37233,2.00,0.00,4),(184859,37233,20.00,0.00,2),(184860,37233,0.00,0.00,5),(184861,37234,1.00,20000.00,1),(184862,37234,10.00,10000000.00,3),(184863,37234,2.00,0.00,4),(184864,37234,20.00,0.00,2),(184865,37234,1.00,0.00,5),(184866,37235,1.00,20000.00,1),(184867,37235,0.00,0.00,3),(184868,37235,0.00,0.00,4),(184869,37235,20.00,0.00,2),(184870,37235,0.00,0.00,5),(184871,37236,2.00,20000.00,1),(184872,37236,0.00,0.00,3),(184873,37236,0.00,0.00,4),(184874,37236,20.00,0.00,2),(184875,37236,0.00,0.00,5),(184876,37237,2.00,75000.00,1),(184877,37237,0.00,0.00,3),(184878,37237,0.00,0.00,4),(184879,37237,20.00,0.00,2),(184880,37237,0.00,0.00,5),(184881,37238,1.00,20000.00,1),(184882,37238,10.00,0.00,3),(184883,37238,2.00,0.00,4),(184884,37238,20.00,0.00,2),(184885,37238,0.00,0.00,5),(184886,37239,1.00,50000.00,1),(184887,37239,10.00,10000000.00,3),(184888,37239,2.00,0.00,4),(184889,37239,20.00,0.00,2),(184890,37239,1.00,0.00,5),(184891,37240,1.00,50000.00,1),(184892,37240,0.00,0.00,3),(184893,37240,0.00,0.00,4),(184894,37240,20.00,0.00,2),(184895,37240,0.00,0.00,5),(184896,37241,2.00,50000.00,1),(184897,37241,0.00,0.00,3),(184898,37241,0.00,0.00,4),(184899,37241,20.00,0.00,2),(184900,37241,0.00,0.00,5),(184901,37242,2.00,75000.00,1),(184902,37242,0.00,0.00,3),(184903,37242,0.00,0.00,4),(184904,37242,20.00,0.00,2),(184905,37242,0.00,0.00,5),(201576,40588,5.00,5000.00,1),(201577,40588,0.00,0.00,3),(201578,40588,0.00,0.00,4),(201579,40588,20.00,50000.00,2),(201580,40588,0.00,0.00,5),(201581,40589,2.55,50000.00,1),(201582,40589,0.00,0.00,3),(201583,40589,0.00,0.00,4),(201584,40589,20.00,45000.00,2),(201585,40589,0.00,0.00,5),(201586,40590,4.50,450000.00,1),(201587,40590,0.00,0.00,3),(201588,40590,0.00,0.00,4),(201589,40590,20.00,45000.00,2),(201590,40590,0.00,0.00,5),(201591,40591,4.78,40000.00,1),(201592,40591,0.00,0.00,3),(201593,40591,0.00,0.00,4),(201594,40591,20.00,6000.00,2),(201595,40591,0.00,0.00,5),(201596,40592,10.00,5000.00,1),(201597,40592,10.00,10000000.00,3),(201598,40592,2.00,0.00,4),(201599,40592,20.00,0.00,2),(201600,40592,1.00,0.00,5),(201601,40593,10.00,5000.00,1),(201602,40593,0.00,0.00,3),(201603,40593,0.00,0.00,4),(201604,40593,20.00,0.00,2),(201605,40593,0.00,0.00,5),(201606,40594,10.00,20000.00,1),(201607,40594,0.00,0.00,3),(201608,40594,0.00,0.00,4),(201609,40594,20.00,0.00,2),(201610,40594,0.00,0.00,5),(201611,40595,10.00,2500.00,1),(201612,40595,10.00,10000000.00,3),(201613,40595,2.00,0.00,4),(201614,40595,20.00,0.00,2),(201615,40595,1.00,0.00,5),(201616,40596,10.00,2500.00,1),(201617,40596,0.00,0.00,3),(201618,40596,0.00,0.00,4),(201619,40596,20.00,0.00,2),(201620,40596,0.00,0.00,5),(201621,40597,10.00,5000.00,1),(201622,40597,0.00,0.00,3),(201623,40597,0.00,0.00,4),(201624,40597,20.00,0.00,2),(201625,40597,0.00,0.00,5),(201626,40598,10.00,5000.00,1),(201627,40598,10.00,10000000.00,3),(201628,40598,2.00,0.00,4),(201629,40598,20.00,0.00,2),(201630,40598,1.00,0.00,5),(201631,40599,10.00,5000.00,1),(201632,40599,0.00,0.00,3),(201633,40599,0.00,0.00,4),(201634,40599,20.00,0.00,2),(201635,40599,0.00,0.00,5),(201636,40600,5.50,6000.00,1),(201637,40600,0.00,0.00,3),(201638,40600,0.00,0.00,4),(201639,40600,20.00,31000.00,2),(201640,40600,0.00,0.00,5),(201641,40601,2.00,8000.00,1),(201642,40601,0.00,0.00,3),(201643,40601,0.00,0.00,4),(201644,40601,20.00,31000.00,2),(201645,40601,0.00,0.00,5),(201646,40602,10.00,100000.00,1),(201647,40602,10.00,10000000.00,3),(201648,40602,2.00,0.00,4),(201649,40602,20.00,0.00,2),(201650,40602,1.00,0.00,5),(201651,40603,10.00,100000.00,1),(201652,40603,0.00,0.00,3),(201653,40603,0.00,0.00,4),(201654,40603,20.00,0.00,2),(201655,40603,0.00,0.00,5),(201656,40604,10.00,200000.00,1),(201657,40604,0.00,0.00,3),(201658,40604,0.00,0.00,4),(201659,40604,20.00,0.00,2),(201660,40604,0.00,0.00,5),(201661,40605,1.00,5000000.00,1),(201662,40605,0.00,0.00,3),(201663,40605,0.00,0.00,4),(201664,40605,20.00,0.00,2),(201665,40605,0.00,0.00,5),(201666,40606,20.00,0.00,1),(201667,40606,10.00,10000000.00,3),(201668,40606,2.00,0.00,4),(201669,40606,20.00,0.00,2),(201670,40606,1.00,0.00,5),(201671,40607,20.00,0.00,1),(201672,40607,0.00,0.00,3),(201673,40607,0.00,0.00,4),(201674,40607,20.00,0.00,2),(201675,40607,0.00,0.00,5),(201676,40608,20.00,0.00,1),(201677,40608,10.00,10000000.00,3),(201678,40608,2.00,0.00,4),(201679,40608,20.00,0.00,2),(201680,40608,1.00,0.00,5),(201681,40609,2.00,50000.00,1),(201682,40609,0.00,0.00,3),(201683,40609,0.00,0.00,4),(201684,40609,20.00,0.00,2),(201685,40609,0.00,0.00,5),(201686,40610,2.00,50000.00,1),(201687,40610,0.00,0.00,3),(201688,40610,0.00,0.00,4),(201689,40610,20.00,0.00,2),(201690,40610,0.00,0.00,5),(201691,40611,2.00,75000.00,1),(201692,40611,0.00,0.00,3),(201693,40611,0.00,0.00,4),(201694,40611,20.00,0.00,2),(201695,40611,0.00,0.00,5),(201696,40612,2.00,50000.00,1),(201697,40612,10.00,10000000.00,3),(201698,40612,2.00,0.00,4),(201699,40612,20.00,0.00,2),(201700,40612,1.00,0.00,5),(201701,40613,10.00,30000.00,1),(201702,40613,0.00,0.00,3),(201703,40613,0.00,0.00,4),(201704,40613,20.00,0.00,2),(201705,40613,0.00,0.00,5),(201706,40614,20.00,120000.00,1),(201707,40614,10.00,10000000.00,3),(201708,40614,2.00,0.00,4),(201709,40614,20.00,0.00,2),(201710,40614,1.00,0.00,5),(201711,40615,20.00,120000.00,1),(201712,40615,0.00,0.00,3),(201713,40615,0.00,0.00,4),(201714,40615,20.00,0.00,2),(201715,40615,0.00,0.00,5),(201716,40616,10.00,120000.00,1),(201717,40616,0.00,0.00,3),(201718,40616,0.00,0.00,4),(201719,40616,20.00,0.00,2),(201720,40616,0.00,0.00,5),(201721,40617,10.00,180000.00,1),(201722,40617,0.00,0.00,3),(201723,40617,0.00,0.00,4),(201724,40617,20.00,0.00,2),(201725,40617,0.00,0.00,5),(201726,40618,10.00,120000.00,1),(201727,40618,10.00,10000000.00,3),(201728,40618,2.00,0.00,4),(201729,40618,20.00,0.00,2),(201730,40618,1.00,0.00,5),(201731,40619,10.00,120000.00,1),(201732,40619,0.00,0.00,3),(201733,40619,0.00,0.00,4),(201734,40619,20.00,0.00,2),(201735,40619,0.00,0.00,5),(201736,40620,2.00,120000.00,1),(201737,40620,0.00,0.00,3),(201738,40620,0.00,0.00,4),(201739,40620,20.00,0.00,2),(201740,40620,0.00,0.00,5),(201741,40621,2.00,180000.00,1),(201742,40621,0.00,0.00,3),(201743,40621,0.00,0.00,4),(201744,40621,20.00,0.00,2),(201745,40621,0.00,0.00,5),(201746,40622,30.00,2500.00,1),(201747,40622,10.00,10000000.00,3),(201748,40622,2.00,0.00,4),(201749,40622,20.00,0.00,2),(201750,40622,1.00,0.00,5),(201751,40623,30.00,2500.00,1),(201752,40623,0.00,0.00,3),(201753,40623,0.00,0.00,4),(201754,40623,20.00,0.00,2),(201755,40623,0.00,0.00,5),(201756,40624,30.00,5000.00,1),(201757,40624,0.00,0.00,3),(201758,40624,0.00,0.00,4),(201759,40624,20.00,0.00,2),(201760,40624,0.00,0.00,5),(201761,40625,30.00,5000.00,1),(201762,40625,10.00,10000000.00,3),(201763,40625,2.00,0.00,4),(201764,40625,20.00,0.00,2),(201765,40625,1.00,0.00,5),(201766,40626,30.00,5000.00,1),(201767,40626,0.00,0.00,3),(201768,40626,0.00,0.00,4),(201769,40626,20.00,0.00,2),(201770,40626,0.00,0.00,5),(201771,40627,30.00,10000.00,1),(201772,40627,0.00,0.00,3),(201773,40627,0.00,0.00,4),(201774,40627,20.00,0.00,2),(201775,40627,0.00,0.00,5),(201776,40628,20.00,2500.00,1),(201777,40628,10.00,10000000.00,3),(201778,40628,2.00,0.00,4),(201779,40628,20.00,0.00,2),(201780,40628,1.00,0.00,5),(201781,40629,10.00,2500.00,1),(201782,40629,0.00,0.00,3),(201783,40629,0.00,0.00,4),(201784,40629,20.00,0.00,2),(201785,40629,0.00,0.00,5),(201786,40630,10.00,0.00,1),(201787,40630,0.00,0.00,3),(201788,40630,0.00,0.00,4),(201789,40630,20.00,0.00,2),(201790,40630,0.00,0.00,5),(201791,40631,10.00,2500.00,1),(201792,40631,0.00,0.00,3),(201793,40631,0.00,0.00,4),(201794,40631,20.00,5000.00,2),(201795,40631,0.00,0.00,5),(201796,40632,10.00,1000.00,1),(201797,40632,10.00,10000000.00,3),(201798,40632,2.00,0.00,4),(201799,40632,20.00,0.00,2),(201800,40632,1.00,0.00,5),(201801,40633,10.00,1000.00,1),(201802,40633,0.00,0.00,3),(201803,40633,0.00,0.00,4),(201804,40633,20.00,0.00,2),(201805,40633,0.00,0.00,5),(201806,40634,5.00,2500.00,1),(201807,40634,10.00,10000000.00,3),(201808,40634,2.00,0.00,4),(201809,40634,20.00,0.00,2),(201810,40634,1.00,0.00,5),(201811,40635,10.00,2500.00,1),(201812,40635,10.00,10000000.00,3),(201813,40635,2.00,0.00,4),(201814,40635,20.00,0.00,2),(201815,40635,1.00,0.00,5),(201816,40636,10.00,2500.00,1),(201817,40636,0.00,0.00,3),(201818,40636,0.00,0.00,4),(201819,40636,20.00,0.00,2),(201820,40636,0.00,0.00,5),(201821,40637,10.00,5000.00,1),(201822,40637,0.00,0.00,3),(201823,40637,0.00,0.00,4),(201824,40637,20.00,0.00,2),(201825,40637,0.00,0.00,5),(201826,40638,20.00,2500.00,1),(201827,40638,10.00,10000000.00,3),(201828,40638,2.00,0.00,4),(201829,40638,20.00,0.00,2),(201830,40638,1.00,0.00,5),(201831,40639,10.00,2500.00,1),(201832,40639,0.00,0.00,3),(201833,40639,0.00,0.00,4),(201834,40639,20.00,0.00,2),(201835,40639,0.00,0.00,5),(201836,40640,10.00,20000.00,1),(201837,40640,10.00,10000000.00,3),(201838,40640,2.00,0.00,4),(201839,40640,20.00,0.00,2),(201840,40640,1.00,0.00,5),(201841,40641,5.00,20000.00,1),(201842,40641,10.00,10000000.00,3),(201843,40641,2.00,0.00,4),(201844,40641,0.00,0.00,2),(201845,40641,1.00,0.00,5),(201846,40642,10.00,20000.00,1),(201847,40642,0.00,0.00,3),(201848,40642,0.00,0.00,4),(201849,40642,20.00,0.00,2),(201850,40642,0.00,0.00,5),(201851,40643,10.00,30000.00,1),(201852,40643,0.00,0.00,3),(201853,40643,0.00,0.00,4),(201854,40643,20.00,0.00,2),(201855,40643,0.00,0.00,5),(201856,40644,2.00,50000.00,1),(201857,40644,0.00,0.00,3),(201858,40644,0.00,0.00,4),(201859,40644,20.00,0.00,2),(201860,40644,0.00,0.00,5),(201861,40645,2.00,75000.00,1),(201862,40645,0.00,0.00,3),(201863,40645,0.00,0.00,4),(201864,40645,20.00,0.00,2),(201865,40645,0.00,0.00,5),(201866,40646,2.00,50000.00,1),(201867,40646,0.00,0.00,3),(201868,40646,0.00,0.00,4),(201869,40646,20.00,0.00,2),(201870,40646,0.00,0.00,5),(201871,40647,0.00,0.00,1),(201872,40647,0.00,0.00,3),(201873,40647,0.00,0.00,4),(201874,40647,20.00,0.00,2),(201875,40647,0.00,0.00,5),(201876,40648,20.00,75000.00,1),(201877,40648,0.00,0.00,3),(201878,40648,0.00,0.00,4),(201879,40648,20.00,0.00,2),(201880,40648,0.00,0.00,5);
/*!40000 ALTER TABLE `tds_tax_rate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_info`
--

DROP TABLE IF EXISTS `user_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_info` (
  `USER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `FIRSTNAME` varchar(100) NOT NULL DEFAULT '',
  `LASTNAME` varchar(100) DEFAULT NULL,
  `USER_NAME` varchar(100) NOT NULL DEFAULT '',
  `PASSWORD` varchar(100) NOT NULL DEFAULT '',
  `NAME` varchar(150) DEFAULT NULL,
  `GENDER` int(10) unsigned NOT NULL DEFAULT '0' COMMENT ' 0-Male,1-Female,2 -Others',
  `ADDRESS` varchar(300) DEFAULT NULL,
  `CONTACT_NO` varchar(50) DEFAULT NULL,
  `EMAIL_ID` varchar(100) DEFAULT NULL,
  `USER_PHOTO` longblob,
  `ROLE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '1',
  `CREATED_BY` varchar(100) NOT NULL DEFAULT '',
  `CREATED_ON` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `MODIFIED_BY` varchar(100) NOT NULL DEFAULT '',
  `MODIFIED_ON` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `NOTES` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`USER_ID`),
  UNIQUE KEY `USER_NAME` (`USER_NAME`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_info`
--

LOCK TABLES `user_info` WRITE;
/*!40000 ALTER TABLE `user_info` DISABLE KEYS */;
INSERT INTO `user_info` VALUES (1,'Admin','Admin','admin','1uHL2oMM9bq8iOYovzJfbQ==','Admin',0,NULL,NULL,NULL,NULL,1,1,'','0000-00-00 00:00:00','','0000-00-00 00:00:00',NULL),(2,'Supervisor','','supervisor','vLmnpxoKJox2kEz6n67l7A==','supervisor',0,'','','','',2,1,'','0000-00-00 00:00:00','','0000-00-00 00:00:00','');
/*!40000 ALTER TABLE `user_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_project`
--

DROP TABLE IF EXISTS `user_project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_project` (
  `ROLE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ROLE_ID`,`PROJECT_ID`),
  KEY `FK_user_project_Project_id` (`PROJECT_ID`),
  CONSTRAINT `FK_user_project_PROJECT_ID` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`),
  CONSTRAINT `FK_user_project_Role_id` FOREIGN KEY (`ROLE_ID`) REFERENCES `user_role` (`USERROLE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_project`
--

LOCK TABLES `user_project` WRITE;
/*!40000 ALTER TABLE `user_project` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_project` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_rights`
--

DROP TABLE IF EXISTS `user_rights`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_rights` (
  `USER_ROLE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `ACTIVITY_ID` int(10) unsigned NOT NULL DEFAULT '0',
  KEY `FK_user_rights_UserRole` (`USER_ROLE_ID`),
  KEY `FK_user_rights_ActivityId` (`ACTIVITY_ID`),
  CONSTRAINT `FK_user_rights_activity_id` FOREIGN KEY (`ACTIVITY_ID`) REFERENCES `activitiy_rights` (`ACTIVITY_ID`),
  CONSTRAINT `FK_user_rights_UserRole` FOREIGN KEY (`USER_ROLE_ID`) REFERENCES `user_role` (`USERROLE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_rights`
--

LOCK TABLES `user_rights` WRITE;
/*!40000 ALTER TABLE `user_rights` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_rights` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_role`
--

DROP TABLE IF EXISTS `user_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_role` (
  `USERROLE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `USERROLE` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`USERROLE_ID`),
  UNIQUE KEY `USERROLE` (`USERROLE`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_role`
--

LOCK TABLES `user_role` WRITE;
/*!40000 ALTER TABLE `user_role` DISABLE KEYS */;
INSERT INTO `user_role` VALUES (1,'Admin'),(2,'Supervisor');
/*!40000 ALTER TABLE `user_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `usr_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `usr_code` varchar(70) DEFAULT NULL,
  `pass` varchar(200) DEFAULT NULL,
  `stat` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`usr_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin','admin',1);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `voucher_cc_trans`
--

DROP TABLE IF EXISTS `voucher_cc_trans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `voucher_cc_trans` (
  `VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `COST_CENTRE_TABLE` varchar(30) DEFAULT NULL,
  `COST_CENTRE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `SEQUENCE_NO` int(10) unsigned NOT NULL DEFAULT '0',
  `BRANCH_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`VOUCHER_ID`,`SEQUENCE_NO`,`BRANCH_ID`),
  KEY `FK_VOUCHER_COST_CENTRE_ID` (`COST_CENTRE_ID`),
  KEY `FK_voucher_cost_centre_voucher_ID` (`VOUCHER_ID`),
  KEY `FK_voucher_cc_trans_3` (`LEDGER_ID`),
  CONSTRAINT `FK_voucher_cc_trans_VOUCHER_ID` FOREIGN KEY (`VOUCHER_ID`) REFERENCES `voucher_master_trans` (`VOUCHER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `voucher_cc_trans`
--

LOCK TABLES `voucher_cc_trans` WRITE;
/*!40000 ALTER TABLE `voucher_cc_trans` DISABLE KEYS */;
/*!40000 ALTER TABLE `voucher_cc_trans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `voucher_fd_interest`
--

DROP TABLE IF EXISTS `voucher_fd_interest`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `voucher_fd_interest` (
  `FD_VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'Fd Voucher',
  `FD_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `FD_LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'Fd Ledger Id',
  `BK_INT_VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'Bank Interest voucher',
  `BK_INT_LEDGER_ID` varchar(45) NOT NULL DEFAULT '' COMMENT 'Bank Interest Ledger',
  PRIMARY KEY (`FD_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `voucher_fd_interest`
--

LOCK TABLES `voucher_fd_interest` WRITE;
/*!40000 ALTER TABLE `voucher_fd_interest` DISABLE KEYS */;
/*!40000 ALTER TABLE `voucher_fd_interest` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `voucher_master_trans`
--

DROP TABLE IF EXISTS `voucher_master_trans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `voucher_master_trans` (
  `VOUCHER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `VOUCHER_DATE` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `VOUCHER_NO` varchar(25) DEFAULT NULL,
  `VOUCHER_TYPE` varchar(2) DEFAULT NULL COMMENT 'RC -Receipts,PY -Payments,CN -Contra ,JR -Journal',
  `VOUCHER_SUB_TYPE` varchar(3) NOT NULL DEFAULT 'GN',
  `DONOR_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `PURPOSE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `CONTRIBUTION_TYPE` varchar(1) NOT NULL DEFAULT 'N' COMMENT 'NO,F-FIRST,S-SECOND/SUBSEQUENT\r\nDEFAULT (N)\r\n',
  `CONTRIBUTION_AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `CURRENCY_COUNTRY_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `EXCHANGE_RATE` decimal(15,2) NOT NULL DEFAULT '0.00',
  `EXCHANGE_COUNTRY_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `NARRATION` varchar(500) DEFAULT NULL,
  `STATUS` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'DEFAULT(1)\r\n0- INACTIVE/CANCELLED\r\n1-ACTIVE\r\n',
  `CREATED_ON` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `MODIFIED_ON` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CREATED_BY` int(10) unsigned NOT NULL DEFAULT '0',
  `MODIFIED_BY` int(10) unsigned NOT NULL DEFAULT '0',
  `CALCULATED_AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `ACTUAL_AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `NAME_ADDRESS` varchar(100) DEFAULT NULL,
  `BRANCH_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `CREATED_BY_NAME` varchar(50) NOT NULL DEFAULT '',
  `MODIFIED_BY_NAME` varchar(50) NOT NULL DEFAULT '',
  `CLIENT_REFERENCE_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `CLIENT_CODE` varchar(15) DEFAULT '',
  PRIMARY KEY (`VOUCHER_ID`,`BRANCH_ID`),
  KEY `FK_VOUCHER_MASTER_CREATED_BY` (`CREATED_BY`),
  KEY `FK_VOUCHER_MASTER_MODIFIED_BY` (`MODIFIED_BY`),
  KEY `FK_VOUCHER_MASTER_PROJECT_ID` (`VOUCHER_DATE`,`PROJECT_ID`) USING BTREE,
  KEY `FK_voucher_master_trans_PROJECT_ID` (`PROJECT_ID`),
  CONSTRAINT `FK_voucher_master_trans_PROJECT_ID` FOREIGN KEY (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `voucher_master_trans`
--

LOCK TABLES `voucher_master_trans` WRITE;
/*!40000 ALTER TABLE `voucher_master_trans` DISABLE KEYS */;
/*!40000 ALTER TABLE `voucher_master_trans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `voucher_number_format`
--

DROP TABLE IF EXISTS `voucher_number_format`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `voucher_number_format` (
  `NUMBER_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `NUMBER_FORMAT_ID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '1-Voucher Number,2-Receipt Number',
  `LAST_VOUCHER_NUMBER` varchar(45) NOT NULL DEFAULT '',
  `RUNNING_NUMBER` int(10) unsigned NOT NULL DEFAULT '0',
  `NUMBER_FORMAT` varchar(25) NOT NULL DEFAULT '',
  `MONTH` varchar(45) NOT NULL DEFAULT '' COMMENT 'Applicable from',
  `VOUCHER_MONTH` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'Current Voucher Month',
  `DURATION` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'Reset Month',
  `VOUCHER_YEAR` int(10) unsigned NOT NULL DEFAULT '0',
  `PROJECT_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`NUMBER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `voucher_number_format`
--

LOCK TABLES `voucher_number_format` WRITE;
/*!40000 ALTER TABLE `voucher_number_format` DISABLE KEYS */;
/*!40000 ALTER TABLE `voucher_number_format` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `voucher_trans`
--

DROP TABLE IF EXISTS `voucher_trans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `voucher_trans` (
  `VOUCHER_ID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'VOUCHER_ID,SEQUENCE_NO(PK)',
  `SEQUENCE_NO` int(10) unsigned NOT NULL DEFAULT '0',
  `LEDGER_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `AMOUNT` decimal(15,2) NOT NULL DEFAULT '0.00',
  `TRANS_MODE` varchar(2) DEFAULT NULL COMMENT 'CR-CREDIT\r\nDR-DEBIT\r\n',
  `LEDGER_FLAG` varchar(2) DEFAULT NULL COMMENT 'C-CASH\r\nB-BANK\r\nF-FIXED DEPOSIT\r\nJ-JOURNAL\r\nCA-CASH (CASH LEDGER TRANS)\r\nBK-BANK(BANK LEDGER TRANS) \r\nFD-FIXED DEPOSIT(FD LEDGER TRANS)\r\n',
  `CHEQUE_NO` varchar(25) DEFAULT NULL,
  `MATERIALIZED_ON` datetime DEFAULT NULL COMMENT 'CLEARED/RECONCILED',
  `STATUS` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'DEFAULT(1)\r\n0- INACTIVE/CANCELLED\r\n1-ACTIVE\r\n',
  `BRANCH_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`VOUCHER_ID`,`SEQUENCE_NO`,`BRANCH_ID`),
  KEY `LEDGER_ID` (`LEDGER_ID`),
  CONSTRAINT `FK_voucher_trans_LEDGER_ID` FOREIGN KEY (`LEDGER_ID`) REFERENCES `master_ledger` (`LEDGER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `voucher_trans`
--

LOCK TABLES `voucher_trans` WRITE;
/*!40000 ALTER TABLE `voucher_trans` DISABLE KEYS */;
/*!40000 ALTER TABLE `voucher_trans` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-05-06 18:17:54

