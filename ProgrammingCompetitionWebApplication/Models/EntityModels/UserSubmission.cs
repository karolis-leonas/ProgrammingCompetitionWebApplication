using System;
using System.Collections.Generic;

namespace ProgrammingCompetitionWebApplication
{
    public partial class UserSubmission
    {
        public int SubmissionId { get; set; }
        public string Nickname { get; set; }
        public int TaskId { get; set; }
        public string Result { get; set; }
        public bool IsCorrect { get; set; }
        public string Code { get; set; }
        public DateTime SubmissionDateTime { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ProgrammingTask Task { get; set; }
    }
}
