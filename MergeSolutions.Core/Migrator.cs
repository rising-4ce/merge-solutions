using System.Text.Json.Nodes;

namespace MergeSolutions.Core
{
    public class Migrator : IMigrator
    {
        public string? Migrate(string? content, string rootDir)
        {
            if (content == null)
            {
                return null;
            }

            if (JsonNode.Parse(content) is not JsonObject rootObject)
            {
                return null;
            }

            var contentVersion = rootObject[nameof(MergePlan.Version)]?.GetValue<string>();
            switch (contentVersion)
            {
                case null:
                    return Migrate(MigrateTo1_0(rootObject, rootDir), rootDir);
                case "1.0":
                    break;
            }

            return content;
        }

        private static string MigrateTo1_0(JsonObject rootObject, string rootDir)
        {
            var newSolutions = new JsonArray();
            var newExcludedProjects = new JsonArray();
            if (rootObject["Solutions"] is JsonObject solutions)
            {
                foreach (var solution in solutions)
                {
                    var path = solution.Value?.GetValue<string>();
                    if (path == null)
                    {
                        continue;
                    }

                    var newSolution = new JsonObject();
                    var relativePath = Path.GetRelativePath(rootDir, path);
                    newSolution.Add("RelativePath", relativePath);
                    newSolution.Add("NodeName", solution.Key);
                    newSolutions.Add(newSolution);
                    if (rootObject["ExcludedProjects"] is JsonArray excludedProjects)
                    {
                        foreach (var projectNode in excludedProjects.Where(p => p?["Key"]?.GetValue<string>() == solution.Key))
                        {
                            var guid = projectNode?["Value"]?.GetValue<string>();
                            if (guid == null)
                            {
                                continue;
                            }

                            var newExcludedProject = new JsonObject
                            {
                                {"Key", relativePath},
                                {"Value", guid}
                            };

                            newExcludedProjects.Add(newExcludedProject);
                        }
                    }
                }
            }

            rootObject["Solutions"] = newSolutions;
            rootObject["ExcludedProjects"] = newExcludedProjects;
            rootObject["Version"] = "1.0";
            return rootObject.ToJsonString();
        }
    }
}