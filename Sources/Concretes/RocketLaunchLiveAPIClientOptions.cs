using Bard.Sources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Sources.Concretes
{
    public class RocketLaunchLiveAPIClientOptions : IRocketLaunchLiveAPIClientOptions
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
    }
}
