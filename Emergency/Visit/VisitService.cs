﻿using Emergency.Patient;
using Emergency.Validator;
using log4net;
using log4net.Core;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            ValidatePesel(visit.PatientPesel);
            if(IsPatientAlreadyOnVisit(visit.PatientPesel))
            {
                throw new PatientAlreadyOnVisitException("Pacjent jest obecnie na wizycie w placówce");
            }
            visitRepository.Save(visit);
            eventRepository.Register(visit);
            logger.Info($"Zarejestrowano wizytę {visit}");
        }

        public void AskForRegistration(WardType ward, string pesel)
        {
            logger.Info($"Rozpoczęto zapytanie oddziałów o przyjęcie pacjenta na oddział {ward.ToPolish()}");
            ValidatePesel(pesel);
            eventRepository.AskForRegistration(ward, pesel);
            logger.Info($"Wyslano zapytanie o wizytę na oddziale {ward.ToPolish()}");
        }

        public void ChangePatientRoom(IPatientWardRoomChangedMessage message)
        {
            logger.Info($"Rozpoczęto rejestrowanie zmiany sali pacjenta {message.PatientPesel}");
            ValidatePesel(message.PatientPesel);
            var visit = visitRepository.GetPatientCurrentVisit(message.PatientPesel);
            visit.Room = message.Room;
            visitRepository.Save(visit);
            logger.Info($"Zmieniono salę pacjenta {message.PatientPesel} na {message.Room}");
        }

        public void MarkVisitAsFinished(IPatientAllowedToLeaveMessage message)
        {
            logger.Info($"Rozpoczęto oznaczanie wizyty jako możliwą do zakończenia dla pacjenta {message.PatientPesel}");
            ValidatePesel(message.PatientPesel);
            ValidatePwz(message.DoctorPwzNumber);
            var visit = visitRepository.GetPatientCurrentVisit(message.PatientPesel);
            visit.AllowedToLeave = true;
            visit.LeavePermissionDoctorPwz = message.DoctorPwzNumber;
            visit.LeavedAtOwnRisk = message.LeavedAtOwnRisk;
            visitRepository.Save(visit);
            logger.Info($"Oznaczono wizytę jako możliwą do zakończenia dla pacjenta {message.PatientPesel}");
        }

        public void MarkVisitAsInProgress(IPatientVisitArrivedMessage message)
        {
            logger.Info($"Rozpoczęto oznaczanie wizyty jako w trakcie dla pacjenta {message.PatientPesel}");
            ValidatePesel(message.PatientPesel);
            var visit = visitRepository.GetPatientCurrentVisit(message.PatientPesel);
            visit.Room = message.Room;
            visit.VisitState = VisitEntityState.IN_PROGRESS;
            visitRepository.Save(visit);
            logger.Info($"Oznaczono wizytę jako w trakcie dla pacjenta {message.PatientPesel}");
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
            ValidatePesel(pesel);

            var visit = visitRepository.GetPatientCurrentVisit(pesel);
            if (visit.AllowedToLeave)
            {
                visit.VisitEndDate = DateTime.Now;
                visit.VisitState = VisitEntityState.FINISHED;
                visitRepository.Save(visit);
                eventRepository.Unregister(visit);
                logger.Info($"Wypisano pacjenta o peselu {pesel}, zezwolił na to doktor {visit.LeavePermissionDoctorPwz}");
            }
            else
            {
                throw new PatientUnallowedToLeaveException();
            }
        }

        private bool IsPatientAlreadyOnVisit(string pesel)
        {
            try
            {
                var currentVisit = visitRepository.GetPatientCurrentVisit(pesel);
                return true;
            } catch (UnregisteredPatientException)
            {
                return false;
            }
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
