using Ward.Bus;
using Ward.Command;
using Ward.Command.Executioner;
using Ward.Command.Factory;
using Ward.Config;
using Ward.Middleware;
using Ward.Patient;
using Ward.Validator;
using Ward.Visit;
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
using Ward.Room;
using Ward.Handler;
using Ward.Visit.Question;

namespace Ward
{
    internal class Bindings : NinjectModule
    {
        public override void Load()
        {
            var config = ParseConfig();
            BusConfig busConfig = config.GetSection("Bus").Get<BusConfig>()!;
            ProdConfig prodConfig = config.GetSection("Production").Get<ProdConfig>()!;
            DbConfig dbConfig = config.GetSection("Db").Get<DbConfig>()!;
            WardConfig wardConfig = config.GetSection("Ward").Get<WardConfig>()!;
            Bind<BusConfig>().ToConstant(busConfig).InSingletonScope();
            Bind<ProdConfig>().ToConstant(prodConfig).InSingletonScope();
            Bind<DbConfig>().ToConstant(dbConfig).InSingletonScope();
            Bind<WardConfig>().ToConstant(wardConfig).InSingletonScope();
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(typeof(Program))).InSingletonScope();
            CreateMiddlewares();
            CreateConsumers();
            Bind<ApplicationDbContext>().ToSelf();
            Bind<IBusOperator>().To<RabbitMqBusOperator>().InSingletonScope();
            Bind<IValidatorService>().To<ValidatorService>().InSingletonScope();
            Bind<IVisitQuestionRepository>().To<VisitQuestionRepository>().InSingletonScope();
            Bind<IVisitQuestionService>().To<VisitQuestionService>().InSingletonScope();
            Bind<IVisitRepository>().To<VisitRepository>().InSingletonScope();
            Bind<IVisitService>().To<VisitService>().InSingletonScope();
            Bind<IPatientRepository>().To<PatientRepository>().InSingletonScope();
            Bind<IPatientService>().To<PatientService>().InSingletonScope();
            Bind<IRoomRepository>().To<RoomRepository>().InSingletonScope();
            Bind<IRoomService>().To<RoomService>().InSingletonScope();
            Bind<ICommandsExecutioner>().To<CommandsExecutioner>().InSingletonScope();
            Bind<ICommandsFactory>().To<CommandsFactory>().InSingletonScope();
            Bind<Initializer>().ToSelf().InSingletonScope();
            Bind<IMenu>().To<Menu>().InSingletonScope();
        }

        private void CreateConsumers()
        {
            Bind<IBusConsumer<INewPatientRegisteredMessage>>().To<NewPatientRegisteredHandler>().InSingletonScope();
            Bind<IBusConsumer<IPatientDataChangedMessage>>().To<PatientDataChangedHandler>().InSingletonScope();
            Bind<IBusConsumer<IPatientVisitAcceptedMessage>>().To<PatientVisitAcceptedHandler>().InSingletonScope();
            Bind<IBusConsumer<IPatientVisitRegisteredMessage>>().To<PatientVisitRegisteredHandler>().InSingletonScope();
            Bind<IBusConsumer<IPatientVisitUnregisteredMessage>>().To<PatientVisitUnregisteredHandler>().InSingletonScope();
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
