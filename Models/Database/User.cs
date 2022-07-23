using System.ComponentModel.DataAnnotations.Schema;

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
    
    [InverseProperty("LikedBy")]
    public IList<Video> LikedVideos { get; set; }
    
    [InverseProperty("DislikedBy")]
    public IList<Video> DislikedVideos { get; set; }

    [InverseProperty("Owner")]
    public IList<Video> PublishedVideo { get; set; }

    public User()
    {
        LikedVideos = new List<Video>();
        PublishedVideo = new List<Video>();
        DislikedVideos = new List<Video>();
    }

}