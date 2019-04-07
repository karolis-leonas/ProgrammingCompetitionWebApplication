using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingCompetitionWebApplication.Models
{
    public class Submission
    {
        public string Nickname { get; set; }
        public int TaskId { get; set; }
        public string Solution { get; set; }
    }
}
