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
using NfzMock.Patient;
using Messages;

namespace NfzMock
{
    internal class Bindings : NinjectModule
    {
        public override void Load()
        {
            var config = ParseConfig();
            BusConfig busConfig = config.GetSection("Bus").Get<BusConfig>()!;
            ProdConfig prodConfig = config.GetSection("Production").Get<ProdConfig>()!;
            MockConfig mockConfig = config.GetSection("Mock").Get<MockConfig>()!;
            Bind<BusConfig>().ToConstant(busConfig).InSingletonScope();
            Bind<ProdConfig>().ToConstant(prodConfig).InSingletonScope();
            Bind<MockConfig>().ToConstant(mockConfig).InSingletonScope();
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(typeof(Program))).InSingletonScope();
            CreateMiddlewares();
            CreateConsumers();
            Bind<IBusOperator>().To<RabbitMqBusOperator>().InSingletonScope();
            Bind<ICommandsExecutioner>().To<CommandsExecutioner>().InSingletonScope();
            Bind<ICommandsFactory>().To<CommandsFactory>().InSingletonScope();
            Bind<IMenu>().To<Menu>().InSingletonScope();
        }

        private void CreateConsumers()
        {
            Bind<IBusConsumer<IIsPatientInsuredMessage>>().To<IsPatientInsuredHandler>().InSingletonScope();
            Bind<ConsumersWrapper>().ToSelf();
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
