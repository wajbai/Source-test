DROP PROCEDURE IF EXISTS DROPFD;
CREATE PROCEDURE DROPFD (IN fdaccount INT(10))
BEGIN
  DECLARE FD_TYPE Varchar(5); -- OP (Opening) or IN (Invested)
  DECLARE RecordsCount Long DEFAULT 0;
  DECLARE ProjectId Long DEFAULT 0;
  DECLARE CountryCurrencyId Long DEFAULT 0;

  START TRANSACTION;

  SELECT TRANS_TYPE INTO FD_TYPE FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = fdaccount;
  SELECT PROJECT_ID INTO ProjectId FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = fdaccount;
  SELECT CURRENCY_COUNTRY_ID INTO CountryCurrencyId FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = fdaccount;

   -- 1. Remove FD renew's contra vouchers in voucher_master_trans, voucher_trans (CN)
  DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID
      IN (SELECT FD_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = fdaccount);
  DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID
      IN (SELECT FD_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = fdaccount);
  SELECT ('1. Removed renewed conta voucher') AS MSG;

  -- 2. Remove FD renew's interest vouchers in voucher_master_trans, voucher_trans (RC,JR)
  DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID
      IN (SELECT FD_INTEREST_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = fdaccount);
  DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID
      IN (SELECT FD_INTEREST_VOUCHER_ID FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = fdaccount);
  SELECT ('2. Removed renewed interest voucher') AS MSG;

  -- 3. Remove FD Invested's contra vouchers in voucher_master_trans, voucher_trans(CN) (if selected account investment fd
  IF FD_TYPE = 'IN' THEN
    BEGIN
      DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID IN (SELECT FD_VOUCHER_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = fdaccount);
      DELETE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID IN (SELECT FD_VOUCHER_ID FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = fdaccount);
      SELECT ('3. Removed contra Voucher') AS MSG;
    END;
  END IF;

  -- 4a. Update/reduce ledger balance amount if fd is opening for all its fd ledgers for not multi currency
  IF FD_TYPE = 'OP' AND CountryCurrencyId = 0 THEN
    BEGIN
      UPDATE ledger_balance lb, (SELECT project_id, ledger_id, SUM(IF(trans_mode='DR', AMOUNT, -AMOUNT)) opamount
      FROM FD_ACCOUNT WHERE trans_type = 'OP' AND fd_account_id = fdaccount) fd
      SET lb.Amount = lb.Amount- fd.opamount WHERE lb.trans_flag = 'OP' and lb.project_id = fd.project_id and lb.ledger_id = fd.ledger_id;
      SELECT ('4a. Updated FD Ledger Balance') AS MSG;
    END;
  END IF;

  -- 4b. Update/reduce ledger balance amount if fd is opening for all its fd ledgers for multi currency enabled
  IF FD_TYPE = 'OP' AND CountryCurrencyId > 0 THEN
    BEGIN
      UPDATE ledger_balance lb, (SELECT project_id, ledger_id, SUM(IF(trans_mode='DR', AMOUNT, -AMOUNT) * EXCHANGE_RATE) opamountlc,
          SUM(IF(trans_mode='DR', AMOUNT, -AMOUNT) ) opamountfc
		FROM FD_ACCOUNT WHERE trans_type = 'OP' AND fd_account_id = fdaccount) fd
	  SET lb.Amount_fc = (lb.Amount_fc- fd.opamountfc), lb.Amount = (lb.Amount- fd.opamountlc)
	  WHERE lb.trans_flag = 'OP' and lb.project_id = fd.project_id and lb.ledger_id = fd.ledger_id;
      SELECT ('4b. Updated FD Ledger Balance for multi currency enabled') AS MSG;
    END;
  END IF;

  -- 5. Remove FD Renews
  DELETE FROM FD_RENEWAL WHERE FD_ACCOUNT_ID = fdaccount;
  SELECT ('5. Removed Renewed') AS MSG;

   -- 6. Remove FD Accounts
  DELETE FROM FD_ACCOUNT WHERE FD_ACCOUNT_ID = fdaccount;
  SELECT ('6. Removed FD Account') AS MSG;
  
  -- 7. If there is no FD Opening records (some times, fd ledger op balance is not getting refreshed if there is no fd opening in fd_account table)
  SELECT COUNT(*) INTO RecordsCount  FROM FD_ACCOUNT WHERE FD_TYPE ='OP' AND PROJECT_ID = ProjectId;
  IF RecordsCount = 0 THEN
    BEGIN
		UPDATE LEDGER_BALANCE SET AMOUNT = 0, AMOUNT_FC = 0 WHERE PROJECT_ID = ProjectId AND TRANS_FLAG = 'OP' AND LEDGER_ID IN (SELECT LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID = 14);
		SELECT ('SET 0 value in ledger blance opening') AS MSG;
	END;
  END IF;

  SELECT ('Deleted FD Account') AS MSG;
  COMMIT;
END