using Bard.Destinations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Destinations.Models
{
    public class HomeAssistantMqttOptions : IMqttOptions
    {
        public string BaseAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
