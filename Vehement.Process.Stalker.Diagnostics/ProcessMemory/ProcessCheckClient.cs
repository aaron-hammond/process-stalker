using System.Configuration;
using ServiceStack;
using Vehement.Process.Stalker.Services.Common.Request;
using Vehement.Process.Stalker.Services.Common.Response;

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory
{
    public class ProcessCheckClient
    {
        private readonly IRestClient serviceClientBase;

        public ProcessCheckClient()
        {
            serviceClientBase = new JsonServiceClient(ConfigurationManager.AppSettings["ProcessCheckEndPoint"]);
        }

        public ProcessCheckClient(IRestClient serviceClientBase)
        {
            this.serviceClientBase = serviceClientBase;
        }

        public CheckResponse Check(Check request)
        {
            var response = serviceClientBase.Get(request);

            return response;
        }

        public CheckResponse WildCardCheck(WildCardCheck request)
        {
            var response = serviceClientBase.Get(request);

            return response;
        }
    }
}