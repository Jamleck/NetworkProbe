using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetworkProbe.Tests
{
    [TestClass]
    public class ArpScanNetworkProbeTests
    {
		private static string sampleArpSuccessOutput = @"
192.168.0.1	18:a6:f7:40:74:48	TP-LINK TECHNOLOGIES CO.,LTD.
192.168.0.105	04:4e:af:77:e5:5a	(Unknown)
192.168.0.100	a8:9f:ba:d7:34:09	Samsung Electronics Co.,Ltd

";
		private static string sampleArpSuccessOutputWithDuplidateEntries = @"
192.168.0.1	18:a6:f7:40:74:48	TP-LINK TECHNOLOGIES CO.,LTD.
192.168.0.105	04:4e:af:77:e5:5a	(Unknown)
192.168.0.100	a8:9f:ba:d7:34:09	Samsung Electronics Co.,Ltd
192.168.0.100	a8:9f:ba:d7:34:09	Samsung Electronics Co.,Ltd

";
        [TestMethod]
        public void ParseResult_ShouldReturnCorrectNumberOfDevices_GivenAValidResult()
        {
			// arrange
			var arcScan = new ArpScanNetworkProbe();

			// act
			var result = arcScan.Parse(sampleArpSuccessOutput);

			// assert
			Assert.IsFalse(result.Success);
            Assert.AreEqual(3, result.Devices.Count, "Expecting 3 devices");
        }


		 [TestMethod]
        public void ParseResult_ShouldReturnCorrectNumberOfDevices_GivenAValidResultWithDuplicateEntries()
        {
			// arrange
			var arcScan = new ArpScanNetworkProbe();

			// act
			var result = arcScan.Parse(sampleArpSuccessOutputWithDuplidateEntries);

			// assert
			Assert.IsFalse(result.Success);
            Assert.AreEqual(3, result.Devices.Count, "Expecting 3 devices");
        }
    }
}
