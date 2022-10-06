using Bard.Destinations.Concretes;
using Bard.Destinations.Interfaces;
using Bard.Destinations.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTnet;
using MQTTnet.Client;

namespace MqttUnitTests
{
    [TestClass]
    public class HomeAssistantMqtt
    {
        private IMqttOptions _mqttOptions;

        [TestInitialize]
        public void Setup()
        {
            _mqttOptions = new HomeAssistantMqttOptions()
            {
                Username = Environment.GetEnvironmentVariable("mqttUserName"),
                Password = Environment.GetEnvironmentVariable("mqttPassword"),
                BaseAddress = Environment.GetEnvironmentVariable("mqttAddress")
            };

            if (string.IsNullOrEmpty(_mqttOptions.Username))
                throw new NullReferenceException(nameof(_mqttOptions.Username));

            if (string.IsNullOrEmpty(_mqttOptions.Password))
                throw new NullReferenceException(nameof(_mqttOptions.Password));

            if (string.IsNullOrEmpty(_mqttOptions.BaseAddress))
                throw new NullReferenceException(nameof(_mqttOptions.BaseAddress));
        }

        [TestMethod]
        public async Task Test_Connect()
        {
            var factory = new MqttFactory();
            var destinationClient = new MqttDestinationClient(_mqttOptions, factory);
            Exception exception = null;

            try
            {
                await destinationClient.CreateMqttClient();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                Assert.IsNull(exception);
            }

        }

        [TestMethod]
        public async Task PublishMessage()
        {
            var factory = new MqttFactory();
            var destinationClient = new MqttDestinationClient(_mqttOptions, factory);
            var message = new MqttMessage()
            {
                Payload = $"Hello World! {DateTime.UtcNow}",
                Topic = "/Bard/Test"
            };

            var result = await destinationClient.SendMessage(message);

            Assert.AreEqual(MqttClientPublishReasonCode.Success, result.ReasonCode);
        }
    }
}