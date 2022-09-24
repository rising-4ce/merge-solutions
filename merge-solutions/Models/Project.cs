using SolutionMerger.Parsers;
using SolutionMerger.Utils;

namespace SolutionMerger.Models
{
    public class Project : BaseProject
    {
        public Project(string guid, string name, ProjectInfo projectInfo, string absolutePath)
            : base(guid, name, projectInfo)
        {
            AbsolutePath = absolutePath;
        }

        public string AbsolutePath { get; }

        public override string Location => AbsolutePath;

        public static void GenerateProjectDirs(NestedProjectsInfo nestedSection, List<BaseProject> projects)
        {
            Func<BaseProject, string?> getActualSolutionName = p =>
                p is ProjectDirectory ||
                p.Location.IsWebSiteUrl() ||
                (p.SolutionDir != null &&
                 p.Location.StartsWith(p
                     .SolutionDir)) //Means it is a project that is located inside solution base folder or a project directory or its a website
                    ? p.SolutionName
                    : PathHelpers.GetDirName(Path.GetDirectoryName(Path.GetDirectoryName(p.Location)) ?? "");

            IEnumerable<IGrouping<string?, BaseProject>> groupedSolutions = projects.GroupBy(getActualSolutionName);
            foreach (var group in groupedSolutions)
            {
                var dir = new ProjectDirectory(group.Key ?? "");
                projects.Add(dir);

                dir.NestedProjectsInfo = nestedSection;
                nestedSection.Dirs.Add(dir);
                dir.NestedProjects.AddRange(group.Select(pr => new ProjectRelationInfo(pr, dir)));
            }
        }
    }
}