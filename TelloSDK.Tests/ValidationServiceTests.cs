using TelloSDK.Infrastructure.Constants;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Services;

namespace TelloSDK.Tests
{
    public class ValidationServiceTests
    {

        private ITelloValidationService validationService;

        [SetUp]
        public void Setup()
        {
            validationService = new TelloValidationService();
        }

        [Test]
        public void ValidateBackwardTest()
        {
            var result = validationService.ValidateBackward(100);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateBackwardLimitTest()
        {
            var resultLow = validationService.ValidateBackward(-20);
            var resultHigh = validationService.ValidateBackward(600);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Distance must be between 20 and 500"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Distance must be between 20 and 500"));
        }

        [Test]
        public void ValidateCurveTest() 
        {
            var result = validationService.ValidateCurve(100, 100, 100, 100, 100, 100, 50);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateCurveX1LimitTest()
        {
            var resultLow = validationService.ValidateCurve(-30, 100, 100, 100, 100, 100, 50);
            var resultHigh = validationService.ValidateCurve(600, 100, 100, 100, 100, 100, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"X Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"X Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateCurveY1LimitTest()
        {
            var resultLow = validationService.ValidateCurve(100, -30, 100, 100, 100, 100, 50);
            var resultHigh = validationService.ValidateCurve(100, 600, 100, 100, 100, 100, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"Y Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"Y Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateCurveZ1LimitTest()
        {
            var resultLow = validationService.ValidateCurve(100, 100, -30, 100, 100, 100, 50);
            var resultHigh = validationService.ValidateCurve(100, 100, 600, 100, 100, 100, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"Z Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"Z Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateCurveX2LimitTest()
        {
            var resultLow = validationService.ValidateCurve(100, 100, 100, -30, 100, 100, 50);
            var resultHigh = validationService.ValidateCurve(100, 100, 100, 600, 100, 100, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"X Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"X Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateCurveY2LimitTest()
        {
            var resultLow = validationService.ValidateCurve(100, 100, 100, 100, -30, 100, 50);
            var resultHigh = validationService.ValidateCurve(100, 100, 100, 100, 600, 100, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"Y Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"Y Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateCurveZ2LimitTest()
        {
            var resultLow = validationService.ValidateCurve(100, 100, 100, 100, 100, -30, 50);
            var resultHigh = validationService.ValidateCurve(100, 100, 100, 100, 100, 600, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"Z Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"Z Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateCurveSpeedLimitTest()
        {
            var resultLow = validationService.ValidateCurve(100, 100, 100, 100, 100, 100, -50);
            var resultHigh = validationService.ValidateCurve(100, 100, 100, 100, 100, 100, 150);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"Speed must be between 10 and 60{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"Speed must be between 10 and 60{Environment.NewLine}"));
        }

        [Test]
        public void ValidateCurveDimensionsLimitTest()
        {
            var resultFirst = validationService.ValidateCurve(20, 20, 20, 100, 100, 100, 50);
            var resultSecond = validationService.ValidateCurve(100, 100, 100, 20, 20, 20, 50);

            Assert.That(resultFirst.Succeeded, Is.False);
            Assert.That(resultFirst.Message, Is.EqualTo($"x, y and z values can’t be set equal to 20 simultaneously{Environment.NewLine}"));
            Assert.That(resultSecond.Succeeded, Is.False);
            Assert.That(resultSecond.Message, Is.EqualTo($"x, y and z values can’t be set equal to 20 simultaneously{Environment.NewLine}"));
        }

        [Test]
        public void ValidateDownTest()
        {
            var result = validationService.ValidateDown(100);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateDownLimitTest()
        {
            var resultLow = validationService.ValidateDown(-20);
            var resultHigh = validationService.ValidateDown(600);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Distance must be between 20 and 500"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Distance must be between 20 and 500"));
        }

        [Test]
        public void ValidateForwardTest()
        {
            var result = validationService.ValidateForward(100);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateForwardLimitTest()
        {
            var resultLow = validationService.ValidateForward(-20);
            var resultHigh = validationService.ValidateForward(600);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Distance must be between 20 and 500"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Distance must be between 20 and 500"));
        }

        [Test]
        public void ValidateGoTest()
        {
            var result = validationService.ValidateGo(100, 100, 100, 50);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateGoXLimitTest()
        {
            var resultLow = validationService.ValidateGo(-30, 100, 100, 50);
            var resultHigh = validationService.ValidateGo(600, 100, 100, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"X Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"X Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateGoYLimitTest()
        {
            var resultLow = validationService.ValidateGo(100, -30, 100, 50);
            var resultHigh = validationService.ValidateGo(100, 600, 100, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"Y Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"Y Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateGoZLimitTest()
        {
            var resultLow = validationService.ValidateGo(100, 100, -30, 50);
            var resultHigh = validationService.ValidateGo(100, 100, 600, 50);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"Z Dimension must be between 20 and 500{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"Z Dimension must be between 20 and 500{Environment.NewLine}"));
        }

        [Test]
        public void ValidateGoSpeedLimitTest()
        {
            var resultLow = validationService.ValidateGo(100, 100, 100, -50);
            var resultHigh = validationService.ValidateGo(100, 100, 100, 150);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo($"Speed must be between 10 and 100{Environment.NewLine}"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo($"Speed must be between 10 and 100{Environment.NewLine}"));
        }

        [Test]
        public void ValidateGoDimensionsLimitTest()
        {
            var result = validationService.ValidateGo(20, 20, 20, 50);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Message, Is.EqualTo($"x, y and z values can’t be set equal to 20 simultaneously{Environment.NewLine}"));
        }

        [Test]
        public void ValidateLeftTest()
        {
            var result = validationService.ValidateLeft(100);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateLeftLimitTest()
        {
            var resultLow = validationService.ValidateLeft(-20);
            var resultHigh = validationService.ValidateLeft(600);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Distance must be between 20 and 500"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Distance must be between 20 and 500"));
        }

        [Test]
        public void ValidateRightTest()
        {
            var result = validationService.ValidateRight(100);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateRightLimitTest()
        {
            var resultLow = validationService.ValidateRight(-20);
            var resultHigh = validationService.ValidateRight(600);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Distance must be between 20 and 500"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Distance must be between 20 and 500"));
        }

        [Test]
        public void ValidateSetSpeedTest()
        {
            var result = validationService.ValidateSetSpeed(50);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateSetSpeedLimitTest()
        {
            var resultLow = validationService.ValidateSetSpeed(-20);
            var resultHigh = validationService.ValidateSetSpeed(150);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Speed must be between 10 and 100"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Speed must be between 10 and 100"));
        }

        [Test]
        public void ValidateTurnClockwiseTest()
        {
            var result = validationService.ValidateTurnClockwise(90);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateTurnClockwiseLimitTest()
        {
            var resultLow = validationService.ValidateTurnClockwise(-20);
            var resultHigh = validationService.ValidateTurnClockwise(450);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Degrees must be between 1 and 360"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Degrees must be between 1 and 360"));
        }

        [Test]
        public void ValidateTurnCounterClockwiseTest()
        {
            var result = validationService.ValidateTurnCounterClockwise(90);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateTurnCounterClockwiseLimitTest()
        {
            var resultLow = validationService.ValidateTurnCounterClockwise(-20);
            var resultHigh = validationService.ValidateTurnCounterClockwise(450);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Degrees must be between 1 and 360"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Degrees must be between 1 and 360"));
        }

        [Test]
        public void ValidateUpTest()
        {
            var result = validationService.ValidateUp(100);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Message, Is.EqualTo(TelloResponse.Success));
        }

        [Test]
        public void ValidateUpLimitTest()
        {
            var resultLow = validationService.ValidateUp(-20);
            var resultHigh = validationService.ValidateUp(600);

            Assert.That(resultLow.Succeeded, Is.False);
            Assert.That(resultLow.Message, Is.EqualTo("Distance must be between 20 and 500"));
            Assert.That(resultHigh.Succeeded, Is.False);
            Assert.That(resultHigh.Message, Is.EqualTo("Distance must be between 20 and 500"));
        }
    }
}
