using System.Net;

namespace Bard.Sources.Models.CommonApi
{
    public interface IAPIResultsWrapper<T>
    {
        bool IsSuccessStatusCode { get; set; }
        T Result { get; set; }
        HttpStatusCode StatusCode { get; set; }
    }
}