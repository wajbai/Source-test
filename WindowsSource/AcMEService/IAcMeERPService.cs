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
    public interface IAcMeERPService
    {
        [OperationContract]
        DataSet GetMasters(string ServiceKey);

        [OperationContract]
        int PostVouchers(string DataTableString, string ServiceKey);

        [OperationContract]
        int DeleteVouchers(string ClientCode, string ClientRefcode, string ServiceKey);

        [OperationContract]
        DataSet GetPayrollMasters(string ServiceKey);

        [OperationContract]
        int PostStaffDetails(string DataTableString, string ServiceKey);

        [OperationContract]
        bool PostPayroll(DataTable dtPayroll, string ServiceKey);

    }
}
