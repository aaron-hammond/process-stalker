using System;
using System.Linq;
using Vehement.Process.Stalker.Services.Common.Request;

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory
{
    public class ProcessMemoryCheck
    {
        private readonly IProcessCheckClient processCheckClient;
        private readonly IProcessCheckSettings processSettings;

        public ProcessMemoryCheck(IProcessCheckSettings processSettings, IProcessCheckClient processCheckClient)
        {
            this.processSettings = processSettings;
            this.processCheckClient = processCheckClient;
        }

        public void DoWildCardCheck(DateTime lastCheckTime)
        {
            var settings = processSettings.Settings;

            for (int i = 0; i < settings.Count; i++)
            {
                var setting = settings[i];

                var check = new WildCardCheck
                {
                    WildCard = setting.WildCard
                };

                long currentMemory = 0;

                var checkResponse = processCheckClient.WildCardCheck(check);
                var matchedProcessNames =
                    checkResponse.Processes.Where(
                        p =>
                            String.Equals(p.ProcessName, setting.ProcessName,
                                StringComparison.CurrentCultureIgnoreCase)).ToArray();

                if (matchedProcessNames.Any())
                {
                    var exactProcess =
                        matchedProcessNames.FirstOrDefault(p => p.ExecutingLocation == setting.ExecutingLocation);

                    if (exactProcess != null)
                    {
                        currentMemory = exactProcess.WorkingSet64;
                    }
                }


                if (currentMemory < setting.MaxMemory)
                {
                    continue;
                }

                throw new ProcessCheckExceededMaximumSetException();
            }
        }
    }
}