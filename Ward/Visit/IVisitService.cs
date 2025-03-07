﻿using Messages;
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
        void AddVisit(VisitEntity visit);
        VisitEntity GetPatientCurrentVisit(string pesel);
        bool IsPatientRegisteredInWard(string patientPesel);
        void MarkVisitAsAllowedToLeave(VisitEntity visit);
        void MarkVisitAsFinished(IPatientVisitUnregisteredMessage message);
        void MarkVisitAsInProgress(string pesel, string roomNumber);
        void UpdateVisitRoom(PatientEntity patient, string number);
    }
}
