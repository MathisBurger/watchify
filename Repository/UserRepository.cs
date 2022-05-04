using Microsoft.EntityFrameworkCore;
using watchify.Models.Database;
using watchify.Models.Request;
using watchify.Modules;
using watchify.Shared;

namespace watchify.Repository;

public class UserRepository
{

    private readonly DbContext ctx;
    private readonly IPasswordHasher hasher;

    public UserRepository(DbContext ctx, IPasswordHasher hasher)
    {
        this.ctx = ctx;
        this.hasher = hasher;
    }

    public async Task<User> RegisterUser(RegisterRequest request)
    {
        var user = new User();
        user.Username = request.Username;
        user.Password = hasher.HashFromPassword(request.Password);
        ctx.Add(user);
        await ctx.SaveChangesAsync();
        return user;
    }

}