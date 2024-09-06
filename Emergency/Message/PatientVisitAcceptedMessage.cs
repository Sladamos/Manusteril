using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Message
{
    internal class PatientVisitAcceptedMessage : IPatientVisitAcceptedMessage
    {
        public required Guid PatientId { get; set; }

        public required string PatientPesel { get; set; }

        public required Guid VisitId { get; set; }

        public required string WardIdentifier { get; set; }
        public required DateTime VisitStartDate { get; set; }
    }
}
