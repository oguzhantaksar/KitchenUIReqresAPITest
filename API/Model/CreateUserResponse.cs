namespace EasternPeakAutomation.API.Model;

/// <summary>
/// Response model for the user creation API.
/// </summary>
public class CreateUserResponse
{
    public string name { get; set; }
    public string job { get; set; }
    public string id { get; set; }
    public DateTime createdAt { get; set; }
}
