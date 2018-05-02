using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverKeeperDO
{
    ///<summary>
    /// Marquette University
    /// MSCS 6931 701-702 Service-Oriented Architecture
    /// Spring 2018 
    /// 
    /// May 1st, 2018
    ///     
    /// User domain specific object
    /// 
    ///</summary>
    public class UserDO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Type { get; set; }
        public string EmailAddress { get; set; }
        public string ZipCode { get; set; }
    }
}
