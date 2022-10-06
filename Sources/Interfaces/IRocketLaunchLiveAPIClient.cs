using Bard.Sources.Models.CommonApi;
using Bard.Sources.Models.RocketLaunchLive.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bard.Sources.Interfaces
{
    internal interface IRocketLaunchLiveAPIClient
    {
        Task<APIResultsWrapper<ResponseBody>> GetLaunchesBetweenDates(DateTime afterDate, DateTime beforeDate);
    }
}
