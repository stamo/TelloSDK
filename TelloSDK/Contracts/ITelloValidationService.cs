using TelloSDK.Models;

namespace TelloSDK.Pilot.Contracts
{
    /// <summary>
    /// Tello SDK command validation
    /// </summary>
    public interface ITelloValidationService
    {
        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        TelloActionResult ValidateUp(int distance);

        /// <summary>
        ///  Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        TelloActionResult ValidateDown(int distance);

        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        TelloActionResult ValidateLeft(int distance);

        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        TelloActionResult ValidateRight(int distance);

        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        TelloActionResult ValidateForward(int distance);

        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        TelloActionResult ValidateBackward(int distance);

        /// <summary>
        /// Validates degrees
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        TelloActionResult ValidateTurnClockwise(int degrees);

        /// <summary>
        /// Validates degrees
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        TelloActionResult ValidateTurnCounterClockwise(int degrees);

        /// <summary>
        /// Validates Position and speed
        /// </summary>
        /// <param name="x">Position X, range(20, 500)</param>
        /// <param name="y">Position Y, range(20, 500)</param>
        /// <param name="z">Position Z, range(20, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be set between - 20 and 20 simultaneously</remarks>
        TelloActionResult ValidateGo(int x, int y, int z, int speed);

        /// <summary>
        /// Validates both Positions and speed
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
        TelloActionResult ValidateCurve(int x1, int y1, int z1, int x2, int y2, int z2, int speed);

        /// <summary>
        /// Validates speed
        /// </summary>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        TelloActionResult ValidateSetSpeed(int speed);
    }
}
