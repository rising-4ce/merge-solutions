using System.Text.RegularExpressions;
using MergeSolutions.Core.Models;
using MergeSolutions.Core.Utils;

namespace MergeSolutions.Core.Parsers
{
    public class ProjectInfo
    {
        private static readonly Regex _reSolutionItemPath =
            new(@"ProjectSection\(SolutionItems\)\s=\spreProject(\s*(?<" + PathResolver.LocationGroupName + @">.*?)\s=\s(?<" +
                PathResolver.LocationGroupName + @">.*?))*\s*EndProjectSection", RegexOptions.Multiline | RegexOptions.Compiled);

        private static readonly Regex _reWebsitePath =
            new(@"AspNetCompiler\.PhysicalPath\s=\s""(?<Path>.*?)""|SlnRelativePath\s=\s""(?<Path>.*?)""",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private static readonly Regex _reProjects =
            new(
                @"Project\(\""(?<Package>\{.*?\})\"".*?\""(?<Name>.*?)\"".*?\""(?<Project1>.*?)\"".*?\""(?<Guid>.*?)\""(?<All>[\s\S]*?)EndProject\s",
                RegexOptions.Multiline | RegexOptions.Compiled);

        private readonly string _all;

        private readonly PathResolver _pathResolver;

        public ProjectInfo(SolutionInfo? solutionInfo, string package, string all)
        {
            SolutionInfo = solutionInfo;
            Package = package;
            _all = all;

            _pathResolver = new PathResolver(BaseDir);
        }

        public string Package { get; }

        public BaseProject? Project { get; set; }

        public SolutionInfo? SolutionInfo { get; set; }

        private string BaseDir => SolutionInfo == null
            ? ""
            : SolutionInfo.BaseDir;

        public static List<BaseProject> Parse(SolutionInfo? solutionInfo)
        {
            if (solutionInfo?.Text == null)
            {
                return new List<BaseProject>();
            }

            return _reProjects.Matches(solutionInfo.Text)
                .Select(m => BaseProject.Create(m.Groups["Guid"].Value, m.Groups["Name"].Value, m.Groups["Project1"].Value,
                    new ProjectInfo(solutionInfo, m.Groups["Package"].Value, m.Groups["All"].Value)))
                .ToList();
        }

        public override string ToString()
        {
            if (Project == null)
            {
                return "<null>";
            }

            var name = Project.Name;
            var guid = Project.Guid;
            var location = Project is ProjectDirectory
                ? Project.Location
                : PathHelpers.ResolveRelativePath(BaseDir, Project.Location);

            var body = _all;
            body = _pathResolver.Relocate(body, BaseDir, _reSolutionItemPath);
            body = _pathResolver.Relocate(body, BaseDir, _reWebsitePath);

            return string.Format(@"{5}Project(""{0}"") = ""{1}"", ""{2}"", ""{3}""{4}EndProject", Package, name, location, guid,
                body, Environment.NewLine);
        }
    }
}