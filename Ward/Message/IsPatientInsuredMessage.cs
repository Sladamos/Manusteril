using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Message
{
    internal class IsPatientInsuredMessage : IIsPatientInsured
    {

        public required string PatientPesel { get; set; }
    }
}
