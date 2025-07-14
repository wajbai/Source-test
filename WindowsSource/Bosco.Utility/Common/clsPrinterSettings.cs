using System;

namespace Bosco.Utility.Common
{	
	public class clsPrinterSettings
	{
		public clsPrinterSettings()
		{			
		}
		public object getReportListQuery()
		{
			object sSql = "SELECT RPT_NAME, RPT_CODE "+
				" FROM REPORT_MAIN WHERE RPT_TYPE = 1 AND "+
				" RPT_MODULE IS NOT NULL";
			return sSql;
		}
	}
}
