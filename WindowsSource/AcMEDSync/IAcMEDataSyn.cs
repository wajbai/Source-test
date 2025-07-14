/*  Class Name      : IAcMEDataSyn
 *  Purpose         : Interface to AcMEDataSyn class object
 *  Author          : Britto
 *  Created on      : 20-June-2014
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Bosco.Utility;
namespace AcMEDSync
{
    public interface IAcMEDataSyn
    {
        ResultArgs SynchronizeVouchers(string VoucherXml);
    }
}
