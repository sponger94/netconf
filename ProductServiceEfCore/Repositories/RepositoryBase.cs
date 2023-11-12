using System.Net;
using Microsoft.EntityFrameworkCore;
using ProductServiceEfCore.Exceptions;
using ProductServiceEfCore.Infrastructure;

namespace ProductServiceEfCore.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
{
    protected DbSet<TEntity> Set => DbContext.Set<TEntity>();
    
    protected ProductsDbContext DbContext { get; set; }

    public async Task<List<TEntity>> Get(int pageNumber, int pageSize)
    {
        return await Set.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<TEntity> GetById(string id)
    {
        var entity = await Set.FindAsync(id);
        if (entity == null)
        {
            throw new HttpStatusCodeException($"Entity not found {id}", HttpStatusCode.NotFound);
        }
        return entity;
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        await Set.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        Set.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(string id)
    {
        var entity = await Set.FindAsync(id);
        if (entity == null)
        {
            throw new HttpStatusCodeException($"Entity not found {id}", HttpStatusCode.NotFound);
        }
        Set.Remove(entity);
        await DbContext.SaveChangesAsync();
    }
}