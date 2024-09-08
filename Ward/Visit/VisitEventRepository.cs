using Ward.Bus;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Visit
{
    internal class VisitEventRepository : IVisitEventRepository
    {
        private IBusInstance busInstance;

        private ILog logger;

        public VisitEventRepository(IBusOperator busOperator, ILog logger)
        {
            busInstance = busOperator.CreateBusInstance();
            this.logger = logger;
        }

        public void Register(VisitEntity visit)
        {
        }

        public void Unregister(VisitEntity visit)
        {
        }
    }
}
