﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
using AcMEERPUpdater;

namespace AcMEERPUpdater
{
    /// <summary>
    /// A class for loading Embedded Assembly
    /// </summary>
    public class EmbeddedAssembly
    {
        // Version 1.3

        static Dictionary<string, Assembly> dic = null;

        /// <summary>
        /// Load Assembly, DLL from Embedded Resources into memory.
        /// </summary>
        /// <param name="embeddedResource">Embedded Resource string. Example: WindowsFormsApplication1.SomeTools.dll</param>
        /// <param name="fileName">File Name. Example: SomeTools.dll</param>
        public static bool Load(string embeddedResource, string fileName)
        {
            bool Rtn = false;
            bool isManagedDLL = false;
            try
            {
                if (dic == null)
                    dic = new Dictionary<string, Assembly>();

                byte[] ba = null;
                Assembly asm = null;
                Assembly curAsm = Assembly.GetExecutingAssembly();

                using (Stream stm = curAsm.GetManifestResourceStream(embeddedResource))
                {
                    // Either the file is not existed or it is not mark as embedded resource
                    if (stm != null)
                    {
                        // Get byte[] from the file from embedded resource
                        ba = new byte[(int)stm.Length];
                        stm.Read(ba, 0, (int)stm.Length);
                        try
                        {
                            asm = Assembly.Load(ba);

                            // Add the assembly/dll into dictionary
                            dic.Add(asm.FullName, asm);
                            Rtn = true;
                            isManagedDLL = true;
                        }
                        catch
                        {
                            // Purposely do nothing
                            // Unmanaged dll or assembly cannot be loaded directly from byte[]
                            // Let the process fall through for next part
                        }
                    }
                }


                if (!isManagedDLL) //For Unmanaged DLLS
                {
                    bool fileOk = false;
                    string tempFile = "";

                    using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                    {
                        // Get the hash value from embedded DLL/assembly
                        string fileHash = BitConverter.ToString(sha1.ComputeHash(ba)).Replace("-", string.Empty);

                        // Define the temporary storage location of the DLL/assembly
                        tempFile = Path.GetTempPath() + fileName;

                        // Determines whether the DLL/assembly is existed or not
                        if (File.Exists(tempFile))
                        {
                            // Get the hash value of the existed file
                            byte[] bb = File.ReadAllBytes(tempFile);
                            string fileHash2 = BitConverter.ToString(sha1.ComputeHash(bb)).Replace("-", string.Empty);

                            // Compare the existed DLL/assembly with the Embedded DLL/assembly
                            if (fileHash == fileHash2)
                            {
                                // Same file
                                fileOk = true;
                            }
                            else
                            {
                                // Not same
                                fileOk = false;
                            }
                        }
                        else
                        {
                            // The DLL/assembly is not existed yet
                            fileOk = false;
                        }
                    }

                    // Create the file on disk
                    if (!fileOk)
                    {
                        System.IO.File.WriteAllBytes(tempFile, ba);
                    }

                    // Load it into memory
                    asm = Assembly.LoadFile(tempFile);

                    // Add the loaded DLL/assembly into dictionary
                    dic.Add(asm.FullName, asm);
                    Rtn = true;
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageBox.Show("MySQL component is missing in AcMEERP Updater, Contact BoscoSoft Admin", General.ACMEERP_UPDATER_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                General.WriteLog("Error in Load EmbeddedAssembly " + err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// Retrieve specific loaded DLL/assembly from memory
        /// </summary>
        /// <param name="assemblyFullName"></param>
        /// <returns></returns>
        public static Assembly Get(string assemblyFullName)
        {
            Assembly rtnAssembly = null;
            try
            {
                if (dic != null || dic.Count > 0)
                {
                    if (dic.ContainsKey(assemblyFullName))
                    {
                        rtnAssembly = dic[assemblyFullName];
                    }
                }
                // Don't throw Exception if the dictionary does not contain the requested assembly.
                // This is because the event of AssemblyResolve will be raised for every
                // Embedded Resources (such as pictures) of the projects.
                // Those resources wil not be loaded by this class and will not exist in dictionary.
            }
            catch (Exception err)
            {
                MessageBox.Show("MySQL component is missing in AcMEERP Updater, Contact BoscoSoft Admin", General.ACMEERP_UPDATER_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                General.WriteLog("Error in Get EmbeddedAssembly " + err.Message);
            }
            return rtnAssembly;
        }
    }
}