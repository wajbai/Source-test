/*  Class Name      : ResultArgs.cs
 *  Purpose         : Call Return
 *  Author          : CS 
 *  Created on      : 15-Jul-2010
 */

using System;
using System.Data;
using System.Collections.Generic;

namespace Payroll.Utility
{
    public class ResultArgs : IDisposable
    {
        private ExceptionHandler exception = new ExceptionHandler();
        private bool success = false;
        private int rowsAffected = 0;
        private bool deadLock = false;
        private bool isShowExceptionMessage = true;
        private object rowUniqueId = "";
        private object returnValue = null;
        private Dictionary<string, object> rowUniqueIdCollection = null;
        private ResultSource resultSource = new ResultSource();

        public ResultArgs()
        {

        }

        public ResultArgs(bool isShowExceptionMessage)
        {
            this.isShowExceptionMessage = isShowExceptionMessage;
        }

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        public bool IsShowExceptionMessage
        {
            get { return isShowExceptionMessage; }
            set { isShowExceptionMessage = value; }
        }

        public int RowsAffected
        {
            get { return rowsAffected; }
            set
            {
                rowsAffected = value;
                success = (rowsAffected >= 0);
            }
        }

        public bool IsDeadLock
        {
            get { return deadLock; }
            set
            {
                deadLock = value;
            }
        }

        public object RowUniqueId
        {
            get { return rowUniqueId; }
            set
            {
                this.success = true;
                rowUniqueId = value;
            }
        }

        public Dictionary<string, object> RowUniqueIdCollection
        {
            get
            {
                if (rowUniqueIdCollection == null)
                {
                    rowUniqueIdCollection = new Dictionary<string, object>();
                }

                return rowUniqueIdCollection;
            }
            set
            {
                this.success = true;
                rowUniqueIdCollection = value;
            }
        }

        public object ReturnValue
        {
            get { return returnValue; }
            set { returnValue = value; }
        }

        public ResultSource DataSource
        {
            get { return resultSource; }

            set
            {
                this.success = true;
                resultSource.Data = value;
            }
        }

        public Exception Exception
        {
            get { return this.exception; }
            set
            {
                this.success = false;
                this.exception.Add(value);

                if (isShowExceptionMessage)
                {
                    MessageRender.ShowMessage(value.Message, true); //Call message when exception set from other location
                }
            }
        }

        public string Message
        {
            get { return this.exception.Message; }
            set
            {
                this.success = false;
                this.exception.Add(value);
            }
        }

        /// <summary>
        /// This method is for windows application
        /// </summary>
        public void ShowMessage()
        {
            MessageRender.ShowMessage(this);
        }

        /// <summary>
        /// Redirect to Error Page
        /// </summary>
        public void ShowMessage(string message)
        {
            MessageRender.ShowMessage(message);
        }

        /// <summary>
        /// Redirect to Error Page
        /// </summary>
        public void ShowMessage(string message, bool isError)
        {
            MessageRender.ShowMessage(message, isError);
        }

        /// <summary>
        /// Redirect to Error Page
        /// </summary>
        public void ShowMessage(Exception e, bool isError)
        {
            this.Exception = e;
            MessageRender.ShowMessage(this, isError);
        }

        #region Class Result Source

        public class ResultSource : IDisposable
        {
            private object dataSource = null;
            private ScalarType scalarType = null;

            public ResultSource()
            {
                scalarType = new ScalarType();
            }

            public object Data
            {
                get { return dataSource; }
                set
                {
                    dataSource = value;
                    scalarType.SclarSource = dataSource;
                }
            }

            /// <summary>
            /// Get Dataset Object
            /// </summary>
            public DataSet TableSet
            {
                get { return dataSource as DataSet; }
            }

            /// <summary>
            /// Get Data Table Object
            /// </summary>
            public DataTable Table
            {
                get { return dataSource as DataTable; }
            }

            /// <summary>
            /// Get Data View Object
            /// </summary>
            public DataView TableView
            {
                get { return dataSource as DataView; }
            }

            /// <summary>
            /// Get Sclar value
            /// </summary>
            /// 
            public ScalarType Sclar
            {
                get { return scalarType; }
            }

            public class ScalarType
            {
                private object dataSource = null;

                public object SclarSource
                {
                    set { dataSource = value; }
                }

                public new string ToString
                {
                    get
                    {
                        string sclarVal = "";

                        if (dataSource != null)
                        {
                            sclarVal = dataSource.ToString();
                        }

                        return sclarVal;
                    }
                }

                public int ToInteger
                {
                    get
                    {
                        int sclarVal = 0;
                        int.TryParse(this.ToString, out sclarVal);
                        return sclarVal;
                    }
                }
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (scalarType != null) { scalarType = null; }
                if (dataSource != null) { dataSource = null; }
            }

            #endregion
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (exception != null) { exception = null; }

            if (resultSource != null)
            {
                resultSource.Dispose();
                resultSource = null;
            }

            if (rowUniqueIdCollection != null)
            {
                rowUniqueIdCollection.Clear();
                rowUniqueIdCollection = null;
            }
        }

        #endregion
    }
}