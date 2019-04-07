using System;
using System.Collections.Generic;

namespace ProgrammingCompetitionWebApplication
{
    public partial class ProgrammingTask
    {
        public ProgrammingTask()
        {
            Submissions = new HashSet<UserSubmission>();
        }

        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Result { get; set; }

        public virtual ICollection<UserSubmission> Submissions { get; set; }
    }
}
