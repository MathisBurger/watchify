using System.Text.Json.Serialization;

namespace watchify.Models.Database;

public class Video : Entity
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }
    
    [JsonPropertyName("category")]
    public string Category { get; set; }
    
    [JsonPropertyName("likes")]
    public int Likes { get; set; }
    
    [JsonPropertyName("dislikes")]
    public int Dislikes { get; set; }
    
    [JsonPropertyName("views")]
    public int Views { get; set; }
    
    [JsonPropertyName("owner")]
    public User Owner { get; set; }
}