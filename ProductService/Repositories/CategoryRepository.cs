using MongoDB.Driver;
using ProductService.Models;

namespace ProductService.Repositories;

public class CategoryRepository : RepositoryBase<CategoryDocument>, ICategoryRepository
{
    private const string CollectionName = "categories";
    
    public CategoryRepository(IMongoClient mongoClient)
    {
        DocumentCollection = mongoClient
            .GetDatabase(DatabaseName)
            .GetCollection<CategoryDocument>(CollectionName);
    }
}