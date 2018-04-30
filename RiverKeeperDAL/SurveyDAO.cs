using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;

namespace RiverKeeperDAL
{
    public class SurveyDAO
    {
        //INFO: Return survey with given Id
        public SurveyDO GetSurvey(int id)
        {
            SurveyDO surveyDO = new SurveyDO();
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                List<Survey> surveys = (from s in RKEntities.Surveys where s.SurveyId == id
                                    select s).ToList();

                if(surveys.Count == 0)
                {
                    throw new Exception("No survey in DB with that ID");
                }
                Survey survey = surveys.ElementAt(0);

                surveyDO.SurveyId = survey.SurveyId;
                surveyDO.Name = survey.Name;
                surveyDO.CreationDate = survey.CreationDate;

                int numberOfQuestions = survey.Answers.Count();

                for (int i = 0; i < numberOfQuestions; i++)
                {
                    AnswerDO answerDO = new AnswerDO();
                    Answer answerdb = survey.Answers.ElementAt(i);
                    answerDO.Response = answerdb.Response;
                    answerDO.QuestionId = answerdb.QuestionId;
                    survey.Answers.Add(answerdb);
                }
            }
            return surveyDO;
        }

        //INFO: Adds a new survey to the database
        public bool SubmitSurvey(SurveyDO surveyDO)
        {
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                Console.WriteLine("Attempting to submit survey");
                Survey surveydb = new Survey();

                if (surveyDO != null)
                {
                    
                    surveydb.Name = surveyDO.Name;
                    surveydb.CreationDate = DateTime.Now;
                    surveydb.UserId = (from u in RKEntities.Users
                                       where u.EmailAddress.Equals(surveyDO.Email)
                                       select u.UserId).FirstOrDefault();

                    int numberOfQuestions = surveyDO.Answers.Count();
                    
                    for(int i = 0; i < numberOfQuestions; i++)
                    {
                        AnswerDO answerDO = surveyDO.Answers.ElementAt(i);
                        Answer answerdb = new Answer();
                        answerdb.Response = answerDO.Response;
                        answerdb.QuestionId = answerDO.QuestionId;
                        surveydb.Answers.Add(answerdb);
                    }

                    RKEntities.Surveys.Add(surveydb);
                }

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
                catch(DbUpdateException e)
                {
                  
                    Debug.WriteLine(e.InnerException);
                }

                if (dbResult == 0)
                {
                    return false;
                }

                return true;
            }
        }

        public List<Dictionary<string, string>> GetSurveyResponse(int surveyID)
        {

            List<Dictionary<string, string>> allAnswers = new List<Dictionary<string, string>>();

            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                //get all questions for the survey
                List<Question> questions = (from q in RKEntities.Questions
                                            where q.SurveyId == surveyID
                                            select q).ToList();

                List<int> uniqueSurveyIds = (from s in RKEntities.Answers
                                             select (int)s.Survey.SurveyId).Distinct().OrderBy(surveyid=>surveyid).ToList();
                if (questions == null || questions.Count == 0)
                {
                    allAnswers.Add(new Dictionary<string, string>()
                    {
                        { "error" , "Survey " + surveyID.ToString() + " doesn't have any answers" }
                    });
                }
                else
                {
                    foreach (var id in uniqueSurveyIds)
                    {
                        List<Answer> answers = (from a in RKEntities.Answers
                                                where a.Survey.SurveyId == id
                                                select a).ToList();
                        Dictionary<string, string> curAnswer = new Dictionary<string, string>();

                        //set up survey general data. ie. ID, completed by, creation date
                        curAnswer["SurveyID"] = id.ToString();
                        curAnswer["CreatedBy"] = (from x in RKEntities.Users
                                                  join surv in RKEntities.Surveys on x.UserId equals surv.UserId
                                                  where surv.SurveyId == id
                                                  select x.EmailAddress).FirstOrDefault();

                        curAnswer["CreatedOn"] = (from su in RKEntities.Surveys
                                                  where su.SurveyId == id
                                                  select su.CreationDate).FirstOrDefault().ToString();

                        foreach (var item in questions)
                        {
                            //add each question to the dictionary
                            foreach (var ans in answers)
                            {
                                //if there is an answer, provide it
                                if (ans.QuestionId == item.QuestionId)
                                {
                                    curAnswer[item.Wording] = ans.Response;
                                }
                                //otherwise leave it blank.
                                else
                                {
                                    if (!curAnswer.ContainsKey(item.Wording))
                                    {
                                        curAnswer[item.Wording] = "";
                                    }

                                }

                            }
                        }
                        allAnswers.Add(curAnswer);

                    }
                }
            }
            return allAnswers;
        }
    }
}
