using System;
using System.ComponentModel.DataAnnotations;

namespace Ward.Patient
{
    internal class PatientEntity
    {
        [Key]
        public required Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Pesel { get; set; }

        public required string City { get; set; }

        public required string Address { get; set; }

        public required string PhoneNumber { get; set; }
    }
}
