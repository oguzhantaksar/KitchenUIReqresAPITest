using EasternPeakAutomation.API.Clients;
using EasternPeakAutomation.API.Model;
using EasternPeakAutomation.API.TestData;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace EasternPeakAutomation.API.Tests
{
    /// <summary>
    /// Contains API tests for verifying ReqRes functionality.
    /// </summary>
    [TestFixture]
    public class ReqResApiTests : BaseApiTest
    {
        protected ReqResApiClient ReqResApiClient = new();
        
        [Test]
        public void VerifyFetchingUserList()
        {
            Log.Information("Starting test: VerifyFetchingUserList.");
            
            RestResponse response = ReqResApiClient.GetUserListResponse(2);
            UserListResponse userListResponse = JsonConvert.DeserializeObject<UserListResponse>(response.Content);
            
            Assert.Multiple(() =>
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200.");
                Assert.That(userListResponse.data.Count, Is.GreaterThan(0), "List is empty; at least one user is expected.");
                Assert.That(userListResponse.data[0].id, Is.Not.Null, "User id is null.");
            });
            
            Log.Information("Test VerifyFetchingUserList completed.");
        }

        [Test]
        public void VerifyCreatingNewUser()
        {
            Log.Information("Starting test: VerifyCreatingNewUser.");
            
            RestResponse response = ReqResApiClient.CreateUserResponse(UserTestData.OguzhanUser);
            CreateUserResponse createUserResponse = JsonConvert.DeserializeObject<CreateUserResponse>(response.Content);
            
            Assert.Multiple(() =>
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(201), "Status code is not 201.");
                Assert.That(createUserResponse.id, Is.Not.Null, "Response does not contain an id.");
                Assert.That(createUserResponse.name, Is.EqualTo(UserTestData.OguzhanUser.name), "Name does not match.");
                Assert.That(createUserResponse.job, Is.EqualTo(UserTestData.OguzhanUser.job), "Job does not match.");
            });
            
            Log.Information("Test VerifyCreatingNewUser completed.");
        }
    }
}
