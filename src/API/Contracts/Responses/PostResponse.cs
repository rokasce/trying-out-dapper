namespace API.Contracts.Responses;

public class PostResponse
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string Title { get; init; } = default!;

    public string Content { get; init; } = default!;

    public DateTime CreatedAt { get; init; } = default!;
}
