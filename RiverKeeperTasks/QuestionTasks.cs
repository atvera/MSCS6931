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
using System.Diagnostics;

namespace RiverKeeperTasks
{
    public class RootObject
    {
        public List<QuestionDO> Questions { get; set; }
    }

    ///<summary>
    /// Marquette University
    /// MSCS 6931 701-702 Service-Oriented Architecture
    /// Spring 2018 
    /// 
    /// May 1st, 2018
    ///     
    /// Business Logic Layer for Questions
    /// 
    ///</summary>
    public class QuestionTasks
    {
        QuestionDAO questionDAO = new QuestionDAO();

        public List<QuestionDO> GetQuestions(int id)
        {
            return questionDAO.GetQuestions(id);
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
                Debug.WriteLine(question);
                result = CreateQuestion(question);
            }

            return result; 
        }

    }
}
