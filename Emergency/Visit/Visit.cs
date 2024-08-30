using Lombok.NET;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    [AllArgsConstructor]
    public partial class Visit
    {
        private readonly Guid patientId;
        private readonly string patientPesel;
        private readonly DateTime visitStartDate;
        private readonly DateTime? visitEndDate;
        private readonly bool allowedToLeave;
        private readonly bool leavedAtOwnRisk;
        private readonly Guid leavePermissionDoctorId;
        private readonly string leavePermissionDoctorPwz;
    }
}
