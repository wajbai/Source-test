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
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules.Master
{
    public partial class StatisticsTypeAdd : frmFinanceBaseAdd
    {
        #region EventsDeclaration
        public event EventHandler UpdataHeld;
        #endregion

        #region VariableDeclaration
        private int StatisticsTypeId = 0;
        #endregion


        public StatisticsTypeAdd()
        {
            InitializeComponent();
        }

        public StatisticsTypeAdd(int statisticstypeId)
            : this()
        {
            StatisticsTypeId = statisticstypeId;
        }

        private void StatisticsTypeAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignStatisticsTypeDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtStatisticstype_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtStatisticstype);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateStatisticstypeDetails())
                {
                    ResultArgs resultArgs = null;
                    using (StatisticsTypeSystem StatisticsTypeSystem = new StatisticsTypeSystem())
                    {
                        StatisticsTypeSystem.StatisticsTypeId = StatisticsTypeId == 0 ? (int)AddNewRow.NewRow : StatisticsTypeId;
                        StatisticsTypeSystem.StatisticsType = txtStatisticstype.Text.Trim();
                        resultArgs = StatisticsTypeSystem.SaveStatisticsTypeDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdataHeld != null)
                            {
                                UpdataHeld(this, e);
                            }
                            ClearControl();
                            txtStatisticstype.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }


        #region Methods
        /// <summary>
        /// To Validate the Purpose Details
        /// </summary>
        /// <returns></returns>
        public bool ValidateStatisticstypeDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtStatisticstype.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.StatisticsType.STATISTICSTYPE_EMPTY));
                this.SetBorderColor(txtStatisticstype);
                isValue = false;
                txtStatisticstype.Focus();
            }
            return isValue;
        }

        /// <summary>
        /// To Clear the Purpose after Entering 
        /// </summary>
        private void ClearControl()
        {
            if (StatisticsTypeId == 0)
            {
                txtStatisticstype.Text = string.Empty;
            }
            txtStatisticstype.Focus();
        }

        /// <summary>
        /// To Set the Title for add and edit form
        /// </summary>
        private void SetTitle()
        {
            this.Text = StatisticsTypeId == 0 ? this.GetMessage(MessageCatalog.Master.StatisticsType.STATISTICSTYPE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.StatisticsType.STATISTICSTYPE_EDIT_CAPTION);
            txtStatisticstype.Focus();
        }

        /// <summary>
        /// To assign the Details to the Controls
        /// </summary>
        public void AssignStatisticsTypeDetails()
        {
            try
            {
                if (StatisticsTypeId != 0)
                {
                    using (StatisticsTypeSystem statisticsTypeSystem = new StatisticsTypeSystem(StatisticsTypeId))
                    {
                        txtStatisticstype.Text = statisticsTypeSystem.StatisticsType;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion
    }
}