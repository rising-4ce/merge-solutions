using MergeSolutions.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MergeSolutions.UI
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            services.AddTransient<MainForm>();
            services.AddSingleton<IMergeSolutionsService, MergeSolutionService>();
            services.AddSingleton<ISolutionService, SolutionService>();
            services.AddTransient<IStartup>(_ => new Startup(args?.ElementAtOrDefault(0)));
            ServiceProvider = services.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }
    }
}