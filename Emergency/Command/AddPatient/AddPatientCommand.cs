using Emergency.Patient;
using Emergency.Visit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.AddPatient
{
    internal class AddPatientCommand : ICommand
    {
        public string Name => "Nowy";

        public string Description => "Dodaj nowego pacjenta";

        public Action? OnPatientAdded;

        public async Task Execute()
        {
            Console.WriteLine("Dodawanie pacjenta");
        }
    }
}
