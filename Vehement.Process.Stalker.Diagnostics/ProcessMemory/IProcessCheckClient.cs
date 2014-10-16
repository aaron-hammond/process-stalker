using Vehement.Process.Stalker.Services.Common.Request;
using Vehement.Process.Stalker.Services.Common.Response;

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory
{
    public interface IProcessCheckClient
    {
        CheckResponse Check(Check request);

        CheckResponse WildCardCheck(WildCardCheck request);
    }
}