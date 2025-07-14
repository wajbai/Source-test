

-- Currency
 UPDATE master_setting set Value='र' where setting_name='Currency' AND Value='';
 UPDATE master_country set COUNTRY_CODE='IND', currency_symbol='र' where country='India';
 UPDATE master_country set COUNTRY_ID=1, COUNTRY_CODE='IND', currency_symbol='र' where country='India';

-- When we take backup, INDIAN Currency is not taking in backup file, so we update currency symbol for country which is avilable in setting table
UPDATE MASTER_SETTING MS, (SELECT 'Currency' SETTING_NAME, MC.CURRENCY_SYMBOL FROM MASTER_SETTING MS INNER JOIN MASTER_COUNTRY MC
ON MS.VALUE = MC.COUNTRY_ID WHERE SETTING_NAME = 'Country') CURRENCY SET MS.VALUE = CURRENCY.CURRENCY_SYMBOL
WHERE MS.SETTING_NAME = CURRENCY.SETTING_NAME ;

-- 27/09/2017, When we migrate tally, BRANCH is empty, so we update BRANCH Name as BANK Name
UPDATE MASTER_BANK SET BRANCH = BANK WHERE BRANCH = '';

SET GLOBAL max_allowed_packet = 1024 * 1024 * 256;
