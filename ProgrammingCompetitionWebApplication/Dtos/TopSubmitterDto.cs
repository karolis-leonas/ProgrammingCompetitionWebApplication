using System.Collections.Generic;

namespace ProgrammingCompetitionWebApplication.Dtos
{
    public class TopSubmitterDto
    {
        public string Nickname { get; set; }
        public int SuccessfulSubmissions { get; set; }
        public List<string> SolvedTasks { get; set; }
    }
}
