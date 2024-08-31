using Lombok.NET;
using System;
using System.ComponentModel.DataAnnotations;

namespace Emergency.Patient
{
    internal class PatientEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }


        public string SecondName { get; set; }


        public string Pesel { get; set; }


        public DateTime BirthDate { get; set; }


        public string City { get; set; }


        public string PostalCode { get; set; }


        public string Street { get; set; }


        public string PhoneNumber { get; set; }


        public int HouseNumber { get; set; }


        public int ApartmentNumber { get; set; }
    }
}
