using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Models
{
    public class ProjectDirectory : BaseProject
    {
        private const string DirPackageGuid = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";

        public ProjectDirectory(string name, string? guid = null, string? packageGuid = null, string? all = null,
            SolutionInfo? solutionInfo = null)
            : base(guid ?? "{" + System.Guid.NewGuid().ToString().ToUpper() + "}",
                name,
                new ProjectInfo(solutionInfo, packageGuid ?? DirPackageGuid, all ?? Environment.NewLine))
        {
            NestedProjects = new List<ProjectRelationInfo>();
        }

        public override bool IsEmpty => NestedProjects.Count == 0 && ProjectInfo.All == Environment.NewLine;

        public override string Location => Name;

        public override string Name => OverridingName ?? base.Name;

        public List<ProjectRelationInfo> NestedProjects { get; }

        public NestedProjectsInfo? NestedProjectsInfo { get; set; }

        public string? OverridingName { get; set; }
    }
}