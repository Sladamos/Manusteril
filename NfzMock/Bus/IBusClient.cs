using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Bus
{
    internal interface IBusClient<TRequest> where TRequest : class
    {
        public Task<TResponse> GetResponse<TResponse>(TRequest request, TimeSpan timeout) where TResponse : class;
    }
}
