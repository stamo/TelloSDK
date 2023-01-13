namespace TelloSDK.Pilot.Models
{
    /// <summary>
    /// Flight plan commands
    /// </summary>
    public class FlightPlanCommand
    {
        /// <summary>
        /// Executable command
        /// </summary>
        public string Command { get; set; } = null!;

        /// <summary>
        /// Validation Method
        /// </summary>
        public string? ValidationMethod { get; set; }

        /// <summary>
        /// Command parameters
        /// </summary>
        public object[]? Parameters { get; set; }
    }
}
