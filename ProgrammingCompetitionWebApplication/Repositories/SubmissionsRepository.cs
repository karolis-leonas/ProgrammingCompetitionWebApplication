using ProgrammingCompetitionWebApplication.DataContexts;
using ProgrammingCompetitionWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingCompetitionWebApplication.Services.SubmissionsService
{
    public class SubmissionsRepository : ISubmissionsRepository
    {
        private ProgrammingCompetitionDataContext _programmingCompetitionDataContext;

        public SubmissionsRepository(ProgrammingCompetitionDataContext programmingCompetitionDataContext)
        {
            _programmingCompetitionDataContext = programmingCompetitionDataContext;
        }

        public IEnumerable<BaseProgrammingTask> GetProgrammingTasks()
        {
            var programmingTasks = _programmingCompetitionDataContext.ProgrammingTask
                .Select(submission => new BaseProgrammingTask()
                {
                    TaskId = submission.TaskId,
                    TaskName = submission.TaskName
                })
                .AsEnumerable();

            return programmingTasks;
        }

        public IEnumerable<TopSubmitter> GetTop5Submitters()
        {
            var topSubmitterGroups =
                _programmingCompetitionDataContext.UserSubmission.GroupBy(submission => submission.Nickname)
                    .OrderBy(submitterGroup => submitterGroup.Count())
                    .Take(5);

            var topSubmitters = new List<TopSubmitter>();
            foreach (var topSubmitterGroup in topSubmitterGroups)
            {
                var solvedTaskIds = topSubmitterGroup.Select(topSubmitterItem => topSubmitterItem.TaskId);

                var topSubmitter = new TopSubmitter()
                {
                    Nickname = topSubmitterGroup.Key,
                    SuccessfulSubmissions = topSubmitterGroup.Count(),
                    SolvedTasks = _programmingCompetitionDataContext.ProgrammingTask
                        .Where(task => solvedTaskIds.Contains(task.TaskId))
                        .Select(task => task.TaskName).ToList()
                };

                topSubmitters.Add(topSubmitter);
            }
            
            return topSubmitters.OrderByDescending(topSubmitter => topSubmitter.SuccessfulSubmissions);
        }

        public SubmissionResponse SaveNewSubmission(BaseSubmission submission, string result)
        {
            var response = new SubmissionResponse();
            if (!DoesUserTaskSubmissionExist(submission.TaskId, submission.Nickname))
            {
                var newSubmission = new UserSubmission()
                {
                    Code = submission.Solution,
                    IsCorrect = DoesTaskHaveCorrectResult(submission.TaskId, result),
                    IsDeleted = false,
                    Nickname = submission.Nickname,
                    Result = result,
                    SubmissionDateTime = DateTime.UtcNow,
                    TaskId = submission.TaskId
                };

                _programmingCompetitionDataContext.UserSubmission.Add(newSubmission);
                _programmingCompetitionDataContext.SaveChanges();

                response.IsCorrectResult = newSubmission.IsCorrect;
                response.IsSubmissionAccepted = true;
                response.Response = newSubmission.Result;
            }
            else
            {
                response.IsSubmissionAccepted = false;
                response.Response = "There is already a submission for this task with current user nickname!";
            }

            return response;
        }

        private bool DoesTaskHaveCorrectResult(int taskId, string result)
        {
            var programmingTask = _programmingCompetitionDataContext.ProgrammingTask
                .Where(task => task.TaskId == taskId)
                .FirstOrDefault();
                
            return programmingTask.Result == result;
        }

        private bool DoesUserTaskSubmissionExist(int taskId, string nickName)
        {
            var userTaskSubmissionCount = _programmingCompetitionDataContext.UserSubmission
                       .Where(submission => submission.TaskId == taskId && submission.Nickname == nickName).Count();
                
            return userTaskSubmissionCount > 0;
        }

    }
}
