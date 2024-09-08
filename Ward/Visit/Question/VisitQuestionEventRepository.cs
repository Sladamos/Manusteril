using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Bus;
using Ward.Config;
using Ward.Message;

namespace Ward.Visit.Question
{
    internal class VisitQuestionEventRepository : IVisitQuestionEventRepository
    {
        private IBusInstance busInstance;

        private ILog logger;

        private string wardIdentifier;

        public VisitQuestionEventRepository(IBusOperator busOperator, ILog logger, WardConfig wardConfig)
        {
            busInstance = busOperator.CreateBusInstance();
            this.logger = logger;
            this.wardIdentifier = wardConfig.WardIdentifier;
        }

        public void AcceptQuestion(VisitQuestionEntity questionEntity)
        {
            PatientVisitRegistrationAcceptedMessage message = new()
            {
                PatientPesel = questionEntity.PatientPesel,
                WardIdentifier = wardIdentifier
            };
            logger.Info($"Wysłanie informacji o możliwości przyjęcia pacjenta {questionEntity.PatientPesel}");
            busInstance.Publish( message );
        }

        public void DeclineQuestion(VisitQuestionEntity questionEntity)
        {

            PatientVisitRegistrationDeclinedMessage message = new()
            {
                PatientPesel = questionEntity.PatientPesel,
                WardIdentifier = wardIdentifier,
                Reason = questionEntity.Reason!
            };
            logger.Info($"Wysłanie informacji o braku możliwości przyjęcia pacjenta {questionEntity.PatientPesel}");
            busInstance.Publish(message);
        }
    }
}
