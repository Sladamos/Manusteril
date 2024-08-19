using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class CheckInsuranceCommand : ICommand
    {
        public string Name => "Ubezpieczenie";

        public string Description => "Sprawdź czy pacjent jest ubezpieczony";

        public void Execute()
        {
            Console.WriteLine("Podaj PESEL pacjenta");
            string? pesel = Console.ReadLine();
            if (pesel != null )
            {
                Console.WriteLine("TODO: NfzMockCheckInsurance");
            } else
            {
                Console.WriteLine("PESEL niepoprawny");
            }
        }
    }
}
