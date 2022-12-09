using System.Text.RegularExpressions;

namespace MergeSolutions.Core.Parsers.GlobalSection
{
    public abstract class GlobalSectionInfoBase<TGlobalSectionInfo>
        where TGlobalSectionInfo : GlobalSectionInfoBase<TGlobalSectionInfo>, new()
    {
        private readonly Regex _reProjectConfigurationPlatformsLine =
            new Regex(@"\s*(?<Left>[^=]*)\s+?=\s+?(?<Right>[^\n\r]*)",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private readonly Regex _reProjectConfigurationPlatformsSection;
        private readonly string _sectionLocation;
        private readonly string _sectionName;

        protected GlobalSectionInfoBase(string sectionName, string sectionLocation,
            IEnumerable<KeyValuePair<string, string>>? lines = null)
        {
            _sectionName = sectionName;
            _sectionLocation = sectionLocation;
            Lines = lines?.ToList() ?? new List<KeyValuePair<string, string>>();
            _reProjectConfigurationPlatformsSection = new Regex(
                $@"GlobalSection\({_sectionName}\)\s=\s{_sectionLocation}(?<Section>[\s\S]*?)EndGlobalSection",
                RegexOptions.Multiline | RegexOptions.Compiled);
        }

        public List<KeyValuePair<string, string>> Lines { get; }

        public static TGlobalSectionInfo Parse(string slnText)
        {
            return new TGlobalSectionInfo().InternalParse(slnText);
        }

        public override string ToString()
        {
            var lines = string.Concat(Lines.Select(p => $"\t\t{p.Key} = {p.Value}{Environment.NewLine}"));
            return $"\tGlobalSection({_sectionName}) = {_sectionLocation}{Environment.NewLine}{lines}\tEndGlobalSection";
        }

        internal virtual TGlobalSectionInfo InternalParse(string slnText)
        {
            var globalSectionInfo = new TGlobalSectionInfo();
            var matchCollection1 = _reProjectConfigurationPlatformsSection.Matches(slnText);
            if (matchCollection1.Count == 1)
            {
                var section = matchCollection1[0].Groups["Section"];
                var matchCollection2 = _reProjectConfigurationPlatformsLine.Matches(section.Value);
                foreach (Match match in matchCollection2)
                {
                    var left = match.Groups["Left"].Value;
                    var right = match.Groups["Right"].Value;
                    globalSectionInfo.Lines.Add(new KeyValuePair<string, string>(left, right));
                }
            }

            return globalSectionInfo;
        }
    }
}