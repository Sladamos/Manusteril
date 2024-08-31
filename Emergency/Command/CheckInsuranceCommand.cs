using Emergency.Bus;
using Emergency.Message;
using Emergency.Patient;
using log4net.Core;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class CheckInsuranceCommand : ICommand
    {
        private IPatientService patientService;

        public CheckInsuranceCommand(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        public string Name => "Ubezpieczenie";

        public string Description => "Sprawdź, czy pacjent jest ubezpieczony";

        public async Task Execute()
        {
            Console.WriteLine("TODO: NfzMockCheckInsurance");
            string pesel = "12312312311";
            try
            {
                Console.WriteLine("Oczekiwanie na odpowiedź o ubezpieczeniu");
                var response = await patientService.IsPatientInsured(pesel);
                ReactToResponse(response, pesel);
            } catch (InvalidPeselException) {
                Console.WriteLine("Brak pacjenta o podanym PESELu");
            } catch (RequestTimeoutException) {
                Console.WriteLine("Nie można uzyskać informacji o ubezpieczeniu pacjenta");
            }
        }

        private void ReactToResponse(IIsPatientInsuredResponse isPatientInsured, string pesel)
        {
            if (isPatientInsured.PatientPesel == pesel)
            {
                Console.WriteLine($"Pacjent${(isPatientInsured.IsInsured ? "" : " nie")} jest ubezpieczony");
            }
            else
            {
                Console.WriteLine("Niepoprawna odpowiedź od NFZ - dotyczy innego pacjenta");
            }
        }
    }
}
