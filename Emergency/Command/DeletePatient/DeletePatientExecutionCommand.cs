using Emergency.Bus;
using Emergency.Messages;
using log4net;
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

        private readonly ILog logger;

        public Action? OnPatientDeleted;

        public DeletePatientExecutionCommand(IBusOperator busOperator, ILog logger, Func<string> peselSupplier) {
            this.peselSupplier = peselSupplier;
            this.logger = logger;
            busInstance = busOperator.CreateBusInstance();
        }

        public string Name => "Wypisz";

        public string Description => "Wypisz wybranego pacjenta";

        public void Execute()
        {
            string pesel = peselSupplier();
            Console.WriteLine("TODO: validate pesel with validator service");
            Console.WriteLine("TODO: find patient by pesel and then switch id");
            Guid patientId = Guid.NewGuid();
            PatientUnregisteredMessage message = new PatientUnregisteredMessage{ patientId = patientId };
            logger.Info($"Wysyłanie wiadomości o wypisaniu pacjenta: {message}");
            busInstance.Publish(message);
            OnPatientDeleted?.Invoke();
        }

    }
}
