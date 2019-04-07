using Newtonsoft.Json;
using ProgrammingCompetitionWebApplication.Enums;
using ProgrammingCompetitionWebApplication.Models;
using ProgrammingCompetitionWebApplication.Models.CompilerModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingCompetitionWebApplication.Services.SubmissionsService
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly HttpClient _httpClient; // declare a HttpClient
        private readonly ISubmissionsRepository _submissionsRepository;

        public SubmissionsService(HttpClient httpClient, ISubmissionsRepository submissionsRepository)
        {
            _httpClient = httpClient;
            _submissionsRepository = submissionsRepository;
        }

        public async Task<SubmissionResponse> ExecuteFiddleAsync(BaseSubmission submission)
        {
            var model = ConfigureSubmissionForm(submission.Solution);
            const string requestEndpoint = "api/fiddles";

            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestEndpoint, stringContent);

            var result = new SubmissionResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                result.Response = 
                    $@"Failed to execute API request. Here is an answer from API:
Response Code: {response.StatusCode}, Response Body: {response.Content}";
                result.IsSubmissionAccepted = false;
            }
            else
            {
                var unparsedResponse = await response.Content.ReadAsStringAsync();
                var executionResult = JsonConvert.DeserializeObject<FiddleExecuteResult>(unparsedResponse);
                if (!executionResult.HasErrors && !executionResult.HasCompilationErrors)
                {
                    result = _submissionsRepository.SaveNewSubmission(submission, executionResult.ConsoleOutput);
                }
                else
                {
                    result.Response = executionResult.ConsoleOutput;
                    result.IsSubmissionAccepted = false;
                }
            }

            return result;
        }

        public IEnumerable<BaseProgrammingTask> GetProgrammingTasks()
        {
            var programmingTasks = _submissionsRepository.GetProgrammingTasks();

            return programmingTasks;
        }

        public IEnumerable<TopSubmitter> GetTop5Submitters()
        {
            var topSubmitters = _submissionsRepository.GetTop5Submitters();

            return topSubmitters;
        }

        private FiddleModel ConfigureSubmissionForm(string code)
        {
            return new FiddleModel()
            {
                Compiler = Compiler.Net45,
                Language = ProgrammingLanguage.CSharp,
                ProjectType = ProjectType.Console,
                CodeBlock = code
            };
        }
    }
}
