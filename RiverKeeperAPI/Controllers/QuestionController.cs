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

    ///<summary>
    /// Marquette University
    /// MSCS 6931 701-702 Service-Oriented Architecture
    /// Spring 2018 
    /// 
    /// May 1st, 2018
    ///     
    /// API Controller class for Question
    /// 
    ///</summary>
    [Authorize]
    public class QuestionController : ApiController
    {
        QuestionTasks questionTask = new QuestionTasks();

        // GET: api/Questions
        /// <summary>
        /// Returns the survey questions
        /// </summary>
        /// <returns>A JSON list of questions.</returns>
        [Route("api/AllQuestions/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {

            List<QuestionDO> questions = null;
            questions = questionTask.GetQuestions(id);

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
