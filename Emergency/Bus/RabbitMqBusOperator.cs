using Emergency.Config;
using MassTransit;
using MassTransit.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Bus
{
    internal class RabbitMqBusOperator : IBusOperator
    {
        private readonly BusConfig busConfig;

        public RabbitMqBusOperator(BusConfig busConfig)
        {
            this.busConfig = busConfig;
        }

        public IBusInstance CreateBusInstance()
        {
            return RabbitMqBusInstance.createWithConfig(ConfigBusInstance);

        }

        private void ConfigBusInstance(IRabbitMqBusFactoryConfigurator sbc)
        {
            var uri = new Uri(busConfig.Uri);
            sbc.Host(uri, h =>
            {
                h.Username(busConfig.Login);
                h.Password(busConfig.Password);
            });
        }
    }
}
