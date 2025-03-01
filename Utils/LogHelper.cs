using Serilog;

namespace EasternPeakAutomation.Utils
{
    public static class LogHelper
    {
        /// <summary>
        /// Initializes Serilog with Console and File sinks.
        /// </summary>
        public static void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose() // Set minimum log level
                .WriteTo.Console()    // Log to the console
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Log to file with daily rolling
                .CreateLogger();

            Log.Information("Logger initialized.");
        }

        /// <summary>
        /// Shuts down the logger (if needed).
        /// </summary>
        public static void ShutdownLogger()
        {
            Log.Information("Shutting down logger.");
            Log.CloseAndFlush();
        }
    }
}
