using Microsoft.AspNetCore.Mvc;
using watchify.Filter;
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

        video.Views += 1;
        Db.EntityManager.Videos.Update(video);
        await Db.EntityManager.SaveChangesAsync();
        var path = Path.GetFullPath(Url.Content(Path.Combine("wwwroot", "videos", videoId.ToString() + ".mp4")));
        return PhysicalFile(
            path,
            "video/mp4",
            enableRangeProcessing: true
            );
    }

}