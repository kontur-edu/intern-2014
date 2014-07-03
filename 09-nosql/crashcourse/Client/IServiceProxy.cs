using Client.Parameters;

namespace Client
{
    public interface IServiceProxy
    {
        void Write(string key, Data value);
        Data Read(string key);
    }
}