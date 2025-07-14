using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmLabelPrint : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtSelectedDatasource = new DataTable("ProspectIndividual");
        public frmLabelPrint()
        {
            InitializeComponent();
        }

        public frmLabelPrint(DataTable dtSelectedSource)
            : this()
        {
            dtSelectedDatasource = dtSelectedSource;
        }

        private void frmLabelPrint_Load(object sender, EventArgs e)
        {
            LoadDonorLabel();
        }

        private void LoadDonorLabel()
        {
            try
            {
                dtSelectedDatasource.TableName = "ProspectIndividual";
                if (dtSelectedDatasource != null && dtSelectedDatasource.Rows.Count > 0)
                {
                    Bosco.Report.Base.ReportProperty.Current.ReportId = "RPT-127";
                    Bosco.Report.Base.ReportProperty.Current.DonorLabelPrint = dtSelectedDatasource;
                    this.rptViewer.ReportId = "RPT-127";
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }
    }
}