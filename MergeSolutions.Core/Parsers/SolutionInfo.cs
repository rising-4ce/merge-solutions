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
        }

        public string BaseDir { get; }

        public string Name { get; }

        public NestedProjectsInfo NestedSection { get; private set; }

        public List<BaseProject> Projects { get; private set; } = new();

        public SolutionPropertiesInfo PropsSection { get; }

        public string? Text { get; private init; }

        public static SolutionInfo MergeSolutions(string newName, string baseDir, out string warnings,
            params SolutionInfo[] solutions)
        {
            var allProjects = solutions.SelectMany(s => s.Projects).Distinct(BaseProject.ProjectGuidLocationComparer).ToList();

            warnings = SolutionDiagnostics.DiagnoseDupeGuids(solutions);

            var mergedSln = new SolutionInfo(newName, baseDir, solutions[0].PropsSection)
            {
                Projects = allProjects
            };

            mergedSln.CreateNestedDirs()
                .Projects.ForEach(pr => pr.ProjectInfo.SolutionInfo = mergedSln);

            return mergedSln;
        }

        public static SolutionInfo Parse(string slnPath)
        {
            var slnText = File.ReadAllText(slnPath);
            var path = Path.GetFullPath(slnPath);
            var slnBaseDir = Path.GetDirectoryName(path);
            var props = SolutionPropertiesInfo.Parse(slnText);

            var sln = new SolutionInfo(Path.GetFileNameWithoutExtension(path), slnBaseDir!, props)
            {
                Text = slnText
            };

            sln.Projects = ProjectInfo.Parse(sln);

            sln.NestedSection = NestedProjectsInfo.Parse(sln.Projects, slnText);

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
MinimumVisualStudioVersion = 10.0.40219.1
{projectsSection}
Global
{PropsSection}
{NestedSection}
EndGlobal
";
        }

        private SolutionInfo CreateNestedDirs()
        {
            Project.GenerateProjectDirs(NestedSection, Projects);
            return this;
        }
    }
}