using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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

        public TelloActionResult Curve(int x1, int y1, int z1, int x2, int y2, int z2, int speed)
        {
            throw new NotImplementedException();
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

        public TelloActionResult Flip(Direction direction)
        {
            throw new NotImplementedException();
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

        public TelloActionResult GetBattery()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult GetSdk()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult GetSerialNumber()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult GetSpeed()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult GetTime()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult GetWiFi()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult Go(int x, int y, int z, int speed)
        {
            throw new NotImplementedException();
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

        public TelloActionResult SetAccessPoint(string ssid, string password)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult SetSpeed(int speed)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult SetWiFi(string ssid, string password)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult Stop()
        {
            throw new NotImplementedException();
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

        public TelloActionResult TurnClockwise(int degrees)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult TurnCounterClockwise(int degrees)
        {
            throw new NotImplementedException();
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
