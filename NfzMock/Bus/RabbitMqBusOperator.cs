using NfzMock.Config;
using MassTransit;
using MassTransit.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace NfzMock.Bus
{
    internal class RabbitMqBusOperator : IBusOperator
    {
        private readonly BusConfig busConfig;

        private readonly ILog logger;

        private IBusInstance? busInstance;

        public RabbitMqBusOperator(BusConfig busConfig, ILog logger)
        {
            this.busConfig = busConfig;
            this.logger = logger;
        }

        public IBusClient<TRequest> CreateBusClient<TRequest>() where TRequest : class
        {
            return CreateBusInstance().GetClient<TRequest>();
        }

        public IBusInstance CreateBusInstance()
        {
            if (busInstance == null)
            {
                busInstance = RabbitMqBusInstance.createWithConfig(ConfigBusInstance, logger);
            }
            return busInstance;
        }

        public void RegisterConsumer<TMessage>(IBusConsumer<TMessage> consumer) where TMessage : class
        {
            CreateBusInstance().ConnectConsumer(consumer);
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
