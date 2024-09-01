﻿using NfzMock.Config;
using MassTransit;
using MassTransit.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Bus
{
    internal class RabbitMqBusOperator : IBusOperator
    {
        private readonly BusConfig busConfig;

        private IBusInstance? busInstance;

        public RabbitMqBusOperator(BusConfig busConfig)
        {
            this.busConfig = busConfig;
        }

        public IBusClient<TRequest> CreateBusClient<TRequest>() where TRequest : class
        {
            return CreateBusInstance().GetClient<TRequest>();
        }

        public IBusInstance CreateBusInstance()
        {
            if (busInstance == null)
            {
                busInstance = RabbitMqBusInstance.createWithConfig(ConfigBusInstance);
            }
            return busInstance;
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
