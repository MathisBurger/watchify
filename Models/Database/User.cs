namespace watchify.Models.Database;

public class User : Entity
{
    /// <summary>
    /// The username of the user
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// The hashed password of the user
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public string Password { get; set; }

}