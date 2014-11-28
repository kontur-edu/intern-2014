using GroboContainer.Core;
using SKBKontur.WebPersonal.Core.Common;
using SKBKontur.WebPersonal.Core.Networking.HttpServers;

namespace Service
{
    internal class EntryPoint
    {
        private static void Main(string[] arguments)
        {
            ServiceUtils.SetExceptionHandler();
            ServiceUtils.ConfugureLog4Net();

            IContainer container =
                ServiceUtils.ConfigureContainer(arguments[0]);
            ServiceLocator.Create(container);
            var service = container.Get<HttpService>();
            service.Start(arguments);
        }
    }
}
