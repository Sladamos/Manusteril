using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;

namespace Ward.Visit
{
    internal interface IVisitService
    {
        VisitEntity GetPatientCurrentVisit(string pesel);
        void MarkVisitAsAllowedToLeave(VisitEntity visit);
        void SetRoomForPatient(PatientEntity patient, string number);
    }
}
