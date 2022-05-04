using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using watchify.Models.Request;
using watchify.Utility;

namespace watchify.Modules;

public class JWTAuthorization : IAuthorization
{
    private readonly byte[] signingKey;
    private readonly TimeSpan expiration;

    private JwtBuilder Builder
    {
        get => new JwtBuilder().WithSecret(signingKey).WithAlgorithm(new HMACSHA256Algorithm());
    }

    public JWTAuthorization(byte[] _signingKey, TimeSpan _expiration)
    {
        signingKey = _signingKey;
        expiration = _expiration;
    }

    public JWTAuthorization(byte[] _signingKey) :
        this(_signingKey, Constants.SESSION_EXPIRATION) {}
    
    public JWTAuthorization(string _signingKey) :
        this(Encoding.UTF8.GetBytes(_signingKey), Constants.SESSION_EXPIRATION) {}

    public JWTAuthorization() :
        this(CryptoRandom.GetBytes(256), Constants.SESSION_EXPIRATION) {}

    public string GetAuthToken(AuthClaims claims) =>
        Builder.AddClaim("sub", claims.SessionId)
            .AddClaim("uid", claims.UserId)
            .ExpirationTime(DateTime.Now.Add(expiration))
            .IssuedAt(DateTime.Now)
            .Encode();

    public AuthClaims ValidateAuth(string token) =>
        new AuthClaims(Builder.MustVerifySignature().Decode<IDictionary<string, object>>(token));
}