using System;
using TelloSDK.Enumerations;

namespace TelloSDK.Pilot.Contracts
{
    /// <summary>
    /// Flight plan
    /// </summary>
    public interface IFlightPlan : IDisposable
    {
        /// <summary>
        /// Validates flight plan
        /// </summary>
        /// <returns></returns>
        IFlightPlan Validate();

        /// <summary>
        /// Executes flight plan
        /// </summary>
        /// <param name="logger">Flight plan logger</param>
        void Execute(Action<string> logger);

        /// <summary>
        /// Auto takeoff
        /// </summary>
        IFlightPlan TakeOff();

        /// <summary>
        /// Auto landing
        /// </summary>
        IFlightPlan Land();

        /// <summary>
        /// Video stream ON
        /// </summary>
        IFlightPlan StreamOn();

        /// <summary>
        /// Video stream OFF
        /// </summary>
        IFlightPlan StreamOff();

        /// <summary>
        /// Stop motors immediately
        /// </summary>
        IFlightPlan Emergency();

        /// <summary>
        /// Ascend to {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        IFlightPlan Up(int distance);

        /// <summary>
        ///  Descend to {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        IFlightPlan Down(int distance);

        /// <summary>
        /// Fly left for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        IFlightPlan Left(int distance);

        /// <summary>
        /// Fly right for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        IFlightPlan Right(int distance);

        /// <summary>
        /// Fly forward for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        IFlightPlan Forward(int distance);

        /// <summary>
        /// Fly backward for {distance} cm
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        IFlightPlan Backward(int distance);

        /// <summary>
        /// Rotate {degrees} degrees clockwise
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        IFlightPlan TurnClockwise(int degrees);

        /// <summary>
        /// Rotate {degrees} degrees counterclockwise
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        IFlightPlan TurnCounterClockwise(int degrees);

        /// <summary>
        /// Flip in {direction} direction
        /// </summary>
        /// <param name="direction">Direction to flip in</param>
        IFlightPlan Flip(Direction direction);

        /// <summary>
        /// Fly to {x} {y} {z} at {speed} (cm/s)
        /// </summary>
        /// <param name="x">Position X, range(-500, 500)</param>
        /// <param name="y">Position Y, range(-500, 500)</param>
        /// <param name="z">Position Z, range(-500, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be set between - 20 and 20 simultaneously</remarks>
        IFlightPlan Go(int x, int y, int z, int speed);

        /// <summary>
        /// Hovers in the air
        /// </summary>
        /// <remarks>Works at any time</remarks>
        IFlightPlan Stop();

        /// <summary>
        /// Fly at a curve according to the two given 
        /// coordinates at {speed} (cm/s)
        /// </summary>
        /// <param name="x1">Position X, range(-500, 500)</param>
        /// <param name="y1">Position Y, range(-500, 500)</param>
        /// <param name="z1">Position Z, range(-500, 500)</param>
        /// <param name="x2">Position X, range(-500, 500)</param>
        /// <param name="y2">Position Y, range(-500, 500)</param>
        /// <param name="z2">Position Z, range(-500, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 60)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be set between - 20 and 20 simultaneously</remarks>
        IFlightPlan Curve(int x1, int y1, int z1, int x2, int y2, int z2, int speed);

        /// <summary>
        /// Returns count of prepared commands
        /// </summary>
        public int Count { get; }
    }
}
