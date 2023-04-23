using System;
using System.Net;
using TelloSDK.Infrastructure.Models;
using TelloSDK.Telemetry.Contracts;
using TelloSDK.Telemetry.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Add Tello SDK Telemetry to IoC
    /// </summary>
    public static class TelloSDKTelemetryServiceCollectionExtension
    {
        /// <summary>
        /// Adding Tello SDK Telemetry with default configuration
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns></returns>
        public static IServiceCollection AddTelloSDKTelemetry(this IServiceCollection services)
        {
            Action<TelloOptions> options = option =>
            {
                option.IPAddress = IPAddress.Parse("0.0.0.0");
                option.Port = 8890;
            };

            AddTelloSDKTelemetry(services, options);

            return services;
        }

        /// <summary>
        /// Adding Tello SDK Telemetry with customizable configuration options
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="options">Configuration options</param>
        /// <returns></returns>
        public static IServiceCollection AddTelloSDKTelemetry(this IServiceCollection services, Action<TelloOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<ITelemetryListener, TelemetryListener>();

            return services;
        }
    }
}
