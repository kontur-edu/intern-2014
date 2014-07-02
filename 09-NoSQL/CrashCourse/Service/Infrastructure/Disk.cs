using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Client;
using Client.Parameters;
using SKBKontur.WebPersonal.Core.ApplicationCommon;

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

        public void Write(string key, Data value)
        {
            lock (storage)
            {
                if (storage.ContainsKey(key) && value.CompareTo(storage[key]) > 0)
                    storage[key] = value;
            }
        }
    }

    public class Replicator
    {
        private readonly IServiceClient serviceClient;
        private readonly IPEndPoint[] endpoints;

        public Replicator(IServiceClient serviceClient, IApplicationSettings applicationSettings)
        {
            endpoints = applicationSettings.GetIPEndpointsArray("endpoints");
            this.serviceClient = serviceClient;
        }

        public void Replicate(string key, Data value)
        {
            Task.Run(() => ReplicateInternal(key, value));
        }

        private void ReplicateInternal(string key, Data value)
        {
            foreach (var endPoint in endpoints)
            {
                Thread.Sleep(5000);
                serviceClient.Write(key, value, endPoint);
            }
        }
    }
}