using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        public override string ToString()
        {
            var sb = new StringBuilder("PatientEntity [");

            sb.Append($"Id={Id}, ");
            if (!string.IsNullOrEmpty(FirstName))
                sb.Append($"FirstName={FirstName}, ");
            if (!string.IsNullOrEmpty(LastName))
                sb.Append($"LastName={LastName}, ");
            if (!string.IsNullOrEmpty(Pesel))
                sb.Append($"Pesel={Pesel}, ");
            if (!string.IsNullOrEmpty(City))
                sb.Append($"City={City}, ");
            if (!string.IsNullOrEmpty(Address))
                sb.Append($"Address={Address}, ");
            if (!string.IsNullOrEmpty(PhoneNumber))
                sb.Append($"PhoneNumber={PhoneNumber}, ");

            if (sb[sb.Length - 2] == ',')
                sb.Length -= 2;

            sb.Append("]");
            return sb.ToString();
        }
    }
}
