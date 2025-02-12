using EntityManager.Interfaces;
using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace EntityManager.Utility
{
    public class MachineCredentials : IMachineCredentials
    {
        public string GetMacAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up &&
                            (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet || nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault() ?? "MAC Address Not Found";
        }

        public string GetPCName()
        {
            return Environment.MachineName;
        }
    }
}
