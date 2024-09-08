using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Message
{
    internal class PatientAllowedToLeaveMessage : IPatientAllowedToLeaveMessage
    {
        public required Guid PatientId { get; set; }

        public required string PatientPesel { get; set; }

        public required string DoctorPwzNumber { get; set; }

        public required bool LeavedAtOwnRisk { get; set; }
    }
}
