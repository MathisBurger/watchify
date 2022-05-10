using System.Text.Json.Serialization;

namespace watchify.Models.Request;

public class CreateVideoRequest
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }
    
    [JsonPropertyName("category")]
    public string Category { get; set; }
}