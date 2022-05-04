using watchify.Modules;
using watchify.Repository;

namespace watchify.Shared;

public class DbAccess
{
    public readonly UserRepository UserRepository;

    public DbAccess(DatabaseContext ctx, IPasswordHasher hasher)
    {
        UserRepository = new UserRepository(ctx, hasher);
    }

}