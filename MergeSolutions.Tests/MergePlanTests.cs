using FluentAssertions;
using MergeSolutions.Core;
using MergeSolutions.Core.Models;
using Xunit;

namespace MergeSolutions.Tests
{
    public class MergePlanTests
    {
        [Fact]
        public void RecalculateRootDir()
        {
            var mergePlan = new MergePlan
            {
                OutputSolutionPath = @"B\c.sln",
                RootDir = @"C:\A",
                Solutions = new List<SolutionEntity>
                {
                    new()
                    {
                        RelativePath = @"B\1.sln",
                        NodeName = "1"
                    },
                    new()
                    {
                        RelativePath = @"B\C\2.sln",
                        NodeName = "2"
                    },
                    new()
                    {
                        RelativePath = @"..\X\3.sln",
                        NodeName = "3"
                    }
                },
                ExcludedProjects = new KeyValuePair<string, string>[]
                {
                    new(@"B\1.sln", "{A0BD3D6D-66E5-4037-915F-90077ACDDBCB}"),
                    new(@"B\C\2.sln", "{6558A6AC-9260-4E49-9105-29031B22CE5A}"),
                    new(@"..\X\3.sln", "{D3768DB0-DCDB-432C-A3C6-8D13634EE9E5}"),
                }
            };

            mergePlan.RecalculateRootDir(@"C:\A\B");
            mergePlan.RootDir.Should().Be(@"C:\A\B");
            mergePlan.OutputSolutionPath.Should().Be(@"c.sln");
            mergePlan.Solutions[0].RelativePath.Should().Be(@"1.sln");
            mergePlan.Solutions[1].RelativePath.Should().Be(@"C\2.sln");
            mergePlan.Solutions[2].RelativePath.Should().Be(@"..\..\X\3.sln");
            mergePlan.ExcludedProjects[0].Key.Should().Be(@"1.sln");
            mergePlan.ExcludedProjects[1].Key.Should().Be(@"C\2.sln");
            mergePlan.ExcludedProjects[2].Key.Should().Be(@"..\..\X\3.sln");

            mergePlan.RecalculateRootDir(@"D:\A\B");
            mergePlan.RootDir.Should().Be(@"D:\A\B");
            mergePlan.OutputSolutionPath.Should().Be(@"C:\A\B\c.sln");
            mergePlan.Solutions[0].RelativePath.Should().Be(@"C:\A\B\1.sln");
            mergePlan.Solutions[1].RelativePath.Should().Be(@"C:\A\B\C\2.sln");
            mergePlan.Solutions[2].RelativePath.Should().Be(@"C:\X\3.sln");
            mergePlan.ExcludedProjects[0].Key.Should().Be(@"C:\A\B\1.sln");
            mergePlan.ExcludedProjects[1].Key.Should().Be(@"C:\A\B\C\2.sln");
            mergePlan.ExcludedProjects[2].Key.Should().Be(@"C:\X\3.sln");

            mergePlan.RecalculateRootDir(@"C:\A\B");
            mergePlan.RootDir.Should().Be(@"C:\A\B");
            mergePlan.OutputSolutionPath.Should().Be(@"c.sln");
            mergePlan.Solutions[0].RelativePath.Should().Be(@"1.sln");
            mergePlan.Solutions[1].RelativePath.Should().Be(@"C\2.sln");
            mergePlan.Solutions[2].RelativePath.Should().Be(@"..\..\X\3.sln");
            mergePlan.ExcludedProjects[0].Key.Should().Be(@"1.sln");
            mergePlan.ExcludedProjects[1].Key.Should().Be(@"C\2.sln");
            mergePlan.ExcludedProjects[2].Key.Should().Be(@"..\..\X\3.sln");
        }

        [Fact]
        public void Version()
        {
            new MergePlan().Version.Should().Be("1.0");
        }
    }
}