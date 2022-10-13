using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MergeSolutions.Core.Models;
using MergeSolutions.Core.Services;

namespace MergeSolutions.Core
{
    public class MergePlan
    {
        private string? _outputSolutionPath;

        /// <summary>
        /// Excluded projects. Key is solution name, value is a project GUID
        /// </summary>
        public KeyValuePair<string, string>[] ExcludedProjects { get; set; } = Array.Empty<KeyValuePair<string, string>>();

        public string? OutputSolutionPath
        {
            get
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(_outputSolutionPath ?? PlanName);
                if (fileNameWithoutExtension == null)
                {
                    return null;
                }

                var directoryName = Path.GetDirectoryName(_outputSolutionPath ?? "");
                if (directoryName != null)
                {
                    fileNameWithoutExtension = Path.Combine(directoryName, fileNameWithoutExtension);
                }

                return $"{fileNameWithoutExtension}.sln";
            }
            set => _outputSolutionPath = value;
        }

        [JsonIgnore]
        public string? PlanName { get; set; }

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
            mergePlan.PlanName = Path.GetFileName(fileName);
            mergePlan.RootDir = pathRoot;
            mergePlan.RecalculateRootDir();
            return mergePlan;
        }

        public void AddSolution(string fileName, ISolutionService solutionService)
        {
            var rootDir = RootDir ?? Environment.CurrentDirectory;
            var relativePath = Path.GetRelativePath(rootDir, fileName);
            var solutionInfo = solutionService.ParseSolution(relativePath, rootDir);
            Solutions.RemoveAll(s => s.RelativePath == relativePath);
            Solutions.Add(new SolutionEntity
            {
                RelativePath = relativePath,
                NodeName = solutionInfo.Name,
                Collapsed = false
            });
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

        public void RecalculateRootDir()
        {
            RecalculateRootDir(RootDir);
        }

        public void RecalculateRootDir(string? rootDir)
        {
            rootDir ??= Environment.CurrentDirectory;
            Solutions = Solutions.Select(s =>
            {
                var absPath = Path.Combine(RootDir ?? Environment.CurrentDirectory, s.RelativePath!);
                s.RelativePath = Path.GetRelativePath(rootDir, absPath);
                return s;
            }).ToList();

            ExcludedProjects = ExcludedProjects.Select(p =>
            {
                var absPath = Path.Combine(RootDir ?? Environment.CurrentDirectory, p.Key);
                return new KeyValuePair<string, string>(Path.GetRelativePath(rootDir, absPath),
                    p.Value);
            }).ToArray();

            if (OutputSolutionPath != null && RootDir != null)
            {
                OutputSolutionPath = Path.GetRelativePath(rootDir,
                    Path.Combine(RootDir, OutputSolutionPath));
            }

            RootDir = rootDir;
        }

        public void SaveMergePlanFile()
        {
            if (RootDir == null || PlanName == null)
            {
                throw new InvalidOperationException("Cannot save merge plan. No root directory or plan name known.");
            }

            SaveMergePlanFile(Path.Combine(RootDir, PlanName));
        }

        public void SaveMergePlanFile(string fileName)
        {
            PlanName = Path.GetFileName(fileName);
            RecalculateRootDir(Path.GetDirectoryName(fileName));
            var content = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(fileName, content, Encoding.UTF8);
        }
    }
}