using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProductService.Contracts;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : CrudControllerBase<ProductDocument, IProductRepository>
{
    public ProductsController(IProductRepository productRepository) 
        : base(productRepository)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody]ProductCreateDto dto)
    {
        var document = new ProductDocument
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = new ObjectId(dto.CategoryId),
            SellerId = dto.SellerId,
            Stock = dto.Stock,
            Images = dto.Images,
            Fields = dto.Fields
        };
        return await Create(document);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody]ProductUpdateDto dto)
    {
        var document = new ProductDocument
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = new ObjectId(dto.CategoryId),
            SellerId = dto.SellerId,
            Stock = dto.Stock,
            Images = dto.Images,
            Fields = dto.Fields
        };
        return await Update(document);
    }
}