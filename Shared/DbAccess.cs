using watchify.Modules;
using watchify.Repository;

namespace watchify.Shared;

public class DbAccess
{
    public readonly UserRepository UserRepository;
    public readonly VideoRepository VideoRepository;

    public DbAccess(IContext ctx, IPasswordHasher hasher)
    {
        UserRepository = new UserRepository(ctx, hasher);
        VideoRepository = new VideoRepository(ctx);
    }

}