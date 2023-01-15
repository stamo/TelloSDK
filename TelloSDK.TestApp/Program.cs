using Microsoft.Extensions.DependencyInjection;
using TelloSDK.Contracts;
using TelloSDK.Pilot.Contracts;

// Building IoC container
IServiceProvider provider = new ServiceCollection()
    .AddTelloSDKPilot()
    .BuildServiceProvider();

// Get Tello SDK services
var flightPlan = provider.GetService<IFlightPlan>();
var repl = provider.GetService<IREPLService>();
var telloPilot = provider.GetService<IPilot>();

if (flightPlan != null)
{
    TestFlightPlan(flightPlan);
}

//if (repl != null)
//{
//    TestRepl(repl);
//}

//if (telloPilot != null)
//{
//    TestTelloPilot(telloPilot);
//}

// The drone will fly according to
// predefind flight plan
void TestFlightPlan(IFlightPlan flightPlan)
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

// User can input commands in real time
// to controll the drone
void TestRepl(IREPLService repl)
{
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

    // Take the drone in air
    result = telloPilot.TakeOff();
    Console.WriteLine(result.Message);

    // Fly back 50 cm
    result = telloPilot.Backward(50);
    Console.WriteLine(result.Message);

    // Ascend 50 cm
    result = telloPilot.Up(50);
    Console.WriteLine(result.Message);

    // Make a flir to the right
    result = telloPilot.Flip(TelloSDK.Enumerations.Direction.Right);
    Console.WriteLine(result.Message);

    // Fly forward 150 cm
    result = telloPilot.Forward(150);
    Console.WriteLine(result.Message);

    // Land
    result = telloPilot.Land();
    Console.WriteLine(result.Message);

    // Destroy client
    telloPilot.EnginesOff();
}
