using System;
using System.Collections.Generic;
using System.Text;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;

namespace Payroll.DAO.Schema
{
    public class AppSchemaSet : CommonMember
    {
        private ApplicationSchemaSet appSchemaSet = null;
        public ApplicationSchemaSet AppSchema
        {
            get
            {
                if (appSchemaSet == null)
                {
                    appSchemaSet = new ApplicationSchemaSet();
                }
                return appSchemaSet;
            }
        }

        public class ApplicationSchemaSet
        {
            private EnumTypeSchema.EnumTypeDataTable enumSchema = null;
            public EnumTypeSchema.EnumTypeDataTable EnumSchema
            {
                get
                {
                    if (enumSchema == null)
                    {
                        enumSchema = new EnumTypeSchema.EnumTypeDataTable();
                    }
                    return enumSchema;
                }
            }

            private ApplicationSchema.UserDataTable userSchema = null;
            public ApplicationSchema.UserDataTable User
            { 
                get
                {
                    if (userSchema == null)
                    {
                        userSchema = new ApplicationSchema.UserDataTable();
                    }
                    return userSchema;
                }
            }

            private ApplicationSchema.PayrollDataTable PayrollSchema = null;
            public ApplicationSchema.PayrollDataTable Payroll
            {
                get
                {
                    if (PayrollSchema == null)
                    {
                        PayrollSchema = new ApplicationSchema.PayrollDataTable();
                    }
                    return PayrollSchema;
                }
            }
            private ApplicationSchema.PRCOMPMONTHDataTable PRCOMPMONTHSchema = null;
            public ApplicationSchema.PRCOMPMONTHDataTable PRCOMPMONTH
            {
                get
                {
                    if (PRCOMPMONTHSchema == null)
                    {
                        PRCOMPMONTHSchema = new ApplicationSchema.PRCOMPMONTHDataTable();
                    }
                    return PRCOMPMONTHSchema;
                }
            }
            private ApplicationSchema.PRCOMPONENTDataTable PRCOMPONENTSchema = null;
            public ApplicationSchema.PRCOMPONENTDataTable PRCOMPONENT
            {
                get
                {
                    if (PRCOMPONENTSchema == null)
                    {
                        PRCOMPONENTSchema = new ApplicationSchema.PRCOMPONENTDataTable();
                    }
                    return PRCOMPONENTSchema;
                }
            }
            private ApplicationSchema.PRSTAFFDataTable PRSTAFFSchema = null;
            public ApplicationSchema.PRSTAFFDataTable PRSTAFF
            {
                get
                {
                    if (PRSTAFFSchema == null)
                    {
                        PRSTAFFSchema = new ApplicationSchema.PRSTAFFDataTable();
                    }
                    return PRSTAFFSchema;
                }
            }
            private ApplicationSchema.PRSTAFFGROUPDataTable PRSTAFFGROUPSchema = null;
            public ApplicationSchema.PRSTAFFGROUPDataTable PRSTAFFGROUP
            {
                get
                {
                    if (PRSTAFFGROUPSchema == null)
                    {
                        PRSTAFFGROUPSchema = new ApplicationSchema.PRSTAFFGROUPDataTable();
                    }
                    return PRSTAFFGROUPSchema;
                }
            }
            private ApplicationSchema.PRSTATUSDataTable PRSTATUSSchema = null;
            public ApplicationSchema.PRSTATUSDataTable PRSTATUS
            {
                get
                {
                    if (PRSTATUSSchema == null)
                    {
                        PRSTATUSSchema = new ApplicationSchema.PRSTATUSDataTable();
                    }
                    return PRSTATUSSchema;
                }
            }

