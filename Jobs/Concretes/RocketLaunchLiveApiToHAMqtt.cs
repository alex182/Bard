using Bard.Destinations.Concretes;
using Bard.Destinations.Interfaces;
using Bard.Destinations.Models;
using Bard.Jobs.Models;
using Bard.Sources.Interfaces;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Jobs.Concretes
{
    public class RocketLaunchLiveApiToHAMqtt : BaseJob
    {
        private readonly IRocketLaunchLiveAPIClient _rocketLaunchLiveClient;
        private readonly IMqttClient _mqttClient;
        private readonly RocketLaunchLiveApiToHAMqttOptions _options;

        public RocketLaunchLiveApiToHAMqtt(IRocketLaunchLiveAPIClient rocketLaunchLiveClient,
            IMqttClient mqttClient, RocketLaunchLiveApiToHAMqttOptions options)
        {
            _rocketLaunchLiveClient = rocketLaunchLiveClient;
            _mqttClient = mqttClient;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.UtcNow;
                    var tomorrow = now.AddDays(1);

                    var afterDate = $"{now.Year}-{now.Month}-{now.Day}";
                    var beforeDate = $"{tomorrow.Year}-{tomorrow.Month}-{tomorrow.Day}";

                    var result = await _rocketLaunchLiveClient.GetLaunchesBetweenDates(afterDate, beforeDate);

                    if (result.IsSuccessStatusCode)
                    {
                        await _mqttClient.CreateMqttClient();

                        var message = new MqttMessage()
                        {
                            Payload = JsonConvert.SerializeObject(result),
                            Topic = _options.Topic
                        };

                        var publishResult = await _mqttClient.SendMessage(message);
                    }
                }
                catch (Exception ex)
                {
                    // need a logger eventually...
                }
                finally
                {
                    DateTime nextStop = DateTime.Now.AddDays(1);
                    var timeToWait = nextStop - DateTime.Now;
                    var millisToWait = timeToWait.TotalMilliseconds;

                    await Task.Delay((int)millisToWait, stoppingToken);
                }
            }
        }
    }
}
