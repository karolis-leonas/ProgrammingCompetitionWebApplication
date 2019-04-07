using ProgrammingCompetitionWebApplication.Models;
using System.Threading.Tasks;

namespace ProgrammingCompetitionWebApplication.Services.SubmissionsService
{
    public interface ISubmissionsService
    {
        Task<SubmissionResponse> ExecuteFiddleAsync(Submission submission);
    }
}
