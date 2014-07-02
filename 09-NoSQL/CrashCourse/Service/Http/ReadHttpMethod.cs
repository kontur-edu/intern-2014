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
        private readonly IGroboSerializer groboSerializer;

        public ReadHttpMethod(Disk disk, IGroboSerializer groboSerializer)
        {
            this.disk = disk;
            this.groboSerializer = groboSerializer;
        }

        #region IHttpMethod Members

        public void Process(HttpContext context)
        {
            var readParameters = groboSerializer.Deserialize<ReadParameters>(context.Request.Body);
            var result = disk.Read(readParameters.Key);
            context.Response.BodyStream.WriteBytes(groboSerializer.Serialize(result));
        }

        #endregion
    }
}