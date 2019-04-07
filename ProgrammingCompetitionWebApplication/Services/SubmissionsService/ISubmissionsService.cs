using ProgrammingCompetitionWebApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgrammingCompetitionWebApplication.Services.SubmissionsService
{
    public interface ISubmissionsService
    {
        Task<SubmissionResponse> ExecuteFiddleAsync(BaseSubmission submission);
        IEnumerable<TopSubmitter> GetTop5Submitters();
        IEnumerable<BaseProgrammingTask> GetProgrammingTasks();
    }
}
