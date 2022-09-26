using MergeSolutions.Core.Parsers;
using MergeSolutions.Core.Utils;

namespace MergeSolutions.Core.Models
{
    public abstract class BaseProject
    {
        public static readonly ProjectLocationComparerImpl ProjectGuidLocationComparer = new();

        protected BaseProject(string guid, string name, ProjectInfo projectInfo)
        {
            Guid = guid;
            Name = name;
            ProjectInfo = projectInfo;
            ProjectInfo.Project = this;
        }

        public string Guid { get; }

        public abstract string Location { get; }

        public string Name { get; }

        public ProjectInfo ProjectInfo { get; }

        public string? SolutionDir => ProjectInfo.SolutionInfo?.BaseDir;

        public string? SolutionName => ProjectInfo.SolutionInfo?.Name;

        public static BaseProject Create(string guid, string name, string relativeLocation, ProjectInfo info)
        {
            var project = name != relativeLocation
                ? new Project(guid, name, info,
                    PathHelpers.ResolveAbsolutePath(
                        info.SolutionInfo?.BaseDir ??
                        throw new InvalidOperationException($"Solution info is required for {name} from {relativeLocation}"),
                        relativeLocation))
                : (BaseProject)new ProjectDirectory(name, guid, info.Package, info.All, info.SolutionInfo);

            project.ProjectInfo.Project = project;

            return project;
        }

        #region Nested Type: ProjectGuidComparerImpl

        public class ProjectGuidComparerImpl : IEqualityComparer<BaseProject>
        {
            public bool Equals(BaseProject? x, BaseProject? y)
            {
                return x?.Guid == y?.Guid;
            }

            public int GetHashCode(BaseProject x)
            {
                return x.Guid.GetHashCode();
            }
        }

        #endregion

        #region Nested Type: ProjectLocationComparerImpl

        public class ProjectLocationComparerImpl : IEqualityComparer<BaseProject>
        {
            public bool Equals(BaseProject? x, BaseProject? y)
            {
                if (x is Project ^ y is Project)
                {
                    return false;
                }

                if (x is Project)
                {
                    return x.Location == y?.Location;
                }

                return x?.Guid == y?.Guid && x?.Location == y?.Location;
            }

            public int GetHashCode(BaseProject x)
            {
                return x is Project
                    ? x.Location.GetHashCode()
                    : (x.Guid + x.Location).GetHashCode();
            }
        }

        #endregion
    }
}