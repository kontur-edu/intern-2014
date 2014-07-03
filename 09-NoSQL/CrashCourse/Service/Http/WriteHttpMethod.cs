using Client.Parameters;
using SKBKontur.WebPersonal.Core.Networking.ServerSide.Http;
using SKBKontur.WebPersonal.Core.Serialization;
using Service.Infrastructure;

namespace Service.Http
{
    public class WriteHttpMethod : IHttpMethod
    {
        private readonly Disk disk;
        private readonly IJsonSerializer jsonSerializer;
        private readonly Replicator replicator;

        public WriteHttpMethod(Disk disk, IJsonSerializer jsonSerializer, Replicator replicator)
        {
            this.disk = disk;
            this.jsonSerializer = jsonSerializer;
            this.replicator = replicator;
        }

        #region IHttpMethod Members

        public void Process(HttpContext context)
        {
            var writeParameters = jsonSerializer.Deserialize<WriteParameters>(context.Request.Body);
            if (disk.Write(writeParameters.Key, writeParameters.Value))
                replicator.Replicate(writeParameters.Key, writeParameters.Value);
        }

        #endregion
    }
}