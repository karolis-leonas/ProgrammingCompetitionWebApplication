using ProgrammingCompetitionWebApplication.Enums;

namespace ProgrammingCompetitionWebApplication.Models
{
    public class FiddleModel
    {
        public ProgrammingLanguage Language { get; set; }
        public ProjectType ProjectType { get; set; }
        public Compiler Compiler { get; set; }
        public string CodeBlock { get; set; }
        public string[] ConsoleInputLines { get; set; }
        public MvcCodeBlock MvcCodeBlock { get; set; }
        public NuGetPackages[] NuGetPackages { get; set; }
    }
}
