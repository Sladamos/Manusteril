using Emergency.Bus;
using Emergency.Command;
using Emergency.Command.Executioner;
using Emergency.Command.Factory;
using Emergency.Config;
using Emergency.Middleware;
using Emergency.Patient;
using Emergency.Validator;
using Emergency.Visit;
using log4net;
using Messages;
using Microsoft.EntityFrameworkCore;
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
            DbConfig dbConfig = config.GetSection("Db").Get<DbConfig>()!;
            Bind<BusConfig>().ToConstant(busConfig).InSingletonScope();
            Bind<ProdConfig>().ToConstant(prodConfig).InSingletonScope();
            Bind<DbConfig>().ToConstant(dbConfig).InSingletonScope();
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(typeof(Program))).InSingletonScope();
            CreateMiddlewares();
            CreateConsumers();
            Bind<ApplicationDbContext>().ToSelf();
            Bind<IBusOperator>().To<RabbitMqBusOperator>().InSingletonScope();
            Bind<IVisitRepository>().To<VisitRepository>().InSingletonScope();
            Bind<IPatientRepository>().To<PatientRepository>().InSingletonScope();
            Bind<IPatientEventRepository>().To<PatientEventRepository>().InSingletonScope();
            Bind<IVisitEventRepository>().To<VisitEventRepository>().InSingletonScope();
            Bind<IValidatorService>().To<ValidatorService>().InSingletonScope();
            Bind<IPatientService>().To<PatientService>().InSingletonScope();
            Bind<IVisitService>().To<VisitService>().InSingletonScope();
            Bind<ICommandsExecutioner>().To<CommandsExecutioner>().InSingletonScope();
            Bind<ICommandsFactory>().To<CommandsFactory>().InSingletonScope();
            Bind<Initializer>().ToSelf();
            Bind<IMenu>().To<Menu>().InSingletonScope();
        }

        private void CreateConsumers()
        {
            Bind<IBusConsumer<IPatientAllowedToLeave>>().To<PatientAllowedToLeaveHandler>().InSingletonScope();
            Bind<IBusConsumer<IPatientWardChanged>>().To<PatientWardChangedHandler>().InSingletonScope();
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
