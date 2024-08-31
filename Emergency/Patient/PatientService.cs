using Emergency.Bus;
using Emergency.Messages;
using Emergency.Validator;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Patient
{
    internal class PatientService : IPatientService
    {
        private readonly IValidatorService validator;

        private readonly IBusInstance busInstance;

        private readonly ILog logger;

        public PatientService(IBusOperator busOperator, ILog logger, IValidatorService validator) {
            this.validator = validator;
            this.logger = logger;
            busInstance = busOperator.CreateBusInstance();
        }

        public void AddPatient(PatientEntity patient)
        {
            throw new NotImplementedException();
        }

        public PatientEntity GetPatientByPesel(string pesel)
        {
            var validationResult = validator.validatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException();
            }
            throw new NotImplementedException();
            //return new Patient { Pesel = pesel, PatientId = Guid.NewGuid() };
        }
    }
}
