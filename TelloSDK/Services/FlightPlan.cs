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
    public class FlightPlan : IFlightPlan
    {
        private readonly ITelloValidationService validationService;

        private readonly ITelloCommandClient commandClient;

        private readonly List<FlightPlanCommand> commands = new List<FlightPlanCommand>();

        public FlightPlan(
            ITelloValidationService _validationService,
            ITelloCommandClient _commandClient)
        {
            validationService = _validationService;
            commandClient = _commandClient;
        }

        public IFlightPlan Backward(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Back, parameters), 
                nameof(validationService.ValidateBackward), 
                parameters);

            return this;
        }

        public IFlightPlan Curve(int x1, int y1, int z1, int x2, int y2, int z2, int speed)
        {
            object[] parameters = { x1, y1, z1, x2, y2, z2, speed };
            AddCommand(
                string.Format(ControlCommands.Curve, parameters),
                nameof(validationService.ValidateCurve),
                parameters);

            return this;
        }

        public IFlightPlan Down(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Down, parameters),
                nameof(validationService.ValidateDown),
                parameters);

            return this;
        }

        public IFlightPlan Emergency()
        {
            AddCommand(ControlCommands.Emergency);

            return this;
        }

        public void Execute()
        {
            commandClient.InitializeCommandSDK();

            foreach (var action in commands)
            {
                commandClient.ExecuteCommand(action.Command);
            }

            commandClient.DisconnectCommandSDK();
        }

        public IFlightPlan Flip(Direction direction)
        {
            AddCommand(string.Format(ControlCommands.Flip, direction));

            return this;
        }

        public IFlightPlan Forward(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Forward, parameters),
                nameof(validationService.ValidateForward),
                parameters);

            return this;
        }

        public IFlightPlan Go(int x, int y, int z, int speed)
        {
            object[] parameters = { x, y, z, speed };
            AddCommand(
                string.Format(ControlCommands.Go, parameters),
                nameof(validationService.ValidateGo),
                parameters);

            return this;
        }

        public IFlightPlan Land()
        {
            AddCommand(ControlCommands.Land);

            return this;
        }

        public IFlightPlan Left(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Left, parameters),
                nameof(validationService.ValidateLeft),
                parameters);

            return this;
        }

        public IFlightPlan Right(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Right, parameters),
                nameof(validationService.ValidateRight),
                parameters);

            return this;
        }

        public IFlightPlan Stop()
        {
            AddCommand(ControlCommands.Stop);

            return this;
        }

        public IFlightPlan StreamOff()
        {
            AddCommand(ControlCommands.VideoStreamOff);

            return this;
        }

        public IFlightPlan StreamOn()
        {
            AddCommand(ControlCommands.VideoStreamOn);

            return this;
        }

        public IFlightPlan TakeOff()
        {
            AddCommand(ControlCommands.TakeOff);

            return this;
        }

        public IFlightPlan TurnClockwise(int degrees)
        {
            object[] parameters = { degrees };
            AddCommand(
                string.Format(ControlCommands.RotateClockwise, parameters),
                nameof(validationService.ValidateTurnClockwise),
                parameters);

            return this;
        }

        public IFlightPlan TurnCounterClockwise(int degrees)
        {
            object[] parameters = { degrees };
            AddCommand(
                string.Format(ControlCommands.RotateCounterClockwise, parameters),
                nameof(validationService.ValidateTurnCounterClockwise),
                parameters);

            return this;
        }

        public IFlightPlan Up(int distance)
        {
            object[] parameters = { distance };
            AddCommand(
                string.Format(ControlCommands.Up, parameters),
                nameof(validationService.ValidateUp),
                parameters);

            return this;
        }

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
