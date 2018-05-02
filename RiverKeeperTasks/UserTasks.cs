using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;
using RiverKeeperDAL;


namespace RiverKeeperTasks
{
    ///<summary>
    /// Marquette University
    /// MSCS 6931 701-702 Service-Oriented Architecture
    /// Spring 2018 
    /// 
    /// May 1st, 2018
    ///     
    /// Business Logic Layer for Users
    /// 
    ///</summary>
    public class UserTasks
    {
        UserDAO userDAO = new UserDAO();

        public UserDO GetUser(string email)
        {
            return userDAO.GetUser(email);
        }

        public List<UserDO> GetUsers()
        {
            return userDAO.GetUsers();
        }

        public bool CreateUser(UserDO user)
        {
            return userDAO.CreateUser(user);
        }
    }
}
