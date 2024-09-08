using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Room;
using Ward.Visit.Question;

namespace Ward.Command.Patients.ConsidierPatientAcceptance
{
    internal class ConsiderPatientAcceptanceLogicCommand : ICommand
    {
        private IVisitQuestionService questionService;

        private Func<string> peselProvider;

        private Func<bool> decisionProvider;

        private Func<string> reasonProvider;

        public string Name => "Odpowiedz";

        public string Description => "Odpowiedz na prośbę o przyjęcie pacjenta";

        public Action? OnAnswerSent;

        public ConsiderPatientAcceptanceLogicCommand(IVisitQuestionService questionService,
            Func<string> peselProvider,
            Func<bool> decisionProvider,
            Func<string> reasonProvider)
        {
            this.questionService = questionService;
            this.peselProvider = peselProvider;
            this.decisionProvider = decisionProvider;
            this.reasonProvider = reasonProvider;
        }

        public async Task Execute()
        {
            try
            {
                string pesel = peselProvider();
                var question = questionService.GetQuestionAboutPatient(pesel);
                if (decisionProvider())
                {
                    questionService.AcceptQuestion(question);
                }
                else
                {
                    question.Reason = reasonProvider();
                    questionService.DeclineQuestion(question);
                }
                OnAnswerSent?.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
