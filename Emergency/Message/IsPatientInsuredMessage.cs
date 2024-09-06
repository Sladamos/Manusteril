using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Message
{
    internal class IsPatientInsuredMessage : IIsPatientInsuredMessage
    {

        public required string PatientPesel { get; set; }
    }
}
