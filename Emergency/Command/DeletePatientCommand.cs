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
            /*
             * - Podaj imie
             * - Usun pacjenta (if imie)
             * - Cofnij
             */
            Console.WriteLine("TODO DeletePatientCommand");
        }
    }
}
