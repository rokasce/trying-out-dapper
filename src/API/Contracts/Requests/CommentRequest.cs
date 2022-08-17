namespace API.Contracts.Requests;

public class CommentRequest
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    public Guid PostId { get; set; } = Guid.NewGuid();

    public string Content { get; init; } = default!;
}
