using System;
using System.Collections.Generic;
using System.Reflection;
using TelloSDK.Contracts;
using TelloSDK.Enumerations;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Models;
using static TelloSDK.Pilot.Constants.TelloSDKCommands;

namespace TelloSDK.Pilot.Services
{
    /// <summary>
    /// Service to use Tello REPL interface
    /// </summary>
    public class REPLService : IREPLService
    {
        /// <summary>
        /// Tello pilot
        /// </summary>
        private readonly IPilot pilot;

        /// <summary>
        /// Available commands
        /// </summary>
        private readonly Dictionary<string, REPLCommand> commands = new Dictionary<string, REPLCommand>();

        /// <summary>
        /// Create commands collection
        /// </summary>
        /// <param name="_pilot">Tello pilot</param>
        public REPLService(IPilot _pilot)
        {
            pilot= _pilot;
            LoadCommands();
        }

        /// <summary>
        /// REPL command executor
        /// </summary>
        /// <param name="command">Command to be executed</param>
        /// <returns></returns>
        public string ExecuteCommand(string command)
        {
            string result = string.Empty;
            string[] parameters = command.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length == 0 || 
                commands.ContainsKey(parameters[0]) == false)
            {
                result = REPLErrorMessages.UnknownCommand;
            }
            else if (parameters.Length - 1 != commands[parameters[0]].TypesParams.Length)
            {
                result = REPLErrorMessages.InvalidParameters;
            }
            else
            {
                try
                {
                    string methodName = commands[parameters[0]].Method;
                    object[] args = new object[parameters.Length - 1];

                    for (int i = 1; i < parameters.Length; i++)
                    {
                        args[i - 1] = Convert.ChangeType(parameters[i], commands[parameters[0]].TypesParams[i - 1]);
                    }

                    if (methodName == nameof(pilot.Flip))
                    {
                        args[0] = GetDirection(args[0].ToString());
                    }

                    MethodInfo methodInfo = pilot
                        .GetType()
                        .GetMethod(methodName);

                    if (methodInfo == null)
                    {
                        result = REPLErrorMessages.UnknownCommand;
                    }
                    else
                    {
                        var commandResult = (TelloActionResult)methodInfo.Invoke(pilot, args);
                        result = commandResult.Message ?? string.Empty;
                    }
                }
                catch (FormatException)
                {
                    result = REPLErrorMessages.InvalidParameters;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets flip direction
        /// </summary>
        /// <param name="direction">flip direction</param>
        /// <returns></returns>
        private object GetDirection(string direction) => direction switch
        {
            "l" => Direction.Left,
            "r" => Direction.Right,
            "f" => Direction.Forward,
            "b" => Direction.Back,
            _ => Direction.Forward
        };

        /// <summary>
        /// Loads all available commands
        /// </summary>
        private void LoadCommands()
        {
            AddCommand("init", nameof(pilot.Ignition), new Type[] { });
            AddCommand("takeoff", nameof(pilot.TakeOff), new Type[] { });
            AddCommand("land", nameof(pilot.Land), new Type[] { });
            AddCommand("streamon", nameof(pilot.StreamOn), new Type[] { });
            AddCommand("streamoff", nameof(pilot.StreamOff), new Type[] { });
            AddCommand("emergency", nameof(pilot.Emergency), new Type[] { });
            AddCommand("up", nameof(pilot.Up), new Type[] { typeof(int) });
            AddCommand("down", nameof(pilot.Down), new Type[] { typeof(int) });
            AddCommand("left", nameof(pilot.Left), new Type[] { typeof(int) });
            AddCommand("right", nameof(pilot.Right), new Type[] { typeof(int) });
            AddCommand("forward", nameof(pilot.Forward), new Type[] { typeof(int) });
            AddCommand("back", nameof(pilot.Backward), new Type[] { typeof(int) });
            AddCommand("cw", nameof(pilot.TurnClockwise), new Type[] { typeof(int) });
            AddCommand("ccw", nameof(pilot.TurnCounterClockwise), new Type[] { typeof(int) });
            AddCommand("flip", nameof(pilot.Flip), new Type[] { typeof(string) });
            AddCommand("go", nameof(pilot.Go), new Type[] 
            {
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int)
            });
            AddCommand("stop", nameof(pilot.Stop), new Type[] { });
            AddCommand("curve", nameof(pilot.Curve), new Type[]
            {
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int)
            });
            AddCommand("speed", nameof(pilot.SetSpeed), new Type[] { typeof(int) });
            AddCommand("wifi", nameof(pilot.SetWiFi), new Type[] { typeof(string), typeof(string) });
            AddCommand("ap", nameof(pilot.SetAccessPoint), new Type[] { typeof(string), typeof(string) });
            AddCommand("speed?", nameof(pilot.GetSpeed), new Type[] { });
            AddCommand("battery?", nameof(pilot.GetBattery), new Type[] { });
            AddCommand("time?", nameof(pilot.GetTime), new Type[] { });
            AddCommand("wifi?", nameof(pilot.GetWiFi), new Type[] { });
            AddCommand("sdk?", nameof(pilot.GetSdk), new Type[] { });
            AddCommand("sn?", nameof(pilot.GetSerialNumber), new Type[] { });
            AddCommand("quit", nameof(pilot.EnginesOff), new Type[] { });
        }

        /// <summary>
        /// Adds command to commands collection
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="method">Method name</param>
        /// <param name="typesOfParameters">Type of parameters</param>
        private void AddCommand(string name, string method, Type[] typesOfParameters)
        {
            if (commands.ContainsKey(name) == false) 
            {
                commands.Add(name, new REPLCommand()
                {
                    Command = name,
                    Method = method,
                    TypesParams = typesOfParameters
                });
            }
        }

        public void Dispose()
        {
            pilot.Dispose();
        }
    }
}
