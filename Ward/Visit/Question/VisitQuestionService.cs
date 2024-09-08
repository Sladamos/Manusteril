using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Patient;
using Ward.Validator;

namespace Ward.Visit.Question
{
    internal class VisitQuestionService : IVisitQuestionService
    {
        private readonly IVisitQuestionRepository questionRepository;

        private IVisitQuestionEventRepository questionEventRepository;

        private IValidatorService validatorService;

        private readonly ILog logger;

        public VisitQuestionService(IVisitQuestionRepository questionRepository,
            IVisitQuestionEventRepository eventRepository,
            IValidatorService validatorService,
            ILog logger)
        {
            this.questionRepository = questionRepository;
            this.questionEventRepository = eventRepository;
            this.validatorService = validatorService;
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
            question.Answered = true;
            questionRepository.Save(question);
            questionEventRepository.AcceptQuestion(question);
            logger.Info($"Wiadomość wysłana pomyślnie");
        }

        public void DeclineQuestion(VisitQuestionEntity question)
        {
            logger.Info($"Próba wysłania informacji o braku możliwości przyjęcia pacjenta {question.PatientPesel}");
            question.Answered = true;
            questionRepository.Save(question);
            questionEventRepository.DeclineQuestion(question);
            logger.Info($"Wiadomość wysłana pomyślnie");
        }

        public bool IsUnansweredQuestionForPatient(string patientPesel)
        {
            return GetQuestions()
                .Where(question => !question.Answered && question.PatientPesel == patientPesel)
                .Count() > 0;
        }

        public VisitQuestionEntity GetQuestionAboutPatient(string pesel)
        {
            ValidatePesel(pesel);
            return GetQuestions()
                .FirstOrDefault(question => question.PatientPesel == pesel && !question.Answered)
                ?? throw new QuestionMissingException("Brak pytania w sprawie pacjenta");
        }

        private void ValidatePesel(string pesel)
        {
            var validationResult = validatorService.ValidatePesel(pesel);
            if (!validationResult.IsValid)
            {
                throw new InvalidPeselException(validationResult.ValidatorMessage);
            }
        }
    }
}
