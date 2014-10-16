using System;
using System.Collections.Generic;
using System.Linq;
using Vehement.Process.Stalker.Services.Common;

namespace Vehement.Process.Stalker.Services.Memory
{
    public class MemoryServices : IMemoryServices
    {
        public List<ProcessInformation> GetProcesses()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            return GetProcessList(processes);
        }

        public List<ProcessInformation> GetProcesses(string wildCard)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();

            System.Diagnostics.Process[] wildcardProcesses =
                processes.Where(p => p.ProcessName.ToLower().Contains(wildCard.ToLower())).ToArray();

            return GetProcessList(wildcardProcesses);
        }

        private static List<ProcessInformation> GetProcessList(System.Diagnostics.Process[] processes)
        {
            var processList = new List<ProcessInformation>();

            foreach (System.Diagnostics.Process p in processes)
            {
                try
                {
                    processList.Add(new ProcessInformation
                    {
                        ProcessName = p.ProcessName,
                        PrivateMemorySize64 = p.PrivateMemorySize64,
                        WorkingSet64 = p.WorkingSet64,
                        ExecutingLocation = p.Modules[0].FileName
                    });
                }
                catch (Exception)
                {
                    //Suppress exception as some processes won't be accessible, 
                    //we can just ignore these
                }
            }

            return processList;
        }
    }
}