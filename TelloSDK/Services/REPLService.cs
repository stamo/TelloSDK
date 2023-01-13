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
            else if (parameters.Length - 1 != commands[parameters[0]].NumberOfParams)
            {
                result = REPLErrorMessages.InvalidParameters;
            }
            else
            {
                string methodName = commands[parameters[0]].Method;
                object[] args = new object[parameters.Length -1];

                for (int i = 1; i < parameters.Length; i++)
                {
                    args[i - 1] = parameters[i];
                }

                if (methodName == nameof(pilot.Flip))
                {
                    args[0] = GetDirection(args[0].ToString());
                }

                MethodInfo methodInfo = pilot
                    .GetType()
                    .GetMethod(methodName);

                if (methodInfo != null) 
                {
                    result = REPLErrorMessages.UnknownCommand;
                }
                else
                {
                    var commandResult = (TelloActionResult)methodInfo.Invoke(pilot, args);
                    result = commandResult.Message ?? string.Empty;
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
            AddCommand("init", nameof(pilot.Ignition), 0);
            AddCommand("takeoff", nameof(pilot.TakeOff), 0);
            AddCommand("land", nameof(pilot.Land), 0);
            AddCommand("streamon", nameof(pilot.StreamOn), 0);
            AddCommand("streamoff", nameof(pilot.StreamOff), 0);
            AddCommand("emergency", nameof(pilot.Emergency), 0);
            AddCommand("up", nameof(pilot.Up), 1);
            AddCommand("down", nameof(pilot.Down), 1);
            AddCommand("left", nameof(pilot.Left), 1);
            AddCommand("right", nameof(pilot.Right), 1);
            AddCommand("forward", nameof(pilot.Forward), 1);
            AddCommand("back", nameof(pilot.Backward), 1);
            AddCommand("cw", nameof(pilot.TurnClockwise), 1);
            AddCommand("ccw", nameof(pilot.TurnCounterClockwise), 1);
            AddCommand("flip", nameof(pilot.Flip), 1);
            AddCommand("go", nameof(pilot.Go), 4);
            AddCommand("stop", nameof(pilot.Stop), 0);
            AddCommand("curve", nameof(pilot.Curve), 7);
            AddCommand("speed", nameof(pilot.SetSpeed), 1);
            AddCommand("wifi", nameof(pilot.SetWiFi), 2);
            AddCommand("ap", nameof(pilot.SetAccessPoint), 2);
            AddCommand("speed?", nameof(pilot.GetSpeed), 0);
            AddCommand("battery?", nameof(pilot.GetBattery), 0);
            AddCommand("time?", nameof(pilot.GetTime), 0);
            AddCommand("wifi?", nameof(pilot.GetWiFi), 0);
            AddCommand("sdk?", nameof(pilot.GetSdk), 0);
            AddCommand("sn?", nameof(pilot.GetSerialNumber), 0);
            AddCommand("quit", nameof(pilot.EnginesOff), 0);
        }

        /// <summary>
        /// Adds command to commands collection
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="method">Method name</param>
        /// <param name="paramsCount">Number of parameters</param>
        private void AddCommand(string name, string method, int paramsCount)
        {
            if (commands.ContainsKey(name) == false) 
            {
                commands.Add(name, new REPLCommand()
                {
                    Command = name,
                    Method = method,
                    NumberOfParams = paramsCount
                });
            }
        }
    }
}
