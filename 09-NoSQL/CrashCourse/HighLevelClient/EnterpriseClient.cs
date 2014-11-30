using System;
using System.Collections.Generic;
using System.Linq;
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
        private IPEndPoint ClienReplica;

        public EnterpriseClient(IPEndPoint[] endpoints)
        {
            this.endpoints = endpoints;
            internalClient = new ServiceClient();
        }

        public void Write(string key, string value)
        {
            int succesWrite = 0;
            foreach (IPEndPoint lPoint in endpoints)
            {
                if (succesWrite < endpoints.Count()%2 + 1)
                {
                    try
                    {

                        Data data = internalClient.Read(key, lPoint) ?? new Data();
                        internalClient.Write(key, new Data {Value = value, Version = data.Version + 1}, lPoint);
                        succesWrite++;
                    }
                    catch (Exception)
                    {
                        continue;
                        throw;
                    }
                }
            }
            
        }

        public string Read(string key)
        {
            var alreadyRead = new List<Tuple<string, long>>();
            foreach (IPEndPoint lPoint in endpoints)
            {
                try
                {
                    Data read = internalClient.Read(key, lPoint);
                    if (read != null)
                    {
                        alreadyRead.Add(Tuple.Create(read.Value, read.Version));
                    }
                }
                catch (Exception)
                {
                    continue;
                    throw;
                }
            }

            return alreadyRead
                .OrderBy(response => response.Item2)
                .Take(1)
                .ToArray()[0].Item1;
        }

        private IPEndPoint GetKeyByHash(string key, IPEndPoint[] endpoints)
        {
            return endpoints[Math.Abs(key.GetHashCode())%endpoints.Length];
        }

        private IPEndPoint GetBackupKeyByHash(string key, IPEndPoint[] endpoints)
        {
            return endpoints[((Math.Abs(key.GetHashCode()) % endpoints.Length) + 1) % endpoints.Length];
        }
    }
}