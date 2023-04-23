using System;
using TelloSDK.Models;

namespace TelloSDK.Pilot.Contracts
{
    /// <summary>
    /// Tello SDK command executor
    /// </summary>
    public interface ITelloCommandClient : IDisposable
    {
        /// <summary>
        /// Executes drone command
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>Command result</returns>
        string ExecuteCommand(string command);

        /// <summary>
        /// Checks if Tello SDK is initialized
        /// </summary>
        /// <returns></returns>
        bool IsInCommandMode();

        /// <summary>
        /// Initializes SDK
        /// </summary>
        /// <returns></returns>
        TelloActionResult InitializeCommandSDK();

        /// <summary>
        /// Disposes SDK client
        /// </summary>
        void DisconnectCommandSDK();
    }
}
