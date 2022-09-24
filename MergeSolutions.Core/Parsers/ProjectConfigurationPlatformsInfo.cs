using System.Text.RegularExpressions;

namespace MergeSolutions.Core.Parsers
{
    public class ProjectConfigurationPlatformsInfo
    {
        private static readonly Regex _reProjectConfigurationPlatformsSection =
            new Regex(@"GlobalSection\(ProjectConfigurationPlatforms\)\s=\spostSolution(?<Section>[\s\S]*?)EndGlobalSection",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private static readonly Regex _reProjectConfigurationPlatformsLine =
            new Regex(@"\s*(?<Left>[^=]*)\s+?=\s+?(?<Right>[^\n\r]*)",
                RegexOptions.Multiline | RegexOptions.Compiled);

        public ProjectConfigurationPlatformsInfo(IEnumerable<KeyValuePair<string, string>>? lines = null)
        {
            Lines = lines?.ToList() ?? new List<KeyValuePair<string, string>>();
        }

        public List<KeyValuePair<string, string>> Lines { get; }

        public static ProjectConfigurationPlatformsInfo Parse(string slnText)
        {
            var solutionConfigurationPlatformsInfo = new ProjectConfigurationPlatformsInfo();
            var matchCollection1 = _reProjectConfigurationPlatformsSection.Matches(slnText);
            if (matchCollection1.Count == 1)
            {
                var section = matchCollection1[0].Groups["Section"];
                var matchCollection2 = _reProjectConfigurationPlatformsLine.Matches(section.Value);
                foreach (Match match in matchCollection2)
                {
                    var left = match.Groups["Left"].Value;
                    var right = match.Groups["Right"].Value;
                    solutionConfigurationPlatformsInfo.Lines.Add(new KeyValuePair<string, string>(left, right));
                }
            }

            return solutionConfigurationPlatformsInfo;
        }

        public override string ToString()
        {
            var lines = string.Concat(Lines.Select(p => $"\t\t{p.Key} = {p.Value}{Environment.NewLine}"));
            return $"\tGlobalSection(ProjectConfigurationPlatforms) = postSolution{Environment.NewLine}{lines}\tEndGlobalSection";
        }
    }
}