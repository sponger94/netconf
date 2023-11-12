using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductService.Repositories;

namespace ProductService.Models;

public class CategoryDocument : IDocumentEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
}