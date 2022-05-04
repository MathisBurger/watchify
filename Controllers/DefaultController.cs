using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace watchify.Controllers;

[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiController]
public class DefaultController : ControllerBase
{
    
    [HttpGet("/")]
    public ActionResult Default()
    {
        return Ok();
    }
    
}