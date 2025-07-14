DROP PROCEDURE IF EXISTS VerifyInvalidTables;
CREATE PROCEDURE VerifyInvalidTables()
BEGIN
  -- DECLARE CONTINUE HANDLER FOR SQLEXCEPTION;
  Declare DBName Varchar(50);
  Declare RecordsCount Long DEFAULT 0;
  Declare RecordsCount1 Long DEFAULT 0;
  Declare IsTableExists TINYINT DEFAULT 0;
  Declare IsFieldExists TINYINT DEFAULT 0;
  Declare tmptxtvalue Varchar(500);
  Select Database() INTO DBName;

 --  This is to delete the invalid Entry 10.12.2019
   SELECT IF(COUNT(*)>0, 1, 0) INTO IsFieldExists FROM Information_schema.COLUMNS where Table_Schema = Database() and Table_Name = 'ASSET_ITEM' and Column_Name ='CATEGORY_ID';
   IF (IsFieldExists) THEN
    BEGIN
    DELETE FROM ASSET_ITEM WHERE DEPRECIATION_LEDGER_ID = 276 AND DISPOSAL_LEDGER_ID =276 AND ACCOUNT_LEDGER_ID =1100 AND CATEGORY_ID =1 AND
     UNIT_ID =1 AND PREFIX = 'C' AND SUFFIX = 'S' AND STARTING_NO =1 AND RUNNING_NUMBER =3 AND CUSTODIANS_ID = 0 AND ASSET_NAME = 'Honda';
    END;
   END IF;

