using EasternPeakAutomation.API.Model;

namespace EasternPeakAutomation.API.TestData
{
    /// <summary>
    /// Provides test data for user-related API tests.
    /// </summary>
    public static class UserTestData
    {
        public static CreateUserRequest OguzhanUser => new CreateUserRequest()
        {
            name = "Oguzhan",
            job = "QA Engineer"
        };

        public static CreateUserRequest AnotherUser => new CreateUserRequest() 
        { 
            name = "Jane Doe", 
            job = "Software Developer" 
        };
    }
}
