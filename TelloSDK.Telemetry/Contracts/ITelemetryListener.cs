using System;
using System.Collections.Generic;
using System.Text;
using TelloSDK.Telemetry.Models;

namespace TelloSDK.Telemetry.Contracts
{
    public interface ITelemetryListener : IDisposable
    {
        void StartListener(Action<TelemetryData> telemetryProcessor);

        void StopListener();
    }
}
