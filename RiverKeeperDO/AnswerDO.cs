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
    /// Answer domain specific object
    /// 
    ///</summary>
    public class AnswerDO
    {
        public string Response { get; set; }
        public int QuestionId { get; set; }
    }
}
