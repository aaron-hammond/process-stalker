using System.Configuration;

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory.Config
{
    [ConfigurationCollection(typeof (ProcessCheckElement), AddItemName = "ProcessCheck",
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ProcessCheckCollection : ConfigurationElementCollection
    {
        public ProcessCheckElement this[int index]
        {
            get { return (ProcessCheckElement) BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(ProcessCheckElement serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProcessCheckElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProcessCheckElement) element).ProcessName;
        }

        public void Remove(ProcessCheckElement serviceConfig)
        {
            BaseRemove(serviceConfig.ProcessName);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}