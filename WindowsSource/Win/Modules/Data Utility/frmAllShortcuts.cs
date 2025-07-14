using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACPP;
using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmAllShortcuts : frmFinanceBase
    {
        ResultArgs resultargs = null;

        public frmAllShortcuts()
        {
            InitializeComponent();
        }


        //private DataTable construct()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("ID", typeof(string));
        //    dt.Columns.Add("MODULE_ID", typeof(string));
        //    dt.Columns.Add("SHORTCUT", typeof(string));
        //    dt.Columns.Add("DESCRIPTION", typeof(string));
        //    dt.Columns.Add("", typeof(int));

        //    DataRow dr = dt.NewRow();
        //    dr["MODULE_ID"] = "COMMON";
        //    dr["SHORTCUT"] = "Alt+S";
        //    dr["DESCRIPTION"] = "Save";
        //    dt.Rows.Add(dr);

        //    DataRow dr2 = dt.NewRow();
        //    dr2["MODULE_ID"] = "COMMON";
        //    dr2["SHORTCUT"] = "Alt+C";
        //    dr2["DESCRIPTION"] = "Close";
        //    dt.Rows.Add(dr2);

        //    DataRow dr3 = dt.NewRow();
        //    dr3["MODULE_ID"] = "COMMON";
        //    dr3["SHORTCUT"] = "Alt+N";
        //    dr3["DESCRIPTION"] = "New";
        //    dt.Rows.Add(dr3);

        //    DataRow dr4 = dt.NewRow();
        //    dr4["MODULE_ID"] = "COMMON";
        //    dr4["SHORTCUT"] = "Ctrl+D";
        //    dr4["DESCRIPTION"] = "Delete a Transaction";
        //    dt.Rows.Add(dr4);

        //    DataRow dr1 = dt.NewRow();
        //    dr1["MODULE_ID"] = "FINANCE";
        //    dr1["SHORTCUT"] = "F3";
        //    dr1["DESCRIPTION"] = "Focus to the date control";
        //    dt.Rows.Add(dr1);

        //    DataRow dr5 = dt.NewRow();
        //    dr5["MODULE_ID"] = "Asset";
        //    dr5["SHORTCUT"] = "F3";
        //    dr5["DESCRIPTION"] = "Focus to the date control";
        //    dt.Rows.Add(dr5);

        //    DataRow dr6 = dt.NewRow();
        //    dr6["MODULE_ID"] = "Stock";
        //    dr6["SHORTCUT"] = "F3";
        //    dr6["DESCRIPTION"] = "Focus to the date control";
        //    dt.Rows.Add(dr6);
        //    return dt;
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            loadShortcuts();
        }


        private void loadShortcuts()
        {
            using (UserSystem usersystem = new UserSystem())
            {
                resultargs = usersystem.FetchAllShortcuts();
                if (resultargs.Success && resultargs != null)
                {
                    gcShortcuts.DataSource = resultargs.DataSource.Table;
                }
            }
        }
    }
}
