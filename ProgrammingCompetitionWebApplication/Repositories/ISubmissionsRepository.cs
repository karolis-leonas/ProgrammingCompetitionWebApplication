using System.Collections.Generic;
using ProgrammingCompetitionWebApplication.Models;

namespace ProgrammingCompetitionWebApplication.Services.SubmissionsService
{
    public interface ISubmissionsRepository
    {
        SubmissionResponse SaveNewSubmission(BaseSubmission submission, string result);
        IEnumerable<TopSubmitter> GetTop5Submitters();
        IEnumerable<BaseProgrammingTask> GetProgrammingTasks();
    }
}
