namespace MergeSolutions.UI
{
    public interface IStartup
    {
        string? PlanPath { get; }
    }

    internal class Startup : IStartup
    {
        public Startup(string? planPath)
        {
            PlanPath = planPath;
        }

        public string? PlanPath { get; }
    }
}