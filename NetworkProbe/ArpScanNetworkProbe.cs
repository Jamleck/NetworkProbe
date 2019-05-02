using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NetworkProbe
{

    public class ArpScanNetworkProbe
    {

        public ArpScanNetworkProbe()
        {
        }


        public NetworkProbeResult Probe()
        {
            string networkProbeCommand = "sudo arp-scan --localnet --plain";

            try
            {
                var result = Bash(networkProbeCommand);

				return Parse(result);
            }
            catch (Exception ex)
            {
                var error = $"An error occcurred while trying to run the {networkProbeCommand} command. Error:{ex.Message}";

                return NetworkProbeResult.ErrorResult(error);
            }


        }

        public NetworkProbeResult Parse(string input)
        {
            NetworkProbeResult result = new NetworkProbeResult();

            if (!string.IsNullOrWhiteSpace(input))
            {
                var devices = ParseDeviceList(input);
                foreach (var device in devices)
                {
                    result.AddDevice(device); // Checks for duplicate entries
                }
            }

            result.RawResult = input;
            return result;
        }



        private List<Device> ParseDeviceList(string input)
        {
            List<Device> devices = new List<Device>();

            string[] lines = input.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in lines)
            {

                var tokens = item.Split('\t');
                if (tokens.Length >= 2)
                {
                    var device = new Device(tokens[0], tokens[1]);
                    devices.Add(device);
                }
            }


            return devices;
        }

        public static string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }


    }


}
