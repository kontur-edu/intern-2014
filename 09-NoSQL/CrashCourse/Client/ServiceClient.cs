using System;
using System.Net;
using Client.Parameters;

namespace Client
{
    public class ServiceClient : IServiceClient
    {
        private readonly Func<IPEndPoint, IServiceProxy> createProxy;

        public ServiceClient(Func<IPEndPoint, IServiceProxy> createProxy)
        {
            this.createProxy = createProxy;
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