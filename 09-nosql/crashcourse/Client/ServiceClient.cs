using System;
using System.Net;
using Client.Parameters;
using SKBKontur.WebPersonal.Core.Serialization;

namespace Client
{
    public class ServiceClient : IServiceClient
    {
        private readonly Func<IPEndPoint, IServiceProxy> createProxy;


        public ServiceClient()
        {
            var jsonSerializer = new JsonSerializer();
            createProxy = ipEndPoint => new ServiceProxy(ipEndPoint, jsonSerializer);
        }

        public void Write(string key, Data value, IPEndPoint endPoint)
        {
            createProxy(endPoint).Write(key, value);
        }

        public Data Read(string key, IPEndPoint endPoint)
        {
            return createProxy(endPoint).Read(key);
        }
    }
}