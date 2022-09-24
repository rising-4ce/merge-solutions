using System.Collections.Generic;
using SolutionMerger.Parsers;
using SolutionMerger.Utils;

namespace SolutionMerger.Models
{
    public abstract class BaseProject
    {
        public static readonly ProjectLocationComparerImpl ProjectGuidLocationComparer = new ProjectLocationComparerImpl();
        public string Guid { get; protected set; }

        public abstract string Location { get; }
        public string Name { get; protected set; }
        public ProjectInfo ProjectInfo { get; protected set; }

        public string SolutionDir => ProjectInfo.SolutionInfo.BaseDir;
        public string SolutionName => ProjectInfo.SolutionInfo.Name;

        public static BaseProject Create(string guid, string name, string relativeLocation, ProjectInfo info)
        {
            var absolutePath = PathHelpers.ResolveAbsolutePath(info.SolutionInfo.BaseDir, relativeLocation);

            var pr = name != relativeLocation
                ? new Project
                {
                    Guid = guid,
                    Name = name,
                    AbsolutePath = absolutePath
                }
                : (BaseProject)new ProjectDirectory(name, guid, info.Package);
            pr.ProjectInfo = info;
            pr.ProjectInfo.Project = pr;

            return pr;
        }

        #region Nested Type: ProjectGuidComparerImpl

        public class ProjectGuidComparerImpl : IEqualityComparer<BaseProject>
        {
            public bool Equals(BaseProject x, BaseProject y)
            {
                return x.Guid == y.Guid;
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
            public bool Equals(BaseProject x, BaseProject y)
            {
                if (x is Project ^ y is Project)
                {
                    return false;
                }

                if (x is Project)
                {
                    return x.Location == y.Location;
                }

                return x.Guid == y.Guid && x.Location == y.Location;
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