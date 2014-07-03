using System.Collections.Generic;
using Client.Parameters;

namespace Service.Infrastructure
{
    public class Disk
    {
        private readonly IDictionary<string, Data> storage;

        public Disk()
        {
            storage = new Dictionary<string, Data>();
        }

        public Data Read(string key)
        {
            lock (storage)
            {
                return storage.ContainsKey(key) ? storage[key] : null;
            }
        }

        public bool Write(string key, Data value)
        {
            lock (storage)
            {
                if (!storage.ContainsKey(key) || value.CompareTo(storage[key]) > 0)
                {
                    storage[key] = value;
                    return true;
                }
            }
            return false;
        }
    }
}