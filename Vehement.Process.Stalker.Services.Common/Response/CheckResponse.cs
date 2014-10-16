using System.Collections.Generic;

namespace Vehement.Process.Stalker.Services.Common.Response
{
    public class CheckResponse : IResponse
    {
        public List<ProcessInformation> Processes { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}