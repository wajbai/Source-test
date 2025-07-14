using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraCharts;
using DevExpress.Utils;
using DevExpress.XtraPrinting; 

namespace Bosco.Report.ReportObject
{
    public partial class UcChart : ReportBase
    {
        SettingProperty settingProperty = new SettingProperty();

        /// <summary>
        /// Chart Width
        /// </summary>
        public float ChartWidth
        {
            set {
                xrChartReport.WidthF = value;
            }
        }

        /// <summary>
        /// Chart Height
        /// </summary>
        public float ChartHeight
        {
            set
            {
                xrChartReport.HeightF = value;
            }
        }

        /// <summary>
        /// Chart DataSource
        /// </summary>
        DataTable chartdatasource = null;
        public DataTable ChartDataSouce
        {
            set
            {
                chartdatasource = value;
            }
        }

        /// <summary>
        /// Chart series properties 
        /// SeriesTitle, ValueDataMembers and ArgumentDataMember
        /// </summary>
        Dictionary<string, ChartSeries> dicChartSeriesProperties = new Dictionary<string,ChartSeries>();
        public Dictionary<string, ChartSeries> ChartSeriesProperties
        {
            get
            {
                return dicChartSeriesProperties;
            }
        }

        public bool ClearChartTitle
        {
            set
            {
                this.xrChartReport.Titles.Clear(); ;
            }
        }
        /// <summary>
        /// Assign Chart Title Collection
        /// </summary>
        public string AddChartTitle
        {
            set
            {
                ChartTitle title = new ChartTitle();
                title.Text = value;
                this.xrChartReport.Titles.Add(title);
            }
        }

        

        public UcChart()
        {
            InitializeComponent();
            this.xrChartReport.Visible = (ReportProperties.ChartViewType > 0);
        }


        /// <summary>
        /// It will validate and generate chart for assigned datasource
        /// </summary>
        public ResultArgs GenerateReportChart()
        {
            ViewType chartype = ViewType.Bar;
            ResultArgs resultarg = new ResultArgs();
            this.xrChartReport.Visible = (ReportProperties.ChartViewType > 0);

            if (chartdatasource != null && ReportProperties.ChartViewType > 0)
            {
                this.xrChartReport.DataSource = chartdatasource;

                chartype = this.GetChartType();

                //Assing Series and its properties
                this.xrChartReport.Series.Clear();

                foreach (KeyValuePair<string, ChartSeries> dateKey in dicChartSeriesProperties)
                {
                    if (!string.IsNullOrEmpty(dateKey.Key))
                    {
                        ChartSeries chartseriesproperties = dateKey.Value as ChartSeries;
                        int seriesnumber = this.xrChartReport.Series.Add(new DevExpress.XtraCharts.Series(chartseriesproperties.SeriesTitle, chartype));
                        this.xrChartReport.Series[seriesnumber].ArgumentDataMember = chartseriesproperties.ArgumentDataMember;
                        this.xrChartReport.Series[seriesnumber].ValueDataMembers.AddRange(chartseriesproperties.ValueDataMembers);
                        this.xrChartReport.Series[seriesnumber].ValueScaleType = ScaleType.Numerical;


                        if (chartseriesproperties.SeriesDatasource != null)
                        {
                            this.xrChartReport.Series[seriesnumber].DataSource = chartseriesproperties.SeriesDatasource;
                        }

                        if (chartype == ViewType.Pie || chartype == ViewType.Pie3D)
                        {
                            PiePointOptions piepoint = this.xrChartReport.Series[seriesnumber].Label.PointOptions as PiePointOptions;
                            if (piepoint != null)
                            {
                                piepoint.PercentOptions.ValueAsPercent = false;
                                //piepoint.PointView = PointView.Argument;
                                piepoint.PointView = PointView.Values;
                                
                                this.xrChartReport.Series[seriesnumber].ShowInLegend = false;
                                this.xrChartReport.Series[seriesnumber].ShowInLegend = true;
                                this.xrChartReport.Legend.UseCheckBoxes = true;
                                
                                //if (seriesnumber == 0 )
                                //{
                                //    this.xrChartReport.Series[seriesnumber].ShowInLegend = true;
                                //}

                                if (ReportProperties.ChartInPercentage > 0)
                                {
                                    piepoint.ValueNumericOptions.Format = NumericFormat.General;
                                    piepoint.Pattern = "{A} : {V}%";
                                }
                                else
                                {
                                    piepoint.ValueNumericOptions.Format = NumericFormat.Currency;
                                    piepoint.Pattern = "{A} : {V}";
                                }
                            }
                                                        
                            this.xrChartReport.Series[seriesnumber].LabelsVisibility = DefaultBoolean.True;
                            this.xrChartReport.Series[seriesnumber].ToolTipEnabled = DefaultBoolean.True;
                        }
                        else
                        {
                            this.xrChartReport.Legend.UseCheckBoxes = false;
                            if (chartype == ViewType.Bar)
                            {
                                SideBySideBarSeriesLabel label = (SideBySideBarSeriesLabel)this.xrChartReport.Series[seriesnumber].Label;
                                label.PointOptions.ValueNumericOptions.Format = NumericFormat.Currency;
                                //label.Position = seriesnumber == 0 ? BarSeriesLabelPosition.Top : BarSeriesLabelPosition.TopInside;
                                //label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAllAroundPoint;
                                label.Position = BarSeriesLabelPosition.Top ;
                                label.TextOrientation = TextOrientation.BottomToTop;
                            }
                            else if (chartype == ViewType.FullStackedBar)
                            {
                                FullStackedBarSeriesLabel label = (FullStackedBarSeriesLabel)this.xrChartReport.Series[seriesnumber].Label;
                                label.PointOptions.PointView = PointView.ArgumentAndValues;
                                label.PointOptions.ValueNumericOptions.Format = NumericFormat.Currency;
                                //label.Position = seriesnumber == 0 ? BarSeriesLabelPosition.Top : BarSeriesLabelPosition.TopInside;
                                label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAroundPoint;
                            }
                            else
                            {
                                PointSeriesLabel label = (PointSeriesLabel)this.xrChartReport.Series[seriesnumber].Label;
                                label.PointOptions.ValueNumericOptions.Format = NumericFormat.Currency;
                                label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAroundPoint;
                            }

                            //this.xrChartReport.Series[seriesnumber].LabelsVisibility = DefaultBoolean.False;
                            this.xrChartReport.Series[seriesnumber].ToolTipEnabled = DefaultBoolean.False;
                            this.xrChartReport.Series[seriesnumber].ShowInLegend = true;

                            if (this.xrChartReport.Series.Count > 0)
                            {
                                XYDiagram diagram = (XYDiagram)this.xrChartReport.Diagram;
                                if (ReportProperties.ChartInPercentage > 0)
                                {
                                    diagram.AxisY.Label.NumericOptions.Format = NumericFormat.General;
                                }
                                else
                                {
                                    diagram.AxisY.Label.NumericOptions.Format = NumericFormat.Currency;
                                }
                            }
                        }
                    }
                }
            }
            return resultarg;
        }
        
        
        private bool validateFields()
        {
            bool rtn = false;

            return rtn;
        }


