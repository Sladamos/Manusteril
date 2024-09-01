using Emergency.Patient;
using Emergency.Validator;
using log4net;
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

        private readonly IVisitEventRepository eventRepository;

        private readonly ILog logger;

        public VisitService(IValidatorService validator,
            IVisitRepository visitRepository,
            IVisitEventRepository eventRepository,
            ILog logger)
        {
            this.validator = validator;
            this.visitRepository = visitRepository;
            this.eventRepository = eventRepository;
            this.logger = logger;
        }

        /// <exception cref = "UnregisteredPatientException">
        /// Thrown when no visit with a null VisitEndDate is found for the given patient.
        /// </exception>
        public void UnregisterPatientByPesel(string pesel)
        {
            logger.Info($"Rozpoczęto wypiskę pacjenta o peselu {pesel}");
            var validationResult = validator.validatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }

            var visit = visitRepository.GetPatientCurrentVisit(pesel);
            //TODO: execute manual logic if is not autofilled

            // dwie drogi:
            //if nie ma info o wypisce then uzupelnij recznie (z możliwością przerwania) - jaki lekarz podał czy na włąsne życzenie
            //if info o wypisce to wypisz i tyle
            visit.VisitEndDate = DateTime.Now;
            visitRepository.Save(visit);
            eventRepository.Unregister(visit);
            logger.Info($"Wypisano pacjenta o peselu {pesel}");
        }


        /*visit = new() { Id = Guid.NewGuid(),
            PatientId = Guid.NewGuid(),
            PatientPesel = pesel,
            VisitStartDate = DateTime.Now,
            AllowedToLeave = false}; */
    }
}
