using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using System.IO;

namespace Bosco.Model.Dsync
{
    public class PortalMessagesSystem : SystemBase
    {
        #region Variable Decalaration
        ResultArgs resultArgs = new ResultArgs();
        DataTable dtDatasynMessage = new DataTable();
        DataTable dtAmendmentMessage = new DataTable();
        DataTable dtTickets = new DataTable();
        #endregion

        #region Properties
        //public int ID { get; set; }
        //public string Status { get; set; }
        //public DateTime UploadOn { get; set; }
        //public string Remarks { get; set; }
        //public DateTime TransDateFrom { get; set; }
        //public DateTime TransDateTo { get; set; }
        //public DateTime AmendmentDate { get; set; }
        //public DateTime CompletedOn { get; set; }
        //public DateTime StartedOn { get; set; }
        //public int VoucherID { get; set; }
        //public string AmendmentStatus { get; set; }
        //public DateTime VoucherDate { get; set; }
        //public string LedgerName { get; set; }
        public string Project { get; set; }
        //public int VoucherNo { get; set; }
        //public int Amount { get; set; }
        //public string VoucherType { get; set; }
        public int ProjecID { get; set; }
        #endregion

        public ResultArgs SavePortalMessage(DataSet dsPortalMessage)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SavePortalDataSynMessage(dsPortalMessage);
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = SavePortalBroadCastMessage(dsPortalMessage);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = SavePortalAmendmentMessage(dsPortalMessage);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = SavePortalTickets(dsPortalMessage);
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/ 2019, For Background Process
        /// </summary>
        /// <param name="dtDataSynMessage"></param>
        /// <returns></returns>
        public ResultArgs SavePortalMessageByBackgroundProcess(DataSet dsPortalMessage)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SavePortalDataSynMessage(dsPortalMessage);
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = SavePortalBroadCastMessage(dsPortalMessage);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = SavePortalAmendmentMessage(dsPortalMessage);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = SavePortalTickets(dsPortalMessage);
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SavePortalDataSynMessage(DataSet dtDataSynMessage)
        {
            dtDatasynMessage = dtDataSynMessage.Tables[(int)PortalMessages.DataSynMessage];
            resultArgs = DeletePortalDataSynMessages();
            if (dtDatasynMessage != null && dtDatasynMessage.Rows.Count > 0)
            {
                foreach (DataRow drDataSyn in dtDatasynMessage.Rows)
                {
                    using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.AddDataSynMessage))
                    {
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.IDColumn, dtDatasynMessage.Rows.IndexOf(drDataSyn)+1);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.UPLOADED_ONColumn, drDataSyn[this.AppSchema.PortalMessage.UPLOADED_ONColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.TRANS_DATE_FROMColumn, drDataSyn[this.AppSchema.PortalMessage.TRANS_DATE_FROMColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.TRANS_DATE_TOColumn, drDataSyn[this.AppSchema.PortalMessage.TRANS_DATE_TOColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.STARTED_ONColumn, drDataSyn[this.AppSchema.PortalMessage.STARTED_ONColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.COMPLETED_ONColumn, drDataSyn[this.AppSchema.PortalMessage.COMPLETED_ONColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.REMARKSColumn, drDataSyn[this.AppSchema.PortalMessage.REMARKSColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.STATUSColumn, drDataSyn[this.AppSchema.PortalMessage.STATUSColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.REFRESH_DATEColumn, DateTime.Now);
                        resultArgs = datamanager.UpdateData();

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/ 2019, For Background Process
        /// </summary>
        /// <param name="dtDataSynMessage"></param>
        /// <returns></returns>
        private ResultArgs SavePortalDataSynMessageByBackgroundProcess(DataSet dtDataSynMessage)
        {
            dtDatasynMessage = dtDataSynMessage.Tables[(int)PortalMessages.DataSynMessage];
            resultArgs = DeletePortalDataSynMessages();
            if (dtDatasynMessage != null && dtDatasynMessage.Rows.Count > 0)
            {
                foreach (DataRow drDataSyn in dtDatasynMessage.Rows)
                {
                    using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.AddDataSynMessage))
                    {
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.IDColumn, dtDatasynMessage.Rows.IndexOf(drDataSyn) + 1);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.UPLOADED_ONColumn, drDataSyn[this.AppSchema.PortalMessage.UPLOADED_ONColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.TRANS_DATE_FROMColumn, drDataSyn[this.AppSchema.PortalMessage.TRANS_DATE_FROMColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.TRANS_DATE_TOColumn, drDataSyn[this.AppSchema.PortalMessage.TRANS_DATE_TOColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.STARTED_ONColumn, drDataSyn[this.AppSchema.PortalMessage.STARTED_ONColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.COMPLETED_ONColumn, drDataSyn[this.AppSchema.PortalMessage.COMPLETED_ONColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.REMARKSColumn, drDataSyn[this.AppSchema.PortalMessage.REMARKSColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.STATUSColumn, drDataSyn[this.AppSchema.PortalMessage.STATUSColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.REFRESH_DATEColumn, DateTime.Now);
                        resultArgs = datamanager.UpdateDataInBackgroundProcess(datamanager);

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            return resultArgs;
        }


        private ResultArgs SavePortalAmendmentMessage(DataSet dsPortalMessage)
        {
            dtAmendmentMessage = dsPortalMessage.Tables[(int)PortalMessages.AmendmentMessage];
            resultArgs = DeletePortalAmendmentMessages();

            //DataRow dr = dtAmendmentMessage.NewRow();
            //dr[this.AppSchema.PortalMessage.AMENDMENT_DATEColumn.ColumnName] = "01/02/2019";
            //dr[this.AppSchema.PortalMessage.REMARKSColumn.ColumnName] = "1";
            //dr[this.AppSchema.PortalMessage.VOUCHER_IDColumn.ColumnName] = "2";
            //dr[this.AppSchema.PortalMessage.VOUCHER_DATEColumn.ColumnName] = "01/02/2019";
            //dr[this.AppSchema.PortalMessage.VOUCHER_NOColumn.ColumnName] = "3";
            //dr[this.AppSchema.PortalMessage.VOUCHER_TYPEColumn.ColumnName] = "test";
            //dr[this.AppSchema.PortalMessage.AMOUNTColumn.ColumnName] = "11";
            //dr[this.AppSchema.PortalMessage.LEDGER_NAMEColumn.ColumnName] = "test";
            //dr[this.AppSchema.PortalMessage.PROJECTColumn.ColumnName] = "test";
            //dtAmendmentMessage.Rows.Add(dr);

            if (dtAmendmentMessage != null && dtAmendmentMessage.Rows.Count > 0)
            {
                foreach (DataRow drAmendment in dtAmendmentMessage.Rows)
                {
                    using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.AddAmenmentMessage))
                    {
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.IDColumn, dtAmendmentMessage.Rows.IndexOf(drAmendment)+1);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.REMARKSColumn, drAmendment[this.AppSchema.PortalMessage.REMARKSColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.AMENDMENT_DATEColumn, drAmendment[this.AppSchema.PortalMessage.AMENDMENT_DATEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.VOUCHER_IDColumn, drAmendment[this.AppSchema.PortalMessage.VOUCHER_IDColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.VOUCHER_DATEColumn, drAmendment[this.AppSchema.PortalMessage.VOUCHER_DATEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.VOUCHER_NOColumn, drAmendment[this.AppSchema.PortalMessage.VOUCHER_NOColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.VOUCHER_TYPEColumn, drAmendment[this.AppSchema.PortalMessage.VOUCHER_TYPEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.AMOUNTColumn, drAmendment[this.AppSchema.PortalMessage.AMOUNTColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.LEDGER_NAMEColumn, drAmendment[this.AppSchema.PortalMessage.LEDGER_NAMEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.PROJECTColumn, drAmendment[this.AppSchema.PortalMessage.PROJECTColumn.ColumnName]);
                        resultArgs = datamanager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/ 2019, For Background Process
        /// </summary>
        /// <param name="dtDataSynMessage"></param>
        /// <returns></returns>
        private ResultArgs SavePortalAmendmentMessageByBackgroundProcess(DataSet dsPortalMessage)
        {
            dtAmendmentMessage = dsPortalMessage.Tables[(int)PortalMessages.AmendmentMessage];
            resultArgs = DeletePortalAmendmentMessages();

            //DataRow dr = dtAmendmentMessage.NewRow();
            //dr[this.AppSchema.PortalMessage.AMENDMENT_DATEColumn.ColumnName] = "01/02/2019";
            //dr[this.AppSchema.PortalMessage.REMARKSColumn.ColumnName] = "1";
            //dr[this.AppSchema.PortalMessage.VOUCHER_IDColumn.ColumnName] = "2";
            //dr[this.AppSchema.PortalMessage.VOUCHER_DATEColumn.ColumnName] = "01/02/2019";
            //dr[this.AppSchema.PortalMessage.VOUCHER_NOColumn.ColumnName] = "3";
            //dr[this.AppSchema.PortalMessage.VOUCHER_TYPEColumn.ColumnName] = "test";
            //dr[this.AppSchema.PortalMessage.AMOUNTColumn.ColumnName] = "11";
            //dr[this.AppSchema.PortalMessage.LEDGER_NAMEColumn.ColumnName] = "test";
            //dr[this.AppSchema.PortalMessage.PROJECTColumn.ColumnName] = "test";
            //dtAmendmentMessage.Rows.Add(dr);

            if (dtAmendmentMessage != null && dtAmendmentMessage.Rows.Count > 0)
            {
                foreach (DataRow drAmendment in dtAmendmentMessage.Rows)
                {
                    using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.AddAmenmentMessage))
                    {
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.IDColumn, dtAmendmentMessage.Rows.IndexOf(drAmendment) + 1);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.REMARKSColumn, drAmendment[this.AppSchema.PortalMessage.REMARKSColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.AMENDMENT_DATEColumn, drAmendment[this.AppSchema.PortalMessage.AMENDMENT_DATEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.VOUCHER_IDColumn, drAmendment[this.AppSchema.PortalMessage.VOUCHER_IDColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.VOUCHER_DATEColumn, drAmendment[this.AppSchema.PortalMessage.VOUCHER_DATEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.VOUCHER_NOColumn, drAmendment[this.AppSchema.PortalMessage.VOUCHER_NOColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.VOUCHER_TYPEColumn, drAmendment[this.AppSchema.PortalMessage.VOUCHER_TYPEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.AMOUNTColumn, drAmendment[this.AppSchema.PortalMessage.AMOUNTColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.LEDGER_NAMEColumn, drAmendment[this.AppSchema.PortalMessage.LEDGER_NAMEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.PortalMessage.PROJECTColumn, drAmendment[this.AppSchema.PortalMessage.PROJECTColumn.ColumnName]);
                        resultArgs = datamanager.UpdateDataInBackgroundProcess(datamanager);
                    }
                }
            }
            return resultArgs;
        }


        private ResultArgs SavePortalTickets(DataSet dsPortalMessage)
        {
            dtTickets = dsPortalMessage.Tables[(int)PortalMessages.Tickets];
            resultArgs = DeletePortalTickets();
            if (dtTickets != null && dtTickets.Rows.Count > 0)
            {
                foreach (DataRow drTickets in dtTickets.Rows)
                {
                    using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.AddTroubleTickets))
                    {
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.TICKET_IDColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.TICKET_IDColumn.ColumnName].ToString()));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.SUBJECTColumn, drTickets[this.AppSchema.TroubleTicket.SUBJECTColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.DESCRIPTIONColumn, drTickets[this.AppSchema.TroubleTicket.DESCRIPTIONColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.PRIORITYColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.PRIORITYColumn.ColumnName].ToString()));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.POSTED_DATEColumn, DateSet.ToDate(drTickets[this.AppSchema.TroubleTicket.POSTED_DATEColumn.ColumnName].ToString(), false));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.COMPLETED_DATEColumn, DateSet.ToDate(drTickets[this.AppSchema.TroubleTicket.COMPLETED_DATEColumn.ColumnName].ToString(), false));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.POSTED_BYColumn, drTickets[this.AppSchema.TroubleTicket.POSTED_BYColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.REPLIED_TICKET_IDColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.REPLIED_TICKET_IDColumn.ColumnName].ToString()));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.USER_NAMEColumn, drTickets[this.AppSchema.TroubleTicket.USER_NAMEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.PHYSICAL_FILE_NAMEColumn, drTickets[this.AppSchema.TroubleTicket.PHYSICAL_FILE_NAMEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.STATUSColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.STATUSColumn.ColumnName].ToString()));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.ATTACH_FILE_NAMEColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.ATTACH_FILE_NAMEColumn.ColumnName].ToString()));
                        resultArgs = datamanager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/ 2019, For Background Process
        /// </summary>
        /// <param name="dtDataSynMessage"></param>
        /// <returns></returns>
        private ResultArgs SavePortalTicketsByBackgroundProcess(DataSet dsPortalMessage)
        {
            dtTickets = dsPortalMessage.Tables[(int)PortalMessages.Tickets];
            resultArgs = DeletePortalTickets();
            if (dtTickets != null && dtTickets.Rows.Count > 0)
            {
                foreach (DataRow drTickets in dtTickets.Rows)
                {
                    using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.AddTroubleTickets))
                    {
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.TICKET_IDColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.TICKET_IDColumn.ColumnName].ToString()));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.SUBJECTColumn, drTickets[this.AppSchema.TroubleTicket.SUBJECTColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.DESCRIPTIONColumn, drTickets[this.AppSchema.TroubleTicket.DESCRIPTIONColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.PRIORITYColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.PRIORITYColumn.ColumnName].ToString()));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.POSTED_DATEColumn, DateSet.ToDate(drTickets[this.AppSchema.TroubleTicket.POSTED_DATEColumn.ColumnName].ToString(), false));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.COMPLETED_DATEColumn, DateSet.ToDate(drTickets[this.AppSchema.TroubleTicket.COMPLETED_DATEColumn.ColumnName].ToString(), false));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.POSTED_BYColumn, drTickets[this.AppSchema.TroubleTicket.POSTED_BYColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.REPLIED_TICKET_IDColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.REPLIED_TICKET_IDColumn.ColumnName].ToString()));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.USER_NAMEColumn, drTickets[this.AppSchema.TroubleTicket.USER_NAMEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.PHYSICAL_FILE_NAMEColumn, drTickets[this.AppSchema.TroubleTicket.PHYSICAL_FILE_NAMEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.STATUSColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.STATUSColumn.ColumnName].ToString()));
                        datamanager.Parameters.Add(this.AppSchema.TroubleTicket.ATTACH_FILE_NAMEColumn, NumberSet.ToInteger(drTickets[this.AppSchema.TroubleTicket.ATTACH_FILE_NAMEColumn.ColumnName].ToString()));
                        resultArgs = datamanager.UpdateDataInBackgroundProcess(datamanager);
                    }
                }
            }
            return resultArgs;
        }


        private ResultArgs SavePortalBroadCastMessage(DataSet dtDataSynMessage)
        {
            dtDatasynMessage = dtDataSynMessage.Tables[(int)PortalMessages.BroadCastMessage];
            resultArgs = DeletePortalBroadcastMessages();

            //DataRow dr = dtDatasynMessage.NewRow();
            //dr[this.AppSchema.BroadCastMessage.IDColumn.ColumnName] = 1; ;
            //dr[this.AppSchema.BroadCastMessage.DATEColumn.ColumnName] = "01/02/2019";
            //dr[this.AppSchema.BroadCastMessage.SUBJECTColumn.ColumnName] = "1";
            //dr[this.AppSchema.BroadCastMessage.CONTENTColumn.ColumnName] = "2";
            //dr[this.AppSchema.BroadCastMessage.TYPEColumn.ColumnName] = "3";
            //dtDatasynMessage.Rows.Add(dr);

            if (dtDatasynMessage != null && dtDatasynMessage.Rows.Count > 0)
            {
                foreach (DataRow drDataSyn in dtDatasynMessage.Rows)
                {
                    using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.AddBroadCastMessage))
                    {
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.IDColumn, drDataSyn[this.AppSchema.BroadCastMessage.IDColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.DATEColumn, drDataSyn[this.AppSchema.BroadCastMessage.DATEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.SUBJECTColumn, drDataSyn[this.AppSchema.BroadCastMessage.SUBJECTColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.CONTENTColumn, drDataSyn[this.AppSchema.BroadCastMessage.CONTENTColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.TYPEColumn, NumberSet.ToInteger(drDataSyn[this.AppSchema.BroadCastMessage.TYPEColumn.ColumnName].ToString()));
                        resultArgs = datamanager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/ 2019, For Background Process
        /// </summary>
        /// <param name="dtDataSynMessage"></param>
        /// <returns></returns>
        private ResultArgs SavePortalBroadCastMessageByBackgroundProcess(DataSet dtDataSynMessage)
        {
            dtDatasynMessage = dtDataSynMessage.Tables[(int)PortalMessages.BroadCastMessage];
            resultArgs = DeletePortalBroadcastMessages();

            //DataRow dr = dtDatasynMessage.NewRow();
            //dr[this.AppSchema.BroadCastMessage.IDColumn.ColumnName] = 1; ;
            //dr[this.AppSchema.BroadCastMessage.DATEColumn.ColumnName] = "01/02/2019";
            //dr[this.AppSchema.BroadCastMessage.SUBJECTColumn.ColumnName] = "1";
            //dr[this.AppSchema.BroadCastMessage.CONTENTColumn.ColumnName] = "2";
            //dr[this.AppSchema.BroadCastMessage.TYPEColumn.ColumnName] = "3";
            //dtDatasynMessage.Rows.Add(dr);

            if (dtDatasynMessage != null && dtDatasynMessage.Rows.Count > 0)
            {
                foreach (DataRow drDataSyn in dtDatasynMessage.Rows)
                {
                    using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.AddBroadCastMessage))
                    {
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.IDColumn, drDataSyn[this.AppSchema.BroadCastMessage.IDColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.DATEColumn, drDataSyn[this.AppSchema.BroadCastMessage.DATEColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.SUBJECTColumn, drDataSyn[this.AppSchema.BroadCastMessage.SUBJECTColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.CONTENTColumn, drDataSyn[this.AppSchema.BroadCastMessage.CONTENTColumn.ColumnName]);
                        datamanager.Parameters.Add(this.AppSchema.BroadCastMessage.TYPEColumn, NumberSet.ToInteger(drDataSyn[this.AppSchema.BroadCastMessage.TYPEColumn.ColumnName].ToString()));
                        resultArgs = datamanager.UpdateDataInBackgroundProcess(datamanager);
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeletePortalDataSynMessages()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.DeleteDataSynMessage))
            {
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeletePortalAmendmentMessages()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.DeleteAmendmentMessage))
            {
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeletePortalTickets()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.DeleteTroubleTickets))
            {
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchPortalDataSynMesseage()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PortalMessage.FetchPortalDataSynMessage))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPortalAmendmentMesseage()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PortalMessage.FetchPortalAmendmentMessage))
            {
                //  dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, this.ProjecID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPortalTickets()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PortalMessage.FetchTroubleTickets))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchUserManualFeature()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PortalMessage.FetchUserManualFeature))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table!=null)
                {
                    DataTable dtUserManualFeature = resultArgs.DataSource.Table;
                    dtUserManualFeature.DefaultView.Sort = this.AppSchema.UsermanualFeature.FEATURE_GROUPColumn.ColumnName + " DESC, " +
                                                            this.AppSchema.UsermanualFeature.FEATURE_CODEColumn.ColumnName;
                    resultArgs.DataSource.Data  = dtUserManualFeature.DefaultView.ToTable();
                }
            }
            return resultArgs;
        }

        private ResultArgs DeletePortalBroadcastMessages()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.PortalMessage.DeleteBroadCastMessage))
            {
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchPortalBroadcastMesseage()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PortalMessage.FetchBroadCastMessage))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchUserMaualsPaidFeaturesFromPortal()
        {
            ResultArgs result = new ResultArgs();
            using (AcMEERPFTP acmeftp = new AcMEERPFTP())
            {
                result = acmeftp.DownloadManualsByWebClient();
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dtUserMaualsPaidFeatues = result.DataSource.Table;
                    if (dtUserMaualsPaidFeatues.Rows.Count > 0)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.PortalMessage.DeleteUserMaualsPaidFeatures))
                        {
                            result = dataManager.UpdateData();

                            if (result.Success)
                            {
                                foreach (DataRow dr in dtUserMaualsPaidFeatues.Rows)
                                {
                                    string code = dr[this.AppSchema.UsermanualFeature.FEATURE_CODEColumn.ColumnName] != null ? dr[this.AppSchema.UsermanualFeature.FEATURE_CODEColumn.ColumnName].ToString() : string.Empty;
                                    string groupcode = dr[this.AppSchema.UsermanualFeature.FEATURE_GROUP_CODEColumn.ColumnName] != null ? dr[this.AppSchema.UsermanualFeature.FEATURE_GROUP_CODEColumn.ColumnName].ToString() : string.Empty;
                                    string group = dr[this.AppSchema.UsermanualFeature.FEATURE_GROUPColumn.ColumnName] != null ? dr[this.AppSchema.UsermanualFeature.FEATURE_GROUPColumn.ColumnName].ToString() : string.Empty;
                                    string feature = dr[this.AppSchema.UsermanualFeature.FEATUREColumn.ColumnName] != null ? dr[this.AppSchema.UsermanualFeature.FEATUREColumn.ColumnName].ToString() : string.Empty;
                                    string lnkfilename = dr[this.AppSchema.UsermanualFeature.LINK_FILENAMEColumn.ColumnName] != null ? dr[this.AppSchema.UsermanualFeature.LINK_FILENAMEColumn.ColumnName].ToString() : string.Empty;

                                    if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(group) && !string.IsNullOrEmpty(feature))
                                    {
                                        using (DataManager dataManager1 = new DataManager(SQLCommand.PortalMessage.UpdateUserMaualsPaidFeatures))
                                        {
                                            dataManager1.Parameters.Add(this.AppSchema.UsermanualFeature.FEATURE_CODEColumn, code);
                                            dataManager1.Parameters.Add(this.AppSchema.UsermanualFeature.FEATURE_GROUP_CODEColumn, groupcode);
                                            dataManager1.Parameters.Add(this.AppSchema.UsermanualFeature.FEATURE_GROUPColumn, group);
                                            dataManager1.Parameters.Add(this.AppSchema.UsermanualFeature.FEATUREColumn, feature);
                                            dataManager1.Parameters.Add(this.AppSchema.UsermanualFeature.LINK_FILENAMEColumn, lnkfilename);
                                            result = dataManager1.UpdateData();

                                            if (result.Success && !string.IsNullOrEmpty(lnkfilename))
                                            {
                                                acmeftp.DownloadManualFileByWebClient(lnkfilename);
                                            }
                                        }
                                    }

                                    if (!result.Success) break;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
