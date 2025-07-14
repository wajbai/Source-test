using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Business;
using Bosco.Model.Setting;
using AcMEDSync.Model;
using Bosco.Model.Dsync;
using System.Net;
using System.ServiceModel;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmPostTicket : frmFinanceBaseAdd
    {
        #region variables
        int pTickID = 0;
        int pRepTickID = 0;
        string pSubject = string.Empty;
        string pDescription = string.Empty;
        int pPriority = 0;
        int pPostedBy = 0;
        string pUserName = string.Empty;
        ResultArgs resultArgs = new ResultArgs();

        #endregion

        #region Constructors
        public frmPostTicket()
        {
            InitializeComponent();
        }

        public frmPostTicket(int TickID, int ticRepID, string ticSubject, string ticDescription, int ticprty, int tickpostBy, string ticUname)
            : this()
        {
            pTickID = TickID;
            pRepTickID = ticRepID;
            pSubject = ticSubject;
            pDescription = ticDescription;
            pPriority = ticprty;
            pPostedBy = tickpostBy;
            pUserName = ticUname;
        }
        #endregion

        #region Methods
        private void PostPortalTickets()
        {
            try
            {
                if (validateInput())
                {
                    bool Isposted = false;
                    this.Cursor = Cursors.WaitCursor;
                    DataSyncService.DataSynchronizerClient dataClienttic = new DataSyncService.DataSynchronizerClient();
                    if (IsServiceAlive(dataClienttic.Endpoint.Address.ToString()))
                    {
                        DataTable dtData = ConstuctData();
                        Isposted = dataClienttic.PostTicket(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, dtData);
                        if (Isposted)
                        {
                            //this.ShowSuccessMessage("Ticket saved Successfully");
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.TICKET_SAVE_SUCCESS));
                            this.Close();
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.PortalMessage.PortalDataSynMessage.PORTAL_SERVICE_NOT_AVIALABLE));
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                MessageRender.ShowMessage(ex.Detail.ToString());
            }
            finally { this.Cursor = Cursors.Default; }
        }

        public bool validateInput()
        {
            bool isvalid = true;
            if (string.IsNullOrEmpty(txtSubject.Text.Trim()))
            {
                //this.ShowMessageBox("Subject is Empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.SUBJUECT_EMPTY));
                txtSubject.Focus();
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(memDescription.Text.Trim()))
            {
                //this.ShowMessageBox("Description is Empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DESCRIPTION_EMPTY));
                memDescription.Select();
                isvalid = false;
            }
            return isvalid;
        }


        public bool IsServiceAlive(string URL)
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(URL))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public DataTable ConstuctEmptySource()
        {
            DataTable dtSource = new DataTable("TICKETS");
            dtSource.Columns.Add("TICKET_ID", typeof(Int32));
            dtSource.Columns.Add("REPLIED_TICKET_ID", typeof(Int32));
            dtSource.Columns.Add("PRIORITY", typeof(Int32));
            dtSource.Columns.Add("HEAD_OFFICE_CODE", typeof(string));
            dtSource.Columns.Add("BRANCH_OFFICE_CODE", typeof(string));
            dtSource.Columns.Add("SUBJECT", typeof(string));
            dtSource.Columns.Add("DESCRIPTION", typeof(string));
            //   dtSource.Columns.Add("POSTED_BY", typeof(Int32));
            //   dtSource.Columns.Add("USER_NAME", typeof(string));
            return dtSource;
        }

        public DataTable ConstuctData()
        {
            DataTable dtPostticket = new DataTable();
            try
            {
                dtPostticket = ConstuctEmptySource();
                if (dtPostticket != null)
                {
                    dtPostticket.Rows.Add(pTickID, pRepTickID, this.UtilityMember.NumberSet.ToInteger(glkPrority.EditValue.ToString()),
                        this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode,
                        txtSubject.Text.Trim(), memDescription.Text);
                    //, pPostedBy,
                    //    !string.IsNullOrEmpty(pUserName) ? pUserName : "Branch User");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return dtPostticket;
        }

        public void LoadPriorityDetails()
        {
            TicketPriority ticketpriority = new TicketPriority();
            DataView ticketpri = this.UtilityMember.EnumSet.GetEnumDataSource(ticketpriority, Sorting.Descending);
            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkPrority, ticketpri.ToTable(), "Name", "Id");
            glkPrority.EditValue = (pTickID > 0 && pPriority > 0) ? pPriority : glkPrority.Properties.GetKeyValue(1);
        }

        #endregion

        #region Events
        private void frmPostTicket_Load(object sender, EventArgs e)
        {
            if (pTickID > 0)
            {
                lblTicketId.Text = pTickID.ToString();
                txtSubject.Text = pSubject.ToString();
                memDescription.Text = pDescription.ToString();
                //txtSubject.Properties.ReadOnly = true;
            }
            lblTicketId.Visibility = simpleLabelItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            LoadPriorityDetails();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PostPortalTickets();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void txtSubject_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtSubject);
        }

        private void memDescription_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(memDescription);
        }
    }
}