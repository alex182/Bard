using Bard.Destinations.Concretes;
using Bard.Destinations.Interfaces;
using Bard.Destinations.Models;
using Bard.Jobs.Concretes;
using Bard.Jobs.Models;
using Bard.Sources.Concretes;
using Bard.Sources.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.  
        public void ConfigureServices(IServiceCollection services)
        {
            var haMqttOptions = new HomeAssistantMqttOptions()
            {
                Username = Environment.GetEnvironmentVariable("mqttUserName"),
                Password = Environment.GetEnvironmentVariable("mqttPassword"),
                BaseAddress = Environment.GetEnvironmentVariable("mqttAddress")
            };

            if (string.IsNullOrEmpty(haMqttOptions.Username))
                throw new NullReferenceException(nameof(haMqttOptions.Username));

            if (string.IsNullOrEmpty(haMqttOptions.Password))
                throw new NullReferenceException(nameof(haMqttOptions.Password));

            if (string.IsNullOrEmpty(haMqttOptions.BaseAddress))
                throw new NullReferenceException(nameof(haMqttOptions.BaseAddress));

            var rocketLaunchLiveApiOptions = new RocketLaunchLiveAPIClientOptions()
            {
                ApiKey = Environment.GetEnvironmentVariable("RocketLaunchLiveAPIKey"),
                BaseUrl = "https://fdo.rocketlaunch.live"
            };

            if (string.IsNullOrEmpty(rocketLaunchLiveApiOptions.ApiKey))
                throw new NullReferenceException(nameof(rocketLaunchLiveApiOptions.ApiKey));

            var httpClient = new HttpClient();

            var rocketJobOptions = new RocketLaunchLiveApiToHAMqttOptions()
            {
                Topic = "/Bard/Launch"
            };

            services.AddSingleton<IMqttOptions, HomeAssistantMqttOptions>(provider => haMqttOptions)
              .AddSingleton<MqttFactory, MqttFactory>(provider => new MqttFactory())
              .AddTransient<IMqttClient, MqttClient>()
              .AddSingleton<HttpClient, HttpClient>(provider => httpClient)
              .AddSingleton<IRocketLaunchLiveAPIClientOptions, RocketLaunchLiveAPIClientOptions>(provider => rocketLaunchLiveApiOptions)
              .AddSingleton<IRocketLaunchLiveAPIClient, RocketLaunchLiveAPIClient>()
              .AddSingleton<IHostedService, RocketLaunchLiveApiToHAMqtt>()
              .AddSingleton<RocketLaunchLiveApiToHAMqttOptions,RocketLaunchLiveApiToHAMqttOptions>(provider => rocketJobOptions)
              .AddHostedService<RocketLaunchLiveApiToHAMqtt>();
        }

        public void Configure(IApplicationBuilder app,
           Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
        }
    }
}
