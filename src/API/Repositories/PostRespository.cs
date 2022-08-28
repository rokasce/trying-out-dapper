using API.Contracts.Data;
using API.Database;
using API.Domain;
using API.Mapping;
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
            @"INSERT INTO Posts (Id, UserId, Title, Content, CreatedAt)
            VALUES (@Id, @UserId, @Title, @Content, @CreatedAt)",
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

    public async Task<PostDto?> GetFullAsync(Guid id)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();

        var sql = @"SELECT p.Id, p.UserId, p.Title, p.Content, p.CreatedAt, c.Id, c. UserId, c.PostId, c.Content, c.CreatedAt FROM Posts p
                    INNER JOIN Comments c ON p.Id = c.PostId 
                    WHERE p.Id = @Id";

        var postsDict = new Dictionary<Guid, PostDto>();
        var fullPost = await connection.QueryAsync<PostDto, CommentDto, PostDto>(sql,
            (post, comment) =>
            {
                if (!postsDict.TryGetValue(post.Id, out PostDto currentPost)) 
                {
                    currentPost = post;
                    postsDict.Add(post.Id, currentPost);
                }

                currentPost.Comments.Add(comment);
                return currentPost;
            },
            param: new { Id = id });

        return fullPost?.FirstOrDefault();
    }

    public async Task<IEnumerable<PostDto>> GetUserPosts(Guid userId)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection
            .QueryAsync<PostDto>(@"SELECT * FROM Posts WHERE Id = @Id", new { Id = userId });
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
