using System.Text.RegularExpressions;

namespace MergeSolutions.Core.Parsers
{
    public class SolutionConfigurationPlatformsInfo
    {
        private static readonly Regex _reSolutionConfigurationPlatformsSection =
            new Regex(@"GlobalSection\(SolutionConfigurationPlatforms\)\s=\spreSolution(?<Section>[\s\S]*?)EndGlobalSection",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private static readonly Regex _reSolutionConfigurationPlatformsLine =
            new Regex(@"\s*(?<Left>[^=]*)\s+?=\s+?(?<Right>[^\n\r]*)",
                RegexOptions.Multiline | RegexOptions.Compiled);

        public SolutionConfigurationPlatformsInfo(IEnumerable<KeyValuePair<string, string>>? lines = null)
        {
            Lines = lines?.ToList() ?? new List<KeyValuePair<string, string>>();
        }

        public List<KeyValuePair<string, string>> Lines { get; }

        public static SolutionConfigurationPlatformsInfo Parse(string slnText)
        {
            var solutionConfigurationPlatformsInfo = new SolutionConfigurationPlatformsInfo();
            var matchCollection1 = _reSolutionConfigurationPlatformsSection.Matches(slnText);
            if (matchCollection1.Count == 1)
            {
                var section = matchCollection1[0].Groups["Section"];
                var matchCollection2 = _reSolutionConfigurationPlatformsLine.Matches(section.Value);
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
            return $"\tGlobalSection(SolutionConfigurationPlatforms) = preSolution{Environment.NewLine}{lines}\tEndGlobalSection";
        }
    }
}