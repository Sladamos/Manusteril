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
using System.ComponentModel;
using static MassTransit.Monitoring.Performance.BuiltInCounters;
using Ward.Room;

namespace Ward.Visit
{
    internal partial class VisitService : IVisitService
    {
        private readonly IValidatorService validator;

        private IRoomService roomService;

        private IPatientService patientService;

        private readonly IVisitRepository visitRepository;

        private readonly ILog logger;

        public VisitService(IValidatorService validator,
            IRoomService roomService,
            IPatientService patientService,
            IVisitRepository visitRepository,
            ILog logger)
        {
            this.validator = validator;
            this.roomService = roomService;
            this.patientService = patientService;
            this.visitRepository = visitRepository;
            this.logger = logger;
        }

        public void AddVisit(VisitEntity visit)
        {
            logger.Info($"Rozpoczęto rejestrowanie wizyty {visit}");
            ValidatePesel(visit.PatientPesel);
            if (IsPatientRegisteredInWard(visit.PatientPesel))
            {
                throw new PatientAlreadyOnVisitException("Pacjent jest już na wizycie na oddziale");
            }
            visitRepository.Save(visit);
            logger.Info($"Zarejestrowano wizytę {visit}");
        }

        public VisitEntity GetPatientCurrentVisit(string pesel)
        {
            logger.Info($"Poszukiwanie obecnej wizyty dla pacjenta {pesel}");
            ValidatePesel(pesel);
            return visitRepository.GetPatientCurrentVisit(pesel);
        }

        public bool IsPatientRegisteredInWard(string patientPesel)
        {
            logger.Info($"Sprawdzanie czy pacjent jest zarejestrowany na oddziale {patientPesel}");
            ValidatePesel(patientPesel);
            try
            {
                visitRepository.GetPatientCurrentVisit(patientPesel);
                logger.Info($"Pacjent {patientPesel} jest zarejestrowany na oddziale");
                return true;
            } catch (UnregisteredPatientException) 
            {
                logger.Info($"Pacjent {patientPesel} nie jest zarejestrowany na oddziale");
                return false;
            }
        }

        public void MarkVisitAsAllowedToLeave(VisitEntity visit)
        {
            logger.Info($"Rozpoczęto pozwolenie na wypiskę {visit.PatientPesel}");
            ValidatePesel(visit.PatientPesel);
            ValidatePwz(visit.LeavePermissionDoctorPwz);
            visit.AllowedToLeave = true;
            visitRepository.Save(visit);
            Console.WriteLine("TODO send message");
            logger.Info($"Zezwolono na opuszczenie oddziału pacjentowi {visit.PatientPesel}");
        }

        public void MarkVisitAsFinished(IPatientVisitUnregisteredMessage message)
        {
            logger.Info($"Rozpocząto oznaczanie wizyty pacjenta {message.PatientPesel} jako zakończoną");
            ValidatePesel(message.PatientPesel);
            var visit = visitRepository.GetPatientCurrentVisit(message.PatientPesel);
            visit.VisitEndDate = message.VisitEndDate;
            visit.VisitState = VisitEntityState.FINISHED;
            var patient = patientService.GetPatientByPesel(message.PatientPesel);
            roomService.RemovePatientFromRoom(patient);
            visitRepository.Save(visit);
            logger.Info($"Oznaczono wizytję pacjenta {message.PatientPesel} jako zakończoną");
        }

        public void MarkVisitAsInProgress(string pesel, string roomNumber)
        {
            logger.Info($"Rozpocząto oznaczanie wizyty dla pacjenta {pesel} jako rozpoczętą");
            ValidatePesel(pesel);
            var visit = visitRepository.GetPatientCurrentVisit(pesel);
            if(visit.VisitState != VisitEntityState.NEW)
            {
                throw new PatientAlreadyOnVisitException("Pacjent nie jest zgłoszony na wizytę na oddziale");
            }
            visit.VisitState = VisitEntityState.IN_PROGRESS;
            var patient = patientService.GetPatientByPesel(pesel);
            roomService.AddPatientToRoom(patient, roomNumber);
            visitRepository.Save(visit);
            Console.WriteLine("TODO send message");
            logger.Info($"Oznaczono wizytję pacjenta {pesel} jako rozpoczętą");
        }

        public void UpdateVisitRoom(PatientEntity patient, string number)
        {
            logger.Info($"Rozpoczęto przypisywanie sali dla pacjenta {patient.Pesel}");
            var visit = visitRepository.GetPatientCurrentVisit(patient.Pesel);
            visit.PatientRoomNumber = number;
            visitRepository.Save(visit);
            logger.Info($"Przypisano do pacjenta {patient.Pesel} salę {number}");
        }

        private void ValidatePesel(string pesel)
        {
            var validationResult = validator.ValidatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException(validationResult.ValidatorMessage);
            }
        }

        private void ValidatePwz(string? pwz)
        {
            if(pwz == null)
            {
                throw new InvalidPwzException("Nie podano pwz");
            }

            var validationResult = validator.ValidatePwz(pwz);
            if (!validationResult.IsValid)
            {
                throw new InvalidPwzException(validationResult.ValidatorMessage);
            }
        }
    }
}
