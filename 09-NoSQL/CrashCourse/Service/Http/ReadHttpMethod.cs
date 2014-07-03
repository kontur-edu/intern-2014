using Client.Parameters;
using SKBKontur.WebPersonal.Core.Common;
using SKBKontur.WebPersonal.Core.Networking.ServerSide.Http;
using SKBKontur.WebPersonal.Core.Serialization;
using Service.Infrastructure;

namespace Service.Http
{
    public class ReadHttpMethod : IHttpMethod
    {
        private readonly Disk disk;
        private readonly IJsonSerializer jsonSerializer;

        public ReadHttpMethod(Disk disk, IJsonSerializer jsonSerializer)
        {
            this.disk = disk;
            this.jsonSerializer = jsonSerializer;
        }

        #region IHttpMethod Members

        public void Process(HttpContext context)
        {
            var readParameters = jsonSerializer.Deserialize<ReadParameters>(context.Request.Body);
            var result = disk.Read(readParameters.Key);
            context.Response.BodyStream.WriteBytes(jsonSerializer.Serialize(result));
        }

        #endregion
    }
}