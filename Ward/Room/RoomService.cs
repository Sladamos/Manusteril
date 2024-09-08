using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Ward.Validator;
using Ward.Visit;

namespace Ward.Room
{
    internal class RoomService : IRoomService
    {
        private IRoomRepository roomRepository;

        private IValidatorService validatorService;

        private IRoomEventRepository roomEventRepository;

        private ILog logger;

        public RoomService(IRoomRepository roomRepository,
            IValidatorService validatorService,
            IRoomEventRepository eventRepository,
            ILog logger)
        {
            this.roomRepository = roomRepository;
            this.validatorService = validatorService;
            this.roomEventRepository = eventRepository;
            this.logger = logger;
        }

        public void AddPatientToRoom(PatientEntity patient, string roomNumber)
        {
            logger.Info($"Rozpoczęto dodawanie pacjenta {patient.Pesel} do sali {roomNumber}");
            var newRoom = roomRepository.GetRoomByRoomNumber(roomNumber);
            CheckIfRoomHasEnoughSpace(newRoom);
            AddPatientToRoom(newRoom, patient);
            logger.Info($"Pomyślnie dodano pacjenta {patient.Pesel} do sali {roomNumber}");
        }

        public List<RoomEntity> GetAll()
        {
            logger.Info($"Rozpoczęto pobieranie sal");
            return roomRepository.GetAll().ToList();
        }

        public RoomEntity GetRoomByPatientPesel(string pesel)
        {
            logger.Info($"Rozpoczęto pobieranie sali pacjenta {pesel}");
            ValidatePesel(pesel);
            return roomRepository.GetRoomByPatientPesel(pesel);
        }

        public void RemovePatientFromRoom(PatientEntity patient)
        {
            logger.Info($"Rozpoczęto usuwanie pacjenta {patient.Pesel} z obecnej sali");
            var currentRoom = roomRepository.GetRoomByPatientPesel(patient.Pesel);
            RemovePatientFromRoom(currentRoom, patient);
            logger.Info($"Pomyślnie usunięto pacjenta z sali {currentRoom.Number}");
        }

        public void TransferPatientToRoom(PatientEntity patient, string roomNumber)
        {
            logger.Info($"Rozpoczęto transfer pacjenta {patient.Pesel} do sali {roomNumber}");
            var newRoom = roomRepository.GetRoomByRoomNumber(roomNumber);
            var currentRoom = roomRepository.GetRoomByPatientPesel(patient.Pesel);
            CheckIfRoomCanReceivePatient(newRoom, currentRoom);
            RemovePatientFromRoom(patient);
            AddPatientToRoom(newRoom, patient);
            roomEventRepository.NotifyAboutTransfer(patient, newRoom);
            logger.Info($"Pomyślny transfer do sali {roomNumber}");
        }

        private void CheckIfRoomCanReceivePatient(RoomEntity newRoom, RoomEntity currentRoom)
        {
            CheckIfRoomHasEnoughSpace(newRoom);

            if(newRoom.Number == currentRoom.Number)
            {
                throw new IncorrectRoomException($"Nie można przenieść pacjenta do sali: {newRoom.Number}, ponieważ pacjent już się w niej znajduje");
            }
        }

        private void CheckIfRoomHasEnoughSpace(RoomEntity newRoom)
        {
            if (newRoom.Capacity - newRoom.OccupiedBeds <= 0)
            {
                throw new IncorrectRoomException($"Nie można przenieść pacjenta do sali: {newRoom.Number} z uwagi na brak miejsca");
            }
        }

        private void ValidatePesel(string pesel)
        {
            var validationResult = validatorService.ValidatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException(validationResult.ValidatorMessage);
            }
        }

        private void RemovePatientFromRoom(RoomEntity room, PatientEntity patient)
        {
            room.Patients = string.Join(";", room.Patients.Split(';').Where(pesel => pesel != patient.Pesel).ToArray());
            room.OccupiedBeds--;
            roomRepository.Save(room);
        }

        private void AddPatientToRoom(RoomEntity room, PatientEntity patient)
        {
            if(string.IsNullOrEmpty(room.Patients))
            {
                room.Patients = patient.Pesel;
            } else
            {
                room.Patients += $";{patient.Pesel}";
            }
            room.OccupiedBeds++;
            roomRepository.Save(room);
        }
    }
}
