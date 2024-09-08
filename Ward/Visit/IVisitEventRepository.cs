using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Visit
{
    internal interface IVisitEventRepository
    {
        void AllowToLeave(VisitEntity visit);
        void ConfirmArrival(VisitEntity visit);
    }
}
