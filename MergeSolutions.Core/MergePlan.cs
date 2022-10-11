using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MergeSolutions.Core.Models;

namespace MergeSolutions.Core
{
    public class MergePlan
    {
        public MergePlan()
        {
            PlanName = "Untitled";
        }

        /// <summary>
        /// Excluded projects. Key is solution name, value is a project GUID
        /// </summary>
        public KeyValuePair<string, string>[] ExcludedProjects { get; set; } = Array.Empty<KeyValuePair<string, string>>();

        public string OutputSolutionPath { get; set; } = "merged.sln";

        public string PlanName { get; set; }

        [JsonIgnore]
        public string? RootDir { get; set; }

        /// <summary>
        /// Excluded projects. Key is solution path, value is a solution data
        /// </summary>
        public List<SolutionEntity> Solutions { get; set; } = new();

        public string? Version { get; set; } = "1.0";

        public static MergePlan LoadMergePlanFile(string fileName, IMigrator? migrator)
        {
            var content = File.ReadAllText(fileName);
            var pathRoot = Path.GetDirectoryName(fileName);
            if (migrator != null)
            {
                content = migrator.Migrate(content, pathRoot ?? Environment.CurrentDirectory);
            }

            var mergePlan = (content == null
                ? null
                : JsonSerializer.Deserialize<MergePlan>(content)) ?? new MergePlan();
            mergePlan.PlanName = Path.GetFileNameWithoutExtension(fileName);
            mergePlan.RootDir = pathRoot;
            mergePlan.RecalculateRootDir(mergePlan.RootDir);
            return mergePlan;
        }

        public bool IsExcluded(BaseProject baseProject)
        {
            if (baseProject is not Project project)
            {
                return false;
            }

            return ExcludedProjects.Any(e =>
                e.Key == project.ProjectInfo.SolutionInfo?.RelativePath && e.Value == baseProject.Guid);
        }

        public void RecalculateRootDir(string? rootDir)
        {
            Solutions = Solutions.Select(s =>
            {
                var absPath = Path.Combine(RootDir ?? Environment.CurrentDirectory, s.RelativePath!);
                s.RelativePath = Path.GetRelativePath(rootDir ?? Environment.CurrentDirectory, absPath);
                return s;
            }).ToList();

            ExcludedProjects = ExcludedProjects.Select(p =>
            {
                var absPath = Path.Combine(RootDir ?? Environment.CurrentDirectory, p.Key);
                return new KeyValuePair<string, string>(Path.GetRelativePath(rootDir ?? Environment.CurrentDirectory, absPath),
                    p.Value);
            }).ToArray();

            OutputSolutionPath = Path.GetRelativePath(rootDir ?? Environment.CurrentDirectory,
                Path.Combine(RootDir ?? Environment.CurrentDirectory, OutputSolutionPath));

            RootDir = rootDir;
        }

        public void SaveMergePlanFile(string fileName)
        {
            PlanName = Path.GetFileNameWithoutExtension(fileName);
            RecalculateRootDir(Path.GetDirectoryName(fileName));
            var content = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(fileName, content, Encoding.UTF8);
        }
    }
}