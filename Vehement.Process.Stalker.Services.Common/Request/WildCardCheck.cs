using ServiceStack;
using Vehement.Process.Stalker.Services.Common.Response;

namespace Vehement.Process.Stalker.Services.Common.Request
{
    public class WildCardCheck : IReturn<CheckResponse>
    {
        public string WildCard { get; set; }
    }
}