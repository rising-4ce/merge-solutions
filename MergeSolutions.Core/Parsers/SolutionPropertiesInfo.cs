using System.Text.RegularExpressions;

namespace MergeSolutions.Core.Parsers
{
    public class SolutionPropertiesInfo
    {
        private static readonly Regex _rePlatforms =
            new(@"GlobalSection\(SolutionProperties\)\s=\spreSolution(?<Section>[\s\S]*?)EndGlobalSection",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private readonly string _all;

        private SolutionPropertiesInfo(string all)
        {
            _all = all;
        }

        public static SolutionPropertiesInfo Parse(string slnText)
        {
            return new SolutionPropertiesInfo(_rePlatforms.Match(slnText).Value);
        }

        public override string ToString()
        {
            return "\t" + _all;
        }
    }
}