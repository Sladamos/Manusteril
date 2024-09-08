using log4net;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Bus;
using Ward.Config;
using Ward.Visit;
using Ward.Visit.Question;

namespace Ward.Handler
{
    internal class PatientVisitRegisteredHandler : IBusConsumer<IPatientVisitRegisteredMessage>
    {
        private ILog logger;

        private IVisitQuestionService questionService;

        private string wardIdentifier;
        
        private WardType wardType;

        public PatientVisitRegisteredHandler(ILog logger, IVisitQuestionService questionService, WardConfig wardConfig)
        {
            this.logger = logger;
            this.questionService = questionService;
            wardIdentifier = wardConfig.WardIdentifier;
            wardType = WardTypeExtensions.FromPolish(wardConfig.WardType);
        }

        public string QueueName => $"{wardIdentifier}_visitQuestion";

        public async Task Consume(ConsumeContext<IPatientVisitRegisteredMessage> context)
        {
            try
            {
                var message = context.Message;
                if (wardType == message.WardType)
                {
                    logger.Info($"Otrzymano zapytanie o przyjęcie pacjenta: {message}");
                    var question = new VisitQuestionEntity
                    {
                        Id = Guid.NewGuid(),
                        Answered = false,
                        PatientPesel = message.PatientPesel
                    };
                    questionService.AddQuestion(question);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze potwierdzenia: {ex.Message}");
                throw;
            }
        }
    }
}
