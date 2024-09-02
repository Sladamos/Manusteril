using NfzMock.Middleware;
using Ninject;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace NfzMock
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<Bindings>();
            MiddlewaresWrapper middlewares = kernel.Get<MiddlewaresWrapper>();
            await middlewares.execute(async () =>
            {
                IMenu menu = kernel.Get<IMenu>();
                await menu.Start();
            });
        }
    }
}
