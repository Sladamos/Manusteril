using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Patient
{
    internal class Patient
    {
        public required Guid PatientId {  get; set; }

        public required string Pesel { get; set; }
    }
}
