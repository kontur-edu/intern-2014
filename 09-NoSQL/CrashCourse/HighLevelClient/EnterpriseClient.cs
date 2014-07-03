using System;
using System.Net;
using Client;
using Client.Parameters;

namespace HighLevelClient
{
    public class EnterpriseClient
    {
        private static readonly Random random = new Random();
        private readonly IPEndPoint[] endpoints;
        private readonly ServiceClient internalClient;

        public EnterpriseClient(IPEndPoint[] endpoints)
        {
            this.endpoints = endpoints;
            internalClient = new ServiceClient();
        }

        private IPEndPoint NextReplica
        {
            get { return endpoints[random.Next(endpoints.Length)]; }
        }

        public void Write(string key, string value)
        {
            Data data = internalClient.Read(key, NextReplica) ?? new Data();
            internalClient.Write(key, new Data { Value = value, Version = data.Version + 1 }, NextReplica);
        }

        public string Read(string key)
        {
            Data read = internalClient.Read(key, NextReplica);
            return read == null ? null : read.Value;
        }
    }
}