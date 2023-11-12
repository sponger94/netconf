using ProductServiceEfCore.Infrastructure;
using ProductServiceEfCore.Models;

namespace ProductServiceEfCore.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ProductsDbContext productsDbContext)
    {
        DbContext = productsDbContext;
    }
}