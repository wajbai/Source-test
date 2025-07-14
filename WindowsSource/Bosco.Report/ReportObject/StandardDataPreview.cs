//This Report is used to preview the records given by GridView,so it will be used in master or wherever Gridcontrol lists records
//1. Dynamically add gridview column as XRTable and cell if grid view column is vissible and not group column.
//2. Default Paper kind is A4, if no of columns more than 5 paper orientation is Landscape.
//3.  

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Columns;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using System.Collections.Generic;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data.Filtering;
using Bosco.Utility;

namespace Bosco.Report.ReportObject
{
    public partial class StandardDataPreview : Bosco.Report.Base.ReportHeaderBase
    {
        DataView dvRptDataSource = new DataView();
        XRTable tblHeadger = new XRTable();
        XRTableRow rowHeader = new XRTableRow();

        XRTable tblDetail = new XRTable();
        XRTableRow rowDetail = new XRTableRow();

        XRTable tblFooter= new XRTable();
        XRTableRow rowFotter= new XRTableRow();

        //XRTable tblAdditionalHeadger = new XRTable();
        //XRTableRow rowAdditionalHeader = new XRTableRow();

        bool AdditionalGrid = false;

        public StandardDataPreview()
        {
            InitializeComponent();
            GridView gvStandardReport = this.ReportProperties.GridViewStandardReport;
            StandardDataPreviewExtracted(gvStandardReport);

            if (this.ReportProperties.AdditionalGridViewStandardReport != null)
            {
                StandardDataPreview additionRpt = new StandardDataPreview(true);
                additionRpt.HideReportHeader = false;
                additionRpt.HidePageFooter = false;
                
                XRSubreport subrpt = new XRSubreport();
                subrpt.ReportSource = additionRpt;
                
                var lines = this.ReportProperties.AdditionText.Split(new string[]{Environment.NewLine},StringSplitOptions.None);
                if (lines.Length > 0 &&  !string.IsNullOrEmpty(lines.GetValue(1).ToString()))
                {
                    XRLabel lblAdditionalGrdText = new XRLabel();
                    lblAdditionalGrdText.Text = lines.GetValue(1).ToString();
                    lblAdditionalGrdText.TopF = rowFotter.TopF + rowFotter.HeightF + 25;
                    lblAdditionalGrdText.Font = new System.Drawing.Font("Tahoma", 10, FontStyle.Bold);
                    lblAdditionalGrdText.WidthF = this.PageWidth - 100;
                    Bands[BandKind.ReportFooter].Controls.Add(lblAdditionalGrdText);
                                        
                    subrpt.TopF = lblAdditionalGrdText.TopF + lblAdditionalGrdText.HeightF;
                    subrpt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(subrpt_BeforePrint);
                    Bands[BandKind.ReportFooter].Controls.Add(subrpt);
                }
            }
        }

        void subrpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.PageHeader.Visible = false;
        }
        
        public StandardDataPreview(bool additionalGrid=false)
        {
            InitializeComponent();
            AdditionalGrid = additionalGrid;
            GridView gvStandardReport = this.ReportProperties.GridViewStandardReport;
            if (additionalGrid == true && this.ReportProperties.GridViewStandardReport!=null)
            {
                gvStandardReport = this.ReportProperties.AdditionalGridViewStandardReport;
            }
            StandardDataPreviewExtracted(gvStandardReport);
            ApplyStyle();
        }


