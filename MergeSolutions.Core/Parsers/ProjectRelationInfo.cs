using MergeSolutions.Core.Models;

namespace MergeSolutions.Core.Parsers
{
    public class ProjectRelationInfo
    {
        public ProjectRelationInfo(BaseProject project, ProjectDirectory dir)
        {
            Project = project;
            Dir = dir;
        }

        public ProjectDirectory Dir { get; }

        public BaseProject Project { get; }

        public override string ToString()
        {
            return $"\t\t{Project.Guid} = {Dir.Guid}{Environment.NewLine}";
        }
    }
}