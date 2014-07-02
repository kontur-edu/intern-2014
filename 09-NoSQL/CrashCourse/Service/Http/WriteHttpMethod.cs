using Client.Parameters;
using GroBuf;
using SKBKontur.WebPersonal.Core.Networking.ServerSide.Http;
using SKBKontur.WebPersonal.Core.Serialization;
using Service.Infrastructure;

namespace Service.Http
{
    public class WriteHttpMethod : IHttpMethod
    {
        private readonly Disk disk;
        private readonly IGroboSerializer groboSerializer;
        private readonly Replicator replicator;

        public WriteHttpMethod(Disk disk, IGroboSerializer groboSerializer, Replicator replicator)
        {
            this.disk = disk;
            this.groboSerializer = groboSerializer;
            this.replicator = replicator;
        }

        #region IHttpMethod Members

        public void Process(HttpContext context)
        {
            var writeParameters = groboSerializer.Deserialize<WriteParameters>(context.Request.Body);
            disk.Write(writeParameters.Key, writeParameters.Value);
            replicator.Replicate(writeParameters.Key, writeParameters.Value);
        }

        #endregion
    }
}