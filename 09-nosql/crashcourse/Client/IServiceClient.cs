using System.Net;
using Client.Parameters;

namespace Client
{
    public interface IServiceClient
    {
        void Write(string key, Data value, IPEndPoint endPoint);
        Data Read(string key, IPEndPoint endPoint);
    }
}