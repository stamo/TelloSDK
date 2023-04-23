using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TelloSDK.Telemetry.Contracts;
using TelloSDK.Telemetry.Models;

IServiceProvider provider = new ServiceCollection()
    .AddLogging()
    .AddTelloSDKTelemetry()
    .BuildServiceProvider();


var telemetryService = provider.GetService<ITelemetryListener>();

if (telemetryService != null)
{
    telemetryService.StartListener(WriteTelemetry);
}

void WriteTelemetry(TelemetryData data)
{
    Console.WriteLine(JsonSerializer.Serialize(data));
}