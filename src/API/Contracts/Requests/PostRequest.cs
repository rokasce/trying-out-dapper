namespace API.Contracts.Requests;

public class PostRequest
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    public string Title { get; init; } = default!;

    public string Content { get; init; } = default!;
}
