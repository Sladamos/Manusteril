using Lombok.NET;
using System;
using System.ComponentModel.DataAnnotations;

namespace Emergency.Patient
{
    internal class PatientEntity
    {
        [Key]
        public required Guid Id { get; set; }

        public required string FirstName { get; set; }


        public required string SecondName { get; set; }


        public required string Pesel { get; set; }


        public required DateTime BirthDate { get; set; }


        public required string City { get; set; }


        public required string PostalCode { get; set; }


        public required string Street { get; set; }


        public required string PhoneNumber { get; set; }


        public required int HouseNumber { get; set; }


        public required int ApartmentNumber { get; set; }
    }
}
