using log4net;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Bus;
using Ward.Config;
using Ward.Message;

namespace Ward.Visit
{
    internal class VisitEventRepository : IVisitEventRepository
    {
        private IBusInstance busInstance;

        private ILog logger;

        private string wardIdentifier;

        public VisitEventRepository(IBusOperator busOperator, ILog logger, WardConfig wardConfig)
        {
            busInstance = busOperator.CreateBusInstance();
            this.logger = logger;
            this.wardIdentifier = wardConfig.WardIdentifier;
        }

        public void AllowToLeave(VisitEntity visit)
        {
            PatientAllowedToLeaveMessage patientAllowedToLeaveMessage = new()
            {
                DoctorPwzNumber = visit.LeavePermissionDoctorPwz!,
                LeavedAtOwnRisk = (bool)visit.LeavedAtOwnRisk!,
                PatientId = visit.PatientId,
                PatientPesel = visit.PatientPesel
            };
            logger.Info($"Wysłanie informacji o wydaniu zgody na wypiskę pacjenta {visit.PatientPesel}");
            busInstance.Publish(patientAllowedToLeaveMessage);
        }

        public void ConfirmArrival(VisitEntity visit)
        {
            PatientVisitArrivedMessage patientVisitArrivedMessage = new()
            {
                VisitId = visit.Id,
                PatientPesel = visit.PatientPesel,
                PatientId = visit.PatientId,
                WardIdentifier = wardIdentifier,
                Room = visit.PatientRoomNumber
            };
            logger.Info($"Wysłanie informacji o przybyciu pacjenta {visit.PatientPesel}");
            busInstance.Publish(patientVisitArrivedMessage);
        }
    }
}
