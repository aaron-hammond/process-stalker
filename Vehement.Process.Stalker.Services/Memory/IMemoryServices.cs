using System.Collections.Generic;
using Vehement.Process.Stalker.Services.Common;

namespace Vehement.Process.Stalker.Services.Memory
{
    public interface IMemoryServices
    {
        List<ProcessInformation> GetProcesses();

        List<ProcessInformation> GetProcesses(string wildCard);
    }
}