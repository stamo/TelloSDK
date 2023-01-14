using Microsoft.Extensions.DependencyInjection;
using TelloSDK.Pilot.Contracts;

// Building IoC container
IServiceProvider provider = new ServiceCollection()
    .AddTelloSDKPilot()
    .BuildServiceProvider();

// Get FlightPlan service
var flightPlan = provider.GetService<IFlightPlan>();

if (flightPlan != null)
{
    flightPlan
        .TakeOff()
        .Backward(50)
        .Up(50)
        .Flip(TelloSDK.Enumerations.Direction.Back)
        .Forward(50)
        .Right(50)
        .Land()
        .Validate()
        .Execute();
}
