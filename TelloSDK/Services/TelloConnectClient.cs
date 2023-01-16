using System.Net;
using System.Net.Sockets;
using TelloSDK.Pilot.Contracts;

namespace TelloSDK.Pilot.Services
{
    /// <summary>
    /// Client to communicate with drone 
    /// </summary>
    public class TelloConnectClient : ITelloConnectClient
    {
        /// <summary>
        /// Internal client used 
        /// for comunication
        /// </summary>
        private UdpClient udpClient;

        /// <summary>
        /// Creates connection client
        /// </summary>
        public TelloConnectClient()
        {
            udpClient = new UdpClient();
        }

        /// <summary>
        /// Time to wait for response
        /// </summary>
        public int ReceiveTimeout
        { 
            get => udpClient.Client.ReceiveTimeout; 
            set => udpClient.Client.ReceiveTimeout = value; 
        }

        /// <summary>
        /// Close communication client
        /// </summary>
        public void Close()
        {
            udpClient.Close();
        }

        /// <summary>
        /// Dispose communication client
        /// </summary>
        public void Dispose()
        {
            udpClient.Dispose();
        }

        /// <summary>
        /// Receive data from drone
        /// </summary>
        /// <param name="endpoint">Remote endpoint</param>
        /// <returns></returns>
        public byte[] Receive(ref IPEndPoint endpoint)
        {
            return udpClient.Receive(ref endpoint);
        }

        /// <summary>
        /// Send command to drone
        /// </summary>
        /// <param name="datagram">Command data</param>
        /// <param name="length">Command length</param>
        /// <param name="endpoint">Remote endpoin</param>
        /// <returns></returns>
        public int Send(byte[] datagram, int length, IPEndPoint endpoint)
        {
            return udpClient.Send(datagram, length, endpoint);
        }
    }
}
