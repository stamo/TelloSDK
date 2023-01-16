using System.Text;
using TelloSDK.Infrastructure.Constants;
using TelloSDK.Models;
using TelloSDK.Pilot.Contracts;
using static TelloSDK.Pilot.Constants.TelloSDKCommands;

namespace TelloSDK.Pilot.Services
{
    /// <summary>
    /// Tello SDK command validation
    /// </summary>
    public class TelloValidationService : ITelloValidationService
    {
        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public TelloActionResult ValidateBackward(int distance)
        {
            var result = CreateResult(true);

            if (distance > 500 || distance < 20)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500);
            };

            return result;
        }

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
        /// <remarks>“x”, “y”, and “z” values can’t be equal to 20 simultaneously</remarks>
        public TelloActionResult ValidateCurve(int x1, int y1, int z1, int x2, int y2, int z2, int speed)
        {
            var result = CreateResult(true);
            result.Message = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (x1 > 500 || x1 < 20 || x2 > 500 || x2 < 20)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.XDimensionOutOfRange, 20, 500));
            };

            if (y1 > 500 || y1 < 20 || y2 > 500 || y2 < 20)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.YDimensionOutOfRange, 20, 500));
            };

            if (z1 > 500 || z1 < 20 || z2 > 500 || z2 < 20)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.ZDimensionOutOfRange, 20, 500));
            };

            if (speed > 60 || speed < 10)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.SpeedOutOfRange, 10, 60));
            };

            if ((x1 == 20 && y1 == 20 && z1 == 20) ||
                (x2 == 20 && y2 == 20 && z2 == 20))
            {
                result.Succeeded = false;
                sb.AppendLine(CommandsErrorMessages.InvalidDimensions);
            }

            if (result.Succeeded == false)
            {
                result.Message = sb.ToString();
            }
            else
            {
                result.Message = TelloResponse.Success;
            }

            return result;
        }

        /// <summary>
        ///  Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public TelloActionResult ValidateDown(int distance)
        {
            var result = CreateResult(true);

            if (distance > 500 || distance < 20)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500);
            };

            return result;
        }

        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public TelloActionResult ValidateForward(int distance)
        {
            var result = CreateResult(true);

            if (distance > 500 || distance < 20)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500);
            };

            return result;
        }

        /// <summary>
        /// Validates Position and speed
        /// </summary>
        /// <param name="x">Position X, range(20, 500)</param>
        /// <param name="y">Position Y, range(20, 500)</param>
        /// <param name="z">Position Z, range(20, 500)</param>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        /// <remarks>“x”, “y”, and “z” values can’t be equal to 20 simultaneously</remarks>
        public TelloActionResult ValidateGo(int x, int y, int z, int speed)
        {
            var result = CreateResult(true);
            result.Message = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (x > 500 || x < 20)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.XDimensionOutOfRange, 20, 500));
            };

            if (y > 500 || y < 20)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.YDimensionOutOfRange, 20, 500));
            };

            if (z > 500 || z < 20)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.ZDimensionOutOfRange, 20, 500));
            };

            if (speed > 100 || speed < 10)
            {
                result.Succeeded = false;
                sb.AppendLine(string.Format(CommandsErrorMessages.SpeedOutOfRange, 10, 100));
            };

            if (x == 20 && y == 20 && z == 20)
            {
                result.Succeeded = false;
                sb.AppendLine(CommandsErrorMessages.InvalidDimensions);
            }
            
            if (result.Succeeded == false)
            {
                result.Message = sb.ToString();
            }
            else
            {
                result.Message = TelloResponse.Success;
            }

            return result;
        }

        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public TelloActionResult ValidateLeft(int distance)
        {
            var result = CreateResult(true);

            if (distance > 500 || distance < 20)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500);
            };

            return result;
        }

        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public TelloActionResult ValidateRight(int distance)
        {
            var result = CreateResult(true);

            if (distance > 500 || distance < 20)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500);
            };

            return result;
        }

        /// <summary>
        /// Validates speed
        /// </summary>
        /// <param name="speed">Speed in (cm/s), range(10, 100)</param>
        public TelloActionResult ValidateSetSpeed(int speed)
        {
            var result = CreateResult(true);

            if (speed > 100 || speed < 10)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.SpeedOutOfRange, 10, 100);
            };

            return result;
        }

        /// <summary>
        /// Validates degrees
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        public TelloActionResult ValidateTurnClockwise(int degrees)
        {
            var result = CreateResult(true);

            if (degrees > 360 || degrees < 1)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.DegreesOutOfRange, 1, 360);
            };

            return result;
        }

        /// <summary>
        /// Validates degrees
        /// </summary>
        /// <param name="degrees">Degrees to rotate to
        /// range(1, 360)</param>
        public TelloActionResult ValidateTurnCounterClockwise(int degrees)
        {
            var result = CreateResult(true);

            if (degrees > 360 || degrees < 1)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.DegreesOutOfRange, 1, 360);
            };

            return result;
        }

        /// <summary>
        /// Validates distance
        /// </summary>
        /// <param name="distance">Distance in centimeters
        /// range(20, 500)</param>
        public TelloActionResult ValidateUp(int distance)
        {
            var result = CreateResult(true);

            if (distance > 500 || distance < 20)
            {
                result.Succeeded = false;
                result.Message = string.Format(CommandsErrorMessages.DistanceOutOfRange, 20, 500);
            };

            return result;
        }

        /// <summary>
        /// Initializes Action result object
        /// </summary>
        /// <param name="forSuccess">Result marked as success or failure</param>
        /// <returns>TelloActionResult</returns>
        private TelloActionResult CreateResult(bool forSuccess = false)
        {
            return new TelloActionResult()
            {
                Succeeded = forSuccess,
                Message = forSuccess ? TelloResponse.Success : TelloResponse.Failure,
            };
        }
    }
}
