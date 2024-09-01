using MassTransit;
using MassTransit.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Bus
{
    internal class RabbitMqBusClient<TRequest> : IBusClient<TRequest> where TRequest : class
    {
        private readonly IRequestClient<TRequest> requestClient;

        public RabbitMqBusClient(IRequestClient<TRequest> requestClient)
        {
            this.requestClient = requestClient;
        }

        public async Task<TResponse> GetResponse<TResponse>(TRequest request, TimeSpan timeout) where TResponse : class
        {
            var response = await requestClient.GetResponse<TResponse>(request, default, timeout);
            return response.Message;
        }

    }
}
