using System.Text.RegularExpressions;

namespace SolutionMerger.Parsers
{
    public class SolutionPropertiesInfo
    {
        private static readonly Regex RePlatforms =
            new Regex(@"GlobalSection\(SolutionProperties\)\s=\spreSolution(?<Section>[\s\S]*?)EndGlobalSection",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private readonly string all;

        private SolutionPropertiesInfo(string all)
        {
            this.all = all;
        }

        public static SolutionPropertiesInfo Parse(string slnText)
        {
            return new SolutionPropertiesInfo(RePlatforms.Match(slnText).Value);
        }

        public override string ToString()
        {
            return "\t" + all;
        }
    }
}