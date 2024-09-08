using Emergency.Config;
using Emergency.Patient;
using Emergency.Visit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency
{
    internal class Initializer
    {
        private PatientRepository patientRepository;

        private VisitRepository visitRepository;

        private ApplicationDbContext applicationDbContext;

        public Initializer(PatientRepository patientRepository,
            VisitRepository visitRepository,
            ApplicationDbContext applicationDbContext)
        {
            this.patientRepository = patientRepository;
            this.visitRepository = visitRepository;
            this.applicationDbContext = applicationDbContext;
        }

        public void Initialize()
        {
            applicationDbContext.Ensure();

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
                Room = "121",
                Ward = WardType.GENERAL,
                WardIdentifier = "Ogólny I",
                VisitState = VisitEntityState.FINISHED
            };

            var visit2ForPatient1 = new VisitEntity
            {
                Id = Guid.NewGuid(),
                PatientId = patientFirst.Id,
                PatientPesel = patientFirst.Pesel,
                VisitStartDate = new DateTime(2023, 9, 5, 14, 0, 0),
                AllowedToLeave = false,
                Room = "118",
                Ward = WardType.GENERAL,
                WardIdentifier = "Ogólny II",
                VisitState = VisitEntityState.IN_PROGRESS
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
                Room = "118",
                Ward = WardType.CARDIOLOGY,
                WardIdentifier = "Kardiologia",
                VisitState = VisitEntityState.FINISHED
            };

            var visitForPatient3 = new VisitEntity
            {
                Id = Guid.NewGuid(),
                PatientId = patientThird.Id,
                PatientPesel = patientThird.Pesel,
                VisitStartDate = new DateTime(2023, 9, 3, 11, 0, 0),
                AllowedToLeave = false,
                Room = "115",
                Ward = WardType.GENERAL,
                WardIdentifier = "Ogólny I",
                VisitState = VisitEntityState.NEW
            };

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
