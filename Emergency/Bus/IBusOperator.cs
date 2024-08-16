using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Bus
{
    internal interface IBusOperator
    {
        IBusInstance CreateBusInstance();
    }
}
