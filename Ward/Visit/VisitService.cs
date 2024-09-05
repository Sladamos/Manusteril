using Ward.Patient;
using Ward.Validator;
using log4net;
using log4net.Core;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Visit
{
    internal partial class VisitService : IVisitService
    {
        private readonly IValidatorService validator;

        private readonly IVisitRepository visitRepository;

        private readonly IVisitEventRepository eventRepository;

        private readonly ILog logger;

        public VisitService(IValidatorService validator,
            IVisitRepository visitRepository,
            IVisitEventRepository eventRepository,
            ILog logger)
        {
            this.validator = validator;
            this.visitRepository = visitRepository;
            this.eventRepository = eventRepository;
            this.logger = logger;
        }

        public void SetRoomForPatient(PatientEntity patient, string number)
        {
            logger.Info($"Rozpoczęto przypisywanie salidla pacjenta {patient.Pesel}");
            var visit = visitRepository.GetPatientCurrentVisit(patient.Pesel);
            visit.PatientRoomNumber = number;
            visitRepository.Save(visit);
            Console.WriteLine("TODO send message");
            logger.Info($"Przypisano do pacjenta {patient.Pesel} salę {number}");
        }
    }
}
