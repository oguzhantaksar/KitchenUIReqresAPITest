using System.IO;
using Microsoft.Extensions.Configuration;

namespace EasternPeakAutomation.Config
{
    /// <summary>
    /// Central configuration for test settings loaded from appsettings.json.
    /// </summary>
    public static class TestConfig
    {
        /// <summary>
        /// The test settings loaded from the configuration file.
        /// </summary>
        public static TestSettingsModel Settings { get; }

        // Static constructor to load the settings at startup.
        static TestConfig()
        {
            // Set the base path to the current directory (where the test assembly runs).
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            Settings = configuration.GetSection("TestSettings").Get<TestSettingsModel>();
        }
    }
}