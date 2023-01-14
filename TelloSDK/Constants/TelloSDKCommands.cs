using TelloSDK.Enumerations;

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
            internal const string InitializeSDK = "command";

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

            /// <summary>
            /// Rotate “x” degrees clockwise
            /// </summary>
            internal const string RotateClockwise = "cw {0}";

            /// <summary>
            /// Rotate “x” degrees counterclockwise
            /// </summary>
            internal const string RotateCounterClockwise = "ccw {0}";

            /// <summary>
            /// Flip in “x” direction
            /// </summary>
            internal const string Flip = "flip {0}";

            /// <summary>
            /// Gets flip directio
            /// </summary>
            /// <param name="direction">Direction to flip</param>
            /// <returns></returns>
            internal static string GetFlipDirection(Direction direction) => direction switch
            {
                Direction.Forward => "f",
                Direction.Back => "b",
                Direction.Left => "l",
                Direction.Right => "r",
                _ => "f"
            };

            /// <summary>
            /// Fly to “x” “y” “z” at “speed” (cm/s)
            /// </summary>
            internal const string Go = "go {0} {1} {2} {3}";

            /// <summary>
            /// Hovers in the air
            /// </summary>
            internal const string Stop = "stop";

            /// <summary>
            /// Fly at a curve according to the two given coordinates
            /// at “speed” (cm/s)
            /// </summary>
            internal const string Curve = "curve {0} {1} {2} {3} {4} {5} {6}";
        }

        /// <summary>
        /// Tello SDK Set commands
        /// </summary>
        internal static class SetCommands
        {
            /// <summary>
            /// Set speed to “x” cm/s
            /// </summary>
            internal const string Speed = "speed {0}";

            /// <summary>
            /// Set Wi-Fi password
            /// </summary>
            internal const string WiFi = "wifi {0} {1}";

            /// <summary>
            /// Set the Tello to station mode, and connect to a
            /// new access point with the access point’s ssid and
            /// password.
            /// </summary>
            internal const string AccessPoint = "ap {0} {1}";
        }

        /// <summary>
        /// Tello SDK Read commands
        /// </summary>
        internal static class ReadCommands
        {
            /// <summary>
            /// Obtain current speed (cm/s)
            /// </summary>
            internal const string GetSpeed = "speed?";

            /// <summary>
            /// Obtain current battery percentage
            /// </summary>
            internal const string GetBattery = "battery?";

            /// <summary>
            /// Obtain current flight time
            /// </summary>
            internal const string GetFlightTime = "time?";

            /// <summary>
            /// Obtain Wi-Fi SNR
            /// </summary>
            internal const string GetWiFi = "wifi?";

            /// <summary>
            /// Obtain the Tello SDK version
            /// </summary>
            internal const string GetSDKVersion = "sdk?";

            /// <summary>
            /// Obtain the Tello serial number
            /// </summary>
            internal const string GetSerialNumber = "sn?";
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

            /// <summary>
            /// Degrees out of range error
            /// Expects two parameters - lower and upper boundary 
            /// </summary>
            internal const string DegreesOutOfRange = "Degrees must be between {0} and {1}";

            /// <summary>
            /// X Dimension out of range error
            /// Expects two parameters - lower and upper boundary 
            /// </summary>
            internal const string XDimensionOutOfRange = "X Dimension must be between {0} and {1}";

            /// <summary>
            /// Y Dimension out of range error
            /// Expects two parameters - lower and upper boundary 
            /// </summary>
            internal const string YDimensionOutOfRange = "Y Dimension must be between {0} and {1}";

            /// <summary>
            /// Z Dimension out of range error
            /// Expects two parameters - lower and upper boundary 
            /// </summary>
            internal const string ZDimensionOutOfRange = "Z Dimension must be between {0} and {1}";

            /// <summary>
            /// Speed out of range error
            /// Expects two parameters - lower and upper boundary 
            /// </summary>
            internal const string SpeedOutOfRange = "Speed must be between {0} and {1}";

            /// <summary>
            /// Invalid dimensions
            /// </summary>
            internal const string InvalidDimensions = "x, y and z values can’t be set between -20 and 20 simultaneously";
        }

        /// <summary>
        /// REPL Error messages
        /// </summary>
        internal static class REPLErrorMessages 
        {
            internal const string UnknownCommand = "Unknown command";

            internal const string InvalidParameters = "Invalid parameters";
        }
    }
}