-- Udate Master Voucher inactive if particular vouhcer doest have 2 child vouchers----------------------------------------------------------------
    UPDATE voucher_master_trans SET STATUS=0 WHERE STATUS = 1 AND VOUCHER_ID in (SELECT VOUCHER_ID FROM (SELECT IF(COUNT(VT.VOUCHER_ID)<=1, VT.VOUCHER_ID,0) as VOUCHER_ID FROM voucher_trans VT GROUP BY VT.VOUCHER_ID) AS INVALID
    WHERE VOUCHER_ID<>0);
  
  -- Updtae Master Voucher inactive if particular vouhcer doest have 0 child vouchers-----------------------------------------------------------------
    UPDATE voucher_master_trans SET STATUS=0 WHERE VOUCHER_ID NOT IN (SELECT VOUCHER_ID FROM voucher_trans GROUP BY VOUCHER_ID);
	  
  -- To remove and add if
  SELECT COLUMN_TYPE INTO tmptxtvalue FROM Information_schema.COLUMNS where Table_Schema = DBName and Table_Name = 'MASTER_DONAUD' and Column_Name ='STATE_ID';
  IF (tmptxtvalue <> 'int(10) unsigned') THEN
    BEGIN
	 ALTER TABLE `master_donaud` DROP COLUMN `STATE_ID`;
	 ALTER TABLE  `master_donaud` ADD COLUMN `STATE_ID` INTEGER UNSIGNED AFTER `CUSTOMERID`;
	 SELECT ('Altered master_donaud table') AS MSG;
	END;
  END If;
  --
    
  -- Drop all old tds tables if there is no records in tds_booking------------------------------------------------------------------------
  SELECT IF(COUNT(*)>0, 1, 0) INTO IsTableExists FROM information_schema.tables WHERE table_schema = DBName AND table_name = 'tds_booking';
  IF (IsTableExists) THEN
    BEGIN
      SELECT COUNT(*) INTO RecordsCount FROM tds_booking;
      SELECT COUNT(*) INTO RecordsCount1 FROM tds_credtiors_profile;
      IF RecordsCount=0 AND RecordsCount1=0 THEN
        BEGIN
          -- Drop all tds tables
      DROP TABLE IF EXISTS tds_company_deductors;
      DROP TABLE IF EXISTS tds_booking;
      DROP TABLE IF EXISTS tds_booking_detail;
      DROP TABLE IF EXISTS tds_credtiors_profile;
      DROP TABLE IF EXISTS tds_deduction;
      DROP TABLE IF EXISTS tds_deduction_detail;
      DROP TABLE IF EXISTS tds_party_payment;
      DROP TABLE IF EXISTS tds_party_payment_detail;
      DROP TABLE IF EXISTS tds_payment;
      DROP TABLE IF EXISTS tds_payment_detail;
      DROP TABLE IF EXISTS tds_tax_rate;
      DROP TABLE IF EXISTS tds_policy;
      DROP TABLE IF EXISTS tds_nature_payment;
      DROP TABLE IF EXISTS tds_duty_taxtype;
      DROP TABLE IF EXISTS tds_dedutee_type;
      DROP TABLE IF EXISTS tds_deductee_type;
      DROP TABLE IF EXISTS tds_section;
    SELECT ('Dropped TDS Tables') AS MSG;
        END;
      END IF;
    END;
  END IF;
  
  -- Drop all old PayRoll tables, if there is no records in stock_item---------------------------------------------------------------------
  SELECT IF(COUNT(*)>0, 1, 0) INTO IsTableExists FROM information_schema.tables WHERE table_schema = DBName AND table_name = 'prcreate';
  IF (IsTableExists) THEN
    BEGIN
      SELECT COUNT(*) INTO RecordsCount FROM prcreate;
      IF RecordsCount=0 THEN
        BEGIN
          -- Drop all Payroll tables
        DROP TABLE IF EXISTS princome;
        DROP TABLE IF EXISTS prtext;
		DROP TABLE IF EXISTS pr_staff_performance;
        DROP TABLE IF EXISTS stfservice;
        DROP TABLE IF EXISTS prstaffgroup;
        DROP TABLE IF EXISTS prstafftemp;
        DROP TABLE IF EXISTS prstaff;
        DROP TABLE IF EXISTS prloanget;
        DROP TABLE IF EXISTS prloanpaid;
        DROP TABLE IF EXISTS prproject_staff;
        DROP TABLE IF EXISTS stfpersonal;
        DROP TABLE IF EXISTS prstatus;
        DROP TABLE IF EXISTS payroll_ledger;
        DROP TABLE IF EXISTS payroll_project;
        DROP TABLE IF EXISTS payroll;
        DROP TABLE IF EXISTS prcompmonth;
		DROP TABLE IF EXISTS payroll_finance;
		DROP TABLE IF EXISTS payroll_voucher;
        DROP TABLE IF EXISTS prcomponent;
        DROP TABLE IF EXISTS prformulagroup;
        DROP TABLE IF EXISTS prsalarygroup;
        DROP TABLE IF EXISTS prloan;
        DROP TABLE IF EXISTS prcreate;
        DROP TABLE IF EXISTS payroll_range_formula;
        DROP TABLE IF EXISTS process_type;
        
          SELECT ('Dropped Payroll Tables') AS MSG;
        END;
      END IF;
    END;
  END IF;

    -- Drop all old tables without checking any condition------------------------------------------------------------------------
    -- Drop unnecessary finance tables
    DROP TABLE IF EXISTS inkindtrans;
	DROP TABLE IF EXISTS country_symbols;
	DROP TABLE IF EXISTS dsafds;
	DROP TABLE IF EXISTS fd_registers;
	DROP TABLE IF EXISTS mas_user;
	DROP TABLE IF EXISTS users;
	DROP TABLE IF EXISTS voucher_fd_interest;

	-- Delete old block table Information.
  SELECT IF(COUNT(*)>0, 1, 0) INTO IsFieldExists FROM Information_schema.COLUMNS where Table_Schema = Database() and Table_Name = 'asset_block' and Column_Name ='BUILDING_ID';
  IF (IsFieldExists) THEN
   BEGIN
     SELECT COUNT(*) INTO RecordsCount FROM asset_block;
     IF RecordsCount=0 THEN
      BEGIN
	  ALTER TABLE `asset_block` DROP COLUMN `BUILDING_ID`
      , DROP INDEX `FK_BUILDING_ID`,
      DROP FOREIGN KEY `FK_BUILDING_ID`;
      SELECT ('delete the asset block') AS MSG;
      END;
     END IF;
   END;
  END IF;

    -- Drop old asset tables -- commanded by chinna on ..14/09/2023...At..11am

    -- DROP TABLE IF EXISTS asset_purchase_detail ;
    -- DROP TABLE IF EXISTS asset_purchase_voucher ;
    -- DROP TABLE IF EXISTS asset_purchase_master ;
    -- DROP TABLE IF EXISTS asset_sales_detail ;
    -- DROP TABLE IF EXISTS asset_sales_voucher;
    -- DROP TABLE IF EXISTS asset_sales_master ;
    -- DROP TABLE IF EXISTS asset_amc_detail ;
    -- DROP TABLE IF EXISTS asset_amc_master ;
    -- DROP TABLE IF EXISTS asset_amc_voucher ;
    -- DROP TABLE IF EXISTS asset_depre_detail ;
    -- DROP TABLE IF EXISTS asset_depre_master ;
    -- DROP TABLE IF EXISTS asset_depreciation_detail;
    -- DROP TABLE IF EXISTS asset_depreciation_master;
    -- DROP TABLE IF EXISTS asset_insurance_detail;
    -- DROP TABLE IF EXISTS asset_insurance_master_detail ;
    -- DROP TABLE IF EXISTS asset_insurance_voucher;
    -- DROP TABLE IF EXISTS asset_insurance_master ;
    -- DROP TABLE IF EXISTS asset_insurance_renewal_detail ;
    -- DROP TABLE IF EXISTS asset_insurance_renewal_master ;
    -- DROP TABLE IF EXISTS asset_transfer_detail ;
    -- DROP TABLE IF EXISTS asset_transfer_voucher ;
    -- DROP TABLE IF EXISTS asset_transfer_master ;
    -- DROP TABLE IF EXISTS asset_insurance_type ;
    -- DROP TABLE IF EXISTS asset_custodians ;
    -- DROP TABLE IF EXISTS asset_ledger ;
    -- DROP TABLE IF EXISTS asset_depre_master;
    -- DROP TABLE IF EXISTS asset_depreciation;
    -- DROP TABLE IF EXISTS asset_insurance;
    -- DROP TABLE IF EXISTS asset_service;
    -- DROP TABLE IF EXISTS asset_floor ;
    -- DROP TABLE IF EXISTS asset_building;
    -- DROP TABLE IF EXISTS asset_area;
    -- DROP TABLE IF EXISTS asset_room ;
    -- DROP TABLE IF EXISTS asset_insurance_renewal ;
    -- DROP TABLE IF EXISTS asset_unitofmeasure ;
    -- DROP TABLE IF EXISTS asset_vendor_info ;
    -- DROP TABLE IF EXISTS asset_id_format;
    -- DROP TABLE IF EXISTS asset_group;
    -- DROP TABLE IF EXISTS asset_depreciation;
    -- DROP TABLE IF EXISTS asset_depreciation_method ;
    -- DROP TABLE IF EXISTS asset_category ;

      -- Drop old stock tables
    -- DROP TABLE IF EXISTS stock_item_details;
    -- DROP TABLE IF EXISTS stock_ledger;
    -- DROP TABLE IF EXISTS stock_purchase_details;
    -- DROP TABLE IF EXISTS stock_master_purchase;
    -- DROP TABLE IF EXISTS stock_purchase_returns_details;
    -- DROP TABLE IF EXISTS stock_master_purchase_returns;
    -- DROP TABLE IF EXISTS stock_sold_utilized_details;
    -- DROP TABLE IF EXISTS stock_master_sold_utilized;
    -- DROP TABLE IF EXISTS inventory_stock;
    -- DROP TABLE IF EXISTS stock_item_transfer;
    -- DROP TABLE IF EXISTS stock_item;
    -- DROP TABLE IF EXISTS stock_group;
    -- DROP TABLE IF EXISTS stock_category;

    -- Common Tables  (Asset & Stock)
    DROP TABLE IF EXISTS asset_stock_unitofmeasure ;
    DROP TABLE IF EXISTS asset_stock_manufacture ;
    DROP TABLE IF EXISTS asset_stock_location ;
    DROP TABLE IF EXISTS stock_location ;
    DROP TABLE IF EXISTS stock_unitofmeasure ;

	-- Delete Cristo Tables
	DROP TABLE IF EXISTS `cristo_family`;
	DROP TABLE IF EXISTS `cristo_family_member`;
	DROP TABLE IF EXISTS `cristo_baptism_register`;
	DROP TABLE IF EXISTS `cristo_communion_register`;
	DROP TABLE IF EXISTS `cristo_confirmation_register`;
	DROP TABLE IF EXISTS `cristo_marriage_register`;
	DROP TABLE IF EXISTS `cristo_death_register`;
	DROP TABLE IF EXISTS `cristo_master_parish`;
	DROP TABLE IF EXISTS `cristo_master_bcc`;
	DROP TABLE IF EXISTS `cristo_master_state`;
	DROP TABLE IF EXISTS `cristo_master_district`;
	DROP TABLE IF EXISTS `cristo_master_housetype`;
	DROP TABLE IF EXISTS `cristo_master_languagegroups`;
	DROP TABLE IF EXISTS `cristo_master_membership_status`;
	DROP TABLE IF EXISTS `cristo_master_occupation`;
	DROP TABLE IF EXISTS `cristo_master_relationship`;
	DROP TABLE IF EXISTS `cristo_master_religion`;
	DROP TABLE IF EXISTS `cristo_master_rite`;
	DROP TABLE IF EXISTS `cristo_offering_category`;
	DROP TABLE IF EXISTS `cristo_offering_feasts`;
	DROP TABLE IF EXISTS `cristo_offering`;
	DROP TABLE IF EXISTS `cristo_mass_offering`;
	DROP TABLE IF EXISTS `cristo_master_community`;
	DROP TABLE IF EXISTS `cristo_parish_subscription`;
	DROP TABLE IF EXISTS `cristo_family_subscription`;
	DROP TABLE IF EXISTS `cristo_daily_collection`;
	DROP TABLE IF EXISTS `cristo_subscription_detail`;
	DROP TABLE IF EXISTS `cristo_master_subscription`;
	DROP TABLE IF EXISTS `custom_report`;


  
 -- Drop all new asset tables if there is no records in asset_item------------------------------------------------------------------------
  SELECT IF(COUNT(*)>0, 1, 0) INTO IsTableExists FROM information_schema.tables WHERE table_schema = DBName AND table_name = 'asset_item';
  IF (IsTableExists) THEN
    BEGIN
      SELECT COUNT(*) INTO RecordsCount FROM asset_item;
      IF RecordsCount=0 THEN
        BEGIN
		DROP TABLE IF EXISTS `asset_amc_item_mapping`;
		DROP TABLE IF EXISTS `asset_amc_renewal_history`;
		DROP TABLE IF EXISTS `asset_amc_renewal_master`;
		DROP TABLE IF EXISTS `asset_dep_method`;
		DROP TABLE IF EXISTS `asset_depreciation_detail`;
		DROP TABLE IF EXISTS `asset_depreciation_master`;
		DROP TABLE IF EXISTS `asset_insurance_detail`;
		DROP TABLE IF EXISTS `asset_project_location`;
		DROP TABLE IF EXISTS `asset_trans`;
		DROP TABLE IF EXISTS `asset_location`;
		DROP TABLE IF EXISTS `asset_custodian`;
		DROP TABLE IF EXISTS `asset_in_out_detail`;
		DROP TABLE IF EXISTS `asset_block`;
		DROP TABLE IF EXISTS `asset_stock_manufacturer`;
		
		-- Common 
		DROP TABLE IF EXISTS `asset_in_out_master`;
		DROP TABLE IF EXISTS `asset_insurance_plan`;
		DROP TABLE IF EXISTS `asset_item_detail`;
		DROP TABLE IF EXISTS `asset_item`;
		DROP TABLE IF EXISTS `asset_class`;

          SELECT ('Dropped Asset Tables') AS MSG;
        END;
      END IF;
    END;
  END IF;

  -- Drop all new Stock tables if there is no records in stock_item------------------------------------------------------------------------
  SELECT IF(COUNT(*)>0, 1, 0) INTO IsTableExists FROM information_schema.tables WHERE table_schema = DBName AND table_name = 'stock_item';
  IF (IsTableExists) THEN
    BEGIN
      SELECT COUNT(*) INTO RecordsCount FROM stock_item;
      IF RecordsCount=0 THEN
        BEGIN
		DROP TABLE IF EXISTS `stock_item`;
		DROP TABLE IF EXISTS `stock_category`;
		DROP TABLE IF EXISTS `stock_group`;
		DROP TABLE IF EXISTS `stock_item_details`;
		DROP TABLE IF EXISTS `stock_item_transfer`;
		DROP TABLE IF EXISTS `stock_ledger`;
		DROP TABLE IF EXISTS `stock_purchase_details`;
		DROP TABLE IF EXISTS `stock_master_purchase`;
		DROP TABLE IF EXISTS `stock_purchase_returns_details`;
		DROP TABLE IF EXISTS `stock_master_purchase_returns`;
		DROP TABLE IF EXISTS `stock_sold_utilized_details`;
		DROP TABLE IF EXISTS `stock_master_sold_utilized`;
		DROP TABLE IF EXISTS `inventory_stock`;
		DROP TABLE IF EXISTS `asset_stock_location`;
		-- DROP TABLE IF EXISTS `asset_stock_manufacture`;
		DROP TABLE IF EXISTS `asset_stock_unitofmeasure`;
		DROP TABLE IF EXISTS `asset_stock_vendor`;

          SELECT ('Dropped Stock Tables') AS MSG;
        END;
      END IF;
    END;
  END IF;

 -- Delete old Budget Information.
  SELECT IF(COUNT(*)>0, 1, 0) INTO IsFieldExists FROM Information_schema.COLUMNS where Table_Schema = Database() and Table_Name = 'BUDGET_MASTER' and Column_Name ='PROJECT_ID';
  IF (IsFieldExists) THEN
   BEGIN
     SELECT COUNT(*) INTO RecordsCount  FROM BUDGET_MASTER WHERE PROJECT_ID >0;
     IF RecordsCount > 0 THEN
      BEGIN
	  DELETE FROM BUDGET_MASTER;
	  DELETE FROM BUDGET_LEDGER;
	  DELETE FROM ALLOT_FUND;
      SELECT ('Verified Budget Details') AS MSG;
      END;
     END IF;
   END;
  END IF;

DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_DATE = '0001-01-01 00:00:00');

DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_DATE = '0001-01-01 00:00:00';

DELETE FROM VOUCHER_TRANS
WHERE VOUCHER_ID IN (SELECT FD_INTEREST_VOUCHER_ID FROM FD_RENEWAL WHERE RENEWAL_DATE = '0001-01-01 00:00:00') OR
VOUCHER_ID IN (SELECT FD_VOUCHER_ID FROM FD_RENEWAL WHERE RENEWAL_DATE = '0001-01-01 00:00:00');

DELETE FROM VOUCHER_MASTER_TRANS
WHERE VOUCHER_ID IN (SELECT FD_INTEREST_VOUCHER_ID FROM FD_RENEWAL WHERE RENEWAL_DATE = '0001-01-01 00:00:00') OR
VOUCHER_ID IN (SELECT FD_VOUCHER_ID FROM FD_RENEWAL WHERE RENEWAL_DATE = '0001-01-01 00:00:00');

DELETE FROM FD_RENEWAL WHERE RENEWAL_DATE = '0001-01-01 00:00:00';

DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID  NOT IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS);

-- 18/04/2024, Clear invalid entries in ledger balances 
DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID NOT IN (SELECT PROJECT_ID FROM MASTER_PROJECT) OR LEDGER_ID NOT IN (SELECT LEDGER_ID FROM MASTER_LEDGER);
DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID = 0 OR LEDGER_ID = 0;

END;

call VerifyInvalidTables();