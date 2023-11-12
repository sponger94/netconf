using Microsoft.AspNetCore.Mvc;
using ProductService.Repositories;

namespace ProductService.Controllers;

public class CrudControllerBase<TDocument, TRepository> : ControllerBase 
    where TDocument : IDocumentEntity 
    where TRepository : IRepositoryBase<TDocument>
{
    private readonly TRepository _repository;

    public CrudControllerBase(TRepository repository)
    {
        _repository = repository;
    }

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
    
    protected async Task<IActionResult> Create([FromBody]TDocument document)
    {
        var createdDocument = await _repository.Create(document);
        return Created("", createdDocument);
    }
    
    protected async Task<IActionResult> Update([FromBody]TDocument document)
    {
        var updatedDocument = await _repository.Update(document);
        return Ok(updatedDocument);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]string id)
    {
        await _repository.Delete(id);
        return Ok();
    }
}