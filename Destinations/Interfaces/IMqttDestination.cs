using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Destinations.Interfaces
{
    public interface IMqttDestination
    {
        Task<MqttClientPublishResult> SendMessage(IMqttMessage message);
    }
}
