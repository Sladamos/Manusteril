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
        bool IsPatientRegisteredInWard(string patientPesel);
        void MarkVisitAsAllowedToLeave(VisitEntity visit);
        void MarkVisitAsFinished(IPatientVisitUnregisteredMessage message);
        void SetRoomForPatient(PatientEntity patient, string number);
    }
}
