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

        public string? SolutionGuid
        {
            get => Lines.FirstOrDefault(l => l.Key == nameof(SolutionGuid)).Value;
            set
            {
                Lines.Remove(Lines.FirstOrDefault(l => l.Key == nameof(SolutionGuid), new KeyValuePair<string, string>()));
                if (value != null)
                {
                    Lines.Add(new KeyValuePair<string, string>(nameof(SolutionGuid), value));
                }
            }
        }
    }
}