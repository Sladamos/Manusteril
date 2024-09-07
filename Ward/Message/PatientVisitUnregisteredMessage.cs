﻿using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Messages
{
    internal class PatientVisitUnregisteredMessage : IPatientVisitUnregisteredMessage
    {
        public required Guid PatientId { get; set; }

        public required string PatientPesel { get; set; }

        public required Guid VisitId { get; set; }

        public DateTime VisitEndDate {  get; set; }
    }
}
