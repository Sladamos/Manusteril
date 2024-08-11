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
        public async Task<IBusInstance> CreateBusInstance()
        {
            return await RabbitMqBusInstance.createWithConfig(ConfigBusInstance);

        }

        private void ConfigBusInstance(IRabbitMqBusFactoryConfigurator sbc)
        {
            var uri = new Uri("rabbitmq://localhost/");
            sbc.Host(uri, h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        }
    }
}
