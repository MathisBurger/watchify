using Microsoft.EntityFrameworkCore;
using watchify.Models.Database;
using watchify.Shared;

namespace watchify.Repository;

public class VideoRepository : IRepository<Video?>
{
    private readonly IContext ctx;

    public VideoRepository(IContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Video?> FindOneById(Guid id)
    {
        return await ctx.Videos
            .Include(v => v.Owner)
            .Include(v => v.LikedBy)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
}