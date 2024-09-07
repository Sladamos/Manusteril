using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Messages
{
    internal class PatientVisitRegisteredMessage : IPatientVisitRegisteredMessage
    {

        public required string PatientPesel { get; set; }

        public required WardType WardType { get; set; }
    }
}
