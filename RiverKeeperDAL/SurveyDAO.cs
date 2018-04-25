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
                surveyDO.isTemplate = survey.isTemplate;

                int numberOfQuestions = survey.Questions.Count();

                for (int i = 0; i < numberOfQuestions; i++)
                {
                    Question questiondb = survey.Questions.ElementAt(i);
                    QuestionDO questionDO = new QuestionDO();
                    questionDO.QuestionId = questiondb.QuestionId;
                    questionDO.Wording = questiondb.Wording;
                    questionDO.Type = questiondb.Type;
                    surveyDO.Questions.Add(questionDO);
                }

                if(!survey.isTemplate)
                {
                    //surveyDO.Answers = survey.Answers;
                }
            }
            return surveyDO;
        }

        //INFO: Return survey template
        public SurveyDO GetSurveyTemplate()
        {
            SurveyDO surveyDO = new SurveyDO();
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                List<Survey> surveys = (from s in RKEntities.Surveys
                                        where s.isTemplate == true
                                        select s).ToList();

                if (surveys.Count == 0)
                {
                    throw new Exception("No template survey stored in DB");
                }
                Survey survey = surveys.ElementAt(0);

                surveyDO.SurveyId = survey.SurveyId;
                surveyDO.Name = survey.Name;
                surveyDO.CreationDate = survey.CreationDate;
                surveyDO.isTemplate = survey.isTemplate;

                int numberOfQuestions = survey.Questions.Count();

                for (int i = 0; i < numberOfQuestions; i++)
                {
                    Question questiondb = survey.Questions.ElementAt(i);
                    QuestionDO questionDO = new QuestionDO();
                    questionDO.QuestionId = questiondb.QuestionId;
                    questionDO.Wording = questiondb.Wording;
                    questionDO.Type = questiondb.Type;
                    surveyDO.Questions.Add(questionDO);
                }
            }
            return surveyDO;
        }

        //INFO: Adds a survey response to the database
        public bool SubmitSurvey(SurveyDO surveyDO)
        {
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                Console.WriteLine("Attempting to submit survey");
                Survey surveydb = new Survey();

                if (surveyDO != null)
                {
                    surveydb.Name = surveyDO.Name;
                    surveydb.isTemplate = false;
                    surveydb.CreationDate = DateTime.Now;
                    surveydb.UserId = 1; //TODO: This will be set to the UserID that is logged in (filling the survey)

                    int numberOfQuestions = surveyDO.Questions.Count();
                    
                    for(int i = 0; i < numberOfQuestions; i++)
                    {
                        QuestionDO questionDO = surveyDO.Questions.ElementAt(i);
                        Question questiondb = new Question();
                        questiondb.Wording = questionDO.Wording;
                        questiondb.Type = questionDO.Type;
                        surveydb.Questions.Add(questiondb);
                    }

                    // for each answer, surveydb.Answers.Add(answerdb)

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

                if (dbResult == 0)
                {
                    return false;
                }

                return true;
            }
        }

        //INFO: Create a survey template
        public bool CreateSurveyTemplate(SurveyDO surveyDO)
        {
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                Console.WriteLine("Attempting to create a template");
                Survey surveydb = new Survey();

                if (surveyDO != null)
                {
                    List<Survey> surveys = (from s in RKEntities.Surveys
                                            where s.isTemplate == true
                                            select s).ToList();

                    //Only one template allowed for now
                    if (surveys.Count > 0)
                    {
                        Debug.WriteLine("Existing template has id {0}. Modify existing one", surveys.ElementAt(0).SurveyId.ToString());
                        throw new Exception("Template already exists");
                    }
                   
                    surveydb.Name = surveyDO.Name;
                    surveydb.isTemplate = true;
                    surveydb.CreationDate = DateTime.Now;
                    surveydb.UserId = 1; //TODO: This will be set to the UserID that is logged in (filling the survey)

                    int numberOfQuestions = surveyDO.Questions.Count();

                    for (int i = 0; i < numberOfQuestions; i++)
                    {
                        QuestionDO questionDO = surveyDO.Questions.ElementAt(i);
                        Question questiondb = new Question();
                        questiondb.Wording = questionDO.Wording;
                        questiondb.Type = questionDO.Type;
                        surveydb.Questions.Add(questiondb);
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

                if (dbResult == 0)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
