namespace ProductService.Repositories;

public interface IRepositoryBase<TDocument> where TDocument : IDocumentEntity
{
    Task<List<TDocument>> Get(int pageNumber, int pageSize);
    
    Task<TDocument> GetById(string id);
    
    Task<TDocument> Create(TDocument document);
    
    Task<TDocument> Update(TDocument document);
    
    Task Delete(string id);
}