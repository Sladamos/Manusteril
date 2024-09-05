using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Visit
{
    public partial class VisitEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public required string PatientPesel { get; set; }

        public DateTime VisitStartDate { get; set; }

        public DateTime? VisitEndDate { get; set; }

        public bool AllowedToLeave { get; set; }

        public bool? LeavedAtOwnRisk { get; set; }

        public Guid? LeavePermissionDoctorId { get; set; }

        public string? LeavePermissionDoctorPwz { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder("VisitEntity [");
            sb.Append($"Id={Id}, ");
            sb.Append($"PatientId={PatientId}, ");
            sb.Append($"PatientPesel={PatientPesel}, ");
            sb.Append($"VisitStartDate={VisitStartDate}, ");
            if (VisitEndDate.HasValue)
                sb.Append($"VisitEndDate={VisitEndDate.Value}, ");
            sb.Append($"AllowedToLeave={AllowedToLeave}, ");
            if (LeavedAtOwnRisk.HasValue)
                sb.Append($"LeavedAtOwnRisk={LeavedAtOwnRisk.Value}, ");
            if (LeavePermissionDoctorId.HasValue)
                sb.Append($"LeavePermissionDoctorId={LeavePermissionDoctorId.Value}, ");
            if (!string.IsNullOrEmpty(LeavePermissionDoctorPwz))
                sb.Append($"LeavePermissionDoctorPwz={LeavePermissionDoctorPwz}, ");

            if (sb[sb.Length - 2] == ',')
                sb.Length -= 2;

            sb.Append("]");
            return sb.ToString();
        }
    }
}
