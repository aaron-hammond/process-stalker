using System;
using System.Configuration;
using ServiceStack.Text;
using Vehement.Process.Stalker.Core.ServiceProcesses;

namespace Vehement.Process.Stalker.Services.Host.ServiceStack
{
    public class ServiceStackManager : IWindowsServiceManager
    {
        private readonly IServiceStackHostFactory hostFactory;
        private ServiceStackHost host;

        public ServiceStackManager(IServiceStackHostFactory hostFactory)
        {
            this.hostFactory = hostFactory;
        }

        public void Start()
        {
            ServiceStackTextMessageConfiguration();

            var serviceType = ConfigurationManager.AppSettings[ServiceType.ServiceTypeConfig];
            host = hostFactory.CreateHost(serviceType);
            if (host == null)
            {
                throw new NotSupportedException(
                    string.Format("Cant create the service host! Service type -{0} is not supported!", serviceType));
            }

            host.Init();
            host.Start();
        }

        public void Stop()
        {
            if (host != null)
            {
                host.Stop();
            }
        }

        private void ServiceStackTextMessageConfiguration()
        {
            JsConfig.EmitCamelCaseNames = true;
            JsConfig.IncludeNullValues = false;
        }
    }
}