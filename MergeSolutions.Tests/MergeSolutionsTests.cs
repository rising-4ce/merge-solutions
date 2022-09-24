using FluentAssertions;
using MergeSolutions.Core.Parsers;
using Xunit;

namespace MergeSolutions.Tests
{
    public class MergeSolutionsTests
    {
        [Fact]
        public void MergeOfSingleA()
        {
            var outDir = Path.Combine(Path.GetTempPath(), nameof(MergeSolutionsTests));
            RecursiveDelete(new DirectoryInfo(outDir));
            Directory.CreateDirectory(outDir);
            var outSolutionName = "out.sln";

            var solutionPaths = new[] {"TestData/SolutionA/SolutionA.sln",};

            var outputSolutionPath = Path.Combine(outDir, outSolutionName);

            var mergedSolution = SolutionInfo.MergeSolutions(Path.GetFileNameWithoutExtension(outputSolutionPath),
                Path.GetDirectoryName(outputSolutionPath) ?? "",
                out _,
                null,
                solutionPaths.Select(SolutionInfo.Parse).ToArray());
            mergedSolution.Save();

            var solutionInfo = SolutionInfo.Parse(outputSolutionPath);

            solutionInfo.Projects.Should().HaveCount(5);
            solutionInfo.NestedSection.Dirs.Should().HaveCount(2);
            solutionInfo.NestedSection.Dirs.Should()
                .Contain(p => p.Guid == "{8FE39D73-9DBF-47A0-94E3-24F96625B4EA}");
            var inFolderDir = solutionInfo.NestedSection.Dirs.Single(d => d.Guid == "{8FE39D73-9DBF-47A0-94E3-24F96625B4EA}");
            inFolderDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{177D7443-2536-4B05-8CBD-5FAD3CE75FD3}");
            inFolderDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{2DA8C987-D5E9-4756-930E-1EBE4AC8D0AD}");
            var solutionNamedSubDir = solutionInfo.NestedSection.Dirs.Single(d => d != inFolderDir);
            solutionNamedSubDir.Name.Should().Be("SolutionA");
            solutionNamedSubDir.NestedProjects.Should().HaveCount(2);
            solutionNamedSubDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{8FE39D73-9DBF-47A0-94E3-24F96625B4EA}");
            solutionNamedSubDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{23030AF7-941A-498B-805B-2EF13D6982E7}");
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public void MergeSolutionsAB()
        {
            var outDir = Path.Combine(Path.GetTempPath(), nameof(MergeSolutionsTests));
            RecursiveDelete(new DirectoryInfo(outDir));
            Directory.CreateDirectory(outDir);
            var outSolutionName = "out.sln";

            var solutionPaths = new[] {"TestData/SolutionA/SolutionA.sln", "TestData/SolutionB/SolutionB.sln"};

            var outputSolutionPath = Path.Combine(outDir, outSolutionName);
            var mergedSolution = SolutionInfo.MergeSolutions(Path.GetFileNameWithoutExtension(outputSolutionPath),
                Path.GetDirectoryName(outputSolutionPath) ?? "",
                out _,
                null,
                solutionPaths.Select(SolutionInfo.Parse).ToArray());
            mergedSolution.Save();

            var solutionInfo = SolutionInfo.Parse(outputSolutionPath);

            solutionInfo.Projects.Should().HaveCount(9);
            solutionInfo.NestedSection.Dirs.Should().HaveCount(4);
            solutionInfo.NestedSection.Dirs.Should()
                .Contain(p => p.Guid == "{8FE39D73-9DBF-47A0-94E3-24F96625B4EA}");
            var inFolderDir = solutionInfo.NestedSection.Dirs.Single(d => d.Guid == "{8FE39D73-9DBF-47A0-94E3-24F96625B4EA}");
            inFolderDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{177D7443-2536-4B05-8CBD-5FAD3CE75FD3}");
            inFolderDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{2DA8C987-D5E9-4756-930E-1EBE4AC8D0AD}");
            var solutionANamedSubDir = solutionInfo.NestedSection.Dirs.Single(d => d.Name == "SolutionA");
            solutionANamedSubDir.NestedProjects.Should().HaveCount(2);
            solutionANamedSubDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{8FE39D73-9DBF-47A0-94E3-24F96625B4EA}");
            solutionANamedSubDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{23030AF7-941A-498B-805B-2EF13D6982E7}");
            var solutionBNamedSubDir = solutionInfo.NestedSection.Dirs.Single(d => d.Name == "SolutionB");
            solutionBNamedSubDir.NestedProjects.Should().HaveCount(2);
            solutionBNamedSubDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{8C9DBCF6-C2A9-4D55-BD48-AADF40240336}");
            solutionBNamedSubDir.NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{EADACA47-6660-4693-A6A8-6ACFF1CF6A46}");
        }

        [Fact]
        public void NestedProjectsParsing()
        {
            var solutionInfo = SolutionInfo.Parse("TestData/SolutionA/SolutionA.sln");
            solutionInfo.NestedSection.Dirs.Should().HaveCount(1);
            solutionInfo.NestedSection.Dirs[0].Guid.Should().Be("{8FE39D73-9DBF-47A0-94E3-24F96625B4EA}");
            solutionInfo.NestedSection.Dirs[0].NestedProjects.Should().HaveCount(2);
            solutionInfo.NestedSection.Dirs[0].NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{177D7443-2536-4B05-8CBD-5FAD3CE75FD3}");
            solutionInfo.NestedSection.Dirs[0].NestedProjects.Should()
                .Contain(p => p.Project.Guid == "{2DA8C987-D5E9-4756-930E-1EBE4AC8D0AD}");
        }

        [Fact]
        public void ProjectFilter()
        {
            var outDir = Path.Combine(Path.GetTempPath(), nameof(MergeSolutionsTests));
            RecursiveDelete(new DirectoryInfo(outDir));
            Directory.CreateDirectory(outDir);
            var outSolutionName = "out.sln";

            var solutionPaths = new[] {"TestData/SolutionA/SolutionA.sln", "TestData/SolutionB/SolutionB.sln"};

            var outputSolutionPath = Path.Combine(outDir, outSolutionName);

            var mergedSolution = SolutionInfo.MergeSolutions(Path.GetFileNameWithoutExtension(outputSolutionPath),
                Path.GetDirectoryName(outputSolutionPath) ?? "",
                out _,
                project =>
                {
                    if (project.Name == "InSolutionFolderClassLibraryB")
                    {
                        return false;
                    }

                    return true;
                },
                solutionPaths.Select(SolutionInfo.Parse).ToArray());
            mergedSolution.Save();

            var solutionInfo = SolutionInfo.Parse(outputSolutionPath);

            solutionInfo.Projects.Should().HaveCount(8);
            solutionInfo.Projects.Should().NotContain(p => p.Name == "InSolutionFolderClassLibraryB");
            solutionInfo.Projects.Should().Contain(p => p.Name == "InDiskFolderClassLibrary");

            mergedSolution = SolutionInfo.MergeSolutions(Path.GetFileNameWithoutExtension(outputSolutionPath),
                Path.GetDirectoryName(outputSolutionPath) ?? "",
                out _,
                project =>
                {
                    if (project.SolutionName == "SolutionA" && project.Name == "InDiskFolderClassLibrary")
                    {
                        return false;
                    }

                    return true;
                },
                solutionPaths.Select(SolutionInfo.Parse).ToArray());
            mergedSolution.Save();

            solutionInfo = SolutionInfo.Parse(outputSolutionPath);

            solutionInfo.Projects.Should().HaveCount(8);
            solutionInfo.Projects.Should().Contain(p => p.Name == "InSolutionFolderClassLibraryB");
            solutionInfo.Projects.Should().NotContain(p => p.Name == "InDiskFolderClassLibrary");
        }

        private void RecursiveDelete(DirectoryInfo baseDir)
        {
            if (!baseDir.Exists)
            {
                return;
            }

            foreach (var dir in baseDir.EnumerateDirectories())
            {
                RecursiveDelete(dir);
            }

            var files = baseDir.GetFiles();

            foreach (var file in files)
            {
                file.IsReadOnly = false;
                file.Delete();
            }

            baseDir.Delete();
        }
    }
}