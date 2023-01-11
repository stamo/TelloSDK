namespace TelloSDK.Models
{
    /// <summary>
    /// Tello Pilot configuration options
    /// </summary>
    public class TelloOptions
    {
        /// <summary>
        /// Command endpoint to controll the drone
        /// </summary>
        public TelloEndpoint CommandEndpoint { get; set; } = null!;

        /// <summary>
        /// Endpoint to receive status update 
        /// </summary>
        public TelloEndpoint? StatusEndpoint { get; set; }

        /// <summary>
        /// Endpoint to receive video stream
        /// </summary>
        public TelloEndpoint? VideoStreamEndpoint { get; set; }
    }
}
