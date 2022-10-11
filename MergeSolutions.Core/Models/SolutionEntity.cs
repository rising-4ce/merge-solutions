using System.Text.Json.Serialization;

namespace MergeSolutions.Core.Models
{
    public class SolutionEntity
    {
        [JsonIgnore]
        public bool Collapsed { get; set; }

        public string? NodeName { get; set; }

        public string? RelativePath { get; set; }
    }
}