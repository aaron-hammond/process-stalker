using System.Reflection;
using ServiceStack;

namespace Vehement.Process.Stalker.Services.Host.ServiceStack
{
    public abstract class ServiceStackHost : AppHostHttpListenerBase
    {
        protected readonly string EndPoint;
        protected readonly bool RequiredAuth;

        protected ServiceStackHost(string name, string endPoint, params Assembly[] assemblies)
            : base(name, assemblies)
        {
            EndPoint = endPoint;
            RequiredAuth = false;
            HostName = name;
        }

        public string HostName { get; private set; }

        public void Start()
        {
            base.Start(EndPoint);
        }
    }
}