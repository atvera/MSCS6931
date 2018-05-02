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
    /// Survey domain specific object
    /// 
    ///</summary>
    public class SurveyDO
    {
        public SurveyDO()
        {
            this.Answers = new HashSet<AnswerDO>();
        }

        public int SurveyId { get; set; }
        public string Name { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string Email { get; set; }
        public virtual ICollection<AnswerDO> Answers { get; set; }
    }
}
