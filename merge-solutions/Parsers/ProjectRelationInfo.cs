using SolutionMerger.Models;

namespace SolutionMerger.Parsers
{
    public class ProjectRelationInfo
    {
        private readonly ProjectDirectory _dir;

        private readonly BaseProject _project;
        //TODO: parse nested projects
        /*
        private static readonly Regex ReNestSection = new Regex(@"(?<ProjGuid>\{\S*?\})\s=\s(?<DirGuid>\{\S*?\})", RegexOptions.Multiline | RegexOptions.Compiled);

        public static NestedProject Parse(ProjectDirectory[] projects, string slnText)
        {
            Project1.ParseConfigs(ref projects, ReConfSection.Match(slnText).Groups["Section"].Value);
            return new VsProjectConfiguration(projects);
        }*/

        public ProjectRelationInfo(BaseProject project, ProjectDirectory dir)
        {
            _project = project;
            _dir = dir;
        }

        public override string ToString()
        {
            return string.Format("\t\t{0} = {1}{2}", _project.Guid, _dir.Guid, Environment.NewLine);
        }
    }
}