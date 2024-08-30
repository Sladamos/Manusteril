using Lombok.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Patient
{
    [AllArgsConstructor]
    internal partial class Patient
    {
        [Property]
        private readonly Guid id;

        [Property]
        private readonly string firstName;

        [Property]
        private readonly string secondName;

        [Property]
        private readonly string pesel;

        [Property]
        private readonly DateTime birthDate;

        [Property]
        private readonly string city;

        [Property]
        private readonly string postalCode;

        [Property]
        private readonly string street;

        [Property]
        private readonly string phoneNumber;

        [Property]
        private readonly int houseNumber;

        [Property]
        private readonly int apartmentNumber;
    }
}
