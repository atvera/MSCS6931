using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using RiverKeeperDO;
using RiverKeeperTasks;
using System.Web.Script.Serialization;

namespace RiverKeeperAPI.Controllers
{ 
    public class UserController : ApiController
    {
        UserTasks userTask = new UserTasks();
        // GET: api/User
        [HttpGet]
        public IHttpActionResult Get()
        {
            UserDO userDO = null;
            try
            {
                userDO = userTask.GetUser();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Json(userDO);
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        //[HttpPost]
        //public string Post([FromBody]string value)
        //{
        //    Console.WriteLine(value);
        //    return value;
        //}

         [HttpPost]
         public bool Post(UserDO user)
        {
            return userTask.CreateUser(user);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
