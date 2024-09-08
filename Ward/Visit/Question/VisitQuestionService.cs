using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Validator;

namespace Ward.Visit.Question
{
    internal class VisitQuestionService : IVisitQuestionService
    {
        private readonly IVisitQuestionRepository questionRepository;

        private IVisitQuestionEventRepository questionEventRepository;

        private readonly ILog logger;

        public VisitQuestionService(IVisitQuestionRepository questionRepository,
            IVisitQuestionEventRepository eventRepository,
            ILog logger)
        {
            this.questionRepository = questionRepository;
            this.questionEventRepository = eventRepository;
            this.logger = logger;
        }

        public void AddQuestion(VisitQuestionEntity question)
        {
            logger.Info($"Rozpoczęto dodawanie zapytania o pacjenta {question.PatientPesel}");
            questionRepository.Save(question);
            logger.Info($"Dodanie zapytania o pacjenta {question.PatientPesel}");
        }

        public List<VisitQuestionEntity> GetQuestions()
        {
            logger.Info($"Pobranie wszystkich pytań");
            return questionRepository.GetAll().ToList();
        }

        public void AcceptQuestion(VisitQuestionEntity question)
        {
            logger.Info($"Próba wysłania informacji o możliwości przyjęcia pacjenta {question.PatientPesel}");
            questionEventRepository.AcceptQuestion(question);
            logger.Info($"Wiadomość wysłana pomyślnie");
        }

        public void DeclineQuestion(VisitQuestionEntity question)
        {
            logger.Info($"Próba wysłania informacji o braku możliwości przyjęcia pacjenta {question.PatientPesel}");
            questionEventRepository.DeclineQuestion(question);
            logger.Info($"Wiadomość wysłana pomyślnie");
        }
    }
}
