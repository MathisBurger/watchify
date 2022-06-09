using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using watchify.Attributes;
using watchify.Filter;
using watchify.Models.Database;
using watchify.Models.Request;
using watchify.Modules;
using watchify.Services;
using watchify.Shared;
using watchify.Utility;

namespace watchify.Controllers;

[Route("[controller]")]
[ApiController]
public class VideoController : AuthorizedControllerBase
{
    private readonly DbAccess Db;
    private readonly IAuthorization Auth;
    private readonly VideoService VideoService;
    private readonly IConfiguration Configuration;
    private readonly int _fileSizeLimit;
    private readonly string[] _permittedExtensions = { ".mov" };

    public VideoController(DbAccess db, IAuthorization auth, VideoService videoService, IConfiguration configuration)
    {
        Db = db;
        Auth = auth;
        VideoService = videoService;
        Configuration = configuration;
        _fileSizeLimit = configuration.GetValue<int>("MaxVideoSize");
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
    [HttpPost("[action]")]
    [DisableFormValueModelBinding]
    public async Task<ActionResult<Video>> UploadVideo([FromQuery] Guid videoId)
    {
        var video = await Db.VideoRepository.FindOneById(videoId);
        if (video == null || video.Owner.Id != AuthorizedUser.Id)
        {
            return Unauthorized();
        }
        
        if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
        {
            ModelState.AddModelError("File", 
                $"The request couldn't be processed (Error 1).");

            return BadRequest(ModelState);
        }

        var boundary = MultipartRequestHelper.GetBoundary(
            MediaTypeHeaderValue.Parse(Request.ContentType),
            _fileSizeLimit);
        var reader = new MultipartReader(boundary, HttpContext.Request.Body);
        var section = await reader.ReadNextSectionAsync();

        while (section != null)
        {
            var hasContentDispositionHeader = 
                ContentDispositionHeaderValue.TryParse(
                    section.ContentDisposition, out var contentDisposition);

            if (hasContentDispositionHeader)
            {
                if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                {
                    ModelState.AddModelError("File", 
                        $"The request couldn't be processed (Error 2).");
                    // Log error

                    return BadRequest(ModelState);
                }
                else
                {
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                            contentDisposition.FileName.Value);
                    var trustedFileNameForFileStorage =
                        videoId.ToString() + '.' + ".mp4";

                    // **WARNING!**
                    // In the following example, the file is saved without
                    // scanning the file's contents. In most production
                    // scenarios, an anti-virus/anti-malware scanner API
                    // is used on the file before making the file available
                    // for download or for use by other systems. 
                    // For more information, see the topic that accompanies 
                    // this sample.

                    var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                        section, contentDisposition, ModelState, 
                        _permittedExtensions, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    using (var targetStream = System.IO.File.Create(
                        Path.Combine("wwwroot/videos/", trustedFileNameForFileStorage)))
                    {
                        await targetStream.WriteAsync(streamedFileContent);
                    }
                }
            }

            // Drain any remaining section body that hasn't been consumed and
            // read the headers for the next section.
            section = await reader.ReadNextSectionAsync();
        }

        return Ok();
    }
}