using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Messages
{
    internal class NewPatientRegisteredMessage : INewPatientRegistered
    {
        public required Guid PatientId { get; set; }

        public required string PatientFirstName { get; set; }

        public required string PatientSecondName { get; set; }

        public required string PatientPesel {  get; set; }

        public required DateTime PatientBirthDate { get; set; }

        public required string PatientCity { get; set; }

        public required string PatientPostalCode { get; set; }

        public required string PatientStreet { get; set; }

        public required string PatientPhoneNumber { get; set; }

        public required int PatientHouseNumber { get; set; }

        public required int PatientApartmentNumber { get; set; }
    }
}
