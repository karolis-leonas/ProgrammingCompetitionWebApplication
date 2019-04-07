using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProgrammingCompetitionWebApplication.Models;
using ProgrammingCompetitionWebApplication.Dtos;
using ProgrammingCompetitionWebApplication.Services.SubmissionsService;
using Microsoft.Extensions.Logging;

namespace ProgrammingCompetitionWebApplication.Controllers
{
    [Route("api/[controller]")]
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService _submissionsService;
        private readonly ILogger _logger;

        public SubmissionsController(ISubmissionsService submissionsService,
            ILogger<SubmissionsController> logger)
        {
            _submissionsService = submissionsService;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public IEnumerable<TopSubmitterDto> TopSubmitters()
        {
            try
            {
                var topSubmitters = _submissionsService.GetTop5Submitters();

            var topSubmitterDtos = topSubmitters.Select(topSubmission => new TopSubmitterDto()
            {
                Nickname = topSubmission.Nickname,
                SolvedTasks = topSubmission.SolvedTasks,
                SuccessfulSubmissions = topSubmission.SuccessfulSubmissions
            });

            return topSubmitterDtos;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        [HttpGet("[action]")]
        public IEnumerable<BaseProgrammingTaskDto> ProgrammingTasks()
        {
            try
            {
                var programmingTasks = _submissionsService.GetProgrammingTasks();

                var programmingTaskDtos = programmingTasks.Select(programmingTask => new BaseProgrammingTaskDto()
                {
                    TaskId = programmingTask.TaskId,
                    TaskName = programmingTask.TaskName
                });

                return programmingTaskDtos;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }

        }

        [HttpPost("[action]")]
        public SubmissionResponse ExecuteSubmission([FromBody] BaseSubmissionDto baseSubmissionDto)
        {
            try
            {
                var baseSubmission = new BaseSubmission()
                {
                    Nickname = baseSubmissionDto.Nickname,
                    Solution = baseSubmissionDto.Solution,
                    TaskId = baseSubmissionDto.TaskId
                };

                var result = _submissionsService.ExecuteFiddleAsync(baseSubmission).Result;

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }
    }
}
