using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using ProductServiceSqliteAot.Models;

namespace ProductServiceSqliteAot.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    protected override string TableName => "Categories";

    public CategoryRepository(DbConnection connection) : base(connection)
    {
    }

    protected override Category ConvertReaderToEntity(DbDataReader reader)
    {
        var category = new Category
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name"))
        };
        return category;
    }

    public override async Task<Category> Create(Category entity)
    {
        await Connection.OpenAsync();
        var command = Connection.CreateCommand();
        command.CommandText = @"INSERT INTO Categories (Name) VALUES (@Name); SELECT last_insert_rowid();";

        command.Parameters.Add(new SqliteParameter("@Name", DbType.String) { Value = entity.Name });

        entity.Id = (int)(long)await command.ExecuteScalarAsync();
        return entity;
    }

    public override async Task<Category> Update(Category entity)
    {
        await Connection.OpenAsync();
        var command = Connection.CreateCommand();
        command.CommandText = @"UPDATE Categories SET Name = @Name WHERE Id = @Id;";

        command.Parameters.Add(new SqliteParameter("@Name", DbType.String) { Value = entity.Name });
        command.Parameters.Add(new SqliteParameter("@Id", DbType.Int32) { Value = entity.Id });

        await command.ExecuteNonQueryAsync();
        return entity;
    }
}