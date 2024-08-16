using Emergency.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Bus
{
    internal interface IBusInstance
    {
        Task Publish(object patientUnregisteredMessage);
        Task Start();
    }
}
