using MergeSolutions.Core.Models;
using MergeSolutions.Core.Utils;

namespace MergeSolutions.Core.Parsers
{
    public class SolutionInfo
    {
        private SolutionInfo(string name, string baseDir, SolutionPropertiesInfo propsSection)
        {
            Name = name;
            BaseDir = Path.GetFullPath(baseDir);
            PropsSection = propsSection;
            NestedSection = new NestedProjectsInfo();
            SolutionPlatformsSection = new SolutionConfigurationPlatformsInfo();
            ProjectPlatformsSection = new ProjectConfigurationPlatformsInfo();
        }

        public string BaseDir { get; }

        public string Name { get; }

        public NestedProjectsInfo NestedSection { get; private set; }

        public ProjectConfigurationPlatformsInfo ProjectPlatformsSection { get; private set; }

        public List<BaseProject> Projects { get; private set; } = new();

        public SolutionPropertiesInfo PropsSection { get; }

        public string RelativePath { get; set; }

        public SolutionConfigurationPlatformsInfo SolutionPlatformsSection { get; private set; }

        public string? Text { get; private init; }

        public static SolutionInfo MergeSolutions(string newName, string baseDir, out string warnings,
            Func<BaseProject, bool>? projectFilter, params SolutionInfo[] solutions)
        {
            if (solutions.Length == 0)
            {
                throw new Exception("No solutions");
            }

            var allProjects = solutions
                .SelectMany(s => s.Projects)
                .Where(p => !p.IsEmpty)
                .Where(projectFilter ?? (_ => true))
                .Distinct(BaseProject.ProjectGuidLocationComparer)
                .ToList();

            if (allProjects.Count == 0)
            {
                throw new Exception("No projects");
            }

            warnings = SolutionDiagnostics.DiagnoseDupeGuids(solutions);

            FixSolutionItems(allProjects);

            var mergedSln = new SolutionInfo(newName, baseDir, solutions[0].PropsSection)
            {
                Projects = allProjects
            };

            var lines = allProjects.Where(p => p.ProjectInfo.SolutionInfo != null)
                .SelectMany(p => p.ProjectInfo.SolutionInfo!.SolutionPlatformsSection.Lines).Distinct().ToArray();
            mergedSln.SolutionPlatformsSection = new SolutionConfigurationPlatformsInfo(lines);

            lines = allProjects.Where(p => p.ProjectInfo.SolutionInfo != null)
                .SelectMany(p => p.ProjectInfo.SolutionInfo!.ProjectPlatformsSection.Lines).Distinct().ToArray();
            mergedSln.ProjectPlatformsSection = new ProjectConfigurationPlatformsInfo(lines);

            mergedSln.CreateNestedDirs()
                .Projects.ForEach(pr => pr.ProjectInfo.SolutionInfo = mergedSln);

            return mergedSln;
        }

        public static SolutionInfo Parse(string slnPath, string? rootDir = null, string? overrideName = null)
        {
            rootDir ??= Environment.CurrentDirectory;
            if (!Path.IsPathFullyQualified(slnPath))
            {
                slnPath = Path.Combine(rootDir, slnPath);
            }

            var slnText = File.ReadAllText(slnPath);
            var path = Path.GetFullPath(slnPath);
            var slnBaseDir = Path.GetDirectoryName(path);
            var props = SolutionPropertiesInfo.Parse(slnText);

            var sln = new SolutionInfo(overrideName ?? Path.GetFileNameWithoutExtension(path), slnBaseDir!, props)
            {
                Text = slnText
            };

            sln.Projects = ProjectInfo.Parse(sln);
            sln.NestedSection = NestedProjectsInfo.Parse(sln.Projects, slnText);
            sln.SolutionPlatformsSection = SolutionConfigurationPlatformsInfo.Parse(slnText);
            sln.ProjectPlatformsSection = ProjectConfigurationPlatformsInfo.Parse(slnText);
            sln.RelativePath = Path.GetRelativePath(rootDir, slnPath);

            return sln;
        }

        public void Save()
        {
            var solutionContent = ToString();
            File.WriteAllText(Path.Combine(BaseDir, Name + ".sln"), solutionContent);
        }

        public override string ToString()
        {
            var projectsSection = string.Concat(Projects.Select(p => p.ProjectInfo));
            return $@"Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.3.32901.215
MinimumVisualStudioVersion = 10.0.40219.1{projectsSection}
Global
{SolutionPlatformsSection}
{ProjectPlatformsSection}
{PropsSection}
{NestedSection}
EndGlobal
";
        }

        private static void FixSolutionItems(List<BaseProject> allProjects)
        {
            foreach (var projectDirectory in allProjects.OfType<ProjectDirectory>())
            {
                if (string.Equals(projectDirectory.Name, "Solution Items", StringComparison.OrdinalIgnoreCase))
                {
                    projectDirectory.OverridingName = "Inner Solution Items";
                }
            }
        }

        private SolutionInfo CreateNestedDirs()
        {
            Project.GenerateProjectDirs(NestedSection, Projects);
            return this;
        }
    }
}