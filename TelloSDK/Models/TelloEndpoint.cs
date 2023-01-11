using System.Net;

namespace TelloSDK.Models
{
    /// <summary>
    /// Configuration option to specify tello endpoints
    /// </summary>
    public class TelloEndpoint
    {
        /// <summary>
        /// IP address
        /// </summary>
        public IPAddress Ip { get; set; } = null!;

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }
    }
}
