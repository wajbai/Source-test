using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.UIControls
{
    public partial class UcMapping : UserControl
    {
        #region Declaration
        ResultArgs resultargs = null;
        #endregion 

        public UcMapping()
        {
            InitializeComponent();
        }

        private void UcMapping_Load(object sender, EventArgs e)
        {
            FetchMappedProject();
        }

#region Methods
        private void FetchMappedProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultargs= mappingSystem.FetchMappedProjects();
                    gvProjectName.DataSource = resultargs.DataSource.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }
#endregion 

        
    }
}
