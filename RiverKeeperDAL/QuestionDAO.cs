using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;

namespace RiverKeeperDAL
{
    public class QuestionDAO
    {
        //INFO: Return all questions in the database.
        public List<QuestionDO> GetQuestions()
        {
            List<QuestionDO> questionDOs = new List<QuestionDO>();
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                List<Question> questions = (from q in RKEntities.Questions
                                            select q).ToList();
                foreach (var question in questions)
                {
                    if (question != null)
                    {
                        QuestionDO questionDO = new QuestionDO()
                        {
                            QuestionId = question.QuestionId,
                            Type = (short)question.Type,
                            Wording = question.Wording,
                            PossibleAnswers = question.PossibleAnswers
                        };
                        questionDOs.Add(questionDO);
                    }
                    if (question == null)
                    {
                        throw new Exception("No questions in DB");
                    }
                }

            }
            return questionDOs;
        }

        //INFO: Adds a question to the database.
        public bool CreateQuestion(QuestionDO questionDO)
        {
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                Question questiondb = new Question();

                questiondb.Wording = questionDO.Wording;
                questiondb.Type = (ResponseFormat)questionDO.Type;
                questiondb.PossibleAnswers = questionDO.PossibleAnswers;
                RKEntities.Questions.Add(questiondb);

                int dbResult = 0;

                try
                {
                    dbResult = RKEntities.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

                if (dbResult != 1)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
