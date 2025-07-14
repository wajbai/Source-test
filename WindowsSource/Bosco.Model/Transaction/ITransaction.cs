using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.Transaction
{
    interface IVoucherMaster
    {
        string VoucherId { get; set; } //Declare all voucher Master properties
        ResultArgs SaveVoucherMaster();
        ResultArgs DeleteVoucherMaster();
        ResultArgs UpdateVoucherMaster();
        ResultArgs FetchVoucherMaster();
    }

    interface IVoucherMasterDetails
    {
        string VoucherDetailId { get; set; } ////Declare all voucher Master Details properties
        ResultArgs SaveVoucherMasterDetails();
        ResultArgs DeleteVoucherMasterDetails();
        ResultArgs UpdateVoucherMasterDetails();
        ResultArgs FetchVoucherMasterDetails();
    }

}
