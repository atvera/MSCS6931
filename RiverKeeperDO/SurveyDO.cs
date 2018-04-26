using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverKeeperDO
{
    public class SurveyDO
    {
        public SurveyDO()
        {
            this.Answers = new HashSet<AnswerDO>();
        }

        public int SurveyId { get; set; }
        public string Name { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<AnswerDO> Answers { get; set; }
    }
}
