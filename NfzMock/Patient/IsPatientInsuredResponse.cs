using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Patient
{
    internal class IsPatientInsuredResponse : IIsPatientInsuredResponse
    {
        public required string PatientPesel { get; set; }

        public required bool IsInsured { get; set; }
    }
}
