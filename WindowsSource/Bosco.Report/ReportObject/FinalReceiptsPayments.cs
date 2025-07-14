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
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Export;
using System.Windows.Forms;
using System.Linq;

namespace Bosco.Report.ReportObject
{
    public partial class FinalReceiptsPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        double ReceiptsAmt = 0;
        double PaymentsAmt = 0;
        double ReceiptOPAmt = 0;
        double PaymentClAmt = 0;
        float ReceiptCodeWidth;
        float ReceiptNameWidth;
        float ReceiptAmountWidth;
        float PaymentCodeWidth;
        float PaymentNameWidth;
        float PaymentAmountWidth;
        string CapReceiptAmount = string.Empty;
        string CapPaymentAmount = string.Empty;
        #endregion

        #region Constructor
        public FinalReceiptsPayments()
        {
            InitializeComponent();
            CapReceiptAmount = xrCapReceiptAmount.Text;
            CapPaymentAmount = xrCapPaymentAmount.Text;
        }

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

            AlignSubReportWidths();

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
            //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            // this.ReportTitle = this.ReportProperties.ReportTitle;
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            setHeaderTitleAlignment();
            SetReportTitle();
            // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            Receipts receiptLedgers = xrSubReceipts.ReportSource as Receipts;
            receiptLedgers.HideReceiptReportHeader();
            Payments paymentLedgers = xrSubPayments.ReportSource as Payments;
            paymentLedgers.HidePaymentReportHeader();

            this.AttachDrillDownToSubReport(receiptLedgers);
            this.AttachDrillDownToSubReport(paymentLedgers);

            //On 17/12/2018, Set Sign details
            FixReportPropertyForCMF();
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrtblHeaderCaption.WidthF;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();


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
                        AccountBalance accountBalance = xrSubOpeningBalance.ReportSource as AccountBalance;
                        accountBalance.BankClosedDate = this.ReportProperties.DateFrom;
                        accountBalance.BindBalance(true, true); //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                        this.AttachDrillDownToSubReport(accountBalance);

