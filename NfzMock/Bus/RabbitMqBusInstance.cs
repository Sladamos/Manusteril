using log4net;
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

        private ILog logger;

        public RabbitMqBusInstance(IBusControl bus, ILog logger)
        {
            this.bus = bus;
            this.logger = logger;
        }

        internal static IBusInstance createWithConfig(Action<IRabbitMqBusFactoryConfigurator> configBusInstance, ILog logger)
        {
            IBusControl busControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(configBusInstance);
            return new RabbitMqBusInstance(busControl, logger);
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

        public void ConnectConsumer<TMessage>(IBusConsumer<TMessage> consumer) where TMessage : class
        {
            bus.ConnectReceiveEndpoint(consumer.QueueName, e =>
            {
                e.Instance(consumer);
            });
            logger.Info($"Dodano consumera: {consumer}");
        }
    }
}
