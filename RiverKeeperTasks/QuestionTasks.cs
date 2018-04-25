using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDAL;
using RiverKeeperDO;

namespace RiverKeeperTasks
{
    class QuestionTasks
    {
        QuestionDAO questionDAO = new QuestionDAO();

        public List<QuestionDO> GetQuestions()
        {
            return questionDAO.GetQuestions();
        }

        public bool CreateQuestion(QuestionDO question)
        {
            return questionDAO.CreateQuestion(question);
        }
    }
}
