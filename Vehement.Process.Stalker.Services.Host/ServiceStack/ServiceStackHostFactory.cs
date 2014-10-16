using System.Configuration;

namespace Vehement.Process.Stalker.Services.Host.ServiceStack
{
    public class ServiceStackHostFactory : IServiceStackHostFactory
    {
        public ServiceStackHost CreateHost(string serviceType)
        {
            ServiceStackHost host = null;

            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ProcessCheckEndPoint"]))
            {
                host = new ProcessCheckServiceHost("Vehement Process Stalker",
                    ConfigurationManager.AppSettings["ProcessCheckEndPoint"]);
            }

            return host;
        }
    }
}