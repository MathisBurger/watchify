using Microsoft.AspNetCore.Mvc;
using watchify.Models.Database;
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
    public async Task<ActionResult<User>> Register([FromBody] RegisterRequest request)
    {
        return await Db.UserRepository.RegisterUser(request);
    }
}