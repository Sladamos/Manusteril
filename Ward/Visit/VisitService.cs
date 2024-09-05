using Ward.Patient;
using Ward.Validator;
using log4net;
using log4net.Core;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Visit
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
            ValidatePesel(visit.PatientPesel);
            visitRepository.Save(visit);
            eventRepository.Register(visit);
            logger.Info($"Zarejestrowano wizytę {visit}");
        }

        public void ChangePatientWard(IPatientWardChanged message)
        {
            logger.Info($"Rozpoczęto rejestrowanie zmiany oddziału pacjenta {message.PatientPesel}");
            ValidatePesel(message.PatientPesel);
            ValidatePwz(message.DoctorPwzNumber);
            var visit = visitRepository.GetPatientCurrentVisit(message.PatientPesel);
            visitRepository.Save(visit);
            logger.Info($"Zmieniono oddział pacjenta {message.PatientPesel} na {message.Destination.ToPolish()}");
        }

        public void MarkVisitAsFinished(IPatientAllowedToLeave message)
        {
            logger.Info($"Rozpoczęto oznaczanie wizyty jako możliwą do zakończenia dla pacjenta {message.PatientPesel}");
            ValidatePesel(message.PatientPesel);
            ValidatePwz(message.DoctorPwzNumber);
            var visit = visitRepository.GetPatientCurrentVisit(message.PatientPesel);
            visit.AllowedToLeave = true;
            visit.LeavePermissionDoctorId = message.DoctorId;
            visit.LeavePermissionDoctorPwz = message.DoctorPwzNumber;
            visit.LeavedAtOwnRisk = message.LeavedAtOwnRisk;
            visitRepository.Save(visit);
            logger.Info($"Oznaczono wizytę jako możliwą do zakończenia dla pacjenta {message.PatientPesel}");
        }

        private void ValidatePesel(string pesel)
        {
            var validationResult = validator.ValidatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException(validationResult.ValidatorMessage);
            }
        }

        private void ValidatePwz(string pwz)
        {
            var validationResult = validator.ValidatePwz(pwz);
            if (!validationResult.IsValid)
            {
                throw new InvalidPwzException(validationResult.ValidatorMessage);
            }
        }
    }
}
