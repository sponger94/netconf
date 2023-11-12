using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Repositories;

public interface IDocumentEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
}