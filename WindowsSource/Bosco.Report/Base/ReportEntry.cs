/*  Class Name      : ReportBase.cs
 *  Purpose         : Interface between Report viewer and Report Interface class to get report source
 *  Author          : CS
 *  Created on      : 21-Jul-2009
 */

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Bosco.Report;
using Bosco.Report.View;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using System.IO;
using Bosco.DAO.Data;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Schema;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Data;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.Base
{
    public class ReportEntry : IReport
    {
        private Form mdiParent = null;
        private CommonMember utilityMember = null;
        private SettingProperty settingproperty = null;
        private ReportSetting.ReportParameterDataTable reportParameters = null;
        ResultArgs resultArgs = null;
        public ReportEntry()
        {

        }

        public ReportEntry(Form mdiParent)
        {
            this.mdiParent = mdiParent;
        }

        #region IReport Members

        /// <summary>
        /// On 03/05/2018, so for Report datefrom , dateto takes previous and already assinged value 
        /// even if user change transaction period
        /// 
        /// so this method is used to assign DateFrom, DateTo current trnasaction period, it will be called only finance year changed
        /// </summary>
        public void ResetReportPropertyTransYearChange()
        {
            if (!string.IsNullOrEmpty(ReportProperty.Current.BudgetId))
            {
                ReportProperty.Current.BudgetId = string.Empty;
            }

            if (!string.IsNullOrEmpty(ReportProperty.Current.Budget))
            {
                ReportProperty.Current.Budget = string.Empty;
            }

            if (!string.IsNullOrEmpty(ReportProperty.Current.DateFrom))
            {
                ReportProperty.Current.DateFrom = string.Empty; //this.UtilityMember.DateSet.ToDate(Settingprop.YearFrom);
            }

            if (!string.IsNullOrEmpty(ReportProperty.Current.DateTo))
            {
                ReportProperty.Current.DateTo = string.Empty; //this.UtilityMember.DateSet.ToDate(Settingprop.YearTo);
            }

            if (!string.IsNullOrEmpty(ReportProperty.Current.DateAsOn))
            {
                ReportProperty.Current.DateAsOn = string.Empty; //this.UtilityMember.DateSet.ToDate(Settingprop.YearFrom);
            }
        }

        public void ShowReport()
        {
            frmReportGallery fReportGallery = new frmReportGallery();

            if (this.mdiParent != null)
            {
                fReportGallery.MdiParent = this.mdiParent;
            }

            fReportGallery.Show();
        }

        public DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            ReportBase report = null;
            try
            {
                string reportAssemblyType = ReportProperty.Current.ReportAssembly;
                report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportBase;
                report.ShowReport();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
            return report;
        }

        public void VoucherPrint(string VoucherId, string VoucherType, string ProjectName, Int32 ProjectId, Int32 InOutId = 0, string Moduletype = "")
        {
            ReportBase report = null;
            try
            {
                //using (BalanceSystem balanceSystem = new BalanceSystem())
                //{
                //    ReportProperty.dtLedgerEntity = balanceSystem.FetchLedgalEntity();
                //}

                //For Legal entity
                using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.FetchAll))
                {
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                    ReportProperty.dtLedgerEntity = resultArgs.DataSource.Table;
                }

                //On 11/05/2022, assign selected voucher's project legal entity 
                using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.Project.Fetch))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, ProjectId);
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtProjectDetails = resultArgs.DataSource.Table;
                        if (dtProjectDetails.Rows.Count > 0)
                        {
                            ReportProperty.Current.LedgalEntityId = dtProjectDetails.Rows[0][this.ReportParameters.CUSTOMERIDColumn.ColumnName].ToString();
                        }
                    }
                }

                ReportProperty.Current.SelectedProjectCount = 1;
                ReportProperty.Current.ReportId = VoucherType;
                string reportAssemblyType = ReportProperty.Current.ReportAssembly;

                report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportBase;
                report.ReportId = VoucherType;
                ReportPrintTool rpttool = new ReportPrintTool(report);
                ReportProperty.Current.Project = ProjectId.ToString();
                ReportProperty.Current.PrintCashBankVoucherId = VoucherId;
                ReportProperty.Current.ProjectTitle = ProjectName;
                ReportProperty.Current.PrintPurchaseInoutVoucherId = InOutId;
                ReportProperty.Current.ModuleType = Moduletype;

                //on 17/01/2023, to set voucher date-------------------------
                //if (!string.IsNullOrEmpty(VoucherDate))
                //{
                //    ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = utilityMember.DateSet.ToDate(VoucherDate,false);
                //}
                //--------------------------------------------------------------

                //To set report form properties -----------------------------------------------------------------------------------------------------------------------
                XtraForm ParentForm = null;
                if (mdiParent != null)
                {
                    if (mdiParent.ActiveMdiChild != null)
                    {
                        ParentForm = mdiParent.ActiveMdiChild as XtraForm;
                    }
                }

                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Open, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Save, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Scale, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Watermark, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.SendFile, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Customize, DevExpress.XtraPrinting.CommandVisibility.None);

                //rpttool.PreviewForm.KeyPreview = true;
                //rpttool.PreviewForm
                rpttool.PreviewForm.KeyDown += new KeyEventHandler(PreviewForm_KeyDown);
                rpttool.PreviewForm.StartPosition = FormStartPosition.CenterParent;
                rpttool.PreviewForm.WindowState = FormWindowState.Maximized;
                rpttool.PreviewForm.Text = ReportProperty.Current.ReportTitle;
                rpttool.PreviewForm.MinimizeBox = false;
                rpttool.PreviewForm.Activated += new EventHandler(PreviewForm_Activated);
                //rpttool.PreviewForm.TopMost = true;
                //-----------------------------------------------------------------------------------------------------------------------------------------------------
                rpttool.PreviewForm.Owner = ParentForm;
                //rpttool.Report.CreateDocument();
                //rpttool.Report.ShowPreviewDialog();
                //rpttool.Report.ShowPreviewDialog();
                //rpttool.ShowPreviewDialog();
                report.ShowPrintDialogue();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }


        private void PreviewForm_Activated(object sender, EventArgs e)
        {
            ((DevExpress.XtraPrinting.Preview.PrintPreviewFormEx)sender).BringToFront();
            ((DevExpress.XtraPrinting.Preview.PrintPreviewFormEx)sender).Focus();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <param name="VoucherType"></param>
        /// <param name="ProjectName"></param>
        public void ShowPrintView(DataTable dtDonorLabel, String ReportId)
        {
            ReportBase report = null;
            try
            {
                ReportProperty.Current.ReportId = ReportId;
                string reportAssemblyType = ReportProperty.Current.ReportAssembly;
                report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportBase;
                report.ReportId = ReportId;
                //ReportProperty.Current.ProjectTitle = "Test";
                report.DataSource = dtDonorLabel;
                report.DataMember = dtDonorLabel.TableName;
                report.ShowPrintDialogue();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }


        /// <summary>
        /// This method is used to print cheque based on given voucher id
        /// </summary>
        /// <param name="VoucherId"></param>
        public void ShowChequePrint(Int32 VoucherId, Int32 BankId)
        {
            try
            {
                XtraReport chequeprint = new ReportObject.ChequePrint(VoucherId, BankId);
                ReportPrintTool rpttool = new ReportPrintTool(chequeprint);
                rpttool.PrintingSystem.ShowMarginsWarning = false;
                chequeprint.ShowPrintMarginsWarning = false;
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Open, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Save, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Scale, DevExpress.XtraPrinting.CommandVisibility.None);
                rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Watermark, DevExpress.XtraPrinting.CommandVisibility.None);

                rpttool.PreviewForm.KeyPreview = true;
                //rpttool.PreviewForm.KeyDown += new KeyEventHandler(PreviewForm_KeyDown);
                rpttool.PreviewForm.StartPosition = FormStartPosition.CenterParent;
                rpttool.PreviewForm.WindowState = FormWindowState.Normal;
                rpttool.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        void PreviewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                e.Handled = true;
                Form f = sender as Form;
                f.Close();
            }
        }

        public void ShowBudgetView1(Int32 BudgetId, string BudgetProjectIds, string BudgetName, GridView gv)
        {
            try
            {
                if (BudgetId > 0)
                {
                    XtraForm parentform = gv.GridControl.FindForm() as XtraForm;
                    frmStandardReport frmstandardreport = new frmStandardReport(BudgetId, BudgetProjectIds, BudgetName, gv);
                    frmstandardreport.ShowDialog(parentform);
                }
                else
                {
                    MessageRender.ShowMessage("Budget is not available", true);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        public void ShowDepreciationCalculation(int DeprId, DateTime dtfrom, DateTime dtTo, int projectid, XtraForm frmf)
        {
            try
            {
                XtraForm parentform = frmf;
                frmStandardReport frmsr = new frmStandardReport(DeprId, dtfrom, dtTo, projectid, frmf);
                frmsr.ShowDialog(parentform);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
        }

        public void ShowBudgetMonthDistribution(Int32 BudgetId, string projectIds, GridView gv, DateTime dtyear, DateTime dtyearto, DateTime monthfrom, DateTime monthto, string BudgetName)
        {
            try
            {
                if (BudgetId > 0)
                {
                    XtraForm parentform = gv.GridControl.FindForm() as XtraForm;
                    frmStandardReport frmstandardreport = new frmStandardReport(BudgetId, projectIds, gv, dtyear, dtyearto, monthfrom, monthto, BudgetName);
                    frmstandardreport.ShowDialog(parentform);
                }
                else
                {
                    MessageRender.ShowMessage("Budget is not available", true);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        public void ShowBudgetExpenseApprovalByMonth(string BudgetId, string ProjectId, bool MonthbyTwo, GridView gv, string datefrom, string dateto, string BudgetName, string ProjectName)
        {
            try
            {
                if (!string.IsNullOrEmpty(BudgetId))
                {
                    XtraForm parentform = gv.GridControl.FindForm() as XtraForm;
                    frmStandardReport frmstandardreport = new frmStandardReport(BudgetId, ProjectId, MonthbyTwo, gv, datefrom, dateto, BudgetName, ProjectName);
                    frmstandardreport.ShowDialog(parentform);
                }
                else
                {
                    MessageRender.ShowMessage("Budget is not available", true);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        /// This method is used to generate Standard reports for all grid view.
        /// It will load defualt or standard repot design called RPT-STD (will have only report tab in report criteria property)
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="standardtitle"></param>
        public void ShowStandardReport(GridView gv, string standardtitle)
        {
            //Form myForm = gv.FindForm() as ;
            XtraForm parentform = gv.GridControl.FindForm() as XtraForm;
            frmStandardReport frmstandardreport = new frmStandardReport(gv, standardtitle);
            frmstandardreport.ShowDialog(parentform);
        }

        /// <summary>
        /// This method is used to generate Standard reports for all grid view.
        /// It will load defualt or standard repot design called RPT-STD (will have only report tab in report criteria property)
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="standardtitle"></param>
        public void ShowStandardReport(GridView gv, string standardtitle, GridView gvAdditional, string AdditionalTitle)
        {
            //Form myForm = gv.FindForm() as ;
            XtraForm parentform = gv.GridControl.FindForm() as XtraForm;
            frmStandardReport frmstandardreport = new frmStandardReport(gv, standardtitle, gvAdditional, AdditionalTitle);
            frmstandardreport.ShowDialog(parentform);
        }
        
        /*public void ShowBudgetView(Int32 BudgetId, string BudgetName)
        {
            ReportBase report = null;
            try
            {
                if (BudgetId > 0)
                {
                    ReportProperty.Current.ReportId = "RPT-152";
                    ReportProperty.Current.BudgetId = BudgetId.ToString();
                    ReportProperty.Current.ProjectTitle = BudgetName;
                    ReportProperty.Current.BudgetName = BudgetName;
                    string reportAssemblyType = ReportProperty.Current.ReportAssembly;
                    report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportBase;
                    report.ReportId = "RPT-152";
                    ReportPrintTool rpttool = new ReportPrintTool(report);

                    //To set report form properties -----------------------------------------------------------------------------------------------------------------------
                    XtraForm ParentForm = null;
                    if (mdiParent != null)
                    {
                        if (mdiParent.ActiveMdiChild != null)
                        {
                            ParentForm = mdiParent.ActiveMdiChild as XtraForm;
                        }
                    }

                    rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Open, DevExpress.XtraPrinting.CommandVisibility.None);
                    rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Save, DevExpress.XtraPrinting.CommandVisibility.None);
                    rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Scale, DevExpress.XtraPrinting.CommandVisibility.None);
                    rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Watermark, DevExpress.XtraPrinting.CommandVisibility.None);
                    rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.SendFile, DevExpress.XtraPrinting.CommandVisibility.None);
                    rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground, DevExpress.XtraPrinting.CommandVisibility.None);
                    rpttool.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Customize, DevExpress.XtraPrinting.CommandVisibility.None);

                    //rpttool.PreviewForm.KeyPreview = true;
                    //rpttool.PreviewForm
                    rpttool.PreviewForm.KeyDown += new KeyEventHandler(PreviewForm_KeyDown);
                    rpttool.PreviewForm.StartPosition = FormStartPosition.CenterParent;
                    rpttool.PreviewForm.WindowState = FormWindowState.Normal;
                    rpttool.PreviewForm.Text = ReportProperty.Current.ReportTitle;
                    rpttool.ShowPreviewDialog();
                    //-----------------------------------------------------------------------------------------------------------------------------------------------------
                    //report.ShowPrintDialogue();
                }
                else
                {
                    MessageRender.ShowMessage("Budget is not available", true);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }*/

        #endregion

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        protected SettingProperty Settingprop
        {
            get
            {
                if (settingproperty == null) { settingproperty = new SettingProperty(); }
                return settingproperty;
            }
        }

        #region Customize Reports


        public static Stream ByteArrayToStream(byte[] byteArrayIn)
        {
            Stream stream = null;
            try
            {
                stream = new MemoryStream(byteArrayIn);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return stream;
        }

        private byte[] SerializedXtraReport(XtraReport report)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                report.SaveLayout(stream);
                return stream.ToArray();
            }
        }

        protected ReportSetting.ReportParameterDataTable ReportParameters
        {
            get
            {
                if (reportParameters == null) { reportParameters = new ReportSetting.ReportParameterDataTable(); }
                return reportParameters;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new { Field = f, Att = a })
                            .Where(a => ((DescriptionAttribute)a.Att)
                                .Description == description).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }
        
        #endregion

    }
}
