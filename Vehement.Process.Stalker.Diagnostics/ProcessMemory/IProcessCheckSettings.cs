#region

using Vehement.Process.Stalker.Diagnostics.ProcessMemory.Config;

#endregion

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory
{
    public interface IProcessCheckSettings
    {
        ProcessCheckCollection Settings { get; }
    }
}