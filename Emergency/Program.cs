using Emergency.Bus;
using Emergency.Messages;
using Emergency.Middleware;
using MassTransit;
using Ninject;
using Ninject.Modules;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace Emergency
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<Bindings>();
            MiddlewaresWrapper middlewares = kernel.Get<MiddlewaresWrapper>();
            middlewares.execute(() =>
            {
                IMenu menu = kernel.Get<IMenu>();
                menu.Start();
            });
        }
    }
}
