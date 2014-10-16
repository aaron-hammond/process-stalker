using System.Configuration;
using System.ServiceProcess;
using Vehement.Process.Stalker.Core.ServiceProcesses;

namespace Vehement.Process.Stalker.Services.Host
{
    public partial class ApiHostService : ServiceBase
    {
       private readonly IWindowsServiceManager serviceManager;

       public ApiHostService(IWindowsServiceManager serviceManager) 
        {
            this.serviceManager = serviceManager;
            InitializeComponent();

            ServiceName = "Vehement." + ConfigurationManager.AppSettings["ServiceType"];
        }

        protected override void OnStart(string[] args)
        {
            serviceManager.Start();
        }

        protected override void OnStop()
        {
            serviceManager.Stop();
        }
    }
}