namespace Vehement.Process.Stalker.Services.Host.ServiceStack
{
    public interface IServiceStackHostFactory
    {
        ServiceStackHost CreateHost(string serviceType);
    }
}