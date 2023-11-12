using MongoDB.Bson;

namespace ProductService.Contracts;

public class ProductUpdateDto
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int SellerId { get; set; }
    public int Stock { get; set; }
    public List<string> Images { get; set; }
    public Dictionary<string, object> Fields { get; set; }
}