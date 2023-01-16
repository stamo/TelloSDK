using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TelloSDK.Infrastructure.Constants;
using TelloSDK.Infrastructure.Models;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Services;

namespace TelloSDK.Tests
{
    public class CommandClientTests
    {
        private Mock<ITelloConnectClient> moqConnectClient = new Mock<ITelloConnectClient>();
        private Mock<IOptionsMonitor<TelloOptions>> moqOptionsAccessor = new Mock<IOptionsMonitor<TelloOptions>>();

        [OneTimeSetUp]
        public void Setup()
        {
            moqConnectClient.Setup(c => c.Send(
                It.IsAny<byte[]>(), 
                It.IsAny<int>(),
                It.IsAny<IPEndPoint>()))
                .Returns((byte[] b, int i, IPEndPoint ep) => i);
            moqConnectClient.Setup(c => c.Receive(ref It.Ref<IPEndPoint>.IsAny))
                .Returns(Encoding.ASCII.GetBytes("ok"));
            moqConnectClient.SetupSet(c => c.ReceiveTimeout = It.IsAny<int>());
            moqOptionsAccessor.Setup(o => o.CurrentValue)
                .Returns(new TelloOptions() 
                { 
                    IPAddress = IPAddress.Parse("127.0.0.1"),
                    Port = 8080
                });
        }

        [Test]
        public void ExecuteCommandTest()
        {
            var connectClient = moqConnectClient.Object;
            var optionsAccessor = moqOptionsAccessor.Object;

            var commandClient = new TelloCommandClient(optionsAccessor, connectClient);
            string result = commandClient.ExecuteCommand("testCommand");

            Assert.That(result, Is.EqualTo("ok"));
        }

        [Test]
        public void IsInCommandModeTest()
        {
            var connectClient = moqConnectClient.Object;
            var optionsAccessor = moqOptionsAccessor.Object;

            var commandClient = new TelloCommandClient(optionsAccessor, connectClient);
            bool result = commandClient.IsInCommandMode();

            Assert.That(result, Is.False);
        }

        [Test]
        public void InitializeCommandSDKTest()
        {
            var connectClient = moqConnectClient.Object;
            var optionsAccessor = moqOptionsAccessor.Object;

            var commandClient = new TelloCommandClient(optionsAccessor, connectClient);
            bool resultBefore = commandClient.IsInCommandMode();
            TelloActionResult result = commandClient.InitializeCommandSDK();
            bool resultAfter = commandClient.IsInCommandMode();

            Assert.That(resultBefore, Is.False);
            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
            Assert.That(resultAfter, Is.True);
        }


        [Test]
        public void DisconnectCommandSDKTest()
        {
            var connectClient = moqConnectClient.Object;
            var optionsAccessor = moqOptionsAccessor.Object;

            var commandClient = new TelloCommandClient(optionsAccessor, connectClient);
            commandClient.InitializeCommandSDK();
            bool resultBefore = commandClient.IsInCommandMode();
            commandClient.DisconnectCommandSDK();
            bool resultAfter = commandClient.IsInCommandMode();

            Assert.That(resultBefore, Is.True);
            Assert.That(resultAfter, Is.False);
        }
    }
}
