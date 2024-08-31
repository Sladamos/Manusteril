using Lombok.NET;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Visit
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
            return $"VisitEntity [Id={Id}, PatientId={PatientId}, PatientPesel={PatientPesel}, " +
                   $"VisitStartDate={VisitStartDate}, VisitEndDate={VisitEndDate}, " +
                   $"AllowedToLeave={AllowedToLeave}, LeavedAtOwnRisk={LeavedAtOwnRisk}, " +
                   $"LeavePermissionDoctorId={LeavePermissionDoctorId}, LeavePermissionDoctorPwz={LeavePermissionDoctorPwz}]";
        }
    }
}
