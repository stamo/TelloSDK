using System.Text;
using TelloSDK.Contracts;
using TelloSDK.Enumerations;
using TelloSDK.Infrastructure.Constants;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using static TelloSDK.Pilot.Constants.TelloSDKCommands;

namespace TelloSDK.Services
{
    /// <summary>
    /// Tello pilot
    /// </summary>
    public class Pilot : IPilot
    {
        /// <summary>
        /// SDK Client to comunicate with drone
        /// </summary>
        private readonly ITelloCommandClient commandClient;

        /// <summary>
        /// Tello pilot
        /// </summary>
        /// <param name="_commandClient">Client to execute commands</param>
        public Pilot(ITelloCommandClient _commandClient)
        {
            commandClient = _commandClient;
        }

        /// <summary>
        /// Fly backward for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Backward(int distance)
        {
            if (distance > 500 || distance < 20)
            {
                return new TelloActionResult(
                    false,
                    string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500));
            };

            return ExecuteAction(string.Format(ControlCommands.Back, distance));
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
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Curve(int x1, int y1, int z1, int x2, int y2, int z2, int speed)
        {
            var result = CreateResult(true);
            result.Message = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (x1 > 500 || x1 < -500 || x2 > 500 || x2 < -500)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.XDimensionOutOfRange, -500, 500));
            };

            if (y1 > 500 || y1 < -500 || y2 > 500 || y2 < -500)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.YDimensionOutOfRange, -500, 500));
            };

            if (z1 > 500 || z1 < -500 || z2 > 500 || z2 < -500)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.ZDimensionOutOfRange, -500, 500));
            };

            if (speed > 100 || speed < 10)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.SpeedOutOfRange, 10, 100));
            };

            if (((x1 > -20 && x1 < 20) &&
                (y1 > -20 && y1 < 20) &&
                (z1 > -20 && z1 < 20)) ||
                ((x2 > -20 && x2 < 20) &&
                (y2 > -20 && y2 < 20) &&
                (z2 > -20 && z2 < 20)))
            {
                result.Succeeded = false;
                sb.AppendLine(CommandsErrorMessages.InvalidDimensions);
            }

            if (result.Succeeded)
            {
                result = ExecuteAction(string
                    .Format(ControlCommands.Curve, x1, y1, z1, x2, y2, z2, speed));
            }
            else
            {
                result.Message = sb.ToString();
            }

            return result;
        }

        /// <summary>
        ///  Descend to {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Down(int distance)
        {
            if (distance > 500 || distance < 20)
            {
                return new TelloActionResult(
                    false,
                    string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500));
            };

            return ExecuteAction(string.Format(ControlCommands.Down, distance));
        }

        /// <summary>
        /// Stop motors immediately
        /// </summary>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult Emergency()
        {
            return ExecuteAction(ControlCommands.Emergency);
        }

        /// <summary>
        /// Disconnects from drone
        /// </summary>
        public void EnginesOff()
        {
            commandClient.DisconnectCommandSDK();
        }

        /// <summary>
        /// Flip in {direction} direction
        /// </summary>
        /// <param name="direction">Direction to flip in</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Flip(Direction direction)
        {
            return ExecuteAction(string.Format(
                ControlCommands.Flip, 
                ControlCommands.GetFlipDirection(direction)));
        }

        /// <summary>
        /// Fly forward for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Forward(int distance)
        {
            if (distance > 500 || distance < 20)
            {
                return new TelloActionResult(
                    false,
                    string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500));
            };

            return ExecuteAction(string.Format(ControlCommands.Forward, distance));
        }

        /// <summary>
        /// Obtain current battery percentage
        /// </summary>
        /// <returns>range(0, 100)</returns>
        public TelloActionResult GetBattery()
        {
            var result = CreateResult(true);
            result.Message = commandClient.ExecuteCommand(ReadCommands.GetBattery);

            return result;
        }

        /// <summary>
        /// Obtain the Tello SDK version
        /// </summary>
        /// <returns>SDK version</returns>
        public TelloActionResult GetSdk()
        {
            var result = CreateResult(true);
            result.Message = commandClient.ExecuteCommand(ReadCommands.GetSDKVersion);

            return result;
        }

        /// <summary>
        /// Obtain the Tello serial number
        /// </summary>
        /// <returns>serial number</returns>
        public TelloActionResult GetSerialNumber()
        {
            var result = CreateResult(true);
            result.Message = commandClient.ExecuteCommand(ReadCommands.GetSerialNumber);

            return result;
        }

        /// <summary>
        /// Obtain current speed (cm/s)
        /// </summary>
        /// <returns>range (10, 100)</returns>
        public TelloActionResult GetSpeed()
        {
            var result = CreateResult(true);
            result.Message = commandClient.ExecuteCommand(ReadCommands.GetSpeed);

            return result;
        }

        /// <summary>
        /// Obtain current flight time
        /// </summary>
        /// <returns>flight time</returns>
        public TelloActionResult GetTime()
        {
            var result = CreateResult(true);
            result.Message = commandClient.ExecuteCommand(ReadCommands.GetFlightTime);

            return result;
        }

        /// <summary>
        /// Obtain Wi-Fi SNR
        /// </summary>
        /// <returns>SNR</returns>
        public TelloActionResult GetWiFi()
        {
            var result = CreateResult(true);
            result.Message = commandClient.ExecuteCommand(ReadCommands.GetWiFi);

            return result;
        }

        /// <summary>
        /// Fly to {x} {y} {z} at {speed} (cm/s)
        /// </summary>
        /// <param name="x">Position X, range(-500, 500)</param>
        /// <param name="y">Position Y, range(-500, 500)</param>
        /// <param name="z">Position Z, range(-500, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be set between - 20 and 20 simultaneously</remarks>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Go(int x, int y, int z, int speed)
        {
            var result = CreateResult(true);
            result.Message = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (x > 500 || x < -500)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.XDimensionOutOfRange, -500, 500));
            };

            if (y > 500 || y < -500)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.YDimensionOutOfRange, -500, 500));
            };

            if (z > 500 || z < -500)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.ZDimensionOutOfRange, -500, 500));
            };

            if (speed > 100 || speed < 10)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.SpeedOutOfRange, 10, 100));
            };

            if ((x > -20 && x < 20) &&
                (y > -20 && y < 20) &&
                (z > -20 && z < 20))
            {
                result.Succeeded = false;
                sb.AppendLine(CommandsErrorMessages.InvalidDimensions);
            }

            if (result.Succeeded)
            {
                result = ExecuteAction(string.Format(ControlCommands.Go, x, y, z, speed));
            }
            else
            {
                result.Message = sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// Prepare drone for flight
        /// </summary>
        /// <returns>OK if ready to fly, 
        /// ERROR if ignition procedyre failed</returns>
        public TelloActionResult Ignition()
        {
            return commandClient.InitializeCommandSDK();
        }

        /// <summary>
        /// Auto landing
        /// </summary>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult Land()
        {
            return ExecuteAction(ControlCommands.Land);
        }

        /// <summary>
        /// Fly left for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Left(int distance)
        {
            if (distance > 500 || distance < 20)
            {
                return new TelloActionResult(
                    false,
                    string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500));
            };

            return ExecuteAction(string.Format(ControlCommands.Left, distance));
        }

        /// <summary>
        /// Fly right for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Right(int distance)
        {
            if (distance > 500 || distance < 20)
            {
                return new TelloActionResult(
                    false,
                    string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500));
            };

            return ExecuteAction(string.Format(ControlCommands.Right, distance));
        }

        /// <summary>
        /// Set the Tello to station mode, and connect to a
        /// new access point with the access point’s ssid and password
        /// </summary>
        /// <param name="ssid">updated Wi-Fi name</param>
        /// <param name="password">updated Wi-Fi password</param>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult SetAccessPoint(string ssid, string password)
        {
            return ExecuteAction(string.Format(SetCommands.AccessPoint, ssid, password));
        }

        /// <summary>
        /// Set speed to {speed} cm/s
        /// </summary>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult SetSpeed(int speed)
        {
            if (speed > 100 || speed < 10)
            {
                return new TelloActionResult(
                    false,
                    string.Format(CommandsErrorMessages.SpeedOutOfRange, 10, 100));
            };

            return ExecuteAction(string.Format(SetCommands.Speed, speed));
        }

        /// <summary>
        /// Set Wi-Fi ssid and password
        /// </summary>
        /// <param name="ssid">updated Wi-Fi name</param>
        /// <param name="password">updated Wi-Fi password</param>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult SetWiFi(string ssid, string password)
        {
            return ExecuteAction(string.Format(SetCommands.WiFi, ssid, password));
        }

        /// <summary>
        /// Hovers in the air
        /// </summary>
        /// <remarks>Works at any time</remarks>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult Stop()
        {
            return ExecuteAction(ControlCommands.Stop);
        }

        /// <summary>
        /// Video stream OFF
        /// </summary>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult StreamOff()
        {
            return ExecuteAction(ControlCommands.VideoStreamOff);
        }

        /// <summary>
        /// Video stream ON
        /// </summary>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult StreamOn()
        {
            return ExecuteAction(ControlCommands.VideoStreamOn);
        }

        /// <summary>
        /// Auto takeoff
        /// </summary>
        /// <returns>OK / ERROR</returns>
        public TelloActionResult TakeOff()
        {
            return ExecuteAction(ControlCommands.TakeOff);
        }

        /// <summary>
        /// Rotate {degrees} degrees clockwise
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult TurnClockwise(int degrees)
        {
            if (degrees > 360 || degrees < 1)
            {
                return new TelloActionResult(
                    false,
                    string.Format(CommandsErrorMessages.DegreesOutOfRange, 1, 360));
            };

            return ExecuteAction(string.Format(ControlCommands.RotateClockwise, degrees));
        }

        /// <summary>
        /// Rotate {degrees} degrees counterclockwise
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult TurnCounterClockwise(int degrees)
        {
            if (degrees > 360 || degrees < 1)
            {
                return new TelloActionResult(
                    false,
                    string.Format(CommandsErrorMessages.DegreesOutOfRange, 1, 360));
            };

            return ExecuteAction(string.Format(ControlCommands.RotateCounterClockwise, degrees));
        }

        /// <summary>
        /// Ascend to {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        public TelloActionResult Up(int distance)
        {
            if (distance > 500 || distance < 20)
            {
                return new TelloActionResult(
                    false, 
                    string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500));
            };

            return ExecuteAction(string.Format(ControlCommands.Up, distance));
        }

        /// <summary>
        /// Initializes Action result object
        /// </summary>
        /// <param name="forSuccess">Result marked as success or failure</param>
        /// <returns>TelloActionResult</returns>
        private TelloActionResult CreateResult(bool forSuccess = false)
        {
            return new TelloActionResult()
            {
                Succeeded = forSuccess,
                Message = forSuccess ? "OK" : "ERROR"
            };
        }

        /// <summary>
        /// Checking if initialized and initializes if not
        /// </summary>
        private void CheckIfInCommandMode()
        {
            if (!commandClient.IsInCommandMode())
            {
                Ignition();
            }
        }

        /// <summary>
        /// Executes desired action
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <returns></returns>
        private TelloActionResult ExecuteAction(string action)
        {
            CheckIfInCommandMode();

            var result = CreateResult(true);
            result.Message = commandClient.ExecuteCommand(action);

            if (result.Message == TelloResponse.Failure)
            {
                result.Succeeded = false;
            }

            return result;
        }
    }
}
