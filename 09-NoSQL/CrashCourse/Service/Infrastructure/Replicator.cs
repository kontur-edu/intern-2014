using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Client;
using Client.Parameters;
using SKBKontur.WebPersonal.Core.ApplicationCommon;
using log4net;

namespace Service.Infrastructure
{
    public class Replicator
    {
        private readonly IServiceClient serviceClient;
        private readonly IPEndPoint[] endpoints;
        private static ILog log = LogManager.GetLogger(typeof (Replicator));

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
                WriteToSingleReplica(key, value, endPoint);
        }

        private void WriteToSingleReplica(string key, Data value, IPEndPoint endPoint)
        {
            log.InfoFormat("Write '{1}':'{2}',{3} to {0}", endPoint, key, value.Value, value.Version);
            Task.Delay(5000).ContinueWith(t =>
                {
                    try
                    {
                        serviceClient.Write(key, value, endPoint);
                        log.InfoFormat("Write success '{1}':'{2}',{3} to {0}", endPoint, key, value.Value, value.Version);
                    }
                    catch (Exception)
                    {
                        log.WarnFormat("Write error '{1}':'{2}',{3} to {0}", endPoint, key, value.Value, value.Version);
                        WriteToSingleReplica(key, value, endPoint);
                    }
                });
        }
    }
}