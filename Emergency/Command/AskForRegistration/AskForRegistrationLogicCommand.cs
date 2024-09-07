using Emergency.Bus;
using Emergency.Messages;
using Emergency.Patient;
using Emergency.Visit;
using log4net;
using MassTransit;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command.AskForRegistration
{
    internal class AskForRegistrationLogicCommand : ICommand
    {
        private IVisitService visitService;

        private Func<string> peselSupplier;

        private Func<WardType> wardSupplier;

        public string Name => "Zapytaj";

        public string Description => "Zapytaj czy ktoś przyjmie pacjenta";

        public Action? OnQuestionSent;

        public AskForRegistrationLogicCommand(IVisitService visitService,
            Func<string> peselSupplier,
            Func<WardType> wardSupplier) 
        {
            this.visitService = visitService;
            this.peselSupplier = peselSupplier;
            this.wardSupplier = wardSupplier;
        }

        public async Task Execute()
        {
            Console.WriteLine("Zapytanie o przyjęcie");
            WardType ward = wardSupplier();
            if (ward == WardType.NONE)
            {
                Console.WriteLine("Najpierw należy wybrać oddział");
            }
            else
            {
                try
                {
                    RegisterVisit(ward);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void RegisterVisit(WardType ward)
        {
            string pesel = peselSupplier();
            visitService.AskForRegistration(ward, pesel);
            OnQuestionSent?.Invoke();
        }
    }
}
