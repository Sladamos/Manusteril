using NfzMock.Bus;
using NfzMock.Command;
using NfzMock.Command.Executioner;
using NfzMock.Command.Factory;
using NfzMock.Config;
using log4net;
using Microsoft.Extensions.Configuration;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NfzMock.Middleware;

namespace NfzMock
{
    internal class Bindings : NinjectModule
    {
        public override void Load()
        {
            var config = ParseConfig();
            BusConfig busConfig = config.GetSection("Bus").Get<BusConfig>()!;
            Bind<BusConfig>().ToConstant(busConfig).InSingletonScope();
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(typeof(Program))).InSingletonScope();
            CreateMiddlewares();
            Bind<IBusOperator>().To<RabbitMqBusOperator>().InSingletonScope();
            Bind<ICommandsExecutioner>().To<CommandsExecutioner>().InSingletonScope();
            Bind<ICommandsFactory>().To<CommandsFactory>().InSingletonScope();
            Bind<IMenu>().To<Menu>().InSingletonScope();
        }

        private void CreateMiddlewares()
        {
            Bind<IMiddleware>().To<ErrorHandlingMiddleware>().InSingletonScope();
            Bind<MiddlewaresWrapper>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("middlewares", ctx => ctx.Kernel.GetAll<IMiddleware>().ToList());
        }

        private IConfigurationRoot ParseConfig()
        {
            return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }
    }
}