        private void StandardDataPreviewExtracted(GridView gvStandardReport)
        {
            try
            {
                if (gvStandardReport.DataSource != null)
                {
                    dvRptDataSource = gvStandardReport.DataSource as DataView;

                    //09/03/2017 To set filter criteria if gridview contains filter condition
                    CriteriaOperator op = gvStandardReport.ActiveFilterCriteria; //filterControl1.FilterCriteria
                    string filterString = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(op);
                    dvRptDataSource.RowFilter = string.Empty;
                    if (!string.IsNullOrEmpty(filterString))
                    {
                        dvRptDataSource.RowFilter = filterString;
                    }

                    dvRptDataSource.Sort = string.Empty;
                    foreach (GridColumn gc in gvStandardReport.SortedColumns)
                    {
                        string sortfieldwithorder = gc.FieldName + string.Empty + (gc.SortOrder == ColumnSortOrder.Descending ? " DESC" : string.Empty);
                        dvRptDataSource.Sort = (string.IsNullOrEmpty(dvRptDataSource.Sort) ? sortfieldwithorder : dvRptDataSource.Sort + "," + sortfieldwithorder);
                    }

                    InitReportDesign(gvStandardReport);

                    this.DataSource = dvRptDataSource;
                    this.DataMember = dvRptDataSource.Table.TableName;
                }
                else //If gridview datasource is empy, prompt proper message
                {
                    //prompt some messsage
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Not able to generate Standarted Report");
                AcMELog.WriteLog("Not able to generate Standarted Report - " + err.Message);
            }
        }

        /// <summary>
        /// This method is used to design dynamically
        /// 1. add grid view column as xtratable cell
        /// 2. set title and paper properties
        /// 3. set report field and value cell style and properties
        /// </summary>
        public void InitReportDesign(GridView gv)
        {
            try
            {
                int columns = 0;
                bool isfooter = gv.OptionsView.ShowFooter; //this.ReportProperties.GridViewStandardReport.OptionsView.ShowFooter;
                //For Group columns
                //for (int i = this.ReportProperties.GridView.GroupCount - 1; i >= 0; i--)
                //{
                //    InsertGroupBand(this.ReportProperties.GridView.GroupedColumns[i], i);
                //}

                //Add each gridview colums in page header band as field caption and in detail band as field value
                //gridview colums should be visible and should not be group column
                foreach (GridColumn gc in gv.Columns) //this.ReportProperties.GridViewStandardReport.Columns
                {
                    //string repostryitem = typeof(CheckEdit).Name;
                    //if (gc.Visible && gc.GroupIndex < 0 &&
                    //      (gc.ColumnEdit == null || gc.ColumnEdit.GetType() == typeof(RepositoryItemTextEdit) || gc.ColumnEdit.GetType() == typeof(RepositoryItemDateEdit)))

                    if (gc.Visible && gc.GroupIndex < 0 &&
                          (gc.ColumnEdit == null || gc.ColumnEdit.GetType() == typeof(RepositoryItemTextEdit) || gc.ColumnEdit.GetType() == typeof(RepositoryItemDateEdit)))
                    {
                        //gc.ColumnType == GridViewColumnType
                        //For field caption
                        XRTableCell cellHeader = new XRTableCell();
                        cellHeader.Text = gc.Caption;
                        //On 16/09/2021, to show double line (word web)
                        //cellHeader.CanGrow = false;
                        //cellHeader.CanShrink = false;
                        cellHeader.WordWrap = true;

                        cellHeader.WidthF = ConvertFromPixelsToReportUnit(gc.Width);
                        cellHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        ReportCellAlignment(gc, cellHeader);
                        rowHeader.Cells.Add(cellHeader);
                        
                        //For field value
                        XRTableCell cellDetail = new XRTableCell();
                        cellDetail.DataBindings.Add("Text", dvRptDataSource, gc.FieldName, "{0:" + gc.DisplayFormat.FormatString + "}");
                        
                        //On 16/09/2021, to show double line (word web)
                        //cellDetail.CanGrow = false;
                        //cellDetail.CanShrink = false;
                        cellDetail.WordWrap = true;
                        cellDetail.WidthF = ConvertFromPixelsToReportUnit(gc.Width);
                        

                        ReportCellAlignment(gc, cellDetail);
                        cellDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(cellDetail_BeforePrint);
                        rowDetail.Cells.Add(cellDetail);

                        //Footer
                        if (isfooter)
                        {
                            XRTableCell cellFooter = new XRTableCell();
                            if (gc.SummaryItem.SummaryType !=SummaryItemType.None)
                            {
                                cellFooter.Text = this.UtilityMember.NumberSet.ToNumber( this.UtilityMember.NumberSet.ToDouble(gc.SummaryItem.SummaryValue.ToString())).ToString();
                            }
                            //On 16/09/2021, to show double line (word web)
                            cellFooter.CanGrow = false;
                            cellFooter.CanShrink = false;
                            cellFooter.WordWrap = true;

                            cellFooter.WidthF = ConvertFromPixelsToReportUnit(gc.Width);
                            cellFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            ReportCellAlignment(gc, cellFooter);
                            rowFotter.Cells.Add(cellFooter);
                        }
                        columns++;
                    }
                }

                tblHeadger.Rows.Add(rowHeader);
                tblDetail.Rows.Add(rowDetail);
                tblFooter.Rows.Add(rowFotter);
                Bands[BandKind.PageHeader].Controls.Add(tblHeadger);
                Bands[BandKind.Detail].Controls.Add(tblDetail);

                if (isfooter)
                {
                    Bands[BandKind.ReportFooter].Controls.Add(tblFooter);
                    tblFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(tblHeadger_BeforePrint);
                }

                //Attach before events to rearrange table width
                tblHeadger.BeforePrint += new System.Drawing.Printing.PrintEventHandler(tblHeadger_BeforePrint);
                tblDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(tblDetail_BeforePrint);
                
            }
            catch(Exception err)
            {
                MessageRender.ShowMessage("Could not generate report, " + err.Message, true);
            }
        }

        private void cellDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 26/04/2017, format of the date, amount is not getting generated in standard report even though we assigned 
            //so based on the format, it will be converted and assigned.
            XRTableCell cell = sender as XRTableCell;
            string colname = cell.DataBindings[0].DataMember;
            string displayformat = cell.DataBindings[0].FormatString;
            DataRowView drv = ((DataRowView)GetCurrentRow());
            if (drv != null)
            {
                DataColumn dc = drv.DataView.Table.Columns[colname];
                if (dc != null)
                {
                    if (dc.DataType == typeof(DateTime))
                    {
                        cell.Text = this.UtilityMember.DateSet.ToDate(cell.Text);
                    }
                    else if (displayformat == "{0:n}") //for Number/Currency format 
                    {
                        cell.Text = string.Format(displayformat, this.UtilityMember.NumberSet.ToDouble(cell.Text));
                    }
                }
            }
        }
        
