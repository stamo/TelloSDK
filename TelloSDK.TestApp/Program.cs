using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading.Channels;
using TelloSDK.Contracts;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Exceptions;
using TelloSDK.Telemetry.Contracts;
using TelloSDK.Telemetry.Models;

// Building IoC container
IServiceProvider provider = new ServiceCollection()
    .AddLogging()
    .AddTelloSDKPilot()
    //.AddTelloSDKTelemetry()
    .BuildServiceProvider();

// Get Tello SDK services
using var flightPlan = provider.GetService<IFlightPlan>();
using var repl = provider.GetService<IREPLService>();
using var telloPilot = provider.GetService<IPilot>();
var telemetryService = provider.GetService<ITelemetryListener>();

//if (flightPlan != null)
//{
//    TestFlightPlan(flightPlan);
//}

if (repl != null)
{
    TestRepl(repl);
}

//if (telloPilot != null)
//{
//    TestTelloPilot(telloPilot);
//}

// The drone will fly according to
// predefind flight plan
void TestFlightPlan(IFlightPlan flightPlan)
{
    try
    {
        flightPlan
            .TakeOff()
            .Backward(50)
            .Up(50)
            .Flip(TelloSDK.Enumerations.Direction.Back)
            .Forward(150)
            .TurnClockwise(90)
            .Right(150)
            .Land()
            .Validate()
            .Execute((log) => Console.WriteLine(log));
    }
    catch (FlightPlanValidationException fe)
    {
        Console.WriteLine(fe.Message);
    }
    
}

// User can input commands in real time
// to controll the drone
void TestRepl(IREPLService repl)
{
    Console.WriteLine("Tello REPL console:");
    string command = Console.ReadLine() ?? "quit";

    while (command != "quit")
    {
        Console.WriteLine(repl.ExecuteCommand(command));

        command = Console.ReadLine() ?? "quit";
    }
}

// Testing IPilot SDK Interface
// You can run commands according to
// user iteractions or program flow
void TestTelloPilot(IPilot telloPilot)
{
    // Initialize client
    var result = telloPilot.Ignition();
    Console.WriteLine(result.Message);

    result = telloPilot.GetBattery();
    Console.WriteLine(result.Message);

    // Take the drone in air
    result = telloPilot.TakeOff();
    Console.WriteLine(result.Message);

    // Fly back 50 cm
    result = telloPilot.Backward(50);
    Console.WriteLine(result.Message);

    // Ascend 50 cm
    result = telloPilot.Up(50);
    Console.WriteLine(result.Message);

    // Make a flip to the right
    result = telloPilot.Flip(TelloSDK.Enumerations.Direction.Right);
    Console.WriteLine(result.Message);

    // Fly forward 100 cm
    result = telloPilot.Forward(100);
    Console.WriteLine(result.Message);

    // Fly forward 100 cm
    result = telloPilot.TurnClockwise(360);
    Console.WriteLine(result.Message);

    // Land
    result = telloPilot.Land();
    Console.WriteLine(result.Message);

    // Destroy client
    telloPilot.EnginesOff();
}