using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverKeeperDO
{
    public class SurveyDO
    { 
        public int SurveyId { get; set; }
        public string Name { get; set; }
        public System.DateTime CreationDate { get; set; }
        public bool isTemplate { get; set; }
    }
}
