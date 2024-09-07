using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Config;
using Ward.Patient;
using Ward.Room;
using Ward.Visit;

namespace Ward
{
    internal class Initializer
    {
        private RoomRepository roomRepository;

        private PatientRepository patientRepository;

        private VisitRepository visitRepository;

        private ApplicationDbContext applicationDbContext;

        public Initializer(RoomRepository roomRepository,
            PatientRepository patientRepository,
            VisitRepository visitRepository,
            ApplicationDbContext applicationDbContext)
        {
            this.roomRepository = roomRepository;
            this.patientRepository = patientRepository;
            this.visitRepository = visitRepository;
            this.applicationDbContext = applicationDbContext;
        }

        public void Initialize()
        {
            applicationDbContext.Ensure();
            List<RoomEntity> rooms = new List<RoomEntity>();

            var patientFirst = new PatientEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Jan",
                LastName = "Kowalski",
                Pesel = "12345678901",
                City = "Warszawa",
                Address = "ul. Miodowa 12",
                PhoneNumber = "123456789"
            };

            var patientSecond = new PatientEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Anna",
                LastName = "Nowak",
                Pesel = "10987654321",
                City = "Kraków",
                Address = "ul. Floriańska 10",
                PhoneNumber = "+48123456789"
            };

            var patientThird = new PatientEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Piotr",
                LastName = "Wiśniewski",
                Pesel = "12312312311",
                City = "Gdańsk",
                Address = "ul. Długa 5",
                PhoneNumber = "987654321"
            };

            var visit1ForPatient1 = new VisitEntity
            {
                Id = Guid.NewGuid(),
                PatientId = patientFirst.Id,
                PatientPesel = patientFirst.Pesel,
                VisitStartDate = new DateTime(2023, 9, 1, 10, 0, 0),
                VisitEndDate = new DateTime(2023, 9, 1, 12, 0, 0),
                AllowedToLeave = true,
                LeavedAtOwnRisk = false,
                LeavePermissionDoctorPwz = "123456",
                PatientRoomNumber = "121"
            };

            var visit2ForPatient1 = new VisitEntity
            {
                Id = Guid.NewGuid(),
                PatientId = patientFirst.Id,
                PatientPesel = patientFirst.Pesel,
                VisitStartDate = new DateTime(2023, 9, 5, 14, 0, 0),
                AllowedToLeave = false,
                PatientRoomNumber = "118"
            };

            var visitForPatient2 = new VisitEntity
            {
                Id = Guid.NewGuid(),
                PatientId = patientSecond.Id,
                PatientPesel = patientSecond.Pesel,
                VisitStartDate = new DateTime(2023, 9, 2, 10, 0, 0),
                VisitEndDate = new DateTime(2023, 9, 1, 12, 0, 0),
                AllowedToLeave = true,
                LeavedAtOwnRisk = true,
                LeavePermissionDoctorPwz = "123456",
                PatientRoomNumber = "118"
            };

            var visitForPatient3 = new VisitEntity
            {
                Id = Guid.NewGuid(),
                PatientId = patientThird.Id,
                PatientPesel = patientThird.Pesel,
                VisitStartDate = new DateTime(2023, 9, 3, 11, 0, 0),
                AllowedToLeave = false,
                PatientRoomNumber = "115"
            };

            rooms.Add(new() { Id = Guid.NewGuid(), Number = "118", Capacity = 2, OccupiedBeds = 2, Patients = $"{patientFirst.Pesel};{patientSecond.Pesel}" });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "119A", Capacity = 1, OccupiedBeds = 0 });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "119B", Capacity = 1, OccupiedBeds = 0 });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "115", Capacity = 4, OccupiedBeds = 1 , Patients = patientThird.Pesel });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "121", Capacity = 3, OccupiedBeds = 0 });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "143", Capacity = 6, OccupiedBeds = 0 });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "152A", Capacity = 2, OccupiedBeds = 0 });
            rooms.Add(new() { Id = Guid.NewGuid(), Number = "153B", Capacity = 2, OccupiedBeds = 0 });
            rooms.ForEach(room => roomRepository.Save(room));

            patientRepository.Save(patientFirst);
            patientRepository.Save(patientSecond);
            patientRepository.Save(patientThird);

            visitRepository.Save(visit1ForPatient1);
            visitRepository.Save(visit2ForPatient1);
            visitRepository.Save(visitForPatient2);
            visitRepository.Save(visitForPatient3);
        }
    }
}
