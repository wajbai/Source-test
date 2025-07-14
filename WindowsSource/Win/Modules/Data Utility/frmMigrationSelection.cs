using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.TallyMigration;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmMigrationSelection : frmFinanceBaseAdd
    {
        #region Properties
        public MigrationType MigrationMode
        {
            set;
            get;
        }
        #endregion

        public frmMigrationSelection()
        {
            InitializeComponent();
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            bool Closeform = false;
            if (rgMigrationType.SelectedIndex == 0) //From AcmePlus
            {
                MigrationMode = MigrationType.AcMePlus;
                Closeform = true;
            }
            else if (rgMigrationType.SelectedIndex == 1) //From Tally, Check tally is opened or not
            {
                using (frmTallyMigration TallyMigration = new frmTallyMigration())
                {
                    MigrationMode = MigrationType.Tally;
                    using (TallyConnector TallyERPConnector = new TallyConnector())
                    {
                        ResultArgs resultarug = TallyERPConnector.IsMoreThanOneTallyRunningInstance;
                        if (!resultarug.Success)
                        {
                            resultarug = TallyERPConnector.IsTallyConnected;
                            if (!resultarug.Success)
                            {
                                this.ShowMessageBox(resultarug.Message);
                            }
                            else
                            {
                                Closeform = true;
                            }
                        }
                        else
                        {
                            this.ShowMessageBox("Could not connect to Tally, more than one instance of Tally is running");
                        }
                    }
                }
            }
            else if (rgMigrationType.SelectedIndex == 2) //From BSOCOPAC, 
            {
                MigrationMode = MigrationType.BOSCOPAC;
                Closeform = true;
            }
            if (Closeform)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MigrationMode = MigrationType.None;
            this.Close();
        }

        private void frmMigrationSelection_Load(object sender, EventArgs e)
        {
            //-----------------------------------------Temporary---------------------------------------------------------------
            //Disabling Tally Migration

            //Commented by Salamon to enable the option by default if the finance is allowed.
            //rgMigrationType.Properties.Items[1].Enabled = Bosco.Utility.ConfigSetting.SettingProperty.EnableTallyMigration;
        }
    }
}