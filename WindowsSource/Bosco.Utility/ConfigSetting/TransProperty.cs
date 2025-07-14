using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using System.Data;

namespace Bosco.Utility.ConfigSetting
{
    public class TransProperty : CommonMember, IDisposable
    {
        private static DataView dvTransInfo = null;
        private static DataView dvCashTransInfo = null;
        private static DataSet dsCostCentreInfo = null;
        private static DataSet dsTDSBooking = null;
        private static DataTable dvFixedDepositInfo = null;
        private static DataTable dvFixedDepositInteretstInfo = null;
        private static DataTable dtMisMatchedLedgers = null;
        private static DataTable dtModifiedLedgers = null;
        private static DataTable dtMisMatchedProjects = null;
        private static DataTable dtModifiedProjects = null;
        private static DataTable dtTDSPayment = null;
        private static DataTable dtTDSPaymentBank = null;
        private static DataTable dtTDSPartyPayment = null;
        private static DataTable dtTDSPartyPaymentBank = null;
        private static DataTable dtTDSBooking = null;
        private static DataSet dsDenominationInfo = null;
        private static DataTable dtReferenceNo = null;
        private static DataTable dtLedgerSubLedgerVouchers = null;

        /// <summary>
        /// Set Transaction Info as Dataview
        /// </summary>
        public DataView TransInfo
        {
            set
            {
                TransProperty.dvTransInfo = value;
            }
            get
            {
                return dvTransInfo;
            }
        }

        /// <summary>
        /// Set Transaction Info as Dataview
        /// </summary>
        public DataView CashTransInfo
        {
            set
            {
                TransProperty.dvCashTransInfo = value;
            }
            get
            {
                return dvCashTransInfo;
            }
        }

        public DataTable FixedDepositInfo
        {
            set
            {
                TransProperty.dvFixedDepositInfo = value;
            }
            get
            {
                return dvFixedDepositInfo;
            }
        }

        public DataTable FixedDepositInterestInfo
        {
            set
            {
                TransProperty.dvFixedDepositInteretstInfo = value;
            }
            get
            {
                return dvFixedDepositInteretstInfo;
            }
        }

        public DataTable MisMatchedLedgers
        {
            set
            {
                TransProperty.dtMisMatchedLedgers = value;
            }
            get
            {
                return dtMisMatchedLedgers;
            }
        }

        public DataTable ModifiedLedgers
        {
            set
            {
                TransProperty.dtModifiedLedgers = value;
            }
            get
            {
                return dtModifiedLedgers;
            }
        }
        public DataTable MisMatchedProjects
        {
            set
            {
                TransProperty.dtMisMatchedProjects = value;
            }
            get
            {
                return dtMisMatchedProjects;
            }
        }

        public DataTable ModifiedProjects
        {
            set
            {
                TransProperty.dtModifiedProjects = value;
            }
            get
            {
                return dtModifiedProjects;
            }
        }
        public DataTable TDSPayment
        {
            set
            {
                TransProperty.dtTDSPayment = value;
            }
            get
            {
                return dtTDSPayment;
            }
        }

        public DataTable TDSPaymentBank
        {
            set
            {
                TransProperty.dtTDSPaymentBank = value;
            }
            get
            {
                return dtTDSPaymentBank;
            }
        }
        public DataTable TDSBookingDetail
        {
            set
            {
                TransProperty.dtTDSBooking = value;
            }
            get
            {
                return dtTDSBooking;
            }
        }

        public DataTable TDSPartyPayment
        {
            set
            {
                TransProperty.dtTDSPartyPayment = value;
            }
            get
            {
                return dtTDSPartyPayment;
            }
        }

        public DataTable TDSPartyPaymentBank
        {
            set
            {
                TransProperty.dtTDSPartyPaymentBank = value;
            }
            get
            {
                return dtTDSPartyPaymentBank;
            }
        }

        public DataTable ReferenceNumberInfo
        {
            set
            {
                TransProperty.dtReferenceNo = value;
            }
            get
            {
                return dtReferenceNo;
            }
        }

        public DataTable LedgerSubLedgerVouchers
        {
            set
            {
                TransProperty.dtLedgerSubLedgerVouchers = value;
            }
            get
            {
                return dtLedgerSubLedgerVouchers;
            }
        }

        public DataSet TDSBooking
        {
            set
            {
                TransProperty.dsTDSBooking = value;
            }
            get
            {
                return dsTDSBooking;
            }
        }

        public DataView GetCostCentreByLedgerID(string LedgerID)
        {
            return dsCostCentreInfo.Tables[dsCostCentreInfo.Tables.IndexOf(LedgerID)].DefaultView;
        }

        public DataSet CostCenterInfo
        {
            set
            {
                dsCostCentreInfo = value;
            }
        }

        public DataSet DenominationInfo
        {
            set
            {
                dsDenominationInfo = value;
            }
        }

        public DataView GetDenominationByLedgerID(string LedgerID)
        {
            return dsDenominationInfo.Tables[dsDenominationInfo.Tables.IndexOf(LedgerID)].DefaultView;
        }

        public bool HasCostCentre(string LedgerID)
        {
            return dsCostCentreInfo != null ? dsCostCentreInfo.Tables.Contains(LedgerID) : false;
        }
        public bool HasReferenceNo(string LedgerId)
        {
            return dtReferenceNo != null ? dtReferenceNo.Rows.Contains(LedgerId) : false;
        }
        public bool HasDenomination(string LedgerID)
        {
            return dsDenominationInfo != null ? dsDenominationInfo.Tables.Contains(LedgerID) : false;
        }
        public virtual void Dispose()
        {
            GC.Collect();
        }
    }
}
