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
using System.Web.Http.Cors;

namespace RiverKeeperAPI.Controllers
{
    [EnableCors(origins: "https://localhost:44370", headers: "*", methods: "*")]
    [Authorize]
    public class UserController : ApiController
    {
        UserTasks userTask = new UserTasks();
        // GET: api/User
        /// <summary>
        ///  Returns all users
        /// </summary>
        /// <returns></returns>
        [Authorize]
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

        // GET: api/User/?email={email}
        /// <summary>
        ///    Get a user by their email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string email)
        {
            UserDO user = userTask.GetUser(email);

            return Json(user);
        }

        /// <summary>
        ///   Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
         [HttpPost]
         public bool Post(UserDO user)
        {
            return userTask.CreateUser(user);
        }

        //// PUT: api/User/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/User/5
        //public void Delete(int id)
        //{
        //}
    }
}
