using EasternPeakAutomation.API.Paths;
using EasternPeakAutomation.Config;
using Serilog;
using RestSharp;
using Serilog;

namespace EasternPeakAutomation.API.Clients
{
    /// <summary>
    /// API client for interacting with ReqRes endpoints.
    /// </summary>
    public class ReqResApiClient : BaseClient
    {
        /// <summary>
        /// Gets the list of users for the specified page.
        /// </summary>
        public RestResponse GetUserListResponse(int page)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "page", page.ToString() }
            };

            Log.Information($"Fetching user list for page: {page}");
            return ExecuteRequest(EndpointPaths.ApiUserEndpoint, Method.Get, queryParams);
        }

        /// <summary>
        /// Creates a new user using the provided user object.
        /// </summary>
        public RestResponse CreateUserResponse(object newUser)
        {
            Log.Information("Creating new user with provided details.");
            return ExecuteRequest(EndpointPaths.ApiUserEndpoint, Method.Post, body: newUser);
        }
    }
}