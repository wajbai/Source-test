using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using System.Globalization;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;

namespace ACPP.Modules.UIControls
{
    public partial class UcBudgetAllotFund : UserControl
    {
        #region Variables
        CommonMember utilityMember = null;
        public frmAllotFund objAllotFund;
        const string AMOUNT = "AMOUNT";
        const string MONTH = "MONTH";
        const string LEDGERID = "LEDGER_ID";
        #endregion

        #region Properties
        private Double amount = 0.00;
        public Double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                this.Refresh();
            }
        }

        private String label = "LabelName";
        public String Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                this.Refresh();
            }
        }

        private int count = 0;
        public int ControlCount
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        private TextEdit ctrlName = null;
        public TextEdit ControlName
        {
            get { return ctrlName; }
            set { ctrlName = value; }
        }

        public DataTable dtFundDetails
        {
            get;
            set;
        }

        BudgetSystem budgetSystem;
        public BudgetSystem BudgetSystem
        {
            get { return budgetSystem; }
            set { budgetSystem = value; }
        }

        public int LedgerId
        {
            get;
            set;
        }

        public int BudgetId
        {
            set;
            get;
        }
        #endregion

        #region Constructor
        public UcBudgetAllotFund()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public void AssignContol()
        {
            txtCtrl.Name = txtCtrl.Name + ControlCount.ToString();
            ControlName = txtCtrl;
            lblLableName.Text = Label;
            LoadData();
            ControlName.EditValueChanged += new EventHandler(objAllotFund.UpdateOnEdit_ValueChanged);
        }

        private void LoadData()
        {
            DataView dvAmount = new DataView(dtFundDetails);
            int Month = Array.IndexOf(CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames, (Label.Substring(0, 3))) + 1;
            dvAmount.RowFilter = String.Format("LEDGER_ID={0} AND MONTH={1}", LedgerId, Month);
            DataTable dtAmount = dvAmount.ToTable();
            foreach (DataRow dr in dtAmount.Rows)
            {
                switch (Month)
                {
                    case 1:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 2:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 3:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 4:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 5:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 6:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 7:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 8:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 9:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 10:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 11:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                    case 12:
                        ControlName.Text = dr[AMOUNT].ToString();
                        break;
                }
            }
        }
        #endregion

        #region Events
        private CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }
        #endregion
    }
}