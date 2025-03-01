namespace EasternPeakAutomation.API.Model;

/// <summary>
/// Request model for creating a new user.
/// </summary>
public class CreateUserRequest
{
    public string name { get; set; }
    public string job { get; set; }
}
