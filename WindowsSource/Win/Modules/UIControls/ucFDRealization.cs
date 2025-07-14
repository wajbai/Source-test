using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ACPP.Modules.Transaction;
using Bosco.Model.Business;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;

namespace ACPP.Modules.UIControls
{
    public partial class ucFDRealization : UserControl
    {
        #region Varaible Decalration
        ResultArgs resultArgs = new ResultArgs();
        #endregion
        public ucFDRealization()
        {
            InitializeComponent();
        }

        #region Properties
        public PopupContainerControl PopupContainerControl
        {
            get { return popupContainerControl1; }
            set { popupContainerControl1 = value; }
        }
        #endregion

        #region Methods
        //public void LoadFDRealisation(string fromDate,string toDate,int projectid)
        //{
        //    gcFDRealizatiion.DataSource = null;
        //    using (LedgerSystem ledgersystem = new LedgerSystem())
        //    {
        //       ledgersystem.ProjectId = projectid;
        //        resultArgs = ledgersystem.FetchFDAccountsByMaturityDate(fromDate, toDate);
        //        if (resultArgs.Success)
        //        {
        //            gcFDRealizatiion.DataSource = resultArgs.DataSource.Table;
        //            gcFDRealizatiion.RefreshDataSource();
        //        }
        //    }
        //}
        #endregion
    }
}
