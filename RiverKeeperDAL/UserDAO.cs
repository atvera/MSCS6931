using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverKeeperDO;

namespace RiverKeeperDAL
{
    public class UserDAO
    {
        //INFO: Return all users in the database.
        public List<UserDO> GetUsers()
        {
            List<UserDO> UserDOs = new List<UserDO>();
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
               List<User> Users =(from u in RKEntities.Users
                     select u).ToList();
                foreach (var user in Users)
                {


                    if (user != null)
                    {
                        UserDO userDO = new UserDO()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Type = user.Type,
                            EmailAddress = user.EmailAddress,
                            ZipCode = user.ZipCode
                        };
                        UserDOs.Add(userDO);

                    }
                    if (user == null)
                    {
                        throw new Exception("No user in DB");
                    }
                }

            }
            return UserDOs;
        }

        public bool CreateUser(UserDO userDO)
        {
            using (riverkeeperEntities RKEntities = new riverkeeperEntities())
            {
                User userdb = new User();

                if (userDO != null)
                {
                    userdb.FirstName = userDO.FirstName;
                    userdb.LastName = userDO.LastName;
                    userdb.EmailAddress = userDO.EmailAddress;
                    userdb.PhoneNumber = userDO.PhoneNumber;
                    userdb.Type = userDO.Type;
                    userdb.ZipCode = userDO.ZipCode;
                    RKEntities.Users.Add(userdb);
                }


                int dbResult = 0;

                try
                {
                    dbResult = RKEntities.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }



                if (dbResult != 1)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
