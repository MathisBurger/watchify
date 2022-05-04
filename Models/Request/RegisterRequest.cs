using System.Text.Json.Serialization;

namespace watchify.Models.Request;

public class RegisterRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
}