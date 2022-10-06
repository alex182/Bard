using Bard.Destinations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Destinations.Models
{
    public class MqttMessage : IMqttMessage
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
    }
}
