using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RiverKeeperDO;
using RiverKeeperTasks;

namespace RiverKeeperAPI.Controllers
{
    [Authorize]
    public class QuestionController : ApiController
    {
        QuestionTasks questionTask = new QuestionTasks();

        public IHttpActionResult Get(int id)
        {
            QuestionDO question = questionTask.GetQuestionById(id);

            return Json(question);
        }

        // GET: api/Questions
        /// <summary>
        /// Returns the survey questions
        /// </summary>
        /// <returns>A JSON list of questions.</returns>
        [Route("api/AllQuestions")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<QuestionDO> questions = null;
            try
            {
                questions = questionTask.GetQuestions();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Json(questions);
        }

        // POST: api/Questions
        /// <summary>
        /// Creates the list of survey questions from an internal file
        /// </summary>
        /// <returns>True if transaction was successful</returns>
        [Route("api/CreateQuestions")]
        [HttpPost]
        public bool Post()
        {
            return questionTask.CreateQuestions();
        }

        // POST: api/Question
        /// <summary>
        /// Creates a question
        /// </summary>
        /// <returns>True if transaction was successful</returns>
        [Route("api/Question")]
        [HttpPost]
        public bool Post(QuestionDO question)
        {
            return questionTask.CreateQuestion(question);
        }
    }
}
