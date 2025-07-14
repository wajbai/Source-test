using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.Transaction;
using System.Collections;
using DevExpress.XtraEditors.DXErrorProvider;

namespace ACPP.Modules.Transaction
{
    public partial class frmFDBreakUp : frmBaseAdd
    {

        #region Variables
        ResultArgs resultArgs = null;
        double Amount;
        string Accountnumber;
        double GivenAmount=0.00;
        double CurrentAmount = 0.00;
        #endregion

        #region Constructor
        public frmFDBreakUp(double Amount, string AccNumber)
        {
            InitializeComponent();
            this.Amount = Amount;
            lblTotalAmount.Text += UtilityMember.NumberSet.ToCurrency(Amount);
            this.Accountnumber = AccNumber;
        }
        #endregion

        #region Events
        private void frmFDBreakUp_Load(object sender, EventArgs e)
        {
            try
            {
                gcFDBreakUp.DataSource = FetchBreakUpDetails();
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }

        }

        private void gvFDBreakUp_RowCountChanged(object sender, EventArgs e)
        {
            DataTable dt = gvFDBreakUp.DataSource as DataTable;
        }
      
        private void ritxtAmount_Leave(object sender, EventArgs e)
        {
            CalculatePercentage();
        }

        private void ritxtInterestRate_Leave(object sender, EventArgs e)
        {
            CalculatePercentage();
        }

        private void gvFDBreakUp_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                DataView dvFDBreakUp = gvFDBreakUp.DataSource as DataView;
                DataTable dtFDBreakUp = dvFDBreakUp.Table;
                GivenAmount = 0.00;
                if (dtFDBreakUp != null && dtFDBreakUp.Rows.Count > 0)
                {
                    GivenAmount = UtilityMember.NumberSet.ToDouble(dtFDBreakUp.Compute("SUM(AMOUNT)", string.Empty).ToString());
                    string FDNo = gvFDBreakUp.GetRowCellValue(e.RowHandle, gvColFDNo).ToString();
                    DateTime InvestedDate = (DateTime)gvFDBreakUp.GetRowCellValue(e.RowHandle, gvColInvestedOn);
                    DateTime MaturityDate = (DateTime)gvFDBreakUp.GetRowCellValue(e.RowHandle, gvColMaturityDate);
                    double InterestRate = this.UtilityMember.NumberSet.ToDouble(gvFDBreakUp.GetRowCellValue(e.RowHandle, gvColInterestRate).ToString());
                    double InterestAmount = CurrentAmount = this.UtilityMember.NumberSet.ToDouble(gvFDBreakUp.GetRowCellValue(e.RowHandle, gvColAmount).ToString());
                    if (Amount >= GivenAmount)
                    {
                        if (!string.IsNullOrEmpty(FDNo) && InterestRate > 0.00 && InterestAmount > 0.00)
                        {
                            if (Amount.Equals(GivenAmount))
                                gvFDBreakUp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                            else
                                gvFDBreakUp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                        }
                        else
                        {
                            gvFDBreakUp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                            ShowMessageBox(GetMessage(MessageCatalog.Master.BreakUp.BREAKUP_REQUIRED_FIELD));
                        }
                    }
                    else
                    {
                        gvFDBreakUp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                        ShowMessageBox(GetMessage(MessageCatalog.Master.BreakUp.BREAKUP_AMOUNT_EXCEEDS));
                    }
                }
                }
               
            catch (Exception ee)
            {
                if (ee.Message.Contains(GetMessage(MessageCatalog.Master.BreakUp.BREAKUP_DATE_EXCEPTION)))
                {
                    gvFDBreakUp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                    ShowMessageBox(GetMessage(MessageCatalog.Master.BreakUp.BREAKUP_REQUIRED_FIELD));
                }
            }
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            DataView dvFDBreakUp = gvFDBreakUp.DataSource as DataView;
            DataTable dtFDBreakUp = dvFDBreakUp.Table;
            if (dtFDBreakUp != null && dtFDBreakUp.Rows.Count > 0)
            {
                GivenAmount = UtilityMember.NumberSet.ToDouble(dtFDBreakUp.Compute("SUM(AMOUNT)", string.Empty).ToString());
            }
            if (Amount.Equals(GivenAmount) && Amount >= CurrentAmount)
            {
                DataTable dtBreakUp = gcFDBreakUp.DataSource as DataTable;
                using (BreakUpSystem breakupSystem = new BreakUpSystem())
                {
                    breakupSystem.AccounNumber = Accountnumber;
                    ValidateInputDetails(dtBreakUp);
                    resultArgs = breakupSystem.GetBreakUpDetails(dtBreakUp);
                    if (resultArgs.Success & resultArgs.RowsAffected > 0)
                        this.ShowSuccessMessage(GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                }
            }
            else
                ShowMessageBox(GetMessage(MessageCatalog.Master.BreakUp.BREAKUP_NOT_TALLIED));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods
       
        private DataTable FetchBreakUpDetails()
        {
            using (BreakUpSystem breakUpSystem = new BreakUpSystem())
            {
                breakUpSystem.AccounNumber = Accountnumber;
                resultArgs = breakUpSystem.FetchBreakUpDetails();
            }
            return resultArgs.DataSource.Table;
        }

        private void CalculatePercentage()
        {
            try
            {
                int RowCount = 0;
                DataView dvFDBreakUp = gvFDBreakUp.DataSource as DataView;
              //  DataTable dtFDBreakUp = dvFDBreakUp.ToTable();
                DataTable dtFDBreakUp = gcFDBreakUp.DataSource as DataTable;
              
                foreach (DataRow dr in dtFDBreakUp.Rows)
                {
                    dtFDBreakUp.Rows[RowCount]["INTEREST_AMOUNT"] = (UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString()) * UtilityMember.NumberSet.ToDouble(dr["INTEREST_RATE"].ToString())) / 100;
                    RowCount++;
                }
              //  gcFDBreakUp.DataSource = dtFDBreakUp;
            }
            catch (Exception e)
            {
                MessageRender.ShowMessage(e.Message);
            }

        }
        private void ValidateInputDetails(DataTable dtInput)
        {
            if (dtInput != null & dtInput.Rows.Count > 0)
            {
                // gvFDBreakUp.GetFocusedRow();
                //  gvColFDNo.AppearanceCell.BackColor = Color.Red;
                //SetErrorInfo( "Address hasn't been entered", ErrorType.Information);
            }
        }
        #endregion

    }
}
