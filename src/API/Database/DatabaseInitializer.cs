using Dapper;

namespace API.Database;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync() 
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS USERS (
        ID UUID PRIMARY KEY,
        FullName TEXT NOT NULL,
        Email TEXT NOT NULL,
        DateOfBirth TEXT NOT NULL)");

        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS POSTS (
        ID UUID PRIMARY KEY,
        UserId UUID NOT NULL,
        Title TEXT NOT NULL,
        Content TEXT NOT NULL,
        CONSTRAINT fk_user FOREIGN KEY(UserId) REFERENCES USERS(ID) ON DELETE CASCADE)");
    }
}
