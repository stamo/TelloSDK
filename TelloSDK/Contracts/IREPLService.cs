using System;

namespace TelloSDK.Pilot.Contracts
{
    /// <summary>
    /// Service to use Tello REPL interface
    /// </summary>
    public interface IREPLService : IDisposable
    {
        /// <summary>
        /// REPL command executor
        /// </summary>
        /// <param name="command">Command to be executed</param>
        /// <returns></returns>
        string ExecuteCommand(string command);
    }
}
