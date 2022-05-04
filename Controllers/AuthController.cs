using Microsoft.AspNetCore.Mvc;
using watchify.Models.Request;
using watchify.Shared;

namespace watchify.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly DbAccess Db;

    public AuthController(DbAccess db)
    {
        Db = db;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        return Ok();
    }
}