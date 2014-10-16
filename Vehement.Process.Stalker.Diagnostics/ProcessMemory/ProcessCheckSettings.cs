#region

using System.Configuration;
using Vehement.Process.Stalker.Diagnostics.ProcessMemory.Config;

#endregion

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory
{
    public class ProcessCheckSettings : IProcessCheckSettings
    {
        public ProcessCheckCollection Settings
        {
            get
            {
                var section = (ProcessCheckAppSettings) ConfigurationManager.GetSection("ProcessCheckAppSettings");
                return section.ProcessMonitorSettings;
            }
        }
    }
}