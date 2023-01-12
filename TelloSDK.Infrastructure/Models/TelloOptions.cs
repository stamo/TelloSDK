using System.Net;

namespace TelloSDK.Infrastructure.Models
{
    /// <summary>
    /// Tello SDK configuration options
    /// </summary>
    public class TelloOptions
    {
        /// <summary>
        /// Command ip to controll the drone
        /// </summary>
        public IPAddress IPAddress { get; set; } = null!;

        /// <summary>
        /// UDP port
        /// </summary>
        public int Port { get; set; }
    }
}
