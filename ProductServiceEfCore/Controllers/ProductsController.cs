using Microsoft.AspNetCore.Mvc;
using ProductServiceEfCore.Contracts;
using ProductServiceEfCore.Models;
using ProductServiceEfCore.Repositories;

namespace ProductServiceEfCore.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : CrudControllerBase<Product, IProductRepository>
{
    public ProductsController(IProductRepository productRepository) 
        : base(productRepository)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody]ProductCreateDto dto)
    {
        var document = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            SellerId = dto.SellerId,
            Stock = dto.Stock,
            Images = dto.Images,
        };
        return await Create(document);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromRoute]int id, [FromBody]ProductUpdateDto dto)
    {
        var document = new Product
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            SellerId = dto.SellerId,
            Stock = dto.Stock,
            Images = dto.Images,
        };
        return await Update(document);
    }
}