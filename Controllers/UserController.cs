using Microsoft.AspNetCore.Mvc;
using watchify.Filter;
using watchify.Models.Database;

namespace watchify.Controllers;

[Route("[controller]")]
[TypeFilter(typeof(FiltersAuthorization))]
[ApiController]
public class UserController : AuthorizedControllerBase
{

    [HttpGet("[action]")]
    public ActionResult<User> Me() => Ok(AuthorizedUser);

}