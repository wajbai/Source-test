using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class FinalReceiptsPaymentsPrevious : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public FinalReceiptsPaymentsPrevious()
        {
            InitializeComponent();
        }

        #endregion

        #region Decelartion
        double ReceiptOPAmt = 0;
        double ReceiptsAmt = 0;
        double ReceiptOPAmtPrevious = 0;
        double ReceiptsAmtPrevious = 0;
        double PaymentsAmt = 0;
        double PaymentClAmt = 0;
        double PaymentsAmtPrevious = 0;
        double PaymentClAmtPrevious = 0;

        float ReceiptCodeWidth;
        float ReceiptNameWidth;
        float ReceiptAmountWidth;
        float PaymentCodeWidth;
        float PaymentNameWidth;
        float PaymentAmountWidth;
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            ReceiptCodeWidth = xrCapReceiptCode.WidthF;
            if (xrCapReceiptCode.Tag != null && xrCapReceiptCode.Tag.ToString() != "")
            {
                ReceiptCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapReceiptCode.Tag.ToString());
            }
            else
            {
                xrCapReceiptCode.Tag = xrCapReceiptCode.WidthF;
            }
            ReceiptNameWidth = xrReceiptLedgerName.WidthF;
            if (xrReceiptLedgerName.Tag != null && xrReceiptLedgerName.Tag.ToString() != "")
            {
                ReceiptNameWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrReceiptLedgerName.Tag.ToString());
            }
            else
            {
                xrReceiptLedgerName.Tag = xrReceiptLedgerName.WidthF;
            }
            ReceiptAmountWidth = xrCapReceiptAmount.WidthF;
            if (xrCapReceiptAmount.Tag != null && xrCapReceiptAmount.Tag.ToString() != "")
            {
                ReceiptAmountWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapReceiptAmount.Tag.ToString());
            }
            else
            {
                xrCapReceiptAmount.Tag = xrCapReceiptAmount.WidthF;
            }
            PaymentCodeWidth = xrCapPaymentCode.WidthF;
            if (xrCapPaymentCode.Tag != null && xrCapPaymentCode.Tag.ToString() != "")
            {
                PaymentCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapPaymentCode.Tag.ToString());
            }
            else
            {
                xrCapPaymentCode.Tag = xrCapReceiptCode.WidthF;
            }
            PaymentNameWidth = xrCapPaymentLedgerName.WidthF;
            if (xrCapPaymentLedgerName.Tag != null && xrCapPaymentLedgerName.Tag.ToString() != "")
            {
                PaymentNameWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapPaymentLedgerName.Tag.ToString());
            }
            else
            {
                xrCapPaymentLedgerName.Tag = xrCapPaymentLedgerName.WidthF;
            }
            PaymentAmountWidth = xrCapPaymentAmount.WidthF;
            if (xrCapPaymentAmount.Tag != null && xrCapPaymentAmount.Tag.ToString() != "")
            {
                PaymentAmountWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapPaymentAmount.Tag.ToString());
            }
            else
            {
                xrCapPaymentAmount.Tag = xrCapPaymentAmount.WidthF;
            }
            BindReceiptPaymentsSource();
        }
        #endregion

        #region Methods

        public void BindReceiptPaymentsSource()
        {
            this.SetLandscapeHeader = this.PageSize.Width - 50;
            this.SetLandscapeFooter = this.PageSize.Width - 50;
            this.SetLandscapeFooterDateWidth = this.PageSize.Width - 50;

            setHeaderTitleAlignment();
            SetReportTitle();

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            ReceiptsPrevious receiptLedgers = xrSubReceipts.ReportSource as ReceiptsPrevious;
            receiptLedgers.HideReceiptReportHeader();
            PaymentsPrevious paymentLedgers = xrSubPayments.ReportSource as PaymentsPrevious;
            paymentLedgers.HidePaymentReportHeader();

            this.AttachDrillDownToSubReport(receiptLedgers);
            this.AttachDrillDownToSubReport(paymentLedgers);

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                                || this.ReportProperties.Project == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        AccountBalancePreviousYear accountBalance = xrSubOpeningBalance.ReportSource as AccountBalancePreviousYear;
                        accountBalance.BankClosedDate = this.AppSetting.YearFromPrevious; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                        accountBalance.BindBalance(true, false);
                        this.AttachDrillDownToSubReport(accountBalance);
                        accountBalance.AmountProgressiveHeaderColumnWidth = 0;
                        accountBalance.AmountProgressiveColumnWidth = 0;
                        accountBalance.AmountProgressVisible = false;
                        accountBalance.GroupProgressVisible = false;
                        ReceiptOPAmt = accountBalance.PeriodBalanceAmount;
                        ReceiptOPAmtPrevious = accountBalance.PreviousBalanceAmount;
                        receiptLedgers.BindReceiptSource(TransType.RC);
                        ReceiptsAmt = receiptLedgers.ReceiptAmount;
                        ReceiptsAmtPrevious = receiptLedgers.ReceiptAmountPrevious;
                        receiptLedgers.PaymentCostCentreNameVisible = false;
                        receiptLedgers.CostCentreCategoryNameWidth = xrCapReceiptCode.WidthF + xrReceiptLedgerName.WidthF + xrCapReceiptAmount.WidthF;
                        receiptLedgers.PaymentCostCentreNameVisible = false;

                        if (ReportProperties.ShowLedgerCode == 1)
                        {
                            accountBalance.CodeColumnWidth = ReceiptCodeWidth;
                            accountBalance.NameColumnWidth = ReceiptNameWidth;
                            receiptLedgers.CodeColumnWidth = ReceiptCodeWidth;
                            receiptLedgers.NameColumnWidth = ReceiptNameWidth;
                            receiptLedgers.AmountColumnWidth = ReceiptAmountWidth;
                            accountBalance.AmountColumnWidth = ReceiptAmountWidth;

                            accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                        }
                        else
                        {
                            accountBalance.CodeColumnWidth = 0;
                            accountBalance.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                            receiptLedgers.CodeColumnWidth = 0;
                            receiptLedgers.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                            receiptLedgers.AmountColumnWidth = ReceiptAmountWidth;
                            accountBalance.AmountColumnWidth = ReceiptAmountWidth;

                            accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                        }
                        if (ReportProperties.ShowGroupCode == 1)
                        {
                            accountBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                            accountBalance.NameHeaderColumWidth = ReceiptNameWidth;
                            receiptLedgers.GroupCodeColumnWidth = ReceiptCodeWidth;
                            receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth;
                            receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                            accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth;

                            accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                        }
                        else
                        {
                            accountBalance.CodeHeaderColumWidth = 0;
                            accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                            receiptLedgers.GroupCodeColumnWidth = 0;
                            receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth;
                            receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                            accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth;
                            if (ReportProperties.ShowByLedgerGroup == 1)
                            {
                                receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                                receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                            }

                            accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                        }

                        paymentLedgers.BindPaymentSource(TransType.PY);
                        PaymentsAmt = paymentLedgers.PaymentAmout;
                        PaymentsAmtPrevious = paymentLedgers.PaymentAmoutPrevious;
                        AccountBalancePreviousYear accountClosingBalance = xrSubClosingBalance.ReportSource as AccountBalancePreviousYear;
                        accountClosingBalance.BankClosedDate = this.AppSetting.YearFromPrevious; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                        accountClosingBalance.BindBalance(false, false);
                        this.AttachDrillDownToSubReport(accountClosingBalance);
                        PaymentClAmt = accountClosingBalance.PeriodBalanceAmount;
                        PaymentClAmtPrevious = accountClosingBalance.PreviousBalanceAmount;
                        paymentLedgers.CostCentreNameVisible = false;

                        paymentLedgers.CategoryNameWidth = xrCapPaymentCode.WidthF + xrCapPaymentAmount.WidthF + xrCapPaymentLedgerName.WidthF;

                        accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = 0;
                        accountClosingBalance.AmountProgressVisible = false;
                        accountClosingBalance.GroupProgressVisible = false;
                        if (ReportProperties.ShowLedgerCode == 1)
                        {
                            if (ReportProperties.ShowByLedgerGroup == 1)
                            {
                                accountClosingBalance.CodeColumnWidth = ReceiptCodeWidth;
                                accountClosingBalance.NameColumnWidth = PaymentNameWidth;
                                paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 2;
                                paymentLedgers.NameColumnWidth = PaymentNameWidth + 0.5F;
                                paymentLedgers.AmountColumnWidth = PaymentAmountWidth + 1;
                                accountClosingBalance.AmountColumnWidth = PaymentAmountWidth;

                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                            }
                            else
                            {
                                accountClosingBalance.CodeColumnWidth = ReceiptCodeWidth;
                                accountClosingBalance.NameColumnWidth = PaymentNameWidth;
                                paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 2;
                                paymentLedgers.NameColumnWidth = PaymentNameWidth + 1;
                                paymentLedgers.AmountColumnWidth = PaymentAmountWidth;
                                accountClosingBalance.AmountColumnWidth = PaymentAmountWidth;

                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                            }

                        }
                        else
                        {
                            accountClosingBalance.CodeColumnWidth = 0;
                            accountClosingBalance.NameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 2;
                            paymentLedgers.CodeColumnWidth = 0;
                            paymentLedgers.NameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 3;
                            paymentLedgers.AmountColumnWidth = PaymentAmountWidth;
                            accountClosingBalance.AmountColumnWidth = PaymentAmountWidth;

                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;

                        }
                        if (ReportProperties.ShowGroupCode == 1)
                        {
                            if (ReportProperties.ShowByLedgerGroup == 1)
                            {
                                accountClosingBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                                accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth;
                                paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth - 2;
                                paymentLedgers.GroupNameColumnWidth = PaymentNameWidth + 1;
                                paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth + 0.5F;
                                accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;

                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                            }
                            else
                            {
                                accountClosingBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                                accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth;
                                paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth - 2;
                                paymentLedgers.GroupNameColumnWidth = PaymentNameWidth;
                                paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth;
                                accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth + 1;

                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                            }

                        }
                        else
                        {
                            if (ReportProperties.ShowByLedgerGroup == 1)
                            {
                                accountClosingBalance.CodeHeaderColumWidth = 0;
                                accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth + 1;
                                paymentLedgers.GroupCodeColumnWidth = 0;
                                paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 3;
                                paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth;
                                if (ReportProperties.ShowDetailedBalance == 1)
                                {
                                    accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;
                                }
                                else
                                {
                                    accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth - 2;
                                }

                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                            }
                            else if (ReportProperties.ShowByLedger == 1)
                            {
                                accountClosingBalance.CodeHeaderColumWidth = 0;
                                accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth - 1;
                                paymentLedgers.GroupCodeColumnWidth = 0;
                                paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + PaymentNameWidth;
                                paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth;
                                accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;

                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                            }
                            else
                            {
                                accountClosingBalance.CodeHeaderColumWidth = 0;
                                accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth;
                                paymentLedgers.GroupCodeColumnWidth = 0;
                                paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + PaymentNameWidth;
                                paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth;
                                accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;

                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                            }

                        }

                        SetReportSetting();
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    AccountBalancePreviousYear accountBalance = xrSubOpeningBalance.ReportSource as AccountBalancePreviousYear;
                    accountBalance.BankClosedDate = this.AppSetting.YearFromPrevious; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    accountBalance.BindBalance(true, true);
                    this.AttachDrillDownToSubReport(accountBalance);
                    accountBalance.AmountProgressiveHeaderColumnWidth = 0;
                    accountBalance.AmountProgressiveColumnWidth = 0;
                    accountBalance.AmountProgressVisible = false;
                    accountBalance.GroupProgressVisible = false;
                    ReceiptOPAmt = accountBalance.PeriodBalanceAmount;
                    ReceiptOPAmtPrevious = accountBalance.PreviousBalanceAmount;
                    receiptLedgers.BindReceiptSource(TransType.RC);
                    ReceiptsAmt = receiptLedgers.ReceiptAmount;
                    ReceiptsAmtPrevious = receiptLedgers.ReceiptAmountPrevious;
                    receiptLedgers.PaymentCostCentreNameVisible = false;
                    receiptLedgers.CostCentreCategoryNameWidth = xrCapReceiptCode.WidthF + xrReceiptLedgerName.WidthF + xrCapReceiptAmount.WidthF;
                    receiptLedgers.PaymentCostCentreNameVisible = false;

                    if (ReportProperties.ShowLedgerCode == 1)
                    {
                        accountBalance.CodeColumnWidth = ReceiptCodeWidth;
                        accountBalance.NameColumnWidth = ReceiptNameWidth;
                        receiptLedgers.CodeColumnWidth = ReceiptCodeWidth;
                        receiptLedgers.NameColumnWidth = ReceiptNameWidth;
                        receiptLedgers.AmountColumnWidth = ReceiptAmountWidth;
                        accountBalance.AmountColumnWidth = ReceiptAmountWidth;

                        accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                    }
                    else
                    {
                        accountBalance.CodeColumnWidth = 0;
                        accountBalance.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                        receiptLedgers.CodeColumnWidth = 0;
                        receiptLedgers.NameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                        receiptLedgers.AmountColumnWidth = ReceiptAmountWidth;
                        accountBalance.AmountColumnWidth = ReceiptAmountWidth;

                        accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                    }
                    if (ReportProperties.ShowGroupCode == 1)
                    {
                        accountBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                        accountBalance.NameHeaderColumWidth = ReceiptNameWidth;
                        receiptLedgers.GroupCodeColumnWidth = ReceiptCodeWidth;
                        receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth;
                        receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                        accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth - 1;

                        accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                    }
                    else
                    {
                        accountBalance.CodeHeaderColumWidth = 0;
                        accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                        receiptLedgers.GroupCodeColumnWidth = 0;
                        receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth;
                        receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                        accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth;
                        if (ReportProperties.ShowByLedgerGroup == 1)
                        {
                            receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                            receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                        }

                        accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                    }

                    paymentLedgers.BindPaymentSource(TransType.PY);
                    PaymentsAmt = paymentLedgers.PaymentAmout;
                    PaymentsAmtPrevious = paymentLedgers.PaymentAmoutPrevious;
                    AccountBalancePreviousYear accountClosingBalance = xrSubClosingBalance.ReportSource as AccountBalancePreviousYear;
                    accountClosingBalance.BankClosedDate = this.AppSetting.YearFromPrevious; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    accountClosingBalance.BindBalance(false, true);
                    this.AttachDrillDownToSubReport(accountClosingBalance);
                    PaymentClAmt = accountClosingBalance.ProgressiveBalanceAmount;
                    PaymentClAmtPrevious = accountClosingBalance.PreviousBalanceAmount;
                    paymentLedgers.CostCentreNameVisible = false;

                    paymentLedgers.CategoryNameWidth = xrCapPaymentCode.WidthF + xrCapPaymentAmount.WidthF + xrCapPaymentLedgerName.WidthF;

                    accountClosingBalance.AmountProgressiveColumnWidth = 0;
                    accountClosingBalance.AmountProgressiveHeaderColumnWidth = 0;
                    accountClosingBalance.AmountProgressVisible = false;
                    accountClosingBalance.GroupProgressVisible = false;
                    if (ReportProperties.ShowLedgerCode == 1)
                    {
                        if (ReportProperties.ShowByLedgerGroup == 1)
                        {
                            accountClosingBalance.CodeColumnWidth = ReceiptCodeWidth;
                            accountClosingBalance.NameColumnWidth = PaymentNameWidth;
                            paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 2;
                            paymentLedgers.NameColumnWidth = PaymentNameWidth + 0.5F;
                            paymentLedgers.AmountColumnWidth = PaymentAmountWidth + 1;
                            accountClosingBalance.AmountColumnWidth = PaymentAmountWidth;

                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        }
                        else
                        {
                            accountClosingBalance.CodeColumnWidth = ReceiptCodeWidth;
                            accountClosingBalance.NameColumnWidth = PaymentNameWidth;
                            paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 2;
                            paymentLedgers.NameColumnWidth = PaymentNameWidth + 1;
                            paymentLedgers.AmountColumnWidth = PaymentAmountWidth;
                            accountClosingBalance.AmountColumnWidth = PaymentAmountWidth;

                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        }

                    }
                    else
                    {
                        accountClosingBalance.CodeColumnWidth = 0;
                        accountClosingBalance.NameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 2;
                        paymentLedgers.CodeColumnWidth = 0;
                        paymentLedgers.NameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 3;
                        paymentLedgers.AmountColumnWidth = PaymentAmountWidth;
                        accountClosingBalance.AmountColumnWidth = PaymentAmountWidth;

                        accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;

                    }
                    if (ReportProperties.ShowGroupCode == 1)
                    {
                        if (ReportProperties.ShowByLedgerGroup == 1)
                        {
                            accountClosingBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                            accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth;
                            paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth - 2;
                            paymentLedgers.GroupNameColumnWidth = PaymentNameWidth + 1;
                            paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth + 0.5F;
                            accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;

                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        }
                        else
                        {
                            accountClosingBalance.CodeHeaderColumWidth = ReceiptCodeWidth;
                            accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth;
                            paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth - 2;
                            paymentLedgers.GroupNameColumnWidth = PaymentNameWidth;
                            paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth;
                            accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth + 1;

                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        }

                    }
                    else
                    {
                        if (ReportProperties.ShowByLedgerGroup == 1)
                        {
                            accountClosingBalance.CodeHeaderColumWidth = 0;
                            accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth + 1;
                            paymentLedgers.GroupCodeColumnWidth = 0;
                            paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + PaymentNameWidth - 3;
                            paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth;
                            if (ReportProperties.ShowDetailedBalance == 1)
                            {
                                accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;
                            }
                            else
                            {
                                accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth - 2;
                            }

                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        }
                        else if (ReportProperties.ShowByLedger == 1)
                        {
                            accountClosingBalance.CodeHeaderColumWidth = 0;
                            accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth - 1;
                            paymentLedgers.GroupCodeColumnWidth = 0;
                            paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + PaymentNameWidth;
                            paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth;
                            accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth - 1;

                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        }
                        else
                        {
                            accountClosingBalance.CodeHeaderColumWidth = 0;
                            accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth;
                            paymentLedgers.GroupCodeColumnWidth = 0;
                            paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + PaymentNameWidth;
                            paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth;
                            accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;

                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        }

                    }

                    //06/06/2017, To change grand total caption with amount filter
                    string AmountFilter = this.GetAmountFilter();
                    lblAmountFilter.Visible = false;
                    if (AmountFilter != "")
                    {
                        lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                        lblAmountFilter.Visible = true;
                    }
                    SetReportSetting();

                    //On 03/07/2017, If there is no records hide, all group tables, otherwise it shows emtpy tables
                    Detail.Visible = true;
                    if (!receiptLedgers.RecordsExists && !paymentLedgers.RecordsExists)
                    {
                        Detail.Visible = false;
                    }


                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }

        private void SetReportSetting()
        {
            float actualCodeWidth = xrCapReceiptCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapReceiptCode.Tag != null && xrCapReceiptCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapReceiptCode.Tag.ToString());
            }
            else
            {
                xrCapReceiptCode.Tag = xrCapReceiptCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapReceiptCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            // this.ReportPeriod = this.ReportProperties.ReportDate;

            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTblOpeningBalance = AlignOpeningBalanceTable(xrTblOpeningBalance);
            xtTblClosingBalance = AlignClosingBalance(xtTblClosingBalance);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
            xrLineRight.ForeColor = xrLineLeft.ForeColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

            this.SetCurrencyFormat(xrCapPaymentAmount.Text, xrCapPaymentAmount);
            this.SetCurrencyFormat(xrCapReceiptAmount.Text, xrCapReceiptAmount);
            this.SetCurrencyFormat(xrCapPaymentAmountPrevious.Text, xrCapPaymentAmountPrevious);
            this.SetCurrencyFormat(xrCapReceiptAmountPrevious.Text, xrCapReceiptAmountPrevious);
        }

        public override XRTable AlignHeaderTable(XRTable table, bool UseSameFont = false)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            }

                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }
            return table;
        }

        public override XRTable AlignTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignOpeningBalanceTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignClosingBalance(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        #endregion

        private void xrReceiptAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ReceiptOPAmt + ReceiptsAmt;
            e.Handled = true;
        }

        private void xrPaymentAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PaymentClAmt + PaymentsAmt;
            e.Handled = true;
        }

        private void SetTotalTableBorders()
        {
            foreach (XRTableRow trow in xrtblGrandTotal.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }

        }

        private void xrReceiptAmtPrevious_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ReceiptOPAmtPrevious + ReceiptsAmtPrevious;
            e.Handled = true;
        }

        private void xrPaymentAmtPrevious_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PaymentClAmtPrevious + PaymentsAmtPrevious;
            e.Handled = true;
        }

        private void FinalReceiptsPaymentsPrevious_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
