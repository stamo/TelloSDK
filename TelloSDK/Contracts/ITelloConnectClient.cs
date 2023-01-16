using System;
using System.Net;
using System.Net.Sockets;

namespace TelloSDK.Pilot.Contracts
{
    /// <summary>
    /// Client to communicate with drone 
    /// </summary>
    public interface ITelloConnectClient : IDisposable
    {
        /// <summary>
        /// Time to wait for response
        /// </summary>
        public int ReceiveTimeout { get; set; }

        /// <summary>
        /// Send command to drone
        /// </summary>
        /// <param name="datagram">Command data</param>
        /// <param name="length">Command length</param>
        /// <param name="endpoint">Remote endpoin</param>
        /// <returns></returns>
        int Send(byte[] datagram, int length, IPEndPoint endpoint);

        /// <summary>
        /// Receive data from drone
        /// </summary>
        /// <param name="endpoint">Remote endpoint</param>
        /// <returns></returns>
        byte[] Receive(ref IPEndPoint endpoint);

        /// <summary>
        /// Close comunication client
        /// </summary>
        void Close();
    }
}
