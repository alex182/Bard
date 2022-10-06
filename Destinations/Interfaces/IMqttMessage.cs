using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Destinations.Interfaces
{
    public interface IMqttMessage
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
    }
}
