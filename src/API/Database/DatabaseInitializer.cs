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
        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Users (
        ID UUID PRIMARY KEY,
        FullName TEXT NOT NULL,
        Email TEXT NOT NULL,
        DateOfBirth TEXT NOT NULL,
        CreatedAt TEXT NOT NULL)");

        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Posts (
        ID UUID PRIMARY KEY,
        UserId UUID NOT NULL,
        Title TEXT NOT NULL,
        Content TEXT NOT NULL,
        CreatedAt TEXT NOT NULL,
        CONSTRAINT fk_user FOREIGN KEY(UserId) REFERENCES Users(ID) ON DELETE CASCADE)");

        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Comments (
        ID UUID PRIMARY KEY,
        UserId UUID NOT NULL,
        PostId UUID NOT NULL,
        Content TEXT NOT NULL,
        CreatedAt TEXT NOT NULL,
        CONSTRAINT fk_user FOREIGN KEY(UserId) REFERENCES Users(ID) ON DELETE CASCADE,
        CONSTRAINT fk_post FOREIGN KEY(PostId) REFERENCES Posts(ID) ON DELETE CASCADE)");
    }
}
