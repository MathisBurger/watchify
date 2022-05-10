using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
    private readonly IConfiguration Configuration;

    public VideoController(DbAccess db, IAuthorization auth, VideoService videoService, IConfiguration configuration)
    {
        Db = db;
        Auth = auth;
        VideoService = videoService;
        Configuration = configuration;
    }

    [RateLimitFilter(5, 10)]
    [TypeFilter(typeof(FiltersAuthorization))]
    [HttpPost("[action]")]
    public async Task<ActionResult<Video>> CreateVideo([FromBody] CreateVideoRequest request)
    {
        var video = await VideoService.CreateVideoEntity(request, AuthClaims);
        return Ok(video);
    }

    [RateLimitFilter(5, 10)]
    [TypeFilter(typeof(FiltersAuthorization))]
    [Consumes("multipart/form-data")]
    [HttpPost("[action]")]
    public async Task<ActionResult<Video>> UploadVideo(List<IFormFile> files, [FromQuery] string videoId)
    {
        Console.WriteLine(files.Count);
        if (files.Count != 1)
        {
            return BadRequest("Please send only one file via the request");
        }

        var formFile = files[0];
        if (formFile.Length > 0)
        {
            var videoEntity = await Db.VideoRepository.FindOneById(Guid.Parse(videoId));
            if (videoEntity == null || videoEntity.Owner.Id != AuthorizedUser.Id)
            {
                return BadRequest("Video does not exist or you do not have access to it");
            }
            Console.WriteLine(formFile.ContentType);
            var filePath = Path.Combine(Configuration["StoredFilesPath"], videoId.ToString());
            var stream = System.IO.File.Create(filePath);
            await formFile.CopyToAsync(stream);
            stream.Close();

            return Ok("Successfully uploaded video");
        }
        else
        {
            return BadRequest("Form file has no content");
        }
    }
}