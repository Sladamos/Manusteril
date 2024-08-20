using Emergency.Bus;
using Emergency.Command;
using Emergency.Config;
using Microsoft.Extensions.Configuration;
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
            Bind<BusConfig>().ToConstant(busConfig);
            Bind<ExitProgramCommand>().ToConstant(new ExitProgramCommand());
            Bind<CheckInsuranceCommand>().ToConstant(new CheckInsuranceCommand());
            Bind<DeletePatientCommand>().ToConstant(new DeletePatientCommand());
            Bind<AddPatientCommand>().ToConstant(new AddPatientCommand());
            Bind<IBusOperator>().To<RabbitMqBusOperator>();
            Bind<IMenu>().To<Menu>();
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
