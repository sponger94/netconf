using Microsoft.AspNetCore.Mvc;
using ProductServiceEfCore.Repositories;

namespace ProductServiceEfCore.Controllers;

public class CrudControllerBase<TEntity, TRepository> (TRepository repository) : ControllerBase
    where TEntity : class, IEntity
    where TRepository : IRepositoryBase<TEntity>
{
    private readonly TRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]int pageNumber, [FromQuery]int pageSize)
    {
        var documents = await _repository.Get(pageNumber, pageSize);
        return Ok(documents);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]string id)
    {
        var document = await _repository.GetById(id);
        return Ok(document);
    }
    
    protected async Task<IActionResult> Create([FromBody]TEntity document)
    {
        var entity = await _repository.Create(document);
        return Created("", entity);
    }
    
    protected async Task<IActionResult> Update([FromBody]TEntity document)
    {
        var updatedEntity = await _repository.Update(document);
        return Ok(updatedEntity);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]string id)
    {
        await _repository.Delete(id);
        return Ok();
    }
}