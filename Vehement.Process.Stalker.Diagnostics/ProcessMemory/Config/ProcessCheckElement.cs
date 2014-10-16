using System.Configuration;

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory.Config
{
    public class ProcessCheckElement : ConfigurationElement
    {
        [ConfigurationProperty("processName", IsRequired = true, IsKey = true)]
        public string ProcessName
        {
            get { return (string)this["processName"]; }
            set { this["processName"] = value; }
        }

        [ConfigurationProperty("maxMemory", IsRequired = true)]
        public double MaxMemory
        {
            get { return (double) this["maxMemory"]; }
            set { this["maxMemory"] = value; }
        }

        [ConfigurationProperty("executingLocation", IsRequired = true)]
        public string ExecutingLocation
        {
            get { return (string)this["executingLocation"]; }
            set { this["executingLocation"] = value; }
        }

        [ConfigurationProperty("wildCard", IsRequired = true)]
        public string WildCard
        {
            get { return (string)this["wildCard"]; }
            set { this["wildCard"] = value; }
        }
    }
}