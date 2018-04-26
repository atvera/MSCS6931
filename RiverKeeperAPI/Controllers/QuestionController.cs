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
    public class QuestionController : ApiController
    {
        QuestionTasks questionTask = new QuestionTasks();

        // GET: api/Questions
        /// <summary>
        /// Returns the survey questions
        /// </summary>
        /// <returns>A JSON list of questions.</returns>
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
        [HttpPost]
        public bool Post()
        {
            return questionTask.CreateQuestions();
        }

        // PUT: api/Survey/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Survey/5
        public void Delete(int id)
        {
        }
    }
}
