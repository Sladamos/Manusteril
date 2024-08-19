using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class DeletePatientCommand : ICommand
    {
        public string Name => "Wypisz";

        public string Description => "Wypisz pacjenta";

        public void Execute()
        {
            Console.WriteLine("TODO DeletePatientCommand");
        }
    }
}
