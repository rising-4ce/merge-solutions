using System.Text.RegularExpressions;

namespace MergeSolutions.Core.Parsers.GlobalSection
{
    public class ProjectConfigurationPlatformsInfo : GlobalSectionInfoBase<ProjectConfigurationPlatformsInfo>
    {
        private static readonly Regex _reKey =
            new Regex(@"\{(.*?)\}\.(.*?)\|(.*?)\.(.*)",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private static readonly Regex _reValue =
            new Regex(@"(.*?)\|(.*)",
                RegexOptions.Multiline | RegexOptions.Compiled);

        public ProjectConfigurationPlatformsInfo(IEnumerable<KeyValuePair<string, string>>? lines)
            : base("ProjectConfigurationPlatforms", "postSolution", lines)
        {
        }

        public ProjectConfigurationPlatformsInfo() : this(null)
        {
        }

        public PlatformInfoLine[] PlatformInfoLines => Lines.Select(l => new PlatformInfoLine(l)).ToArray();

        public void AddPlatformInfoLine(PlatformInfoLine platformInfoLine)
        {
            Lines.Add(platformInfoLine.ToLine());
        }

        public bool RemovePlatformInfoLine(PlatformInfoLine platformInfoLine)
        {
            return Lines.Remove(platformInfoLine.ToLine());
        }

        #region Nested Type: PlatformInfoLine

        public class PlatformInfoLine
        {
            public PlatformInfoLine(KeyValuePair<string, string> line)
            {
                var keyMatch = _reKey.Match(line.Key);
                if (!keyMatch.Success)
                {
                    throw new InvalidOperationException($"Invalid platform info line key {line.Key}");
                }

                var valueMatch = _reValue.Match(line.Value);
                if (!valueMatch.Success)
                {
                    throw new InvalidOperationException($"Invalid platform info line value {line.Value}");
                }

                ProjectGuid = keyMatch.Groups[1].Value;
                SolutionConfiguration = keyMatch.Groups[2].Value;
                SolutionPlatform = keyMatch.Groups[3].Value;
                Parameter = keyMatch.Groups[4].Value;
                ProjectConfiguration = valueMatch.Groups[1].Value;
                ProjectPlatform = valueMatch.Groups[2].Value;
            }

            public string? Parameter { get; set; }
            public string? ProjectConfiguration { get; set; }

            public string? ProjectGuid { get; set; }
            public string? ProjectPlatform { get; set; }
            public string? SolutionConfiguration { get; set; }
            public string? SolutionPlatform { get; set; }

            public KeyValuePair<string, string> ToLine()
            {
                return new KeyValuePair<string, string>(
                    $"{{{ProjectGuid}}}.{SolutionConfiguration}|{SolutionPlatform}.{Parameter}",
                    $"{ProjectConfiguration}|{ProjectPlatform}");
            }
        }

        #endregion
    }
}