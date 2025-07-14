/*  Class Name      : MessageRender.cs
 *  Purpose         : To show messages
 *  Author          : CS 
 *  Created on      : 16-Jul-2010
 */

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Bosco.Utility.ConfigSetting;
using System.Text.RegularExpressions;
using System.Resources;
using System.Reflection;

namespace Bosco.Utility
{
    public class MessageRender
    {
        private static ResultArgs returnResult;

        public static ResultArgs Result
        {
            get { return returnResult; }
            set { returnResult = value; }
        }

        public static string GetMessage(ResultArgs resultArgs)
        {
            string msg = "";

            if (!resultArgs.Success)
            {
                ExceptionHandler e = resultArgs.Exception as ExceptionHandler;
                msg = e.ErrorMessage;

                if (e.IsError)
                {
                    msg += "\r\n\r\nSource::" + e.ErrorSource;
                }
            }

            return msg;
        }

        public static string GetMessage(string keyCode)
        {
            ResourceManager resourceManger = new ResourceManager("Bosco.Utility.Resources.Messages.Messages", Assembly.GetExecutingAssembly());

            string msg = "";
            try
            {
                msg = resourceManger.GetString(keyCode);
                if (string.IsNullOrEmpty(msg))
                {
                    msg = "Resoure File is not available " + keyCode;
                }
            }
            catch (Exception ex)
            {
                msg = "Resoure File is not available " + keyCode;
            }
            return msg;
        }

        public static string GetReportMessage(string keyCode)
        {
            ResourceManager resourceManger = new ResourceManager("Bosco.Utility.Resources.Messages.Reports", Assembly.GetExecutingAssembly());

            string msg = "";
            try
            {
                msg = resourceManger.GetString(keyCode);
                if (string.IsNullOrEmpty(msg))
                {
                    msg = "Resoure File is not available " + keyCode;
                }
            }
            catch (Exception ex)
            {
                msg = ""; //"Resoure File is not available " + keyCode;
            }
            return msg;
        }


        public static void ShowMessage(ResultArgs resultArgs)
        {
            ShowMessage(resultArgs, false);
        }

        public static void ShowMessage(ResultArgs resultArgs, bool isError)
        {
            returnResult = resultArgs;
            ((ExceptionHandler)returnResult.Exception).IsError = isError;
            ShowError();
        }

        public static void ShowMessage(Exception e)
        {
            returnResult = new ResultArgs();
            returnResult.Exception = e;

            if (!returnResult.IsShowExceptionMessage)
            {
                ShowError();
            }
        }

        public static void ShowMessage(string message)
        {
            ShowMessage(message, false);
        }

        public static void ShowMessage(string message, bool isError)
        {
            returnResult = new ResultArgs();
            returnResult.Message = message;
            ((ExceptionHandler)returnResult.Exception).IsError = isError;
            ShowError();
        }

        public static DialogResult ShowConfirmationMessage(string Message, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            DialogResult drResult = DialogResult.None;
            drResult = XtraMessageBox.Show(Message, "Acme.erp", messageBoxButtons, messageBoxIcon);
            return drResult;
        }

        private static void ShowError()
        {
            try
            {
                if (returnResult != null && returnResult.Success == false && returnResult.Message != "")
                {
                    if (SplashScreenManager.Default != null)
                    {
                        if (SplashScreenManager.Default.IsSplashFormVisible)
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }

                    if (((ExceptionHandler)returnResult.Exception).IsError)
                    {
                        XtraMessageBox.Show(returnResult.Message, "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        XtraMessageBox.Show(returnResult.Message, "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem in showing Message " + ex.Message);
            }
        }

    }
}
