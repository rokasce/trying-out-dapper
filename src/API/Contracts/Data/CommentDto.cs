namespace API.Contracts.Data;

public class CommentDto
{
    public Guid Id { get; set; } = default!;

    public Guid UserId { get; set; } = default!;

    public Guid PostId { get; set; } = default!;

    public string Content { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = default!;
}
