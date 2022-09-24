using System.Text.RegularExpressions;
using MergeSolutions.Core.Models;

namespace MergeSolutions.Core.Parsers
{
    public class NestedProjectsInfo
    {
        private static readonly Regex _reNestSection =
            new Regex(@"GlobalSection\(NestedProjects\)\s=\spreSolution(?<Section>[\s\S]*?)EndGlobalSection",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private static readonly Regex _reNestLine =
            new Regex(@"(?<Guid1>\{[\dABCDEFabcdef-]*\}).*=.*(?<Guid2>\{[\dABCDEFabcdef-]*\})",
                RegexOptions.Multiline | RegexOptions.Compiled);

        public NestedProjectsInfo(List<ProjectDirectory>? dirs = null)
        {
            Dirs = dirs ?? new List<ProjectDirectory>();
        }

        public List<ProjectDirectory> Dirs { get; }

        public static NestedProjectsInfo Parse(ICollection<BaseProject> projects, string slnText)
        {
            var nestedProjectsInfo = new NestedProjectsInfo();
            var matchCollection1 = _reNestSection.Matches(slnText);
            if (matchCollection1.Count == 1)
            {
                var section = matchCollection1[0].Groups["Section"];
                var matchCollection2 = _reNestLine.Matches(section.Value);
                foreach (Match match in matchCollection2)
                {
                    var guid1 = match.Groups["Guid1"].Value;
                    var guid2 = match.Groups["Guid2"].Value;
                    var dir = nestedProjectsInfo.Dirs.FirstOrDefault(d => d.Guid == guid2);
                    if (dir == null)
                    {
                        dir = projects.FirstOrDefault(p => p.Guid == guid2) as ProjectDirectory;
                        if (dir != null)
                        {
                            nestedProjectsInfo.Dirs.Add(dir);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    dir.NestedProjects.Add(new ProjectRelationInfo(projects.Single(p => p.Guid == guid1), dir));
                }
            }

            return nestedProjectsInfo;
        }

        public override string ToString()
        {
            return string.Format("\tGlobalSection(NestedProjects) = preSolution{1}{0}\tEndGlobalSection",
                string.Concat(Dirs.SelectMany(p => p.NestedProjects)), Environment.NewLine);
        }
    }
}