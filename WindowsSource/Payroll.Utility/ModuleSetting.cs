using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Payroll.Utility
{
    public static class GeneralColor
    {
        private static Color formBackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        public static Color FormBackColor
        {
            get { return GeneralColor.formBackColor; }
            set { GeneralColor.formBackColor = value; }
        }

        private static Color color1 = Color.White;
        public static Color Color1
        {
            get { return GeneralColor.color1; }
            set { GeneralColor.color1 = value; }
        }

        private static Color color2 = Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(185)))), ((int)(((byte)(251)))));
        public static Color Color2
        {
            get { return GeneralColor.color2; }
            set { GeneralColor.color2 = value; }
        }

        private static float colorAngle = 90;
        public static float ColorAngle
        {
            get { return GeneralColor.colorAngle; }
            set { GeneralColor.colorAngle = value; }
        }

        public static void resetProperty()
        {
            GeneralColor.color1 = Color.White;
            GeneralColor.color2 = Color.Red;
            GeneralColor.colorAngle = 90;
        }
    }

    public static class LabelProperty
    {
        private static Color fieldCaption = Color.MidnightBlue;
        public static Color FieldCaption
        {
            get { return LabelProperty.fieldCaption; }
            set { LabelProperty.fieldCaption = value; }
        }

        private static Color fieldValue = Color.Black;
        public static Color FieldValue
        {
            get { return LabelProperty.fieldValue; }
            set { LabelProperty.fieldValue = value; }
        }

        private static Color mandatoryField = Color.Red;
        public static Color MandatoryField
        {
            get { return LabelProperty.mandatoryField; }
            set { LabelProperty.mandatoryField = value; }
        }

        private static Font fieldCaptionFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        public static Font FieldCaptionFont
        {
            get { return LabelProperty.fieldCaptionFont; }
            set { LabelProperty.fieldCaptionFont = value; }
        }

        private static Font fieldValueFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        public static Font FieldValueFont
        {
            get { return LabelProperty.fieldValueFont; }
            set { LabelProperty.fieldValueFont = value; }
        }

        private static Font mandatoryFieldFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        public static Font MandatoryFieldFont
        {
            get { return LabelProperty.mandatoryFieldFont; }
            set { LabelProperty.mandatoryFieldFont = value; }
        }

        public static void resetProperty()
        {
            LabelProperty.fieldCaption = Color.MidnightBlue;
            LabelProperty.fieldValue = Color.Black;
            LabelProperty.mandatoryField = Color.Red;
            LabelProperty.fieldCaptionFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            LabelProperty.fieldValueFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            LabelProperty.mandatoryFieldFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }

    public static class ButtonProperty
    {
        private static Font captionFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        public static Font CaptionFont
        {
            get { return ButtonProperty.captionFont; }
            set { ButtonProperty.captionFont = value; }
        }

        private static Color normal1 = Color.SteelBlue;
        public static Color Normal1
        {
            get { return ButtonProperty.normal1; }
            set { ButtonProperty.normal1 = value; }
        }
        private static Color normal2 = Color.White;
        public static Color Normal2
        {
            get { return ButtonProperty.normal2; }
            set { ButtonProperty.normal2 = value; }
        }

        private static Color focus1 = Color.Coral;
        public static Color Focus1
        {
            get { return ButtonProperty.focus1; }
            set { ButtonProperty.focus1 = value; }
        }
        private static Color focus2 = Color.White;
        public static Color Focus2
        {
            get { return ButtonProperty.focus2; }
            set { ButtonProperty.focus2 = value; }
        }

        private static Color click1 = Color.Crimson;
        public static Color Click1
        {
            get { return ButtonProperty.click1; }
            set { ButtonProperty.click1 = value; }
        }
        private static Color click2 = Color.White;
        public static Color Click2
        {
            get { return ButtonProperty.click2; }
            set { ButtonProperty.click2 = value; }
        }

        private static float colorAngle = 90;
        public static float ColorAngle
        {
            get { return ButtonProperty.colorAngle; }
            set { ButtonProperty.colorAngle = value; }
        }

        public static void resetProperty()
        {
            ButtonProperty.normal1 = Color.SteelBlue;
            ButtonProperty.normal2 = Color.White;
            ButtonProperty.focus1 = Color.Coral;
            ButtonProperty.focus2 = Color.White;
            ButtonProperty.click1 = Color.Crimson;
            ButtonProperty.click2 = Color.White;
            ButtonProperty.colorAngle = 90;
            ButtonProperty.captionFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }

    public enum UserSettingType
    {
        General = 1,
        Label = 2,
        Button = 3,
        Grid = 4
    }

    public enum GeneralSetting
    {
        GeneralColor1 = 1,
        GeneralColor2 = 2,
        ColorAngle = 3
    }

    public enum LabelSetting
    {
        FieldCaption = 1,
        FieldValue = 2,
        MandatoryField = 3
    }

    public enum ButtonSetting
    {
        Normal = 1,
        Focus = 2,
        Click = 3,
        ColorAngle = 4,
        Font
    }

    public enum CaptionGroup
    {
        FieldCaption = 1,
        ColumnTitle = 2,
        ReportTitle = 3
    }

    public enum ActivityRightType
    {
        Form = 0,
        Control = 1
    }

    public enum ActivityGroup
    {
        Admin = 1,
        Inventory = 2,
        Purchase = 3,
        Sales = 4,
        Setting = 5
    }

    public enum DepartmentCategory
    {
        Administration = 1,
        Inventory = 2,
        Purchase = 3,
        Sales = 4,
        Setting = 5
    }

    //Activity Id from 1 to 30
    public enum AdminItem
    {
        Account = 1,
        AccountGroup = 2,
        AccountHead = 3,
        DiscountHead = 4,
        PaymentMode = 5,
        DepartmentDetail = 6,
        Department = 7,
        UserManagement = 8,
        Role = 9,
        User = 10,
        ChangePassword = 11
    }

    //Activity ID from 31 to 80
    public enum InventoryItem
    {
        Master = 31,
        Manufacturer = 32,
        Vendor = 33,
        MeasurementType = 34,
        Measurement = 35,
        MeasurementConversion = 36,
        Location = 37,
        ItemType = 38,
        Item = 39,
        MapGenericItemwithItems = 40,
        MapDepartmentwithItems = 41,
        ItemReorder = 42,
        PositionItemsbyLocations = 43,
        TaxComponent = 44,
        TaxCode = 45,
        TaxRule = 46,
        ItemTemplate = 47,
        MapIssueandReceiveDepartment = 48,
        ItemExpiryNotification = 49,
        OrderMode = 50,
        DeliveryMode = 51,
        Adjustment = 52,
        ManageStockCategories = 53,
        StockAdjustment = 54,
        StockAdjustmentbyLocation = 55,
        UpdateSellingPricebyItem = 56,
        UpdateSellingPricebyBatch = 57,
        ViewStockPosition = 58,
        AddOpeningStock = 59,
        ViewStockbyItem = 60
    }

    //Activity ID from 81 to 120
    public enum PurchaseItem
    {
        PurchaseOrderDetail = 81,
        PurchaseOrder = 82,
        PurchaseOrderStatus = 83,
        ViewPurchaseOrders = 84,
        PurchaseReceiptDetail = 85,
        PurchaseReceipt = 86,
        ReceiptPayment = 87,
        AccountsPayable = 88,
        DebitMemoDetail = 89,
        DebitMemo = 90,
        CollectRefundtoReturns = 91,
        ReplaceVendorItems = 92,
        AccountsReceivable = 93
    }

    //Activity ID from 121 to 160
    public enum SalesItem
    {
        Sales = 121,
        IssueItems = 122,
        ViewIssuedBills = 123,
        CancelIssuedBill = 124,
        Transaction = 125,
        CollectDeposit = 126,
        RefundDeposit = 127,
        TransactionHistory = 128,
        DueCollection = 129,
        SalesReturn = 130,
        ReturnItems = 131,
        SalesAccount = 132,
        MapAccountHead = 133
    }

    //Activity ID from 161 to 200
    public enum SettingItem
    {
        Setting = 161,
        CaptionSetting = 162,
        InventorySetting = 163,
        UserProfile = 164,
        GeneralSetting = 165,
        LabelSetting = 166,
        ButtonSetting = 167,
        GridSetting = 168
    }

    //Activity ID from 201 to 250
    public enum DistributeItem
    {
        DistributeToBranch = 201,
        ViewDistributedItems = 202,
        CancelDistributionForBranch = 203,
        DueCollection = 204,
        DistributedItemsReturn = 205,
        DistributionAccount = 206,
        MapAccountHead = 207
    }
}
