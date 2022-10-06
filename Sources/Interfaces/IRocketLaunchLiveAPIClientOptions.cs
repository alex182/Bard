using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Sources.Interfaces
{
    public interface IRocketLaunchLiveAPIClientOptions
    {
        string BaseUrl { get; set; }
        string ApiKey { get; set; }
    }
}
