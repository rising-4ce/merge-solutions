using System.Text;
using MergeSolutions.Core.Models;
using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Utils
{
    public static class SolutionDiagnostics
    {
        public static string DiagnoseDupeGuids(IEnumerable<SolutionInfo> solutions, Func<BaseProject, bool>? projectFilter = null)
        {
            var weirdProjects = GetProjectGuidDuplicates(solutions, projectFilter);
            if (weirdProjects.Length == 0)
            {
                return "";
            }

            var report = ReportWeirdProjects(new StringBuilder("Following projects have duplicate GUIDs:"), weirdProjects,
                p => p.SolutionName ?? "<no solution name>");
            report.AppendLine();
            report.AppendLine("Projects above have duplicate GUIDs");
            report.AppendLine();

            return report.ToString();
        }

        public static string DiagnoseDupeGuidsInTheSameSolution(IEnumerable<IGrouping<string, BaseProject>> weirdProjects)
        {
            var hopelessProjects = weirdProjects.SelectMany(g => g)
                .GroupBy(p => Path.Combine(p.SolutionDir ?? "", p.SolutionName + ".sln"))
                .Where(g => g.GroupBy(p => p.Guid).Count(gg => gg.Count() > 1) > 1)
                .ToArray();

            if (hopelessProjects.Length == 0)
            {
                return "";
            }

            var report = ReportWeirdProjects(
                new StringBuilder("Following projects with identical GUIDs are located in the same solution:"), hopelessProjects,
                p => p.Guid);
            report.AppendLine("Its a pity, but in this case there is no way to help you");
            report.AppendLine("Good luck figuring it out...");
            return report.ToString();
        }

        public static IGrouping<string, BaseProject>[] GetProjectGuidDuplicates(IEnumerable<SolutionInfo> solutions,
            Func<BaseProject, bool>? projectFilter = null)
        {
            return solutions.SelectMany(s => s.Projects)
                .Where(projectFilter ?? (_ => true))
                .Distinct(BaseProject.ProjectGuidLocationComparer)
                .GroupBy(p => p.Guid)
                .Where(gr => gr.Count() > 1)
                .ToArray();
        }

        private static StringBuilder ReportWeirdProjects<T>(StringBuilder sb,
            IEnumerable<IGrouping<T, BaseProject>> weirdProjects, Func<BaseProject, string> itemDescriptor)
        {
            sb.AppendLine();
            foreach (var weirdProjectGroup in weirdProjects)
            {
                sb.AppendLine();
                sb.AppendFormat("------------- {0} -------------", weirdProjectGroup.Key);
                sb.AppendLine();
                foreach (var weirdProject in weirdProjectGroup)
                {
                    sb.AppendFormat("|{0, 25} | {1}", itemDescriptor(weirdProject), weirdProject.Location);
                    sb.AppendLine();
                }
            }

            sb.AppendLine("---------------");
            return sb;
        }
    }
}