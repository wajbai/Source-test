/*  Class Name      : ExceptionHandler.cs
 *  Purpose         : To handle exception
 *  Author          : CS
 *  Created on      : 15-Jul-2010
 */

using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;

namespace Bosco.Utility
{
    public class ExceptionHandler : Exception
    {
        #region Data Members

        private string message = "";
        private bool isError = false;
        private bool isDBError = false;
        private string errorSource = "";
        private Exception exception = new Exception();
        private static ExceptionHandler exceptionLog = null;

        #endregion

        #region Properties

        public static ExceptionHandler ExceptionLog
        {
            set { exceptionLog = value; }
        }

        public virtual void WirteLog(ExceptionHandler exceptionHandler)
        {
            
        }

        public string TargetMethod
        {
            get { return this.GetExceptionItem("METHOD"); }
        }

        public string TargetSource
        {
            get { return this.GetExceptionItem("SOURCE"); }
        }

        public string TargetAssembly
        {
            get { return this.GetExceptionItem("ASSEMBLY"); }
        }

        public override string Message
        {
            get { return this.ErrorMessage; }
        }

        public string ErrorMessage
        {
            get { return this.message; }
            set { this.message = value; }
        }

        public bool IsError
        {
            get { return this.isError; }
            set { this.isError = value; }
        }

        //Added by alwar on 07/12/2015 for decide db error or not
        public bool IsDBError
        {
            get { return this.isDBError; }
            set { this.isDBError = value; }
        }

        public string ErrorSource
        {
            get { return this.errorSource; }
            set { this.errorSource = value; }
        }

        public Exception Exception
        {
            get { return exception; }
            set { exception = value; this.isError = true; }
        }

        

        #endregion

        #region Constructors

        public ExceptionHandler()
        {

        }

        private string GetExceptionItem(string type)
        {
            string value = "";

            if (exception != null)
            {
                if (type == "METHOD")
                {
                    value = exception.TargetSite.ToString();
                }
                else if (type == "SOURCE")
                {
                    value = exception.StackTrace;
                }
                else if (type == "ASSEMBLY")
                {
                    value = exception.TargetSite.Module.Name;
                }
            }

            return value;
        }

        public ExceptionHandler(Exception e, bool showMessage)
            : this(e, "", true, showMessage) { }
        public ExceptionHandler(string message, bool showMessage)
            : this(null, message, false, showMessage) { }
        public ExceptionHandler(string message, bool isError, bool showMessage)
            : this(null, message, isError, showMessage) { }

        public ExceptionHandler(Exception e, string message, bool isError, bool showMessage)
        {
            HandleException(e, message, isError, showMessage);
        }
        #endregion

        #region Methods

        public void Add(Exception e)
        {
            HandleException(e, "", true, false);
        }

        public void Add(Exception e, string message)
        {
            HandleException(e, message, false, true);
        }

        public void Add(Exception e, string message, bool isError)
        {
            HandleException(e, message, isError, false);
        }

        public void Add(string message)
        {
            HandleException(null, message, false, false);
        }

        public void Add(string message, bool isError)
        {
            HandleException(null, message, isError, false);
        }

        public void Add(string message, bool isError, bool showMessage)
        {
            HandleException(null, message, isError, showMessage);
        }

        private void HandleException(Exception e, string message, bool isError, bool showMessage)
        {
            if (e != null)
            {
                this.exception = e;
                this.message = e.Message;
                this.ErrorSource = e.Source + "\r\n\r\n" + e.StackTrace;
            }

            this.isError = isError;
            if (message != "") { this.message = message; }
            if (exceptionLog != null) { exceptionLog.WirteLog(this); }

            //added by alwar on 07/12/2015 for decide db error or not
            this.IsDBError = IsDBStructureException(this.message);

            if (showMessage)
            {
                //MessageRender.ShowMessage(message + "\r\n\r\nSource::" + errorSource, isError);
                MessageRender.ShowMessage(message, isError);
            }
        }
        
        /// <summary>
        ///Added by alwar on 07/12/2015
        ///While showing error messages, If error message related to DB design issues like (Table not existss,
        ///Table column not exists, System instruct user to cleint updater, this will get latest version from
        ///acmerp.org port and update locally.
        /// </summary>
        /// <returns></returns>
        private bool IsDBStructureException(string execptionmessage)
        {
            bool Rtn = false;
            Match matchtblexists;
            //Could not find actual mysql exception code, so check error message, later it would be changed as exception number
            
            //1. Check Table is not exists execption exp: Table 'xxx.xxxxx' doesn't exist
            matchtblexists = Regex.Match(execptionmessage, @"Table '[^']*' doesn't exist$", RegexOptions.IgnoreCase);

            //2. Check Table Column is not exists execption exp: Unknown column 'xxxxx' in 'field list'
            if (!matchtblexists.Success)
            {
                matchtblexists = Regex.Match(execptionmessage, @"Unknown column '[^']*' in 'field list'$", RegexOptions.IgnoreCase);
            }
            
            Rtn = matchtblexists.Success;
            
            return Rtn;
        }


        /// <summary>
        /// Added by alwar on 10/12/2015, to get Acmerp updater form when exeception errors
        /// </summary>
        public Form GetUpdaterForm()
        {
            Form form = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly a in assemblies)
            {
                if (a.FullName.Contains("ACPP"))
                {
                    Type[] types = a.GetTypes();
                    foreach (Type t in types)
                    {
                        //if (t.BaseType == typeof(Form))
                        if (t.Name == "frmAcMEERPUpdater")
                        {
                            form = (Form)Activator.CreateInstance(t);
                            break;
                        }
                    }
                }
            }
            return form;
        }

        #endregion
    }
}
