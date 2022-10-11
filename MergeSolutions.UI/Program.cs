using System.Runtime.CompilerServices;
using MergeSolutions.Core;
using MergeSolutions.Core.Services;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable LocalizableElement

namespace MergeSolutions.UI
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        public static void ShowExceptionMessage(string message, Exception? exception, string? caption = null)
        {
            MessageBox.Show(@$"{message} 

{exception?.Message}
{exception?.InnerException?.Message}",
                caption ?? "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs args)
        {
            ShowExceptionMessage("Application thread unhandled exception.", args.Exception);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var exception = args.ExceptionObject as Exception ?? args.ExceptionObject as RuntimeWrappedException;
            ShowExceptionMessage("CurrentDomain unhandled exception.", exception);
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
                Application.ThreadException += ApplicationOnThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                var services = new ServiceCollection();
                services.AddTransient<MainForm>();
                services.AddSingleton<IMergeSolutionsService, MergeSolutionService>();
                services.AddSingleton<IMigrator, Migrator>();
                services.AddSingleton<ISolutionService, SolutionService>();
                services.AddTransient<IStartup>(_ => new Startup(args.ElementAtOrDefault(0)));
                ServiceProvider = services.BuildServiceProvider();

                Application.Run(ServiceProvider.GetRequiredService<MainForm>());
            }
            catch (Exception e)
            {
                ShowExceptionMessage("Unhandled application exception.", e);
            }
        }
    }
}