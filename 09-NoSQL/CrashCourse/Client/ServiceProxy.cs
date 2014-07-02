using System.Net;
using Client.Parameters;
using GroboContainer.Core;
using SKBKontur.WebPersonal.Core.Networking.ClientSide.Http;
using SKBKontur.WebPersonal.Core.Serialization;
using SKBKontur.WebPersonal.Core.Common;

namespace Client
{
    public class ServiceProxy : IServiceProxy
    {
        private readonly IIpRemoteHttpServer remoteHttpServer;
        private readonly IGroboSerializer groboSerializer;

        public ServiceProxy(IPEndPoint endPoint, IGroboSerializer groboSerializer, IContainer container)
        {
            remoteHttpServer = container.Create<IPEndPoint, int, IIpRemoteHttpServer>(endPoint, 1000);
            this.groboSerializer = groboSerializer;
        }

        #region IPrintServiceProxy Members

        #endregion

        public void Write(string key, Data value)
        {
            IHttpRequest request = remoteHttpServer.CreatePostRequest("Write");
            request.BodyStream.WriteBytes(groboSerializer.Serialize(new WriteParameters{Key = key, Value = value}));
            request.SendOrDie();
        }

        public Data Read(string key)
        {
            IHttpRequest request = remoteHttpServer.CreateGetRequest("Read");
            request.BodyStream.WriteBytes(groboSerializer.Serialize(new ReadParameters{Key = key}));
            var response = request.SendOrDie();
            return groboSerializer.Deserialize<Data>(response.BodyStream.ReadToEnd());
        }
    }
}
