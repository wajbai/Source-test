using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace AcMEService
{
    [ServiceContract]
    public interface IAcmeERPSSPService
    {
        [OperationContract]
        string PostMaster(string ManagementCode, string JsonScriptMaster);

        [OperationContract]
        string GetVoucher(string DateFrom, string DateTo, string ManagementCode, string JsonScriptMaster, string JsonFilterProjectMaster);

        [OperationContract]
        string UpdateGetVouchers(string ManagementCode, string JsonSavedScriptTransaction);

    }
}