        /// <summary>
        /// On 25/01/2021, to get Chart Type
        /// </summary>
        /// <returns></returns>
        private ViewType GetChartType()
        {
            ViewType cviewtype = ViewType.Bar;

            string chartype = UtilityMember.EnumSet.GetEnumItemNameByValue(typeof(ChartViewType), this.ReportProperties.ChartViewType);

            if (!string.IsNullOrEmpty(chartype))
            {
                cviewtype = (ViewType)Enum.Parse(typeof(ViewType), chartype);
            }

            return cviewtype;
        }

        private void xrChartReport_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
         AxisBase axis = e.Item.Axis;

         if (ReportProperties.ChartInPercentage > 0)
         {

             if (axis is AxisY || axis is AxisY3D || axis is RadarAxisY)
             {
                 e.Item.Text = e.Item.Text + " %";
             }
         }
        }

        private void xrChartReport_PreviewMouseDown(object sender, PreviewMouseEventArgs e)
        {

            
        }
    }

    public class ChartSeries
    {
        private string seriestitle = string.Empty;
        private string[] valuedatamembers;
        private string argumentdatamember;
        private DataTable seiresdatasource = null;

        public ChartSeries(string SeriesTitle, string[] ValueDataMembers, string ArgumentDataMember, DataTable SeiresDatasource)
        {
            seriestitle = SeriesTitle;
            valuedatamembers = ValueDataMembers;
            argumentdatamember = ArgumentDataMember;
            seiresdatasource = SeiresDatasource;
        }

        public string SeriesTitle
        {
            get
            {
                return seriestitle;
            }
        }

        public string[] ValueDataMembers
        {
            get
            {
                return valuedatamembers;
            }
        }

        public string ArgumentDataMember
        {
            get
            {
                return argumentdatamember;
            }
        }

        /// <summary>
        /// This can be null
        /// </summary>
        public DataTable SeriesDatasource
        {
            get
            {
                return seiresdatasource;
            }
        }

    }
}
