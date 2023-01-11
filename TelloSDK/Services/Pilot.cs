using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TelloSDK.Constants;
using TelloSDK.Contracts;
using TelloSDK.Enumerations;
using TelloSDK.Models;

namespace TelloSDK.Services
{
    /// <summary>
    /// Tello pilot
    /// </summary>
    public class Pilot : IPilot
    {
        /// <summary>
        /// UDP Cllient to send commands to Tello
        /// </summary>
        private UdpClient client;

        /// <summary>
        /// Tello command endpoint
        /// </summary>
        private readonly IPEndPoint commandEndpoint;

        /// <summary>
        /// Remote endpoint to be used for Tello responses
        /// </summary>
        private IPEndPoint remoteIpEndPoint;

        /// <summary>
        /// Indicates whether drone is in SDK mode 
        /// </summary>
        private bool isInCommandMode;

        /// <summary>
        /// Initializes Tello Pilot
        /// </summary>
        /// <param name="optionsAccessor">Tello pilot options accessor</param>
        public Pilot(IOptionsMonitor<TelloOptions> optionsAccessor)
        {
            var options = optionsAccessor.CurrentValue;
            commandEndpoint = new IPEndPoint(
                options.CommandEndpoint.Ip, 
                options.CommandEndpoint.Port);

            isInCommandMode = false;
            remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            client = new UdpClient();
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

        public void EnginesOff()
        {
            client.Close();
            client.Dispose();
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
            var result = CreateResult(true);

            if (client == null)
            {
                client = new UdpClient();
            }

            if (!isInCommandMode)
            {
                result.Message = ExecuteCommand("Command");

                if (result.Message == TelloResponse.Success) 
                {
                    isInCommandMode = true;
                } 
                else
                {
                    result.Succeeded = false;
                }
            }

            return result;
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
        /// Executes drone command
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>Command result</returns>
        private string ExecuteCommand(string command) 
        {
            byte[] commandBytes = Encoding.ASCII.GetBytes(command);
            client.Send(commandBytes, commandBytes.Length, commandEndpoint);
            client.Client.ReceiveTimeout = 2500;
            var receiveBytes = client.Receive(ref remoteIpEndPoint);
            var response = Encoding.ASCII.GetString(receiveBytes);

            return response;
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
