namespace ProgrammingCompetitionWebApplication.Models.Configuration
{
    public class MainConfiguration
    {
        public string DatabaseLocation { get; set; }
        public string OnlineCompilerApi { get; set; }
        public string RootPath { get; set; }
        public int ConnectionLeaseTimeout { get; set; }
    }
}
