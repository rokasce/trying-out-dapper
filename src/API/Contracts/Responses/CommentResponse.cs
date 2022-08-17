namespace API.Contracts.Responses;

public class CommentResponse
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid PostId { get; init; }

    public string Content { get; init; } = default!;

    public DateTime CreatedAt { get; init; } = default!;
}
