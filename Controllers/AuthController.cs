using Microsoft.AspNetCore.Mvc;
using watchify.Models.Database;
using watchify.Models.Request;
using watchify.Modules;
using watchify.Shared;

namespace watchify.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly DbAccess Db;
    private readonly IAuthorization auth;

    public AuthController(DbAccess db, IAuthorization _auth)
    {
        Db = db;
        auth = _auth;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<User>> Register([FromBody] RegisterRequest request)
    {
        return Ok(await Db.UserRepository.RegisterUser(request));
    }
    
    /// <summary>
    /// Logs in a user
    /// </summary>
    /// <param name="creds">The login credentials</param>
    /// <returns>The http response that indicates the login status</returns>
    [HttpPost("[action]")]
    public async Task<ActionResult> Login([FromBody] LoginRequest creds)
    {
        var loginSuccessful = await Db.UserRepository.LoginUser(creds);
        if (!loginSuccessful)
        {
            return Unauthorized();
        }

        var user = await Db.UserRepository.FindUserByUsername(creds.Username);
        
        var sessionJwt = auth.GetAuthToken(new AuthClaims(userId: user!.Id));
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            MaxAge = Constants.SESSION_EXPIRATION,
            Secure = true,
            SameSite = SameSiteMode.Lax
        };
        Response.Cookies.Append(Constants.SESSION_COOKIE_NAME, sessionJwt, cookieOptions);
        Response.Headers.AccessControlExposeHeaders = "Set-Cookie";
        return Ok();
    }
}