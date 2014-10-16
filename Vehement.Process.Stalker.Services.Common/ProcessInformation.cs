namespace Vehement.Process.Stalker.Services.Common
{
    public class ProcessInformation : IProcessInformation
    {
        public long WorkingSet64 { get; set; }

        public long PrivateMemorySize64 { get; set; }

        public string ProcessName { get; set; }

        public string ExecutingLocation { get; set; }
    }
}