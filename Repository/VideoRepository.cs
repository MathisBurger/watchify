using watchify.Shared;

namespace watchify.Repository;

public class VideoRepository : IRepository
{
    private readonly IContext ctx;

    public VideoRepository(IContext ctx)
    {
        this.ctx = ctx;
    }
}