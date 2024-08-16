using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Bus
{
    internal class RabbitMqBusInstance : IBusInstance
    {

        private IBusControl bus;

        public RabbitMqBusInstance(IBusControl bus)
        {
            this.bus = bus;
        }

        internal static IBusInstance createWithConfig(Action<IRabbitMqBusFactoryConfigurator> configBusInstance)
        {
            IBusControl busControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(configBusInstance);
            return new RabbitMqBusInstance(busControl);
        }

        public async Task Publish(object message)
        {
            await this.bus.Publish(message);
        }

        public async Task Start()
        {
            await bus.StartAsync();
        }
    }
}
