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

        private ILog logger;

        public RoomService(IRoomRepository roomRepository,
            IValidatorService validatorService,
            ILog logger)
        {
            this.roomRepository = roomRepository;
            this.validatorService = validatorService;
            this.logger = logger;
        }

        public void AddPatientToRoom(PatientEntity patient, string roomNumber)
        {
            logger.Info($"Rozpoczęto dodawanie pacjenta {patient.Pesel} do sali {roomNumber}");
            var newRoom = roomRepository.GetRoomByRoomNumber(roomNumber);
            CheckIfRoomCanReceivePatient(newRoom);
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
            CheckIfRoomCanReceivePatient(newRoom);
            RemovePatientFromRoom(patient);
            AddPatientToRoom(newRoom, patient);
            //TODO send message
            logger.Info($"Pomyślny transfer do sali {roomNumber}");
        }

        private void CheckIfRoomCanReceivePatient(RoomEntity room)
        {
            if (room.Capacity - room.OccupiedBeds <= 0)
            {
                throw new IncorrectRoomException($"Nie można przenieść pacjenta do sali: {room.Number}");
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
