using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class AddPatientCommand : ICommand
    {
        public string Name => "Nowy";

        public string Description => "Dodaj nowego pacjenta";

        public async Task Execute()
        {
            Console.WriteLine("TODO AddPatientCommand");
        }
    }
}
