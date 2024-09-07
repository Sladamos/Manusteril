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
        void AskForRegistration(WardType ward, string pesel);
        void ChangePatientRoom(IPatientWardRoomChangedMessage message);
        void MarkVisitAsFinished(IPatientAllowedToLeaveMessage message);
        void MarkVisitAsInProgress(IPatientVisitArrivedMessage message);
        void UnregisterPatientByPesel(string pesel);
    }
}
