using API.Contracts.Data;
using API.Database;
using Dapper;

namespace API.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CommentRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(CommentDto comment)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO Comments (Id, UserId, PostId, Content, CreatedAt)
            VALUES (@Id, @UserId, @PostId, @Content, @CreatedAt)",
            comment);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"DELETE FROM Comments WHERE Id = @Id", new { Id = id });

        return result > 0;
    }

    public async Task<IEnumerable<CommentDto>> GetAllAsync()
    {
        var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<CommentDto>("SELECT * FROM Comments");

    }

    public async Task<CommentDto> GetAsync(Guid id)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection
            .QuerySingleOrDefaultAsync<CommentDto>(@"SELECT * FROM Comments WHERE Id = @Id LIMIT 1", new { Id = id });
    }

    public async Task<IEnumerable<CommentDto>> GetPostComments(Guid postId)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection
            .QueryAsync<CommentDto>(@"SELECT * FROM Comments WHERE PostId = @Id", new {PostId = postId});
    }

    public async Task<bool> UpdateAsync(CommentDto comment)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();   
        var result = await connection.ExecuteAsync(
            @"UPDATE Comments SET Content = @Content", 
            comment);

        return result > 0;
    }
}
