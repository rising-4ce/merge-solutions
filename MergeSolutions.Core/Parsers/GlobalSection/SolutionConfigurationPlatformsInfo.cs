namespace MergeSolutions.Core.Parsers.GlobalSection
{
    public class SolutionConfigurationPlatformsInfo : GlobalSectionInfoBase<SolutionConfigurationPlatformsInfo>
    {
        public SolutionConfigurationPlatformsInfo(IEnumerable<KeyValuePair<string, string>>? lines)
            : base("SolutionConfigurationPlatforms", "preSolution", lines)
        {
        }

        public SolutionConfigurationPlatformsInfo() : this(null)
        {
        }
    }
}