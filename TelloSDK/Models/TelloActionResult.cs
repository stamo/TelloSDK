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

        /// <summary>
        /// Create empty response
        /// </summary>
        public TelloActionResult()
        {}

        /// <summary>
        /// Create initialized response
        /// </summary>
        /// <param name="succeeded">Shows if result succeeded</param>
        /// <param name="message">Status message</param>
        public TelloActionResult(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }
    }
}
