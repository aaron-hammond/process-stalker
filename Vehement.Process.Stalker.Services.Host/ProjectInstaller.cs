using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Vehement.Process.Stalker.Services.Host
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private const string ServiceTypeKey = "ServiceType";
        private const string ServiceTypeDefault = "NotSpecified";

        private readonly ServiceProcessInstaller processInstaller;
        private readonly ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            // Service will run under system account
            processInstaller.Account = ServiceAccount.LocalSystem;

            // Service will have Start Type of Manual
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }


        protected override void OnBeforeInstall(IDictionary savedState)
        {
            string serverId = Context.Parameters.ContainsKey(ServiceTypeKey)
                ? Context.Parameters[ServiceTypeKey]
                : ServiceTypeDefault;

            SetServiceName(serverId);

            base.OnBeforeInstall(savedState);
        }

        private void SetServiceName(string serviceType)
        {
            if (serviceInstaller.ServiceName.EndsWith(serviceType))
            {
                return;
            }

            serviceInstaller.ServiceName = "Vehement." + serviceType;
            serviceInstaller.DisplayName = serviceInstaller.ServiceName;
            serviceInstaller.Description =
                "Api Host for Rest Endpoints";
        }

        public override void Install(IDictionary stateSaver)
        {
            string env = Context.Parameters.ContainsKey(ServiceTypeKey)
                ? Context.Parameters[ServiceTypeKey]
                : ServiceTypeDefault;
            stateSaver.Add(ServiceTypeKey, env);
            base.Install(stateSaver);
        }

        protected override void OnBeforeRollback(IDictionary savedState)
        {
            string env = savedState.Contains(ServiceTypeKey)
                ? savedState[ServiceTypeKey].ToString()
                : ServiceTypeDefault;
            SetServiceName(env);
            base.OnBeforeRollback(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            string env;
            if (savedState != null && savedState.Contains(ServiceTypeKey))
            {
                env = savedState[ServiceTypeKey].ToString();
                SetServiceName(env);
            }
            else if (Context.Parameters.ContainsKey(ServiceTypeKey))
            {
                env = Context.Parameters[ServiceTypeKey];
                SetServiceName(env);
            }

            base.OnBeforeUninstall(savedState);
        }
    }
}