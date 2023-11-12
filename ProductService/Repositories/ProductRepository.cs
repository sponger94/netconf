using MongoDB.Driver;
using ProductService.Models;

namespace ProductService.Repositories;

public class ProductRepository : RepositoryBase<ProductDocument>, IProductRepository
{
    private const string CollectionName = "products";
    
    public ProductRepository(IMongoClient mongoClient)
    {
        DocumentCollection = mongoClient
            .GetDatabase(DatabaseName)
            .GetCollection<ProductDocument>(CollectionName);
    }
}