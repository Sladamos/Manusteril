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

        public required string PatientLastName { get; set; }

        public required string PatientPesel {  get; set; }

        public required string PatientCity { get; set; }

        public required string PatientAddress { get; set; }

        public required string PatientPhoneNumber { get; set; }
    }
}
