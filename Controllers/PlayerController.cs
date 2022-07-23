using Microsoft.AspNetCore.Mvc;
using watchify.Filter;
using watchify.Models.Database;
using watchify.Models.Response;
using watchify.Shared;

namespace watchify.Controllers;

[Route("[controller]")]
[ApiController]
public class PlayerController : AuthorizedControllerBase
{

    private readonly DbAccess Db;

    public PlayerController(DbAccess _db)
    {
        this.Db = _db;
    } 

    [RateLimitFilter(5, 10)]
    [HttpGet("[action]")]
    public async Task<ActionResult> Watch([FromQuery] Guid videoId)
    {
        var video = await Db.VideoRepository.FindOneById(videoId);
        if (null == video)
        {
            return BadRequest("The video does not exist");
        }
        var path = Path.GetFullPath(Url.Content(Path.Combine("wwwroot", "videos", videoId.ToString() + ".mp4")));
        return PhysicalFile(
            path,
            "video/mp4",
            enableRangeProcessing: true
            );
    }

    [RateLimitFilter(5, 10)]
    [HttpGet("[action]")]
    public async Task<ActionResult<Video>> GetVideoMetaData([FromQuery] Guid videoId)
    {
        var meta = await Db.VideoRepository.FindOneById(videoId);
        if (null == meta)
        {
            return BadRequest();
        }
        meta.Views += 1;
        Db.EntityManager.Videos.Update(meta);
        await Db.EntityManager.SaveChangesAsync();
        return Ok(meta);
    }

    [HttpPost("[action]")]
    [RateLimitFilter(5, 10)]
    [TypeFilter(typeof(FiltersAuthorization))]
    public async Task<ActionResult<Video>> LikeVideo([FromQuery] Guid videoId)
    {
        var video = await Db.VideoRepository.FindOneById(videoId);
        if (null == video)
        {
            return BadRequest();
        }

        if (video.LikedBy.Contains(AuthorizedUser))
        {
            video.Likes -= 1;
            video.LikedBy.Remove(AuthorizedUser);
            AuthorizedUser.LikedVideos.Remove(video);
            return Ok(video);
        }
        else
        {
            video.Likes += 1;
            video.LikedBy.Add(AuthorizedUser);
            AuthorizedUser.LikedVideos.Add(video);
        }

        if (video.DislikedBy.Contains(AuthorizedUser))
        {
            video.Dislikes -= 1;
            AuthorizedUser.DislikedVideos.Remove(video);
        }

        Db.EntityManager.Users.Update(AuthorizedUser);
        Db.EntityManager.Videos.Update(video);
        await Db.EntityManager.SaveChangesAsync();
        return video;
    }

    [HttpGet("[action]")]
    [RateLimitFilter(5, 10)]
    [TypeFilter(typeof(FiltersAuthorization))]
    public async Task<ActionResult<VideoLikeStatus>> GetLikeStatus([FromQuery] Guid videoId)
    {
        var video = await Db.VideoRepository.FindOneById(videoId);
        if (null == AuthorizedUser)
        {
            return Ok(new VideoLikeStatus
            {
                Liked = false,
                Disliked = false
            });
        }
        var currentUser = await Db.UserRepository.FindOneById(AuthorizedUser.Id);
        if (null == video || currentUser == null)
        {
            return BadRequest();
        }
        

        return Ok(new VideoLikeStatus
        {
            Liked = currentUser.LikedVideos.Contains(video),
            Disliked = currentUser.DislikedVideos.Contains(video)
        });
    }

}