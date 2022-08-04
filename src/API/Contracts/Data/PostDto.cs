namespace API.Contracts.Data;

public class PostDto
{
    public Guid Id { get; set; } = default!;

    public Guid UserId { get; set; } = default!;

    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;
}
