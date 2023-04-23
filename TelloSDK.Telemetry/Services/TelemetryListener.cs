using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TelloSDK.Infrastructure.Models;
using TelloSDK.Telemetry.Contracts;
using TelloSDK.Telemetry.Models;
using static TelloSDK.Telemetry.Constants.ParserConstants;

namespace TelloSDK.Telemetry.Services
{
    internal class TelemetryListener : ITelemetryListener
    {
        private readonly int port;

        private readonly IPAddress address;

        private readonly ILogger logger;

        private UdpClient udpClient;

        private bool isListening = false;

        public TelemetryListener(
            IOptionsMonitor<TelloOptions> optionsAccessor,
            ILogger<TelemetryListener> _logger)
        {
            port = optionsAccessor.CurrentValue.Port;
            address = optionsAccessor.CurrentValue.IPAddress;
            logger = _logger;
        }

        public void Dispose()
        {
            if (isListening)
            {
                isListening = false;
                udpClient.Close();
                udpClient.Dispose();
            }
        }

        public void StartListener(Action<TelemetryData> telemetryProcessor)
        {
            if (isListening == false)
            {
                udpClient = new UdpClient(port);
                IPEndPoint groupEP = new IPEndPoint(address, port);
                isListening = true;

                try
                {
                    while (true)
                    {
                        byte[] bytes = udpClient.Receive(ref groupEP);
                        string data = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                        TelemetryData parsedData = ParsedData(data);
                        telemetryProcessor(parsedData);
                    }
                }
                catch (SocketException e)
                {
                    logger.LogError(e, "TelemetryListener/StartListener");
                }
                finally
                {
                    isListening = false;
                    udpClient.Close();
                }
            }
        }

        private TelemetryData ParsedData(string data)
        {
            var parser = data
                .Split(";", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(":", StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(k => k[0], v => v[1]);

            return new TelemetryData()
            {
                Acceleration = new AccelerationMetrics()
                {
                    X = Convert.ToDouble(parser[AccelerationX]),
                    Y = Convert.ToDouble(parser[AccelerationY]),
                    Z = Convert.ToDouble(parser[AccelerationZ])
                },
                Attitude = new Attitude()
                {
                    Pitch = Convert.ToInt32(parser[Pitch]),
                    Roll = Convert.ToInt32(parser[Roll]),
                    Yaw = Convert.ToInt32(parser[Yaw])
                },
                Battery = Convert.ToInt32(parser[Baterry]),
                Environment = new Models.Environment()
                {
                    Barometer = Convert.ToDouble(parser[Barometer]),
                    TemperatureHigh = Convert.ToInt32(parser[TemperatureHigh]),
                    TemperatureLow = Convert.ToInt32(parser[TemperatureLow])
                },
                FlightTime = Convert.ToInt32(parser[Time]),
                Height= Convert.ToInt32(parser[Height]),
                Speed = new SpeedMetrics() 
                {
                    X = Convert.ToInt32(parser[SpeedX]),
                    Y= Convert.ToInt32(parser[SpeedY]),
                    Z = Convert.ToInt32(parser[SpeedZ])
                },
                TOF = Convert.ToInt32(parser[Tof])
                
            };
        }

        public void StopListener()
        {
            Dispose();
        }
    }
}