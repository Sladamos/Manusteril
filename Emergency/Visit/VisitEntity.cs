using Lombok.NET;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
{
    public class VisitEntity
    {
        [Key]
        public Guid VisitId { get; set; }

        public Guid PatientId { get; set; }

        public string PatientPesel { get; set; }

        public DateTime VisitStartDate { get; set; }

        public DateTime? VisitEndDate { get; set; }

        public bool AllowedToLeave { get; set; }

        public bool LeavedAtOwnRisk { get; set; }

        public Guid LeavePermissionDoctorId { get; set; }

        public string LeavePermissionDoctorPwz { get; set; }
    }
}
