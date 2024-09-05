using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Patient
{
    internal interface IPatientEventRepository
    {
        void Create(PatientEntity patient);
        void Update(PatientEntity patient);
        Task<IIsPatientInsuredResponse> IsPatientInsured(string patientPesel);
    }
}
