using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Messages
{
    internal class PatientUnregisteredMessage : IPatientUnregisteredMessage
    {
        public Guid pateintId { get; set; }
    }
}
