using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using RiverKeeperDAL;
using RiverKeeperDO;

namespace RiverKeeperTasks
{
    public class RootObject
    {
        public List<QuestionDO> Questions { get; set; }
    }
    public class QuestionTasks
    {
        QuestionDAO questionDAO = new QuestionDAO();

        public List<QuestionDO> GetQuestions()
        {
            return questionDAO.GetQuestions();
        }

        public QuestionDO GetQuestionById(int id)
        {
            return questionDAO.GetQuestionById(id);
        }

        public bool CreateQuestion(QuestionDO question)
        {
            if(question == null)
            {
                return false;
            }
            return questionDAO.CreateQuestion(question);
        }

        public bool CreateQuestions()
        {
            bool result = false;

            string appDataPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
            string questionsFullPath = appDataPath + "AestheticsMonitoringForm.json";
            string json = File.ReadAllText(questionsFullPath);
            RootObject questionsList = JsonConvert.DeserializeObject<RootObject>(json);
            
            foreach (QuestionDO question in questionsList.Questions)
            {
                result = CreateQuestion(question);
            }

            return result; //TODO: Change return value. we could be returning true even if we had issues adding a single question
        }

    }
}
