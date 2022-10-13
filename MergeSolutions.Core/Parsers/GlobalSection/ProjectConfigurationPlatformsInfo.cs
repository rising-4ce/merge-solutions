namespace MergeSolutions.Core.Parsers.GlobalSection
{
    public class ProjectConfigurationPlatformsInfo : GlobalSectionInfoBase<ProjectConfigurationPlatformsInfo>
    {
        public ProjectConfigurationPlatformsInfo(IEnumerable<KeyValuePair<string, string>>? lines)
            : base("ProjectConfigurationPlatforms", "postSolution", lines)
        {
        }

        public ProjectConfigurationPlatformsInfo() : this(null)
        {
        }
    }
}