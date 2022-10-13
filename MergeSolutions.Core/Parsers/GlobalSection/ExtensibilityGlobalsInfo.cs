namespace MergeSolutions.Core.Parsers.GlobalSection
{
    public class ExtensibilityGlobalsInfo : GlobalSectionInfoBase<ExtensibilityGlobalsInfo>
    {
        public ExtensibilityGlobalsInfo(IEnumerable<KeyValuePair<string, string>>? lines)
            : base("ExtensibilityGlobals", "postSolution", lines)
        {
        }

        public ExtensibilityGlobalsInfo() : this(null)
        {
        }

        public string? SolutionGuid =>
            Lines.FirstOrDefault(l => l.Key == "SolutionGuid", new KeyValuePair<string, string>()).Value;
    }
}