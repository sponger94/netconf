using System.Data.Common;
using System.Net;
using Microsoft.Data.Sqlite;
using ProductServiceSqliteAot.Exceptions;

namespace ProductServiceSqliteAot.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
{
    protected DbConnection Connection { get; set; }

    protected virtual string TableName { get; } = $"{typeof(TEntity).Name}s";
    
    protected RepositoryBase(DbConnection connection)
    {
        Connection = connection;
    }

    public virtual async Task<List<TEntity>> Get(int pageNumber, int pageSize)
    {
        await Connection.OpenAsync();
        var command = Connection.CreateCommand();
        command.CommandText = $"SELECT * FROM {TableName} LIMIT @PageSize OFFSET @PageNumber";
        command.Parameters.Add(new SqliteParameter("@PageSize", pageSize));
        command.Parameters.Add(new SqliteParameter("@PageNumber", pageNumber * pageSize));

        var reader = await command.ExecuteReaderAsync();
        List<TEntity> results = new();
        while (await reader.ReadAsync())
        {
            // Assuming a method to convert a DbDataReader to TEntity
            results.Add(ConvertReaderToEntity(reader));
        }

        return results;
    }

    public async Task<TEntity?> GetById(string id)
    {
        await Connection.OpenAsync();
        var command = Connection.CreateCommand();
        command.CommandText = $"SELECT * FROM {TableName} WHERE Id = @Id";
        command.Parameters.Add(new SqliteParameter("@Id", id));

        var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows)
        {
            throw new HttpStatusCodeException($"Entity not found {id}", HttpStatusCode.NotFound);
        }

        await reader.ReadAsync();
        return ConvertReaderToEntity(reader); // Convert DbDataReader to TEntity
    }
    protected abstract TEntity ConvertReaderToEntity(DbDataReader reader);

    public abstract Task<TEntity> Create(TEntity entity);

    public abstract Task<TEntity> Update(TEntity entity);

    public async Task Delete(string id)
    {
        await Connection.OpenAsync();
        var command = Connection.CreateCommand();
        command.CommandText = $"DELETE FROM {TableName} WHERE Id = @Id";
        command.Parameters.Add(new SqliteParameter("@Id", id));

        await command.ExecuteNonQueryAsync();
    }
}