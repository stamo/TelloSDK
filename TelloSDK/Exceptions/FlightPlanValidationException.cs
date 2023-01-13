using System;

namespace TelloSDK.Pilot.Exceptions
{
    /// <summary>
    /// Flight plan validation exception
    /// </summary>
    public class FlightPlanValidationException : ApplicationException
    {
        /// <summary>
        /// Create FlightPlanValidationException
        /// with empty message
        /// </summary>
        public FlightPlanValidationException()
            : base() { }

        /// <summary>
        /// Create FlightPlanValidationException
        /// with message
        /// </summary>
        /// <param name="message">Exception message</param>
        public FlightPlanValidationException(string message)
            :base(message) { }
    }
}
