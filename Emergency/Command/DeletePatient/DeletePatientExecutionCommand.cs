using Emergency.Bus;
using Emergency.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.DeletePatient
{
    internal class DeletePatientExecutionCommand : ICommand
    {
        private Func<string> peselSupplier;

        private readonly IBusInstance busInstance;

        public DeletePatientExecutionCommand(Func<string> peselSupplier, IBusInstance busInstance) {
            this.peselSupplier = peselSupplier;
            this.busInstance = busInstance;
        }

        public string Name => "Wypisz";

        public string Description => "Wypisz wybranego pacjenta";

        public void Execute()
        {
            string pesel = peselSupplier();
            Console.WriteLine("TODO: find patient by pesel and switch id");
            Guid patientId = Guid.NewGuid();
            PatientUnregisteredMessage message = new PatientUnregisteredMessage{ patientId = patientId };
            busInstance.Publish(message);
        }

    }
}
