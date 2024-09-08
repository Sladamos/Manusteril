using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Ward.Room;
using Ward.Visit;

namespace Ward.Command.Patients.AllowPatientToLeave
{
    internal class AllowPatientToLeaveLogicCommand : ICommand
    {
        private IVisitService visitService;

        private Func<string> peselSupplier;

        private Func<string> pwzSupplier;

        private Func<bool> ownRiskSupplier;

        public string Name => "Zezwól";

        public string Description => "Wydaj zezwolenie na opuszczenie szpitala przez pacjenta";

        public Action? OnAllowedToLeave;

        public AllowPatientToLeaveLogicCommand(IVisitService visitService,
            Func<string> peselSupplier,
            Func<string> pwzSupplier,
            Func<bool> ownRiskSupplier)
        {
            this.visitService = visitService;
            this.peselSupplier = peselSupplier;
            this.pwzSupplier = pwzSupplier;
            this.ownRiskSupplier = ownRiskSupplier;
        }

        public async Task Execute()
        {
            string pesel = peselSupplier();
            try
            {
                var visit = visitService.GetPatientCurrentVisit(pesel);
                visit.LeavedAtOwnRisk = ownRiskSupplier();
                visit.LeavePermissionDoctorPwz = pwzSupplier();
                visitService.MarkVisitAsAllowedToLeave(visit);
                OnAllowedToLeave?.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