            private ApplicationSchema.STFPERSONALDataTable STFPERSONALSchema = null;
            public ApplicationSchema.STFPERSONALDataTable STFPERSONAL
            {
                get
                {
                    if (STFPERSONALSchema == null)
                    {
                        STFPERSONALSchema = new ApplicationSchema.STFPERSONALDataTable();
                    }
                    return STFPERSONALSchema;
                }
            }
            private ApplicationSchema.PAYWagesDataTable PAYROLLWAGESSchema = null;
            public ApplicationSchema.PAYWagesDataTable PAYROLLWAGES
            {
                get
                {
                    if (PAYROLLWAGESSchema == null)
                    {
                        PAYROLLWAGESSchema = new ApplicationSchema.PAYWagesDataTable();
                    }
                    return PAYROLLWAGESSchema;
                }
            }
            private ApplicationSchema.PRPROJECT_STAFFDataTable PRProjectStaffschema = null;
            public ApplicationSchema.PRPROJECT_STAFFDataTable PRPayrollStaff
            {
                get
                {
                    if (PRProjectStaffschema == null)
                    {
                        PRProjectStaffschema = new ApplicationSchema.PRPROJECT_STAFFDataTable();
                    }
                    return PRProjectStaffschema;
                }
            }
            private ApplicationSchema.PAYROLL_LEDGERDataTable PRPayrollLedgerSchema = null;
            public ApplicationSchema.PAYROLL_LEDGERDataTable PRPayrollLedger
            {
                get
                {
                    if (PRPayrollLedgerSchema == null)
                    {
                        PRPayrollLedgerSchema = new ApplicationSchema.PAYROLL_LEDGERDataTable();
                    }
                    return PRPayrollLedgerSchema;
                }
            }
            private ApplicationSchema.LedgerDataTable PRLedgerSchema = null;
            public ApplicationSchema.LedgerDataTable PRLedger
            {
                get
                {
                    if (PRLedgerSchema == null)
                    {
                        PRLedgerSchema = new ApplicationSchema.LedgerDataTable();
                    }
                    return PRLedgerSchema;
                }
            }
            private ApplicationSchema.PAYROLL_PROJECTDataTable PRPayrollProjectschema = null;
            public ApplicationSchema.PAYROLL_PROJECTDataTable PRPayrollProject
            {
                get
                {
                    if (PRPayrollProjectschema == null)
                    {
                        PRPayrollProjectschema = new ApplicationSchema.PAYROLL_PROJECTDataTable();
                    }
                    return PRPayrollProjectschema;
                }
            }
            private ApplicationSchema.PAYROLL_RANGE_FORMULADataTable PRPayrollRangeFormulaschema = null;
            public ApplicationSchema.PAYROLL_RANGE_FORMULADataTable PRPayrollRangeFormula
            {
                get
                {
                    if (PRPayrollRangeFormulaschema == null)
                    {
                        PRPayrollRangeFormulaschema = new ApplicationSchema.PAYROLL_RANGE_FORMULADataTable();
                    }
                    return PRPayrollRangeFormulaschema;
                }
            }
            private ApplicationSchema.PAYROLL_FINANCEDataTable PayrollFinanceschema = null;
            public ApplicationSchema.PAYROLL_FINANCEDataTable PayrollFinance
            {
                get
                {
                    if (PayrollFinanceschema == null)
                    {
                        PayrollFinanceschema = new ApplicationSchema.PAYROLL_FINANCEDataTable();
                    }
                    return PayrollFinanceschema;
                }
            }

            private ApplicationSchema.Payroll_SettingDataTable PayrollSettingschema = null;
            public ApplicationSchema.Payroll_SettingDataTable PayrollSetting
            {
                get
                {
                    if (PayrollSettingschema == null)
                    {
                        PayrollSettingschema = new ApplicationSchema.Payroll_SettingDataTable();
                    }
                    return PayrollSettingschema;
                }
            }
            
            private ApplicationSchema.PR_DEPARTMENTDataTable PayrollDepartmentschema = null;
            public ApplicationSchema.PR_DEPARTMENTDataTable PayrollDepartment
            {
                get
                {
                    if (PayrollDepartmentschema == null)
                    {
                        PayrollDepartmentschema = new ApplicationSchema.PR_DEPARTMENTDataTable();
                    }
                    return PayrollDepartmentschema;
                }
            }

            private ApplicationSchema.PR_WORK_LOCATIONDataTable PayrollWorkLocationschema = null;
            public ApplicationSchema.PR_WORK_LOCATIONDataTable PayrollWorkLocation
            {
                get
                {
                    if (PayrollWorkLocationschema == null)
                    {
                        PayrollWorkLocationschema = new ApplicationSchema.PR_WORK_LOCATIONDataTable();
                    }
                    return PayrollWorkLocationschema;
                }
            }

            private ApplicationSchema.PR_NAME_TITLEDataTable NameTitleSchema = null;
            public ApplicationSchema.PR_NAME_TITLEDataTable NameTitle
            {
                get
                {
                    if (NameTitleSchema == null)
                    {
                        NameTitleSchema = new ApplicationSchema.PR_NAME_TITLEDataTable();
                    }
                    return NameTitleSchema;
                }
            }
        }
    }
}
