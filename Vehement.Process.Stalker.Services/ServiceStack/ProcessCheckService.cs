using Vehement.Process.Stalker.Services.Common.Request;
using Vehement.Process.Stalker.Services.Common.Response;
using Vehement.Process.Stalker.Services.Memory;

namespace Vehement.Process.Stalker.Services.ServiceStack
{
    public class ProcessCheckService
    {
        private readonly IMemoryServices memoryServices;

        public ProcessCheckService(IMemoryServices memoryServices)
        {
            this.memoryServices = memoryServices;
        }

        public object Get(Check check)
        {
            var response = new CheckResponse
            {
                Processes = memoryServices.GetProcesses()
            };

            return response;
        }

        public object Get(WildCardCheck check)
        {
            var response = new CheckResponse
            {
                Processes = memoryServices.GetProcesses(check.WildCard)
            };

            return response;
        }
    }

}