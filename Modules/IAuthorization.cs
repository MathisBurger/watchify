using watchify.Models.Request;

namespace watchify.Modules;

/// <summary>
/// Scaffold for the authorization service
/// </summary>
public interface IAuthorization
{
    /// <summary>
    /// Creates an auth token from the claims.
    /// </summary>
    /// <param name="claims">The auth claims of the user</param>
    /// <returns>The token that is used for authentification</returns>
    string GetAuthToken(AuthClaims claims);

    /// <summary>
    /// Checks if the token is valid
    /// </summary>
    /// <param name="token">The provided token</param>
    /// <returns>The auth claims from the token</returns>
    AuthClaims ValidateAuth(string token);
}