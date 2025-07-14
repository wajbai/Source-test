DROP PROCEDURE IF EXISTS DROPPROJECT;
CREATE PROCEDURE DROPPROJECT (IN project INT(10))
  BEGIN

    START TRANSACTION;
      -- Remove all TDS entries
      DELETE FROM tds_booking WHERE project_id = project;
      DELETE FROM tds_booking_detail WHERE booking_id in (SELECT booking_id FROM tds_booking WHERE project_id = project);

      DELETE FROM tds_deduction WHERE project_id = project;
      DELETE FROM tds_deduction_detail WHERE deduction_id in (SELECT deduction_id FROM tds_deduction WHERE project_id = project);

      DELETE FROM tds_party_payment WHERE project_id = project;
      DELETE FROM tds_party_payment_detail WHERE party_payment_id in (SELECT party_payment_id FROM tds_party_payment WHERE project_id = project);

      DELETE FROM tds_payment WHERE project_id = project;
      DELETE FROM tds_payment_detail WHERE tds_payment_id in (SELECT tds_payment_id FROM tds_payment WHERE project_id = project);

      -- Remove all Payroll entries
	  DELETE FROM PAYROLL_VOUCHER WHERE VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID = project);

      -- Remove all Asset and Stock entries

      -- Remove budget entries
      -- DELETE FROM budget_costcenter WHERE budget_id in (SELECT budget_id FROM budget_master WHERE project_id = project);
      -- DELETE FROM budget_ledger WHERE budget_id in (SELECT budget_id FROM budget_master WHERE project_id = project);
      -- DELETE FROM allot_fund WHERE budget_id in (SELECT budget_id FROM budget_master WHERE project_id = project);
      -- DELETE FROM budget_master WHERE project_id = project;
	  DELETE BM.*, BP.*, BL.*, AF.*, PBL.* FROM BUDGET_MASTER BM 
			INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID
			INNER JOIN BUDGET_LEDGER BL ON BL.BUDGET_ID = BM.BUDGET_ID
			LEFT JOIN PROJECT_BUDGET_LEDGER PBL ON PBL.PROJECT_ID = BP.PROJECT_ID
            LEFT JOIN ALLOT_FUND AF ON BL.BUDGET_ID = AF.BUDGET_ID
            WHERE BP.PROJECT_ID = project;

      -- Remove all FD Vouchers entries
      DELETE FROM fd_renewal WHERE fd_account_id in (SELECT fd_account_id FROM fd_account WHERE project_id = project);
      DELETE FROM fd_account WHERE project_id = project;

      -- Remove all Vouchers entries
	  DELETE VR.* FROM VOUCHER_MASTER_TRANS VMT JOIN VOUCHER_REFERENCE VR ON VR.REF_VOUCHER_ID = VMT.VOUCHER_ID WHERE VMT.PROJECT_ID = project;
      DELETE FROM voucher_cc_trans WHERE voucher_id in (SELECT voucher_id FROM voucher_master_trans WHERE project_id = project);
      DELETE FROM voucher_trans WHERE voucher_id in (SELECT voucher_id FROM voucher_master_trans   WHERE project_id = project);
      DELETE FROM Voucher_Master_trans WHERE project_id = project;

      -- Remove all Mapping for given project
      DELETE FROM project_costcentre WHERE project_id = project;
      DELETE FROM project_donor WHERE project_id = project;
      DELETE FROM project_purpose WHERE project_id = project;
      DELETE FROM project_category_ledger WHERE project_id = project;
      DELETE FROM project_ledger WHERE project_id = project;
      DELETE FROM project_voucher WHERE project_id = project;

      -- Remove User rights for given project
      DELETE FROM user_project WHERE project_id = project;

      -- Remove ledger balance entries
      DELETE FROM ledger_balance WHERE project_id = project;

      -- Remove project from master
      DELETE FROM master_project where project_id =  project;
      SELECT ('Removed project') AS MSG;

    COMMIT;

  END