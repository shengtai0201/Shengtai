using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;

namespace Shengtai
{
    public class Performances
    {
        private static readonly PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        private static readonly ManagementObjectSearcher memorySearcher = new ManagementObjectSearcher("select * from Win32_ComputerSystem");
        private static readonly PerformanceCounter memoryCounter = new PerformanceCounter("Memory", "Available MBytes");

        private static readonly ManagementObjectSearcher hardDiskSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DeviceId='C:'");

        private static readonly PerformanceCounterCategory networkCategory = new PerformanceCounterCategory("Network Interface");
        private static readonly char[] CHARACTER = new char[] { '(', ')', '#', '\\', '/' };
        private static readonly char[] INSTANCE_MAPPED_CHARACTER = new char[] { '[', ']', '_', '_', '_' };

        private class NetworkCounter
        {
            public PerformanceCounter CurrentBandwidthCounter { get; set; }
            public PerformanceCounter BytesTotalCounter { get; set; }

            public NetworkCounter(string instanceName)
            {
                this.CurrentBandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", instanceName);
                this.BytesTotalCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", instanceName);

                this.CurrentBandwidthCounter.NextValue();
                this.BytesTotalCounter.NextValue();
            }
        }

        public enum NetworkType
        {
            All,
            Ethernet,
            Tunnel
        }

        public Performances()
        {
            cpuCounter.NextValue();
            memoryCounter.NextValue();
        }

        public double? GetCpuUsage()
        {
            try
            {
                return Math.Round(cpuCounter.NextValue(), 1);
            }
            catch { }

            return null;
        }

        public double? GetMemoryUsage()
        {
            try
            {
                double total = 0;
                foreach (ManagementObject memory in memorySearcher.Get())
                    total += Convert.ToDouble(memory["TotalPhysicalMemory"]) / 1024 / 1024;

                return Math.Round(100 - (memoryCounter.NextValue() / total * 100), 1);
            }
            catch { }

            return null;
        }

        public decimal? GetHardDiskUsage()
        {
            try
            {
                foreach (ManagementObject hardDisk in hardDiskSearcher.Get())
                {
                    var size = Convert.ToDecimal(hardDisk["Size"]);
                    var freeSpace = Convert.ToDecimal(hardDisk["FreeSpace"]);

                    return Math.Round(100 - (freeSpace / size * 100), 1);
                }
            }
            catch { }

            return null;
        }

        private string InstanceNameNormalization(string name)
        {
            for (int i = 0; i < CHARACTER.Length; i++)
                name = name.Replace(CHARACTER[i], INSTANCE_MAPPED_CHARACTER[i]);

            return name;
        }

        public string[] GetNetworkInstanceNames(NetworkType type)
        {
            switch (type)
            {
                case NetworkType.Ethernet:
                    return NetworkInterface.GetAllNetworkInterfaces()
                        .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                        .Select(n => InstanceNameNormalization(n.Description))
                        .ToArray();

                case NetworkType.Tunnel:
                    return NetworkInterface.GetAllNetworkInterfaces().Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType == NetworkInterfaceType.Tunnel).Select(n => InstanceNameNormalization(n.Name)).ToArray();

                default:
                    return networkCategory.GetInstanceNames();
            }
        }

        public double? GetNetworkUsage(string instanceName)
        {
            try
            {
                var networkCounter = new NetworkCounter(instanceName);
                float bandwidth = networkCounter.CurrentBandwidthCounter.NextValue();
                float total = networkCounter.BytesTotalCounter.NextValue();

                return Math.Round((8 * total) / bandwidth * 100, 1);
            }
            catch { }

            return null;
        }
    }
}