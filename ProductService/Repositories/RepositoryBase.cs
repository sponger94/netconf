using System.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductService.Exceptions;

namespace ProductService.Repositories;

public abstract class RepositoryBase<TDocument> : IRepositoryBase<TDocument> where TDocument : IDocumentEntity
{
    protected const string DatabaseName = "product-service";
    
    protected IMongoCollection<TDocument> DocumentCollection { get; set; }

    public async Task<List<TDocument>> Get(int pageNumber, int pageSize)
    {
        var filter = new FilterDefinitionBuilder<TDocument>().Empty;
        var options = new FindOptions<TDocument>()
        {
            Skip = pageNumber * pageSize,
            Limit = pageSize
        };
        var cursor =  await DocumentCollection.FindAsync(filter, options);
        var products = await cursor.ToListAsync();
        return products;
    }

    public async Task<TDocument> GetById(string id)
    {
        var cursor = await DocumentCollection.FindAsync(x => x.Id == new ObjectId(id));
        return await cursor.FirstOrDefaultAsync();
    }

    public async Task<TDocument> Create(TDocument productDocument)
    {
        await DocumentCollection.InsertOneAsync(productDocument);
        return productDocument;
    }

    public async Task<TDocument> Update(TDocument productDocument)
    {
        var result = await DocumentCollection.ReplaceOneAsync(x => x.Id == productDocument.Id, productDocument);
        if (!result.IsAcknowledged || result.MatchedCount == 0)
        {
            throw new HttpStatusCodeException($"Document not found {productDocument.Id}", HttpStatusCode.NotFound);
        }
        return productDocument;
    }

    public async Task Delete(string id)
    {
        var result = await DocumentCollection.DeleteOneAsync(x => x.Id == new ObjectId(id));
        if (!result.IsAcknowledged || result.DeletedCount == 0)
        {
            throw new HttpStatusCodeException($"Document not found {id}", HttpStatusCode.NotFound);
        }
    }
}