using System;
using System.Net;
using TelloSDK.Contracts;
using TelloSDK.Infrastructure.Models;
using TelloSDK.Pilot.Contracts;
using TelloSDK.Pilot.Services;
using TelloSDK.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Add Tello SDK Pilot to IoC
    /// </summary>
    public static class TelloSDKPilotServiceCollectionExtension
    {
        /// <summary>
        /// Adding Tello SDK Pilot with default configuration
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns></returns>
        public static IServiceCollection AddTelloSDKPilot(this IServiceCollection services)
        {
            Action<TelloOptions> options = option =>
            {
                option.IPAddress = IPAddress.Parse("192.168.10.1");
                option.Port = 8889;
            };

            AddTelloSDKPilot(services, options);

            return services;
        }

        /// <summary>
        /// Adding Tello SDK Pilot with customizable configuration options
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="options">Configuration options</param>
        /// <returns></returns>
        public static IServiceCollection AddTelloSDKPilot(this IServiceCollection services, Action<TelloOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<ITelloCommandClient, TelloCommandClient>();
            services.AddScoped<ITelloValidationService, TelloValidationService>();
            services.AddScoped<IPilot, Pilot>();
            services.AddScoped<IREPLService, REPLService>();
            services.AddScoped<IFlightPlan, FlightPlan>();

            return services;
        }
    }
}
