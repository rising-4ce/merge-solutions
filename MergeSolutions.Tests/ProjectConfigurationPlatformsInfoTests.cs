using FluentAssertions;
using MergeSolutions.Core.Parsers.GlobalSection;
using Xunit;

namespace MergeSolutions.Tests
{
    public class ProjectConfigurationPlatformsInfoTests
    {
        [Fact]
        public void ParsePlatformInfoLine()
        {
            var key = "{EB8B3AAD-6725-42BC-8A48-D4D4F6574F64}.AzureDebug|Any CPU.ActiveCfg";
            var value = "Debug|Any CPU";
            var line = new KeyValuePair<string, string>(key, value);
            var platformInfoLine = new ProjectConfigurationPlatformsInfo.PlatformInfoLine(line);
            platformInfoLine.ProjectGuid.Should().Be("EB8B3AAD-6725-42BC-8A48-D4D4F6574F64");
            platformInfoLine.SolutionConfiguration.Should().Be("AzureDebug");
            platformInfoLine.SolutionPlatform.Should().Be("Any CPU");
            platformInfoLine.Parameter.Should().Be("ActiveCfg");
            platformInfoLine.ProjectConfiguration.Should().Be("Debug");
            platformInfoLine.ProjectPlatform.Should().Be("Any CPU");
            platformInfoLine.ToLine().Should().Be(line);

            key = "{EB8B3AAD-6725-42BC-8A48-D4D4F6574F64}.Beta|Any CPU.Build.0";
            value = "Release|Any CPU";
            line = new KeyValuePair<string, string>(key, value);
            platformInfoLine = new ProjectConfigurationPlatformsInfo.PlatformInfoLine(line);
            platformInfoLine.ProjectGuid.Should().Be("EB8B3AAD-6725-42BC-8A48-D4D4F6574F64");
            platformInfoLine.SolutionConfiguration.Should().Be("Beta");
            platformInfoLine.SolutionPlatform.Should().Be("Any CPU");
            platformInfoLine.Parameter.Should().Be("Build.0");
            platformInfoLine.ProjectConfiguration.Should().Be("Release");
            platformInfoLine.ProjectPlatform.Should().Be("Any CPU");
            platformInfoLine.ToLine().Should().Be(line);
        }
    }
}