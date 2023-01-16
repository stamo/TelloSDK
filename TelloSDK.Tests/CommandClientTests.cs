using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Services;

namespace TelloSDK.Tests
{
    public class CommandClientTests
    {
        private Mock<UdpClient> moqUdpClient = new Mock<UdpClient>();

        [OneTimeSetUp]
        public void Setup()
        {
            moqUdpClient.Setup(c => c.Send(
                It.IsAny<byte[]>(), 
                It.IsAny<int>(),
                It.IsAny<IPEndPoint>()))
                .Returns((byte[] b, int i, IPEndPoint ep) => i);
            moqUdpClient.Setup(c => c.Receive(ref It.Ref<IPEndPoint?>.IsAny))
                .Returns(Encoding.ASCII.GetBytes("ok"));
        }

        //[Test]
        //public void TestExecuteCommand()
        //{
        //    var commandClient = new TelloCommandClient()
        //}
    }
}
