/*  Class Name      : EnumCommand.cs
 *  Purpose         : Enum Data type to avoid using scalars/magic numbers in application
 *  Author          : CS
 *  Created on      : 14-Jul-2010
 */

namespace Payroll.Utility
{

    public enum AppSettingName
    {
        DatabaseProvider = 1,
        AppConnectionString = 2,
        SQLAdapter = 3
    }

    public enum SelectionType
    {
        All = 0,
        Selected = 1,
        Deselected = 2
    }

    public enum Source
    {
        By,
        To
    }

    public enum Row
    {
        RowNew = 0
    }
    public enum Mode
    {
        Add = 0,
    }
    public enum CashSource
    {
        By,
        To
    }

    public enum CashFlag
    {
        Cash,
        Bank
    }

    public enum Sorting
    {
        Ascending,
        Descending,
        None
    }

    public enum TimeMode
    {
        AM = 1,
        PM = 2
    }

    public enum DateFormat
    {
    }
    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum YesNo
    {
        No = 0,
        Yes = 1
    }

    public enum DonorType
    {
        Institutional = 1,
        Individual = 2
    }

    public enum Types
    {
        Donor = 0,
        Auditor = 1,
        Other = 2
    }
    public enum DonorCategory
    {
        Above = 1,
        Below = 2
    }

    public enum DateDataType
    {
        Date,
        DateTime,
        Time,
        TimeStamp,
        DateNoFormatBegin,
        DateNoFormatEnd,
        DateFormatYMD
    }

    public enum UserType
    {
        None = 0,
        Admin = 1,
        User = 2,
    }

    public enum Status
    {
        Inactive = 0,
        Active = 1
    }

    public enum ViewDetails
    {
        Donor = 0,
        Auditor = 1,
    }

    public enum IdentityKey
    {
        Donor = 0,
        Auditor = 1,
    }

    public enum RowAdd
    {
        NewRow = 0
    }
    public enum FormMode
    {
        Add = 0,
        Edit = 1
    }
    public enum AccessFlag
    {
        Accessable = 0,
        Editable = 1,
        Readonly = 2
    }
    public enum BankAccoutType
    {
        SavingAccount = 1,
        FixedDeposit = 2,
        MutualFund = 3,
        RecurringDeposit = 4,
        Equity = 5
    }

    public enum Setting
    {
        UILanguage,
        UIDateFormat,
        UIDateSeparator,
        UIThemes,
        Country,
        Currency,
        CurrencyCode,
        CurrencyPosition,
        CodePosition,
        DigitGrouping,
        GroupingSeparator,
        DecimalPlaces,
        DecimalSeparator,
        NegativeSign,
        HighNaturedAmt
    }

    public enum UISetting
    {
        UILanguage,
        UIDateFormat,
        UIDateSeparator,
        UIThemes
    }
    public enum Division
    {
        Local = 1,
        Foreign = 2
    }

    public enum VoucherType
    {
        Receipts = 1,
        Payments = 2,
        Contra = 3,
        Journal = 4
    }
    public enum VoucherMethod
    {
        Automatic = 1,
        Manual = 2
    }

    public enum TransType
    {
        Receipts = 0,
        Payments = 1,
        Contra = 2,
        Journal = 3
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum EnumColumns
    {
        Id,
        Name
    }

    public enum CurrencyPosition
    {
        Before,
        After,
        None
    }

    public enum NegativePattern
    {
        Bracket = 13,
        Minus = 9
    }

    public enum CurrencyCodePosition
    {
        Before = 3,
        After = 4
    }
    public enum MapForm
    {
        Ledger,
        Project,
        CostCentre
    }
    public enum ProjectVoucher
    {
        MoveIn,
        MoveOut
    }

    public enum MasterRights
    {
        ReadOnly = 0,
        FullRights = 1
    }
    public enum StaffStatus
    {
        InService = 1,
        Outof = 2,
        All = 3
    }
    public enum Result
    {
        Success = 1,
        Failure
    }
    public enum LinkValue
    {
        Basic,
        Loan
    }
}
