namespace TelloSDK.Pilot.Constants
{
    /// <summary>
    /// SDK commands
    /// </summary>
    internal static class TelloSDKCommands
    {
        /// <summary>
        /// SDK Control commands
        /// </summary>
        internal static class ControlCommands
        {
            /// <summary>
            /// Enter SDK mode
            /// </summary>
            internal const string InitializeSDK = "Command";

            /// <summary>
            /// Auto takeoff
            /// </summary>
            internal const string TakeOff = "takeoff";

            /// <summary>
            /// Auto landing
            /// </summary>
            internal const string Land = "land";

            /// <summary>
            /// Enable video stream
            /// </summary>
            internal const string VideoStreamOn = "streamon";

            /// <summary>
            /// Disable video stream
            /// </summary>
            internal const string VideoStreamOff = "streamoff";

            /// <summary>
            /// Stop motors immediately
            /// </summary>
            internal const string Emergency = "emergency";

            /// <summary>
            /// Ascend to “x” cm
            /// </summary>
            internal const string Up = "up {0}";

            /// <summary>
            /// Descend to “x” cm
            /// </summary>
            internal const string Down = "down {0}";

            /// <summary>
            /// Fly left for “x” cm
            /// </summary>
            internal const string Left = "left {0}";

            /// <summary>
            /// Fly right for “x” cm
            /// </summary>
            internal const string Right = "right {0}";

            /// <summary>
            /// Fly forward for “x” cm
            /// </summary>
            internal const string Forward = "forward {0}";

            /// <summary>
            /// Fly backward for “x” cm
            /// </summary>
            internal const string Back = "back {0}";
        }

        /// <summary>
        /// Commands Error messages
        /// </summary>
        internal static class CommandsErrorMessages 
        {
            /// <summary>
            /// Distance out of range error
            /// Expects two parameters - lower and upper boundary 
            /// </summary>
            internal const string DistanceOutOfRange = "Distance must be between {0} and {1}";
        }
    }
}
