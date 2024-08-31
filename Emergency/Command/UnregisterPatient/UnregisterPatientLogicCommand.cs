using Emergency.Bus;
using Emergency.Messages;
using Emergency.Patient;
using Emergency.Visit;
using log4net;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.DeletePatient
{
    internal class UnregisterPatientLogicCommand : ICommand
    {
        private IVisitService visitService;

        private Func<string> peselSupplier;

        public string Name => "Wypisz";

        public string Description => "Wypisz wybranego pacjenta";

        public Action? OnPatientDeleted;

        public UnregisterPatientLogicCommand(IVisitService visitService, Func<string> peselSupplier) {
            this.visitService = visitService;
            this.peselSupplier = peselSupplier;
        }

        public async Task Execute()
        {
            string pesel = peselSupplier();
            try
            {
                visitService.UnregisterPatientByPesel(pesel);
                OnPatientDeleted?.Invoke();
            } catch (InvalidPeselException) {
                Console.WriteLine("Brak pacjenta o podanym PESELu");
            } catch (UnregisteredPatientException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
