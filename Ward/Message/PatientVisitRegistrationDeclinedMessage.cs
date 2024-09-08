using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Message
{
    internal class PatientVisitRegistrationDeclinedMessage : IPatientVisitRegistrationDeclinedResponse
    {
        public required string PatientPesel { get; set; }

        public required string Reason { get; set; }

        public required string WardIdentifier { get; set; }
    }
}
