using EasternPeakAutomation.Config;
using Serilog;
using Newtonsoft.Json;
using RestSharp;

namespace EasternPeakAutomation.API.Clients
{
    /// <summary>
    /// Base client for API interactions using RestSharp.
    /// </summary>
    public class BaseClient
    {
        protected RestClient Client { get; private set; }

        public BaseClient()
        {
            // Initialize RestClient with the base API URL.
            Client = new RestClient(TestConfig.Settings.BaseUrlApi);
            Log.Information($"RestClient initialized with base URL: {TestConfig.Settings.BaseUrlApi}");
        }

        /// <summary>
        /// Executes an API request with the given parameters.
        /// </summary>
        /// <param name="endpoint">API endpoint.</param>
        /// <param name="method">HTTP method (default GET).</param>
        /// <param name="queryParams">Query parameters if any.</param>
        /// <param name="body">Request body if needed.</param>
        /// <returns>Response from the API.</returns>
        protected RestResponse ExecuteRequest(
            string endpoint,
            Method method = Method.Get,
            Dictionary<string, string> queryParams = null,
            object body = null)
        {
            Log.Information($"Preparing {method} request for endpoint: {endpoint}");

            var request = new RestRequest(endpoint, method)
            {
                Timeout = TestConfig.Settings.ApiTimeout
            };

            // Set common headers.
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            // Append query parameters if provided.
            if (queryParams != null)
            {
                foreach (var param in queryParams)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }

            // Add JSON body if provided.
            if (body != null)
            {
                request.AddJsonBody(body);
            }

            Log.Information($"Sending {method} request to: {endpoint} " +
                       $"with query parameters: {(queryParams != null ? string.Join(", ", queryParams) : "none")} " +
                       $"{(body != null ? "and body: " + JsonConvert.SerializeObject(body) : "")}");

            var response = Client.Execute(request);

            // Log the API response details.
            Log.Information($"Response received. Status: {(int)response.StatusCode} - {response.StatusCode}");
            Log.Information("Response content: " + response.Content);

            return response;
        }
    }
}
