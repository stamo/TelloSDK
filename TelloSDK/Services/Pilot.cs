using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TelloSDK.Contracts;
using TelloSDK.Enumerations;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;

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

        public TelloActionResult Backward(int distance)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult Curve(int x1, int y1, int z1, int x2, int y2, int z2, int speed)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult Down(int distance)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult Emergency()
        {
            throw new NotImplementedException();
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

        public TelloActionResult Forward(int distance)
        {
            throw new NotImplementedException();
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

        public TelloActionResult Land()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult Left(int distance)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult Right(int distance)
        {
            throw new NotImplementedException();
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

        public TelloActionResult StreamOff()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult StreamOn()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult TakeOff()
        {
            throw new NotImplementedException();
        }

        public TelloActionResult TurnClockwise(int degrees)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult TurnCounterClockwise(int degrees)
        {
            throw new NotImplementedException();
        }

        public TelloActionResult Up(int distance)
        {
            throw new NotImplementedException();
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
    }
}
