using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Bus
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

        public IBusClient<TRequest> GetClient<TRequest>() where TRequest : class
        {
            return new RabbitMqBusClient<TRequest>(bus.CreateRequestClient<TRequest>());
        }

        public async Task Publish(object message)
        {
            await bus.Publish(message);
        }

        public async Task Start()
        {
            await bus.StartAsync();
        }

        public async Task Stop()
        {
            await bus.StopAsync();
        }
    }
}
