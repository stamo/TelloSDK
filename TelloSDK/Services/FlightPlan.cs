using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TelloSDK.Enumerations;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Exceptions;
using TelloSDK.Pilot.Models;
using static TelloSDK.Pilot.Constants.TelloSDKCommands;

namespace TelloSDK.Pilot.Services
{
    /// <summary>
    /// Flight plan
    /// </summary>
    public class FlightPlan : IFlightPlan
    {
        /// <summary>
        /// Tello SDK Validation service
        /// </summary>
        private readonly ITelloValidationService validationService;

        /// <summary>
        /// Tello SDK command client
        /// </summary>
        private readonly ITelloCommandClient commandClient;

        /// <summary>
        /// List of commands in flight plan
        /// </summary>
        private readonly List<FlightPlanCommand> commands = new List<FlightPlanCommand>();

        /// <summary>
        /// Create flight plan object
        /// </summary>
        /// <param name="_validationService">Tello SDK validation service</param>
        /// <param name="_commandClient">Tello SDK command client</param>
        public FlightPlan(
            ITelloValidationService _validationService,
            ITelloCommandClient _commandClient)
        {
            validationService = _validationService;
            commandClient = _commandClient;
        }

        /// <summary>
        /// Fly backward for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public IFlightPlan Backward(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Back, parameters), 
                nameof(validationService.ValidateBackward), 
                parameters);

            return this;
        }

        /// <summary>
        /// Fly at a curve according to the two given 
        /// coordinates at {speed} (cm/s)
        /// </summary>
        /// <param name="x1">Position X, range(-500, 500)</param>
        /// <param name="y1">Position Y, range(-500, 500)</param>
        /// <param name="z1">Position Z, range(-500, 500)</param>
        /// <param name="x2">Position X, range(-500, 500)</param>
        /// <param name="y2">Position Y, range(-500, 500)</param>
        /// <param name="z2">Position Z, range(-500, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 60)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be set between - 20 and 20 simultaneously</remarks>
        public IFlightPlan Curve(int x1, int y1, int z1, int x2, int y2, int z2, int speed)
        {
            object[] parameters = { x1, y1, z1, x2, y2, z2, speed };
            AddCommand(
                string.Format(ControlCommands.Curve, parameters),
                nameof(validationService.ValidateCurve),
                parameters);

            return this;
        }

        /// <summary>
        ///  Descend to {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public IFlightPlan Down(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Down, parameters),
                nameof(validationService.ValidateDown),
                parameters);

            return this;
        }

        /// <summary>
        /// Stop motors immediately
        /// </summary>
        public IFlightPlan Emergency()
        {
            AddCommand(ControlCommands.Emergency);

            return this;
        }

        /// <summary>
        /// Executes flight plan
        /// </summary>
        public void Execute()
        {
            commandClient.InitializeCommandSDK();

            foreach (var action in commands)
            {
                commandClient.ExecuteCommand(action.Command);
            }

            commands.Clear();
            commandClient.DisconnectCommandSDK();
        }

        /// <summary>
        /// Flip in {direction} direction
        /// </summary>
        /// <param name="direction">Direction to flip in</param>
        public IFlightPlan Flip(Direction direction)
        {
            AddCommand(string.Format(ControlCommands.Flip, direction));

            return this;
        }

        /// <summary>
        /// Fly forward for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public IFlightPlan Forward(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Forward, parameters),
                nameof(validationService.ValidateForward),
                parameters);

            return this;
        }

        /// <summary>
        /// Fly to {x} {y} {z} at {speed} (cm/s)
        /// </summary>
        /// <param name="x">Position X, range(-500, 500)</param>
        /// <param name="y">Position Y, range(-500, 500)</param>
        /// <param name="z">Position Z, range(-500, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be set between - 20 and 20 simultaneously</remarks>
        public IFlightPlan Go(int x, int y, int z, int speed)
        {
            object[] parameters = { x, y, z, speed };
            AddCommand(
                string.Format(ControlCommands.Go, parameters),
                nameof(validationService.ValidateGo),
                parameters);

            return this;
        }

        /// <summary>
        /// Auto landing
        /// </summary>
        public IFlightPlan Land()
        {
            AddCommand(ControlCommands.Land);

            return this;
        }

        /// <summary>
        /// Fly left for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public IFlightPlan Left(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Left, parameters),
                nameof(validationService.ValidateLeft),
                parameters);

            return this;
        }

        /// <summary>
        /// Fly right for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public IFlightPlan Right(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Right, parameters),
                nameof(validationService.ValidateRight),
                parameters);

            return this;
        }

        /// <summary>
        /// Hovers in the air
        /// </summary>
        /// <remarks>Works at any time</remarks>
        public IFlightPlan Stop()
        {
            AddCommand(ControlCommands.Stop);

            return this;
        }

        /// <summary>
        /// Video stream OFF
        /// </summary>
        public IFlightPlan StreamOff()
        {
            AddCommand(ControlCommands.VideoStreamOff);

            return this;
        }

        /// <summary>
        /// Video stream ON
        /// </summary>
        public IFlightPlan StreamOn()
        {
            AddCommand(ControlCommands.VideoStreamOn);

            return this;
        }

        /// <summary>
        /// Auto takeoff
        /// </summary>
        public IFlightPlan TakeOff()
        {
            AddCommand(ControlCommands.TakeOff);

            return this;
        }

        /// <summary>
        /// Rotate {degrees} degrees clockwise
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        public IFlightPlan TurnClockwise(int degrees)
        {
            object[] parameters = { degrees };
            AddCommand(
                string.Format(ControlCommands.RotateClockwise, parameters),
                nameof(validationService.ValidateTurnClockwise),
                parameters);

            return this;
        }

        /// <summary>
        /// Rotate {degrees} degrees counterclockwise
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        public IFlightPlan TurnCounterClockwise(int degrees)
        {
            object[] parameters = { degrees };
            AddCommand(
                string.Format(ControlCommands.RotateCounterClockwise, parameters),
                nameof(validationService.ValidateTurnCounterClockwise),
                parameters);

            return this;
        }

        /// <summary>
        /// Ascend to {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public IFlightPlan Up(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Up, parameters),
                nameof(validationService.ValidateUp),
                parameters);

            return this;
        }

        /// <summary>
        /// Validates flight plan
        /// </summary>
        /// <returns></returns>
        public IFlightPlan Validate()
        {
            bool hasErrors = false;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].ValidationMethod == null)
                {
                    sb.AppendLine($"{i + 1}. OK");
                }
                else
                {
                    MethodInfo methodInfo = validationService
                    .GetType()
                    .GetMethod(commands[i].ValidationMethod);

                    var result = (TelloActionResult)methodInfo
                        .Invoke(validationService, commands[i].Parameters);

                    if (result.Succeeded == false)
                    {
                        hasErrors = true;
                        sb.AppendLine($"{i + 1}. {result.Message}");
                    }
                    else
                    {
                        sb.AppendLine($"{i + 1}. OK");
                    }
                }
            }

            if (hasErrors)
            {
                throw new FlightPlanValidationException(sb.ToString());
            }

            return this;
        }

        private void AddCommand(string command, string? validationMethod = null, object[]? parameters = null) 
        {
            commands.Add(new FlightPlanCommand() 
            {
                Command = command,
                ValidationMethod = validationMethod,
                Parameters = parameters
            });
        }
    }
}
