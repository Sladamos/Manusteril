using Ward.Bus;
using Ward.Message;
using Ward.Patient;
using log4net.Core;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command.CheckInsurance
{
    internal class CheckInsuranceLogicCommand : ICommand
    {
        private IPatientService patientService;

        private readonly Func<string> peselProvider;

        public Action<IIsPatientInsuredResponse>? OnResponseArrived;

        public CheckInsuranceLogicCommand(IPatientService patientService, Func<string> peselProvider)
        {
            this.patientService = patientService;
            this.peselProvider = peselProvider;
        }

        public string Name => "Ubezpieczenie";

        public string Description => "Wyślij zapytanie do NFZ z zapytaniem czy pacjent jest ubezpieczony";

        public async Task Execute()
        {
            string pesel = peselProvider.Invoke();
            try
            {
                Console.WriteLine("Oczekiwanie na odpowiedź o ubezpieczeniu");
                var response = await patientService.IsPatientInsured(pesel);
                ReactToResponse(response, pesel);
            }
            catch (InvalidPeselException)
            {
                Console.WriteLine("Brak pacjenta o podanym PESELu");
            }
            catch (RequestTimeoutException)
            {
                Console.WriteLine("Nie można uzyskać informacji o ubezpieczeniu pacjenta");
            }
        }

        private void ReactToResponse(IIsPatientInsuredResponse isPatientInsured, string pesel)
        {
            if (isPatientInsured.PatientPesel == pesel)
            {
                Console.WriteLine($"Pacjent{(isPatientInsured.IsInsured ? "" : " nie")} jest ubezpieczony");
            }
            else
            {
                Console.WriteLine("Niepoprawna odpowiedź od NFZ - dotyczy innego pacjenta");
            }
        }
    }
}
