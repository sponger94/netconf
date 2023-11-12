using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductService.Repositories;

namespace ProductService.Models;

public class ProductDocument : IDocumentEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    public ObjectId CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int SellerId { get; set; }
    public int Stock { get; set; }
    public List<string> Images { get; set; }
    public Dictionary<string, object> Fields { get; set; }
}