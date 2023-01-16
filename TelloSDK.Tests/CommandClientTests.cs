using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using System.Text;
using TelloSDK.Infrastructure.Constants;
using TelloSDK.Infrastructure.Models;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Services;

namespace TelloSDK.Tests
{
    public class CommandClientTests
    {
        private TelloCommandClient commandClient;

        [SetUp]
        public void Setup()
        {
            var moqConnectClient = new Mock<ITelloConnectClient>();
            var moqOptionsAccessor = new Mock<IOptionsMonitor<TelloOptions>>();
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

            commandClient = new TelloCommandClient(moqOptionsAccessor.Object, moqConnectClient.Object);
        }

        [Test]
        public void ExecuteCommandTest()
        {
            string result = commandClient.ExecuteCommand("testCommand");

            Assert.That(result, Is.EqualTo("ok"));
        }

        [Test]
        public void IsInCommandModeTest()
        {
            bool result = commandClient.IsInCommandMode();

            Assert.That(result, Is.False);
        }

        [Test]
        public void InitializeCommandSDKTest()
        {
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
            commandClient.InitializeCommandSDK();
            bool resultBefore = commandClient.IsInCommandMode();
            commandClient.DisconnectCommandSDK();
            bool resultAfter = commandClient.IsInCommandMode();

            Assert.That(resultBefore, Is.True);
            Assert.That(resultAfter, Is.False);
        }
    }
}
