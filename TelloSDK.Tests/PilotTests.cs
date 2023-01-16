namespace TelloSDK.Tests
{
    using Moq;
    using TelloSDK.Contracts;
    using TelloSDK.Enumerations;
    using TelloSDK.Models;
    using TelloSDK.Pilot.Contracts;
    using TelloSDK.Services;

    public class PilotTests
    {
        private readonly TelloActionResult okResult = new TelloActionResult(true, "ok");
        private readonly TelloActionResult errorResult = new TelloActionResult(false, "error");
        private Mock<ITelloCommandClient> moqCommandClient = new Mock<ITelloCommandClient>();
        private Mock<ITelloValidationService> moqValidationService = new Mock<ITelloValidationService>();

        [OneTimeSetUp]
        public void Setup()
        {
            moqCommandClient.Setup(c => c.InitializeCommandSDK()).Returns(new TelloActionResult(true, "command"));
            moqCommandClient.Setup(c => c.DisconnectCommandSDK());
            moqCommandClient.Setup(c => c.ExecuteCommand(It.IsAny<string>())).Returns((string s) => s);
            moqCommandClient.Setup(c => c.IsInCommandMode()).Returns(true);
        }

        [Test]
        public void IgnitionTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Ignition();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("command"));
        }

        [Test]
        public void TakeOffTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.TakeOff();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("takeoff"));
        }

        [Test]
        public void LandTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Land();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("land"));
        }

        [Test]
        public void StreamOnTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.StreamOn();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("streamon"));
        }

        [Test]
        public void StreamOffTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.StreamOff();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("streamoff"));
        }

        [Test]
        public void EmergencyTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Emergency();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("emergency"));
        }

        [Test]
        public void UpTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateUp(It.IsInRange(20, 500, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Up(100);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("up 100"));
        }

        [Test]
        public void UpLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateUp(It.Is<int>(x => x < 20 || x > 500)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Up(600);
            var resultLow = pilot.Up(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void DownTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateDown(It.IsInRange(20, 500, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Down(100);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("down 100"));
        }

        [Test]
        public void DownLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateDown(It.Is<int>(x => x < 20 || x > 500)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Down(600);
            var resultLow = pilot.Down(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void LeftTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateLeft(It.IsInRange(20, 500, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Left(100);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("left 100"));
        }

        [Test]
        public void LeftLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateLeft(It.Is<int>(x => x < 20 || x > 500)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Left(600);
            var resultLow = pilot.Left(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void RightTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateRight(It.IsInRange(20, 500, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Right(100);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("right 100"));
        }

        [Test]
        public void RightLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateRight(It.Is<int>(x => x < 20 || x > 500)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Right(600);
            var resultLow = pilot.Right(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void ForwardTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateForward(It.IsInRange(20, 500, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Forward(100);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("forward 100"));
        }

        [Test]
        public void ForwardLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateForward(It.Is<int>(x => x < 20 || x > 500)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Forward(600);
            var resultLow = pilot.Forward(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void BackwardTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateBackward(It.IsInRange(20, 500, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Backward(100);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("back 100"));
        }

        [Test]
        public void BackwardLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateBackward(It.Is<int>(x => x < 20 || x > 500)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Backward(600);
            var resultLow = pilot.Backward(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void TurnClockwiseTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateTurnClockwise(It.IsInRange(1, 360, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.TurnClockwise(90);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("cw 90"));
        }

        [Test]
        public void TurnClockwiseLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateTurnClockwise(It.Is<int>(x => x < 1 || x > 360)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.TurnClockwise(600);
            var resultLow = pilot.TurnClockwise(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void TurnCounterClockwiseTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateTurnCounterClockwise(It.IsInRange(1, 360, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.TurnCounterClockwise(90);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("ccw 90"));
        }

        [Test]
        public void TurnCounterClockwiseLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateTurnCounterClockwise(It.Is<int>(x => x < 1 || x > 360)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.TurnCounterClockwise(600);
            var resultLow = pilot.TurnCounterClockwise(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void FlipBackTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Flip(Direction.Back);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("flip b"));
        }

        [Test]
        public void FlipRightTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Flip(Direction.Right);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("flip r"));
        }
        [Test]
        public void FlipForwardTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Flip(Direction.Forward);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("flip f"));
        }
        [Test]
        public void FlipLeftTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Flip(Direction.Left);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("flip l"));
        }

        [Test]
        public void GoTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateGo(
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(10, 100, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Go(100, 100, 100, 50);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("go 100 100 100 50"));
        }

        [Test]
        public void GoLimitsXTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateGo(
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Go(600, 100, 100, 50);
            var resultLow = pilot.Go(-30, 100, 100, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void GoLimitsYTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateGo(
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Go(100, 600, 100, 50);
            var resultLow = pilot.Go(100, -30, 100, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void GoLimitsZTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateGo(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Go(100, 100, 600, 50);
            var resultLow = pilot.Go(100, 100, -30, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void GoLimitsSpeedTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateGo(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 10 || x > 100)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Go(100, 100, 100, 150);
            var resultLow = pilot.Go(100, 100, 100, -30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void StopTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Stop();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("stop"));
        }

        [Test]
        public void CurveTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateCurve(
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(20, 500, Range.Inclusive),
                    It.IsInRange(10, 100, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.Curve(100, 100, 100, 100, 100, 100, 50);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("curve 100 100 100 100 100 100 50"));
        }

        [Test]
        public void CurveLimitsX1Test()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateCurve(
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Curve(600, 100, 100, 100, 100, 100, 50);
            var resultLow = pilot.Curve(-30, 100, 100, 100, 100, 100, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void CurveLimitsX2Test()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateCurve(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Curve(100, 100, 100, 600, 100, 100, 50);
            var resultLow = pilot.Curve(100, 100, 100, -30, 100, 100, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void CurveLimitsY1Test()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateCurve(
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Curve(100, 600, 100, 100, 100, 100, 50);
            var resultLow = pilot.Curve(100, -30, 100, 100, 100, 100, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void CurveLimitsY2Test()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateCurve(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Curve(100, 100, 100, 100, 600, 100, 50);
            var resultLow = pilot.Curve(100, 100, 100, 100, -30, 100, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void CurveLimitsZ1Test()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateCurve(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Curve(100, 100, 600, 100, 100, 100, 50);
            var resultLow = pilot.Curve(100, 100, -30, 100, 100, 100, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void CurveLimitsZ2Test()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateCurve(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 20 || x > 500),
                    It.IsAny<int>()))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Curve(100, 100, 100, 100, 100, 600, 50);
            var resultLow = pilot.Curve(100, 100, 100, 100, 100, -30, 50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void CurveLimitsSpeedTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateCurve(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<int>(x => x < 10 || x > 100)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.Curve(100, 100, 100, 100, 100, 100, 150);
            var resultLow = pilot.Curve(100, 100, 100, 100, 100, 100, -50);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void SetSpeedTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateSetSpeed(It.IsInRange(10, 100, Range.Inclusive)))
                .Returns(okResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.SetSpeed(90);

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("speed 90"));
        }

        [Test]
        public void SetSpeedLimitsTest()
        {
            var commandClient = moqCommandClient.Object;
            moqValidationService
                .Setup(v => v.ValidateSetSpeed(It.Is<int>(x => x < 10 || x > 100)))
                .Returns(errorResult);
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var resultHigh = pilot.SetSpeed(600);
            var resultLow = pilot.SetSpeed(-30);

            Assert.That(resultHigh.Succeeded == false);
            Assert.That(resultHigh.Message, Is.EqualTo("error"));
            Assert.That(resultLow.Succeeded == false);
            Assert.That(resultLow.Message, Is.EqualTo("error"));
        }

        [Test]
        public void SetWiFiTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.SetWiFi("ssid", "pass");

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("wifi ssid pass"));
        }

        [Test]
        public void SetAccessPointTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.SetAccessPoint("ssid", "pass");

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("ap ssid pass"));
        }

        [Test]
        public void GetSpeedTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.GetSpeed();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("speed?"));
        }

        [Test]
        public void GetWiFiTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.GetWiFi();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("wifi?"));
        }

        [Test]
        public void GetBatteryTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.GetBattery();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("battery?"));
        }

        [Test]
        public void GetTimeTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.GetTime();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("time?"));
        }

        [Test]
        public void GetSdkTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.GetSdk();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("sdk?"));
        }

        [Test]
        public void GetSerialNumberTest()
        {
            var commandClient = moqCommandClient.Object;
            var validationService = moqValidationService.Object;

            IPilot pilot = new Pilot(commandClient, validationService);
            var result = pilot.GetSerialNumber();

            Assert.That(result.Succeeded);
            Assert.That(result.Message, Is.EqualTo("sn?"));
        }
    }
}