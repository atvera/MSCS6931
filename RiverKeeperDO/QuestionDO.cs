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
    /// Question domain specific object
    /// 
    ///</summary>
    public class QuestionDO
    {
        public int QuestionId { get; set; }
        public short Type { get; set; }
        public string Wording { get; set; }
        public string [] PossibleAnswers { get; set; }
        public int SurveyId { get; set;}
    }
}
