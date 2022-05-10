using Microsoft.AspNetCore.Mvc;
using watchify.Filter;
using watchify.Models.Database;
using watchify.Models.Request;
using watchify.Modules;
using watchify.Services;
using watchify.Shared;

namespace watchify.Controllers;

[Route("[controller]")]
[ApiController]
public class VideoController : AuthorizedControllerBase
{
    private readonly DbAccess Db;
    private readonly IAuthorization Auth;
    private readonly VideoService VideoService;

    public VideoController(DbAccess db, IAuthorization auth, VideoService videoService)
    {
        Db = db;
        Auth = auth;
        VideoService = videoService;
    }

    [RateLimitFilter(5, 10)]
    [TypeFilter(typeof(FiltersAuthorization))]
    [HttpPost("[action]")]
    public async Task<ActionResult<Video>> CreateVideo([FromBody] CreateVideoRequest request)
    {
        var video = await VideoService.CreateVideoEntity(request, AuthClaims);
        return Ok(video);
    }
}