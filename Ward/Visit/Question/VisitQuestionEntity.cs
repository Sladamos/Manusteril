using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Visit.Question
{
    internal class VisitQuestionEntity
    {
        [Key]
        public Guid Id { get; set; }

        public required string PatientPesel { get; set; }

        public required bool Answered { get; set; }

        public required string Reason { get; set; }
    }
}
