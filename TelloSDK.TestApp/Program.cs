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

void TestRepl(IREPLService repl)
{
    string command = Console.ReadLine() ?? "quit";

    while (command != "quit")
    {
        Console.WriteLine(repl.ExecuteCommand(command));

        command = Console.ReadLine() ?? "quit";
    }
}

void TestTelloPilot(IPilot telloPilot)
{
    var result = telloPilot.Ignition();
    Console.WriteLine(result.Message);
    result = telloPilot.TakeOff();
    Console.WriteLine(result.Message);
    result = telloPilot.Backward(50);
    Console.WriteLine(result.Message);
    result = telloPilot.Up(50);
    Console.WriteLine(result.Message);
    result = telloPilot.Flip(TelloSDK.Enumerations.Direction.Right);
    Console.WriteLine(result.Message);
    result = telloPilot.Forward(150);
    Console.WriteLine(result.Message);
    result = telloPilot.Land();
    Console.WriteLine(result.Message);
    telloPilot.EnginesOff();
}
