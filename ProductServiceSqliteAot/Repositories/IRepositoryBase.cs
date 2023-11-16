namespace ProductServiceSqliteAot.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : class, IEntity
{
    Task<List<TEntity>> Get(int pageNumber, int pageSize);
    
    Task<TEntity?> GetById(string id);
    
    Task<TEntity> Create(TEntity document);
    
    Task<TEntity> Update(TEntity document);
    
    Task Delete(string id);
}