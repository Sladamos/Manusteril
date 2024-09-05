using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command
{
    internal class PatientInfo
    {
        public required Func<string> PeselSupplier { get; set; }
        public required Func<string> FirstNameSupplier { get; set; }
        public required Func<string> LastNameSupplier { get; set; }
        public required Func<string> PhoneNumberSupplier { get; set; }
        public required Func<string> AddressSupplier { get; set; }
        public required Func<string> CitySupplier { get; set; }
    }
}
