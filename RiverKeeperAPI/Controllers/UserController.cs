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
    //[Authorize]
    public class UserController : ApiController
    {
        UserTasks userTask = new UserTasks();
        // GET: api/User
        /// <summary>
        ///  Returns all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<UserDO> users = null;
 
            try
            {
                users = userTask.GetUsers();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Json(users);
        }

        // GET: api/User/5
        /// <summary>
        /// Get user by email address.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string email)
        {
            UserDO user = userTask.GetUser(email);

            return Json(user);
        }

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
