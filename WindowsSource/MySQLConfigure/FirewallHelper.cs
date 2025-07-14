using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetFwTypeLib;

namespace MySQLConfigure
{
    class FirewallHelper : IDisposable
    {
        public bool AddExecptionPort(string program, int port)
        {
            bool success = false;
            INetFwMgr icfMgr = null;
            try
            {
                Type TicfMgr = Type.GetTypeFromProgID("HNetCfg.FwMgr");
                icfMgr = (INetFwMgr)Activator.CreateInstance(TicfMgr);
                success = true;
            }
            catch (Exception ex)
            {
                General.WriteLog("Error in adding port into firewall exception " + ex.Message);
                success = false;
            }
            
            if (success)
            {
                try
                {
                    INetFwProfile profile;
                    INetFwOpenPort portClass;
                    Type TportClass = Type.GetTypeFromProgID("HNetCfg.FWOpenPort");
                    portClass = (INetFwOpenPort)Activator.CreateInstance(TportClass);
                    // Get the current profile
                    profile = icfMgr.LocalPolicy.CurrentProfile;

                    // Set the port properties
                    portClass.Scope = NetFwTypeLib.NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
                    portClass.Enabled = true;
                    portClass.Name = program;
                    portClass.Port = port;

                    //NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP; ;
                    // Add the port to the ICF Permissions List
                    profile.GloballyOpenPorts.Add(portClass);
                    General.WriteLog("Added My service into firewall exception ");
                    success = true;
                }
                catch (Exception ex)
                {
                    General.WriteLog("Error in adding port into firewall exception " + ex.Message);
                    success = false;
                }
            }
            return success;
        }

        public bool AddExecptionApplication(string program, string path)
        {
            bool success = false;
            INetFwMgr icfMgr = null;
            try
            {
                Type TicfMgr = Type.GetTypeFromProgID("HNetCfg.FwMgr");
                icfMgr = (INetFwMgr)Activator.CreateInstance(TicfMgr);
                success = true;
            }
            catch (Exception ex)
            {
                General.WriteLog("Error in adding application into firewall exception " + ex.Message);
                success = false;
            }

            if (success)
            {
                try
                {
                    INetFwProfile profile;
                    INetFwAuthorizedApplication applicationClass;
                    Type TportClass = Type.GetTypeFromProgID("HNetCfg.FwAuthorizedApplication");
                    applicationClass = (INetFwAuthorizedApplication)Activator.CreateInstance(TportClass);
                    // Get the current profile
                    profile = icfMgr.LocalPolicy.CurrentProfile;

                    // Set the port properties
                    applicationClass.Scope = NetFwTypeLib.NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
                    applicationClass.Enabled = true;
                    applicationClass.Name = program;
                    applicationClass.ProcessImageFileName = path;

                    //NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP; ;
                    // Add the port to the ICF Permissions List
                    profile.AuthorizedApplications.Add(applicationClass);
                    General.WriteLog("Added My.exe into firewall exception ");
                    success = true;
                }
                catch (Exception ex)
                {
                    General.WriteLog("Error in adding application into firewall exception " + ex.Message);
                    success = false;
                }
            }
            return success;
        }


        public void IncludeExecptionPort(string program, int port)
        {
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
        }

    }   
}
