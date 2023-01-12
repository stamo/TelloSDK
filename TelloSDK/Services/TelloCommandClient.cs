using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TelloSDK.Infrastructure.Constants;
using TelloSDK.Infrastructure.Models;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using static TelloSDK.Pilot.Constants.TelloSDKCommands;

namespace TelloSDK.Pilot.Services
{
    public class TelloCommandClient : ITelloCommandClient
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
        public TelloCommandClient(IOptionsMonitor<TelloOptions> optionsAccessor)
        {
            var options = optionsAccessor.CurrentValue;
            commandEndpoint = new IPEndPoint(
                options.IPAddress,
                options.Port);

            isInCommandMode = false;
            remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            client = new UdpClient();
        }

        /// <summary>
        /// Executes drone command
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>Command result</returns>
        public string ExecuteCommand(string command)
        {
            byte[] commandBytes = Encoding.ASCII.GetBytes(command);
            client.Send(commandBytes, commandBytes.Length, commandEndpoint);
            client.Client.ReceiveTimeout = 2500;
            var receiveBytes = client.Receive(ref remoteIpEndPoint);
            var response = Encoding.ASCII.GetString(receiveBytes);

            return response;
        }

        /// <summary>
        /// Checks if Tello SDK is initialized
        /// </summary>
        /// <returns></returns>
        public bool IsInCommandMode()
        {
            return isInCommandMode;
        }

        /// <summary>
        /// Initializes SDK
        /// </summary>
        /// <returns></returns>
        public TelloActionResult InitializeCommandSDK()
        {
            var result = new TelloActionResult()
            {
                Succeeded = true,
                Message = "OK"
            };

            if (client == null)
            {
                client = new UdpClient();
            }

            if (!isInCommandMode)
            {
                result.Message = ExecuteCommand(ControlCommands.InitializeSDK);

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

        /// <summary>
        /// Disposes SDK client
        /// </summary>
        public void DisconnectCommandSDK()
        {
            isInCommandMode = false;
            client.Close();
            client.Dispose();
        }
    }
}
