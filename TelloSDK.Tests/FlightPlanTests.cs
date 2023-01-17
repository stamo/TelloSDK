using Moq;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Services;

namespace TelloSDK.Tests
{
    public class FlightPlanTests
    {
        private readonly Mock<ITelloValidationService> moqValidationService = new Mock<ITelloValidationService>();
        private readonly Mock<ITelloCommandClient> moqCommandClient = new Mock<ITelloCommandClient>();

        [Test]
        public void BackwardTest()
        {
            var flightPlan = new FlightPlan(moqValidationService.Object, moqCommandClient.Object);
            var result = flightPlan.Backward(100);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. back 100{Environment.NewLine}"));
        }

        [Test]
        public void CurveTest()
        {
            var flightPlan = new FlightPlan(moqValidationService.Object, moqCommandClient.Object);
            var result = flightPlan.Curve(100, 100, 100, 100, 100, 100, 50);

            Assert.That(result, Is.EqualTo(flightPlan));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToString(), Is.EqualTo($"1. curve 100 100 100 100 100 100 50{Environment.NewLine}"));
        }
    }
}
