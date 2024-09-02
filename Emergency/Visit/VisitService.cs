using Emergency.Patient;
using Emergency.Validator;
using log4net;
using log4net.Core;
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

        public void AddVisit(VisitEntity visit)
        {
            logger.Info($"Rozpoczęto rejestrowanie wizyty {visit}");
            var validationResult = validator.validatePesel(visit.PatientPesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }
            visitRepository.Save(visit);
            eventRepository.Register(visit);
            logger.Info($"Zarejestrowano wizytę {visit}");
        }

        /// <exception cref = "UnregisteredPatientException">
        /// Thrown when no visit with a null VisitEndDate is found for the given patient.
        /// </exception>
        /// <exception cref = "PatientUnallowedToLeaveException">
        /// Thrown when current patient visit doesn't have allowance to leave.
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
            if (visit.AllowedToLeave)
            {
                visit.VisitEndDate = DateTime.Now;
                visitRepository.Save(visit);
                eventRepository.Unregister(visit);
                logger.Info($"Wypisano pacjenta o peselu {pesel}");
            }
            else
            {
                throw new PatientUnallowedToLeaveException();
            }
        }
    }
}
