using EasternPeakAutomation.Config;
using Serilog;
using RestSharp;
using Serilog;

namespace EasternPeakAutomation.API.Tests
{
    /// <summary>
    /// Base class for API tests; handles common setup and teardown logic.
    /// </summary>
    public abstract class BaseApiTest
    {
        protected RestClient Client;

        [SetUp]
        public virtual void Setup()
        {
            Log.Information("Initializing RestClient for API tests.");
            Client = new RestClient(TestConfig.Settings.BaseUrlApi);
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (Client != null)
            {
                Log.Information("Disposing RestClient.");
                Client.Dispose();
                Client = null;
            }
        }
    }
}