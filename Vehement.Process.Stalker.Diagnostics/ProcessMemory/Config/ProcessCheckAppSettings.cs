using System.Configuration;

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory.Config
{
    public class ProcessCheckAppSettings : ConfigurationSection
    {
        [ConfigurationProperty("ProcessChecks", Options = ConfigurationPropertyOptions.IsRequired)]
        public ProcessCheckCollection ProcessMonitorSettings
        {
            get { return (ProcessCheckCollection) this["ProcessChecks"]; }
        }
    }
}