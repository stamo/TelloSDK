using System;
using TelloSDK.Enumerations;
using TelloSDK.Models;

namespace TelloSDK.Contracts
{
    /// <summary>
    /// Tello pilot
    /// </summary>
    public interface IPilot : IDisposable
    {
        /// <summary>
        /// Prepare drone for flight
        /// </summary>
        /// <returns>OK if ready to fly, 
        /// ERROR if ignition procedyre failed</returns>
        TelloActionResult Ignition();

        /// <summary>
        /// Auto takeoff
        /// </summary>
        /// <returns>OK / ERROR</returns>
        TelloActionResult TakeOff();

        /// <summary>
        /// Auto landing
        /// </summary>
        /// <returns>OK / ERROR</returns>
        TelloActionResult Land();

        /// <summary>
        /// Video stream ON
        /// </summary>
        /// <returns>OK / ERROR</returns>
        TelloActionResult StreamOn();

        /// <summary>
        /// Video stream OFF
        /// </summary>
        /// <returns>OK / ERROR</returns>
        TelloActionResult StreamOff();

        /// <summary>
        /// Stop motors immediately
        /// </summary>
        /// <returns>OK / ERROR</returns>
        TelloActionResult Emergency();

        /// <summary>
        /// Ascend to {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Up(int distance);

        /// <summary>
        ///  Descend to {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Down(int distance);

        /// <summary>
        /// Fly left for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Left(int distance);

        /// <summary>
        /// Fly right for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Right(int distance);

        /// <summary>
        /// Fly forward for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Forward(int distance);

        /// <summary>
        /// Fly backward for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Backward(int distance);

        /// <summary>
        /// Rotate {degrees} degrees clockwise
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult TurnClockwise(int degrees);

        /// <summary>
        /// Rotate {degrees} degrees counterclockwise
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult TurnCounterClockwise(int degrees);

        /// <summary>
        /// Flip in {direction} direction
        /// </summary>
        /// <param name="direction">Direction to flip in</param>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Flip(Direction direction);

        /// <summary>
        /// Fly to {x} {y} {z} at {speed} (cm/s)
        /// </summary>
        /// <param name="x">Position X, range(20, 500)</param>
        /// <param name="y">Position Y, range(20, 500)</param>
        /// <param name="z">Position Z, range(20, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be set between - 20 and 20 simultaneously</remarks>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Go(int x, int y, int z, int speed);

        /// <summary>
        /// Hovers in the air
        /// </summary>
        /// <remarks>Works at any time</remarks>
        /// <returns>OK / ERROR</returns>
        TelloActionResult Stop();

        /// <summary>
        /// Fly at a curve according to the two given 
        /// coordinates at {speed} (cm/s)
        /// </summary>
        /// <param name="x1">Position X, range(20, 500)</param>
        /// <param name="y1">Position Y, range(20, 500)</param>
        /// <param name="z1">Position Z, range(20, 500)</param>
        /// <param name="x2">Position X, range(20, 500)</param>
        /// <param name="y2">Position Y, range(20, 500)</param>
        /// <param name="z2">Position Z, range(20, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 60)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be set between - 20 and 20 simultaneously</remarks>
        /// <returns>OK / ERROR / INVALID PARAMETER</returns>
        TelloActionResult Curve(int x1, int y1, int z1, int x2, int y2, int z2, int speed);

        /// <summary>
        /// Set speed to {speed} cm/s
        /// </summary>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        /// <returns>OK / ERROR</returns>
        TelloActionResult SetSpeed(int speed);

        /// <summary>
        /// Set Wi-Fi ssid and password
        /// </summary>
        /// <param name="ssid">updated Wi-Fi name</param>
        /// <param name="password">updated Wi-Fi password</param>
        /// <returns>OK / ERROR</returns>
        TelloActionResult SetWiFi(string ssid, string password);

        /// <summary>
        /// Set the Tello to station mode, and connect to a
        /// new access point with the access point’s ssid and password
        /// </summary>
        /// <param name="ssid">updated Wi-Fi name</param>
        /// <param name="password">updated Wi-Fi password</param>
        /// <returns>OK / ERROR</returns>
        TelloActionResult SetAccessPoint(string ssid, string password);

        /// <summary>
        /// Obtain current speed (cm/s)
        /// </summary>
        /// <returns>range (10, 100)</returns>
        TelloActionResult GetSpeed();

        /// <summary>
        /// Obtain Wi-Fi SNR
        /// </summary>
        /// <returns>SNR</returns>
        TelloActionResult GetWiFi();

        /// <summary>
        /// Obtain current battery percentage
        /// </summary>
        /// <returns>range(0, 100)</returns>
        TelloActionResult GetBattery();

        /// <summary>
        /// Obtain current flight time
        /// </summary>
        /// <returns>flight time</returns>
        TelloActionResult GetTime();

        /// <summary>
        /// Obtain the Tello SDK version
        /// </summary>
        /// <returns>SDK version</returns>
        TelloActionResult GetSdk();

        /// <summary>
        /// Obtain the Tello serial number
        /// </summary>
        /// <returns>serial number</returns>
        TelloActionResult GetSerialNumber();

        /// <summary>
        /// Disconnects Tello
        /// </summary>
        void EnginesOff();
    }
}
