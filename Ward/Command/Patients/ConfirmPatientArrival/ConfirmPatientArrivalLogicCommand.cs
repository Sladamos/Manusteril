using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Ward.Room;
using Ward.Visit;

namespace Ward.Command.Patients.ConfirmPatientArrival
{
    internal class ConfirmPatientArrivalLogicCommand : ICommand
    {
        private IVisitService visitService;
        
        private Func<string> peselSupplier;

        private Func<string> roomSupplier;

        public string Name => "Potwierdź";

        public string Description => "Potwierdź przybycie pacjenta";

        public Action? OnConfirmed;

        public ConfirmPatientArrivalLogicCommand(IVisitService visitService,
            Func<string> peselSupplier,
            Func<string> roomSupplier)
        {
            this.visitService = visitService;
            this.peselSupplier = peselSupplier;
            this.roomSupplier = roomSupplier;
        }

        public async Task Execute()
        {
            string pesel = peselSupplier();
            string roomNumber = roomSupplier();
            try
            {
                visitService.MarkVisitAsInProgress(pesel, roomNumber);
                OnConfirmed?.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
