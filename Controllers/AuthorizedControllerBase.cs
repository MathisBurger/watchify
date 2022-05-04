using Microsoft.AspNetCore.Mvc;
using watchify.Models.Database;
using watchify.Models.Request;

namespace watchify.Controllers;

public class AuthorizedControllerBase : ControllerBase
{
    /// <summary>
    /// The AuthClaims of the user
    /// </summary>
    public AuthClaims AuthClaims { get; set; }
    
    /// <summary>
    /// The user that is currently logged in
    /// </summary>
    public User AuthorizedUser { get; set; }
}