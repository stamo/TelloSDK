namespace TelloSDK.Pilot.Models
{
    /// <summary>
    /// Command for REPL interface
    /// </summary>
    public class REPLCommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Command { get; set; } = null!;

        /// <summary>
        /// Method name
        /// </summary>
        public string Method { get; set; } = null!;

        /// <summary>
        /// Number of parameters
        /// </summary>
        public int NumberOfParams { get; set; }
    }
}
