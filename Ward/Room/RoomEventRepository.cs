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
using Ward.Patient;
using Ward.Visit;

namespace Ward.Room
{
    internal class RoomEventRepository : IRoomEventRepository
    {
        private IBusInstance busInstance;

        private ILog logger;

        private string wardIdentifier;

        public RoomEventRepository(IBusOperator busOperator, ILog logger, WardConfig wardConfig)
        {
            busInstance = busOperator.CreateBusInstance();
            this.logger = logger;
            this.wardIdentifier = wardConfig.WardIdentifier;
        }

        public void NotifyAboutTransfer(PatientEntity patient, RoomEntity room)
        {
            PatientRoomChangedMessage patientRoomChangedMessage = new()
            {
                PatientId = patient.Id,
                PatientPesel = patient.Pesel,
                Room = room.Number,
                WardIdentifier = wardIdentifier,
            };
            logger.Info($"Wysłanie informacji o zmianie sali przez pacjenta {patient}");
            busInstance.Publish(patientRoomChangedMessage);
        }
    }
}
