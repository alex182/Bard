using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Destinations.Interfaces
{
    public interface IMqttOptions
    {
        public string BaseAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
