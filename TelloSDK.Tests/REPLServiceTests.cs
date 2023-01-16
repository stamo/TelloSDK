using Moq;
using TelloSDK.Contracts;
using TelloSDK.Enumerations;
using TelloSDK.Infrastructure.Constants;
using TelloSDK.Pilot.Services;

namespace TelloSDK.Tests
{
    public class REPLServiceTests
    {
        private Mock<IPilot> moqPilot = new Mock<IPilot>();

        [Test]
        public void UnknownCommandTest()
        {
            var replService = new REPLService(moqPilot.Object);
            var result = replService.ExecuteCommand("unknownCommand");

            Assert.That(result, Is.EqualTo("Unknown command"));
        }

        [Test]
        public void InvalidParametersTest()
        {
            var replService = new REPLService(moqPilot.Object);
            var result = replService.ExecuteCommand("up 40 80");

            Assert.That(result, Is.EqualTo("Invalid parameters"));
        }

        [Test]
        public void FlipCommandTest()
        {
            moqPilot.Setup(p => p.Flip(It.IsAny<Direction>()))
                .Returns(new Models.TelloActionResult()
                {
                    Succeeded = true,
                    Message = TelloResponse.Success
                });

            var replService = new REPLService(moqPilot.Object);
            var result = replService.ExecuteCommand("flip f");

            Assert.That(result, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ForwardCommandOneIntParameterTest()
        {
            moqPilot.Setup(p => p.Forward(It.IsAny<int>()))
                .Returns(new Models.TelloActionResult()
                {
                    Succeeded = true,
                    Message = TelloResponse.Success
                });

            var replService = new REPLService(moqPilot.Object);
            var result = replService.ExecuteCommand("forward 50");

            Assert.That(result, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void TakeoffCommandNoParametersTest()
        {
            moqPilot.Setup(p => p.TakeOff())
                .Returns(new Models.TelloActionResult()
                {
                    Succeeded = true,
                    Message = TelloResponse.Success
                });

            var replService = new REPLService(moqPilot.Object);
            var result = replService.ExecuteCommand("takeoff");

            Assert.That(result, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void GoCommandFourIntParameterTest()
        {
            moqPilot.Setup(p => p.Go(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Models.TelloActionResult()
                {
                    Succeeded = true,
                    Message = TelloResponse.Success
                });

            var replService = new REPLService(moqPilot.Object);
            var result = replService.ExecuteCommand("go 50 50 50 50");

            Assert.That(result, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void GoCommandWrongParameterTypeTest()
        {
            moqPilot.Setup(p => p.Go(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Models.TelloActionResult()
                {
                    Succeeded = true,
                    Message = TelloResponse.Success
                });

            var replService = new REPLService(moqPilot.Object);
            var result = replService.ExecuteCommand("go 50 rt 50 50");

            Assert.That(result, Is.EqualTo("Invalid parameters"));
        }
    }
}
