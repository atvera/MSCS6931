using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverKeeperDO
{
    public class QuestionDO
    {
        public int QuestionId { get; set; }
        public short Type { get; set; }
        public string Wording { get; set; }
        public string [] PossibleAnswers { get; set; }
        public int SurveyId { get; set;}
    }
}
