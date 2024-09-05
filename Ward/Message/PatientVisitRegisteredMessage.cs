using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Messages
{
    internal class PatientVisitRegisteredMessage : IPatientVisitRegisteredMessage
    {
        public required Guid PatientId { get; set; }

        public required string PatientPesel { get; set; }

        public required Guid VisitId { get; set; }

        public required WardType WardType { get; set; }
    }
}
