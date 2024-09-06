using Emergency.Bus;
using Emergency.Messages;
using Emergency.Patient;
using Emergency.Visit;
using log4net;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.RegisterPatient
{
    internal class RegisterPatientLogicCommand : ICommand
    {
        private IVisitService visitService;

        private IPatientService patientService;

        private Func<string> peselSupplier;

        private Func<WardType> wardSupplier;

        public string Name => "Zarejestruj";

        public string Description => "Zarejestruj wybranego pacjenta";

        public Action? OnPatientRegistered;

        public RegisterPatientLogicCommand(IVisitService visitService,
            IPatientService patientService,
            Func<string> peselSupplier,
            Func<WardType> wardSupplier) 
        {
            this.visitService = visitService;
            this.patientService = patientService;
            this.peselSupplier = peselSupplier;
            this.wardSupplier = wardSupplier;
        }

        public async Task Execute()
        {
            Console.WriteLine("Rejestrowanie pacjenta");
            WardType ward = wardSupplier();
            if (ward == WardType.NONE)
            {
                Console.WriteLine("Najpierw należy wybrać oddział");
            }
            else
            {
                try
                {
                    RegisterVisit(ward);
                }
                catch (InvalidPeselException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (UnknownPatientException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void RegisterVisit(WardType ward)
        {
            string pesel = peselSupplier();
            PatientEntity patient = patientService.GetPatientByPesel(pesel);
            VisitEntity visit = new()
            {
                Id = Guid.NewGuid(),
                PatientId = patient.Id,
                PatientPesel = patient.Pesel,
                VisitStartDate = DateTime.Now,
                Ward = ward,
                AllowedToLeave = false,
                WardIdentifier = "",
                Room = "",
                visitState = VisitEntityState.NEW
            };
            visitService.AddVisit(visit);
            OnPatientRegistered?.Invoke();
        }
    }
}
