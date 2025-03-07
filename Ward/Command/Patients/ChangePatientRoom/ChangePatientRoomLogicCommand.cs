﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Ward.Room;
using Ward.Visit;

namespace Ward.Command.Patients.ChangePatientRoom
{
    internal class ChangePatientRoomLogicCommand : ICommand
    {
        private IRoomService roomService;

        private IPatientService patientService;

        private IVisitService visitService;

        private Func<string> peselSupplier;

        private Func<string> roomSupplier;

        public string Name => "Przenieś";

        public string Description => "Przenieś pacjenta do innej sali";

        public Action? OnPatientTransfered;

        public ChangePatientRoomLogicCommand(IRoomService roomService,
            IPatientService patientService,
            IVisitService visitService,
            Func<string> peselSupplier,
            Func<string> roomSupplier)
        {
            this.roomService = roomService;
            this.patientService = patientService;
            this.visitService = visitService;
            this.peselSupplier = peselSupplier;
            this.roomSupplier = roomSupplier;
        }

        public async Task Execute()
        {
            string pesel = peselSupplier();
            string room = roomSupplier();
            try
            {
                var patient = patientService.GetPatientByPesel(pesel);
                if (visitService.IsPatientRegisteredInWard(pesel))
                {
                    roomService.TransferPatientToRoom(patient, room);
                    visitService.UpdateVisitRoom(patient, room);
                }
                OnPatientTransfered?.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
