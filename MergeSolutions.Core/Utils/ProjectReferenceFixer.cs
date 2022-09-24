using System.Text;
using MergeSolutions.Core.Models;
using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Utils
{
    public static class ProjectReferenceFixer
    {
        public static void FixAllSolutions(SolutionInfo[] allSolutions, out string? errors)
        {
            var dupes = SolutionDiagnostics.GetProjectGuidDuplicates(allSolutions);
            if (dupes.Length == 0)
            {
                errors = null;
                return;
            }

            errors = SolutionDiagnostics.DiagnoseDupeGuidsInTheSameSolution(dupes);
            if (!string.IsNullOrEmpty(errors))
            {
                return;
            }

            var dupedProjects =
                dupes.SelectMany(g => g.Skip(1))
                    .ToArray(); //it's sufficient to rename all but one dupe - it'll be left with uniquer GUID

            //Find all solutions with dupes
            var solutions = allSolutions
                .Where(s => s.Projects.Any(p => dupedProjects.Contains(p, BaseProject.ProjectGuidLocationComparer)))
                .Where(s => s.Text != null)
                .ToDictionary(s => s, s => s.Text!);

            //Find all projects of these solutions - check for read/write access??
            var projects = solutions.Keys.SelectMany(s => s.Projects).OfType<Project>()
                .Distinct<Project>(BaseProject.ProjectGuidLocationComparer).Where(pp => File.Exists(pp.AbsolutePath))
                .ToDictionary(pp => pp, pp => File.ReadAllText(pp.AbsolutePath));

            //checks for Read/Write access :) - overwrites potentially modified files without changes
            errors = OverwriteFiles(solutions, projects);

            if (!string.IsNullOrEmpty(errors))
            {
                return;
            }

            foreach (var dupedProject in dupedProjects)
            {
                var dupe = dupedProject;
                var brokenGuid = dupe.Guid.Substring(1, dupe.Guid.Length - 2);
                var newGuid = Guid.NewGuid().ToString().ToUpper();

                var affectedSolutions = solutions.Keys
                    .Where(s => s.Projects.Contains(dupe, BaseProject.ProjectGuidLocationComparer))
                    .ToArray();
                foreach (var s in affectedSolutions)
                {
                    solutions[s] = solutions[s].Replace(brokenGuid, newGuid);
                }

                if (dupe is ProjectDirectory) // directories are mentioned only in solution files
                {
                    continue;
                }

                var affectedProjects = projects.Keys.Where(p => affectedSolutions.Contains(p.ProjectInfo.SolutionInfo)).ToArray();
                foreach (var p in affectedProjects)
                {
                    projects[p] = projects[p].Replace(brokenGuid, newGuid);
                }
            }

            errors = OverwriteFiles(solutions, projects);
        }

        private static string OverwriteFiles(Dictionary<SolutionInfo, string> solutions,
            Dictionary<Project, string> projectsToCleanup)
        {
            var errorLog = new StringBuilder();
            foreach (var sln in solutions.Keys)
            {
                var filename = Path.Combine(sln.BaseDir, sln.Name + ".sln");
                try
                {
                    File.WriteAllText(filename, solutions[sln]);
                }
                catch
                {
                    errorLog.AppendLine("Cannot write to file: " + filename);
                }
            }

            foreach (var prj in projectsToCleanup.Keys)
            {
                var filename = prj.AbsolutePath;
                try
                {
                    File.WriteAllText(filename, projectsToCleanup[prj]);
                }
                catch
                {
                    errorLog.AppendLine("Cannot write to file: " + filename);
                }
            }

            return errorLog.ToString();
        }
    }
}