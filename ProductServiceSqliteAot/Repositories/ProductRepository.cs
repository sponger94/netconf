using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using ProductServiceSqliteAot.Models;

namespace ProductServiceSqliteAot.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(DbConnection connection) : base(connection)
    {
    }

    protected override Product ConvertReaderToEntity(DbDataReader reader)
    {
        var product = new Product
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Description = reader.GetString(reader.GetOrdinal("Description")),
            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
            SellerId = reader.GetInt32(reader.GetOrdinal("SellerId")),
            Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
        };
        return product;
    }

    public override async Task<Product> Create(Product entity)
    {
        await Connection.OpenAsync();
        var command = Connection.CreateCommand();
        command.CommandText = @"INSERT INTO Products (CategoryId, Name, Description, Price, SellerId, Stock) 
                            VALUES (@CategoryId, @Name, @Description, @Price, @SellerId, @Stock);
                            SELECT last_insert_rowid();";

        command.Parameters.Add(new SqliteParameter("@CategoryId", DbType.Int32) { Value = entity.CategoryId });
        command.Parameters.Add(new SqliteParameter("@Name", DbType.String) { Value = entity.Name });
        command.Parameters.Add(new SqliteParameter("@Description", DbType.String) { Value = entity.Description });
        command.Parameters.Add(new SqliteParameter("@Price", DbType.Decimal) { Value = entity.Price });
        command.Parameters.Add(new SqliteParameter("@SellerId", DbType.Int32) { Value = entity.SellerId });
        command.Parameters.Add(new SqliteParameter("@Stock", DbType.Int32) { Value = entity.Stock });

        entity.Id = (int)(long)await command.ExecuteScalarAsync();
        return entity;
    }

    public override async Task<Product> Update(Product entity)
    {
        await Connection.OpenAsync();
        var command = Connection.CreateCommand();
        command.CommandText = @"UPDATE Products 
                            SET CategoryId = @CategoryId, 
                                Name = @Name, 
                                Description = @Description, 
                                Price = @Price, 
                                SellerId = @SellerId, 
                                Stock = @Stock
                            WHERE Id = @Id;";

        command.Parameters.Add(new SqliteParameter("@CategoryId", DbType.Int32) { Value = entity.CategoryId });
        command.Parameters.Add(new SqliteParameter("@Name", DbType.String) { Value = entity.Name });
        command.Parameters.Add(new SqliteParameter("@Description", DbType.String) { Value = entity.Description });
        command.Parameters.Add(new SqliteParameter("@Price", DbType.Decimal) { Value = entity.Price });
        command.Parameters.Add(new SqliteParameter("@SellerId", DbType.Int32) { Value = entity.SellerId });
        command.Parameters.Add(new SqliteParameter("@Stock", DbType.Int32) { Value = entity.Stock });
        command.Parameters.Add(new SqliteParameter("@Id", DbType.Int32) { Value = entity.Id });

        await command.ExecuteNonQueryAsync();
        return entity;
    }
}