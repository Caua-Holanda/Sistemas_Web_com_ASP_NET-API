using Microsoft.EntityFrameworkCore;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.Infra.Data;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly VibraDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(VibraDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public virtual async Task<TEntity> Read(Guid id)
    {
        var entityType = Db.Model.FindEntityType(typeof(TEntity));
        var primaryKey = entityType?.FindPrimaryKey();

        if (primaryKey == null || primaryKey.Properties.Count != 1)
            return null;

        var keyProperty = primaryKey.Properties[0];

        if (keyProperty.ClrType != typeof(Guid))
            return null;

        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, keyProperty.Name) == id);
    }

    public virtual async Task<IEnumerable<TEntity>> ReadAll()
    {
        return await DbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual void Create(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual async Task Delete(Guid id)
    {
        var entity = await ReadTracked(id);

        if (entity != null)
            DbSet.Remove(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    private async Task<TEntity> ReadTracked(Guid id)
    {
        var entityType = Db.Model.FindEntityType(typeof(TEntity));
        var primaryKey = entityType?.FindPrimaryKey();

        if (primaryKey == null || primaryKey.Properties.Count != 1)
            return null;

        var keyProperty = primaryKey.Properties[0];

        if (keyProperty.ClrType != typeof(Guid))
            return null;

        return await DbSet
            .FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, keyProperty.Name) == id);
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}