        /// <summary>
        /// This method is used to greate group band it gridview is group bannded grid
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="i"></param>
        private void InsertGroupBand(GridColumn gridColumn, int i)
        {
            GroupHeaderBand gb = new GroupHeaderBand();
            gb.Height = 25;
            gb.RepeatEveryPage = true;
            XRLabel l = new XRLabel();
            l.DataBindings.Add("Text", this.DataSource, gridColumn.Caption);
            l.Size = new Size(300, 25);
            l.Location = new Point(0 + i * 20, 0);
            l.BackColor = Color.Beige;
            gb.Controls.Add(l);
            GroupField gf;
            if (gridColumn.SortOrder == ColumnSortOrder.Ascending)
                gf = new GroupField(gridColumn.FieldName, XRColumnSortOrder.Ascending);
            else
                gf = new GroupField(gridColumn.FieldName, XRColumnSortOrder.Descending);
            gb.GroupFields.Add(gf);
            this.Bands.Add(gb);
        }

        private void ReportCellAlignment(GridColumn gcrpt , XRTableCell xrtblcellrpt)
        {
            //HorzAlignment alignment -------------------------------------------------------------------------------------------
            if (gcrpt.AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Default ||
                gcrpt.AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Near)
                xrtblcellrpt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            else if (gcrpt.AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Far)
                xrtblcellrpt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            else if (gcrpt.AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Center)
                xrtblcellrpt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            //--------------------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// Assign table width based on the cell width and its value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tblHeadger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right-20;
        }

        /// <summary>
        /// Assign table width based on the cell width and its value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tblDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right-20;
        }

        /// <summary>
        /// This method converts column width to report table cell width as pixesl
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private float ConvertFromPixelsToReportUnit(int value)
        {
            

            GraphicsUnit unit = (this.ReportUnit == ReportUnit.HundredthsOfAnInch ? GraphicsUnit.Inch : GraphicsUnit.Millimeter);
            float multiplier = (unit == GraphicsUnit.Inch ? 100 : 10);

            return GraphicsUnitConverter.Convert(value, GraphicsUnit.Pixel, unit) * multiplier;
        }

        private void ApplyStyle()
        {
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);

            //If no of columns in grid view more than 5, we set page orientation is landscape
            if (rowHeader.Cells.Count > 5)
            {
                this.Landscape = true;
            }
            

            this.HideDateRange = false;
            this.HideReportTitle = false;
            this.HideBudgetName = true;
            this.HideCostCenter = true;
            if (!string.IsNullOrEmpty(this.ReportProperties.AdditionText))
            {
                var lines = this.ReportProperties.AdditionText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                
                if (lines.Length == 4)
                {
                    this.CosCenterName = lines.GetValue(0).ToString(); //Budget imcome ledgers title
                    this.ReportPeriod = lines.GetValue(2).ToString(); //Date Range
                    this.ReportTitle = lines.GetValue(3).ToString(); //Budget Name 
                    this.HideDateRange = this.HideReportTitle = this.HideBudgetName = true; 
                }
                else if (lines.Length == 3)
                {
                    this.CosCenterName = lines.GetValue(0).ToString();
                    this.ReportPeriod = lines.GetValue(2).ToString();
                    this.HideDateRange = this.HideReportTitle = this.HideBudgetName = true; 
                }
                else if (lines.Length == 2)
                {
                    this.CosCenterName = lines.GetValue(0).ToString();
                    this.ReportPeriod = string.Empty;
                    this.HideDateRange = this.HideReportTitle = this.HideBudgetName = true;
                }
                else if (lines.Length == 1)
                {
                    this.CosCenterName = lines.GetValue(0).ToString();
                    this.ReportPeriod = string.Empty;
                    this.HideDateRange = this.HideReportTitle = this.HideBudgetName = true;
                }
            }
            this.HideReportDate = false;
            //this.HideCostCenter = true;
            this.HideReportSubTitle = true;

            tblHeadger.StyleName = "styleColumnHeader";
            tblFooter.StyleName = "styleColumnHeader";
            tblDetail.StyleName = "styleRow";
            this.AlignHeaderTable(tblHeadger);
            this.AlignContentTable(tblDetail);
            this.AlignHeaderTable(tblFooter);
            this.SetTitleWidth(tblHeadger.WidthF);
        }
       
        #region ShowReport
        public override void ShowReport()
        {
            //Set report title and paper properties
            SetReportTitle();
            
            ApplyStyle();
            base.ShowReport();
        }
        #endregion

        
    }
}
