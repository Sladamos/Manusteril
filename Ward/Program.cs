using Ward.Bus;
using Ward.Middleware;
using MassTransit;
using Ninject;
using Ninject.Modules;
using System.Runtime.CompilerServices;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
namespace Ward
{
    internal class Program
    {

        [ModuleInitializer]
        public static void Initialize()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        static async Task Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<Bindings>();
            MiddlewaresWrapper middlewares = kernel.Get<MiddlewaresWrapper>();
            await middlewares.execute(async () =>
            {   
                Initializer initializer = kernel.Get<Initializer>();
                initializer.Initialize();
                IMenu menu = kernel.Get<IMenu>();
                await menu.Start();
            });
        }
    }
}
