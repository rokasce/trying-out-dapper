using API.Contracts.Data;
using API.Database;
using Dapper;

namespace API.Repositories;

public class PostRespository : IPostRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public PostRespository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(PostDto post)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO Posts (Id, UserId, Title, Content)
            VALUES (@Id, @UserId, @Title, @Content)",
            post);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"DELETE FROM Posts WHERE Id = @Id", new { Id = id });

        return result > 0;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        
        return await connection.QueryAsync<PostDto>("SELECT * FROM Posts");

    }

    public async Task<PostDto> GetAsync(Guid id)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection
            .QuerySingleOrDefaultAsync<PostDto>(@"SELECT * FROM Posts WHERE Id = @Id LIMIT 1", new { Id = id });
    }

    public async Task<bool> UpdateAsync(PostDto post)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();   
        var result = await connection.ExecuteAsync(
            @"UPDATE Posts SET Title = @Title, Content = @Content", 
            post);

        return result > 0;
    }
}
