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
        
        var video = new Video();
        video.Title = request.Title;
        video.Category = request.Category;
        video.Description = request.Description;
        video.Tags = request.Tags;
        video.Owner = owner;
        video.Dislikes = 0;
        video.Likes = 0;
        video.Views = 0;
        Db.EntityManager.Videos.Add(video);
        await Db.EntityManager.SaveChangesAsync();
        return video;
    }
}