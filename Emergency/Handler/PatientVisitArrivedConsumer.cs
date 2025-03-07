﻿using Emergency.Bus;
using Emergency.Visit;
using log4net;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Handler
{
    internal class PatientVisitArrivedConsumer : IBusConsumer<IPatientVisitArrivedMessage>
    {
        private ILog logger;

        private IVisitService visitService;

        public PatientVisitArrivedConsumer(ILog logger, IVisitService visitService)
        {
            this.logger = logger;
            this.visitService = visitService;
        }

        public string QueueName => "emergency_patientVisitArrived";

        public async Task Consume(ConsumeContext<IPatientVisitArrivedMessage> context)
        {
            try
            {
                var message = context.Message;
                logger.Info($"Otrzymano potwierdzenie przybycia pacjenta: {message}");
                visitService.MarkVisitAsInProgress(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy obsłusze potwierdzania przybycia: {ex.Message}");
                throw;
            }
        }
    }
}
