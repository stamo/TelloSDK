namespace TelloSDK.Models
{
    /// <summary>
    /// Result of sdk command
    /// </summary>
    public class TelloActionResult
    {
        /// <summary>
        /// If command succeded
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Result message
        /// </summary>
        public string? Message { get; set; }
    }
}
