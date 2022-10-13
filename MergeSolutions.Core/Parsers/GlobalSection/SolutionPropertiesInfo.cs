namespace MergeSolutions.Core.Parsers.GlobalSection
{
    public class SolutionPropertiesInfo : GlobalSectionInfoBase<SolutionPropertiesInfo>
    {
        public SolutionPropertiesInfo(IEnumerable<KeyValuePair<string, string>>? lines)
            : base("SolutionProperties", "preSolution", lines)
        {
        }

        public SolutionPropertiesInfo() : this(null)
        {
        }
    }
}