namespace TelloSDK.Telemetry.Models
{
    public class TelemetryData
    {
        public Attitude Attitude { get; set; } = null!;

        public Environment Environment { get; set; } = null!;

        public SpeedMetrics Speed { get; set; } = null!;

        public AccelerationMetrics Acceleration { get; set; } = null!;

        public int TOF { get; set; }

        public int Height { get; set; }

        public int Battery { get; set; }

        public int FlightTime { get; set; }
    }
}
