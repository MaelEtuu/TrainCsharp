using Microsoft.EntityFrameworkCore;

namespace Train.Models.Repository.Managers;

public abstract class BaseManager<TEntity, TKey> where TEntity : class
{
    protected readonly TrainDbContext context;
    protected readonly DbSet<TEntity> dbSet;

    public BaseManager(TrainDbContext context)
    {
        this.context = context;
        this.dbSet = context?.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        dbSet.Remove(entity);
        await context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public abstract Task<TEntity?> GetByNameAsync(TKey name);

    public virtual async Task UpdateAsync(TEntity entityToUpdate, TEntity entity)
    {
        context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
    }
}