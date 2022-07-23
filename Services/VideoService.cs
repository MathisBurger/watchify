using watchify.Models.Database;
using watchify.Models.Request;
using watchify.Shared;

namespace watchify.Services;

public class VideoService
{
    private readonly DbAccess Db;

    public VideoService(DbAccess db)
    {
        Db = db;
    }

    public async Task<Video> CreateVideoEntity(CreateVideoRequest request, AuthClaims claims)
    {
        var owner = await Db.UserRepository.FindOneById(claims.UserId);
        if (null == owner)
        {
            throw new Exception("User is not logged in currently");
        }
        
        var video = new Video
        {
            Title = request.Title,
            Category = request.Category,
            Description = request.Description,
            Tags = request.Tags,
            Owner = owner,
            Dislikes = 0,
            Likes = 0,
            Views = 0
        };
        owner.PublishedVideo.Add(video);
        Db.EntityManager.Videos.Add(video);
        Db.EntityManager.Users.Update(owner);
        await Db.EntityManager.SaveChangesAsync();
        return video;
    }
}