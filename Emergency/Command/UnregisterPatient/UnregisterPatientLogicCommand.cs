using Emergency.Bus;
using Emergency.Messages;
using Emergency.Patient;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.DeletePatient
{
    internal class UnregisterPatientLogicCommand : ICommand
    {
        private readonly IPatientService patientService;

        private Func<string> peselSupplier;

        public Action? OnPatientDeleted;

        public UnregisterPatientLogicCommand(IPatientService patientService, Func<string> peselSupplier) {
            this.patientService = patientService;
            this.peselSupplier = peselSupplier;
        }

        public string Name => "Wypisz";

        public string Description => "Wypisz wybranego pacjenta";

        public void Execute()
        {
            string pesel = peselSupplier();
            try
            {
                patientService.DeletePatientByPesel(pesel);
                OnPatientDeleted?.Invoke();
            } catch (InvalidPeselException _) {
                Console.WriteLine("Brak pacjenta o podanym PESELu");
            }
        }

    }
}
