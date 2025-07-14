using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetFwTypeLib;

using NATUPNPLib;
using NETCONLib;
using NetFwTypeLib;

namespace MySQLConfigure
{
    class FirewallHelper1 : IDisposable
    {
        private const string CLSID_FIREWALL_MANAGER = "{304CE942-6E39-40D8-943A-B913C40C9CD4}";
        private static NetFwTypeLib.INetFwMgr GetFirewallManager()
        {
            Type objectType = Type.GetTypeFromCLSID(
                  new Guid(CLSID_FIREWALL_MANAGER));
            return Activator.CreateInstance(objectType)
                  as NetFwTypeLib.INetFwMgr;
        }

        public bool AddExecptionPort(string program, int port)
        {
            bool success = false;
            try
            {
                success =  GloballyOpenPort(program, port, NetFwTypeLib.NET_FW_SCOPE_.NET_FW_SCOPE_ALL, NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NetFwTypeLib.NET_FW_IP_VERSION_.NET_FW_IP_VERSION_ANY);
            }
            catch (Exception ex)
            {
              General.WriteLog("Error in adding port into firewall exception " + ex.Message);
              success = false;
            }
            return success;
        }

        public bool AddExecptionApplication(string program, string path)
        {
            bool success = false;
            try
            {
                success = AuthorizeApplication(program, path,NetFwTypeLib.NET_FW_SCOPE_.NET_FW_SCOPE_ALL,NetFwTypeLib.NET_FW_IP_VERSION_.NET_FW_IP_VERSION_ANY);
            }
            catch (Exception ex)
            {
                General.WriteLog("Error in adding application into firewall exception " + ex.Message);
                success = false;
            }
            return success;
        }
        
        public void IncludeExecptionPort(string program, int port)
        {
            //INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            //firewallRule.Name = "MySQLACPERP";
            //firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            //firewallRule.Description = "Allow AcMEERP MySQL Service port";
            //firewallRule.InterfaceTypes = "All";
            //firewallRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            //firewallRule.LocalPorts = port.ToString();
            //firewallRule.Enabled = true;

            //INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
            //    Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            //firewallPolicy.Rules.Add(firewallRule);
        }

    // ProgID for the AuthorizedApplication object
    private const string PROGID_AUTHORIZED_APPLICATION = "HNetCfg.FwAuthorizedApplication";
    private bool AuthorizeApplication(string title, string applicationPath, NET_FW_SCOPE_ scope, NET_FW_IP_VERSION_ ipVersion)
    {    
      // Create the type from prog id  
      Type type = Type.GetTypeFromProgID(PROGID_AUTHORIZED_APPLICATION);
      INetFwAuthorizedApplication auth = Activator.CreateInstance(type) as INetFwAuthorizedApplication;
      auth.Name  = title;
      auth.ProcessImageFileName = applicationPath;
      auth.Scope = scope;
      auth.IpVersion = ipVersion;
      auth.Enabled = true;
        
      INetFwMgr manager = GetFirewallManager();
      try  {
        manager.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(auth);
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    private const string PROGID_OPEN_PORT = "HNetCfg.FWOpenPort";
    private bool GloballyOpenPort(string title, int portNo, NET_FW_SCOPE_ scope, NET_FW_IP_PROTOCOL_ protocol, NET_FW_IP_VERSION_ ipVersion)
    {
      Type type = Type.GetTypeFromProgID(PROGID_OPEN_PORT);
      INetFwOpenPort port = Activator.CreateInstance(type) as INetFwOpenPort;
      port.Name = title;
      port.Port = portNo;
      port.Scope = scope;
      port.Protocol = protocol;
      port.IpVersion = ipVersion;
      INetFwMgr manager = GetFirewallManager();
      try  {
        manager.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(port);
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
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
