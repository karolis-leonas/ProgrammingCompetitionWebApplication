using System.Collections.Generic;

namespace ProgrammingCompetitionWebApplication.Models
{
    public class TopSubmitter
    {
        public string Nickname { get; set; }
        public int SuccessfulSubmissions { get; set; }
        public List<string> SolvedTasks { get; set; }
    }
}
