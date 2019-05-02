using System;
using System.Collections.Generic;
using System.Net;

namespace NetworkProbe
{
    public class Device : EqualityComparer<Device>
    {
        public Device(string IPAddress, string physicalAddress)
        {
            Ip = System.Net.IPAddress.Parse(IPAddress);
            PhysicalAddress = physicalAddress;
        }
        public IPAddress Ip { get; set; }
        public string PhysicalAddress { get; set; }

        public override bool Equals(Device d1, Device d2)
        {
            if (d1 == null && d2 == null)
                return true;
            else if (d1 == null || d2 == null)
                return false;

            return (d1.Ip.ToString() == d2.Ip.ToString() &&
            d1.PhysicalAddress == d2.PhysicalAddress);
        }

        public override int GetHashCode(Device obj)
        {
            if (obj == null)
                return 0;
            return obj.Ip.GetHashCode() ^ obj.PhysicalAddress.GetHashCode();
        }

        public override string ToString()
        {
            return $"IP: {Ip} Mac Address: {PhysicalAddress}";
        }
    }
}
