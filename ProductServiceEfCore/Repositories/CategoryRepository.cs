using ProductServiceEfCore.Infrastructure;
using ProductServiceEfCore.Models;

namespace ProductServiceEfCore.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(ProductsDbContext productsDbContext)
    {
        DbContext = productsDbContext;
    }
}