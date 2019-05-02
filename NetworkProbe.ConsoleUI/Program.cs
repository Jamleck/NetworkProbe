using System;

namespace NetworkProbe.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var arcScan = new ArpScanNetworkProbe();
            var result = arcScan.Probe();

            Console.WriteLine(result);

            Console.Read();
        }
    }
}
