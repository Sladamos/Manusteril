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

        /// <exception cref = "UnregisteredPatientException">
        /// Thrown when no visit with a null VisitEndDate is found for the given patient.
        /// </exception>
        public void UnregisterPatientByPesel(string pesel)
        {
            var validationResult = validator.validatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }

            var visit = visitRepository.GetAllByPatient(pesel)
                .FirstOrDefault(visit => visit?.VisitEndDate == null)
                ?? throw new UnregisteredPatientException("Pacjent nie jest zarejestrowany w placówce");
            //TODO: execute manual logic if is not autofilled

            // dwie drogi:
            //if nie ma info o wypisce then uzupelnij recznie (z możliwością przerwania) - jaki lekarz podał czy na włąsne życzenie
            //if info o wypisce to wypisz i tyle
            visit.VisitEndDate = DateTime.Now;
            visitRepository.Save(visit);

            //RESTREPOSITORY
            //logger.Info($"Wysyłanie wiadomości o wypisaniu pacjenta: {message}");
            //busInstance.Publish(message);


            /*visit = new() { Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                PatientPesel = pesel,
                VisitStartDate = DateTime.Now,
                AllowedToLeave = false}; */
        }
    }
}
