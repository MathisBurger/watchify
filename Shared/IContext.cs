using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using watchify.Models.Database;

namespace watchify.Shared;

public interface IContext : IDisposable
{
    
    DbSet<User> Users { get; set; }
    DbSet<Video> Videos { get; set; }

    DatabaseFacade Database { get; }

    EntityEntry Add([NotNull] object entity);
    EntityEntry Remove([NotNull] object entity);
    void RemoveRange([NotNullAttribute] IEnumerable<object> entities);
    DbSet<TEntity> Set<TEntity>([NotNull] string name) where TEntity : class;
    EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}