                        accountBalance.AmountProgressiveHeaderColumnWidth = 0;
                        accountBalance.AmountProgressiveColumnWidth = 0;
                        accountBalance.AmountProgressVisible = false;
                        accountBalance.GroupProgressVisible = false;
                        ReceiptOPAmt = accountBalance.PeriodBalanceAmount;
                        receiptLedgers.BindReceiptSource(TransType.RC);
                        ReceiptsAmt = receiptLedgers.ReceiptAmount;
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
                            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Province)
                            {
                                accountBalance.CodeHeaderColumWidth = 0; //ReceiptCodeWidth; 25.08.2022
                                accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth + 4;  // ReceiptNameWidth 25.08.2022
                                receiptLedgers.GroupCodeColumnWidth = ReceiptCodeWidth;
                                receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth;
                                receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                                accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth;  //ReceiptAmountWidth - 1; 25.08.2022

                                receiptLedgers.ParentGroupCodeColumnWidth = 0;
                                receiptLedgers.ParentGroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                                receiptLedgers.ParentGroupAmountColumnWidth = ReceiptAmountWidth;

                                accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                            }
                            else if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                            {
                                accountBalance.CodeHeaderColumWidth = 0; //ReceiptCodeWidth; 25.08.2022
                                accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth + 4;  // ReceiptNameWidth 25.08.2022
                                receiptLedgers.GroupCodeColumnWidth = ReceiptCodeWidth;
                                receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth;
                                receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                                accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth;  //ReceiptAmountWidth - 1; 25.08.2022

                                receiptLedgers.ParentGroupCodeColumnWidth = ReceiptCodeWidth;
                                receiptLedgers.ParentGroupNameColumnWidth = ReceiptNameWidth;
                                receiptLedgers.ParentGroupAmountColumnWidth = ReceiptAmountWidth;
                                accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                            }
                        }
                        else // Standard
                        {
                            accountBalance.CodeHeaderColumWidth = 0;
                            accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth - 2; //ReceiptNameWidth + ReceiptCodeWidth - 2 25.08.2022 
                            receiptLedgers.GroupCodeColumnWidth = 0;
                            receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth; // 01.09.2022
                            receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;  // 01.09.2022
                            accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth + 2;
                            if (ReportProperties.ShowByLedgerGroup == 1)
                            {
                                receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                                receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                            }
                            accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                        }

                        paymentLedgers.BindPaymentSource(TransType.PY);
                        PaymentsAmt = paymentLedgers.PaymentAmout;
                        AccountBalance accountClosingBalance = xrSubClosingBalance.ReportSource as AccountBalance;
                        accountClosingBalance.BankClosedDate = this.ReportProperties.DateFrom;
                        accountClosingBalance.FDLedgerHasOpening = accountBalance.FDLedgerHasOpening;
                        accountClosingBalance.BindBalance(false, true); //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                        this.AttachDrillDownToSubReport(accountClosingBalance);
                        PaymentClAmt = accountClosingBalance.ProgressiveBalanceAmount;
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
                                paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 0.5F; // Temp / 18/04/2024 // ReceiptCodeWidth - 2 25.08.2022
                                paymentLedgers.NameColumnWidth = PaymentNameWidth - 1F; //  2.5F;    //PaymentNameWidth + 0.5F; 25.08.2022
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
                                if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Province)
                                {
                                    accountClosingBalance.CodeHeaderColumWidth = 0;  //ReceiptCodeWidth; 25.08.2022
                                    accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth + ReceiptCodeWidth; // PaymentNameWidth 25.08.2022
                                    paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth; //ReceiptCodeWidth - 2; 25.08.2022
                                    paymentLedgers.GroupNameColumnWidth = PaymentNameWidth + 3;
                                    paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth + 0.5F;
                                    accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;
                                    accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                                    paymentLedgers.ParentGroupCodeColumnWidth = 0;
                                    paymentLedgers.ParentGroupNameColumnWidth = PaymentNameWidth + ReceiptCodeWidth;
                                    paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth;
                                }
                                else if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                                {
                                    accountClosingBalance.CodeHeaderColumWidth = 0;  //ReceiptCodeWidth; 25.08.2022
                                    accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth + ReceiptCodeWidth; // PaymentNameWidth 25.08.2022
                                    paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth; //ReceiptCodeWidth - 2; 25.08.2022
                                    paymentLedgers.GroupNameColumnWidth = PaymentNameWidth + 3;
                                    paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth + 0.5F;
                                    accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;
                                    accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                                    paymentLedgers.ParentGroupCodeColumnWidth = ReceiptCodeWidth - 1;
                                    paymentLedgers.ParentGroupNameColumnWidth = PaymentNameWidth + 1;
                                    paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth;
                                }
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
                        else // standard
                        {
                            if (ReportProperties.ShowByLedgerGroup == 1)
                            {
                                accountClosingBalance.CodeHeaderColumWidth = 0;
                                accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth - 7; // 25.08.2022
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
                                paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + PaymentNameWidth; // 01.09.2022
                                paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth; // 01.09.2022
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

                        //On 26/06/2019, to show Income/Expense heder caption if and if only show group is disabled ------------------------
                        xrTblIncomeCaption.Visible = xrTblExpenseCaption.Visible = false;
                        xrSubReceipts.TopF = xrSubPayments.TopF = 0;
                        if (ReportProperties.ShowByLedgerGroup == 0 && Detail.Visible)
                        {
                            xrTblIncomeCaption.Visible = xrTblExpenseCaption.Visible = true;
                            xrSubReceipts.TopF = xrSubPayments.TopF = xrTblIncomeCaption.HeightF;
                        }

                        // 25/04/2025, *Chinna
                        SetCodeHeaderVisibility(accountBalance, accountClosingBalance);

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
                    AccountBalance accountBalance = xrSubOpeningBalance.ReportSource as AccountBalance;
                    accountBalance.BankClosedDate = this.ReportProperties.DateFrom;
                    accountBalance.BindBalance(true, true); //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    this.AttachDrillDownToSubReport(accountBalance);

                    accountBalance.AmountProgressiveHeaderColumnWidth = 0;
                    accountBalance.AmountProgressiveColumnWidth = 0;
                    accountBalance.AmountProgressVisible = false;
                    accountBalance.GroupProgressVisible = false;
                    ReceiptOPAmt = accountBalance.PeriodBalanceAmount;
                    receiptLedgers.BindReceiptSource(TransType.RC);
                    ReceiptsAmt = receiptLedgers.ReceiptAmount;
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
                        if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Province)
                        {
                            accountBalance.CodeHeaderColumWidth = 0; //ReceiptCodeWidth; 25.08.2022
                            accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth + 4;  // ReceiptNameWidth 25.08.2022
                            receiptLedgers.GroupCodeColumnWidth = ReceiptCodeWidth;
                            receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth;
                            receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                            accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth;  //ReceiptAmountWidth - 1; 25.08.2022

                            receiptLedgers.ParentGroupCodeColumnWidth = 0;
                            receiptLedgers.ParentGroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                            receiptLedgers.ParentGroupAmountColumnWidth = ReceiptAmountWidth;

                            accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                        }
                        else if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                        {
                            accountBalance.CodeHeaderColumWidth = 0; //ReceiptCodeWidth; 25.08.2022
                            accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth + 4;  // ReceiptNameWidth 25.08.2022
                            receiptLedgers.GroupCodeColumnWidth = ReceiptCodeWidth;
                            receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth;
                            receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                            accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth;  //ReceiptAmountWidth - 1; 25.08.2022

                            receiptLedgers.ParentGroupCodeColumnWidth = ReceiptCodeWidth;
                            receiptLedgers.ParentGroupNameColumnWidth = ReceiptNameWidth;
                            receiptLedgers.ParentGroupAmountColumnWidth = ReceiptAmountWidth;
                            accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                        }
                    }
                    else // Standard
                    {
                        accountBalance.CodeHeaderColumWidth = 0;
                        accountBalance.NameHeaderColumWidth = ReceiptNameWidth + ReceiptCodeWidth - 2; //ReceiptNameWidth + ReceiptCodeWidth - 2 25.08.2022 
                        receiptLedgers.GroupCodeColumnWidth = 0;
                        receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth; // 01.09.2022
                        receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;  // 01.09.2022
                        accountBalance.AmountHeaderColumWidth = ReceiptAmountWidth + 2;
                        if (ReportProperties.ShowByLedgerGroup == 1)
                        {
                            receiptLedgers.GroupNameColumnWidth = ReceiptNameWidth + ReceiptCodeWidth - 2;
                            receiptLedgers.GroupAmountColumnWidth = ReceiptAmountWidth;
                        }
                        accountBalance.AmountProgressiveHeaderColumnWidth = accountBalance.AmountProgressiveColumnWidth = 0;
                    }

                    paymentLedgers.BindPaymentSource(TransType.PY);
                    PaymentsAmt = paymentLedgers.PaymentAmout;
                    AccountBalance accountClosingBalance = xrSubClosingBalance.ReportSource as AccountBalance;
                    accountClosingBalance.BankClosedDate = this.ReportProperties.DateFrom;
                    accountClosingBalance.FDLedgerHasOpening = accountBalance.FDLedgerHasOpening;
                    accountClosingBalance.BindBalance(false, true); //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    this.AttachDrillDownToSubReport(accountClosingBalance);
                    PaymentClAmt = accountClosingBalance.ProgressiveBalanceAmount;
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
                            paymentLedgers.CodeColumnWidth = ReceiptCodeWidth - 0.5F; // ReceiptCodeWidth - 2 25.08.2022
                            paymentLedgers.NameColumnWidth = PaymentNameWidth + 2.5F;    //PaymentNameWidth + 0.5F; 25.08.2022
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
                            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Province)
                            {
                                accountClosingBalance.CodeHeaderColumWidth = 0;  //ReceiptCodeWidth; 25.08.2022
                                accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth + ReceiptCodeWidth; // PaymentNameWidth 25.08.2022
                                paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth; //ReceiptCodeWidth - 2; 25.08.2022
                                paymentLedgers.GroupNameColumnWidth = PaymentNameWidth + 3;
                                paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth + 0.5F;
                                accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;
                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                                paymentLedgers.ParentGroupCodeColumnWidth = 0;
                                paymentLedgers.ParentGroupNameColumnWidth = PaymentNameWidth + ReceiptCodeWidth;
                                paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth;
                            }
                            else if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                            {
                                accountClosingBalance.CodeHeaderColumWidth = 0;  //ReceiptCodeWidth; 25.08.2022
                                accountClosingBalance.NameHeaderColumWidth = PaymentNameWidth + ReceiptCodeWidth; // PaymentNameWidth 25.08.2022
                                paymentLedgers.GroupCodeColumnWidth = ReceiptCodeWidth; //ReceiptCodeWidth - 2; 25.08.2022
                                paymentLedgers.GroupNameColumnWidth = PaymentNameWidth + 3;
                                paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth + 0.5F;
                                accountClosingBalance.AmountHeaderColumWidth = PaymentAmountWidth;
                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                                paymentLedgers.ParentGroupCodeColumnWidth = ReceiptCodeWidth - 1;
                                paymentLedgers.ParentGroupNameColumnWidth = PaymentNameWidth + 1;
                                paymentLedgers.ParentGroupAmountColumnWidth = PaymentAmountWidth;
                            }
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
                    else // standard
                    {
                        if (ReportProperties.ShowByLedgerGroup == 1)
                        {
                            accountClosingBalance.CodeHeaderColumWidth = 0;
                            accountClosingBalance.NameHeaderColumWidth = ReceiptCodeWidth + PaymentNameWidth - 7; // 25.08.2022
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
                            paymentLedgers.GroupNameColumnWidth = ReceiptCodeWidth + PaymentNameWidth; // 01.09.2022
                            paymentLedgers.GroupAmountColumnWidth = PaymentAmountWidth; // 01.09.2022
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

                    //On 26/06/2019, to show Income/Expense heder caption if and if only show group is disabled ------------------------
                    xrTblIncomeCaption.Visible = xrTblExpenseCaption.Visible = false;
                    xrSubReceipts.TopF = xrSubPayments.TopF = 0;
                    if (ReportProperties.ShowByLedgerGroup == 0 && Detail.Visible)
                    {
                        xrTblIncomeCaption.Visible = xrTblExpenseCaption.Visible = true;
                        xrSubReceipts.TopF = xrSubPayments.TopF = xrTblIncomeCaption.HeightF;
                    }


                    //--------------------------------------------------------------------------------------------------------------------

                    // 25/04/2025, *Chinna
                    SetCodeHeaderVisibility(accountBalance, accountClosingBalance);

                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }


            }

            //08/10/2024, To Show Forex split -----------------------------------------------------
            xrsubforex.Visible = false;
            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                xrsubforex.Visible = true;
                UcForexSplit forexsplit = xrsubforex.ReportSource as UcForexSplit;
                xrsubforex.WidthF = xrCapReceiptCode.WidthF + xrReceiptLedgerName.WidthF + xrCapReceiptAmount.WidthF;
                forexsplit.CurrencyNameWidth = (xrCapReceiptCode.WidthF + xrReceiptLedgerName.WidthF) / 2;
                forexsplit.GainWidth = (xrCapReceiptCode.WidthF + xrReceiptLedgerName.WidthF) / 2;
                forexsplit.LossWidth = xrCapReceiptAmount.WidthF;
                forexsplit.DateAsOn = string.Empty;
                forexsplit.DateFrom = ReportProperties.DateFrom;
                forexsplit.DateTo = ReportProperties.DateTo;
                forexsplit.ShowForex();
            }
            //-------------------------------------------------------------------------------------
        }

        private void AlignSubReportWidths()
        {
            float totalReceiptWidth = xrCapReceiptCode.WidthF + xrReceiptLedgerName.WidthF + xrCapReceiptAmount.WidthF;
            xrSubOpeningBalance.WidthF = totalReceiptWidth;

            xrTblOpeningBalance.WidthF = totalReceiptWidth;
            xrTblIncomeCaption.WidthF = totalReceiptWidth;
            xrTblExpenseCaption.WidthF = totalReceiptWidth;
            xtTblClosingBalance.WidthF = totalReceiptWidth;
            xrsubforex.WidthF = totalReceiptWidth;
        }

        public void SetCodeHeaderVisibility(AccountBalance openingBalance, AccountBalance closingBalance)
        {
            bool conditionForTopGroupsOnly = (this.settingProperty.IS_SAPPIC &&
                                              this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate);

            openingBalance.CodeHeaderColumWidth = conditionForTopGroupsOnly ? ReceiptCodeWidth : 0;
            closingBalance.CodeHeaderColumWidth = conditionForTopGroupsOnly ? ReceiptCodeWidth : 0;
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
            xrTblIncomeCaption = AlignOpeningBalanceTable(xrTblIncomeCaption);
            xrTblExpenseCaption = AlignOpeningBalanceTable(xrTblExpenseCaption);
            xtTblClosingBalance = AlignClosingBalance(xtTblClosingBalance);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
            xrCrossBandLine1.ForeColor = xrCrossBandLine2.ForeColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

            if (settingProperty.AllowMultiCurrency == 1)
            {
                xrCapReceiptAmount.Text = CapReceiptAmount + " (" + ReportProperties.CurrencyCountrySymbol + ")";
                xrCapPaymentAmount.Text = CapPaymentAmount + " (" + ReportProperties.CurrencyCountrySymbol + ")";
            }
            else
            {
                this.SetCurrencyFormat(xrCapPaymentAmount.Text, xrCapPaymentAmount);
                this.SetCurrencyFormat(xrCapReceiptAmount.Text, xrCapReceiptAmount);
            }
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
        private void xrReceiptAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ReceiptOPAmt + ReceiptsAmt;
            e.Handled = true;
        }
        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

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

        private void GenerateFinalData(DataTable dtReceipts, DataTable dtPayments)
        {
            DataTable dtFinalData = new DataTable();
            //dtFinalData.Columns.Add("REC_LEDGER_ID", typeof(System.Int32));
            //dtFinalData.Columns.Add("REC_GROUP_ID", typeof(System.Int32));
            //dtFinalData.Columns.Add("REC_LEDGER_CODE", typeof(System.String));
            //dtFinalData.Columns.Add("REC_GROUP_CODE", typeof(System.String));
            //dtFinalData.Columns.Add("REC_LEDGER_GROUP", typeof(System.String));
            //dtFinalData.Columns.Add("REC_LEDGER_NAME", typeof(System.String));
            //dtFinalData.Columns.Add("REC_LEDGER_AMOUNT", typeof(System.Double));

            //dtFinalData.Columns.Add("PAY_LEDGER_ID", typeof(System.Int32));
            //dtFinalData.Columns.Add("PAY_GROUP_ID", typeof(System.Int32));
            //dtFinalData.Columns.Add("PAY_LEDGER_CODE", typeof(System.String));
            //dtFinalData.Columns.Add("PAY_LEDGER_GROUP", typeof(System.String));
            //dtFinalData.Columns.Add("PAY_LEDGER_NAME", typeof(System.String));
            //dtFinalData.Columns.Add("PAY_LEDGER_AMOUNT", typeof(System.Double));

            dtReceipts = dtReceipts.DefaultView.ToTable(false, new string[] { "GROUP_CODE", "LEDGER_GROUP", "LEDGER_CODE", "LEDGER_NAME", "RECEIPTAMT" });
            dtPayments = dtPayments.DefaultView.ToTable(false, new string[] { "GROUP_CODE", "LEDGER_GROUP", "LEDGER_CODE", "LEDGER_NAME", "PAYMENTAMT" });

            dtReceipts.Columns["GROUP_CODE"].ColumnName = "REC_GROUP_CODE";
            dtReceipts.Columns["LEDGER_GROUP"].ColumnName = "REC_LEDGER_GROUP";
            dtReceipts.Columns["LEDGER_CODE"].ColumnName = "REC_LEDGER_CODE";
            dtReceipts.Columns["LEDGER_NAME"].ColumnName = "REC_LEDGER_NAME";
            dtReceipts.Columns["RECEIPTAMT"].ColumnName = "REC_LEDGER_RECEIPT_AMOUNT";

            dtPayments.Columns["GROUP_CODE"].ColumnName = "PAY_GROUP_CODE";
            dtPayments.Columns["LEDGER_GROUP"].ColumnName = "PAY_LEDGER_GROUP";
            dtPayments.Columns["LEDGER_CODE"].ColumnName = "PAY_LEDGER_CODE";
            dtPayments.Columns["LEDGER_NAME"].ColumnName = "PAY_LEDGER_NAME";
            dtPayments.Columns["PAYMENTAMT"].ColumnName = "PAY_LEDGER_RECEIPT_AMOUNT";

            //CombileDataTable(dtReceipts, dtPayments);

            //dtFinalData.Merge(dtReceipts);
            //dtFinalData.Merge(dtPayments);

        }

        public DataTable CombileDataTable(DataTable dtReceipts, DataTable dtPayments)
        {
            DataTable dtNew = new DataTable();
            foreach (DataColumn col in dtReceipts.Columns)
            {
                dtNew.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataColumn col in dtPayments.Columns)
            {
                if (!dtNew.Columns.Contains(col.ColumnName))
                {
                    dtNew.Columns.Add(col.ColumnName, col.DataType);
                }
            }

            DataTable dtFirst = (dtReceipts.Rows.Count > dtPayments.Rows.Count ? dtReceipts : dtPayments);
            DataTable dtSecond = (dtReceipts.Rows.Count > dtPayments.Rows.Count ? dtPayments : dtReceipts);

            for (int i = 0; i < dtFirst.Rows.Count; i++)
            {
                DataRow dr = dtNew.NewRow();
                foreach (DataColumn col in dtFirst.Columns)
                {
                    dr[col.ColumnName] = dtFirst.Rows[i][col.ColumnName];
                }
                if (i < dtSecond.Rows.Count)
                {
                    foreach (DataColumn col in dtSecond.Columns)
                    {
                        dr[col.ColumnName] = dtSecond.Rows[i][col.ColumnName];
                    }
                }
                dtNew.Rows.Add(dr);
                dtNew.AcceptChanges();
            }

            GridControl gcCalXLExport = new GridControl();
            GridView gvCalXLExport = new GridView();
            gcCalXLExport.MainView = gvCalXLExport;
            gcCalXLExport.Name = "gcCalXLExport";
            gcCalXLExport.ViewCollection.Add(gvCalXLExport);
            gcCalXLExport.Parent = new Form();
            gvCalXLExport.GridControl = gcCalXLExport;
            gvCalXLExport.Name = "gvCalXLExport";
            gcCalXLExport.DataSource = dtNew;
            gcCalXLExport.RefreshDataSource();

            gvCalXLExport.Columns["REC_GROUP_CODE"].Caption = "Code";
            gvCalXLExport.Columns["REC_LEDGER_CODE"].Caption = "Code";
            gvCalXLExport.Columns["REC_LEDGER_GROUP"].Caption = "GROUP";
            gvCalXLExport.Columns["REC_LEDGER_NAME"].Caption = "Receipts";
            gvCalXLExport.Columns["REC_LEDGER_RECEIPT_AMOUNT"].Caption = "Amount";
            gvCalXLExport.Columns["REC_LEDGER_RECEIPT_AMOUNT"].SummaryItem.Assign(new GridSummaryItem(DevExpress.Data.SummaryItemType.Sum, "REC_LEDGER_RECEIPT_AMOUNT", "{0:n}"));
            gvCalXLExport.Columns["REC_LEDGER_RECEIPT_AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvCalXLExport.Columns["REC_LEDGER_RECEIPT_AMOUNT"].DisplayFormat.FormatString = "n";

            gvCalXLExport.Columns["PAY_GROUP_CODE"].Caption = "Code";
            gvCalXLExport.Columns["PAY_LEDGER_CODE"].Caption = "Code";
            gvCalXLExport.Columns["PAY_LEDGER_GROUP"].Caption = "Group";

            gvCalXLExport.Columns["PAY_LEDGER_NAME"].Caption = "Payments";
            gvCalXLExport.Columns["PAY_LEDGER_RECEIPT_AMOUNT"].Caption = "Amount";
            gvCalXLExport.Columns["PAY_LEDGER_RECEIPT_AMOUNT"].SummaryItem.Assign(new GridSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PAY_LEDGER_RECEIPT_AMOUNT", "{0:n}"));
            gvCalXLExport.Columns["PAY_LEDGER_RECEIPT_AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvCalXLExport.Columns["PAY_LEDGER_RECEIPT_AMOUNT"].DisplayFormat.FormatString = "n";

            gvCalXLExport.BestFitColumns();
            gcCalXLExport.Height = 500;
            gcCalXLExport.Width = 500;
            //gvCalXLExport.ExportToXls(@"c:\test.xls", new XlsExportOptions(TextExportMode.Value)); 


            // Link the required control with the PrintableComponentContainers of a report.
            //printableComponentContainer1.PrintableComponent = gcCalXLExport;
            this.ShowPreviewDialog();
            /*
            GridViewExportLink gvLink = gvCalXLExport.CreateExportLink(new DevExpress.XtraExport.ExportXlsProvider(@"c:\test.xls")) as GridViewExportLink;
            
            gvLink.ExportAll = true;
            gvLink.ExpandAll = true;
            gvLink.ExportCellsAsDisplayText = true;
            gvLink.ExportTo(true);*/


            //GridControl1.ShowPrintPreview();
            //grd.ExportToXlsx(path);
            // Open the created XLSX file with the default application.
            //Process.Start(path);

            return dtNew;
        }

        private void xrPaymentAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PaymentClAmt + PaymentsAmt;
            e.Handled = true;
        }

        #endregion

        private void xrcellTotal2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ReceiptsAmt;
            e.Handled = true;
        }

        private void xrcellTotal4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PaymentsAmt;
            e.Handled = true;
        }

    }
}
