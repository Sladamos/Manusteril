using Emergency.Bus;
using Emergency.Command;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using Emergency.Config;
using Emergency.Middleware;
using Emergency.Validator;
using log4net;
using Microsoft.Extensions.Configuration;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency
{
    internal class Bindings : NinjectModule
    {
        public override void Load()
        {
            var config = ParseConfig();
            BusConfig busConfig = config.GetSection("Bus").Get<BusConfig>()!;
            ProdConfig prodConfig = config.GetSection("Production").Get<ProdConfig>()!;
            Bind<BusConfig>().ToConstant(busConfig);
            Bind<ProdConfig>().ToConstant(prodConfig);
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(typeof(Program)));
            CreateMiddlewares();
            Bind<IBusOperator>().To<RabbitMqBusOperator>();
            Bind<IValidatorService>().To<ValidatorService>();
            Bind<ICommandsExecutioner>().To<CommandsExecutioner>();
            Bind<ICommandsFactory>().To<CommandsFactory>();
            Bind<IMenu>().To<Menu>();
        }

        private void CreateMiddlewares()
        {
            Bind<IMiddleware>().To<ErrorHandlingMiddleware>();
            Bind<MiddlewaresWrapper>()
                .ToSelf()
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
