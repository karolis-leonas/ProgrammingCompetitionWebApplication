using Newtonsoft.Json;
using ProgrammingCompetitionWebApplication.Enums;
using ProgrammingCompetitionWebApplication.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingCompetitionWebApplication.Services.SubmissionsService
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly HttpClient _httpClient; // declare a HttpClient

        public SubmissionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SubmissionResponse> ExecuteFiddleAsync(Submission submission)
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
            }
            else
            {
                var unparsedResponse = await response.Content.ReadAsStringAsync();
                var executionResult = JsonConvert.DeserializeObject<FiddleExecuteResult>(unparsedResponse);

                result.Response = executionResult.ConsoleOutput;
            }

            return result;
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
