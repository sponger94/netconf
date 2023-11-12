using Microsoft.AspNetCore.Mvc;
using ProductServiceEfCore.Contracts;
using ProductServiceEfCore.Models;
using ProductServiceEfCore.Repositories;

namespace ProductServiceEfCore.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : CrudControllerBase<Category, ICategoryRepository>
{
    public CategoriesController(ICategoryRepository categoryRepository) 
        : base(categoryRepository)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateDto dto)
    {
        var document = new Category
        {
            Name = dto.Name,
        };
        return await Create(document);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute]int id, [FromBody]CategoryUpdateDto dto)
    {
        var document = new Category
        {
            Id = id,
            Name = dto.Name,
        };
        return await Update(document);
    }
}