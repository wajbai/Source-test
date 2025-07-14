/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace Bosco.Utility.CommonMemberSet
{
    #region General Set

    public class FileSetMember
    {
        public ResultArgs CreateDirectory(string path, string directoryName)
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = true;

            string createPath = Path.Combine(path, directoryName);

            if (Directory.Exists(path))
            {
                if (!Directory.Exists(createPath))
                {
                    try
                    {
                        Directory.CreateDirectory(createPath);
                    }
                    catch (Exception err)
                    {
                        resultArgs.Exception = err;
                    }
                }
            }

            return resultArgs;
        }

        public double GetFileSizeInMB(string filepath)
        {
            double rtn = 0;
            try
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                if (File.Exists(filepath))
                {
                    double len = new FileInfo(filepath).Length;
                    rtn = len / (1024 * 1024); //For MB in size
                    //int order = 0;
                    //while (len >= 1024 && order < sizes.Length - 1)
                    //{
                    //    order++;
                    //    len = len / 1024;
                    //}
                    //string result = String.Format("{0:0.##} {1}", len, sizes[order]);
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn;
        }
    }

    #endregion
}
