using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Models
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
            //Func<BaseProject, string?> getActualSolutionName = p =>
            //    p is ProjectDirectory ||
            //    p.Location.IsWebSiteUrl() ||
            //    (p.SolutionDir != null &&
            //     p.Location.StartsWith(p
            //         .SolutionDir)) //Means it is a project that is located inside solution base folder or a project directory or its a website
            //        ? p.SolutionName
            //        : PathHelpers.GetDirName(Path.GetDirectoryName(Path.GetDirectoryName(p.Location)) ?? "");

            Func<BaseProject, string?> getActualSolutionName = p => p.SolutionName;
            var groupedSolutions = projects.GroupBy(getActualSolutionName).Where(g => g.Key != null);
            foreach (var group in groupedSolutions)
            {
                var root = new ProjectDirectory(group.Key ?? "");
                projects.Add(root);

                root.NestedProjectsInfo = nestedSection;
                nestedSection.Dirs.Add(root);
                root.NestedProjects.AddRange(group.Select(pr =>
                {
                    var nestedSectionDirs = pr.ProjectInfo.SolutionInfo?.NestedSection.Dirs;
                    var parentGuid = nestedSectionDirs?.FirstOrDefault(d => d.NestedProjects.Any(p => p.Project.Guid == pr.Guid))
                        ?.Guid;
                    var subDir = group.FirstOrDefault(g => g.Guid == parentGuid) as ProjectDirectory;
                    return new ProjectRelationInfo(pr, subDir ?? root);
                }));
            }
        }
    }
}