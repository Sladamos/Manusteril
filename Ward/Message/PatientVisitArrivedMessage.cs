using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Message
{
    internal class PatientVisitArrivedMessage : IPatientVisitArrivedMessage
    {
        public required Guid PatientId { get; set; }

        public required string PatientPesel { get; set; }

        public required Guid VisitId { get; set; }

        public required string WardIdentifier { get; set; }

        public required string Room { get; set; }
    }
}
