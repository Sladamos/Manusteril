using Emergency.Bus;
using Emergency.Command;
using Emergency.Config;
using Emergency.Middleware;
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
            CreateMiddlewares();
            Bind<IBusOperator>().To<RabbitMqBusOperator>();
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
