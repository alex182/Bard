using Bard.Destinations.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Destinations.Concretes
{
    public class MqttDestinationClient : IMqttDestination
    {
        private readonly IMqttOptions _mqttOptions;
        private readonly MqttFactory _mqttFactory;

        public MqttDestinationClient(IMqttOptions mqttOptions, MqttFactory mqttFactory)
        {
            _mqttOptions = mqttOptions;
            _mqttFactory = mqttFactory;
        }

        public async Task<MqttClientPublishResult> SendMessage(IMqttMessage message)
        {
            var mqttClient = await CreateMqttClient();

            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(message.Topic)
                .WithPayload(message.Payload)
                .Build();

            var publishResult = await mqttClient.PublishAsync(mqttMessage, CancellationToken.None);

            await mqttClient.DisconnectAsync();

            mqttClient.Dispose();

            return publishResult;
        }

        internal async Task<IMqttClient> CreateMqttClient()
        {
            var client = _mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttOptions.BaseAddress)
                .WithCredentials(_mqttOptions.Username, _mqttOptions.Password)
                .Build();

            await client.ConnectAsync(mqttClientOptions, CancellationToken.None);

            return client; 
        }
    }
}
