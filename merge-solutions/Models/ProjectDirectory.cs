﻿using SolutionMerger.Parsers;

namespace SolutionMerger.Models
{
    public class ProjectDirectory : BaseProject
    {
        private const string DirPackageGuid = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";

        public ProjectDirectory(string name, string? guid = null, string? packageGuid = null)
            : base(guid ?? "{" + System.Guid.NewGuid().ToString().ToUpper() + "}",
                name,
                new ProjectInfo(null, packageGuid ?? DirPackageGuid, Environment.NewLine))
        {
            NestedProjects = new List<ProjectRelationInfo>();
        }

        public override string Location => Name;

        public List<ProjectRelationInfo> NestedProjects { get; }

        public NestedProjectsInfo? NestedProjectsInfo { get; set; }
    }
}