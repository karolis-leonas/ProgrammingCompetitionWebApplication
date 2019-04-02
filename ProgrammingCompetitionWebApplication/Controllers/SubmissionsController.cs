using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProgrammingCompetitionWebApplication.Models;

namespace ProgrammingCompetitionWebApplication.Controllers
{
    [Route("api/[controller]")]
    public class SubmissionsController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<TopSubmitter> TopSubmitters()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new TopSubmitter
            {
                Nickname = DateTime.Now.AddDays(index).ToString("d"),
                SuccessfulSubmissions = rng.Next(0, 100),
                SolvedTasks = new List<string>(){ RandomString(5), RandomString(6), RandomString(7) }
            });
        }

        [HttpGet("[action]")]
        public IEnumerable<ProgrammingTask> ProgrammingTasks()
        {
            return Enumerable.Range(1, 15).Select(index => new ProgrammingTask
            {
                Id = index,
                TaskName = RandomString(5)
            });
        }

        private string RandomString(int length)
        {
            var rng = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rng.Next(s.Length)]).ToArray());
        }
    }
}
