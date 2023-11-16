using System.Data.Common;
using System.Text.Json.Serialization;
using Microsoft.Data.Sqlite;
using ProductServiceSqliteAot.Contracts;
using ProductServiceSqliteAot.Models;
using ProductServiceSqliteAot.Repositories;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Adjust the implementation class as needed
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // Adjust the implementation class as needed
builder.Services.AddScoped<DbConnection, SqliteConnection>(_ =>
    new SqliteConnection("Data Source=app.sqlite"));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapGet("/api/categories", async (ICategoryRepository repo, int pageNumber, int pageSize) =>
{
    var categories = await repo.Get(pageNumber, pageSize);
    return Results.Ok(categories);
});

app.MapGet("/api/categories/{id}", async (ICategoryRepository repo, int id) =>
{
    var category = await repo.GetById(id.ToString());
    return category != null ? Results.Ok(category) : Results.NotFound();
});

app.MapPost("/api/categories", async (ICategoryRepository repo, CategoryCreateDto dto) =>
{
    var category = new Category
    {
        Name = dto.Name
    };
    var createdCategory = await repo.Create(category);
    return Results.Created($"/api/categories/{createdCategory.Id}", createdCategory);
});

app.MapPut("/api/categories/{id}", async (ICategoryRepository repo, int id, CategoryUpdateDto dto) =>
{
    var categoryToUpdate = await repo.GetById(id.ToString());
    if (categoryToUpdate == null) return Results.NotFound();

    categoryToUpdate.Name = dto.Name;

    var updatedCategory = await repo.Update(categoryToUpdate);
    return Results.Ok(updatedCategory);
});

app.MapDelete("/api/categories/{id}", async (ICategoryRepository repo, int id) =>
{
    await repo.Delete(id.ToString());
    return Results.Ok();
});

app.MapGet("/api/products", async (IProductRepository repo, int pageNumber, int pageSize) =>
{
    var products = await repo.Get(pageNumber, pageSize);
    return Results.Ok(products);
});

app.MapGet("/api/products/{id}", async (IProductRepository repo, string id) =>
{
    var product = await repo.GetById(id);
    return product != null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/api/products", async (IProductRepository repo, ProductCreateDto dto) =>
{
    var product = new Product
    {
        CategoryId = dto.CategoryId,
        Name = dto.Name,
        Description = dto.Description,
        Price = dto.Price,
        SellerId = dto.SellerId,
        Stock = dto.Stock
        // Images field should be handled according to your application logic
    };
    var createdProduct = await repo.Create(product);
    return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
});

app.MapPut("/api/products/{id}", async (IProductRepository repo, int id, ProductUpdateDto dto) =>
{
    var productToUpdate = await repo.GetById(id.ToString());
    if (productToUpdate == null) return Results.NotFound();

    productToUpdate.CategoryId = dto.CategoryId;
    productToUpdate.Name = dto.Name;
    productToUpdate.Description = dto.Description;
    productToUpdate.Price = dto.Price;
    productToUpdate.SellerId = dto.SellerId;
    productToUpdate.Stock = dto.Stock;
    // Update Images field as necessary

    var updatedProduct = await repo.Update(productToUpdate);
    return Results.Ok(updatedProduct);
});

app.MapDelete("/api/products/{id}", async (IProductRepository repo, string id) =>
{
    await repo.Delete(id);
    return Results.Ok();
});

app.Run();

using var connection = new SqliteConnection("Data Source=app.sqlite");
connection.Open();

var command = connection.CreateCommand();
command.CommandText = @"CREATE TABLE Categories (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL
                            );

                            CREATE TABLE Products (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                CategoryId INTEGER,
                                Name TEXT NOT NULL,
                                Description TEXT,
                                Price REAL,
                                SellerId INTEGER,
                                Stock INTEGER,
                                FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
                            );";
command.ExecuteNonQuery();

[JsonSerializable(typeof(List<Product>))]
[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(ProductCreateDto))]
[JsonSerializable(typeof(ProductUpdateDto))]
[JsonSerializable(typeof(List<Category>))]
[JsonSerializable(typeof(Category))]
[JsonSerializable(typeof(CategoryCreateDto))]
[JsonSerializable(typeof(CategoryUpdateDto))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}