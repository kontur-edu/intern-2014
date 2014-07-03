using System.Net;
using Client.Parameters;
using SKBKontur.WebPersonal.Core.Logging.RequestLoggers;
using SKBKontur.WebPersonal.Core.Networking.ClientSide.Http;
using SKBKontur.WebPersonal.Core.Networking.ServerSide.Http;
using SKBKontur.WebPersonal.Core.Serialization;
using SKBKontur.WebPersonal.Core.Common;
using IHttpRequest = SKBKontur.WebPersonal.Core.Networking.ClientSide.Http.IHttpRequest;

namespace Client
{
    public class ServiceProxy : IServiceProxy
    {
        private readonly IJsonSerializer jsonSerializer;
        private readonly IIpRemoteHttpServer remoteHttpServer;

        public ServiceProxy(IPEndPoint endPoint, IJsonSerializer jsonSerializer)
        {
            this.jsonSerializer = jsonSerializer;
            var requestTracker = new RequestTracker(new GuidFactory());
            remoteHttpServer = new IpRemoteHttpServerWithLogger(endPoint, 1000, requestTracker, new OuterHttpMethodsLogger(requestTracker));
        }

        public void Write(string key, Data value)
        {
            IHttpRequest request = remoteHttpServer.CreatePostRequest("Write");
            request.BodyStream.WriteBytes(jsonSerializer.Serialize(new WriteParameters { Key = key, Value = value }));
            request.SendOrDie();
        }

        public Data Read(string key)
        {
            IHttpRequest request = remoteHttpServer.CreatePostRequest("Read");
            request.BodyStream.WriteBytes(jsonSerializer.Serialize(new ReadParameters { Key = key }));
            var response = request.SendOrDie();
            return jsonSerializer.Deserialize<Data>(response.BodyStream.ReadToEnd());
        }
    }
}
