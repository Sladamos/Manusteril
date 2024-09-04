using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal interface IVisitService
    {
        void AddVisit(VisitEntity visit);
        void ChangePatientWard(IPatientWardChanged message);
        void MarkVisitAsFinished(IPatientAllowedToLeave message);
        void UnregisterPatientByPesel(string pesel);
    }
}
