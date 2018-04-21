using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;
using RiverKeeperDAL;


namespace RiverKeeperTasks
{
    public class UserTasks
    {
        UserDAO userDAO = new UserDAO();

        public UserDO GetUser()
        {
            return userDAO.GetUser();
        }

        public bool CreateUser(UserDO user)
        {
            return userDAO.CreateUser(user);
        }
    }
}
