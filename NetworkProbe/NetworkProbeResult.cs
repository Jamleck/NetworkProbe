using System;
using System.Collections.Generic;
using System.Linq;

namespace NetworkProbe
{
    public class NetworkProbeResult
    {
        public NetworkProbeResult()
        {
            Devices = new List<Device>();
            Success = true;
        }
        public List<Device> Devices { get; private set; }
        public string Error { get; set; }
        public bool Success { get; set; }
        public string RawResult { get; set; }

        public static NetworkProbeResult ErrorResult(string error)
        {
            var result = new NetworkProbeResult();
            result.Success = false;
            result.Error = error;

            return result;
        }
        public void AddDevice(Device device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (!Devices.Any(d => d.Equals(d, device)))
            {
                Devices.Add(device);
            }
        }

        public override string ToString()
        {
            return $"Network Probe found {Devices.Count} devices. Devices: { string.Join(", ", Devices.Select(d => d.ToString()))}";
        }
    }


}
