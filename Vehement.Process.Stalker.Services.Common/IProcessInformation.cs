namespace Vehement.Process.Stalker.Services.Common
{
    public interface IProcessInformation
    {
        long WorkingSet64 { get; }
        long PrivateMemorySize64 { get; }
    }
}