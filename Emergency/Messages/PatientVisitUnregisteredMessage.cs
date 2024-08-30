using Lombok.NET;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Messages
{
    [AllArgsConstructor]
    internal partial class PatientVisitUnregisteredMessage : IPatientVisitUnregisteredMessage
    {
        [Property]
        private readonly Guid patientId;

        [Property]
        private readonly string patientPesel;

        [Property]
        private readonly Guid visitId;
    }
}
