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

        private readonly ILog logger;

        public VisitQuestionService(IVisitQuestionRepository questionRepository, ILog logger)
        {
            this.questionRepository = questionRepository;
            this.logger = logger;
        }

        public void AddQuestion(VisitQuestionEntity question)
        {
            questionRepository.Save(question);
        }

        public List<VisitQuestionEntity> GetQuestions()
        {
            return questionRepository.GetAll().ToList();
        }
    }
}
