using MergeSolutions.Core.Parsers;
using MergeSolutions.Core.Parsers.GlobalSection;

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
            var groupedSolutions = projects.GroupBy(p => p.ProjectInfo.SolutionInfo).Where(g => g.Key != null);
            foreach (var group in groupedSolutions)
            {
                var root = new ProjectDirectory(group.Key?.Name ?? "", group.Key?.ExtensibilityGlobalsSection.SolutionGuid);
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