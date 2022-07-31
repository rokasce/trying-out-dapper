using API.Contracts.Data;
using API.Database;
using Dapper;

namespace API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(UserDto user)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO Users (Id, FullName, Email, DateOfBirth)
            VALUES (@Id, @FullName, @Email, @DateOfBirth)",
            user);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"DELETE FROM Users WHERE Id = @Id", new { Id = id });

        return result > 0;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<UserDto>("SELECT * FROM Users");
    }

    public async Task<UserDto?> GetAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<UserDto>(
            @"SELECT * FROM Users WHERE Id = @Id LIMIT 1", new { Id = id });
    }

    public async Task<bool> UpdateAsync(UserDto user)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"UPDATE Users SET FullName = @FullName, Email = @Email, DateOfBirth = @DateOfBirth", 
            user);

        return result > 0;
    }
}

