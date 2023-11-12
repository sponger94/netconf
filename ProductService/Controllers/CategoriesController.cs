using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProductService.Contracts;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : CrudControllerBase<CategoryDocument, ICategoryRepository>
{
    public CategoriesController(ICategoryRepository categoryRepository) 
        : base(categoryRepository)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateDto dto)
    {
        var document = new CategoryDocument
        {
            Name = dto.Name,
        };
        return await Create(document);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody]CategoryUpdateDto dto)
    {
        var document = new CategoryDocument
        {
            Id = new ObjectId(dto.Id),
            Name = dto.Name,
        };
        return await Update(document);
    }
}