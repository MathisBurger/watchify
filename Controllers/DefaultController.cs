using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using watchify.Filter;

namespace watchify.Controllers;

[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ProxyAddressFilter]
[ApiController]
public class DefaultController : ControllerBase
{


    [HttpGet("/")]
    [RateLimitFilter]
    public ActionResult Default()
    {
        return Ok();
    }
    
}