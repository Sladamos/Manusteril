using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal interface IVisitEventRepository
    {
        void Unregister(VisitEntity visit);
        void Register(VisitEntity visit);
        void AskForRegistration(VisitEntity visit);
    }
}
