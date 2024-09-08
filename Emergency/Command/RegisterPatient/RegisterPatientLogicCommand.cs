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

        private Func<string> wardIdentifierSupplier;

        public string Name => "Zarejestruj";

        public string Description => "Zarejestruj wybranego pacjenta";

        public Action? OnPatientRegistered;

        public RegisterPatientLogicCommand(IVisitService visitService,
            IPatientService patientService,
            Func<string> peselSupplier,
            Func<WardType> wardSupplier,
            Func<string> wardIdentifierSupplier) 
        {
            this.visitService = visitService;
            this.patientService = patientService;
            this.peselSupplier = peselSupplier;
            this.wardSupplier = wardSupplier;
            this.wardIdentifierSupplier = wardIdentifierSupplier;
        }

        public async Task Execute()
        {
            Console.WriteLine("Rejestrowanie pacjenta");
            WardType ward = wardSupplier();
            string wardIdentifier = wardIdentifierSupplier();
            if (ward == WardType.NONE || string.IsNullOrEmpty(wardIdentifier))
            {
                Console.WriteLine("Najpierw należy wybrać oddział i jego identyfikator");
            }
            else
            {
                try
                {
                    RegisterVisit(ward, wardIdentifier);
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

        private void RegisterVisit(WardType ward, string wardIdentifier)
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
                WardIdentifier = wardIdentifier,
                Room = "",
                VisitState = VisitEntityState.NEW
            };
            visitService.AddVisit(visit);
            OnPatientRegistered?.Invoke();
        }
    }
}
