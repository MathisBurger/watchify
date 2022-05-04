using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace watchify.Models.Database;

public class Entity
{
    /// <summary>
    /// The ID of the entity
    /// </summary>
    [Key]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// The creation date of the entity
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now.ToUniversalTime();
    }

    public Entity(Entity entity)
    {
        Id = entity.Id;
        Created = entity.Created;
    }
}