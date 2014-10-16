using Funq;
using Vehement.Process.Stalker.Services.Common.Request;
using Vehement.Process.Stalker.Services.Memory;
using Vehement.Process.Stalker.Services.ServiceStack;

namespace Vehement.Process.Stalker.Services.Host.ServiceStack
{
    public class ProcessCheckServiceHost : ServiceStackHost
    {
        public ProcessCheckServiceHost(string name, string endPoint)
            : base(name, endPoint, typeof (ProcessCheckService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            Routes.Add<Check>("/check");
            Routes.Add<WildCardCheck>("/wildcardcheck");

            container.RegisterAutoWiredAs<MemoryServices, IMemoryServices>();
        }
    }
}