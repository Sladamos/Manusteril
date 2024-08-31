using Emergency.Patient;
using Emergency.Validator;
using Lombok.NET;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    internal partial class VisitService : IVisitService
    {
        private readonly IValidatorService validator;

        private readonly IVisitRepository visitRepository;

        public VisitService(IValidatorService validator, IVisitRepository visitRepository)
        {
            this.validator = validator;
            this.visitRepository = visitRepository;
        }

        public void UnregisterPatientByPesel(string pesel)
        {
            var validationResult = validator.validatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }
            //throw new NotImplementedException();
            var visits = visitRepository.GetAllByPatient(pesel);

            //checkMode : manual or automat
            //askAnotherMicroserviceIfIsAllowed + retry, timeout : when timeouted manual 
            //ifNotSendAlertMessage - patient want to escape e.t.c
            //ifYes:

            //var temporaryPatient = new Patient { Pesel = pesel, Id = Guid.NewGuid() };
            //PatientVisitUnregisteredMessage message = new(temporaryPatient.PatientId, temporaryPatient.Pesel, Guid.NewGuid());
            //logger.Info($"Wysyłanie wiadomości o wypisaniu pacjenta: {message}");
            //busInstance.Publish(message);
        }
    }
}
