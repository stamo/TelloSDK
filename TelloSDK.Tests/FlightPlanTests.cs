using Moq;
using TelloSDK.Infrastructure.Constants;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Exceptions;
using TelloSDK.Pilot.Services;

namespace TelloSDK.Tests
{
    public class FlightPlanTests 
    {
        private Mock<ITelloValidationService> moqValidationService;
        private Mock<ITelloCommandClient> moqCommandClient;
        private FlightPlan flightPlan;

        [SetUp] 
        public void SetUp() 
        {
            moqValidationService = new Mock<ITelloValidationService>();
            moqCommandClient = new Mock<ITelloCommandClient>();
            flightPlan = new FlightPlan(moqValidationService.Object, moqCommandClient.Object);
        }

        [Test]
        public void BackwardTest()
        {
            var result = flightPlan.Backward(100);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. back 100{Environment.NewLine}"));
        }

        [Test]
        public void CurveTest()
        {
            var result = flightPlan.Curve(100, 100, 100, 100, 100, 100, 50);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. curve 100 100 100 100 100 100 50{Environment.NewLine}"));
        }

        [Test]
        public void DownTest()
        {
            var result = flightPlan.Down(100);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. down 100{Environment.NewLine}"));
        }

        [Test]
        public void EmergencyTest()
        {
            var result = flightPlan.Emergency();

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. emergency{Environment.NewLine}"));
        }

        [Test]
        public void ExecuteTest() 
        {
            TelloActionResult result = new TelloActionResult() 
            { 
                Succeeded = true, 
                Message = TelloResponse.Success 
            };

            moqCommandClient
                .Setup(c => c.InitializeCommandSDK())
                .Returns(result);
            moqCommandClient
                .Setup(c => c.ExecuteCommand(It.IsAny<string>()))
                .Returns(TelloResponse.Success);
            moqCommandClient
                .Setup(c => c.DisconnectCommandSDK());

            flightPlan = new FlightPlan(moqValidationService.Object, moqCommandClient.Object);
            flightPlan.Backward(100);

            Assert.That(flightPlan.Count, Is.EqualTo(1));

            flightPlan.Execute();

            Assert.That(flightPlan.Count, Is.EqualTo(0));
        }

        [Test]
        public void FlipTest()
        {
            var result = flightPlan.Flip(Enumerations.Direction.Back);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. flip b{Environment.NewLine}"));
        }

        [Test]
        public void ForwardTest()
        {
            var result = flightPlan.Forward(100);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. forward 100{Environment.NewLine}"));
        }

        [Test]
        public void GoTest()
        {
            var result = flightPlan.Go(100, 100, 100, 50);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. go 100 100 100 50{Environment.NewLine}"));
        }

        [Test]
        public void LandTest()
        {
            var result = flightPlan.Land();

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. land{Environment.NewLine}"));
        }

        [Test]
        public void LeftTest()
        {
            var result = flightPlan.Left(100);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. left 100{Environment.NewLine}"));
        }

        [Test]
        public void RightTest()
        {
            var result = flightPlan.Right(100);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. right 100{Environment.NewLine}"));
        }

        [Test]
        public void StopTest()
        {
            var result = flightPlan.Stop();

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. stop{Environment.NewLine}"));
        }

        [Test]
        public void StreamOffTest()
        {
            var result = flightPlan.StreamOff();

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. streamoff{Environment.NewLine}"));
        }

        [Test]
        public void StreamOnTest()
        {
            var result = flightPlan.StreamOn();

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. streamon{Environment.NewLine}"));
        }

        [Test]
        public void TakeOffTest()
        {
            var result = flightPlan.TakeOff();

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. takeoff{Environment.NewLine}"));
        }

        [Test]
        public void TurnClocwiseTest()
        {
            var result = flightPlan.TurnClockwise(90);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. cw 90{Environment.NewLine}"));
        }

        [Test]
        public void TurnCounterClocwiseTest()
        {
            var result = flightPlan.TurnCounterClockwise(90);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. ccw 90{Environment.NewLine}"));
        }

        [Test]
        public void UpTest()
        {
            var result = flightPlan.Up(100);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. up 100{Environment.NewLine}"));
        }

        [Test]
        public void ValidateNoValidationNeededTest()
        {
            var result = flightPlan
                .TakeOff()
                .Validate();

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. takeoff{Environment.NewLine}"));
        }

        [Test]
        public void ValidateSuccessfullValidationTest()
        {
            TelloActionResult successResult = new TelloActionResult()
            {
                Succeeded = true,
                Message = TelloResponse.Success
            };

            moqValidationService
                .Setup(v => v.ValidateUp(It.IsAny<int>()))
                .Returns(successResult);

            flightPlan = new FlightPlan(moqValidationService.Object, moqCommandClient.Object);

            var result = flightPlan
                .Up(100)
                .Validate();

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. up 100{Environment.NewLine}"));
        }

        [Test]
        public void ValidateUnsuccessfullValidationTest()
        {
            TelloActionResult unsuccessResult = new TelloActionResult()
            {
                Succeeded = false,
                Message = "Distance must be between 20 and 500"
            };

            moqValidationService
                .Setup(v => v.ValidateUp(It.IsAny<int>()))
                .Returns(unsuccessResult);

            flightPlan = new FlightPlan(moqValidationService.Object, moqCommandClient.Object);
            FlightPlanValidationException ex = Assert
                .Throws<FlightPlanValidationException>(() => flightPlan.Up(600).Validate());

            Assert.That(ex.Message, Is.EqualTo($"1. Distance must be between 20 and 500{Environment.NewLine}"));
        }
    }
}
